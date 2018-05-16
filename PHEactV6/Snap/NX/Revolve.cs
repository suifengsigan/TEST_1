namespace Snap.NX
{
    using NXOpen;
    using NXOpen.Features;
    using NXOpen.GeometricUtilities;
    using Snap;
    using System;

    public class Revolve : Snap.NX.Feature
    {
        internal Revolve(NXOpen.Features.Revolve revolve) : base(revolve)
        {
            this.NXOpenRevolve = revolve;
        }

        internal static Snap.NX.Revolve CreateRevolve(Snap.NX.ICurve[] icurves, Position axisPoint, Vector axisVector, Snap.Number[] extents, bool offset, Snap.Number[] offsetValues, bool createSheet)
        {
            NXOpen.Part workPart = (NXOpen.Part) Globals.WorkPart;
            NXOpen.Features.RevolveBuilder featureBuilder = workPart.Features.CreateRevolveBuilder(null);
            featureBuilder.Tolerance = Globals.DistanceTolerance;
            Snap.NX.Section section = Snap.NX.Section.CreateSection(icurves);
            featureBuilder.Section = (NXOpen.Section) section;
            section.NXOpenSection.DistanceTolerance = Globals.DistanceTolerance;
            section.NXOpenSection.ChainingTolerance = (Globals.UnitType == Globals.Unit.Millimeter) ? 0.02413 : 0.00095;
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
            Direction direction = workPart.Directions.CreateDirection((Point3d) axisPoint, (Vector3d) axisVector, SmartObject.UpdateOption.WithinModeling);
            NXOpen.Point point = null;
            NXOpen.Axis axis = workPart.Axes.CreateAxis(point, direction, SmartObject.UpdateOption.WithinModeling);
            featureBuilder.Axis = axis;
            NXOpen.Features.Revolve revolve = (NXOpen.Features.Revolve) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.Destroy();
            return new Snap.NX.Revolve(revolve);
        }

        public static implicit operator Snap.NX.Revolve(NXOpen.Features.Revolve revolve)
        {
            return new Snap.NX.Revolve(revolve);
        }

        public static implicit operator NXOpen.Features.Revolve(Snap.NX.Revolve revolve)
        {
            return revolve.NXOpenRevolve;
        }

        public static Snap.NX.Revolve Wrap(Tag nxopenRevolveTag)
        {
            if (nxopenRevolveTag == Tag.Null)
            {
                throw new ArgumentException("Input tag is NXOpen.Tag.Null");
            }
            NXOpen.Features.Revolve objectFromTag = Snap.NX.NXObject.GetObjectFromTag(nxopenRevolveTag) as NXOpen.Features.Revolve;
            if (objectFromTag == null)
            {
                throw new ArgumentException("Input tag doesn't belong to an NXOpen.Features.Revolve object");
            }
            return objectFromTag;
        }

        public Position AxisPoint
        {
            get
            {
                NXOpen.Features.RevolveBuilder builder = Globals.WorkPart.NXOpenPart.Features.CreateRevolveBuilder(this.NXOpenRevolve);
                Position origin = builder.Axis.Origin;
                builder.Destroy();
                return origin;
            }
            set
            {
                Session.UndoMarkId undoMark = Globals.Session.SetUndoMark(Session.MarkVisibility.Invisible, "");
                Snap.NX.Part workPart = Globals.WorkPart;
                NXOpen.Features.RevolveBuilder featureBuilder = workPart.NXOpenPart.Features.CreateRevolveBuilder(this.NXOpenRevolve);
                NXOpen.Axis axis = workPart.NXOpenPart.Axes.CreateAxis((Point3d) value, (Vector3d) this.AxisVector, SmartObject.UpdateOption.WithinModeling);
                featureBuilder.Axis = axis;
                Snap.NX.Feature.CommitFeature(featureBuilder);
                featureBuilder.Destroy();
                Globals.Session.UpdateManager.DoUpdate(undoMark);
                Globals.Session.DeleteUndoMark(undoMark, "");
            }
        }

        public Vector AxisVector
        {
            get
            {
                NXOpen.Features.RevolveBuilder builder = Globals.WorkPart.NXOpenPart.Features.CreateRevolveBuilder(this.NXOpenRevolve);
                Vector directionVector = builder.Axis.DirectionVector;
                builder.Destroy();
                return directionVector;
            }
            set
            {
                Session.UndoMarkId undoMark = Globals.Session.SetUndoMark(Session.MarkVisibility.Invisible, "");
                Snap.NX.Part workPart = Globals.WorkPart;
                NXOpen.Features.RevolveBuilder featureBuilder = workPart.NXOpenPart.Features.CreateRevolveBuilder(this.NXOpenRevolve);
                NXOpen.Axis axis = workPart.NXOpenPart.Axes.CreateAxis((Point3d) this.AxisPoint, (Vector3d) value, SmartObject.UpdateOption.WithinModeling);
                featureBuilder.Axis = axis;
                Snap.NX.Feature.CommitFeature(featureBuilder);
                featureBuilder.Destroy();
                Globals.Session.UpdateManager.DoUpdate(undoMark);
                Globals.Session.DeleteUndoMark(undoMark, "");
            }
        }

        public double[] Extents
        {
            get
            {
                NXOpen.Features.RevolveBuilder builder = Globals.WorkPart.NXOpenPart.Features.CreateRevolveBuilder(this.NXOpenRevolve);
                Snap.NX.Expression expression = builder.Limits.StartExtend.Value;
                Snap.NX.Expression expression2 = builder.Limits.EndExtend.Value;
                builder.Destroy();
                return new double[] { Snap.Number.Parse(expression.RightHandSide), Snap.Number.Parse(expression2.RightHandSide) };
            }
            set
            {
                Session.UndoMarkId undoMark = Globals.Session.SetUndoMark(Session.MarkVisibility.Invisible, "");
                NXOpen.Features.RevolveBuilder featureBuilder = Globals.WorkPart.NXOpenPart.Features.CreateRevolveBuilder(this.NXOpenRevolve);
                featureBuilder.Limits.StartExtend.Value.RightHandSide = Snap.Number.ToString(value[0]);
                featureBuilder.Limits.EndExtend.Value.RightHandSide = Snap.Number.ToString(value[1]);
                Snap.NX.Feature.CommitFeature(featureBuilder);
                featureBuilder.Destroy();
                Globals.Session.UpdateManager.DoUpdate(undoMark);
                Globals.Session.DeleteUndoMark(undoMark, "");
            }
        }

        public Snap.NX.ICurve[] ICurves
        {
            get
            {
                NXOpen.Features.RevolveBuilder builder = Globals.WorkPart.NXOpenPart.Features.CreateRevolveBuilder(this.NXOpenRevolve);
                Snap.NX.ICurve[] icurves = ((Snap.NX.Section)builder.Section).GetIcurves();
                builder.Destroy();
                return icurves;
            }
        }

        public NXOpen.Features.Revolve NXOpenRevolve
        {
            get
            {
                return (NXOpen.Features.Revolve) base.NXOpenTaggedObject;
            }
            private set
            {
                base.NXOpenTaggedObject = value;
            }
        }

        public double[] Offsets
        {
            get
            {
                NXOpen.Features.RevolveBuilder builder = Globals.WorkPart.NXOpenPart.Features.CreateRevolveBuilder(this.NXOpenRevolve);
                double num = builder.Offset.StartOffset.Value;
                double num2 = builder.Offset.EndOffset.Value;
                builder.Destroy();
                return new double[] { num, num2 };
            }
            set
            {
                Session.UndoMarkId undoMark = Globals.Session.SetUndoMark(Session.MarkVisibility.Invisible, "");
                NXOpen.Features.RevolveBuilder featureBuilder = Globals.WorkPart.NXOpenPart.Features.CreateRevolveBuilder(this.NXOpenRevolve);
                featureBuilder.Offset.StartOffset.RightHandSide = Snap.Number.ToString(value[0]);
                featureBuilder.Offset.EndOffset.RightHandSide = Snap.Number.ToString(value[1]);
                Snap.NX.Feature.CommitFeature(featureBuilder);
                featureBuilder.Destroy();
                Globals.Session.UpdateManager.DoUpdate(undoMark);
                Globals.Session.DeleteUndoMark(undoMark, "");
            }
        }

        public NXOpen.Features.RevolveBuilder RevolveBuilder
        {
            get
            {
                NXOpen.Part owningPart = (NXOpen.Part) this.NXOpenRevolve.OwningPart;
                return owningPart.Features.CreateRevolveBuilder(this.NXOpenRevolve);
            }
        }
    }
}

