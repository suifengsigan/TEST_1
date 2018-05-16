namespace NXOpen
{
    using NXOpen.Utilities;
    using System;

    public class SourceBehavior : RuntimeObject
    {
        private static Factory m_factory = new Factory();
        private const int SOURCE_BEHAVIOR_ACTIVE = 0;

        protected SourceBehavior(IntPtr pItem) : base(pItem)
        {
        }

        internal static void Init()
        {
            RuntimeObject.s_arrClassGen[0x11558] = m_factory;
        }

        public override bool Active
        {
            get
            {
                return (PropertyFunc.ExGetBool(base.m_pItem, 0) != 0);
            }
            set
            {
                if (PropertyFunc.ExSetBool(base.m_pItem, 0, value ? -1 : 0) != 0)
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, 0);
                }
            }
        }

        internal class Factory : IItemFactory
        {
            public RuntimeObject Create(IntPtr pItem)
            {
                return new SourceBehavior(pItem);
            }
        }
    }
}

