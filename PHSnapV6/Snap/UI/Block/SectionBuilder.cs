namespace Snap.UI.Block
{
    using NXOpen.BlockStyler;
    using Snap.NX;
    using System;
    using System.Runtime.CompilerServices;

    public class SectionBuilder : General
    {
        public SectionBuilder()
        {
            this.Initialize();
        }

        internal SectionBuilder(UIBlock uiBlock)
        {
            base.NXOpenBlock = uiBlock;
        }

        public static Snap.UI.Block.SectionBuilder GetBlock(BlockDialog dialog, string name)
        {
            return new Snap.UI.Block.SectionBuilder(dialog.TopBlock.FindBlock(name));
        }

        private void Initialize()
        {
            base.Type = "Section Builder";
            base.Show = true;
            this.AllowStopAtIntersectionFollowFillet = true;
            this.AngularTolerance = 0.5;
            this.AutomaticProgression = false;
            this.BalloonTooltipImage = "";
            this.BalloonTooltipLayout = Snap.UI.Block.BalloonTooltipLayout.Horizontal;
            this.BalloonTooltipText = "";
            this.BeginGroup = false;
            this.Bitmap = "sectionbuilder";
            this.ChainWithinFeature = false;
            this.CreateInterpartLink = false;
            this.Cue = "Select Curve";
            this.CurveRules = 0x384f;
            this.DefaultCurveRules = 1;
            this.EntityType = 4;
            this.Enabled = true;
            this.Expanded = true;
            this.FollowFillet = false;
            this.InterpartSelection = InterPartSelectionCopy.None;
            this.LabelString = "Select Curve";
            this.PointOverlay = false;
            this.SelectedObjects = new NXObject[0];
            this.ShowFlowDirectionAndOriginCurve = false;
            this.StepStatus = Snap.UI.Block.StepStatus.Required;
            this.StopAtIntersection = false;
            this.ToolTip = "Curve";
            this.SnapPointStates = new SnapPointsStateSet(this);
            this.SnapPointStates.ArcCenter = SnapPointState.Shown;
            this.SnapPointStates.BoundedGridPoint = SnapPointState.Shown;
            this.SnapPointStates.ControlPoint = SnapPointState.Shown;
            this.SnapPointStates.EndPoint = SnapPointState.Shown;
            this.SnapPointStates.ExistingPoint = SnapPointState.Selected;
            this.SnapPointStates.Intersection = SnapPointState.Shown;
            this.SnapPointStates.MidPoint = SnapPointState.Shown;
            this.SnapPointStates.PointConstructor = SnapPointState.Shown;
            this.SnapPointStates.PointOnCurve = SnapPointState.Shown;
            this.SnapPointStates.PointOnSurface = SnapPointState.Shown;
            this.SnapPointStates.QuadrantPoint = SnapPointState.Shown;
        }

        public bool AllowStopAtIntersectionFollowFillet
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "AllowStopAtIntersectionFollowFillet");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "AllowStopAtIntersectionFollowFillet", value);
            }
        }

        public double AngularTolerance
        {
            get
            {
                return (double) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Double, "AngularTolerance");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Double, "AngularTolerance", value);
            }
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

        public bool ChainWithinFeature
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "ChainWithinFeature");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "ChainWithinFeature", value);
            }
        }

        public bool CreateInterpartLink
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "CreateInterpartLink");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "CreateInterpartLink", value);
            }
        }

        public string Cue
        {
            get
            {
                return (string) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.String, "Cue");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.String, "Cue", value);
            }
        }

        public int CurveRules
        {
            get
            {
                return (int) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.BitArray, "CurveRules");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.BitArray, "CurveRules", value);
            }
        }

        public int DefaultCurveRules
        {
            get
            {
                return (int) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Enum, "DefaultCurveRules");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Enum, "DefaultCurveRules", value);
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

        public int EntityType
        {
            get
            {
                return (int) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.BitArray, "EntityType");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.BitArray, "EntityType", value);
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

        public bool FollowFillet
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "FollowFillet");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "FollowFillet", value);
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

        public bool PointOverlay
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "PointOverlay");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "PointOverlay", value);
            }
        }

        public NXObject[] SelectedObjects
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

        public bool ShowFlowDirectionAndOriginCurve
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "ShowFlowDirectionAndOriginCurve");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "ShowFlowDirectionAndOriginCurve", value);
            }
        }

        public SnapPointsStateSet SnapPointStates { get; set; }

        internal int SnapPointTypesEnabled
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

        internal int SnapPointTypesOnByDefault
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

        public bool StopAtIntersection
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "StopAtIntersection");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "StopAtIntersection", value);
            }
        }

        public string ToolTip
        {
            get
            {
                return (string) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.String, "ToolTip");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.String, "ToolTip", value);
            }
        }
    }
}

