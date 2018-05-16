namespace Snap.UI.Block
{
    using NXOpen;
    using NXOpen.BlockStyler;
    using Snap;
    using Snap.NX;
    using Snap.UI;
    using System;
    using System.Runtime.CompilerServices;

    public class SelectObject : General
    {
        public SelectObject()
        {
            this.Initialize();
        }

        internal SelectObject(UIBlock uiBlock)
        {
            base.NXOpenBlock = uiBlock;
        }

        public SelectObject(string cue, string label)
        {
            this.Initialize();
            this.Cue = cue;
            this.LabelString = label;
        }

        public static Snap.UI.Block.SelectObject GetBlock(NXOpen.BlockStyler.BlockDialog dialog, string name)
        {
            return new Snap.UI.Block.SelectObject(dialog.TopBlock.FindBlock(name));
        }

        private void Initialize()
        {
            base.Type = "Selection";
            base.Show = true;
            this.AutomaticProgression = true;
            this.BalloonTooltipImage = "";
            this.BalloonTooltipLayout = Snap.UI.Block.BalloonTooltipLayout.Horizontal;
            this.BalloonTooltipText = "";
            this.BeginGroup = false;
            this.Bitmap = "selection_cursor";
            this.CreateInterpartLink = false;
            this.Cue = "Select Object";
            this.Enabled = true;
            this.Expanded = true;
            this.InterpartSelection = InterPartSelectionCopy.None;
            this.LabelString = "Select Object";
            this.MaskTriples = null;
            this.MaximumScope = Snap.UI.Block.SelectionScope.UseDefault;
            this.PointOverlay = false;
            this.SelectedObjects = new Snap.NX.NXObject[0];
            this.AllowMultiple = false;
            this.StepStatus = Snap.UI.Block.StepStatus.Required;
            this.ToolTip = "";
            this.SnapPointStates = new SnapPointsStateSet(this);
            this.SnapPointStates.ArcCenter = SnapPointState.Selected;
            this.SnapPointStates.BoundedGridPoint = SnapPointState.Shown;
            this.SnapPointStates.ControlPoint = SnapPointState.Shown;
            this.SnapPointStates.EndPoint = SnapPointState.Selected;
            this.SnapPointStates.ExistingPoint = SnapPointState.Selected;
            this.SnapPointStates.Intersection = SnapPointState.Shown;
            this.SnapPointStates.MidPoint = SnapPointState.Selected;
            this.SnapPointStates.PointConstructor = SnapPointState.Shown;
            this.SnapPointStates.PointOnCurve = SnapPointState.Shown;
            this.SnapPointStates.PointOnSurface = SnapPointState.Shown;
            this.SnapPointStates.QuadrantPoint = SnapPointState.Shown;
        }

        public void SetCurveFilter()
        {
            this.MaskTriples = Snap.UI.MaskTriple.BuildFromICurveTypes(ObjectTypes.AllCurvesPrivate);
        }

        public void SetCurveFilter(params ObjectTypes.Type[] types)
        {
            this.MaskTriples = Snap.UI.MaskTriple.BuildFromICurveTypes(types);
        }

        public void SetFaceFilter()
        {
            NXOpen.Selection.MaskTriple[] tripleArray = new NXOpen.Selection.MaskTriple[] { Snap.UI.MaskTriple.BuildFromType(ObjectTypes.Type.Face) };
            this.MaskTriples = tripleArray;
        }

        public void SetFaceFilter(params ObjectTypes.SubType[] faceTypes)
        {
            int length = faceTypes.Length;
            ObjectTypes.TypeCombo[] combos = new ObjectTypes.TypeCombo[length];
            for (int i = 0; i < length; i++)
            {
                combos[i].Type = ObjectTypes.Type.Face;
                combos[i].SubType = faceTypes[i];
            }
            this.MaskTriples = Snap.UI.MaskTriple.BuildFromTypeCombos(combos);
        }

        public void SetFilter(params ObjectTypes.Type[] types)
        {
            int length = types.Length;
            NXOpen.Selection.MaskTriple[] tripleArray = new NXOpen.Selection.MaskTriple[length];
            for (int i = 0; i < length; i++)
            {
                tripleArray[i] = Snap.UI.MaskTriple.BuildFromType(types[i]);
            }
            this.MaskTriples = tripleArray;
        }

        public void SetFilter(params ObjectTypes.TypeCombo[] combos)
        {
            this.MaskTriples = Snap.UI.MaskTriple.BuildFromTypeCombos(combos);
        }

        public void SetFilter(ObjectTypes.Type type, ObjectTypes.SubType subtype)
        {
            ObjectTypes.TypeCombo combo = new ObjectTypes.TypeCombo(type, subtype);
            NXOpen.Selection.MaskTriple[] tripleArray = new NXOpen.Selection.MaskTriple[] { Snap.UI.MaskTriple.BuildFromTypeCombo(combo) };
            this.MaskTriples = tripleArray;
        }

        public bool AllowMultiple
        {
            get
            {
                return (((int) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Enum, "SelectMode")) != 0);
            }
            set
            {
                int propValue = !value ? 0 : 1;
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Enum, "SelectMode", propValue);
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

        public NXOpen.Selection.MaskTriple[] MaskTriples
        {
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Filter, "SelectionFilter", value);
            }
        }

        public Snap.UI.Block.SelectionScope MaximumScope
        {
            get
            {
                return (Snap.UI.Block.SelectionScope) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Enum, "MaximumScope");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Enum, "MaximumScope", value);
            }
        }

        public Position PickPoint
        {
            get
            {
                return (Position) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Point, "PickPoint");
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

        public Snap.NX.NXObject[] SelectedObjects
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

