namespace Snap.NX
{
    using NXOpen;
    using NXOpen.Features;
    using NXOpen.UF;
    using Snap;
    using Snap.Geom;
    using System;

    public class Torus : Snap.NX.Revolve
    {
        internal Torus(NXOpen.Features.Revolve torus) : base(torus)
        {
        }

        internal static Snap.NX.Torus CreateTorus(Snap.Position axisPoint, Vector axisVector, double majorRadius, double minorRadius)
        {
            Snap.Orientation orientation = new Snap.Orientation(axisVector);
            Snap.Position center = axisPoint + ((Snap.Position) (majorRadius * orientation.AxisX));
            Snap.NX.Arc arc = Create.Circle(center, orientation.AxisY, minorRadius);
            Snap.NX.ICurve[] curves = new Snap.NX.ICurve[] { arc };
            return new Snap.NX.Torus(Create.Revolve(curves, axisPoint, axisVector).NXOpenRevolve);
        }

        internal static Snap.NX.Torus CreateTorus(Snap.Position center, Snap.Orientation matrix, double majorRadius, double minorRadius, Box2d boxUV)
        {
            Vector axisX = matrix.AxisX;
            Vector axisZ = matrix.AxisZ;
            Snap.Orientation orientation = new Snap.Orientation(axisX, axisZ);
            Snap.Position position = center + ((Snap.Position) (majorRadius * matrix.AxisX));
            Snap.NX.Arc arc = Create.Arc(position, orientation, minorRadius, boxUV.MinV, boxUV.MaxV);
            Snap.NX.ICurve[] curves = new Snap.NX.ICurve[] { arc };
            double x = matrix.AxisZ.X;
            double y = matrix.AxisZ.Y;
            double z = matrix.AxisZ.Z;
            string str = Snap.Number.ToString(boxUV.MinU);
            string str2 = Snap.Number.ToString(boxUV.MaxU);
            Snap.Number[] angles = new Snap.Number[] { str, str2 };
            return new Snap.NX.Torus(Create.RevolveSheet(curves, center, matrix.AxisZ, angles).NXOpenRevolve);
        }

        public Snap.Position AxisPoint
        {
            get
            {
                int num;
                double num2;
                double num3;
                int num4;
                BodyFeature feature = (BodyFeature) this;
                UFSession uFSession = Globals.UFSession;
                NXOpen.Face[] faces = feature.GetFaces();
                double[] point = new double[3];
                double[] dir = new double[3];
                double[] box = new double[6];
                uFSession.Modl.AskFaceData(faces[0].Tag, out num, point, dir, box, out num2, out num3, out num4);
                return new Snap.Position(point);
            }
        }

        public Vector AxisVector
        {
            get
            {
                int num;
                double num2;
                double num3;
                int num4;
                BodyFeature feature = (BodyFeature) this;
                UFSession uFSession = Globals.UFSession;
                NXOpen.Face[] faces = feature.GetFaces();
                double[] point = new double[3];
                double[] dir = new double[3];
                double[] box = new double[6];
                uFSession.Modl.AskFaceData(faces[0].Tag, out num, point, dir, box, out num2, out num3, out num4);
                return new Vector(dir);
            }
        }

        public double MajorRadius
        {
            get
            {
                int num;
                double num2;
                double num3;
                int num4;
                BodyFeature feature = (BodyFeature) this;
                UFSession uFSession = Globals.UFSession;
                NXOpen.Face[] faces = feature.GetFaces();
                double[] point = new double[3];
                double[] dir = new double[3];
                double[] box = new double[6];
                uFSession.Modl.AskFaceData(faces[0].Tag, out num, point, dir, box, out num2, out num3, out num4);
                return num2;
            }
        }

        public double MinorRadius
        {
            get
            {
                int num;
                double num2;
                double num3;
                int num4;
                BodyFeature feature = (BodyFeature) this;
                UFSession uFSession = Globals.UFSession;
                NXOpen.Face[] faces = feature.GetFaces();
                double[] point = new double[3];
                double[] dir = new double[3];
                double[] box = new double[6];
                uFSession.Modl.AskFaceData(faces[0].Tag, out num, point, dir, box, out num2, out num3, out num4);
                return num3;
            }
        }
    }
}

