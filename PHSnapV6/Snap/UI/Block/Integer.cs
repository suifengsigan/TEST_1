namespace Snap.UI.Block
{
    using NXOpen.BlockStyler;
    using System;
    using System.Runtime.InteropServices;

    public class Integer : General
    {
        public Integer()
        {
            this.Initialize();
        }

        internal Integer(UIBlock uiBlock)
        {
            base.NXOpenBlock = uiBlock;
        }

        public Integer(string label, int initialValue = 0)
        {
            this.Initialize();
            this.Label = label;
            this.Value = initialValue;
        }

        public static Integer GetBlock(BlockDialog dialog, string blockID)
        {
            return new Integer(dialog.TopBlock.FindBlock(blockID));
        }

        private void Initialize()
        {
            base.Type = "Integer";
            base.Show = true;
            this.AdaptiveScaleLimits = false;
            this.BalloonTooltipImage = "";
            this.BalloonTooltipLayout = Snap.UI.Block.BalloonTooltipLayout.Horizontal;
            this.BalloonTooltipText = "";
            this.BeginGroup = false;
            this.Bitmap = "";
            this.Enabled = true;
            this.Expanded = true;
            this.Increment = 1.0;
            this.Label = "Integer";
            this.LabelVisibility = true;
            this.LineIncrement = 1E+19;
            this.Localize = true;
            this.MaximumValue = 0x7fffffff;
            this.MaxInclusive = true;
            this.MinimumValue = -2147483648;
            this.MinInclusive = true;
            this.PageIncrement = 1E+19;
            this.PresentationStyle = NumberPresentationStyle.KeyIn;
            this.RetainValue = true;
            this.ScaleLimits = true;
            this.ScaleMaxLimitLabel = "";
            this.ScaleMinLimitLabel = "";
            this.ShowScaleValue = true;
            this.ReadOnly = false;
            this.Value = 0;
            this.WrapSpin = false;
        }

        public bool AdaptiveScaleLimits
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "AdaptiveScaleLimits");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "AdaptiveScaleLimits", value);
            }
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

        public double Increment
        {
            get
            {
                return (double) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Double, "Increment");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Double, "Increment", value);
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
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "TitleVisibility");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "TitleVisibility", value);
            }
        }

        public double LineIncrement
        {
            get
            {
                return (double) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Double, "LineIncrement");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Double, "LineIncrement", value);
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

        public int MaximumValue
        {
            get
            {
                return (int) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Integer, "MaximumValue");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Integer, "MaximumValue", value);
            }
        }

        public bool MaxInclusive
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "MaxInclusive");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "MaxInclusive", value);
            }
        }

        public int MinimumValue
        {
            get
            {
                return (int) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Integer, "MinimumValue");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Integer, "MinimumValue", value);
            }
        }

        public bool MinInclusive
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "MinInclusive");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "MinInclusive", value);
            }
        }

        public double PageIncrement
        {
            get
            {
                return (double) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Double, "PageIncrement");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Double, "PageIncrement", value);
            }
        }

        public NumberPresentationStyle PresentationStyle
        {
            get
            {
                return (NumberPresentationStyle) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Enum, "PresentationStyle");
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
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "ReadOnlyValue");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "ReadOnlyValue", value);
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

        public bool ScaleLimits
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "ScaleLimits");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "ScaleLimits", value);
            }
        }

        public string ScaleMaxLimitLabel
        {
            get
            {
                return (string) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.String, "ScaleMaxLimitLabel");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.String, "ScaleMaxLimitLabel", value);
            }
        }

        public string ScaleMinLimitLabel
        {
            get
            {
                return (string) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.String, "ScaleMinLimitLabel");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.String, "ScaleMinLimitLabel", value);
            }
        }

        public bool ShowScaleValue
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "ShowScaleValue");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "ShowScaleValue", value);
            }
        }

        public int Value
        {
            get
            {
                return (int) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Integer, "Value");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Integer, "Value", value);
            }
        }

        public bool WrapSpin
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "WrapSpin");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "WrapSpin", value);
            }
        }
    }
}

