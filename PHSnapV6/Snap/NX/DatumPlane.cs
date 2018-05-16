namespace Snap.NX
{
    using NXOpen;
    using NXOpen.Features;
    using Snap;
    using System;

    public class DatumPlane : Snap.NX.Feature
    {
        internal DatumPlane(DatumPlaneFeature datumPlaneFeature) : base(datumPlaneFeature)
        {
            this.NXOpenDatumPlaneFeature = datumPlaneFeature;
        }

        internal static Snap.NX.DatumPlane CreateDatumPlane(Snap.NX.ICurve curve, Snap.Number arcLength)
        {
            NXOpen.Features.DatumPlaneBuilder featureBuilder = Globals.WorkPart.NXOpenPart.Features.CreateDatumPlaneBuilder(null);
            NXOpen.Plane plane = featureBuilder.GetPlane();
            plane.SetMethod(PlaneTypes.MethodType.Frenet);
            NXOpen.NXObject[] geom = new NXOpen.NXObject[] { (NXOpen.NXObject) curve.NXOpenICurve };
            plane.SetGeometry(geom);
            plane.SetFrenetSubtype(PlaneTypes.FrenetSubtype.Tangent);
            plane.SetPercent(true);
            plane.SetExpression(arcLength.ToString());
            plane.Evaluate();
            DatumPlaneFeature datumPlaneFeature = (DatumPlaneFeature) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.Destroy();
            return new Snap.NX.DatumPlane(datumPlaneFeature);
        }

        internal static Snap.NX.DatumPlane CreateDatumPlane(Position origin, Orientation orientation)
        {
            return (DatumPlaneFeature) Globals.NXOpenWorkPart.Datums.CreateFixedDatumPlane((Point3d) origin, (Matrix3x3) orientation).Feature;
        }

        internal static Snap.NX.DatumPlane CreateDatumPlane(Position position, Vector direction)
        {
            NXOpen.Part workPart = (NXOpen.Part) Globals.WorkPart;
            NXOpen.Features.DatumPlaneBuilder featureBuilder = workPart.Features.CreateDatumPlaneBuilder(null);
            NXOpen.Plane plane = featureBuilder.GetPlane();
            plane.SetMethod(PlaneTypes.MethodType.PointDir);
            Direction direction2 = Globals.NXOpenWorkPart.Directions.CreateDirection((Point3d) position, (Vector3d) direction, SmartObject.UpdateOption.WithinModeling);
            NXOpen.NXObject[] geom = new NXOpen.NXObject[] { workPart.Points.CreatePoint((Point3d) position), direction2 };
            plane.SetGeometry(geom);
            plane.Evaluate();
            DatumPlaneFeature datumPlaneFeature = (DatumPlaneFeature) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.Destroy();
            return new Snap.NX.DatumPlane(datumPlaneFeature);
        }

        public static implicit operator Snap.NX.DatumPlane(DatumPlaneFeature datumPlaneFeature)
        {
            return new Snap.NX.DatumPlane(datumPlaneFeature);
        }

        public static implicit operator DatumPlaneFeature(Snap.NX.DatumPlane datumPlane)
        {
            return (DatumPlaneFeature) datumPlane.NXOpenTaggedObject;
        }

        public void ReverseDirection()
        {
            Session.UndoMarkId undoMark = Globals.Session.SetUndoMark(Session.MarkVisibility.Invisible, "");
            NXOpen.Features.DatumPlaneBuilder datumPlaneBuilder = this.DatumPlaneBuilder;
            datumPlaneBuilder.GetPlane().SetFlip(true);
            Snap.NX.Feature.CommitFeature(datumPlaneBuilder);
            datumPlaneBuilder.Destroy();
            Globals.Session.UpdateManager.DoUpdate(undoMark);
            Globals.Session.DeleteUndoMark(undoMark, null);
        }

        public static Snap.NX.DatumPlane Wrap(Tag nxopenDatumPlaneTag)
        {
            if (nxopenDatumPlaneTag == Tag.Null)
            {
                throw new ArgumentException("Input tag is NXOpen.Tag.Null");
            }
            DatumPlaneFeature objectFromTag = Snap.NX.NXObject.GetObjectFromTag(nxopenDatumPlaneTag) as DatumPlaneFeature;
            if (objectFromTag == null)
            {
                throw new ArgumentException("Input tag doesn't belong to an NXOpen.Features.DatumPlaneFeature object");
            }
            return objectFromTag;
        }

        public Position[] CornerPoints
        {
            get
            {
                Point3d[] pointdArray = new Point3d[4];
                this.NXOpenDatumPlaneFeature.DatumPlane.GetCornerPoints(out pointdArray[0], out pointdArray[1], out pointdArray[2], out pointdArray[3]);
                return new Position[] { pointdArray[0], pointdArray[1], pointdArray[2], pointdArray[3] };
            }
            set
            {
                this.NXOpenDatumPlaneFeature.DatumPlane.SetCornerPoints(value[0], value[1], value[2], value[3]);
            }
        }

        public NXOpen.Features.DatumPlaneBuilder DatumPlaneBuilder
        {
            get
            {
                NXOpen.Part owningPart = (NXOpen.Part) this.NXOpenDatumPlaneFeature.OwningPart;
                DatumPlaneFeature dplane = (DatumPlaneFeature) this;
                return owningPart.Features.CreateDatumPlaneBuilder(dplane);
            }
        }

        public Vector Normal
        {
            get
            {
                return this.NXOpenDatumPlaneFeature.DatumPlane.Normal;
            }
        }

        public DatumPlaneFeature NXOpenDatumPlaneFeature
        {
            get
            {
                return (DatumPlaneFeature) base.NXOpenTaggedObject;
            }
            private set
            {
                base.NXOpenTaggedObject = value;
            }
        }

        public override DisplayableObject[] NXOpenDisplayableObjects
        {
            get
            {
                NXOpen.DatumPlane datumPlane = this.NXOpenDatumPlaneFeature.DatumPlane;
                return new DisplayableObject[] { datumPlane };
            }
        }

        public override ObjectTypes.Type ObjectType
        {
            get
            {
                return ObjectTypes.Type.DatumPlane;
            }
        }

        public Position Origin
        {
            get
            {
                return this.NXOpenDatumPlaneFeature.DatumPlane.Origin;
            }
        }

        public override int Translucency
        {
            get
            {
                return 70;
            }
        }
    }
}

