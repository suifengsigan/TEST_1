namespace Snap.NX
{
    using NXOpen;
    using Snap;
    using Snap.Geom;
    using System;

    public class Line : Snap.NX.Curve
    {
        internal Line(NXOpen.Line line) : base(line)
        {
            this.NXOpenLine = line;
        }

        public Snap.NX.Line Copy()
        {
            Transform xform = Transform.CreateTranslation(0.0, 0.0, 0.0);
            return this.Copy(xform);
        }

        public Snap.NX.Line Copy(Transform xform)
        {
            return (NXOpen.Line) base.Copy(xform);
        }

        public static Snap.NX.Line[] Copy(params Snap.NX.Line[] original)
        {
            Snap.NX.Line[] lineArray = new Snap.NX.Line[original.Length];
            for (int i = 0; i < original.Length; i++)
            {
                lineArray[i] = original[i].Copy();
            }
            return lineArray;
        }

        public static Snap.NX.Line[] Copy(Transform xform, params Snap.NX.Line[] original)
        {
            Snap.NX.Line[] lineArray = new Snap.NX.Line[original.Length];
            for (int i = 0; i < original.Length; i++)
            {
                lineArray[i] = original[i].Copy(xform);
            }
            return lineArray;
        }

        internal static Snap.NX.Line CreateLine(double x0, double y0, double z0, double x1, double y1, double z1)
        {
            Point3d startPoint = new Point3d(x0, y0, z0);
            Point3d endPoint = new Point3d(x1, y1, z1);
            return new Snap.NX.Line(Globals.NXOpenWorkPart.Curves.CreateLine(startPoint, endPoint));
        }

        public Snap.NX.Line[] Divide(params double[] parameters)
        {
            Snap.NX.Curve[] curveArray = base.Divide(parameters);
            return this.LineArray(curveArray);
        }

        public Snap.NX.Line[] Divide(Surface.Plane geomPlane, Position helpPoint)
        {
            Snap.NX.Curve[] curveArray = base.Divide(geomPlane, helpPoint);
            return this.LineArray(curveArray);
        }

        public Snap.NX.Line[] Divide(Snap.NX.Face face, Position helpPoint)
        {
            Snap.NX.Curve[] curveArray = base.Divide(face, helpPoint);
            return this.LineArray(curveArray);
        }

        public Snap.NX.Line[] Divide(Snap.NX.ICurve boundingCurve, Position helpPoint)
        {
            Snap.NX.Curve[] curveArray = base.Divide(boundingCurve, helpPoint);
            return this.LineArray(curveArray);
        }

        private Snap.NX.Line[] LineArray(Snap.NX.Curve[] curveArray)
        {
            Snap.NX.Line[] lineArray = new Snap.NX.Line[curveArray.Length];
            for (int i = 0; i < curveArray.Length; i++)
            {
                lineArray[i] = (NXOpen.Line) curveArray[i].NXOpenTaggedObject;
            }
            return lineArray;
        }

        public static implicit operator Snap.NX.Line(NXOpen.Line line)
        {
            return new Snap.NX.Line(line);
        }

        public static implicit operator NXOpen.Line(Snap.NX.Line line)
        {
            return (NXOpen.Line) line.NXOpenTaggedObject;
        }

        public static Snap.NX.Line Wrap(Tag nxopenLineTag)
        {
            if (nxopenLineTag == Tag.Null)
            {
                throw new ArgumentException("Input tag is NXOpen.Tag.Null");
            }
            NXOpen.Line objectFromTag = Snap.NX.NXObject.GetObjectFromTag(nxopenLineTag) as NXOpen.Line;
            if (objectFromTag == null)
            {
                throw new ArgumentException("Input tag doesn't belong to an NXOpen.Line object");
            }
            return objectFromTag;
        }

        public Vector Direction
        {
            get
            {
                return Vector.Unit((Vector) (base.EndPoint - base.StartPoint));
            }
        }

        public Position EndPoint
        {
            get
            {
                return this.NXOpenLine.EndPoint;
            }
            set
            {
                Session.UndoMarkId undoMark = Globals.Session.SetUndoMark(Session.MarkVisibility.Invisible, "");
                this.NXOpenLine.SetEndPoint((Point3d) value);
                Globals.Session.UpdateManager.DoUpdate(undoMark);
                Globals.Session.DeleteUndoMark(undoMark, null);
            }
        }

        public NXOpen.Line NXOpenLine
        {
            get
            {
                return (NXOpen.Line) base.NXOpenTaggedObject;
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
                int num = (int)(((int)base.ObjectSubType) % 100);
                if (num == 1)
                {
                    return ObjectTypes.SubType.LineGeneral;
                }
                return ObjectTypes.SubType.LineInfinite;
            }
        }

        public Snap.NX.Line Prototype
        {
            get
            {
                Tag protoTagFromOccTag = base.GetProtoTagFromOccTag(base.NXOpenTag);
                Snap.NX.Line line = null;
                if (protoTagFromOccTag != Tag.Null)
                {
                    line = Wrap(protoTagFromOccTag);
                }
                return line;
            }
        }

        public Position StartPoint
        {
            get
            {
                return this.NXOpenLine.StartPoint;
            }
            set
            {
                Session.UndoMarkId undoMark = Globals.Session.SetUndoMark(Session.MarkVisibility.Invisible, "");
                this.NXOpenLine.SetStartPoint((Point3d) value);
                Globals.Session.UpdateManager.DoUpdate(undoMark);
                Globals.Session.DeleteUndoMark(undoMark, null);
            }
        }
    }
}

