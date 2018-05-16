namespace NXOpen
{
    using NXOpen.Utilities;
    using System;

    public class CollisionMaterial : RuntimeObject
    {
        private const int COLLIDE_MATERIAL_DYNAMIC_FRICTION = 1;
        private const int COLLIDE_MATERIAL_RESTITUTION = 2;
        private const int COLLIDE_MATERIAL_STATIC_FRICTION = 0;
        private static Factory m_factory = new Factory();

        protected CollisionMaterial(IntPtr pItem) : base(pItem)
        {
        }

        public CollisionMaterial Copy()
        {
            return (RuntimeObject.FromPtr(InstanceFunc.ExCopy(base.m_pItem)) as CollisionMaterial);
        }

        internal static void Init()
        {
            RuntimeObject.s_arrClassGen[0x9c40] = m_factory;
        }

        public double DynamicFriction
        {
            get
            {
                return PropertyFunc.ExGetFloat(base.m_pItem, 1);
            }
            set
            {
                if (PropertyFunc.ExSetFloat(base.m_pItem, 1, value) != 0)
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, 1);
                }
            }
        }

        public double Restitution
        {
            get
            {
                return PropertyFunc.ExGetFloat(base.m_pItem, 2);
            }
            set
            {
                if (PropertyFunc.ExSetFloat(base.m_pItem, 2, value) != 0)
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, 2);
                }
            }
        }

        public double StaticFriction
        {
            get
            {
                return PropertyFunc.ExGetFloat(base.m_pItem, 0);
            }
            set
            {
                if (PropertyFunc.ExSetFloat(base.m_pItem, 0, value) != 0)
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, 0);
                }
            }
        }

        internal class Factory : IItemFactory
        {
            public RuntimeObject Create(IntPtr pItem)
            {
                return new CollisionMaterial(pItem);
            }
        }
    }
}

