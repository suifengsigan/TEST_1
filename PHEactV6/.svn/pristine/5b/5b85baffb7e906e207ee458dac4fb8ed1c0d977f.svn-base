namespace Snap.UI.Block
{
    using NXOpen.BlockStyler;
    using Snap;
    using System;
    using System.Runtime.CompilerServices;

    public class SpecifyOrientation : General
    {
        public SpecifyOrientation()
        {
            this.Initialize();
        }

        internal SpecifyOrientation(UIBlock uiBlock)
        {
            base.NXOpenBlock = uiBlock;
        }

        public static Snap.UI.Block.SpecifyOrientation GetBlock(BlockDialog dialog, string name)
        {
            return new Snap.UI.Block.SpecifyOrientation(dialog.TopBlock.FindBlock(name));
        }

        private void Initialize()
        {
            base.Type = "Specify Orientation";
            base.Show = true;
            this.AxisX = Vector.AxisX;
            this.AxisY = Vector.AxisY;
            this.BalloonTooltipImage = "";
            this.BalloonTooltipLayout = Snap.UI.Block.BalloonTooltipLayout.Horizontal;
            this.BalloonTooltipText = "";
            this.BeginGroup = false;
            this.Enabled = false;
            this.EnableFacetSelection = false;
            this.Expanded = true;
            this.HasOriginGwif = true;
            this.IsOriginSpecified = false;
            this.IsWCSCoordinates = false;
            this.Label = "Specify Orientation";
            this.Origin = Position.Origin;
            this.SnapPointStates = new SnapPointsStateSet(this);
            this.SnapPointStates.ArcCenter = SnapPointState.Selected;
            this.SnapPointStates.BoundedGridPoint = SnapPointState.Shown;
            this.SnapPointStates.ControlPoint = SnapPointState.Shown;
            this.SnapPointStates.EndPoint = SnapPointState.Selected;
            this.SnapPointStates.ExistingPoint = SnapPointState.Selected;
            this.SnapPointStates.Inferred = SnapPointState.Shown;
            this.SnapPointStates.Intersection = SnapPointState.Shown;
            this.SnapPointStates.MidPoint = SnapPointState.Selected;
            this.SnapPointStates.PointConstructor = SnapPointState.Shown;
            this.SnapPointStates.PointOnCurve = SnapPointState.Shown;
            this.SnapPointStates.PointOnSurface = SnapPointState.Shown;
            this.SnapPointStates.Pole = SnapPointState.Shown;
            this.SnapPointStates.QuadrantPoint = SnapPointState.Shown;
            this.SnapPointStates.ScreenPosition = SnapPointState.Shown;
            this.SnapPointStates.TangentPoint = SnapPointState.Shown;
            this.SnapPointStates.TwoCurveIntersection = SnapPointState.Shown;
            this.SnapPointStates.UserDefined = SnapPointState.Shown;
        }

        public Vector AxisX
        {
            get
            {
                return (Vector) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Vector, "XAxis");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Vector, "XAxis", value);
            }
        }

        public Vector AxisY
        {
            get
            {
                return (Vector) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Vector, "YAxis");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Vector, "YAxis", value);
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

        public bool EnableFacetSelection
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "EnableFacetSelection");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "EnableFacetSelection", value);
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

        public bool HasOriginGwif
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "HasOriginGwif");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "HasOriginGwif", value);
            }
        }

        public bool IsOriginSpecified
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "IsOriginSpecified");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "IsOriginSpecified", value);
            }
        }

        public bool IsWCSCoordinates
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "IsWCSCoordinates");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "IsWCSCoordinates", value);
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

        public Snap.Orientation Orientation
        {
            get
            {
                return new Snap.Orientation(this.AxisX, this.AxisY);
            }
            set
            {
                this.AxisX = value.AxisX;
                this.AxisY = value.AxisY;
            }
        }

        public Position Origin
        {
            get
            {
                return (Position) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Point, "Origin");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Point, "Origin", value);
            }
        }

        public SnapPointsStateSet SnapPointStates { get; set; }

        internal int SnapPointTypesOnByDefault
        {
            get
            {
                return (int) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.BitArray, "SnapPointTypesOnByDefault");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.BitArray, "SnapPointTypesOnByDefault", value);
            }
        }
    }
}

