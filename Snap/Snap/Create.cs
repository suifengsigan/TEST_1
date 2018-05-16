namespace Snap
{
    using NXOpen;
    using NXOpen.Features;
    using NXOpen.GeometricUtilities;
    using Snap.Geom;
    using Snap.NX;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class Create
    {
        internal static Vector ApproxCurveDeriv(CurvePositionFunction f, object curveData, double t)
        {
            Position position;
            Position position2;
            Position position3;
            double num = 65536.0;
            double num2 = 1.0 / num;
            if (t < num2)
            {
                position = f(curveData, t);
                position2 = f(curveData, t + num2);
                position3 = f(curveData, t + (2.0 * num2));
                return (Vector) (((3 * (position2 - position)) - (position3 - position2)) / (2.0 * num2));
            }
            if (t > (1.0 - num2))
            {
                position = f(curveData, t - (2.0 * num2));
                position2 = f(curveData, t - num2);
                position3 = f(curveData, t);
                return (Vector) (((3 * (position3 - position2)) - (position2 - position)) / (2.0 * num2));
            }
            position = f(curveData, t - num2);
            return (Vector) ((f(curveData, t + num2) - position) / (2.0 * num2));
        }

        public static Snap.NX.Arc Arc(Position startPoint, Position throughPoint, Position endPoint)
        {
            return Snap.NX.Arc.CreateArc(startPoint, throughPoint, endPoint);
        }

        public static Snap.NX.Arc Arc(Position center, double radius, double angle1, double angle2)
        {
            Vector axisX = Vector.AxisX;
            Vector axisY = Vector.AxisY;
            return Snap.NX.Arc.CreateArc(center, axisX, axisY, radius, angle1, angle2);
        }

        public static Snap.NX.Arc Arc(Position center, Orientation matrix, double radius, double angle1, double angle2)
        {
            Vector axisX = matrix.AxisX;
            Vector axisY = matrix.AxisY;
            return Snap.NX.Arc.CreateArc(center, axisX, axisY, radius, angle1, angle2);
        }

        public static Snap.NX.Arc Arc(double cx, double cy, double radius, double angle1, double angle2)
        {
            Position center = new Position(cx, cy, 0.0);
            Vector axisX = Vector.AxisX;
            Vector axisY = Vector.AxisY;
            return Snap.NX.Arc.CreateArc(center, axisX, axisY, radius, angle1, angle2);
        }

        public static Snap.NX.Arc Arc(Position center, Vector axisX, Vector axisY, double radius, double angle1, double angle2)
        {
            return Snap.NX.Arc.CreateArc(center, axisX, axisY, radius, angle1, angle2);
        }

        public static Snap.NX.Spline BezierCurve(params Snap.NX.Point[] poles)
        {
            int length = poles.Length;
            Position[] positionArray = new Position[length];
            for (int i = 0; i < length; i++)
            {
                positionArray[i] = poles[i].Position;
            }
            return BezierCurve(positionArray);
        }

        public static Snap.NX.Spline BezierCurve(params Position[] poles)
        {
            int length = poles.Length;
            double[] weights = new double[length];
            for (int i = 0; i < length; i++)
            {
                weights[i] = 1.0;
            }
            return BezierCurve(poles, weights);
        }

        public static Snap.NX.Spline BezierCurve(Position[] poles, double[] weights)
        {
            int num2 = poles.Length;
            int m = num2 - 1;
            return new Snap.NX.Spline((NXOpen.Spline) Snap.NX.Spline.CreateSpline(Snap.Math.SplineMath.BezierKnots(m), poles, weights));
        }

        public static Snap.NX.Spline BezierCurve(Position p0, Vector u0, Position p1, Vector u1)
        {
            double num = Vector.Norm((Vector) (p1 - p0));
            Vector u = (Vector) ((p1 - p0) / num);
            double a = Vector.Angle(u, u0);
            double num3 = Vector.Angle(u, u1);
            double num4 = System.Math.Sin(a);
            double num5 = System.Math.Sin(num3);
            double num6 = System.Math.Cos(a);
            double num7 = System.Math.Cos(num3);
            double num8 = System.Math.Sqrt(2.0);
            double num9 = 0.0625;
            double num10 = (3.0 - System.Math.Sqrt(5.0)) / 2.0;
            double num11 = ((num8 * (num4 - (num9 * num5))) * (num5 - (num9 * num4))) * (num6 - num7);
            double num12 = (2.0 + num11) / ((1.0 + ((1.0 - num10) * num6)) + (num10 * num7));
            double num13 = (2.0 + num11) / ((1.0 + ((1.0 - num10) * num7)) + (num10 * num6));
            Position position = p0 + ((Position) (((num12 / 3.0) * num) * u0));
            Position position2 =(Position)(p1 - ((Position)(((num13 / 3.0) * num) * u1)));
            Position[] poles = new Position[] { p0, position, position2, p1 };
            return BezierCurve(poles);
        }

        public static Snap.NX.Spline BezierCurveFit(CurvePositionFunction f, object data, int m)
        {
            double[] numArray = ChebyshevConstants.Zeros(m);
            Position[] q = new Position[m - 1];
            for (int i = 0; i < (m - 1); i++)
            {
                q[i] = f(data, numArray[i]);
            }
            Vector u = ApproxCurveDeriv(f, data, 0.0);
            Vector v = ApproxCurveDeriv(f, data, 1.0);
            return ChebyshevApproximation(q, u, v);
        }

        public static Snap.NX.Spline BezierCurveThroughPoints(params Position[] intPoints)
        {
            double[] nodes = Snap.Math.SplineMath.ChordalNodes(intPoints);
            return BezierCurveThroughPoints(intPoints, nodes);
        }

        public static Snap.NX.Spline BezierCurveThroughPoints(Position[] intPoints, double[] nodes)
        {
            int num2 = intPoints.Length;
            int m = num2 - 1;
            double[] t = Snap.Math.SplineMath.BezierKnots(m);
            return Snap.NX.Spline.CreateSplineThroughPoints(intPoints, nodes, t);
        }

        public static Snap.NX.Bsurface BezierPatch(Position[,] poles)
        {
            int length = poles.GetLength(0);
            int num2 = poles.GetLength(1);
            double[,] weights = new double[length, num2];
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < num2; j++)
                {
                    weights[i, j] = 1.0;
                }
            }
            return BezierPatch(poles, weights);
        }

        public static Snap.NX.Bsurface BezierPatch(Position[,] poles, double[,] weights)
        {
            int m = poles.GetLength(0) - 1;
            int num2 = poles.GetLength(1) - 1;
            double[] knotsU = Snap.Math.SplineMath.BezierKnots(m);
            double[] knotsV = Snap.Math.SplineMath.BezierKnots(num2);
            return Snap.NX.Bsurface.CreateBsurface(poles, weights, knotsU, knotsV);
        }

        public static Snap.NX.Bsurface BezierPatchThroughPoints(Position[,] intPoints)
        {
            double[][] numArray = Snap.Math.SplineMath.ChordalNodes(intPoints);
            double[] nodesU = numArray[0];
            double[] nodesV = numArray[1];
            return BezierPatchThroughPoints(intPoints, nodesU, nodesV);
        }

        public static Snap.NX.Bsurface BezierPatchThroughPoints(Position[,] intPoints, double[] nodesU, double[] nodesV)
        {
            int m = intPoints.GetLength(0) - 1;
            int num2 = intPoints.GetLength(1) - 1;
            double[] knotsU = Snap.Math.SplineMath.BezierKnots(m);
            double[] knotsV = Snap.Math.SplineMath.BezierKnots(num2);
            return BsurfaceThroughPoints(intPoints, nodesU, nodesV, knotsU, knotsV);
        }

        public static Snap.NX.Bsurface BezierSpherePatch(Position center, Orientation matrix, double radius)
        {
            double num = Sqrt(2.0);
            double num2 = Sqrt(3.0);
            double num3 = 0.96592582628906831;
            double num4 = 0.33333333333333331;
            double num5 = 5.0 * num4;
            double[,] numArray5 = new double[5, 5];
            numArray5[0, 0] = 1.0 - num2;
            numArray5[0, 1] = -1.0 / (2.0 * num);
            numArray5[0, 3] = 1.0 / (2.0 * num);
            numArray5[0, 4] = -1.0 + num2;
            numArray5[1, 0] = -Sqrt(9.5 - (4.0 * num2)) / 2.0;
            numArray5[1, 1] = (num3 / 4.0) - ((num2 * (1.0 + (2.0 * num3))) / 8.0);
            numArray5[1, 3] = ((-2.0 * num3) + (num2 * (1.0 + (2.0 * num3)))) / 8.0;
            numArray5[1, 4] = Sqrt(9.5 - (4.0 * num2)) / 2.0;
            numArray5[2, 0] = num4 - (2.0 / num2);
            numArray5[2, 1] = (-1.0 + ((2.0 * (-3.0 + num2)) * num3)) / (6.0 * num);
            numArray5[2, 3] = (1.0 - ((2.0 * (-3.0 + num2)) * num3)) / (6.0 * num);
            numArray5[2, 4] = -num4 + (2.0 / num2);
            numArray5[3, 0] = -Sqrt(9.5 - (4.0 * num2)) / 2.0;
            numArray5[3, 1] = (num3 / 4.0) - ((num2 * (1.0 + (2.0 * num3))) / 8.0);
            numArray5[3, 3] = ((-2.0 * num3) + (num2 * (1.0 + (2.0 * num3)))) / 8.0;
            numArray5[3, 4] = Sqrt(9.5 - (4.0 * num2)) / 2.0;
            numArray5[4, 0] = 1.0 - num2;
            numArray5[4, 1] = -1.0 / (2.0 * num);
            numArray5[4, 3] = 1.0 / (2.0 * num);
            numArray5[4, 4] = -1.0 + num2;
            double[,] numArray = numArray5;
            double[,] numArray6 = new double[5, 5];
            numArray6[0, 0] = 1.0 - num2;
            numArray6[0, 1] = -Sqrt(9.5 - (4.0 * num2)) / 2.0;
            numArray6[0, 2] = num4 - (2.0 / num2);
            numArray6[0, 3] = -Sqrt(9.5 - (4.0 * num2)) / 2.0;
            numArray6[0, 4] = 1.0 - num2;
            numArray6[1, 0] = -1.0 / (2.0 * num);
            numArray6[1, 1] = (num3 / 4.0) - ((num2 * (1.0 + (2.0 * num3))) / 8.0);
            numArray6[1, 2] = (-1.0 + ((2.0 * (-3.0 + num2)) * num3)) / (6.0 * num);
            numArray6[1, 3] = (num3 / 4.0) - ((num2 * (1.0 + (2.0 * num3))) / 8.0);
            numArray6[1, 4] = -1.0 / (2.0 * num);
            numArray6[3, 0] = 1.0 / (2.0 * num);
            numArray6[3, 1] = ((-2.0 * num3) + (num2 * (1.0 + (2.0 * num3)))) / 8.0;
            numArray6[3, 2] = (1.0 - ((2.0 * (-3.0 + num2)) * num3)) / (6.0 * num);
            numArray6[3, 3] = ((-2.0 * num3) + (num2 * (1.0 + (2.0 * num3)))) / 8.0;
            numArray6[3, 4] = 1.0 / (2.0 * num);
            numArray6[4, 0] = -1.0 + num2;
            numArray6[4, 1] = Sqrt(9.5 - (4.0 * num2)) / 2.0;
            numArray6[4, 2] = -num4 + (2.0 / num2);
            numArray6[4, 3] = Sqrt(9.5 - (4.0 * num2)) / 2.0;
            numArray6[4, 4] = -1.0 + num2;
            double[,] numArray2 = numArray6;
            double[,] numArray3 = new double[,] { { -1.0 + num2, Sqrt(9.5 - (4.0 * num2)) / 2.0, -num4 + (2.0 / num2), Sqrt(9.5 - (4.0 * num2)) / 2.0, -1.0 + num2 }, { Sqrt(9.5 - (4.0 * num2)) / 2.0, ((2.0 + num2) + (4.0 * num3)) / 8.0, (((4.0 * num) + (19.0 * Sqrt(6.0))) + ((4.0 * Sqrt(2.0 + num2)) * (-5.0 + (3.0 * num3)))) / 36.0, ((2.0 + num2) + (4.0 * num3)) / 8.0, Sqrt(9.5 - (4.0 * num2)) / 2.0 }, { -num4 + (2.0 / num2), (((4.0 * num) + (19.0 * Sqrt(6.0))) + ((4.0 * Sqrt(2.0 + num2)) * (-5.0 + (3.0 * num3)))) / 36.0, ((11.0 - (3.0 * num2)) + ((4.0 * num3) * num3)) / 9.0, (((4.0 * num) + (19.0 * Sqrt(6.0))) + ((4.0 * Sqrt(2.0 + num2)) * (-5.0 + (3.0 * num3)))) / 36.0, -num4 + (2.0 / num2) }, { Sqrt(9.5 - (4.0 * num2)) / 2.0, ((2.0 + num2) + (4.0 * num3)) / 8.0, (((4.0 * num) + (19.0 * Sqrt(6.0))) + ((4.0 * Sqrt(2.0 + num2)) * (-5.0 + (3.0 * num3)))) / 36.0, ((2.0 + num2) + (4.0 * num3)) / 8.0, Sqrt(9.5 - (4.0 * num2)) / 2.0 }, { -1.0 + num2, Sqrt(9.5 - (4.0 * num2)) / 2.0, -num4 + (2.0 / num2), Sqrt(9.5 - (4.0 * num2)) / 2.0, -1.0 + num2 } };
            double[,] weights = new double[,] { { 3.0 - num2, Sqrt(3.875 - ((3.0 * num2) / 2.0)), num5 - (1.0 / num2), Sqrt(3.875 - ((3.0 * num2) / 2.0)), 3.0 - num2 }, { Sqrt(3.875 - ((3.0 * num2) / 2.0)), ((2.0 + num2) + (4.0 * num3)) / 8.0, (((22.0 * num) + (7.0 * Sqrt(6.0))) + ((4.0 * Sqrt(2.0 + num2)) * (-5.0 + (3.0 * num3)))) / 36.0, ((2.0 + num2) + (4.0 * num3)) / 8.0, Sqrt(3.875 - ((3.0 * num2) / 2.0)) }, { num5 - (1.0 / num2), (((22.0 * num) + (7.0 * Sqrt(6.0))) + ((4.0 * Sqrt(2.0 + num2)) * (-5.0 + (3.0 * num3)))) / 36.0, ((-5.0 + (5.0 * num2)) + ((4.0 * num3) * num3)) / 9.0, (((22.0 * num) + (7.0 * Sqrt(6.0))) + ((4.0 * Sqrt(2.0 + num2)) * (-5.0 + (3.0 * num3)))) / 36.0, num5 - (1.0 / num2) }, { Sqrt(3.875 - ((3.0 * num2) / 2.0)), ((2.0 + num2) + (4.0 * num3)) / 8.0, (((22.0 * num) + (7.0 * Sqrt(6.0))) + ((4.0 * Sqrt(2.0 + num2)) * (-5.0 + (3.0 * num3)))) / 36.0, ((2.0 + num2) + (4.0 * num3)) / 8.0, Sqrt(3.875 - ((3.0 * num2) / 2.0)) }, { 3.0 - num2, Sqrt(3.875 - ((3.0 * num2) / 2.0)), num5 - (1.0 / num2), Sqrt(3.875 - ((3.0 * num2) / 2.0)), 3.0 - num2 } };
            Position[,] poles = new Position[5, 5];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    poles[i, j] = ((center + (((numArray[i, j] / weights[i, j]) * radius) * matrix.AxisX)) + (((numArray2[i, j] / weights[i, j]) * radius) * matrix.AxisY)) + (((numArray3[i, j] / weights[i, j]) * radius) * matrix.AxisZ);
                }
            }
            return BezierPatch(poles, weights);
        }

        public static Snap.NX.Block Block(Orientation matrix, Position originPoint, Position cornerPoint)
        {
            return Snap.NX.Block.CreateBlock(matrix, originPoint, cornerPoint);
        }

        public static Snap.NX.Block Block(Orientation matrix, Position originPoint, Position cornerPoint, Snap.Number height)
        {
            return Snap.NX.Block.CreateBlock(matrix, originPoint, cornerPoint, height);
        }

        public static Snap.NX.Block Block(Position origin, Snap.Number xLength, Snap.Number yLength, Snap.Number zLength)
        {
            return Snap.NX.Block.CreateBlock(origin, Orientation.Identity, xLength, yLength, zLength);
        }

        public static Snap.NX.Block Block(Position origin, Orientation matrix, Snap.Number xLength, Snap.Number yLength, Snap.Number zLength)
        {
            return Snap.NX.Block.CreateBlock(origin, matrix, xLength, yLength, zLength);
        }

        public static Snap.NX.BoundedPlane BoundedPlane(params Snap.NX.Curve[] boundingCurves)
        {
            return Snap.NX.BoundedPlane.CreateBoundedPlane(boundingCurves);
        }

        public static Snap.NX.Bsurface Bsurface(Position[,] poles, double[] knotsU, double[] knotsV)
        {
            return Snap.NX.Bsurface.CreateBsurface(poles, knotsU, knotsV);
        }

        public static Snap.NX.Bsurface Bsurface(Position[,] poles, double[,] weights, double[] knotsU, double[] knotsV)
        {
            return Snap.NX.Bsurface.CreateBsurface(poles, weights, knotsU, knotsV);
        }

        public static Snap.NX.Bsurface BsurfaceThroughPoints(Position[,] intPoints, int degreeU, int degreeV)
        {
            intPoints.GetLength(0);
            intPoints.GetLength(1);
            double[][] numArray = Snap.Math.SplineMath.ChordalNodes(intPoints);
            double[] nodes = numArray[0];
            double[] numArray3 = numArray[1];
            double[] knotsU = Snap.Math.SplineMath.GrevilleKnots(nodes, degreeU);
            double[] knotsV = Snap.Math.SplineMath.GrevilleKnots(numArray3, degreeV);
            return BsurfaceThroughPoints(intPoints, nodes, numArray3, knotsU, knotsV);
        }

        public static Snap.NX.Bsurface BsurfaceThroughPoints(Position[,] intPoints, double[] nodesU, double[] nodesV, double[] knotsU, double[] knotsV)
        {
            int length = intPoints.GetLength(0);
            int num2 = intPoints.GetLength(1);
            Position[,] a = new Position[length, num2];
            for (int i = 0; i < length; i++)
            {
                Position[] positionArray2 = Snap.Math.SplineMath.BsplineInterpolation(Snap.Math.MatrixMath.GetRow(intPoints, i), nodesV, knotsV);
                for (int k = 0; k < num2; k++)
                {
                    a[i, k] = positionArray2[k];
                }
            }
            Position[,] poles = new Position[length, num2];
            for (int j = 0; j < num2; j++)
            {
                Position[] positionArray4 = Snap.Math.SplineMath.BsplineInterpolation(Snap.Math.MatrixMath.GetColumn(a, j), nodesU, knotsU);
                for (int m = 0; m < length; m++)
                {
                    poles[m, j] = positionArray4[m];
                }
            }
            return Bsurface(poles, knotsU, knotsV);
        }

        public static Snap.NX.Category Category(string name, string description, params int[] layers)
        {
            return Snap.NX.Category.CreateCategory(name, description, layers);
        }

        public static Snap.NX.Chamfer Chamfer(Snap.NX.Edge edge, Snap.Number distance, Snap.Number angle)
        {
            return Snap.NX.Chamfer.CreateChamfer(edge, distance, angle);
        }

        public static Snap.NX.Chamfer Chamfer(Snap.NX.Edge edge, Snap.Number distance, bool offsetFaces)
        {
            return Snap.NX.Chamfer.CreateChamfer(edge, distance, offsetFaces);
        }

        public static Snap.NX.Chamfer Chamfer(Snap.NX.Edge edge, Snap.Number distance1, Snap.Number distance2, bool offsetFaces)
        {
            return Snap.NX.Chamfer.CreateChamfer(edge, distance1, distance2, offsetFaces);
        }

        private static Snap.NX.Spline ChebyshevApproximation(Position[] q, Vector u, Vector v)
        {
            int m = q.Length + 1;
            int num2 = m + 1;
            double[,] numArray = ChebyshevConstants.InterpolationMatrix(m);
            Position[] poles = new Position[num2];
            double num3 = 1.0 / ((double) m);
            poles[0] = q[0];
            poles[1] = q[0] + ((Position) (num3 * u));
            poles[m - 1] = (Position)(q[m - 2] - ((Position)(num3 * v)));
            poles[m] = q[m - 2];
            for (int i = 2; i < (m - 1); i++)
            {
                poles[i] = (Position) ((numArray[0, i] * u) + (numArray[1, i] * v));
            }
            for (int j = 2; j < (m - 1); j++)
            {
                for (int k = 2; k < (m + 1); k++)
                {
                    poles[j] += (Position) (numArray[k, j] * q[k - 2]);
                }
            }
            return BezierCurve(poles);
        }

        public static Snap.NX.Arc Circle(Position center, double radius)
        {
            return Snap.NX.Arc.CreateArc(center, Vector.AxisX, Vector.AxisY, radius, 0.0, 360.0);
        }

        public static Snap.NX.Arc Circle(Position center, Orientation matrix, double radius)
        {
            return Snap.NX.Arc.CreateArc(center, matrix.AxisX, matrix.AxisY, radius, 0.0, 360.0);
        }

        public static Snap.NX.Arc Circle(Position p1, Position p2, Position p3)
        {
            Vector u = (Vector) (p2 - p1);
            Vector v = (Vector) (p3 - p1);
            double num = (double) (u * u);
            double num2 = (double) (u * v);
            double num3 = (double) (v * v);
            double num4 = (num * num3) - (num2 * num2);
            double num5 = ((num * num3) - (num2 * num3)) / (2.0 * num4);
            double num6 = ((num * num3) - (num * num2)) / (2.0 * num4);
            Vector vector3 = (Vector) ((num5 * u) + (num6 * v));
            Position center = p1 + vector3;
            double radius = Vector.Norm(vector3);
            Orientation orientation = new Orientation(Vector.Cross(u, v));
            Vector axisX = orientation.AxisX;
            Vector axisY = orientation.AxisY;
            return Snap.NX.Arc.CreateArc(center, axisX, axisY, radius, 0.0, 360.0);
        }

        public static Snap.NX.Arc Circle(Position center, Vector axisZ, double radius)
        {
            Orientation orientation = new Orientation(axisZ);
            Vector axisX = orientation.AxisX;
            Vector axisY = orientation.AxisY;
            return Snap.NX.Arc.CreateArc(center, axisX, axisY, radius, 0.0, 360.0);
        }

        public static Snap.NX.Arc Circle(double cx, double cy, double radius)
        {
            return Snap.NX.Arc.CreateArc(new Position(cx, cy), Vector.AxisX, Vector.AxisY, radius, 0.0, 360.0);
        }

        public static Snap.NX.Arc Circle(Position center, Vector axisX, Vector axisY, double radius)
        {
            return Snap.NX.Arc.CreateArc(center, axisX, axisY, radius, 0.0, 360.0);
        }

        public static Snap.NX.Cone Cone(Snap.NX.ICurve baseArc, Snap.NX.ICurve topArc)
        {
            return Snap.NX.Cone.CreateConeFromArcs(baseArc, topArc);
        }

        public static Snap.NX.Cone Cone(Position axisPoint, Vector axisVector, Snap.Number[] diameters, Snap.Number height)
        {
            return Snap.NX.Cone.CreateConeFromDiametersHeight(axisPoint, axisVector, diameters[0], diameters[1], height);
        }

        public static Snap.NX.Cone Cone(Position axisPoint, Vector axisVector, Snap.Number halfAngle, Snap.Number[] diameters)
        {
            return Snap.NX.Cone.CreateConeFromDiametersAngle(axisPoint, axisVector, diameters[0], diameters[1], halfAngle);
        }

        public static Snap.NX.Cone Cone(Position axisPoint, Vector axisVector, Snap.Number baseDiameter, Snap.Number height, Snap.Number halfAngle)
        {
            return Snap.NX.Cone.CreateConeFromDiameterHeightAngle(axisPoint, axisVector, baseDiameter, height, halfAngle);
        }

        [Obsolete("Deprecated in NX8.5, please use Snap.Create.Cone functions instead.")]
        public static Snap.NX.Cone ConeFromArcs(Snap.NX.ICurve baseArc, Snap.NX.ICurve topArc)
        {
            return Snap.NX.Cone.CreateConeFromArcs(baseArc, topArc);
        }

        [Obsolete("Deprecated in NX8.5, please use Snap.Create.Cone functions instead.")]
        public static Snap.NX.Cone ConeFromDiameterHeightAngle(Position axisPoint, Vector axisVector, Snap.Number baseDiameter, Snap.Number height, Snap.Number halfAngle)
        {
            return Snap.NX.Cone.CreateConeFromDiameterHeightAngle(axisPoint, axisVector, baseDiameter, height, halfAngle);
        }

        [Obsolete("Deprecated in NX8.5, please use Snap.Create.Cone functions instead.")]
        public static Snap.NX.Cone ConeFromDiametersAngle(Position axisPoint, Vector axisVector, Snap.Number baseDiameter, Snap.Number topDiameter, Snap.Number halfAngle)
        {
            return Snap.NX.Cone.CreateConeFromDiametersAngle(axisPoint, axisVector, baseDiameter, topDiameter, halfAngle);
        }

        [Obsolete("Deprecated in NX8.5, please use Snap.Create.Cone functions instead.")]
        public static Snap.NX.Cone ConeFromDiametersHeight(Position axisPoint, Vector axisVector, Snap.Number baseDiameter, Snap.Number topDiameter, Snap.Number height)
        {
            return Snap.NX.Cone.CreateConeFromDiametersHeight(axisPoint, axisVector, baseDiameter, topDiameter, height);
        }

        public static Snap.NX.CoordinateSystem CoordinateSystem(Position origin, Snap.NX.Matrix matrix)
        {
            return Snap.NX.CoordinateSystem.CreateCoordinateSystem(origin, matrix);
        }

        public static Snap.NX.CoordinateSystem CoordinateSystem(Position origin, Orientation matrix)
        {
            return Snap.NX.CoordinateSystem.CreateCoordinateSystem(origin, matrix);
        }

        public static Snap.NX.CoordinateSystem CoordinateSystem(Position origin, Vector axisX, Vector axisY, Vector axisZ)
        {
            return Snap.NX.CoordinateSystem.CreateCoordinateSystem(origin, axisX, axisY, axisZ);
        }

        public static Snap.NX.Cylinder Cylinder(Snap.NX.ICurve arc, Snap.Number height)
        {
            return Snap.NX.Cylinder.CreateCylinder(arc, height);
        }

        public static Snap.NX.Cylinder Cylinder(Position basePoint, Position topPoint, Snap.Number diameter)
        {
            Vector u = (Vector) (topPoint - basePoint);
            double height = Vector.Norm(u);
            Vector axisVector = (Vector) ((topPoint - basePoint) / height);
            return Snap.NX.Cylinder.CreateCylinder(basePoint, axisVector, height, diameter);
        }

        public static Snap.NX.Cylinder Cylinder(Position axisPoint, Vector axisVector, Snap.Number height, Snap.Number diameter)
        {
            return Snap.NX.Cylinder.CreateCylinder(axisPoint, axisVector, height, diameter);
        }

        public static Snap.NX.DatumAxis DatumAxis(Position startPoint, Position endPoint)
        {
            return Snap.NX.DatumAxis.CreateDatumAxis(startPoint, endPoint);
        }

        public static Snap.NX.DatumAxis DatumAxis(Position origin, Vector direction)
        {
            return Snap.NX.DatumAxis.CreateDatumAxis(origin, direction);
        }

        public static Snap.NX.DatumAxis DatumAxis(Snap.NX.ICurve icurve, Snap.Number arcLength, Snap.NX.DatumAxis.CurveOrientations curveOrientation)
        {
            return Snap.NX.DatumAxis.CreateDatumAxis(icurve, arcLength, curveOrientation);
        }

        public static Snap.NX.DatumCsys DatumCsys(Position origin, Snap.NX.Matrix matrix)
        {
            return Snap.NX.DatumCsys.CreateDatumCsys(origin, matrix);
        }

        public static Snap.NX.DatumCsys DatumCsys(Position origin, Vector axisX, Vector axisY)
        {
            return Snap.NX.DatumCsys.CreateDatumCsys(origin, axisX, axisY);
        }

        public static Snap.NX.DatumPlane DatumPlane(Snap.NX.ICurve curve, Snap.Number arcLength)
        {
            return Snap.NX.DatumPlane.CreateDatumPlane(curve, arcLength);
        }

        public static Snap.NX.DatumPlane DatumPlane(Position origin, Orientation orientation)
        {
            return Snap.NX.DatumPlane.CreateDatumPlane(origin, orientation);
        }

        public static Snap.NX.DatumPlane DatumPlane(Position origin, Vector normal)
        {
            return Snap.NX.DatumPlane.CreateDatumPlane(origin, normal);
        }

        public static Snap.NX.EdgeBlend EdgeBlend(Snap.Number radius, params Snap.NX.Edge[] edges)
        {
            return Snap.NX.EdgeBlend.CreateEdgeBlend(radius, edges);
        }

        public static Snap.NX.Ellipse Ellipse(Position center, double majorRadius, double minorRadius, double rotation)
        {
            Vector axisX = Vector.AxisX;
            Vector axisY = Vector.AxisY;
            return Snap.NX.Ellipse.CreateEllipse(center, axisX, axisY, rotation, majorRadius, minorRadius, 0.0, 360.0);
        }

        public static Snap.NX.Ellipse Ellipse(Position center, Orientation matrix, double majorRadius, double minorRadius, double startAngle, double endAngle)
        {
            Vector axisX = matrix.AxisX;
            Vector axisY = matrix.AxisY;
            return Snap.NX.Ellipse.CreateEllipse(center, axisX, axisY, majorRadius, minorRadius, startAngle, endAngle);
        }

        public static Snap.NX.Ellipse Ellipse(Position center, double majorRadius, double minorRadius, double rotation, double startAngle, double endAngle)
        {
            Vector axisX = Vector.AxisX;
            Vector axisY = Vector.AxisY;
            return Snap.NX.Ellipse.CreateEllipse(center, axisX, axisY, rotation, majorRadius, minorRadius, startAngle, endAngle);
        }

        public static Snap.NX.Ellipse Ellipse(Position center, Vector axisX, Vector axisY, double majorRadius, double minorRadius, double startAngle, double endAngle)
        {
            return Snap.NX.Ellipse.CreateEllipse(center, axisX, axisY, majorRadius, minorRadius, startAngle, endAngle);
        }

        [Obsolete("Deprecated in NX8.5, please use Snap.Create.Expression(string, Position) instead.")]
        public static ExpressionPoint Expression(string name, Snap.NX.Point value)
        {
            return new ExpressionPoint(name, value.Position);
        }

        public static ExpressionPoint Expression(string name, Position value)
        {
            return new ExpressionPoint(name, value);
        }

        public static ExpressionVector Expression(string name, Vector value)
        {
            return new ExpressionVector(name, value);
        }

        public static ExpressionBoolean Expression(string name, bool value)
        {
            return new ExpressionBoolean(name, value);
        }

        public static Snap.NX.ExpressionNumber Expression(string name, double value)
        {
            return new Snap.NX.ExpressionNumber(name, value);
        }

        public static ExpressionInteger Expression(string name, int value)
        {
            return new ExpressionInteger(name, value);
        }

        public static ExpressionString Expression(string name, string value)
        {
            return new ExpressionString(name, value);
        }

        public static Snap.NX.ExpressionNumber ExpressionNumber(string name, string rightHandSide)
        {
            return new Snap.NX.ExpressionNumber(name, rightHandSide);
        }

        public static Snap.NX.ExpressionNumber ExpressionNumber(string name, Snap.Number rightHandSide, Snap.NX.Unit unit)
        {
            return new Snap.NX.ExpressionNumber(name, rightHandSide, unit);
        }

        public static Snap.NX.Curve[] ExtractCurve(params Snap.NX.Edge[] edges)
        {
            Snap.NX.Curve[] curveArray = new Snap.NX.Curve[edges.Length];
            for (int i = 0; i < edges.Length; i++)
            {
                Tag tag;
                Globals.UFSession.Modl.CreateCurveFromEdge(edges[i].NXOpenTag, out tag);
                NXOpen.Curve objectFromTag = (NXOpen.Curve) Snap.NX.NXObject.GetObjectFromTag(tag);
                curveArray[i] = Snap.NX.Curve.CreateCurve(objectFromTag);
            }
            return curveArray;
        }

        public static Snap.NX.ExtractFace ExtractFace(params Snap.NX.Face[] faces)
        {
            return Snap.NX.ExtractFace.CreateExtractFace(faces);
        }

        public static Snap.NX.Extrude Extrude(Snap.NX.ICurve[] curves, Vector axis, Snap.Number[] distances, Snap.Number draftAngle = null)
        {
            draftAngle = Snap.Number.NullToZero(draftAngle);
            Snap.NX.Section section = Snap.NX.Section.CreateSection(curves);
            bool offset = false;
            Snap.Number[] offsetValues = new Snap.Number[] { 0, 0 };
            bool createSheet = false;
            return Snap.NX.Extrude.CreateExtrude(section, axis, distances, draftAngle, offset, offsetValues, createSheet);
        }

        public static Snap.NX.Extrude Extrude(Snap.NX.ICurve[] curves, Vector axis, Snap.Number length, Snap.Number draftAngle = null)
        {
            Snap.Number[] distances = new Snap.Number[] { 0, length };
            return Extrude(curves, axis, distances, draftAngle);
        }

        public static Snap.NX.Extrude ExtrudeSheet(Snap.NX.ICurve[] curves, Vector axis, Snap.Number[] distances, Snap.Number draftAngle = null)
        {
            Snap.NX.Section section = Snap.NX.Section.CreateSection(curves);
            bool offset = false;
            Snap.Number[] offsetValues = new Snap.Number[] { 0, 0 };
            bool createSheet = true;
            return Snap.NX.Extrude.CreateExtrude(section, axis, distances, draftAngle, offset, offsetValues, createSheet);
        }

        public static Snap.NX.Extrude ExtrudeSheet(Snap.NX.ICurve[] curves, Vector axis, Snap.Number length, Snap.Number draftAngle = null)
        {
            Snap.Number[] distances = new Snap.Number[] { 0, length };
            return ExtrudeSheet(curves, axis, distances, draftAngle);
        }

        public static Snap.NX.Extrude ExtrudeShell(Snap.NX.ICurve[] curves, Vector axis, Snap.Number[] distances, Snap.Number[] offsets, Snap.Number draftAngle = null)
        {
            Snap.NX.Section section = Snap.NX.Section.CreateSection(curves);
            bool offset = true;
            bool createSheet = false;
            return Snap.NX.Extrude.CreateExtrude(section, axis, distances, draftAngle, offset, offsets, createSheet);
        }

        public static Snap.NX.FaceBlend FaceBlend(Snap.NX.Face face1, Snap.NX.Face face2, Snap.Number radius)
        {
            return Snap.NX.FaceBlend.CreateFaceBlend(face1, face2, radius);
        }

        public static Snap.NX.Arc Fillet(Position p0, Position pa, Position p1, double radius)
        {
            return Snap.NX.Arc.CreateArcFillet(p0, pa, p1, radius);
        }

        public static Snap.NX.Arc Fillet(Snap.NX.Curve curve1, Snap.NX.Curve curve2, double radius, Position center, bool doTrim = true)
        {
            return Snap.NX.Arc.CreateArcFillet(curve1, curve2, radius, center, doTrim);
        }

        public static Snap.NX.Boolean Intersect(Snap.NX.Body targetBody, params Snap.NX.Body[] toolBodies)
        {
            NXOpen.Features.Feature.BooleanType intersect = NXOpen.Features.Feature.BooleanType.Intersect;
            return Snap.NX.Boolean.CreateBoolean(targetBody, toolBodies, intersect);
        }

        private static void IsLinearCurve(Snap.NX.ICurve icurve)
        {
            if (icurve.ObjectType == ObjectTypes.Type.Line)
            {
                throw new ArgumentException("The input curve is a straight line.");
            }
            if ((icurve.ObjectType == ObjectTypes.Type.Edge) && (icurve.ObjectSubType == ObjectTypes.SubType.EdgeLine))
            {
                throw new ArgumentException("The input curve is a straight line.");
            }
        }

        public static Snap.NX.Curve[] Isocline(Snap.NX.Face face, Vector direction, double draftAngle)
        {
            Tag[] tagArray;
            int num;
            Tag nXOpenTag = face.NXOpenTag;
            double[] array = direction.Array;
            double distanceTolerance = Globals.DistanceTolerance;
            Globals.UFSession.Modl.CreateIsoclineCurves(nXOpenTag, array, draftAngle, distanceTolerance, out tagArray, out num);
            return Enumerable.Select<Tag, Snap.NX.Curve>(tagArray, (u)=> { return Snap.NX.Curve.Wrap(u); }).ToArray<Snap.NX.Curve>();
        }

        public static Snap.NX.Curve[] IsoparametricCurve(Snap.NX.Face face, DirectionUV UV, double value, double tolerance = 0.0254)
        {
            Tag[] tagArray;
            if (UV == DirectionUV.U)
            {
                value /= face.FactorU;
            }
            else
            {
                value /= face.FactorV;
            }
            try
            {
                int num;
                Globals.UFSession.Modl.CreateIsocurve(face.NXOpenTag, (int) UV, value, tolerance, out tagArray, out num);
            }
            catch (NXException exception)
            {
                if ((face.ObjectSubType == ObjectTypes.SubType.FaceBlend) && (UV == DirectionUV.V))
                {
                    throw new InvalidOperationException("Cannot create v=constant iso-curves on blend faces", exception);
                }
                throw exception;
            }
            Snap.NX.Curve[] curveArray = new Snap.NX.Curve[tagArray.Length];
            for (int i = 0; i < curveArray.Length; i++)
            {
                NXOpen.Curve objectFromTag = (NXOpen.Curve) Snap.NX.NXObject.GetObjectFromTag(tagArray[i]);
                curveArray[i] = Snap.NX.Curve.CreateCurve(objectFromTag);
            }
            return curveArray;
        }

        private static void IsParallelToXYPlane(Snap.NX.ICurve icurve)
        {
            double num = Vector.Angle(icurve.Binormal(icurve.MinU), Vector.AxisZ);
            if ((num > 1E-06) || (num > 179.999999))
            {
                throw new ArgumentException("The input curve does not lie in a plane parallel to X-Y plane.");
            }
        }

        private static void IsPlanar(Snap.NX.ICurve icurve)
        {
            int num;
            double[] data = new double[6];
            Globals.UFSession.Modl.AskObjDimensionality(icurve.NXOpenTag, out num, data);
            if (num == 3)
            {
                throw new ArgumentException("The input curve is not planar.");
            }
        }

        public static Snap.NX.Spline JoinCurves(params Snap.NX.ICurve[] icurves)
        {
            JoinCurvesBuilder builder = Globals.WorkPart.NXOpenPart.Features.CreateJoinCurvesBuilder(null);
            builder.DistanceTolerance = Globals.DistanceTolerance;
            builder.AngleTolerance = Globals.AngleTolerance;
            builder.Section.DistanceTolerance = Globals.DistanceTolerance;
            builder.Section.AngleTolerance = Globals.AngleTolerance;
            builder.Section.ChainingTolerance = (Globals.UnitType == Globals.Unit.Millimeter) ? 0.02413 : 0.00095;
            builder.Section.SetAllowedEntityTypes(NXOpen.Section.AllowTypes.OnlyCurves);
            builder.CurveOptions.Associative = false;
            builder.CurveOptions.InputCurveOption = CurveOptions.InputCurve.Retain;
            for (int i = 0; i < icurves.Length; i++)
            {
                SelectionIntentRule[] rules = Snap.NX.Section.CreateSelectionIntentRule(new Snap.NX.ICurve[] { icurves[i] });
                builder.Section.AddToSection(rules, (NXOpen.NXObject) icurves[i].NXOpenTaggedObject, null, null, (Point3d) Position.Origin, NXOpen.Section.Mode.Create, false);
            }
            builder.Commit();
            NXOpen.NXObject obj2 = builder.GetCommittedObjects()[0];
            builder.Destroy();
            return new Snap.NX.Spline((NXOpen.Spline) obj2);
        }

        public static Snap.NX.Line Line(Snap.NX.Point pt0, Snap.NX.Point pt1)
        {
            return Snap.NX.Line.CreateLine(pt0.X, pt0.Y, pt0.Z, pt1.X, pt1.Y, pt1.Z);
        }

        public static Snap.NX.Line Line(Position p0, Position p1)
        {
            return Snap.NX.Line.CreateLine(p0.X, p0.Y, p0.Z, p1.X, p1.Y, p1.Z);
        }

        public static Snap.NX.Line Line(double x0, double y0, double x1, double y1)
        {
            return Snap.NX.Line.CreateLine(x0, y0, 0.0, x1, y1, 0.0);
        }

        public static Snap.NX.Line Line(double x0, double y0, double z0, double x1, double y1, double z1)
        {
            return Snap.NX.Line.CreateLine(x0, y0, z0, x1, y1, z1);
        }

        public static Snap.NX.Line LineTangent(Snap.NX.ICurve icurve, double angle, Position helpPoint)
        {
            IsLinearCurve(icurve);
            IsPlanar(icurve);
            Globals.UndoMarkId markId = Globals.SetUndoMark(Globals.MarkVisibility.Invisible, "Snap_TangentLineAngle999");
            Snap.NX.Part workPart = Globals.WorkPart;
            AssociativeLine associativeLine = null;
            AssociativeLineBuilder builder = workPart.NXOpenPart.BaseFeatures.CreateAssociativeLineBuilder(associativeLine);
            builder.StartPointOptions = AssociativeLineBuilder.StartOption.Tangent;
            builder.StartTangent.SetValue(icurve.NXOpenICurve, null, (Point3d) helpPoint);
            builder.EndPointOptions = AssociativeLineBuilder.EndOption.AtAngle;
            builder.EndAtAngle.Value = DatumAxis(Position.Origin, Vector.AxisX).NXOpenDisplayableObject;
            builder.EndAngle.RightHandSide = angle.ToString();
            builder.Associative = false;
            builder.Commit();
            NXOpen.NXObject obj2 = builder.GetCommittedObjects()[0];
            TaggedObject obj3 = (NXOpen.Line) obj2;
            NXOpen.Line line2 = (NXOpen.Line) obj3;
            Position startPoint = line2.StartPoint;
            Position endPoint = line2.EndPoint;
            Snap.Geom.Curve.Ray ray = new Snap.Geom.Curve.Ray(startPoint, (Vector) (endPoint - startPoint));
            Position[] positionArray = Compute.ClipRay(ray);
            builder.Destroy();
            Globals.UndoToMark(markId, "Snap_TangentLineAngle999");
            Globals.DeleteUndoMark(markId, "Snap_TangentLineAngle999");
            return Line(positionArray[0], positionArray[1]);
        }

        public static Snap.NX.Line LineTangent(Position basePoint, Snap.NX.ICurve icurve, Position helpPoint)
        {
            IsLinearCurve(icurve);
            if (Compute.Distance(basePoint, (Snap.NX.NXObject) icurve) < 1E-05)
            {
                double num2 = icurve.Parameter(basePoint);
                Vector axis = icurve.Tangent(num2);
                Snap.Geom.Curve.Ray ray = new Snap.Geom.Curve.Ray(basePoint, axis);
                Position[] positionArray = Compute.ClipRay(ray);
                return Line(positionArray[0], positionArray[1]);
            }
            Globals.UndoMarkId markId = Globals.SetUndoMark(Globals.MarkVisibility.Invisible, "Snap_ProjectPoint999");
            double num3 = icurve.Parameter(icurve.StartPoint);
            Surface.Plane geomPlane = new Surface.Plane(icurve.StartPoint, icurve.Binormal(num3));
            Position p = ProjectCurve(geomPlane, new Snap.NX.Point[] { Point(basePoint) }).Points[0].Position;
            Position position = ProjectCurve(geomPlane, new Snap.NX.Point[] { Point(helpPoint) }).Points[0].Position;
            Globals.UndoToMark(markId, "Snap_ProjectPoint999");
            Globals.DeleteUndoMark(markId, "Snap_ProjectPoint999");
            Snap.NX.Part workPart = Globals.WorkPart;
            AssociativeLine associativeLine = null;
            AssociativeLineBuilder builder = workPart.NXOpenPart.BaseFeatures.CreateAssociativeLineBuilder(associativeLine);
            builder.StartPointOptions = AssociativeLineBuilder.StartOption.Point;
            builder.StartPoint.Value = (NXOpen.Point) Point(p);
            builder.EndPointOptions = AssociativeLineBuilder.EndOption.Tangent;
            builder.EndTangent.SetValue(icurve.NXOpenICurve, null, (Point3d) position);
            builder.Associative = false;
            builder.Commit();
            NXOpen.NXObject obj2 = builder.GetCommittedObjects()[0];
            builder.Destroy();
            return new Snap.NX.Line((NXOpen.Line) obj2);
        }

        public static Snap.NX.Line LineTangent(Snap.NX.ICurve icurve1, Position helpPoint1, Snap.NX.ICurve icurve2, Position helpPoint2)
        {
            IsLinearCurve(icurve1);
            IsPlanar(icurve1);
            IsLinearCurve(icurve2);
            IsPlanar(icurve2);
            Globals.UndoMarkId markId = Globals.SetUndoMark(Globals.MarkVisibility.Invisible, "Snap_TangentLineTwoCurve999");
            double num = icurve1.Parameter(icurve1.StartPoint);
            Surface.Plane geomPlane = new Surface.Plane(icurve1.StartPoint, icurve1.Binormal(num));
            Snap.NX.Point[] points = new Snap.NX.Point[] { Point(helpPoint1), Point(helpPoint2) };
            Snap.NX.Point[] pointArray2 = ProjectCurve(geomPlane, points).Points;
            helpPoint1 = pointArray2[0].Position;
            helpPoint2 = pointArray2[1].Position;
            Globals.UndoToMark(markId, "Snap_TangentLineTwoCurve999");
            Globals.DeleteUndoMark(markId, "Snap_TangentLineTwoCurve999");
            Snap.NX.Part workPart = Globals.WorkPart;
            AssociativeLine associativeLine = null;
            AssociativeLineBuilder builder = workPart.NXOpenPart.BaseFeatures.CreateAssociativeLineBuilder(associativeLine);
            builder.StartPointOptions = AssociativeLineBuilder.StartOption.Tangent;
            builder.StartTangent.SetValue(icurve1.NXOpenICurve, null, (Point3d) helpPoint1);
            builder.EndPointOptions = AssociativeLineBuilder.EndOption.Tangent;
            builder.EndTangent.SetValue(icurve2.NXOpenICurve, null, (Point3d) helpPoint2);
            builder.Associative = false;
            builder.Commit();
            NXOpen.NXObject obj2 = builder.GetCommittedObjects()[0];
            builder.Destroy();
            return new Snap.NX.Line((NXOpen.Line) obj2);
        }

        public static Snap.NX.Matrix Matrix(Orientation rotation)
        {
            return (NXMatrix) new Snap.NX.Matrix(rotation);
        }

        public static Snap.NX.Matrix Matrix(Vector axisX, Vector axisY, Vector axisZ)
        {
            return new Snap.NX.Matrix(axisX, axisY, axisZ);
        }

        public static Snap.NX.Note Note(Position origin, TextStyle textStyle, params string[] text)
        {
            if (textStyle == null)
            {
                textStyle = new TextStyle();
            }
            return Snap.NX.Note.CreateNote(text, origin, Orientation.Identity, textStyle);
        }

        public static Snap.NX.Note Note(Position origin, Orientation matrix, TextStyle textStyle, params string[] text)
        {
            if (textStyle == null)
            {
                textStyle = new TextStyle();
            }
            return Snap.NX.Note.CreateNote(text, origin, matrix, textStyle);
        }

        [Obsolete("Deprecated in NX8.5, please use Snap.Create.OffsetCurve(Number, Position, Vector, params NX.ICurve[]) instead.")]
        public static Snap.NX.OffsetCurve OffsetCurve(Snap.Number distance, bool reverse, params Snap.NX.ICurve[] curves)
        {
            return Snap.NX.OffsetCurve.CreateOffsetCurve(curves, distance, reverse);
        }

        [Obsolete("Deprecated in NX8.5, please use Snap.Create.OffsetCurve(Number, Number, Position, Vector, params NX.ICurve[]) instead.")]
        public static Snap.NX.OffsetCurve OffsetCurve(Snap.Number height, Snap.Number angle, bool reverse, params Snap.NX.ICurve[] curves)
        {
            return Snap.NX.OffsetCurve.CreateOffsetCurve(curves, height, angle, reverse);
        }

        public static Snap.NX.OffsetCurve OffsetCurve(Snap.Number distance, Position helpPoint, Vector helpVector, params Snap.NX.ICurve[] curves)
        {
            return Snap.NX.OffsetCurve.CreateOffsetCurve(curves, distance, helpPoint, helpVector);
        }

        public static Snap.NX.OffsetCurve OffsetCurve(Snap.Number height, Snap.Number angle, Position helpPoint, Vector helpVector, params Snap.NX.ICurve[] curves)
        {
            return Snap.NX.OffsetCurve.CreateOffsetCurve(curves, height, angle, helpPoint, helpVector);
        }

        public static Snap.NX.OffsetFace OffsetFace(Snap.Number distance, bool reverse, params Snap.NX.Face[] faces)
        {
            return Snap.NX.OffsetFace.CreateOffsetFace(faces, distance, reverse);
        }

        public static Snap.NX.Part Part(string pathName, Snap.NX.Part.Templates templateType, Snap.NX.Part.Units unitType)
        {
            return Snap.NX.Part.CreatePart(pathName, templateType, unitType);
        }

        public static Snap.NX.Point Point(Position p)
        {
            return Snap.NX.Point.CreatePoint(p.X, p.Y, p.Z);
        }

        public static Snap.NX.Point Point(double[] coords)
        {
            return Snap.NX.Point.CreatePoint(coords[0], coords[1], coords[2]);
        }

        public static Snap.NX.Point Point(double x, double y)
        {
            return Snap.NX.Point.CreatePoint(x, y, 0.0);
        }

        public static Snap.NX.Point Point(double x, double y, double z)
        {
            return Snap.NX.Point.CreatePoint(x, y, z);
        }

        public static Snap.NX.Line[] Polygon(params Position[] points)
        {
            int length = points.Length;
            Snap.NX.Line[] lineArray = new Snap.NX.Line[length];
            for (int i = 0; i < (length - 1); i++)
            {
                lineArray[i] = Line(points[i], points[i + 1]);
            }
            lineArray[length - 1] = Line(points[length - 1], points[0]);
            return lineArray;
        }

        public static Snap.NX.Line[] PolyLine(params Position[] points)
        {
            int length = points.Length;
            Snap.NX.Line[] lineArray = new Snap.NX.Line[length - 1];
            for (int i = 0; i < (length - 1); i++)
            {
                lineArray[i] = Line(points[i], points[i + 1]);
            }
            return lineArray;
        }

        public static Snap.NX.ProjectCurve ProjectCurve(Surface.Plane geomPlane, params Snap.NX.Curve[] curves)
        {
            return Snap.NX.ProjectCurve.CreateProjectCurve(curves, null, geomPlane);
        }

        public static Snap.NX.ProjectCurve ProjectCurve(Surface.Plane geomPlane, params Snap.NX.Point[] points)
        {
            return Snap.NX.ProjectCurve.CreateProjectCurve(null, points, geomPlane);
        }

        public static Snap.NX.ProjectCurve ProjectCurve(Snap.NX.DatumPlane datumPlane, params Snap.NX.Curve[] curves)
        {
            return Snap.NX.ProjectCurve.CreateProjectCurve(curves, null, datumPlane);
        }

        public static Snap.NX.ProjectCurve ProjectCurve(Snap.NX.DatumPlane datumPlane, params Snap.NX.Point[] points)
        {
            return Snap.NX.ProjectCurve.CreateProjectCurve(null, points, datumPlane);
        }

        public static Snap.NX.ProjectCurve ProjectCurve(Snap.NX.Face face, params Snap.NX.Curve[] curves)
        {
            return Snap.NX.ProjectCurve.CreateProjectCurve(curves, null, face);
        }

        public static Snap.NX.ProjectCurve ProjectCurve(Snap.NX.Face face, params Snap.NX.Point[] points)
        {
            return Snap.NX.ProjectCurve.CreateProjectCurve(null, points, face);
        }

        public static Snap.NX.ProjectCurve ProjectCurve(Surface.Plane geomPlane, Snap.NX.Curve[] curves, Snap.NX.Point[] points)
        {
            return Snap.NX.ProjectCurve.CreateProjectCurve(curves, points, geomPlane);
        }

        public static Snap.NX.ProjectCurve ProjectCurve(Snap.NX.DatumPlane datumPlane, Snap.NX.Curve[] curves, Snap.NX.Point[] points)
        {
            return Snap.NX.ProjectCurve.CreateProjectCurve(curves, points, datumPlane);
        }

        public static Snap.NX.ProjectCurve ProjectCurve(Snap.NX.Curve[] curves, Snap.NX.Point[] points, Snap.NX.Face face)
        {
            return Snap.NX.ProjectCurve.CreateProjectCurve(curves, points, face);
        }

        public static Snap.NX.Line[] Rectangle(Position bottomLeft, Position topRight)
        {
            Position center = (Position) ((bottomLeft + topRight) / 2.0);
            double width = topRight.X - bottomLeft.X;
            double height = topRight.Y - bottomLeft.Y;
            return Rectangle(center, width, height);
        }

        public static Snap.NX.Line[] Rectangle(Position center, double width, double height)
        {
            Vector vector = new Vector(width / 2.0, 0.0);
            Vector vector2 = new Vector(0.0, height / 2.0);
            Position position = (center + vector) - vector2;
            Position position2 = (center + vector) + vector2;
            Position position3 = (center - vector) + vector2;
            Position position4 = (center - vector) - vector2;
            Position[] points = new Position[] { position, position2, position3, position4, position };
            return PolyLine(points);
        }

        public static Snap.NX.Revolve Revolve(Snap.NX.ICurve[] curves, Position axisPoint, Vector axisVector)
        {
            Snap.Number[] angles = new Snap.Number[] { 0, 360 };
            return Revolve(curves, axisPoint, axisVector, angles);
        }

        public static Snap.NX.Revolve Revolve(Snap.NX.ICurve[] curves, Position axisPoint, Vector axisVector, Snap.Number[] angles)
        {
            bool offset = false;
            Snap.Number[] offsetValues = new Snap.Number[] { 0, 0 };
            bool createSheet = false;
            return Snap.NX.Revolve.CreateRevolve(curves, axisPoint, axisVector, angles, offset, offsetValues, createSheet);
        }

        public static Snap.NX.Revolve RevolveSheet(Snap.NX.ICurve[] curves, Position axisPoint, Vector axisVector)
        {
            Snap.Number[] angles = new Snap.Number[] { 0, 360 };
            return RevolveSheet(curves, axisPoint, axisVector, angles);
        }

        public static Snap.NX.Revolve RevolveSheet(Snap.NX.ICurve[] curves, Position axisPoint, Vector axisVector, Snap.Number[] angles)
        {
            bool offset = false;
            Snap.Number[] offsetValues = new Snap.Number[] { 0, 0 };
            bool createSheet = true;
            return Snap.NX.Revolve.CreateRevolve(curves, axisPoint, axisVector, angles, offset, offsetValues, createSheet);
        }

        public static Snap.NX.Revolve RevolveShell(Snap.NX.ICurve[] curves, Position axisPoint, Vector axisVector, Snap.Number[] angles, Snap.Number[] offsets)
        {
            bool offset = true;
            bool createSheet = false;
            return Snap.NX.Revolve.CreateRevolve(curves, axisPoint, axisVector, angles, offset, offsets, createSheet);
        }

        public static Snap.NX.Ruled Ruled(Snap.NX.Curve curve0, Snap.NX.Curve curve1)
        {
            return Snap.NX.Ruled.CreateRuled(curve0, curve1);
        }

        public static Snap.NX.Sew Sew(Snap.NX.Body targetBody, params Snap.NX.Body[] toolBodies)
        {
            return Snap.NX.Sew.CreateSew(targetBody, toolBodies);
        }

        public static Snap.NX.Curve[] Silhouette(Snap.NX.Face face, Vector direction)
        {
            return Isocline(face, direction, 0.0);
        }

        public static Snap.NX.Sphere Sphere(Snap.NX.Point center, Snap.Number diameter)
        {
            return Snap.NX.Sphere.CreateSphere(center, diameter);
        }

        public static Snap.NX.Sphere Sphere(Position center, Snap.Number diameter)
        {
            return Snap.NX.Sphere.CreateSphere(center, diameter);
        }

        public static Snap.NX.Sphere Sphere(double x, double y, double z, Snap.Number diameter)
        {
            return Snap.NX.Sphere.CreateSphere(new Position(x, y, z), diameter);
        }

        public static Snap.NX.Spline Spline(double[] knots, Position[] poles)
        {
            int length = poles.Length;
            double[] weights = new double[length];
            for (int i = 0; i < length; i++)
            {
                weights[i] = 1.0;
            }
            return Snap.NX.Spline.CreateSpline(knots, poles, weights);
        }

        public static Snap.NX.Spline Spline(double[] knots, Position[] poles, double[] weights)
        {
            return Snap.NX.Spline.CreateSpline(knots, poles, weights);
        }

        public static Snap.NX.Spline SplineThroughPoints(Position[] points, int degree)
        {
            double[] nodes = Snap.Math.SplineMath.ChordalNodes(points);
            double[] t = Snap.Math.SplineMath.GrevilleKnots(nodes, degree);
            return Snap.NX.Spline.CreateSplineThroughPoints(points, nodes, t);
        }

        public static Snap.NX.Spline SplineThroughPoints(Position[] points, double[] nodes, double[] knots)
        {
            return Snap.NX.Spline.CreateSplineThroughPoints(points, nodes, knots);
        }

        public static Snap.NX.Spline SplineThroughPoints(Position[] points, Vector startTangent, Vector endTangent)
        {
            return Snap.NX.Spline.CreateSplineThroughPoints(points, startTangent, endTangent);
        }

        public static Snap.NX.SplitBody SplitBody(Snap.NX.Body targetBody, params Snap.NX.Body[] toolBodies)
        {
            return Snap.NX.SplitBody.CreateSplitBody(targetBody, toolBodies);
        }

        public static Snap.NX.SplitBody SplitBody(Snap.NX.Body targetBody, params Snap.NX.DatumPlane[] toolDatumPlanes)
        {
            return Snap.NX.SplitBody.CreateSplitBody(targetBody, toolDatumPlanes);
        }

        public static Snap.NX.SplitBody SplitBody(Snap.NX.Body targetBody, params Snap.NX.Face[] toolFaces)
        {
            return Snap.NX.SplitBody.CreateSplitBody(targetBody, toolFaces);
        }

        private static double Sqrt(double x)
        {
            return System.Math.Sqrt(x);
        }

        public static Snap.NX.Boolean Subtract(Snap.NX.Body targetBody, params Snap.NX.Body[] toolBodies)
        {
            NXOpen.Features.Feature.BooleanType subtract = NXOpen.Features.Feature.BooleanType.Subtract;
            return Snap.NX.Boolean.CreateBoolean(targetBody, toolBodies, subtract);
        }

        public static Snap.NX.Thicken Thicken(Snap.Number offset1, Snap.Number offset2, params Snap.NX.Body[] targetBodies)
        {
            return Snap.NX.Thicken.CreateThicken(targetBodies, offset1, offset2);
        }

        public static Snap.NX.ThroughCurveMesh ThroughCurveMesh(Snap.NX.ICurve[] primaryCurves, Snap.NX.ICurve[] crossCurves)
        {
            return Snap.NX.ThroughCurveMesh.CreateThroughCurveMesh(primaryCurves, crossCurves);
        }

        public static Snap.NX.ThroughCurves ThroughCurves(params Snap.NX.ICurve[] curves)
        {
            return Snap.NX.ThroughCurves.CreateThroughCurves(curves);
        }

        public static Snap.NX.Torus Torus(Position axisPoint, Vector axisVector, double majorRadius, double minorRadius)
        {
            return Snap.NX.Torus.CreateTorus(axisPoint, axisVector, majorRadius, minorRadius);
        }

        public static Snap.NX.Torus Torus(Position center, Orientation matrix, double majorRadius, double minorRadius, Box2d boxUV)
        {
            return Snap.NX.Torus.CreateTorus(center, matrix, majorRadius, minorRadius, boxUV);
        }

        public static Snap.NX.TrimBody TrimBody(Snap.NX.Body targetBody, Snap.NX.Body toolBody, bool direction)
        {
            return Snap.NX.TrimBody.CreateTrimBody(targetBody, toolBody, direction);
        }

        public static Snap.NX.TrimBody TrimBody(Snap.NX.Body targetBody, Snap.NX.DatumPlane toolDatumPlane, bool direction)
        {
            return Snap.NX.TrimBody.CreateTrimBody(targetBody, toolDatumPlane, direction);
        }

        public static Snap.NX.TrimBody TrimBody(Snap.NX.Body targetBody, Snap.NX.Face toolFace, bool direction)
        {
            return Snap.NX.TrimBody.CreateTrimBody(targetBody, toolFace, direction);
        }

        public static Snap.NX.Tube Tube(Snap.NX.Curve spine, bool createBsurface, Snap.Number outerDiameter, Snap.Number innerDiameter = null)
        {
            return Snap.NX.Tube.CreateTube(spine, outerDiameter, Snap.Number.NullToZero(innerDiameter), createBsurface);
        }

        public static Snap.NX.Boolean Unite(Snap.NX.Body targetBody, params Snap.NX.Body[] toolBodies)
        {
            NXOpen.Features.Feature.BooleanType unite = NXOpen.Features.Feature.BooleanType.Unite;
            return Snap.NX.Boolean.CreateBoolean(targetBody, toolBodies, unite);
        }

        private static class ChebyshevConstants
        {
            private static readonly double[,] matrix_10 = new double[,] { { 0.0, 0.1, -0.37071184367050575, 0.728800267170619, -0.91243227768576163, 0.76036023140480136, -0.416457295526068, 0.13901694137643966, -0.022222222222222223, 0.0, 0.0 }, { 0.0, 0.0, 0.022222222222222223, -0.13901694137643966, 0.416457295526068, -0.76036023140480136, 0.91243227768576163, -0.728800267170619, 0.37071184367050575, -0.1, 0.0 }, { 1.0, 1.0, -7.9478782542747544, 17.849116267723087, -23.433115834013396, 19.940909013477576, -11.032489035598132, 3.7009212791903185, -0.592934065892728, 0.0, 0.0 }, { 0.0, 0.0, 13.724423174681663, -38.431675497548468, 54.839401283547396, -48.489327924128979, 27.338202331472278, -9.25672624012215, 1.4894009641371264, 0.0, 0.0 }, { 0.0, 0.0, -7.9766328207091544, 38.8454882118813, -67.453811692498959, 65.194617804146247, -38.39591988641498, 13.283485954932372, -2.1584106257479228, 0.0, 0.0 }, { 0.0, 0.0, 5.5124696111223193, -30.654449736492573, 70.850110065245488, -79.217929372743214, 50.200441813077, -18.009190063249893, 2.975065576714635, 0.0, 0.0 }, { 0.0, 0.0, -4.0255035600311837, 23.673029823685997, -61.9128190448167, 86.143460958496746, -61.9128190448167, 23.673029823685997, -4.0255035600311837, 0.0, 0.0 }, { 0.0, 0.0, 2.975065576714635, -18.009190063249893, 50.200441813077, -79.217929372743214, 70.850110065245488, -30.654449736492573, 5.5124696111223193, 0.0, 0.0 }, { 0.0, 0.0, -2.1584106257479228, 13.283485954932372, -38.39591988641498, 65.194617804146247, -67.453811692498959, 38.8454882118813, -7.9766328207091544, 0.0, 0.0 }, { 0.0, 0.0, 1.4894009641371264, -9.25672624012215, 27.338202331472278, -48.489327924128979, 54.839401283547396, -38.431675497548468, 13.724423174681663, 0.0, 0.0 }, { 0.0, 0.0, -0.592934065892728, 3.7009212791903185, -11.032489035598132, 19.940909013477576, -23.433115834013396, 17.849116267723087, -7.9478782542747544, 1.0, 1.0 } };
            private static readonly double[,] matrix_11 = new double[,] { { 0.0, 0.090909090909090912, -0.37970033498143696, 0.85619756469994057, -1.2595557941147295, 1.2771623317829848, -0.89968271008194955, 0.42809878234997029, -0.12656677832714566, 0.018181818181818181, 0.0, 0.0 }, { 0.0, 0.0, -0.018181818181818181, 0.12656677832714566, -0.42809878234997029, 0.89968271008194955, -1.2771623317829848, 1.2595557941147295, -0.85619756469994057, 0.37970033498143696, -0.090909090909090912, 0.0 }, { 1.0, 1.0, -9.5375899318718336, 24.779479240447074, -38.371043103190338, 39.820746025732866, -28.379265418097535, 13.586012025174695, -4.0293336025966129, 0.57970033498143692, 0.0, 0.0 }, { 0.0, 0.0, 16.196314023656942, -52.44221806506134, 88.4650400891499, -95.6237591059765, 69.595756141717956, -33.691241778555671, 10.05050586650721, -1.450011078371668, 0.0, 0.0 }, { 0.0, 0.0, -9.4857122336620439, 51.624481984009194, -105.73185543982386, 125.2581714078425, -95.5754593774733, 47.443367091498523, -14.339475593071359, 2.0818002126583628, 0.0, 0.0 }, { 0.0, 0.0, 6.6318899425749454, -40.98141384075371, 108.09639049133844, -147.08771609967459, 120.90330533279847, -62.472473711878557, 19.283451929754268, -2.8278848777272252, 0.0, 0.0 }, { 0.0, 0.0, -4.9277863506687964, 32.138488363453831, -94.850112088850167, 154.55541699441414, -142.46719590128401, 78.525916425137041, -25.083966282688554, 3.739279958429881, 0.0, 0.0 }, { 0.0, 0.0, 3.739279958429881, -25.083966282688554, 78.525916425137041, -142.46719590128401, 154.55541699441414, -94.850112088850167, 32.138488363453831, -4.9277863506687964, 0.0, 0.0 }, { 0.0, 0.0, -2.8278848777272252, 19.283451929754268, -62.472473711878557, 120.90330533279847, -147.08771609967459, 108.09639049133844, -40.98141384075371, 6.6318899425749454, 0.0, 0.0 }, { 0.0, 0.0, 2.0818002126583628, -14.339475593071359, 47.443367091498523, -95.5754593774733, 125.2581714078425, -105.73185543982386, 51.624481984009194, -9.4857122336620439, 0.0, 0.0 }, { 0.0, 0.0, -1.450011078371668, 10.05050586650721, -33.691241778555671, 69.595756141717956, -95.6237591059765, 88.4650400891499, -52.44221806506134, 16.196314023656942, 0.0, 0.0 }, { 0.0, 0.0, 0.57970033498143692, -4.0293336025966129, 13.586012025174695, -28.379265418097535, 39.820746025732866, -38.371043103190338, 24.779479240447074, -9.5375899318718336, 1.0, 1.0 } };
            private static readonly double[,] matrix_12 = new double[,] { { 0.0, 0.083333333333333329, -0.38693428290356224, 0.9833525489895335, -1.6595362701886669, 1.9774178122922215, -1.6949295533933326, 1.0372101688679167, -0.43704557732868154, 0.11608028487106868, -0.015151515151515152, 0.0, 0.0 }, { 0.0, 0.0, 0.015151515151515152, -0.11608028487106868, 0.43704557732868154, -1.0372101688679167, 1.6949295533933326, -1.9774178122922215, 1.6595362701886669, -0.9833525489895335, 0.38693428290356224, -0.083333333333333329, 0.0 }, { 1.0, 1.0, -11.246766757759836, 33.178799663337145, -59.131244202018124, 72.250224986415859, -62.734656539863, 38.661291952723126, -16.35407821617359, 4.35283712177203, -0.56875246472174412, 0.0, 0.0 }, { 0.0, 0.0, 18.853334661389038, -69.248326215591689, 134.66471910273822, -171.69866121261745, 152.50543417161359, -95.174540740096589, 40.544368403285418, -10.832862303595345, 1.4181491373688024, 0.0, 0.0 }, { 0.0, 0.0, -11.106331464541324, 66.789111179809055, -157.32832524849988, 220.23398660958154, -205.62005348484064, 131.93287069558858, -57.084543777054776, 15.381930400061092, -2.0221616509718237, 0.0, 0.0 }, { 0.0, 0.0, 7.8322182042229969, -53.235520712903472, 157.75469651407937, -252.10187648916917, 253.60101292285705, -169.77702096006874, 75.247362767797213, -20.544567597840931, 2.7185533550404974, 0.0, 0.0 }, { 0.0, 0.0, -5.8929719117352439, 42.20423699782711, -138.75123967140476, 259.01844818868773, -289.55774366869326, 206.6152868535064, -95.022630383021877, 26.477720981756249, -3.5392717566339313, 0.0, 0.0 }, { 0.0, 0.0, 4.5540006483425666, -33.523359514631245, 116.46091471027277, -238.96000988455128, 304.61201319785249, -238.96000988455128, 116.46091471027277, -33.523359514631245, 4.5540006483425666, 0.0, 0.0 }, { 0.0, 0.0, -3.5392717566339313, 26.477720981756249, -95.022630383021877, 206.6152868535064, -289.55774366869326, 259.01844818868773, -138.75123967140476, 42.20423699782711, -5.8929719117352439, 0.0, 0.0 }, { 0.0, 0.0, 2.7185533550404974, -20.544567597840931, 75.247362767797213, -169.77702096006874, 253.60101292285705, -252.10187648916917, 157.75469651407937, -53.235520712903472, 7.8322182042229969, 0.0, 0.0 }, { 0.0, 0.0, -2.0221616509718237, 15.381930400061092, -57.084543777054776, 131.93287069558858, -205.62005348484064, 220.23398660958154, -157.32832524849988, 66.789111179809055, -11.106331464541324, 0.0, 0.0 }, { 0.0, 0.0, 1.4181491373688024, -10.832862303595345, 40.544368403285418, -95.174540740096589, 152.50543417161359, -171.69866121261745, 134.66471910273822, -69.248326215591689, 18.853334661389038, 0.0, 0.0 }, { 0.0, 0.0, -0.56875246472174412, 4.35283712177203, -16.35407821617359, 38.661291952723126, -62.734656539863, 72.250224986415859, -59.131244202018124, 33.178799663337145, -11.246766757759836, 1.0, 1.0 } };
            private static readonly double[,] matrix_13 = new double[,] { { 0.0, 0.076923076923076927, -0.39287680689861321, 1.110273696017761, -2.1120760982874569, 2.887268278214651, -2.9082592438169419, 2.1654512086609885, -1.1733756101596984, 0.44410947840710435, -0.10714822006325814, 0.01282051282051282, 0.0, 0.0 }, { 0.0, 0.0, -0.01282051282051282, 0.10714822006325814, -0.44410947840710435, 1.1733756101596984, -2.1654512086609885, 2.9082592438169419, -2.887268278214651, 2.1120760982874569, -1.110273696017761, 0.39287680689861321, -0.076923076923076927, 0.0 }, { 1.0, 1.0, -13.075865398727164, 43.177028991560647, -86.983192023871055, 122.13538648579559, -124.76375217780495, 93.629767400543869, -50.96453635902796, 19.340028406886411, -4.6729223012650287, 0.55954347356527989, 0.0, 0.0 }, { 0.0, 0.0, 21.696269103445957, -89.088432144356162, 196.06996932287379, -287.69833030022033, 301.03266241410131, -229.03841421676375, 125.67596426915048, -47.915409813199155, 11.607839350610078, -1.3918162955500391, 0.0, 0.0 }, { 0.0, 0.0, -12.839157826445749, 84.533090937916782, -224.86665956605168, 362.68430023331138, -399.68866346601783, 313.29900634426639, -174.94099212337315, 67.382101453091082, -16.417807490675315, 1.9743328985600577, 0.0, 0.0 }, { 0.0, 0.0, 9.1142132655512462, -67.571390521165213, 222.1948333051642, -407.02018201711303, 483.13446996653636, -395.72504267110634, 226.8235660391237, -88.720783407617674, 21.805807177042375, -2.633950921249109, 0.0, 0.0 }, { 0.0, 0.0, -6.9220192996614456, 53.997818024026238, -195.69484160417025, 411.61059047384316, -539.03748464243574, 469.94919979633869, -279.88740185915077, 112.00738616072198, -27.888973697876978, 3.3911902541168777, 0.0, 0.0 }, { 0.0, 0.0, 5.4203712883390933, -43.434642269297747, 165.82879848027386, -379.77292734139712, 555.91019542567426, -527.70194417333232, 332.35456249905803, -137.64223071410146, 34.95258394348032, -4.2931105419450066, 0.0, 0.0 }, { 0.0, 0.0, -4.2931105419450066, 34.95258394348032, -137.64223071410146, 332.35456249905803, -527.70194417333232, 555.91019542567426, -379.77292734139712, 165.82879848027386, -43.434642269297747, 5.4203712883390933, 0.0, 0.0 }, { 0.0, 0.0, 3.3911902541168777, -27.888973697876978, 112.00738616072198, -279.88740185915077, 469.94919979633869, -539.03748464243574, 411.61059047384316, -195.69484160417025, 53.997818024026238, -6.9220192996614456, 0.0, 0.0 }, { 0.0, 0.0, -2.633950921249109, 21.805807177042375, -88.720783407617674, 226.8235660391237, -395.72504267110634, 483.13446996653636, -407.02018201711303, 222.1948333051642, -67.571390521165213, 9.1142132655512462, 0.0, 0.0 }, { 0.0, 0.0, 1.9743328985600577, -16.417807490675315, 67.382101453091082, -174.94099212337315, 313.29900634426639, -399.68866346601783, 362.68430023331138, -224.86665956605168, 84.533090937916782, -12.839157826445749, 0.0, 0.0 }, { 0.0, 0.0, -1.3918162955500391, 11.607839350610078, -47.915409813199155, 125.67596426915048, -229.03841421676375, 301.03266241410131, -287.69833030022033, 196.06996932287379, -89.088432144356162, 21.696269103445957, 0.0, 0.0 }, { 0.0, 0.0, 0.55954347356527989, -4.6729223012650287, 19.340028406886411, -50.96453635902796, 93.629767400543869, -124.76375217780495, 122.13538648579559, -86.983192023871055, 43.177028991560647, -13.075865398727164, 1.0, 1.0 } };
            private static readonly double[,] matrix_14 = new double[,] { { 0.0, 0.071428571428571425, -0.39784234965025855, 1.2369824340263549, -2.6169701187715191, 4.0328119811847412, -4.6605061443516718, 4.0779428763077128, -2.688541320789827, 1.3084850593857595, -0.44981179419140177, 0.099460587412564638, -0.01098901098901099, 0.0, 0.0 }, { 0.0, 0.0, 0.01098901098901099, -0.099460587412564638, 0.44981179419140177, -1.3084850593857595, 2.688541320789827, -4.0779428763077128, 4.6605061443516718, -4.0328119811847412, 2.6169701187715191, -1.2369824340263549, 0.39784234965025855, -0.071428571428571425, 0.0 }, { 1.0, 1.0, -15.025209859944914, 54.904370070399118, -123.31583793294033, 195.47113740065376, -229.31393094415031, 202.37515879674814, -134.10227437186367, 65.4658250505592, -22.546028112584811, 4.99053764626533, -0.55168850349641241, 0.0, 0.0 }, { 0.0, 0.0, 24.725680453726451, -112.20138546969683, 275.55180629380345, -456.97314965641147, 549.71823137656929, -492.32223253697248, 329.13447060787695, -161.54386824140889, 55.815286943032689, -12.37781668835731, 1.3696722330059834, 0.0, 0.0 }, { 0.0, 0.0, -14.684682790742642, 105.05045968033987, -311.19748059535993, 567.74782886842456, -720.410510268841, 665.7869313539145, -453.64257023206568, 225.25010251584422, -78.372097308727348, 17.45047273965173, -1.93507778731681, 0.0, 0.0 }, { 0.0, 0.0, 10.478441021484397, -84.143993126154484, 303.98647712697732, -627.1251565176193, 856.65567077387391, -828.078733196188, 580.06250047446088, -292.9797409438936, 102.99747996815802, -23.071604993455516, 2.5664540535073992, 0.0, 0.0 }, { 0.0, 0.0, -8.015658206855143, 67.6473290371328, -267.93316675035175, 626.75098128476543, -939.041044982993, 964.71934266146252, -702.61660129909114, 363.66757935379184, -129.77610687877305, 29.324596540017424, -3.2769641531599345, 0.0, 0.0 }, { 0.0, 0.0, 6.339353854527916, -54.926628045547218, 228.60726561584823, -578.11585234502479, 955.0865654671411, -1060.4136121901893, 814.84065415628061, -436.71856789916768, 159.26174747348577, -36.44781774705995, 4.1002285911090137, 0.0, 0.0 }, { 0.0, 0.0, -5.0905489058453046, 44.801480356465042, -192.07934584256827, 510.1028811294866, -905.37116075719791, 1096.8662902224492, -905.37116075719791, 510.1028811294866, -192.07934584256827, 44.801480356465042, -5.0905489058453046, 0.0, 0.0 }, { 0.0, 0.0, 4.1002285911090137, -36.44781774705995, 159.26174747348577, -436.71856789916768, 814.84065415628061, -1060.4136121901893, 955.0865654671411, -578.11585234502479, 228.60726561584823, -54.926628045547218, 6.339353854527916, 0.0, 0.0 }, { 0.0, 0.0, -3.2769641531599345, 29.324596540017424, -129.77610687877305, 363.66757935379184, -702.61660129909114, 964.71934266146252, -939.041044982993, 626.75098128476543, -267.93316675035175, 67.6473290371328, -8.015658206855143, 0.0, 0.0 }, { 0.0, 0.0, 2.5664540535073992, -23.071604993455516, 102.99747996815802, -292.9797409438936, 580.06250047446088, -828.078733196188, 856.65567077387391, -627.1251565176193, 303.98647712697732, -84.143993126154484, 10.478441021484397, 0.0, 0.0 }, { 0.0, 0.0, -1.93507778731681, 17.45047273965173, -78.372097308727348, 225.25010251584422, -453.64257023206568, 665.7869313539145, -720.410510268841, 567.74782886842456, -311.19748059535993, 105.05045968033987, -14.684682790742642, 0.0, 0.0 }, { 0.0, 0.0, 1.3696722330059834, -12.37781668835731, 55.815286943032689, -161.54386824140889, 329.13447060787695, -492.32223253697248, 549.71823137656929, -456.97314965641147, 275.55180629380345, -112.20138546969683, 24.725680453726451, 0.0, 0.0 }, { 0.0, 0.0, -0.55168850349641241, 4.99053764626533, -22.546028112584811, 65.4658250505592, -134.10227437186367, 202.37515879674814, -229.31393094415031, 195.47113740065376, -123.31583793294033, 54.904370070399118, -15.025209859944914, 1.0, 1.0 } };
            private static readonly double[,] matrix_15 = new double[,] { { 0.0, 0.066666666666666666, -0.40205159568525128, 1.3635031307700829, -3.1740725538031369, 5.4401211844967721, -7.0872760920815674, 7.1149346968109874, -5.5123258493967748, 3.2640727106980632, -1.4427602517286986, 0.45450104359002763, -0.092781137465827224, 0.0095238095238095247, 0.0, 0.0 }, { 0.0, 0.0, -0.0095238095238095247, 0.092781137465827224, -0.45450104359002763, 1.4427602517286986, -3.2640727106980632, 5.5123258493967748, -7.1149346968109874, 7.0872760920815674, -5.4401211844967721, 3.1740725538031369, -1.3635031307700829, 0.40205159568525128, -0.066666666666666666, 0.0 }, { 1.0, 1.0, -17.095036355879373, 68.491183292176345, -169.63761988427191, 299.44667825686264, -396.35368769735862, 401.57163364793581, -312.85130190865812, 185.88962689516191, -82.341538839352168, 25.973539938706644, -5.3063137183905971, 0.54490873854239419, 0.0, 0.0 }, { 0.0, 0.0, 27.941983093216908, -138.82629785620361, 376.220692434395, -695.46234257834726, 944.77900722512049, -972.16159199192214, 764.63958902350657, -457.05584663358837, 203.21786790587453, -64.251026239786157, 13.144273774033238, -1.3507818471231674, 0.0, 0.0 }, { 0.0, 0.0, -16.643274319277026, 128.53545405130617, -419.39847041517038, 853.37174743026355, -1224.3034785604009, 1301.7003527348782, -1044.7864383453448, 632.514568842654, -283.49275811109254, 90.076507872251966, -18.481643585860272, 1.9022562552035267, 0.0, 0.0 }, { 0.0, 0.0, 11.925326745972674, -103.1084885490322, 405.89901950409484, -930.44195174139224, 1436.1333698103701, -1598.1036065902319, 1320.1977230547941, -814.05487064869, 369.12954290953627, -118.13744351261035, 24.343180230372603, -2.5113090783677379, 0.0, 0.0 }, { 0.0, 0.0, -9.17443688514198, 83.28109496779031, -357.89165076781978, 921.399859722803, -1552.4215971182657, 1833.2708522560067, -1574.9965367280247, 996.27821189578742, -459.22311747584712, 148.48564899912509, -30.783744110269961, 3.1861036603469244, 0.0, 0.0 }, { 0.0, 0.0, 7.3116833254077607, -68.108570018707553, 306.92607142795265, -849.51029232141741, 1562.8191325694993, -1982.902161021414, 1793.4105918819221, -1174.7012150132916, 553.99194699434452, -181.73752380491709, 38.0044162252217, -3.9516729870827705, 0.0, 0.0 }, { 0.0, 0.0, -5.9325580949137642, 56.119358290940418, -260.2202031208069, 753.94354135804417, -1479.5493614615382, 2025.1837784784666, -1953.1728844919144, 1341.0261398945404, -653.02918351027972, 218.69245756885636, -46.303902993376582, 4.8468077490956292, 0.0, 0.0 }, { 0.0, 0.0, 4.8468077490956292, -46.303902993376582, 218.69245756885636, -653.02918351027972, 1341.0261398945404, -1953.1728844919144, 2025.1837784784666, -1479.5493614615382, 753.94354135804417, -260.2202031208069, 56.119358290940418, -5.9325580949137642, 0.0, 0.0 }, { 0.0, 0.0, -3.9516729870827705, 38.0044162252217, -181.73752380491709, 553.99194699434452, -1174.7012150132916, 1793.4105918819221, -1982.902161021414, 1562.8191325694993, -849.51029232141741, 306.92607142795265, -68.108570018707553, 7.3116833254077607, 0.0, 0.0 }, { 0.0, 0.0, 3.1861036603469244, -30.783744110269961, 148.48564899912509, -459.22311747584712, 996.27821189578742, -1574.9965367280247, 1833.2708522560067, -1552.4215971182657, 921.399859722803, -357.89165076781978, 83.28109496779031, -9.17443688514198, 0.0, 0.0 }, { 0.0, 0.0, -2.5113090783677379, 24.343180230372603, -118.13744351261035, 369.12954290953627, -814.05487064869, 1320.1977230547941, -1598.1036065902319, 1436.1333698103701, -930.44195174139224, 405.89901950409484, -103.1084885490322, 11.925326745972674, 0.0, 0.0 }, { 0.0, 0.0, 1.9022562552035267, -18.481643585860272, 90.076507872251966, -283.49275811109254, 632.514568842654, -1044.7864383453448, 1301.7003527348782, -1224.3034785604009, 853.37174743026355, -419.39847041517038, 128.53545405130617, -16.643274319277026, 0.0, 0.0 }, { 0.0, 0.0, -1.3507818471231674, 13.144273774033238, -64.251026239786157, 203.21786790587453, -457.05584663358837, 764.63958902350657, -972.16159199192214, 944.77900722512049, -695.46234257834726, 376.220692434395, -138.82629785620361, 27.941983093216908, 0.0, 0.0 }, { 0.0, 0.0, 0.54490873854239419, -5.3063137183905971, 25.973539938706644, -82.341538839352168, 185.88962689516191, -312.85130190865812, 401.57163364793581, -396.35368769735862, 299.44667825686264, -169.63761988427191, 68.491183292176345, -17.095036355879373, 1.0, 1.0 } };
            private static readonly double[,] matrix_16 = new double[,] { 
                { 
                    0.0, 0.0625, -0.40566381986248679, 1.4898591742830996, -3.7832770990506757, 7.1352518539922238, -10.338945720736087, 11.700912510227381, -10.400811120202116, 7.2372620045152605, -3.8919555567230311, 1.5763654579377815, -0.4584182074717229, 0.086927961399104317, -0.0083333333333333332, 0.0, 
                    0.0
                 }, { 
                    0.0, 0.0, 0.0083333333333333332, -0.086927961399104317, 0.4584182074717229, -1.5763654579377815, 3.8919555567230311, -7.2372620045152605, 10.400811120202116, -11.700912510227381, 10.338945720736087, -7.1352518539922238, 3.7832770990506757, -1.4898591742830996, 0.40566381986248679, -0.0625, 
                    0.0
                 }, { 
                    1.0, 1.0, -19.28552114059622, 84.067931860237209, -227.57646588712927, 442.55091857509126, -652.003887962418, 745.11011142648249, -666.287788384173, 465.37926519421393, -250.87052014827435, 101.76777172051619, -29.623585991000859, 5.6206851329822891, -0.53899715319582009, 0.0, 
                    0.0
                 }, { 
                    0.0, 0.0, 31.345488512149121, -169.20245233591072, 501.42651504165383, -1021.9166985705199, 1546.4108058206755, -1796.0404187686615, 1622.3888488645032, -1140.5508165305837, 617.41269545217165, -251.134813665103, 73.227335983539135, -13.908172448060304, 1.3344715096928157, 0.0, 
                    0.0
                 }, { 
                    0.0, 0.0, -18.715212527006173, 155.18243849631966, -552.77430190688722, 1240.5436486722922, -1984.4166327969908, 2383.9897225964537, -2199.8482221302925, 1567.8254870532021, -856.27026606129959, 350.28953575591049, -102.50938121470136, 19.512227191700998, -1.87439250962424, 0.0, 
                    0.0
                 }, { 
                    0.0, 0.0, 13.455194301985307, -124.62014956670357, 530.90148962888259, -1337.958286307957, 2301.083768463749, -2894.5162938039816, 2751.41990944952, -1999.2841643240986, 1105.8358103279243, -456.11853654809886, 134.17702641222198, -25.620542347486417, 2.4653883528358507, 0.0, 
                    0.0
                 }, { 
                    0.0, 0.0, -10.398770390491942, 101.02754668611351, -468.1687695389374, 1315.2587540401514, -2459.4020290100475, 3278.5008421924626, -3241.0217042358222, 2417.6794777533937, -1360.7739573130973, 567.7152912479246, -168.22496713358603, 32.263604081404587, -3.1120770591203555, 0.0, 
                    0.0
                 }, { 
                    0.0, 0.0, 8.33791461793319, -83.089856178359256, 403.06636251970178, -1211.9691174662519, 2456.3218188348642, -3501.6539153474491, 3636.9601419225073, -2808.5465594345446, 1618.4549573069264, -685.8669159522002, 205.28787518960345, -39.612875501176084, 3.8336912260944862, 0.0, 
                    0.0
                 }, { 
                    0.0, 0.0, -6.81988498991258, 69.001517902260886, -344.08597030258693, 1080.2195280014976, -2322.6672359640711, 3543.0168768105455, -3902.0302752476809, 3150.3309143808156, -1873.6958454496621, 811.26017626889018, -246.23009615962036, 47.919642033522905, -4.6591957285735415, 0.0, 
                    0.0
                 }, { 
                    0.0, 0.0, 5.6319029778300989, -57.541545006845688, 292.10693335884673, -943.64125577214315, 2115.5805184995497, -3410.2405291982504, 3997.8381795228752, -3410.2405291982504, 2115.5805184995497, -943.64125577214315, 292.10693335884673, -57.541545006845688, 5.6319029778300989, 0.0, 
                    0.0
                 }, { 
                    0.0, 0.0, -4.6591957285735415, 47.919642033522905, -246.23009615962036, 811.26017626889018, -1873.6958454496621, 3150.3309143808156, -3902.0302752476809, 3543.0168768105455, -2322.6672359640711, 1080.2195280014976, -344.08597030258693, 69.001517902260886, -6.81988498991258, 0.0, 
                    0.0
                 }, { 
                    0.0, 0.0, 3.8336912260944862, -39.612875501176084, 205.28787518960345, -685.8669159522002, 1618.4549573069264, -2808.5465594345446, 3636.9601419225073, -3501.6539153474491, 2456.3218188348642, -1211.9691174662519, 403.06636251970178, -83.089856178359256, 8.33791461793319, 0.0, 
                    0.0
                 }, { 
                    0.0, 0.0, -3.1120770591203555, 32.263604081404587, -168.22496713358603, 567.7152912479246, -1360.7739573130973, 2417.6794777533937, -3241.0217042358222, 3278.5008421924626, -2459.4020290100475, 1315.2587540401514, -468.1687695389374, 101.02754668611351, -10.398770390491942, 0.0, 
                    0.0
                 }, { 
                    0.0, 0.0, 2.4653883528358507, -25.620542347486417, 134.17702641222198, -456.11853654809886, 1105.8358103279243, -1999.2841643240986, 2751.41990944952, -2894.5162938039816, 2301.083768463749, -1337.958286307957, 530.90148962888259, -124.62014956670357, 13.455194301985307, 0.0, 
                    0.0
                 }, { 
                    0.0, 0.0, -1.87439250962424, 19.512227191700998, -102.50938121470136, 350.28953575591049, -856.27026606129959, 1567.8254870532021, -2199.8482221302925, 2383.9897225964537, -1984.4166327969908, 1240.5436486722922, -552.77430190688722, 155.18243849631966, -18.715212527006173, 0.0, 
                    0.0
                 }, { 
                    0.0, 0.0, 1.3344715096928157, -13.908172448060304, 73.227335983539135, -251.134813665103, 617.41269545217165, -1140.5508165305837, 1622.3888488645032, -1796.0404187686615, 1546.4108058206755, -1021.9166985705199, 501.42651504165383, -169.20245233591072, 31.345488512149121, 0.0, 
                    0.0
                 }, 
                { 
                    0.0, 0.0, -0.53899715319582009, 5.6206851329822891, -29.623585991000859, 101.76777172051619, -250.87052014827435, 465.37926519421393, -666.287788384173, 745.11011142648249, -652.003887962418, 442.55091857509126, -227.57646588712927, 84.067931860237209, -19.28552114059622, 1.0, 
                    1.0
                 }
             };
            private static readonly double[,] matrix_17 = new double[,] { 
                { 
                    0.0, 0.058823529411764705, -0.4087967590033933, 1.6160715953108835, -4.4445044797593418, 9.1442493432512766, -14.580662146152232, 18.353706314105207, -18.39184679608482, 14.682965051284166, -9.2786031839150578, 4.5721246716256383, -1.7094247999074392, 0.4617347415173953, -0.081759351800678656, 0.0073529411764705881, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, -0.0073529411764705881, 0.081759351800678656, -0.4617347415173953, 1.7094247999074392, -4.5721246716256383, 9.2786031839150578, -14.682965051284166, 18.39184679608482, -18.353706314105207, 14.580662146152232, -9.1442493432512766, 4.4445044797593418, -1.6160715953108835, 0.4087967590033933, 
                    -0.058823529411764705, 0.0
                 }, { 
                    1.0, 1.0, -21.596798389851536, 101.76514838647977, -298.87979549488421, 634.67788586841129, -1029.6614483989813, 1309.4224481839426, -1320.4981517585504, 1058.5069227961069, -670.683275958786, 331.06489268040417, -123.92042655608466, 33.49690134681051, -5.9339606850458964, 0.5337967590033933, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, 34.936435268042558, -203.56924789931367, 654.75857289779958, -1458.1212863255751, 2431.29343183363, -3144.0886723879335, 3204.5648310663314, -2586.5972615375244, 1646.398300365255, -815.16213549895883, 305.7297184897343, -82.747480547674854, 14.670161676576116, -1.3202429295673359, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, -20.900714032690605, 185.18586320824807, -714.85659819336479, 1753.5234924530405, -3093.1968484577133, 4141.2791531583562, -4315.572452289156, 3534.2309208322026, -2271.2812036279529, 1131.7472705738689, -426.25302753272, 115.68006307191393, -20.542711142836009, 1.8504338115168757, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, 15.068293884351979, -148.83432023176644, 682.16251199754663, -1873.8444144537229, 3551.5262255240077, -4980.0578875342544, 5349.60199349645, -4470.3784579316389, 2911.9874319195869, -1464.178716737722, 554.76639613521763, -151.13931921633454, 26.903263224430063, -2.4265451970555589, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, -11.688977028412131, 121.01516959930478, -601.53598193352639, 1830.9707383898306, -3760.4402493935768, 5580.90381593509, -6234.0184210644638, 5350.6121441138785, -3549.4403708027608, 1806.8180975696389, -690.23142195476373, 189.04742941150982, -33.761152475107622, 3.05059377797098, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, 9.41846990025643, -99.979912424507418, 519.46045057273818, -1686.1767175971122, 3732.0378830216791, -5900.6123931782722, 6912.6871042376506, -6139.4432045932845, 4171.4329141157132, -2158.6927488080414, 833.84040801196079, -230.03480510161677, 41.264057597346024, -3.7377196068733824, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, -7.7530968842707111, 83.543316876365751, -445.83179688239721, 1507.694689992031, -3525.1007834670563, 5927.4733072805284, -7330.6893624502836, 6791.4853523918136, -4759.5497405182714, 2517.4466480201386, -987.03744424570857, 274.98017430333061, -49.62652783616457, 4.5103038561046134, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, 6.4562708716440422, -70.2453441478127, 381.43381582038933, -1325.5799572263165, 3222.2055210862518, -5695.7498295439545, 7445.0307782159116, -7248.5227355254419, 5283.5660025937141, -2876.70703954757, 1150.9613665517775, -324.99414205223962, 59.146196273803753, -5.4005040601696139, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, -5.4005040601696139, 59.146196273803753, -324.99414205223962, 1150.9613665517775, -2876.70703954757, 5283.5660025937141, -7248.5227355254419, 7445.0307782159116, -5695.7498295439545, 3222.2055210862518, -1325.5799572263165, 381.43381582038933, -70.2453441478127, 6.4562708716440422, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, 4.5103038561046134, -49.62652783616457, 274.98017430333061, -987.03744424570857, 2517.4466480201386, -4759.5497405182714, 6791.4853523918136, -7330.6893624502836, 5927.4733072805284, -3525.1007834670563, 1507.694689992031, -445.83179688239721, 83.543316876365751, -7.7530968842707111, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, -3.7377196068733824, 41.264057597346024, -230.03480510161677, 833.84040801196079, -2158.6927488080414, 4171.4329141157132, -6139.4432045932845, 6912.6871042376506, -5900.6123931782722, 3732.0378830216791, -1686.1767175971122, 519.46045057273818, -99.979912424507418, 9.41846990025643, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, 3.05059377797098, -33.761152475107622, 189.04742941150982, -690.23142195476373, 1806.8180975696389, -3549.4403708027608, 5350.6121441138785, -6234.0184210644638, 5580.90381593509, -3760.4402493935768, 1830.9707383898306, -601.53598193352639, 121.01516959930478, -11.688977028412131, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, -2.4265451970555589, 26.903263224430063, -151.13931921633454, 554.76639613521763, -1464.178716737722, 2911.9874319195869, -4470.3784579316389, 5349.60199349645, -4980.0578875342544, 3551.5262255240077, -1873.8444144537229, 682.16251199754663, -148.83432023176644, 15.068293884351979, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, 1.8504338115168757, -20.542711142836009, 115.68006307191393, -426.25302753272, 1131.7472705738689, -2271.2812036279529, 3534.2309208322026, -4315.572452289156, 4141.2791531583562, -3093.1968484577133, 1753.5234924530405, -714.85659819336479, 185.18586320824807, -20.900714032690605, 
                    0.0, 0.0
                 }, 
                { 
                    0.0, 0.0, -1.3202429295673359, 14.670161676576116, -82.747480547674854, 305.7297184897343, -815.16213549895883, 1646.398300365255, -2586.5972615375244, 3204.5648310663314, -3144.0886723879335, 2431.29343183363, -1458.1212863255751, 654.75857289779958, -203.56924789931367, 34.936435268042558, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, 0.5337967590033933, -5.9339606850458964, 33.49690134681051, -123.92042655608466, 331.06489268040417, -670.683275958786, 1058.5069227961069, -1320.4981517585504, 1309.4224481839426, -1029.6614483989813, 634.67788586841129, -298.87979549488421, 101.76514838647977, -21.596798389851536, 
                    1.0, 1.0
                 }
             };
            private static readonly double[,] matrix_18 = new double[,] { 
                { 
                    0.0, 0.055555555555555552, -0.41153926461631329, 1.74215875557542, -5.1576944137176515, 11.493151871486528, -19.992342824508125, 27.693598171684577, -30.866488130190838, 27.779839317171753, -20.140798670316055, 11.662199980963074, -5.3045316329937826, 1.8420337191848755, -0.46457566815344536, 0.07716361211555875, 
                    -0.0065359477124183009, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 0.0065359477124183009, -0.07716361211555875, 0.46457566815344536, -1.8420337191848755, 5.3045316329937826, -11.662199980963074, 20.140798670316055, -27.779839317171753, 30.866488130190838, -27.693598171684577, 19.992342824508125, -11.493151871486528, 5.1576944137176515, -1.74215875557542, 
                    0.41153926461631329, -0.055555555555555552, 0.0
                 }, { 
                    1.0, 1.0, -24.028972047127933, 121.71341371541649, -385.414520425721, 887.23213110554434, -1571.2154429034197, 2199.7848089129866, -2468.25438765044, 2231.0820693992832, -1622.2283855352989, 941.14162974609962, -428.63392623359715, 148.97527499918922, -37.5940277745979, 6.2463655242158289, 
                    -0.52918632343984273, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 38.715009094122188, -242.16616430758782, 840.04556781795657, -2029.1181597418683, 3695.2978842959237, -5263.6077927470151, 5971.725257407822, -5437.4393083535851, 3972.98117698842, -2312.5854890687192, 1055.6078992351288, -367.43634829552167, 92.813789723655958, -15.430693250753317, 
                    1.3077191976886517, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -23.199948792697832, 218.74023781562713, -909.40390777633218, 2420.0755492900871, -4665.5795596965954, 6885.5429762468457, -7992.9717990260333, 7389.2602676635406, -5454.64292053822, 3197.1470810610272, -1466.2621214798578, 511.99045329928731, -129.59498467782637, 21.57335702201032, 
                    -1.8296079286402105, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 16.764821484870527, -175.90639191347634, 863.0502816510924, -2565.673109886975, 5311.1639967504625, -8210.9139243792361, 9830.45441177216, -9279.4183002132268, 6947.9779990379093, -4112.2316527951934, 1898.4774121362207, -665.87553789814433, 169.03958242421285, -28.190786976324034, 
                    2.3932549419249334, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -13.045304327833685, 143.37247952384877, -760.937705427538, 2494.3207196735716, -5579.30859270273, 9118.4821483773831, -11350.106710377711, 11007.929096676768, -8398.7418937605416, 5036.0909715174921, -2345.9110380692114, 827.80984087401919, -210.98659765934315, 35.273668814040455, 
                    -2.9987139507690337, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 10.553674468121075, -118.88816983712169, 658.69172844543061, -2295.6692826234494, 5508.5321365404552, -9560.7328870337315, 12461.517169786064, -12500.244793956987, 9770.4251728165418, -5959.2896303708967, 2809.0532415264424, -999.27875248684859, 256.05044163864318, -42.950301103871126, 
                    3.6581339663951398, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -8.7326277085235766, 99.8400910217243, -567.74648037910845, 2057.715771696815, -5197.8533303016411, 9549.34751131484, -13092.694731530826, 13672.910241838059, -11015.241945965558, 6867.81534804252, -3288.0482495568112, 1182.2685380428893, -305.1005036605456, 51.406386980217512, 
                    -4.3892854551180331, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 7.3204903483782706, -84.499892215531546, 488.72171389424739, -1818.218449643643, 4763.70568050557, -9162.7499476485063, 13207.561008898203, -14434.526371553988, 12066.109882863217, -7737.0260097339951, 3780.4816941087574, -1378.8520699772594, 359.24567911717992, -60.893836921266988, 
                    5.2169026683990634, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -6.1763596357497015, 71.760236108832032, -419.88005693140616, 1590.7334315723072, -4278.5076841550981, 8524.7848585581, -12832.869305185708, 14701.894197000267, -12832.869305185708, 8524.7848585581, -4278.5076841550981, 1590.7334315723072, -419.88005693140616, 71.760236108832032, 
                    -6.1763596357497015, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 5.2169026683990634, -60.893836921266988, 359.24567911717992, -1378.8520699772594, 3780.4816941087574, -7737.0260097339951, 12066.109882863217, -14434.526371553988, 13207.561008898203, -9162.7499476485063, 4763.70568050557, -1818.218449643643, 488.72171389424739, -84.499892215531546, 
                    7.3204903483782706, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -4.3892854551180331, 51.406386980217512, -305.1005036605456, 1182.2685380428893, -3288.0482495568112, 6867.81534804252, -11015.241945965558, 13672.910241838059, -13092.694731530826, 9549.34751131484, -5197.8533303016411, 2057.715771696815, -567.74648037910845, 99.8400910217243, 
                    -8.7326277085235766, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 3.6581339663951398, -42.950301103871126, 256.05044163864318, -999.27875248684859, 2809.0532415264424, -5959.2896303708967, 9770.4251728165418, -12500.244793956987, 12461.517169786064, -9560.7328870337315, 5508.5321365404552, -2295.6692826234494, 658.69172844543061, -118.88816983712169, 
                    10.553674468121075, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -2.9987139507690337, 35.273668814040455, -210.98659765934315, 827.80984087401919, -2345.9110380692114, 5036.0909715174921, -8398.7418937605416, 11007.929096676768, -11350.106710377711, 9118.4821483773831, -5579.30859270273, 2494.3207196735716, -760.937705427538, 143.37247952384877, 
                    -13.045304327833685, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 2.3932549419249334, -28.190786976324034, 169.03958242421285, -665.87553789814433, 1898.4774121362207, -4112.2316527951934, 6947.9779990379093, -9279.4183002132268, 9830.45441177216, -8210.9139243792361, 5311.1639967504625, -2565.673109886975, 863.0502816510924, -175.90639191347634, 
                    16.764821484870527, 0.0, 0.0
                 }, 
                { 
                    0.0, 0.0, -1.8296079286402105, 21.57335702201032, -129.59498467782637, 511.99045329928731, -1466.2621214798578, 3197.1470810610272, -5454.64292053822, 7389.2602676635406, -7992.9717990260333, 6885.5429762468457, -4665.5795596965954, 2420.0755492900871, -909.40390777633218, 218.74023781562713, 
                    -23.199948792697832, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 1.3077191976886517, -15.430693250753317, 92.813789723655958, -367.43634829552167, 1055.6078992351288, -2312.5854890687192, 3972.98117698842, -5437.4393083535851, 5971.725257407822, -5263.6077927470151, 3695.2978842959237, -2029.1181597418683, 840.04556781795657, -242.16616430758782, 
                    38.715009094122188, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -0.52918632343984273, 6.2463655242158289, -37.5940277745979, 148.97527499918922, -428.63392623359715, 941.14162974609962, -1622.2283855352989, 2231.0820693992832, -2468.25438765044, 2199.7848089129866, -1571.2154429034197, 887.23213110554434, -385.414520425721, 121.71341371541649, 
                    -24.028972047127933, 1.0, 1.0
                 }
             };
            private static readonly double[,] matrix_19 = new double[,] { 
                { 
                    0.0, 0.052631578947368418, -0.41395961250733365, 1.8681364675844065, -5.922800269321959, 14.20799272832425, -26.768675494690342, 40.452430172758234, -49.61640274133552, 49.6678340196035, -40.595238606547241, 26.968286781838824, -14.413902189448645, 6.0891397407103929, -1.9742667564406531, 0.46703411689610164, 
                    -0.073051696324823584, 0.0058479532163742687, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -0.0058479532163742687, 0.073051696324823584, -0.46703411689610164, 1.9742667564406531, -6.0891397407103929, 14.413902189448645, -26.968286781838824, 40.595238606547241, -49.6678340196035, 49.61640274133552, -40.452430172758234, 26.768675494690342, -14.20799272832425, 5.922800269321959, 
                    -1.8681364675844065, 0.41395961250733365, -0.052631578947368418, 0.0
                 }, { 
                    1.0, 1.0, -26.582123882609306, 144.04334305449817, -489.16704479255577, 1213.2341321166414, -2328.3556341546423, 3557.6815357698433, -4394.2074734476619, 4418.93523031247, -3622.8635322007312, 2411.7948251282651, -1290.9036194725677, 545.88004636334779, -177.10799628687749, 41.915373108865296, 
                    -6.558067513947865, 0.52507072361844476, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 42.681356722757485, -285.232739000818, 1061.3555991474855, -2763.4290600905797, 5456.3957294339752, -8486.0060357597085, 10602.062895088418, -10743.398219416586, 8853.7638411746411, -5915.1368085338063, 3173.8627095745742, -1344.400210156454, 436.68760396491285, -103.42796816546387, 
                    16.190090226460836, -1.2966097244094694, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -25.6130519218073, 256.04011428174158, -1140.4016887341511, 3271.7003406164868, -6842.3036239782123, 11032.642871504962, -14112.364552308383, 14528.145394885238, -12102.563273544994, 8145.9778127883665, -4393.4194202070357, 1867.6090321582944, -608.10535646009782, 144.2587015699626, 
                    -22.604301109633948, 1.8113349345824163, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 18.544932644113882, -205.99178862945462, 1077.132548927643, -3444.6396577905157, 7730.7900067661576, -13059.202095935922, 17235.801802126625, -18127.352775387903, 15325.972117605555, -10422.402194793023, 5661.6778822001079, -2418.7390626497145, 790.2367597149298, -187.88824807696545, 
                    29.482554622443821, -2.3644036299827174, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -14.46794739033257, 168.22801038477985, -949.49130500678541, 3334.4357391392368, -8066.3873078821816, 14389.739817408936, -19740.791272014932, 21336.595136436568, -18390.391854509944, 12677.688945798736, -6953.0268308138839, 2990.3078047205208, -981.45650766089136, 234.06443450214269, 
                    -36.798846038483866, 2.954353141706028, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 11.743782422341647, -139.92405387974392, 823.494637736151, -3067.0151258108463, 7929.5819102201385, -14982.521945374467, 21492.834254358473, -24015.099019979298, 21206.602583053937, -14876.470649140618, 8261.0824577194144, -3584.0946060233177, 1183.4634698424773, -283.37948130089342, 
                    44.665440935453, -3.5910821290369026, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -9.75881346029607, 117.98713636507921, -712.25233894202256, 2754.3747284307974, -7475.4205879541614, 14894.686451123474, -22410.22376946381, 26024.437149329842, -23670.03638666798, 16973.069171058884, -9576.9336326234934, 4202.4601221664916, -1398.6455016420355, 336.68254104079813, 
                    -53.245032261601168, 4.2890145990137656, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 8.2250070310477881, -100.38970548589353, 616.13876125466209, -2443.4241562713155, 6864.63379423734, -14273.576824982823, 22486.058870711455, -27236.438166449596, 25651.359108076031, -18899.935541196432, 10882.937373575311, -4846.3850568115258, 1629.7240943194818, -395.05617726907593, 
                    62.754129212486951, -5.0677194888304689, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -6.9873500883950941, 85.837853896539215, -532.84676169656734, 2150.4936679331518, -6193.9780017659787, 13308.360843008595, -21811.071860502321, 27562.96961087899, -26999.63694359623, 20558.315544341767, -12146.081536715317, 5513.4056453099211, -1879.5271740649564, 459.86841669677045, 
                    -73.484139059905687, 5.9542494965184405, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 5.9542494965184405, -73.484139059905687, 459.86841669677045, -1879.5271740649564, 5513.4056453099211, -12146.081536715317, 20558.315544341767, -26999.63694359623, 27562.96961087899, -21811.071860502321, 13308.360843008595, -6193.9780017659787, 2150.4936679331518, -532.84676169656734, 
                    85.837853896539215, -6.9873500883950941, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -5.0677194888304689, 62.754129212486951, -395.05617726907593, 1629.7240943194818, -4846.3850568115258, 10882.937373575311, -18899.935541196432, 25651.359108076031, -27236.438166449596, 22486.058870711455, -14273.576824982823, 6864.63379423734, -2443.4241562713155, 616.13876125466209, 
                    -100.38970548589353, 8.2250070310477881, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 4.2890145990137656, -53.245032261601168, 336.68254104079813, -1398.6455016420355, 4202.4601221664916, -9576.9336326234934, 16973.069171058884, -23670.03638666798, 26024.437149329842, -22410.22376946381, 14894.686451123474, -7475.4205879541614, 2754.3747284307974, -712.25233894202256, 
                    117.98713636507921, -9.75881346029607, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -3.5910821290369026, 44.665440935453, -283.37948130089342, 1183.4634698424773, -3584.0946060233177, 8261.0824577194144, -14876.470649140618, 21206.602583053937, -24015.099019979298, 21492.834254358473, -14982.521945374467, 7929.5819102201385, -3067.0151258108463, 823.494637736151, 
                    -139.92405387974392, 11.743782422341647, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 2.954353141706028, -36.798846038483866, 234.06443450214269, -981.45650766089136, 2990.3078047205208, -6953.0268308138839, 12677.688945798736, -18390.391854509944, 21336.595136436568, -19740.791272014932, 14389.739817408936, -8066.3873078821816, 3334.4357391392368, -949.49130500678541, 
                    168.22801038477985, -14.46794739033257, 0.0, 0.0
                 }, 
                { 
                    0.0, 0.0, -2.3644036299827174, 29.482554622443821, -187.88824807696545, 790.2367597149298, -2418.7390626497145, 5661.6778822001079, -10422.402194793023, 15325.972117605555, -18127.352775387903, 17235.801802126625, -13059.202095935922, 7730.7900067661576, -3444.6396577905157, 1077.132548927643, 
                    -205.99178862945462, 18.544932644113882, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 1.8113349345824163, -22.604301109633948, 144.2587015699626, -608.10535646009782, 1867.6090321582944, -4393.4194202070357, 8145.9778127883665, -12102.563273544994, 14528.145394885238, -14112.364552308383, 11032.642871504962, -6842.3036239782123, 3271.7003406164868, -1140.4016887341511, 
                    256.04011428174158, -25.6130519218073, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -1.2966097244094694, 16.190090226460836, -103.42796816546387, 436.68760396491285, -1344.400210156454, 3173.8627095745742, -5915.1368085338063, 8853.7638411746411, -10743.398219416586, 10602.062895088418, -8486.0060357597085, 5456.3957294339752, -2763.4290600905797, 1061.3555991474855, 
                    -285.232739000818, 42.681356722757485, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 0.52507072361844476, -6.558067513947865, 41.915373108865296, -177.10799628687749, 545.88004636334779, -1290.9036194725677, 2411.7948251282651, -3622.8635322007312, 4418.93523031247, -4394.2074734476619, 3557.6815357698433, -2328.3556341546423, 1213.2341321166414, -489.16704479255577, 
                    144.04334305449817, -26.582123882609306, 1.0, 1.0
                 }
             };
            private static readonly double[,] matrix_20 = new double[,] { 
                { 
                    0.0, 0.05, -0.41611110946738666, 1.99401826901664, -6.7397854239875485, 17.31480171510298, -35.119118161805709, 57.482712435680774, -76.917246334342408, 84.73215952663881, -77.029235933308016, 57.68793475075681, -35.373976883495857, 17.559559080902854, -6.9259206860411915, 2.1061829449961089, 
                    -0.46918076918038587, 0.069351851577897777, -0.005263157894736842, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 0.005263157894736842, -0.069351851577897777, 0.46918076918038587, -2.1061829449961089, 6.9259206860411915, -17.559559080902854, 35.373976883495857, -57.68793475075681, 77.029235933308016, -84.73215952663881, 76.917246334342408, -57.482712435680774, 35.119118161805709, -17.31480171510298, 
                    6.7397854239875485, -1.99401826901664, 0.41611110946738666, -0.05, 0.0
                 }, { 
                    1.0, 1.0, -29.256319105461984, 168.88557662994859, -612.24326515746156, 1627.4256957546143, -3363.9734305115444, 5569.3108203134243, -7506.531648762968, 8308.92968356362, -7578.1580984904558, 5688.1004134362429, -3493.3795609389708, 1736.0064673484324, -685.24692328565288, 208.49420308644085, 
                    -46.461250077721829, 6.8691942602778484, -0.52137426736212344, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 46.835595594324921, -333.0085515090791, 1322.9961600808806, -3693.2781159642091, 7857.7706413131355, -13246.325843055842, 18065.686231832831, -20155.944737242982, 18483.566019632784, -13926.300488312412, 8575.7105835970233, -4269.61349138397, 1687.5357493204185, -513.91577364569991, 
                    114.59129032070334, -16.9485890154424, 1.2866867344984492, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -28.140132129851995, 297.28007543098875, -1412.0622983611827, 4343.8665770727257, -9793.4503039787269, 17125.936487211409, -23927.075839294896, 27134.733250799942, -25165.031314214189, 19110.171544806555, -11833.250870640186, 5914.48806472209, -2344.0594786651295, 715.19871590775483, 
                    -159.67452231265381, 23.635608866557835, -1.7951705054920026, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 20.408752286113771, -239.24595763962444, 1328.1766096733979, -4545.7818514511737, 10991.917655215433, -20138.748781663802, 29040.884889277939, -33661.793810411014, 31699.40856456686, -24333.632848308018, 15183.431327017364, -7630.207791876448, 3035.5817699156642, -928.63290556584934, 
                    207.69265752190435, -30.778051401806081, 2.3391577742818841, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -15.957061939718999, 195.71030750103162, -1170.48708785263, 4383.9852818929166, -11402.172123722865, 22039.977565493809, -33027.688387655042, 39348.608406316409, -37789.701432970141, 29419.907961637506, -18543.059830572176, 9385.75613714564, -3753.0073680366631, 1152.1557098205367, 
                    -258.29580865933815, 38.334770130444142, -2.91599101247908, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 12.988994888165236, -163.19698151117737, 1016.7546563651254, -4029.9950229251153, 11167.466755642994, -22811.770111602615, 35701.503179977342, -43949.982822718594, 43243.891411506804, -34268.830500890886, 21879.64126308123, -11177.946608347705, 4499.2519576446721, -1387.6207035074572, 
                    312.05074617040509, -46.4045708239085, 3.5338341461565368, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -10.831917761344684, 138.07970954884371, -881.90516041035937, 3624.6711479539904, -10518.838635956708, 22589.383793814734, -36989.746839275525, 47255.7274487544, -47858.186962383377, 38762.720119198064, -25153.254209798935, 13003.401303233644, -5278.6655651956708, 1637.7231170393848, 
                    -369.78180564467135, 55.131501391739008, -4.20460960036931, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 9.17016786568142, -117.99921880800117, 765.97244384481746, -3225.7055383392421, 9674.1049513977359, -21623.069432782046, 36953.004231985935, -49107.66280238569, 51408.39554924758, -42746.998373358219, 28302.193868439223, -14852.155872403246, 6095.1652919534226, -1905.6516329809147, 
                    432.53551026113075, -64.70418427806861, 4.9441600316591341, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -7.8339309005530993, 101.45501841902818, -665.96115446403735, 2852.571468590078, -8760.7041388913512, 20193.158224429138, -35791.785802133389, 49443.3930676781, -53667.225394845642, 46021.371736563538, -31230.792613212758, 16701.722661778796, -6950.7590858200065, 2194.9858566349881, 
                    -501.62917911968862, 75.371001474261973, -5.7737793793830461, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 6.7229372811349757, -87.46665866601397, 578.73145782137976, -2509.4962293737681, 7843.0822816608406, -18508.30359237544, 33795.510027075958, -48341.517249126555, 54447.083315899559, -48341.517249126555, 33795.510027075958, -18508.30359237544, 7843.0822816608406, -2509.4962293737681, 
                    578.73145782137976, -87.46665866601397, 6.7229372811349757, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -5.7737793793830461, 75.371001474261973, -501.62917911968862, 2194.9858566349881, -6950.7590858200065, 16701.722661778796, -31230.792613212758, 46021.371736563538, -53667.225394845642, 49443.3930676781, -35791.785802133389, 20193.158224429138, -8760.7041388913512, 2852.571468590078, 
                    -665.96115446403735, 101.45501841902818, -7.8339309005530993, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 4.9441600316591341, -64.70418427806861, 432.53551026113075, -1905.6516329809147, 6095.1652919534226, -14852.155872403246, 28302.193868439223, -42746.998373358219, 51408.39554924758, -49107.66280238569, 36953.004231985935, -21623.069432782046, 9674.1049513977359, -3225.7055383392421, 
                    765.97244384481746, -117.99921880800117, 9.17016786568142, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -4.20460960036931, 55.131501391739008, -369.78180564467135, 1637.7231170393848, -5278.6655651956708, 13003.401303233644, -25153.254209798935, 38762.720119198064, -47858.186962383377, 47255.7274487544, -36989.746839275525, 22589.383793814734, -10518.838635956708, 3624.6711479539904, 
                    -881.90516041035937, 138.07970954884371, -10.831917761344684, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 3.5338341461565368, -46.4045708239085, 312.05074617040509, -1387.6207035074572, 4499.2519576446721, -11177.946608347705, 21879.64126308123, -34268.830500890886, 43243.891411506804, -43949.982822718594, 35701.503179977342, -22811.770111602615, 11167.466755642994, -4029.9950229251153, 
                    1016.7546563651254, -163.19698151117737, 12.988994888165236, 0.0, 0.0
                 }, 
                { 
                    0.0, 0.0, -2.91599101247908, 38.334770130444142, -258.29580865933815, 1152.1557098205367, -3753.0073680366631, 9385.75613714564, -18543.059830572176, 29419.907961637506, -37789.701432970141, 39348.608406316409, -33027.688387655042, 22039.977565493809, -11402.172123722865, 4383.9852818929166, 
                    -1170.48708785263, 195.71030750103162, -15.957061939718999, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 2.3391577742818841, -30.778051401806081, 207.69265752190435, -928.63290556584934, 3035.5817699156642, -7630.207791876448, 15183.431327017364, -24333.632848308018, 31699.40856456686, -33661.793810411014, 29040.884889277939, -20138.748781663802, 10991.917655215433, -4545.7818514511737, 
                    1328.1766096733979, -239.24595763962444, 20.408752286113771, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -1.7951705054920026, 23.635608866557835, -159.67452231265381, 715.19871590775483, -2344.0594786651295, 5914.48806472209, -11833.250870640186, 19110.171544806555, -25165.031314214189, 27134.733250799942, -23927.075839294896, 17125.936487211409, -9793.4503039787269, 4343.8665770727257, 
                    -1412.0622983611827, 297.28007543098875, -28.140132129851995, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 1.2866867344984492, -16.9485890154424, 114.59129032070334, -513.91577364569991, 1687.5357493204185, -4269.61349138397, 8575.7105835970233, -13926.300488312412, 18483.566019632784, -20155.944737242982, 18065.686231832831, -13246.325843055842, 7857.7706413131355, -3693.2781159642091, 
                    1322.9961600808806, -333.0085515090791, 46.835595594324921, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -0.52137426736212344, 6.8691942602778484, -46.461250077721829, 208.49420308644085, -685.24692328565288, 1736.0064673484324, -3493.3795609389708, 5688.1004134362429, -7578.1580984904558, 8308.92968356362, -7506.531648762968, 5569.3108203134243, -3363.9734305115444, 1627.4256957546143, 
                    -612.24326515746156, 168.88557662994859, -29.256319105461984, 1.0, 1.0
                 }
             };
            private static readonly double[,] matrix_21 = new double[,] { 
                { 
                    0.0, 0.047619047619047616, -0.41803596721576425, 2.1198157316213955, -7.6086207253466345, 20.839606112625834, -45.267899100404122, 79.766731141197724, -115.60800738128259, 138.93777919688412, -138.99620183244366, 115.78148266407011, -80.036312802426409, 45.580989223541557, -21.125019580188592, 7.8148522922346872, 
                    -2.2378296251019516, 0.47107016258253237, -0.066005679034068038, 0.0047619047619047623, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -0.0047619047619047623, 0.066005679034068038, -0.47107016258253237, 2.2378296251019516, -7.8148522922346872, 21.125019580188592, -45.580989223541557, 80.036312802426409, -115.78148266407011, 138.99620183244366, -138.93777919688412, 115.60800738128259, -79.766731141197724, 45.267899100404122, 
                    -20.839606112625834, 7.6086207253466345, -2.1198157316213955, 0.41803596721576425, -0.047619047619047616, 0.0
                 }, { 
                    1.0, 1.0, -32.05161035399297, 196.37077323437347, -756.86857050997139, 2146.3753592783542, -4753.6552682154488, 8475.3134879404952, -12376.024340262098, 14947.905591905381, -15005.275207009312, 12528.887267751572, -8675.3985606851165, 4946.5728464443146, -2294.492258572031, 849.31947272037371, 
                    -243.3094591559755, 51.231902404310951, -7.1798444341315646, 0.51803596721576428, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 51.177820808925944, -385.73321266564545, 1629.5141351394766, -4854.8145419005832, 11071.132110633689, -20105.545391463649, 29716.235882162771, -36186.846910337437, 36532.4297613338, -30625.353520853379, 21266.24653748484, -12150.290621819398, 5644.1411513045123, -2091.3576562834573, 
                    599.55270292412126, -126.30472714805997, 17.706366155454582, -1.2777690842838423, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -30.781277838931189, 342.6547270396494, -1728.8249861891481, 5676.2430955945465, -13722.206366906346, 25863.168477425894, -39178.624110513731, 48517.047003606007, -49555.930542519825, 41887.25518067243, -29258.141787332108, 16786.959856514859, -7821.5293083220222, 2904.3621187422445, 
                    -833.86968722433926, 175.84490274111178, -24.667305352835943, 1.7807682667829232, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 22.356381849561611, -275.82436318302257, 1620.149298740726, -5908.1999916344621, 15310.636658356414, -30234.375497033492, 47285.803675494608, -59873.621096705785, 62124.571237349446, -53103.5775307352, 37393.0261781277, -21579.2982319128, 10096.418171768275, -3760.2339633563461, 
                    1081.8412714184119, -228.45810882449618, 32.076821172168579, -2.316880937481399, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -17.512773714379367, 225.94792370208611, -1427.3883006956364, 5679.3815909638251, -15800.995887092931, 32891.107670370155, -53442.611622213684, 69559.86323090292, -73627.836431463438, 63853.018537183591, -45437.060214579593, 26422.105186939847, -12430.47116368497, 4647.7497420971667, 
                    -1340.8769594799282, 283.69108556725195, -39.879864024606277, 2.8824919152533055, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 14.289473028955967, -188.81636115369633, 1241.5082942789504, -5217.7825574965927, 15426.456873797413, -33868.686287569821, 57410.384157573157, -77175.786036058809, 83688.536427185812, -73892.86425027877, 53282.747917911176, -31287.17090129713, 14823.75839273661, -5570.8254061411262, 
                    1612.939827837032, -342.08346571295289, 48.163784036929087, -3.4843999806158417, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -11.952150109289166, 160.21303315792503, -1079.3941819487861, 4698.6747146270645, -14518.914790974739, 33426.998298409482, -59162.612662985586, 82428.2691572364, -91943.141545156075, 82959.923257609087, -60807.554527851025, 36143.466421887955, -17278.748619261431, 6535.62424706458, 
                    -1900.9676610218644, 404.43334560262258, -57.057296087155429, 4.1326106719967868, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 10.156246375711525, -137.41279559551128, 940.62952912186427, -4192.3590704187936, 13368.778812951065, -31964.814013143066, 58888.714088115034, -85156.052203964791, 98036.760067078838, -90740.965401835114, 67844.636958722287, -40940.0813633358, 19793.518026944548, -7548.6474726099614, 
                    2208.498299732029, -471.7485069914681, 66.726686831388974, -4.8401970911003067, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -8.7164588102791427, 118.6875785397386, -821.39789643070333, 3721.8417960622064, -12141.732538846883, 29887.153028783963, -56966.290346144742, 85382.442615538923, -101662.94995177287, 96871.275681633473, -74164.543704551223, 45592.8083059189, -22356.581792971963, 8615.6642977686388, 
                    -2539.6318801872271, 545.28792568139954, -77.386767978058046, 5.6243201738515367, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 7.5234289784657964, -102.91045393892782, 717.81626761715313, -3291.5308250304056, 10919.942344756233, -27484.810549230297, 53858.370782559039, -83347.071007837745, 102633.28846104228, -100957.05149721632, 79460.890858468, -49967.41700312552, 24939.478175568798, -9740.0973284605789, 
                    2898.9939751125817, -626.63794244364465, 89.32057054387684, -6.50806011636793, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -6.50806011636793, 89.32057054387684, -626.63794244364465, 2898.9939751125817, -9740.0973284605789, 24939.478175568798, -49967.41700312552, 79460.890858468, -100957.05149721632, 102633.28846104228, -83347.071007837745, 53858.370782559039, -27484.810549230297, 10919.942344756233, 
                    -3291.5308250304056, 717.81626761715313, -102.91045393892782, 7.5234289784657964, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 5.6243201738515367, -77.386767978058046, 545.28792568139954, -2539.6318801872271, 8615.6642977686388, -22356.581792971963, 45592.8083059189, -74164.543704551223, 96871.275681633473, -101662.94995177287, 85382.442615538923, -56966.290346144742, 29887.153028783963, -12141.732538846883, 
                    3721.8417960622064, -821.39789643070333, 118.6875785397386, -8.7164588102791427, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -4.8401970911003067, 66.726686831388974, -471.7485069914681, 2208.498299732029, -7548.6474726099614, 19793.518026944548, -40940.0813633358, 67844.636958722287, -90740.965401835114, 98036.760067078838, -85156.052203964791, 58888.714088115034, -31964.814013143066, 13368.778812951065, 
                    -4192.3590704187936, 940.62952912186427, -137.41279559551128, 10.156246375711525, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 4.1326106719967868, -57.057296087155429, 404.43334560262258, -1900.9676610218644, 6535.62424706458, -17278.748619261431, 36143.466421887955, -60807.554527851025, 82959.923257609087, -91943.141545156075, 82428.2691572364, -59162.612662985586, 33426.998298409482, -14518.914790974739, 
                    4698.6747146270645, -1079.3941819487861, 160.21303315792503, -11.952150109289166, 0.0, 0.0
                 }, 
                { 
                    0.0, 0.0, -3.4843999806158417, 48.163784036929087, -342.08346571295289, 1612.939827837032, -5570.8254061411262, 14823.75839273661, -31287.17090129713, 53282.747917911176, -73892.86425027877, 83688.536427185812, -77175.786036058809, 57410.384157573157, -33868.686287569821, 15426.456873797413, 
                    -5217.7825574965927, 1241.5082942789504, -188.81636115369633, 14.289473028955967, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 2.8824919152533055, -39.879864024606277, 283.69108556725195, -1340.8769594799282, 4647.7497420971667, -12430.47116368497, 26422.105186939847, -45437.060214579593, 63853.018537183591, -73627.836431463438, 69559.86323090292, -53442.611622213684, 32891.107670370155, -15800.995887092931, 
                    5679.3815909638251, -1427.3883006956364, 225.94792370208611, -17.512773714379367, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -2.316880937481399, 32.076821172168579, -228.45810882449618, 1081.8412714184119, -3760.2339633563461, 10096.418171768275, -21579.2982319128, 37393.0261781277, -53103.5775307352, 62124.571237349446, -59873.621096705785, 47285.803675494608, -30234.375497033492, 15310.636658356414, 
                    -5908.1999916344621, 1620.149298740726, -275.82436318302257, 22.356381849561611, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 1.7807682667829232, -24.667305352835943, 175.84490274111178, -833.86968722433926, 2904.3621187422445, -7821.5293083220222, 16786.959856514859, -29258.141787332108, 41887.25518067243, -49555.930542519825, 48517.047003606007, -39178.624110513731, 25863.168477425894, -13722.206366906346, 
                    5676.2430955945465, -1728.8249861891481, 342.6547270396494, -30.781277838931189, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -1.2777690842838423, 17.706366155454582, -126.30472714805997, 599.55270292412126, -2091.3576562834573, 5644.1411513045123, -12150.290621819398, 21266.24653748484, -30625.353520853379, 36532.4297613338, -36186.846910337437, 29716.235882162771, -20105.545391463649, 11071.132110633689, 
                    -4854.8145419005832, 1629.5141351394766, -385.73321266564545, 51.177820808925944, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 0.51803596721576428, -7.1798444341315646, 51.231902404310951, -243.3094591559755, 849.31947272037371, -2294.492258572031, 4946.5728464443146, -8675.3985606851165, 12528.887267751572, -15005.275207009312, 14947.905591905381, -12376.024340262098, 8475.3134879404952, -4753.6552682154488, 
                    2146.3753592783542, -756.86857050997139, 196.37077323437347, -32.05161035399297, 1.0, 1.0
                 }
             };
            private static readonly double[,] matrix_22 = new double[,] { 
                { 
                    0.0, 0.045454545454545456, -0.41976803611360441, 2.24553875491122, -8.5292826854909354, 24.80843134642684, -57.4540168665534, 108.42565659080772, -169.17632077546409, 220.21852310408673, -240.3506789310857, 220.32145568682856, -169.39886392622057, 108.75620621279835, -57.827016848430787, 25.136132379117111, 
                    -8.75591694579771, 2.3692451904141487, -0.472745001033941, 0.062965205417040662, -0.004329004329004329, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 0.004329004329004329, -0.062965205417040662, 0.472745001033941, -2.3692451904141487, 8.75591694579771, -25.136132379117111, 57.827016848430787, -108.75620621279835, 169.39886392622057, -220.32145568682856, 240.3506789310857, -220.21852310408673, 169.17632077546409, -108.42565659080772, 
                    57.4540168665534, -24.80843134642684, 8.5292826854909354, -2.24553875491122, 0.41976803611360441, -0.045454545454545456, 0.0
                 }, { 
                    1.0, 1.0, -34.968040585381793, 226.62960567332735, -925.38784221565913, 2788.5837912350567, -6587.2684188730609, 12581.806202709058, -19783.738465571849, 25886.152623264144, -28353.308258925139, 26055.571734435835, -20069.117153585968, 12901.161597283426, -6866.0670249514324, 2986.5248761839853, 
                    -1040.8238566470154, 281.72929168382274, -56.227522783998367, 7.4900954920691385, -0.51500613135169959, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 55.708110191175244, -443.64635697920147, 1985.69579840596, -6288.3353361913651, 15300.231321900597, -29775.83646814969, 47402.315690983749, -62548.874700268272, 68914.396739521908, -63594.517384431208, 49130.304185908411, -31651.340138212297, 16871.541127083798, -7346.9555181885853, 
                    2562.555542028369, -694.02991196557423, 138.56903079341538, -18.463555841441295, 1.2697108590285322, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -33.536561685596276, 392.35869225486846, -2095.3558891739253, 7312.9307957435694, -18868.851301332077, 38126.854879083468, -62236.410466481182, 83547.037427451258, -93166.292953522963, 86716.310103142867, -67409.26754653416, 43622.679650128426, -23328.782502925995, 10182.899949261973, 
                    -3557.7428340735796, 964.71610590659088, -192.77170095126206, 25.699392636243243, -1.7678540857549485, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 24.38790453133409, -315.88248217511295, 1957.2169855450447, -7575.2768876505761, 20941.693926812382, -44334.916350889041, 74734.430439699077, -102614.83579320347, 116285.10173090811, -109496.85297692473, 85837.823388571938, -55890.818049845322, 30023.982718006566, -13148.121057327995, 
                    4604.5344642574846, -1250.6352105196515, 250.18851060782791, -33.378466981141408, 2.2970785321648055, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -19.135185319655179, 259.06941696795315, -1723.8311284595734, 7260.9799841341073, -21514.964272192745, 47975.206503453694, -83996.4551962663, 118561.07058035003, -137091.86871408852, 131010.8318459904, -103825.17530870916, 68145.404096084487, -36822.590325109159, 16194.64157631146, 
                    -5689.0159147974264, 1548.5794982900866, -310.25767800949308, 41.432828229694223, -2.8529900998616253, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 15.645347452541326, -216.89159360905953, 1500.9430924875803, -6667.1244684497688, 20946.500267451651, -49180.642092927759, 89741.75010824381, -130768.72827506623, 154896.26057055808, -150729.25685254356, 121079.43078508135, -80275.0785200657, 43701.837720140567, -19325.782146089332, 
                    6815.9788680315523, -1860.5853084642938, 373.49085309726775, -49.939952508924677, 3.441294047025091, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -13.11967898818115, 184.48230170028845, -1307.5420824914549, 6009.6876915923967, -19699.641347990113, 48399.992234645368, -92053.861071887892, 138865.67118512915, -169095.55636141944, 168119.63602389969, -137287.92137445623, 92161.035856720453, -50642.449390892485, 22550.124778538411, 
                    -7993.2647227309044, 2189.787187882177, -440.6600382779896, 59.0157722739676, -4.0704957843089158, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 11.183460854559742, -158.71474045592976, 1142.6360367181439, -5373.6163014413123, 18156.010031141286, -46240.599055102823, 91344.450898763491, -142749.37907158327, 179174.78582110192, -182603.56551732359, 152062.57025993816, -103637.10456316239, 57608.465577658077, -25874.029785923794, 
                    9229.6947183226748, -2539.9936021631311, 512.73484735199077, -68.808406444120834, 4.7515576898227705, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -9.6352165180497433, 137.6112813684893, -1001.4393235216821, 4785.8589629443686, -16528.479497647932, 43275.057998194, -88264.2766550152, 142634.18494703286, -184776.11621622767, 193574.44457811755, -164918.17121988311, 114464.26234274929, -64533.420988094236, 29297.136643665774, 
                    -10534.266996274187, 2915.67839316135, -590.91584699077373, 79.5060098862172, -5.4985838829518681, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 8.356089442408793, -119.88442786748956, 879.20245368870258, -4250.862736563975, 14920.676975108963, -39902.112684095984, 83528.647472009776, -139044.21459015121, 185793.64842125692, -200456.64188597904, 175268.09342087037, -124304.84314300514, 71303.08753441539, -32806.353513711438, 
                    11915.140282405921, -3322.0327799287152, 676.70267536611141, -91.35161323160294, 6.3300891877611738, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -7.27102970672837, 104.66619961090635, -771.99123118623277, 3764.9608407648375, -13377.697234901843, 36366.103030359256, -77731.457199808588, 132697.55653836974, -182448.62021636483, 202809.08066323158, -182448.62021636483, 132697.55653836974, -77731.457199808588, 36366.103030359256, 
                    -13377.697234901843, 3764.9608407648375, -771.99123118623277, 104.66619961090635, -7.27102970672837, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 6.3300891877611738, -91.35161323160294, 676.70267536611141, -3322.0327799287152, 11915.140282405921, -32806.353513711438, 71303.08753441539, -124304.84314300514, 175268.09342087037, -200456.64188597904, 185793.64842125692, -139044.21459015121, 83528.647472009776, -39902.112684095984, 
                    14920.676975108963, -4250.862736563975, 879.20245368870258, -119.88442786748956, 8.356089442408793, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -5.4985838829518681, 79.5060098862172, -590.91584699077373, 2915.67839316135, -10534.266996274187, 29297.136643665774, -64533.420988094236, 114464.26234274929, -164918.17121988311, 193574.44457811755, -184776.11621622767, 142634.18494703286, -88264.2766550152, 43275.057998194, 
                    -16528.479497647932, 4785.8589629443686, -1001.4393235216821, 137.6112813684893, -9.6352165180497433, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 4.7515576898227705, -68.808406444120834, 512.73484735199077, -2539.9936021631311, 9229.6947183226748, -25874.029785923794, 57608.465577658077, -103637.10456316239, 152062.57025993816, -182603.56551732359, 179174.78582110192, -142749.37907158327, 91344.450898763491, -46240.599055102823, 
                    18156.010031141286, -5373.6163014413123, 1142.6360367181439, -158.71474045592976, 11.183460854559742, 0.0, 0.0
                 }, 
                { 
                    0.0, 0.0, -4.0704957843089158, 59.0157722739676, -440.6600382779896, 2189.787187882177, -7993.2647227309044, 22550.124778538411, -50642.449390892485, 92161.035856720453, -137287.92137445623, 168119.63602389969, -169095.55636141944, 138865.67118512915, -92053.861071887892, 48399.992234645368, 
                    -19699.641347990113, 6009.6876915923967, -1307.5420824914549, 184.48230170028845, -13.11967898818115, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 3.441294047025091, -49.939952508924677, 373.49085309726775, -1860.5853084642938, 6815.9788680315523, -19325.782146089332, 43701.837720140567, -80275.0785200657, 121079.43078508135, -150729.25685254356, 154896.26057055808, -130768.72827506623, 89741.75010824381, -49180.642092927759, 
                    20946.500267451651, -6667.1244684497688, 1500.9430924875803, -216.89159360905953, 15.645347452541326, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -2.8529900998616253, 41.432828229694223, -310.25767800949308, 1548.5794982900866, -5689.0159147974264, 16194.64157631146, -36822.590325109159, 68145.404096084487, -103825.17530870916, 131010.8318459904, -137091.86871408852, 118561.07058035003, -83996.4551962663, 47975.206503453694, 
                    -21514.964272192745, 7260.9799841341073, -1723.8311284595734, 259.06941696795315, -19.135185319655179, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 2.2970785321648055, -33.378466981141408, 250.18851060782791, -1250.6352105196515, 4604.5344642574846, -13148.121057327995, 30023.982718006566, -55890.818049845322, 85837.823388571938, -109496.85297692473, 116285.10173090811, -102614.83579320347, 74734.430439699077, -44334.916350889041, 
                    20941.693926812382, -7575.2768876505761, 1957.2169855450447, -315.88248217511295, 24.38790453133409, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -1.7678540857549485, 25.699392636243243, -192.77170095126206, 964.71610590659088, -3557.7428340735796, 10182.899949261973, -23328.782502925995, 43622.679650128426, -67409.26754653416, 86716.310103142867, -93166.292953522963, 83547.037427451258, -62236.410466481182, 38126.854879083468, 
                    -18868.851301332077, 7312.9307957435694, -2095.3558891739253, 392.35869225486846, -33.536561685596276, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 1.2697108590285322, -18.463555841441295, 138.56903079341538, -694.02991196557423, 2562.555542028369, -7346.9555181885853, 16871.541127083798, -31651.340138212297, 49130.304185908411, -63594.517384431208, 68914.396739521908, -62548.874700268272, 47402.315690983749, -29775.83646814969, 
                    15300.231321900597, -6288.3353361913651, 1985.69579840596, -443.64635697920147, 55.708110191175244, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -0.51500613135169959, 7.4900954920691385, -56.227522783998367, 281.72929168382274, -1040.8238566470154, 2986.5248761839853, -6866.0670249514324, 12901.161597283426, -20069.117153585968, 26055.571734435835, -28353.308258925139, 25886.152623264144, -19783.738465571849, 12581.806202709058, 
                    -6587.2684188730609, 2788.5837912350567, -925.38784221565913, 226.62960567332735, -34.968040585381793, 1.0, 1.0
                 }
             };
            private static readonly double[,] matrix_23 = new double[,] { 
                { 
                    0.0, 0.043478260869565216, -0.421334770897178, 2.3711958266381794, -9.501752173337696, 29.247301453742018, -71.931240313247315, 144.72865128406133, -241.84975046692531, 338.92492309563704, -400.6619329694712, 400.68109040555782, -339.02163558955255, 242.08923078259787, -145.1098502801552, 72.364325642030664, 
                    -29.61874601133713, 9.7491004845806728, -2.5004610982467619, 0.47423916532763594, -0.060190681556739713, 0.003952569169960474, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -0.003952569169960474, 0.060190681556739713, -0.47423916532763594, 2.5004610982467619, -9.7491004845806728, 29.61874601133713, -72.364325642030664, 145.1098502801552, -242.08923078259787, 339.02163558955255, -400.68109040555782, 400.6619329694712, -338.92492309563704, 241.84975046692531, 
                    -144.72865128406133, 71.931240313247315, -29.247301453742018, 9.501752173337696, -2.3711958266381794, 0.421334770897178, -0.043478260869565216, 0.0
                 }, { 
                    1.0, 1.0, -38.005645203413138, 259.79275748933196, -1120.2654539565226, 3574.5891920117174, -8970.6392222067134, 18272.800400749529, -30778.295817657196, 43362.962795474894, -51451.331605478706, 51588.32492723914, -43731.578484379192, 31270.556893822857, -18762.483818304736, 9363.4405778360215, 
                    -3834.506936699057, 1262.6274839510709, -323.92920008300388, 61.448265534892741, -7.8000090601014911, 0.51224386180626891, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 60.42652803867837, -506.98763713501847, 2396.5668122648422, -8038.5079781746517, 20784.579199115036, -43148.960817381863, 73601.381506186983, -104595.76736918927, 124861.15590598695, -125737.931812766, 106923.92858516847, -76632.494474643725, 46057.506985011889, -23013.579580819165, 
                    9433.0597360967149, -3108.1655047737745, 797.77867770719865, -151.38479256661876, 19.220261705367136, -1.2623931736378988, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -36.406043880899787, 446.58660757527213, -2516.548028305584, 9302.69457555069, -25514.96865158794, 55018.371752607862, -96265.1244365032, 139226.83213092014, -168275.21678130236, 170971.85098391864, -146333.09912575575, 105375.38545605566, -63553.805040750391, 31837.60979428361, 
                    -13074.531974436244, 4313.9048112235132, -1108.33483538012, 210.45634678463787, -26.731859941916266, 1.7562080920739935, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 26.503389194262109, -359.575801173153, 2343.7455709541218, -9594.8978594972141, 28182.799479939564, -63670.187606199528, 115064.33343383754, -170263.22565211687, 209191.1162390559, -215092.35239314227, 185711.55609490533, -134596.94933315733, 81565.760578940914, -41004.940799644435, 
                    16882.846884810559, -5580.9327709734807, 1435.7852238899675, -272.88680295529218, 34.682646213787265, -2.2793602596363933, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -20.824381295968294, 295.20334893199691, -2063.6246935535478, 9173.2791725219849, -28838.105559830226, 68572.0230542299, -128680.52991702227, 195748.15882006328, -245446.31815189656, 256196.31647103184, -223686.03552138619, 163473.24900553338, -99679.994962297249, 50342.210225447794, 
                    -20797.619264599805, 6892.0287048254477, -1776.2152919761338, 338.00100728328295, -42.992588326090846, 2.8268138394762596, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 17.05672510202119, -247.53207319123243, 1798.3976234242837, -8418.52099859969, 28007.1086153174, -70018.734593784029, 136821.22944461511, -214776.24667635062, 275847.9531371292, -293218.765697284, 259556.72657309147, -191665.35726103367, 117784.54259114151, -59833.015474027845, 
                    24825.356004938036, -8252.7423274173925, 2131.7043885746893, -406.28222437836087, 51.73055251239024, -3.4033845590075407, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -14.334641392736938, 210.98268695138839, -1569.304980648918, 7594.4074107515, -26321.792188105122, 68734.58566598194, -139782.7681569563, 226930.28265157965, -299459.26475162624, 325162.81099970394, -292615.73762232723, 218824.86423492257, -135773.41678671152, 69472.575856915471, 
                    -28981.006364587596, 9672.4894568793316, -2505.550602360212, 478.47725470206478, -61.001681638414162, 4.0163823619938439, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 12.251987527752219, -181.98931066322621, 1374.637226854991, -6802.7909550490594, 24277.16676212669, -65613.263782957409, 138335.99745107206, -232284.43998003122, 315601.01555976638, -351039.79295680736, 322051.06790961081, -244508.7632532203, 153494.21943729187, -79243.626644617878, 
                    33279.433294374867, -11162.215751839038, 2901.7796717026035, -555.51957626807132, 70.939107883563707, -4.6751262798131084, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -10.590430667044505, 158.30179135782788, -1208.4754094951847, 6074.9905491907339, -22143.795672786444, 61449.121773705534, -133538.62421993067, 231416.83532602777, -323951.36228965648, 369922.77168101305, -346930.2916746604, 268137.76670875243, -170717.09372520712, 89102.5205346559, 
                    -37731.421302149276, 12733.75563452168, -3325.1439447718085, 638.554523843929, -81.709475845438391, 5.3914016387344192, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 9.2212093685791245, -138.457353239932, 1065.0676631758774, -5415.3064225719563, 20051.074873719543, -56780.941495092695, 126464.41719325625, -225324.03429058264, 324652.00580144941, -381060.33708972309, 366226.48552667181, -288965.71187513776, 187101.4710162699, -98962.648760697179, 
                    42338.921527607556, -14399.232135420603, 3781.2205377383029, -728.99858746179211, 93.524278992637008, -6.18036087504257, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -8.0630593238668, 121.47106460930674, -939.68785329172215, 4818.4833220505, -18053.861306823255, 51933.078794038134, -117972.26250513534, 215199.60282720876, -318345.93812433636, 384032.258794736, -378897.176168859, 306064.63895901473, -202156.66685838896, 108670.70029490479, 
                    -47087.424751253129, 16169.916070144718, -4276.5146332260374, 828.62610805869087, -106.65731400834611, 7.0619378856891739, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 7.0619378856891739, -106.65731400834611, 828.62610805869087, -4276.5146332260374, 16169.916070144718, -47087.424751253129, 108670.70029490479, -202156.66685838896, 306064.63895901473, -378897.176168859, 384032.258794736, -318345.93812433636, 215199.60282720876, -117972.26250513534, 
                    51933.078794038134, -18053.861306823255, 4818.4833220505, -939.68785329172215, 121.47106460930674, -8.0630593238668, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -6.18036087504257, 93.524278992637008, -728.99858746179211, 3781.2205377383029, -14399.232135420603, 42338.921527607556, -98962.648760697179, 187101.4710162699, -288965.71187513776, 366226.48552667181, -381060.33708972309, 324652.00580144941, -225324.03429058264, 126464.41719325625, 
                    -56780.941495092695, 20051.074873719543, -5415.3064225719563, 1065.0676631758774, -138.457353239932, 9.2212093685791245, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 5.3914016387344192, -81.709475845438391, 638.554523843929, -3325.1439447718085, 12733.75563452168, -37731.421302149276, 89102.5205346559, -170717.09372520712, 268137.76670875243, -346930.2916746604, 369922.77168101305, -323951.36228965648, 231416.83532602777, -133538.62421993067, 
                    61449.121773705534, -22143.795672786444, 6074.9905491907339, -1208.4754094951847, 158.30179135782788, -10.590430667044505, 0.0, 0.0
                 }, 
                { 
                    0.0, 0.0, -4.6751262798131084, 70.939107883563707, -555.51957626807132, 2901.7796717026035, -11162.215751839038, 33279.433294374867, -79243.626644617878, 153494.21943729187, -244508.7632532203, 322051.06790961081, -351039.79295680736, 315601.01555976638, -232284.43998003122, 138335.99745107206, 
                    -65613.263782957409, 24277.16676212669, -6802.7909550490594, 1374.637226854991, -181.98931066322621, 12.251987527752219, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 4.0163823619938439, -61.001681638414162, 478.47725470206478, -2505.550602360212, 9672.4894568793316, -28981.006364587596, 69472.575856915471, -135773.41678671152, 218824.86423492257, -292615.73762232723, 325162.81099970394, -299459.26475162624, 226930.28265157965, -139782.7681569563, 
                    68734.58566598194, -26321.792188105122, 7594.4074107515, -1569.304980648918, 210.98268695138839, -14.334641392736938, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -3.4033845590075407, 51.73055251239024, -406.28222437836087, 2131.7043885746893, -8252.7423274173925, 24825.356004938036, -59833.015474027845, 117784.54259114151, -191665.35726103367, 259556.72657309147, -293218.765697284, 275847.9531371292, -214776.24667635062, 136821.22944461511, 
                    -70018.734593784029, 28007.1086153174, -8418.52099859969, 1798.3976234242837, -247.53207319123243, 17.05672510202119, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 2.8268138394762596, -42.992588326090846, 338.00100728328295, -1776.2152919761338, 6892.0287048254477, -20797.619264599805, 50342.210225447794, -99679.994962297249, 163473.24900553338, -223686.03552138619, 256196.31647103184, -245446.31815189656, 195748.15882006328, -128680.52991702227, 
                    68572.0230542299, -28838.105559830226, 9173.2791725219849, -2063.6246935535478, 295.20334893199691, -20.824381295968294, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -2.2793602596363933, 34.682646213787265, -272.88680295529218, 1435.7852238899675, -5580.9327709734807, 16882.846884810559, -41004.940799644435, 81565.760578940914, -134596.94933315733, 185711.55609490533, -215092.35239314227, 209191.1162390559, -170263.22565211687, 115064.33343383754, 
                    -63670.187606199528, 28182.799479939564, -9594.8978594972141, 2343.7455709541218, -359.575801173153, 26.503389194262109, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 1.7562080920739935, -26.731859941916266, 210.45634678463787, -1108.33483538012, 4313.9048112235132, -13074.531974436244, 31837.60979428361, -63553.805040750391, 105375.38545605566, -146333.09912575575, 170971.85098391864, -168275.21678130236, 139226.83213092014, -96265.1244365032, 
                    55018.371752607862, -25514.96865158794, 9302.69457555069, -2516.548028305584, 446.58660757527213, -36.406043880899787, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -1.2623931736378988, 19.220261705367136, -151.38479256661876, 797.77867770719865, -3108.1655047737745, 9433.0597360967149, -23013.579580819165, 46057.506985011889, -76632.494474643725, 106923.92858516847, -125737.931812766, 124861.15590598695, -104595.76736918927, 73601.381506186983, 
                    -43148.960817381863, 20784.579199115036, -8038.5079781746517, 2396.5668122648422, -506.98763713501847, 60.42652803867837, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 0.51224386180626891, -7.8000090601014911, 61.448265534892741, -323.92920008300388, 1262.6274839510709, -3834.506936699057, 9363.4405778360215, -18762.483818304736, 31270.556893822857, -43731.578484379192, 51588.32492723914, -51451.331605478706, 43362.962795474894, -30778.295817657196, 
                    18272.800400749529, -8970.6392222067134, 3574.5891920117174, -1120.2654539565226, 259.79275748933196, -38.005645203413138, 1.0, 1.0
                 }
             };
            private static readonly double[,] matrix_24 = new double[,] { 
                { 
                    0.0, 0.041666666666666664, -0.42275866827337011, 2.4967942461152455, -10.526013452261523, 34.182239417457289, -88.968108606382131, 190.10197801052834, -338.69304108590666, 508.33486863771111, -646.94803251720782, 700.76735803659949, -646.86217664916876, 508.31631126352039, -338.88991242514072, 190.51483561082247, 
                    -89.45975435789569, 34.598708902481938, -10.794391394986512, 2.6315033630653808, -0.4755798564029039, 0.057648909310005013, -0.0036231884057971015, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 0.0036231884057971015, -0.057648909310005013, 0.4755798564029039, -2.6315033630653808, 10.794391394986512, -34.598708902481938, 89.45975435789569, -190.51483561082247, 338.88991242514072, -508.31631126352039, 646.86217664916876, -700.76735803659949, 646.94803251720782, -508.33486863771111, 
                    338.69304108590666, -190.10197801052834, 88.968108606382131, -34.182239417457289, 10.526013452261523, -2.4967942461152455, 0.42275866827337011, -0.041666666666666664, 0.0
                 }, { 
                    1.0, 1.0, -41.164453648724539, 295.99092056391413, -1344.0852716728098, 4527.07269416307, -12027.323744064581, 26024.088260467717, -46744.102197738306, 70542.8350987357, -90121.664324661047, 97884.16034947, -90532.256673455151, 71244.375824793387, -47548.494874600459, 26751.571243039194, 
                    -12569.0410653519, 4863.1967262709259, -1517.7390109642784, 370.084662371559, -66.894255672186119, 8.109634763431437, -0.50971519001250054, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 65.33312793616129, -575.9967199620985, 2867.392226488364, -10154.59312519324, 27803.366620088178, -61327.987104937485, 111578.89459754463, -169881.54237675315, 218389.57607618594, -238267.09793094173, 221093.99227823797, -174410.35916054744, 116610.08811786072, -65694.256690602822, 
                    30896.596675230558, -11963.237113017147, 3735.5701451208756, -911.23009256141279, 164.75248350564581, -19.976564913977622, 1.2557181698047781, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -39.389774750683152, 505.53311990361709, -2997.5213061757881, 11699.195267039422, -33987.881469770458, 77895.959009584476, -145423.27586142739, 225406.73730872871, -293473.31079508277, 323136.80551745958, -301868.16744361207, 239314.64550998958, -160596.68886173217, 90724.187884441533, 
                    -42755.914556390671, 16580.327601079447, -5183.0286274296968, 1265.322011981807, -228.89995723981383, 27.764689644446396, -1.7456518346178671, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 28.702893316695089, -407.05981418723491, 2784.30048506407, -12019.670740708092, 37379.157397106923, -89752.130438527267, 173091.46455098843, -274562.730121209, 363488.2962804889, -405145.20730254817, 381908.97377920884, -304809.03642209509, 205577.00343732871, -116571.36925569577, 
                    55091.266519035613, -21408.349329236684, 6702.4893873359888, -1638.0597145822519, 296.55523582496352, -35.989064037219869, 2.2634139226616359, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -22.580431916986861, 334.47828389492457, -2450.7510554996215, 11465.121580316596, -38110.734486160261, 96250.661598751816, -192704.22256797861, 314243.09388094331, -424635.78324093943, 480585.09817592613, -458233.19234202959, 368881.79777577455, -250405.51706430331, 142684.0123977836, 
                    -67678.541521842446, 26371.249422280485, -8272.7538171145643, 2024.7310583367785, -366.92511756671018, 44.558251502756335, -2.8034338601605189, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 18.523694372428743, -280.84718880565674, 2137.3614917686145, -10516.40624340945, 36931.441866901136, -97938.375775206543, 204018.10851839709, -343180.05520461267, 474951.88549489941, -547443.15912831831, 529307.16899259994, -430643.33122162794, 294698.67810760974, -168952.49148701655, 
                    80508.384488036667, -31478.598193958784, 9900.0148631563188, -2427.4321674178746, 440.46429879830367, -53.533530809222896, 3.3697939660823821, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -15.597149837405402, 239.80934238086942, -1867.7724349033483, 9493.08876609156, -34686.702250746814, 95929.4224308318, -207698.83172356308, 360997.678317481, -513066.7681618983, 603968.259374546, -593662.89459014183, 489214.00912095682, -338090.15846648446, 195293.42769347774, 
                    -93593.229752071231, 36751.9689685889, -11595.182743923417, 2849.5999112882064, -517.89558689084242, 63.010833613835167, -3.9688363606504744, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 13.361970173638277, -207.32072563778564, 1639.3975965595498, -8516.4260488503623, 32011.117251815704, -91503.122945530675, 205071.236011527, -368150.57969182759, 538190.18082930183, -648583.20573227247, 649735.18648121611, -543554.53229319013, 380107.44141483813, -221581.81597735165, 
                    106938.08752544908, -42214.962991018321, 13371.183790290735, -3295.440355020919, 600.11891740983447, -73.110770844513709, 4.6085768368329765, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -11.582284959435629, 180.83470677418237, -1445.0037512489612, 7622.5513847445354, -29245.185447044783, 85743.272524699874, -197782.51726389898, 365850.53518990835, -550228.326404545, 679992.74930148886, -695868.76426153781, 592407.39566421008, -420110.74045030796, 247615.30508603918, 
                    -120526.57479225622, 47889.542122165447, -15242.40105179943, 3769.921520974649, -688.22878673260539, 83.982375229393739, -5.2990033990356515, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 10.119022914315984, -158.69789739939174, 1277.6875404593002, -6815.3817137966453, 26549.002806773638, -79368.2058819023, 187405.95774836876, -355843.9463452929, 549866.040168129, -697365.77073183039, 730406.7249653386, -634277.1374245265, 457239.12345805945, -273076.49411055993, 
                    134305.58070743515, -53792.327548642359, 17224.360171497156, -4278.8927158044917, 783.56511398959663, -95.811857458462072, 6.0527281451541555, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -8.884446326435576, 139.79818482282045, -1131.8165458278484, 6087.6555578998041, -23988.665434604194, 72803.387651272438, -175161.47118698843, 340039.6960554804, -538510.64847384719, 700534.49095087964, -751858.982699283, 667455.52322141628, -490359.43027697818, 297485.14424201933, 
                    -148163.32265544593, 59928.826806530618, -19333.139629122357, 4829.2436953492152, -887.78903347968355, 108.83603917709036, -6.88599969651575, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 7.8202420268886073, -123.3622482157177, 1002.987713041976, -5429.0551932124636, 21584.033412658187, -66283.6058005464, 161896.46780294014, -320137.94313715596, 518030.21801067848, -690119.47343901265, 759145.42302691611, -690119.47343901265, 518030.21801067848, -320137.94313715596, 
                    161896.46780294014, -66283.6058005464, 21584.033412658187, -5429.0551932124636, 1002.987713041976, -123.3622482157177, 7.8202420268886073, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -6.88599969651575, 108.83603917709036, -887.78903347968355, 4829.2436953492152, -19333.139629122357, 59928.826806530618, -148163.32265544593, 297485.14424201933, -490359.43027697818, 667455.52322141628, -751858.982699283, 700534.49095087964, -538510.64847384719, 340039.6960554804, 
                    -175161.47118698843, 72803.387651272438, -23988.665434604194, 6087.6555578998041, -1131.8165458278484, 139.79818482282045, -8.884446326435576, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 6.0527281451541555, -95.811857458462072, 783.56511398959663, -4278.8927158044917, 17224.360171497156, -53792.327548642359, 134305.58070743515, -273076.49411055993, 457239.12345805945, -634277.1374245265, 730406.7249653386, -697365.77073183039, 549866.040168129, -355843.9463452929, 
                    187405.95774836876, -79368.2058819023, 26549.002806773638, -6815.3817137966453, 1277.6875404593002, -158.69789739939174, 10.119022914315984, 0.0, 0.0
                 }, 
                { 
                    0.0, 0.0, -5.2990033990356515, 83.982375229393739, -688.22878673260539, 3769.921520974649, -15242.40105179943, 47889.542122165447, -120526.57479225622, 247615.30508603918, -420110.74045030796, 592407.39566421008, -695868.76426153781, 679992.74930148886, -550228.326404545, 365850.53518990835, 
                    -197782.51726389898, 85743.272524699874, -29245.185447044783, 7622.5513847445354, -1445.0037512489612, 180.83470677418237, -11.582284959435629, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 4.6085768368329765, -73.110770844513709, 600.11891740983447, -3295.440355020919, 13371.183790290735, -42214.962991018321, 106938.08752544908, -221581.81597735165, 380107.44141483813, -543554.53229319013, 649735.18648121611, -648583.20573227247, 538190.18082930183, -368150.57969182759, 
                    205071.236011527, -91503.122945530675, 32011.117251815704, -8516.4260488503623, 1639.3975965595498, -207.32072563778564, 13.361970173638277, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -3.9688363606504744, 63.010833613835167, -517.89558689084242, 2849.5999112882064, -11595.182743923417, 36751.9689685889, -93593.229752071231, 195293.42769347774, -338090.15846648446, 489214.00912095682, -593662.89459014183, 603968.259374546, -513066.7681618983, 360997.678317481, 
                    -207698.83172356308, 95929.4224308318, -34686.702250746814, 9493.08876609156, -1867.7724349033483, 239.80934238086942, -15.597149837405402, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 3.3697939660823821, -53.533530809222896, 440.46429879830367, -2427.4321674178746, 9900.0148631563188, -31478.598193958784, 80508.384488036667, -168952.49148701655, 294698.67810760974, -430643.33122162794, 529307.16899259994, -547443.15912831831, 474951.88549489941, -343180.05520461267, 
                    204018.10851839709, -97938.375775206543, 36931.441866901136, -10516.40624340945, 2137.3614917686145, -280.84718880565674, 18.523694372428743, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -2.8034338601605189, 44.558251502756335, -366.92511756671018, 2024.7310583367785, -8272.7538171145643, 26371.249422280485, -67678.541521842446, 142684.0123977836, -250405.51706430331, 368881.79777577455, -458233.19234202959, 480585.09817592613, -424635.78324093943, 314243.09388094331, 
                    -192704.22256797861, 96250.661598751816, -38110.734486160261, 11465.121580316596, -2450.7510554996215, 334.47828389492457, -22.580431916986861, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 2.2634139226616359, -35.989064037219869, 296.55523582496352, -1638.0597145822519, 6702.4893873359888, -21408.349329236684, 55091.266519035613, -116571.36925569577, 205577.00343732871, -304809.03642209509, 381908.97377920884, -405145.20730254817, 363488.2962804889, -274562.730121209, 
                    173091.46455098843, -89752.130438527267, 37379.157397106923, -12019.670740708092, 2784.30048506407, -407.05981418723491, 28.702893316695089, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -1.7456518346178671, 27.764689644446396, -228.89995723981383, 1265.322011981807, -5183.0286274296968, 16580.327601079447, -42755.914556390671, 90724.187884441533, -160596.68886173217, 239314.64550998958, -301868.16744361207, 323136.80551745958, -293473.31079508277, 225406.73730872871, 
                    -145423.27586142739, 77895.959009584476, -33987.881469770458, 11699.195267039422, -2997.5213061757881, 505.53311990361709, -39.389774750683152, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 1.2557181698047781, -19.976564913977622, 164.75248350564581, -911.23009256141279, 3735.5701451208756, -11963.237113017147, 30896.596675230558, -65694.256690602822, 116610.08811786072, -174410.35916054744, 221093.99227823797, -238267.09793094173, 218389.57607618594, -169881.54237675315, 
                    111578.89459754463, -61327.987104937485, 27803.366620088178, -10154.59312519324, 2867.392226488364, -575.9967199620985, 65.33312793616129, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -0.50971519001250054, 8.109634763431437, -66.894255672186119, 370.084662371559, -1517.7390109642784, 4863.1967262709259, -12569.0410653519, 26751.571243039194, -47548.494874600459, 71244.375824793387, -90532.256673455151, 97884.16034947, -90121.664324661047, 70542.8350987357, 
                    -46744.102197738306, 26024.088260467717, -12027.323744064581, 4527.07269416307, -1344.0852716728098, 295.99092056391413, -41.164453648724539, 1.0, 1.0
                 }
             };
            private static readonly double[,] matrix_25 = new double[,] { 
                { 
                    0.0, 0.04, -0.42405833444572916, 2.6223403123639919, -11.602053461369641, 39.639267408961196, -108.84793123997426, 246.13810795318233, -465.71133855911091, 745.23118290222681, -1015.7752687582459, 1184.5347091141862, -1184.3215622045691, 1015.3154649550166, -744.901863756047, 465.76948931389177, 
                    -246.55306159011755, 109.39471464585881, -40.101869404201047, 11.891780222688359, -2.7623936812784859, 0.476789147702544, -0.055311956666834236, 0.0033333333333333335, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -0.0033333333333333335, 0.055311956666834236, -0.476789147702544, 2.7623936812784859, -11.891780222688359, 40.101869404201047, -109.39471464585881, 246.55306159011755, -465.76948931389177, 744.901863756047, -1015.3154649550166, 1184.3215622045691, -1184.5347091141862, 1015.7752687582459, 
                    -745.23118290222681, 465.71133855911091, -246.13810795318233, 108.84793123997426, -39.639267408961196, 11.602053461369641, -2.6223403123639919, 0.42405833444572916, -0.04, 0.0
                 }, { 
                    1.0, 1.0, -44.444490603551721, 335.35479333485705, -1599.550653510219, 5670.9637625853375, -15900.47085968674, 36418.677017746435, -69481.756931500538, 111811.98740210413, -153003.49770785403, 178925.20399065141, -179259.15232514928, 153910.29481382915, -113045.53540271029, 70744.17173157951, 
                    -37471.775885942938, 16633.966960044549, -6099.8251372294117, 1809.3083419111917, -420.371139876227, 72.5655955301315, -8.4190129997837815, 0.50739166777906253, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 70.427954895791544, -650.91328342936822, 3403.6764775627325, -12690.667309331549, 36679.586799446544, -85662.510646214432, 165575.7107047711, -268861.74527501839, 370270.19507315371, -435007.76646829158, 437301.42905573139, -376410.28183488618, 276991.18435656768, -173587.589998355, 
                    92044.037520678481, -40891.718951672628, 15004.337996905249, -4452.4985793368623, 1034.8151070349629, -178.67248331234268, 20.732529849787621, -1.2496045501243476, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -42.487796679226761, 569.39288435058836, -3543.6225052026771, 14561.221571535056, -44665.311885375348, 108416.85035101707, -215096.45857212294, 355683.37499231263, -496236.57043364923, 588520.44295277284, -595747.2783581519, 515452.16303195205, -380782.35060637479, 239330.26649247424, 
                    -127183.44186581241, 56596.455067371433, -20792.55452786863, 6175.7723287223153, -1436.2732203204525, 248.103416777839, -28.797860826755631, 1.7360389343847606, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 30.986465245472154, -458.49002107029719, 3283.6466855797835, -14907.145881675846, 48928.221806083209, -124420.3479866353, 255033.57662537729, -431671.92132644868, 612539.13719975634, -735563.83107029367, 751533.4226681086, -654779.08856617787, 486251.06773222494, -306834.02505570935, 
                    163545.60014502436, -72942.285015793372, 26843.111105138858, -7982.8761281791258, 1858.2255185189977, -321.19555771549653, 37.297466753061521, -2.2489867748325323, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -24.403396071662687, 377.02278815379714, -2889.3652107381045, 14189.893665291809, -49724.030160570946, 132915.65669026479, -282773.06566991284, 492039.78448070411, -712747.51121166116, 869240.5472875986, -898534.58007436839, 789805.35175244161, -590476.570868525, 374510.61316089245, 
                    -200393.93504956641, 89639.526682411029, -33060.485111613627, 9847.9008336127918, -2295.0696661515731, 397.03307943434436, -46.129071565836043, 2.7824274281111752, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 20.046328959730694, -316.94632488883792, 2521.4753353665528, -13009.328499769055, 48090.591557383341, -134824.11713269213, 298226.03952501807, -535091.84962299839, 793760.97900065011, -985936.556324864, 1033615.9184665845, -918422.418903273, 692359.58524483093, -441934.05725904863, 
                    237623.640742701, -106686.28788186428, 39457.124413920559, -11777.567510516064, 2748.895022572769, -476.04202145097435, 55.3472027457015, -3.3398312965714543, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -16.907297582577467, 271.05740670899621, -2206.1674446852476, 11749.7067083716, -45140.229869346309, 131798.24365685711, -302657.10782493907, 560664.26994300517, -853696.70896473457, 1082739.3184545985, -1153936.446049368, 1038626.2782060029, -790864.22939745011, 508742.51949928969, 
                    -275184.61048964266, 124114.97814059279, -46061.491912029931, 13784.218322875871, -3223.2582615921719, 558.92249847684207, -65.039846782569228, 3.9267460599301964, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 14.513527241807189, -234.79317450739043, 1939.8008792946498, -10554.441023043079, 41677.884485687682, -125628.15069932702, 298215.46184342942, -569922.03224255552, 891815.83524617751, -1157294.3006866369, 1256676.8706036808, -1148195.375895814, 884747.88710154663, -574465.35317376419, 
                    312993.72854402033, -141956.45688890928, 52906.197623840642, -15882.655534577432, 3722.5197324564469, -646.5436149686933, 75.317031931221536, -4.5501372892090366, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -12.610929810793779, 205.28557302301948, -1713.6295639666009, 9464.9375651026767, -38128.181102276983, 117770.77628913811, -287382.43426494033, 565116.70964527375, -908616.38734414056, 1207980.9620894715, -1339099.0012983582, 1244622.8388060282, -972453.695598401, 638442.60856476682, 
                    -350894.68902333366, 160225.78508129777, -60024.470499672272, 18089.601047140055, -4251.8210475980613, 739.95368729117706, -86.313180387256949, 5.2185736848390532, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 11.049720667064712, -180.67464321841874, 1519.4357108947383, -8484.4365958100334, 34687.529288290971, -109172.2337683396, 272415.57497388381, -549143.6093226166, 905821.77690477064, -1234161.3032640079, 1398740.7603343846, -1325142.6744881836, 1052039.4379966494, -699751.47537057381, 
                    388617.91603590053, -178908.32765353957, 67447.308615658781, -20423.575137954063, 4817.21071315596, -840.42345081861652, 98.1939273073683, -5.9427058692229577, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -9.73543139635948, 159.71045251601018, -1350.555929399844, 7603.1820024536555, -31434.860409402107, 100384.50598219734, -255019.6773912356, 524961.991466601, -886141.2318584749, 1236384.682533486, -1433710.5640367561, 1386848.2460982376, -1121135.5559336701, 757125.26937269443, 
                    -425728.81262325658, 197940.27807654123, -75199.252611002463, 22904.68784215127, -5425.8295113692129, 949.51175412889893, -111.16661803000241, 6.7360079399662, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 8.6053043005680472, -141.52461273662877, 1201.7999952471264, -6808.0540518984863, 28391.957312584389, -91707.494413848617, 236358.32114961874, -495111.8469108686, 852779.68192339432, -1216414.54494378, 1443032.6804415178, -1426928.3924480129, 1176956.9208246069, -808867.64579163434, 
                    461558.22871973523, -217179.09778392687, 83291.144703788479, -25554.002788667665, 6086.1248393564792, -1069.1566798159995, 125.4955937687363, -7.6158791013125482, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -7.6158791013125482, 125.4955937687363, -1069.1566798159995, 6086.1248393564792, -25554.002788667665, 83291.144703788479, -217179.09778392687, 461558.22871973523, -808867.64579163434, 1176956.9208246069, -1426928.3924480129, 1443032.6804415178, -1216414.54494378, 852779.68192339432, 
                    -495111.8469108686, 236358.32114961874, -91707.494413848617, 28391.957312584389, -6808.0540518984863, 1201.7999952471264, -141.52461273662877, 8.6053043005680472, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 6.7360079399662, -111.16661803000241, 949.51175412889893, -5425.8295113692129, 22904.68784215127, -75199.252611002463, 197940.27807654123, -425728.81262325658, 757125.26937269443, -1121135.5559336701, 1386848.2460982376, -1433710.5640367561, 1236384.682533486, -886141.2318584749, 
                    524961.991466601, -255019.6773912356, 100384.50598219734, -31434.860409402107, 7603.1820024536555, -1350.555929399844, 159.71045251601018, -9.73543139635948, 0.0, 0.0
                 }, 
                { 
                    0.0, 0.0, -5.9427058692229577, 98.1939273073683, -840.42345081861652, 4817.21071315596, -20423.575137954063, 67447.308615658781, -178908.32765353957, 388617.91603590053, -699751.47537057381, 1052039.4379966494, -1325142.6744881836, 1398740.7603343846, -1234161.3032640079, 905821.77690477064, 
                    -549143.6093226166, 272415.57497388381, -109172.2337683396, 34687.529288290971, -8484.4365958100334, 1519.4357108947383, -180.67464321841874, 11.049720667064712, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 5.2185736848390532, -86.313180387256949, 739.95368729117706, -4251.8210475980613, 18089.601047140055, -60024.470499672272, 160225.78508129777, -350894.68902333366, 638442.60856476682, -972453.695598401, 1244622.8388060282, -1339099.0012983582, 1207980.9620894715, -908616.38734414056, 
                    565116.70964527375, -287382.43426494033, 117770.77628913811, -38128.181102276983, 9464.9375651026767, -1713.6295639666009, 205.28557302301948, -12.610929810793779, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -4.5501372892090366, 75.317031931221536, -646.5436149686933, 3722.5197324564469, -15882.655534577432, 52906.197623840642, -141956.45688890928, 312993.72854402033, -574465.35317376419, 884747.88710154663, -1148195.375895814, 1256676.8706036808, -1157294.3006866369, 891815.83524617751, 
                    -569922.03224255552, 298215.46184342942, -125628.15069932702, 41677.884485687682, -10554.441023043079, 1939.8008792946498, -234.79317450739043, 14.513527241807189, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 3.9267460599301964, -65.039846782569228, 558.92249847684207, -3223.2582615921719, 13784.218322875871, -46061.491912029931, 124114.97814059279, -275184.61048964266, 508742.51949928969, -790864.22939745011, 1038626.2782060029, -1153936.446049368, 1082739.3184545985, -853696.70896473457, 
                    560664.26994300517, -302657.10782493907, 131798.24365685711, -45140.229869346309, 11749.7067083716, -2206.1674446852476, 271.05740670899621, -16.907297582577467, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -3.3398312965714543, 55.3472027457015, -476.04202145097435, 2748.895022572769, -11777.567510516064, 39457.124413920559, -106686.28788186428, 237623.640742701, -441934.05725904863, 692359.58524483093, -918422.418903273, 1033615.9184665845, -985936.556324864, 793760.97900065011, 
                    -535091.84962299839, 298226.03952501807, -134824.11713269213, 48090.591557383341, -13009.328499769055, 2521.4753353665528, -316.94632488883792, 20.046328959730694, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 2.7824274281111752, -46.129071565836043, 397.03307943434436, -2295.0696661515731, 9847.9008336127918, -33060.485111613627, 89639.526682411029, -200393.93504956641, 374510.61316089245, -590476.570868525, 789805.35175244161, -898534.58007436839, 869240.5472875986, -712747.51121166116, 
                    492039.78448070411, -282773.06566991284, 132915.65669026479, -49724.030160570946, 14189.893665291809, -2889.3652107381045, 377.02278815379714, -24.403396071662687, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -2.2489867748325323, 37.297466753061521, -321.19555771549653, 1858.2255185189977, -7982.8761281791258, 26843.111105138858, -72942.285015793372, 163545.60014502436, -306834.02505570935, 486251.06773222494, -654779.08856617787, 751533.4226681086, -735563.83107029367, 612539.13719975634, 
                    -431671.92132644868, 255033.57662537729, -124420.3479866353, 48928.221806083209, -14907.145881675846, 3283.6466855797835, -458.49002107029719, 30.986465245472154, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 1.7360389343847606, -28.797860826755631, 248.103416777839, -1436.2732203204525, 6175.7723287223153, -20792.55452786863, 56596.455067371433, -127183.44186581241, 239330.26649247424, -380782.35060637479, 515452.16303195205, -595747.2783581519, 588520.44295277284, -496236.57043364923, 
                    355683.37499231263, -215096.45857212294, 108416.85035101707, -44665.311885375348, 14561.221571535056, -3543.6225052026771, 569.39288435058836, -42.487796679226761, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -1.2496045501243476, 20.732529849787621, -178.67248331234268, 1034.8151070349629, -4452.4985793368623, 15004.337996905249, -40891.718951672628, 92044.037520678481, -173587.589998355, 276991.18435656768, -376410.28183488618, 437301.42905573139, -435007.76646829158, 370270.19507315371, 
                    -268861.74527501839, 165575.7107047711, -85662.510646214432, 36679.586799446544, -12690.667309331549, 3403.6764775627325, -650.91328342936822, 70.427954895791544, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 0.50739166777906253, -8.4190129997837815, 72.5655955301315, -420.371139876227, 1809.3083419111917, -6099.8251372294117, 16633.966960044549, -37471.775885942938, 70744.17173157951, -113045.53540271029, 153910.29481382915, -179259.15232514928, 178925.20399065141, -153003.49770785403, 
                    111811.98740210413, -69481.756931500538, 36418.677017746435, -15900.47085968674, 5670.9637625853375, -1599.550653510219, 335.35479333485705, -44.444490603551721, 1.0, 1.0
                 }
             };
            private static readonly double[,] matrix_26 = new double[,] { 
                { 
                    0.0, 0.038461538461538464, -0.42524928889483443, 2.747839481227405, -12.729861271530371, 45.644406967446237, -131.86878805004486, 314.60482880072152, -629.95937972234856, 1070.5502185525056, -1555.7706898012, 1943.0487635507179, -2091.4699876398831, 1942.0792742370345, -1554.4390108405744, 1069.5923492383249, 
                    -629.73542267794448, 314.97968986117428, -132.46519107398802, 46.1540758175157, -13.041259133556068, 2.8931502889841751, -0.47788512716998349, 0.053156161111854304, -0.0030769230769230769, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 0.0030769230769230769, -0.053156161111854304, 0.47788512716998349, -2.8931502889841751, 13.041259133556068, -46.1540758175157, 132.46519107398802, -314.97968986117428, 629.73542267794448, -1069.5923492383249, 1554.4390108405744, -1942.0792742370345, 2091.4699876398831, -1943.0487635507179, 
                    1555.7706898012, -1070.5502185525056, 629.95937972234856, -314.60482880072152, 131.86878805004486, -45.644406967446237, 12.729861271530371, -2.747839481227405, 0.42524928889483443, -0.038461538461538464, 0.0
                 }, { 
                    1.0, 1.0, -47.845776915754229, 378.01507945207192, -1889.4844497736858, 7033.54559458165, -20754.777762224727, 50163.852934354269, -101302.02137003296, 173145.97923854346, -252640.6585322989, 316445.96448884608, -341340.49031234038, 317457.69833991543, -254392.56769932609, 175200.55840141512, 
                    -103221.06454196725, 51655.328308031989, -21732.148372790456, 7574.2126048826321, -2140.6266292765208, 474.96408075074532, -78.4623696699582, 8.7281769803120675, -0.50524928889483445, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 75.71104700449483, -731.97701437684714, 4011.1633881844646, -15705.84563400315, 47784.359840377663, -117787.55804412669, 241026.83970322771, -415761.90887960605, 610624.06884851749, -768479.90688344813, 831845.41630468215, -775670.50422926375, 622805.22967667307, -429568.5802335277, 
                    253372.24140729127, -126905.75817903133, 53426.114123745472, -18629.566668574767, 5267.0264505449868, -1168.9645611401165, 193.14510135562929, -21.488208170043578, 1.243984207418954, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -45.700145612957748, 638.36056257459188, -4160.4252863157462, 17952.921994827997, -57980.264792581991, 148583.74023968246, -312169.14759546932, 548509.64827934676, -816314.50318211573, 1037312.8427699036, -1130905.617103966, 1060200.51850075, -854713.33261630021, 591337.48188792751, 
                    -349605.55661223648, 175418.16376636532, -73949.31351965385, 25812.241312028673, -7303.271496747112, 1621.7836207741716, -268.06743429364036, 29.831351390003569, -1.7272481676435325, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 33.354145935998815, -514.0219263136205, 3846.7486566161656, -18320.036153301204, 63284.677908662714, -169892.25778971842, 368816.54841020174, -663456.7456268986, 1004468.6501940124, -1292712.1884243321, 1422799.9585838022, -1343443.7582967714, 1088974.3525134372, -756549.28536600887, 
                    448703.21908606513, -225687.89570349772, 95316.576030186217, -33316.850332940674, 9436.3763817625368, -2097.0482842668862, 346.80914630737908, -38.607635705101885, 2.2358719847724542, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -26.293323476674693, 422.965429532103, -3383.7950925284636, 17405.726239860112, -64124.828052531448, 180857.65864992409, -407411.5205383682, 753413.67454506189, -1164545.5632931951, 1522341.5622040059, -1695540.3718655794, 1615533.6158931123, -1318638.2762831091, 920993.81645557343, 
                    -548470.53167941165, 276733.71671978314, -117154.4873955795, 41024.003820120866, -11634.92406043106, 2588.1711172356167, -428.32726137993706, 47.704421087501331, -2.763452802056904, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 21.624690791931933, -355.93886219451213, 2954.5308260994511, -15950.130614708616, 61908.062842114945, -182938.91775802933, 428188.41124112596, -816221.11589252821, 1291827.8399596286, -1719991.5660590054, 1943071.2866382562, -1871843.8231245303, 1540899.752668665, -1083339.8587113754, 
                    648440.43690460059, -328457.44088335935, 139468.72359254945, -48948.771385198, 13906.045688717089, -3097.2129604738557, 513.01909806850915, -57.170174644760223, 3.3129450548884836, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -18.26516258109158, 304.82200672992076, -2587.8464517059633, 14412.1187402694, -58076.901970104096, 178516.764643613, -433337.01201847586, 852227.22748722939, -1383900.348826705, 1881047.9058658688, -2160199.1600236814, 2108150.0972481123, -1753172.1456033161, 1242424.9458356714, 
                    -748283.19756963733, 380853.92240786273, -162314.50617940459, 57126.780699226336, -16263.465795347043, 3627.835324750934, -601.56335437761925, 67.0859651400714, -3.8892362866395627, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 15.706757186407552, -264.49082205953516, 2278.85004600544, -12960.278873756713, 53642.468898148421, -170048.10638012292, 426198.35348043061, -863817.27584417362, 1440425.9587053114, -2002231.6660400352, 2342135.8314663367, -2320039.1382998894, 1952528.7801577772, -1396847.4859239233, 
                    847568.2088926367, -433897.63454849529, 185752.21530447106, -65599.961754311065, 18723.787178858431, -4184.5341766681031, 694.800919922231, -77.55278274740715, 4.4984337417877862, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -13.67648953685207, 231.7298931760904, -2017.0656806127981, 11641.760484162645, -49129.871781021771, 159465.58030630165, -410410.72501866956, 854875.81340515008, -1463126.6476227259, 2081836.247556817, -2484647.8990199817, 2502853.3563881149, -2135531.8187653036, 1544803.0591261087, 
                    -945663.01419339865, 487497.6187586434, -209832.49667206951, 74412.973135378066, -21305.918545059347, 4772.59620305583, -793.73841967583655, 88.6927847166924, -5.1479669954902558, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 12.0134592616209, -204.4561059650639, 1792.7837759925694, -10458.770039944806, 44778.191362761485, -148001.45783923825, 389154.8908177276, -830008.81658694579, 1455556.3888201995, -2120004.5005168654, 2584407.1550960885, -2651826.453693423, 2298167.1572182663, -1683960.0006623133, 
                    1041643.3276490571, -541456.34771007241, 234583.77896556503, -83611.018964442672, 24031.078600672881, -5398.2217788658245, 899.58462769403957, -100.65463395578213, 5.8469434400037885, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -10.616211269637407, 181.27065791567787, -1598.1744919175778, 9398.4905930249, -40680.906362503854, 136364.62131317111, -364778.48445926467, 793685.9933041028, -1422543.3268414843, 2118849.6643226021, -2639433.8037818745, 2762375.9836646332, -2435864.7245953316, 1811346.7734283791, 
                    -1134188.7723653407, 595417.05978064134, -259995.475541259, 93236.885211531786, -26922.850446575187, 6068.7194758095238, -1013.8072348058004, 113.62181185419668, -6.6067020695345366, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 9.41737089028105, -161.20245718508528, 1427.158428112391, -8444.4373420295833, 36861.404623144517, -124934.43024480094, 338858.67792879796, -749628.37549589458, 1369364.7856796428, -2082253.0142097133, 2649514.0815429222, -2830553.6532417824, 2543639.5101555893, -1923264.1785749353, 
                    1221458.1458295698, -648791.02882756, 285992.43390144949, -103325.81629023913, 30007.024816852783, -6792.7490511909409, 1138.2115898337045, -127.82456003128343, 7.441621352319272, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -8.3703058486984752, 143.55704279980927, -1275.0480771349476, 7580.59600124627, -33310.8968982143, 113896.39858152796, -312395.22481844091, 700657.19817730074, -1300949.0867160158, 2015262.4092623645, -2616407.3043523859, 2853613.1217020447, -2616407.3043523859, 2015262.4092623645, 
                    -1300949.0867160158, 700657.19817730074, -312395.22481844091, 113896.39858152796, -33310.8968982143, 7580.59600124627, -1275.0480771349476, 143.55704279980927, -8.3703058486984752, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 7.441621352319272, -127.82456003128343, 1138.2115898337045, -6792.7490511909409, 30007.024816852783, -103325.81629023913, 285992.43390144949, -648791.02882756, 1221458.1458295698, -1923264.1785749353, 2543639.5101555893, -2830553.6532417824, 2649514.0815429222, -2082253.0142097133, 
                    1369364.7856796428, -749628.37549589458, 338858.67792879796, -124934.43024480094, 36861.404623144517, -8444.4373420295833, 1427.158428112391, -161.20245718508528, 9.41737089028105, 0.0, 0.0
                 }, 
                { 
                    0.0, 0.0, -6.6067020695345366, 113.62181185419668, -1013.8072348058004, 6068.7194758095238, -26922.850446575187, 93236.885211531786, -259995.475541259, 595417.05978064134, -1134188.7723653407, 1811346.7734283791, -2435864.7245953316, 2762375.9836646332, -2639433.8037818745, 2118849.6643226021, 
                    -1422543.3268414843, 793685.9933041028, -364778.48445926467, 136364.62131317111, -40680.906362503854, 9398.4905930249, -1598.1744919175778, 181.27065791567787, -10.616211269637407, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 5.8469434400037885, -100.65463395578213, 899.58462769403957, -5398.2217788658245, 24031.078600672881, -83611.018964442672, 234583.77896556503, -541456.34771007241, 1041643.3276490571, -1683960.0006623133, 2298167.1572182663, -2651826.453693423, 2584407.1550960885, -2120004.5005168654, 
                    1455556.3888201995, -830008.81658694579, 389154.8908177276, -148001.45783923825, 44778.191362761485, -10458.770039944806, 1792.7837759925694, -204.4561059650639, 12.0134592616209, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -5.1479669954902558, 88.6927847166924, -793.73841967583655, 4772.59620305583, -21305.918545059347, 74412.973135378066, -209832.49667206951, 487497.6187586434, -945663.01419339865, 1544803.0591261087, -2135531.8187653036, 2502853.3563881149, -2484647.8990199817, 2081836.247556817, 
                    -1463126.6476227259, 854875.81340515008, -410410.72501866956, 159465.58030630165, -49129.871781021771, 11641.760484162645, -2017.0656806127981, 231.7298931760904, -13.67648953685207, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 4.4984337417877862, -77.55278274740715, 694.800919922231, -4184.5341766681031, 18723.787178858431, -65599.961754311065, 185752.21530447106, -433897.63454849529, 847568.2088926367, -1396847.4859239233, 1952528.7801577772, -2320039.1382998894, 2342135.8314663367, -2002231.6660400352, 
                    1440425.9587053114, -863817.27584417362, 426198.35348043061, -170048.10638012292, 53642.468898148421, -12960.278873756713, 2278.85004600544, -264.49082205953516, 15.706757186407552, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -3.8892362866395627, 67.0859651400714, -601.56335437761925, 3627.835324750934, -16263.465795347043, 57126.780699226336, -162314.50617940459, 380853.92240786273, -748283.19756963733, 1242424.9458356714, -1753172.1456033161, 2108150.0972481123, -2160199.1600236814, 1881047.9058658688, 
                    -1383900.348826705, 852227.22748722939, -433337.01201847586, 178516.764643613, -58076.901970104096, 14412.1187402694, -2587.8464517059633, 304.82200672992076, -18.26516258109158, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 3.3129450548884836, -57.170174644760223, 513.01909806850915, -3097.2129604738557, 13906.045688717089, -48948.771385198, 139468.72359254945, -328457.44088335935, 648440.43690460059, -1083339.8587113754, 1540899.752668665, -1871843.8231245303, 1943071.2866382562, -1719991.5660590054, 
                    1291827.8399596286, -816221.11589252821, 428188.41124112596, -182938.91775802933, 61908.062842114945, -15950.130614708616, 2954.5308260994511, -355.93886219451213, 21.624690791931933, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -2.763452802056904, 47.704421087501331, -428.32726137993706, 2588.1711172356167, -11634.92406043106, 41024.003820120866, -117154.4873955795, 276733.71671978314, -548470.53167941165, 920993.81645557343, -1318638.2762831091, 1615533.6158931123, -1695540.3718655794, 1522341.5622040059, 
                    -1164545.5632931951, 753413.67454506189, -407411.5205383682, 180857.65864992409, -64124.828052531448, 17405.726239860112, -3383.7950925284636, 422.965429532103, -26.293323476674693, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 2.2358719847724542, -38.607635705101885, 346.80914630737908, -2097.0482842668862, 9436.3763817625368, -33316.850332940674, 95316.576030186217, -225687.89570349772, 448703.21908606513, -756549.28536600887, 1088974.3525134372, -1343443.7582967714, 1422799.9585838022, -1292712.1884243321, 
                    1004468.6501940124, -663456.7456268986, 368816.54841020174, -169892.25778971842, 63284.677908662714, -18320.036153301204, 3846.7486566161656, -514.0219263136205, 33.354145935998815, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -1.7272481676435325, 29.831351390003569, -268.06743429364036, 1621.7836207741716, -7303.271496747112, 25812.241312028673, -73949.31351965385, 175418.16376636532, -349605.55661223648, 591337.48188792751, -854713.33261630021, 1060200.51850075, -1130905.617103966, 1037312.8427699036, 
                    -816314.50318211573, 548509.64827934676, -312169.14759546932, 148583.74023968246, -57980.264792581991, 17952.921994827997, -4160.4252863157462, 638.36056257459188, -45.700145612957748, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 1.243984207418954, -21.488208170043578, 193.14510135562929, -1168.9645611401165, 5267.0264505449868, -18629.566668574767, 53426.114123745472, -126905.75817903133, 253372.24140729127, -429568.5802335277, 622805.22967667307, -775670.50422926375, 831845.41630468215, -768479.90688344813, 
                    610624.06884851749, -415761.90887960605, 241026.83970322771, -117787.55804412669, 47784.359840377663, -15705.84563400315, 4011.1633881844646, -731.97701437684714, 75.71104700449483, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -0.50524928889483445, 8.7281769803120675, -78.4623696699582, 474.96408075074532, -2140.6266292765208, 7574.2126048826321, -21732.148372790456, 51655.328308031989, -103221.06454196725, 175200.55840141512, -254392.56769932609, 317457.69833991543, -341340.49031234038, 316445.96448884608, 
                    -252640.6585322989, 173145.97923854346, -101302.02137003296, 50163.852934354269, -20754.777762224727, 7033.54559458165, -1889.4844497736858, 378.01507945207192, -47.845776915754229, 1.0, 1.0
                 }
             };
            private static readonly double[,] matrix_27 = new double[,] { 
                { 
                    0.0, 0.037037037037037035, -0.42634457726733316, 2.8732964960459046, -13.909427668617829, 52.223679134123323, -158.34352922698702, 397.45435286692629, -839.65665093227449, 1510.1055726857576, -2330.5984464086459, 3103.4043182660871, -3577.6244600587988, 3576.4842798171085, -3100.6078653842924, 2327.5532386995651, 
                    -1508.0342888526534, 838.94754038097642, -397.73209781002475, 158.98174114677053, -52.78117640899567, 14.242821582033635, -3.0237886236125715, 0.47888274934098413, -0.051161349272079978, 0.0028490028490028491, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -0.0028490028490028491, 0.051161349272079978, -0.47888274934098413, 3.0237886236125715, -14.242821582033635, 52.78117640899567, -158.98174114677053, 397.73209781002475, -838.94754038097642, 1508.0342888526534, -2327.5532386995651, 3100.6078653842924, -3576.4842798171085, 3577.6244600587988, 
                    -3103.4043182660871, 2330.5984464086459, -1510.1055726857576, 839.65665093227449, -397.45435286692629, 158.34352922698702, -52.223679134123323, 13.909427668617829, -2.8732964960459046, 0.42634457726733316, -0.037037037037037035, 0.0
                 }, { 
                    1.0, 1.0, -51.368330315575406, 424.10248675114315, -2216.8290028877168, 8644.5605198500889, -26778.537896513255, 68109.956227771341, -145134.78294361397, 262560.46128650929, -406896.929047299, 543436.73100486991, -627849.72439483833, 628674.41980172019, -545695.699281804, 410024.14699707174, 
                    -265846.80411686015, 147976.55910679713, -70182.853510696543, 28062.526671477211, -9318.8860445106675, 2515.1262741069354, -534.03892264246588, 84.584648568929666, -9.0371542535730729, 0.50326765419041009, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 81.1824367057358, -819.42760678005629, 4695.8361668790176, -19264.504470435681, 61541.459455150565, -159666.3588828815, 344814.87488135911, -629641.30681501958, 982298.12463893753, -1318317.448292268, 1528595.4567495291, -1534752.0631456808, 1334916.510321205, -1004600.2109066803, 
                    652134.05635752552, -363328.18047525047, 172442.68534148927, -68988.339072745061, 22918.768245248033, -6187.5759379681876, 1314.1092078684619, -208.17059215858427, 22.243641755001796, -1.2387996473289582, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -49.026852235790308, 712.63082151032052, -4853.7301879680454, 21944.036782236384, -74426.135655200967, 200795.79883009219, -445339.02765963139, 828551.71773602918, -1310125.2696064098, 1775716.9519583234, -2074125.5063427235, 2094026.3619739451, -1829038.7549992425, 1380885.9701200393, 
                    -898614.236262253, 501602.77902590344, -238418.00606205946, 95489.269229060927, -31749.572824124745, 8577.1393042575655, -1822.4480435399325, 288.79258424616665, -30.865139289125288, 1.7191782704845744, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 35.805970310118845, -573.81103813200934, 4478.7704077977987, -22326.436950871157, 80965.648043619934, -228818.08070578202, 524428.00766493124, -999067.60065946809, 1607400.4124723074, -2206925.0844755503, 2602930.9622225282, -2647333.1303487155, 2325386.576138014, -1763232.7377349618, 
                    1151261.17361439, -644284.96582847636, 306843.9678881336, -123081.42733914993, 40971.027245635814, -11077.885327123367, 2355.29274893606, -373.3971007006694, 39.919381950618529, -2.2238986460019876, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -28.25025639331761, 472.43477704222204, -3938.5415709021195, 21175.694792509046, -81820.62604729092, 242808.94773553984, -577334.99257525255, 1130637.7715183964, -1857324.7897070122, 2590622.4592630644, -3092500.4738431904, 3174466.4390047379, -2808362.6977355834, 2141224.9762633117, 
                    -1404046.5049347531, 788355.31129689852, -376418.41380168468, 151285.66529727099, -50434.825183370362, 13652.023223226228, -2904.9732473283807, 460.80951652936886, -49.283769311140261, 2.7462307131845667, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 23.258832286875322, -397.93417843931445, 3440.4706706498123, -19396.130334022146, 78864.455250466955, -244978.06429516413, 604872.85155572544, -1220658.6937167749, 2052958.3371642551, -2916536.3714072923, 3531676.4037359743, -3665883.6954735122, 3271393.8468096219, -2511217.4975869441, 
                    1655380.2551727302, -933305.248289309, 447050.61907442706, -180114.96619671691, 60158.323881386335, -16306.971169289383, 3473.5012601395724, -551.3983511769477, 59.001284433018164, -3.2886897239150219, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -19.670810494254308, 341.1982595457676, -3016.2993412752389, 17532.22741158897, -73944.242133680644, 238673.95094907237, -610608.90185835, 1270480.0235325338, -2191462.5173376976, 3177677.5330197555, -3911414.4959244514, 4113218.0605342248, -3708573.1639836826, 2870020.8216697071, 
                    -1904020.1394837629, 1078870.6317369181, -518793.23534136411, 209650.29748161227, -70184.599167254637, 19057.795762046218, -4064.6309757930339, 645.82208050377778, -69.146921174654111, 3.8556086903366973, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 16.94174251097974, -296.49781341319226, 2659.667306664328, -15781.053288770965, 68318.839139187912, -227212.78495818059, 599566.714459778, -1284467.5977225688, 2273555.8841558909, -3369836.9042721242, 4224005.4510062514, -4508218.0652591381, 4113554.176963076, -3213996.6556778108, 
                    2148474.1228563273, -1224706.9146469936, 591695.51804592554, -239985.39634894222, 80565.253278500837, -21922.847077000344, 4682.9803781874689, -744.89580549975631, 79.8138773529824, -4.4523845704184053, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -14.779067766889172, 260.24313622583111, -2358.1325532596816, 14195.980876411855, -62632.587224489049, 213127.51163148019, -576961.67097611609, 1268984.1085508149, -2303220.4507637569, 3491828.746708686, -4463349.6032437421, 4842745.3639728269, -4479303.5215522023, 3538751.8195184553, 
                    -2386771.2727308418, 1370265.1974924591, -665752.40909976664, 271210.73957051244, -91356.611607028361, 24923.1115833624, -5333.9617437092829, 849.58859628813343, -91.113903717000241, 5.085519551211477, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 13.010368585706503, -230.11074661027865, 2100.3013135847191, -12777.754860225417, 57174.404448392525, -198006.70410456555, 547203.2694729116, -1231122.2536471745, 2287061.0415140111, -3545690.6475316193, 4625498.1004023217, -5109117.6518072831, 4798103.1394828632, -3838958.7653683312, 
                    2616283.7019075342, -1514688.3742462494, 740863.0766939054, -303402.32812600891, 102618.04267083252, -28082.291773778721, 6023.8964748528933, -961.05392505764416, 103.18149224599726, -5.7628868086966456, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -11.5269482507568, 204.54150930143553, -1877.030585020842, 11509.845982159593, -52054.140014143239, 182752.22076348111, -513481.412954298, 1177484.2814845247, -2233233.9669247372, 3536547.504792287, -4709225.0055647185, 5300680.5908713117, -5061738.5746617476, 4108243.8791860356, 
                    -2833545.3479977986, 1656689.6769277896, -816779.35594652547, 336607.53049345367, -114410.01414845348, 31427.04076351838, -6760.2088556970757, 1080.678701466989, -116.18053497774579, 6.4941509143302785, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 10.25664389479531, -182.45371240476541, 1681.2414388091131, -10371.76259305378, 47296.296576462839, -167839.61118210049, 477908.51056782482, -1113426.6735113813, 2150148.0306073111, -3471969.2386294506, 4716414.6258285576, -5412545.7535876436, 5261905.1145937284, -4339170.5745057892, 
                    3034064.8911046805, -1794399.6607816867, 893033.89501334645, -370823.66352420818, 126790.67791707377, -34987.13379526572, 7551.67791569344, -1210.1512650594259, 130.31379266075328, -7.291372660618749, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -9.149530629932098, 163.07432017670826, -1507.4867845602041, 9343.8417007616754, -42886.7210953976, 153496.20926409206, -441806.72223764338, 1042939.4443310056, -2045379.523592537, 3360850.0132382852, -4652014.9731874233, 5442362.1868891595, -5390869.0155201992, 4523384.6105818218, 
                    -3212157.1408638856, 1925175.8051079365, -968838.17066255736, 405964.37446675886, -139809.5755367899, 38795.355323342941, -8408.7417651061969, 1351.5536335387214, -145.83626244894432, 8.1698780555459543, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 8.1698780555459543, -145.83626244894432, 1351.5536335387214, -8408.7417651061969, 38795.355323342941, -139809.5755367899, 405964.37446675886, -968838.17066255736, 1925175.8051079365, -3212157.1408638856, 4523384.6105818218, -5390869.0155201992, 5442362.1868891595, -4652014.9731874233, 
                    3360850.0132382852, -2045379.523592537, 1042939.4443310056, -441806.72223764338, 153496.20926409206, -42886.7210953976, 9343.8417007616754, -1507.4867845602041, 163.07432017670826, -9.149530629932098, 0.0, 0.0
                 }, 
                { 
                    0.0, 0.0, -7.291372660618749, 130.31379266075328, -1210.1512650594259, 7551.67791569344, -34987.13379526572, 126790.67791707377, -370823.66352420818, 893033.89501334645, -1794399.6607816867, 3034064.8911046805, -4339170.5745057892, 5261905.1145937284, -5412545.7535876436, 4716414.6258285576, 
                    -3471969.2386294506, 2150148.0306073111, -1113426.6735113813, 477908.51056782482, -167839.61118210049, 47296.296576462839, -10371.76259305378, 1681.2414388091131, -182.45371240476541, 10.25664389479531, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 6.4941509143302785, -116.18053497774579, 1080.678701466989, -6760.2088556970757, 31427.04076351838, -114410.01414845348, 336607.53049345367, -816779.35594652547, 1656689.6769277896, -2833545.3479977986, 4108243.8791860356, -5061738.5746617476, 5300680.5908713117, -4709225.0055647185, 
                    3536547.504792287, -2233233.9669247372, 1177484.2814845247, -513481.412954298, 182752.22076348111, -52054.140014143239, 11509.845982159593, -1877.030585020842, 204.54150930143553, -11.5269482507568, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -5.7628868086966456, 103.18149224599726, -961.05392505764416, 6023.8964748528933, -28082.291773778721, 102618.04267083252, -303402.32812600891, 740863.0766939054, -1514688.3742462494, 2616283.7019075342, -3838958.7653683312, 4798103.1394828632, -5109117.6518072831, 4625498.1004023217, 
                    -3545690.6475316193, 2287061.0415140111, -1231122.2536471745, 547203.2694729116, -198006.70410456555, 57174.404448392525, -12777.754860225417, 2100.3013135847191, -230.11074661027865, 13.010368585706503, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 5.085519551211477, -91.113903717000241, 849.58859628813343, -5333.9617437092829, 24923.1115833624, -91356.611607028361, 271210.73957051244, -665752.40909976664, 1370265.1974924591, -2386771.2727308418, 3538751.8195184553, -4479303.5215522023, 4842745.3639728269, -4463349.6032437421, 
                    3491828.746708686, -2303220.4507637569, 1268984.1085508149, -576961.67097611609, 213127.51163148019, -62632.587224489049, 14195.980876411855, -2358.1325532596816, 260.24313622583111, -14.779067766889172, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -4.4523845704184053, 79.8138773529824, -744.89580549975631, 4682.9803781874689, -21922.847077000344, 80565.253278500837, -239985.39634894222, 591695.51804592554, -1224706.9146469936, 2148474.1228563273, -3213996.6556778108, 4113554.176963076, -4508218.0652591381, 4224005.4510062514, 
                    -3369836.9042721242, 2273555.8841558909, -1284467.5977225688, 599566.714459778, -227212.78495818059, 68318.839139187912, -15781.053288770965, 2659.667306664328, -296.49781341319226, 16.94174251097974, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 3.8556086903366973, -69.146921174654111, 645.82208050377778, -4064.6309757930339, 19057.795762046218, -70184.599167254637, 209650.29748161227, -518793.23534136411, 1078870.6317369181, -1904020.1394837629, 2870020.8216697071, -3708573.1639836826, 4113218.0605342248, -3911414.4959244514, 
                    3177677.5330197555, -2191462.5173376976, 1270480.0235325338, -610608.90185835, 238673.95094907237, -73944.242133680644, 17532.22741158897, -3016.2993412752389, 341.1982595457676, -19.670810494254308, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -3.2886897239150219, 59.001284433018164, -551.3983511769477, 3473.5012601395724, -16306.971169289383, 60158.323881386335, -180114.96619671691, 447050.61907442706, -933305.248289309, 1655380.2551727302, -2511217.4975869441, 3271393.8468096219, -3665883.6954735122, 3531676.4037359743, 
                    -2916536.3714072923, 2052958.3371642551, -1220658.6937167749, 604872.85155572544, -244978.06429516413, 78864.455250466955, -19396.130334022146, 3440.4706706498123, -397.93417843931445, 23.258832286875322, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 2.7462307131845667, -49.283769311140261, 460.80951652936886, -2904.9732473283807, 13652.023223226228, -50434.825183370362, 151285.66529727099, -376418.41380168468, 788355.31129689852, -1404046.5049347531, 2141224.9762633117, -2808362.6977355834, 3174466.4390047379, -3092500.4738431904, 
                    2590622.4592630644, -1857324.7897070122, 1130637.7715183964, -577334.99257525255, 242808.94773553984, -81820.62604729092, 21175.694792509046, -3938.5415709021195, 472.43477704222204, -28.25025639331761, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -2.2238986460019876, 39.919381950618529, -373.3971007006694, 2355.29274893606, -11077.885327123367, 40971.027245635814, -123081.42733914993, 306843.9678881336, -644284.96582847636, 1151261.17361439, -1763232.7377349618, 2325386.576138014, -2647333.1303487155, 2602930.9622225282, 
                    -2206925.0844755503, 1607400.4124723074, -999067.60065946809, 524428.00766493124, -228818.08070578202, 80965.648043619934, -22326.436950871157, 4478.7704077977987, -573.81103813200934, 35.805970310118845, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 1.7191782704845744, -30.865139289125288, 288.79258424616665, -1822.4480435399325, 8577.1393042575655, -31749.572824124745, 95489.269229060927, -238418.00606205946, 501602.77902590344, -898614.236262253, 1380885.9701200393, -1829038.7549992425, 2094026.3619739451, -2074125.5063427235, 
                    1775716.9519583234, -1310125.2696064098, 828551.71773602918, -445339.02765963139, 200795.79883009219, -74426.135655200967, 21944.036782236384, -4853.7301879680454, 712.63082151032052, -49.026852235790308, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -1.2387996473289582, 22.243641755001796, -208.17059215858427, 1314.1092078684619, -6187.5759379681876, 22918.768245248033, -68988.339072745061, 172442.68534148927, -363328.18047525047, 652134.05635752552, -1004600.2109066803, 1334916.510321205, -1534752.0631456808, 1528595.4567495291, 
                    -1318317.448292268, 982298.12463893753, -629641.30681501958, 344814.87488135911, -159666.3588828815, 61541.459455150565, -19264.504470435681, 4695.8361668790176, -819.42760678005629, 81.1824367057358, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 0.50326765419041009, -9.0371542535730729, 84.584648568929666, -534.03892264246588, 2515.1262741069354, -9318.8860445106675, 28062.526671477211, -70182.853510696543, 147976.55910679713, -265846.80411686015, 410024.14699707174, -545695.699281804, 628674.41980172019, -627849.72439483833, 
                    543436.73100486991, -406896.929047299, 262560.46128650929, -145134.78294361397, 68109.956227771341, -26778.537896513255, 8644.5605198500889, -2216.8290028877168, 424.10248675114315, -51.368330315575406, 1.0, 1.0
                 }
             };
            private static readonly double[,] matrix_28 = new double[,] { 
                { 
                    0.0, 0.035714285714285712, -0.42735524414338705, 2.998715496231569, -15.140744830634002, 59.4031045539564, -188.59977532642813, 496.83242521559066, -1104.3085156792692, 2095.391020391387, -3422.4535684764196, 4839.8393406035757, -5948.6809547214452, 6368.9958384389056, -5944.3961158763113, 4833.3032757111741, 
                    -3416.3571816025237, 2091.499402957812, -1102.8373791533616, 496.93883205567113, -189.26949532022502, 60.009019422045313, -15.496462057553844, 3.1543218397154171, -0.47979447939705105, 0.049310220478083124, -0.0026455026455026454, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 0.0026455026455026454, -0.049310220478083124, 0.47979447939705105, -3.1543218397154171, 15.496462057553844, -60.009019422045313, 189.26949532022502, -496.93883205567113, 1102.8373791533616, -2091.499402957812, 3416.3571816025237, -4833.3032757111741, 5944.3961158763113, -6368.9958384389056, 
                    5948.6809547214452, -4839.8393406035757, 3422.4535684764196, -2095.391020391387, 1104.3085156792692, -496.83242521559066, 188.59977532642813, -59.4031045539564, 15.140744830634002, -2.998715496231569, 0.42735524414338705, -0.035714285714285712, 0.0
                 }, { 
                    1.0, 1.0, -55.012165977418704, 473.7477264602723, -2584.646147362706, 10536.315400415593, -34185.781318093286, 91270.948270642475, -204654.5233031449, 390658.3437248476, -640773.07837762823, 908917.21681867051, -1119675.9032795983, 1200812.2732589061, -1122191.4347668584, 913329.87704306049, 
                    -646060.95207169512, 395749.43106189376, -208770.56140678795, 94104.7552339125, -35851.332992334668, 11369.195788499688, -2936.3809262592658, 597.77109473797907, -90.932491429001075, 9.3459678587054, -0.50142931821746117, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 86.842151806944372, -913.504760406865, 5463.9174077086027, -23436.5041540842, 78432.041854436276, -213637.16662374584, 485561.55482956546, -935683.05138170114, 1545194.4458145394, -2202730.1181163229, 2723552.4254180524, -2929072.456272292, 2743122.2749404362, -2236221.7043780917, 
                    1583828.3712159831, -971133.58421631774, 512692.17360464134, -231234.92564273623, 88133.930507767538, -27958.715591001383, 7222.9157644942479, -1470.6797309583444, 223.74916698247816, -22.998864881737823, 1.2340019940158795, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -52.467942896586017, 792.39833238350673, -5629.5646253848536, 26610.129853598035, -94562.042429294728, 267904.44577956659, -625478.046810396, 1228331.844089197, -2056382.7595719111, 2961075.2407012158, -3688825.7526780325, 3989811.7355839028, -3752790.6228004261, 3069559.0958007569, 
                    -2179677.3356550271, 1339164.8225885313, -708088.70563777327, 319748.07047870156, -121983.50652609265, 38724.285751807533, -10009.466560538536, 2038.8610589465277, -310.27933682617419, 31.8992032359334, -1.7117439834392814, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 38.341968325938645, -638.01286775959852, 5185.0754735738274, -27000.04619810087, 102556.1227870562, -304340.88740571978, 734322.85516952467, -1476844.9544548, 2516146.1357700559, -3670833.1073583048, 4618459.90008808, -5033120.6848194906, 4761655.8429889726, -3912258.619490277, 
                    2787764.9091246813, -1717412.3146360451, 910004.39558065555, -411598.68731089006, 157222.71781139824, -49959.35110460392, 12922.910110995535, -2633.7229414956118, 400.96030769269896, -41.232541709465217, 2.2129242916805132, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -30.274230973293179, 525.55940063752473, -4558.2784526433788, 25568.019809513662, -103384.80457036154, 322003.99516626471, -805875.813744632, 1666052.2158168138, -2898373.3438131534, 4296257.13751673, -5471684.280943756, 6019355.35051802, -5736435.2857951662, 4740029.5450202292, 
                    -3392634.489625325, 2097302.9647927508, -1114307.6183722008, 505067.71406031714, -193240.28886891389, 61480.929404483453, -15918.144027320419, 3246.4122340707218, -494.48131356920669, 50.866664605718512, -2.7305307117703159, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 24.948798110014927, -443.041648828676, 3983.3886111539064, -23409.300650803543, 99502.342158859421, -324127.95114178257, 841899.54234074126, -1793024.1511821558, 3193166.63472733, -4820907.0820003282, 6228754.2804935807, -6929813.6602820065, 6662728.1769449105, -5543787.0979522411, 
                    3989628.9738466591, -2476953.5038710167, 1320444.9084533448, -600071.42297117959, 230055.81475227885, -73308.243781360521, 19002.7436403432, -3878.8716421012364, 591.18196249107393, -60.839555915458426, 3.2667014899717026, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -21.124297022695554, 380.28127433676531, -3495.1494434964366, 21166.142814360483, -93247.2815197497, 315327.8895157199, -847952.96000717476, 1860871.2491363906, -3397605.0179860531, 5234643.1696688151, -6874574.5277446108, 7748760.6110267658, -7527950.3202737262, 6315654.4070195742, 
                    -4574972.4553091535, 2855109.0682841223, -1528257.7939389225, 696734.34549684171, -267779.04869976483, 85492.107038752743, -22193.083844160046, 4534.9378539447225, -691.70159661208243, 71.2208328438609, -3.8252992667101395, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 18.218552871750465, -330.89827769243442, 3085.4941118967158, -19067.695784403553, 86174.090897185262, -300014.57497250644, 831387.29353190551, -1877041.2616577912, 3514584.9428808619, -5532633.0390735166, 7397346.1867984412, -8461552.1174568981, 8319218.8243769342, -7047054.24629405, 
                    5144365.4446781361, -3230284.7482682294, 1737542.8427257636, -795199.46411809, 306541.13138644944, -98094.184852784412, 25509.225509243235, -5219.3407972987707, 796.83173018636342, -82.096916790610464, 4.411126663578921, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -15.918751568902442, 290.90074355701978, -2739.7582550738662, 17174.042864271403, -79067.736280721423, 281471.501325708, -799535.10590779141, 1851554.1408417902, -3551974.7366638794, 5715452.95445315, -7788972.4474180415, 9054743.0893628523, -9023027.7401837744, 7728175.6732423017, 
                    -5692505.4482276589, 3600458.2121543735, -1947900.5625882519, 895569.84467415966, -346476.81598002679, 111182.77736383991, -28974.15611085877, 5937.6049559209941, -907.50760392609868, 93.57064306745454, -5.0299212626315173, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 14.040557238511525, -257.70698227221425, 2444.65588003018, -15483.960584802453, 72275.01521995326, -261727.242022965, 758421.22948925593, -1795043.8413587902, 3521255.1908232132, -5788988.1150375409, 8045773.96637207, -9516766.804057952, 9625440.7022274733, -8347768.8057943778, 
                    6212773.4840030065, -3962837.8909021649, 2158619.3454004251, -997867.12767652224, 387714.29530720686, -124831.49764565844, 32613.907269918614, -6696.1526930291984, 1024.8333558506522, -105.76454158086821, 5.6885548382721725, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -12.467777389017682, 229.58564889121195, -2189.5724258385162, 13976.462804658917, -65924.074671616429, 241919.19918616835, -712327.2922725213, 1717074.8156953761, -3435660.6670135693, 5763737.8858340364, -8169076.09787716, 9838896.3034104872, -10112626.632859251, 8893132.0574752539, 
                    -6696967.8666547192, 4313617.8954590717, -2368541.9064598936, 1101982.3610854314, -430363.52735451888, 139118.14484953936, -36457.910810037451, 7502.4915834541, -1150.1244118210873, 118.82622303515063, -6.3953569689002379, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 11.12329068387429, -205.33621346221634, 1966.3104070478967, -12626.323667370969, 60039.835075481809, -222638.51016418485, 664051.53029140318, -1625236.8340333961, 3308228.7032854566, -5653410.34355542, 8165335.5306179682, -10016301.450824128, 10471728.725542802, -9350326.7997300569, 
                    7135089.59049646, -4647699.1087424653, 2575893.3169585238, -1207607.65681449, 474498.79056788521, -154122.66039738647, 40539.394700521123, -8365.464084064195, 1284.9663060624198, -132.93598600252147, 7.1605760239385354, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -9.95376012414208, 184.10122522113872, -1768.5733575090214, 11409.596562616685, -54601.076264048228, 204161.26422480849, -615324.45645022462, 1525095.4163517635, -3150365.3889181674, 5473019.887347688, -8045525.7463289257, 10048931.776917258, -10692038.804285659, 9704706.9228670076, 
                    -7515237.4175657583, 4958381.9102457678, -2778056.6015335005, 1314137.8002342442, -520131.08064196678, 169923.07348275997, -44895.668341626377, 9295.55887557081, -1431.2939767901021, 148.31739187941866, -7.99702918994, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 8.921032315172658, -165.25212071101544, 1591.4987176057157, -10305.284837567684, 49568.062686198013, -186587.77144486157, 567164.113574121, -1420528.5383172443, 2971286.7062568804, -5237063.8411491476, 7823718.6582263419, -9941853.2711945139, 10766333.587886658, -9941853.2711945139, 
                    7823718.6582263419, -5237063.8411491476, 2971286.7062568804, -1420528.5383172443, 567164.113574121, -186587.77144486157, 49568.062686198013, -10305.284837567684, 1591.4987176057157, -165.25212071101544, 8.921032315172658, 0.0, 0.0
                 }, 
                { 
                    0.0, 0.0, -7.99702918994, 148.31739187941866, -1431.2939767901021, 9295.55887557081, -44895.668341626377, 169923.07348275997, -520131.08064196678, 1314137.8002342442, -2778056.6015335005, 4958381.9102457678, -7515237.4175657583, 9704706.9228670076, -10692038.804285659, 10048931.776917258, 
                    -8045525.7463289257, 5473019.887347688, -3150365.3889181674, 1525095.4163517635, -615324.45645022462, 204161.26422480849, -54601.076264048228, 11409.596562616685, -1768.5733575090214, 184.10122522113872, -9.95376012414208, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 7.1605760239385354, -132.93598600252147, 1284.9663060624198, -8365.464084064195, 40539.394700521123, -154122.66039738647, 474498.79056788521, -1207607.65681449, 2575893.3169585238, -4647699.1087424653, 7135089.59049646, -9350326.7997300569, 10471728.725542802, -10016301.450824128, 
                    8165335.5306179682, -5653410.34355542, 3308228.7032854566, -1625236.8340333961, 664051.53029140318, -222638.51016418485, 60039.835075481809, -12626.323667370969, 1966.3104070478967, -205.33621346221634, 11.12329068387429, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -6.3953569689002379, 118.82622303515063, -1150.1244118210873, 7502.4915834541, -36457.910810037451, 139118.14484953936, -430363.52735451888, 1101982.3610854314, -2368541.9064598936, 4313617.8954590717, -6696967.8666547192, 8893132.0574752539, -10112626.632859251, 9838896.3034104872, 
                    -8169076.09787716, 5763737.8858340364, -3435660.6670135693, 1717074.8156953761, -712327.2922725213, 241919.19918616835, -65924.074671616429, 13976.462804658917, -2189.5724258385162, 229.58564889121195, -12.467777389017682, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 5.6885548382721725, -105.76454158086821, 1024.8333558506522, -6696.1526930291984, 32613.907269918614, -124831.49764565844, 387714.29530720686, -997867.12767652224, 2158619.3454004251, -3962837.8909021649, 6212773.4840030065, -8347768.8057943778, 9625440.7022274733, -9516766.804057952, 
                    8045773.96637207, -5788988.1150375409, 3521255.1908232132, -1795043.8413587902, 758421.22948925593, -261727.242022965, 72275.01521995326, -15483.960584802453, 2444.65588003018, -257.70698227221425, 14.040557238511525, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -5.0299212626315173, 93.57064306745454, -907.50760392609868, 5937.6049559209941, -28974.15611085877, 111182.77736383991, -346476.81598002679, 895569.84467415966, -1947900.5625882519, 3600458.2121543735, -5692505.4482276589, 7728175.6732423017, -9023027.7401837744, 9054743.0893628523, 
                    -7788972.4474180415, 5715452.95445315, -3551974.7366638794, 1851554.1408417902, -799535.10590779141, 281471.501325708, -79067.736280721423, 17174.042864271403, -2739.7582550738662, 290.90074355701978, -15.918751568902442, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 4.411126663578921, -82.096916790610464, 796.83173018636342, -5219.3407972987707, 25509.225509243235, -98094.184852784412, 306541.13138644944, -795199.46411809, 1737542.8427257636, -3230284.7482682294, 5144365.4446781361, -7047054.24629405, 8319218.8243769342, -8461552.1174568981, 
                    7397346.1867984412, -5532633.0390735166, 3514584.9428808619, -1877041.2616577912, 831387.29353190551, -300014.57497250644, 86174.090897185262, -19067.695784403553, 3085.4941118967158, -330.89827769243442, 18.218552871750465, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -3.8252992667101395, 71.2208328438609, -691.70159661208243, 4534.9378539447225, -22193.083844160046, 85492.107038752743, -267779.04869976483, 696734.34549684171, -1528257.7939389225, 2855109.0682841223, -4574972.4553091535, 6315654.4070195742, -7527950.3202737262, 7748760.6110267658, 
                    -6874574.5277446108, 5234643.1696688151, -3397605.0179860531, 1860871.2491363906, -847952.96000717476, 315327.8895157199, -93247.2815197497, 21166.142814360483, -3495.1494434964366, 380.28127433676531, -21.124297022695554, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 3.2667014899717026, -60.839555915458426, 591.18196249107393, -3878.8716421012364, 19002.7436403432, -73308.243781360521, 230055.81475227885, -600071.42297117959, 1320444.9084533448, -2476953.5038710167, 3989628.9738466591, -5543787.0979522411, 6662728.1769449105, -6929813.6602820065, 
                    6228754.2804935807, -4820907.0820003282, 3193166.63472733, -1793024.1511821558, 841899.54234074126, -324127.95114178257, 99502.342158859421, -23409.300650803543, 3983.3886111539064, -443.041648828676, 24.948798110014927, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -2.7305307117703159, 50.866664605718512, -494.48131356920669, 3246.4122340707218, -15918.144027320419, 61480.929404483453, -193240.28886891389, 505067.71406031714, -1114307.6183722008, 2097302.9647927508, -3392634.489625325, 4740029.5450202292, -5736435.2857951662, 6019355.35051802, 
                    -5471684.280943756, 4296257.13751673, -2898373.3438131534, 1666052.2158168138, -805875.813744632, 322003.99516626471, -103384.80457036154, 25568.019809513662, -4558.2784526433788, 525.55940063752473, -30.274230973293179, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 2.2129242916805132, -41.232541709465217, 400.96030769269896, -2633.7229414956118, 12922.910110995535, -49959.35110460392, 157222.71781139824, -411598.68731089006, 910004.39558065555, -1717412.3146360451, 2787764.9091246813, -3912258.619490277, 4761655.8429889726, -5033120.6848194906, 
                    4618459.90008808, -3670833.1073583048, 2516146.1357700559, -1476844.9544548, 734322.85516952467, -304340.88740571978, 102556.1227870562, -27000.04619810087, 5185.0754735738274, -638.01286775959852, 38.341968325938645, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -1.7117439834392814, 31.8992032359334, -310.27933682617419, 2038.8610589465277, -10009.466560538536, 38724.285751807533, -121983.50652609265, 319748.07047870156, -708088.70563777327, 1339164.8225885313, -2179677.3356550271, 3069559.0958007569, -3752790.6228004261, 3989811.7355839028, 
                    -3688825.7526780325, 2961075.2407012158, -2056382.7595719111, 1228331.844089197, -625478.046810396, 267904.44577956659, -94562.042429294728, 26610.129853598035, -5629.5646253848536, 792.39833238350673, -52.467942896586017, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 1.2340019940158795, -22.998864881737823, 223.74916698247816, -1470.6797309583444, 7222.9157644942479, -27958.715591001383, 88133.930507767538, -231234.92564273623, 512692.17360464134, -971133.58421631774, 1583828.3712159831, -2236221.7043780917, 2743122.2749404362, -2929072.456272292, 
                    2723552.4254180524, -2202730.1181163229, 1545194.4458145394, -935683.05138170114, 485561.55482956546, -213637.16662374584, 78432.041854436276, -23436.5041540842, 5463.9174077086027, -913.504760406865, 86.842151806944372, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -0.50142931821746117, 9.3459678587054, -90.932491429001075, 597.77109473797907, -2936.3809262592658, 11369.195788499688, -35851.332992334668, 94104.7552339125, -208770.56140678795, 395749.43106189376, -646060.95207169512, 913329.87704306049, -1122191.4347668584, 1200812.2732589061, 
                    -1119675.9032795983, 908917.21681867051, -640773.07837762823, 390658.3437248476, -204654.5233031449, 91270.948270642475, -34185.781318093286, 10536.315400415593, -2584.646147362706, 473.7477264602723, -55.012165977418704, 1.0, 1.0
                 }
             };
            private static readonly double[,] matrix_29 = new double[,] { 
                { 
                    0.0, 0.034482758620689655, -0.42829070161231636, 3.1241001075483328, -16.423806074976582, 67.208703553669991, -222.97991727869194, 615.08743178989664, -1434.8333112030339, 2864.4667663847608, -4936.1297140316137, 7387.4260594869756, -9642.8614207114824, 11007.530150023518, -11003.110805915559, 9631.58888127058, 
                    -7373.9528511323106, 4924.95070632465, -2857.7593081235659, 1432.2333831923804, -614.92856194415742, 223.66815701450787, -67.863453084819284, 16.802175888417498, -3.2847612149953163, 0.48063078577666657, -0.047587855734701819, 0.0024630541871921183, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -0.0024630541871921183, 0.047587855734701819, -0.48063078577666657, 3.2847612149953163, -16.802175888417498, 67.863453084819284, -223.66815701450787, 614.92856194415742, -1432.2333831923804, 2857.7593081235659, -4924.95070632465, 7373.9528511323106, -9631.58888127058, 11003.110805915559, 
                    -11007.530150023518, 9642.8614207114824, -7387.4260594869756, 4936.1297140316137, -2864.4667663847608, 1434.8333112030339, -615.08743178989664, 222.97991727869194, -67.208703553669991, 16.423806074976582, -3.1241001075483328, 0.42829070161231636, -0.034482758620689655, 0.0
                 }, { 
                    1.0, 1.0, -58.77729696437568, 527.08151258096314, -2996.1172097664862, 12743.787030520594, -43218.50747748607, 120846.85236806897, -284423.87105071626, 571288.01613185229, -988711.63628925406, 1484341.2149855304, -1942007.3593680607, 2220699.0518272459, -2222745.8235656363, 1947662.5724100152, 
                    -1492305.8426560569, 997298.49853898259, -578970.23826825945, 290272.67349898565, -124664.55372082838, 45354.465643924545, -13763.432523580486, 3408.105484605092, -666.33601934981186, 97.505948340396344, -9.654637208680386, 0.49971927304088776, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 92.6902162786922, -1014.4481797669425, 6321.8690900458769, -28297.411680993864, 98999.576805446486, -282464.31084891735, 673961.08494583273, -1366744.6985720664, 2381756.8767409967, -3593868.2264164239, 4719796.9809510894, -5412614.5864341389, 5429503.9682028349, -4765622.8955182983, 
                    3656244.0509785637, -2445953.4677725746, 1421109.8561415426, -712934.90575545258, 306336.7008234458, -111491.23828555182, 33843.396231762177, -8382.1612028448635, 1639.1067585023402, -239.8810026050688, 23.753905848943248, -1.2295494302911412, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -56.023440347508313, 877.85776993918444, -6494.1828899856382, 32032.820738212293, -119018.38160347888, 353274.09386392555, -866044.58499033737, 1790199.8054580134, -3163216.0475900173, 4822113.3767804932, -6381641.8692529248, 7361210.166994256, -7417308.8969014985, 6532956.7038410436, 
                    -5025687.0120156826, 3369166.21946124, -1960726.9512653281, 984915.26782804064, -423627.91022492241, 154299.65728795616, -46866.064135013563, 11612.82174834973, -2271.6170306784766, 332.52808040480227, -32.933523076554465, 1.70487300315339, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 40.962165828260012, -706.78292890121543, 5971.2269126903648, -32420.38435129456, 128714.6170901829, -400161.92353766027, 1013885.5048687932, -2146602.0463070464, 3860653.0655217511, -5963837.3217243673, 7972370.8101123841, -9267241.5088728014, 9393620.2492352035, -8312024.8667668765, 
                    6417460.4329833053, -4314406.1433383431, 2516409.5396070033, -1266251.5787331855, 545377.447262084, -198855.22988170836, 60448.287246419924, -14987.56999070066, 2933.102334597686, -429.49949016803919, 42.546972525436232, -2.2028292093182054, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -32.365278323515831, 582.46787102668679, -5247.8524812854112, 30656.267096851392, -129462.06078074437, 422245.28918112838, -1109468.1431952477, 2414539.06326238, -4434377.13749106, 6960690.65327525, -9420464.59865415, 11055799.047981018, -11290472.393576961, 10049000.7285162, 
                    -7794213.6387943607, 5258946.1176736774, -3076053.0708441841, 1551325.8423937433, -669329.81685476622, 244383.16576254967, -74365.876097616041, 18452.9786052102, -3613.4229704650479, 529.34383019809763, -52.45272048589176, 2.7161609485296223, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 26.694626556500438, -491.37064648399286, 4587.5294257484811, -28056.450153905214, 124431.34898285181, -424129.92927238683, 1156028.2387918425, -2591029.6409617, 4870681.01257129, -7787128.2021287559, 10692188.216448389, -12691787.102604143, 13078008.996828252, -11722832.604276499, 
                    9143644.19624508, -6196904.2982421489, 3637434.2620870057, -1839531.7837612533, 795406.09781447332, -290908.46426696493, 88639.400308826778, -22016.641939604368, 4314.4331127393825, -632.37164114821985, 62.684163291368655, -3.2466803532552539, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -22.625669726574841, 422.16615376892605, -4028.1535343069331, 25374.345077772803, -116553.25265439357, 412066.45067199523, -1161935.1002185273, 2682078.8894878714, -5167287.2868041992, 8429063.4899750035, -11763285.246731397, 14146895.724834235, -14730895.075872034, 13315537.439562147, 
                    -10455654.201688703, 7123983.8547813743, -4199377.1675371211, 2130863.9905296951, -923818.10495463072, 338572.81127876788, -103327.70053118411, 25696.213805374842, -5040.0431757224806, 739.20410643546029, -73.306125266651478, 3.797847574616279, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 19.537247484514872, -367.77633092784544, 3559.691154507283, -22875.102842937184, 107732.77377724754, -391845.50673965854, 1137704.1826758832, -2699774.0652296213, 5331089.4775769627, -8881936.2410232555, 12616591.544643942, -15396308.853786647, 16224379.650151454, -14808336.851766841, 
                    11719219.51317776, -8035350.0108820479, 4760536.3924761424, -2425325.5499781766, 1054823.4775656415, -387549.0383633349, 118502.93284438105, -29513.44214279201, 5795.0874385939824, -850.61112758805837, 84.3990884458082, -4.3739629530981405, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -17.095614628145057, 323.77813403877036, -3164.9784824252165, 20626.008008315803, -98919.80017944974, 367681.004515428, -1093471.8552596169, 2659407.7931435159, -5376309.6355123064, 9150359.6247586273, -13242491.656092416, 16418977.840344422, -17533908.939256769, 16180780.386502407, 
                    -12921450.763715426, 8924927.6969033983, -5318993.622290601, 2722743.5748366774, -1188657.6632479476, 438020.88155805628, -134245.74498613275, 33493.263117152936, -6585.1933872095124, 967.49743028412445, -96.058182806849786, 4.9801265370968979, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 15.104116711419673, -287.31319442215721, 2828.6130130975926, -18623.276335692553, 90527.9975194758, -342140.75492653262, 1037362.8603891297, -2576567.4011563975, 5321967.330555412, -9247309.4232727587, 13639670.638568977, -17198772.411223669, 18635718.835125461, -17410616.171960615, 
                    14047105.046694333, -9784935.38738635, 5871971.1499794908, -3022641.6866585445, 1325492.2600712785, -490173.57490708667, 150644.18442745451, -37663.927370564714, 7416.8714173240633, -1090.9230892330916, 108.3957404606016, -5.6223871573682649, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -13.43881194696457, 256.46566534694659, -2538.3381007785952, 16840.61896438909, -82705.832944145557, 316647.63523685012, -975059.92525261722, 2464872.3451845646, -5188858.1992672877, 9192378.1361197829, -13815426.073884327, 17725895.900581766, -19507984.117162008, 18474092.00158859, 
                    -15078284.149478946, 10605449.81104394, -6415528.6590164145, 3324098.4391456852, -1465389.0079964278, 544184.439550838, -167793.22123806132, 42057.435254125885, -8297.6925590678529, 1222.1402168281611, -121.54568815342265, 6.3079944497136662, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 12.017451002772678, -229.90771789795056, 2284.7096693258327, -15247.255740519178, 75477.7026649759, -291930.73118563177, 910232.06194186863, -2334923.5288525759, 4996730.9819383034, -9009129.4857239518, 13785093.766736992, -17998174.75527329, 20132396.397385687, -19346698.614745792, 
                    15994326.832183765, -11373971.476940535, 6944203.246034191, -3625564.1332840556, 1608236.609130864, -600209.13352253439, 185793.80382474617, -46710.0851462041, 9236.52988858596, -1362.6440089822634, 135.66973034539444, -7.0457557257728318, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -10.783166150503554, 206.69144824578305, -2060.4853498522693, 13814.243030058593, -68813.258717886958, 268322.51962325023, -845122.50694492133, 2194385.7722355742, -4762441.08830626, 8722060.8835907336, -13570350.337585947, 18021776.293580931, -20495998.809708718, 20004428.362560317, 
                    -16771997.554343218, 12075024.689924842, -7450579.2153052175, 3924615.9091302962, -1753659.148889082, 658359.67943461612, -204750.57834236047, 51663.027074935919, -10243.864506209035, 1514.241112576907, -150.96583174533686, 7.8465312669927263, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 9.695294640147516, -186.12238081797761, 1860.0794856141129, -12516.333473260986, 62660.739392621261, -245936.12342223234, 781033.59627281746, -2048537.8619588651, 4499369.2305441322, -8354027.9216047181, 13196560.816486776, -17810889.522456836, 20592935.437202446, -20425574.668033518, 
                    17386128.80493724, -12689880.736144781, 7924794.92061056, -4217632.0033809189, 1900882.1713553192, -718668.96426371136, 224767.04744273139, -56962.679172260454, 11332.163330841773, -1679.1410679722021, 167.67993209665627, -8.7239286387591886, 0.0, 0.0
                 }, 
                { 
                    0.0, 0.0, -8.7239286387591886, 167.67993209665627, -1679.1410679722021, 11332.163330841773, -56962.679172260454, 224767.04744273139, -718668.96426371136, 1900882.1713553192, -4217632.0033809189, 7924794.92061056, -12689880.736144781, 17386128.80493724, -20425574.668033518, 20592935.437202446, 
                    -17810889.522456836, 13196560.816486776, -8354027.9216047181, 4499369.2305441322, -2048537.8619588651, 781033.59627281746, -245936.12342223234, 62660.739392621261, -12516.333473260986, 1860.0794856141129, -186.12238081797761, 9.695294640147516, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 7.8465312669927263, -150.96583174533686, 1514.241112576907, -10243.864506209035, 51663.027074935919, -204750.57834236047, 658359.67943461612, -1753659.148889082, 3924615.9091302962, -7450579.2153052175, 12075024.689924842, -16771997.554343218, 20004428.362560317, -20495998.809708718, 
                    18021776.293580931, -13570350.337585947, 8722060.8835907336, -4762441.08830626, 2194385.7722355742, -845122.50694492133, 268322.51962325023, -68813.258717886958, 13814.243030058593, -2060.4853498522693, 206.69144824578305, -10.783166150503554, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -7.0457557257728318, 135.66973034539444, -1362.6440089822634, 9236.52988858596, -46710.0851462041, 185793.80382474617, -600209.13352253439, 1608236.609130864, -3625564.1332840556, 6944203.246034191, -11373971.476940535, 15994326.832183765, -19346698.614745792, 20132396.397385687, 
                    -17998174.75527329, 13785093.766736992, -9009129.4857239518, 4996730.9819383034, -2334923.5288525759, 910232.06194186863, -291930.73118563177, 75477.7026649759, -15247.255740519178, 2284.7096693258327, -229.90771789795056, 12.017451002772678, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 6.3079944497136662, -121.54568815342265, 1222.1402168281611, -8297.6925590678529, 42057.435254125885, -167793.22123806132, 544184.439550838, -1465389.0079964278, 3324098.4391456852, -6415528.6590164145, 10605449.81104394, -15078284.149478946, 18474092.00158859, -19507984.117162008, 
                    17725895.900581766, -13815426.073884327, 9192378.1361197829, -5188858.1992672877, 2464872.3451845646, -975059.92525261722, 316647.63523685012, -82705.832944145557, 16840.61896438909, -2538.3381007785952, 256.46566534694659, -13.43881194696457, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -5.6223871573682649, 108.3957404606016, -1090.9230892330916, 7416.8714173240633, -37663.927370564714, 150644.18442745451, -490173.57490708667, 1325492.2600712785, -3022641.6866585445, 5871971.1499794908, -9784935.38738635, 14047105.046694333, -17410616.171960615, 18635718.835125461, 
                    -17198772.411223669, 13639670.638568977, -9247309.4232727587, 5321967.330555412, -2576567.4011563975, 1037362.8603891297, -342140.75492653262, 90527.9975194758, -18623.276335692553, 2828.6130130975926, -287.31319442215721, 15.104116711419673, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 4.9801265370968979, -96.058182806849786, 967.49743028412445, -6585.1933872095124, 33493.263117152936, -134245.74498613275, 438020.88155805628, -1188657.6632479476, 2722743.5748366774, -5318993.622290601, 8924927.6969033983, -12921450.763715426, 16180780.386502407, -17533908.939256769, 
                    16418977.840344422, -13242491.656092416, 9150359.6247586273, -5376309.6355123064, 2659407.7931435159, -1093471.8552596169, 367681.004515428, -98919.80017944974, 20626.008008315803, -3164.9784824252165, 323.77813403877036, -17.095614628145057, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -4.3739629530981405, 84.3990884458082, -850.61112758805837, 5795.0874385939824, -29513.44214279201, 118502.93284438105, -387549.0383633349, 1054823.4775656415, -2425325.5499781766, 4760536.3924761424, -8035350.0108820479, 11719219.51317776, -14808336.851766841, 16224379.650151454, 
                    -15396308.853786647, 12616591.544643942, -8881936.2410232555, 5331089.4775769627, -2699774.0652296213, 1137704.1826758832, -391845.50673965854, 107732.77377724754, -22875.102842937184, 3559.691154507283, -367.77633092784544, 19.537247484514872, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 3.797847574616279, -73.306125266651478, 739.20410643546029, -5040.0431757224806, 25696.213805374842, -103327.70053118411, 338572.81127876788, -923818.10495463072, 2130863.9905296951, -4199377.1675371211, 7123983.8547813743, -10455654.201688703, 13315537.439562147, -14730895.075872034, 
                    14146895.724834235, -11763285.246731397, 8429063.4899750035, -5167287.2868041992, 2682078.8894878714, -1161935.1002185273, 412066.45067199523, -116553.25265439357, 25374.345077772803, -4028.1535343069331, 422.16615376892605, -22.625669726574841, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -3.2466803532552539, 62.684163291368655, -632.37164114821985, 4314.4331127393825, -22016.641939604368, 88639.400308826778, -290908.46426696493, 795406.09781447332, -1839531.7837612533, 3637434.2620870057, -6196904.2982421489, 9143644.19624508, -11722832.604276499, 13078008.996828252, 
                    -12691787.102604143, 10692188.216448389, -7787128.2021287559, 4870681.01257129, -2591029.6409617, 1156028.2387918425, -424129.92927238683, 124431.34898285181, -28056.450153905214, 4587.5294257484811, -491.37064648399286, 26.694626556500438, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 2.7161609485296223, -52.45272048589176, 529.34383019809763, -3613.4229704650479, 18452.9786052102, -74365.876097616041, 244383.16576254967, -669329.81685476622, 1551325.8423937433, -3076053.0708441841, 5258946.1176736774, -7794213.6387943607, 10049000.7285162, -11290472.393576961, 
                    11055799.047981018, -9420464.59865415, 6960690.65327525, -4434377.13749106, 2414539.06326238, -1109468.1431952477, 422245.28918112838, -129462.06078074437, 30656.267096851392, -5247.8524812854112, 582.46787102668679, -32.365278323515831, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -2.2028292093182054, 42.546972525436232, -429.49949016803919, 2933.102334597686, -14987.56999070066, 60448.287246419924, -198855.22988170836, 545377.447262084, -1266251.5787331855, 2516409.5396070033, -4314406.1433383431, 6417460.4329833053, -8312024.8667668765, 9393620.2492352035, 
                    -9267241.5088728014, 7972370.8101123841, -5963837.3217243673, 3860653.0655217511, -2146602.0463070464, 1013885.5048687932, -400161.92353766027, 128714.6170901829, -32420.38435129456, 5971.2269126903648, -706.78292890121543, 40.962165828260012, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 1.70487300315339, -32.933523076554465, 332.52808040480227, -2271.6170306784766, 11612.82174834973, -46866.064135013563, 154299.65728795616, -423627.91022492241, 984915.26782804064, -1960726.9512653281, 3369166.21946124, -5025687.0120156826, 6532956.7038410436, -7417308.8969014985, 
                    7361210.166994256, -6381641.8692529248, 4822113.3767804932, -3163216.0475900173, 1790199.8054580134, -866044.58499033737, 353274.09386392555, -119018.38160347888, 32032.820738212293, -6494.1828899856382, 877.85776993918444, -56.023440347508313, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -1.2295494302911412, 23.753905848943248, -239.8810026050688, 1639.1067585023402, -8382.1612028448635, 33843.396231762177, -111491.23828555182, 306336.7008234458, -712934.90575545258, 1421109.8561415426, -2445953.4677725746, 3656244.0509785637, -4765622.8955182983, 5429503.9682028349, 
                    -5412614.5864341389, 4719796.9809510894, -3593868.2264164239, 2381756.8767409967, -1366744.6985720664, 673961.08494583273, -282464.31084891735, 98999.576805446486, -28297.411680993864, 6321.8690900458769, -1014.4481797669425, 92.6902162786922, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 0.49971927304088776, -9.654637208680386, 97.505948340396344, -666.33601934981186, 3408.105484605092, -13763.432523580486, 45354.465643924545, -124664.55372082838, 290272.67349898565, -578970.23826825945, 997298.49853898259, -1492305.8426560569, 1947662.5724100152, -2222745.8235656363, 
                    2220699.0518272459, -1942007.3593680607, 1484341.2149855304, -988711.63628925406, 571288.01613185229, -284423.87105071626, 120846.85236806897, -43218.50747748607, 12743.787030520594, -2996.1172097664862, 527.08151258096314, -58.77729696437568, 1.0, 1.0
                 }
             };
            private static readonly double[,] matrix_30 = new double[,] { 
                { 
                    0.0, 0.033333333333333333, -0.42915901944679097, 3.2494535173243078, -17.7586056587267, 75.666496202200136, -261.84111639700274, 754.77950754535107, -1843.6954141121053, 3862.9331141765488, -7003.7203549558744, 11058.761857727259, -15276.823790612496, 18519.976990117808, -19737.852283049131, 18504.236515358563, 
                    -15251.745756567607, 11033.261626553469, -6984.4811733014267, 3852.0461952257306, -1839.49195913169, 754.23903304586122, -262.53200262446995, 76.370325615792467, -18.159959088528034, 3.4151164728320582, -0.48140052108508263, 0.045981323512156176, -0.0022988505747126436, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 0.0022988505747126436, -0.045981323512156176, 0.48140052108508263, -3.4151164728320582, 18.159959088528034, -76.370325615792467, 262.53200262446995, -754.23903304586122, 1839.49195913169, -3852.0461952257306, 6984.4811733014267, -11033.261626553469, 15251.745756567607, -18504.236515358563, 
                    19737.852283049131, -18519.976990117808, 15276.823790612496, -11058.761857727259, 7003.7203549558744, -3862.9331141765488, 1843.6954141121053, -754.77950754535107, 261.84111639700274, -75.666496202200136, 17.7586056587267, -3.2494535173243078, 0.42915901944679097, -0.033333333333333333, 0.0
                 }, { 
                    1.0, 1.0, -62.663734583094566, 584.23456139960331, -3454.5430087003629, 15304.727536484914, -54149.010429717862, 158248.14942095042, -390056.89152747369, 822328.65622870682, -1497488.5165127453, 2372070.3682903256, -3284602.891089825, 3989018.5145319453, -4257142.3555322774, 3995276.6831800863, 
                    -3295725.7076169015, 2385688.828596496, -1510993.6587980441, 833665.72536221473, -398229.63330527063, 163324.26201070577, -56859.966403793711, 16542.944228145574, -3934.1560971999024, 739.90911315975359, -104.30506196629575, 9.9631787743800064, -0.49812453668817031, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 98.726650894139212, -1122.497573282008, 7276.3925783969171, -33928.723404126285, 123854.98085890396, -369393.6629996202, 923159.0095690639, -1965206.7839724331, 3603849.1078174752, -5738135.0819983818, 7976365.1181967389, -9715477.4624424074, 10391945.492494455, -9769805.2136424389, 
                    8070190.0024238685, -5848066.643854023, 3707044.5838362919, -2046661.7204810518, 978171.9994604137, -401338.62474721973, 139767.50138685142, -40674.299273366, 9674.7740806045313, -1819.8208734827219, 256.56624805258633, -24.508788206626193, 1.2254059653905021, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -59.693364336683715, 969.20381183120571, -7454.0661489345921, 38300.016509745881, -148502.60835690965, 460848.07332129788, -1183551.3234725753, 2568679.6317170812, -4777080.7341664732, 7685661.2979489211, -10767470.046359167, 13193626.603433516, -14177314.247799711, 13376269.214418022, 
                    -11080178.525097776, 8046910.6530227354, -5109711.1025428558, 2824928.9594805609, -1351586.7498769136, 555017.42863691039, -193414.17808434332, 56314.934921735716, -13400.251053913918, 2521.3101565303887, -355.539138459667, 33.96807996988418, -1.6985036067830046, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 43.666585230253546, -780.27673730025344, 6842.9873077797756, -38673.014403594228, 160179.05145457364, -520611.43465616414, 1381953.8677359347, -3072337.7436196283, 5816558.7212446136, -9484355.50498821, 13423738.451998502, -16578092.374560187, 17923004.841258995, -16991030.205339056, 
                    14127190.914697716, -10290085.449725261, 6549349.1361958133, -3627515.8299144153, 1738109.70385823, -714556.37147891079, 249233.3913146751, -72617.564213108955, 17288.596449267054, -3254.1939593519764, 459.01524294623164, -43.862550048233288, 2.1935120689688814, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -34.523425355952334, 643.28875953216368, -6012.2833371139986, 36519.54810226769, -160774.0568328723, 547974.64430931711, -1508196.9530047334, 3446458.728945653, -6663148.6721779089, 11041234.457694763, -15823230.755944652, 19731970.074276775, -21495504.008961592, 20500039.731019925, 
                    -17125511.469218712, 12520816.454318704, -7992841.6631083181, 4437461.33934828, -2130154.7080597174, 877016.11502556549, -306249.80007357668, 89309.423393580379, -21276.965873268029, 4006.93934310219, -565.3980209121147, 54.041604421333567, -2.702960421669137, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 28.496350647896669, -543.03054278974037, 5257.2889290248168, -33409.403376331764, 154333.43008821423, -549349.43205867952, 1567709.0969554263, -3688517.7333509587, 7298400.7150247013, -12317647.069047859, 17910062.851718776, -22591827.399366152, 24835727.718298621, -23857317.98097882, 
                    20045092.859011278, -14722689.561193924, 9432811.7769627925, -5252137.17163091, 2527048.261769278, -1042318.1279693934, 364491.99926393339, -106411.80098052358, 25372.825024523136, -4781.2925830277927, 674.96874256447165, -64.53440339689682, 3.2283767437634125, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -24.174969460844395, 466.94799511902107, -4619.2018363644265, 30221.846862928207, -144496.46608033759, 533072.93684492726, -1572744.821311295, 3809056.9932756294, -7722010.3976174993, 13294904.179707708, -19646585.377686787, 25108660.110435992, -27895041.746455587, 27024297.147963107, 
                    -22861265.207901623, 16883203.866053045, -10864571.067346057, 6070566.1899768561, -2928993.4093485144, 1210778.2862846758, -424136.85591495334, 123991.85471506654, -29595.079972183583, 5581.2300392228562, -788.33129592652585, 75.401470597383693, -3.7728740503666245, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 20.89787701037481, -407.21607836099275, 4085.7383708393863, -27262.28305025617, 133581.38623475074, -506658.97363262804, 1538055.4845630594, -3826961.6655874345, 7947695.5344617991, -13970626.397636527, 21009145.866567228, -27242147.989295542, 30628089.396884013, -29962260.330758128, 
                    25548066.552164644, -18988807.088185497, 12282973.753252862, -6891716.4265649179, 3336276.9071611711, -1382792.0151459149, 485403.44230046443, -142132.69038323432, 33967.151723483548, -6411.6845104292634, 906.23572726520854, -86.718044857506825, 4.3403243822349156, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -18.309719721960477, 358.95070805253744, -3636.9365568802164, 24605.689359099015, -122730.48057162235, 475465.78661785432, -1477445.4338806993, 3764975.1948937, -7999673.0735921822, 14357354.080981603, -21988330.112235691, 28961092.84214906, -32992362.021358661, 32630964.040870745, 
                    -28076499.934911374, 21023393.641748976, -13681425.303789731, 7713976.8116234913, -3749042.7151588486, 1558753.7597828212, -548529.48763117346, 160927.73487431795, -38515.891481311555, 7278.38016789111, -1029.5591772594014, 98.57254326550445, -4.9352908122970094, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 16.201124624053463, -318.99773535871691, 3255.0362349054249, -22245.033713366585, 112434.29128799368, -442717.38949364435, 1401741.4329149495, -3645504.5727519644, 7908304.8957713973, -14480322.761611439, 22589373.949335173, -30245115.148425214, 34949499.390204847, -34988815.328740209, 
                    30413879.723387975, -22967452.458263006, 15051243.450653965, -8534813.5791228283, 4167152.7739572991, -1739014.751976324, 613762.422342521, -180479.75681425512, 43271.692236008035, -8187.9072938294712, 1159.3222796255784, -111.06852596809428, 5.5631381356215925, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -14.440147610678148, 285.24410363400318, -2925.9555698525737, 20147.768932620311, -102863.71215602373, 410180.38362263865, -1318407.975718864, 3487647.4396659811, -7705411.7512781033, 14373900.22594668, -22831605.011148687, 31086438.412696462, -36467391.294974014, 36993884.285909459, 
                    -32523678.199626856, 24797396.575297017, -16381044.388219487, 9350414.6529027317, -4590040.5757544646, 1923840.5550211221, -681351.98647499108, 200900.90051179094, -48268.97224194021, 9147.88869129727, -1296.7209467062212, 124.32830219713118, -6.2302310446108438, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 12.939242418208543, -256.22592015303712, 2638.8665250087547, -18276.640306358535, 94041.386135749068, -378747.44326354889, 1232210.3361058068, -3306017.607303292, 7420307.9147757478, -14077018.745544756, 22746399.506693929, -31491072.975429751, 37522700.632781096, -38605657.37677367, 
                    34365853.4026829, -26485022.179055948, 17656078.764992915, -10155260.480095621, 5016523.0264600515, -2113354.0461980039, 751539.85130581283, -222312.62431068762, 53546.82513182814, -10167.209675429669, 1443.1708315086476, -138.49800798786075, 6.9442141854122452, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -11.637892665087097, 230.89859095373487, -2385.4773214517027, 16596.909131319928, -85925.369562396023, 348815.76535933616, -1146030.3364335389, 3111047.4019721476, -7077488.73155863, 13628537.406098012, -22373598.811872046, 31478766.198574431, -38103380.133845724, 39787508.736364789, 
                    -35897795.665133238, 27997180.818814505, -18857517.393913642, 10941590.040357959, -5444545.912206511, 2307452.5607900973, -824542.78286408412, 244844.82732773895, -59149.753202740409, 11256.312204075291, -1600.3665218994383, 153.75449527429794, -7.7143957159595917, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 10.492840696656829, -208.49716718845102, 2159.4003986551538, -15078.412276712063, 78449.105897016183, -320510.79273906682, 1061519.3338998952, -2909844.0978632658, 6696100.3076603031, -13063706.401738739, 21756954.276077028, -31081246.447458897, 38210563.47978387, -40509740.643403955, 
                    37076071.379542105, -29295838.957353383, 19961740.361462191, -11698745.176474229, 5870837.3031727364, -2505685.5037465678, 900524.89773499325, -268633.26197144896, 65128.3990436611, -12427.562767866899, 1770.3600205043642, -170.314702751773, 8.5522755617358044, 0.0, 0.0
                 }, 
                { 
                    0.0, 0.0, -9.4722846420353211, 188.44857262800667, -1955.6654946497815, 13695.706044418606, -71540.121734139961, 293813.76732127863, -979552.274597292, 2707073.6314605144, -6290440.5919814343, 12412397.611129778, -20939753.263889205, 30338779.540659536, -37859084.267182343, 40752775.479820289, 
                    -37859084.267182343, 30338779.540659536, -20939753.263889205, 12412397.611129778, -6290440.5919814343, 2707073.6314605144, -979552.274597292, 293813.76732127863, -71540.121734139961, 13695.706044418606, -1955.6654946497815, 188.44857262800667, -9.4722846420353211, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 8.5522755617358044, -170.314702751773, 1770.3600205043642, -12427.562767866899, 65128.3990436611, -268633.26197144896, 900524.89773499325, -2505685.5037465678, 5870837.3031727364, -11698745.176474229, 19961740.361462191, -29295838.957353383, 37076071.379542105, -40509740.643403955, 
                    38210563.47978387, -31081246.447458897, 21756954.276077028, -13063706.401738739, 6696100.3076603031, -2909844.0978632658, 1061519.3338998952, -320510.79273906682, 78449.105897016183, -15078.412276712063, 2159.4003986551538, -208.49716718845102, 10.492840696656829, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -7.7143957159595917, 153.75449527429794, -1600.3665218994383, 11256.312204075291, -59149.753202740409, 244844.82732773895, -824542.78286408412, 2307452.5607900973, -5444545.912206511, 10941590.040357959, -18857517.393913642, 27997180.818814505, -35897795.665133238, 39787508.736364789, 
                    -38103380.133845724, 31478766.198574431, -22373598.811872046, 13628537.406098012, -7077488.73155863, 3111047.4019721476, -1146030.3364335389, 348815.76535933616, -85925.369562396023, 16596.909131319928, -2385.4773214517027, 230.89859095373487, -11.637892665087097, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 6.9442141854122452, -138.49800798786075, 1443.1708315086476, -10167.209675429669, 53546.82513182814, -222312.62431068762, 751539.85130581283, -2113354.0461980039, 5016523.0264600515, -10155260.480095621, 17656078.764992915, -26485022.179055948, 34365853.4026829, -38605657.37677367, 
                    37522700.632781096, -31491072.975429751, 22746399.506693929, -14077018.745544756, 7420307.9147757478, -3306017.607303292, 1232210.3361058068, -378747.44326354889, 94041.386135749068, -18276.640306358535, 2638.8665250087547, -256.22592015303712, 12.939242418208543, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -6.2302310446108438, 124.32830219713118, -1296.7209467062212, 9147.88869129727, -48268.97224194021, 200900.90051179094, -681351.98647499108, 1923840.5550211221, -4590040.5757544646, 9350414.6529027317, -16381044.388219487, 24797396.575297017, -32523678.199626856, 36993884.285909459, 
                    -36467391.294974014, 31086438.412696462, -22831605.011148687, 14373900.22594668, -7705411.7512781033, 3487647.4396659811, -1318407.975718864, 410180.38362263865, -102863.71215602373, 20147.768932620311, -2925.9555698525737, 285.24410363400318, -14.440147610678148, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 5.5631381356215925, -111.06852596809428, 1159.3222796255784, -8187.9072938294712, 43271.692236008035, -180479.75681425512, 613762.422342521, -1739014.751976324, 4167152.7739572991, -8534813.5791228283, 15051243.450653965, -22967452.458263006, 30413879.723387975, -34988815.328740209, 
                    34949499.390204847, -30245115.148425214, 22589373.949335173, -14480322.761611439, 7908304.8957713973, -3645504.5727519644, 1401741.4329149495, -442717.38949364435, 112434.29128799368, -22245.033713366585, 3255.0362349054249, -318.99773535871691, 16.201124624053463, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -4.9352908122970094, 98.57254326550445, -1029.5591772594014, 7278.38016789111, -38515.891481311555, 160927.73487431795, -548529.48763117346, 1558753.7597828212, -3749042.7151588486, 7713976.8116234913, -13681425.303789731, 21023393.641748976, -28076499.934911374, 32630964.040870745, 
                    -32992362.021358661, 28961092.84214906, -21988330.112235691, 14357354.080981603, -7999673.0735921822, 3764975.1948937, -1477445.4338806993, 475465.78661785432, -122730.48057162235, 24605.689359099015, -3636.9365568802164, 358.95070805253744, -18.309719721960477, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 4.3403243822349156, -86.718044857506825, 906.23572726520854, -6411.6845104292634, 33967.151723483548, -142132.69038323432, 485403.44230046443, -1382792.0151459149, 3336276.9071611711, -6891716.4265649179, 12282973.753252862, -18988807.088185497, 25548066.552164644, -29962260.330758128, 
                    30628089.396884013, -27242147.989295542, 21009145.866567228, -13970626.397636527, 7947695.5344617991, -3826961.6655874345, 1538055.4845630594, -506658.97363262804, 133581.38623475074, -27262.28305025617, 4085.7383708393863, -407.21607836099275, 20.89787701037481, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -3.7728740503666245, 75.401470597383693, -788.33129592652585, 5581.2300392228562, -29595.079972183583, 123991.85471506654, -424136.85591495334, 1210778.2862846758, -2928993.4093485144, 6070566.1899768561, -10864571.067346057, 16883203.866053045, -22861265.207901623, 27024297.147963107, 
                    -27895041.746455587, 25108660.110435992, -19646585.377686787, 13294904.179707708, -7722010.3976174993, 3809056.9932756294, -1572744.821311295, 533072.93684492726, -144496.46608033759, 30221.846862928207, -4619.2018363644265, 466.94799511902107, -24.174969460844395, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 3.2283767437634125, -64.53440339689682, 674.96874256447165, -4781.2925830277927, 25372.825024523136, -106411.80098052358, 364491.99926393339, -1042318.1279693934, 2527048.261769278, -5252137.17163091, 9432811.7769627925, -14722689.561193924, 20045092.859011278, -23857317.98097882, 
                    24835727.718298621, -22591827.399366152, 17910062.851718776, -12317647.069047859, 7298400.7150247013, -3688517.7333509587, 1567709.0969554263, -549349.43205867952, 154333.43008821423, -33409.403376331764, 5257.2889290248168, -543.03054278974037, 28.496350647896669, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -2.702960421669137, 54.041604421333567, -565.3980209121147, 4006.93934310219, -21276.965873268029, 89309.423393580379, -306249.80007357668, 877016.11502556549, -2130154.7080597174, 4437461.33934828, -7992841.6631083181, 12520816.454318704, -17125511.469218712, 20500039.731019925, 
                    -21495504.008961592, 19731970.074276775, -15823230.755944652, 11041234.457694763, -6663148.6721779089, 3446458.728945653, -1508196.9530047334, 547974.64430931711, -160774.0568328723, 36519.54810226769, -6012.2833371139986, 643.28875953216368, -34.523425355952334, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 2.1935120689688814, -43.862550048233288, 459.01524294623164, -3254.1939593519764, 17288.596449267054, -72617.564213108955, 249233.3913146751, -714556.37147891079, 1738109.70385823, -3627515.8299144153, 6549349.1361958133, -10290085.449725261, 14127190.914697716, -16991030.205339056, 
                    17923004.841258995, -16578092.374560187, 13423738.451998502, -9484355.50498821, 5816558.7212446136, -3072337.7436196283, 1381953.8677359347, -520611.43465616414, 160179.05145457364, -38673.014403594228, 6842.9873077797756, -780.27673730025344, 43.666585230253546, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -1.6985036067830046, 33.96807996988418, -355.539138459667, 2521.3101565303887, -13400.251053913918, 56314.934921735716, -193414.17808434332, 555017.42863691039, -1351586.7498769136, 2824928.9594805609, -5109711.1025428558, 8046910.6530227354, -11080178.525097776, 13376269.214418022, 
                    -14177314.247799711, 13193626.603433516, -10767470.046359167, 7685661.2979489211, -4777080.7341664732, 2568679.6317170812, -1183551.3234725753, 460848.07332129788, -148502.60835690965, 38300.016509745881, -7454.0661489345921, 969.20381183120571, -59.693364336683715, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 1.2254059653905021, -24.508788206626193, 256.56624805258633, -1819.8208734827219, 9674.7740806045313, -40674.299273366, 139767.50138685142, -401338.62474721973, 978171.9994604137, -2046661.7204810518, 3707044.5838362919, -5848066.643854023, 8070190.0024238685, -9769805.2136424389, 
                    10391945.492494455, -9715477.4624424074, 7976365.1181967389, -5738135.0819983818, 3603849.1078174752, -1965206.7839724331, 923159.0095690639, -369393.6629996202, 123854.98085890396, -33928.723404126285, 7276.3925783969171, -1122.497573282008, 98.726650894139212, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -0.49812453668817031, 9.9631787743800064, -104.30506196629575, 739.90911315975359, -3934.1560971999024, 16542.944228145574, -56859.966403793711, 163324.26201070577, -398229.63330527063, 833665.72536221473, -1510993.6587980441, 2385688.828596496, -3295725.7076169015, 3995276.6831800863, 
                    -4257142.3555322774, 3989018.5145319453, -3284602.891089825, 2372070.3682903256, -1497488.5165127453, 822328.65622870682, -390056.89152747369, 158248.14942095042, -54149.010429717862, 15304.727536484914, -3454.5430087003629, 584.23456139960331, -62.663734583094566, 1.0, 1.0
                 }
             };
            private static readonly double[,] matrix_31 = new double[,] { 
                { 
                    0.0, 0.032258064516129031, -0.42996715563112226, 3.3747785372761907, -19.145138619440662, 84.802502358000368, -305.55530438458158, 918.68964458559549, -2345.0442750082261, 5144.995652238099, -9790.0157860785966, 16264.126408057389, -23704.402292304407, 30412.390434862205, -34421.805734545072, 34406.369422269025, 
                    -30372.18153048095, 23654.081449337271, -16218.801568418805, 9758.4758448344328, -5128.1035069935506, 2338.6343873809542, -917.62602065539284, 306.22988152853185, -85.555485227682837, 19.569808236461622, -3.5453960406371596, 0.48211121961088438, -0.044479360927357478, 0.0021505376344086021, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -0.0021505376344086021, 0.044479360927357478, -0.48211121961088438, 3.5453960406371596, -19.569808236461622, 85.555485227682837, -306.22988152853185, 917.62602065539284, -2338.6343873809542, 5128.1035069935506, -9758.4758448344328, 16218.801568418805, -23654.081449337271, 30372.18153048095, 
                    -34406.369422269025, 34421.805734545072, -30412.390434862205, 23704.402292304407, -16264.126408057389, 9790.0157860785966, -5144.995652238099, 2345.0442750082261, -918.68964458559549, 305.55530438458158, -84.802502358000368, 19.145138619440662, -3.3747785372761907, 0.42996715563112226, -0.032258064516129031, 0.0
                 }, { 
                    1.0, 1.0, -66.671488669400688, 645.33759109876269, -3963.3438547789292, 18259.769776542485, -67282.296469095221, 205122.20978358813, -528403.83809363423, 1166620.1335322056, -2229804.9306211686, 3716444.260777256, -5429720.8024780219, 6979020.4863254381, -7910196.4374924293, 7915253.3607401419, 
                    -6993120.2129897038, 5449934.2272122269, -3738807.6665523066, 2250494.0966544617, -1183032.1863400415, 539654.79192769236, -211791.88582219247, 70690.595707795044, -19752.25310962605, 4518.5301614234695, -818.66578820231859, 111.32986886770286, -10.271606619872701, 0.49663382229778891, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 104.95147374477594, -1237.8926526245382, 8334.4286222614464, -40418.087729659506, 153681.95274485496, -478212.69775454991, 1249180.5897566748, -2785160.1222274411, 5361292.1179772634, -8982703.4316409118, 13175539.10225687, -16985963.128836449, 19296978.471555769, -19344178.619083866, 
                    17114762.942336611, -13352904.446915848, 9168563.3859938513, -5522688.1426496143, 2904764.5103050927, -1325629.1144690339, 520435.19099811849, -173755.16654719756, 48560.702321705794, -11110.56278432665, 2013.2526220109949, -273.80502981934751, 25.26353169752732, -1.2215404526562663, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -63.477732087580613, 1066.6311381349158, -8515.9224447877368, 45506.143721112581, -183805.24083496229, 595219.947847553, -1598093.59708088, 3633242.6141343829, -7093804.448674567, 12011485.822453676, -17758892.462580897, 23034735.250613302, -26292301.164985992, 26453688.914802227, 
                    -23472610.498663861, 18355118.436179936, -12626073.484451614, 7616256.1643520826, -4010492.8628516425, 1831901.5686315075, -719709.961978595, 240421.06191902849, -67221.66353745971, 15385.278391276586, -2788.5344999479853, 379.3127824922085, -35.002856445470563, 1.6925827816663381, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 46.455246064113815, -858.64981039492272, 7806.3187650377458, -45849.761889294059, 197772.85814490847, -670725.21201086335, 1861410.3193300597, -4335437.3649262823, 8618251.9328092821, -14791785.431492014, 22097032.377491165, -28891558.736110505, 33183262.169235189, -33550384.429235522, 
                    29884533.379747402, -23440621.443555132, 16163488.618073564, -9768955.96161751, 5151964.3073773449, -2356178.9380636727, 926581.73457366147, -309762.44617232756, 86660.680936357661, -19843.333288225927, 3597.7604930865537, -489.50805969414779, 45.179165342254848, -2.1848865233859938, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -36.748695471405441, 708.15063798064455, -6856.7636371750159, 43242.720237455258, -198125.28220725394, 704350.21203009144, -2026416.4820329512, 4851109.5275944946, -9848116.2738824468, 17178600.957896415, -25987292.563953031, 34313569.948957592, -39716474.950744063, 40402018.752398804, 
                    -36162568.619993038, 28474774.576752704, -19695440.396006253, 11932964.803309923, -6305601.4246915029, 2888292.8012472191, -1137247.1287833147, 380560.03424732958, -106548.14720976386, 24411.29181633157, -4427.8944420021462, 602.64466690247514, -55.633028830894141, 2.6907930214597817, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 30.353999009923839, -598.13070767617, 5997.2139724044218, -39545.181143580638, 189968.34442092245, -704850.58646309783, 2101702.6247427305, -5179035.7564888531, 10759259.670487821, -19114849.927972227, 29339610.552443933, -39189834.203330658, 45779610.432929836, -46913028.327157922, 
                    42237814.598256849, -33415458.845318872, 23200256.193737671, -14099059.430703845, 7468279.374961569, -3427479.0355079728, 1351620.7743586863, -452843.50845397433, 126905.32298206471, -29096.332736744913, 5280.55532805659, -718.97435377773525, 66.389673816855648, -3.2115813707240757, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -25.77223151721109, 514.7218911786498, -5272.3180197933534, 35778.355857430375, -177783.36987002674, 683196.91381983971, -2104800.1371258479, 5336616.62055828, -11355586.501302376, 20577004.036523614, -32097406.2675262, 43438382.528380223, -51283055.6441303, 53004870.093596861, 
                    -48054061.3971323, 38229975.808903635, -26662961.094334487, 16262370.166898081, -8639372.55544611, 3974213.2285103831, -1570140.7917680631, 526828.25686831982, -147807.9668798935, 33918.589114554074, -6159.778376222338, 839.08447109982365, -77.505741404131726, 3.7500630357503857, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 22.300485047160841, -449.30161628620164, 4667.2349419530137, -32292.504233522759, 164373.03856390054, -649036.309225316, 2056054.1334240027, -5352473.8419618094, 11661889.815645089, -21568417.683400333, 34230073.464624144, -46996237.950497366, 56147077.497468993, -58601646.204752043, 
                    53554119.145208359, -42883979.5026273, 30067598.480640255, -18417790.531306777, 9818385.4770319778, -4529156.1819331059, 1793365.9205615397, -602784.41972393019, 169350.62264883911, -38903.148430738343, 7070.59032917089, -963.70676861986317, 89.051811244204089, -4.3097418731555868, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -19.561120665632153, 396.49385069836472, -4158.8834269881172, 29170.785509866408, -151103.00233240181, 609124.24602826475, -1974014.6231172069, 5259697.3553854087, -11717631.006068245, 22115890.413100962, -35732384.018636495, 49819842.623060629, -60301327.276276708, 63627999.735314839, 
                    -58676718.099612735, 47338434.511226378, -33394953.346978653, 20558607.56672246, -11004274.088503072, 5092873.8037890727, -2021878.8729112684, 681009.06400041748, -191639.98075367016, 44078.757641301236, -8018.8077401275532, 1093.6933847775913, -101.11040933850454, 4.8947245419808763, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 17.331647254285372, -352.82893334482304, 3726.8870546539438, -26402.129684203283, 138551.7845142213, -567478.043019366, 1872952.5626364741, -5089951.2880438073, 11569848.035729259, -22264892.688281711, 36623757.977519169, -51887172.848887295, 63687230.512623489, -68010026.88129881, 
                    63355825.155644469, -51548197.914251342, 36621245.432922028, -22675665.934235707, 12195040.309229547, -5665684.8769314913, 2256242.7955694413, -761817.32770566118, 214793.90288434236, -49477.900766429542, 9011.09586098044, -1230.0295538069081, 113.77748861305533, -5.5098014209853572, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -15.471865764085772, 315.98347282788808, -3355.1426708747817, 23946.657053233339, -126914.88244713689, 526275.63179301075, -1762579.1894068161, 4869645.8136604661, -11266145.135261649, 22073037.316446159, -36945744.352527075, 53199470.484857231, -66261378.215367429, 71677647.217781633, 
                    -67521083.065534309, 55461174.919616848, -39717016.599848658, 24756569.026746552, -13387329.171556203, 6247512.1068649795, -2496962.4889688841, 845536.79396235954, -238941.85103975915, 55137.307569143166, -10055.122908208987, 1373.861157383544, -127.16540466196348, 6.1606036640557509, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 13.888764533651893, -284.34846298938578, 3031.291242659338, -21759.61019691794, 116211.62373580133, -486602.53116180934, 1649029.0761929969, -4618685.7505531842, 10849262.949461821, -21602638.083498005, 36757248.195830725, -53781512.415868007, 67999079.111773089, -74568054.238483191, 
                    71099233.167874575, -59017928.208462663, 42646064.686503485, -26784784.224403135, 14575942.345058035, -6837694.0535709392, 2744434.1154677537, -932500.15875316365, 264225.53521714761, -61098.669553083622, 11159.777163861994, -1526.5335995436294, 141.407123859604, -6.8538256139954949, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -12.518061156207647, 256.77618263802287, -2745.880847189288, 19799.566276132526, -106385.53967363691, 448929.68339682271, -1535973.4286573618, 4351115.9312827336, -10354264.799782272, 20913874.107350521, -36127854.522724286, 53679649.125917248, -68897252.241286188, 76629972.727512687, 
                    -74016635.099439144, 62151888.823362261, -45364459.574024148, 28738612.612075951, -15753220.163150156, 7434729.6050049653, -2998872.14125375, 1023033.0194056029, -290799.16094121477, 67409.491758548378, -12335.4461304444, 1689.6433016382575, -156.66188090779775, 7.5975261114278805, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 11.313818168277646, -232.42650498995732, 2491.6377468369487, -18030.706693774606, 97352.038018575535, -413393.63791805384, 1425488.8956298328, -4076408.1305965078, 9808180.8068825155, -20060064.606998097, 35130442.415219642, -52957313.361548334, 68975711.159121364, -77828232.244529635, 
                    76202990.829615176, -64790443.994174615, 47819789.381209664, -30590033.120785464, 16908254.547534566, -8035925.24506402, 3260200.5237081624, -1117433.0668095741, 318828.60578137467, -74124.031064265946, 13594.367657623839, -1865.1053875235602, 173.12277151053408, -8.4015370952453274, 0.0, 0.0
                 }, 
                { 
                    0.0, 0.0, -10.242276138132205, 210.67033933356748, -2262.9074819493653, 16422.901353563131, -89020.532742101874, 379955.30613702734, -1318653.823251765, 3800717.726997281, -9230971.0028151982, 19085612.381495073, -33834796.254555508, 51688530.466365747, -68275966.555979043, 78147809.814522788, 
                    -77596190.897447318, 66857237.183606192, -49950901.28628318, 32303491.068538386, -18025904.86171978, 8636909.3089792877, -3527890.2520986642, 1215935.1851086675, -348488.48258358618, 81304.238604667567, -14951.067784018889, 2055.2431573533186, -191.02705346021756, 9.27802501897556, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 9.27802501897556, -191.02705346021756, 2055.2431573533186, -14951.067784018889, 81304.238604667567, -348488.48258358618, 1215935.1851086675, -3527890.2520986642, 8636909.3089792877, -18025904.86171978, 32303491.068538386, -49950901.28628318, 66857237.183606192, -77596190.897447318, 
                    78147809.814522788, -68275966.555979043, 51688530.466365747, -33834796.254555508, 19085612.381495073, -9230971.0028151982, 3800717.726997281, -1318653.823251765, 379955.30613702734, -89020.532742101874, 16422.901353563131, -2262.9074819493653, 210.67033933356748, -10.242276138132205, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -8.4015370952453274, 173.12277151053408, -1865.1053875235602, 13594.367657623839, -74124.031064265946, 318828.60578137467, -1117433.0668095741, 3260200.5237081624, -8035925.24506402, 16908254.547534566, -30590033.120785464, 47819789.381209664, -64790443.994174615, 76202990.829615176, 
                    -77828232.244529635, 68975711.159121364, -52957313.361548334, 35130442.415219642, -20060064.606998097, 9808180.8068825155, -4076408.1305965078, 1425488.8956298328, -413393.63791805384, 97352.038018575535, -18030.706693774606, 2491.6377468369487, -232.42650498995732, 11.313818168277646, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 7.5975261114278805, -156.66188090779775, 1689.6433016382575, -12335.4461304444, 67409.491758548378, -290799.16094121477, 1023033.0194056029, -2998872.14125375, 7434729.6050049653, -15753220.163150156, 28738612.612075951, -45364459.574024148, 62151888.823362261, -74016635.099439144, 
                    76629972.727512687, -68897252.241286188, 53679649.125917248, -36127854.522724286, 20913874.107350521, -10354264.799782272, 4351115.9312827336, -1535973.4286573618, 448929.68339682271, -106385.53967363691, 19799.566276132526, -2745.880847189288, 256.77618263802287, -12.518061156207647, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -6.8538256139954949, 141.407123859604, -1526.5335995436294, 11159.777163861994, -61098.669553083622, 264225.53521714761, -932500.15875316365, 2744434.1154677537, -6837694.0535709392, 14575942.345058035, -26784784.224403135, 42646064.686503485, -59017928.208462663, 71099233.167874575, 
                    -74568054.238483191, 67999079.111773089, -53781512.415868007, 36757248.195830725, -21602638.083498005, 10849262.949461821, -4618685.7505531842, 1649029.0761929969, -486602.53116180934, 116211.62373580133, -21759.61019691794, 3031.291242659338, -284.34846298938578, 13.888764533651893, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 6.1606036640557509, -127.16540466196348, 1373.861157383544, -10055.122908208987, 55137.307569143166, -238941.85103975915, 845536.79396235954, -2496962.4889688841, 6247512.1068649795, -13387329.171556203, 24756569.026746552, -39717016.599848658, 55461174.919616848, -67521083.065534309, 
                    71677647.217781633, -66261378.215367429, 53199470.484857231, -36945744.352527075, 22073037.316446159, -11266145.135261649, 4869645.8136604661, -1762579.1894068161, 526275.63179301075, -126914.88244713689, 23946.657053233339, -3355.1426708747817, 315.98347282788808, -15.471865764085772, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -5.5098014209853572, 113.77748861305533, -1230.0295538069081, 9011.09586098044, -49477.900766429542, 214793.90288434236, -761817.32770566118, 2256242.7955694413, -5665684.8769314913, 12195040.309229547, -22675665.934235707, 36621245.432922028, -51548197.914251342, 63355825.155644469, 
                    -68010026.88129881, 63687230.512623489, -51887172.848887295, 36623757.977519169, -22264892.688281711, 11569848.035729259, -5089951.2880438073, 1872952.5626364741, -567478.043019366, 138551.7845142213, -26402.129684203283, 3726.8870546539438, -352.82893334482304, 17.331647254285372, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 4.8947245419808763, -101.11040933850454, 1093.6933847775913, -8018.8077401275532, 44078.757641301236, -191639.98075367016, 681009.06400041748, -2021878.8729112684, 5092873.8037890727, -11004274.088503072, 20558607.56672246, -33394953.346978653, 47338434.511226378, -58676718.099612735, 
                    63627999.735314839, -60301327.276276708, 49819842.623060629, -35732384.018636495, 22115890.413100962, -11717631.006068245, 5259697.3553854087, -1974014.6231172069, 609124.24602826475, -151103.00233240181, 29170.785509866408, -4158.8834269881172, 396.49385069836472, -19.561120665632153, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -4.3097418731555868, 89.051811244204089, -963.70676861986317, 7070.59032917089, -38903.148430738343, 169350.62264883911, -602784.41972393019, 1793365.9205615397, -4529156.1819331059, 9818385.4770319778, -18417790.531306777, 30067598.480640255, -42883979.5026273, 53554119.145208359, 
                    -58601646.204752043, 56147077.497468993, -46996237.950497366, 34230073.464624144, -21568417.683400333, 11661889.815645089, -5352473.8419618094, 2056054.1334240027, -649036.309225316, 164373.03856390054, -32292.504233522759, 4667.2349419530137, -449.30161628620164, 22.300485047160841, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 3.7500630357503857, -77.505741404131726, 839.08447109982365, -6159.778376222338, 33918.589114554074, -147807.9668798935, 526828.25686831982, -1570140.7917680631, 3974213.2285103831, -8639372.55544611, 16262370.166898081, -26662961.094334487, 38229975.808903635, -48054061.3971323, 
                    53004870.093596861, -51283055.6441303, 43438382.528380223, -32097406.2675262, 20577004.036523614, -11355586.501302376, 5336616.62055828, -2104800.1371258479, 683196.91381983971, -177783.36987002674, 35778.355857430375, -5272.3180197933534, 514.7218911786498, -25.77223151721109, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -3.2115813707240757, 66.389673816855648, -718.97435377773525, 5280.55532805659, -29096.332736744913, 126905.32298206471, -452843.50845397433, 1351620.7743586863, -3427479.0355079728, 7468279.374961569, -14099059.430703845, 23200256.193737671, -33415458.845318872, 42237814.598256849, 
                    -46913028.327157922, 45779610.432929836, -39189834.203330658, 29339610.552443933, -19114849.927972227, 10759259.670487821, -5179035.7564888531, 2101702.6247427305, -704850.58646309783, 189968.34442092245, -39545.181143580638, 5997.2139724044218, -598.13070767617, 30.353999009923839, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 2.6907930214597817, -55.633028830894141, 602.64466690247514, -4427.8944420021462, 24411.29181633157, -106548.14720976386, 380560.03424732958, -1137247.1287833147, 2888292.8012472191, -6305601.4246915029, 11932964.803309923, -19695440.396006253, 28474774.576752704, -36162568.619993038, 
                    40402018.752398804, -39716474.950744063, 34313569.948957592, -25987292.563953031, 17178600.957896415, -9848116.2738824468, 4851109.5275944946, -2026416.4820329512, 704350.21203009144, -198125.28220725394, 43242.720237455258, -6856.7636371750159, 708.15063798064455, -36.748695471405441, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -2.1848865233859938, 45.179165342254848, -489.50805969414779, 3597.7604930865537, -19843.333288225927, 86660.680936357661, -309762.44617232756, 926581.73457366147, -2356178.9380636727, 5151964.3073773449, -9768955.96161751, 16163488.618073564, -23440621.443555132, 29884533.379747402, 
                    -33550384.429235522, 33183262.169235189, -28891558.736110505, 22097032.377491165, -14791785.431492014, 8618251.9328092821, -4335437.3649262823, 1861410.3193300597, -670725.21201086335, 197772.85814490847, -45849.761889294059, 7806.3187650377458, -858.64981039492272, 46.455246064113815, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 1.6925827816663381, -35.002856445470563, 379.3127824922085, -2788.5344999479853, 15385.278391276586, -67221.66353745971, 240421.06191902849, -719709.961978595, 1831901.5686315075, -4010492.8628516425, 7616256.1643520826, -12626073.484451614, 18355118.436179936, -23472610.498663861, 
                    26453688.914802227, -26292301.164985992, 23034735.250613302, -17758892.462580897, 12011485.822453676, -7093804.448674567, 3633242.6141343829, -1598093.59708088, 595219.947847553, -183805.24083496229, 45506.143721112581, -8515.9224447877368, 1066.6311381349158, -63.477732087580613, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -1.2215404526562663, 25.26353169752732, -273.80502981934751, 2013.2526220109949, -11110.56278432665, 48560.702321705794, -173755.16654719756, 520435.19099811849, -1325629.1144690339, 2904764.5103050927, -5522688.1426496143, 9168563.3859938513, -13352904.446915848, 17114762.942336611, 
                    -19344178.619083866, 19296978.471555769, -16985963.128836449, 13175539.10225687, -8982703.4316409118, 5361292.1179772634, -2785160.1222274411, 1249180.5897566748, -478212.69775454991, 153681.95274485496, -40418.087729659506, 8334.4286222614464, -1237.8926526245382, 104.95147374477594, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 0.49663382229778891, -10.271606619872701, 111.32986886770286, -818.66578820231859, 4518.5301614234695, -19752.25310962605, 70690.595707795044, -211791.88582219247, 539654.79192769236, -1183032.1863400415, 2250494.0966544617, -3738807.6665523066, 5449934.2272122269, -6993120.2129897038, 
                    7915253.3607401419, -7910196.4374924293, 6979020.4863254381, -5429720.8024780219, 3716444.260777256, -2229804.9306211686, 1166620.1335322056, -528403.83809363423, 205122.20978358813, -67282.296469095221, 18259.769776542485, -3963.3438547789292, 645.33759109876269, -66.671488669400688, 1.0, 1.0
                 }
             };
            private static readonly double[,] matrix_32 = new double[,] { 
                { 
                    0.0, 0.03125, -0.43072114104890807, 3.50007765614789, -20.583400647190981, 94.642741706397544, -354.50918334077932, 1109.8288003005464, -2954.8594221163062, 6774.6260566235687, -13498.66126672278, 23535.617382360811, -36090.597495952396, 48852.604711276064, -58517.4301296726, 62117.5342338115, 
                    -58463.561631822588, 48764.5251080605, -35996.65610304552, 23458.88837236906, -13448.924218491893, 6749.33063336139, -2945.4895898363343, 1108.0722832936149, -355.14521609617486, 95.4447801302098, -21.031720379199456, 3.675607258426961, -0.48276933188246757, 0.043072114104890806, -0.0020161290322580645, 0.0, 
                    0.0
                 }, { 
                    0.0, 0.0, 0.0020161290322580645, -0.043072114104890806, 0.48276933188246757, -3.675607258426961, 21.031720379199456, -95.4447801302098, 355.14521609617486, -1108.0722832936149, 2945.4895898363343, -6749.33063336139, 13448.924218491893, -23458.88837236906, 35996.65610304552, -48764.5251080605, 
                    58463.561631822588, -62117.5342338115, 58517.4301296726, -48852.604711276064, 36090.597495952396, -23535.617382360811, 13498.66126672278, -6774.6260566235687, 2954.8594221163062, -1109.8288003005464, 354.50918334077932, -94.642741706397544, 20.583400647190981, -3.50007765614789, 0.43072114104890807, -0.03125, 
                    0.0
                 }, { 
                    1.0, 1.0, -70.800567819936944, 710.52132144523807, -4526.0595506130512, 21652.532740660426, -82958.594189230847, 263381.8426237625, -707759.16130218632, 1633056.5468816217, -3268708.6823544325, 5718076.0458312277, -8790053.00042688, 11920624.262090228, -14299545.084313307, 15196289.832516778, 
                    -14314957.968604734, 11948407.891075697, -8824849.8107638843, 5753645.2542467285, -3299705.8633233043, 1656417.2137646589, -723041.47630947409, 272051.13208890735, -87206.506732078633, 23439.17254191445, -5165.366324096779, 902.7814526494634, -118.58040055493349, 10.579932825238359, -0.49523727008116614, 0.0, 
                    0.0
                 }, { 
                    0.0, 0.0, 111.36470065949621, -1360.8731321863129, 9503.157356021522, -47859.527813268658, 189242.5109373311, -613315.33219479362, 1671412.8057286434, -3894976.2597132488, 7852365.6484552165, -13809734.559270095, 21314182.939722285, -28993987.955398969, 34862743.881596059, -37117771.746179491, 
                    35016136.991270684, -29261078.286464926, 21631529.228465378, -14113735.362409372, 8098957.4796942864, -4067498.8150485298, 1776165.6619429556, -668497.67977040634, 214338.44954060277, -57619.9584043329, 12699.682262895616, -2219.8325198323314, 291.59745595588049, -26.018152985497245, 1.2179257998191046, 0.0, 
                    0.0
                 }, { 
                    0.0, 0.0, -67.376558689547039, 1170.3344309548697, -9686.6866952133387, 53752.380339333817, -225806.08854260363, 761710.43316685443, -2133943.2071678732, 5071561.9451884488, -10372155.43085124, 18437181.908754677, -28687397.288539615, 39266877.598928951, -47442982.764444143, 50702640.828836322, 
                    -47974377.348001674, 40184349.60252019, -29762610.300542638, 19448170.303711474, -11173476.940678947, 5617000.9077257281, -2454674.890576358, 924432.834147331, -296540.88455517194, 79748.149463809881, -17581.905422139185, 3073.884014681782, -403.84924199597242, 36.0378363896997, -1.6870647389248417, 0.0, 
                    0.0
                 }, { 
                    0.0, 0.0, 49.328165429112872, -942.05766704231746, 8867.38291396704, -54048.934888204654, 242411.312439228, -856327.08028914046, 2479845.1034083338, -6038424.2167017451, 12574893.006975455, -22660500.406681437, 35630046.507139586, -49167232.138233975, 59782768.342638619, -64210242.409922615, 
                    60996187.2196405, -51253062.510530055, 38056468.444110587, -24917988.422812354, 14339216.13606317, -7217795.4276359528, 3157503.985685918, -1190098.9640635177, 382010.05492817512, -102785.41395707433, 22669.736702591239, -3964.56432716742, 520.9783533788908, -46.496722636334205, 2.1768785366519308, 0.0, 
                    0.0
                 }, { 
                    0.0, 0.0, -39.041109113858511, 777.18207861708277, -7786.6589352833907, 50916.587200320057, -242409.13010980852, 897329.41100023489, -2693443.7568843081, 6740776.94218054, -14336068.117813712, 26257963.087492332, -41812970.491553515, 58275614.964858435, -71415849.7419322, 77184092.26148507, 
                    -73685716.451159626, 62162283.921619207, -46304268.865378976, 30396164.559309285, -17527740.651195783, 8837374.7862485237, -3871126.7989888792, 1460610.1420229792, -469232.21541099035, 126336.06062418707, -27877.889715410729, 4877.220720692143, -641.08441328536787, 57.226743793928165, -2.6795429036763232, 0.0, 
                    0.0
                 }, { 
                    0.0, 0.0, 32.267596580551363, -656.78050985031109, 6812.00244444961, -46546.180921939806, 232179.32985603643, -896476.51798135124, 2787774.2833171254, -7180014.4194837082, 15625013.428038068, -29147122.45513808, 47094569.81003131, -66403599.17286846, 82136027.12249361, -89433853.711064488, 
                    85893062.147710159, -72810919.505185738, 54447765.880256832, -35854277.214679152, 20727516.382220577, -10471988.969670195, 4594653.1676975628, -1735863.4394839669, 558234.81986413139, -150420.44480385908, 33213.086406697221, -5813.3253325533115, 764.38935564333588, -68.249455488396222, 3.1961174285965135, 0.0, 
                    0.0
                 }, { 
                    0.0, 0.0, -27.41748654037545, 565.58293098504055, -5991.6592028098776, 42118.437269824572, -217197.79200154994, 868030.42038303439, -2787424.9133211016, 7383608.1101669716, -16453396.67929562, 31300243.1445671, -51392389.7523361, 73418235.853605628, -91783998.197837159, 100806465.41534492, 
                    -97497205.351181835, 83119915.112229839, -62444916.877816245, 41274969.7191998, -23933803.194632333, 12121554.147801474, -5328904.6350136213, 2016438.8193487984, -649275.19428224943, 175123.20048133531, -38696.661913726057, 6776.9656566393514, -891.464655412596, 79.617974176048563, -3.7291499069005756, 0.0, 
                    0.0
                 }, { 
                    0.0, 0.0, 23.745109319384195, -494.11703353203666, 5307.8992946302933, -38033.440598808913, 200832.28394121127, -824258.403098117, 2720037.9455866013, -7393855.8501159484, 16863307.550604973, -32732520.422193065, 54669943.819449782, -79224837.753076717, 100224727.6018144, -111159476.80455665, 
                    108378902.03266878, -93008580.54336144, 70252042.662554964, -46640299.756983846, 27142043.141500272, -13786392.05609243, 6075021.21139369, -2303086.4525823523, 742679.662097814, -200550.82336197956, 44355.369227154966, -7773.2587030764289, 1023.0251457633469, -91.398714175794908, 4.2818253517941125, 0.0, 
                    0.0
                 }, { 
                    0.0, 0.0, -20.849863856338029, 436.4829343638628, -4734.177669837145, 34383.014649739242, -184706.57112688979, 773610.44356569124, -2610241.7295808238, 7257995.1660075653, -16916870.395108085, 33494936.6905294, -56934030.271228865, 83766817.315762162, -107346689.61990614, 120357848.15216714, 
                    -118415342.46064724, 102388696.74165693, -77818883.624400824, 51928399.253468871, -30345991.796005607, 15466351.942605646, -6834112.6379378121, 2596609.5172832366, -838810.32961533067, 226823.79956422688, -50219.842914171633, 8808.1105141370117, -1159.900237681624, 103.66899694686015, -4.8578592721923517, 0.0, 
                    0.0
                 }, { 
                    0.0, 0.0, 18.495741534938279, -388.87509670978022, 4247.2249710524093, -31151.149469645556, 169499.43819764096, -721057.04723449936, 2476659.4572455068, -7020096.6573424144, 16685158.465858568, -33665175.1057288, 58231347.761269391, -87027630.659240723, 113065693.59968773, -128276284.39252044, 
                    127479823.05628188, -111162384.3688271, 85086127.173606992, -57111605.4484434, 33536654.684336036, -17160335.440890066, 7607090.68499008, -2897818.3288421403, 938055.94418603566, -254075.5306675754, 56324.626128378819, -9888.25858556358, 1303.0432977036153, -116.51812905301091, 5.4615552142884374, 0.0, 
                    0.0
                 }, { 
                    0.0, 0.0, -16.534036059824967, 348.746252330969, -3828.7071232713997, 28289.430852083769, -155433.21755419878, 669265.56933993055, -2331813.2192305229, 6716224.9432243183, -16238006.991736708, 33336452.530153446, -58642152.366057612, 89031377.510014474, -117329709.41531475, 134803889.34555179, 
                    -135443663.81701553, 119221424.12573238, -91983619.325100243, 62154835.602446079, -36701297.749790281, 18865850.638579749, -8394519.24583436, 3207495.1409316137, -1040827.7780740529, 282453.059542989, -62708.686969716087, 11021.413675396256, -1453.5555234287986, 130.04986692466912, -6.097929985661712, 0.0, 
                    0.0
                 }, { 
                    0.0, 0.0, 14.86610228320882, -314.33294656847727, 3464.5770659447003, -25744.454611048055, 142521.97597286425, -619547.6016869935, 2183535.4092947743, -6373192.3764954722, 15636742.677362647, -32605906.832569908, 58270775.379511006, -89840533.750926957, 120123093.99187873, -139850148.53360575, 
                    142179883.99357173, -126447710.32779108, 98428948.9059584, -67013919.822467551, 39822326.298333623, -20578479.626514021, 9196430.3493411858, -3526350.8527939594, 1147555.0315920932, -312118.28825453261, 69416.174565119378, -12216.463525668834, 1612.7200587075645, -144.38593237912426, 6.7728921493692686, 0.0, 
                    0.0
                 }, { 
                    0.0, 0.0, -13.4237748426992, 284.37769306188653, -3144.1045258328527, 23467.127069874146, -130691.27025902751, 572457.52965296106, -2036466.1991788787, 6009716.2160151685, -14930867.976962052, 31564811.130508177, -57234095.990329042, 89550076.803917617, -121468868.97540864, 143351463.309612, 
                    -147568567.71621352, 132715023.3334595, -104326500.35556743, 71633853.9937883, -42875948.717071667, 22291195.719971184, -10012074.476610214, 3854962.2972566546, -1258676.7636152515, 343249.12234413449, -76497.3468014726, 13483.735826501335, -1782.0470096903282, 159.67070539064329, -7.4934817459120833, 0.0, 
                    0.0
                 }, { 
                    0.0, 0.0, 12.158352077015341, -257.96033795995339, 2859.0396618355458, -21415.154488102391, 119834.70285311405, -528140.88455162093, 1893196.8566770691, -5638299.35651761, 14158072.857389838, -30292422.549309622, 55650068.612198785, -88278199.237148613, 121427796.44227205, -145277180.80040246, 
                    151503759.30114231, -137892461.44657797, 109567275.53122792, -75947061.172809333, 45830588.033207394, -23993475.491647057, 10839573.037704829, -4193677.9917907864, 1374627.23055301, -376040.0348489217, 84009.641576454931, -14835.329562811608, 1963.3321335686571, -176.07744740181991, 8.2681909975592269, 0.0, 
                    0.0
                 }, 
                { 
                    0.0, 0.0, -11.034054069449509, 234.39214866726843, -2602.9745400971988, 19553.049027746551, -109839.98337826184, 486530.58169959311, -1755045.2679453951, 5266987.7124859132, -13345903.375291584, 28853745.150192279, -53628658.89815139, 86155016.5178158, -120093467.85872014, 145633705.91873479, 
                    -153901352.55159307, 141849847.93612796, -114029944.13664611, 79871877.927710846, -48645049.435056917, 25670151.422398254, -11675433.206550142, 4542475.9090421814, -1495810.1927623719, 410701.3136332407, -92018.853681834982, 16285.529893098381, -2158.7336783924989, 193.8166247981589, -9.1073978559809685, 0.0, 
                    0.0
                 }, { 
                    0.0, 0.0, 10.023961309574004, -213.14719370229778, 2370.8738947779639, -17851.325555686766, 100600.32983742066, -447455.74829310848, 1622555.2656386697, -4900749.5285749733, 12513874.283793306, -27299957.308734588, 51266526.421700358, -83311648.573375255, 117583697.14553055, -144465187.34168035, 
                    154706858.84426239, -144465187.34168035, 117583697.14553055, -83311648.573375255, 51266526.421700358, -27299957.308734588, 12513874.283793306, -4900749.5285749733, 1622555.2656386697, -447455.74829310848, 100600.32983742066, -17851.325555686766, 2370.8738947779639, -213.14719370229778, 10.023961309574004, 0.0, 
                    0.0
                 }, { 
                    0.0, 0.0, -9.1073978559809685, 193.8166247981589, -2158.7336783924989, 16285.529893098381, -92018.853681834982, 410701.3136332407, -1495810.1927623719, 4542475.9090421814, -11675433.206550142, 25670151.422398254, -48645049.435056917, 79871877.927710846, -114029944.13664611, 141849847.93612796, 
                    -153901352.55159307, 145633705.91873479, -120093467.85872014, 86155016.5178158, -53628658.89815139, 28853745.150192279, -13345903.375291584, 5266987.7124859132, -1755045.2679453951, 486530.58169959311, -109839.98337826184, 19553.049027746551, -2602.9745400971988, 234.39214866726843, -11.034054069449509, 0.0, 
                    0.0
                 }, { 
                    0.0, 0.0, 8.2681909975592269, -176.07744740181991, 1963.3321335686571, -14835.329562811608, 84009.641576454931, -376040.0348489217, 1374627.23055301, -4193677.9917907864, 10839573.037704829, -23993475.491647057, 45830588.033207394, -75947061.172809333, 109567275.53122792, -137892461.44657797, 
                    151503759.30114231, -145277180.80040246, 121427796.44227205, -88278199.237148613, 55650068.612198785, -30292422.549309622, 14158072.857389838, -5638299.35651761, 1893196.8566770691, -528140.88455162093, 119834.70285311405, -21415.154488102391, 2859.0396618355458, -257.96033795995339, 12.158352077015341, 0.0, 
                    0.0
                 }, { 
                    0.0, 0.0, -7.4934817459120833, 159.67070539064329, -1782.0470096903282, 13483.735826501335, -76497.3468014726, 343249.12234413449, -1258676.7636152515, 3854962.2972566546, -10012074.476610214, 22291195.719971184, -42875948.717071667, 71633853.9937883, -104326500.35556743, 132715023.3334595, 
                    -147568567.71621352, 143351463.309612, -121468868.97540864, 89550076.803917617, -57234095.990329042, 31564811.130508177, -14930867.976962052, 6009716.2160151685, -2036466.1991788787, 572457.52965296106, -130691.27025902751, 23467.127069874146, -3144.1045258328527, 284.37769306188653, -13.4237748426992, 0.0, 
                    0.0
                 }, { 
                    0.0, 0.0, 6.7728921493692686, -144.38593237912426, 1612.7200587075645, -12216.463525668834, 69416.174565119378, -312118.28825453261, 1147555.0315920932, -3526350.8527939594, 9196430.3493411858, -20578479.626514021, 39822326.298333623, -67013919.822467551, 98428948.9059584, -126447710.32779108, 
                    142179883.99357173, -139850148.53360575, 120123093.99187873, -89840533.750926957, 58270775.379511006, -32605906.832569908, 15636742.677362647, -6373192.3764954722, 2183535.4092947743, -619547.6016869935, 142521.97597286425, -25744.454611048055, 3464.5770659447003, -314.33294656847727, 14.86610228320882, 0.0, 
                    0.0
                 }, { 
                    0.0, 0.0, -6.097929985661712, 130.04986692466912, -1453.5555234287986, 11021.413675396256, -62708.686969716087, 282453.059542989, -1040827.7780740529, 3207495.1409316137, -8394519.24583436, 18865850.638579749, -36701297.749790281, 62154835.602446079, -91983619.325100243, 119221424.12573238, 
                    -135443663.81701553, 134803889.34555179, -117329709.41531475, 89031377.510014474, -58642152.366057612, 33336452.530153446, -16238006.991736708, 6716224.9432243183, -2331813.2192305229, 669265.56933993055, -155433.21755419878, 28289.430852083769, -3828.7071232713997, 348.746252330969, -16.534036059824967, 0.0, 
                    0.0
                 }, { 
                    0.0, 0.0, 5.4615552142884374, -116.51812905301091, 1303.0432977036153, -9888.25858556358, 56324.626128378819, -254075.5306675754, 938055.94418603566, -2897818.3288421403, 7607090.68499008, -17160335.440890066, 33536654.684336036, -57111605.4484434, 85086127.173606992, -111162384.3688271, 
                    127479823.05628188, -128276284.39252044, 113065693.59968773, -87027630.659240723, 58231347.761269391, -33665175.1057288, 16685158.465858568, -7020096.6573424144, 2476659.4572455068, -721057.04723449936, 169499.43819764096, -31151.149469645556, 4247.2249710524093, -388.87509670978022, 18.495741534938279, 0.0, 
                    0.0
                 }, { 
                    0.0, 0.0, -4.8578592721923517, 103.66899694686015, -1159.900237681624, 8808.1105141370117, -50219.842914171633, 226823.79956422688, -838810.32961533067, 2596609.5172832366, -6834112.6379378121, 15466351.942605646, -30345991.796005607, 51928399.253468871, -77818883.624400824, 102388696.74165693, 
                    -118415342.46064724, 120357848.15216714, -107346689.61990614, 83766817.315762162, -56934030.271228865, 33494936.6905294, -16916870.395108085, 7257995.1660075653, -2610241.7295808238, 773610.44356569124, -184706.57112688979, 34383.014649739242, -4734.177669837145, 436.4829343638628, -20.849863856338029, 0.0, 
                    0.0
                 }, { 
                    0.0, 0.0, 4.2818253517941125, -91.398714175794908, 1023.0251457633469, -7773.2587030764289, 44355.369227154966, -200550.82336197956, 742679.662097814, -2303086.4525823523, 6075021.21139369, -13786392.05609243, 27142043.141500272, -46640299.756983846, 70252042.662554964, -93008580.54336144, 
                    108378902.03266878, -111159476.80455665, 100224727.6018144, -79224837.753076717, 54669943.819449782, -32732520.422193065, 16863307.550604973, -7393855.8501159484, 2720037.9455866013, -824258.403098117, 200832.28394121127, -38033.440598808913, 5307.8992946302933, -494.11703353203666, 23.745109319384195, 0.0, 
                    0.0
                 }, { 
                    0.0, 0.0, -3.7291499069005756, 79.617974176048563, -891.464655412596, 6776.9656566393514, -38696.661913726057, 175123.20048133531, -649275.19428224943, 2016438.8193487984, -5328904.6350136213, 12121554.147801474, -23933803.194632333, 41274969.7191998, -62444916.877816245, 83119915.112229839, 
                    -97497205.351181835, 100806465.41534492, -91783998.197837159, 73418235.853605628, -51392389.7523361, 31300243.1445671, -16453396.67929562, 7383608.1101669716, -2787424.9133211016, 868030.42038303439, -217197.79200154994, 42118.437269824572, -5991.6592028098776, 565.58293098504055, -27.41748654037545, 0.0, 
                    0.0
                 }, { 
                    0.0, 0.0, 3.1961174285965135, -68.249455488396222, 764.38935564333588, -5813.3253325533115, 33213.086406697221, -150420.44480385908, 558234.81986413139, -1735863.4394839669, 4594653.1676975628, -10471988.969670195, 20727516.382220577, -35854277.214679152, 54447765.880256832, -72810919.505185738, 
                    85893062.147710159, -89433853.711064488, 82136027.12249361, -66403599.17286846, 47094569.81003131, -29147122.45513808, 15625013.428038068, -7180014.4194837082, 2787774.2833171254, -896476.51798135124, 232179.32985603643, -46546.180921939806, 6812.00244444961, -656.78050985031109, 32.267596580551363, 0.0, 
                    0.0
                 }, { 
                    0.0, 0.0, -2.6795429036763232, 57.226743793928165, -641.08441328536787, 4877.220720692143, -27877.889715410729, 126336.06062418707, -469232.21541099035, 1460610.1420229792, -3871126.7989888792, 8837374.7862485237, -17527740.651195783, 30396164.559309285, -46304268.865378976, 62162283.921619207, 
                    -73685716.451159626, 77184092.26148507, -71415849.7419322, 58275614.964858435, -41812970.491553515, 26257963.087492332, -14336068.117813712, 6740776.94218054, -2693443.7568843081, 897329.41100023489, -242409.13010980852, 50916.587200320057, -7786.6589352833907, 777.18207861708277, -39.041109113858511, 0.0, 
                    0.0
                 }, { 
                    0.0, 0.0, 2.1768785366519308, -46.496722636334205, 520.9783533788908, -3964.56432716742, 22669.736702591239, -102785.41395707433, 382010.05492817512, -1190098.9640635177, 3157503.985685918, -7217795.4276359528, 14339216.13606317, -24917988.422812354, 38056468.444110587, -51253062.510530055, 
                    60996187.2196405, -64210242.409922615, 59782768.342638619, -49167232.138233975, 35630046.507139586, -22660500.406681437, 12574893.006975455, -6038424.2167017451, 2479845.1034083338, -856327.08028914046, 242411.312439228, -54048.934888204654, 8867.38291396704, -942.05766704231746, 49.328165429112872, 0.0, 
                    0.0
                 }, { 
                    0.0, 0.0, -1.6870647389248417, 36.0378363896997, -403.84924199597242, 3073.884014681782, -17581.905422139185, 79748.149463809881, -296540.88455517194, 924432.834147331, -2454674.890576358, 5617000.9077257281, -11173476.940678947, 19448170.303711474, -29762610.300542638, 40184349.60252019, 
                    -47974377.348001674, 50702640.828836322, -47442982.764444143, 39266877.598928951, -28687397.288539615, 18437181.908754677, -10372155.43085124, 5071561.9451884488, -2133943.2071678732, 761710.43316685443, -225806.08854260363, 53752.380339333817, -9686.6866952133387, 1170.3344309548697, -67.376558689547039, 0.0, 
                    0.0
                 }, { 
                    0.0, 0.0, 1.2179257998191046, -26.018152985497245, 291.59745595588049, -2219.8325198323314, 12699.682262895616, -57619.9584043329, 214338.44954060277, -668497.67977040634, 1776165.6619429556, -4067498.8150485298, 8098957.4796942864, -14113735.362409372, 21631529.228465378, -29261078.286464926, 
                    35016136.991270684, -37117771.746179491, 34862743.881596059, -28993987.955398969, 21314182.939722285, -13809734.559270095, 7852365.6484552165, -3894976.2597132488, 1671412.8057286434, -613315.33219479362, 189242.5109373311, -47859.527813268658, 9503.157356021522, -1360.8731321863129, 111.36470065949621, 0.0, 
                    0.0
                 }, 
                { 
                    0.0, 0.0, -0.49523727008116614, 10.579932825238359, -118.58040055493349, 902.7814526494634, -5165.366324096779, 23439.17254191445, -87206.506732078633, 272051.13208890735, -723041.47630947409, 1656417.2137646589, -3299705.8633233043, 5753645.2542467285, -8824849.8107638843, 11948407.891075697, 
                    -14314957.968604734, 15196289.832516778, -14299545.084313307, 11920624.262090228, -8790053.00042688, 5718076.0458312277, -3268708.6823544325, 1633056.5468816217, -707759.16130218632, 263381.8426237625, -82958.594189230847, 21652.532740660426, -4526.0595506130512, 710.52132144523807, -70.800567819936944, 1.0, 
                    1.0
                 }
             };
            private static readonly double[,] matrix_33 = new double[,] { 
                { 
                    0.0, 0.030303030303030304, -0.43142622860592217, 3.6253530839625183, -22.073387980938914, 105.21323378933988, -409.10422576637762, 1331.447005506436, -3691.1014339205417, 8826.8226095097343, -18379.144526224474, 33555.825098162793, -54003.822274701837, 76911.05366593934, -97198.29737958753, 109194.86270054945, 
                    -109143.30064659382, 97062.100178266177, -76735.497931253325, 53837.737566157535, -33430.937598624943, 18303.177326270616, -8790.0256429769233, 3677.8427539623895, -1328.7965162113949, 409.676001694288, -106.06405853202384, 22.545692954858545, -3.8057565484377438, 0.48338041119500241, -0.041750925348960209, 0.001893939393939394, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, -0.001893939393939394, 0.041750925348960209, -0.48338041119500241, 3.8057565484377438, -22.545692954858545, 106.06405853202384, -409.676001694288, 1328.7965162113949, -3677.8427539623895, 8790.0256429769233, -18303.177326270616, 33430.937598624943, -53837.737566157535, 76735.497931253325, 
                    -97062.100178266177, 109143.30064659382, -109194.86270054945, 97198.29737958753, -76911.05366593934, 54003.822274701837, -33555.825098162793, 18379.144526224474, -8826.8226095097343, 3691.1014339205417, -1331.447005506436, 409.10422576637762, -105.21323378933988, 22.073387980938914, -3.6253530839625183, 0.43142622860592217, 
                    -0.030303030303030304, 0.0
                 }, { 
                    1.0, 1.0, -75.05097958136308, 779.9164735377268, -5146.3493907955062, 25529.726950344535, -101555.95696831984, 335236.04409349588, -938094.64433449169, 2257864.0318398569, -4722990.9538134672, 8652118.9216220025, -13959580.519839549, 19919013.454029385, -25210326.809699513, 28354474.933255587, 
                    -28366752.021056216, 25244911.687601555, -19969580.972279515, 14017106.058881119, -8707225.5990313031, 4768546.8564450787, -2290618.6173440129, 958602.15924441011, -346393.41120931681, 106808.01836775556, -27654.924002823282, 5878.944481579827, -992.43151144180729, 126.05668432925596, -10.888167823507919, 0.49392622860592217, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, 117.96634554749039, -1491.6787286479866, 10789.99829885219, -56353.664256392229, 231382.73338786664, -779771.72490135452, 2213144.2670058203, -5380309.1750654988, 11336618.391644724, -20880074.26267083, 33826112.715221085, -48417741.577942066, 61428241.044006646, -69220762.895026237, 
                    69354777.927260056, -61795833.575384364, 48929280.251222812, -34370893.664087482, 21363859.099992543, -11705802.474492745, 5625239.2371883485, -2354861.8491477789, 851152.6927626814, -262500.13911520288, 67977.782893079944, -14452.634030289453, 2439.9910575050812, -309.9436193009156, 26.772666224583595, -1.214538329141549, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, -71.389857418112953, 1280.5083741065971, -10973.520692768534, 63146.887680385007, -275480.70485546137, 966450.12910084, -2820212.8693630672, 6993308.9214395694, -14950373.789721228, 27835425.418504816, -45465589.577045821, 65490485.347647659, -83498076.110497028, 94454328.992338389, 
                    -94926981.837445691, 84787195.480375543, -67264608.430575818, 47324789.589216471, -29452687.239464931, 16154265.687271899, -7769296.7033540653, 3254548.5884338976, -1176955.6710394253, 363130.18541600951, -94067.821823480641, 20004.611572060006, -3377.9525642359081, 429.14871222849638, -37.07300499086, 1.6819097219152128, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, 52.285358358650662, -1030.6558272950697, 10032.540907172448, -63375.544030055324, 295108.08891671011, -1084117.5484074482, 3270297.8363391659, -8309329.7304293606, 18089904.363555256, -34148612.076998614, 56371556.9094804, -81870965.424777433, 105058421.33012155, -119451414.58687389, 
                    120537226.3290163, -108011742.15128815, 85912989.331003278, -60572015.546341233, 37760963.689476, -20739492.459753308, 9985508.5499603543, -4186613.6832066169, 1515091.592963028, -467718.32368854719, 121214.32466837158, -25786.375341653911, 4355.3676199267138, -553.42647198787131, 47.815137439813292, -2.1694242652064766, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, -41.400684223066371, 850.51165403639618, -8807.5077220323365, 59638.099297314206, -294614.18793988478, 1133757.9950269221, -3544332.9949913546, 9255444.65917057, -20578708.750883818, 39486589.737964779, -66020485.906191178, 96852160.688692123, -125274932.92694986, 143342913.98384863, 
                    -145381489.77277365, 130806622.32184501, -104386755.04395036, 73792487.68937315, -46101605.757968649, 25364439.835580971, -12229424.914967284, 5133202.5865266388, -1859328.9915544554, 574397.88599023363, -148945.23331615675, 31699.440330609741, -5355.850119623201, 680.71779722743827, -58.822531117304017, 2.6691108580605341, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, 34.237165185487676, -719.08931698548156, 7706.5032711205067, -54500.357166750924, 281898.97626543319, -1130935.557709831, 3661469.4773257109, -9837623.9498154987, 22379031.266131077, -43732710.744320706, 74194677.8091838, -110122276.62868008, 143780658.49149281, -165763553.99001658, 
                    169147982.74205709, -152942263.14347371, 122539988.40226108, -86906456.9418079, 54437483.97306855, -30014641.71998056, 14496491.064199639, -6093247.9663125779, 2209532.3919234709, -683189.85598995013, 177278.97803580583, -37749.889333990475, 6380.7055536796861, -811.2144687831119, 70.11329876911384, -3.181834544699861, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, -29.110761268922449, 619.62620041529317, -6781.5159522439535, 49321.676323910775, -263606.36559741723, 1093989.7521833761, -3655604.1447405689, 10097777.069263551, -23515726.188292563, 46857219.245953396, -80777520.839375228, 121471643.22828844, -160301283.84764683, 186426237.89154431, 
                    -191588265.68195879, 174239101.96912423, -140264833.12115809, 99861360.115442365, -62749179.219635114, 34685920.712608263, -16787397.541429423, 7068000.6350609027, -2566444.5979671171, 794396.9733008719, -206309.32942158129, 43960.234114146515, -7434.067416316434, 945.47265957057232, -81.737340502088941, 3.7099111919269774, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, 25.231782635446379, -541.7464126606626, 6011.5691022268265, -44557.3198686453, 243760.11752384683, -1038382.5381001054, 3563794.1682659122, -10097087.208497833, 24057081.030319545, -48897094.944780856, 85730700.947734833, -130763222.60514404, 174615764.50128397, -205073635.46904811, 
                    212465032.34838721, -194518736.80872396, 157451806.73370135, -112603716.24389148, 71017589.980586261, -39374974.253793448, 19103631.26456517, -8059201.400233536, 2931038.0979989157, -908407.15508024162, 236155.27190840521, -50358.896451423825, 8521.1399518537128, -1084.1915070389396, 93.757325993197242, -4.2562478388165976, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, -22.175989508686325, 478.99332079629045, -5366.2854923895229, 40308.248617133111, -224280.98573811687, 974606.0089736057, -3418381.5092144608, 9901872.3824517652, -24098185.408937443, 49942590.355537444, -89087130.881369829, 137930469.45601064, -186554654.01075909, 221473195.00042257, 
                    -231543096.7760154, 213592913.57809025, -173979555.20603877, 125071436.70540617, -79219133.753700688, 44076844.450170808, -21446338.790329117, 9068644.54337094, -3304371.9282882768, 1025653.6230859284, -266951.66245212959, 56978.39907822141, -9647.9167888836855, 1228.1796991990241, -106.24595039674745, 4.8242222527659342, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, 19.693456643523078, -427.20451714664722, 4819.2074744245547, -36552.4894363656, 205961.55432438903, -908769.40742519265, 3243444.9484647946, -9572638.8554317169, 23744155.774412043, -50121184.92505905, 90942264.313336432, -142977603.07302427, 196005403.95186973, -235421842.7299535, 
                    248590222.31108677, -231260699.48712176, 189710504.22942549, -137189939.36063033, 87323183.78475666, -48783586.326867968, 23815769.860967275, -10097990.888151266, 3687542.1349239238, -1146605.0514293965, 298847.95560556831, -63855.327404400152, 10821.206429075864, -1378.3618241241518, 119.28667375785891, -5.4177219137619792, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, -17.626718456074656, 383.59489685441758, -4349.5465314190869, 33231.754348909686, -189053.25826850475, 844119.31837767235, -3054997.4863581033, 9158069.4703898747, -23095686.681387164, 49579401.855519146, -91440903.901377, 145976827.01402375, -202918179.09234238, 246754050.86761412, 
                    -263382065.70202988, 247308886.37472904, -204488400.59324896, 148869142.71325698, -95289860.320608556, 53483081.47911986, -26210791.266554013, 11148617.793259609, -4081649.8880635519, 1271762.7037403788, -332009.14105929568, 71030.839925052118, -12048.761581787996, 1535.7990337946485, -132.97576703804387, 6.04124429142123, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, 15.871328529075198, -346.23693570890492, 3941.4002188840104, -30282.724148457739, 173562.51999868924, -782230.98437893984, 2862962.2808223031, -8693912.2594784349, 22239510.718627833, -48465287.457781993, 90760007.815341115, -147060568.630711, 207309442.34947196, -255351103.88698441, 
                    275709919.46035922, -261514782.14679939, 218137043.12854365, -160000718.54417443, 103067699.60144565, -58157698.77915556, 28628318.9595938, -12221442.438412063, 4487765.264496875, -1401658.2826662858, 366617.41907963733, -78551.445621927924, 13339.469576461306, -1701.7190845856167, 147.42526975921103, -6.7000414125448557, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, -14.355121970208016, 313.75654274089436, -3582.6339880880223, 27647.543138312027, -159392.88897920863, 723752.56977505679, -2673161.1271069129, 8204876.391066134, -21244636.171575792, 46914748.9587613, -89089813.002413183, 146408380.88502181, -209261349.9917976, 261149912.57335815, 
                    -285390674.00177008, 273651479.85815912, -230460355.20439556, 170455579.05417481, -110591064.43355605, 62782669.845459528, -31062588.527278263, 13316683.434554433, -4906875.1024847412, 1536849.5646091814, -402874.06566780491, 86469.977740080925, -14703.59899972564, 1877.5558788851304, -162.76691760550034, 7.4003127558218083, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, 13.026548945504064, -285.14854264013326, 3263.9261287804247, -25276.537238039957, 146411.80315425681, -668835.33219497441, 2488803.93815453, -7707318.9761207709, 20163015.671732433, -45043757.621802464, 86616613.766327351, -144229711.73997045, 208915584.44759718, -264149506.82924235, 
                    292278224.91078115, -283495856.40782809, 241244289.62731239, -180081849.97117755, 117777300.5651516, -67324095.165549576, 33504194.879785445, -14433527.261803849, 5339803.6740179872, -1677911.1702286608, 441001.09428382735, -94846.747484010484, 16153.110386416141, -2065.0006320676944, 179.15729672516665, -8.14946027164274, 
                    0.0, 0.0
                 }, 
                { 
                    0.0, 0.0, -11.847746609756541, 259.66082696782837, -2978.0414809753347, 23128.120166321565, -134481.1346844364, 617374.637365171, -2311488.5133502781, 7211666.69082761, -19032258.855878349, 42946153.312219918, -83510242.368799478, 140745306.75573742, -206461358.9704808, 264413169.31149966, 
                    -296274971.38704741, 290839346.42903322, -250261224.11918205, 188703809.94687364, -124523766.59200732, 71736526.698573425, -35938826.691506907, 15569658.125683814, -5787091.3656675518, 1825416.89944491, -481242.19788495282, 103750.86705168089, -17702.0453479809, 2266.0682464963038, -196.78464915499561, 8.9564281786829252, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, 10.790238188206512, -236.71931426500714, 2719.298869521017, -21167.800067186818, 123470.39880724391, -569142.92825155344, 2141839.6413886459, -6724283.3572390219, 17878774.739928473, -40695009.538338266, 79917450.0829131, -136170786.69062844, 202119039.52786422, -262064560.75606808, 
                    297341356.63738769, -295500988.4645769, 257277509.57669085, -196122514.48404929, 130705032.76292191, -75960120.748763531, 38345614.002173036, -16720596.53472852, 6248810.0028167609, -1979908.1440646295, 523862.11562678916, -113261.70797282111, 19367.013444340035, -2483.1841241122484, 215.87796585455257, -9.8321619005800684, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, -9.8321619005800684, 215.87796585455257, -2483.1841241122484, 19367.013444340035, -113261.70797282111, 523862.11562678916, -1979908.1440646295, 6248810.0028167609, -16720596.53472852, 38345614.002173036, -75960120.748763531, 130705032.76292191, -196122514.48404929, 257277509.57669085, 
                    -295500988.4645769, 297341356.63738769, -262064560.75606808, 202119039.52786422, -136170786.69062844, 79917450.0829131, -40695009.538338266, 17878774.739928473, -6724283.3572390219, 2141839.6413886459, -569142.92825155344, 123470.39880724391, -21167.800067186818, 2719.298869521017, -236.71931426500714, 10.790238188206512, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, 8.9564281786829252, -196.78464915499561, 2266.0682464963038, -17702.0453479809, 103750.86705168089, -481242.19788495282, 1825416.89944491, -5787091.3656675518, 15569658.125683814, -35938826.691506907, 71736526.698573425, -124523766.59200732, 188703809.94687364, -250261224.11918205, 
                    290839346.42903322, -296274971.38704741, 264413169.31149966, -206461358.9704808, 140745306.75573742, -83510242.368799478, 42946153.312219918, -19032258.855878349, 7211666.69082761, -2311488.5133502781, 617374.637365171, -134481.1346844364, 23128.120166321565, -2978.0414809753347, 259.66082696782837, -11.847746609756541, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, -8.14946027164274, 179.15729672516665, -2065.0006320676944, 16153.110386416141, -94846.747484010484, 441001.09428382735, -1677911.1702286608, 5339803.6740179872, -14433527.261803849, 33504194.879785445, -67324095.165549576, 117777300.5651516, -180081849.97117755, 241244289.62731239, 
                    -283495856.40782809, 292278224.91078115, -264149506.82924235, 208915584.44759718, -144229711.73997045, 86616613.766327351, -45043757.621802464, 20163015.671732433, -7707318.9761207709, 2488803.93815453, -668835.33219497441, 146411.80315425681, -25276.537238039957, 3263.9261287804247, -285.14854264013326, 13.026548945504064, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, 7.4003127558218083, -162.76691760550034, 1877.5558788851304, -14703.59899972564, 86469.977740080925, -402874.06566780491, 1536849.5646091814, -4906875.1024847412, 13316683.434554433, -31062588.527278263, 62782669.845459528, -110591064.43355605, 170455579.05417481, -230460355.20439556, 
                    273651479.85815912, -285390674.00177008, 261149912.57335815, -209261349.9917976, 146408380.88502181, -89089813.002413183, 46914748.9587613, -21244636.171575792, 8204876.391066134, -2673161.1271069129, 723752.56977505679, -159392.88897920863, 27647.543138312027, -3582.6339880880223, 313.75654274089436, -14.355121970208016, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, -6.7000414125448557, 147.42526975921103, -1701.7190845856167, 13339.469576461306, -78551.445621927924, 366617.41907963733, -1401658.2826662858, 4487765.264496875, -12221442.438412063, 28628318.9595938, -58157698.77915556, 103067699.60144565, -160000718.54417443, 218137043.12854365, 
                    -261514782.14679939, 275709919.46035922, -255351103.88698441, 207309442.34947196, -147060568.630711, 90760007.815341115, -48465287.457781993, 22239510.718627833, -8693912.2594784349, 2862962.2808223031, -782230.98437893984, 173562.51999868924, -30282.724148457739, 3941.4002188840104, -346.23693570890492, 15.871328529075198, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, 6.04124429142123, -132.97576703804387, 1535.7990337946485, -12048.761581787996, 71030.839925052118, -332009.14105929568, 1271762.7037403788, -4081649.8880635519, 11148617.793259609, -26210791.266554013, 53483081.47911986, -95289860.320608556, 148869142.71325698, -204488400.59324896, 
                    247308886.37472904, -263382065.70202988, 246754050.86761412, -202918179.09234238, 145976827.01402375, -91440903.901377, 49579401.855519146, -23095686.681387164, 9158069.4703898747, -3054997.4863581033, 844119.31837767235, -189053.25826850475, 33231.754348909686, -4349.5465314190869, 383.59489685441758, -17.626718456074656, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, -5.4177219137619792, 119.28667375785891, -1378.3618241241518, 10821.206429075864, -63855.327404400152, 298847.95560556831, -1146605.0514293965, 3687542.1349239238, -10097990.888151266, 23815769.860967275, -48783586.326867968, 87323183.78475666, -137189939.36063033, 189710504.22942549, 
                    -231260699.48712176, 248590222.31108677, -235421842.7299535, 196005403.95186973, -142977603.07302427, 90942264.313336432, -50121184.92505905, 23744155.774412043, -9572638.8554317169, 3243444.9484647946, -908769.40742519265, 205961.55432438903, -36552.4894363656, 4819.2074744245547, -427.20451714664722, 19.693456643523078, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, 4.8242222527659342, -106.24595039674745, 1228.1796991990241, -9647.9167888836855, 56978.39907822141, -266951.66245212959, 1025653.6230859284, -3304371.9282882768, 9068644.54337094, -21446338.790329117, 44076844.450170808, -79219133.753700688, 125071436.70540617, -173979555.20603877, 
                    213592913.57809025, -231543096.7760154, 221473195.00042257, -186554654.01075909, 137930469.45601064, -89087130.881369829, 49942590.355537444, -24098185.408937443, 9901872.3824517652, -3418381.5092144608, 974606.0089736057, -224280.98573811687, 40308.248617133111, -5366.2854923895229, 478.99332079629045, -22.175989508686325, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, -4.2562478388165976, 93.757325993197242, -1084.1915070389396, 8521.1399518537128, -50358.896451423825, 236155.27190840521, -908407.15508024162, 2931038.0979989157, -8059201.400233536, 19103631.26456517, -39374974.253793448, 71017589.980586261, -112603716.24389148, 157451806.73370135, 
                    -194518736.80872396, 212465032.34838721, -205073635.46904811, 174615764.50128397, -130763222.60514404, 85730700.947734833, -48897094.944780856, 24057081.030319545, -10097087.208497833, 3563794.1682659122, -1038382.5381001054, 243760.11752384683, -44557.3198686453, 6011.5691022268265, -541.7464126606626, 25.231782635446379, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, 3.7099111919269774, -81.737340502088941, 945.47265957057232, -7434.067416316434, 43960.234114146515, -206309.32942158129, 794396.9733008719, -2566444.5979671171, 7068000.6350609027, -16787397.541429423, 34685920.712608263, -62749179.219635114, 99861360.115442365, -140264833.12115809, 
                    174239101.96912423, -191588265.68195879, 186426237.89154431, -160301283.84764683, 121471643.22828844, -80777520.839375228, 46857219.245953396, -23515726.188292563, 10097777.069263551, -3655604.1447405689, 1093989.7521833761, -263606.36559741723, 49321.676323910775, -6781.5159522439535, 619.62620041529317, -29.110761268922449, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, -3.181834544699861, 70.11329876911384, -811.2144687831119, 6380.7055536796861, -37749.889333990475, 177278.97803580583, -683189.85598995013, 2209532.3919234709, -6093247.9663125779, 14496491.064199639, -30014641.71998056, 54437483.97306855, -86906456.9418079, 122539988.40226108, 
                    -152942263.14347371, 169147982.74205709, -165763553.99001658, 143780658.49149281, -110122276.62868008, 74194677.8091838, -43732710.744320706, 22379031.266131077, -9837623.9498154987, 3661469.4773257109, -1130935.557709831, 281898.97626543319, -54500.357166750924, 7706.5032711205067, -719.08931698548156, 34.237165185487676, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, 2.6691108580605341, -58.822531117304017, 680.71779722743827, -5355.850119623201, 31699.440330609741, -148945.23331615675, 574397.88599023363, -1859328.9915544554, 5133202.5865266388, -12229424.914967284, 25364439.835580971, -46101605.757968649, 73792487.68937315, -104386755.04395036, 
                    130806622.32184501, -145381489.77277365, 143342913.98384863, -125274932.92694986, 96852160.688692123, -66020485.906191178, 39486589.737964779, -20578708.750883818, 9255444.65917057, -3544332.9949913546, 1133757.9950269221, -294614.18793988478, 59638.099297314206, -8807.5077220323365, 850.51165403639618, -41.400684223066371, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, -2.1694242652064766, 47.815137439813292, -553.42647198787131, 4355.3676199267138, -25786.375341653911, 121214.32466837158, -467718.32368854719, 1515091.592963028, -4186613.6832066169, 9985508.5499603543, -20739492.459753308, 37760963.689476, -60572015.546341233, 85912989.331003278, 
                    -108011742.15128815, 120537226.3290163, -119451414.58687389, 105058421.33012155, -81870965.424777433, 56371556.9094804, -34148612.076998614, 18089904.363555256, -8309329.7304293606, 3270297.8363391659, -1084117.5484074482, 295108.08891671011, -63375.544030055324, 10032.540907172448, -1030.6558272950697, 52.285358358650662, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, 1.6819097219152128, -37.07300499086, 429.14871222849638, -3377.9525642359081, 20004.611572060006, -94067.821823480641, 363130.18541600951, -1176955.6710394253, 3254548.5884338976, -7769296.7033540653, 16154265.687271899, -29452687.239464931, 47324789.589216471, -67264608.430575818, 
                    84787195.480375543, -94926981.837445691, 94454328.992338389, -83498076.110497028, 65490485.347647659, -45465589.577045821, 27835425.418504816, -14950373.789721228, 6993308.9214395694, -2820212.8693630672, 966450.12910084, -275480.70485546137, 63146.887680385007, -10973.520692768534, 1280.5083741065971, -71.389857418112953, 
                    0.0, 0.0
                 }, 
                { 
                    0.0, 0.0, -1.214538329141549, 26.772666224583595, -309.9436193009156, 2439.9910575050812, -14452.634030289453, 67977.782893079944, -262500.13911520288, 851152.6927626814, -2354861.8491477789, 5625239.2371883485, -11705802.474492745, 21363859.099992543, -34370893.664087482, 48929280.251222812, 
                    -61795833.575384364, 69354777.927260056, -69220762.895026237, 61428241.044006646, -48417741.577942066, 33826112.715221085, -20880074.26267083, 11336618.391644724, -5380309.1750654988, 2213144.2670058203, -779771.72490135452, 231382.73338786664, -56353.664256392229, 10789.99829885219, -1491.6787286479866, 117.96634554749039, 
                    0.0, 0.0
                 }, { 
                    0.0, 0.0, 0.49392622860592217, -10.888167823507919, 126.05668432925596, -992.43151144180729, 5878.944481579827, -27654.924002823282, 106808.01836775556, -346393.41120931681, 958602.15924441011, -2290618.6173440129, 4768546.8564450787, -8707225.5990313031, 14017106.058881119, -19969580.972279515, 
                    25244911.687601555, -28366752.021056216, 28354474.933255587, -25210326.809699513, 19919013.454029385, -13959580.519839549, 8652118.9216220025, -4722990.9538134672, 2257864.0318398569, -938094.64433449169, 335236.04409349588, -101555.95696831984, 25529.726950344535, -5146.3493907955062, 779.9164735377268, -75.05097958136308, 
                    1.0, 1.0
                 }
             };
            private static readonly double[,] matrix_34 = new double[,] { 
                { 
                    0.0, 0.029411764705882353, -0.43208701451707743, 3.7506067893539576, -23.615097324014695, 116.53999802927241, -469.75667456817541, 1587.0424725874118, -4573.8688808071529, 11388.974533114551, -24734.683787385049, 47191.656648774049, -79529.832832362648, 118872.89188720769, -158060.59569152392, 187343.96955643466, 
                    -198175.18334324053, 187165.45093528272, -157763.34278436605, 118545.44676864294, -79248.594591471789, 46994.901219123385, -24621.7339037082, 11336.730069218147, -4555.5898132458206, 1583.2623048947837, -470.23480669256645, 117.43916864204385, -24.111723730194292, 3.935849554002449, -0.48394926314244618, 0.040508157610976009, 
                    -0.0017825311942959, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 0.0017825311942959, -0.040508157610976009, 0.48394926314244618, -3.935849554002449, 24.111723730194292, -117.43916864204385, 470.23480669256645, -1583.2623048947837, 4555.5898132458206, -11336.730069218147, 24621.7339037082, -46994.901219123385, 79248.594591471789, -118545.44676864294, 
                    157763.34278436605, -187165.45093528272, 198175.18334324053, -187343.96955643466, 158060.59569152392, -118872.89188720769, 79529.832832362648, -47191.656648774049, 24734.683787385049, -11388.974533114551, 4573.8688808071529, -1587.0424725874118, 469.75667456817541, -116.53999802927241, 23.615097324014695, -3.7506067893539576, 
                    0.43208701451707743, -0.029411764705882353, 0.0
                 }, { 
                    1.0, 1.0, -79.422730605916769, 853.6537696012424, -5827.9921618888193, 29941.259858434238, -123492.95787966656, 423223.02561871341, -1231319.6050326868, 3086085.1351395724, -6733723.1466578823, 12891380.186125349, -21780941.113789909, 32619473.322621066, -43438285.531887561, 51546692.867506236, 
                    -54577607.03405603, 51583684.492691852, -43506088.800806478, 32706621.6725285, -21873046.209959485, 12974879.798183598, -6799552.5055573005, 3131393.4535358991, -1258535.4329212562, 437452.21807288181, -129938.48708823571, 32454.254011571506, -6663.6857798537421, 1087.7913668002186, -133.75874396190264, 11.196320671359191, 
                    -0.49269307512313804, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 124.75642068063411, -1630.5491606279561, 12202.610354649221, -66007.937802487621, 281038.69942787581, -983403.21713141177, 2902167.4772759378, -7347580.1752377357, 16150374.936495388, -31088511.758470722, 52744103.904108167, -79241894.717591852, 105785124.15990567, -125775727.14792588, 
                    133376580.2512051, -126215136.37574629, 106555941.24701199, -80169407.891436189, 53648969.769072331, -31840612.527362764, 16693267.536814084, -7690358.8293313729, 3091673.9526090506, -1074866.5247889231, 319328.64358013583, -79768.54042741211, 16380.266167858868, -2674.1587045587607, 328.84360005878364, -27.527083507714739, 
                    1.211357255208968, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -75.517639999346017, 1397.3476528565643, -12383.813104719478, 73805.0423440314, -333907.06364859274, 1216468.2760599274, -3691596.6661424763, 9534555.401509745, -21266159.927800104, 41386292.468620434, -70801698.483853713, 107056113.79393096, -143633540.2916114, 171451595.22186813, 
                    -182383345.92395002, 173023485.91575611, -146367841.01450339, 110301681.13404918, -73910398.13488698, 43912474.098357886, -23042119.653858114, 10622597.739499096, -4272895.2903864654, 1486206.7623327882, -441691.1830551271, 110366.03496950791, -22668.354043739007, 3701.3339373433369, -455.21136032863211, 38.108348662205181, 
                    -1.677083043360561, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 55.326838122865624, -1124.5998122191788, 11308.353420195403, -73941.522498926061, 356982.04278297897, -1361768.8444458782, 4272082.989448675, -11306755.426683655, 25684506.300539624, -50684709.81254185, 87642340.994275078, -133629680.75223014, 180465680.12520823, -216539656.85929462, 
                    231303761.61977202, -220165193.50723395, 186747664.87506393, -141037562.76120484, 94672279.110227361, -56328181.052540369, 29591277.302597627, -13654626.399772281, 5496681.29009558, -1913030.5319139068, 568816.26249416708, -142185.26657421992, 29212.430358476835, -4770.9323383501123, 586.85271073782189, -49.134334961910774, 
                    2.1624683622862704, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -43.827436606519306, 928.26793712860194, -9925.0214248022185, 69510.553765821081, -355830.74182696041, 1421465.4769638979, -4620736.9208444124, 12568245.025528425, -29158654.310134314, 58491632.9227564, -102449120.41699043, 157796840.71529883, -214825815.70747072, 259432200.35896876, 
                    -278557200.48993027, 266251532.8496674, -226602997.79556146, 171607449.46610555, -115449585.71997452, 68814955.3650791, -36204370.381681457, 16726167.477049446, -6739643.837508061, 2347446.4597883048, -698416.99851444154, 174666.41104977272, -35899.372048754136, 5864.7141622637455, -721.54526943864073, 60.420199478193084, 
                    -2.659411430605366, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 36.262724009152549, -785.16649587742927, 8685.71641598814, -63501.401670644867, 340155.29730437271, -1415893.5599137635, 4764974.8840314429, -13332386.642319422, 31643746.350190923, -64645194.961490631, 114894103.69871274, -179053679.36129582, 246078776.26185718, -299450872.36152011, 
                    323520052.58892357, -310785204.34763241, 265588868.55194372, -201803386.18150809, 136133894.44200146, -81324360.756282136, 42863165.396444887, -19831613.692179531, 8000467.1670445837, -2789266.3823445463, 830502.60867926036, -207824.79925882129, 42734.427171181087, -6983.7981230481155, 859.45028796199188, -71.980812197921139, 
                    3.1686040305159597, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -30.852079138799564, 676.94678267241375, -7646.3122839752932, 57472.840752949087, -317964.13702619454, 1368403.0156487161, -4750822.9099445473, 13661372.05462601, -33185837.378124841, 69118369.561415642, -124817259.8692572, 197077967.50953111, -273765503.22802216, 336074987.80292684, 
                    -365702756.54336107, 353378881.55482095, -303447106.42238867, 231482111.18308157, -156661236.66292211, 93835538.145012751, -49564627.399869792, 22972709.859802015, -9280925.0727537535, 3239418.3053486324, -965424.75918032858, 241763.58251724468, -49741.257433080034, 8132.3576570299838, -1001.1091322423641, 83.863124114649892, 
                    -3.69215689741661, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 26.76053366351865, -592.273830944952, 6782.2012853895631, -51941.070419473894, 294039.143602792, -1298324.6318633161, 4627363.9874990676, -13642073.637999034, 33891902.838930458, -71985471.793509081, 132187724.59912263, -211679119.86090901, 297535413.47691578, -368855294.27564764, 
                    404651763.33169836, -393657587.14397573, 339921356.6454494, -260500226.18418652, 176968653.77500153, -106329601.25368364, 56307658.7350197, -26152533.76376906, 10583498.884206319, -3699130.8622331871, 1103638.6715289683, -276614.79077494796, 56949.959839605777, -9315.6816673442881, 1147.2063242698637, -96.126421083153218, 
                    4.2327332334391565, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -23.539532651323352, 524.100362784811, -6058.7807326170032, 47016.646953273237, -270641.40515700018, 1218597.0947722602, -4436645.9215920968, 13366225.352268066, -33904085.721237853, 73398390.2559841, -137088364.55059448, 222791192.41898566, -317142142.5367415, 397405780.0887531, 
                    -439934463.16300744, 431238682.76548058, -374736662.43019927, 288697333.265935, -196982577.8856954, 118782839.45620713, -63089690.082938723, 29374023.5433866, -11910829.107072841, 4169757.7885190602, -1245655.1478998181, 312528.26618912612, -64394.952663565287, 10539.850160248767, -1298.5315976205823, 108.83926264422084, 
                    -4.7934171771462459, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 20.924835278416268, -467.88547238039621, 5446.090048510986, -42670.479987019382, 248692.18685489619, -1136682.7556055584, 4209535.1800968852, -12915877.505448371, 33374979.58135885, -73559709.826424465, 139697397.56218004, -230468335.39215162, 332449492.03644395, -421411777.20776141, 
                    471142566.04339546, -465728765.58546436, 407592343.86822951, -315888458.3823061, 216613079.11044252, -131163292.23384815, 69905026.511970192, -32639323.379858445, 13265504.01932252, -4652723.0471756179, 1392029.5414431067, -349670.08840273391, 72114.858411011592, -11811.742411977693, 1455.9834689895019, -122.0799336832394, 
                    5.3777379063340094, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -18.749964844011664, 420.59184043677607, -4920.6483875177073, 38832.921370802193, -228474.30856749418, 1056510.2731746647, -3966350.6690692147, 12356051.556755034, -32447554.960801817, 72694202.08215563, -140263624.21419773, 234874417.82519162, -343436463.21586895, 440642459.98359567, 
                    -497902486.6124844, 496727125.55761147, -438159435.21823996, 341859025.27969146, -235749341.84939876, 143427881.92240053, -76743455.9965655, 35949258.4057005, -14649909.713685198, 5149491.8301937254, -1543359.8887202507, 388223.64875258581, -80152.997390282471, 13139.15397328689, -1620.5870774694545, 135.93814385248044, 
                    -5.9897501424606538, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 16.904506127577026, -380.11796572392581, 4464.5199103916984, -35429.335847081908, 209983.66756897856, -979960.86542410485, 3719573.7040945119, -11733956.759039678, 31242873.382677797, -71023112.19035387, 139077097.67425808, -236264721.03979972, 350197033.94123155, -454963496.49744225, 
                    519889663.55479729, -523833674.45034534, 466081060.54905367, -366361004.03002614, 254255279.96727309, -155519366.23507258, 83588702.2001631, -39302741.0236965, 16066063.178554848, -5661541.3544601407, 1700286.9391039079, -428391.690948307, 88558.172613842064, -14530.974149160336, 1793.5207177533098, -150.51753114482682, 
                    6.6341521827862584, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -15.312178421849284, 344.96611119492303, -4064.0319036419805, 32391.902958831743, -193097.12161809014, 907787.399138249, -3476458.1983163003, 11081925.243959213, -29856018.796200182, 68745503.715191513, -136439426.23983315, 234960367.59060419, -352932186.92752731, 464346850.51134825, 
                    -536845131.71322745, 546660978.33350813, -490975843.81072116, 389110208.61073965, -271965071.42955667, 167362850.44124767, -90416534.70065397, 42696011.46396289, -17515389.5997211, 6190319.3032452865, -1863493.1050712951, 470398.75813746, -97385.668468266857, 15997.416764119787, -1976.1507332134188, 165.93897574225042, 
                    -7.3164426948915349, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 13.918500080478649, -314.04093978771442, 3708.6889968541263, -29662.571845724015, 177650.93015724124, -840142.22101772355, 3240953.2084107092, -10421177.81965765, 28357841.908898719, -66028650.914850235, 132638650.92343418, -231318474.50217581, 351933165.9918943, -468874853.06572497, 
                    548592421.60787129, -564850182.44130373, 512444978.03417754, -409785262.47146904, 288678744.08344913, -178861760.9113645, 97192406.149763182, -46121631.352264576, 18998409.581442781, -6737179.4108963655, 2033698.0364387459, -514493.72834051034, 106698.45203281078, -17550.310492256205, 2170.0763656669096, -182.34490933524333, 
                    8.0431258890568, 0.0, 0.0
                 }, 
                { 
                    0.0, 0.0, -12.683463274218951, 286.52312593871062, -3390.3504334464328, 27192.843119226556, -163476.39509149524, 776871.38000448281, -3014981.1858349284, 9765117.1786596272, -26799179.918846793, 63006280.642674744, -127932389.11755551, 225702772.59755665, -347556963.85861617, 468736027.37582254, 
                    -555052157.2306484, 578090117.67243373, -530083670.93610048, 428029103.30734587, -304158215.35013843, 189893292.7216078, -103868503.43429217, 49567140.664937206, -20514294.80402492, 7303281.0021809535, -2211647.9492415413, 560952.07942535565, -116568.58265060189, 19203.461820481811, -2377.1874267593848, 199.90493908494943, 
                    -8.8219813826838287, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 11.576986611506507, -261.78750750241682, 3102.6280043647275, -24942.554227997603, 150415.09601156323, -717676.15098697192, 2799249.716800645, -9121828.9823041689, 25215430.850702457, -59781490.1354723, 122539955.46598832, -218459624.97930619, 340197439.68848842, -464211407.33755481, 
                    556251130.37004566, -586137928.99971879, 543497412.22795343, -443454159.16683167, 318124497.06210846, -200303488.97639677, 110380121.33553924, -53013271.812353261, 22060236.704301029, -7889433.6498696394, 2398094.3967130743, -610077.30671827134, 127078.82852530925, -20973.108978211021, 2599.7389993773559, -218.82326313536436, 
                    9.6624233276376916, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -10.575985375741658, 239.34857724899157, -2840.4491375160878, 22878.490199317803, -138324.45753715755, 662200.48140041612, -2593755.6520915395, 8495859.0942447148, -23630552.909493543, 56431598.203540735, -116641320.82890211, 209902337.83019447, -330257147.46124285, 455652424.69157404, 
                    -552322769.227406, 588838368.41890109, -552322769.227406, 455652424.69157404, -330257147.46124285, 209902337.83019447, -116641320.82890211, 56431598.203540735, -23630552.909493543, 8495859.0942447148, -2593755.6520915395, 662200.48140041612, -138324.45753715755, 22878.490199317803, -2840.4491375160878, 239.34857724899157, 
                    -10.575985375741658, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 9.6624233276376916, -218.82326313536436, 2599.7389993773559, -20973.108978211021, 127078.82852530925, -610077.30671827134, 2398094.3967130743, -7889433.6498696394, 22060236.704301029, -53013271.812353261, 110380121.33553924, -200303488.97639677, 318124497.06210846, -443454159.16683167, 
                    543497412.22795343, -586137928.99971879, 556251130.37004566, -464211407.33755481, 340197439.68848842, -218459624.97930619, 122539955.46598832, -59781490.1354723, 25215430.850702457, -9121828.9823041689, 2799249.716800645, -717676.15098697192, 150415.09601156323, -24942.554227997603, 3102.6280043647275, -261.78750750241682, 
                    11.576986611506507, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -8.8219813826838287, 199.90493908494943, -2377.1874267593848, 19203.461820481811, -116568.58265060189, 560952.07942535565, -2211647.9492415413, 7303281.0021809535, -20514294.80402492, 49567140.664937206, -103868503.43429217, 189893292.7216078, -304158215.35013843, 428029103.30734587, 
                    -530083670.93610048, 578090117.67243373, -555052157.2306484, 468736027.37582254, -347556963.85861617, 225702772.59755665, -127932389.11755551, 63006280.642674744, -26799179.918846793, 9765117.1786596272, -3014981.1858349284, 776871.38000448281, -163476.39509149524, 27192.843119226556, -3390.3504334464328, 286.52312593871062, 
                    -12.683463274218951, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 8.0431258890568, -182.34490933524333, 2170.0763656669096, -17550.310492256205, 106698.45203281078, -514493.72834051034, 2033698.0364387459, -6737179.4108963655, 18998409.581442781, -46121631.352264576, 97192406.149763182, -178861760.9113645, 288678744.08344913, -409785262.47146904, 
                    512444978.03417754, -564850182.44130373, 548592421.60787129, -468874853.06572497, 351933165.9918943, -231318474.50217581, 132638650.92343418, -66028650.914850235, 28357841.908898719, -10421177.81965765, 3240953.2084107092, -840142.22101772355, 177650.93015724124, -29662.571845724015, 3708.6889968541263, -314.04093978771442, 
                    13.918500080478649, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -7.3164426948915349, 165.93897574225042, -1976.1507332134188, 15997.416764119787, -97385.668468266857, 470398.75813746, -1863493.1050712951, 6190319.3032452865, -17515389.5997211, 42696011.46396289, -90416534.70065397, 167362850.44124767, -271965071.42955667, 389110208.61073965, 
                    -490975843.81072116, 546660978.33350813, -536845131.71322745, 464346850.51134825, -352932186.92752731, 234960367.59060419, -136439426.23983315, 68745503.715191513, -29856018.796200182, 11081925.243959213, -3476458.1983163003, 907787.399138249, -193097.12161809014, 32391.902958831743, -4064.0319036419805, 344.96611119492303, 
                    -15.312178421849284, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 6.6341521827862584, -150.51753114482682, 1793.5207177533098, -14530.974149160336, 88558.172613842064, -428391.690948307, 1700286.9391039079, -5661541.3544601407, 16066063.178554848, -39302741.0236965, 83588702.2001631, -155519366.23507258, 254255279.96727309, -366361004.03002614, 
                    466081060.54905367, -523833674.45034534, 519889663.55479729, -454963496.49744225, 350197033.94123155, -236264721.03979972, 139077097.67425808, -71023112.19035387, 31242873.382677797, -11733956.759039678, 3719573.7040945119, -979960.86542410485, 209983.66756897856, -35429.335847081908, 4464.5199103916984, -380.11796572392581, 
                    16.904506127577026, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -5.9897501424606538, 135.93814385248044, -1620.5870774694545, 13139.15397328689, -80152.997390282471, 388223.64875258581, -1543359.8887202507, 5149491.8301937254, -14649909.713685198, 35949258.4057005, -76743455.9965655, 143427881.92240053, -235749341.84939876, 341859025.27969146, 
                    -438159435.21823996, 496727125.55761147, -497902486.6124844, 440642459.98359567, -343436463.21586895, 234874417.82519162, -140263624.21419773, 72694202.08215563, -32447554.960801817, 12356051.556755034, -3966350.6690692147, 1056510.2731746647, -228474.30856749418, 38832.921370802193, -4920.6483875177073, 420.59184043677607, 
                    -18.749964844011664, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 5.3777379063340094, -122.0799336832394, 1455.9834689895019, -11811.742411977693, 72114.858411011592, -349670.08840273391, 1392029.5414431067, -4652723.0471756179, 13265504.01932252, -32639323.379858445, 69905026.511970192, -131163292.23384815, 216613079.11044252, -315888458.3823061, 
                    407592343.86822951, -465728765.58546436, 471142566.04339546, -421411777.20776141, 332449492.03644395, -230468335.39215162, 139697397.56218004, -73559709.826424465, 33374979.58135885, -12915877.505448371, 4209535.1800968852, -1136682.7556055584, 248692.18685489619, -42670.479987019382, 5446.090048510986, -467.88547238039621, 
                    20.924835278416268, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -4.7934171771462459, 108.83926264422084, -1298.5315976205823, 10539.850160248767, -64394.952663565287, 312528.26618912612, -1245655.1478998181, 4169757.7885190602, -11910829.107072841, 29374023.5433866, -63089690.082938723, 118782839.45620713, -196982577.8856954, 288697333.265935, 
                    -374736662.43019927, 431238682.76548058, -439934463.16300744, 397405780.0887531, -317142142.5367415, 222791192.41898566, -137088364.55059448, 73398390.2559841, -33904085.721237853, 13366225.352268066, -4436645.9215920968, 1218597.0947722602, -270641.40515700018, 47016.646953273237, -6058.7807326170032, 524.100362784811, 
                    -23.539532651323352, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 4.2327332334391565, -96.126421083153218, 1147.2063242698637, -9315.6816673442881, 56949.959839605777, -276614.79077494796, 1103638.6715289683, -3699130.8622331871, 10583498.884206319, -26152533.76376906, 56307658.7350197, -106329601.25368364, 176968653.77500153, -260500226.18418652, 
                    339921356.6454494, -393657587.14397573, 404651763.33169836, -368855294.27564764, 297535413.47691578, -211679119.86090901, 132187724.59912263, -71985471.793509081, 33891902.838930458, -13642073.637999034, 4627363.9874990676, -1298324.6318633161, 294039.143602792, -51941.070419473894, 6782.2012853895631, -592.273830944952, 
                    26.76053366351865, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -3.69215689741661, 83.863124114649892, -1001.1091322423641, 8132.3576570299838, -49741.257433080034, 241763.58251724468, -965424.75918032858, 3239418.3053486324, -9280925.0727537535, 22972709.859802015, -49564627.399869792, 93835538.145012751, -156661236.66292211, 231482111.18308157, 
                    -303447106.42238867, 353378881.55482095, -365702756.54336107, 336074987.80292684, -273765503.22802216, 197077967.50953111, -124817259.8692572, 69118369.561415642, -33185837.378124841, 13661372.05462601, -4750822.9099445473, 1368403.0156487161, -317964.13702619454, 57472.840752949087, -7646.3122839752932, 676.94678267241375, 
                    -30.852079138799564, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 3.1686040305159597, -71.980812197921139, 859.45028796199188, -6983.7981230481155, 42734.427171181087, -207824.79925882129, 830502.60867926036, -2789266.3823445463, 8000467.1670445837, -19831613.692179531, 42863165.396444887, -81324360.756282136, 136133894.44200146, -201803386.18150809, 
                    265588868.55194372, -310785204.34763241, 323520052.58892357, -299450872.36152011, 246078776.26185718, -179053679.36129582, 114894103.69871274, -64645194.961490631, 31643746.350190923, -13332386.642319422, 4764974.8840314429, -1415893.5599137635, 340155.29730437271, -63501.401670644867, 8685.71641598814, -785.16649587742927, 
                    36.262724009152549, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -2.659411430605366, 60.420199478193084, -721.54526943864073, 5864.7141622637455, -35899.372048754136, 174666.41104977272, -698416.99851444154, 2347446.4597883048, -6739643.837508061, 16726167.477049446, -36204370.381681457, 68814955.3650791, -115449585.71997452, 171607449.46610555, 
                    -226602997.79556146, 266251532.8496674, -278557200.48993027, 259432200.35896876, -214825815.70747072, 157796840.71529883, -102449120.41699043, 58491632.9227564, -29158654.310134314, 12568245.025528425, -4620736.9208444124, 1421465.4769638979, -355830.74182696041, 69510.553765821081, -9925.0214248022185, 928.26793712860194, 
                    -43.827436606519306, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 2.1624683622862704, -49.134334961910774, 586.85271073782189, -4770.9323383501123, 29212.430358476835, -142185.26657421992, 568816.26249416708, -1913030.5319139068, 5496681.29009558, -13654626.399772281, 29591277.302597627, -56328181.052540369, 94672279.110227361, -141037562.76120484, 
                    186747664.87506393, -220165193.50723395, 231303761.61977202, -216539656.85929462, 180465680.12520823, -133629680.75223014, 87642340.994275078, -50684709.81254185, 25684506.300539624, -11306755.426683655, 4272082.989448675, -1361768.8444458782, 356982.04278297897, -73941.522498926061, 11308.353420195403, -1124.5998122191788, 
                    55.326838122865624, 0.0, 0.0
                 }, 
                { 
                    0.0, 0.0, -1.677083043360561, 38.108348662205181, -455.21136032863211, 3701.3339373433369, -22668.354043739007, 110366.03496950791, -441691.1830551271, 1486206.7623327882, -4272895.2903864654, 10622597.739499096, -23042119.653858114, 43912474.098357886, -73910398.13488698, 110301681.13404918, 
                    -146367841.01450339, 173023485.91575611, -182383345.92395002, 171451595.22186813, -143633540.2916114, 107056113.79393096, -70801698.483853713, 41386292.468620434, -21266159.927800104, 9534555.401509745, -3691596.6661424763, 1216468.2760599274, -333907.06364859274, 73805.0423440314, -12383.813104719478, 1397.3476528565643, 
                    -75.517639999346017, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 1.211357255208968, -27.527083507714739, 328.84360005878364, -2674.1587045587607, 16380.266167858868, -79768.54042741211, 319328.64358013583, -1074866.5247889231, 3091673.9526090506, -7690358.8293313729, 16693267.536814084, -31840612.527362764, 53648969.769072331, -80169407.891436189, 
                    106555941.24701199, -126215136.37574629, 133376580.2512051, -125775727.14792588, 105785124.15990567, -79241894.717591852, 52744103.904108167, -31088511.758470722, 16150374.936495388, -7347580.1752377357, 2902167.4772759378, -983403.21713141177, 281038.69942787581, -66007.937802487621, 12202.610354649221, -1630.5491606279561, 
                    124.75642068063411, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -0.49269307512313804, 11.196320671359191, -133.75874396190264, 1087.7913668002186, -6663.6857798537421, 32454.254011571506, -129938.48708823571, 437452.21807288181, -1258535.4329212562, 3131393.4535358991, -6799552.5055573005, 12974879.798183598, -21873046.209959485, 32706621.6725285, 
                    -43506088.800806478, 51583684.492691852, -54577607.03405603, 51546692.867506236, -43438285.531887561, 32619473.322621066, -21780941.113789909, 12891380.186125349, -6733723.1466578823, 3086085.1351395724, -1231319.6050326868, 423223.02561871341, -123492.95787966656, 29941.259858434238, -5827.9921618888193, 853.6537696012424, 
                    -79.422730605916769, 1.0, 1.0
                 }
             };
            private static readonly double[,] matrix_35 = new double[,] { 
                { 
                    0.0, 0.028571428571428571, -0.43270753762723047, 3.8758405311722943, -25.208525774723579, 128.64905374844025, -536.89754306296163, 1880.3707036384196, -5625.5612357141008, 14562.334238455649, -32931.090384253759, 65533.972393513279, -115411.23477431435, 180638.1611536516, -252080.39454506649, 314359.25907627906, 
                    -350839.49235682655, 350670.797365275, -313909.01947716059, 251487.40726102324, -180057.42467504748, 114951.55709777829, -65232.437046351588, 32766.986196756639, -14489.679769071654, 5600.8977840214038, -1875.187078571367, 537.24877246811991, -129.59595867037007, 25.729810749688053, -4.0658912539876741, 0.48448006639653679, 
                    -0.039337048875202771, 0.0016806722689075631, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -0.0016806722689075631, 0.039337048875202771, -0.48448006639653679, 4.0658912539876741, -25.729810749688053, 129.59595867037007, -537.24877246811991, 1875.187078571367, -5600.8977840214038, 14489.679769071654, -32766.986196756639, 65232.437046351588, -114951.55709777829, 180057.42467504748, 
                    -251487.40726102324, 313909.01947716059, -350670.797365275, 350839.49235682655, -314359.25907627906, 252080.39454506649, -180638.1611536516, 115411.23477431435, -65533.972393513279, 32931.090384253759, -14562.334238455649, 5625.5612357141008, -1880.3707036384196, 536.89754306296163, -128.64905374844025, 25.208525774723579, 
                    -3.8758405311722943, 0.43270753762723047, -0.028571428571428571, 0.0
                 }, { 
                    1.0, 1.0, -83.915826780113377, 931.86393281847313, -6574.8861424149572, 34940.341248889286, -149231.47702746195, 530245.60361601447, -1601570.1768304706, 4173293.7786508664, -9482118.2808723636, 18935307.640294515, -33433692.796449188, 52433488.185105614, -73283577.496949926, 91499269.152728036, 
                    -102214937.28706376, 102243617.74950232, -91581125.952978253, 73406208.874799222, -52577663.501827821, 33577334.052400962, -19059422.314774591, 9575820.2150370367, -4235201.6209523836, 1637322.8563735981, -548239.97692445642, 157087.277709241, -37895.551066293105, 7524.1526145900207, -1189.0364186433694, 141.68660024592333, 
                    -11.504399268249063, 0.49153106703899518, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 131.73493692746786, -1777.7241483939774, 13748.891811970268, -76936.832033278639, 339242.63383989345, -1230862.5982194445, 3771448.0650263713, -9928001.9358257, 22725374.351249617, -45633038.690751262, 80912033.179130957, -127303506.86032926, 178374243.40937829, -223154174.63560653, 
                    249681781.4697285, -250067469.31702992, 224217081.34784546, -179867578.47743165, 128917387.71878925, -82374439.656550169, 46778710.1106212, -23510995.730459634, 10401509.220941521, -4022162.8997225654, 1347035.5808192443, -386025.28004315693, 93135.531838303723, -18493.773326215247, 2922.7659128573055, -348.29746787143694, 
                    28.281415223451155, -1.2083642558445338, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -79.759916829325931, 1521.0469537082636, -13925.179472895476, 85849.668148655968, -402272.46004295378, 1519787.7468802489, -4789192.0703115286, 12862852.151615387, -29880673.88201781, 60667803.049670607, -108480164.67457932, 171793051.01956153, -241940979.998358, 303899369.430057, 
                    -341117181.89676976, 342523455.2537536, -307752901.83360386, 247295170.91194385, -177487129.73540655, 113535166.13548119, -64532641.777629212, 32458063.71637268, -14368402.042777376, 5558823.5259763664, -1862397.7584460126, 533881.82519221737, -128840.46407756759, 25588.567827957075, -4044.6218603785965, 482.037330174229, 
                    -39.143854954905507, 1.6725543014633579, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 58.45261647961366, -1224.0451437443649, 12701.580651379618, -85865.94603770251, 429264.21623295423, -1698026.5558205824, 5531705.4388184724, -15225705.474714605, 36025940.8721877, -74176319.177640647, 134076161.91508467, -214126958.41298622, 303573594.22738779, -383337014.39280695, 
                    432108685.61549771, -435371748.14818007, 392256468.13224852, -315905122.39728588, 227143546.12566596, -145515896.65444338, 82810836.051774159, -41692769.531616025, 18471278.567135852, -7150814.4571013246, 2397034.0298363785, -687432.67268298892, 165951.89255866964, -32967.695450370811, 5212.02029119311, -621.257321648992, 
                    50.454248781942304, -2.1559626101445373, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -46.321380247043749, 1010.5795010343049, -11145.084407768758, 80643.7950965814, -427257.49523601925, 1769367.1267088547, -5971861.2424044088, 16891732.409294046, -40820566.493968457, 85442232.32848531, -156447461.66823071, 252421534.103702, -360790147.80597979, 458570009.75052208, 
                    -519639572.85313779, 525798225.83417052, -475370640.70063227, 383923253.49669152, -276687284.65437984, 177589732.72738433, -101219473.30295701, 51025187.5989648, -22629066.046749081, 8767729.0264087189, -2941019.7214505393, 843893.65454381483, -203809.63518510415, 40501.861003126309, -6404.7440306093895, 763.5672107721083, 
                    -62.019580425178425, 2.6503706231109105, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 38.344289982623522, -855.12141257355825, 9754.7928804116236, -73648.92391175493, 408078.00091688114, -1760072.5384719851, 6148072.2829870824, -17885630.593273379, 44213493.25585182, -94245012.172811553, 175109242.525553, -285879609.78549659, 412518317.64345247, -528374959.53507012, 
                    602504909.95927, -612768914.33639848, 556319030.535509, -450838984.4010691, 325824857.08759254, -209609962.25542459, 119695341.15079086, -60432192.090430573, 26834766.847348105, -10407906.38246344, 3494087.6831798949, -1003255.7335476915, 242424.58199498, -48195.268232067749, 7623.7045035593947, -909.09730810824169, 
                    73.851653364868866, -3.1563151203205022, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -32.641460778224776, 737.6397586851208, -8590.6056632978289, 66662.043293774943, -381320.35686699935, 1699603.6477930993, -6121994.9423745964, 18297587.68753593, -46283517.38271594, 100569115.92814215, -189848636.71989855, 314017749.75500208, -458009504.94366825, 591835437.98131335, 
                    -679773942.24028647, 695484274.60329568, -634517473.41635621, 516291045.29229754, -374370553.78040516, 241500815.7120325, -138217155.32289627, 69912642.96627754, -31091512.162766524, 12073747.809076369, -4057374.78164512, 1165923.0278528179, -281909.48806906573, 56072.7002834794, -8873.1091529263413, 1058.3745973629332, 
                    -85.99470245777114, 3.6757244883399425, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 28.331387564934214, -645.78336116940727, 7623.8720126611843, -60266.468419003351, 352638.91081083677, -1611947.065364592, 5957932.6484312639, -18248953.456953295, 47193533.515272178, -104549755.67936683, 200660109.52449569, -336582293.72980815, 496724028.933699, -648190981.66412389, 
                    750610186.30417407, -773189567.74139869, 709398118.13892746, -579923737.92187381, 422142776.67912173, -273191387.23013759, 156768338.52236071, -79468891.629793778, 35404485.593309365, -13768634.804052792, 4632402.7313106237, -1332424.0768856395, 322410.00312079728, -64165.938112232841, 10158.329287717341, -1212.0699414971623, 
                    98.504941169895446, -4.2110468268623649, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -24.940523936800361, 571.87940553496139, -6815.3448604624491, 54582.790955728648, -324683.2704340755, 1512956.5477593776, -5710050.060703801, 17864937.596031852, -47151739.433202669, 106432907.38891342, -207715089.0644646, 353532000.69353324, -528320567.2591024, 696822737.38026667, 
                    -814246172.74301589, 845139250.59786987, -780370040.04819286, 641351460.63846028, -468937282.02977884, 304598430.34753156, -175327489.38099363, 89102205.090831384, -39779015.3153867, 15496239.031168424, -5220862.5076411581, 1503354.1290209035, -364091.60467457981, 72511.30827681128, -11485.530566672471, 1370.9556827836343, 
                    -111.44721273590466, 4.7651093902541417, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 22.189914691233987, -510.98622834096665, 6131.2261720010065, -49573.508451334812, 298519.6957225884, -1411694.1764960538, 5417599.99433756, -17255557.148077022, 46376027.009253547, -106532065.10787535, 211326892.51856631, -365022735.22502416, 552660017.8530823, -737265586.17219687, 
                    869991448.250859, -910594756.35250962, 846808403.07386482, -700144769.96968579, 514515039.99935693, -335618154.35588729, 193863877.22464919, -98810717.754909486, 44219799.331846647, -17260279.497802105, 5824552.10403223, -1679362.419207552, 407137.62261814257, -81149.474433804629, 12861.663483302284, -1535.9066443608606, 
                    124.89519472594817, -5.3411306458914325, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -19.903820358534137, 459.79949970659874, -5545.0900740372454, 45155.968866633753, -274464.66441832978, 1312887.9973108226, -5106178.5718740029, 16506807.183511045, -45066592.76794444, 105184878.1343558, -211907217.84114766, 371382121.949563, -569805373.03601885, 769225193.31087029, 
                    -917251719.15918612, 968834680.68200588, -908053633.35761786, 755822937.47786856, -558593700.61636972, 366119845.05373645, -212333832.33109191, 108587822.68358952, -48730340.719731726, 19064372.076708868, -6445349.8442827817, 1861151.0346437225, -451750.38235371321, 90125.905123683187, -14294.568263101204, 1707.9154682622766, 
                    -138.93280886217957, 5.9427849454373725, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 17.96568958594921, -416.03354714682695, 5036.7783381567633, -41242.678222524686, 252500.10657497327, -1218772.6936231363, 4791378.3272123458, -15680481.142588709, 43390393.061486982, -102716286.20627862, 209918306.16052181, -373072447.77141434, 580011227.12674582, -792592339.68248689, 
                    955552666.55717671, -1019172552.6815881, 963416884.67069888, -807849930.753777, 600840195.15293241, -395939535.4276799, 230676936.89602849, -118420427.95756221, 53312336.8318956, -20911874.336894032, 7085192.0275456933, -2049476.8781868168, 498153.5340827672, -99491.655848070048, 15793.13994239444, -1888.1161123750799, 
                    153.65635143132192, -6.5742995658945755, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -16.2950098029993, 378.05974361034157, -4590.9379872057925, 37754.529695685, -232470.77929283728, 1130217.2774068317, -4482178.89332923, 14818537.041948214, -41477032.820888914, 99413555.35197936, -205827692.96232724, 370644566.96757621, -583700294.34108722, 807449653.61983311, 
                    -984564682.21183157, 1060980578.5587645, -1012191447.8617947, 855633655.13663864, -640864083.646869, 424873223.61148709, -248811602.31946138, 128286828.38106443, -57964905.625688747, 22805682.138305292, -7746041.8547727466, 2245153.5924068647, -546594.97108334058, 109304.3775683042, -17367.544022092392, 2077.8147751099727, 
                    -169.1773096922856, 7.240583322893122, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 14.83428418450301, -344.68730356995496, 4195.79198846943, -34624.00205361846, 214176.02482878993, -1047368.8809160953, 4183407.3504666132, -13948289.672097303, 39422176.281291068, -95514804.352423862, 200072455.03374529, -364688681.57423955, 581427194.99253035, -814066854.56055653, 
                    1004124717.8045199, -1093717947.8150611, 1053670566.0278395, -898529303.9205637, 678212047.2299217, -452669582.77799255, 266629817.02536029, -138154016.21818629, 62683557.139262892, -24747943.045844287, 8429839.714445835, -2449051.3954604189, 597350.06469499075, -119629.54152501248, 19029.487343735993, -2278.5294120229905, 
                    185.62599564449914, -7.9473933852252667, 0.0, 0.0
                 }, 
                { 
                    0.0, 0.0, -13.541298176856484, 315.02573511091543, -3842.2108568611757, 31794.789421333473, -197411.18005235842, 970009.89098317071, -3897359.9775746721, 13086836.10602585, -37293897.429982536, 91208331.422932908, -193037078.22304136, 355789452.11927032, -573832981.73550963, 812882061.15535474, 
                    -1014250882.9946548, 1116961424.0461714, -1087171994.9693515, 935848131.41104925, -712364439.92989874, 479022402.25402308, -283990946.77448505, 147974264.4148978, -67458809.382530391, 26739645.9180634, -9138423.1366439667, 2662092.51646943, -650724.96848337888, 130541.89491517015, -20792.555725563358, 2492.0401349626532, 
                    -203.15623885339727, 8.7015521613332165, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 12.384318782602183, -288.39578395266881, 3523.0345272475488, -29220.330840133433, 181985.07529927342, -897753.08637838718, 3624825.8580469885, -12244371.501532845, 35139217.242594413, -86637980.043353975, 185044609.03642035, -344491901.27095455, 561596844.6429776, -804469325.0034709, 
                    1015145383.6416516, -1130434598.9035318, 1112068869.3589165, -966873109.25825262, 742735293.10148108, -503563330.02418727, 300714578.94359714, -157680813.94648069, 72274325.307354763, -28780032.530978169, 9873400.0289690476, -2885238.8644848592, 707059.61460320221, -142127.16583543742, 22672.635147007153, -2720.4538913147385, 
                    221.95150000357981, -9.5112321005373559, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -11.339001669364306, 264.27013519030413, -3232.5808403102083, 26862.165913755412, -167726.25884124116, 830146.712116772, -3365719.4091645088, 11426524.66310537, -32989687.098507471, 81910743.433100641, -176357295.71115014, 331280882.82142889, -545392631.27607965, 789496114.46798921, 
                    -1007182664.8497106, 1134030898.4462507, -1127825012.6214094, 990882651.49296987, -768677614.80254185, 525855928.55172569, -316572584.38538313, 167182510.56017423, -77104418.169884413, 30865756.573690958, -10635951.010987874, 3119466.6419006349, -766729.73580372555, 154484.02313617553, -24688.43854346696, 2966.2882224822188, 
                    -242.23292960319415, 10.386334591184294, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 10.386334591184294, -242.23292960319415, 2966.2882224822188, -24688.43854346696, 154484.02313617553, -766729.73580372555, 3119466.6419006349, -10635951.010987874, 30865756.573690958, -77104418.169884413, 167182510.56017423, -316572584.38538313, 525855928.55172569, -768677614.80254185, 
                    990882651.49296987, -1127825012.6214094, 1134030898.4462507, -1007182664.8497106, 789496114.46798921, -545392631.27607965, 331280882.82142889, -176357295.71115014, 81910743.433100641, -32989687.098507471, 11426524.66310537, -3365719.4091645088, 830146.712116772, -167726.25884124116, 26862.165913755412, -3232.5808403102083, 
                    264.27013519030413, -11.339001669364306, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -9.5112321005373559, 221.95150000357981, -2720.4538913147385, 22672.635147007153, -142127.16583543742, 707059.61460320221, -2885238.8644848592, 9873400.0289690476, -28780032.530978169, 72274325.307354763, -157680813.94648069, 300714578.94359714, -503563330.02418727, 742735293.10148108, 
                    -966873109.25825262, 1112068869.3589165, -1130434598.9035318, 1015145383.6416516, -804469325.0034709, 561596844.6429776, -344491901.27095455, 185044609.03642035, -86637980.043353975, 35139217.242594413, -12244371.501532845, 3624825.8580469885, -897753.08637838718, 181985.07529927342, -29220.330840133433, 3523.0345272475488, 
                    -288.39578395266881, 12.384318782602183, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 8.7015521613332165, -203.15623885339727, 2492.0401349626532, -20792.555725563358, 130541.89491517015, -650724.96848337888, 2662092.51646943, -9138423.1366439667, 26739645.9180634, -67458809.382530391, 147974264.4148978, -283990946.77448505, 479022402.25402308, -712364439.92989874, 
                    935848131.41104925, -1087171994.9693515, 1116961424.0461714, -1014250882.9946548, 812882061.15535474, -573832981.73550963, 355789452.11927032, -193037078.22304136, 91208331.422932908, -37293897.429982536, 13086836.10602585, -3897359.9775746721, 970009.89098317071, -197411.18005235842, 31794.789421333473, -3842.2108568611757, 
                    315.02573511091543, -13.541298176856484, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -7.9473933852252667, 185.62599564449914, -2278.5294120229905, 19029.487343735993, -119629.54152501248, 597350.06469499075, -2449051.3954604189, 8429839.714445835, -24747943.045844287, 62683557.139262892, -138154016.21818629, 266629817.02536029, -452669582.77799255, 678212047.2299217, 
                    -898529303.9205637, 1053670566.0278395, -1093717947.8150611, 1004124717.8045199, -814066854.56055653, 581427194.99253035, -364688681.57423955, 200072455.03374529, -95514804.352423862, 39422176.281291068, -13948289.672097303, 4183407.3504666132, -1047368.8809160953, 214176.02482878993, -34624.00205361846, 4195.79198846943, 
                    -344.68730356995496, 14.83428418450301, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 7.240583322893122, -169.1773096922856, 2077.8147751099727, -17367.544022092392, 109304.3775683042, -546594.97108334058, 2245153.5924068647, -7746041.8547727466, 22805682.138305292, -57964905.625688747, 128286828.38106443, -248811602.31946138, 424873223.61148709, -640864083.646869, 
                    855633655.13663864, -1012191447.8617947, 1060980578.5587645, -984564682.21183157, 807449653.61983311, -583700294.34108722, 370644566.96757621, -205827692.96232724, 99413555.35197936, -41477032.820888914, 14818537.041948214, -4482178.89332923, 1130217.2774068317, -232470.77929283728, 37754.529695685, -4590.9379872057925, 
                    378.05974361034157, -16.2950098029993, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -6.5742995658945755, 153.65635143132192, -1888.1161123750799, 15793.13994239444, -99491.655848070048, 498153.5340827672, -2049476.8781868168, 7085192.0275456933, -20911874.336894032, 53312336.8318956, -118420427.95756221, 230676936.89602849, -395939535.4276799, 600840195.15293241, 
                    -807849930.753777, 963416884.67069888, -1019172552.6815881, 955552666.55717671, -792592339.68248689, 580011227.12674582, -373072447.77141434, 209918306.16052181, -102716286.20627862, 43390393.061486982, -15680481.142588709, 4791378.3272123458, -1218772.6936231363, 252500.10657497327, -41242.678222524686, 5036.7783381567633, 
                    -416.03354714682695, 17.96568958594921, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 5.9427849454373725, -138.93280886217957, 1707.9154682622766, -14294.568263101204, 90125.905123683187, -451750.38235371321, 1861151.0346437225, -6445349.8442827817, 19064372.076708868, -48730340.719731726, 108587822.68358952, -212333832.33109191, 366119845.05373645, -558593700.61636972, 
                    755822937.47786856, -908053633.35761786, 968834680.68200588, -917251719.15918612, 769225193.31087029, -569805373.03601885, 371382121.949563, -211907217.84114766, 105184878.1343558, -45066592.76794444, 16506807.183511045, -5106178.5718740029, 1312887.9973108226, -274464.66441832978, 45155.968866633753, -5545.0900740372454, 
                    459.79949970659874, -19.903820358534137, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -5.3411306458914325, 124.89519472594817, -1535.9066443608606, 12861.663483302284, -81149.474433804629, 407137.62261814257, -1679362.419207552, 5824552.10403223, -17260279.497802105, 44219799.331846647, -98810717.754909486, 193863877.22464919, -335618154.35588729, 514515039.99935693, 
                    -700144769.96968579, 846808403.07386482, -910594756.35250962, 869991448.250859, -737265586.17219687, 552660017.8530823, -365022735.22502416, 211326892.51856631, -106532065.10787535, 46376027.009253547, -17255557.148077022, 5417599.99433756, -1411694.1764960538, 298519.6957225884, -49573.508451334812, 6131.2261720010065, 
                    -510.98622834096665, 22.189914691233987, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 4.7651093902541417, -111.44721273590466, 1370.9556827836343, -11485.530566672471, 72511.30827681128, -364091.60467457981, 1503354.1290209035, -5220862.5076411581, 15496239.031168424, -39779015.3153867, 89102205.090831384, -175327489.38099363, 304598430.34753156, -468937282.02977884, 
                    641351460.63846028, -780370040.04819286, 845139250.59786987, -814246172.74301589, 696822737.38026667, -528320567.2591024, 353532000.69353324, -207715089.0644646, 106432907.38891342, -47151739.433202669, 17864937.596031852, -5710050.060703801, 1512956.5477593776, -324683.2704340755, 54582.790955728648, -6815.3448604624491, 
                    571.87940553496139, -24.940523936800361, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -4.2110468268623649, 98.504941169895446, -1212.0699414971623, 10158.329287717341, -64165.938112232841, 322410.00312079728, -1332424.0768856395, 4632402.7313106237, -13768634.804052792, 35404485.593309365, -79468891.629793778, 156768338.52236071, -273191387.23013759, 422142776.67912173, 
                    -579923737.92187381, 709398118.13892746, -773189567.74139869, 750610186.30417407, -648190981.66412389, 496724028.933699, -336582293.72980815, 200660109.52449569, -104549755.67936683, 47193533.515272178, -18248953.456953295, 5957932.6484312639, -1611947.065364592, 352638.91081083677, -60266.468419003351, 7623.8720126611843, 
                    -645.78336116940727, 28.331387564934214, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 3.6757244883399425, -85.99470245777114, 1058.3745973629332, -8873.1091529263413, 56072.7002834794, -281909.48806906573, 1165923.0278528179, -4057374.78164512, 12073747.809076369, -31091512.162766524, 69912642.96627754, -138217155.32289627, 241500815.7120325, -374370553.78040516, 
                    516291045.29229754, -634517473.41635621, 695484274.60329568, -679773942.24028647, 591835437.98131335, -458009504.94366825, 314017749.75500208, -189848636.71989855, 100569115.92814215, -46283517.38271594, 18297587.68753593, -6121994.9423745964, 1699603.6477930993, -381320.35686699935, 66662.043293774943, -8590.6056632978289, 
                    737.6397586851208, -32.641460778224776, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -3.1563151203205022, 73.851653364868866, -909.09730810824169, 7623.7045035593947, -48195.268232067749, 242424.58199498, -1003255.7335476915, 3494087.6831798949, -10407906.38246344, 26834766.847348105, -60432192.090430573, 119695341.15079086, -209609962.25542459, 325824857.08759254, 
                    -450838984.4010691, 556319030.535509, -612768914.33639848, 602504909.95927, -528374959.53507012, 412518317.64345247, -285879609.78549659, 175109242.525553, -94245012.172811553, 44213493.25585182, -17885630.593273379, 6148072.2829870824, -1760072.5384719851, 408078.00091688114, -73648.92391175493, 9754.7928804116236, 
                    -855.12141257355825, 38.344289982623522, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 2.6503706231109105, -62.019580425178425, 763.5672107721083, -6404.7440306093895, 40501.861003126309, -203809.63518510415, 843893.65454381483, -2941019.7214505393, 8767729.0264087189, -22629066.046749081, 51025187.5989648, -101219473.30295701, 177589732.72738433, -276687284.65437984, 
                    383923253.49669152, -475370640.70063227, 525798225.83417052, -519639572.85313779, 458570009.75052208, -360790147.80597979, 252421534.103702, -156447461.66823071, 85442232.32848531, -40820566.493968457, 16891732.409294046, -5971861.2424044088, 1769367.1267088547, -427257.49523601925, 80643.7950965814, -11145.084407768758, 
                    1010.5795010343049, -46.321380247043749, 0.0, 0.0
                 }, 
                { 
                    0.0, 0.0, -2.1559626101445373, 50.454248781942304, -621.257321648992, 5212.02029119311, -32967.695450370811, 165951.89255866964, -687432.67268298892, 2397034.0298363785, -7150814.4571013246, 18471278.567135852, -41692769.531616025, 82810836.051774159, -145515896.65444338, 227143546.12566596, 
                    -315905122.39728588, 392256468.13224852, -435371748.14818007, 432108685.61549771, -383337014.39280695, 303573594.22738779, -214126958.41298622, 134076161.91508467, -74176319.177640647, 36025940.8721877, -15225705.474714605, 5531705.4388184724, -1698026.5558205824, 429264.21623295423, -85865.94603770251, 12701.580651379618, 
                    -1224.0451437443649, 58.45261647961366, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 1.6725543014633579, -39.143854954905507, 482.037330174229, -4044.6218603785965, 25588.567827957075, -128840.46407756759, 533881.82519221737, -1862397.7584460126, 5558823.5259763664, -14368402.042777376, 32458063.71637268, -64532641.777629212, 113535166.13548119, -177487129.73540655, 
                    247295170.91194385, -307752901.83360386, 342523455.2537536, -341117181.89676976, 303899369.430057, -241940979.998358, 171793051.01956153, -108480164.67457932, 60667803.049670607, -29880673.88201781, 12862852.151615387, -4789192.0703115286, 1519787.7468802489, -402272.46004295378, 85849.668148655968, -13925.179472895476, 
                    1521.0469537082636, -79.759916829325931, 0.0, 0.0
                 }, { 
                    0.0, 0.0, -1.2083642558445338, 28.281415223451155, -348.29746787143694, 2922.7659128573055, -18493.773326215247, 93135.531838303723, -386025.28004315693, 1347035.5808192443, -4022162.8997225654, 10401509.220941521, -23510995.730459634, 46778710.1106212, -82374439.656550169, 128917387.71878925, 
                    -179867578.47743165, 224217081.34784546, -250067469.31702992, 249681781.4697285, -223154174.63560653, 178374243.40937829, -127303506.86032926, 80912033.179130957, -45633038.690751262, 22725374.351249617, -9928001.9358257, 3771448.0650263713, -1230862.5982194445, 339242.63383989345, -76936.832033278639, 13748.891811970268, 
                    -1777.7241483939774, 131.73493692746786, 0.0, 0.0
                 }, { 
                    0.0, 0.0, 0.49153106703899518, -11.504399268249063, 141.68660024592333, -1189.0364186433694, 7524.1526145900207, -37895.551066293105, 157087.277709241, -548239.97692445642, 1637322.8563735981, -4235201.6209523836, 9575820.2150370367, -19059422.314774591, 33577334.052400962, -52577663.501827821, 
                    73406208.874799222, -91581125.952978253, 102243617.74950232, -102214937.28706376, 91499269.152728036, -73283577.496949926, 52433488.185105614, -33433692.796449188, 18935307.640294515, -9482118.2808723636, 4173293.7786508664, -1601570.1768304706, 530245.60361601447, -149231.47702746195, 34940.341248889286, -6574.8861424149572, 
                    931.86393281847313, -83.915826780113377, 1.0, 1.0
                 }
             };
            private static readonly double[,] matrix_5 = new double[,] { { 0.0, 0.2, -0.24737351968108265, 0.1, 0.0, 0.0 }, { 0.0, 0.0, -0.1, 0.24737351968108265, -0.2, 0.0 }, { 1.0, 1.0, -1.7488041807994832, 0.74737351968108268, 0.0, 0.0 }, { 0.0, 0.0, 4.0759471808487291, -2.0745165197303286, 0.0, 0.0 }, { 0.0, 0.0, -2.0745165197303286, 4.0759471808487291, 0.0, 0.0 }, { 0.0, 0.0, 0.74737351968108268, -1.7488041807994832, 1.0, 1.0 } };
            private static readonly double[,] matrix_6 = new double[,] { { 0.0, 0.16666666666666666, -0.29378226340479052, 0.22033669755359289, -0.066666666666666666, 0.0, 0.0 }, { 0.0, 0.0, 0.066666666666666666, -0.22033669755359289, 0.29378226340479052, -0.16666666666666666, 0.0 }, { 1.0, 1.0, -2.7635315913925784, 2.2429853910980269, -0.69378226340479054, 0.0, 0.0 }, { 0.0, 0.0, 5.6555102666721329, -5.6177580995847789, 1.8348338661075723, 0.0, 0.0 }, { 0.0, 0.0, -3.0330302779823359, 7.7495454169735041, -3.0330302779823359, 0.0, 0.0 }, { 0.0, 0.0, 1.8348338661075723, -5.6177580995847789, 5.6555102666721329, 0.0, 0.0 }, { 0.0, 0.0, -0.69378226340479054, 2.2429853910980269, -2.7635315913925784, 1.0, 1.0 } };
            private static readonly double[,] matrix_7 = new double[,] { { 0.0, 0.14285714285714285, -0.32357945637702518, 0.34614019886067621, -0.19414767382621512, 0.047619047619047616, 0.0, 0.0 }, { 0.0, 0.0, -0.047619047619047616, 0.19414767382621512, -0.34614019886067621, 0.32357945637702518, -0.14285714285714285, 0.0 }, { 1.0, 1.0, -3.88693281957899, 4.5809147231275515, -2.6497284620366415, 0.65691278971035849, 0.0, 0.0 }, { 0.0, 0.0, 7.4053040789526339, -10.803497580839934, 6.6856145062220467, -1.6970275208467061, 0.0, 0.0 }, { 0.0, 0.0, -4.1077443232119029, 12.897457104331421, -9.7107602908044424, 2.6294877949746067, 0.0, 0.0 }, { 0.0, 0.0, 2.6294877949746067, -9.7107602908044424, 12.897457104331421, -4.1077443232119029, 0.0, 0.0 }, { 0.0, 0.0, -1.6970275208467061, 6.6856145062220467, -10.803497580839934, 7.4053040789526339, 0.0, 0.0 }, { 0.0, 0.0, 0.65691278971035849, -2.6497284620366415, 4.5809147231275515, -3.88693281957899, 1.0, 1.0 } };
            private static readonly double[,] matrix_8 = new double[,] { { 0.0, 0.125, -0.34419955253075951, 0.47349967525956188, -0.3787997402076495, 0.17209977626537976, -0.035714285714285712, 0.0, 0.0 }, { 0.0, 0.0, 0.035714285714285712, -0.17209977626537976, 0.3787997402076495, -0.47349967525956188, 0.34419955253075951, -0.125, 0.0 }, { 1.0, 1.0, -5.1238503646734541, 7.8778922645553644, -6.5434337308396371, 3.0175677147391462, -0.62991383824504521, 0.0, 0.0 }, { 0.0, 0.0, 9.33100787945892, -17.847599752918129, 15.982011430196467, -7.5981343369871421, 1.605431724860455, 0.0, 0.0 }, { 0.0, 0.0, -5.2890824922312181, 19.672874291559392, -21.496843461000324, 11.045585075701418, -2.4057275897895933, 0.0, 0.0 }, { 0.0, 0.0, 3.5121346806199369, -15.16818525665005, 25.116531523286987, -15.16818525665005, 3.5121346806199369, 0.0, 0.0 }, { 0.0, 0.0, -2.4057275897895933, 11.045585075701418, -21.496843461000324, 19.672874291559392, -5.2890824922312181, 0.0, 0.0 }, { 0.0, 0.0, 1.605431724860455, -7.5981343369871421, 15.982011430196467, -17.847599752918129, 9.33100787945892, 0.0, 0.0 }, { 0.0, 0.0, -0.62991383824504521, 3.0175677147391462, -6.5434337308396371, 7.8778922645553644, -5.1238503646734541, 1.0, 1.0 } };
            private static readonly double[,] matrix_9 = new double[,] { { 0.0, 0.1111111111111111, -0.3592590045879816, 0.60119023172938391, -0.61861310266395586, 0.40079348781958929, -0.15396814482342067, 0.027777777777777776, 0.0, 0.0 }, { 0.0, 0.0, -0.027777777777777776, 0.15396814482342067, -0.40079348781958929, 0.61861310266395586, -0.60119023172938391, 0.3592590045879816, -0.1111111111111111, 0.0 }, { 1.0, 1.0, -6.4769669995149766, 12.258180583456713, -13.167448225728897, 8.6880880617528184, -3.3651284696167552, 0.60925900458798155, 0.0, 0.0 }, { 0.0, 0.0, 11.436539929117433, -26.978965258561058, 31.395824020963406, -21.45434870987928, 8.444159649157184, -1.5394543913313523, 0.0, 0.0 }, { 0.0, 0.0, -6.5781756041750272, 28.258977906216121, -40.098176192375043, 29.82555359202415, -12.198432633249208, 2.2608282772724788, 0.0, 0.0 }, { 0.0, 0.0, 4.4729523904640223, -22.100734766580253, 43.846277553504251, -38.0357701002614, 16.681942989177255, -3.1849826064205589, 0.0, 0.0 }, { 0.0, 0.0, -3.1849826064205589, 16.681942989177255, -38.0357701002614, 43.846277553504251, -22.100734766580253, 4.4729523904640223, 0.0, 0.0 }, { 0.0, 0.0, 2.2608282772724788, -12.198432633249208, 29.82555359202415, -40.098176192375043, 28.258977906216121, -6.5781756041750272, 0.0, 0.0 }, { 0.0, 0.0, -1.5394543913313523, 8.444159649157184, -21.45434870987928, 31.395824020963406, -26.978965258561058, 11.436539929117433, 0.0, 0.0 }, { 0.0, 0.0, 0.60925900458798155, -3.3651284696167552, 8.6880880617528184, -13.167448225728897, 12.258180583456713, -6.4769669995149766, 1.0, 1.0 } };
            private static readonly double[][,] MatrixData;
            private static readonly double[][] ZeroData;
            private static readonly double[] Zeros_10;
            private static readonly double[] Zeros_11;
            private static readonly double[] Zeros_12;
            private static readonly double[] Zeros_13;
            private static readonly double[] Zeros_14;
            private static readonly double[] Zeros_15;
            private static readonly double[] Zeros_16;
            private static readonly double[] Zeros_17;
            private static readonly double[] Zeros_18;
            private static readonly double[] Zeros_19;
            private static readonly double[] Zeros_20;
            private static readonly double[] Zeros_21;
            private static readonly double[] Zeros_22;
            private static readonly double[] Zeros_23;
            private static readonly double[] Zeros_24;
            private static readonly double[] Zeros_25;
            private static readonly double[] Zeros_26;
            private static readonly double[] Zeros_27;
            private static readonly double[] Zeros_28;
            private static readonly double[] Zeros_29;
            private static readonly double[] Zeros_30;
            private static readonly double[] Zeros_31;
            private static readonly double[] Zeros_32;
            private static readonly double[] Zeros_33;
            private static readonly double[] Zeros_34;
            private static readonly double[] Zeros_35;
            private static readonly double[] Zeros_5;
            private static readonly double[] Zeros_6;
            private static readonly double[] Zeros_7;
            private static readonly double[] Zeros_8;
            private static readonly double[] Zeros_9;

            static ChebyshevConstants()
            {
                double[][,] numArray = new double[0x24][,];
                numArray[5] = matrix_5;
                numArray[6] = matrix_6;
                numArray[7] = matrix_7;
                numArray[8] = matrix_8;
                numArray[9] = matrix_9;
                numArray[10] = matrix_10;
                numArray[11] = matrix_11;
                numArray[12] = matrix_12;
                numArray[13] = matrix_13;
                numArray[14] = matrix_14;
                numArray[15] = matrix_15;
                numArray[0x10] = matrix_16;
                numArray[0x11] = matrix_17;
                numArray[0x12] = matrix_18;
                numArray[0x13] = matrix_19;
                numArray[20] = matrix_20;
                numArray[0x15] = matrix_21;
                numArray[0x16] = matrix_22;
                numArray[0x17] = matrix_23;
                numArray[0x18] = matrix_24;
                numArray[0x19] = matrix_25;
                numArray[0x1a] = matrix_26;
                numArray[0x1b] = matrix_27;
                numArray[0x1c] = matrix_28;
                numArray[0x1d] = matrix_29;
                numArray[30] = matrix_30;
                numArray[0x1f] = matrix_31;
                numArray[0x20] = matrix_32;
                numArray[0x21] = matrix_33;
                numArray[0x22] = matrix_34;
                numArray[0x23] = matrix_35;
                MatrixData = numArray;
                Zeros_5 = new double[] { 0.0, 0.337294327830114, 0.66270567216988607, 1.0 };
                Zeros_6 = new double[] { 0.0, 0.24495988883579586, 0.5, 0.75504011116420411, 1.0 };
                Zeros_7 = new double[] { 0.0, 0.1864387714554496, 0.3902919995700545, 0.60970800042994555, 0.81356122854455037, 1.0 };
                Zeros_8 = new double[] { 0.0, 0.14679656112455336, 0.31264288060996576, 0.5, 0.6873571193900343, 0.8532034388754467, 1.0 };
                Zeros_9 = new double[] { 0.0, 0.11863864558767077, 0.2557786270484404, 0.41590619504034454, 0.58409380495965546, 0.74422137295155955, 0.88136135441232921, 1.0 };
                Zeros_10 = new double[] { 0.0, 0.097897869105562335, 0.21296510835406846, 0.35052173697942918, 0.5, 0.64947826302057077, 0.78703489164593154, 0.90210213089443769, 1.0 };
                Zeros_11 = new double[] { 0.0, 0.082170710898042773, 0.17996956755560506, 0.29893786389694366, 0.431435485211921, 0.568564514788079, 0.70106213610305634, 0.82003043244439489, 0.91782928910195727, 1.0 };
                Zeros_12 = new double[] { 0.0, 0.069957835916071492, 0.15402846565706443, 0.25766393858216263, 0.37523116249666055, 0.5, 0.62476883750333945, 0.74233606141783737, 0.84597153434293559, 0.93004216408392848, 1.0 };
                Zeros_13 = new double[] { 0.0, 0.060282880606919588, 0.13327938263918437, 0.22420106489561009, 0.32882006677295617, 0.44197442451173469, 0.55802557548826526, 0.67117993322704383, 0.77579893510438991, 0.86672061736081563, 0.93971711939308045, 1.0 };
                Zeros_14 = new double[] { 0.0, 0.052487209099969775, 0.11643235040770682, 0.19674010704980802, 0.29018628700123811, 0.392757910813056, 0.5, 0.607242089186944, 0.70981371299876184, 0.803259892950192, 0.88356764959229317, 0.94751279090003027, 1.0 };
                Zeros_15 = new double[] { 0.0, 0.046113156264841332, 0.10257222070643333, 0.17395389818822113, 0.25776410413618384, 0.35084329017457716, 0.44963755931794736, 0.55036244068205264, 0.64915670982542284, 0.74223589586381611, 0.82604610181177884, 0.89742777929356665, 0.95388684373515864, 1.0 };
                Zeros_16 = new double[] { 0.0, 0.040834551474387054, 0.091035865248000572, 0.15485540989853586, 0.23033914569212746, 0.31497004382339489, 0.40588578849091, 0.5, 0.59411421150909, 0.68502995617660511, 0.76966085430787257, 0.84514459010146414, 0.90896413475199944, 0.95916544852561292, 1.0 };
                Zeros_17 = new double[] { 0.0, 0.036413786237437733, 0.081333646292839776, 0.13870062969795163, 0.20696625553370196, 0.28410350921501837, 0.36778573509835205, 0.45547833126857989, 0.54452166873142016, 0.632214264901648, 0.71589649078498163, 0.793033744466298, 0.86129937030204839, 0.91866635370716021, 0.96358621376256226, 1.0 };
                Zeros_18 = new double[] { 
                    0.0, 0.032674414101754978, 0.07309789578016955, 0.12492146333530674, 0.186905418499074, 0.25740101854389136, 0.33450041928946228, 0.41610745243591629, 0.5, 0.58389254756408371, 0.66549958071053772, 0.7425989814561087, 0.81309458150092606, 0.87507853666469326, 0.92690210421983044, 0.967325585898245, 
                    1.0
                 };
                Zeros_19 = new double[] { 
                    0.0, 0.029483166893747918, 0.066048329323373234, 0.1130788466447806, 0.1695730788407159, 0.23417762295879405, 0.30531514059722081, 0.38124003237801374, 0.46008605485364518, 0.53991394514635482, 0.61875996762198626, 0.69468485940277924, 0.76582237704120593, 0.83042692115928407, 0.88692115335521937, 0.93395167067662677, 
                    0.970516833106252, 1.0
                 };
                Zeros_20 = new double[] { 
                    0.0, 0.026737857645786969, 0.059968343308895826, 0.10282956843382206, 0.1545055280597549, 0.21387585254413385, 0.27962637244787564, 0.35029369217011896, 0.42430205086716027, 0.5, 0.57569794913283967, 0.649706307829881, 0.72037362755212431, 0.78612414745586612, 0.84549447194024507, 0.89717043156617793, 
                    0.94003165669110422, 0.973262142354213, 1.0
                 };
                Zeros_21 = new double[] { 
                    0.0, 0.024359064246253871, 0.054688463403116033, 0.093902495080549, 0.14133142306640409, 0.19604055788737654, 0.25692708323054031, 0.32275633231385814, 0.39219070177954324, 0.46381820720397676, 0.53618179279602318, 0.60780929822045671, 0.67724366768614186, 0.74307291676945963, 0.80395944211262349, 0.85866857693359588, 
                    0.906097504919451, 0.94531153659688394, 0.97564093575374611, 1.0
                 };
                Zeros_22 = new double[] { 
                    0.0, 0.022284300024561769, 0.050074588323580874, 0.086081318721422342, 0.12975110692128181, 0.18029856363694291, 0.23679199532183046, 0.29818337999348055, 0.36333133365416603, 0.431023573059176, 0.5, 0.568976426940824, 0.636668666345834, 0.70181662000651945, 0.76320800467816952, 0.81970143636305715, 
                    0.87024889307871822, 0.91391868127857767, 0.94992541167641908, 0.97771569997543828, 1.0
                 };
                Zeros_23 = new double[] { 
                    0.0, 0.020463855564811863, 0.046019508841294972, 0.079191887602234043, 0.11952097233513127, 0.16634243977147967, 0.21886421246435611, 0.27619157837313224, 0.33734565196162924, 0.40128121855640431, 0.46690507031812489, 0.53309492968187511, 0.59871878144359569, 0.66265434803837076, 0.72380842162686776, 0.78113578753564394, 
                    0.83365756022852033, 0.88047902766486874, 0.92080811239776594, 0.953980491158705, 0.97953614443518811, 1.0
                 };
                Zeros_24 = new double[] { 
                    0.0, 0.018857785398570998, 0.042436701877832078, 0.073092794215544793, 0.11044156492051728, 0.15391763587325827, 0.20284366555695657, 0.25645167251434281, 0.31389804418431921, 0.37427784554039945, 0.43663950191987561, 0.5, 0.56336049808012434, 0.62572215445960055, 0.68610195581568079, 0.74354832748565713, 
                    0.79715633444304346, 0.84608236412674176, 0.88955843507948273, 0.92690720578445518, 0.95756329812216789, 0.981142214601429, 1.0
                 };
                Zeros_25 = new double[] { 
                    0.0, 0.017433692773360026, 0.039255729637581012, 0.067668307127538047, 0.10234846952987536, 0.1428122983836746, 0.18847738477030274, 0.238681145579647, 0.29269315806339158, 0.34972673599807474, 0.40895077852187361, 0.46950206483884555, 0.5304979351611544, 0.59104922147812644, 0.6502732640019252, 0.70730684193660842, 
                    0.761318854420353, 0.81152261522969726, 0.8571877016163254, 0.89765153047012458, 0.932331692872462, 0.96074427036241894, 0.98256630722664, 1.0
                 };
                Zeros_26 = new double[] { 
                    0.0, 0.016165079629340294, 0.036418787328558848, 0.062823008118451679, 0.095105278560431411, 0.13284920331816011, 0.17555142479195887, 0.22263752704808373, 0.27347227722858963, 0.32736907099367452, 0.38359956526682371, 0.44140368802800278, 0.5, 0.55859631197199722, 0.61640043473317629, 0.67263092900632548, 
                    0.72652772277141042, 0.77736247295191629, 0.82444857520804116, 0.86715079668183992, 0.90489472143956862, 0.93717699188154835, 0.96358121267144115, 0.98383492037065967, 1.0
                 };
                Zeros_27 = new double[] { 
                    0.0, 0.015030102703414429, 0.033878083730549, 0.058477683648290757, 0.088598127386134859, 0.12387934823364384, 0.16388421289636751, 0.20811246719618129, 0.25600932301158569, 0.30697322801109544, 0.36036375222392819, 0.41550978924562015, 0.4717180709752648, 0.52828192902473514, 0.5844902107543799, 0.63963624777607186, 
                    0.6930267719889045, 0.74399067698841437, 0.79188753280381874, 0.83611578710363255, 0.87612065176635612, 0.91140187261386518, 0.94152231635170924, 0.966121916269451, 0.98496989729658557, 1.0
                 };
                Zeros_28 = new double[] { 
                    0.0, 0.0140106253595559, 0.031593833076771778, 0.054566148468091456, 0.082731417297463292, 0.11577684380159799, 0.15332108873979938, 0.19492660763013828, 0.24010691765924166, 0.28833303881760597, 0.33903999449676542, 0.39163357173670332, 0.44549735669096568, 0.5, 0.55450264330903432, 0.60836642826329668, 
                    0.66096000550323464, 0.711666961182394, 0.75989308234075836, 0.80507339236986175, 0.84667891126020067, 0.884223156198402, 0.91726858270253675, 0.94543385153190851, 0.96840616692322823, 0.98598937464044412, 1.0
                 };
                Zeros_29 = new double[] { 
                    0.0, 0.013091487409593651, 0.029532700752876718, 0.051032768737734935, 0.077424444517987259, 0.10843482427273063, 0.14372982715836544, 0.18292520006506124, 0.22559272188505544, 0.27126558308924559, 0.31944377855321371, 0.36959971247767143, 0.42118404240015045, 0.47363173281848653, 0.52636826718151342, 0.57881595759984961, 
                    0.63040028752232857, 0.68055622144678629, 0.72873441691075436, 0.77440727811494459, 0.8170747999349387, 0.85627017284163454, 0.89156517572726934, 0.92257555548201275, 0.948967231262265, 0.97046729924712327, 0.98690851259040635, 1.0
                 };
                Zeros_30 = new double[] { 
                    0.0, 0.012259937452939653, 0.027666589463397461, 0.047830514938878528, 0.072608725376021158, 0.1017621587069051, 0.13499696493052035, 0.17197439368662196, 0.21231613575840683, 0.25560884818616531, 0.3014086557633861, 0.34924582399559739, 0.39862963821451441, 0.44905347101607024, 0.5, 0.55094652898392971, 
                    0.60137036178548564, 0.65075417600440255, 0.69859134423661384, 0.74439115181383464, 0.7876838642415932, 0.82802560631337807, 0.86500303506947962, 0.89823784129309492, 0.92739127462397886, 0.95216948506112142, 0.97233341053660249, 0.98774006254706037, 1.0
                 };
                Zeros_31 = new double[] { 
                    0.0, 0.011505187658935366, 0.025971683350958411, 0.0449194195648121, 0.068225860027230273, 0.095680793667303851, 0.12702478220393629, 0.1619581030278692, 0.20014538448489866, 0.24121943836909687, 0.28478503966086582, 0.33042284330015803, 0.37769347771674211, 0.42614180586386458, 0.47530132574810358, 0.52469867425189642, 
                    0.57385819413613537, 0.62230652228325789, 0.669577156699842, 0.71521496033913423, 0.75878056163090313, 0.79985461551510129, 0.83804189697213083, 0.87297521779606368, 0.90431920633269613, 0.9317741399727697, 0.95508058043518784, 0.97402831664904155, 0.98849481234106462, 1.0
                 };
                Zeros_32 = new double[] { 
                    0.0, 0.010818061703856538, 0.024427689356208024, 0.04226534663476636, 0.0642258156116576, 0.090123595847069887, 0.11972881608622987, 0.15277537133582278, 0.18896497554079802, 0.22797042886170882, 0.26943880939931752, 0.31299477295361272, 0.35824400366116954, 0.40477681257044673, 0.45217186376132207, 0.5, 
                    0.54782813623867788, 0.59522318742955327, 0.6417559963388304, 0.68700522704638733, 0.73056119060068248, 0.77202957113829118, 0.811035024459202, 0.84722462866417725, 0.88027118391377013, 0.90987640415293014, 0.93577418438834237, 0.95773465336523367, 0.975572310643792, 0.98918193829614343, 1.0
                 };
                Zeros_33 = new double[] { 
                    0.0, 0.010190714228959383, 0.023017230699952176, 0.03983900331017081, 0.060565538213804705, 0.085032592202964108, 0.11303580681816908, 0.14433815247618631, 0.17867349691136106, 0.21574940823278216, 0.25524986480132961, 0.29683804881468184, 0.3401592682019598, 0.38484400852280964, 0.43051110028453826, 0.4767709800144117, 
                    0.52322901998558835, 0.56948889971546179, 0.61515599147719036, 0.65984073179804026, 0.70316195118531821, 0.74475013519867039, 0.7842505917672179, 0.821326503088639, 0.85566184752381369, 0.886964193181831, 0.91496740779703589, 0.93943446178619527, 0.96016099668982924, 0.97698276930004779, 0.98980928577104066, 1.0
                 };
                Zeros_34 = new double[] { 
                    0.0, 0.0096164056797598436, 0.021725358641843242, 0.037615140875717192, 0.057207824210442161, 0.0803575275813256, 0.10688199598757236, 0.13656944417997532, 0.16918171973501012, 0.20445672248720964, 0.2421107173520734, 0.28184071051600518, 0.32332693437863863, 0.36623544632870075, 0.410220831209466, 0.4549289907451648, 
                    0.5, 0.5450710092548352, 0.58977916879053394, 0.63376455367129925, 0.67667306562136142, 0.71815928948399477, 0.75788928264792654, 0.7955432775127903, 0.83081828026498994, 0.8634305558200247, 0.89311800401242758, 0.91964247241867436, 0.94279217578955787, 0.96238485912428284, 0.97827464135815678, 0.99038359432024015, 
                    1.0
                 };
                Zeros_35 = new double[] { 
                    0.0, 0.0090893203725876254, 0.020539156901883231, 0.035571904877553177, 0.05412039749483722, 0.076054677094252662, 0.10121171179320398, 0.12940171519325244, 0.16041096777136554, 0.19400391741351555, 0.22992516330661894, 0.2679014862319376, 0.30764397073670835, 0.34885022671840304, 0.3912067036837068, 0.43439108480533323, 
                    0.47807474492558311, 0.52192525507441689, 0.56560891519466683, 0.6087932963162932, 0.651149773281597, 0.69235602926329165, 0.73209851376806245, 0.77007483669338106, 0.80599608258648447, 0.83958903222863446, 0.8705982848067475, 0.898788288206796, 0.92394532290574738, 0.94587960250516279, 0.96442809512244687, 0.97946084309811676, 
                    0.99091067962741242, 1.0
                 };
                double[][] numArray2 = new double[0x24][];
                numArray2[5] = Zeros_5;
                numArray2[6] = Zeros_6;
                numArray2[7] = Zeros_7;
                numArray2[8] = Zeros_8;
                numArray2[9] = Zeros_9;
                numArray2[10] = Zeros_10;
                numArray2[11] = Zeros_11;
                numArray2[12] = Zeros_12;
                numArray2[13] = Zeros_13;
                numArray2[14] = Zeros_14;
                numArray2[15] = Zeros_15;
                numArray2[0x10] = Zeros_16;
                numArray2[0x11] = Zeros_17;
                numArray2[0x12] = Zeros_18;
                numArray2[0x13] = Zeros_19;
                numArray2[20] = Zeros_20;
                numArray2[0x15] = Zeros_21;
                numArray2[0x16] = Zeros_22;
                numArray2[0x17] = Zeros_23;
                numArray2[0x18] = Zeros_24;
                numArray2[0x19] = Zeros_25;
                numArray2[0x1a] = Zeros_26;
                numArray2[0x1b] = Zeros_27;
                numArray2[0x1c] = Zeros_28;
                numArray2[0x1d] = Zeros_29;
                numArray2[30] = Zeros_30;
                numArray2[0x1f] = Zeros_31;
                numArray2[0x20] = Zeros_32;
                numArray2[0x21] = Zeros_33;
                numArray2[0x22] = Zeros_34;
                numArray2[0x23] = Zeros_34;
                ZeroData = numArray2;
            }

            public static double[,] InterpolationMatrix(int m)
            {
                return MatrixData[m];
            }

            public static double[] Zeros(int m)
            {
                return ZeroData[m];
            }
        }

        public delegate Position CurvePositionFunction(object data, double t);

        public enum DirectionUV
        {
            U = 1,
            V = 2
        }
    }
}

