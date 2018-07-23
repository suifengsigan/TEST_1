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
            //if (ConfigData.ShareElec) 
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

            if (datas.Count > 0 || (shareElecDatas != null && shareElecDatas.Count > 0))
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
                        //移至绝对坐标原点
                        var baseDir = u.Electrode.BaseFace.GetFaceDirection();
                        var acsOrientation = Snap.Orientation.Identity;
                        acsOrientation.AxisZ = new Snap.Vector(0, 0, 0);
                        var wcsOrientation = new Snap.Orientation(-baseDir);
                        wcsOrientation.AxisZ = new Snap.Vector(0, 0, 0);
                        var transR = Snap.Geom.Transform.CreateRotation(acsOrientation, wcsOrientation);
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
                        SnapEx.Create.ExportPrt(u.Electrode.ElecBody, System.IO.Path.Combine(path, u.Electrode.ElecBody.Name),
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
                        if (ConfigData.IsSetPrtColor)
                        {
                            u.Electrode.InitAllFace();
                        }

                        if (showMsgHandle != null) { showMsgHandle(string.Format("正在导出Prt文件:{0}", u.Electrode.ElecBody.Name)); }
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
                        var acsOrientation = Snap.Orientation.Identity;
                        acsOrientation.AxisZ = new Snap.Vector(0, 0, 0);
                        var wcsOrientation = new Snap.Orientation(-baseDir);
                        wcsOrientation.AxisZ = new Snap.Vector(0, 0, 0);
                        var transR = Snap.Geom.Transform.CreateRotation(acsOrientation, wcsOrientation);
                        var baseDirOrientation = new Snap.Orientation(new Snap.Vector(0, 0, -1));
                        baseDirOrientation.AxisZ = new Snap.Vector(0, 0, 0);
                        var transY = Snap.Geom.Transform.CreateRotation(baseDirOrientation, new Snap.Orientation(new Snap.Vector(-1, 0, 0), new Snap.Vector(0, -1, 0), new Snap.Vector(0, 0, 0)));
                        transY = Snap.Geom.Transform.Composition(transR, transY);
                        var pos = u.Electrode.GetElecBasePos().Copy(transY);
                        var transQ = Snap.Geom.Transform.CreateRotation(u.Electrode.GetElecBasePos(), new Snap.Vector(0, 0, 1), u.C);
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
                                                if (Math.Abs(absX - absY) >= SnapEx.Helper.Tolerance && absX < absY)
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
                                }
                                var partName = GetPARTFILENAME(u.Electrode.ElecBody, steelInfo);
                                datas.Where(d => d.PARTFILENAME == partName).ToList().ForEach(d => d.REGION = isCmmRotation?"1":"0");
                                datas.Where(d => d.PARTFILENAME == partName).ToList().ForEach(d => d.DISCHARGING = area.ToString());
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

                #region
                var topFaceDir = electrode.TopFace.GetFaceDirection();
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
                var elecBox = electrodeBody.AcsToWcsBox3d(topFaceOrientation);
                var baseFaceBox = electrode.BaseFace.AcsToWcsBox3d(topFaceOrientation);
                var topFaceBox = electrode.TopFace.AcsToWcsBox3d(topFaceOrientation);
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
                        tempVar = info.FINISH_NUMBER;
                    }
                    else if (i == 1)
                    {
                        tempVar = info.MIDDLE_NUMBER;
                    }
                    else
                    {
                        tempVar = info.ROUGH_NUMBER;
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
                    positioning.QuadrantType = info.GetQuadrantType();
                    result.Add(positioning);
                }
            });

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
            var mouldBody = mouldInfo.MouldBody;
            var result = new List<ViewElecInfo>();
            var workPart = Snap.Globals.WorkPart;
            var removeBodies = new List<Snap.NX.Body>();
            removeBodies.Add(mouldBody);
            var bodies = workPart.Bodies.Where(u => u.NXOpenTag != mouldBody.NXOpenTag && !string.IsNullOrEmpty(u.Name)).ToList();
            var reg = @"\d+$";
            bodies =bodies.OrderBy(u => RegexGetInt(reg,u.Name)).ToList();
            bodies.ForEach(u =>
            {
                var distance = Snap.Compute.Distance(mouldBody, u);
                if (distance <= SnapEx.Helper.Tolerance)
                {
                    removeBodies.Add(u);
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


            //去参数
            try
            {
                SnapEx.Create.RemoveParameters(Enumerable.Select(workPart.Bodies.Where(u => u.ObjectSubType == Snap.NX.ObjectTypes.SubType.BodySolid),u=>u.NXOpenBody).ToList());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
