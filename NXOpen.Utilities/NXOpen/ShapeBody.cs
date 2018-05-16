namespace NXOpen
{
    using System;

    public class ShapeBody : RuntimeObject
    {
        protected ShapeBody(IntPtr pItem) : base(pItem)
        {
        }

        public virtual RigidBody GetOwner()
        {
            return null;
        }
    }
}

