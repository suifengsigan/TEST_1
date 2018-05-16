namespace Snap.UI.Block
{
    using NXOpen;
    using NXOpen.BlockStyler;
    using Snap.NX;
    using System;

    public class SelectExpression : General
    {
        public SelectExpression()
        {
            this.Initialize();
        }

        internal SelectExpression(UIBlock uiBlock)
        {
            base.NXOpenBlock = uiBlock;
        }

        private SelectExpression(BlockDialog dialog, string blockID)
        {
            base.NXOpenBlock = dialog.TopBlock.FindBlock(blockID);
        }

        public static SelectExpression GetBlock(BlockDialog dialog, string name)
        {
            return new SelectExpression(dialog.TopBlock.FindBlock(name));
        }

        private void Initialize()
        {
            base.Type = "Select Expression";
            base.Show = true;
            this.BeginGroup = false;
            this.Enabled = true;
            this.Expanded = true;
            this.ExpressionSortOrder = Snap.UI.Block.ExpressionSortOrder.Alphanumeric;
            this.ExpressionType = Snap.UI.Block.ExpressionType.Number;
            this.Label = "Select Expression";
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

        public Snap.UI.Block.ExpressionSortOrder ExpressionSortOrder
        {
            get
            {
                return (Snap.UI.Block.ExpressionSortOrder) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Enum, "ExpressionSortType");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Enum, "ExpressionSortType", value);
            }
        }

        public Snap.UI.Block.ExpressionType ExpressionType
        {
            get
            {
                return (Snap.UI.Block.ExpressionType) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Enum, "ExpressionTypeIndex");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Enum, "ExpressionTypeIndex", value);
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

        public Snap.NX.Expression SelectedExpression
        {
            get
            {
                TaggedObject obj2 = (TaggedObject) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Object, "SelectedExpression");
                NXOpen.Expression expression = (NXOpen.Expression) obj2;
                return Snap.NX.Expression.Wrap(expression.Tag);
            }
            set
            {
                NXOpen.Expression propValue = null;
                if (value != null)
                {
                    propValue = value.NXOpenExpression;
                }
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Object, "SelectedExpression", propValue);
            }
        }
    }
}

