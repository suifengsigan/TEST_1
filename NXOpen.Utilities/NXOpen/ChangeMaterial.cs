namespace NXOpen
{
    using NXOpen.Utilities;
    using System;

    public class ChangeMaterial : RuntimeObject
    {
        private const int CHANGE_MATERIAL_ACTIVE = 3;
        private const int CHANGE_MATERIAL_BODY_1 = 0;
        private const int CHANGE_MATERIAL_BODY_2 = 1;
        private const int CHANGE_MATERIAL_MATERIAL = 2;
        private static Factory m_factory = new Factory();

        protected ChangeMaterial(IntPtr pItem) : base(pItem)
        {
        }

        internal static void Init()
        {
            RuntimeObject.s_arrClassGen[0x88b8] = m_factory;
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

        public CollisionMaterial Material
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
                return new ChangeMaterial(pItem);
            }
        }
    }
}

