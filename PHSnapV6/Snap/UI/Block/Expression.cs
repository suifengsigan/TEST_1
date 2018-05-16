namespace Snap.UI.Block
{
    using NXOpen;
    using NXOpen.BlockStyler;
    using Snap.NX;
    using System;

    public class Expression : General
    {
        public Expression()
        {
            this.Initialize();
        }

        internal Expression(UIBlock uiBlock)
        {
            base.NXOpenBlock = uiBlock;
        }

        public Expression(string label)
        {
            this.Initialize();
            this.Label = label;
        }

        private Expression(BlockDialog dialog, string blockID)
        {
            base.NXOpenBlock = dialog.TopBlock.FindBlock(blockID);
        }

        public static Snap.UI.Block.Expression GetBlock(BlockDialog dialog, string name)
        {
            return new Snap.UI.Block.Expression(dialog.TopBlock.FindBlock(name));
        }

        private void Initialize()
        {
            base.Type = "Expression";
            base.Show = true;
            this.BalloonTooltipImage = "";
            this.BalloonTooltipLayout = Snap.UI.Block.BalloonTooltipLayout.Horizontal;
            this.BalloonTooltipText = "";
            this.BeginGroup = false;
            this.Dimensionality = Snap.UI.Block.DimensionalityType.None;
            this.Enabled = true;
            this.Expanded = true;
            this.ExpressionObject = null;
            this.Formula = "0";
            this.HasUnitsMenu = false;
            this.Label = "Expression";
            this.Units = null;
            this.Value = 0.0;
            this.WithScale = false;
        }

        public string BalloonTooltipImage
        {
            get
            {
                return (string) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.String, "BalloonTooltipImage");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.String, "BalloonTooltipImage", value);
            }
        }

        public Snap.UI.Block.BalloonTooltipLayout BalloonTooltipLayout
        {
            get
            {
                return (Snap.UI.Block.BalloonTooltipLayout) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Enum, "BalloonTooltipLayout");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Enum, "BalloonTooltipLayout", value);
            }
        }

        public string BalloonTooltipText
        {
            get
            {
                return (string) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.String, "BalloonTooltipText");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.String, "BalloonTooltipText", value);
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

        public Snap.UI.Block.DimensionalityType Dimensionality
        {
            get
            {
                return (Snap.UI.Block.DimensionalityType) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Enum, "Dimensionality");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Enum, "Dimensionality", value);
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

        public ExpressionNumber ExpressionObject
        {
            get
            {
                TaggedObject obj2 = (TaggedObject) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Object, "ExpressionObject");
                return new ExpressionNumber((NXOpen.Expression) obj2);
            }
            set
            {
                NXOpen.Expression propValue = null;
                if (value != null)
                {
                    propValue = value.NXOpenExpression;
                }
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Object, "ExpressionObject", propValue);
            }
        }

        public string Formula
        {
            get
            {
                return (string) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.String, "Formula");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.String, "Formula", value);
            }
        }

        public bool HasUnitsMenu
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "HasUnitsMenu");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "HasUnitsMenu", value);
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

        public NXOpen.Unit Units
        {
            get
            {
                TaggedObject obj2 = (TaggedObject) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Object, "Units");
                return (NXOpen.Unit) obj2;
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Object, "Units", value);
            }
        }

        public double Value
        {
            get
            {
                return (double) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Double, "Value");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Double, "Value", value);
            }
        }

        public bool WithScale
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "WithScale");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "WithScale", value);
            }
        }
    }
}

