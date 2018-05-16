namespace NXOpen
{
    using NXOpen.VectorArithmetic;
    using System;

    public class LimitJoint : Joint
    {
        protected LimitJoint(IntPtr pItem) : base(pItem)
        {
        }

        public virtual Vector3 AttachVector
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        public virtual Vector3 BaseVector
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        public virtual double Maximum
        {
            get
            {
                return 0.0;
            }
            set
            {
            }
        }

        public virtual double Minimum
        {
            get
            {
                return 0.0;
            }
            set
            {
            }
        }
    }
}

