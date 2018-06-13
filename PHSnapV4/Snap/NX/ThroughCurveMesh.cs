namespace Snap.NX
{
    using NXOpen;
    using NXOpen.Features;
    using Snap;
    using System;

    public class ThroughCurveMesh : Snap.NX.Feature
    {
        internal ThroughCurveMesh(NXOpen.Features.ThroughCurveMesh throughCurveMesh) : base(throughCurveMesh)
        {
            this.NXOpenThroughCurveMesh = throughCurveMesh;
        }

        internal static NXOpen.Features.ThroughCurveMesh CreateThroughCurveMesh(Snap.NX.ICurve[] primaryCurves, Snap.NX.ICurve[] crossCurves)
        {
            Snap.NX.Section section;
            if ((primaryCurves.Length == 0) || (crossCurves.Length == 0))
            {
                return null;
            }
            NXOpen.Features.ThroughCurveMeshBuilder featureBuilder = Globals.WorkPart.NXOpenPart.Features.CreateThroughCurveMeshBuilder(null);
            featureBuilder.PositionTolerance = (Globals.UnitType == Globals.Unit.Millimeter) ? 0.0254 : 0.001;
            featureBuilder.TangentTolerance = 0.5;
            featureBuilder.IntersectionTolerance = (Globals.UnitType == Globals.Unit.Millimeter) ? 0.0254 : 0.001;
            featureBuilder.Spine.DistanceTolerance = Globals.DistanceTolerance;
            featureBuilder.Spine.ChainingTolerance = (Globals.UnitType == Globals.Unit.Millimeter) ? 0.02413 : 0.00095;
            featureBuilder.PrimaryTemplateString.DistanceTolerance = Globals.DistanceTolerance;
            featureBuilder.PrimaryTemplateString.ChainingTolerance = (Globals.UnitType == Globals.Unit.Millimeter) ? 0.02413 : 0.00095;
            featureBuilder.CrossTemplateString.DistanceTolerance = Globals.DistanceTolerance;
            featureBuilder.CrossTemplateString.ChainingTolerance = (Globals.UnitType == Globals.Unit.Millimeter) ? 0.02413 : 0.00095;
            featureBuilder.Spine.AngleTolerance = Globals.AngleTolerance;
            featureBuilder.PrimaryTemplateString.AngleTolerance = Globals.AngleTolerance;
            featureBuilder.CrossTemplateString.AngleTolerance = Globals.AngleTolerance;
            for (int i = 0; i < primaryCurves.Length; i++)
            {
                section = Snap.NX.Section.CreateSection(new Snap.NX.ICurve[] { primaryCurves[i] });
                featureBuilder.PrimaryCurvesList.Append((NXOpen.Section) section);
            }
            for (int j = 0; j < crossCurves.Length; j++)
            {
                section = Snap.NX.Section.CreateSection(new Snap.NX.ICurve[] { crossCurves[j] });
                featureBuilder.CrossCurvesList.Append((NXOpen.Section) section);
            }
            Snap.NX.ThroughCurveMesh mesh = (NXOpen.Features.ThroughCurveMesh) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.Destroy();
            return (NXOpen.Features.ThroughCurveMesh) mesh;
        }

        public static implicit operator Snap.NX.ThroughCurveMesh(NXOpen.Features.ThroughCurveMesh throughCurveMesh)
        {
            return new Snap.NX.ThroughCurveMesh(throughCurveMesh);
        }

        public static implicit operator NXOpen.Features.ThroughCurveMesh(Snap.NX.ThroughCurveMesh throughCurveMesh)
        {
            return throughCurveMesh.NXOpenThroughCurveMesh;
        }

        public static Snap.NX.ThroughCurveMesh Wrap(Tag nxopenThroughCurveMeshTag)
        {
            if (nxopenThroughCurveMeshTag == Tag.Null)
            {
                throw new ArgumentException("Input tag is NXOpen.Tag.Null");
            }
            NXOpen.Features.ThroughCurveMesh objectFromTag = Snap.NX.NXObject.GetObjectFromTag(nxopenThroughCurveMeshTag) as NXOpen.Features.ThroughCurveMesh;
            if (objectFromTag == null)
            {
                throw new ArgumentException("Input tag doesn't belong to an NXOpen.Features.ThroughCurveMesh object");
            }
            return objectFromTag;
        }

        public NXOpen.Features.ThroughCurveMesh NXOpenThroughCurveMesh
        {
            get
            {
                return (NXOpen.Features.ThroughCurveMesh) base.NXOpenTaggedObject;
            }
            private set
            {
                base.NXOpenTaggedObject = value;
            }
        }

        public NXOpen.Features.ThroughCurveMeshBuilder ThroughCurveMeshBuilder
        {
            get
            {
                NXOpen.Part owningPart = (NXOpen.Part) this.NXOpenThroughCurveMesh.OwningPart;
                NXOpen.Features.ThroughCurveMesh throughCurveMesh = (NXOpen.Features.ThroughCurveMesh) this;
                return owningPart.Features.CreateThroughCurveMeshBuilder(throughCurveMesh);
            }
        }
    }
}

