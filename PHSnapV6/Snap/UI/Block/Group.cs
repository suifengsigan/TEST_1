namespace Snap.UI.Block
{
    using NXOpen.BlockStyler;
    using System;

    public class Group : General
    {
        private Group()
        {
            this.Initialize();
        }

        internal Group(UIBlock uiBlock)
        {
            base.NXOpenBlock = uiBlock;
        }

        public static Snap.UI.Block.Group GetBlock(BlockDialog dialog, string name)
        {
            return new Snap.UI.Block.Group(dialog.TopBlock.FindBlock(name));
        }

        private void Initialize()
        {
            base.Type = "Group";
            base.Show = true;
            this.Enabled = true;
            this.Expanded = true;
            this.Label = "Group";
            this.Localize = true;
            this.NumberOfColumns = 1;
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
                return (int) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Integer, "Column");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Integer, "Column", value);
            }
        }
    }
}

