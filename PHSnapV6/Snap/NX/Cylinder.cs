namespace Snap.NX
{
    using NXOpen;
    using NXOpen.Features;
    using NXOpen.GeometricUtilities;
    using Snap;
    using System;

    public class Cylinder : Snap.NX.Feature
    {
        internal Cylinder(NXOpen.Features.Cylinder cylinder) : base(cylinder)
        {
            this.NXOpenCylinder = cylinder;
        }

        internal static Snap.NX.Cylinder CreateCylinder(Snap.NX.ICurve arc, Snap.Number height)
        {
            NXOpen.Features.CylinderBuilder featureBuilder = Globals.WorkPart.NXOpenPart.Features.CreateCylinderBuilder(null);
            featureBuilder.Type = NXOpen.Features.CylinderBuilder.Types.ArcAndHeight;
            featureBuilder.BooleanOption.Type = BooleanOperation.BooleanType.Create;
            featureBuilder.Arc.Value = arc.NXOpenICurve;
            featureBuilder.Height.RightHandSide = height.ToString();
            NXOpen.Features.Cylinder cylinder = (NXOpen.Features.Cylinder) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.Destroy();
            return new Snap.NX.Cylinder(cylinder);
        }

        internal static Snap.NX.Cylinder CreateCylinder(Position axisPoint, Vector axisVector, Snap.Number height, Snap.Number diameter)
        {
            NXOpen.Part workPart = (NXOpen.Part) Globals.WorkPart;
            NXOpen.Features.CylinderBuilder featureBuilder = workPart.Features.CreateCylinderBuilder(null);
            featureBuilder.Type = NXOpen.Features.CylinderBuilder.Types.AxisDiameterAndHeight;
            featureBuilder.BooleanOption.Type = BooleanOperation.BooleanType.Create;
            featureBuilder.Diameter.RightHandSide = diameter.ToString();
            featureBuilder.Height.RightHandSide = height.ToString();
            Position origin = Position.Origin;
            Direction direction = workPart.Directions.CreateDirection((Point3d) origin, (Vector3d) axisVector, SmartObject.UpdateOption.WithinModeling);
            featureBuilder.Axis.Direction = direction;
            featureBuilder.Axis.Point = workPart.Points.CreatePoint((Point3d) axisPoint);
            NXOpen.Features.Cylinder cylinder = (NXOpen.Features.Cylinder) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.Destroy();
            return new Snap.NX.Cylinder(cylinder);
        }

        public static implicit operator Snap.NX.Cylinder(NXOpen.Features.Cylinder cylinder)
        {
            return new Snap.NX.Cylinder(cylinder);
        }

        public static implicit operator NXOpen.Features.Cylinder(Snap.NX.Cylinder cylinder)
        {
            return cylinder.NXOpenCylinder;
        }

        public static Snap.NX.Cylinder Wrap(Tag nxopenCylinderTag)
        {
            if (nxopenCylinderTag == Tag.Null)
            {
                throw new ArgumentException("Input tag is NXOpen.Tag.Null");
            }
            NXOpen.Features.Cylinder objectFromTag = Snap.NX.NXObject.GetObjectFromTag(nxopenCylinderTag) as NXOpen.Features.Cylinder;
            if (objectFromTag == null)
            {
                throw new ArgumentException("Input tag doesn't belong to an NXOpen.Features.Cylinder object");
            }
            return objectFromTag;
        }

        public Position AxisPoint
        {
            get
            {
                NXOpen.Features.CylinderBuilder cylinderBuilder = this.CylinderBuilder;
                Position coordinates = cylinderBuilder.Axis.Point.Coordinates;
                cylinderBuilder.Destroy();
                return coordinates;
            }
        }

        public Vector AxisVector
        {
            get
            {
                NXOpen.Features.CylinderBuilder cylinderBuilder = this.CylinderBuilder;
                Vector directionVector = cylinderBuilder.Axis.DirectionVector;
                cylinderBuilder.Destroy();
                return directionVector;
            }
        }

        public NXOpen.Features.CylinderBuilder CylinderBuilder
        {
            get
            {
                NXOpen.Part owningPart = (NXOpen.Part) this.NXOpenCylinder.OwningPart;
                return owningPart.Features.CreateCylinderBuilder(this.NXOpenCylinder);
            }
        }

        public double Diameter
        {
            get
            {
                NXOpen.Features.CylinderBuilder cylinderBuilder = this.CylinderBuilder;
                double num = cylinderBuilder.Diameter.Value;
                cylinderBuilder.Destroy();
                return num;
            }
        }

        public double Height
        {
            get
            {
                NXOpen.Features.CylinderBuilder cylinderBuilder = this.CylinderBuilder;
                double num = cylinderBuilder.Height.Value;
                cylinderBuilder.Destroy();
                return num;
            }
        }

        public NXOpen.Features.Cylinder NXOpenCylinder
        {
            get
            {
                return (NXOpen.Features.Cylinder) base.NXOpenTaggedObject;
            }
            private set
            {
                base.NXOpenTaggedObject = value;
            }
        }
    }
}

