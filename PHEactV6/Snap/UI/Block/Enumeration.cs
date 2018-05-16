namespace Snap.UI.Block
{
    using NXOpen.BlockStyler;
    using System;
    using System.Runtime.InteropServices;

    public class Enumeration : General
    {
        public Enumeration()
        {
            this.Initialize();
        }

        internal Enumeration(UIBlock uiBlock)
        {
            base.NXOpenBlock = uiBlock;
        }

        public Enumeration(string label, EnumPresentationStyle style = 0)
        {
            this.Initialize();
            this.Label = label;
            this.PresentationStyle = style;
        }

        public static Snap.UI.Block.Enumeration GetBlock(BlockDialog dialog, string name)
        {
            return new Snap.UI.Block.Enumeration(dialog.TopBlock.FindBlock(name));
        }

        private void Initialize()
        {
            base.Type = "Enumeration";
            base.Show = true;
            this.AllowShortcuts = true;
            this.BeginGroup = false;
            this.Bitmaps = null;
            this.BorderVisibility = true;
            this.Enabled = true;
            this.Expanded = true;
            this.HighQualityBitmap = true;
            this.IconsOnly = false;
            this.InitialShortcuts = null;
            this.Items = null;
            this.Label = "Enumeration";
            this.LabelVisibility = true;
            this.Layout = Snap.UI.Block.Layout.Vertical;
            this.Localize = true;
            this.NumberOfColumns = 0;
            this.PresentationStyle = EnumPresentationStyle.OptionMenu;
            this.RetainValue = true;
            this.SelectedIndex = 0;
            this.Value = 0;
        }

        public bool AllowShortcuts
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "AllowShortcuts");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "AllowShortcuts", value);
            }
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

        public string[] Bitmaps
        {
            get
            {
                return (string[]) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.StringArray, "Bitmaps");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.StringArray, "Bitmaps", value);
            }
        }

        public bool BorderVisibility
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "BorderVisibility");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "BorderVisibility", value);
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

        public bool HighQualityBitmap
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "HighQualityBitmap");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "HighQualityBitmap", value);
            }
        }

        public bool IconsOnly
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "IconsOnly");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "IconsOnly", value);
            }
        }

        public int[] InitialShortcuts
        {
            get
            {
                return (int[]) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.IntegerArray, "InitialShortcuts");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.IntegerArray, "InitialShortcuts", value);
            }
        }

        public string[] Items
        {
            get
            {
                return (string[]) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.EnumMembers, "Value");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.EnumMembers, "Value", value);
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

        public bool LabelVisibility
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "LabelVisibility");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "LabelVisibility", value);
            }
        }

        public Snap.UI.Block.Layout Layout
        {
            get
            {
                return (Snap.UI.Block.Layout) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Enum, "Layout");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Enum, "Layout", value);
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

        public int NumberOfColumns
        {
            get
            {
                return (int) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Integer, "NumberOfColumns");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Integer, "NumberOfColumns", value);
            }
        }

        public EnumPresentationStyle PresentationStyle
        {
            get
            {
                return (EnumPresentationStyle) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Enum, "PresentationStyle");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Enum, "PresentationStyle", value);
            }
        }

        public bool RetainValue
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "RetainValue");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "RetainValue", value);
            }
        }

        public int SelectedIndex
        {
            get
            {
                return this.Value;
            }
            set
            {
                this.Value = value;
            }
        }

        public string SelectedItem
        {
            get
            {
                return (string) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.EnumAsString, "Value");
            }
        }

        public int Value
        {
            get
            {
                return (int) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Enum, "Value");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Enum, "Value", value);
            }
        }
    }
}

