namespace Snap.UI.Block
{
    using NXOpen.BlockStyler;
    using Snap;
    using Snap.NX;
    using System;

    public class SpecifyCsys : General
    {
        public SpecifyCsys()
        {
            this.Initialize();
        }

        internal SpecifyCsys(UIBlock uiBlock)
        {
            base.NXOpenBlock = uiBlock;
        }

        public static SpecifyCsys GetBlock(BlockDialog dialog, string name)
        {
            return new SpecifyCsys(dialog.TopBlock.FindBlock(name));
        }

        private void Initialize()
        {
            base.Type = "Specify Csys";
            base.Show = true;
            this.BalloonTooltipImage = "";
            this.BalloonTooltipLayout = Snap.UI.Block.BalloonTooltipLayout.Horizontal;
            this.BalloonTooltipText = "";
            this.BeginGroup = false;
            this.CreateInterpartLink = false;
            this.Enabled = true;
            this.Expanded = true;
            this.InterpartSelection = InterPartSelectionCopy.None;
            this.Label = "Specify CSYS";
            this.LabelString = "Specify CSYS";
            this.PrivateSelectedObjects = new NXObject[0];
            this.StepStatus = Snap.UI.Block.StepStatus.Required;
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

        public Snap.Orientation Orientation
        {
            get
            {
                return this.SpecifiedCsys.Orientation;
            }
        }

        public Position? Origin
        {
            get
            {
                return new Position?(this.SpecifiedCsys.Origin);
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

        [Obsolete("Deprecated in NX9. Please use the SpecifiedCsys property, instead.")]
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

        public CoordinateSystem SpecifiedCsys
        {
            get
            {
                return (CoordinateSystem) this.PrivateSelectedObjects[0];
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

