using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

partial class SelectSteelUI : SnapEx.BaseUI
{
    
    public System.Windows.Forms.DialogResult Result = System.Windows.Forms.DialogResult.No;
    public ElecManage.MouldInfo MouldInfo = null;
    List<string> _items = new List<string>();

    public NXOpen.Body GetSteel() 
    {
        return selectSteel.SelectedObjects.FirstOrDefault() as Snap.NX.Body;
    }

    void RereshUI() 
    {
        toggleIsDistinguishSideElec.Show = false;
        groupElec.Show = EactBom.EactBomBusiness.Instance.ConfigData.IsCanSelElecInBom;
        groupElecLayer.Show = EactBom.EactBomBusiness.Instance.ConfigData.isCanSelLayerInBom;
        coord_system0.Show = enum0.SelectedItem == "指定";
        enumSInsert.Show = toggleSInsert.Value;
        selectionSInsert.Show = toggleSInsert.Value && enumSInsert.SelectedItem == "对象";
        integerSInsertLayer.Show= toggleSInsert.Value && enumSInsert.SelectedItem == "图层";
    }
    
    public override void Init()
    {
        var list = new List<string>();
        var proper = EactBom.EactBomBusiness.Instance.ConfigData.Poperties.FirstOrDefault(u => u.DisplayName == "钢件材质");
        if (proper == null)
        {
            list.Add("钢");
            list.Add("铜");
        }
        else 
        {
            list = Enumerable.Select(proper.Selections, u => u.Value).ToList();
        }
        eMATERAL.Items = list.ToArray();
        _items = list;
        
        selectSteel.AllowMultiple = false;
        selectSteel.SetFilter(Snap.NX.ObjectTypes.Type.Body);

        selectElec.AllowMultiple = true;
        selectElec.SetFilter(Snap.NX.ObjectTypes.Type.Body);

        selectionSInsert.AllowMultiple = true;
        selectionSInsert.SetFilter(Snap.NX.ObjectTypes.Type.Body);
    }
    public override void DialogShown()
    {
        sMRNUMBER.Value = string.Empty;
        sMODELNUMBER.Value = string.Empty;
        enumSelectedXX.SelectedIndex = (int)EactBom.EactBomBusiness.Instance.ConfigData.QuadrantType;
        RereshUI(); 
    }

    public override void Update(NXOpen.BlockStyler.UIBlock block)
    {
        RereshUI();
        if (block == selectSteel.NXOpenBlock)
        {
            var steel = GetSteel();
            if (steel != null) 
            {
                var info=EactBom.EactBomBusiness.Instance.GetMouldInfo(steel);

                sMODELNUMBER.Value = info.MODEL_NUMBER;
                sMRNUMBER.Value = info.MR_NUMBER;
                if (_items.Contains(info.MR_MATERAL)) 
                {
                    _items.Remove(info.MR_MATERAL);
                    _items.Insert(0, info.MR_MATERAL);
                    eMATERAL.Items = _items.ToArray();
                    //eMATERAL.SelectedIndex = _items.ToList().IndexOf(info.MR_MATERAL);
                }

                //默认值
                if (string.IsNullOrEmpty(info.MODEL_NUMBER)|| string.IsNullOrEmpty(info.MR_NUMBER))
                {
                    var tempStrs = System.IO.Path.GetFileNameWithoutExtension(Snap.Globals.WorkPart.FullPath).Split('-').ToList();
                    if (tempStrs.Count >= 2)
                    {
                        if (string.IsNullOrEmpty(info.MODEL_NUMBER))
                        {
                            sMODELNUMBER.Value = tempStrs[0];
                        }

                        if (string.IsNullOrEmpty(info.MR_NUMBER))
                        {
                            sMRNUMBER.Value = tempStrs[1];
                        }
                    }
                }
                
            }
        }
    }

    public override void Apply()
    {
        if (string.IsNullOrEmpty(sMODELNUMBER.Value))
        {
            ShowMessage("模具编号不能为空！");
            return;
        }

        if (string.IsNullOrEmpty(sMRNUMBER.Value))
        {
            ShowMessage("钢件编号不能为空！");
            return;
        }

        if (eMATERAL.SelectedIndex < 0)
        {
            ShowMessage("钢件材质不能为空！");
            return;
        }

        Snap.NX.Body steel = GetSteel();
        steel.SetStringAttribute("MODEL_NUMBER", sMODELNUMBER.Value.Trim());
        steel.SetStringAttribute("MR_NUMBER", sMRNUMBER.Value.Trim());
        steel.SetStringAttribute("MR_MATERAL", _items[eMATERAL.SelectedIndex]);
        MouldInfo = EactBom.EactBomBusiness.Instance.GetMouldInfo(steel);
        if (enum0.SelectedItem == "指定")
        {
            MouldInfo.Origin = coord_system0.SpecifiedCsys.Origin;
            MouldInfo.Orientation = coord_system0.SpecifiedCsys.Orientation;
        }
        else if (enum0.SelectedItem == "绝对")
        {
            MouldInfo.Origin = new Snap.Position();
            MouldInfo.Orientation = Snap.Orientation.Identity;
        }
        else if (enum0.SelectedItem == "工作")
        {
            MouldInfo.Origin = Snap.Globals.Wcs.Origin;
            MouldInfo.Orientation = Snap.Globals.Wcs.Orientation;
        }
        else //TODO 默认
        {
            var box = steel.Box;
            MouldInfo.Orientation = Snap.Orientation.Identity;
            MouldInfo.Origin = new Snap.Position((box.MinX + box.MaxX) / 2, (box.MinY + box.MaxY) / 2, box.MaxZ);
        }

        if (toggleSInsert.Value)
        {
            if (integerSInsertLayer.Show)
            {
                var bodies=Snap.Globals.WorkPart.Bodies.Where(u => u.Layer == integerSInsertLayer.Value).ToList();
                bodies = bodies.Where(u => u.NXOpenTag != steel.NXOpenTag).ToList();
                //foreach (var item in bodies.ToList())
                //{
                //    var distance = Snap.Compute.Distance(steel, item);
                //    bool isContact = distance <= SnapEx.Helper.Tolerance;
                //    if (!isContact)
                //    {
                //        bodies.RemoveAll(u=>u.NXOpenTag==item.NXOpenTag);
                //    }
                //}

                MouldInfo.SInsertBodies = bodies;
            }
            else
            {
                MouldInfo.SInsertBodies = Enumerable.Select(selectionSInsert.SelectedObjects, u => Snap.NX.Body.Wrap(u.NXOpenTag)).ToList();
            }
        }

        if (groupElec.Show)
        {
            MouldInfo.ElecBodies = Enumerable.Select(selectElec.SelectedObjects, u => Snap.NX.Body.Wrap(u.NXOpenTag)).ToList();
        }
        else if (groupElecLayer.Show)
        {
            MouldInfo.ElecBodies = Snap.Globals.WorkPart.Bodies.Where(u => u.Layer >= integerElecStartLayer.Value && u.Layer <= integerElecEndLayer.Value).ToList();
        }

        NXOpen.UF.UFSession.GetUFSession().Csys.SetOrigin(Snap.Globals.Wcs.NXOpenTag, MouldInfo.Origin.Array);
        Snap.Globals.WcsOrientation = MouldInfo.Orientation;
        ElecManage.Entry.Instance.DefaultQuadrantType = (QuadrantType)enumSelectedXX.SelectedIndex;
        ElecManage.Entry.Instance.IsDistinguishSideElec = EactBom.EactBomBusiness.Instance.ConfigData.IsDistinguishSideElec;
        ElecManage.Entry.Edition = EactBom.EactBomBusiness.Instance.ConfigData.Edition;
        steel.SetIntegerAttribute(EactBom.EactBomBusiness.EACT_DEFAULTQUADRANTTYPE, enumSelectedXX.SelectedIndex);

        Result = System.Windows.Forms.DialogResult.OK;
    }

}