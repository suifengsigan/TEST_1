using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
partial class SetPropertyUI : SnapEx.BaseUI
{
    bool _isAllowMultiple = false;
    EactConfig.ConfigData _configData = EactConfig.ConfigData.GetInstance();
    ElecManage.ElectrodeInfo GetElecInfo(Snap.NX.Body b)
    {
        ElecManage.ElectrodeInfo info = null;
        var xkElec = ElecManage.XKElectrode.GetElectrode(b);
        if (xkElec != null)
        {
            info = new ElecManage.XKElectrodeInfo(b);
        }
        else
        {
            info = new ElecManage.ElectrodeInfo(b);
        }
        return info;
    }

    /// <summary>
    /// 匹配夹具
    /// </summary>
    EactConfig.ConfigData.PopertySelection MatchJiaju(ElecManage.ElectrodeInfo info)
    {
        EactConfig.ConfigData.PopertySelection result = null;
        if (info != null)
        {
            strElecSize.Value = info.ElecSize;
            strElecCuttingSize.Value = info.ElecCuttingSize(_configData.PQBlankStock);
            var p = _configData.Poperties.FirstOrDefault(u => u.DisplayName == "夹具类型");
            var x = info.CuttingX(_configData.PQBlankStock);
            var y = info.CuttingY(_configData.PQBlankStock);
            if (p != null)
            {
                p.Selections.ForEach(m => {
                    var list = EactConfig.MatchJiaju.DeserializeObject(m.Ex1);
                    list.ForEach(u => {
                        if ((IsEquals(x, u.X) && IsEquals(y, u.Y)) || IsEquals(x, u.Y) && IsEquals(y, u.X))
                        {
                            result = m;
                        }
                    });
                    return;
                });
            }
        }
         
        return result;
    }

    bool IsEquals(double x,double x1)
    {
        return Math.Abs(x - x1) < SnapEx.Helper.Tolerance;
    }

    void SetDefaultValue(ElecManage.ElectrodeInfo info)
    {
        //赋值
        strElecName.Value = info == null ? string.Empty : info.Elec_Name; ;
        txtFINISHNUMBER.Value =(info == null ? 0 : info.FINISH_NUMBER);
        txtMIDDLENUMBER.Value =(info == null ? 0 : info.MIDDLE_NUMBER);
        txtROUGHNUMBER.Value = (info == null ? 0 : info.ROUGH_NUMBER);
        txtFINISHSPACE.Value = (info == null ? 0 : info.FINISH_SPACE);
        txtMIDDLESPACE.Value = (info == null ? 0 : info.MIDDLE_SPACE);
        txtROUGHSPACE.Value = (info == null ? 0 : info.ROUGH_SPACE);

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
                var defaultSelection = string.IsNullOrEmpty(realValue)?(u.Selections.FirstOrDefault(m => m.IsDefault)): u.Selections.FirstOrDefault(m => m.Value==realValue);
                if (u.DisplayName == "夹具类型")
                {
                    defaultSelection = string.IsNullOrEmpty(realValue) ? (MatchJiaju(info) ?? u.Selections.FirstOrDefault(m => m.IsDefault)) : u.Selections.FirstOrDefault(m => m.Value == realValue);
                }
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
        _isAllowMultiple = _configData.IsSetPropertyAllowMultiple;
        selectCuprum.AllowMultiple = _isAllowMultiple;
        selectCuprum.SetFilter(Snap.NX.ObjectTypes.Type.Body, Snap.NX.ObjectTypes.SubType.BodySolid);
    }

    public override void DialogShown()
    {
        SetDefaultValue(null);
    }

    public override void Update(NXOpen.BlockStyler.UIBlock block)
    {
        if (block == selectCuprum.NXOpenBlock)
        {
            var cuprums = Enumerable.Select(selectCuprum.SelectedObjects, u => Snap.NX.Body.Wrap(u.NXOpenTag)).ToList();
            var unNameC = cuprums.Where(u => string.IsNullOrEmpty(u.Name)).ToList();
            var nameC = cuprums.Where(u => !string.IsNullOrEmpty(u.Name)).ToList();
           

            if (nameC.Count == 1)
            {
                SetDefaultValue(GetElecInfo(nameC.First()));
            }

            if (_isAllowMultiple)
            {
                if (nameC.Count > 1)
                {
                    strElecName.Show = false;
                }
                else
                {
                    strElecName.Show = true;
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

            strElecSize.Show = strElecName.Show;
            strElecCuttingSize.Show= strElecName.Show;
        }
    }

    public override void Apply()
    {
        try
        {
            var cuprums = Enumerable.Select(selectCuprum.SelectedObjects, u => Snap.NX.Body.Wrap(u.NXOpenTag)).ToList();
            var unNameC = cuprums.Where(u => string.IsNullOrEmpty(u.Name)).ToList();
            var nameC = cuprums.Where(u => !string.IsNullOrEmpty(u.Name)).ToList();

            if (cuprums.Count == 1 && string.IsNullOrEmpty(strElecName.Value))
            {
                theUI.NXMessageBox.Show("错误", NXOpen.NXMessageBox.DialogType.Error, "电极名称不能为空");
                return;
            }

            if (cuprums.Count == 1)
            {
                cuprums.First().Name = strElecName.Value;
            }

            cuprums.ForEach(u => {
                var bodies = Snap.Globals.WorkPart.Bodies.Where(m => m.Name == u.Name).ToList();

                bodies.ForEach(b => {
                    ElecManage.ElectrodeInfo info = GetElecInfo(b);
                    info.FINISH_NUMBER = txtFINISHNUMBER.Value;
                    info.MIDDLE_NUMBER = txtMIDDLENUMBER.Value;
                    info.ROUGH_NUMBER = txtROUGHNUMBER.Value;
                    info.FINISH_SPACE = txtFINISHSPACE.Value;
                    info.MIDDLE_SPACE = txtMIDDLESPACE.Value;
                    info.ROUGH_SPACE = txtROUGHSPACE.Value;

                    _configData.Poperties.ForEach(p => {
                        if (p.DisplayName == "电极材质")
                        {
                            info.MAT_NAME = p.Selections[cboxMATNAME.SelectedIndex].Value;
                        }
                        else if (p.DisplayName == "加工方向")
                        {
                            info.EDMPROCDIRECTION = p.Selections[cbbProdirection.SelectedIndex].Value;
                        }
                        else if (p.DisplayName == "电极类型")
                        {
                            info.UNIT = p.Selections[cbbElecType.SelectedIndex].Value;
                        }
                        else if (p.DisplayName == "摇摆方式")
                        {
                            info.EDMROCK = p.Selections[cbbRock.SelectedIndex].Value;
                        }
                        else if (p.DisplayName == "精公光洁度")
                        {
                            info.F_SMOOTH = p.Selections[cbbFSmoth.SelectedIndex].Value;
                        }
                        else if (p.DisplayName == "中公光洁度")
                        {
                            info.M_SMOOTH = p.Selections[cbbMSmoth.SelectedIndex].Value;
                        }
                        else if (p.DisplayName == "粗公光洁度")
                        {
                            info.R_SMOOTH = p.Selections[cbbRSmoth.SelectedIndex].Value;
                        }
                        else if (p.DisplayName == "夹具类型")
                        {
                            info.ELEC_CLAMP_GENERAL_TYPE = p.Selections[cbbChuckType.SelectedIndex].Value;
                        }
                    });
                });
            });

            theUI.NXMessageBox.Show("成功", NXOpen.NXMessageBox.DialogType.Information, "电极信息保存成功");
        }
        catch (Exception ex)
        {
            theUI.NXMessageBox.Show("错误", NXOpen.NXMessageBox.DialogType.Error, ex.Message);
        }

    }
}