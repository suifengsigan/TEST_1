namespace NXOpen
{
    using NXOpen.Utilities;
    using System;

    public class SinkBehavior : RuntimeObject
    {
        private static Factory m_factory = new Factory();
        private const int SINK_BEHAVIOR_ACTIVE = 0;

        protected SinkBehavior(IntPtr pItem) : base(pItem)
        {
        }

        internal static void Init()
        {
            RuntimeObject.s_arrClassGen[0x11940] = m_factory;
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
                return new SinkBehavior(pItem);
            }
        }
    }
}

