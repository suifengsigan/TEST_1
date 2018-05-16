namespace NXOpen
{
    using NXOpen.Utilities;
    using System;

    public class SpeedControl : ControlBase
    {
        private static Factory m_factory = new Factory();
        private const int SPEED_CONSTRAINT_ACCELERATION = 4;
        private const int SPEED_CONSTRAINT_ACTIVE = 2;
        private const int SPEED_CONSTRAINT_AXIS = 0;
        private const int SPEED_CONSTRAINT_JERK = 6;
        private const int SPEED_CONSTRAINT_LIMIT_JERK = 5;
        private const int SPEED_CONSTRAINT_SPEED = 1;
        private const int SPEED_CONSTRAINT_USE_ACCELERATION = 3;

        protected SpeedControl(IntPtr pItem) : base(pItem)
        {
        }

        internal static void Init()
        {
            RuntimeObject.s_arrClassGen[0x7918] = m_factory;
            RuntimeObject.s_arrClassGen[0x797c] = m_factory;
            RuntimeObject.s_arrClassGen[0x79e0] = m_factory;
        }

        public double Acceleration
        {
            get
            {
                return PropertyFunc.ExGetFloat(base.m_pItem, 4);
            }
            set
            {
                if (PropertyFunc.ExSetFloat(base.m_pItem, 4, value) != 0)
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, 4);
                }
            }
        }

        public override bool Active
        {
            get
            {
                return (PropertyFunc.ExGetBool(base.m_pItem, 2) != 0);
            }
            set
            {
                if (PropertyFunc.ExSetBool(base.m_pItem, 2, value ? -1 : 0) != 0)
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, 2);
                }
            }
        }

        public AxisJoint Axis
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

        public double Jerk
        {
            get
            {
                return PropertyFunc.ExGetFloat(base.m_pItem, 6);
            }
            set
            {
                if (PropertyFunc.ExSetFloat(base.m_pItem, 6, value) != 0)
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, 6);
                }
            }
        }

        public bool LimitAcceleration
        {
            get
            {
                return (PropertyFunc.ExGetBool(base.m_pItem, 3) != 0);
            }
            set
            {
                if (PropertyFunc.ExSetBool(base.m_pItem, 3, value ? -1 : 0) != 0)
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, 3);
                }
            }
        }

        public bool LimitJerk
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

        public double Speed
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

        internal class Factory : IItemFactory
        {
            public RuntimeObject Create(IntPtr pItem)
            {
                return new SpeedControl(pItem);
            }
        }
    }
}

