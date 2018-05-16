namespace Snap.NX
{
    using NXOpen;
    using Snap;
    using Snap.Geom;
    using System;

    public class Arc : Snap.NX.Curve
    {
        internal Arc(NXOpen.Arc arc) : base(arc)
        {
            this.NXOpenArc = arc;
        }

        private Snap.NX.Arc[] ArcArray(Snap.NX.Curve[] curveArray)
        {
            Snap.NX.Arc[] arcArray = new Snap.NX.Arc[curveArray.Length];
            for (int i = 0; i < curveArray.Length; i++)
            {
                arcArray[i] = (Snap.NX.Arc) curveArray[i];
            }
            return arcArray;
        }

        public Snap.NX.Arc Copy()
        {
            Transform xform = Transform.CreateTranslation(0.0, 0.0, 0.0);
            return this.Copy(xform);
        }

        public Snap.NX.Arc Copy(Transform xform)
        {
            TaggedObject obj3 = (TaggedObject) base.Copy(xform).NXOpenTaggedObject;
            NXOpen.Ellipse ellipse = obj3 as NXOpen.Ellipse;
            if (ellipse != null)
            {
                Snap.NX.NXObject.Delete(new Snap.NX.NXObject[] { ellipse });
                throw new ArgumentException("The transform would convert the arc to an ellipse. Please use Curve.Copy instead");
            }
            return (NXOpen.Arc) obj3;
        }

        public static Snap.NX.Arc[] Copy(params Snap.NX.Arc[] original)
        {
            Snap.NX.Arc[] arcArray = new Snap.NX.Arc[original.Length];
            for (int i = 0; i < original.Length; i++)
            {
                arcArray[i] = original[i].Copy();
            }
            return arcArray;
        }

        public static Snap.NX.Arc[] Copy(Transform xform, params Snap.NX.Arc[] original)
        {
            Snap.NX.Arc[] arcArray = new Snap.NX.Arc[original.Length];
            for (int i = 0; i < original.Length; i++)
            {
                arcArray[i] = original[i].Copy(xform);
            }
            return arcArray;
        }

        internal static Snap.NX.Arc CreateArc(Position startPoint, Position pointOn, Position endPoint)
        {
            bool flag;
            return new Snap.NX.Arc(Globals.NXOpenWorkPart.Curves.CreateArc((Point3d) startPoint, (Point3d) pointOn, (Point3d) endPoint, false, out flag));
        }

        internal static Snap.NX.Arc CreateArc(Position center, Vector axisX, Vector axisY, double radius, double angle1, double angle2)
        {
            double num = 0.017453292519943295;
            double startAngle = angle1 * num;
            double endAngle = angle2 * num;
            Snap.Orientation rotation = new Snap.Orientation(axisX, axisY);
            Snap.NX.Matrix matrix = new Snap.NX.Matrix(rotation);
            return new Snap.NX.Arc(Globals.NXOpenWorkPart.Curves.CreateArc((Point3d) center, (NXMatrix) matrix, radius, startAngle, endAngle));
        }

        internal static Snap.NX.Arc CreateArcFillet(Position p0, Position pa, Position p1, double radius)
        {
            Snap.Geom.Curve.Arc arc = Snap.Geom.Curve.Arc.Fillet(p0, pa, p1, radius);
            return CreateArc(arc.Center, arc.AxisX, arc.AxisY, arc.Radius, arc.StartAngle, arc.EndAngle);
        }

        internal static Snap.NX.Arc CreateArcFillet(Snap.NX.Curve curve1, Snap.NX.Curve curve2, double radius, Position center, bool doTrim)
        {
            Tag tag;
            Tag[] tagArray = new Tag[] { curve1.NXOpenTag, curve2.NXOpenTag };
            double[] array = center.Array;
            int[] numArray2 = new int[3];
            int[] numArray3 = new int[3];
            if (doTrim)
            {
                numArray2[0] = 1;
                numArray2[1] = 1;
            }
            else
            {
                numArray2[0] = 0;
                numArray2[1] = 0;
            }
            Globals.UFSession.Curve.CreateFillet(0, tagArray, array, radius, numArray2, numArray3, out tag);
            return new Snap.NX.Arc((NXOpen.Arc) Snap.NX.NXObject.GetObjectFromTag(tag));
        }

        public Snap.NX.Arc[] Divide(params double[] parameters)
        {
            Snap.NX.Curve[] curveArray = base.Divide(parameters);
            return this.ArcArray(curveArray);
        }

        public Snap.NX.Arc[] Divide(Surface.Plane geomPlane, Position helpPoint)
        {
            Snap.NX.Curve[] curveArray = base.Divide(geomPlane, helpPoint);
            return this.ArcArray(curveArray);
        }

        public Snap.NX.Arc[] Divide(Snap.NX.Face face, Position helpPoint)
        {
            Snap.NX.Curve[] curveArray = base.Divide(face, helpPoint);
            return this.ArcArray(curveArray);
        }

        public Snap.NX.Arc[] Divide(Snap.NX.ICurve boundingCurve, Position helpPoint)
        {
            Snap.NX.Curve[] curveArray = base.Divide(boundingCurve, helpPoint);
            return this.ArcArray(curveArray);
        }

        public static implicit operator Snap.NX.Arc(NXOpen.Arc arc)
        {
            return new Snap.NX.Arc(arc);
        }

        public static implicit operator NXOpen.Arc(Snap.NX.Arc arc)
        {
            return (NXOpen.Arc) arc.NXOpenTaggedObject;
        }

        public override void Trim(double lowerParam, double upperParam)
        {
            this.StartAngle = lowerParam;
            this.EndAngle = upperParam;
        }

        public static Snap.NX.Arc Wrap(Tag nxopenArcTag)
        {
            if (nxopenArcTag == Tag.Null)
            {
                throw new ArgumentException("Input tag is NXOpen.Tag.Null");
            }
            if (Snap.NX.NXObject.GetTypeFromTag(nxopenArcTag) != ObjectTypes.Type.Arc)
            {
                throw new ArgumentException("Input tag doesn't belong to an NXOpen.Arc object");
            }
            return new Snap.NX.Arc((NXOpen.Arc) Snap.NX.NXObject.GetObjectFromTag(nxopenArcTag));
        }

        public Vector AxisX
        {
            get
            {
                Vector3d vectord;
                Vector3d vectord2;
                Point3d pointd;
                this.NXOpenArc.GetOrientation(out pointd, out vectord, out vectord2);
                return vectord;
            }
        }

        public Vector AxisY
        {
            get
            {
                Vector3d vectord;
                Vector3d vectord2;
                Point3d pointd;
                this.NXOpenArc.GetOrientation(out pointd, out vectord, out vectord2);
                return vectord2;
            }
        }

        public Vector AxisZ
        {
            get
            {
                return Vector.Cross(this.AxisX, this.AxisY);
            }
        }

        public Position Center
        {
            get
            {
                return this.NXOpenArc.CenterPoint;
            }
            set
            {
                Vector3d vectord;
                Vector3d vectord2;
                Point3d pointd;
                Session.UndoMarkId undoMark = Globals.Session.SetUndoMark(Session.MarkVisibility.Invisible, "");
                this.NXOpenArc.GetOrientation(out pointd, out vectord, out vectord2);
                this.NXOpenArc.SetOrientation((Point3d) value, vectord, vectord2);
                Globals.Session.UpdateManager.DoUpdate(undoMark);
                Globals.Session.DeleteUndoMark(undoMark, null);
            }
        }

        public double Diameter
        {
            get
            {
                return (this.Radius * 2.0);
            }
            set
            {
                this.Radius = 0.5 * value;
            }
        }

        public double EndAngle
        {
            get
            {
                return (this.NXOpenArc.EndAngle * this.Factor);
            }
            set
            {
                Session.UndoMarkId undoMark = Globals.Session.SetUndoMark(Session.MarkVisibility.Visible, "");
                this.NXOpenArc.SetParameters(this.Radius, (Point3d) this.Center, this.StartAngle / this.Factor, value / this.Factor);
                Globals.Session.UpdateManager.DoUpdate(undoMark);
                Globals.Session.DeleteUndoMark(undoMark, "");
            }
        }

        internal override double Factor
        {
            get
            {
                return 57.295779513082323;
            }
        }

        public Snap.Geom.Curve.Arc Geometry
        {
            get
            {
                Vector3d vectord;
                Vector3d vectord2;
                Point3d pointd;
                this.NXOpenArc.GetOrientation(out pointd, out vectord, out vectord2);
                double startAngle = (this.NXOpenArc.StartAngle * 180.0) / 3.1415926535897931;
                return new Snap.Geom.Curve.Arc(pointd, vectord, vectord2, this.NXOpenArc.Radius, startAngle, (this.NXOpenArc.EndAngle * 180.0) / 3.1415926535897931);
            }
            set
            {
                Session.UndoMarkId undoMark = Globals.Session.SetUndoMark(Session.MarkVisibility.Invisible, "");
                Vector3d axisX = (Vector3d) value.AxisX;
                Vector3d axisY = (Vector3d) value.AxisY;
                Point3d center = (Point3d) value.Center;
                this.NXOpenArc.SetOrientation(center, axisX, axisY);
                double startAngle = (value.StartAngle * 3.1415926535897931) / 180.0;
                double endAngle = (value.EndAngle * 3.1415926535897931) / 180.0;
                this.NXOpenArc.SetParameters(value.Radius, center, startAngle, endAngle);
                Globals.Session.UpdateManager.DoUpdate(undoMark);
                Globals.Session.DeleteUndoMark(undoMark, null);
            }
        }

        public Snap.NX.Matrix Matrix
        {
            get
            {
                return this.NXOpenArc.Matrix;
            }
        }

        public NXOpen.Arc NXOpenArc
        {
            get
            {
                return (NXOpen.Arc) base.NXOpenTaggedObject;
            }
            private set
            {
                base.NXOpenTaggedObject = value;
            }
        }

        public Snap.Orientation Orientation
        {
            get
            {
                return this.Matrix.Orientation;
            }
        }

        public Snap.NX.Arc Prototype
        {
            get
            {
                Tag protoTagFromOccTag = base.GetProtoTagFromOccTag(base.NXOpenTag);
                Snap.NX.Arc arc = null;
                if (protoTagFromOccTag != Tag.Null)
                {
                    arc = Wrap(protoTagFromOccTag);
                }
                return arc;
            }
        }

        public double Radius
        {
            get
            {
                return this.NXOpenArc.Radius;
            }
            set
            {
                Session.UndoMarkId undoMark = Globals.Session.SetUndoMark(Session.MarkVisibility.Invisible, "");
                this.NXOpenArc.SetRadius(value);
                Globals.Session.UpdateManager.DoUpdate(undoMark);
                Globals.Session.DeleteUndoMark(undoMark, null);
            }
        }

        public double StartAngle
        {
            get
            {
                return (this.NXOpenArc.StartAngle * this.Factor);
            }
            set
            {
                Session.UndoMarkId undoMark = Globals.Session.SetUndoMark(Session.MarkVisibility.Visible, "");
                this.NXOpenArc.SetParameters(this.Radius, (Point3d) this.Center, value / this.Factor, this.EndAngle / this.Factor);
                Globals.Session.UpdateManager.DoUpdate(undoMark);
                Globals.Session.DeleteUndoMark(undoMark, "");
            }
        }
    }
}

