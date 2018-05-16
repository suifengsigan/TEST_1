namespace Snap.UI.Block
{
    using NXOpen.BlockStyler;
    using System;

    public class TableLayout : General
    {
        private TableLayout()
        {
            this.Initialize();
        }

        internal TableLayout(UIBlock uiBlock)
        {
            base.NXOpenBlock = uiBlock;
        }

        public static TableLayout GetBlock(BlockDialog dialog, string name)
        {
            return new TableLayout(dialog.TopBlock.FindBlock(name));
        }

        private void Initialize()
        {
            base.Type = "TableLayout";
            base.Show = true;
            this.BeginGroup = false;
            this.Enabled = true;
            this.Expanded = true;
            this.HasColumnLabels = true;
            this.HighQualityBitmap = true;
            this.Label = "Table Layout";
            this.Localize = true;
            this.NumberOfColumns = 2;
            this.RetainValue = true;
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

        public bool HasColumnLabels
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "HasColumnLabels");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "HasColumnLabels", value);
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

        public UIBlock[] Members
        {
            get
            {
                if (base.NXOpenBlock == null)
                {
                    return null;
                }
                PropertyList properties = base.NXOpenBlock.GetProperties();
                PropertyList array = properties.GetArray("Members");
                int length = array.GetPropertyNames().Length;
                UIBlock[] blockArray = new UIBlock[length];
                for (int i = 0; i < length; i++)
                {
                    blockArray[i] = array.GetUIBlock(i);
                }
                properties.Dispose();
                return blockArray;
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
    }
}

