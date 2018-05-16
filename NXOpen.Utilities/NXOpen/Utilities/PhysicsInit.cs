namespace NXOpen.Utilities
{
    using NXOpen;
    using System;

    public class PhysicsInit
    {
        public static void Start()
        {
            RuntimeObject.Init();
            RigidBody.Init();
            CollisionBody.Init();
            CollisionSensor.Init();
            TransportSurface.Init();
            CollisionMaterial.Init();
            HingeJoint.Init();
            SlidingJoint.Init();
            CylindricalJoint.Init();
            FixedJoint.Init();
            BallJoint.Init();
            AngularLimit.Init();
            LinearLimit.Init();
            AngularSpring.Init();
            LinearSpring.Init();
            SpeedControl.Init();
            PositionControl.Init();
            BreakingConstraint.Init();
            GearCoupling.Init();
            CamCoupling.Init();
            ElecCamCoupling.Init();
            PreventCollision.Init();
            ChangeMaterial.Init();
            ComponentPart.Init();
            SourceBehavior.Init();
            SinkBehavior.Init();
            GraphControl.Init();
            ExternalConnection.Init();
            SignalAdapter.Init();
            Signal.Init();
            ProxyObject.Init();
            RuntimeParameters.Init();
        }

        public static void Stop()
        {
            RuntimeObject.ClearHash();
        }
    }
}

