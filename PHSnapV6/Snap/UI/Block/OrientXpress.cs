namespace Snap.UI.Block
{
    using NXOpen.BlockStyler;
    using NXOpen.GeometricUtilities;
    using System;

    public class OrientXpress : General
    {
        public OrientXpress()
        {
            this.Initialize();
        }

        internal OrientXpress(UIBlock uiBlock)
        {
            base.NXOpenBlock = uiBlock;
        }

        public static Snap.UI.Block.OrientXpress GetBlock(BlockDialog dialog, string name)
        {
            return new Snap.UI.Block.OrientXpress(dialog.TopBlock.FindBlock(name));
        }

        private void Initialize()
        {
            base.Type = "OrientXpress";
            base.Show = true;
            this.ActiveAxis = OrientXpressBuilder.Axis.X;
            this.ActivePlane = OrientXpressBuilder.Plane.Yz;
            this.BeginGroup = false;
            this.Enabled = true;
            this.Expanded = true;
            this.Label = "OrientXpress";
            this.Reference = NXOpen.GeometricUtilities.OrientXpressBuilder.Reference.AcsWorkPart;
            this.ShowAxisSubBlock = true;
            this.ShowAxisX = true;
            this.ShowAxisY = true;
            this.ShowAxisZ = true;
            this.ShowPlaneSubBlock = true;
            this.ShowPlaneXY = true;
            this.ShowPlaneXZ = true;
            this.ShowPlaneYZ = true;
            this.ShowReferenceSubBlock = true;
            this.ShowSceneControl = true;
        }

        public OrientXpressBuilder.Axis ActiveAxis
        {
            get
            {
                return (OrientXpressBuilder.Axis) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Enum, "Direction");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Enum, "Direction", value);
            }
        }

        public OrientXpressBuilder.Plane ActivePlane
        {
            get
            {
                return (OrientXpressBuilder.Plane) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Enum, "Plane");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Enum, "Plane", value);
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

        public NXOpen.GeometricUtilities.OrientXpressBuilder.Reference Reference
        {
            get
            {
                return (NXOpen.GeometricUtilities.OrientXpressBuilder.Reference) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Enum, "Reference");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Enum, "Reference", value);
            }
        }

        public bool ShowAxisSubBlock
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "ShowAxisSubBlock");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "ShowAxisSubBlock", value);
            }
        }

        public bool ShowAxisX
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "ShowXAxis");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "ShowXAxis", value);
            }
        }

        public bool ShowAxisY
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "ShowYAxis");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "ShowYAxis", value);
            }
        }

        public bool ShowAxisZ
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "ShowZAxis");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "ShowZAxis", value);
            }
        }

        public bool ShowPlaneSubBlock
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "ShowPlaneSubBlock");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "ShowPlaneSubBlock", value);
            }
        }

        public bool ShowPlaneXY
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "ShowXYPlane");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "ShowXYPlane", value);
            }
        }

        public bool ShowPlaneXZ
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "ShowXZPlane");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "ShowXZPlane", value);
            }
        }

        public bool ShowPlaneYZ
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "ShowYZPlane");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "ShowYZPlane", value);
            }
        }

        public bool ShowReferenceSubBlock
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "ShowReferenceSubBlock");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "ShowReferenceSubBlock", value);
            }
        }

        public bool ShowSceneControl
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "ShowSceneControl");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "ShowSceneControl", value);
            }
        }
    }
}

