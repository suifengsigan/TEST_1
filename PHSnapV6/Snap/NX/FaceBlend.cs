namespace Snap.NX
{
    using NXOpen;
    using NXOpen.Features;
    using Snap;
    using System;

    public class FaceBlend : Snap.NX.Feature
    {
        internal FaceBlend(NXOpen.Features.FaceBlend FaceBlend) : base(FaceBlend)
        {
            this.NXOpenFaceBlend = FaceBlend;
        }

        internal static Snap.NX.FaceBlend CreateFaceBlend(Snap.NX.Face face1, Snap.NX.Face face2, Snap.Number radius)
        {
            NXOpen.Part workPart = (NXOpen.Part) Globals.WorkPart;
            NXOpen.Features.FaceBlendBuilder featureBuilder = workPart.Features.CreateFaceBlendBuilder(null);
            featureBuilder.Tolerance = Globals.DistanceTolerance;
            ScCollector collector = workPart.ScCollectors.CreateCollector();
            NXOpen.Face[] boundaryFaces = new NXOpen.Face[0];
            FaceTangentRule rule = workPart.ScRuleFactory.CreateRuleFaceTangent((NXOpen.Face) face1, boundaryFaces, 0.5);
            SelectionIntentRule[] rules = new SelectionIntentRule[] { rule };
            collector.ReplaceRules(rules, false);
            featureBuilder.FirstFaceCollector = collector;
            featureBuilder.ReverseFirstFaceNormal = true;
            ScCollector collector2 = workPart.ScCollectors.CreateCollector();
            NXOpen.Face[] faceArray2 = new NXOpen.Face[0];
            FaceTangentRule rule2 = workPart.ScRuleFactory.CreateRuleFaceTangent((NXOpen.Face) face2, faceArray2, 0.5);
            SelectionIntentRule[] ruleArray2 = new SelectionIntentRule[] { rule2 };
            collector2.ReplaceRules(ruleArray2, false);
            featureBuilder.SecondFaceCollector = collector2;
            featureBuilder.ReverseSecondFaceNormal = true;
            featureBuilder.CircularCrossSection.SetRadius(radius.ToString());
            NXOpen.Features.Feature feature = (NXOpen.Features.Feature) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.Destroy();
            return (NXOpen.Features.FaceBlend) feature;
        }

        public static implicit operator Snap.NX.FaceBlend(NXOpen.Features.FaceBlend faceBlend)
        {
            return new Snap.NX.FaceBlend(faceBlend);
        }

        public static implicit operator NXOpen.Features.FaceBlend(Snap.NX.FaceBlend faceBlend)
        {
            return (NXOpen.Features.FaceBlend) faceBlend.NXOpenTaggedObject;
        }

        public static Snap.NX.FaceBlend Wrap(Tag nxopenFaceBlendTag)
        {
            if (nxopenFaceBlendTag == Tag.Null)
            {
                throw new ArgumentException("Input tag is NXOpen.Tag.Null");
            }
            NXOpen.Features.FaceBlend objectFromTag = Snap.NX.NXObject.GetObjectFromTag(nxopenFaceBlendTag) as NXOpen.Features.FaceBlend;
            if (objectFromTag == null)
            {
                throw new ArgumentException("Input tag doesn't belong to an NXOpen.Features.FaceBlend object");
            }
            return objectFromTag;
        }

        public NXOpen.Features.FaceBlendBuilder FaceBlendBuilder
        {
            get
            {
                NXOpen.Part owningPart = (NXOpen.Part) this.NXOpenFaceBlend.OwningPart;
                return owningPart.Features.CreateFaceBlendBuilder(this.NXOpenFaceBlend);
            }
        }

        public NXOpen.Features.FaceBlend NXOpenFaceBlend
        {
            get
            {
                return (NXOpen.Features.FaceBlend) base.NXOpenTaggedObject;
            }
            private set
            {
                base.NXOpenTaggedObject = value;
            }
        }

        public double Radius
        {
            get
            {
                NXOpen.Part owningPart = (NXOpen.Part) this.NXOpenFaceBlend.OwningPart;
                NXOpen.Features.FaceBlendBuilder builder = Globals.WorkPart.NXOpenPart.Features.CreateFaceBlendBuilder(this.NXOpenFaceBlend);
                double num = builder.CircularCrossSection.Radius.Value;
                builder.Destroy();
                return num;
            }
        }
    }
}

