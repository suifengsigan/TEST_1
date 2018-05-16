namespace NXOpen
{
    using NXOpen.Utilities;
    using System;

    public class TransportSurface : RuntimeObject
    {
        private const int CONVEYOR_SURFACE_ACTIVE = 3;
        private const int CONVEYOR_SURFACE_MATERIAL = 2;
        private const int CONVEYOR_SURFACE_VELOCITY_X = 0;
        private const int CONVEYOR_SURFACE_VELOCITY_Y = 1;
        private static Factory m_factory = new Factory();

        protected TransportSurface(IntPtr pItem) : base(pItem)
        {
        }

        internal static void Init()
        {
            RuntimeObject.s_arrClassGen[0xc350] = m_factory;
        }

        public override bool Active
        {
            get
            {
                return (PropertyFunc.ExGetBool(base.m_pItem, 3) != 0);
            }
            set
            {
                if (PropertyFunc.ExSetBool(base.m_pItem, 3, value ? -1 : 0) != 0)
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, 3);
                }
            }
        }

        public double ParallelSpeed
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

        public double PerpendicularSpeed
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

        public CollisionMaterial Surface
        {
            get
            {
                return (RuntimeObject.FromPtr(PropertyFunc.ExGetObject(base.m_pItem, 2)) as CollisionMaterial);
            }
            set
            {
                if (PropertyFunc.ExSetObject(base.m_pItem, 2, value.GetPtr()) != 0)
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, 2);
                }
            }
        }

        internal class Factory : IItemFactory
        {
            public RuntimeObject Create(IntPtr pItem)
            {
                return new TransportSurface(pItem);
            }
        }
    }
}

