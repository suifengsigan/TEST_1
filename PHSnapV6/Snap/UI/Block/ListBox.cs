namespace Snap.UI.Block
{
    using NXOpen.BlockStyler;
    using System;

    public class ListBox : General
    {
        public ListBox()
        {
            this.Initialize();
        }

        internal ListBox(UIBlock uiBlock)
        {
            base.NXOpenBlock = uiBlock;
        }

        public static Snap.UI.Block.ListBox GetBlock(BlockDialog dialog, string name)
        {
            return new Snap.UI.Block.ListBox(dialog.TopBlock.FindBlock(name));
        }

        private void Initialize()
        {
            base.Type = "List Box";
            base.Show = true;
            this.BeginGroup = false;
            this.Enabled = true;
            this.Expanded = true;
            this.Height = 3;
            this.IsAddButtonSensitive = true;
            this.IsDeleteButtonSensitive = true;
            this.Label = "List Box";
            this.ListItems = null;
            this.Localize = true;
            this.MaximumHeight = 0;
            this.MaximumStringLength = 0;
            this.MinimumHeight = 0;
            this.ResizeHeightWithDialog = true;
            this.SelectedItems = null;
            this.ShowAddButton = false;
            this.ShowDeleteButton = false;
            this.ShowMoveUpDownButtons = false;
            this.SingleSelect = false;
        }

        public bool BeginGroup
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "Group");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "Group", value);
            }
        }

        public bool Enabled
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "Enable");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "Enable", value);
            }
        }

        public bool Expanded
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "Expanded");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "Expanded", value);
            }
        }

        public int Height
        {
            get
            {
                return (int) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Integer, "Height");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Integer, "Height", value);
            }
        }

        public bool IsAddButtonSensitive
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "IsAddButtonSensitive");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "IsAddButtonSensitive", value);
            }
        }

        public bool IsDeleteButtonSensitive
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "IsDeleteButtonSensitive");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "IsDeleteButtonSensitive", value);
            }
        }

        public string Label
        {
            get
            {
                return (string) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.String, "Label");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.String, "Label", value);
            }
        }

        public string[] ListItems
        {
            get
            {
                return (string[]) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.StringArray, "ListItems");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.StringArray, "ListItems", value);
            }
        }

        public bool Localize
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "Localize");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "Localize", value);
            }
        }

        public int MaximumHeight
        {
            get
            {
                return (int) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Integer, "MaximumHeight");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Integer, "MaximumHeight", value);
            }
        }

        public int MaximumStringLength
        {
            get
            {
                return (int) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Integer, "MaximumStringLength");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Integer, "MaximumStringLength", value);
            }
        }

        public int MinimumHeight
        {
            get
            {
                return (int) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Integer, "MinimumHeight");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Integer, "MinimumHeight", value);
            }
        }

        public bool ResizeHeightWithDialog
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "ResizeHeightWithDialog");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "ResizeHeightWithDialog", value);
            }
        }

        public int[] SelectedItems
        {
            get
            {
                return (int[]) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.IntegerArray, "SelectedItems");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.IntegerArray, "SelectedItems", value);
            }
        }

        public bool ShowAddButton
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "ShowAddButton");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "ShowAddButton", value);
            }
        }

        public bool ShowDeleteButton
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "ShowDeleteButton");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "ShowDeleteButton", value);
            }
        }

        public bool ShowMoveUpDownButtons
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "ShowMoveUpDownButtons");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "ShowMoveUpDownButtons", value);
            }
        }

        public bool SingleSelect
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "SingleSelect");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "SingleSelect", value);
            }
        }
    }
}

