namespace Snap.UI.Block
{
    using NXOpen.BlockStyler;
    using System;

    public class IntegerTable : General
    {
        public IntegerTable()
        {
            this.Initialize();
        }

        internal IntegerTable(UIBlock uiBlock)
        {
            base.NXOpenBlock = uiBlock;
        }

        public static Snap.UI.Block.IntegerTable GetBlock(BlockDialog dialog, string name)
        {
            return new Snap.UI.Block.IntegerTable(dialog.TopBlock.FindBlock(name));
        }

        private void Initialize()
        {
            base.Type = "Integer Table";
            base.Show = true;
            this.BeginGroup = false;
            this.ColumnTitles = null;
            this.Enabled = true;
            this.Expanded = true;
            this.Label = "Integer Table";
            this.MaximumValues = null;
            this.MinimumValues = null;
            this.RowTitles = null;
            this.Spin = false;
            this.Values = null;
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

        public string[] ColumnTitles
        {
            get
            {
                return (string[]) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.StringArray, "ColumnTitles");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.StringArray, "ColumnTitles", value);
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

        public int[,] MaximumValues
        {
            get
            {
                return (int[,]) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.IntegerMatrix, "MaximumValues");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.IntegerMatrix, "MaximumValues", value);
            }
        }

        public int[,] MinimumValues
        {
            get
            {
                return (int[,]) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.IntegerMatrix, "MinimumValues");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.IntegerMatrix, "MinimumValues", value);
            }
        }

        public string[] RowTitles
        {
            get
            {
                return (string[]) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.StringArray, "RowTitles");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.StringArray, "RowTitles", value);
            }
        }

        public bool Spin
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "Spin");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "Spin", value);
            }
        }

        public int[,] Values
        {
            get
            {
                return (int[,]) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.IntegerMatrix, "Values");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.IntegerMatrix, "Values", value);
            }
        }
    }
}

