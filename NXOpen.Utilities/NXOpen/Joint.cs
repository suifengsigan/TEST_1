namespace NXOpen
{
    using NXOpen.Utilities;
    using NXOpen.VectorArithmetic;
    using System;

    public class Joint : RuntimeObject
    {
        protected Joint(IntPtr pItem) : base(pItem)
        {
        }

        public Vector3 GetForce(double stepSize)
        {
            double x = 0.0;
            double y = 0.0;
            x = InstanceFunc.ExGetForce(base.m_pItem, stepSize, 1);
            y = InstanceFunc.ExGetForce(base.m_pItem, stepSize, 2);
            return new Vector3(x, y, InstanceFunc.ExGetForce(base.m_pItem, stepSize, 3));
        }

        public Vector3 GetTorque(double stepSize)
        {
            double x = 0.0;
            double y = 0.0;
            x = InstanceFunc.ExGetTorque(base.m_pItem, stepSize, 1);
            y = InstanceFunc.ExGetTorque(base.m_pItem, stepSize, 2);
            return new Vector3(x, y, InstanceFunc.ExGetTorque(base.m_pItem, stepSize, 3));
        }

        public virtual RigidBody Attach
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        public virtual RigidBody Base
        {
            get
            {
                return null;
            }
            set
            {
            }
        }
    }
}

