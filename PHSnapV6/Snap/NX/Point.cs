namespace Snap.NX
{
    using NXOpen;
    using NXOpen.UF;
    using NXOpen.Utilities;
    using Snap;
    using Snap.Geom;
    using System;

    public class Point : Snap.NX.NXObject
    {
        internal Point(NXOpen.Point pt) : base(pt)
        {
            this.NXOpenPoint = pt;
        }

        public Snap.NX.Point Copy()
        {
            Transform xform = Transform.CreateTranslation(0.0, 0.0, 0.0);
            return this.Copy(xform);
        }

        public Snap.NX.Point Copy(Transform xform)
        {
            return (NXOpen.Point) base.Copy(xform);
        }

        public static Snap.NX.Point[] Copy(params Snap.NX.Point[] original)
        {
            Snap.NX.Point[] pointArray = new Snap.NX.Point[original.Length];
            for (int i = 0; i < original.Length; i++)
            {
                pointArray[i] = original[i].Copy();
            }
            return pointArray;
        }

        public static Snap.NX.Point[] Copy(Transform xform, params Snap.NX.Point[] original)
        {
            Snap.NX.Point[] pointArray = new Snap.NX.Point[original.Length];
            for (int i = 0; i < original.Length; i++)
            {
                pointArray[i] = original[i].Copy(xform);
            }
            return pointArray;
        }

        internal static Snap.NX.Point CreatePoint(double x, double y, double z)
        {
            Tag tag;
            UFSession uFSession = Globals.UFSession;
            double[] numArray = new double[] { x, y, z };
            uFSession.Curve.CreatePoint(numArray, out tag);
            return new Snap.NX.Point((NXOpen.Point) NXObjectManager.Get(tag));
        }

        internal static NXOpen.Point CreatePointInvisible(Snap.Position p)
        {
            return CreatePointInvisible(p.X, p.Y, p.Z);
        }

        internal static NXOpen.Point CreatePointInvisible(double x, double y, double z)
        {
            NXOpen.Part workPart = (NXOpen.Part) Globals.WorkPart;
            Point3d coordinates = new Point3d(x, y, z);
            NXOpen.Point point = workPart.Points.CreatePoint(coordinates);
            point.SetVisibility(SmartObject.VisibilityOption.Invisible);
            return point;
        }

        public static implicit operator Snap.NX.Point(NXOpen.Point point)
        {
            return new Snap.NX.Point(point);
        }

        public static implicit operator NXOpen.Point(Snap.NX.Point point)
        {
            return point.NXOpenPoint;
        }

        public static Snap.NX.Point Wrap(Tag nxopenPointTag)
        {
            if (nxopenPointTag == Tag.Null)
            {
                throw new ArgumentException("Input tag is NXOpen.Tag.Null");
            }
            NXOpen.Point objectFromTag = Snap.NX.NXObject.GetObjectFromTag(nxopenPointTag) as NXOpen.Point;
            if (objectFromTag == null)
            {
                throw new ArgumentException("Input tag doesn't belong to an NXOpen.Point object");
            }
            return objectFromTag;
        }

        public NXOpen.Point NXOpenPoint
        {
            get
            {
                return (NXOpen.Point) base.NXOpenTaggedObject;
            }
            private set
            {
                base.NXOpenTaggedObject = value;
            }
        }

        public Snap.Position Position
        {
            get
            {
                return new Snap.Position { X = this.NXOpenPoint.Coordinates.X, Y = this.NXOpenPoint.Coordinates.Y, Z = this.NXOpenPoint.Coordinates.Z };
            }
            set
            {
                double x = value.X;
                double y = value.Y;
                double z = value.Z;
                this.NXOpenPoint.SetCoordinates(new Point3d(x, y, z));
            }
        }

        public Snap.NX.Point Prototype
        {
            get
            {
                Tag protoTagFromOccTag = base.GetProtoTagFromOccTag(base.NXOpenTag);
                Snap.NX.Point point = null;
                if (protoTagFromOccTag != Tag.Null)
                {
                    point = Wrap(protoTagFromOccTag);
                }
                return point;
            }
        }

        public double X
        {
            get
            {
                return this.NXOpenPoint.Coordinates.X;
            }
            set
            {
                double y = this.NXOpenPoint.Coordinates.Y;
                double z = this.NXOpenPoint.Coordinates.Z;
                this.NXOpenPoint.SetCoordinates(new Point3d(value, y, z));
            }
        }

        public double Y
        {
            get
            {
                return this.NXOpenPoint.Coordinates.Y;
            }
            set
            {
                double x = this.NXOpenPoint.Coordinates.X;
                double z = this.NXOpenPoint.Coordinates.Z;
                this.NXOpenPoint.SetCoordinates(new Point3d(x, value, z));
            }
        }

        public double Z
        {
            get
            {
                return this.NXOpenPoint.Coordinates.Z;
            }
            set
            {
                double x = this.NXOpenPoint.Coordinates.X;
                double y = this.NXOpenPoint.Coordinates.Y;
                this.NXOpenPoint.SetCoordinates(new Point3d(x, y, value));
            }
        }
    }
}

