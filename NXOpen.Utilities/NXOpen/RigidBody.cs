namespace NXOpen
{
    using NXOpen.Utilities;
    using NXOpen.VectorArithmetic;
    using System;

    public class RigidBody : RuntimeObject
    {
        private static Factory m_factory = new Factory();
        private const int RIGID_BODY_ACTIVE = 0x12;
        private const int RIGID_BODY_ANGULAR_VELOCITY_X = 15;
        private const int RIGID_BODY_ANGULAR_VELOCITY_Y = 0x10;
        private const int RIGID_BODY_ANGULAR_VELOCITY_Z = 0x11;
        private const int RIGID_BODY_APPLY_FORCE = 2;
        private const int RIGID_BODY_APPLY_TORQUE = 3;
        private const int RIGID_BODY_ASK_NUM_PARTS = 0;
        private const int RIGID_BODY_GET_PART = 1;
        private const int RIGID_BODY_LINEAR_VELOCITY_X = 12;
        private const int RIGID_BODY_LINEAR_VELOCITY_Y = 13;
        private const int RIGID_BODY_LINEAR_VELOCITY_Z = 14;
        private const int RIGID_BODY_ORIENTATION_M11 = 3;
        private const int RIGID_BODY_ORIENTATION_M12 = 4;
        private const int RIGID_BODY_ORIENTATION_M13 = 5;
        private const int RIGID_BODY_ORIENTATION_M21 = 6;
        private const int RIGID_BODY_ORIENTATION_M22 = 7;
        private const int RIGID_BODY_ORIENTATION_M23 = 8;
        private const int RIGID_BODY_ORIENTATION_M31 = 8;
        private const int RIGID_BODY_ORIENTATION_M32 = 10;
        private const int RIGID_BODY_ORIENTATION_M33 = 11;
        private const int RIGID_BODY_POSITION_X = 0;
        private const int RIGID_BODY_POSITION_Y = 1;
        private const int RIGID_BODY_POSITION_Z = 2;

        protected RigidBody(IntPtr pItem) : base(pItem)
        {
        }

        public void ActivateAll(bool bActive)
        {
            this.Active = bActive;
            foreach (ShapeBody body in this.Parts)
            {
                body.Active = bActive;
                CollisionBody body2 = body as CollisionBody;
                if (body2 != null)
                {
                    foreach (TransportSurface surface in body2.TransportSurfaces)
                    {
                        surface.Active = true;
                    }
                }
            }
        }

        public void ApplyForce(Vector3 force)
        {
            IntPtr pFunc = PropertyFunc.ExGetFunc(base.m_pItem, 2);
            PropertyFunc.ExSetFuncArgFloat(pFunc, 0, force.x);
            PropertyFunc.ExSetFuncArgFloat(pFunc, 1, force.y);
            PropertyFunc.ExSetFuncArgFloat(pFunc, 2, force.z);
            PropertyFunc.ExFuncInvokeVoid(pFunc);
            PropertyFunc.ExFuncRelease(pFunc);
        }

        public void ApplyTorque(Vector3 torque)
        {
            IntPtr pFunc = PropertyFunc.ExGetFunc(base.m_pItem, 3);
            PropertyFunc.ExSetFuncArgFloat(pFunc, 0, torque.x);
            PropertyFunc.ExSetFuncArgFloat(pFunc, 1, torque.y);
            PropertyFunc.ExSetFuncArgFloat(pFunc, 2, torque.z);
            PropertyFunc.ExFuncInvokeVoid(pFunc);
            PropertyFunc.ExFuncRelease(pFunc);
        }

        public RigidBody Copy()
        {
            return (RuntimeObject.FromPtr(InstanceFunc.ExCopy(base.m_pItem)) as RigidBody);
        }

        internal static void Init()
        {
            RuntimeObject.s_arrClassGen[0x2af8] = m_factory;
        }

        public override bool Active
        {
            get
            {
                return (PropertyFunc.ExGetBool(base.m_pItem, 0x12) != 0);
            }
            set
            {
                if (PropertyFunc.ExSetBool(base.m_pItem, 0x12, value ? -1 : 0) != 0)
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, 0x12);
                }
            }
        }

        public Vector3 AngularVelocity
        {
            get
            {
                double x = PropertyFunc.ExGetFloat(base.m_pItem, 15);
                double y = PropertyFunc.ExGetFloat(base.m_pItem, 0x10);
                return new Vector3(x, y, PropertyFunc.ExGetFloat(base.m_pItem, 0x11));
            }
            set
            {
                bool flag = PropertyFunc.ExSetFloat(base.m_pItem, 15, value.x) != 0;
                bool flag2 = PropertyFunc.ExSetFloat(base.m_pItem, 0x10, value.y) != 0;
                bool flag3 = PropertyFunc.ExSetFloat(base.m_pItem, 0x11, value.z) != 0;
                if ((flag || flag2) || flag3)
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, 15);
                }
            }
        }

        public Vector3 LinearVelocity
        {
            get
            {
                double x = PropertyFunc.ExGetFloat(base.m_pItem, 12);
                double y = PropertyFunc.ExGetFloat(base.m_pItem, 13);
                return new Vector3(x, y, PropertyFunc.ExGetFloat(base.m_pItem, 14));
            }
            set
            {
                bool flag = PropertyFunc.ExSetFloat(base.m_pItem, 12, value.x) != 0;
                bool flag2 = PropertyFunc.ExSetFloat(base.m_pItem, 13, value.y) != 0;
                bool flag3 = PropertyFunc.ExSetFloat(base.m_pItem, 14, value.z) != 0;
                if ((flag || flag2) || flag3)
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, 12);
                }
            }
        }

        public Matrix3 Orientation
        {
            get
            {
                double num = PropertyFunc.ExGetFloat(base.m_pItem, 3);
                double num2 = PropertyFunc.ExGetFloat(base.m_pItem, 4);
                double num3 = PropertyFunc.ExGetFloat(base.m_pItem, 5);
                double num4 = PropertyFunc.ExGetFloat(base.m_pItem, 6);
                double num5 = PropertyFunc.ExGetFloat(base.m_pItem, 7);
                double num6 = PropertyFunc.ExGetFloat(base.m_pItem, 8);
                double num7 = PropertyFunc.ExGetFloat(base.m_pItem, 8);
                double num8 = PropertyFunc.ExGetFloat(base.m_pItem, 10);
                return new Matrix3(num, num2, num3, num4, num5, num6, num7, num8, PropertyFunc.ExGetFloat(base.m_pItem, 11));
            }
            set
            {
                bool flag = PropertyFunc.ExSetFloat(base.m_pItem, 3, value.m[0]) != 0;
                bool flag2 = PropertyFunc.ExSetFloat(base.m_pItem, 4, value.m[1]) != 0;
                bool flag3 = PropertyFunc.ExSetFloat(base.m_pItem, 5, value.m[2]) != 0;
                bool flag4 = PropertyFunc.ExSetFloat(base.m_pItem, 6, value.m[3]) != 0;
                bool flag5 = PropertyFunc.ExSetFloat(base.m_pItem, 7, value.m[4]) != 0;
                bool flag6 = PropertyFunc.ExSetFloat(base.m_pItem, 8, value.m[5]) != 0;
                bool flag7 = PropertyFunc.ExSetFloat(base.m_pItem, 8, value.m[6]) != 0;
                bool flag8 = PropertyFunc.ExSetFloat(base.m_pItem, 10, value.m[7]) != 0;
                bool flag9 = PropertyFunc.ExSetFloat(base.m_pItem, 11, value.m[8]) != 0;
                if (((flag || flag2) || (flag3 || flag4)) || ((flag5 || flag6) || ((flag7 || flag8) || flag9)))
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, 3);
                }
            }
        }

        public ShapeBody[] Parts
        {
            get
            {
                IntPtr pFunc = PropertyFunc.ExGetFunc(base.m_pItem, 0);
                int num = PropertyFunc.ExFuncInvokeInt(pFunc);
                PropertyFunc.ExFuncRelease(pFunc);
                ShapeBody[] bodyArray = new ShapeBody[num];
                IntPtr ptr2 = PropertyFunc.ExGetFunc(base.m_pItem, 1);
                for (int i = 0; i < num; i++)
                {
                    PropertyFunc.ExSetFuncArgInt(ptr2, 0, i);
                    IntPtr pItem = PropertyFunc.ExFuncInvokeObject(ptr2);
                    bodyArray[i] = RuntimeObject.FromPtr(pItem) as ShapeBody;
                }
                PropertyFunc.ExFuncRelease(ptr2);
                return bodyArray;
            }
        }

        public Vector3 Position
        {
            get
            {
                double x = PropertyFunc.ExGetFloat(base.m_pItem, 0);
                double y = PropertyFunc.ExGetFloat(base.m_pItem, 1);
                return new Vector3(x, y, PropertyFunc.ExGetFloat(base.m_pItem, 2));
            }
            set
            {
                bool flag = PropertyFunc.ExSetFloat(base.m_pItem, 0, value.x) != 0;
                bool flag2 = PropertyFunc.ExSetFloat(base.m_pItem, 1, value.y) != 0;
                bool flag3 = PropertyFunc.ExSetFloat(base.m_pItem, 2, value.z) != 0;
                if ((flag || flag2) || flag3)
                {
                    PropertyFunc.ExSetDirty(base.m_pItem, 0);
                }
            }
        }

        internal class Factory : IItemFactory
        {
            public RuntimeObject Create(IntPtr pItem)
            {
                return new RigidBody(pItem);
            }
        }
    }
}

