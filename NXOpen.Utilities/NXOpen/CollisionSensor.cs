namespace NXOpen
{
    using NXOpen.Utilities;
    using System;

    public class CollisionSensor : ShapeBody
    {
        private static Factory m_factory = new Factory();
        private const int TRIGGER_BODY_ACTIVE = 1;
        private const int TRIGGER_BODY_ASK_NUM_INTERSECT = 1;
        private const int TRIGGER_BODY_GET_INTERSECT = 2;
        private const int TRIGGER_BODY_GET_OWNER = 0;
        private const int TRIGGER_BODY_IS_MEMBER = 3;
        private const int TRIGGER_BODY_TRIGGERED = 0;

        protected CollisionSensor(IntPtr pItem) : base(pItem)
        {
        }

        public override RigidBody GetOwner()
        {
            IntPtr pFunc = PropertyFunc.ExGetFunc(base.m_pItem, 0);
            IntPtr pItem = PropertyFunc.ExFuncInvokeObject(pFunc);
            PropertyFunc.ExFuncRelease(pFunc);
            return (RuntimeObject.FromPtr(pItem) as RigidBody);
        }

        internal static void Init()
        {
            RuntimeObject.s_arrClassGen[0x32c8] = m_factory;
        }

        public bool IsMember(CollisionBody part)
        {
            IntPtr pFunc = PropertyFunc.ExGetFunc(base.m_pItem, 3);
            PropertyFunc.ExSetFuncArgObject(pFunc, 0, part.GetPtr());
            bool flag = PropertyFunc.ExFuncInvokeBool(pFunc) != 0;
            PropertyFunc.ExFuncRelease(pFunc);
            return flag;
        }

        public bool IsMember(RigidBody body)
        {
            IntPtr pFunc = PropertyFunc.ExGetFunc(base.m_pItem, 3);
            PropertyFunc.ExSetFuncArgObject(pFunc, 0, body.GetPtr());
            bool flag = PropertyFunc.ExFuncInvokeBool(pFunc) != 0;
            PropertyFunc.ExFuncRelease(pFunc);
            return flag;
        }

        public override bool Active
        {
            get
            {
                return (PropertyFunc.ExGetBool(base.m_pItem, 1) != 0);
            }
            set
            {
                if (PropertyFunc.ExSetBool(base.m_pItem, 1, value ? -1 : 0) != 0)
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, 1);
                }
            }
        }

        public CollisionBody[] Intersects
        {
            get
            {
                IntPtr pFunc = PropertyFunc.ExGetFunc(base.m_pItem, 1);
                int num = PropertyFunc.ExFuncInvokeInt(pFunc);
                PropertyFunc.ExFuncRelease(pFunc);
                CollisionBody[] bodyArray = new CollisionBody[num];
                IntPtr ptr2 = PropertyFunc.ExGetFunc(base.m_pItem, 2);
                for (int i = 0; i < num; i++)
                {
                    PropertyFunc.ExSetFuncArgInt(ptr2, 0, i);
                    IntPtr pItem = PropertyFunc.ExFuncInvokeObject(ptr2);
                    bodyArray[i] = RuntimeObject.FromPtr(pItem) as CollisionBody;
                }
                PropertyFunc.ExFuncRelease(ptr2);
                return bodyArray;
            }
        }

        public int NumIntersect
        {
            get
            {
                IntPtr pFunc = PropertyFunc.ExGetFunc(base.m_pItem, 1);
                int num = PropertyFunc.ExFuncInvokeInt(pFunc);
                PropertyFunc.ExFuncRelease(pFunc);
                return num;
            }
        }

        public bool Triggered
        {
            get
            {
                return (PropertyFunc.ExGetBool(base.m_pItem, 0) != 0);
            }
            set
            {
                if (PropertyFunc.ExSetBool(base.m_pItem, 0, value ? -1 : 0) != 0)
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, 0);
                }
            }
        }

        internal class Factory : IItemFactory
        {
            public RuntimeObject Create(IntPtr pItem)
            {
                return new CollisionSensor(pItem);
            }
        }
    }
}

