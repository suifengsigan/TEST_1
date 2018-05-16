namespace NXOpen
{
    using NXOpen.Utilities;
    using System;

    public class ComponentPart : RuntimeObject
    {
        private const int COMPONENT_ASK_NUM_PARTS = 0;
        private const int COMPONENT_GET_PART = 1;
        private static Factory m_factory = new Factory();

        protected ComponentPart(IntPtr pItem) : base(pItem)
        {
        }

        public void ActivateAll(bool bActive)
        {
            this.Active = bActive;
            foreach (RuntimeObject obj2 in this.Parts)
            {
                obj2.Active = bActive;
                ComponentPart part = obj2 as ComponentPart;
                if (part != null)
                {
                    part.ActivateAll(bActive);
                }
            }
        }

        public ComponentPart Copy()
        {
            return (RuntimeObject.FromPtr(InstanceFunc.ExCopy(base.m_pItem)) as ComponentPart);
        }

        internal static void Init()
        {
            RuntimeObject.s_arrClassGen[0xea60] = m_factory;
        }

        public int NumParts
        {
            get
            {
                IntPtr pFunc = PropertyFunc.ExGetFunc(base.m_pItem, 0);
                int num = PropertyFunc.ExFuncInvokeInt(pFunc);
                PropertyFunc.ExFuncRelease(pFunc);
                return num;
            }
        }

        public RuntimeObject[] Parts
        {
            get
            {
                IntPtr pFunc = PropertyFunc.ExGetFunc(base.m_pItem, 0);
                int num = PropertyFunc.ExFuncInvokeInt(pFunc);
                PropertyFunc.ExFuncRelease(pFunc);
                RuntimeObject[] objArray = new RuntimeObject[num];
                IntPtr ptr2 = PropertyFunc.ExGetFunc(base.m_pItem, 1);
                PropertyFunc.ExSetFuncArgObject(ptr2, 0, IntPtr.Zero);
                IntPtr pItem = PropertyFunc.ExFuncInvokeObject(ptr2);
                for (int i = 0; pItem != IntPtr.Zero; i++)
                {
                    objArray[i] = RuntimeObject.FromPtr(pItem);
                    PropertyFunc.ExSetFuncArgObject(ptr2, 0, pItem);
                    pItem = PropertyFunc.ExFuncInvokeObject(ptr2);
                }
                PropertyFunc.ExFuncRelease(ptr2);
                return objArray;
            }
        }

        internal class Factory : IItemFactory
        {
            public RuntimeObject Create(IntPtr pItem)
            {
                return new ComponentPart(pItem);
            }
        }
    }
}

