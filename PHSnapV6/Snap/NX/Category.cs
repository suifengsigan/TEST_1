namespace Snap.NX
{
    using NXOpen;
    using NXOpen.Layer;
    using Snap;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class Category
    {
        internal Category(NXOpen.Layer.Category category)
        {
            this.NXOpenCategory = category;
        }

        public void Add(params int[] layers)
        {
            List<int> list = this.Layers;
            for (int i = 0; i < layers.Length; i++)
            {
                if (list.IndexOf(layers[i]) == -1)
                {
                    list.Add(layers[i]);
                }
            }
            this.NXOpenCategory.SetMemberLayers(list.ToArray());
        }

        internal static Snap.NX.Category CreateCategory(string name, string description, params int[] layers)
        {
            return new Snap.NX.Category(Globals.NXOpenWorkPart.LayerCategories.CreateCategory(name.ToString(), description.ToString(), layers));
        }

        public void Delete()
        {
            Delete(new Snap.NX.Category[] { this });
        }

        public static void Delete(params Snap.NX.Category[] categories)
        {
            NXOpen.NXObject[] objects = new NXOpen.NXObject[categories.Length];
            for (int i = 0; i < categories.Length; i++)
            {
                objects[i] = categories[i].NXOpenCategory;
            }
            Globals.UndoMarkId markId = Globals.SetUndoMark(Globals.MarkVisibility.Visible, "Snap_DeleteNXObjects999");
            Globals.Session.UpdateManager.ClearErrorList();
            Globals.Session.UpdateManager.AddToDeleteList(objects);
            Globals.Session.UpdateManager.DoUpdate((Session.UndoMarkId) markId);
            Globals.DeleteUndoMark(markId, "Snap_DeleteNXObjects999");
        }

        public static Snap.NX.Category FindByName(string name)
        {
            Snap.NX.Category category = null;
            foreach (NXOpen.Layer.Category category2 in Globals.NXOpenWorkPart.LayerCategories.ToArray())
            {
                if (category2.Name == name)
                {
                    category = category2;
                }
            }
            return category;
        }

        public static implicit operator Snap.NX.Category(NXOpen.Layer.Category category)
        {
            return new Snap.NX.Category(category);
        }

        public static implicit operator NXOpen.Layer.Category(Snap.NX.Category category)
        {
            return category.NXOpenCategory;
        }

        public bool Remove(params int[] layers)
        {
            List<int> list = this.Layers;
            for (int i = 0; i < layers.Length; i++)
            {
                if (list.IndexOf(layers[i]) == -1)
                {
                    return false;
                }
                list.Remove(layers[i]);
            }
            this.NXOpenCategory.SetMemberLayers(list.ToArray());
            return true;
        }

        public static Snap.NX.Category Wrap(Tag nxopenCategoryTag)
        {
            if (nxopenCategoryTag == Tag.Null)
            {
                throw new ArgumentException("Input tag is NXOpen.Tag.Null");
            }
            NXOpen.Layer.Category objectFromTag = Snap.NX.NXObject.GetObjectFromTag(nxopenCategoryTag) as NXOpen.Layer.Category;
            if (objectFromTag == null)
            {
                throw new ArgumentException("Input tag doesn't belong to an NXOpen.Layer.Category object");
            }
            return objectFromTag;
        }

        public string Description
        {
            get
            {
                return this.NXOpenCategory.Description;
            }
            set
            {
                this.NXOpenCategory.Description = value;
            }
        }

        public bool[] LayerMask
        {
            get
            {
                bool[] flagArray = new bool[0x101];
                for (int i = 0; i < flagArray.Length; i++)
                {
                    flagArray[i] = false;
                }
                for (int j = 0; j < this.Layers.Count; j++)
                {
                    flagArray[this.Layers[j]] = true;
                }
                return flagArray;
            }
            set
            {
                List<int> list = new List<int>();
                for (int i = 1; i <= 0x100; i++)
                {
                    if (value[i])
                    {
                        list.Add(i + 1);
                    }
                }
                this.Layers = list;
            }
        }

        public List<int> Layers
        {
            get
            {
                return new List<int>(this.NXOpenCategory.GetMemberLayers());
            }
            set
            {
                this.NXOpenCategory.SetMemberLayers(value.ToArray());
            }
        }

        public string Name
        {
            get
            {
                return this.NXOpenCategory.Name;
            }
            set
            {
                this.NXOpenCategory.SetName(value);
            }
        }

        public NXOpen.Layer.Category NXOpenCategory { get; private set; }

        public Tag NXOpenTag
        {
            get
            {
                return this.NXOpenCategory.Tag;
            }
        }
    }
}

