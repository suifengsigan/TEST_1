namespace NXOpen
{
    using NXOpen.Utilities;
    using System;

    public class FixedJoint : Joint
    {
        private const int FIXED_JOINT_ACTIVE = 2;
        private const int FIXED_JOINT_ATTACH = 0;
        private const int FIXED_JOINT_BASE = 1;
        private static Factory m_factory = new Factory();

        protected FixedJoint(IntPtr pItem) : base(pItem)
        {
        }

        internal static void Init()
        {
            RuntimeObject.s_arrClassGen[0x5dc0] = m_factory;
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

        internal class Factory : IItemFactory
        {
            public RuntimeObject Create(IntPtr pItem)
            {
                return new FixedJoint(pItem);
            }
        }
    }
}

