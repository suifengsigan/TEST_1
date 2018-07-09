using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
partial class SetPropertyUI : SnapEx.BaseUI
{
    EactConfig.ConfigData _configData = EactConfig.ConfigData.GetInstance();
    public override void Init()
    {
        selectCuprum.AllowMultiple = true;
        selectCuprum.SetFilter(Snap.NX.ObjectTypes.Type.Body, Snap.NX.ObjectTypes.SubType.BodySolid);

        _configData.Poperties.ForEach(u => {
            Snap.UI.Block.General cbb = null;
            if (u.DisplayName == "电极材质")
            {
                cbb = cboxMATNAME;
            }
            else if (u.DisplayName == "加工方向")
            {
                cbb = cbbProdirection;
            }
            else if (u.DisplayName == "电极类型")
            {
                cbb = cbbElecType;
            }
            else if (u.DisplayName == "摇摆方式")
            {
                cbb = cbbRock;
            }
            else if (u.DisplayName == "精公光洁度")
            {
                cbb = cbbFSmoth;
            }
            else if (u.DisplayName == "中公光洁度")
            {
                cbb = cbbMSmoth;
            }
            else if (u.DisplayName == "粗公光洁度")
            {
                cbb = cbbRSmoth;
            }
            else if (u.DisplayName == "夹具类型")
            {
                cbb = cbbChuckType;
            }

            if (cbb is Snap.UI.Block.Enumeration)
            {
                var enumration = cbb as Snap.UI.Block.Enumeration;
                enumration.Items = Enumerable.Select(u.Selections, m => m.Value).ToArray();
                var defaultSelection = u.Selections.FirstOrDefault(m => m.IsDefault) ?? u.Selections.FirstOrDefault();
                //enumration.SelectedIndex = u.Selections.IndexOf(defaultSelection);
            }
        });
    }

    public override void DialogShown()
    {
        
    }

    public override void Update(NXOpen.BlockStyler.UIBlock block)
    {
        if (block == selectCuprum.NXOpenBlock)
        {
        }
    }

    public override void Apply()
    {

    }
}