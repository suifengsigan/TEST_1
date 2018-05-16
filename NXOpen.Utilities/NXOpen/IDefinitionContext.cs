namespace NXOpen
{
    using System;
    using System.Runtime.InteropServices;

    public interface IDefinitionContext
    {
        void Connect(string strName, out AngularLimit item);
        void Connect(string strName, out AngularSpring item);
        void Connect(string strName, out AxisJoint item);
        void Connect(string strName, out BallJoint item);
        void Connect(string strName, out BreakingConstraint item);
        void Connect(string strName, out CamCoupling item);
        void Connect(string strName, out ChangeMaterial item);
        void Connect(string strName, out CollisionBody item);
        void Connect(string strName, out CollisionMaterial item);
        void Connect(string strName, out CollisionSensor item);
        void Connect(string strName, out ComponentPart item);
        void Connect(string strName, out CylindricalJoint item);
        void Connect(string strName, out ElecCamCoupling item);
        void Connect(string strName, out ExternalConnection item);
        void Connect(string strName, out FixedJoint item);
        void Connect(string strName, out GearCoupling item);
        void Connect(string strName, out GraphControl item);
        void Connect(string strName, out HingeJoint item);
        void Connect(string strName, out Joint item);
        void Connect(string strName, out LimitJoint item);
        void Connect(string strName, out LinearLimit item);
        void Connect(string strName, out LinearSpring item);
        void Connect(string strName, out PositionControl item);
        void Connect(string strName, out PreventCollision item);
        void Connect(string strName, out ProxyObject item);
        void Connect(string strName, out RigidBody item);
        void Connect(string strName, out RuntimeObject item);
        void Connect(string strName, out RuntimeParameters item);
        void Connect(string strName, out Signal item);
        void Connect(string strName, out SignalAdapter item);
        void Connect(string strName, out SinkBehavior item);
        void Connect(string strName, out SlidingJoint item);
        void Connect(string strName, out SourceBehavior item);
        void Connect(string strName, out SpeedControl item);
        void Connect(string strName, out SpringJoint item);
        void Connect(string strName, out TransportSurface item);
    }
}

