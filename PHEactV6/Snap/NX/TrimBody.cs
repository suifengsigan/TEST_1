namespace Snap.NX
{
    using NXOpen;
    using NXOpen.Features;
    using Snap;
    using System;

    public class TrimBody : Snap.NX.Feature
    {
        internal TrimBody(NXOpen.Features.TrimBody trim) : base(trim)
        {
            this.NXOpenTrimBody = trim;
        }

        internal static Snap.NX.TrimBody CreateTrimBody(Snap.NX.Body targetBody, Snap.NX.Body toolBody, bool direction)
        {
            NXOpen.Part workPart = (NXOpen.Part) Globals.WorkPart;
            TrimBodyBuilder featureBuilder = workPart.Features.CreateTrimBodyBuilder(null);
            //featureBuilder.Tolerance = Globals.DistanceTolerance;
            //featureBuilder.BooleanTool.ExtrudeRevolveTool.ToolSection.DistanceTolerance = Globals.DistanceTolerance;
            //featureBuilder.BooleanTool.ExtrudeRevolveTool.ToolSection.ChainingTolerance = (Globals.UnitType == Globals.Unit.Millimeter) ? 0.02413 : 0.00095;
            //ScCollector collector = workPart.ScCollectors.CreateCollector();
            //NXOpen.Body[] bodies = new NXOpen.Body[] { targetBody };
            //BodyDumbRule rule = workPart.ScRuleFactory.CreateRuleBodyDumb(bodies);
            //SelectionIntentRule[] rules = new SelectionIntentRule[] { rule };
            //collector.ReplaceRules(rules, false);
            //featureBuilder.TargetBodyCollector = collector;
            featureBuilder.SetTargets(new NXOpen.Body[] { targetBody });
            featureBuilder.Tool = toolBody;
            featureBuilder.TrimDirection = direction ? TrimBodyBuilder.DirectionType.PositiveNormal : TrimBodyBuilder.DirectionType.NegativeNormal;
            //SelectionIntentRule[] ruleArray2 = new SelectionIntentRule[] { workPart.ScRuleFactory.CreateRuleFaceBody((NXOpen.Body) toolBody) };
            //featureBuilder.BooleanTool.FacePlaneTool.ToolFaces.FaceCollector.ReplaceRules(ruleArray2, false);
            //featureBuilder.BooleanTool.ReverseDirection = direction;
            NXOpen.Features.TrimBody trim = (NXOpen.Features.TrimBody)Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.Destroy();
            return new Snap.NX.TrimBody(trim);
        }

        internal static Snap.NX.TrimBody CreateTrimBody(Snap.NX.Body targetBody, Snap.NX.DatumPlane toolDatumPlane, bool direction)
        {
            NXOpen.Part workPart = (NXOpen.Part) Globals.WorkPart;
            TrimBodyBuilder featureBuilder = workPart.Features.CreateTrimBodyBuilder(null);
            //featureBuilder.Tolerance = Globals.DistanceTolerance;
            //featureBuilder.BooleanTool.ExtrudeRevolveTool.ToolSection.DistanceTolerance = Globals.DistanceTolerance;
            //featureBuilder.BooleanTool.ExtrudeRevolveTool.ToolSection.ChainingTolerance = (Globals.UnitType == Globals.Unit.Millimeter) ? 0.02413 : 0.00095;
            //ScCollector collector = workPart.ScCollectors.CreateCollector();
            //NXOpen.Body[] bodies = new NXOpen.Body[] { targetBody };
            //BodyDumbRule rule = workPart.ScRuleFactory.CreateRuleBodyDumb(bodies);
            //SelectionIntentRule[] rules = new SelectionIntentRule[] { rule };
            //collector.ReplaceRules(rules, false);
            //featureBuilder.TargetBodyCollector = collector;
            featureBuilder.SetTargets(new NXOpen.Body[] { targetBody });
            featureBuilder.Tool = toolDatumPlane.NXOpenDisplayableObject;
            featureBuilder.TrimDirection = direction ? TrimBodyBuilder.DirectionType.PositiveNormal : TrimBodyBuilder.DirectionType.NegativeNormal;
            //SelectionIntentRule[] ruleArray2 = new SelectionIntentRule[1];
            //NXOpen.DatumPlane[] faces = new NXOpen.DatumPlane[] { toolDatumPlane.NXOpenDatumPlaneFeature.DatumPlane };
            //ruleArray2[0] = workPart.ScRuleFactory.CreateRuleFaceDatum(faces);
            //featureBuilder.BooleanTool.FacePlaneTool.ToolFaces.FaceCollector.ReplaceRules(ruleArray2, false);
            //featureBuilder.BooleanTool.ReverseDirection = direction;
            NXOpen.Features.TrimBody trim = (NXOpen.Features.TrimBody)Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.Destroy();
            return new Snap.NX.TrimBody(trim);
        }

        internal static Snap.NX.TrimBody CreateTrimBody(Snap.NX.Body targetBody, Snap.NX.Face toolFace, bool direction)
        {
            NXOpen.Part workPart = (NXOpen.Part) Globals.WorkPart;
            TrimBodyBuilder featureBuilder = workPart.Features.CreateTrimBodyBuilder(null);
            //featureBuilder.Tolerance = Globals.DistanceTolerance;
            //featureBuilder.BooleanTool.ExtrudeRevolveTool.ToolSection.DistanceTolerance = Globals.DistanceTolerance;
            //featureBuilder.BooleanTool.ExtrudeRevolveTool.ToolSection.ChainingTolerance = (Globals.UnitType == Globals.Unit.Millimeter) ? 0.02413 : 0.00095;
            //ScCollector collector = workPart.ScCollectors.CreateCollector();
            //NXOpen.Body[] bodies = new NXOpen.Body[] { targetBody };
            //BodyDumbRule rule = workPart.ScRuleFactory.CreateRuleBodyDumb(bodies);
            //SelectionIntentRule[] rules = new SelectionIntentRule[] { rule };
            //collector.ReplaceRules(rules, false);
            //featureBuilder.TargetBodyCollector = collector;
            //SelectionIntentRule[] ruleArray2 = new SelectionIntentRule[] { workPart.ScRuleFactory.CreateRuleFaceBody((NXOpen.Body) toolFace.Body) };
            //featureBuilder.BooleanTool.FacePlaneTool.ToolFaces.FaceCollector.ReplaceRules(ruleArray2, false);
            //featureBuilder.BooleanTool.ReverseDirection = direction;
            featureBuilder.SetTargets(new NXOpen.Body[] { targetBody });
            featureBuilder.Tool = toolFace;
            featureBuilder.TrimDirection=direction?TrimBodyBuilder.DirectionType.PositiveNormal:TrimBodyBuilder.DirectionType.NegativeNormal;
            NXOpen.Features.TrimBody trim = (NXOpen.Features.TrimBody)Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.Destroy();
            return new Snap.NX.TrimBody(trim);
        }

        public static implicit operator Snap.NX.TrimBody(NXOpen.Features.TrimBody trim)
        {
            return new Snap.NX.TrimBody(trim);
        }

        public static implicit operator NXOpen.Features.TrimBody(Snap.NX.TrimBody trim)
        {
            return (NXOpen.Features.TrimBody)trim.NXOpenTaggedObject;
        }

        public static Snap.NX.TrimBody Wrap(Tag nxopenTrimBodyTag)
        {
            if (nxopenTrimBodyTag == Tag.Null)
            {
                throw new ArgumentException("Input tag is NXOpen.Tag.Null");
            }
            NXOpen.Features.TrimBody objectFromTag = Snap.NX.NXObject.GetObjectFromTag(nxopenTrimBodyTag) as NXOpen.Features.TrimBody;
            if (objectFromTag == null)
            {
                throw new ArgumentException("Input tag doesn't belong to an NXOpen.Features.TrimBody2 object");
            }
            return objectFromTag;
        }

        public NXOpen.Features.TrimBody NXOpenTrimBody
        {
            get
            {
                return (NXOpen.Features.TrimBody)base.NXOpenTaggedObject;
            }
            private set
            {
                base.NXOpenTaggedObject = value;
            }
        }

        public TrimBodyBuilder TrimBodyBuilder
        {
            get
            {
                NXOpen.Part owningPart = (NXOpen.Part) this.NXOpenTrimBody.OwningPart;
                NXOpen.Features.TrimBody body = (NXOpen.Features.TrimBody)this;
                return owningPart.Features.CreateTrimBodyBuilder(body);
            }
        }
    }
}

