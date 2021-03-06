﻿namespace NXOpen
{
    using NXOpen.Utilities;
    using NXOpen.VectorArithmetic;
    using System;

    public class BallJoint : Joint
    {
        private const int BALL_JOINT_ACTIVE = 5;
        private const int BALL_JOINT_ANCHOR_X = 2;
        private const int BALL_JOINT_ANCHOR_Y = 3;
        private const int BALL_JOINT_ANCHOR_Z = 4;
        private const int BALL_JOINT_ATTACH = 0;
        private const int BALL_JOINT_BASE = 1;
        private static Factory m_factory = new Factory();

        protected BallJoint(IntPtr pItem) : base(pItem)
        {
        }

        internal static void Init()
        {
            RuntimeObject.s_arrClassGen[0x61a8] = m_factory;
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

        public Vector3 Anchor
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
                return new BallJoint(pItem);
            }
        }
    }
}

