using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

partial class ElecInterveneUI
{
    void Init()
    {
        SelectElec.BodyRules = BodyRules_SingleBody;
        var snapSelectElec = Snap.UI.Block.SelectObject.GetBlock(theDialog, SelectElec.Name);
        snapSelectElec.AllowMultiple = false;
        SelectSteel.BodyRules = BodyRules_SingleBody;
        var snapSelectSteel = Snap.UI.Block.SelectObject.GetBlock(theDialog, SelectElec.Name);
        snapSelectSteel.AllowMultiple = false;
    }

    void Apply()
    {
        var firstBody=SelectElec.GetSelectedObjects().FirstOrDefault();
        var secondBody = SelectSteel.GetSelectedObjects().FirstOrDefault();
        //简单干涉
        var result1 = SnapEx.Create.SimpleInterference(firstBody, secondBody);
        string message = string.Empty;
        if (result1 == NXOpen.GeometricAnalysis.SimpleInterference.Result.OnlyEdgesOrFacesInterfere)
        {
            message = "仅面或边干涉";
        }
        else if (result1 == NXOpen.GeometricAnalysis.SimpleInterference.Result.NoInterference)
        {
            message = "没有干涉";
        }
        else if (result1 == NXOpen.GeometricAnalysis.SimpleInterference.Result.InterferenceExists)
        {
            message = "存在多个干涉";
        }
        else
        {
            message = "无法完成检查";
        }
        theUI.NXMessageBox.Show("提示", NXOpen.NXMessageBox.DialogType.Information,message);
    }
}