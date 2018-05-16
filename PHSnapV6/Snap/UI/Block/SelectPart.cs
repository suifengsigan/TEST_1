namespace Snap.UI.Block
{
    using NXOpen;
    using NXOpen.BlockStyler;
    using Snap.NX;
    using System;

    public class SelectPart : General
    {
        public SelectPart()
        {
            this.Initialize();
        }

        internal SelectPart(UIBlock uiBlock)
        {
            base.NXOpenBlock = uiBlock;
        }

        public static Snap.UI.Block.SelectPart GetBlock(BlockDialog dialog, string name)
        {
            return new Snap.UI.Block.SelectPart(dialog.TopBlock.FindBlock(name));
        }

        internal Snap.NX.Part[] GetSelectedParts()
        {
            TaggedObject[] selectedObjects = ((SelectPartFromList) base.NXOpenBlock).GetSelectedObjects();
            if (((selectedObjects == null) || (selectedObjects.Length == 0)) || ((selectedObjects.Length == 1) && (selectedObjects[0] == null)))
            {
                return new Snap.NX.Part[0];
            }
            Snap.NX.Part[] partArray = new Snap.NX.Part[selectedObjects.Length];
            for (int i = 0; i < selectedObjects.Length; i++)
            {
                partArray[i] = Snap.NX.Part.Wrap(selectedObjects[i].Tag);
            }
            return partArray;
        }

        private void Initialize()
        {
            base.Type = "Select Part";
            base.Show = true;
            this.BeginGroup = false;
            this.Enabled = true;
            this.Expanded = true;
            this.Label = "Select Part";
            this.SelectedParts = new Snap.NX.Part[0];
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

        [Obsolete("Deprecated in NX9, please use the SelectedParts property, instead.")]
        public Snap.NX.NXObject[] SelectedObjects
        {
            get
            {
                TaggedObject[] objArray = (TaggedObject[]) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.ObjectArray, "SelectedObjects");
                Snap.NX.NXObject[] objArray2 = new Snap.NX.NXObject[objArray.Length];
                for (int i = 0; i < objArray.Length; i++)
                {
                    objArray2[i] = new Snap.NX.NXObject(objArray[i].Tag);
                }
                return objArray2;
            }
            set
            {
                TaggedObject[] propValue = new TaggedObject[value.Length];
                for (int i = 0; i < value.Length; i++)
                {
                    propValue[i] = value[i].NXOpenTaggedObject;
                }
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.ObjectArray, "SelectedObjects", propValue);
            }
        }

        public Snap.NX.Part[] SelectedParts
        {
            get
            {
                TaggedObject[] objArray = (TaggedObject[]) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.ObjectArray, "SelectedObjects");
                Snap.NX.Part[] partArray = new Snap.NX.Part[objArray.Length];
                for (int i = 0; i < objArray.Length; i++)
                {
                    partArray[i] = new Snap.NX.Part((NXOpen.Part) objArray[i]);
                }
                return partArray;
            }
            set
            {
                TaggedObject[] propValue = new TaggedObject[value.Length];
                for (int i = 0; i < value.Length; i++)
                {
                    propValue[i] = value[i].NXOpenPart;
                }
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.ObjectArray, "SelectedObjects", propValue);
            }
        }
    }
}

