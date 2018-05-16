namespace NXOpen
{
    using NXOpen.Utilities;
    using System;

    public class Signal : RuntimeObject
    {
        private static Factory m_factory = new Factory();
        private const int SIGNAL_ADAPTER_GET_ADAPTER = 0;
        private const int SIGNAL_ADAPTER_GET_NUM_SIGNALS = 1;
        private const int SIGNAL_ADAPTER_GET_SIGNAL = 2;

        protected Signal(IntPtr pItem) : base(pItem)
        {
        }

        internal static void Init()
        {
            RuntimeObject.s_arrClassGen[0x18a88] = m_factory;
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

        public SignalAdapter Adapter
        {
            get
            {
                IntPtr pFunc = PropertyFunc.ExGetFunc(base.m_pItem, 0);
                IntPtr pItem = PropertyFunc.ExFuncInvokeObject(pFunc);
                PropertyFunc.ExFuncRelease(pFunc);
                return (RuntimeObject.FromPtr(pItem) as SignalAdapter);
            }
        }

        public bool BoolValue
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

        public double FloatValue
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

        public int IntValue
        {
            get
            {
                return PropertyFunc.ExGetInt(base.m_pItem, 0);
            }
            set
            {
                if (PropertyFunc.ExSetInt(base.m_pItem, 0, value) != 0)
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, 0);
                }
            }
        }

        public int Type
        {
            get
            {
                return PropertyFunc.ExGetPropType(base.m_pItem, 0);
            }
        }

        internal class Factory : IItemFactory
        {
            public RuntimeObject Create(IntPtr pItem)
            {
                return new Signal(pItem);
            }
        }
    }
}

