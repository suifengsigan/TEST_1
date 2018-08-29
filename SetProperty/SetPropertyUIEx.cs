using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
partial class SetPropertyUI : SnapEx.BaseUI
{
    bool _isAllowMultiple = false;
    EactConfig.ConfigData _configData = EactConfig.ConfigData.GetInstance();
    bool SpecialshapedElec { get { return _configData.SpecialshapedElec && !_configData.IsSetPropertyAllowMultiple; } }
    ElecManage.ElectrodeInfo GetElecInfo(Snap.NX.Body b)
    {
        ElecManage.ElectrodeInfo info = null;
        if (SpecialshapedElec)
        {
            var xkElec = ElecManage.Electrode.GetElectrode(b);
            if (xkElec != null)
            {
                info = xkElec.GetElectrodeInfo();
            }
        }
        else
        {
            var xkElec = ElecManage.Electrode.GetElectrode(b);
            if (xkElec != null)
            {
                info = xkElec.GetElectrodeInfo();
            }
            else
            {
                info = new ElecManage.ElectrodeInfo(b);
            }
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
            var p = EactConfig.ConfigData.GetInstance().Poperties.FirstOrDefault(u => u.DisplayName == "夹具类型");
            var x = info.CuttingX(_configData.PQBlankStock);
            var y = info.CuttingY(_configData.PQBlankStock);
            if (p != null)
            {
                foreach (var m in p.Selections)
                {
                    if (result != null)
                    {
                        break;
                    }
                    var list = EactConfig.MatchJiaju.DeserializeObject(m.Ex1);
                    foreach (var u in list)
                    {
                        if ((IsEquals(x, u.X) && IsEquals(y, u.Y)) || IsEquals(x, u.Y) && IsEquals(y, u.X))
                        {
                            result = m;
                            break;
                        }
                    }
                }
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

        double matchJiajuValue = 0;

        _configData.Poperties.ForEach(u => {
            var dic = new Dictionary<Snap.UI.Block.General, string>();
            Snap.UI.Block.General cbb = null;
            var realValue = string.Empty;
            if (u.DisplayName == "电极材质")
            {
                realValue = info == null ? string.Empty : info.MAT_NAME;
                cbb = cboxMATNAME;
                if (_configData.IsMatNameSel)
                {
                    dic.Add(cboxMMATNAME, info == null ? string.Empty : info.M_MAT_NAME);
                    dic.Add(cboxRMATNAME, info == null ? string.Empty : info.R_MAT_NAME);
                }
                
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
                dic.Add(cbb, realValue);
            }

            foreach (var item in dic)
            {
                var enumration = item.Key as Snap.UI.Block.Enumeration;
                var rValue = item.Value;
                var defaultSelection = string.IsNullOrEmpty(rValue) ? (u.Selections.FirstOrDefault(m => m.IsDefault)) : u.Selections.FirstOrDefault(m => m.Value == rValue);
                if (u.DisplayName == "夹具类型")
                {
                    defaultSelection = string.IsNullOrEmpty(rValue) ? (MatchJiaju(info) ?? u.Selections.FirstOrDefault(m => m.IsDefault)) : u.Selections.FirstOrDefault(m => m.Value == rValue);
                    if (defaultSelection != null)
                    {
                        matchJiajuValue = EactConfig.MatchJiaju.GetDouble(defaultSelection.Ex2);
                    }
                }
                if (defaultSelection != null)
                {
                    defaultSelection = u.Selections.FirstOrDefault(m => m.Value == defaultSelection.Value);
                    if (defaultSelection != null)
                    {
                        u.Selections.Remove(defaultSelection);
                        u.Selections.Insert(0, defaultSelection);
                    }
                }
                enumration.Items = Enumerable.Select(u.Selections, m => m.Value).ToArray();
            }
        });

        if (info != null)
        {
            strElecSize.Value = info.ElecSize;
            strElecCuttingSize.Value = info.ElecCuttingSize(_configData.PQBlankStock, matchJiajuValue);
        }
        else
        {
            strElecSize.Value = string.Empty;
            strElecCuttingSize.Value = string.Empty;
        }
    }

    void SetEactElec(Snap.NX.Body body,Snap.NX.Face baseFace = null, Snap.NX.Face topFace = null, Snap.NX.Point basePoint = null)
    {
        selectBaseFace.SelectedObjects = new Snap.NX.NXObject[] { };
        selectTopFace.SelectedObjects = new Snap.NX.NXObject[] { };
        selectBaseFacePoint.SelectedObjects = new Snap.NX.NXObject[] { };
        ElecManage.Electrode.SetEactElectrode(body, ref topFace, ref baseFace, ref basePoint);
        if (baseFace != null) { selectBaseFace.SelectedObjects = new Snap.NX.NXObject[] { baseFace }; }
        if (topFace != null) { selectTopFace.SelectedObjects = new Snap.NX.NXObject[] { topFace }; }
        if (basePoint != null) { selectBaseFacePoint.SelectedObjects = new Snap.NX.NXObject[] { basePoint }; }
        if (baseFace != null && topFace != null && basePoint != null)
        {
            //更新尺寸
            var info = GetElecInfo(body);
            if (info != null)
            {
                strElecSize.Value = info.ElecSize;
                strElecCuttingSize.Value = info.ElecCuttingSize(_configData.PQBlankStock, EactConfig.MatchJiaju.GetMatchJiajuValue(_configData, cbbChuckType.SelectedIndex));
            }
            else
            {
                strElecSize.Value = string.Empty;
                strElecCuttingSize.Value = string.Empty;
            }
        }
    }
    void RereshUI()
    {
        
    }
    public override void Init()
    {
        _isAllowMultiple = _configData.IsSetPropertyAllowMultiple;
        selectCuprum.AllowMultiple = _isAllowMultiple;
        selectCuprum.SetFilter(Snap.NX.ObjectTypes.Type.Body, Snap.NX.ObjectTypes.SubType.BodySolid);
        selectBaseFace.SetFilter(Snap.NX.ObjectTypes.Type.Face, Snap.NX.ObjectTypes.SubType.FacePlane);
        selectTopFace.SetFilter(Snap.NX.ObjectTypes.Type.Face, Snap.NX.ObjectTypes.SubType.FacePlane);
        selectBaseFacePoint.SetFilter(Snap.NX.ObjectTypes.Type.Point, Snap.NX.ObjectTypes.SubType.PatternPoint);
        ElecManage.Entry.Edition = _configData.Edition;
    }

    public override void DialogShown()
    {
        SetDefaultValue(null);
        groupSElec.Show = false;
        if (_configData.IsMatNameSel)
        {
            cboxMATNAME.Label = "精公材质";
        }
        cboxMMATNAME.Show = _configData.IsMatNameSel;
        cboxRMATNAME.Show = _configData.IsMatNameSel;
    }

    public override void Update(NXOpen.BlockStyler.UIBlock block)
    {
        if (block == selectCuprum.NXOpenBlock)
        {
            var cuprums = Enumerable.Select(selectCuprum.SelectedObjects, u => Snap.NX.Body.Wrap(u.NXOpenTag)).ToList();
            var unNameC = cuprums.Where(u => string.IsNullOrEmpty(u.Name)).ToList();
            var nameC = cuprums.Where(u => !string.IsNullOrEmpty(u.Name)).ToList();

            groupSElec.Show = false;
            if (nameC.Count == 1)
            {
                var body = nameC.First();
                var info = GetElecInfo(body);
                var groupSElecShow = info == null;
                if (info == null)
                {
                    info = new ElecManage.EactElectrodeInfo(body);
                }

                if (_configData.Edition==1)
                {
                    if ((info.KL_SIZE_HEIGHT == 0 || info.KL_SIZE_LEN == 0 || info.KL_SIZE_WIDTH == 0))
                    {
                        theUI.NXMessageBox.Show("提示", NXOpen.NXMessageBox.DialogType.Error, "该电极未设置开料尺寸！");
                        selectCuprum.SelectedObjects = new Snap.NX.NXObject[] { };
                        return;
                    }
                   
                    if (string.IsNullOrEmpty(info.ELEC_CLAMP_GENERAL_TYPE))
                    {
                        theUI.NXMessageBox.Show("提示", NXOpen.NXMessageBox.DialogType.Error, "该电极未设置夹具！");
                        selectCuprum.SelectedObjects = new Snap.NX.NXObject[] { };
                        return;
                    }
                }

                Action<bool> action = (b) =>
                {
                    groupSElec.Show = b;
                    if (b)
                    {
                        theUI.NXMessageBox.Show("提示", NXOpen.NXMessageBox.DialogType.Information, "异形电极，请设置相关参数！");
                        SetEactElec(body);
                    }
                };

                if (SpecialshapedElec)
                {
                    action(groupSElecShow);
                }
                SetDefaultValue(info);
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
            strElecCuttingSize.Show = strElecName.Show;
        }
        else if (block == cbbChuckType.NXOpenBlock)
        {
            var cuprums = Enumerable.Select(selectCuprum.SelectedObjects, u => Snap.NX.Body.Wrap(u.NXOpenTag)).ToList();
            if (cuprums.Count == 1)
            {
                var info = GetElecInfo(cuprums.First());
                if (info != null)
                {
                    strElecSize.Value = info.ElecSize;
                    strElecCuttingSize.Value = info.ElecCuttingSize(_configData.PQBlankStock, EactConfig.MatchJiaju.GetMatchJiajuValue(_configData, cbbChuckType.SelectedIndex));
                }
                else
                {
                    strElecSize.Value = string.Empty;
                    strElecCuttingSize.Value = string.Empty;
                }
            }

        }
        else if (block == selectBaseFace.NXOpenBlock 
            || block == selectBaseFacePoint.NXOpenBlock
            ||block==selectTopFace.NXOpenBlock
            )
        {
            var cuprums = Enumerable.Select(selectCuprum.SelectedObjects, u => Snap.NX.Body.Wrap(u.NXOpenTag)).ToList();
            if (cuprums.Count == 1)
            {
                var basePoint = Enumerable.Select(selectBaseFacePoint.SelectedObjects, u => Snap.NX.Point.Wrap(u.NXOpenTag)).FirstOrDefault();
                var baseFace = Enumerable.Select(selectBaseFace.SelectedObjects, u => Snap.NX.Face.Wrap(u.NXOpenTag)).FirstOrDefault();
                var topFace = Enumerable.Select(selectTopFace.SelectedObjects, u => Snap.NX.Face.Wrap(u.NXOpenTag)).FirstOrDefault();
                SetEactElec(cuprums.FirstOrDefault(),baseFace,topFace, basePoint);
            }
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
                            if (_configData.IsMatNameSel)
                            {
                                info.M_MAT_NAME = p.Selections[cboxMMATNAME.SelectedIndex].Value;
                                info.R_MAT_NAME = p.Selections[cboxRMATNAME.SelectedIndex].Value;
                            }
                            
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