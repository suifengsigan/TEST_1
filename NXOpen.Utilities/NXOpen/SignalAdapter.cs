namespace NXOpen
{
    using NXOpen.Utilities;
    using System;

    public class SignalAdapter : RuntimeObject
    {
        private static Factory m_factory = new Factory();
        private const int SIGNAL_ADAPTER_GET_ADAPTER = 0;
        private const int SIGNAL_ADAPTER_GET_NUM_SIGNALS = 1;
        private const int SIGNAL_ADAPTER_GET_SIGNAL = 2;

        protected SignalAdapter(IntPtr pItem) : base(pItem)
        {
        }

        internal static void Init()
        {
            RuntimeObject.s_arrClassGen[0x186a0] = m_factory;
        }

        public override bool Active
        {
            get
            {
                IntPtr pFunc = PropertyFunc.ExGetFunc(base.m_pItem, 1);
                int nProp = PropertyFunc.ExFuncInvokeInt(pFunc);
                PropertyFunc.ExFuncRelease(pFunc);
                return (PropertyFunc.ExGetBool(base.m_pItem, nProp) != 0);
            }
            set
            {
                IntPtr pFunc = PropertyFunc.ExGetFunc(base.m_pItem, 1);
                int nProp = PropertyFunc.ExFuncInvokeInt(pFunc);
                PropertyFunc.ExFuncRelease(pFunc);
                if (PropertyFunc.ExSetBool(base.m_pItem, nProp, value ? -1 : 0) != 0)
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, nProp);
                }
            }
        }

        public Signal[] Signals
        {
            get
            {
                IntPtr pFunc = PropertyFunc.ExGetFunc(base.m_pItem, 1);
                IntPtr ptr2 = PropertyFunc.ExGetFunc(base.m_pItem, 2);
                int num = PropertyFunc.ExFuncInvokeInt(pFunc);
                Signal[] signalArray = new Signal[num];
                for (int i = 0; i < num; i++)
                {
                    PropertyFunc.ExSetFuncArgInt(ptr2, 0, i);
                    IntPtr pItem = PropertyFunc.ExFuncInvokeObject(ptr2);
                    signalArray[i] = RuntimeObject.FromPtr(pItem) as Signal;
                }
                PropertyFunc.ExFuncRelease(pFunc);
                PropertyFunc.ExFuncRelease(ptr2);
                return signalArray;
            }
        }

        internal class Factory : IItemFactory
        {
            public RuntimeObject Create(IntPtr pItem)
            {
                return new SignalAdapter(pItem);
            }
        }
    }
}

