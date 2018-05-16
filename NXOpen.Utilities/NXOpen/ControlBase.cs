namespace NXOpen
{
    using NXOpen.Utilities;
    using NXOpen.VectorArithmetic;
    using System;

    public class ControlBase : RuntimeObject
    {
        protected ControlBase(IntPtr pItem) : base(pItem)
        {
        }

        public Vector3 GetForce(double stepSize)
        {
            double x = 0.0;
            double y = 0.0;
            x = InstanceFunc.ExGetControlForce(base.m_pItem, stepSize, 1);
            y = InstanceFunc.ExGetControlForce(base.m_pItem, stepSize, 2);
            return new Vector3(x, y, InstanceFunc.ExGetControlForce(base.m_pItem, stepSize, 3));
        }

        public Vector3 GetTorque(double stepSize)
        {
            double x = 0.0;
            double y = 0.0;
            x = InstanceFunc.ExGetControlTorque(base.m_pItem, stepSize, 1);
            y = InstanceFunc.ExGetControlTorque(base.m_pItem, stepSize, 2);
            return new Vector3(x, y, InstanceFunc.ExGetControlTorque(base.m_pItem, stepSize, 3));
        }
    }
}

