namespace Snap.UI.Block
{
    using NXOpen;
    using NXOpen.BlockStyler;
    using Snap;
    using Snap.NX;
    using System;

    public class SpecifyAxis : General
    {
        public SpecifyAxis()
        {
            this.Initialize();
        }

        internal SpecifyAxis(UIBlock uiBlock)
        {
            base.NXOpenBlock = uiBlock;
        }

        private SpecifyAxis(BlockDialog dialog, string blockID)
        {
            base.NXOpenBlock = dialog.TopBlock.FindBlock(blockID);
        }

        public static Snap.UI.Block.SpecifyAxis GetBlock(BlockDialog dialog, string name)
        {
            return new Snap.UI.Block.SpecifyAxis(dialog.TopBlock.FindBlock(name));
        }

        private void Initialize()
        {
            base.Type = "Specify Axis";
            base.Show = true;
            this.BeginGroup = false;
            this.Enabled = true;
            this.Expanded = true;
            this.Label = "Specify Axis";
            this.PrivateSelectedObjects = new Snap.NX.NXObject[0];
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

        public Vector? Direction
        {
            get
            {
                NXOpen.Axis specifiedAxis = (NXOpen.Axis) this.SpecifiedAxis;
                return new Vector(specifiedAxis.DirectionVector);
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

        public Position? Origin
        {
            get
            {
                NXOpen.Axis specifiedAxis = (NXOpen.Axis) this.SpecifiedAxis;
                return new Position(specifiedAxis.Origin);
            }
        }

        internal Snap.NX.NXObject[] PrivateSelectedObjects
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

        [Obsolete("Deprecated in NX9. Please use the SpecifiedAxis property, instead.")]
        public Snap.NX.NXObject[] SelectedObjects
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

        public Snap.NX.NXObject SpecifiedAxis
        {
            get
            {
                return this.PrivateSelectedObjects[0];
            }
            set
            {
                this.PrivateSelectedObjects = new Snap.NX.NXObject[] { value };
            }
        }
    }
}

