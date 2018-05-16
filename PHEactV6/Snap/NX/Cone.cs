namespace Snap.NX
{
    using NXOpen;
    using NXOpen.Features;
    using NXOpen.GeometricUtilities;
    using Snap;
    using System;

    public class Cone : Snap.NX.Feature
    {
        internal Cone(NXOpen.Features.Cone cone) : base(cone)
        {
            this.NXOpenCone = cone;
        }

        internal static Snap.NX.Cone CreateConeFromArcs(Snap.NX.ICurve baseArc, Snap.NX.ICurve topArc)
        {
            NXOpen.Features.ConeBuilder featureBuilder = Globals.WorkPart.NXOpenPart.Features.CreateConeBuilder(null);
            featureBuilder.BooleanOption.Type = BooleanOperation.BooleanType.Create;
            featureBuilder.Type = NXOpen.Features.ConeBuilder.Types.TwoCoaxialArcs;
            featureBuilder.BaseArc.Value = baseArc.NXOpenICurve;
            featureBuilder.TopArc.Value = topArc.NXOpenICurve;
            NXOpen.Features.Cone cone = (NXOpen.Features.Cone) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.Destroy();
            return new Snap.NX.Cone(cone);
        }

        internal static Snap.NX.Cone CreateConeFromDiameterHeightAngle(Position axisPoint, Vector direction, Snap.Number baseDiameter, Snap.Number height, Snap.Number halfAngle)
        {
            NXOpen.Part workPart = (NXOpen.Part) Globals.WorkPart;
            NXOpen.Features.ConeBuilder featureBuilder = workPart.Features.CreateConeBuilder(null);
            featureBuilder.BooleanOption.Type = BooleanOperation.BooleanType.Create;
            featureBuilder.Type = NXOpen.Features.ConeBuilder.Types.BaseDiameterHeightAndHalfAngle;
            Direction direction2 = workPart.Directions.CreateDirection((Point3d) axisPoint, (Vector3d) direction, SmartObject.UpdateOption.WithinModeling);
            NXOpen.Axis axis = featureBuilder.Axis;
            axis.Direction = direction2;
            axis.Point = workPart.Points.CreatePoint((Point3d) axisPoint);
            featureBuilder.BaseDiameter.RightHandSide = baseDiameter.ToString();
            featureBuilder.Height.RightHandSide = height.ToString();
            featureBuilder.HalfAngle.RightHandSide = halfAngle.ToString();
            NXOpen.Features.Cone cone = (NXOpen.Features.Cone) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.Destroy();
            return new Snap.NX.Cone(cone);
        }

        internal static Snap.NX.Cone CreateConeFromDiametersAngle(Position axisPoint, Vector direction, Snap.Number baseDiameter, Snap.Number topDiameter, Snap.Number halfAngle)
        {
            NXOpen.Part workPart = (NXOpen.Part) Globals.WorkPart;
            NXOpen.Features.ConeBuilder featureBuilder = workPart.Features.CreateConeBuilder(null);
            featureBuilder.BooleanOption.Type = BooleanOperation.BooleanType.Create;
            featureBuilder.Type = NXOpen.Features.ConeBuilder.Types.DiametersAndHalfAngle;
            Direction direction2 = workPart.Directions.CreateDirection((Point3d) axisPoint, (Vector3d) direction, SmartObject.UpdateOption.WithinModeling);
            NXOpen.Axis axis = featureBuilder.Axis;
            axis.Direction = direction2;
            axis.Point = workPart.Points.CreatePoint((Point3d) axisPoint);
            featureBuilder.BaseDiameter.RightHandSide = baseDiameter.ToString();
            featureBuilder.TopDiameter.RightHandSide = topDiameter.ToString();
            featureBuilder.HalfAngle.RightHandSide = halfAngle.ToString();
            NXOpen.Features.Cone cone = (NXOpen.Features.Cone) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.Destroy();
            return new Snap.NX.Cone(cone);
        }

        internal static Snap.NX.Cone CreateConeFromDiametersHeight(Position axisPoint, Vector direction, Snap.Number baseDiameter, Snap.Number topDiameter, Snap.Number height)
        {
            NXOpen.Part workPart = (NXOpen.Part) Globals.WorkPart;
            NXOpen.Features.ConeBuilder featureBuilder = workPart.Features.CreateConeBuilder(null);
            featureBuilder.BooleanOption.Type = BooleanOperation.BooleanType.Create;
            featureBuilder.Type = NXOpen.Features.ConeBuilder.Types.DiametersAndHeight;
            Direction direction2 = workPart.Directions.CreateDirection((Point3d) axisPoint, (Vector3d) direction, SmartObject.UpdateOption.WithinModeling);
            NXOpen.Axis axis = featureBuilder.Axis;
            axis.Direction = direction2;
            axis.Point = workPart.Points.CreatePoint((Point3d) axisPoint);
            featureBuilder.BaseDiameter.RightHandSide = baseDiameter.ToString();
            featureBuilder.TopDiameter.RightHandSide = topDiameter.ToString();
            featureBuilder.Height.RightHandSide = height.ToString();
            NXOpen.Features.Cone cone = (NXOpen.Features.Cone) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.Destroy();
            return new Snap.NX.Cone(cone);
        }

        public static implicit operator Snap.NX.Cone(NXOpen.Features.Cone cone)
        {
            return new Snap.NX.Cone(cone);
        }

        public static implicit operator NXOpen.Features.Cone(Snap.NX.Cone cone)
        {
            return cone.NXOpenCone;
        }

        public static Snap.NX.Cone Wrap(Tag nxopenConeTag)
        {
            if (nxopenConeTag == Tag.Null)
            {
                throw new ArgumentException("Input tag is NXOpen.Tag.Null");
            }
            NXOpen.Features.Cone objectFromTag = Snap.NX.NXObject.GetObjectFromTag(nxopenConeTag) as NXOpen.Features.Cone;
            if (objectFromTag == null)
            {
                throw new ArgumentException("Input tag doesn't belong to an NXOpen.Features.Cone object");
            }
            return objectFromTag;
        }

        public Position AxisPoint
        {
            get
            {
                NXOpen.Features.ConeBuilder coneBuilder = this.ConeBuilder;
                Position origin = coneBuilder.Axis.Origin;
                coneBuilder.Destroy();
                return origin;
            }
        }

        public Vector AxisVector
        {
            get
            {
                NXOpen.Features.ConeBuilder coneBuilder = this.ConeBuilder;
                Vector directionVector = coneBuilder.Axis.DirectionVector;
                coneBuilder.Destroy();
                return directionVector;
            }
        }

        public Snap.NX.Arc BaseArc
        {
            get
            {
                NXOpen.Features.ConeBuilder coneBuilder = this.ConeBuilder;
                Snap.NX.Arc arc = (NXOpen.Arc) coneBuilder.BaseArc.Value;
                coneBuilder.Destroy();
                return arc;
            }
        }

        public double BaseDiameter
        {
            get
            {
                NXOpen.Features.ConeBuilder coneBuilder = this.ConeBuilder;
                double num = coneBuilder.BaseDiameter.Value;
                coneBuilder.Destroy();
                return num;
            }
        }

        public NXOpen.Features.ConeBuilder ConeBuilder
        {
            get
            {
                NXOpen.Part owningPart = (NXOpen.Part) this.NXOpenCone.OwningPart;
                return owningPart.Features.CreateConeBuilder(this.NXOpenCone);
            }
        }

        public double HalfAngle
        {
            get
            {
                NXOpen.Features.ConeBuilder coneBuilder = this.ConeBuilder;
                double num = coneBuilder.HalfAngle.Value;
                coneBuilder.Destroy();
                return num;
            }
        }

        public double Height
        {
            get
            {
                NXOpen.Features.ConeBuilder coneBuilder = this.ConeBuilder;
                double num = coneBuilder.Height.Value;
                coneBuilder.Destroy();
                return num;
            }
        }

        public NXOpen.Features.Cone NXOpenCone
        {
            get
            {
                return (NXOpen.Features.Cone) base.NXOpenTaggedObject;
            }
            private set
            {
                base.NXOpenTaggedObject = value;
            }
        }

        public Snap.NX.Arc TopArc
        {
            get
            {
                NXOpen.Features.ConeBuilder coneBuilder = this.ConeBuilder;
                Snap.NX.Arc arc = (NXOpen.Arc) coneBuilder.TopArc.Value;
                coneBuilder.Destroy();
                return arc;
            }
        }

        public double TopDiameter
        {
            get
            {
                NXOpen.Features.ConeBuilder coneBuilder = this.ConeBuilder;
                double num = coneBuilder.TopDiameter.Value;
                coneBuilder.Destroy();
                return num;
            }
        }
    }
}

