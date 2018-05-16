namespace Snap.UI.Block
{
    using NXOpen.BlockStyler;
    using System;

    public class String : General
    {
        public String()
        {
            this.Initialize();
        }

        internal String(UIBlock uiBlock)
        {
            base.NXOpenBlock = uiBlock;
        }

        public String(string label)
        {
            this.Initialize();
            this.Label = label;
        }

        public static Snap.UI.Block.String GetBlock(BlockDialog dialog, string name)
        {
            return new Snap.UI.Block.String(dialog.TopBlock.FindBlock(name));
        }

        private void Initialize()
        {
            base.Type = "String";
            base.Show = true;
            this.AllowInternationalTextInput = true;
            this.BeginGroup = false;
            this.Bitmap = "";
            this.Enabled = true;
            this.Expanded = true;
            this.IsSecret = false;
            this.Label = "String";
            this.ListItems = null;
            this.Localize = true;
            this.MaxTextLength = 0;
            this.PresentationStyle = StringPresentationStyle.KeyIn;
            this.ReadOnly = false;
            this.RetainValue = true;
            this.Tooltip = "";
            this.Value = "";
            this.WideValue = "";
        }

        internal bool AllowInternationalTextInput
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "AllowInternationalTextInput");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "AllowInternationalTextInput", value);
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

        public string Bitmap
        {
            get
            {
                return (string) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.String, "Bitmap");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.String, "Bitmap", value);
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

        public bool IsSecret
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "IsPassword");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "IsPassword", value);
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

        public int MaxTextLength
        {
            get
            {
                return (int) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Integer, "MaxTextLength");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Integer, "MaxTextLength", value);
            }
        }

        public StringPresentationStyle PresentationStyle
        {
            get
            {
                return (StringPresentationStyle) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Enum, "PresentationStyle");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Enum, "PresentationStyle", value);
            }
        }

        public bool ReadOnly
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "ReadOnlyString");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "ReadOnlyString", value);
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

        public string Tooltip
        {
            get
            {
                return (string) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.String, "Tooltip");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.String, "Tooltip", value);
            }
        }

        public string Value
        {
            get
            {
                if (this.AllowInternationalTextInput)
                {
                    return (string) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.String, "WideValue");
                }
                return (string) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.String, "Value");
            }
            set
            {
                if (this.AllowInternationalTextInput)
                {
                    PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.String, "WideValue", value);
                }
                else
                {
                    PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.String, "Value", value);
                }
            }
        }

        public string WideValue
        {
            get
            {
                return (string) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.String, "WideValue");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.String, "WideValue", value);
            }
        }
    }
}

