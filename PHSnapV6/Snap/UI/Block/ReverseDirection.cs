namespace Snap.UI.Block
{
    using NXOpen.BlockStyler;
    using Snap;
    using System;

    public class ReverseDirection : General
    {
        public ReverseDirection()
        {
            this.Initialize();
        }

        internal ReverseDirection(UIBlock uiBlock)
        {
            base.NXOpenBlock = uiBlock;
        }

        public static Snap.UI.Block.ReverseDirection GetBlock(BlockDialog dialog, string name)
        {
            return new Snap.UI.Block.ReverseDirection(dialog.TopBlock.FindBlock(name));
        }

        private void Initialize()
        {
            base.Type = "Reverse Direction";
            base.Show = true;
            this.BeginGroup = false;
            this.Direction = Vector.AxisZ;
            this.Enabled = true;
            this.Expanded = true;
            this.Flip = false;
            this.Label = "Reverse Direction";
            this.Origin = Position.Origin;
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
                return (Vector) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Vector, "Direction");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Vector, "Direction", value);
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

        public bool Flip
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "Flip");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "Flip", value);
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
    }
}

