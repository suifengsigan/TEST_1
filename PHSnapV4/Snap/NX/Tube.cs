namespace Snap.NX
{
    using NXOpen;
    using NXOpen.Features;
    using NXOpen.GeometricUtilities;
    using Snap;
    using System;

    public class Tube : Snap.NX.Feature
    {
        internal Tube(NXOpen.Features.Tube tube) : base(tube)
        {
            this.NXOpenTube = tube;
        }

        internal static Snap.NX.Tube CreateTube(Snap.NX.Curve spine, Snap.Number outerDiameter, Snap.Number innerDiameter, bool createBsurface)
        {
            NXOpen.Features.TubeBuilder featureBuilder = Globals.WorkPart.NXOpenPart.Features.CreateTubeBuilder(null);
            featureBuilder.Tolerance = Globals.DistanceTolerance;
            featureBuilder.OuterDiameter.RightHandSide = outerDiameter.ToString();
            featureBuilder.InnerDiameter.RightHandSide = innerDiameter.ToString();
            featureBuilder.OutputOption = NXOpen.Features.TubeBuilder.Output.MultipleSegments;
            if (createBsurface)
            {
                featureBuilder.OutputOption = NXOpen.Features.TubeBuilder.Output.SingleSegment;
            }
            ((Snap.NX.Section)featureBuilder.PathSection).AddICurve(new Snap.NX.ICurve[] { spine });
            featureBuilder.BooleanOption.Type = BooleanOperation.BooleanType.Create;
            NXOpen.Features.Tube tube = (NXOpen.Features.Tube) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.Destroy();
            return new Snap.NX.Tube(tube);
        }

        public static implicit operator Snap.NX.Tube(NXOpen.Features.Tube tube)
        {
            return new Snap.NX.Tube(tube);
        }

        public static implicit operator NXOpen.Features.Tube(Snap.NX.Tube tube)
        {
            return tube.NXOpenTube;
        }

        public static Snap.NX.Tube Wrap(Tag nxopenTubeTag)
        {
            if (nxopenTubeTag == Tag.Null)
            {
                throw new ArgumentException("Input tag is NXOpen.Tag.Null");
            }
            NXOpen.Features.Tube objectFromTag = Snap.NX.NXObject.GetObjectFromTag(nxopenTubeTag) as NXOpen.Features.Tube;
            if (objectFromTag == null)
            {
                throw new ArgumentException("Input tag doesn't belong to an NXOpen.Features.Tube object");
            }
            return objectFromTag;
        }

        public double InnerDiameter
        {
            get
            {
                NXOpen.Part owningPart = (NXOpen.Part) this.NXOpenTube.OwningPart;
                NXOpen.Features.TubeBuilder builder = owningPart.Features.CreateTubeBuilder(this.NXOpenTube);
                double num = builder.InnerDiameter.Value;
                builder.Destroy();
                return num;
            }
            set
            {
                Session.UndoMarkId undoMark = Globals.Session.SetUndoMark(Session.MarkVisibility.Invisible, "");
                NXOpen.Features.TubeBuilder featureBuilder = Globals.WorkPart.NXOpenPart.Features.CreateTubeBuilder(this.NXOpenTube);
                featureBuilder.InnerDiameter.Value = value;
                Snap.NX.Feature.CommitFeature(featureBuilder);
                featureBuilder.Destroy();
                Globals.Session.UpdateManager.DoUpdate(undoMark);
                Globals.Session.DeleteUndoMark(undoMark, null);
            }
        }

        public NXOpen.Features.Tube NXOpenTube
        {
            get
            {
                return (NXOpen.Features.Tube) base.NXOpenTaggedObject;
            }
            private set
            {
                base.NXOpenTaggedObject = value;
            }
        }

        public double OuterDiameter
        {
            get
            {
                NXOpen.Part owningPart = (NXOpen.Part) this.NXOpenTube.OwningPart;
                NXOpen.Features.TubeBuilder builder = owningPart.Features.CreateTubeBuilder(this.NXOpenTube);
                double num = builder.OuterDiameter.Value;
                builder.Destroy();
                return num;
            }
            set
            {
                Session.UndoMarkId undoMark = Globals.Session.SetUndoMark(Session.MarkVisibility.Invisible, "");
                NXOpen.Part owningPart = (NXOpen.Part) this.NXOpenTube.OwningPart;
                NXOpen.Features.TubeBuilder featureBuilder = owningPart.Features.CreateTubeBuilder(this.NXOpenTube);
                featureBuilder.OuterDiameter.Value = value;
                Snap.NX.Feature.CommitFeature(featureBuilder);
                featureBuilder.Destroy();
                Globals.Session.UpdateManager.DoUpdate(undoMark);
                Globals.Session.DeleteUndoMark(undoMark, null);
            }
        }

        public Snap.NX.Curve Spine
        {
            get
            {
                SectionData[] dataArray;
                SelectionIntentRule[] ruleArray;
                this.NXOpenTube.GetSections();
                Snap.NX.Section section = this.NXOpenTube.GetSections()[0];
                section.NXOpenSection.GetSectionData(out dataArray);
                dataArray[0].GetRules(out ruleArray);
                if (ruleArray[0].Type == SelectionIntentRule.RuleType.CurveDumb)
                {
                    NXOpen.Curve[] curveArray;
                    ((CurveDumbRule) ruleArray[0]).GetData(out curveArray);
                    return curveArray[0];
                }
                return null;
            }
        }

        public NXOpen.Features.TubeBuilder TubeBuilder
        {
            get
            {
                NXOpen.Part owningPart = (NXOpen.Part) this.NXOpenTube.OwningPart;
                return owningPart.Features.CreateTubeBuilder(this.NXOpenTube);
            }
        }
    }
}

