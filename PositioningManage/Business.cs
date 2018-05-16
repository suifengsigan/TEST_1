using NXOpen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

partial class PositioningManageUI:SnapEx.BaseUI
{

    public override void DialogShown()
    {
        tree_control0.InsertColumn(0, "序号", 200);
        tree_control0.InsertColumn(1, "跑位X", 200);
        tree_control0.InsertColumn(2, "跑位Y", 200);
        tree_control0.InsertColumn(3, "跑位Z", 200);

        tree_control0.SetColumnResizePolicy(0, NXOpen.BlockStyler.Tree.ColumnResizePolicy.ResizeWithTree);
        tree_control0.SetColumnResizePolicy(1, NXOpen.BlockStyler.Tree.ColumnResizePolicy.ResizeWithTree);
        tree_control0.SetColumnResizePolicy(2, NXOpen.BlockStyler.Tree.ColumnResizePolicy.ResizeWithTree);
        tree_control0.SetColumnResizePolicy(3, NXOpen.BlockStyler.Tree.ColumnResizePolicy.ResizeWithTree);
    }
    public override void Init()
    {
        var snapSelectElec = Snap.UI.Block.SelectObject.GetBlock(theDialog, selection0.Name);
        snapSelectElec.LabelString = "选择电极";
        snapSelectElec.MaximumScope = Snap.UI.Block.SelectionScope.AnyInAssembly;
        snapSelectElec.SetFilter(Snap.NX.ObjectTypes.Type.Component);
        snapSelectElec.AllowMultiple = false;
        tree_control0.SetOnSelectHandler(new NXOpen.BlockStyler.Tree.OnSelectCallback(OnSelectcallback));
    }
    public override void Apply()
    { 
    }

    public override void Update(NXOpen.BlockStyler.UIBlock block)
    {
        if (block == selection0) 
        {
            Snap.NX.Component component = selection0.GetSelectedObjects().FirstOrDefault() as NXOpen.Assemblies.Component;
            var com=component.Parent.Children.FirstOrDefault(u => u.Name == component.Name && u.GetAttributeInfo().Where(m => m.Title == SnapEx.ConstString.EACT_POSITIONING_DATE).Count() <= 0);
            if (com != null) 
            {
                selection0.SetSelectedObjects(new TaggedObject[] { com.NXOpenComponent });
                UpdateTreeControl();
            }
        }
        else if (block == button0) //删除按钮
        {
            var node=tree_control0.GetSelectedNodes().FirstOrDefault();
            if (node != null) 
            {
                var index = int.Parse(node.DisplayText) - 1;
                
                if (coms.Count > index)
                {
                    var com = coms[index];
                    coms.Remove(com);
                    com.Delete();
                    UpdateTreeControl();
                }
            }
        }
    }

    public override void Close()
    {
        coms.ForEach(u =>
        {
            u.IsHighlighted = false;
        });
    }

    List<Snap.NX.Component> coms = new List<Snap.NX.Component>();

    void UpdateTreeControl() 
    {
        DeleteTreeNodes();
        Snap.NX.Component component = selection0.GetSelectedObjects().FirstOrDefault() as NXOpen.Assemblies.Component;
        component.IsHighlighted = false;
        coms = new List<Snap.NX.Component>();
        component.Parent.Children.ToList().ForEach(u =>
        {
            if (u.Name == component.Name)
            {
                if (u.GetAttributeInfo().Where(m => m.Title == SnapEx.ConstString.EACT_POSITIONING_DATE).Count() > 0)
                {
                    coms.Add(u);
                }
            }
        });

        coms.OrderBy(u => u.GetDateTimeAttribute(SnapEx.ConstString.EACT_POSITIONING_DATE));

        for (int i = coms.Count - 1; i >= 0; i--)
        {
            var com = coms[i];
            var node = tree_control0.CreateNode((i + 1).ToString());
            tree_control0.InsertNode(node, null, null, NXOpen.BlockStyler.Tree.NodeInsertOption.AlwaysFirst);
            node.SetColumnDisplayText(1, com.Position.X.ToString());
            node.SetColumnDisplayText(2, com.Position.X.ToString());
            node.SetColumnDisplayText(3, com.Position.X.ToString());
        }
    }
    void OnSelectcallback(NXOpen.BlockStyler.Tree tree, NXOpen.BlockStyler.Node node, int columnID, bool selected) 
    {
        var index = int.Parse(node.DisplayText) - 1;
        coms.ForEach(u=>{
            u.IsHighlighted=false;
        });
        if (coms.Count > index)
        {
            var com = coms[index];
            com.IsHighlighted = true;
        }
    }

    void DeleteTreeNodes() 
    {
        coms.Clear();
        var nodes = new List<NXOpen.BlockStyler.Node>();
        if (tree_control0.RootNode != null) 
        {
            var node=tree_control0.RootNode;
            nodes.Add(node);

            while (node.NextNode != null) 
            {
                node = node.NextNode;
                nodes.Add(node);
            }
        }

        nodes.ForEach(u => {
            tree_control0.DeleteNode(u);
        });
    }
}