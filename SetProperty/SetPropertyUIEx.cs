using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
partial class SetPropertyUI : SnapEx.BaseUI
{
    EactConfig.ConfigData _configData = EactConfig.ConfigData.GetInstance();

    int ConvertToInt(string value)
    {
        int result = 0;
        int.TryParse(value, out result);
        return result;
    }

    double ConvertToDouble(string value)
    {
        double result = 0.00;
        double.TryParse(value, out result);
        return result;
    }

    void SetDefaultValue(ElecManage.ElectrodeInfo info)
    {
        //赋值
        strElecName.Value = info == null ? string.Empty : info.Elec_Name; ;
        txtFINISHNUMBER.Value = ConvertToInt(info == null ? string.Empty : info.FINISH_NUMBER);
        txtMIDDLENUMBER.Value = ConvertToInt(info == null ? string.Empty : info.MIDDLE_NUMBER);
        txtROUGHNUMBER.Value = ConvertToInt(info == null ? string.Empty : info.ROUGH_NUMBER);
        txtFINISHSPACE.Value = ConvertToDouble(info == null ? string.Empty : info.FINISH_SPACE);
        txtMIDDLESPACE.Value = ConvertToDouble(info == null ? string.Empty : info.MIDDLE_SPACE);
        txtROUGHSPACE.Value = ConvertToDouble(info == null ? string.Empty : info.ROUGH_SPACE);

        _configData.Poperties.ForEach(u => {
            Snap.UI.Block.General cbb = null;
            var realValue = string.Empty;
            if (u.DisplayName == "电极材质")
            {
                realValue = info == null ? string.Empty : info.MAT_NAME;
                cbb = cboxMATNAME;
            }
            else if (u.DisplayName == "加工方向")
            {
                realValue = info == null ? string.Empty : info.EDMPROCDIRECTION;
                cbb = cbbProdirection;
            }
            else if (u.DisplayName == "电极类型")
            {
                realValue = info == null ? string.Empty : info.UNIT;
                cbb = cbbElecType;
            }
            else if (u.DisplayName == "摇摆方式")
            {
                realValue = info == null ? string.Empty : info.EDMROCK;
                cbb = cbbRock;
            }
            else if (u.DisplayName == "精公光洁度")
            {
                realValue = info == null ? string.Empty : info.F_SMOOTH;
                cbb = cbbFSmoth;
            }
            else if (u.DisplayName == "中公光洁度")
            {
                realValue = info == null ? string.Empty : info.M_SMOOTH;
                cbb = cbbMSmoth;
            }
            else if (u.DisplayName == "粗公光洁度")
            {
                realValue = info == null ? string.Empty : info.R_SMOOTH;
                cbb = cbbRSmoth;
            }
            else if (u.DisplayName == "夹具类型")
            {
                realValue = info == null ? string.Empty : info.ELEC_CLAMP_GENERAL_TYPE;
                cbb = cbbChuckType;
            }

            if (cbb is Snap.UI.Block.Enumeration)
            {
                var enumration = cbb as Snap.UI.Block.Enumeration;
                var defaultSelection = string.IsNullOrEmpty(realValue)?u.Selections.FirstOrDefault(m => m.IsDefault): u.Selections.FirstOrDefault(m => m.Value==realValue);
                if (defaultSelection != null)
                {
                    u.Selections.Remove(defaultSelection);
                    u.Selections.Insert(0, defaultSelection);
                }
                enumration.Items = Enumerable.Select(u.Selections, m => m.Value).ToArray();
            }
        });
    }
    public override void Init()
    {
        selectCuprum.AllowMultiple = true;
        selectCuprum.SetFilter(Snap.NX.ObjectTypes.Type.Body, Snap.NX.ObjectTypes.SubType.BodySolid);
        SetDefaultValue(null);
    }

    public override void DialogShown()
    {
        
    }

    public override void Update(NXOpen.BlockStyler.UIBlock block)
    {
        if (block == selectCuprum.NXOpenBlock)
        {
            var cuprums = Enumerable.Select(selectCuprum.SelectedObjects, u => Snap.NX.Body.Wrap(u.NXOpenTag)).ToList();
            var unNameC = cuprums.Where(u => string.IsNullOrEmpty(u.Name)).ToList();
            var nameC = cuprums.Where(u => !string.IsNullOrEmpty(u.Name)).ToList();
            if (nameC.Count > 1)
            {
                strElecName.Show = false;
            }
            else
            {
                strElecName.Show = true;
            }

            if (nameC.Count == 1)
            {
                SetDefaultValue(new ElecManage.ElectrodeInfo(nameC.First()));
            }

            if (cuprums.Count != 1)
            {
                selectCuprum.AllowMultiple = true;
               
                if (unNameC.Count > 0)
                {
                    theUI.NXMessageBox.Show("提示", NXOpen.NXMessageBox.DialogType.Information, "电极名称不能为空");
                    selectCuprum.SelectedObjects = cuprums.Where(u => !string.IsNullOrEmpty(u.Name)).ToArray();
                }
            }
            else
            {
                if (string.IsNullOrEmpty(cuprums.First().Name))
                {
                    selectCuprum.AllowMultiple = false;
                }
                else
                {
                    selectCuprum.AllowMultiple = true;
                }
            }
        }
    }

    public override void Apply()
    {
        var cuprums = Enumerable.Select(selectCuprum.SelectedObjects, u => Snap.NX.Body.Wrap(u.NXOpenTag)).ToList();
        var unNameC = cuprums.Where(u => string.IsNullOrEmpty(u.Name)).ToList();
        var nameC = cuprums.Where(u => !string.IsNullOrEmpty(u.Name)).ToList();

        if (unNameC.Count > 0)
        {
            theUI.NXMessageBox.Show("提示", NXOpen.NXMessageBox.DialogType.Information, "电极名称不能为空");
            return;
        }

        nameC.ForEach(u => {
        });


    }
}