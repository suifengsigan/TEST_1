namespace Snap.UI.Block
{
    using NXOpen.BlockStyler;
    using System;

    public class DoubleTable : General
    {
        public DoubleTable()
        {
            this.Initialize();
        }

        internal DoubleTable(UIBlock uiBlock)
        {
            base.NXOpenBlock = uiBlock;
        }

        public static Snap.UI.Block.DoubleTable GetBlock(BlockDialog dialog, string name)
        {
            return new Snap.UI.Block.DoubleTable(dialog.TopBlock.FindBlock(name));
        }

        private void Initialize()
        {
            base.Type = "Double Table";
            base.Show = true;
            this.Values = null;
            this.MaximumValues = null;
            this.MinimumValues = null;
            this.BeginGroup = false;
            this.ColumnTitles = null;
            this.Enabled = true;
            this.Expanded = true;
            this.Label = "Double Table";
            this.RowTitles = null;
            this.Spin = false;
            this.CellWidth = 0;
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

        public int CellWidth
        {
            get
            {
                return (int) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Integer, "CellWidth");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Integer, "CellWidth", value);
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

        public double[,] MaximumValues
        {
            get
            {
                return (double[,]) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.DoubleMatrix, "MaximumValues");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.DoubleMatrix, "MaximumValues", value);
            }
        }

        public double[,] MinimumValues
        {
            get
            {
                return (double[,]) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.DoubleMatrix, "MinimumValues");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.DoubleMatrix, "MinimumValues", value);
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

        public double[,] Values
        {
            get
            {
                return (double[,]) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.DoubleMatrix, "Values");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.DoubleMatrix, "Values", value);
            }
        }
    }
}

