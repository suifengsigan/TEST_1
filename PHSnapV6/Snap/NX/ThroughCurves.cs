namespace Snap.NX
{
    using NXOpen;
    using NXOpen.Features;
    using Snap;
    using System;
    using System.Collections.Generic;

    public class ThroughCurves : Snap.NX.Feature
    {
        internal ThroughCurves(NXOpen.Features.ThroughCurves throughCurves) : base(throughCurves)
        {
            this.NXOpenThroughCurves = throughCurves;
        }

        internal static Snap.NX.ThroughCurves CreateThroughCurves(Snap.NX.ICurve[] icurves)
        {
            Snap.NX.Section[] sectionArray = new Snap.NX.Section[icurves.Length];
            for (int i = 0; i < icurves.Length; i++)
            {
                sectionArray[i] = Snap.NX.Section.CreateSection(new Snap.NX.ICurve[] { icurves[i] });
            }
            if (sectionArray.Length == 0)
            {
                return null;
            }
            NXOpen.Features.ThroughCurvesBuilder featureBuilder = Globals.NXOpenWorkPart.Features.CreateThroughCurvesBuilder(null);
            featureBuilder.Alignment.AlignCurve.DistanceTolerance = Globals.DistanceTolerance;
            featureBuilder.Alignment.AlignCurve.ChainingTolerance = (Globals.UnitType == Globals.Unit.Millimeter) ? 0.02413 : 0.00095;
            featureBuilder.SectionTemplateString.DistanceTolerance = Globals.DistanceTolerance;
            featureBuilder.SectionTemplateString.ChainingTolerance = (Globals.UnitType == Globals.Unit.Millimeter) ? 0.02413 : 0.00095;
            featureBuilder.Alignment.AlignCurve.AngleTolerance = Globals.AngleTolerance;
            featureBuilder.SectionTemplateString.AngleTolerance = Globals.AngleTolerance;
            NXOpen.Section[] sections = new NXOpen.Section[sectionArray.Length];
            for (int j = 0; j < sectionArray.Length; j++)
            {
                sections[j] = (NXOpen.Section) sectionArray[j];
                featureBuilder.SectionsList.Append(sections[j]);
            }
            featureBuilder.Alignment.SetSections(sections);
            Snap.NX.ThroughCurves curves = (NXOpen.Features.ThroughCurves) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.Destroy();
            return curves;
        }

        public static implicit operator Snap.NX.ThroughCurves(NXOpen.Features.ThroughCurves throughCurves)
        {
            return new Snap.NX.ThroughCurves(throughCurves);
        }

        public static implicit operator NXOpen.Features.ThroughCurves(Snap.NX.ThroughCurves throughCurves)
        {
            return throughCurves.NXOpenThroughCurves;
        }

        public static Snap.NX.ThroughCurves Wrap(Tag nxopenThroughCurvesTag)
        {
            if (nxopenThroughCurvesTag == Tag.Null)
            {
                throw new ArgumentException("Input tag is NXOpen.Tag.Null");
            }
            NXOpen.Features.ThroughCurves objectFromTag = Snap.NX.NXObject.GetObjectFromTag(nxopenThroughCurvesTag) as NXOpen.Features.ThroughCurves;
            if (objectFromTag == null)
            {
                throw new ArgumentException("Input tag doesn't belong to an NXOpen.Features.ThroughCurves object");
            }
            return objectFromTag;
        }

        public Snap.NX.ICurve[] ICurves
        {
            get
            {
                List<Snap.NX.ICurve> list = new List<Snap.NX.ICurve>();
                NXOpen.Section[] sections = this.NXOpenThroughCurves.GetSections();
                for (int i = 0; i < sections.Length; i++)
                {
                    Snap.NX.Section section = sections[i];
                    foreach (Snap.NX.ICurve curve in section.GetIcurves())
                    {
                        list.Add(curve);
                    }
                }
                return list.ToArray();
            }
        }

        public NXOpen.Features.ThroughCurves NXOpenThroughCurves
        {
            get
            {
                return (NXOpen.Features.ThroughCurves) base.NXOpenTaggedObject;
            }
            private set
            {
                base.NXOpenTaggedObject = value;
            }
        }

        public NXOpen.Features.ThroughCurvesBuilder ThroughCurvesBuilder
        {
            get
            {
                NXOpen.Part owningPart = (NXOpen.Part) this.NXOpenThroughCurves.OwningPart;
                NXOpen.Features.ThroughCurves throughCurves = (NXOpen.Features.ThroughCurves) this;
                return owningPart.Features.CreateThroughCurvesBuilder(throughCurves);
            }
        }
    }
}

