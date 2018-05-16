namespace Snap.NX
{
    using NXOpen;
    using NXOpen.Assemblies;
    using Snap;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class Component : Snap.NX.NXObject
    {
        internal Component(NXOpen.Assemblies.Component component) : base(component)
        {
            this.NXOpenComponent = component;
        }

        private static int GetDepth(Snap.NX.Component comp)
        {
            Snap.NX.Component parent = comp;
            int num = 0;
            while (parent.Parent != null)
            {
                parent = parent.Parent;
                num++;
            }
            return num;
        }

        private static void GetDepthBadly(Snap.NX.Component comp, ref int depth)
        {
            Snap.NX.Component parent = comp.Parent;
            if (parent != null)
            {
                depth++;
                GetDepthBadly(parent, ref depth);
            }
        }

        private IEnumerable<Snap.NX.Component> GetDescendants(Snap.NX.Component comp)
        {
            Stack<Snap.NX.Component> iteratorVariable0 = new Stack<Snap.NX.Component>();
            iteratorVariable0.Push(comp);
            while (true)
            {
                if (iteratorVariable0.Count <= 0)
                {
                    yield break;
                }
                Snap.NX.Component iteratorVariable1 = iteratorVariable0.Pop();
                yield return iteratorVariable1;
                foreach (Snap.NX.Component component in iteratorVariable1.Children)
                {
                    iteratorVariable0.Push(component);
                }
            }
        }

        private IEnumerable<Snap.NX.Component> GetDescendantsBadly(Snap.NX.Component comp)
        {
            yield return comp;
            foreach (Snap.NX.Component iteratorVariable0 in comp.Children)
            {
                foreach (Snap.NX.Component iteratorVariable1 in this.GetDescendantsBadly(iteratorVariable0))
                {
                    yield return iteratorVariable1;
                }
            }
        }

        private static void GetDescendents(Snap.NX.Component comp, ref List<Snap.NX.Component> compList)
        {
            compList.AddRange(comp.Children);
            foreach (Snap.NX.Component component in comp.Children)
            {
                GetDescendents(component, ref compList);
            }
        }

        private IEnumerable<Snap.NX.Component> GetLeaves(Snap.NX.Component comp)
        {
            Stack<Snap.NX.Component> iteratorVariable0 = new Stack<Snap.NX.Component>();
            iteratorVariable0.Push(comp);
            while (iteratorVariable0.Count > 0)
            {
                Snap.NX.Component iteratorVariable1 = iteratorVariable0.Pop();
                if (iteratorVariable1.IsLeaf)
                {
                    yield return iteratorVariable1;
                }
                Snap.NX.Component[] children = iteratorVariable1.Children;
                for (int j = 0; j < children.Length; j++)
                {
                    Snap.NX.Component item = children[j];
                    iteratorVariable0.Push(item);
                }
            }
        }

        public static implicit operator Snap.NX.Component(NXOpen.Assemblies.Component component)
        {
            return new Snap.NX.Component(component);
        }

        public static implicit operator NXOpen.Assemblies.Component(Snap.NX.Component component)
        {
            return component.NXOpenComponent;
        }

        public static Snap.NX.Component Wrap(Tag nxopenComponentTag)
        {
            if (nxopenComponentTag == Tag.Null)
            {
                throw new ArgumentException("Input tag is NXOpen.Tag.Null");
            }
            NXOpen.Assemblies.Component objectFromTag = Snap.NX.NXObject.GetObjectFromTag(nxopenComponentTag) as NXOpen.Assemblies.Component;
            if (objectFromTag == null)
            {
                throw new ArgumentException("Input tag doesn't belong to an NXOpen.Assemblies.Component object");
            }
            return objectFromTag;
        }

        public Snap.NX.Component[] Children
        {
            get
            {
                NXOpen.Assemblies.Component[] children = this.NXOpenComponent.GetChildren();
                Snap.NX.Component[] componentArray2 = new Snap.NX.Component[children.Length];
                for (int i = 0; i < children.Length; i++)
                {
                    componentArray2[i] = new Snap.NX.Component(children[i]);
                }
                return componentArray2;
            }
        }

        public int Depth
        {
            get
            {
                return GetDepth(this);
            }
        }

        public IEnumerable<Snap.NX.Component> Descendants
        {
            get
            {
                return this.GetDescendants(this);
            }
        }

        public bool IsLeaf
        {
            get
            {
                return (this.Children.Length == 0);
            }
        }

        public bool IsRoot
        {
            get
            {
                return (this.Parent == null);
            }
        }

        public IEnumerable<Snap.NX.Component> Leaves
        {
            get
            {
                return this.GetLeaves(this);
            }
        }

        public NXOpen.Assemblies.Component NXOpenComponent
        {
            get
            {
                return (NXOpen.Assemblies.Component) base.NXOpenTaggedObject;
            }
            private set
            {
                base.NXOpenTaggedObject = value;
            }
        }

        public Snap.Orientation Orientation
        {
            get
            {
                Point3d pointd;
                Matrix3x3 matrixx;
                this.NXOpenComponent.GetPosition(out pointd, out matrixx);
                return new Snap.Orientation(matrixx);
            }
        }

        public Snap.NX.Component Parent
        {
            get
            {
                Snap.NX.Component component = null;
                NXOpen.Assemblies.Component parent = this.NXOpenComponent.Parent;
                if (parent != null)
                {
                    component = new Snap.NX.Component(parent);
                }
                return component;
            }
        }

        public Snap.Position Position
        {
            get
            {
                Point3d pointd;
                Matrix3x3 matrixx;
                this.NXOpenComponent.GetPosition(out pointd, out matrixx);
                return new Snap.Position(pointd);
            }
        }

        public Snap.NX.Part Prototype
        {
            get
            {
                return Snap.NX.Part.Wrap(base.GetProtoTagFromOccTag(base.NXOpenTag));
            }
        }



    }
}

