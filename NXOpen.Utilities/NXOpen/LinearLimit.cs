namespace NXOpen
{
    using NXOpen.Utilities;
    using NXOpen.VectorArithmetic;
    using System;

    public class LinearLimit : LimitJoint
    {
        private const int LINEAR_LIMIT_ACTIVE = 10;
        private const int LINEAR_LIMIT_ATTACH = 0;
        private const int LINEAR_LIMIT_ATTACH_POSITION_X = 2;
        private const int LINEAR_LIMIT_ATTACH_POSITION_Y = 3;
        private const int LINEAR_LIMIT_ATTACH_POSITION_Z = 4;
        private const int LINEAR_LIMIT_BASE = 1;
        private const int LINEAR_LIMIT_BASE_POSITION_X = 5;
        private const int LINEAR_LIMIT_BASE_POSITION_Y = 6;
        private const int LINEAR_LIMIT_BASE_POSITION_Z = 7;
        private const int LINEAR_LIMIT_MAXIMUM_POSITION = 9;
        private const int LINEAR_LIMIT_MINIMUM_POSITION = 8;
        private static Factory m_factory = new Factory();

        protected LinearLimit(IntPtr pItem) : base(pItem)
        {
        }

        internal static void Init()
        {
            RuntimeObject.s_arrClassGen[0x6a40] = m_factory;
        }

        public override bool Active
        {
            get
            {
                return (PropertyFunc.ExGetBool(base.m_pItem, 10) != 0);
            }
            set
            {
                if (PropertyFunc.ExSetBool(base.m_pItem, 10, value ? -1 : 0) != 0)
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, 10);
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

        public override Vector3 AttachVector
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

        public override Vector3 BaseVector
        {
            get
            {
                double x = PropertyFunc.ExGetFloat(base.m_pItem, 5);
                double y = PropertyFunc.ExGetFloat(base.m_pItem, 6);
                return new Vector3(x, y, PropertyFunc.ExGetFloat(base.m_pItem, 7));
            }
            set
            {
                bool flag = PropertyFunc.ExSetFloat(base.m_pItem, 5, value.x) != 0;
                bool flag2 = PropertyFunc.ExSetFloat(base.m_pItem, 6, value.y) != 0;
                bool flag3 = PropertyFunc.ExSetFloat(base.m_pItem, 7, value.z) != 0;
                if ((flag || flag2) || flag3)
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, 5);
                }
            }
        }

        public override double Maximum
        {
            get
            {
                return PropertyFunc.ExGetFloat(base.m_pItem, 9);
            }
            set
            {
                if (PropertyFunc.ExSetFloat(base.m_pItem, 9, value) != 0)
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, 9);
                }
            }
        }

        public override double Minimum
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

        internal class Factory : IItemFactory
        {
            public RuntimeObject Create(IntPtr pItem)
            {
                return new LinearLimit(pItem);
            }
        }
    }
}

