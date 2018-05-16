namespace Snap.NX
{
    using NXOpen;
    using NXOpen.Features;
    using Snap;
    using System;

    public class TrimBody : Snap.NX.Feature
    {
        internal TrimBody(TrimBody2 trim) : base(trim)
        {
            this.NXOpenTrimBody = trim;
        }

        internal static Snap.NX.TrimBody CreateTrimBody(Snap.NX.Body targetBody, Snap.NX.Body toolBody, bool direction)
        {
            NXOpen.Part workPart = (NXOpen.Part) Globals.WorkPart;
            TrimBody2Builder featureBuilder = workPart.Features.CreateTrimBody2Builder(null);
            featureBuilder.Tolerance = Globals.DistanceTolerance;
            featureBuilder.BooleanTool.ExtrudeRevolveTool.ToolSection.DistanceTolerance = Globals.DistanceTolerance;
            featureBuilder.BooleanTool.ExtrudeRevolveTool.ToolSection.ChainingTolerance = (Globals.UnitType == Globals.Unit.Millimeter) ? 0.02413 : 0.00095;
            ScCollector collector = workPart.ScCollectors.CreateCollector();
            NXOpen.Body[] bodies = new NXOpen.Body[] { targetBody };
            BodyDumbRule rule = workPart.ScRuleFactory.CreateRuleBodyDumb(bodies);
            SelectionIntentRule[] rules = new SelectionIntentRule[] { rule };
            collector.ReplaceRules(rules, false);
            featureBuilder.TargetBodyCollector = collector;
            SelectionIntentRule[] ruleArray2 = new SelectionIntentRule[] { workPart.ScRuleFactory.CreateRuleFaceBody((NXOpen.Body) toolBody) };
            featureBuilder.BooleanTool.FacePlaneTool.ToolFaces.FaceCollector.ReplaceRules(ruleArray2, false);
            featureBuilder.BooleanTool.ReverseDirection = direction;
            TrimBody2 trim = (TrimBody2) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.Destroy();
            return new Snap.NX.TrimBody(trim);
        }

        internal static Snap.NX.TrimBody CreateTrimBody(Snap.NX.Body targetBody, Snap.NX.DatumPlane toolDatumPlane, bool direction)
        {
            NXOpen.Part workPart = (NXOpen.Part) Globals.WorkPart;
            TrimBody2Builder featureBuilder = workPart.Features.CreateTrimBody2Builder(null);
            featureBuilder.Tolerance = Globals.DistanceTolerance;
            featureBuilder.BooleanTool.ExtrudeRevolveTool.ToolSection.DistanceTolerance = Globals.DistanceTolerance;
            featureBuilder.BooleanTool.ExtrudeRevolveTool.ToolSection.ChainingTolerance = (Globals.UnitType == Globals.Unit.Millimeter) ? 0.02413 : 0.00095;
            ScCollector collector = workPart.ScCollectors.CreateCollector();
            NXOpen.Body[] bodies = new NXOpen.Body[] { targetBody };
            BodyDumbRule rule = workPart.ScRuleFactory.CreateRuleBodyDumb(bodies);
            SelectionIntentRule[] rules = new SelectionIntentRule[] { rule };
            collector.ReplaceRules(rules, false);
            featureBuilder.TargetBodyCollector = collector;
            SelectionIntentRule[] ruleArray2 = new SelectionIntentRule[1];
            NXOpen.DatumPlane[] faces = new NXOpen.DatumPlane[] { toolDatumPlane.NXOpenDatumPlaneFeature.DatumPlane };
            ruleArray2[0] = workPart.ScRuleFactory.CreateRuleFaceDatum(faces);
            featureBuilder.BooleanTool.FacePlaneTool.ToolFaces.FaceCollector.ReplaceRules(ruleArray2, false);
            featureBuilder.BooleanTool.ReverseDirection = direction;
            TrimBody2 trim = (TrimBody2) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.Destroy();
            return new Snap.NX.TrimBody(trim);
        }

        internal static Snap.NX.TrimBody CreateTrimBody(Snap.NX.Body targetBody, Snap.NX.Face toolFace, bool direction)
        {
            NXOpen.Part workPart = (NXOpen.Part) Globals.WorkPart;
            TrimBody2Builder featureBuilder = workPart.Features.CreateTrimBody2Builder(null);
            featureBuilder.Tolerance = Globals.DistanceTolerance;
            featureBuilder.BooleanTool.ExtrudeRevolveTool.ToolSection.DistanceTolerance = Globals.DistanceTolerance;
            featureBuilder.BooleanTool.ExtrudeRevolveTool.ToolSection.ChainingTolerance = (Globals.UnitType == Globals.Unit.Millimeter) ? 0.02413 : 0.00095;
            ScCollector collector = workPart.ScCollectors.CreateCollector();
            NXOpen.Body[] bodies = new NXOpen.Body[] { targetBody };
            BodyDumbRule rule = workPart.ScRuleFactory.CreateRuleBodyDumb(bodies);
            SelectionIntentRule[] rules = new SelectionIntentRule[] { rule };
            collector.ReplaceRules(rules, false);
            featureBuilder.TargetBodyCollector = collector;
            SelectionIntentRule[] ruleArray2 = new SelectionIntentRule[] { workPart.ScRuleFactory.CreateRuleFaceBody((NXOpen.Body) toolFace.Body) };
            featureBuilder.BooleanTool.FacePlaneTool.ToolFaces.FaceCollector.ReplaceRules(ruleArray2, false);
            featureBuilder.BooleanTool.ReverseDirection = direction;
            TrimBody2 trim = (TrimBody2) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.Destroy();
            return new Snap.NX.TrimBody(trim);
        }

        public static implicit operator Snap.NX.TrimBody(TrimBody2 trim)
        {
            return new Snap.NX.TrimBody(trim);
        }

        public static implicit operator TrimBody2(Snap.NX.TrimBody trim)
        {
            return (TrimBody2) trim.NXOpenTaggedObject;
        }

        public static Snap.NX.TrimBody Wrap(Tag nxopenTrimBodyTag)
        {
            if (nxopenTrimBodyTag == Tag.Null)
            {
                throw new ArgumentException("Input tag is NXOpen.Tag.Null");
            }
            TrimBody2 objectFromTag = Snap.NX.NXObject.GetObjectFromTag(nxopenTrimBodyTag) as TrimBody2;
            if (objectFromTag == null)
            {
                throw new ArgumentException("Input tag doesn't belong to an NXOpen.Features.TrimBody2 object");
            }
            return objectFromTag;
        }

        public TrimBody2 NXOpenTrimBody
        {
            get
            {
                return (TrimBody2) base.NXOpenTaggedObject;
            }
            private set
            {
                base.NXOpenTaggedObject = value;
            }
        }

        public TrimBody2Builder TrimBodyBuilder
        {
            get
            {
                NXOpen.Part owningPart = (NXOpen.Part) this.NXOpenTrimBody.OwningPart;
                TrimBody2 body = (TrimBody2) this;
                return owningPart.Features.CreateTrimBody2Builder(body);
            }
        }
    }
}

