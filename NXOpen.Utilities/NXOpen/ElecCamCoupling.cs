namespace NXOpen
{
    using NXOpen.Utilities;
    using System;

    public class ElecCamCoupling : RuntimeObject
    {
        private const int ELEC_CAM_COUPLING_ACTIVE = 4;
        private const int ELEC_CAM_COUPLING_MASTER_AXIS = 7;
        private const int ELEC_CAM_COUPLING_MASTER_OFFSET = 1;
        private const int ELEC_CAM_COUPLING_MASTER_SCALE_FACTOR = 5;
        private const int ELEC_CAM_COUPLING_REPEAT = 3;
        private const int ELEC_CAM_COUPLING_SLAVE_AXIS_CONTROL = 0;
        private const int ELEC_CAM_COUPLING_SLAVE_OFFSET = 2;
        private const int ELEC_CAM_COUPLING_SLAVE_SCALE_FACTOR = 6;
        private static Factory m_factory = new Factory();

        protected ElecCamCoupling(IntPtr pItem) : base(pItem)
        {
        }

        internal static void Init()
        {
            RuntimeObject.s_arrClassGen[0x821e] = m_factory;
            RuntimeObject.s_arrClassGen[0x8228] = m_factory;
            RuntimeObject.s_arrClassGen[0x8232] = m_factory;
            RuntimeObject.s_arrClassGen[0x823c] = m_factory;
            RuntimeObject.s_arrClassGen[0x8246] = m_factory;
            RuntimeObject.s_arrClassGen[0x8250] = m_factory;
            RuntimeObject.s_arrClassGen[0x825a] = m_factory;
            RuntimeObject.s_arrClassGen[0x8264] = m_factory;
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

        public AxisJoint MasterAxis
        {
            get
            {
                return (RuntimeObject.FromPtr(PropertyFunc.ExGetObject(base.m_pItem, 7)) as AxisJoint);
            }
            set
            {
                if (PropertyFunc.ExSetObject(base.m_pItem, 7, value.GetPtr()) != 0)
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, 7);
                }
            }
        }

        public double MasterOffset
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

        public ControlBase SlaveControl
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

        public double SlaveOffset
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

        internal class Factory : IItemFactory
        {
            public RuntimeObject Create(IntPtr pItem)
            {
                return new ElecCamCoupling(pItem);
            }
        }
    }
}

