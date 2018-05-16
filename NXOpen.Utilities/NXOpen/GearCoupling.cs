namespace NXOpen
{
    using NXOpen.Utilities;
    using NXOpen.VectorArithmetic;
    using System;

    public class GearCoupling : RuntimeObject
    {
        private const int GEAR_COUPLING_ACTIVE = 5;
        private const int GEAR_COUPLING_ALLOW_SLIP = 4;
        private const int GEAR_COUPLING_MASTER_AXIS = 0;
        private const int GEAR_COUPLING_MASTER_MULTIPLE = 2;
        private const int GEAR_COUPLING_SLAVE_AXIS = 1;
        private const int GEAR_COUPLING_SLAVE_MULTIPLE = 3;
        private static Factory m_factory = new Factory();

        protected GearCoupling(IntPtr pItem) : base(pItem)
        {
        }

        public Vector3 GetMasterForce(double stepSize)
        {
            double x = 0.0;
            double y = 0.0;
            x = InstanceFunc.ExGetCouplingMasterForce(base.m_pItem, stepSize, 1);
            y = InstanceFunc.ExGetCouplingMasterForce(base.m_pItem, stepSize, 2);
            return new Vector3(x, y, InstanceFunc.ExGetCouplingMasterForce(base.m_pItem, stepSize, 3));
        }

        public Vector3 GetMasterTorque(double stepSize)
        {
            double x = 0.0;
            double y = 0.0;
            x = InstanceFunc.ExGetCouplingMasterTorque(base.m_pItem, stepSize, 1);
            y = InstanceFunc.ExGetCouplingMasterTorque(base.m_pItem, stepSize, 2);
            return new Vector3(x, y, InstanceFunc.ExGetCouplingMasterTorque(base.m_pItem, stepSize, 3));
        }

        public Vector3 GetSlaveForce(double stepSize)
        {
            double x = 0.0;
            double y = 0.0;
            x = InstanceFunc.ExGetCouplingSlaveForce(base.m_pItem, stepSize, 1);
            y = InstanceFunc.ExGetCouplingSlaveForce(base.m_pItem, stepSize, 2);
            return new Vector3(x, y, InstanceFunc.ExGetCouplingSlaveForce(base.m_pItem, stepSize, 3));
        }

        public Vector3 GetSlaveTorque(double stepSize)
        {
            double x = 0.0;
            double y = 0.0;
            x = InstanceFunc.ExGetCouplingSlaveTorque(base.m_pItem, stepSize, 1);
            y = InstanceFunc.ExGetCouplingSlaveTorque(base.m_pItem, stepSize, 2);
            return new Vector3(x, y, InstanceFunc.ExGetCouplingSlaveTorque(base.m_pItem, stepSize, 3));
        }

        internal static void Init()
        {
            RuntimeObject.s_arrClassGen[0x8156] = m_factory;
            RuntimeObject.s_arrClassGen[0x8160] = m_factory;
        }

        public override bool Active
        {
            get
            {
                return (PropertyFunc.ExGetBool(base.m_pItem, 5) != 0);
            }
            set
            {
                if (PropertyFunc.ExSetBool(base.m_pItem, 5, value ? -1 : 0) != 0)
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, 5);
                }
            }
        }

        public bool AllowSlip
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
                return (RuntimeObject.FromPtr(PropertyFunc.ExGetObject(base.m_pItem, 0)) as AxisJoint);
            }
            set
            {
                if (PropertyFunc.ExSetObject(base.m_pItem, 0, value.GetPtr()) != 0)
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, 0);
                }
            }
        }

        public double MasterMultiple
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

        public AxisJoint SlaveAxis
        {
            get
            {
                return (RuntimeObject.FromPtr(PropertyFunc.ExGetObject(base.m_pItem, 1)) as AxisJoint);
            }
            set
            {
                if (PropertyFunc.ExSetObject(base.m_pItem, 1, value.GetPtr()) != 0)
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, 1);
                }
            }
        }

        public double SlaveMultiple
        {
            get
            {
                return PropertyFunc.ExGetFloat(base.m_pItem, 3);
            }
            set
            {
                if (PropertyFunc.ExSetFloat(base.m_pItem, 3, value) != 0)
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, 3);
                }
            }
        }

        internal class Factory : IItemFactory
        {
            public RuntimeObject Create(IntPtr pItem)
            {
                return new GearCoupling(pItem);
            }
        }
    }
}

