namespace Snap.UI.Block
{
    using NXOpen;
    using NXOpen.BlockStyler;
    using Snap.NX;
    using Snap.UI;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class General
    {
        private string blockType;
        private string privateName;
        internal Dictionary<PropertyKey, object> PropertyDictionary;

        internal General()
        {
            this.Initialize();
        }

        internal General(UIBlock uiBlock)
        {
            this.NXOpenBlock = uiBlock;
        }

        internal void BlockToDictionary(BlockForm form)
        {
            Dictionary<PropertyKey, object> dict = new Dictionary<PropertyKey, object>();
            foreach (PropertyKey key in this.PropertyDictionary.Keys)
            {
                string name = key.Name;
                Snap.UI.Block.PropertyType propType = key.Type;
                object propValue = PropertyAccess.GetNxopenBlockValue(this, propType, name);
                if (name != "SelectedObjects")
                {
                    PropertyAccess.SetDictionaryValue(this, propType, name, propValue, dict);
                }
            }
            this.PropertyDictionary = dict;
        }

        internal void CopyFromDictionary(BlockForm form)
        {
            string privateName = this.privateName;
            UIBlock block = form.SnapBlockDialog.TopBlock.FindBlock(privateName);
            this.NXOpenBlock = block;
            foreach (PropertyKey key in this.PropertyDictionary.Keys)
            {
                PropertyAccess.DictionaryToBlock(this, key);
            }
        }

        internal static General CreateUiBlock(UIBlock uiBlock)
        {
            General general = new General(uiBlock);
            General general2 = general;
            if (uiBlock.Type == "Label")
            {
                return new LabelBlock(general.NXOpenBlock);
            }
            if (uiBlock.Type == "Toggle")
            {
                return new Snap.UI.Block.Toggle(general.NXOpenBlock);
            }
            if (uiBlock.Type == "Separator")
            {
                return new Snap.UI.Block.Separator(general.NXOpenBlock);
            }
            if (uiBlock.Type == "Enumeration")
            {
                return new Snap.UI.Block.Enumeration(general.NXOpenBlock);
            }
            if (uiBlock.Type == "String")
            {
                return new Snap.UI.Block.String(general.NXOpenBlock);
            }
            if (uiBlock.Type == "Multiline String")
            {
                return new Snap.UI.Block.MultilineString(general.NXOpenBlock);
            }
            if (uiBlock.Type == "Button")
            {
                return new Snap.UI.Block.Button(general.NXOpenBlock);
            }
            if (uiBlock.Type == "List Box")
            {
                return new Snap.UI.Block.ListBox(general.NXOpenBlock);
            }
            if (uiBlock.Type == "Color Picker")
            {
                return new ColorPicker(general.NXOpenBlock);
            }
            if (uiBlock.Type == "RGB Color Picker")
            {
                return new Snap.UI.Block.RGBColorPicker(general.NXOpenBlock);
            }
            if (uiBlock.Type == "Drawing Area")
            {
                return new Snap.UI.Block.DrawingArea(general.NXOpenBlock);
            }
            if (uiBlock.Type == "Integer")
            {
                return new Integer(general.NXOpenBlock);
            }
            if (uiBlock.Type == "Double")
            {
                return new Snap.UI.Block.Double(general.NXOpenBlock);
            }
            if (uiBlock.Type == "Expression")
            {
                return new Snap.UI.Block.Expression(general.NXOpenBlock);
            }
            if (uiBlock.Type == "Linear Dim")
            {
                return new Snap.UI.Block.LinearDimension(general.NXOpenBlock);
            }
            if (uiBlock.Type == "Angular Dim")
            {
                return new Snap.UI.Block.AngularDimension(general.NXOpenBlock);
            }
            if (uiBlock.Type == "Radius Dim")
            {
                return new Snap.UI.Block.RadiusDimension(general.NXOpenBlock);
            }
            if (uiBlock.Type == "On Path Dim")
            {
                return new Snap.UI.Block.OnPathDimension(general.NXOpenBlock);
            }
            if (uiBlock.Type == "Integer Table")
            {
                return new Snap.UI.Block.IntegerTable(general.NXOpenBlock);
            }
            if (uiBlock.Type == "Double Table")
            {
                return new Snap.UI.Block.DoubleTable(general.NXOpenBlock);
            }
            if (uiBlock.Type == "Group")
            {
                return new Snap.UI.Block.Group(general.NXOpenBlock);
            }
            if (uiBlock.Type == "TableLayout")
            {
                return new TableLayout(general.NXOpenBlock);
            }
            if (uiBlock.Type == "Selection")
            {
                return new Snap.UI.Block.SelectObject(general.NXOpenBlock);
            }
            if (uiBlock.Type == "Section Builder")
            {
                return new Snap.UI.Block.SectionBuilder(general.NXOpenBlock);
            }
            if (uiBlock.Type == "Super Section")
            {
                return new Snap.UI.Block.SuperSection(general.NXOpenBlock);
            }
            if (uiBlock.Type == "Curve Collector")
            {
                return new Snap.UI.Block.CurveCollector(general.NXOpenBlock);
            }
            if (uiBlock.Type == "Face Collector")
            {
                return new Snap.UI.Block.FaceCollector(general.NXOpenBlock);
            }
            if (uiBlock.Type == "Select Feature")
            {
                return new Snap.UI.Block.SelectFeature(general.NXOpenBlock);
            }
            if (uiBlock.Type == "Specify Point")
            {
                return new Snap.UI.Block.SpecifyPoint(general.NXOpenBlock);
            }
            if (uiBlock.Type == "Specify Vector")
            {
                return new Snap.UI.Block.SpecifyVector(general.NXOpenBlock);
            }
            if (uiBlock.Type == "Specify Axis")
            {
                return new Snap.UI.Block.SpecifyAxis(general.NXOpenBlock);
            }
            if (uiBlock.Type == "Specify Plane")
            {
                return new Snap.UI.Block.SpecifyPlane(general.NXOpenBlock);
            }
            if (uiBlock.Type == "Specify Csys")
            {
                return new SpecifyCsys(general.NXOpenBlock);
            }
            if (uiBlock.Type == "Specify Cursor Location")
            {
                return new SpecifyCursorLocation(general.NXOpenBlock);
            }
            if (uiBlock.Type == "Specify Orientation")
            {
                return new Snap.UI.Block.SpecifyOrientation(general.NXOpenBlock);
            }
            if (uiBlock.Type == "Select Part")
            {
                return new Snap.UI.Block.SelectPart(general.NXOpenBlock);
            }
            if (uiBlock.Type == "Reverse Direction")
            {
                return new Snap.UI.Block.ReverseDirection(general.NXOpenBlock);
            }
            if (uiBlock.Type == "OrientXpress")
            {
                return new Snap.UI.Block.OrientXpress(general.NXOpenBlock);
            }
            if (uiBlock.Type == "Select Expression")
            {
                general2 = new SelectExpression(general.NXOpenBlock);
            }
            return general2;
        }

        public override bool Equals(object obj)
        {
            General general = (General) obj;
            return ((general.Name != null) && !(this.Name != general.Name));
        }

        public static General GetBlock(NXOpen.BlockStyler.BlockDialog dialog, string blockName)
        {
            return new General(dialog.TopBlock.FindBlock(blockName));
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        internal Snap.NX.NXObject[] GetSelectedObjects()
        {
            TaggedObject[] objArray = (TaggedObject[]) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.ObjectArray, "SelectedObjects");
            if (((objArray == null) || (objArray.Length == 0)) || ((objArray.Length == 1) && (objArray[0] == null)))
            {
                return new Snap.NX.NXObject[0];
            }
            Snap.NX.NXObject[] objArray2 = new Snap.NX.NXObject[objArray.Length];
            for (int i = 0; i < objArray2.Length; i++)
            {
                objArray2[i] = Snap.NX.NXObject.CreateNXObject(objArray[i]);
            }
            return objArray2;
        }

        private void Initialize()
        {
            this.Type = "General";
            this.PropertyDictionary = new Dictionary<PropertyKey, object>();
            this.PropertyDictionary.Add(new PropertyKey(Snap.UI.Block.PropertyType.String, "BlockID"), "UnknownName");
            this.PropertyDictionary.Add(new PropertyKey(Snap.UI.Block.PropertyType.Logical, "Show"), true);
        }

        public static bool operator ==(General block1, General block2)
        {
            return (block1.Name == block2.Name);
        }

        public static bool operator !=(General block1, General block2)
        {
            return (block1.Name != block2.Name);
        }

        internal void SetSelectedObjects(Snap.NX.NXObject[] objs)
        {
            TaggedObject[] propValue = new TaggedObject[objs.Length];
            for (int i = 0; i < objs.Length; i++)
            {
                propValue[i] = objs[i].NXOpenTaggedObject;
            }
            PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.ObjectArray, "SelectedObjects", propValue);
        }

        public string Name
        {
            get
            {
                UIBlock nXOpenBlock = this.NXOpenBlock;
                if (nXOpenBlock != null)
                {
                    return nXOpenBlock.Name;
                }
                if (this.PropertyDictionary != null)
                {
                    PropertyKey key = new PropertyKey(Snap.UI.Block.PropertyType.String, "BlockID");
                    return (string) this.PropertyDictionary[key];
                }
                return this.privateName;
            }
            internal set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.String, "BlockID", value);
                this.privateName = value;
            }
        }

        public UIBlock NXOpenBlock { get; set; }

        public bool Show
        {
            get
            {
                return (bool) PropertyAccess.GetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "Show");
            }
            set
            {
                PropertyAccess.SetPropertyValue(this, Snap.UI.Block.PropertyType.Logical, "Show", value);
            }
        }

        public string Type
        {
            get
            {
                return this.blockType;
            }
            internal set
            {
                this.blockType = value;
            }
        }
    }
}

