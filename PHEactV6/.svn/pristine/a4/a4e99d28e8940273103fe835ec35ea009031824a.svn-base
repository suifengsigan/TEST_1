namespace Snap.NX
{
    using NXOpen;
    using NXOpen.Features;
    using NXOpen.GeometricUtilities;
    using Snap;
    using System;

    public class Thicken : Snap.NX.Feature
    {
        internal Thicken(NXOpen.Features.Thicken thicken) : base(thicken)
        {
            this.NXOpenThicken = thicken;
        }

        internal static Snap.NX.Thicken CreateThicken(Snap.NX.Body[] targetBodies, Snap.Number offset1, Snap.Number offset2)
        {
            NXOpen.Part workPart = (NXOpen.Part) Globals.WorkPart;
            NXOpen.Features.ThickenBuilder featureBuilder = workPart.Features.CreateThickenBuilder(null);
            featureBuilder.Tolerance = Globals.DistanceTolerance;
            featureBuilder.FirstOffset.RightHandSide = offset1.ToString();
            featureBuilder.SecondOffset.RightHandSide = offset2.ToString();
            featureBuilder.BooleanOperation.Type = BooleanOperation.BooleanType.Create;
            NXOpen.Body[] bodyArray = new NXOpen.Body[targetBodies.Length];
            for (int i = 0; i < targetBodies.Length; i++)
            {
                bodyArray[i] = (NXOpen.Body) targetBodies[i];
            }
            featureBuilder.BooleanOperation.SetTargetBodies(bodyArray);
            SelectionIntentRule[] rules = new SelectionIntentRule[targetBodies.Length];
            for (int j = 0; j < targetBodies.Length; j++)
            {
                rules[j] = workPart.ScRuleFactory.CreateRuleFaceBody((NXOpen.Body) targetBodies[j]);
            }
            featureBuilder.FaceCollector.ReplaceRules(rules, false);
            NXOpen.Features.Thicken thicken = (NXOpen.Features.Thicken) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.Destroy();
            return new Snap.NX.Thicken(thicken);
        }

        public static implicit operator Snap.NX.Thicken(NXOpen.Features.Thicken thicken)
        {
            return new Snap.NX.Thicken(thicken);
        }

        public static implicit operator NXOpen.Features.Thicken(Snap.NX.Thicken thicken)
        {
            return (NXOpen.Features.Thicken) thicken.NXOpenTaggedObject;
        }

        public static Snap.NX.Thicken Wrap(Tag nxopenThickenTag)
        {
            if (nxopenThickenTag == Tag.Null)
            {
                throw new ArgumentException("Input tag is NXOpen.Tag.Null");
            }
            NXOpen.Features.Thicken objectFromTag = Snap.NX.NXObject.GetObjectFromTag(nxopenThickenTag) as NXOpen.Features.Thicken;
            if (objectFromTag == null)
            {
                throw new ArgumentException("Input tag doesn't belong to an NXOpen.Features.Thicken object");
            }
            return objectFromTag;
        }

        public NXOpen.Features.Thicken NXOpenThicken
        {
            get
            {
                return (NXOpen.Features.Thicken) base.NXOpenTaggedObject;
            }
            private set
            {
                base.NXOpenTaggedObject = value;
            }
        }

        public double Offset1
        {
            get
            {
                NXOpen.Features.ThickenBuilder thickenBuilder = this.ThickenBuilder;
                double num = thickenBuilder.FirstOffset.Value;
                thickenBuilder.Destroy();
                return num;
            }
        }

        public double Offset2
        {
            get
            {
                NXOpen.Features.ThickenBuilder thickenBuilder = this.ThickenBuilder;
                double num = thickenBuilder.SecondOffset.Value;
                thickenBuilder.Destroy();
                return num;
            }
        }

        public NXOpen.Features.ThickenBuilder ThickenBuilder
        {
            get
            {
                NXOpen.Part owningPart = (NXOpen.Part) this.NXOpenThicken.OwningPart;
                NXOpen.Features.Thicken thicken = (NXOpen.Features.Thicken) this;
                return owningPart.Features.CreateThickenBuilder(thicken);
            }
        }
    }
}

