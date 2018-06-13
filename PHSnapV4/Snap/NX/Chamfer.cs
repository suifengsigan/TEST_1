namespace Snap.NX
{
    using NXOpen;
    using NXOpen.Features;
    using Snap;
    using System;

    public class Chamfer : Snap.NX.Feature
    {
        internal Chamfer(NXOpen.Features.Feature chamfer) : base(chamfer)
        {
            this.NXOpenChamfer = chamfer;
        }

        internal static Snap.NX.Chamfer CreateChamfer(Snap.NX.Edge edge, Snap.Number distance, Snap.Number angle)
        {
            NXOpen.Part workPart = (NXOpen.Part) Globals.WorkPart;
            NXOpen.Features.ChamferBuilder featureBuilder = workPart.Features.CreateChamferBuilder(null);
            featureBuilder.Option = NXOpen.Features.ChamferBuilder.ChamferOption.OffsetAndAngle;
            featureBuilder.FirstOffset = distance.ToString();
            featureBuilder.Angle = angle.ToString();
            //featureBuilder.Tolerance = Globals.DistanceTolerance;
            ScCollector collector = workPart.ScCollectors.CreateCollector();
            EdgeTangentRule rule = workPart.ScRuleFactory.CreateRuleEdgeTangent((NXOpen.Edge) edge, null, true, Globals.AngleTolerance, false, false);
            SelectionIntentRule[] rules = new SelectionIntentRule[] { rule };
            collector.ReplaceRules(rules, false);
            featureBuilder.SmartCollector = collector;
            featureBuilder.AllInstances = false;
            NXOpen.Features.Feature feature = (NXOpen.Features.Feature) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.Destroy();
            return (NXOpen.Features.Feature) feature;
        }

        internal static Snap.NX.Chamfer CreateChamfer(Snap.NX.Edge edge, Snap.Number distance, bool offsetFaces)
        {
            NXOpen.Part workPart = (NXOpen.Part) Globals.WorkPart;
            NXOpen.Features.ChamferBuilder featureBuilder = workPart.Features.CreateChamferBuilder(null);
            featureBuilder.Option = NXOpen.Features.ChamferBuilder.ChamferOption.SymmetricOffsets;
            if (offsetFaces)
            {
                featureBuilder.Method = NXOpen.Features.ChamferBuilder.OffsetMethod.FacesAndTrim;
            }
            else
            {
                featureBuilder.Method = NXOpen.Features.ChamferBuilder.OffsetMethod.EdgesAlongFaces;
            }
            featureBuilder.FirstOffset = distance.ToString();
            featureBuilder.SecondOffset = distance.ToString();
            //featureBuilder.Tolerance = Globals.DistanceTolerance;
            ScCollector collector = workPart.ScCollectors.CreateCollector();
            EdgeTangentRule rule = workPart.ScRuleFactory.CreateRuleEdgeTangent((NXOpen.Edge) edge, null, false, Globals.AngleTolerance, false, false);
            SelectionIntentRule[] rules = new SelectionIntentRule[] { rule };
            collector.ReplaceRules(rules, false);
            featureBuilder.SmartCollector = collector;
            NXOpen.Features.Feature feature = (NXOpen.Features.Feature) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.Destroy();
            return (NXOpen.Features.Feature) feature;
        }

        internal static Snap.NX.Chamfer CreateChamfer(Snap.NX.Edge edge, Snap.Number distance1, Snap.Number distance2, bool offsetFaces)
        {
            NXOpen.Part workPart = (NXOpen.Part) Globals.WorkPart;
            NXOpen.Features.ChamferBuilder featureBuilder = workPart.Features.CreateChamferBuilder(null);
            featureBuilder.Option = NXOpen.Features.ChamferBuilder.ChamferOption.TwoOffsets;
            if (offsetFaces)
            {
                featureBuilder.Method = NXOpen.Features.ChamferBuilder.OffsetMethod.FacesAndTrim;
            }
            else
            {
                featureBuilder.Method = NXOpen.Features.ChamferBuilder.OffsetMethod.EdgesAlongFaces;
            }
            featureBuilder.FirstOffset = distance1.ToString();
            featureBuilder.SecondOffset = distance2.ToString();
            //featureBuilder.Tolerance = Globals.DistanceTolerance;
            ScCollector collector = workPart.ScCollectors.CreateCollector();
            EdgeTangentRule rule = workPart.ScRuleFactory.CreateRuleEdgeTangent((NXOpen.Edge) edge, null, false, Globals.AngleTolerance, false, false);
            SelectionIntentRule[] rules = new SelectionIntentRule[] { rule };
            collector.ReplaceRules(rules, false);
            featureBuilder.SmartCollector = collector;
            NXOpen.Features.Feature feature = (NXOpen.Features.Feature) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.Destroy();
            return (NXOpen.Features.Feature) feature;
        }

        public static implicit operator Snap.NX.Chamfer(NXOpen.Features.Feature chamfer)
        {
            return new Snap.NX.Chamfer(chamfer);
        }

        public static implicit operator NXOpen.Features.Feature(Snap.NX.Chamfer chamfer)
        {
            return (NXOpen.Features.Feature) chamfer.NXOpenTaggedObject;
        }

        public static Snap.NX.Chamfer Wrap(Tag nxopenChamferTag)
        {
            if (nxopenChamferTag == Tag.Null)
            {
                throw new ArgumentException("Input tag is NXOpen.Tag.Null");
            }
            NXOpen.Features.Feature objectFromTag = Snap.NX.NXObject.GetObjectFromTag(nxopenChamferTag) as NXOpen.Features.Chamfer;
            if (objectFromTag == null)
            {
                throw new ArgumentException("Input tag doesn't belong to an NXOpen.Features.Chamfer object");
            }
            return objectFromTag;
        }

        public NXOpen.Features.ChamferBuilder ChamferBuilder
        {
            get
            {
                NXOpen.Part owningPart = (NXOpen.Part) this.NXOpenChamfer.OwningPart;
                return owningPart.Features.CreateChamferBuilder(this.NXOpenChamfer);
            }
        }

        public NXOpen.Features.Feature NXOpenChamfer
        {
            get
            {
                return (NXOpen.Features.Feature) base.NXOpenTaggedObject;
            }
            private set
            {
                base.NXOpenTaggedObject = value;
            }
        }

        public override DisplayableObject[] NXOpenDisplayableObjects
        {
            get
            {
                Tag[] tagArray;
                Globals.UFSession.Modl.AskFeatFaces(base.NXOpenTag, out tagArray);
                NXOpen.Face[] faceArray = new NXOpen.Face[tagArray.Length];
                for (int i = 0; i < faceArray.Length; i++)
                {
                    faceArray[i] = (NXOpen.Face) Globals.Session.GetObjectManager().GetTaggedObject(tagArray[i]);
                }
                return faceArray;
            }
        }
    }
}

