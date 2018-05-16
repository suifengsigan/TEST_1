namespace NXOpen.Utilities
{
    using NXOpen;
    using System;
    using System.Runtime.InteropServices;

    public class FetchConnect : IDefinitionContext
    {
        private IntPtr m_pSelf;

        public void Connect(string strName, out AngularLimit item)
        {
            ExAddProperty(this.m_pSelf, strName, 4, 0x69dc);
            item = null;
        }

        public void Connect(string strName, out AngularSpring item)
        {
            ExAddProperty(this.m_pSelf, strName, 4, 0x65f4);
            item = null;
        }

        public void Connect(string strName, out AxisJoint item)
        {
            ExAddProperty(this.m_pSelf, strName, 4, 0x5208);
            item = null;
        }

        public void Connect(string strName, out BallJoint item)
        {
            ExAddProperty(this.m_pSelf, strName, 4, 0x61a8);
            item = null;
        }

        public void Connect(string strName, out BreakingConstraint item)
        {
            ExAddProperty(this.m_pSelf, strName, 4, 0x84d0);
            item = null;
        }

        public void Connect(string strName, out CamCoupling item)
        {
            ExAddProperty(this.m_pSelf, strName, 4, 0x81b0);
            item = null;
        }

        public void Connect(string strName, out ChangeMaterial item)
        {
            ExAddProperty(this.m_pSelf, strName, 4, 0x88b8);
            item = null;
        }

        public void Connect(string strName, out CollisionBody item)
        {
            ExAddProperty(this.m_pSelf, strName, 4, 0x2ee0);
            item = null;
        }

        public void Connect(string strName, out CollisionMaterial item)
        {
            ExAddProperty(this.m_pSelf, strName, 4, 0x9c40);
            item = null;
        }

        public void Connect(string strName, out CollisionSensor item)
        {
            ExAddProperty(this.m_pSelf, strName, 4, 0x32c8);
            item = null;
        }

        public void Connect(string strName, out ComponentPart item)
        {
            ExAddProperty(this.m_pSelf, strName, 4, 0xea60);
            item = null;
        }

        public void Connect(string strName, out CylindricalJoint item)
        {
            ExAddProperty(this.m_pSelf, strName, 4, 0x5334);
            item = null;
        }

        public void Connect(string strName, out ElecCamCoupling item)
        {
            ExAddProperty(this.m_pSelf, strName, 4, 0x8214);
            item = null;
        }

        public void Connect(string strName, out ExternalConnection item)
        {
            ExAddProperty(this.m_pSelf, strName, 4, 0x15f90);
            item = null;
        }

        public void Connect(string strName, out FixedJoint item)
        {
            ExAddProperty(this.m_pSelf, strName, 4, 0x5dc0);
            item = null;
        }

        public void Connect(string strName, out GearCoupling item)
        {
            ExAddProperty(this.m_pSelf, strName, 4, 0x814c);
            item = null;
        }

        public void Connect(string strName, out GraphControl item)
        {
            ExAddProperty(this.m_pSelf, strName, 4, 0x13c68);
            item = null;
        }

        public void Connect(string strName, out HingeJoint item)
        {
            ExAddProperty(this.m_pSelf, strName, 4, 0x526c);
            item = null;
        }

        public void Connect(string strName, out Joint item)
        {
            ExAddProperty(this.m_pSelf, strName, 4, 0x4e20);
            item = null;
        }

        public void Connect(string strName, out LimitJoint item)
        {
            ExAddProperty(this.m_pSelf, strName, 4, 0x6978);
            item = null;
        }

        public void Connect(string strName, out LinearLimit item)
        {
            ExAddProperty(this.m_pSelf, strName, 4, 0x6a40);
            item = null;
        }

        public void Connect(string strName, out LinearSpring item)
        {
            ExAddProperty(this.m_pSelf, strName, 4, 0x6658);
            item = null;
        }

        public void Connect(string strName, out PositionControl item)
        {
            ExAddProperty(this.m_pSelf, strName, 4, 0x7d00);
            item = null;
        }

        public void Connect(string strName, out PreventCollision item)
        {
            ExAddProperty(this.m_pSelf, strName, 4, 0x8ca0);
            item = null;
        }

        public void Connect(string strName, out ProxyObject item)
        {
            ExAddProperty(this.m_pSelf, strName, 4, 0x1adb0);
            item = null;
        }

        public void Connect(string strName, out RigidBody item)
        {
            ExAddProperty(this.m_pSelf, strName, 4, 0x2af8);
            item = null;
        }

        public void Connect(string strName, out RuntimeObject item)
        {
            ExAddProperty(this.m_pSelf, strName, 4, 0x3e8);
            item = null;
        }

        public void Connect(string strName, out RuntimeParameters item)
        {
            ExAddProperty(this.m_pSelf, strName, 4, 0x1b198);
            item = null;
        }

        public void Connect(string strName, out Signal item)
        {
            ExAddProperty(this.m_pSelf, strName, 4, 0x18a88);
            item = null;
        }

        public void Connect(string strName, out SignalAdapter item)
        {
            ExAddProperty(this.m_pSelf, strName, 4, 0x186a0);
            item = null;
        }

        public void Connect(string strName, out SinkBehavior item)
        {
            ExAddProperty(this.m_pSelf, strName, 4, 0x11940);
            item = null;
        }

        public void Connect(string strName, out SlidingJoint item)
        {
            ExAddProperty(this.m_pSelf, strName, 4, 0x52d0);
            item = null;
        }

        public void Connect(string strName, out SourceBehavior item)
        {
            ExAddProperty(this.m_pSelf, strName, 4, 0x11558);
            item = null;
        }

        public void Connect(string strName, out SpeedControl item)
        {
            ExAddProperty(this.m_pSelf, strName, 4, 0x7918);
            item = null;
        }

        public void Connect(string strName, out SpringJoint item)
        {
            ExAddProperty(this.m_pSelf, strName, 4, 0x6590);
            item = null;
        }

        public void Connect(string strName, out TransportSurface item)
        {
            ExAddProperty(this.m_pSelf, strName, 4, 0xc350);
            item = null;
        }

        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_define_results_add_property", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        public static extern void ExAddProperty(IntPtr pItem, string strName, int nBaseType, int nContextType);
        public void Init(long pSelf)
        {
            this.m_pSelf = new IntPtr(pSelf);
        }
    }
}

