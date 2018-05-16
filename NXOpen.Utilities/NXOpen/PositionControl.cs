namespace NXOpen
{
    using NXOpen.Utilities;
    using System;

    public class PositionControl : ControlBase
    {
        private static Factory m_factory = new Factory();
        private const int POSITION_CONSTRAINT_ACCELERATION = 5;
        private const int POSITION_CONSTRAINT_ACTIVE = 3;
        private const int POSITION_CONSTRAINT_AXIS = 0;
        private const int POSITION_CONSTRAINT_DECELERATION = 6;
        private const int POSITION_CONSTRAINT_JERK = 8;
        private const int POSITION_CONSTRAINT_LIMIT_JERK = 7;
        private const int POSITION_CONSTRAINT_POSITION = 2;
        private const int POSITION_CONSTRAINT_SPEED = 1;
        private const int POSITION_CONSTRAINT_USE_ACCELERATION = 4;

        protected PositionControl(IntPtr pItem) : base(pItem)
        {
        }

        internal static void Init()
        {
            RuntimeObject.s_arrClassGen[0x7d00] = m_factory;
            RuntimeObject.s_arrClassGen[0x7d64] = m_factory;
            RuntimeObject.s_arrClassGen[0x7dc8] = m_factory;
        }

        public double Acceleration
        {
            get
            {
                return PropertyFunc.ExGetFloat(base.m_pItem, 5);
            }
            set
            {
                if (PropertyFunc.ExSetFloat(base.m_pItem, 5, value) != 0)
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, 5);
                }
            }
        }

        public override bool Active
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

        public double Deceleration
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

        public double Jerk
        {
            get
            {
                return PropertyFunc.ExGetFloat(base.m_pItem, 8);
            }
            set
            {
                if (PropertyFunc.ExSetFloat(base.m_pItem, 8, value) != 0)
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, 8);
                }
            }
        }

        public bool LimitAcceleration
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

        public bool LimitJerk
        {
            get
            {
                return (PropertyFunc.ExGetBool(base.m_pItem, 7) != 0);
            }
            set
            {
                if (PropertyFunc.ExSetBool(base.m_pItem, 7, value ? -1 : 0) != 0)
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, 7);
                }
            }
        }

        public double Position
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
                return new PositionControl(pItem);
            }
        }
    }
}

