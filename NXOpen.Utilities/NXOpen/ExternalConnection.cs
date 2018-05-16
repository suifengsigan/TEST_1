namespace NXOpen
{
    using NXOpen.Utilities;
    using System;

    public class ExternalConnection : RuntimeObject
    {
        private const int EXTERNAL_CONNECTION_ACTIVE = 1;
        private const int EXTERNAL_CONNECTION_TARGET = 0;
        private static Factory m_factory = new Factory();

        protected ExternalConnection(IntPtr pItem) : base(pItem)
        {
        }

        internal static void Init()
        {
            RuntimeObject.s_arrClassGen[0x15f90] = m_factory;
        }

        public override bool Active
        {
            get
            {
                return (PropertyFunc.ExGetBool(base.m_pItem, 1) != 0);
            }
            set
            {
                if (PropertyFunc.ExSetBool(base.m_pItem, 1, value ? -1 : 0) != 0)
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, 1);
                }
            }
        }

        public RuntimeObject Target
        {
            get
            {
                return RuntimeObject.FromPtr(PropertyFunc.ExGetObject(base.m_pItem, 0));
            }
        }

        internal class Factory : IItemFactory
        {
            public RuntimeObject Create(IntPtr pItem)
            {
                return new ExternalConnection(pItem);
            }
        }
    }
}

