namespace NXOpen
{
    using NXOpen.Utilities;
    using System;

    public class RuntimeParameters : RuntimeObject
    {
        private static Factory m_factory = new Factory();

        protected RuntimeParameters(IntPtr pItem) : base(pItem)
        {
        }

        public int GetNumParameters()
        {
            return PropertyFunc.ExGetNumProps(base.m_pItem);
        }

        public Parameter GetParameter(int nProp)
        {
            Parameter parameter = new Parameter();
            int num = PropertyFunc.ExGetNumProps(base.m_pItem);
            if ((nProp >= 0) && (nProp < num))
            {
                if (PropertyFunc.ExGetPropType(base.m_pItem, nProp) == 1)
                {
                    parameter.SetValue(PropertyFunc.ExGetInt(base.m_pItem, nProp));
                    return parameter;
                }
                if (PropertyFunc.ExGetPropType(base.m_pItem, nProp) == 2)
                {
                    parameter.SetValue(PropertyFunc.ExGetFloat(base.m_pItem, nProp));
                    return parameter;
                }
                if (PropertyFunc.ExGetPropType(base.m_pItem, nProp) == 3)
                {
                    parameter.SetValue(PropertyFunc.ExGetBool(base.m_pItem, nProp) != 0);
                }
            }
            return parameter;
        }

        internal static void Init()
        {
            RuntimeObject.s_arrClassGen[0x1b198] = m_factory;
        }

        public void SetParameter(int nProp, Parameter value)
        {
            int num = PropertyFunc.ExGetNumProps(base.m_pItem);
            if ((nProp >= 0) && (nProp < num))
            {
                bool flag = false;
                if (PropertyFunc.ExGetPropType(base.m_pItem, nProp) == 1)
                {
                    flag = PropertyFunc.ExSetInt(base.m_pItem, nProp, value.IntValue) != 0;
                }
                else if (PropertyFunc.ExGetPropType(base.m_pItem, nProp) == 2)
                {
                    flag = PropertyFunc.ExSetFloat(base.m_pItem, nProp, value.FloatValue) != 0;
                }
                else if (PropertyFunc.ExGetPropType(base.m_pItem, nProp) == 3)
                {
                    flag = PropertyFunc.ExSetBool(base.m_pItem, nProp, value.BoolValue ? -1 : 0) != 0;
                }
                if (flag)
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, nProp);
                }
            }
        }

        internal class Factory : IItemFactory
        {
            public RuntimeObject Create(IntPtr pItem)
            {
                return new RuntimeParameters(pItem);
            }
        }
    }
}

