using NXOpen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

partial class InitCMMUI : SnapEx.BaseUI
{
    public override void Apply()
    {
        InitCMM();
    }
    void InitCMM() 
    {
        Session theSession = Session.GetSession();
        Part workPart = theSession.Parts.Work;
        //Part displayPart = theSession.Parts.Display;
        //// ----------------------------------------------
        ////   菜单：文件->新建...
        //// ----------------------------------------------
        //NXOpen.Session.UndoMarkId markId1;
        //markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "开始");

        //FileNew fileNew1;
        //fileNew1 = theSession.Parts.FileNew();

        //theSession.SetUndoMarkName(markId1, "新建 对话框");

        //NXOpen.Session.UndoMarkId markId2;
        //markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "新建");

        //theSession.DeleteUndoMark(markId2, null);

        //NXOpen.Session.UndoMarkId markId3;
        //markId3 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "新建");

        //fileNew1.TemplateFileName = "${UGII_INSPECTION_TEMPLATE_PART_METRIC_DIR}insp_general_assy.prt";

        //fileNew1.ApplicationName = "InspectionTemplate";

        //fileNew1.Units = NXOpen.Part.Units.Millimeters;

        //fileNew1.RelationType = "";

        //fileNew1.UsesMasterModel = "Yes";

        //fileNew1.TemplateType = FileNewTemplateType.Item;

        //fileNew1.NewFileName = "C:\\Users\\PENGHUI\\Desktop\\PHWork\\AB005-101-02_inspection_setup_1.prt";

        //fileNew1.MasterFileName = "AB005-101-02";

        //fileNew1.UseBlankTemplate = false;

        //fileNew1.MakeDisplayedPart = true;

        //NXObject nXObject1;
        //nXObject1 = fileNew1.Commit();

        //workPart = theSession.Parts.Work;
        //displayPart = theSession.Parts.Display;
        //theSession.DeleteUndoMark(markId3, null);

        //fileNew1.Destroy();

        theSession.CreateInspectionSession();

        NXOpen.SIM.KinematicConfigurator kinematicConfigurator1;
        kinematicConfigurator1 = workPart.CreateKinematicConfigurator();

        //
        UI.GetUI().MenuBarManager.ApplicationSwitchRequest("UG_APP_INSPECTION");
    }
}