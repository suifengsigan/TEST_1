namespace NXOpen
{
    using NXOpen.VectorArithmetic;
    using System;

    public class SpringJoint : Joint
    {
        protected SpringJoint(IntPtr pItem) : base(pItem)
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

        public virtual double Damping
        {
            get
            {
                return 0.0;
            }
            set
            {
            }
        }

        public virtual double RelaxedPosition
        {
            get
            {
                return 0.0;
            }
            set
            {
            }
        }

        public virtual double SpringConstant
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

