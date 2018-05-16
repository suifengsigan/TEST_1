namespace Snap.NX
{
    using NXOpen;
    using NXOpen.UF;
    using Snap;
    using Snap.Geom;
    using System;

    public class CoordinateSystem : Snap.NX.NXObject
    {
        internal CoordinateSystem(NXOpen.CoordinateSystem csys) : base(csys)
        {
            base.NXOpenTaggedObject = csys;
        }

        public Snap.NX.CoordinateSystem Copy()
        {
            Transform xform = Transform.CreateTranslation(0.0, 0.0, 0.0);
            return this.Copy(xform);
        }

        public Snap.NX.CoordinateSystem Copy(Transform xform)
        {
            return (NXOpen.CoordinateSystem) base.Copy(xform);
        }

        internal static Snap.NX.CoordinateSystem CreateCoordinateSystem(Snap.Position origin, Snap.NX.Matrix matrix)
        {
            Tag tag2;
            UFSession uFSession = Globals.UFSession;
            double[] array = origin.Array;
            Tag nXOpenTag = matrix.NXOpenTag;
            uFSession.Csys.CreateCsys(array, nXOpenTag, out tag2);
            return (NXOpen.CoordinateSystem) Snap.NX.NXObject.GetObjectFromTag(tag2);
        }

        internal static NXOpen.CoordinateSystem CreateCoordinateSystem(Snap.Position origin, Snap.Orientation matrix)
        {
            return (NXOpen.CoordinateSystem) CreateCoordinateSystem(origin, matrix.AxisX, matrix.AxisY, matrix.AxisZ);
        }

        internal static Snap.NX.CoordinateSystem CreateCoordinateSystem(Snap.Position origin, Vector axisX, Vector axisY, Vector axisZ)
        {
            Tag tag2;
            UFSession uFSession = Globals.UFSession;
            double[] array = origin.Array;
            Snap.NX.Matrix matrix = new Snap.NX.Matrix(axisX, axisY, axisZ);
            Tag nXOpenTag = matrix.NXOpenTag;
            uFSession.Csys.CreateCsys(array, nXOpenTag, out tag2);
            return (NXOpen.CoordinateSystem) Snap.NX.NXObject.GetObjectFromTag(tag2);
        }

        public static Snap.Position MapAcsToWcs(Snap.Position absCoords)
        {
            UFSession uFSession = Globals.UFSession;
            int num = 3;
            int num2 = 1;
            double[] numArray = new double[3];
            uFSession.Csys.MapPoint(num2, absCoords.Array, num, numArray);
            return new Snap.Position(numArray);
        }

        public static Vector MapAcsToWcs(Vector absVector)
        {
            Vector axisX = Globals.Wcs.AxisX;
            Vector axisY = Globals.Wcs.AxisY;
            Vector axisZ = Globals.Wcs.AxisZ;
            return new Vector((double) (absVector * axisX), (double) (absVector * axisY), (double) (absVector * axisZ));
        }

        public static Snap.Position MapCsysToCsys(Snap.Position inputCoords, Snap.NX.CoordinateSystem inputCsys, Snap.NX.CoordinateSystem outputCsys)
        {
            Snap.Position origin = inputCsys.Origin;
            Vector axisX = inputCsys.AxisX;
            Vector axisY = inputCsys.AxisY;
            Vector axisZ = inputCsys.AxisZ;
            Snap.Position position2 = (Snap.Position) (((origin + (inputCoords.X * axisX)) + (inputCoords.Y * axisY)) + (inputCoords.Z * axisZ));
            Vector vector4 = (Vector) (position2 - outputCsys.Origin);
            double x = (double) (vector4 * outputCsys.AxisX);
            double y = (double) (vector4 * outputCsys.AxisY);
            return new Snap.Position(x, y, (double) (vector4 * outputCsys.AxisZ));
        }

        public static Vector MapCsysToCsys(Vector inputVector, Snap.NX.CoordinateSystem inputCsys, Snap.NX.CoordinateSystem outputCsys)
        {
            Vector axisX = inputCsys.AxisX;
            Vector axisY = inputCsys.AxisY;
            Vector axisZ = inputCsys.AxisZ;
            Vector vector4 = (Vector) (((inputVector.X * axisX) + (inputVector.Y * axisY)) + (inputVector.Z * axisZ));
            double x = (double) (vector4 * outputCsys.AxisX);
            double y = (double) (vector4 * outputCsys.AxisY);
            return new Vector(x, y, (double) (vector4 * outputCsys.AxisZ));
        }

        public static Snap.Position MapWcsToAcs(Snap.Position workCoords)
        {
            UFSession uFSession = Globals.UFSession;
            int num = 3;
            int num2 = 1;
            double[] numArray = new double[3];
            uFSession.Csys.MapPoint(num, workCoords.Array, num2, numArray);
            return new Snap.Position(numArray);
        }

        public static Vector MapWcsToAcs(Vector workVector)
        {
            Vector axisX = Globals.Wcs.AxisX;
            Vector axisY = Globals.Wcs.AxisY;
            Vector axisZ = Globals.Wcs.AxisZ;
            return (Vector) (((workVector.X * axisX) + (workVector.Y * axisY)) + (workVector.Z * axisZ));
        }

        public static implicit operator Snap.NX.CoordinateSystem(NXOpen.CoordinateSystem csys)
        {
            return new Snap.NX.CoordinateSystem(csys);
        }

        public static implicit operator NXOpen.CoordinateSystem(Snap.NX.CoordinateSystem csys)
        {
            return (NXOpen.CoordinateSystem) csys.NXOpenTaggedObject;
        }

        public static Snap.NX.CoordinateSystem Wrap(Tag nxopenCoordinateSystemTag)
        {
            if (nxopenCoordinateSystemTag == Tag.Null)
            {
                throw new ArgumentException("Input tag is NXOpen.Tag.Null");
            }
            NXOpen.CoordinateSystem objectFromTag = Snap.NX.NXObject.GetObjectFromTag(nxopenCoordinateSystemTag) as NXOpen.CoordinateSystem;
            if (objectFromTag == null)
            {
                throw new ArgumentException("Input tag doesn't belong to an NXOpen.CoordinateSystem object");
            }
            return objectFromTag;
        }

        public Vector AxisX
        {
            get
            {
                return this.Matrix.AxisX;
            }
        }

        public Vector AxisY
        {
            get
            {
                return this.Matrix.AxisY;
            }
        }

        public Vector AxisZ
        {
            get
            {
                return this.Matrix.AxisZ;
            }
        }

        public Snap.NX.Matrix Matrix
        {
            get
            {
                return this.NXOpenCoordinateSystem.Orientation;
            }
        }

        public NXOpen.CoordinateSystem NXOpenCoordinateSystem
        {
            get
            {
                return (NXOpen.CoordinateSystem) base.NXOpenTaggedObject;
            }
        }

        public ObjectTypes.SubType ObjectSubType
        {
            get
            {
                switch (( ((int)base.ObjectSubType % (100))))
                {
                    case 0:
                        return ObjectTypes.SubType.CsysGeneral;

                    case 1:
                        return ObjectTypes.SubType.CsysWcs;

                    case 2:
                        return ObjectTypes.SubType.CsysCylindrical;
                }
                return ObjectTypes.SubType.CsysSpherical;
            }
        }

        public Snap.Orientation Orientation
        {
            get
            {
                return this.Matrix.Orientation;
            }
        }

        public Snap.Position Origin
        {
            get
            {
                return this.NXOpenCoordinateSystem.Origin;
            }
            set
            {
                this.NXOpenCoordinateSystem.Origin = (Point3d) value;
            }
        }

        public Snap.NX.CoordinateSystem Prototype
        {
            get
            {
                Tag protoTagFromOccTag = base.GetProtoTagFromOccTag(base.NXOpenTag);
                Snap.NX.CoordinateSystem system = null;
                if (protoTagFromOccTag != Tag.Null)
                {
                    system = Wrap(protoTagFromOccTag);
                }
                return system;
            }
        }
    }
}

