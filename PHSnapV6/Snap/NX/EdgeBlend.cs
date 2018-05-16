namespace Snap.NX
{
    using NXOpen;
    using NXOpen.Features;
    using NXOpen.GeometricUtilities;
    using Snap;
    using System;

    public class EdgeBlend : Snap.NX.Feature
    {
        internal EdgeBlend(NXOpen.Features.EdgeBlend edgeBlend) : base(edgeBlend)
        {
            this.NXOpenEdgeBlend = edgeBlend;
        }

        internal static Snap.NX.EdgeBlend CreateEdgeBlend(Snap.Number radius, Snap.NX.Edge[] edges)
        {
            NXOpen.Part workPart = (NXOpen.Part) Globals.WorkPart;
            NXOpen.Features.EdgeBlendBuilder featureBuilder = workPart.Features.CreateEdgeBlendBuilder(null);
            BlendLimitsData limitsListData = featureBuilder.LimitsListData;
            ScCollector collector = workPart.ScCollectors.CreateCollector();
            NXOpen.Edge[] seedEdges = new NXOpen.Edge[edges.Length];
            for (int i = 0; i < seedEdges.Length; i++)
            {
                seedEdges[i] = (NXOpen.Edge) edges[i];
            }
            EdgeMultipleSeedTangentRule rule = workPart.ScRuleFactory.CreateRuleEdgeMultipleSeedTangent(seedEdges, Globals.AngleTolerance, true);
            SelectionIntentRule[] rules = new SelectionIntentRule[] { rule };
            collector.ReplaceRules(rules, false);
            featureBuilder.Tolerance = Globals.DistanceTolerance;
            featureBuilder.AllInstancesOption = false;
            featureBuilder.RemoveSelfIntersection = true;
            featureBuilder.ConvexConcaveY = false;
            featureBuilder.RollOverSmoothEdge = true;
            featureBuilder.RollOntoEdge = true;
            featureBuilder.MoveSharpEdge = true;
            featureBuilder.TrimmingOption = false;
            featureBuilder.OverlapOption = NXOpen.Features.EdgeBlendBuilder.Overlap.AnyConvexityRollOver;
            featureBuilder.BlendOrder = NXOpen.Features.EdgeBlendBuilder.OrderOfBlending.ConvexFirst;
            featureBuilder.SetbackOption = NXOpen.Features.EdgeBlendBuilder.Setback.SeparateFromCorner;
            featureBuilder.AddChainset(collector, radius.ToString());
            NXOpen.Features.Feature feature = (NXOpen.Features.Feature) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.Destroy();
            return (NXOpen.Features.EdgeBlend) feature;
        }

        public static implicit operator Snap.NX.EdgeBlend(NXOpen.Features.EdgeBlend edgeBlend)
        {
            return new Snap.NX.EdgeBlend(edgeBlend);
        }

        public static implicit operator NXOpen.Features.EdgeBlend(Snap.NX.EdgeBlend edgeBlend)
        {
            return (NXOpen.Features.EdgeBlend) edgeBlend.NXOpenTaggedObject;
        }

        public static Snap.NX.EdgeBlend Wrap(Tag nxopenEdgeBlendTag)
        {
            if (nxopenEdgeBlendTag == Tag.Null)
            {
                throw new ArgumentException("Input tag is NXOpen.Tag.Null");
            }
            NXOpen.Features.EdgeBlend objectFromTag = Snap.NX.NXObject.GetObjectFromTag(nxopenEdgeBlendTag) as NXOpen.Features.EdgeBlend;
            if (objectFromTag == null)
            {
                throw new ArgumentException("Input tag doesn't belong to an NXOpen.Features.EdgeBlend object");
            }
            return objectFromTag;
        }

        public NXOpen.Features.EdgeBlendBuilder EdgeBlendBuilder
        {
            get
            {
                NXOpen.Part owningPart = (NXOpen.Part) this.NXOpenEdgeBlend.OwningPart;
                return owningPart.Features.CreateEdgeBlendBuilder(this.NXOpenEdgeBlend);
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

        public NXOpen.Features.EdgeBlend NXOpenEdgeBlend
        {
            get
            {
                return (NXOpen.Features.EdgeBlend) base.NXOpenTaggedObject;
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
                NXOpen.Expression expression;
                ScCollector collector;
                NXOpen.Features.EdgeBlendBuilder edgeBlendBuilder = this.EdgeBlendBuilder;
                edgeBlendBuilder.GetChainset(0, out collector, out expression);
                double num = expression.Value;
                edgeBlendBuilder.Destroy();
                return num;
            }
        }
    }
}

