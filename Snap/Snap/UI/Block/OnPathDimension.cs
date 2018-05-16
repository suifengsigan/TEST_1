namespace Snap.UI.Block
{
    using NXOpen;
    using NXOpen.BlockStyler;
    using Snap.NX;
    using System;

    public class OnPathDimension : General
    {
        public OnPathDimension()
        {
            this.Initialize();
        }

        internal OnPathDimension(UIBlock uiBlock)
        {
            base.NXOpenBlock = uiBlock;
        }

        public static Snap.UI.Block.OnPathDimension GetBlock(BlockDialog dialog, string name)
        {
            return new Snap.UI.Block.OnPathDimension(dialog.TopBlock.FindBlock(name));
        }

        private void Initialize()
        {
            base.Type = "On Path Dim";
            base.Show = true;
            this.BalloonTooltipImage = "";
            this.BalloonTooltipLayout = Snap.UI.Block.BalloonTooltipLayout.Horizontal;
            this.BalloonTooltipText = "";
            this.BeginGroup = false;
            this.Enabled = true;
            this.Expanded = true;
            this.Label = "On Path Dimension";
            this.LocationOption = 0;
            this.OptionMask = 15;
            this.Path = null;
            this.ShowFocusHandle = false;
            this.WithScale = true;
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

        public int LocationOption
        {
            get
            {
                return (int) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Enum, "LocationOption");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Enum, "LocationOption", value);
            }
        }

        public int OptionMask
        {
            get
            {
                return (int) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.BitArray, "OptionMask");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.BitArray, "OptionMask", value);
            }
        }

        public Snap.NX.NXObject Path
        {
            get
            {
                TaggedObject nxopenTaggedObject = (TaggedObject) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Object, "Path");
                return Snap.NX.NXObject.CreateNXObject(nxopenTaggedObject);
            }
            set
            {
                TaggedObject propValue = null;
                if (value != null)
                {
                    propValue = value.NXOpenTaggedObject;
                }
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Object, "Path", propValue);
            }
        }

        public bool ShowFocusHandle
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "ShowFocusHandle");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "ShowFocusHandle", value);
            }
        }

        public Snap.NX.Unit Units
        {
            get
            {
                TaggedObject obj2 = (TaggedObject) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Object, "Units");
                NXOpen.Unit unit = (NXOpen.Unit) obj2;
                return Snap.NX.Unit.Wrap(unit.Tag);
            }
            set
            {
                NXOpen.Unit propValue = null;
                if (value != null)
                {
                    propValue = value.NXOpenUnit;
                }
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Object, "Units", propValue);
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
                if (base.NXOpenBlock == null)
                {
                    PropertyKey key = new PropertyKey(Snap.UI.Block.PropertyType.Double, "Value");
                    base.PropertyDictionary[key] = value;
                    key = new PropertyKey(Snap.UI.Block.PropertyType.String, "Formula");
                    base.PropertyDictionary[key] = value.ToString();
                }
                else
                {
                    PropertyAccess.NxSetDouble(base.NXOpenBlock, "Value", value);
                }
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

