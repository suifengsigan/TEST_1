namespace Snap.NX
{
    using NXOpen;
    using NXOpen.Features;
    using Snap;
    using System;

    public class Ruled : Snap.NX.Feature
    {
        internal Ruled(NXOpen.Features.Ruled ruled) : base(ruled)
        {
            this.NXOpenRuled = ruled;
        }

        internal static Snap.NX.Ruled CreateRuled(Snap.NX.Curve curve0, Snap.NX.Curve curve1)
        {
            NXOpen.Features.RuledBuilder featureBuilder = Globals.WorkPart.NXOpenPart.Features.CreateRuledBuilder(null);
            featureBuilder.PositionTolerance = Globals.DistanceTolerance;
            featureBuilder.FirstSection.DistanceTolerance = Globals.DistanceTolerance;
            featureBuilder.FirstSection.ChainingTolerance = (Globals.UnitType == Globals.Unit.Millimeter) ? 0.02413 : 0.00095;
            featureBuilder.SecondSection.DistanceTolerance = Globals.DistanceTolerance;
            featureBuilder.SecondSection.ChainingTolerance = (Globals.UnitType == Globals.Unit.Millimeter) ? 0.02413 : 0.00095;
            featureBuilder.AlignmentMethod.AlignCurve.DistanceTolerance = Globals.DistanceTolerance;
            featureBuilder.AlignmentMethod.AlignCurve.ChainingTolerance = (Globals.UnitType == Globals.Unit.Millimeter) ? 0.02413 : 0.00095;
            ((Snap.NX.Section)featureBuilder.FirstSection).AddICurve(new Snap.NX.ICurve[] { curve0 });
            ((Snap.NX.Section)featureBuilder.SecondSection).AddICurve(new Snap.NX.ICurve[] { curve1 });
            NXOpen.Section[] sections = new NXOpen.Section[] { featureBuilder.FirstSection, featureBuilder.SecondSection };
            featureBuilder.AlignmentMethod.SetSections(sections);
            NXOpen.Features.Ruled ruled = (NXOpen.Features.Ruled) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.Destroy();
            return new Snap.NX.Ruled(ruled);
        }

        public static implicit operator Snap.NX.Ruled(NXOpen.Features.Ruled ruled)
        {
            return new Snap.NX.Ruled(ruled);
        }

        public static implicit operator NXOpen.Features.Ruled(Snap.NX.Ruled ruled)
        {
            return ruled.NXOpenRuled;
        }

        public static Snap.NX.Ruled Wrap(Tag nxopenRuledTag)
        {
            if (nxopenRuledTag == Tag.Null)
            {
                throw new ArgumentException("Input tag is NXOpen.Tag.Null");
            }
            NXOpen.Features.Ruled objectFromTag = Snap.NX.NXObject.GetObjectFromTag(nxopenRuledTag) as NXOpen.Features.Ruled;
            if (objectFromTag == null)
            {
                throw new ArgumentException("Input tag doesn't belong to an NXOpen.Features.Ruled object");
            }
            return objectFromTag;
        }

        public Snap.NX.Curve FirstCurve
        {
            get
            {
                this.NXOpenRuled.GetSections();
                Snap.NX.ICurve[] icurves = ((Snap.NX.Section)this.NXOpenRuled.GetSections()[0]).GetIcurves();
                if (icurves == null)
                {
                    return null;
                }
                return (Snap.NX.Curve) icurves[0];
            }
        }

        public NXOpen.Features.Ruled NXOpenRuled
        {
            get
            {
                return (NXOpen.Features.Ruled) base.NXOpenTaggedObject;
            }
            private set
            {
                base.NXOpenTaggedObject = value;
            }
        }

        public NXOpen.Features.RuledBuilder RuledBuilder
        {
            get
            {
                NXOpen.Part owningPart = (NXOpen.Part) this.NXOpenRuled.OwningPart;
                return owningPart.Features.CreateRuledBuilder(this.NXOpenRuled);
            }
        }

        public Snap.NX.Curve SecondCurve
        {
            get
            {
                this.NXOpenRuled.GetSections();
                Snap.NX.ICurve[] icurves = ((Snap.NX.Section)(this.NXOpenRuled.GetSections()[1])).GetIcurves();
                if (icurves == null)
                {
                    return null;
                }
                return (Snap.NX.Curve) icurves[0];
            }
        }
    }
}

