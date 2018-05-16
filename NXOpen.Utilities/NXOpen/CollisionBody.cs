namespace NXOpen
{
    using NXOpen.Utilities;
    using System;

    public class CollisionBody : ShapeBody
    {
        private const int COLLIDE_BODY_ACTIVE = 1;
        private const int COLLIDE_BODY_ASK_NUM_CONVEYOR = 1;
        private const int COLLIDE_BODY_GET_CONVEYOR = 2;
        private const int COLLIDE_BODY_GET_OWNER = 0;
        private const int COLLIDE_BODY_MATERIAL = 0;
        private static Factory m_factory = new Factory();

        protected CollisionBody(IntPtr pItem) : base(pItem)
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
            RuntimeObject.s_arrClassGen[0x2ee0] = m_factory;
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

        public CollisionMaterial Surface
        {
            get
            {
                return (RuntimeObject.FromPtr(PropertyFunc.ExGetObject(base.m_pItem, 0)) as CollisionMaterial);
            }
            set
            {
                if (PropertyFunc.ExSetObject(base.m_pItem, 0, value.GetPtr()) != 0)
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, 0);
                }
            }
        }

        public TransportSurface[] TransportSurfaces
        {
            get
            {
                IntPtr pFunc = PropertyFunc.ExGetFunc(base.m_pItem, 1);
                int num = PropertyFunc.ExFuncInvokeInt(pFunc);
                PropertyFunc.ExFuncRelease(pFunc);
                TransportSurface[] surfaceArray = new TransportSurface[num];
                IntPtr ptr2 = PropertyFunc.ExGetFunc(base.m_pItem, 2);
                for (int i = 0; i < num; i++)
                {
                    PropertyFunc.ExSetFuncArgInt(ptr2, 0, i);
                    IntPtr pItem = PropertyFunc.ExFuncInvokeObject(ptr2);
                    surfaceArray[i] = RuntimeObject.FromPtr(pItem) as TransportSurface;
                }
                PropertyFunc.ExFuncRelease(ptr2);
                return surfaceArray;
            }
        }

        internal class Factory : IItemFactory
        {
            public RuntimeObject Create(IntPtr pItem)
            {
                return new CollisionBody(pItem);
            }
        }
    }
}

