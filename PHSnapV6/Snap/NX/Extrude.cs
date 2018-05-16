namespace Snap.NX
{
    using NXOpen;
    using NXOpen.Features;
    using NXOpen.GeometricUtilities;
    using Snap;
    using System;

    public class Extrude : Snap.NX.Feature
    {
        internal Extrude(NXOpen.Features.Extrude extrude) : base(extrude)
        {
            this.NXOpenExtrude = extrude;
        }

        internal static Snap.NX.Extrude CreateExtrude(Snap.NX.Section section, Vector axis, Snap.Number[] extents, Snap.Number draftAngle, bool offset, Snap.Number[] offsetValues, bool createSheet)
        {
            NXOpen.Part workPart = (NXOpen.Part) Globals.WorkPart;
            NXOpen.Features.ExtrudeBuilder featureBuilder = workPart.Features.CreateExtrudeBuilder(null);
            featureBuilder.DistanceTolerance = Globals.DistanceTolerance;
            featureBuilder.BooleanOperation.Type = BooleanOperation.BooleanType.Create;
            if (createSheet)
            {
                featureBuilder.FeatureOptions.BodyType = FeatureOptions.BodyStyle.Sheet;
            }
            featureBuilder.Limits.StartExtend.Value.RightHandSide = extents[0].ToString();
            featureBuilder.Limits.EndExtend.Value.RightHandSide = extents[1].ToString();
            featureBuilder.Offset.Option = NXOpen.GeometricUtilities.Type.NoOffset;
            if (offset)
            {
                featureBuilder.Offset.Option = NXOpen.GeometricUtilities.Type.NonsymmetricOffset;
                featureBuilder.Offset.StartOffset.RightHandSide = offsetValues[0].ToString();
                featureBuilder.Offset.EndOffset.RightHandSide = offsetValues[1].ToString();
            }
            double num = Snap.Number.Parse(Snap.Number.NullToZero(draftAngle).ToString());
            if (System.Math.Abs(num) < 0.001)
            {
                featureBuilder.Draft.DraftOption = SimpleDraft.SimpleDraftType.NoDraft;
            }
            else
            {
                featureBuilder.Draft.DraftOption = SimpleDraft.SimpleDraftType.SimpleFromProfile;
            }
            featureBuilder.Draft.FrontDraftAngle.RightHandSide = Snap.Number.ToString(num);
            featureBuilder.Section = (NXOpen.Section) section;
            Point3d origin = new Point3d(30.0, 0.0, 0.0);
            Vector3d vector = new Vector3d(axis.X, axis.Y, axis.Z);
            NXOpen.Direction direction = workPart.Directions.CreateDirection(origin, vector, SmartObject.UpdateOption.WithinModeling);
            featureBuilder.Direction = direction;
            NXOpen.Features.Extrude extrude = (NXOpen.Features.Extrude) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.Destroy();
            return new Snap.NX.Extrude(extrude);
        }

        public static implicit operator Snap.NX.Extrude(NXOpen.Features.Extrude extrude)
        {
            return new Snap.NX.Extrude(extrude);
        }

        public static implicit operator NXOpen.Features.Extrude(Snap.NX.Extrude extrude)
        {
            return extrude.NXOpenExtrude;
        }

        public static Snap.NX.Extrude Wrap(Tag nxopenExtrudeTag)
        {
            if (nxopenExtrudeTag == Tag.Null)
            {
                throw new ArgumentException("Input tag is NXOpen.Tag.Null");
            }
            NXOpen.Features.Extrude objectFromTag = Snap.NX.NXObject.GetObjectFromTag(nxopenExtrudeTag) as NXOpen.Features.Extrude;
            if (objectFromTag == null)
            {
                throw new ArgumentException("Input tag doesn't belong to an NXOpen.Features.Extrude object");
            }
            return objectFromTag;
        }

        public Vector Direction
        {
            get
            {
                NXOpen.Part owningPart = (NXOpen.Part) this.NXOpenExtrude.OwningPart;
                NXOpen.Features.ExtrudeBuilder builder = owningPart.Features.CreateExtrudeBuilder(this.NXOpenExtrude);
                Vector vector = new Vector(builder.Direction.Vector);
                builder.Destroy();
                return vector;
            }
            set
            {
                Session.UndoMarkId undoMark = Globals.Session.SetUndoMark(Session.MarkVisibility.Invisible, "");
                NXOpen.Part owningPart = (NXOpen.Part) this.NXOpenExtrude.OwningPart;
                NXOpen.Features.ExtrudeBuilder featureBuilder = owningPart.Features.CreateExtrudeBuilder(this.NXOpenExtrude);
                featureBuilder.Direction.Vector = (Vector3d) value;
                Snap.NX.Feature.CommitFeature(featureBuilder);
                featureBuilder.Destroy();
                Globals.Session.UpdateManager.DoUpdate(undoMark);
                Globals.Session.DeleteUndoMark(undoMark, "");
            }
        }

        public NXOpen.Features.ExtrudeBuilder ExtrudeBuilder
        {
            get
            {
                NXOpen.Part owningPart = (NXOpen.Part) this.NXOpenExtrude.OwningPart;
                return owningPart.Features.CreateExtrudeBuilder(this.NXOpenExtrude);
            }
        }

        public NXOpen.Features.Extrude NXOpenExtrude
        {
            get
            {
                return (NXOpen.Features.Extrude) base.NXOpenTaggedObject;
            }
            private set
            {
                base.NXOpenTaggedObject = value;
            }
        }
    }
}

