namespace Snap.UI.Block
{
    using NXOpen.BlockStyler;
    using System;

    public class MultilineString : General
    {
        public MultilineString()
        {
            this.Initialize();
        }

        internal MultilineString(UIBlock uiBlock)
        {
            base.NXOpenBlock = uiBlock;
        }

        public static Snap.UI.Block.MultilineString GetBlock(BlockDialog dialog, string name)
        {
            return new Snap.UI.Block.MultilineString(dialog.TopBlock.FindBlock(name));
        }

        private void Initialize()
        {
            base.Type = "Multiline String";
            base.Show = true;
            this.BeginGroup = false;
            this.Enabled = true;
            this.Expanded = true;
            this.Height = 3;
            this.Label = "Multiline String";
            this.Localize = true;
            this.MaximumCharactersAccepted = 0;
            this.MaximumHeight = 0;
            this.MinimumHeight = 0;
            this.ResizeHeightWithDialog = true;
            this.RetainValue = true;
            this.Value = null;
            this.Width = 0;
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

        public int MaximumCharactersAccepted
        {
            get
            {
                return (int) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Integer, "MaximumCharactersAccepted");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Integer, "MaximumCharactersAccepted", value);
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

        public string[] Value
        {
            get
            {
                return (string[]) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.StringArray, "Value");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.StringArray, "Value", value);
            }
        }

        public int Width
        {
            get
            {
                return (int) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Integer, "Width");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Integer, "Width", value);
            }
        }
    }
}

