using NXOpen;
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

namespace InitCMM
{
    public partial class FrmTest : Form
    {
        public FrmTest()
        {
            InitializeComponent();
            Init();
        }

        void Init()
        {
            Snap.UI.WinForm.SetApplicationIcon(this);
            Snap.UI.WinForm.ReparentForm(this);
            Text = "设计初始化";
        }

        void InitCMM(string partFileName) 
        {
            var theSession = Session.GetSession();
            var fileNew1 = theSession.Parts.FileNew();



            BasePart basePart1;
            PartLoadStatus partLoadStatus1;
            basePart1 = theSession.Parts.OpenBase(partFileName, out partLoadStatus1);

            partLoadStatus1.Dispose();
            Snap.NX.Part tempPart = basePart1 as Part;
            var EACT_ELEC_BASE_TOP_FACE = "EACT_ELEC_BASE_TOP_FACE";
            var EACT_ELEC_BASE_BOTTOM_FACE = "EACT_ELEC_BASE_BOTTOM_FACE";
            //EACT_ELEC_BASE_TOP_FACE
            if (tempPart != null) 
            {
                var body=tempPart.Bodies.FirstOrDefault();
                var midPoint = new Snap.Position();
                var bottomPoint = new Snap.Position();
                body.Faces.ToList().ForEach(u => {
                    if (u.GetAttributeInfo().Where(a => a.Title == EACT_ELEC_BASE_TOP_FACE).Count() > 0)
                    {
                        var boxUV = u.BoxUV;
                        midPoint = u.Position((boxUV.MaxU + boxUV.MinU) / 2, (boxUV.MaxV + boxUV.MinV) / 2);

                    }
                    else if (u.GetAttributeInfo().Where(a => a.Title == EACT_ELEC_BASE_BOTTOM_FACE).Count() > 0)
                    {
                        var boxUV = u.BoxUV;
                        bottomPoint = u.Position((boxUV.MaxU + boxUV.MinU) / 2, (boxUV.MaxV + boxUV.MinV) / 2);

                    }
                });

                body.Move(Snap.Geom.Transform.CreateTranslation(midPoint - bottomPoint));
            }


            fileNew1.TemplateFileName = "${UGII_INSPECTION_TEMPLATE_PART_METRIC_DIR}insp_general_assy.prt";

            fileNew1.ApplicationName = "InspectionTemplate";

            fileNew1.Units = NXOpen.Part.Units.Millimeters;

            fileNew1.RelationType = "";

            fileNew1.UsesMasterModel = "Yes";

            fileNew1.TemplateType = FileNewTemplateType.Item;

            fileNew1.NewFileName = "C:\\Users\\PENGHUI\\Desktop\\PHWork\\AB005-101-02_inspection_setup_1.prt";

            fileNew1.MasterFileName = Path.GetFileNameWithoutExtension(partFileName);

            fileNew1.UseBlankTemplate = false;

            fileNew1.MakeDisplayedPart = true;

            NXObject nXObject1;
            nXObject1 = fileNew1.Commit();

            fileNew1.Destroy();



            Part workPart = theSession.Parts.Work;
            Part displayPart = theSession.Parts.Display;
            theSession.CreateInspectionSession();

            NXOpen.SIM.KinematicConfigurator kinematicConfigurator1;
            kinematicConfigurator1 = workPart.CreateKinematicConfigurator();


            NXOpen.CAM.InspectionGroup inspectionGroup1 = (NXOpen.CAM.InspectionGroup)workPart.InspectionSetup.CmmInspectionGroupCollection.FindObject("GENERIC_MACHINE");
            NXOpen.CAM.MachineGroupBuilder machineGroupBuilder1;
            machineGroupBuilder1 = workPart.InspectionSetup.CmmInspectionGroupCollection.CreateMachineGroupBuilder(inspectionGroup1);


            NXOpen.CAM.NcmctPartMountingBuilder ncmctPartMountingBuilder1;
            ncmctPartMountingBuilder1 = workPart.InspectionSetup.CreateNcmctPartMountingBuilder("brown_sharpe");

            ncmctPartMountingBuilder1.Positioning = NXOpen.CAM.NcmctPartMountingBuilder.PositioningTypes.UseAssemblyPositioning;

            SelectNXObjectList selectNXObjectList1;
            selectNXObjectList1 = ncmctPartMountingBuilder1.Geometry;

            // ----------------------------------------------
            //   对话开始 部件安装
            // ----------------------------------------------
            ncmctPartMountingBuilder1.Positioning = NXOpen.CAM.NcmctPartMountingBuilder.PositioningTypes.UsePartMountJunction;

            ncmctPartMountingBuilder1.LayerOptions = NXOpen.CAM.NcmctPartMountingBuilder.LayerTypes.OriginalMakeVisible;

            Point3d origin1 = new Point3d(0.0, 0.0, 0.0);
            Vector3d xDirection1 = new Vector3d(1.0, 0.0, 0.0);
            Vector3d yDirection1 = new Vector3d(0.0, 1.0, 0.0);
            Xform xform1;
            xform1 = workPart.Xforms.CreateXform(origin1, xDirection1, yDirection1, NXOpen.SmartObject.UpdateOption.AfterModeling, 1.0);

            CartesianCoordinateSystem cartesianCoordinateSystem1;
            cartesianCoordinateSystem1 = workPart.CoordinateSystems.CreateCoordinateSystem(xform1, NXOpen.SmartObject.UpdateOption.AfterModeling);

            ncmctPartMountingBuilder1.PartMountJunction = cartesianCoordinateSystem1;

            var dd=ncmctPartMountingBuilder1.Commit();


            machineGroupBuilder1.RemoveMachine();

            machineGroupBuilder1.UpdateCamSetup(NXOpen.CAM.MachineGroupBuilder.RetrieveToolPocketInformation.Yes, ncmctPartMountingBuilder1);

            ncmctPartMountingBuilder1.Destroy();
            machineGroupBuilder1.Destroy();

            //测座
            workPart.InspectionSetup.RetrieveDevice("Renishaw_PH10M");

            //测针
            bool success1;
            workPart.InspectionSetup.RetrieveTool("SP25_SH2_A-5003-0063-01-A", out success1);

            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "部件文件|*.prt";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var file = dialog.FileName;
                InitCMM(file);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Session theSession = Session.GetSession();
            Part workPart = theSession.Parts.Work;
            Part displayPart = theSession.Parts.Display;
            NXOpen.CAM.CAMObject nullCAM_CAMObject = null;
            NXOpen.CAM.InspectionCollisionAvoidanceBuilder inspectionCollisionAvoidanceBuilder1;
            inspectionCollisionAvoidanceBuilder1 = workPart.InspectionSetup.CmmInspectionOperationCollection.CreateInspectionCollisionAvoidanceBuilder(nullCAM_CAMObject);

            string[] selectedops1 = new string[1];
            selectedops1[0] = "PATH_INSP_FEAT_PLANE";
            inspectionCollisionAvoidanceBuilder1.SetSelectedOps(selectedops1);

            inspectionCollisionAvoidanceBuilder1.ChangeProbeTip = false;

            inspectionCollisionAvoidanceBuilder1.ChangeApproachRetract = false;

            inspectionCollisionAvoidanceBuilder1.ProbeRotationsAtSafePlane = false;

            inspectionCollisionAvoidanceBuilder1.TableRotationsAtSafePlane = false;

            inspectionCollisionAvoidanceBuilder1.ToolChangesAtSafePlane = false;

            inspectionCollisionAvoidanceBuilder1.TransitionsBetweenPaths = false;

            inspectionCollisionAvoidanceBuilder1.TransitionsWithinPaths = false;

            inspectionCollisionAvoidanceBuilder1.NewSensors = false;

            inspectionCollisionAvoidanceBuilder1.StartFromSafePlane = false;

            inspectionCollisionAvoidanceBuilder1.FinishAtSafePlane = false;

            inspectionCollisionAvoidanceBuilder1.ChangeProbeAngles = false;

            inspectionCollisionAvoidanceBuilder1.ListOutput = false;

            inspectionCollisionAvoidanceBuilder1.DoCollisionAvoidance();

            

            var obj = inspectionCollisionAvoidanceBuilder1.Commit();

            inspectionCollisionAvoidanceBuilder1.Destroy();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Session theSession = Session.GetSession();
            Part workPart = theSession.Parts.Work;
            Part displayPart = theSession.Parts.Display;
          

            UI theUI = UI.GetUI();

            NXOpen.CAM.InspectionCreatePathsBuilder inspectionCreatePathsBuilder1;
            inspectionCreatePathsBuilder1 = workPart.InspectionSetup.CmmInspectionOperationCollection.CreateInspectionCreatePathsBuilder((NXOpen.CAM.InspectionOperation)theUI.SelectionManager.GetSelectedObject(0));

            inspectionCreatePathsBuilder1.ProgramLocationString = "INSPECTION_PATHS";

            NXOpen.Assemblies.Component component1 = (NXOpen.Assemblies.Component)workPart.ComponentAssembly.RootComponent.FindObject("COMPONENT AB005-101-ASM 1");
            Face face1 = (Face)component1.FindObject("PROTO#.Features|UNPARAMETERIZED_FEATURE(2)|FACE 1 {(-2,-79.9999999999999,20.5) UNPARAMETERIZED_FEATURE(2)}");
            bool added1;

            //选择特征和探针
            added1 = inspectionCreatePathsBuilder1.SelectFeatures.Add(face1);
            inspectionCreatePathsBuilder1.ToolString = "SP25_SH2_A-5003-0063-01-A";

            NXOpen.Session.UndoMarkId markId4;
            markId4 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "创建刀轨");

            inspectionCreatePathsBuilder1.ToolString = "SP25_SH2_A-5003-0063-01-A";

            inspectionCreatePathsBuilder1.Setup = workPart.InspectionSetup;

            NXOpen.CAM.InspectionGroup inspectionGroup1 = (NXOpen.CAM.InspectionGroup)workPart.InspectionSetup.CmmInspectionGroupCollection.FindObject("INSPECTION_PATHS");
            inspectionCreatePathsBuilder1.ProgramGroup = inspectionGroup1;

            NXOpen.CAM.InspectionGroup inspectionGroup2 = (NXOpen.CAM.InspectionGroup)workPart.InspectionSetup.CmmInspectionGroupCollection.FindObject("GEOMETRY");
            inspectionCreatePathsBuilder1.GeometryGroup = inspectionGroup2;

            NXOpen.CAM.InspectionGroup inspectionGroup3 = (NXOpen.CAM.InspectionGroup)workPart.InspectionSetup.CmmInspectionGroupCollection.FindObject("SENSORS");
            inspectionCreatePathsBuilder1.SensorGroup = inspectionGroup3;

            NXOpen.CAM.InspectionTool inspectionTool1 = (NXOpen.CAM.InspectionTool)workPart.InspectionSetup.CmmInspectionGroupCollection.FindObject("SP25_SH2_A-5003-0063-01-A");
            inspectionCreatePathsBuilder1.ToolGroup = inspectionTool1;

            inspectionCreatePathsBuilder1.TypeName = "cmm_metric_template";

            string[] selectedfeatures1 = new string[1];
            selectedfeatures1[0] = "INSP_FEAT_PLANE";
            inspectionCreatePathsBuilder1.SetSelectedFeatures(selectedfeatures1);

            NXOpen.CAM.InspectionGroup[] featuremethods1 = new NXOpen.CAM.InspectionGroup[1];
            NXOpen.CAM.InspectionGroup inspectionGroup4 = (NXOpen.CAM.InspectionGroup)workPart.InspectionSetup.CmmInspectionGroupCollection.FindObject("INSP-PLANE");
            featuremethods1[0] = inspectionGroup4;
            inspectionCreatePathsBuilder1.SetFeatureMethods(featuremethods1);

            inspectionCreatePathsBuilder1.CreatePaths();
    
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                Session theSession = Session.GetSession();
                Part workPart = theSession.Parts.Work;
                Part displayPart = theSession.Parts.Display;

                NXOpen.CAM.InspectionGroup inspectionGroup1 = (NXOpen.CAM.InspectionGroup)workPart.InspectionSetup.CmmInspectionGroupCollection.FindObject("FEATURES");
                NXOpen.CAM.InspectionGroup inspectionGroup2 = (NXOpen.CAM.InspectionGroup)workPart.InspectionSetup.CmmInspectionGroupCollection.FindObject("INSP-PLANE");
                NXOpen.CAM.InspectionGroup inspectionGroup3 = (NXOpen.CAM.InspectionGroup)workPart.InspectionSetup.CmmInspectionGroupCollection.FindObject("X-CEL_122010");
                NXOpen.CAM.InspectionGroup inspectionGroup4 = (NXOpen.CAM.InspectionGroup)workPart.InspectionSetup.CmmInspectionGroupCollection.FindObject("GEOMETRY");
                NXOpen.CAM.InspectionOperation inspectionOperation1;
                inspectionOperation1 = workPart.InspectionSetup.CmmInspectionOperationCollection.Create(inspectionGroup1, inspectionGroup2, inspectionGroup3, inspectionGroup4, "cmm_metric_template", "INSP_FEAT_PLANE", NXOpen.CAM.OperationCollection.UseDefaultName.False, "INSP_FEAT_PLANE_1");

                NXOpen.CAM.InspectionPlaneFeatureBuilder inspectionPlaneFeatureBuilder1;
                inspectionPlaneFeatureBuilder1 = workPart.InspectionSetup.CmmInspectionOperationCollection.CreateInspectionPlaneFeatureBuilder(inspectionOperation1);

                NXOpen.Assemblies.Component component1 = (NXOpen.Assemblies.Component)workPart.ComponentAssembly.RootComponent.FindObject("COMPONENT AB005-101-ASM 1");
                Face face1 = (Face)component1.FindObject("PROTO#.Features|UNPARAMETERIZED_FEATURE(2)|FACE 5 {(97,2.5000000000001,20.5) UNPARAMETERIZED_FEATURE(2)}");
                bool added1;
                added1 = inspectionPlaneFeatureBuilder1.SelectedPlane.Add(face1);

                inspectionPlaneFeatureBuilder1.ExtentType = NXOpen.CAM.CamInspectionOperationExtenttypes.Bounded;

                inspectionPlaneFeatureBuilder1.UpdateParams();

                NXObject nXObject1;
                nXObject1 = inspectionPlaneFeatureBuilder1.Commit();

                inspectionPlaneFeatureBuilder1.Destroy();
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Session theSession = Session.GetSession();
            Part workPart = theSession.Parts.Work;
            Part displayPart = theSession.Parts.Display;
            NXOpen.Session.UndoMarkId markId1;
            markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Create Inspection Measurement Path");

            UI theUI = UI.GetUI();

            NXOpen.CAM.InspectionGroup inspectionGroup1 = (NXOpen.CAM.InspectionGroup)workPart.InspectionSetup.CmmInspectionGroupCollection.FindObject("INSP-PLANE");
            NXOpen.CAM.InspectionTool inspectionTool1 = (NXOpen.CAM.InspectionTool)workPart.InspectionSetup.CmmInspectionGroupCollection.FindObject("SP25_SH2_A-5003-0063-01-A");
            NXOpen.CAM.InspectionGroup inspectionGroup2 = (NXOpen.CAM.InspectionGroup)workPart.InspectionSetup.CmmInspectionGroupCollection.FindObject("GEOMETRY");
            NXOpen.CAM.InspectionOperation inspectionOperation1;
            inspectionOperation1 = workPart.InspectionSetup.CmmInspectionOperationCollection.Create((NXOpen.CAM.InspectionGroup)theUI.SelectionManager.GetSelectedObject(0), inspectionGroup1, inspectionTool1, inspectionGroup2, "cmm_metric_template", "INSP_PATH", NXOpen.CAM.OperationCollection.UseDefaultName.False, "INSP_PATH");

            workPart.InspectionSetup.CmmInspectionOperationCollection.SetPathFeature(inspectionOperation1, "INSP_FEAT_PLANE");

            NXOpen.Session.UndoMarkId markId2;
            markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "开始");

            NXOpen.CAM.InspectionPathBuilder inspectionPathBuilder1;
            inspectionPathBuilder1 = workPart.InspectionSetup.CmmInspectionOperationCollection.CreateInspectionPathBuilder(inspectionOperation1);

            double[] direction1;
            direction1 = inspectionPathBuilder1.GetApproachDirection();

            double[] direction2 = new double[3];
            direction2[0] = 4.62592926927149e-016;
            direction2[1] = 1.0;
            direction2[2] = -0.0;
            inspectionPathBuilder1.SetApproachDirection(direction2);

            NXOpen.CAM.InspectionGroup inspectionGroup3 = (NXOpen.CAM.InspectionGroup)workPart.InspectionSetup.CmmInspectionGroupCollection.FindObject("SENSORS");
            NXOpen.CAM.InspectionGroup inspectionGroup4 = (NXOpen.CAM.InspectionGroup)workPart.InspectionSetup.CmmInspectionGroupCollection.FindObject("METHOD");
            NXOpen.CAM.InspectionGroup inspectionGroup5 = (NXOpen.CAM.InspectionGroup)workPart.InspectionSetup.CmmInspectionGroupCollection.FindObject("X-CEL_122010");
            inspectionPathBuilder1.UpdateSensors(workPart.InspectionSetup, inspectionGroup3, inspectionGroup4, inspectionGroup5, inspectionGroup2, "cmm_metric_template");

            theSession.SetUndoMarkName(markId2, "检测刀轨 对话框");

            NXOpen.Session.UndoMarkId markId3;
            markId3 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "检测刀轨");

            theSession.DeleteUndoMark(markId3, null);

            NXOpen.Session.UndoMarkId markId4;
            markId4 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "检测刀轨");

            NXObject nXObject1;
            nXObject1 = inspectionPathBuilder1.Commit();

            inspectionPathBuilder1.UpdateSensors(workPart.InspectionSetup, inspectionGroup3, inspectionGroup4, inspectionGroup5, inspectionGroup2, "cmm_metric_template");

            theSession.DeleteUndoMark(markId4, null);

            theSession.SetUndoMarkName(markId2, "检测刀轨");

            inspectionPathBuilder1.Destroy();

            theSession.DeleteUndoMark(markId2, null);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Session theSession = Session.GetSession();
            Part workPart = theSession.Parts.Work;
            Part displayPart = theSession.Parts.Display;
            // ----------------------------------------------
            //   菜单：工具->检测导航器->操作->Link to PMI...
            // ----------------------------------------------
            NXOpen.Session.UndoMarkId markId1;
            markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "开始");

            NXOpen.CAM.CAMObject nullCAM_CAMObject = null;
            NXOpen.CAM.InspectionLinkPmiBuilder inspectionLinkPmiBuilder1;
            inspectionLinkPmiBuilder1 = workPart.InspectionSetup.CmmInspectionOperationCollection.CreateInspectionLinkPmiBuilder(nullCAM_CAMObject);

            inspectionLinkPmiBuilder1.WorkpieceString = "Cam Workpiece";

            inspectionLinkPmiBuilder1.ViewString = "All";

            inspectionLinkPmiBuilder1.ToolString = "自动";

            inspectionLinkPmiBuilder1.TipString = "自动";

            inspectionLinkPmiBuilder1.AngleString = "Auto";

            inspectionLinkPmiBuilder1.ProgramLocationString = "INSPECTION_PATHS";

            theSession.SetUndoMarkName(markId1, "链接 PMI 对话框");

            inspectionLinkPmiBuilder1.ToolString = "SP25_SH2_A-5003-0063-01-A";

            NXOpen.Session.UndoMarkId markId2;
            markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "链接 PMI");

            theSession.DeleteUndoMark(markId2, null);

            NXOpen.Session.UndoMarkId markId3;
            markId3 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "链接 PMI");

            inspectionLinkPmiBuilder1.CreatePathsEnum = NXOpen.CAM.InspectionLinkPmiBuilder.CreatePathsOptions.Yes;

            inspectionLinkPmiBuilder1.LinkToPmi();

            var dd = inspectionLinkPmiBuilder1.GetResults().ToList();

            inspectionLinkPmiBuilder1.OutputResults(NXOpen.ListingWindow.DeviceType.Window, "");

            theSession.DeleteUndoMark(markId3, null);

            theSession.SetUndoMarkName(markId1, "链接 PMI");

            inspectionLinkPmiBuilder1.Destroy();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Session theSession = Session.GetSession();
            Part workPart = theSession.Parts.Work;
            Part displayPart = theSession.Parts.Display;
            // ----------------------------------------------
            //   菜单：插入->Sensor...
            // ----------------------------------------------
            NXOpen.Session.UndoMarkId markId1;
            markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Create Inspection Sensor");

            NXOpen.CAM.InspectionGroup inspectionGroup1 = (NXOpen.CAM.InspectionGroup)workPart.InspectionSetup.CmmInspectionGroupCollection.FindObject("SENSORS");
            NXOpen.CAM.InspectionGroup inspectionGroup2 = (NXOpen.CAM.InspectionGroup)workPart.InspectionSetup.CmmInspectionGroupCollection.FindObject("METHOD");
            NXOpen.CAM.InspectionGroup inspectionGroup3 = (NXOpen.CAM.InspectionGroup)workPart.InspectionSetup.CmmInspectionGroupCollection.FindObject("X-CEL_122010");
            NXOpen.CAM.InspectionGroup inspectionGroup4 = (NXOpen.CAM.InspectionGroup)workPart.InspectionSetup.CmmInspectionGroupCollection.FindObject("GEOMETRY");
            NXOpen.CAM.InspectionOperation inspectionOperation1;
            inspectionOperation1 = workPart.InspectionSetup.CmmInspectionOperationCollection.Create(inspectionGroup1, inspectionGroup2, inspectionGroup3, inspectionGroup4, "cmm_metric_template", "INSP_SENSOR", NXOpen.CAM.OperationCollection.UseDefaultName.False, "SENSOR_1");

            NXOpen.Session.UndoMarkId markId2;
            markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "开始");

            NXOpen.CAM.InspectionSensorBuilder inspectionSensorBuilder1;
            inspectionSensorBuilder1 = workPart.InspectionSetup.CmmInspectionOperationCollection.CreateInspectionSensorBuilder(inspectionOperation1);

            theSession.SetUndoMarkName(markId2, "传感器定义 对话框");

            inspectionSensorBuilder1.ToolName = "SP25_SH2_A-5003-0063-01-A";

            inspectionSensorBuilder1.TipNumber = 0;

            NXOpen.Session.UndoMarkId markId3;
            markId3 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "传感器定义");

            theSession.DeleteUndoMark(markId3, null);

            NXOpen.Session.UndoMarkId markId4;
            markId4 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "传感器定义");

            NXObject nXObject1;
            nXObject1 = inspectionSensorBuilder1.Commit();

            theSession.DeleteUndoMark(markId4, null);

            theSession.SetUndoMarkName(markId2, "传感器定义");

            inspectionSensorBuilder1.Destroy();

            theSession.DeleteUndoMark(markId2, null);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Session theSession = Session.GetSession();
            Part workPart = theSession.Parts.Work;
            Part displayPart = theSession.Parts.Display;
            // ----------------------------------------------
            //   菜单：PMI->Datum Feature Symbol...
            // ----------------------------------------------
            NXOpen.Session.UndoMarkId markId1;
            markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "开始");

            NXOpen.Annotations.Datum nullAnnotations_Datum = null;
            NXOpen.Annotations.PmiDatumFeatureSymbolBuilder pmiDatumFeatureSymbolBuilder1;
            pmiDatumFeatureSymbolBuilder1 = workPart.Annotations.Datums.CreatePmiDatumFeatureSymbolBuilder(nullAnnotations_Datum);

            pmiDatumFeatureSymbolBuilder1.Letter = "A";

            pmiDatumFeatureSymbolBuilder1.Origin.SetInferRelativeToGeometry(true);

            pmiDatumFeatureSymbolBuilder1.Origin.Anchor = NXOpen.Annotations.OriginBuilder.AlignmentPosition.MidCenter;

            theSession.SetUndoMarkName(markId1, "基准特征符号 对话框");

            pmiDatumFeatureSymbolBuilder1.Origin.Plane.PlaneMethod = NXOpen.Annotations.PlaneBuilder.PlaneMethodType.ModelView;

            pmiDatumFeatureSymbolBuilder1.Origin.SetInferRelativeToGeometry(true);

            NXOpen.Annotations.LeaderData leaderData1;
            leaderData1 = workPart.Annotations.CreateLeaderData();

            leaderData1.StubSize = 5.0;

            leaderData1.Arrowhead = NXOpen.Annotations.LeaderData.ArrowheadType.FilledArrow;

            pmiDatumFeatureSymbolBuilder1.Leader.Leaders.Append(leaderData1);

            leaderData1.TerminatorType = NXOpen.Annotations.LeaderData.LeaderType.Datum;

            leaderData1.Arrowhead = NXOpen.Annotations.LeaderData.ArrowheadType.FilledDatum;

            leaderData1.StubSide = NXOpen.Annotations.LeaderSide.Inferred;

            NXOpen.Annotations.DimensionUnit dimensionlinearunits1;
            dimensionlinearunits1 = pmiDatumFeatureSymbolBuilder1.Style.UnitsStyle.DimensionLinearUnits;

            NXOpen.Annotations.DimensionUnit dimensionlinearunits2;
            dimensionlinearunits2 = pmiDatumFeatureSymbolBuilder1.Style.UnitsStyle.DimensionLinearUnits;

            NXOpen.Annotations.DimensionUnit dimensionlinearunits3;
            dimensionlinearunits3 = pmiDatumFeatureSymbolBuilder1.Style.UnitsStyle.DimensionLinearUnits;

            NXOpen.Annotations.DimensionUnit dimensionlinearunits4;
            dimensionlinearunits4 = pmiDatumFeatureSymbolBuilder1.Style.UnitsStyle.DimensionLinearUnits;

            NXOpen.Annotations.DimensionUnit dimensionlinearunits5;
            dimensionlinearunits5 = pmiDatumFeatureSymbolBuilder1.Style.UnitsStyle.DimensionLinearUnits;

            NXOpen.Annotations.DimensionUnit dimensionlinearunits6;
            dimensionlinearunits6 = pmiDatumFeatureSymbolBuilder1.Style.UnitsStyle.DimensionLinearUnits;

            NXOpen.Annotations.DimensionUnit dimensionlinearunits7;
            dimensionlinearunits7 = pmiDatumFeatureSymbolBuilder1.Style.UnitsStyle.DimensionLinearUnits;

            NXOpen.Annotations.DimensionUnit dimensionlinearunits8;
            dimensionlinearunits8 = pmiDatumFeatureSymbolBuilder1.Style.UnitsStyle.DimensionLinearUnits;

            NXOpen.Annotations.DimensionUnit dimensionlinearunits9;
            dimensionlinearunits9 = pmiDatumFeatureSymbolBuilder1.Style.UnitsStyle.DimensionLinearUnits;

            NXOpen.Annotations.DimensionUnit dimensionlinearunits10;
            dimensionlinearunits10 = pmiDatumFeatureSymbolBuilder1.Style.UnitsStyle.DimensionLinearUnits;

            pmiDatumFeatureSymbolBuilder1.Origin.SetInferRelativeToGeometry(true);

            pmiDatumFeatureSymbolBuilder1.Origin.SetInferRelativeToGeometry(true);

            NXOpen.Annotations.Annotation.AssociativeOriginData assocOrigin1;
            assocOrigin1.OriginType = NXOpen.Annotations.AssociativeOriginType.Drag;
            NXOpen.View nullView = null;
            assocOrigin1.View = nullView;
            assocOrigin1.ViewOfGeometry = nullView;
            NXOpen.Point nullPoint = null;
            assocOrigin1.PointOnGeometry = nullPoint;
            assocOrigin1.VertAnnotation = null;
            assocOrigin1.VertAlignmentPosition = NXOpen.Annotations.AlignmentPosition.TopLeft;
            assocOrigin1.HorizAnnotation = null;
            assocOrigin1.HorizAlignmentPosition = NXOpen.Annotations.AlignmentPosition.TopLeft;
            assocOrigin1.AlignedAnnotation = null;
            assocOrigin1.DimensionLine = 0;
            assocOrigin1.AssociatedView = nullView;
            assocOrigin1.AssociatedPoint = nullPoint;
            assocOrigin1.OffsetAnnotation = null;
            assocOrigin1.OffsetAlignmentPosition = NXOpen.Annotations.AlignmentPosition.TopLeft;
            assocOrigin1.XOffsetFactor = 0.0;
            assocOrigin1.YOffsetFactor = 0.0;
            assocOrigin1.StackAlignmentPosition = NXOpen.Annotations.StackAlignmentPosition.Above;
            pmiDatumFeatureSymbolBuilder1.Origin.SetAssociativeOrigin(assocOrigin1);

            Point3d point1 = new Point3d(49.546611857756, 54.8906579436626, -9.33362639585766);
            pmiDatumFeatureSymbolBuilder1.Origin.Origin.SetValue(null, nullView, point1);

            pmiDatumFeatureSymbolBuilder1.Origin.SetInferRelativeToGeometry(true);

            NXOpen.Session.UndoMarkId markId2;
            markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "基准特征符号");

            NXObject nXObject1;
            nXObject1 = pmiDatumFeatureSymbolBuilder1.Commit();

            theSession.DeleteUndoMark(markId2, null);

            theSession.SetUndoMarkName(markId1, "基准特征符号");

            theSession.SetUndoMarkVisibility(markId1, null, NXOpen.Session.MarkVisibility.Visible);

            pmiDatumFeatureSymbolBuilder1.Destroy();

            NXOpen.Session.UndoMarkId markId3;
            markId3 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Start");

            NXOpen.Annotations.PmiDatumFeatureSymbolBuilder pmiDatumFeatureSymbolBuilder2;
            pmiDatumFeatureSymbolBuilder2 = workPart.Annotations.Datums.CreatePmiDatumFeatureSymbolBuilder(nullAnnotations_Datum);

            pmiDatumFeatureSymbolBuilder2.Letter = "B";

            pmiDatumFeatureSymbolBuilder2.Origin.SetInferRelativeToGeometry(true);

            pmiDatumFeatureSymbolBuilder2.Origin.Anchor = NXOpen.Annotations.OriginBuilder.AlignmentPosition.MidCenter;

            pmiDatumFeatureSymbolBuilder2.Style.LineArrowStyle.TextToLineDistance = 0.0;

            theSession.SetUndoMarkName(markId3, "基准特征符号 对话框");

            pmiDatumFeatureSymbolBuilder2.Origin.Plane.PlaneMethod = NXOpen.Annotations.PlaneBuilder.PlaneMethodType.ModelView;

            pmiDatumFeatureSymbolBuilder2.Origin.SetInferRelativeToGeometry(true);

            NXOpen.Annotations.LeaderData leaderData2;
            leaderData2 = workPart.Annotations.CreateLeaderData();

            leaderData2.StubSize = 5.0;

            leaderData2.Arrowhead = NXOpen.Annotations.LeaderData.ArrowheadType.FilledArrow;

            pmiDatumFeatureSymbolBuilder2.Leader.Leaders.Append(leaderData2);

            leaderData2.TerminatorType = NXOpen.Annotations.LeaderData.LeaderType.Datum;

            leaderData2.Arrowhead = NXOpen.Annotations.LeaderData.ArrowheadType.FilledDatum;

            leaderData2.StubSide = NXOpen.Annotations.LeaderSide.Inferred;

            NXOpen.Annotations.DimensionUnit dimensionlinearunits11;
            dimensionlinearunits11 = pmiDatumFeatureSymbolBuilder2.Style.UnitsStyle.DimensionLinearUnits;

            NXOpen.Annotations.DimensionUnit dimensionlinearunits12;
            dimensionlinearunits12 = pmiDatumFeatureSymbolBuilder2.Style.UnitsStyle.DimensionLinearUnits;

            NXOpen.Annotations.DimensionUnit dimensionlinearunits13;
            dimensionlinearunits13 = pmiDatumFeatureSymbolBuilder2.Style.UnitsStyle.DimensionLinearUnits;

            NXOpen.Annotations.DimensionUnit dimensionlinearunits14;
            dimensionlinearunits14 = pmiDatumFeatureSymbolBuilder2.Style.UnitsStyle.DimensionLinearUnits;

            NXOpen.Annotations.DimensionUnit dimensionlinearunits15;
            dimensionlinearunits15 = pmiDatumFeatureSymbolBuilder2.Style.UnitsStyle.DimensionLinearUnits;

            NXOpen.Annotations.DimensionUnit dimensionlinearunits16;
            dimensionlinearunits16 = pmiDatumFeatureSymbolBuilder2.Style.UnitsStyle.DimensionLinearUnits;

            NXOpen.Annotations.DimensionUnit dimensionlinearunits17;
            dimensionlinearunits17 = pmiDatumFeatureSymbolBuilder2.Style.UnitsStyle.DimensionLinearUnits;

            NXOpen.Annotations.DimensionUnit dimensionlinearunits18;
            dimensionlinearunits18 = pmiDatumFeatureSymbolBuilder2.Style.UnitsStyle.DimensionLinearUnits;

            NXOpen.Annotations.DimensionUnit dimensionlinearunits19;
            dimensionlinearunits19 = pmiDatumFeatureSymbolBuilder2.Style.UnitsStyle.DimensionLinearUnits;

            NXOpen.Annotations.DimensionUnit dimensionlinearunits20;
            dimensionlinearunits20 = pmiDatumFeatureSymbolBuilder2.Style.UnitsStyle.DimensionLinearUnits;

            pmiDatumFeatureSymbolBuilder2.Origin.SetInferRelativeToGeometry(true);

            pmiDatumFeatureSymbolBuilder2.Origin.SetInferRelativeToGeometry(true);

            NXOpen.Annotations.DimensionUnit dimensionlinearunits21;
            dimensionlinearunits21 = pmiDatumFeatureSymbolBuilder2.Style.UnitsStyle.DimensionLinearUnits;

            NXOpen.Annotations.DimensionUnit dimensionlinearunits22;
            dimensionlinearunits22 = pmiDatumFeatureSymbolBuilder2.Style.UnitsStyle.DimensionLinearUnits;

            NXOpen.Annotations.DimensionUnit dimensionlinearunits23;
            dimensionlinearunits23 = pmiDatumFeatureSymbolBuilder2.Style.UnitsStyle.DimensionLinearUnits;

            pmiDatumFeatureSymbolBuilder2.Destroy();

            theSession.UndoToMark(markId3, null);

            theSession.DeleteUndoMark(markId3, null);

            NXOpen.Session.UndoMarkId markId4;
            markId4 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "开始");

            NXOpen.Annotations.Datum datum1 = (NXOpen.Annotations.Datum)nXObject1;
            NXOpen.Annotations.PmiDatumFeatureSymbolBuilder pmiDatumFeatureSymbolBuilder3;
            pmiDatumFeatureSymbolBuilder3 = workPart.Annotations.Datums.CreatePmiDatumFeatureSymbolBuilder(datum1);

            theSession.SetUndoMarkName(markId4, "基准特征符号 对话框");

            pmiDatumFeatureSymbolBuilder3.Origin.SetInferRelativeToGeometry(true);

            NXOpen.Annotations.LeaderData leaderData3;
            leaderData3 = workPart.Annotations.CreateLeaderData();

            leaderData3.StubSize = 5.0;

            leaderData3.Arrowhead = NXOpen.Annotations.LeaderData.ArrowheadType.FilledArrow;

            pmiDatumFeatureSymbolBuilder3.Leader.Leaders.Append(leaderData3);

            leaderData3.TerminatorType = NXOpen.Annotations.LeaderData.LeaderType.Datum;

            leaderData3.StubSide = NXOpen.Annotations.LeaderSide.Inferred;

            leaderData3.Arrowhead = NXOpen.Annotations.LeaderData.ArrowheadType.FilledDatum;

            NXOpen.Annotations.DimensionUnit dimensionlinearunits24;
            dimensionlinearunits24 = pmiDatumFeatureSymbolBuilder3.Style.UnitsStyle.DimensionLinearUnits;

            NXOpen.Annotations.DimensionUnit dimensionlinearunits25;
            dimensionlinearunits25 = pmiDatumFeatureSymbolBuilder3.Style.UnitsStyle.DimensionLinearUnits;

            NXOpen.Annotations.DimensionUnit dimensionlinearunits26;
            dimensionlinearunits26 = pmiDatumFeatureSymbolBuilder3.Style.UnitsStyle.DimensionLinearUnits;

            pmiDatumFeatureSymbolBuilder3.Style.OrdinateStyle.DoglegCreationOption = NXOpen.Annotations.OrdinateDoglegCreationOption.No;

            pmiDatumFeatureSymbolBuilder3.Origin.SetInferRelativeToGeometry(true);

            pmiDatumFeatureSymbolBuilder3.Origin.SetInferRelativeToGeometry(true);

            NXOpen.Session.UndoMarkId markId5;
            markId5 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "基准特征符号");

            // ----------------------------------------------
            //   对话开始 基准特征符号
            // ----------------------------------------------
            theSession.DeleteUndoMark(markId5, null);

            NXOpen.Session.UndoMarkId markId6;
            markId6 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "基准特征符号");

            NXOpen.Assemblies.Component component1 = (NXOpen.Assemblies.Component)workPart.ComponentAssembly.RootComponent.FindObject("COMPONENT AB005-101-ASM 1");
            Face face1 = (Face)component1.FindObject("PROTO#.Features|UNPARAMETERIZED_FEATURE(2)|FACE 5 {(97,2.5000000000001,20.5) UNPARAMETERIZED_FEATURE(2)}");
            Point3d point2 = new Point3d(97.0, 32.3461773259121, 23.7551994724711);
            leaderData3.Leader.SetValue(face1, workPart.ModelingViews.WorkView, point2);

            bool added1;
            added1 = pmiDatumFeatureSymbolBuilder3.AssociatedObjects.Nxobjects.Add(face1);

            Face face2 = (Face)component1.FindObject("PROTO#.Features|UNPARAMETERIZED_FEATURE(2)|FACE 5 {(97,2.5000000000001,20.5) UNPARAMETERIZED_FEATURE(2)}");
            Point3d point3 = new Point3d(97.0, 32.3461773259121, 23.7551994724711);
            leaderData3.Leader.SetValue(face2, nullView, point3);

            theSession.SetUndoMarkName(markId6, "基准特征符号 - 选择终止对象");

            theSession.SetUndoMarkVisibility(markId6, null, NXOpen.Session.MarkVisibility.Visible);

            theSession.SetUndoMarkVisibility(markId4, null, NXOpen.Session.MarkVisibility.Invisible);

            NXOpen.Session.UndoMarkId markId7;
            markId7 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "基准特征符号");

            NXOpen.Session.UndoMarkId markId8;
            markId8 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "基准特征符号");

            NXObject nXObject2;
            nXObject2 = pmiDatumFeatureSymbolBuilder3.Commit();

            theSession.DeleteUndoMark(markId8, null);

            theSession.SetUndoMarkName(markId4, "基准特征符号");

            pmiDatumFeatureSymbolBuilder3.Destroy();

            theSession.DeleteUndoMark(markId7, null);

            theSession.SetUndoMarkVisibility(markId4, null, NXOpen.Session.MarkVisibility.Visible);

            theSession.DeleteUndoMark(markId6, null);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Session theSession = Session.GetSession();
            Part workPart = theSession.Parts.Work;
            Part displayPart = theSession.Parts.Display;
            //检测路径
            NXOpen.CAM.InspectionOperation inspectionOperation1 = (NXOpen.CAM.InspectionOperation)workPart.InspectionSetup.CmmInspectionOperationCollection.FindObject("INSP_PATH");
            NXOpen.CAM.InspectionMoveSubop inspectionMoveSubop1 = (NXOpen.CAM.InspectionMoveSubop)inspectionOperation1.CmmInspectionMoveCollection.FindObject("Point_Set");
            NXOpen.CAM.InspectionUVGridBuilder inspectionUVGridBuilder1;
            //创建点集
            inspectionUVGridBuilder1 = inspectionOperation1.CmmInspectionMoveCollection.CreateInspectionUvgridBuilder(inspectionMoveSubop1);
        }


        /// <summary>
        /// 创建程序
        /// </summary>
        private void button10_Click(object sender, EventArgs e)
        {
            Session theSession = Session.GetSession();
            Part workPart = theSession.Parts.Work;
            Snap.NX.Part snapWorkPart=workPart;
            Part displayPart = theSession.Parts.Display;

            //进入CAM模块
            theSession.CreateCamSession();
            //初始化CAM
            var cAMSetup1 = workPart.CreateCamSetup("mill_contour");
            //切换到加工模块
            UI.GetUI().MenuBarManager.ApplicationSwitchRequest("UG_APP_MANUFACTURING");

            #region 创建程序
            var nCGroup1 = (NXOpen.CAM.NCGroup)workPart.CAMSetup.CAMGroupCollection.FindObject("NC_PROGRAM");
            var PROGRAM_PH = workPart.CAMSetup.CAMGroupCollection.CreateProgram(nCGroup1, "mill_contour", "PROGRAM", NXOpen.CAM.NCGroupCollection.UseDefaultName.False, "PROGRAM_PH");
            #endregion

            #region 定义加工对象
            //设置机床坐标系
            var orientGeometry1 = (NXOpen.CAM.OrientGeometry)workPart.CAMSetup.CAMGroupCollection.FindObject("MCS_MILL");
            nCGroup1 = workPart.CAMSetup.CAMGroupCollection.CreateGeometry(orientGeometry1, "mill_contour", "MCS", NXOpen.CAM.NCGroupCollection.UseDefaultName.False, "MCS_PH");


            var millOrientGeomBuilder1 = workPart.CAMSetup.CAMGroupCollection.CreateMillOrientGeomBuilder(nCGroup1);
            //设置安全平面
            Point3d origin1 = new Point3d(0.0, 0.0, 0.0);
            Vector3d normal1 = new Vector3d(0.0, 0.0, 1.0);
            Plane plane1;
            plane1 = workPart.Planes.CreatePlane(origin1, normal1, NXOpen.SmartObject.UpdateOption.AfterModeling);
           
            millOrientGeomBuilder1.TransferClearanceBuilder.ClearanceType = NXOpen.CAM.NcmClearanceBuilder.ClearanceTypes.Plane;
            millOrientGeomBuilder1.TransferClearanceBuilder.PlaneXform = plane1;
            NXObject nXObject1;
            nXObject1 = millOrientGeomBuilder1.Commit();
            millOrientGeomBuilder1.Destroy();
            #endregion 

            //workpiece 工件几何体 指定部件 毛坯包容块
            var workpiece_ph = workPart.CAMSetup.CAMGroupCollection.CreateGeometry(nCGroup1, "mill_planar", "WORKPIECE", NXOpen.CAM.NCGroupCollection.UseDefaultName.False, "WORKPIECE_PH");
            var millGeomBuilder1 = workPart.CAMSetup.CAMGroupCollection.CreateMillGeomBuilder(workpiece_ph);
            var dumb = workPart.ScRuleFactory.CreateRuleBodyDumb(new Body[] { Snap.Globals.WorkPart.Bodies.FirstOrDefault() }, true);

            millGeomBuilder1.PartGeometry.GeometryList.FindItem(0).ScCollector.ReplaceRules(new SelectionIntentRule[] { dumb }, false);

            millGeomBuilder1.BlankGeometry.BlankDefinitionType = NXOpen.CAM.GeometryGroup.BlankDefinitionTypes.AutoBlock;//毛坯包容块

            millGeomBuilder1.Commit();

            millGeomBuilder1.Destroy();

            //切削区域
            var mill_area_ph=workPart.CAMSetup.CAMGroupCollection.CreateGeometry(workpiece_ph, "mill_planar", "MILL_AREA", NXOpen.CAM.NCGroupCollection.UseDefaultName.False, "MILL_AREA_PH");
            var millAreaGeomBuilder1 = workPart.CAMSetup.CAMGroupCollection.CreateMillAreaGeomBuilder(mill_area_ph);
            var faces = new List<Face>();
            snapWorkPart.Bodies.FirstOrDefault().Faces.Where(u => u.Name == "DDD").ToList().ForEach(u =>
            {
                faces.Add(u);
            });
            var faceDumb = workPart.ScRuleFactory.CreateRuleFaceDumb(faces.ToArray());
            millAreaGeomBuilder1.CutAreaGeometry.GeometryList.FindItem(0).ScCollector.ReplaceRules(new SelectionIntentRule[] { faceDumb }, false);
            millAreaGeomBuilder1.Commit();
            millAreaGeomBuilder1.Destroy();

            //创建刀具
            var GENERIC_MACHINE = workPart.CAMSetup.CAMGroupCollection.FindObject("GENERIC_MACHINE");
            var TOOL_PH = workPart.CAMSetup.CAMGroupCollection.CreateTool(GENERIC_MACHINE, "mill_planar", "MILL", NXOpen.CAM.NCGroupCollection.UseDefaultName.False, "TOOL_PH");
            var millToolBuilder1 = workPart.CAMSetup.CAMGroupCollection.CreateMillToolBuilder(TOOL_PH);
            millToolBuilder1.TlDiameterBuilder.Value = 6.0;

            millToolBuilder1.TlFluteLnBuilder.Value = 30.0;

            millToolBuilder1.TlNumFlutesBuilder.Value = 20;
            millToolBuilder1.Commit();
            millToolBuilder1.Destroy();

            //创建加工方法
            var MILL_SEMI_FINISH = workPart.CAMSetup.CAMGroupCollection.FindObject("MILL_SEMI_FINISH");
            var MOLD_FINISH_HSM_PH = workPart.CAMSetup.CAMGroupCollection.CreateMethod(MILL_SEMI_FINISH, "mill_contour", "MOLD_FINISH_HSM", NXOpen.CAM.NCGroupCollection.UseDefaultName.False, "MOLD_FINISH_HSM_PH");
            var millMethodBuilder1 = workPart.CAMSetup.CAMGroupCollection.CreateMillMethodBuilder(MOLD_FINISH_HSM_PH);
            millMethodBuilder1.CutParameters.PartStock.Value = 0.4;
            millMethodBuilder1.Commit();
            millMethodBuilder1.Destroy();

            //创建工序
            var CAVITY_MILL_PH = workPart.CAMSetup.CAMOperationCollection.Create(PROGRAM_PH, MOLD_FINISH_HSM_PH, TOOL_PH, mill_area_ph, "mill_contour", "CAVITY_MILL", NXOpen.CAM.OperationCollection.UseDefaultName.False, "CAVITY_MILL_PH");
            var cavityMillingBuilder1 = workPart.CAMSetup.CAMOperationCollection.CreateCavityMillingBuilder(CAVITY_MILL_PH);
            cavityMillingBuilder1.CutLevel.GlobalDepthPerCut.DistanceBuilder.Value = 1.0;
            cavityMillingBuilder1.CutParameters.PartStock.Value = 0.1;
            cavityMillingBuilder1.CutParameters.Tolerances.Value0 = 0.02;
            cavityMillingBuilder1.CutParameters.Tolerances.Value1 = 0.02;

            cavityMillingBuilder1.FeedsBuilder.SurfaceSpeedBuilder.Value = 28.0; //进给率和速度
            cavityMillingBuilder1.FeedsBuilder.FeedPerToothBuilder.Value = 0.8333;  //进给率和速度

            cavityMillingBuilder1.FeedsBuilder.SpindleRpmBuilder.Value = 1500.0;

            cavityMillingBuilder1.FeedsBuilder.SpindleRpmToggle = 1;

            cavityMillingBuilder1.FeedsBuilder.RecalculateData(NXOpen.CAM.FeedsBuilder.RecalcuateBasedOn.SpindleSpeed);

            cavityMillingBuilder1.FeedsBuilder.FeedCutBuilder.Value = 2500.0;

            cavityMillingBuilder1.Commit();

            cavityMillingBuilder1.Destroy();
            
            //生成路径
            workPart.CAMSetup.GenerateToolPath(new NXOpen.CAM.CAMObject[] { CAVITY_MILL_PH });

            workPart.CAMSetup.ListToolPath(new NXOpen.CAM.CAMObject[] { CAVITY_MILL_PH });

            //后处理
            workPart.CAMSetup.PostprocessWithSetting(new NXOpen.CAM.CAMObject[] { CAVITY_MILL_PH }, "钢料通用", "C:\\Users\\PENGHUI\\Desktop\\JIAGONG\\pocketing.nc", NXOpen.CAM.CAMSetup.OutputUnits.PostDefined, NXOpen.CAM.CAMSetup.PostprocessSettingsOutputWarning.PostDefined, NXOpen.CAM.CAMSetup.PostprocessSettingsReviewTool.PostDefined);

            //var programGroup = workPart.CAMSetup.CAMGroupCollection.FindObject("PROGRAM");
            //var methodGroup = workPart.CAMSetup.CAMGroupCollection.FindObject("METHOD");
            //var toolGroup = workPart.CAMSetup.CAMGroupCollection.FindObject("MILL");
            //var geometryGroup = workPart.CAMSetup.CAMGroupCollection.FindObject("WORKPIECE");

            Close();

          

        }

        private void btnCreateStd_Click(object sender, EventArgs e)
        {
            Session theSession = Session.GetSession();
            Part workPart = theSession.Parts.Work;
            Part displayPart = theSession.Parts.Display;


            Step214Creator step214Creator1;
            step214Creator1 = theSession.DexManager.CreateStep214Creator();

            step214Creator1.OutputFile = "C:\\Users\\PENGHUI\\Desktop\\FKTA8-SPMH18-EDM-TEST\\AB005-101.stp";

            //step214Creator1.SettingsFile = "I:\\UG\\NX 9.0-64bit\\step214ug\\ugstep214.def";

            step214Creator1.ObjectTypes.Solids = true;

            step214Creator1.ExportSelectionBlock.SelectionScope = ObjectSelector.Scope.SelectedObjects;
            workPart.Bodies.ToArray().ToList().ForEach(u =>
            {
                step214Creator1.ExportSelectionBlock.SelectionComp.Add(u);
            });

            step214Creator1.FileSaveFlag = false;

            step214Creator1.LayerMask = "1-256";
           
            NXObject nXObject1;
            nXObject1 = step214Creator1.Commit();

            step214Creator1.Destroy();
        }



        private void btnCreateCamera_Click(object sender, EventArgs e)
        {
            var ufSession = NXOpen.UF.UFSession.GetUFSession();
            ufSession.View.SetViewMatrix("", 4, NXOpen.Tag.Null, new double[] { 1, 0, 0, 0, 1, 0 });
            
            Session theSession = Session.GetSession();
            Part workPart = theSession.Parts.Work;
            Part displayPart = theSession.Parts.Display;

            NXOpen.Display.Camera nullDisplay_Camera = null;
            NXOpen.Display.CameraBuilder cameraBuilder1;
            cameraBuilder1 = workPart.Cameras.CreateCameraBuilder(nullDisplay_Camera);
            cameraBuilder1.Commit();
            var Guid = System.Guid.NewGuid().ToString();
            cameraBuilder1.CameraName = Guid;
            cameraBuilder1.CameraNameChar = Guid;
            cameraBuilder1.Commit();
            cameraBuilder1.Destroy();

          
           
        }


        private void btnCreateOrdinateDimension_Click(object sender, EventArgs e)
        {
            Session theSession = Session.GetSession();
            Part workPart = theSession.Parts.Work;
            Part displayPart = theSession.Parts.Display;

            NXOpen.Annotations.OrdinateDimension nullAnnotations_OrdinateDimension = null;
            NXOpen.Annotations.OrdinateDimensionBuilder ordinateDimensionBuilder1;
            ordinateDimensionBuilder1 = workPart.Dimensions.CreateOrdinateDimensionBuilder(nullAnnotations_OrdinateDimension);

            ordinateDimensionBuilder1.Baseline.ActivateBaseline = true;
            //ordinateDimensionBuilder1.Baseline.ActivatePerpendicular = true;

            //设置 - 显示名称样式
            ordinateDimensionBuilder1.Style.OrdinateStyle.DisplayNameStyle = NXOpen.Annotations.OrdinateOriginDisplayOption.NoText;
            //设置 - 显示尺寸线
            ordinateDimensionBuilder1.Style.OrdinateStyle.DisplayDimensionLine = NXOpen.Annotations.OrdinateLineArrowDisplayOption.None;
            //尺寸样式
            ordinateDimensionBuilder1.Style.DimensionStyle.ToleranceType = NXOpen.Annotations.ToleranceType.Basic;

            var points = Snap.Globals.WorkPart.Points.Where(u => u.GetAttributeInfo().Count() > 0);
            var point = points.FirstOrDefault();
           
            NXOpen.Drawings.BaseView baseView1 = (NXOpen.Drawings.BaseView)workPart.DraftingViews.FindObject("Front@45");
            Point3d point1_1 = Snap.Globals.Wcs.Origin;
            var edge = Snap.Globals.WorkPart.Bodies.First().Edges.FirstOrDefault();
            var point2 = Snap.Create.Point(Snap.Globals.Wcs.Origin);
            ordinateDimensionBuilder1.OrdinateOrigin.SetValue(point2, baseView1, point.Position);

           
           
            Point3d point1_2 = point.Position;
            var line = Snap.Create.Line(point1_2, point1_2);
            ordinateDimensionBuilder1.SecondAssociativities.SetValue(NXOpen.InferSnapType.SnapType.Mid, line.NXOpenLine, baseView1, point1_2, null, null, new Point3d(0.0, 0.0, 0.0));

            Point3d location1 = new Point3d(245.421568627451, 124.730742296919, 0.0);
            ordinateDimensionBuilder1.VerticalInferredMarginLocation = location1;
            
            ordinateDimensionBuilder1.Commit();
            ordinateDimensionBuilder1.Destroy();
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            Session theSession = Session.GetSession();
            Part workPart = theSession.Parts.Work;
            Part displayPart = theSession.Parts.Display;
            var printPDFBuilder1 = workPart.PlotManager.CreatePrintPdfbuilder();
            printPDFBuilder1.Colors = NXOpen.PrintPDFBuilder.Color.PartColors;
            NXObject[] sheets1 = new NXObject[1];
            NXOpen.Drawings.DrawingSheet drawingSheet1 = (NXOpen.Drawings.DrawingSheet)workPart.DrawingSheets.FindObject("AB005-101-ASM_E01");
            sheets1[0] = drawingSheet1;
            printPDFBuilder1.SourceBuilder.SetSheets(sheets1);
            printPDFBuilder1.Filename = "F:\\PENGHUI_AB005-101-ASM.pdf";

            NXObject nXObject1;
            nXObject1 = printPDFBuilder1.Commit();
            printPDFBuilder1.Destroy();
        }

        private void btnSetDraw_Click(object sender, EventArgs e)
        {
            var theSession = NXOpen.Session.GetSession();
            var workPart = theSession.Parts.Work;
            NXOpen.Drafting.PreferencesBuilder preferencesBuilder1;
            preferencesBuilder1 = workPart.SettingsManager.CreatePreferencesBuilder();


            preferencesBuilder1.AnnotationStyle.LetteringStyle.GeneralTextFont = 3;  //字体

            preferencesBuilder1.AnnotationStyle.LetteringStyle.GeneralTextColor = workPart.Colors.Find("Red");//字体颜色

            preferencesBuilder1.AnnotationStyle.LetteringStyle.GeneralTextLineWidth = NXOpen.Annotations.LineWidth.Thick;//字体样式
            
            preferencesBuilder1.AnnotationStyle.LetteringStyle.GeneralTextSize = 2.0;//高度

            preferencesBuilder1.AnnotationStyle.LetteringStyle.GeneralNxTextCharacterSpaceFactor = 0.4;//NX字体间隙因子

            preferencesBuilder1.AnnotationStyle.LetteringStyle.GeneralStandardTextCharacterSpaceFactor = 0.1;//标准字体间隙因子

            preferencesBuilder1.AnnotationStyle.LetteringStyle.GeneralTextAspectRatio = 0.8;//宽高比

            preferencesBuilder1.AnnotationStyle.LetteringStyle.GeneralTextLineSpaceFactor = 3.0;//行间隙因子

            preferencesBuilder1.AnnotationStyle.LetteringStyle.Angle = 0.1;//文字角度
            preferencesBuilder1.Commit();
            preferencesBuilder1.Destroy();
        }

        private void btnCreateOrdinateDimensionex_Click(object sender, EventArgs e)
        {
            NXOpen.NXObject edge1 = null;

            var theSession = NXOpen.Session.GetSession();
            var workPart = theSession.Parts.Work;

            //创建原点
            var origin=workPart.Dimensions.CreateOrdinateOriginDimension(null, new Snap.Position());


            //创建边距
            NXOpen.Annotations.Associativity associativity1;
            associativity1 = workPart.Annotations.NewAssociativity();
            associativity1.FirstObject = edge1;

            var baseView1 = (NXOpen.Drawings.BaseView)workPart.DraftingViews.FindObject("Front@5");
            associativity1.ObjectView = baseView1;
            //associativity1.LineOption = NXOpen.Annotations.AssociativityLineOption.VerticalUp;
           
            var verticalOrdinateMargin1 = workPart.Annotations.OrdinateMargins.CreateVerticalMargin(origin, associativity1, 40.0);


            //创建坐标
            NXOpen.Annotations.OrdinateDimension nullAnnotations_OrdinateDimension = null;
            NXOpen.Annotations.OrdinateDimensionBuilder ordinateDimensionBuilder1;
            ordinateDimensionBuilder1 = workPart.Dimensions.CreateOrdinateDimensionBuilder(nullAnnotations_OrdinateDimension);

            ordinateDimensionBuilder1.Baseline.ActivateBaseline = true;
            //ordinateDimensionBuilder1.Baseline.ActivatePerpendicular = true;

            //设置 - 显示名称样式
            ordinateDimensionBuilder1.Style.OrdinateStyle.DisplayNameStyle = NXOpen.Annotations.OrdinateOriginDisplayOption.NoText;
            //设置 - 显示尺寸线
            ordinateDimensionBuilder1.Style.OrdinateStyle.DisplayDimensionLine = NXOpen.Annotations.OrdinateLineArrowDisplayOption.None;
            //尺寸样式
            ordinateDimensionBuilder1.Style.DimensionStyle.ToleranceType = NXOpen.Annotations.ToleranceType.Basic;

            var points = Snap.Globals.WorkPart.Points.Where(u => u.GetAttributeInfo().Count() > 0);
            var point = points.FirstOrDefault();
            Point3d point1_1 = Snap.Globals.Wcs.Origin;
            var edge = Snap.Globals.WorkPart.Bodies.First().Edges.FirstOrDefault();
            var point2 = Snap.Create.Point(Snap.Globals.Wcs.Origin);
            ordinateDimensionBuilder1.OrdinateOrigin.SetValue(point2, baseView1, point.Position);

            Point3d point1_2 = point.Position;
            var line = Snap.Create.Line(point1_2, point1_2);
            ordinateDimensionBuilder1.SecondAssociativities.SetValue(NXOpen.InferSnapType.SnapType.Mid, line.NXOpenLine, baseView1, point1_2, null, null, new Point3d(0.0, 0.0, 0.0));

            Point3d location1 = new Point3d(245.421568627451, 124.730742296919, 0.0);
            ordinateDimensionBuilder1.VerticalInferredMarginLocation = location1;

            ordinateDimensionBuilder1.Commit();
            ordinateDimensionBuilder1.Destroy();

        }

        private void button11_Click(object sender, EventArgs e)
        {
            Session theSession = Session.GetSession();
            Part workPart = theSession.Parts.Work;
            Part displayPart = theSession.Parts.Display;
            var mWMoldedPartValidationBuilder1 = workPart.ToolingManager.MWMoldedPartValidation.CreateMWMoldedPartValidationBuilder();

            mWMoldedPartValidationBuilder1.InitMpvData(0);

            mWMoldedPartValidationBuilder1.FaceSelectAllFaces = true;

            mWMoldedPartValidationBuilder1.FaceAllFacesColor = workPart.Colors.Find("Strong Emerald");

            mWMoldedPartValidationBuilder1.FacePositiveFacesColor1 = workPart.Colors.Find("Medium Carrot");

            mWMoldedPartValidationBuilder1.FacePositiveFacesColor2 = workPart.Colors.Find("Deep Yellow");

            mWMoldedPartValidationBuilder1.FaceVerticalFacesColor = workPart.Colors.Find("Silver Gray");

            mWMoldedPartValidationBuilder1.FaceNegativeFacesColor1 = workPart.Colors.Find("Deep Aqua");

            mWMoldedPartValidationBuilder1.FaceNegativeFacesColor2 = workPart.Colors.Find("Strong Turquoise");

            mWMoldedPartValidationBuilder1.FaceCrossoverFacesColor = workPart.Colors.Find("Medium Coral");

            mWMoldedPartValidationBuilder1.FaceUndercutAreasColor = workPart.Colors.Find("Pale Crimson");

            mWMoldedPartValidationBuilder1.RegionCavityRegionColor = workPart.Colors.Find("Strong Coral");

            mWMoldedPartValidationBuilder1.RegionCoreRegionColor = workPart.Colors.Find("Medium Royal");

            mWMoldedPartValidationBuilder1.RegionUndefinedRegionColor = workPart.Colors.Find("Deep Cyan");

            Body body1 = (Body)workPart.Bodies.FindObject("UNPARAMETERIZED_FEATURE(0)");
            mWMoldedPartValidationBuilder1.CalculationProductBody.Value = body1;

            mWMoldedPartValidationBuilder1.ActiveTabPage = 0;

            Point3d origin2 = new Point3d(0.0, 1.77635683940025e-015, -8.21662500000032);
            Vector3d vector2 = new Vector3d(0.0, 0.0, 1.0);
            Direction direction2;
            direction2 = workPart.Directions.CreateDirection(origin2, vector2, NXOpen.SmartObject.UpdateOption.AfterModeling);

            mWMoldedPartValidationBuilder1.CalculationDrawDirection = direction2;

            mWMoldedPartValidationBuilder1.Calculate();

            mWMoldedPartValidationBuilder1.UpdateMpvData();

            mWMoldedPartValidationBuilder1.UpdateSmallRadiusFacesInformation();

            mWMoldedPartValidationBuilder1.ActiveTabPage = 1;

            mWMoldedPartValidationBuilder1.FaceSetAllFacesColor();

            mWMoldedPartValidationBuilder1.FaceSelectAllFaces = false;

            mWMoldedPartValidationBuilder1.Commit();

            mWMoldedPartValidationBuilder1.Destroy();

        }
    }
}
