using CMMTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SnapEx;
using NXOpen;
using System.IO;

namespace InitCMM
{
    /// <summary>
    /// UG_EACT
    /// </summary>
    public class BomBusiness
    {
        private BomBusiness() { }
        public readonly static BomBusiness Instance = new BomBusiness();

        /// <summary>
        /// 导出std文件
        /// </summary>
        public void ExportStp(JYElecInfo info)
        {
            var outDir = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "STP");

            if(!Directory.Exists(outDir)){Directory.CreateDirectory(outDir);}

            string path = Path.Combine(outDir, string.Format("{0}{1}", info.ElecName, ".stp"));

            if (File.Exists(path)) { File.Delete(path); }

            ExportStp(info.PositioningInfos.First().NXObjects, path);
        }

        void ExportStp(List<NXOpen.NXObject> list,string path) 
        {
            Session theSession = Session.GetSession();
            Part workPart = theSession.Parts.Work;
            Part displayPart = theSession.Parts.Display;
            Step214Creator step214Creator1;
            step214Creator1 = theSession.DexManager.CreateStep214Creator();

            step214Creator1.OutputFile = path;

            //step214Creator1.SettingsFile = "I:\\UG\\NX 9.0-64bit\\step214ug\\ugstep214.def";

            step214Creator1.ObjectTypes.Solids = true;

            step214Creator1.ExportSelectionBlock.SelectionScope = ObjectSelector.Scope.SelectedObjects;
            list.ForEach(u =>
            {
                step214Creator1.ExportSelectionBlock.SelectionComp.Add(u);
            });

            step214Creator1.FileSaveFlag = false;

            step214Creator1.LayerMask = "1-256";

            NXObject nXObject1;
            nXObject1 = step214Creator1.Commit();

            step214Creator1.Destroy();
        }

        /// <summary>
        /// 获取模仁列表
        /// </summary>
        public List<JYMouldInfo> GetMouldInfo() 
        {
            var result = new List<JYMouldInfo>();
            var workPart = Snap.Globals.WorkPart;
            workPart.Bodies.ToList().ForEach(u => {
                var MODEL_NUMBER=GetAttrValue(u, JYElecConst.MODEL_NUMBER);
                var MR_NUMBER=GetAttrValue(u, JYElecConst.MR_NUMBER);
                if (!string.IsNullOrEmpty(MR_NUMBER) && !string.IsNullOrEmpty(MODEL_NUMBER))
                {
                    var info = new JYMouldInfo();
                    info.MODEL_NUMBER = MODEL_NUMBER;
                    info.MR_NUMBER = MR_NUMBER;
                    info.MR_MATERAL = GetAttrValue(u, JYElecConst.MR_MATERAL);
                    info.MouldBody = u;
                    result.Add(info);
                }
            });
            return result;
        }

        public void GetElecInfo(JYElecInfo mainElec)
        {
            var workPart = Snap.Globals.WorkPart;
            var bodys = workPart.Bodies.Where(u => u.Name == mainElec.ElecName).ToList();
            var body = bodys.FirstOrDefault();
            var box3d = body.Box;
            mainElec.ELEC_FINISH_NUMBER = GetDoubleAttr(body, JYElecConst.ELEC_FINISH_NUMBER);
            mainElec.ELEC_MIDDLE_NUMBER = GetDoubleAttr(body, JYElecConst.ELEC_MIDDLE_NUMBER);
            mainElec.ELEC_ROUGH_NUMBER = GetDoubleAttr(body, JYElecConst.ELEC_ROUGH_NUMBER);
            mainElec.ELEC_FINISH_SPACE = GetDoubleAttr(body, JYElecConst.ELEC_FINISH_SPACE);
            mainElec.ELEC_MIDDLE_SPACE = GetDoubleAttr(body, JYElecConst.ELEC_MIDDLE_SPACE);
            mainElec.ELEC_ROUGH_SPACE = GetDoubleAttr(body, JYElecConst.ELEC_ROUGH_SPACE);
            mainElec.ELEC_MAT_NAME = GetAttrValue(body, JYElecConst.ELEC_MAT_NAME);
            mainElec.CLAMP_GENERAL_TYPE = GetAttrValue(body, JYElecConst.CLAMP_GENERAL_TYPE);
            mainElec.ELEC_MACH_TYPE = GetAttrValue(body, JYElecConst.ELEC_MACH_TYPE);
            mainElec.F_ELEC_SMOOTH = GetAttrValue(body, JYElecConst.F_ELEC_SMOOTH);
            mainElec.M_ELEC_SMOOTH = GetAttrValue(body, JYElecConst.M_ELEC_SMOOTH);
            mainElec.R_ELEC_SMOOTH = GetAttrValue(body, JYElecConst.R_ELEC_SMOOTH);
            mainElec.SPECL = Math.Abs(box3d.MaxX - box3d.MinX);
            mainElec.SPECW = Math.Abs(box3d.MaxY - box3d.MinY);
            mainElec.SPECH = Math.Abs(box3d.MaxZ - box3d.MinZ);

            mainElec.PositioningInfos.Clear();
            bodys.ForEach(u => {
                Snap.NX.Line line = GetDiagonalLine(u);
                if (line != null) 
                {
                    var basePoint = (line.StartPoint + line.EndPoint) / 2;
                    var info = new JYPositioningInfo();
                    info.X = basePoint.X;
                    info.Y = basePoint.Y;
                    info.Z = basePoint.Z;
                    info.NXObjects.Add(u);
                    info.NXObjects.Add(line.NXOpenLine);

                    var cornerface=JYElecHelper.GetBaseCornerFace(line.StartPoint,line.EndPoint,u);
                    if (cornerface != null) 
                    {
                        var type = SnapEx.Helper.GetQuadrantType(cornerface.GetFaceDirection());
                        info.C = "右上角";
                        if (type == QuadrantType.Second)
                        {
                            info.C = "左上角";
                        }
                        else if (type == QuadrantType.Three)
                        {
                            info.C = "左下角";
                        }
                        else if (type == QuadrantType.Four)
                        {
                            info.C = "右下角";
                        }
                    }

                    var infoStartPoint = line.StartPoint;
                    var infoEndPoint = line.EndPoint;
                    info.Lines.Add(infoStartPoint);
                    info.Lines.Add(infoEndPoint);
                    //info.FaceDir = JYElecHelper.GetBaseFace(line.StartPoint, line.EndPoint, u).GetFaceDirection();

                    mainElec.PositioningInfos.Add(info);
                }
               
            });

        }

        public List<JYElecInfo> GetElecList(Snap.NX.Body mouldBody) 
        {
            var result = new List<JYElecInfo>();
            var workPart = Snap.Globals.WorkPart;
            workPart.Bodies.Where(u => u.NXOpenTag != mouldBody.NXOpenTag).ToList().ForEach(u =>
            {
                var distance = Snap.Compute.Distance(mouldBody, u);
                if (distance <= JYElecHelper._tolerance
                    && result.FirstOrDefault(m => m.ElecName == u.Name) == null)
                {
                    var info = GetJYElecInfo(u);
                    if (info != null)
                    {
                        result.Add(info);
                    }
                }
            });
            return result;
        }

        /// <summary>
        /// 获取电极列表
        /// </summary>
        public List<JYElecInfo> GetElecList() 
        {
            var result = new List<JYElecInfo>();
            var workPart = Snap.Globals.WorkPart;
            workPart.Bodies.ToList().ForEach(u => {
                if (result.FirstOrDefault(m => m.ElecName == u.Name) == null) 
                {
                    var info = GetJYElecInfo(u);
                    if (info != null)
                    {
                        result.Add(info);
                    }
                }
            });
            return result;
        }

        /// <summary>
        /// 获取进玉电极信息
        /// </summary>
        public JYElecInfo GetJYElecInfo(Snap.NX.Body body)
        {
            JYElecInfo result = null;
            var diagonalLine = GetDiagonalLine(body);
            if (diagonalLine != null)
            {
                result = new JYElecInfo();
                result.ElecName = body.Name;
            }

            return result;
        }

        /// <summary>
        /// 获取对角线
        /// </summary>
        Snap.NX.Line GetDiagonalLine(Snap.NX.Body body) 
        {
            Snap.NX.Line result = null;
            var workPart = Snap.Globals.WorkPart;

            var box = body.Box;

            var p2 = new Snap.Position((box.MaxX + box.MinX) / 2, (box.MaxY + box.MinY) / 2);

            if (!string.IsNullOrEmpty(body.Name))
            {
                var lines = workPart.NXOpenPart.Layers.GetAllObjectsOnLayer(body.Layer).Where(l => l is NXOpen.Line).ToList();
                lines.ForEach(u =>
                {
                    Snap.NX.Line line = u as NXOpen.Line;
                    var p1 = (line.StartPoint + line.EndPoint) / 2;
                    p2.Z = p1.Z;
                    if (SnapEx.Helper.Equals(p1, p2, JYElecHelper._tolerance))
                    {
                        result = line;
                        return;
                    }
                });
            }

            return result;
        }

        public static double GetDoubleAttr(Snap.NX.NXObject nxObject, string title) 
        {
            return ConvertToDouble(GetAttrValue(nxObject,title));
        }

        public static double ConvertToDouble(string str)
        {
            return ConvertToDouble(str, 0);
        }

        public static double ConvertToDouble(string str, double defalut)
        {
            double.TryParse(str, out defalut);
            return defalut;
        }

        public static string GetAttrValue(Snap.NX.NXObject nxObject, string title)
        {
            string result = string.Empty;
            if (nxObject.GetAttributeInfo().Where(u => u.Title == title).Count() > 0)
            {
                var attr = nxObject.GetAttributeInfo().FirstOrDefault(u => u.Title == title);
                switch (attr.Type)
                {
                    case Snap.NX.NXObject.AttributeType.Integer:
                        {
                            result = nxObject.GetIntegerAttribute(title).ToString();
                            break;
                        }
                    case Snap.NX.NXObject.AttributeType.Real:
                        {
                            result = nxObject.GetRealAttribute(title).ToString();
                            break;
                        }
                    case Snap.NX.NXObject.AttributeType.String:
                        {
                            result = nxObject.GetStringAttribute(title);
                            break;
                        }
                }
            }
            return result;
        }
    }
}
