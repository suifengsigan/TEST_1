using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SnapEx;

namespace CMMTool
{
    public partial class FrmTool : Form
    {
        public FrmTool()
        {
            InitializeComponent();
            Snap.UI.WinForm.SetApplicationIcon(this);
            Snap.UI.WinForm.ReparentForm(this);
            gridView1.FocusedRowChanged += gridView1_FocusedRowChanged;
            this.Load += FrmTool_Load;
        }

        void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle >= 0)
            {
                var dd = (gridControl1.DataSource as List<JYElecInfo>)[e.FocusedRowHandle];
                gridControlPosition.DataSource = dd.PositioningInfos;
            }
        }

        void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            //if (e.ControllerRow >= 0)
            //{
            //    var dd = (gridControl1.DataSource as List<ElecInfo>)[e.ControllerRow];
            //    gridControlPosition.DataSource = dd.GetPositioningInfos();
            //}
           
        }

        void FrmTool_Load(object sender, EventArgs e)
        {
            var theSession = NXOpen.Session.GetSession();
            var workPart = theSession.Parts.Work;
            if (workPart == null)
            {
                MessageBox.Show("未打开部件！");
                Close();
                return;
            }

            var list = new List<JYElecInfo>();
           
            Snap.Globals.WorkPart.Bodies.ToList().ForEach(u =>
            {
                if (JYElecHelper.IsElec(u) && !string.IsNullOrEmpty(u.Name))
                {
                    var lines = new List<Snap.NX.Line>();
                    var allObjects = workPart.Layers.GetAllObjectsOnLayer(u.Layer).Where(l => l is NXOpen.Line).ToList();
                    var box = u.Box;
                    var p2 = new Snap.Position((box.MaxX + u.Box.MinX) / 2, (box.MaxY + box.MinY) / 2, 0);
                    allObjects.ForEach(a =>
                    {
                        Snap.NX.Line l = a as NXOpen.Line;
                        var p1 = (l.StartPoint + l.EndPoint) / 2;
                        p2.Z = p1.Z;
                        if (SnapEx.Helper.Equals(p1, p2, JYElecHelper._tolerance))
                        {
                            lines.Add(l);
                            return;
                        }
                    });

                    var basePoint = new Snap.Position();
                    if (lines.Count > 0) 
                    {
                        var line=lines.First();
                        basePoint = (line.StartPoint + line.EndPoint) / 2;
                    }

                    var mainElec = list.FirstOrDefault(m => m.ElecName == u.Name);
                    if (mainElec == null)
                    {
                        mainElec = new JYElecInfo();
                        mainElec.ElecName = u.Name;
                        mainElec.ELEC_FINISH_NUMBER = (int)JYElecHelper.GetAttrValue(u, JYElecConst.ELEC_FINISH_NUMBER);
                        mainElec.ELEC_MIDDLE_NUMBER = (int)JYElecHelper.GetAttrValue(u, JYElecConst.ELEC_MIDDLE_NUMBER);
                        mainElec.ELEC_ROUGH_NUMBER = (int)JYElecHelper.GetAttrValue(u, JYElecConst.ELEC_ROUGH_NUMBER);
                        mainElec.ELEC_FINISH_SPACE = (double)JYElecHelper.GetAttrValue(u, JYElecConst.ELEC_FINISH_SPACE);
                        mainElec.ELEC_MIDDLE_SPACE = (double)JYElecHelper.GetAttrValue(u, JYElecConst.ELEC_MIDDLE_SPACE);
                        mainElec.ELEC_ROUGH_SPACE = (double)JYElecHelper.GetAttrValue(u, JYElecConst.ELEC_ROUGH_SPACE);
                        list.Add(mainElec);
                    }

                    if (lines.Count > 0) 
                    {
                        var pInfo = mainElec.PositioningInfos.FirstOrDefault();
                        var info = new JYPositioningInfo();
                        info.X = basePoint.X;
                        info.Y = basePoint.Y;
                        info.Z = basePoint.Z;
                        info.NXObjects.Add(u);
                        info.NXObjects.Add(lines.FirstOrDefault().NXOpenLine);

                        var infoStartPoint=lines.First().StartPoint;
                        var infoEndPoint=lines.First().EndPoint;
                        info.Lines.Add(infoStartPoint);
                        info.Lines.Add(infoEndPoint);
                        info.FaceDir = JYElecHelper.GetBaseFace(lines.First().StartPoint, lines.First().EndPoint, u).GetFaceDirection();
                        if (pInfo != null) 
                        {
                            var startPoint = pInfo.Lines.First();
                            var endPoint = pInfo.Lines.Last();

                            info.Rotation=Snap.Vector.Angle(endPoint - startPoint, infoStartPoint - infoEndPoint);
                        }
                        mainElec.PositioningInfos.Add(info);
                    }

                }
            });

            gridControl1.DataSource = list;
;            
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                var theSession = NXOpen.Session.GetSession();
                var workPart = theSession.Parts.Work;
                var infos = gridControl1.DataSource as List<JYElecInfo>;
                if (infos != null)
                {
                    infos.ForEach(u =>
                    {
                        string path = Path.Combine(Path.GetDirectoryName(workPart.FullPath), string.Format("{0}{1}", u.ElecName, Path.GetExtension(workPart.FullPath)));
                        var firstPosition = u.PositioningInfos.FirstOrDefault();
                        var lines = firstPosition.Lines;
                        var basePoint = (lines.First() + lines.Last()) / 2;
                        
                        var trans = Snap.Geom.Transform.CreateTranslation(new Snap.Position() - basePoint);
                        Snap.Vector v1 = new Snap.Vector(0, 0, 1);
                        var faceDir = firstPosition.FaceDir;
                        var angle=Snap.Vector.Angle(v1, faceDir);
                        var trans1 = Snap.Geom.Transform.CreateRotation(new Snap.Position(),new Snap.Orientation(faceDir).AxisY, angle);
                        trans = Snap.Geom.Transform.Composition(trans, trans1);
                        var c=SnapEx.Create.ExtractObject(firstPosition.NXObjects, path, true, true, trans, basePoint, new Snap.Orientation(faceDir));

                        u.PositioningInfos.ForEach(m =>
                        {
                            m.NXObjects.ForEach(n =>
                            {
                                Snap.NX.NXObject snapN = n;
                                snapN.Delete();
                            });

                            if (m != firstPosition && c != null)
                            {
                                var mLines = m.Lines;
                                var mBasePoint = (mLines.First() + mLines.Last()) / 2;
                                Snap.NX.Component snapC = c;
                                Snap.NX.Component newComponent = workPart.ComponentAssembly.CopyComponents(new List<NXOpen.Assemblies.Component> { c }.ToArray()).First();

                                var transForm = Snap.Geom.Transform.CreateTranslation(mBasePoint-snapC.Position);
                                //平移旋转
                                var tempTrans = transForm.Matrix;
                                NXOpen.Matrix3x3 matrix = new NXOpen.Matrix3x3();
                                matrix.Xx = tempTrans[0]; matrix.Xy = tempTrans[4]; matrix.Xz = tempTrans[8];
                                matrix.Yx = tempTrans[1]; matrix.Yy = tempTrans[5]; matrix.Yz = tempTrans[9];
                                matrix.Zx = tempTrans[2]; matrix.Zy = tempTrans[6]; matrix.Zz = tempTrans[10];
                                workPart.ComponentAssembly.MoveComponent(newComponent, new NXOpen.Vector3d(tempTrans[3], tempTrans[7], tempTrans[11]), matrix);
                            }
                        });
                    });
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
