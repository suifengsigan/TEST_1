namespace Snap.UI.Block
{
    using NXOpen.BlockStyler;
    using Snap;
    using Snap.NX;
    using System;
    using System.Runtime.CompilerServices;

    public class SpecifyVector : General
    {
        public SpecifyVector()
        {
            this.Initialize();
        }

        internal SpecifyVector(UIBlock uiBlock)
        {
            base.NXOpenBlock = uiBlock;
        }

        public SpecifyVector(Position origin, Vector direction)
        {
            this.Initialize();
            this.Origin = origin;
            this.Direction = direction;
        }

        public static Snap.UI.Block.SpecifyVector GetBlock(BlockDialog dialog, string name)
        {
            return new Snap.UI.Block.SpecifyVector(dialog.TopBlock.FindBlock(name));
        }

        private void Initialize()
        {
            base.Type = "Specify Vector";
            base.Show = true;
            this.AutomaticProgression = true;
            this.BalloonTooltipImage = "";
            this.BalloonTooltipLayout = Snap.UI.Block.BalloonTooltipLayout.Horizontal;
            this.BalloonTooltipText = "";
            this.BeginGroup = false;
            this.DoubleSide = false;
            this.Enabled = true;
            this.EnableFacetSelection = false;
            this.Expanded = true;
            this.InterpartSelection = InterPartSelectionCopy.None;
            this.Is2DMode = false;
            this.Label = "Specify Vector";
            this.LabelString = "Select Vector";
            this.Origin = Position.Origin;
            this.PrivateSelectedObjects = new NXObject[0];
            this.StepStatus = Snap.UI.Block.StepStatus.Required;
            this.Direction = Vector.AxisZ;
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

        public bool AutomaticProgression
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "AutomaticProgression");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "AutomaticProgression", value);
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

        public Vector Direction
        {
            get
            {
                return (Vector) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Vector, "Vector");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Vector, "Vector", value);
            }
        }

        public bool DoubleSide
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "DoubleSide");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "DoubleSide", value);
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

        public InterPartSelectionCopy InterpartSelection
        {
            get
            {
                return (InterPartSelectionCopy) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Enum, "InterpartSelection");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Enum, "InterpartSelection", value);
            }
        }

        public bool Is2DMode
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "Is2DMode");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "Is2DMode", value);
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

        public string LabelString
        {
            get
            {
                return (string) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.String, "LabelString");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.String, "LabelString", value);
            }
        }

        public Position Origin
        {
            get
            {
                return (Position) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Point, "Point");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Point, "Point", value);
            }
        }

        internal NXObject[] PrivateSelectedObjects
        {
            get
            {
                return base.GetSelectedObjects();
            }
            set
            {
                base.SetSelectedObjects(value);
            }
        }

        [Obsolete("Deprecated in NX9. Please use the SpecifiedVector property, instead.")]
        public NXObject[] SelectedObjects
        {
            get
            {
                return this.PrivateSelectedObjects;
            }
            set
            {
                this.PrivateSelectedObjects = value;
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

        public NXObject SpecifiedVector
        {
            get
            {
                return this.PrivateSelectedObjects[0];
            }
            set
            {
                this.PrivateSelectedObjects = new NXObject[] { value };
            }
        }

        public Snap.UI.Block.StepStatus StepStatus
        {
            get
            {
                return (Snap.UI.Block.StepStatus) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Enum, "StepStatus");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Enum, "StepStatus", value);
            }
        }
    }
}

