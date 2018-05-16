namespace Snap.NX
{
    using NXOpen;
    using NXOpen.UF;
    using Snap;
    using Snap.Geom;
    using System;

    public class Ellipse : Snap.NX.Curve
    {
        internal Ellipse(NXOpen.Ellipse ellipse) : base(ellipse)
        {
            this.NXOpenEllipse = ellipse;
        }

        public Snap.NX.Ellipse Copy()
        {
            Transform xform = Transform.CreateTranslation(0.0, 0.0, 0.0);
            return this.Copy(xform);
        }

        public Snap.NX.Ellipse Copy(Transform xform)
        {
            TaggedObject obj3 = (TaggedObject) base.Copy(xform).NXOpenTaggedObject;
            NXOpen.Arc arc = obj3 as NXOpen.Arc;
            if (arc != null)
            {
                Snap.NX.NXObject.Delete(new Snap.NX.NXObject[] { arc });
                throw new ArgumentException("The transform would convert the ellipse to an arc. Please use Curve.Copy instead");
            }
            return (NXOpen.Ellipse) obj3;
        }

        public static Snap.NX.Ellipse[] Copy(params Snap.NX.Ellipse[] original)
        {
            Snap.NX.Ellipse[] ellipseArray = new Snap.NX.Ellipse[original.Length];
            for (int i = 0; i < original.Length; i++)
            {
                ellipseArray[i] = original[i].Copy();
            }
            return ellipseArray;
        }

        public static Snap.NX.Ellipse[] Copy(Transform xform, params Snap.NX.Ellipse[] original)
        {
            Snap.NX.Ellipse[] ellipseArray = new Snap.NX.Ellipse[original.Length];
            for (int i = 0; i < original.Length; i++)
            {
                ellipseArray[i] = original[i].Copy(xform);
            }
            return ellipseArray;
        }

        internal static Snap.NX.Ellipse CreateEllipse(Snap.Position center, Vector axisX, Vector axisY, double a, double b, double angle1, double angle2)
        {
            NXOpen.Part workPart = (NXOpen.Part) Globals.WorkPart;
            double num = 0.017453292519943295;
            angle1 *= num;
            angle2 *= num;
            return new Snap.NX.Ellipse(workPart.Curves.CreateEllipse((Point3d) center, (Vector3d) axisX, (Vector3d) axisY, a, b, angle1, angle2));
        }

        internal static Snap.NX.Ellipse CreateEllipse(Snap.Position center, Vector axisX, Vector axisY, double rotationAngle, double a, double b, double angle1, double angle2)
        {
            NXOpen.Part workPart = (NXOpen.Part) Globals.WorkPart;
            double num = 0.017453292519943295;
            angle1 *= num;
            angle2 *= num;
            rotationAngle *= num;
            Vector axisZ = Vector.UnitCross(axisX, axisY);
            Snap.NX.Matrix matrix = Snap.NX.Matrix.CreateMatrix(axisX, axisY, axisZ);
            return new Snap.NX.Ellipse(workPart.Curves.CreateEllipse((Point3d) center, a, b, angle1, angle2, rotationAngle, (NXMatrix) matrix));
        }

        public Snap.NX.Ellipse[] Divide(params double[] parameters)
        {
            Snap.NX.Curve[] curveArray = base.Divide(parameters);
            return this.EllipseArray(curveArray);
        }

        public Snap.NX.Ellipse[] Divide(Surface.Plane geomPlane, Snap.Position helpPoint)
        {
            Snap.NX.Curve[] curveArray = base.Divide(geomPlane, helpPoint);
            return this.EllipseArray(curveArray);
        }

        public Snap.NX.Ellipse[] Divide(Snap.NX.Face face, Snap.Position helpPoint)
        {
            Snap.NX.Curve[] curveArray = base.Divide(face, helpPoint);
            return this.EllipseArray(curveArray);
        }

        public Snap.NX.Ellipse[] Divide(Snap.NX.ICurve boundingCurve, Snap.Position helpPoint)
        {
            Snap.NX.Curve[] curveArray = base.Divide(boundingCurve, helpPoint);
            return this.EllipseArray(curveArray);
        }

        private Snap.NX.Ellipse[] EllipseArray(Snap.NX.Curve[] curveArray)
        {
            Snap.NX.Ellipse[] ellipseArray = new Snap.NX.Ellipse[curveArray.Length];
            for (int i = 0; i < curveArray.Length; i++)
            {
                ellipseArray[i] = (NXOpen.Ellipse) curveArray[i].NXOpenTaggedObject;
            }
            return ellipseArray;
        }

        public static implicit operator Snap.NX.Ellipse(NXOpen.Ellipse ellipse)
        {
            return new Snap.NX.Ellipse(ellipse);
        }

        public static implicit operator NXOpen.Ellipse(Snap.NX.Ellipse ellipse)
        {
            return (NXOpen.Ellipse) ellipse.NXOpenTaggedObject;
        }

        public override void Trim(double lowerParam, double upperParam)
        {
            this.StartAngle = lowerParam;
            this.EndAngle = upperParam;
        }

        public static Snap.NX.Ellipse Wrap(Tag nxopenEllipseTag)
        {
            if (nxopenEllipseTag == Tag.Null)
            {
                throw new ArgumentException("Input tag is NXOpen.Tag.Null");
            }
            NXOpen.Ellipse objectFromTag = Snap.NX.NXObject.GetObjectFromTag(nxopenEllipseTag) as NXOpen.Ellipse;
            if (objectFromTag == null)
            {
                throw new ArgumentException("Input tag doesn't belong to an NXOpen.Ellipse object");
            }
            return objectFromTag;
        }

        public Vector AxisX
        {
            get
            {
                Vector3d vectord;
                Vector3d vectord2;
                Point3d pointd;
                this.NXOpenEllipse.GetOrientation(out pointd, out vectord, out vectord2);
                return Vector.Unit(vectord);
            }
        }

        public Vector AxisY
        {
            get
            {
                Vector3d vectord;
                Vector3d vectord2;
                Point3d pointd;
                this.NXOpenEllipse.GetOrientation(out pointd, out vectord, out vectord2);
                return Vector.Unit(vectord2);
            }
        }

        public Vector AxisZ
        {
            get
            {
                return Vector.UnitCross(this.AxisX, this.AxisY);
            }
        }

        public Snap.Position Center
        {
            get
            {
                return this.NXOpenEllipse.CenterPoint;
            }
            set
            {
                this.NXOpenEllipse.SetOrientation((Point3d) value, (Vector3d) this.Orientation.AxisX, (Vector3d) this.Orientation.AxisY);
            }
        }

        public double EndAngle
        {
            get
            {
                return (this.NXOpenEllipse.EndAngle * this.Factor);
            }
            set
            {
                UFCurve.Conic conic;
                Globals.UFSession.Curve.AskConicData(base.NXOpenTag, out conic);
                conic.end_param = value / this.Factor;
                Globals.UFSession.Curve.EditConicData(base.NXOpenTag, ref conic);
            }
        }

        internal override double Factor
        {
            get
            {
                return 57.295779513082323;
            }
        }

        public Snap.NX.Matrix Matrix
        {
            get
            {
                return this.NXOpenEllipse.Matrix;
            }
        }

        public NXOpen.Ellipse NXOpenEllipse
        {
            get
            {
                return (NXOpen.Ellipse) base.NXOpenTaggedObject;
            }
            private set
            {
                base.NXOpenTaggedObject = value;
            }
        }

        public ObjectTypes.SubType ObjectSubType
        {
            get
            {
                return ObjectTypes.SubType.ConicEllipse;
            }
        }

        public Snap.Orientation Orientation
        {
            get
            {
                return this.Matrix.Orientation;
            }
        }

        public Snap.NX.Ellipse Prototype
        {
            get
            {
                Tag protoTagFromOccTag = base.GetProtoTagFromOccTag(base.NXOpenTag);
                Snap.NX.Ellipse ellipse = null;
                if (protoTagFromOccTag != Tag.Null)
                {
                    ellipse = Wrap(protoTagFromOccTag);
                }
                return ellipse;
            }
        }

        public double RadiusX
        {
            get
            {
                return this.NXOpenEllipse.MajorRadius;
            }
        }

        public double RadiusY
        {
            get
            {
                return this.NXOpenEllipse.MinorRadius;
            }
        }

        public double StartAngle
        {
            get
            {
                return (this.NXOpenEllipse.StartAngle * this.Factor);
            }
            set
            {
                UFCurve.Conic conic;
                Globals.UFSession.Curve.AskConicData(base.NXOpenTag, out conic);
                conic.start_param = value / this.Factor;
                Globals.UFSession.Curve.EditConicData(base.NXOpenTag, ref conic);
            }
        }
    }
}

