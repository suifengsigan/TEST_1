namespace Snap.UI.Block
{
    using NXOpen.BlockStyler;
    using Snap;
    using Snap.NX;
    using System;
    using System.Runtime.CompilerServices;

    public class SpecifyPoint : General
    {
        public SpecifyPoint()
        {
            this.Initialize();
        }

        internal SpecifyPoint(UIBlock uiBlock)
        {
            base.NXOpenBlock = uiBlock;
        }

        public static Snap.UI.Block.SpecifyPoint GetBlock(BlockDialog dialog, string name)
        {
            return new Snap.UI.Block.SpecifyPoint(dialog.TopBlock.FindBlock(name));
        }

        private void Initialize()
        {
            base.Type = "Specify Point";
            base.Show = true;
            this.AutomaticProgression = true;
            this.BalloonTooltipImage = "";
            this.BalloonTooltipLayout = Snap.UI.Block.BalloonTooltipLayout.Horizontal;
            this.BalloonTooltipText = "";
            this.BeginGroup = false;
            this.Enabled = true;
            this.EnableFacetSelection = false;
            this.Expanded = true;
            this.InterpartSelection = InterPartSelectionCopy.None;
            this.Label = "Specify Point";
            this.LabelString = "Select Point";
            this.Position = Snap.Position.Origin;
            this.PrivateSelectedObjects = new NXObject[0];
            this.StepStatus = Snap.UI.Block.StepStatus.Required;
            SnapPointsStateSet set = new SnapPointsStateSet(this) {
                ArcCenter = SnapPointState.Selected,
                BoundedGridPoint = SnapPointState.Shown,
                ControlPoint = SnapPointState.Shown,
                EndPoint = SnapPointState.Selected,
                ExistingPoint = SnapPointState.Selected,
                Intersection = SnapPointState.Shown,
                MidPoint = SnapPointState.Selected,
                PointConstructor = SnapPointState.Shown,
                PointOnCurve = SnapPointState.Shown,
                PointOnSurface = SnapPointState.Shown,
                QuadrantPoint = SnapPointState.Shown,
                ScreenPosition = SnapPointState.Shown
            };
            this.SnapPointStates = set;
            this.SnapPointTypesEnabled = set.TypesEnabled;
            this.SnapPointTypesOnByDefault = set.TypesOnByDefault;
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

        public Snap.Position Position
        {
            get
            {
                return (Snap.Position) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Point, "Point");
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

        [Obsolete("Deprecated in NX9. Please use the SpecifiedPoint property, instead.")]
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

        public int SnapPointTypesEnabled
        {
            get
            {
                return this.SnapPointStates.TypesEnabled;
            }
            set
            {
                this.SnapPointStates.TypesEnabled = value;
            }
        }

        public int SnapPointTypesOnByDefault
        {
            get
            {
                return this.SnapPointStates.TypesOnByDefault;
            }
            set
            {
                this.SnapPointStates.TypesOnByDefault = value;
            }
        }

        public Point SpecifiedPoint
        {
            get
            {
                return (Point) this.PrivateSelectedObjects[0];
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

