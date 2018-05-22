using ElecManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SnapEx;

namespace EactBom
{
    public class EactBomBusiness
    {
        public EactConfig.ConfigData ConfigData = EactConfig.ConfigData.GetInstance();
        private EactBomBusiness()
        {
            //var connStr = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}", @"192.168.1.30\SQLSERVER2014", "EACT", "sa", "Qwer1234");
            var data = ConfigData;
            var connStr = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}", data.DataBaseInfo.IP, data.DataBaseInfo.Name, data.DataBaseInfo.User, data.DataBaseInfo.Pass);
            DataAccess.Entry.Instance.Init(connStr);
            ElecManage.Entry.Instance.DefaultQuadrantType = data.QuadrantType;
        }
        public readonly static EactBomBusiness Instance = new EactBomBusiness();

        public void ExportEact(List<ViewElecInfo> infos, ElecManage.MouldInfo steelInfo,Action<string> showMsgHandle=null,bool isExportPrt=false,bool isExportStd=false,bool isExportCncPrt=false) 
        {
            var datas = new List<DataAccess.Model.EACT_CUPRUM>();
            List<DataAccess.Model.EACT_CUPRUM_EXP> shareElecDatas = null;
            if (ConfigData.ShareElec) 
            {
                shareElecDatas = new List<DataAccess.Model.EACT_CUPRUM_EXP>();
            }
            var positions = new List<PositioningInfo>();
            var allPositions = new List<PositioningInfo>();
            infos.ForEach(u => {
                if (u.Checked) 
                {
                    if (showMsgHandle != null) { showMsgHandle(string.Format("正在导入电极:{0}", u.ElectName)); }
                    var tempPoss = GetPositioningInfos(u,steelInfo);
                    if (ConfigData.ShareElec && u.ShareElec())//共用电极
                    {
                        var shareElec = u.ShareElecList.FirstOrDefault();
                        tempPoss.ForEach(p => {
                            shareElecDatas.Add(new DataAccess.Model.EACT_CUPRUM_EXP {
                                CUPRUMID = shareElec.CUPRUMID
                                ,MODELNO = steelInfo.MODEL_NUMBER
                                ,PARTNO=steelInfo.MR_NUMBER
                                ,X=p.X.ToString()
                                ,Y=p.Y.ToString()
                                ,Z=p.Z.ToString()
                                ,C=p.C.ToString()
                                ,CUPRUMNAME=shareElec.CUPRUMNAME
                            });
                        });
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

            if (datas.Count > 0 || (shareElecDatas != null && shareElecDatas.Count > 0))
            {
                //if (isExportStd)
                //{
                //    if (showMsgHandle != null) { showMsgHandle(string.Format("正在导出Stp文件...")); }
                //    var path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, string.Format(@"STDFILE\{0}\{1}", steelInfo.MODEL_NUMBER, steelInfo.MR_NUMBER));
                //    if (!System.IO.Directory.Exists(path))
                //    {
                //        System.IO.Directory.CreateDirectory(path);
                //    }
                //    positions.ForEach(u =>
                //    {
                //        if (showMsgHandle != null) { showMsgHandle(string.Format("正在导出Stp文件:{0}", u.Electrode.ElecBody.Name)); }
                //        //移至绝对坐标原点
                //        var baseDir = u.Electrode.BaseFace.GetFaceDirection();
                //        {
                //            var transY = Snap.Geom.Transform.CreateRotation(new Snap.Orientation(baseDir), new Snap.Orientation(new Snap.Vector(0, 0, 1)));
                //            var pos = u.Electrode.GetElecBasePos().Copy(transY);
                //            var transX = Snap.Geom.Transform.CreateTranslation(new Snap.Position() - pos);
                //            SnapEx.Create.ExportStp(u.Electrode.ElecBody, System.IO.Path.Combine(path, string.Format("{0}ZPlus", u.Electrode.ElecBody.Name)), transY, transX);
                //        }

                //        {
                //            var transY = Snap.Geom.Transform.CreateRotation(new Snap.Orientation(baseDir), new Snap.Orientation(new Snap.Vector(0, 0, -1)));
                //            var pos = u.Electrode.GetElecBasePos().Copy(transY);
                //            var transX = Snap.Geom.Transform.CreateTranslation(new Snap.Position() - pos);
                //            SnapEx.Create.ExportStp(u.Electrode.ElecBody, System.IO.Path.Combine(path, string.Format("{0}Z", u.Electrode.ElecBody.Name)), transY, transX);
                //        }

                //    });
                //}

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
                        //移至绝对坐标原点
                        var baseDir = u.Electrode.BaseFace.GetFaceDirection();
                        var acsOrientation = Snap.Orientation.Identity;
                        var wcsOrientation = Snap.Globals.WcsOrientation;
                        var transR = Snap.Geom.Transform.CreateRotation(acsOrientation, wcsOrientation);
                        var baseDirOrientation = new Snap.Orientation(new Snap.Vector(0, 0, -1));
                        var transY = Snap.Geom.Transform.CreateRotation(baseDirOrientation, new Snap.Orientation(new Snap.Vector(-1, 0, 0), new Snap.Vector(0, -1, 0), new Snap.Vector(0, 0, 0)));
                        transY = Snap.Geom.Transform.Composition(transR, transY);
                        var topFaceUV = u.Electrode.TopFace.BoxUV;
                        var topFaceCenterPoint = u.Electrode.TopFace.Position((topFaceUV.MinU + topFaceUV.MaxU) / 2, (topFaceUV.MinV + topFaceUV.MaxV) / 2);
                        var box3d = u.Electrode.ElecBody.AcsToWcsBox3d();
                        topFaceCenterPoint = topFaceCenterPoint + (System.Math.Abs(box3d.MaxZ - box3d.MinZ) * baseDir);
                        var pos = topFaceCenterPoint.Copy(transY);
                        var topFaceDir = u.Electrode.TopFace.GetFaceDirection();
                        var baseFaceOrientation = new Snap.Orientation(topFaceDir);
                        var transQ = Snap.Geom.Transform.CreateRotation(topFaceCenterPoint, topFaceDir, u.C);
                        pos = pos.Copy(transQ);
                        var transX = Snap.Geom.Transform.CreateTranslation(new Snap.Position() - pos);
                        SnapEx.Create.ExportPrt(u.Electrode.ElecBody, System.IO.Path.Combine(path, u.Electrode.ElecBody.Name),
                            () =>
                            {
                                var trans = Snap.Geom.Transform.CreateTranslation();
                                trans = Snap.Geom.Transform.CreateRotation(new Snap.Position(), u.Electrode.TopFace.GetFaceDirection(), 180);
                                switch (ConfigData.CNCTranRule)
                                {
                                    case 1://长度矩阵
                                        {
                                            var uv = u.Electrode.BaseFace.BoxUV;
                                            if (Math.Abs(uv.MaxU - uv.MinU) < Math.Abs(uv.MaxV - uv.MinV))
                                            {
                                                trans = Snap.Geom.Transform.Composition(trans,Snap.Geom.Transform.CreateRotation(new Snap.Position(), u.Electrode.BaseFace.GetFaceDirection(), 90));
                                            }
                                        }
                                        break;
                                }
                                return trans;
                            }
                            , transY, transQ, transX);

                        var fileName = string.Format("{0}{1}", System.IO.Path.Combine(path, u.Electrode.ElecBody.Name), ".prt");
                        if (System.IO.File.Exists(fileName))
                        {
                            //Ftp上传
                            var EACTFTP = FlieFTP.Entry.GetFtp(ConfigData.FTP.Address, "", ConfigData.FTP.User, ConfigData.FTP.Pass, false);
                            string sToPath = string.Format("{0}/{1}/{2}", "CNC", steelInfo.MODEL_NUMBER, GetPARTFILENAME(u.Electrode.ElecBody, steelInfo));
                            if (!EACTFTP.DirectoryExist(sToPath))
                            {
                                EACTFTP.MakeDirPath(sToPath);
                            }
                            EACTFTP.DeleteFtpDirWithAll(sToPath, false);
                            EACTFTP.NextDirectory(sToPath);
                            EACTFTP.UpLoadFile(fileName);
                        }
                       
                    });
                }

                if (isExportPrt)
                {
                    if (showMsgHandle != null) { showMsgHandle(string.Format("正在导出Prt文件...")); }
                    var path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, string.Format(@"PRTFILE\{0}\{1}", steelInfo.MODEL_NUMBER, steelInfo.MR_NUMBER));
                    if (!System.IO.Directory.Exists(path))
                    {
                        System.IO.Directory.CreateDirectory(path);
                    }
                    positions.ForEach(u =>
                    {
                        u.Electrode.InitAllFace();
                        if (showMsgHandle != null) { showMsgHandle(string.Format("正在导出Prt文件:{0}", u.Electrode.ElecBody.Name)); }
                        var headFaces = new List<Snap.NX.Face>();
                        var topFaceDir = u.Electrode.TopFace.GetFaceDirection();
                        var baseDir = u.Electrode.BaseFace.GetFaceDirection();

                        var allPoss = allPositions.Where(m => m.Electrode.ElecBody.Name == u.Electrode.ElecBody.Name).ToList();
                        allPoss.ForEach(h =>
                        {
                            var mark = Snap.Globals.SetUndoMark(Snap.Globals.MarkVisibility.Visible, "EACT_EDM_ELEC_AREA");
                            try
                            {
                                if (u != h) 
                                {
                                    var transR1 = Snap.Geom.Transform.CreateRotation(u.Electrode.GetElecBasePos(), topFaceDir, u.C);
                                    var transR2 = Snap.Geom.Transform.CreateRotation(u.Electrode.GetElecBasePos(), baseDir, h.C);
                                    var transM1 = Snap.Geom.Transform.CreateTranslation(h.Electrode.GetElecBasePos() - u.Electrode.GetElecBasePos());
                                    u.Electrode.ElecBody.Move(transR1);
                                    u.Electrode.ElecBody.Move(transR2);
                                    u.Electrode.ElecBody.Move(transM1);
                                    //u.Electrode.ElecBody.Copy().Color = System.Drawing.Color.Red;
                                }
                                u.Electrode.ElecHeadFaces.ToList().ForEach(m =>
                                {
                                    var headFace = headFaces.FirstOrDefault(f => f.NXOpenTag == m.NXOpenTag);
                                    if (headFace == null && Snap.Compute.Distance(m, steelInfo.MouldBody) < SnapEx.Helper.Tolerance)
                                    {
                                        headFaces.Add(m);
                                    }
                                });
                            }
                            catch (Exception ex)
                            {
                                NXOpen.UI.GetUI().NXMessageBox.Show("提示", NXOpen.NXMessageBox.DialogType.Information, ex.Message);
                            }

                            Snap.Globals.UndoToMark(mark, null);
                        });


                        //移至绝对坐标原点
                        var acsOrientation = Snap.Orientation.Identity;
                        var wcsOrientation = Snap.Globals.WcsOrientation;
                        var transR = Snap.Geom.Transform.CreateRotation(acsOrientation, wcsOrientation);
                        var baseDirOrientation = new Snap.Orientation(new Snap.Vector(0, 0, -1));
                        var transY = Snap.Geom.Transform.CreateRotation(baseDirOrientation, new Snap.Orientation(new Snap.Vector(-1, 0, 0), new Snap.Vector(0, -1, 0), new Snap.Vector(0, 0, 0)));
                        transY = Snap.Geom.Transform.Composition(transR, transY);
                        var pos = u.Electrode.GetElecBasePos().Copy(transY);
                        var baseFaceOrientation = new Snap.Orientation(topFaceDir);
                        var transQ = Snap.Geom.Transform.CreateRotation(u.Electrode.GetElecBasePos(), topFaceDir, u.C);
                        pos = pos.Copy(transQ);
                        var transX = Snap.Geom.Transform.CreateTranslation(new Snap.Position() - pos);
                        SnapEx.Create.ExportPrt(u.Electrode.ElecBody, System.IO.Path.Combine(path, u.Electrode.ElecBody.Name),
                            () =>
                            {
                                headFaces.ForEach(m => { m.Color = System.Drawing.Color.FromArgb(ConfigData.EDMColor); });
                                var area = 0.0;
                                var ddf = new List<NXOpen.Body>();
                                headFaces.ForEach(m =>
                                {
                                    area += SnapEx.Create.GetProjectionArea(m);
                                });
                                area *= 0.5;
                                u.Electrode.ElecBody.SetStringAttribute("EACT_EDM_ELEC_AREA", area.ToString());
                                var trans = Snap.Geom.Transform.CreateTranslation();
                                switch (ConfigData.EDMTranRule)
                                {
                                    case 1://长度矩阵
                                        {
                                            var uv = u.Electrode.BaseFace.BoxUV;
                                            if (Math.Abs(uv.MaxU - uv.MinU) < Math.Abs(uv.MaxV - uv.MinV))
                                            {
                                                trans = Snap.Geom.Transform.CreateRotation(new Snap.Position(), u.Electrode.BaseFace.GetFaceDirection(), 90);
                                            }
                                        }
                                        break;
                                }
                                return trans;
                            }
                            , transY, transQ, transX);

                        var fileName = string.Format("{0}{1}", System.IO.Path.Combine(path, u.Electrode.ElecBody.Name), ".prt");
                        if (System.IO.File.Exists(fileName))
                        {
                            //Ftp上传
                            var EACTFTP = FlieFTP.Entry.GetFtp(ConfigData.FTP.Address, "", ConfigData.FTP.User, ConfigData.FTP.Pass, false);
                            string sToPath = string.Format("{0}/{1}/{2}", "CMM", steelInfo.MODEL_NUMBER, GetPARTFILENAME(u.Electrode.ElecBody, steelInfo));
                            if (!EACTFTP.DirectoryExist(sToPath))
                            {
                                EACTFTP.MakeDirPath(sToPath);
                            }
                            EACTFTP.DeleteFtpDirWithAll(sToPath, false);
                            EACTFTP.NextDirectory(sToPath);
                            EACTFTP.UpLoadFile(fileName);
                        }

                        if (isExportStd)
                        {
                            var stpFileName = string.Format("{0}{1}", System.IO.Path.Combine(path, u.Electrode.ElecBody.Name), ".stp");
                            SnapEx.Create.ExportStp(fileName, stpFileName);
                            if (System.IO.File.Exists(stpFileName))
                            {
                                if (showMsgHandle != null) { showMsgHandle(string.Format("正在导出Stp文件...")); }
                                //Ftp上传
                                var EACTFTP = FlieFTP.Entry.GetFtp(ConfigData.FTP.Address, "", ConfigData.FTP.User, ConfigData.FTP.Pass, false);
                                string sToPath = string.Format("{0}/{1}/{2}", "CMM_PROG", steelInfo.MODEL_NUMBER, GetPARTFILENAME(u.Electrode.ElecBody, steelInfo));
                                if (!EACTFTP.DirectoryExist(sToPath))
                                {
                                    EACTFTP.MakeDirPath(sToPath);
                                }
                                EACTFTP.DeleteFtpDirWithAll(sToPath, false);
                                EACTFTP.NextDirectory(sToPath);
                                EACTFTP.UpLoadFile(stpFileName);
                            }
                        }
                       
                    });
                }

                DataAccess.BOM.ImportCuprum(datas, ConfigData.DataBaseInfo.LoginUser, steelInfo.MODEL_NUMBER,ConfigData.IsImportEman, shareElecDatas);
            }
            else
            {
                throw new Exception("未找到可导入的电极,请检查电极属性信息！");
            }
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
                electrodeBody.SetStringAttribute("EACT_DIE_NO_OF_WORKPIECE",steelInfo.MODEL_NUMBER);
                electrodeBody.SetStringAttribute("EACT_WORKPIECE_NO_OF_WORKPIECE",steelInfo.MR_NUMBER);
                electrodeBody.SetStringAttribute("EACT_ELECT_CREATE_AIX","Z+");
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

                var baseFaceDir = electrode.TopFace.GetFaceDirection();
                var baseFaceOrientation = new Snap.Orientation(baseFaceDir);
                var xFaces = new List<Snap.NX.Face>();
                var yFaces = new List<Snap.NX.Face>();
                var trans = Snap.Geom.Transform.CreateRotation(electrode.GetElecBasePos(), baseFaceDir, item.C);
                var xDir = baseFaceOrientation.AxisX.Copy(trans);
                var yDir = baseFaceOrientation.AxisY.Copy(trans);
                electrode.ElecBody.Faces.Where(u=>u.ObjectSubType==Snap.NX.ObjectTypes.SubType.FacePlane).ToList().ForEach(u =>
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

                if (positions.IndexOf(item) > 0) continue;

                //TODO 导BOM
                var elecBox = electrodeBody.Box;
                var baseFaceBox = electrode.BaseFace.Box;
                var topFaceBox = electrode.TopFace.Box;
                var STRETCHH = Math.Abs(elecBox.MinZ - elecBox.MaxZ);
                var HEADPULLUPH = Math.Abs(STRETCHH - Math.Abs((baseFaceBox.MinZ + baseFaceBox.MaxZ) / 2 - (topFaceBox.MinZ + topFaceBox.MaxZ) / 2));
                var PROCESSNUM = positions.Count.ToString();
                var X = string.Join(",", positions.Select(u => u.X.ToString()).ToArray());
                var Y = string.Join(",", positions.Select(u => u.Y.ToString()).ToArray());
                var Z = string.Join(",", positions.Select(u => u.Z.ToString()).ToArray());
                var C = string.Join(",", positions.Select(u => u.C.ToString()).ToArray());
                var partName = GetPARTFILENAME(electrodeBody, steelInfo);
                var info = electrode.GetElectrodeInfo();
                for (int i = 0; i < 3; i++)
                {
                    int tempVar = 0;
                    if (i == 0) //精公
                    {
                        int.TryParse(info.FINISH_NUMBER, out tempVar);
                    }
                    else if (i == 1)
                    {
                        int.TryParse(info.MIDDLE_NUMBER, out tempVar);
                    }
                    else
                    {
                        int.TryParse(info.ROUGH_NUMBER, out tempVar);
                    }
                    for (int j = 0; j < tempVar; j++)
                    {
                        var cuprum = new DataAccess.Model.EACT_CUPRUM();
                        switch (i)
                        {
                            case 0:
                                cuprum.FRIENUM = info.FINISH_SPACE;
                                cuprum.RMF = "3";
                                cuprum.VDI = info.F_SMOOTH;
                                cuprum.CUPRUMNAME = string.Format("{0}-{1}", partName, "F");
                                break;

                            case 1:
                                cuprum.FRIENUM = info.MIDDLE_SPACE;
                                cuprum.RMF = "2";
                                cuprum.VDI = info.M_SMOOTH;
                                cuprum.CUPRUMNAME = string.Format("{0}-{1}", partName, "M");
                                break;
                            default:
                                cuprum.FRIENUM = info.ROUGH_SPACE;
                                cuprum.RMF = "1";
                                cuprum.VDI = info.R_SMOOTH;
                                cuprum.CUPRUMNAME = string.Format("{0}-{1}", partName, "R");
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
                        cuprum.STRUFF = info.MAT_NAME;
                        cuprum.CUPRUMSN = string.Format("{0}{1}", cuprum.CUPRUMNAME, 1 + j);
                        cuprum.PARTFILENAME = string.Format("{0}", partName); ;
                        cuprum.STRUFFTYPE = "否";
                        cuprum.PROCDIRECTION = info.EDMPROCDIRECTION;
                        cuprum.ROCK = info.EDMROCK;
                        cuprum.SHAPE = "简单有底";
                        cuprum.OPENSTRUFF = info.OPENSTRUFF;
                        cuprum.DISCHARGING = string.Empty;
                        cuprum.EDMCONDITIONSN = info.ElecSize;
                        cuprum.HEADPULLUPH = STRETCHH.ToString();
                        cuprum.STRETCHH = HEADPULLUPH.ToString();
                        cuprum.CLEARROOTH = string.Empty;
                        cuprum.CLEARROOT = string.Empty;
                        cuprum.UNIT = info.UNIT;
                        cuprum.STRUFFGROUPL = string.Empty;
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
                    positioning.QuadrantType = info.GetQuadrantType();
                    result.Add(positioning);
                }
            });

            return result;
        }

        public List<ViewElecInfo> GetElecList(MouldInfo mouldInfo,Action<string> action=null) 
        {
            var mouldBody = mouldInfo.MouldBody;
            var result = new List<ViewElecInfo>();
            var workPart = Snap.Globals.WorkPart;
            workPart.Bodies.Where(u => u.NXOpenTag != mouldBody.NXOpenTag&&!string.IsNullOrEmpty(u.Name)).ToList().ForEach(u =>
            {
                var distance = Snap.Compute.Distance(mouldBody, u);
                if (distance <= SnapEx.Helper.Tolerance)
                {
                    var elec = result.FirstOrDefault(m => m.ElectName == u.Name);
                    if (elec == null)
                    {
                        var info = Electrode.GetElectrode(u);
                        if (info != null)
                        {
                            if (action != null) {
                                action(u.Name);
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

        string GetPARTFILENAME(Snap.NX.Body body, MouldInfo steelInfo)
        {
            string partName = string.Format("{0}-{1}-{2}", steelInfo.MODEL_NUMBER, steelInfo.MR_NUMBER, body.Name);
            switch (ConfigData.ElecNameRule)
            {
                case 0://A-B-C
                    {
                        partName = body.Name;
                        break;
                    }
                default://C
                    {
                        break;
                    }
            }
            return partName;
        }

       
    }
}
