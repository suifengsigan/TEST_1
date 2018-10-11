using ElecManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SnapEx;
using System.IO;

namespace EactBom
{
    public class EactBomBusiness
    {
        public EactConfig.ConfigData ConfigData = EactConfig.ConfigData.GetInstance();
        public const string EACT_MOULDBODY = "EACT_MOULDBODY";
        public const string EACT_SINSERTBODY = "EACT_SINSERTBODY";
        public const string Eact_AutoPrtTool = "Eact_AutoPrtTool";
        public const string Eact_AutoCMM = "Eact_AutoCMM";
        //DefaultQuadrantType
        public const string EACT_DEFAULTQUADRANTTYPE = "EACT_DEFAULTQUADRANTTYPE";
        private EactBomBusiness()
        {
            //var connStr = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}", @"192.168.1.30\SQLSERVER2014", "EACT", "sa", "Qwer1234");
            var data = ConfigData;
            var connStr = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}", data.DataBaseInfo.IP, data.DataBaseInfo.Name, data.DataBaseInfo.User, data.DataBaseInfo.Pass);
            DataAccess.Entry.Instance.Init(connStr);
            ElecManage.Entry.Instance.DefaultQuadrantType = data.QuadrantType;
        }
        public readonly static EactBomBusiness Instance = new EactBomBusiness();

        public void ExportEact(List<ViewElecInfo> infos, ElecManage.MouldInfo steelInfo, Action<string> showMsgHandle = null, bool isExportPrt = false, bool isExportStd = false, bool isExportCncPrt = false, bool isAutoPrtTool = false, bool isAutoPartBusiness=false) 
        {
            var datas = new List<DataAccess.Model.EACT_CUPRUM>();
            List<DataAccess.Model.EACT_CUPRUM_EXP> shareElecDatas = null;
            //if (ConfigData.ShareElec) 
            {
                shareElecDatas = new List<DataAccess.Model.EACT_CUPRUM_EXP>();
            }
            var positions = new List<PositioningInfo>();
            var allPositions = new List<PositioningInfo>();

            GetExportEactData(infos, steelInfo, datas, positions, allPositions, shareElecDatas, showMsgHandle);

            if (datas.Count > 0 || (shareElecDatas != null && shareElecDatas.Count > 0))
            {
                if (isAutoPrtTool)
                {
                    if (showMsgHandle != null) { showMsgHandle(string.Format("正在导出自动转换图档...")); }
                    ExportAutoPrt(steelInfo, allPositions);
                }
                else
                {
                    ExportPrt(steelInfo, datas, positions, allPositions, showMsgHandle, isExportPrt, isExportStd, isExportCncPrt);
                }

                if (!isAutoPartBusiness)
                {
                    if (ConfigData.IsExportBomXls)
                    {
                        if (showMsgHandle != null) { showMsgHandle(string.Format("正在导出物料单...")); }
                        ExcelHelper.ExportExcelBom(Enumerable.Select(positions, s => s.Electrode).ToList(), steelInfo);
                    }

                    DataAccess.BOM.ImportCuprum(datas, ConfigData.DataBaseInfo.LoginUser, steelInfo.MODEL_NUMBER, ConfigData.IsImportEman, shareElecDatas);
                }
                else
                {
                    DataAccess.BOM.UpdateCuprumDISCHARGING(datas);
                }
            }
            else
            {
                throw new Exception("未找到可导入的电极,请检查电极属性信息！");
            }
        }

        /// <summary>
        /// 设置电极属性默认值
        /// </summary>
        public void SetElecDefaultValue(ElectrodeInfo info)
        {
            var _configData = ConfigData;
            if (_configData.IsElecSetDefault)
            {
                var ent = info;
                foreach (var item in ent.GetType().GetProperties())
                {
                    var v = ((System.ComponentModel.DisplayNameAttribute[])item.GetCustomAttributes(typeof(System.ComponentModel.DisplayNameAttribute), false)).ToList();
                    if (v.Count > 0)
                    {
                        var displayName = v.First().DisplayName;
                        var poperty = _configData.Poperties.FirstOrDefault(u => u.DisplayName == displayName);
                        var rValue = (item.GetValue(ent, null) ?? string.Empty).ToString();
                        if (poperty != null && string.IsNullOrEmpty(rValue))
                        {
                            var defaultSelection =(poperty.Selections.FirstOrDefault(m => m.IsDefault))?? poperty.Selections.FirstOrDefault();
                            if (defaultSelection != null)
                            {
                                item.SetValue(ent, defaultSelection.Value, null);
                            }
                        }
                    }
                }
            }
        }

        private void GetExportEactData(List<ViewElecInfo> infos, ElecManage.MouldInfo steelInfo, List<DataAccess.Model.EACT_CUPRUM> datas, List<PositioningInfo> positions, List<PositioningInfo> allPositions, List<DataAccess.Model.EACT_CUPRUM_EXP> shareElecDatas, Action<string> showMsgHandle = null)
        {
            infos.ForEach(u => {
                if (u.Checked)
                {
                    if (showMsgHandle != null) { showMsgHandle(string.Format("正在导入电极:{0}", u.ElectName)); }
                    var tempPoss = GetPositioningInfos(u, steelInfo);
                    //检查属性是否缺失
                    var fTempPoss = tempPoss.FirstOrDefault();
                    if (fTempPoss != null)
                    {
                        var fTempPossInfo = fTempPoss.Electrode.GetElectrodeInfo();
                        if (fTempPossInfo != null && fTempPossInfo.FINISH_NUMBER == 0 && fTempPossInfo.ROUGH_NUMBER == 0 && fTempPossInfo.MIDDLE_NUMBER == 0)
                        {
                            throw new Exception(string.Format("电极【{0}】属性缺失，请检查", fTempPossInfo.Elec_Name));
                        }
                        else if (fTempPossInfo != null && (
                        (fTempPossInfo.FINISH_NUMBER != 0 && fTempPossInfo.FINISH_SPACE == 0)
                        || (fTempPossInfo.MIDDLE_NUMBER != 0 && fTempPossInfo.MIDDLE_SPACE == 0)
                        || (fTempPossInfo.ROUGH_NUMBER != 0 && fTempPossInfo.ROUGH_SPACE == 0)
                        ))
                        {
                            throw new Exception(string.Format("请检查电极【{0}】火花位信息", fTempPossInfo.Elec_Name));
                        }

                        if (ConfigData.Edition == 1&& fTempPossInfo != null)//PZ
                        {
                            if ((fTempPossInfo.KL_SIZE_LEN == 0 || fTempPossInfo.KL_SIZE_WIDTH == 0 || fTempPossInfo.KL_SIZE_HEIGHT== 0))
                            {
                                throw new Exception(string.Format("电极【{0}】开料尺寸缺失，请检查", fTempPossInfo.Elec_Name));
                            }

                            if ((fTempPossInfo.FINISH_NUMBER > 0 && string.IsNullOrEmpty(fTempPossInfo.F_SMOOTH))
                            || fTempPossInfo.MIDDLE_NUMBER > 0 && string.IsNullOrEmpty(fTempPossInfo.M_SMOOTH)
                            || fTempPossInfo.ROUGH_NUMBER > 0 && string.IsNullOrEmpty(fTempPossInfo.R_SMOOTH)
                            )
                            {
                                throw new Exception(string.Format("电极【{0}】光洁度缺失，请检查", fTempPossInfo.Elec_Name));
                            }

                            if (string.IsNullOrEmpty(fTempPossInfo.EDMROCK))
                            {
                                throw new Exception(string.Format("电极【{0}】摇摆方式缺失，请检查", fTempPossInfo.Elec_Name));
                            }

                            if (string.IsNullOrEmpty(fTempPossInfo.ELEC_CLAMP_GENERAL_TYPE))
                            {
                                throw new Exception(string.Format("电极【{0}】夹具缺失，请检查", fTempPossInfo.Elec_Name));
                            }
                        }
                    }
                    if (ConfigData.ShareElec && u.ShareElec())//共用电极
                    {
                        //var shareElec = u.ShareElecList.FirstOrDefault();
                        foreach (var shareElec in u.ShareElecList)
                        {
                            shareElecDatas.Add(new DataAccess.Model.EACT_CUPRUM_EXP
                            {
                                CUPRUMID = shareElec.CUPRUMID
                               ,
                                MODELNO = steelInfo.MODEL_NUMBER
                               ,
                                PARTNO = steelInfo.MR_NUMBER
                               ,
                                X = string.Join(",", tempPoss.Select(m => m.X.ToString()).ToArray())
                               ,
                                Y = string.Join(",", tempPoss.Select(m => m.Y.ToString()).ToArray())
                               ,
                                Z = string.Join(",", tempPoss.Select(m => m.Z.ToString()).ToArray())
                               ,
                                C = string.Join(",", tempPoss.Select(m => m.C.ToString()).ToArray())
                               ,
                                CUPRUMNAME = shareElec.CUPRUMNAME
                                ,CUPRUMSN=shareElec.CUPRUMSN
                            });
                        }
                    }
                    else
                    {
                        allPositions.AddRange(tempPoss);
                        if (tempPoss.Count > 0)
                        {
                            positions.Add(tempPoss.FirstOrDefault());
                        }
                        datas.AddRange(ExportEact(tempPoss, steelInfo));
                    }
                }
            });
        }

        private void ExportAutoPrt(ElecManage.MouldInfo steelInfo, List<PositioningInfo> allPositions)
        {
            var bodies = new List<Snap.NX.NXObject>();
            bodies.Add(steelInfo.MouldBody);
            steelInfo.SInsertBodies.ForEach(u => {
                bodies.Add(u);
            });
            allPositions.ForEach(u => {
                bodies.AddRange(u.Electrode.AllObject);
            });
            var path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, Eact_AutoPrtTool);
            if (System.IO.Directory.Exists(path))
            {
                System.IO.Directory.Delete(path, true);
            }
            System.IO.Directory.CreateDirectory(path);
            var partName = steelInfo.MODEL_NUMBER + "-" + steelInfo.MR_NUMBER + "-" + Guid.NewGuid().ToString("N");
            SnapEx.Create.ExportPrt(bodies, System.IO.Path.Combine(path, partName), () => {
                steelInfo.MouldBody.SetStringAttribute(EACT_MOULDBODY, "1");
                steelInfo.SInsertBodies.ForEach(u => {
                    u.SetStringAttribute(EACT_SINSERTBODY, "1");
                });
                return Snap.Geom.Transform.CreateTranslation();
            }, false);

            var fileName = string.Format("{0}{1}", System.IO.Path.Combine(path, partName), ".prt");
            if (System.IO.File.Exists(fileName))
            {
                //Ftp上传
                FtpUpload(Eact_AutoPrtTool, fileName);
            }
        }

        private void ExportPrt(ElecManage.MouldInfo steelInfo, List<DataAccess.Model.EACT_CUPRUM> datas, List<PositioningInfo> positions, List<PositioningInfo> allPositions, 
            Action<string> showMsgHandle = null, bool isExportPrt = false, bool isExportStd = false, bool isExportCncPrt = false)
        {
            //去参数
            positions.ForEach(u => {
                var list = Enumerable.Select(u.Electrode.ElecBody.NXOpenBody.GetFeatures(), m => Snap.NX.Feature.Wrap(m.Tag)).ToList();
                if (list.Count > 1)
                {
                    SnapEx.Create.RemoveParameters(new List<NXOpen.Body> { u.Electrode.ElecBody });
                }
            });

            var PRTFILEPath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, string.Format(@"PRTFILE"));
            if (System.IO.Directory.Exists(PRTFILEPath))
            {
                System.IO.Directory.Delete(PRTFILEPath, true);
            }

            //CNC图档
            if (isExportCncPrt)
            {
                if (showMsgHandle != null) { showMsgHandle(string.Format("正在导出CNC图档...")); }
                var path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, string.Format(@"PRTFILE\{0}\{1}", steelInfo.MODEL_NUMBER, steelInfo.MR_NUMBER));
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                positions.ForEach(u =>
                {
                    if (showMsgHandle != null) { showMsgHandle(string.Format("正在导出CNC图档:{0}", u.Electrode.ElecBody.Name)); }
                    var partName = GetPARTFILENAME(u.Electrode.ElecBody, steelInfo);
                    //移至绝对坐标原点
                    var baseDir = u.Electrode.BaseFace.GetFaceDirection();
                    var wcsOrientation = Snap.Globals.WcsOrientation; var transR = ElecManage.Electrode.GetElecTransWcsToAcs(baseDir);
                    var baseDirOrientation = new Snap.Orientation(new Snap.Vector(0, 0, -1));
                    baseDirOrientation.AxisZ = new Snap.Vector(0, 0, 0);
                    var transY = Snap.Geom.Transform.CreateRotation(baseDirOrientation, new Snap.Orientation(new Snap.Vector(-1, 0, 0), new Snap.Vector(0, -1, 0), new Snap.Vector(0, 0, 0)));
                    transY = Snap.Geom.Transform.Composition(transR, transY);
                    var topFaceUV = u.Electrode.TopFace.BoxUV;
                    var topFaceCenterPoint = u.Electrode.TopFace.Position((topFaceUV.MinU + topFaceUV.MaxU) / 2, (topFaceUV.MinV + topFaceUV.MaxV) / 2);
                    var box3d = u.Electrode.ElecBody.AcsToWcsBox3d(wcsOrientation);
                    switch (ConfigData.CNCTranRule)
                    {
                        case 2://长度矩阵Y轴（基准台底面）
                            {
                                break;
                            }
                        default:
                            {
                                topFaceCenterPoint = topFaceCenterPoint + (System.Math.Abs(box3d.MaxZ - box3d.MinZ) * baseDir);
                                break;
                            }
                    }
                    var pos = topFaceCenterPoint.Copy(transY);
                    var transQ = Snap.Geom.Transform.CreateRotation(topFaceCenterPoint, new Snap.Vector(0, 0, 1), u.C);
                    pos = pos.Copy(transQ);
                    var transX = Snap.Geom.Transform.CreateTranslation(new Snap.Position() - pos);
                    SnapEx.Create.ExportPrt(u.Electrode.ElecBody, System.IO.Path.Combine(path, partName),
                        () =>
                        {
                            var trans = Snap.Geom.Transform.CreateTranslation();
                            trans = Snap.Geom.Transform.CreateRotation(new Snap.Position(), u.Electrode.TopFace.GetFaceDirection(), 180);
                            switch (ConfigData.CNCTranRule)
                            {
                                case 1://长度矩阵
                                        {
                                        var uv = u.Electrode.BaseFace.Box;
                                        var absX = Math.Abs(uv.MaxX - uv.MinX);
                                        var absY = Math.Abs(uv.MaxY - uv.MinY);
                                        if (Math.Abs(absX - absY) >= SnapEx.Helper.Tolerance && absX < absY)
                                        {
                                            trans = Snap.Geom.Transform.Composition(trans, Snap.Geom.Transform.CreateRotation(new Snap.Position(), u.Electrode.BaseFace.GetFaceDirection(), 90));
                                        }
                                        break;
                                    }
                                case 2://长度矩阵Y轴（基准台底面）
                                        {
                                        var uv = u.Electrode.BaseFace.Box;
                                        var absX = Math.Abs(uv.MaxX - uv.MinX);
                                        var absY = Math.Abs(uv.MaxY - uv.MinY);
                                        if (absX >= absY)
                                        {
                                            trans = Snap.Geom.Transform.Composition(trans, Snap.Geom.Transform.CreateRotation(new Snap.Position(), u.Electrode.BaseFace.GetFaceDirection(), 270));
                                        }
                                        break;
                                    }

                            }
                            switch (ConfigData.FtpPathType)
                            {
                                case 1:
                                    {
                                        u.Electrode.ElecBody.Name = partName;
                                        u.Electrode.ElecBody.SetStringAttribute("EACT_ELEC_NAME", partName);
                                        break;
                                    }
                            }
                            return trans;
                        }
                        , transY, transQ, transX);

                    var fileName = string.Format("{0}{1}", System.IO.Path.Combine(path, partName), ".prt");
                    if (System.IO.File.Exists(fileName))
                    {
                        //Ftp上传
                        FtpUpload("CNC", steelInfo, fileName, partName);
                    }

                });
            }

            if (isExportPrt)
            {
                if (showMsgHandle != null) { showMsgHandle(string.Format("正在导出CMM图档...")); }
                var path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, string.Format(@"PRTFILE\{0}\{1}", steelInfo.MODEL_NUMBER, steelInfo.MR_NUMBER));
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                positions.ForEach(u =>
                {
                    var partName = GetPARTFILENAME(u.Electrode.ElecBody, steelInfo);
                    if (ConfigData.IsSetPrtColor)
                    {
                        u.Electrode.InitAllFace();
                    }

                    if (showMsgHandle != null) { showMsgHandle(string.Format("正在导出CMM图档:{0}", u.Electrode.ElecBody.Name)); }
                    var headFaces = new List<Snap.NX.Face>();
                    var topFaceDir = u.Electrode.TopFace.GetFaceDirection();
                    var baseDir = u.Electrode.BaseFace.GetFaceDirection();

                    var allPoss = allPositions.Where(m => m.Electrode.ElecBody.Name == u.Electrode.ElecBody.Name).ToList();
                    if (ConfigData.IsSetPrtColor)
                    {
                        allPoss.ForEach(h =>
                        {
                            var mark = Snap.Globals.SetUndoMark(Snap.Globals.MarkVisibility.Invisible, "EACT_EDM_ELEC_AREA");
                            try
                            {
                                if (u != h)
                                {
                                    var transR1 = Snap.Geom.Transform.CreateRotation(u.Electrode.GetElecBasePos(), topFaceDir, u.C);
                                    var transR2 = Snap.Geom.Transform.CreateRotation(u.Electrode.GetElecBasePos(), baseDir, h.C);
                                    var transM1 = Snap.Geom.Transform.CreateTranslation(h.Electrode.GetElecBasePos() - u.Electrode.GetElecBasePos());
                                    transR1 = Snap.Geom.Transform.Composition(transR1, transR2);
                                    transR1 = Snap.Geom.Transform.Composition(transR1, transM1);
                                    u.Electrode.ElecBody.Move(transR1);
                                    //u.Electrode.ElecBody.Copy().Color = System.Drawing.Color.Red;
                                }
                                u.Electrode.ElecHeadFaces.ToList().ForEach(m =>
                                {
                                    var headFace = headFaces.FirstOrDefault(f => f.NXOpenTag == m.NXOpenTag);
                                    var matchBodies = new List<Snap.NX.Body>();
                                    matchBodies.Add(steelInfo.MouldBody);
                                    matchBodies.AddRange(steelInfo.SInsertBodies);
                                    matchBodies.ForEach(mb => {
                                        if (headFace == null && Snap.Compute.Distance(m, mb) < SnapEx.Helper.Tolerance)
                                        {
                                            headFaces.Add(m);
                                        }
                                    });

                                });
                            }
                            catch (Exception ex)
                            {
                                NXOpen.UI.GetUI().NXMessageBox.Show("提示", NXOpen.NXMessageBox.DialogType.Information, ex.Message);
                            }

                            Snap.Globals.UndoToMark(mark, null);
                        });
                    }

                    //移至绝对坐标原点
                    var transR = ElecManage.Electrode.GetElecTransWcsToAcs(baseDir);
                    var baseDirOrientation = new Snap.Orientation(new Snap.Vector(0, 0, -1));
                    baseDirOrientation.AxisZ = new Snap.Vector(0, 0, 0);
                    var transY = Snap.Geom.Transform.CreateRotation(baseDirOrientation, new Snap.Orientation(new Snap.Vector(-1, 0, 0), new Snap.Vector(0, -1, 0), new Snap.Vector(0, 0, 0)));
                    transY = Snap.Geom.Transform.Composition(transR, transY);
                    var pos = u.Electrode.GetElecBasePos().Copy(transY);
                    var transQ = Snap.Geom.Transform.CreateRotation(u.Electrode.GetElecBasePos(), new Snap.Vector(0, 0, 1), u.C);
                    pos = pos.Copy(transQ);
                    var transX = Snap.Geom.Transform.CreateTranslation(new Snap.Position() - pos);
                    SnapEx.Create.ExportPrt(u.Electrode.ElecBody, System.IO.Path.Combine(path, partName),
                        () =>
                        {
                            headFaces.ForEach(m => {
                                var faceColor = System.Drawing.Color.Red;
                                try
                                {
                                    faceColor = Snap.Color.WindowsColor(ConfigData.EDMColor);
                                }
                                catch(Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                    ConfigData.EDMColor = Snap.Color.ColorIndex(faceColor);
                                }
                                m.Color = faceColor;
                            });
                            var area = 0.0;
                            var ddf = new List<NXOpen.Body>();
                            headFaces.ForEach(m =>
                            {
                                area += SnapEx.Create.GetProjectionArea(m);
                            });
                            area *= 0.5;
                            u.Electrode.ElecBody.SetStringAttribute("EACT_EDM_ELEC_AREA", area.ToString());
                            var trans = Snap.Geom.Transform.CreateTranslation();
                            bool isCmmRotation = false;
                            switch (ConfigData.EDMTranRule)
                            {
                                case 1://长度矩阵X轴
                                        {
                                        var uv = u.Electrode.BaseFace.Box;
                                        var absX = Math.Abs(uv.MaxX - uv.MinX);
                                        var absY = Math.Abs(uv.MaxY - uv.MinY);
                                        if (Math.Abs(absX - absY) >= SnapEx.Helper.Tolerance && absX < absY)
                                        {
                                            isCmmRotation = true;
                                            trans = Snap.Geom.Transform.CreateRotation(new Snap.Position(), u.Electrode.BaseFace.GetFaceDirection(), 90);
                                        }
                                        break;
                                    }
                                case 2://长度矩阵Y轴
                                        {
                                        var uv = u.Electrode.BaseFace.Box;
                                        var absX = Math.Abs(uv.MaxX - uv.MinX);
                                        var absY = Math.Abs(uv.MaxY - uv.MinY);
                                        trans = Snap.Geom.Transform.Composition(trans, Snap.Geom.Transform.CreateRotation(new Snap.Position(), u.Electrode.BaseFace.GetFaceDirection(), 180));
                                        if (Math.Abs(absX - absY) >= SnapEx.Helper.Tolerance && absX < absY)
                                        {
                                            isCmmRotation = true;
                                            trans = Snap.Geom.Transform.Composition(trans, Snap.Geom.Transform.CreateRotation(new Snap.Position(), u.Electrode.BaseFace.GetFaceDirection(), 270));
                                        }
                                        break;
                                    }
                                case 3://JR
                                        {
                                        if (ElecManage.Entry.Instance.DefaultQuadrantType == QuadrantType.First)
                                        {
                                            var uv = u.Electrode.BaseFace.Box;
                                            var absX = Math.Abs(uv.MaxX - uv.MinX);
                                            var absY = Math.Abs(uv.MaxY - uv.MinY);
                                            if ((Math.Abs(absX - absY) >= SnapEx.Helper.Tolerance && absX < absY) || (Math.Abs(absX - absY) < SnapEx.Helper.Tolerance))
                                            {
                                                isCmmRotation = true;
                                                trans = Snap.Geom.Transform.CreateRotation(new Snap.Position(), u.Electrode.BaseFace.GetFaceDirection(), -90);
                                            }
                                        }
                                        else
                                        {
                                            var uv = u.Electrode.BaseFace.Box;
                                            var absX = Math.Abs(uv.MaxX - uv.MinX);
                                            var absY = Math.Abs(uv.MaxY - uv.MinY);
                                            trans = Snap.Geom.Transform.Composition(trans, Snap.Geom.Transform.CreateRotation(new Snap.Position(), u.Electrode.BaseFace.GetFaceDirection(), 180));
                                            if (Math.Abs(absX - absY) >= SnapEx.Helper.Tolerance && absX < absY)
                                            {
                                                isCmmRotation = true;
                                                trans = Snap.Geom.Transform.Composition(trans, Snap.Geom.Transform.CreateRotation(new Snap.Position(), u.Electrode.BaseFace.GetFaceDirection(), 90));
                                            }
                                        }
                                        break;
                                    }
                                case 4://默认矩阵(沿Y轴)
                                        {
                                        trans = Snap.Geom.Transform.Composition(trans, Snap.Geom.Transform.CreateRotation(new Snap.Position(), u.Electrode.BaseFace.GetFaceDirection(), 180));
                                        break;
                                    }
                                case 5://宝讯
                                    {
                                        var uv = u.Electrode.BaseFace.Box;
                                        var absX = Math.Abs(uv.MaxX - uv.MinX);
                                        var absY = Math.Abs(uv.MaxY - uv.MinY);
                                        if (Math.Abs(absX - absY) >= SnapEx.Helper.Tolerance && absX < absY)
                                        {
                                            isCmmRotation = true;
                                            trans = Snap.Geom.Transform.CreateRotation(new Snap.Position(), u.Electrode.BaseFace.GetFaceDirection(), -90);
                                        }
                                        break;
                                    }
                            }
                            datas.Where(d => d.PARTFILENAME == partName).ToList().ForEach(d => d.REGION = isCmmRotation ? "1" : "0");
                            datas.Where(d => d.PARTFILENAME == partName).ToList().ForEach(d => d.DISCHARGING = System.Math.Round(area, 2).ToString());
                            u.Electrode.ElecBody.Name = partName;
                            u.Electrode.ElecBody.SetStringAttribute("EACT_ELEC_NAME", partName);
                            return trans;
                        }
                        , transY, transQ, transX);

                    var fileName = string.Format("{0}{1}", System.IO.Path.Combine(path, partName), ".prt");
                    if (System.IO.File.Exists(fileName))
                    {
                        //Ftp上传
                        FtpUpload("CMM", steelInfo, fileName, partName);
                        if (ConfigData.IsAutoCMM)
                        {
                            //Ftp AutoCMM上传
                            var autoCmmFileName = Path.GetFileNameWithoutExtension(fileName);
                            var autoCmmExtension = Path.GetExtension(fileName);
                            var destFileName = Path.Combine(Path.GetDirectoryName(fileName), string.Format("{0}_{1}{2}", autoCmmFileName, Guid.NewGuid().ToString("N"), autoCmmExtension));
                            File.Copy(fileName, destFileName);
                            FtpUpload(Eact_AutoCMM, destFileName);
                        }
                    }

                    if (isExportStd)
                    {
                        var stpFileName = string.Format("{0}{1}", System.IO.Path.Combine(path, partName), ".stp");
                        SnapEx.Create.ExportStp(fileName, stpFileName);
                        if (System.IO.File.Exists(stpFileName))
                        {
                            if (showMsgHandle != null) { showMsgHandle(string.Format("正在导出Stp文件...")); }
                            //Ftp上传

                            FtpUpload("CMM", steelInfo, stpFileName, partName);
                        }
                    }

                });
            }
        }

        private void FtpUpload(string type,  string fileName)
        {
            var EACTFTP = FlieFTP.Entry.GetFtp(ConfigData.FTP.Address, "", ConfigData.FTP.User, ConfigData.FTP.Pass, false);
            string sToPath = string.Format("{0}", type);
           
            if (!EACTFTP.DirectoryExist(sToPath))
            {
                EACTFTP.MakeDirPath(sToPath);
            }
            EACTFTP.NextDirectory(sToPath);
            EACTFTP.UpLoadFile(fileName);
        }


        private void FtpUpload(string type, ElecManage.MouldInfo steelInfo, string fileName, string partName)
        {
            var EACTFTP = FlieFTP.Entry.GetFtp(ConfigData.FTP.Address, "", ConfigData.FTP.User, ConfigData.FTP.Pass, false);
            string sToPath = string.Format("{0}/{1}/{2}", type, steelInfo.MODEL_NUMBER, partName);
            switch (ConfigData.FtpPathType)
            {
                case 1:
                    {
                        sToPath = string.Format("{0}/{1}", type, steelInfo.MODEL_NUMBER, partName);
                        break;
                    }
            }
            if (!EACTFTP.DirectoryExist(sToPath))
            {
                EACTFTP.MakeDirPath(sToPath);
            }

            //switch (ConfigData.FtpPathType)
            //{
            //    case 0:
            //        {
            //            EACTFTP.DeleteFtpDirWithAll(sToPath, false);
            //            break;
            //        }
            //    default:
            //        {
            //            break;
            //        }
            //}

            EACTFTP.NextDirectory(sToPath);
            EACTFTP.UpLoadFile(fileName);
        }


        List<DataAccess.Model.EACT_CUPRUM> ExportEact(List<PositioningInfo> positions, ElecManage.MouldInfo steelInfo) 
        {
            var cuprums = new List<DataAccess.Model.EACT_CUPRUM>();
            foreach (var item in positions)
            {
                var electrode = item.Electrode;
                var electrodeBody = electrode.ElecBody;
                //electrode.InitAllFace();
                //TODO 
                electrode.GetTopFace();
                electrodeBody.SetStringAttribute("EACT_ELEC", "1");
                electrodeBody.SetStringAttribute("EACT_ELEC_NAME", electrodeBody.Name);
                electrodeBody.SetStringAttribute(EactElectrodeInfo.EACT_DIE_NO_OF_WORKPIECE, steelInfo.MODEL_NUMBER);
                electrodeBody.SetStringAttribute(EactElectrodeInfo.EACT_WORKPIECE_NO_OF_WORKPIECE,steelInfo.MR_NUMBER);
                electrodeBody.SetStringAttribute("EACT_ELECT_CREATE_AIX","Z+");

                var info = electrode.GetElectrodeInfo();
                var topFaceDir = electrode.TopFace.GetFaceDirection();
                if ( !(info.EDMPROCDIRECTION == "自动判断" || string.IsNullOrEmpty(info.EDMPROCDIRECTION)))
                {
                    electrodeBody.SetStringAttribute("EACT_ELECT_CREATE_AIX", info.EDMPROCDIRECTION);
                }
                if (info.EDMPROCDIRECTION == "自动判断"|| string.IsNullOrEmpty(info.EDMPROCDIRECTION))
                {
                    var temptopFaceDir = topFaceDir;
                    var tempAixValue = "Z+";
                    if (SnapEx.Helper.Equals(temptopFaceDir, -Snap.Globals.WcsOrientation.AxisZ, SnapEx.Helper.Tolerance))
                    {
                        tempAixValue = "Z-";
                    }
                    if (SnapEx.Helper.Equals(temptopFaceDir, Snap.Globals.WcsOrientation.AxisX, SnapEx.Helper.Tolerance))
                    {
                        tempAixValue = "X+";
                    }
                    if (SnapEx.Helper.Equals(temptopFaceDir, -Snap.Globals.WcsOrientation.AxisX, SnapEx.Helper.Tolerance))
                    {
                        tempAixValue = "X-";
                    }
                    if (SnapEx.Helper.Equals(temptopFaceDir, Snap.Globals.WcsOrientation.AxisY, SnapEx.Helper.Tolerance))
                    {
                        tempAixValue = "Y+";
                    }
                    if (SnapEx.Helper.Equals(temptopFaceDir, -Snap.Globals.WcsOrientation.AxisY, SnapEx.Helper.Tolerance))
                    {
                        tempAixValue = "Y-";
                    }
                    info.EDMPROCDIRECTION = tempAixValue;
                    electrodeBody.SetStringAttribute("EACT_ELECT_CREATE_AIX", tempAixValue);
                }

                var guid = Guid.NewGuid().ToString();
                electrodeBody.SetStringAttribute("EACT_ELECT_GROUP", guid);
                var jyElec = electrode as JYElectrode;
                if (jyElec != null) 
                {
                    jyElec.DiagonalLine.SetStringAttribute("EACT_ELECT_GROUP", guid);
                }
                //var guid = Guid.NewGuid().ToString();
                //electrodeBody.SetStringAttribute("EACT_ELECT_GROUP", guid);
                //var basePoint=Snap.Create.Point(electrode.GetElecBasePos());
                //basePoint.Name = "EACT_ELECT_MID_POINT";
                //basePoint.SetStringAttribute("EACT_ELECT_GROUP", guid); ;
                electrode.TopFace.SetStringAttribute("EACT_ELEC_BASE_BOTTOM_FACE", "1");
                electrode.BaseFace.SetStringAttribute("EACT_ELEC_BASE_TOP_FACE", "1");
                electrode.TopFace.SetStringAttribute("ELEC_BASE_BOTTOM_FACE", "1");
                electrode.BaseFace.SetStringAttribute("ELEC_BASE_EDM_FACE","1");

                #region
                
                var topFaceOrientation = new Snap.Orientation(topFaceDir);
                if (false)
                {
                    var xFaces = new List<Snap.NX.Face>();
                    var yFaces = new List<Snap.NX.Face>();
                    var trans = Snap.Geom.Transform.CreateRotation(electrode.GetElecBasePos(), topFaceDir, item.C);
                    var xDir = topFaceOrientation.AxisX.Copy(trans);
                    var yDir = topFaceOrientation.AxisY.Copy(trans);
                    electrode.ElecBody.Faces.Where(u => u.ObjectSubType == Snap.NX.ObjectTypes.SubType.FacePlane).ToList().ForEach(u =>
                    {
                        if (u.IsHasAttr("EACT_ELECT_X_FACE")) { u.DeleteAttributes(Snap.NX.NXObject.AttributeType.String, "EACT_ELECT_X_FACE"); }
                        if (u.IsHasAttr("EACT_ELECT_Y_FACE")) { u.DeleteAttributes(Snap.NX.NXObject.AttributeType.String, "EACT_ELECT_Y_FACE"); }
                        if (SnapEx.Helper.Equals(xDir, u.GetFaceDirection(), SnapEx.Helper.Tolerance))
                        {
                            xFaces.Add(u);
                        }
                        else if (SnapEx.Helper.Equals(yDir, u.GetFaceDirection(), SnapEx.Helper.Tolerance))
                        {
                            yFaces.Add(u);
                        }
                    });

                    if (xFaces.Count > 0 && yFaces.Count > 0)
                    {
                        if (xFaces.Count > 1)
                        {
                            xFaces = xFaces.Where(u => Snap.Compute.Distance(u, electrode.TopFace) < SnapEx.Helper.Tolerance && Snap.Compute.Distance(u, electrode.BaseFace) < SnapEx.Helper.Tolerance).ToList();
                        }

                        if (xFaces.Count > 0)
                        {
                            xFaces.FirstOrDefault().SetStringAttribute("EACT_ELECT_X_FACE", "1");
                        }

                        if (yFaces.Count > 1)
                        {
                            yFaces = yFaces.Where(u => Snap.Compute.Distance(u, electrode.TopFace) < SnapEx.Helper.Tolerance && Snap.Compute.Distance(u, electrode.BaseFace) < SnapEx.Helper.Tolerance).ToList();
                        }

                        if (yFaces.Count > 0)
                        {
                            yFaces.FirstOrDefault().SetStringAttribute("EACT_ELECT_Y_FACE", "1");
                        }
                    }
                }

                #endregion

                if (positions.IndexOf(item) > 0) continue;

                //TODO 导BOM
                var STRETCHH = System.Math.Round(info.STRETCHH,2);
                var HEADPULLUPH = System.Math.Round(info.HEADPULLUPH,2);
                var PROCESSNUM = positions.Count.ToString();
                var X = string.Join(",", positions.Select(u => u.X.ToString()).ToArray());
                var Y = string.Join(",", positions.Select(u => u.Y.ToString()).ToArray());
                var Z = string.Join(",", positions.Select(u => u.Z.ToString()).ToArray());
                var C = string.Join(",", positions.Select(u => u.C.ToString()).ToArray());
                var partName = GetPARTFILENAME(electrodeBody, steelInfo);
              
                for (int i = 0; i < 3; i++)
                {
                    int tempVar = 0;
                    var matName = info.MAT_NAME;
                    if (i == 0) //精公
                    {
                        tempVar = info.FINISH_NUMBER;
                        matName = info.F_MAT_NAME;
                    }
                    else if (i == 1)
                    {
                        tempVar = info.MIDDLE_NUMBER;
                        matName = info.M_MAT_NAME;
                    }
                    else
                    {
                        tempVar = info.ROUGH_NUMBER;
                        matName = info.R_MAT_NAME;
                    }
                    for (int j = 0; j < tempVar; j++)
                    {
                        var cuprum = new DataAccess.Model.EACT_CUPRUM();
                        switch (i)
                        {
                            case 0:
                                cuprum.FRIENUM = info.FINISH_SPACE.ToString();
                                cuprum.RMF = "3";
                                cuprum.VDI = info.F_SMOOTH;
                                cuprum.CUPRUMNAME = string.Format("{0}-{1}", partName, ConfigData.EleFType);
                                break;

                            case 1:
                                cuprum.FRIENUM = info.MIDDLE_SPACE.ToString();
                                cuprum.RMF = "2";
                                cuprum.VDI = info.M_SMOOTH;
                                cuprum.CUPRUMNAME = string.Format("{0}-{1}", partName, ConfigData.EleMType);
                                break;
                            default:
                                cuprum.FRIENUM = info.ROUGH_SPACE.ToString();
                                cuprum.RMF = "1";
                                cuprum.VDI = info.R_SMOOTH;
                                cuprum.CUPRUMNAME = string.Format("{0}-{1}", partName, ConfigData.EleRType);
                                break;
                        }

                        cuprum.STEELMODELSN = steelInfo.MODEL_NUMBER;
                        cuprum.STEELMODULESN = steelInfo.MR_NUMBER;
                        cuprum.STEEL = steelInfo.MR_MATERAL;
                        cuprum.PROCESSNUM = PROCESSNUM;
                        cuprum.SUBSTRATECQUADRANT = ((int)item.QuadrantType).ToString();
                        cuprum.X = X;
                        cuprum.Y = Y;
                        cuprum.Z = Z;
                        cuprum.C = C;//旋转
                        cuprum.STRUFF = matName;
                        cuprum.CUPRUMSN = string.Format("{0}{1}", cuprum.CUPRUMNAME, 1 + j);
                        cuprum.PARTFILENAME = string.Format("{0}", partName); ;
                        cuprum.STRUFFTYPE = "否";
                        cuprum.PROCDIRECTION = info.EDMPROCDIRECTION;
                        cuprum.ROCK = info.EDMROCK;
                        cuprum.SHAPE = "简单有底";
                        cuprum.OPENSTRUFF = info.OPENSTRUFF;
                        cuprum.DISCHARGING = string.Empty;
                        cuprum.EDMCONDITIONSN = info.ElecCuttingSize(ConfigData.PQBlankStock,EactConfig.MatchJiaju.GetMatchJiajuValue(info.ELEC_CLAMP_GENERAL_TYPE));
                        cuprum.HEADPULLUPH = HEADPULLUPH.ToString();
                        cuprum.STRETCHH = STRETCHH.ToString();
                        cuprum.CLEARROOTH = string.Empty;
                        cuprum.CLEARROOT = string.Empty;
                        cuprum.UNIT = info.UNIT;
                        cuprum.STRUFFGROUPL = string.Empty;
                        cuprum.CHUCK = info.ELEC_CLAMP_GENERAL_TYPE;
                        cuprums.Add(cuprum);
                    }
                }


            }
            return cuprums;
        }

        public MouldInfo GetMouldInfo(Snap.NX.Body body)
        {
            var MODEL_NUMBER = body.GetAttrValue(XKElecConst.MODEL_NUMBER);
            var MR_NUMBER = body.GetAttrValue(XKElecConst.MR_NUMBER);
            var info = new MouldInfo();
            info.MODEL_NUMBER = MODEL_NUMBER;
            info.MR_NUMBER = MR_NUMBER;
            info.MR_MATERAL = body.GetAttrValue(XKElecConst.MR_MATERAL);
            info.MouldBody = body;
            return info;
        }

        /// <summary>
        /// 获取模仁列表
        /// </summary>
        public List<MouldInfo> GetMouldInfo()
        {
            var result = new List<MouldInfo>();
            var workPart = Snap.Globals.WorkPart;
            workPart.Bodies.ToList().ForEach(u =>
            {
                var info = GetMouldInfo(u);
                if (!string.IsNullOrEmpty(info.MR_NUMBER) && !string.IsNullOrEmpty(info.MODEL_NUMBER))
                {
                    result.Add(info);
                }
            });
            return result;
        }

        /// <summary>
        /// 获取跑位信息
        /// </summary>
        public List<PositioningInfo> GetPositioningInfos(ViewElecInfo elecInfo, ElecManage.MouldInfo steelInfo) 
        {
            var result = new List<PositioningInfo>();
            var workPart = Snap.Globals.WorkPart;
            elecInfo.Bodies.ToList().ForEach(u =>
            {
                var info = Electrode.GetElectrode(u);
                if (info != null)
                {
                    var positioning = new PositioningInfo();
                    positioning.Electrode = info;
                    var pos = info.GetElecBasePos();
                    pos = Snap.NX.CoordinateSystem.MapAcsToWcs(pos);
                    positioning.X = Math.Round(pos.X,4);
                    positioning.Y = Math.Round(pos.Y, 4);
                    positioning.Z = Math.Round(pos.Z, 4);
                    positioning.QuadrantType = info.GetQuadrantType(ConfigData.QuadrantType);
                    result.Add(positioning);
                    SetElecDefaultValue(info.GetElectrodeInfo());
                }
            });

            //调整顺序
            result = result.OrderBy(u => u.C).ToList();

            return result;
        }

        /// <summary>
        /// 正则表达式提取值
        /// </summary>
        public static string RegexGetValue(string rule, string str)
        {
            string result = string.Empty;
            var match = System.Text.RegularExpressions.Regex.Match(str, rule);
            if (match.Success)
            {
                result = match.Value;   
            }
            return result;
        }

        public static int RegexGetInt(string rule, string str)
        {
            int result = 0;
            int.TryParse(RegexGetValue(rule, str), out result);
            return result;
        }

        public List<ViewElecInfo> GetElecList(MouldInfo mouldInfo,Action<string> action=null) 
        {
            var workPart = Snap.Globals.WorkPart;
            //删除图纸
            try
            {
                if (ConfigData.IsDeleteDraft)
                {
                    workPart.NXOpenPart.DrawingSheets.ToArray().ToList().ForEach(u =>
                    {
                        if (action != null)
                        {
                            action(string.Format("正在删除图纸：{0}", u.Name));
                        }
                        Snap.NX.NXObject.Delete(u);
                    });
                }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //去参数
            try
            {
                if (action != null)
                {
                    action(string.Format("正在移除参数..."));
                }
                SnapEx.Create.RemoveParameters(Enumerable.Select(workPart.Bodies.Where(u => u.ObjectSubType == Snap.NX.ObjectTypes.SubType.BodySolid&&u.NXOpenBody.GetFeatures().Count()>1), u => u.NXOpenBody).ToList());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            var mouldBody = mouldInfo.MouldBody;
            var result = new List<ViewElecInfo>();
            if (ConfigData.IsCanSelElecInBom||ConfigData.isCanSelLayerInBom)
            {
                var bodies = mouldInfo.ElecBodies;
                mouldInfo.SInsertBodies.ForEach(u => {
                    bodies.RemoveAll(m => m.NXOpenTag == u.NXOpenTag);
                });
                bodies = bodies.Where(u => u.NXOpenTag != mouldBody.NXOpenTag && !string.IsNullOrEmpty(u.Name)).ToList();
                var reg = @"\d+$";
                bodies = bodies.OrderBy(u => RegexGetInt(reg, u.Name)).ToList();
                bodies.ForEach(u =>
                {
                    bool isContact = true;
                    if (isContact)
                    {
                        var elec = result.FirstOrDefault(m => m.ElectName == u.Name);
                        if (elec == null)
                        {
                            var info = Electrode.GetElectrode(u);
                            if (info != null)
                            {
                                if (action != null)
                                {
                                    action(string.Format("正在加载电极{0}", u.Name));
                                }
                                var viewInfo = new ViewElecInfo { ElectName = u.Name };
                                viewInfo.Bodies.Add(u);
                                result.Add(viewInfo);
                                elec = viewInfo;
                            }
                            else
                            {
                                if (ConfigData.isCanSelLayerInBom && ConfigData.Edition == 1)
                                {
                                    throw new Exception(string.Format("图层【{0}】中电极{1}无法识别，请检查", u.Layer, u.Name));
                                }
                            }
                        }
                        else
                        {
                            elec.Bodies.Add(u);
                        }
                    }
                });
            }
            else
            {
                var bodies = workPart.Bodies.Where(u => u.NXOpenTag != mouldBody.NXOpenTag && !string.IsNullOrEmpty(u.Name)).ToList();
                mouldInfo.SInsertBodies.ForEach(u => {
                    bodies.RemoveAll(m => m.NXOpenTag == u.NXOpenTag);
                });
                var reg = @"\d+$";
                bodies = bodies.OrderBy(u => RegexGetInt(reg, u.Name)).ToList();
                bodies.ForEach(u =>
                {
                    var distance = Snap.Compute.Distance(mouldBody, u);
                    bool isContact = distance <= SnapEx.Helper.Tolerance;
                    if (!isContact)
                    {
                        foreach (var item in mouldInfo.SInsertBodies)
                        {
                            distance = Snap.Compute.Distance(item, u);
                            isContact = distance <= SnapEx.Helper.Tolerance;
                            if (isContact) { break; }
                        }
                    }
                    if (isContact)
                    {
                        var elec = result.FirstOrDefault(m => m.ElectName == u.Name);
                        if (elec == null)
                        {
                            var info = Electrode.GetElectrode(u);
                            if (info != null)
                            {
                                if (action != null)
                                {
                                    action(string.Format("正在加载电极{0}", u.Name));
                                }
                                var viewInfo = new ViewElecInfo { ElectName = u.Name };
                                viewInfo.Bodies.Add(u);
                                result.Add(viewInfo);
                                elec = viewInfo;
                            }
                        }
                        else
                        {
                            elec.Bodies.Add(u);
                        }
                    }
                });
            }
           


            

            if (ConfigData.ShareElec) 
            {
                var list = DataAccess.BOM.GetCuprumList(Enumerable.Select(result, u => u.ElectName).ToList(), mouldInfo.MODEL_NUMBER, mouldInfo.MR_NUMBER);
                result.ForEach(u =>
                {
                    u.ShareElecList = list.Where(m => m.PARTFILENAME == u.ElectName).ToList();
                });
            }
           
            return result;
        }

        public string GetPARTFILENAME(Snap.NX.Body body, MouldInfo steelInfo)
        {
            string partName = string.Format("{0}-{1}-{2}", steelInfo.MODEL_NUMBER, steelInfo.MR_NUMBER, body.Name);
            switch (ConfigData.ElecNameRule)
            {
                case 0://A-B-C
                    {
                        partName = body.Name;
                        break;
                    }
                case 2://A(A-C)
                    {
                        partName = string.Format("{0}-{1}", steelInfo.MODEL_NUMBER, body.Name);
                        break;
                    }
                default://C
                    {
                        break;
                    }
            }
            return partName;
        }

        public double GetBodyProjectionArea(ElecManage.MouldInfo steelInfo, List<ElecManage.PositioningInfo> positions,out List<Snap.NX.Face> touchHeadFaces)
        {
            var mark = Snap.Globals.SetUndoMark(Snap.Globals.MarkVisibility.Invisible, "EACT_ELECHEADFACEAREA");
            double area = 0;
            touchHeadFaces = new List<Snap.NX.Face>();
            try
            {
                var EACT_ELECHEADFACE = string.Format("EACT_ELECHEADFACE{0}", Guid.NewGuid().ToString("N")).ToUpper();
                var headFaceInts = new List<int>();
                if (positions.Count > 0)
                {
                    var u = positions.First();
                    var elecBody = u.Electrode.ElecBody;
                    var headFaces = u.Electrode.ElecHeadFaces;
                    for (int i = 0; i < headFaces.Count; i++)
                    {
                        var hf = headFaces[i];
                        hf.SetIntegerAttribute(EACT_ELECHEADFACE, i + 1);
                    };
                    var cs = new List<Snap.NX.Component>();
                    var dir = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, string.Format(@"TEMPPRTFILE", steelInfo.MODEL_NUMBER, steelInfo.MR_NUMBER));
                    if (Directory.Exists(dir))
                    {
                        Directory.Delete(dir, true);
                    }
                    Directory.CreateDirectory(dir);
                    var path = System.IO.Path.Combine(dir, string.Format("{0}.prt", elecBody.Name));
                    var nxObjects = new List<NXOpen.NXObject> { elecBody };
                    var c = SnapEx.Create.ExtractObject(nxObjects, path, true);
                    var workPart = Snap.Globals.WorkPart.NXOpenPart;
                    cs.Add(c);

                    var baseDir = u.Electrode.BaseFace.GetFaceDirection();
                    var topFaceDir = -baseDir;
                    positions.ForEach(h =>
                    {
                        if (u != h)
                        {
                            Snap.NX.Component newComponent = workPart.ComponentAssembly.CopyComponents(new List<NXOpen.Assemblies.Component> { c }.ToArray()).First();
                            cs.Add(newComponent);
                            var transR1 = Snap.Geom.Transform.CreateRotation(u.Electrode.GetElecBasePos(), topFaceDir, u.C);
                            var transR2 = Snap.Geom.Transform.CreateRotation(u.Electrode.GetElecBasePos(), baseDir, h.C);
                            var transM1 = Snap.Geom.Transform.CreateTranslation(h.Electrode.GetElecBasePos() - u.Electrode.GetElecBasePos());
                            transR1 = Snap.Geom.Transform.Composition(transR1, transR2);
                            transR1 = Snap.Geom.Transform.Composition(transR1, transM1);
                            var trans = transR1.Matrix;
                            NXOpen.Matrix3x3 matrix = new NXOpen.Matrix3x3();
                            matrix.Xx = trans[0]; matrix.Xy = trans[4]; matrix.Xz = trans[8];
                            matrix.Yx = trans[1]; matrix.Yy = trans[5]; matrix.Yz = trans[9];
                            matrix.Zx = trans[2]; matrix.Zy = trans[6]; matrix.Zz = trans[10];
                            workPart.ComponentAssembly.MoveComponent(newComponent, new NXOpen.Vector3d(trans[3], trans[7], trans[11]), matrix);
                        }
                    });

                    var occBodies = Snap.Globals.WorkPart.BodiesByName(u.Electrode.ElecBody.Name).Where(m => m.IsOccurrence).ToList();
                    foreach (var body in occBodies)
                    {
                        if (body != null)
                        {
                            body.Faces.ToList().ForEach(f =>
                            {
                                var hfInt = f.GetAttrIntegerValue(EACT_ELECHEADFACE);
                                if (hfInt > 0 && !headFaceInts.Contains(hfInt) && Snap.Compute.Distance(f, steelInfo.MouldBody) < SnapEx.Helper.Tolerance)
                                {
                                    headFaceInts.Add(hfInt);
                                }
                            });
                        }
                    }

                    touchHeadFaces = headFaces.Where(hf => headFaceInts.Contains(hf.GetAttrIntegerValue(EACT_ELECHEADFACE))).ToList();
                    touchHeadFaces.ForEach(thf => {
                        area += SnapEx.Create.GetProjectionArea(thf, new Snap.Orientation(topFaceDir));
                    });
                    area *= 0.5;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Snap.Globals.UndoToMark(mark, null);
            }

            return area;
        }

    }
}
