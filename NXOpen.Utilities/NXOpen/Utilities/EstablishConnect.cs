namespace NXOpen.Utilities
{
    using NXOpen;
    using System;
    using System.Runtime.InteropServices;

    public class EstablishConnect : IDefinitionContext
    {
        private IntPtr m_pSelf;

        public void Connect(string strName, out AngularLimit item)
        {
            item = RuntimeObject.FromPtr(ExGetProperty(this.m_pSelf, strName, 4, 0x69dc)) as AngularLimit;
        }

        public void Connect(string strName, out AngularSpring item)
        {
            item = RuntimeObject.FromPtr(ExGetProperty(this.m_pSelf, strName, 4, 0x65f4)) as AngularSpring;
        }

        public void Connect(string strName, out AxisJoint item)
        {
            item = RuntimeObject.FromPtr(ExGetProperty(this.m_pSelf, strName, 4, 0x5208)) as AxisJoint;
        }

        public void Connect(string strName, out BallJoint item)
        {
            item = RuntimeObject.FromPtr(ExGetProperty(this.m_pSelf, strName, 4, 0x61a8)) as BallJoint;
        }

        public void Connect(string strName, out BreakingConstraint item)
        {
            item = RuntimeObject.FromPtr(ExGetProperty(this.m_pSelf, strName, 4, 0x84d0)) as BreakingConstraint;
        }

        public void Connect(string strName, out CamCoupling item)
        {
            item = RuntimeObject.FromPtr(ExGetProperty(this.m_pSelf, strName, 4, 0x81b0)) as CamCoupling;
        }

        public void Connect(string strName, out ChangeMaterial item)
        {
            item = RuntimeObject.FromPtr(ExGetProperty(this.m_pSelf, strName, 4, 0x88b8)) as ChangeMaterial;
        }

        public void Connect(string strName, out CollisionBody item)
        {
            item = RuntimeObject.FromPtr(ExGetProperty(this.m_pSelf, strName, 4, 0x2ee0)) as CollisionBody;
        }

        public void Connect(string strName, out CollisionMaterial item)
        {
            item = RuntimeObject.FromPtr(ExGetProperty(this.m_pSelf, strName, 4, 0x9c40)) as CollisionMaterial;
        }

        public void Connect(string strName, out CollisionSensor item)
        {
            item = RuntimeObject.FromPtr(ExGetProperty(this.m_pSelf, strName, 4, 0x32c8)) as CollisionSensor;
        }

        public void Connect(string strName, out ComponentPart item)
        {
            item = RuntimeObject.FromPtr(ExGetProperty(this.m_pSelf, strName, 4, 0xea60)) as ComponentPart;
        }

        public void Connect(string strName, out CylindricalJoint item)
        {
            item = RuntimeObject.FromPtr(ExGetProperty(this.m_pSelf, strName, 4, 0x5334)) as CylindricalJoint;
        }

        public void Connect(string strName, out ElecCamCoupling item)
        {
            item = RuntimeObject.FromPtr(ExGetProperty(this.m_pSelf, strName, 4, 0x8214)) as ElecCamCoupling;
        }

        public void Connect(string strName, out ExternalConnection item)
        {
            item = RuntimeObject.FromPtr(ExGetProperty(this.m_pSelf, strName, 4, 0x15f90)) as ExternalConnection;
        }

        public void Connect(string strName, out FixedJoint item)
        {
            item = RuntimeObject.FromPtr(ExGetProperty(this.m_pSelf, strName, 4, 0x5dc0)) as FixedJoint;
        }

        public void Connect(string strName, out GearCoupling item)
        {
            item = RuntimeObject.FromPtr(ExGetProperty(this.m_pSelf, strName, 4, 0x814c)) as GearCoupling;
        }

        public void Connect(string strName, out GraphControl item)
        {
            item = RuntimeObject.FromPtr(ExGetProperty(this.m_pSelf, strName, 4, 0x13c68)) as GraphControl;
        }

        public void Connect(string strName, out HingeJoint item)
        {
            item = RuntimeObject.FromPtr(ExGetProperty(this.m_pSelf, strName, 4, 0x526c)) as HingeJoint;
        }

        public void Connect(string strName, out Joint item)
        {
            item = RuntimeObject.FromPtr(ExGetProperty(this.m_pSelf, strName, 4, 0x4e20)) as Joint;
        }

        public void Connect(string strName, out LimitJoint item)
        {
            item = RuntimeObject.FromPtr(ExGetProperty(this.m_pSelf, strName, 4, 0x6978)) as LimitJoint;
        }

        public void Connect(string strName, out LinearLimit item)
        {
            item = RuntimeObject.FromPtr(ExGetProperty(this.m_pSelf, strName, 4, 0x6a40)) as LinearLimit;
        }

        public void Connect(string strName, out LinearSpring item)
        {
            item = RuntimeObject.FromPtr(ExGetProperty(this.m_pSelf, strName, 4, 0x6658)) as LinearSpring;
        }

        public void Connect(string strName, out PositionControl item)
        {
            item = RuntimeObject.FromPtr(ExGetProperty(this.m_pSelf, strName, 4, 0x7d00)) as PositionControl;
        }

        public void Connect(string strName, out PreventCollision item)
        {
            item = RuntimeObject.FromPtr(ExGetProperty(this.m_pSelf, strName, 4, 0x8ca0)) as PreventCollision;
        }

        public void Connect(string strName, out ProxyObject item)
        {
            item = RuntimeObject.FromPtr(ExGetProperty(this.m_pSelf, strName, 4, 0x1adb0)) as ProxyObject;
        }

        public void Connect(string strName, out RigidBody item)
        {
            item = RuntimeObject.FromPtr(ExGetProperty(this.m_pSelf, strName, 4, 0x2af8)) as RigidBody;
        }

        public void Connect(string strName, out RuntimeObject item)
        {
            item = RuntimeObject.FromPtr(ExGetProperty(this.m_pSelf, strName, 4, 0x3e8));
        }

        public void Connect(string strName, out RuntimeParameters item)
        {
            item = RuntimeObject.FromPtr(ExGetProperty(this.m_pSelf, strName, 4, 0x1b198)) as RuntimeParameters;
        }

        public void Connect(string strName, out Signal item)
        {
            item = RuntimeObject.FromPtr(ExGetProperty(this.m_pSelf, strName, 4, 0x18a88)) as Signal;
        }

        public void Connect(string strName, out SignalAdapter item)
        {
            item = RuntimeObject.FromPtr(ExGetProperty(this.m_pSelf, strName, 4, 0x186a0)) as SignalAdapter;
        }

        public void Connect(string strName, out SinkBehavior item)
        {
            item = RuntimeObject.FromPtr(ExGetProperty(this.m_pSelf, strName, 4, 0x11940)) as SinkBehavior;
        }

        public void Connect(string strName, out SlidingJoint item)
        {
            item = RuntimeObject.FromPtr(ExGetProperty(this.m_pSelf, strName, 4, 0x52d0)) as SlidingJoint;
        }

        public void Connect(string strName, out SourceBehavior item)
        {
            item = RuntimeObject.FromPtr(ExGetProperty(this.m_pSelf, strName, 4, 0x11558)) as SourceBehavior;
        }

        public void Connect(string strName, out SpeedControl item)
        {
            item = RuntimeObject.FromPtr(ExGetProperty(this.m_pSelf, strName, 4, 0x7918)) as SpeedControl;
        }

        public void Connect(string strName, out SpringJoint item)
        {
            item = RuntimeObject.FromPtr(ExGetProperty(this.m_pSelf, strName, 4, 0x6590)) as SpringJoint;
        }

        public void Connect(string strName, out TransportSurface item)
        {
            item = RuntimeObject.FromPtr(ExGetProperty(this.m_pSelf, strName, 4, 0xc350)) as TransportSurface;
        }

        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_class_props_get_property_obj", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        public static extern IntPtr ExGetProperty(IntPtr pItem, string strName, int nBaseType, int nContextType);
        public void Init(long pSelf)
        {
            this.m_pSelf = new IntPtr(pSelf);
        }
    }
}

