namespace NXOpen
{
    using NXOpen.Utilities;
    using System;

    public class PreventCollision : RuntimeObject
    {
        private static Factory m_factory = new Factory();
        private const int PREVENT_COLLISION_ACTIVE = 2;
        private const int PREVENT_COLLISION_BODY_1 = 0;
        private const int PREVENT_COLLISION_BODY_2 = 1;

        protected PreventCollision(IntPtr pItem) : base(pItem)
        {
        }

        internal static void Init()
        {
            RuntimeObject.s_arrClassGen[0x8ca0] = m_factory;
        }

        public override bool Active
        {
            get
            {
                return (PropertyFunc.ExGetBool(base.m_pItem, 2) != 0);
            }
            set
            {
                if (PropertyFunc.ExSetBool(base.m_pItem, 2, value ? -1 : 0) != 0)
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, 2);
                }
            }
        }

        public RuntimeObject Body1
        {
            get
            {
                return RuntimeObject.FromPtr(PropertyFunc.ExGetObject(base.m_pItem, 0));
            }
            set
            {
                if (PropertyFunc.ExSetObject(base.m_pItem, 0, value.GetPtr()) != 0)
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, 0);
                }
            }
        }

        public RuntimeObject Body2
        {
            get
            {
                return RuntimeObject.FromPtr(PropertyFunc.ExGetObject(base.m_pItem, 1));
            }
            set
            {
                if (PropertyFunc.ExSetObject(base.m_pItem, 1, value.GetPtr()) != 0)
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, 1);
                }
            }
        }

        internal class Factory : IItemFactory
        {
            public RuntimeObject Create(IntPtr pItem)
            {
                return new PreventCollision(pItem);
            }
        }
    }
}

