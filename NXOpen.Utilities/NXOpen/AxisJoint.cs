namespace NXOpen
{
    using NXOpen.VectorArithmetic;
    using System;

    public class AxisJoint : Joint
    {
        protected AxisJoint(IntPtr pItem) : base(pItem)
        {
        }

        public virtual Vector3 Axis
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

