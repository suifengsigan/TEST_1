namespace NXOpen
{
    using NXOpen.Utilities;
    using NXOpen.VectorArithmetic;
    using System;

    public class SlidingJoint : AxisJoint
    {
        private static Factory m_factory = new Factory();
        private const int SLIDING_JOINT_ACTIVE = 6;
        private const int SLIDING_JOINT_ATTACH = 0;
        private const int SLIDING_JOINT_AXIS_X = 2;
        private const int SLIDING_JOINT_AXIS_Y = 3;
        private const int SLIDING_JOINT_AXIS_Z = 4;
        private const int SLIDING_JOINT_BASE = 1;
        private const int SLIDING_JOINT_POSITION = 5;

        protected SlidingJoint(IntPtr pItem) : base(pItem)
        {
        }

        public double GetVelocity()
        {
            Vector3 vector = new Vector3(0.0, 0.0, 0.0);
            if (this.Attach != null)
            {
                vector = (this.Base != null) ? (this.Attach.LinearVelocity - this.Base.LinearVelocity) : this.Attach.LinearVelocity;
            }
            double num = 0.0;
            double num2 = Math.Sqrt(this.Axis.LengthSqr());
            if (num2 != 0.0)
            {
                num = vector.Dot(this.Axis) / num2;
            }
            return num;
        }

        internal static void Init()
        {
            RuntimeObject.s_arrClassGen[0x52d0] = m_factory;
        }

        public override bool Active
        {
            get
            {
                return (PropertyFunc.ExGetBool(base.m_pItem, 6) != 0);
            }
            set
            {
                if (PropertyFunc.ExSetBool(base.m_pItem, 6, value ? -1 : 0) != 0)
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, 6);
                }
            }
        }

        public override RigidBody Attach
        {
            get
            {
                return (RuntimeObject.FromPtr(PropertyFunc.ExGetObject(base.m_pItem, 0)) as RigidBody);
            }
            set
            {
                if (PropertyFunc.ExSetObject(base.m_pItem, 0, value.GetPtr()) != 0)
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, 0);
                }
            }
        }

        public override Vector3 Axis
        {
            get
            {
                double x = PropertyFunc.ExGetFloat(base.m_pItem, 2);
                double y = PropertyFunc.ExGetFloat(base.m_pItem, 3);
                return new Vector3(x, y, PropertyFunc.ExGetFloat(base.m_pItem, 4));
            }
            set
            {
                bool flag = PropertyFunc.ExSetFloat(base.m_pItem, 2, value.x) != 0;
                bool flag2 = PropertyFunc.ExSetFloat(base.m_pItem, 3, value.y) != 0;
                bool flag3 = PropertyFunc.ExSetFloat(base.m_pItem, 4, value.z) != 0;
                if ((flag || flag2) || flag3)
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, 2);
                }
            }
        }

        public override RigidBody Base
        {
            get
            {
                return (RuntimeObject.FromPtr(PropertyFunc.ExGetObject(base.m_pItem, 1)) as RigidBody);
            }
            set
            {
                if (PropertyFunc.ExSetObject(base.m_pItem, 1, value.GetPtr()) != 0)
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, 1);
                }
            }
        }

        public double Position
        {
            get
            {
                return PropertyFunc.ExGetFloat(base.m_pItem, 5);
            }
        }

        internal class Factory : IItemFactory
        {
            public RuntimeObject Create(IntPtr pItem)
            {
                return new SlidingJoint(pItem);
            }
        }
    }
}

