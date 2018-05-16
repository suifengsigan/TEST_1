namespace Snap.NX
{
    using NXOpen;
    using NXOpen.Features;
    using Snap;
    using System;

    public class SplitBody : Snap.NX.Feature
    {
        internal SplitBody(NXOpen.Features.SplitBody split) : base(split)
        {
            this.NXOpenSplitBody = split;
        }

        internal static Snap.NX.SplitBody CreateSplitBody(Snap.NX.Body targetBody, Snap.NX.Body[] toolBodies)
        {
            NXOpen.Part workPart = (NXOpen.Part) Globals.WorkPart;
            NXOpen.Features.SplitBodyBuilder featureBuilder = workPart.Features.CreateSplitBodyBuilder(null);
            featureBuilder.BooleanTool.ExtrudeRevolveTool.ToolSection.PrepareMappingData();
            featureBuilder.BooleanTool.ExtrudeRevolveTool.ToolSection.DistanceTolerance = Globals.DistanceTolerance;
            featureBuilder.BooleanTool.ExtrudeRevolveTool.ToolSection.ChainingTolerance = (Globals.UnitType == Globals.Unit.Millimeter) ? 0.02413 : 0.00095;
            //ScCollector collector = workPart.ScCollectors.CreateCollector();
            //NXOpen.Body[] bodies = new NXOpen.Body[] { targetBody };
            //BodyDumbRule rule = workPart.ScRuleFactory.CreateRuleBodyDumb(bodies);
            //SelectionIntentRule[] rules = new SelectionIntentRule[] { rule };
            //collector.ReplaceRules(rules, false);
            //featureBuilder.TargetBodyCollector = collector;
            featureBuilder.TargetBody.Add(targetBody);
            SelectionIntentRule[] ruleArray2 = new SelectionIntentRule[toolBodies.Length];
            for (int i = 0; i < toolBodies.Length; i++)
            {
                ruleArray2[i] = workPart.ScRuleFactory.CreateRuleFaceBody((NXOpen.Body) toolBodies[i]);
            }
            featureBuilder.BooleanTool.FacePlaneTool.ToolFaces.FaceCollector.ReplaceRules(ruleArray2, false);
            NXOpen.Features.SplitBody split = (NXOpen.Features.SplitBody) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.Destroy();
            return new Snap.NX.SplitBody(split);
        }

        internal static Snap.NX.SplitBody CreateSplitBody(Snap.NX.Body targetBody, Snap.NX.DatumPlane[] toolDatumPlanes)
        {
            NXOpen.Part workPart = (NXOpen.Part) Globals.WorkPart;
            NXOpen.Features.SplitBodyBuilder featureBuilder = workPart.Features.CreateSplitBodyBuilder(null);
            featureBuilder.BooleanTool.ExtrudeRevolveTool.ToolSection.PrepareMappingData();
            featureBuilder.BooleanTool.ExtrudeRevolveTool.ToolSection.DistanceTolerance = Globals.DistanceTolerance;
            featureBuilder.BooleanTool.ExtrudeRevolveTool.ToolSection.ChainingTolerance = (Globals.UnitType == Globals.Unit.Millimeter) ? 0.02413 : 0.00095;
            //ScCollector collector = workPart.ScCollectors.CreateCollector();
            //NXOpen.Body[] bodies = new NXOpen.Body[] { targetBody };
            //BodyDumbRule rule = workPart.ScRuleFactory.CreateRuleBodyDumb(bodies);
            //SelectionIntentRule[] rules = new SelectionIntentRule[] { rule };
            //collector.ReplaceRules(rules, false);
            //featureBuilder.TargetBodyCollector = collector;
            featureBuilder.TargetBody.Add(targetBody);
            SelectionIntentRule[] ruleArray2 = new SelectionIntentRule[toolDatumPlanes.Length];
            for (int i = 0; i < toolDatumPlanes.Length; i++)
            {
                NXOpen.DatumPlane[] faces = new NXOpen.DatumPlane[] { toolDatumPlanes[i].NXOpenDatumPlaneFeature.DatumPlane };
                ruleArray2[i] = workPart.ScRuleFactory.CreateRuleFaceDatum(faces);
            }
            featureBuilder.BooleanTool.FacePlaneTool.ToolFaces.FaceCollector.ReplaceRules(ruleArray2, false);
            NXOpen.Features.SplitBody split = (NXOpen.Features.SplitBody) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.Destroy();
            return new Snap.NX.SplitBody(split);
        }

        internal static Snap.NX.SplitBody CreateSplitBody(Snap.NX.Body targetBody, Snap.NX.Face[] toolFaces)
        {
            NXOpen.Part work = Globals.Session.Parts.Work;
            NXOpen.Features.SplitBodyBuilder featureBuilder = work.Features.CreateSplitBodyBuilder(null);
            featureBuilder.BooleanTool.ExtrudeRevolveTool.ToolSection.PrepareMappingData();
            featureBuilder.BooleanTool.ExtrudeRevolveTool.ToolSection.DistanceTolerance = Globals.DistanceTolerance;
            featureBuilder.BooleanTool.ExtrudeRevolveTool.ToolSection.ChainingTolerance = (Globals.UnitType == Globals.Unit.Millimeter) ? 0.02413 : 0.00095;
            //ScCollector collector = work.ScCollectors.CreateCollector();
            //NXOpen.Body[] bodies = new NXOpen.Body[] { targetBody };
            //BodyDumbRule rule = work.ScRuleFactory.CreateRuleBodyDumb(bodies);
            //SelectionIntentRule[] rules = new SelectionIntentRule[] { rule };
            //collector.ReplaceRules(rules, false);
            //featureBuilder.TargetBodyCollector = collector;
            featureBuilder.TargetBody.Add(targetBody);
            SelectionIntentRule[] ruleArray2 = new SelectionIntentRule[toolFaces.Length];
            for (int i = 0; i < toolFaces.Length; i++)
            {
                ruleArray2[i] = work.ScRuleFactory.CreateRuleFaceBody((NXOpen.Body) toolFaces[i].Body);
            }
            featureBuilder.BooleanTool.FacePlaneTool.ToolFaces.FaceCollector.ReplaceRules(ruleArray2, false);
            NXOpen.Features.SplitBody split = (NXOpen.Features.SplitBody) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.Destroy();
            return new Snap.NX.SplitBody(split);
        }

        public static implicit operator Snap.NX.SplitBody(NXOpen.Features.SplitBody split)
        {
            return new Snap.NX.SplitBody(split);
        }

        public static implicit operator NXOpen.Features.SplitBody(Snap.NX.SplitBody split)
        {
            return (NXOpen.Features.SplitBody) split.NXOpenTaggedObject;
        }

        public static Snap.NX.SplitBody Wrap(Tag nxopenSplitBodyTag)
        {
            if (nxopenSplitBodyTag == Tag.Null)
            {
                throw new ArgumentException("Input tag is NXOpen.Tag.Null");
            }
            NXOpen.Features.SplitBody objectFromTag = Snap.NX.NXObject.GetObjectFromTag(nxopenSplitBodyTag) as NXOpen.Features.SplitBody;
            if (objectFromTag == null)
            {
                throw new ArgumentException("Input tag doesn't belong to an NXOpen.Features.SplitBody object");
            }
            return objectFromTag;
        }

        public NXOpen.Features.SplitBody NXOpenSplitBody
        {
            get
            {
                return (NXOpen.Features.SplitBody) base.NXOpenTaggedObject;
            }
            private set
            {
                base.NXOpenTaggedObject = value;
            }
        }

        public NXOpen.Features.SplitBodyBuilder SplitBodyBuilder
        {
            get
            {
                NXOpen.Part owningPart = (NXOpen.Part) this.NXOpenSplitBody.OwningPart;
                NXOpen.Features.SplitBody splitBody = (NXOpen.Features.SplitBody) this;
                return owningPart.Features.CreateSplitBodyBuilder(splitBody);
            }
        }
    }
}

