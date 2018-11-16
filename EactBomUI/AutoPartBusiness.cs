using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EactBom;
using SnapEx;

namespace EactBomUI
{
    /// <summary>
    /// 图档转换工具业务
    /// </summary>
    public abstract class AutoPartBusiness
    {
        public static void Start(string filename,Action<string> showMsg)
        {
            
            var part = NXOpen.Session.GetSession().Parts.Work;
            if (part != null)
            {
                Snap.NX.Part.Wrap(part.Tag).Close(true, true);
            }
            Snap.NX.Part snapPart = Snap.NX.Part.OpenPart(filename);
            var name = Path.GetFileNameWithoutExtension(filename);
            showMsg(string.Format("正在进行图档转换【{0}】", name));
            Snap.Globals.WorkPart = snapPart;
            try
            {
                EactBomBusiness.Instance.ConfigData.IsCanSelElecInBom = true;
                var bodies = Snap.Globals.WorkPart.Bodies.ToList();
                var steel = bodies.FirstOrDefault(u => u.IsHasAttr(EactBom.EactBomBusiness.EACT_MOULDBODY));
                ElecManage.MouldInfo MouldInfo = EactBom.EactBomBusiness.Instance.GetMouldInfo(steel);
                var sInsertBodies = bodies.Where(u => u.IsHasAttr(EactBom.EactBomBusiness.EACT_SINSERTBODY)).ToList();
                MouldInfo.SInsertBodies = sInsertBodies;
                MouldInfo.ElecBodies = bodies.Where(u => !(u.IsHasAttr(EactBom.EactBomBusiness.EACT_MOULDBODY) || u.IsHasAttr(EactBom.EactBomBusiness.EACT_SINSERTBODY))).ToList();
                MouldInfo.Orientation = Snap.Globals.WcsOrientation;
                ElecManage.Entry.Instance.IsDistinguishSideElec = true;
                ElecManage.Entry.Instance.DefaultQuadrantType = (QuadrantType)steel.GetAttrIntegerValue(EactBom.EactBomBusiness.EACT_DEFAULTQUADRANTTYPE);
                var list = EactBomBusiness.Instance.GetElecList(MouldInfo, showMsg);
                EactBomBusiness.Instance.ExportEact(list, MouldInfo, showMsg, EactBomBusiness.Instance.ConfigData.ExportPrt, EactBomBusiness.Instance.ConfigData.ExportStp
                    , EactBomBusiness.Instance.ConfigData.ExportCNCPrt,
                    false, true, EactBomBusiness.Instance.ConfigData.IsExportEDM
                    );
            }
            catch (Exception ex)
            {
                Console.WriteLine("AutoPartBusiness错误:{0}", ex.Message);
                throw ex;
            }
            finally
            {
                snapPart.Close(true, true);
            }
        }
    }
}
