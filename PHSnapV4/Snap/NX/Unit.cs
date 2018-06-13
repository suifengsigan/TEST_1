namespace Snap.NX
{
    using NXOpen;
    using System;

    public class Unit : Snap.NX.NXObject
    {
        public static readonly Snap.NX.Unit Degree = new Snap.NX.Unit(Compute.GetUnit("degrees"));
        public static readonly Snap.NX.Unit Inch = new Snap.NX.Unit(Compute.GetUnit("in"));
        public static readonly Snap.NX.Unit Millimeter = new Snap.NX.Unit(Compute.GetUnit("mm"));
        public static readonly Snap.NX.Unit Radian = new Snap.NX.Unit(Compute.GetUnit("radians"));

        internal Unit(NXOpen.Unit unit) : base(unit)
        {
            this.NXOpenUnit = unit;
        }

        public static implicit operator Snap.NX.Unit(NXOpen.Unit unit)
        {
            return new Snap.NX.Unit(unit);
        }

        public static implicit operator NXOpen.Unit(Snap.NX.Unit unit)
        {
            return unit.NXOpenUnit;
        }

        public static Snap.NX.Unit Wrap(Tag nxopenUnitTag)
        {
            if (nxopenUnitTag == Tag.Null)
            {
                throw new ArgumentException("Input tag is NXOpen.Tag.Null");
            }
            NXOpen.Unit objectFromTag = Snap.NX.NXObject.GetObjectFromTag(nxopenUnitTag) as NXOpen.Unit;
            if (objectFromTag == null)
            {
                throw new ArgumentException("Input tag doesn't belong to an NXOpen.Unit object");
            }
            return objectFromTag;
        }

        public string Abbreviation
        {
            get
            {
                return this.NXOpenUnit.Abbreviation;
            }
        }

        public string Name
        {
            get
            {
                return this.NXOpenUnit.Name;
            }
        }

        public NXOpen.Unit NXOpenUnit
        {
            get
            {
                return (NXOpen.Unit) base.NXOpenTaggedObject;
            }
            private set
            {
                base.NXOpenTaggedObject = value;
            }
        }
    }
}

