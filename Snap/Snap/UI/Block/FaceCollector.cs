namespace Snap.UI.Block
{
    using NXOpen.BlockStyler;
    using Snap.NX;
    using System;

    public class FaceCollector : General
    {
        public FaceCollector()
        {
            this.Initialize();
        }

        internal FaceCollector(UIBlock uiBlock)
        {
            base.NXOpenBlock = uiBlock;
        }

        public static Snap.UI.Block.FaceCollector GetBlock(BlockDialog dialog, string name)
        {
            return new Snap.UI.Block.FaceCollector(dialog.TopBlock.FindBlock(name));
        }

        private void Initialize()
        {
            base.Type = "Face Collector";
            base.Show = true;
            this.AutomaticProgression = false;
            this.BalloonTooltipImage = "";
            this.BalloonTooltipLayout = Snap.UI.Block.BalloonTooltipLayout.Horizontal;
            this.BalloonTooltipText = "";
            this.BeginGroup = false;
            this.Bitmap = "select_face";
            this.CreateInterpartLink = false;
            this.Cue = "Select Face";
            this.Enabled = true;
            this.EntityType = 0x10;
            this.FaceRules = 0xe7f;
            this.DefaultFaceRules = 4;
            this.Expanded = true;
            this.InterpartSelection = InterPartSelectionCopy.None;
            this.LabelString = "Select Face";
            this.SelectedObjects = new NXObject[0];
            this.AllowMultiple = true;
            this.StepStatus = Snap.UI.Block.StepStatus.Required;
            this.ToolTip = "Face";
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

        public int DefaultFaceRules
        {
            get
            {
                return (int) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Enum, "DefaultFaceRules");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Enum, "DefaultFaceRules", value);
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

        public int FaceRules
        {
            get
            {
                return (int) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.BitArray, "FaceRules");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.BitArray, "FaceRules", value);
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

