namespace NXOpen
{
    using NXOpen.Utilities;
    using System;

    public class GraphControl : RuntimeObject
    {
        private const int GRAPH_CONTROL_ACTIVE = 4;
        private const int GRAPH_CONTROL_AXIS_CONTROL = 0;
        private const int GRAPH_CONTROL_INITIAL_OFFSET = 1;
        private const int GRAPH_CONTROL_MASTER_SCALE = 5;
        private const int GRAPH_CONTROL_REPEAT = 3;
        private const int GRAPH_CONTROL_SLAVE_SCALE = 6;
        private const int GRAPH_CONTROL_VALUE_OFFSET = 2;
        private static Factory m_factory = new Factory();

        protected GraphControl(IntPtr pItem) : base(pItem)
        {
        }

        internal static void Init()
        {
            RuntimeObject.s_arrClassGen[0x13c68] = m_factory;
            RuntimeObject.s_arrClassGen[0x13ccc] = m_factory;
            RuntimeObject.s_arrClassGen[0x13cd6] = m_factory;
            RuntimeObject.s_arrClassGen[0x13ce0] = m_factory;
            RuntimeObject.s_arrClassGen[0x13d30] = m_factory;
            RuntimeObject.s_arrClassGen[0x13d3a] = m_factory;
            RuntimeObject.s_arrClassGen[0x13d44] = m_factory;
        }

        public override bool Active
        {
            get
            {
                return (PropertyFunc.ExGetBool(base.m_pItem, 4) != 0);
            }
            set
            {
                if (PropertyFunc.ExSetBool(base.m_pItem, 4, value ? -1 : 0) != 0)
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, 4);
                }
            }
        }

        public ControlBase AxisControl
        {
            get
            {
                return (RuntimeObject.FromPtr(PropertyFunc.ExGetObject(base.m_pItem, 0)) as ControlBase);
            }
            set
            {
                if (PropertyFunc.ExSetObject(base.m_pItem, 0, value.GetPtr()) != 0)
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, 0);
                }
            }
        }

        public double InitialTime
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

        public double MasterScale
        {
            get
            {
                return PropertyFunc.ExGetFloat(base.m_pItem, 5);
            }
            set
            {
                if ((value >= 0.0) && (PropertyFunc.ExSetFloat(base.m_pItem, 5, value) != 0))
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, 5);
                }
            }
        }

        public bool Repeat
        {
            get
            {
                return (PropertyFunc.ExGetBool(base.m_pItem, 3) != 0);
            }
        }

        public double SlaveScale
        {
            get
            {
                return PropertyFunc.ExGetFloat(base.m_pItem, 6);
            }
            set
            {
                if ((value >= 0.0) && (PropertyFunc.ExSetFloat(base.m_pItem, 6, value) != 0))
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, 6);
                }
            }
        }

        public double ValueOffset
        {
            get
            {
                return PropertyFunc.ExGetFloat(base.m_pItem, 2);
            }
            set
            {
                if (PropertyFunc.ExSetFloat(base.m_pItem, 2, value) != 0)
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, 2);
                }
            }
        }

        internal class Factory : IItemFactory
        {
            public RuntimeObject Create(IntPtr pItem)
            {
                return new GraphControl(pItem);
            }
        }
    }
}

