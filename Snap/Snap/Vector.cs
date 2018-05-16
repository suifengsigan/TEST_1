namespace Snap
{
    using NXOpen;
    using Snap.Geom;
    using Snap.NX;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct Vector
    {
        public double X;
        public double Y;
        public double Z;
        public double[] Array
        {
            get
            {
                return new double[] { this.X, this.Y, this.Z };
            }
        }
        public static Vector operator +(Vector u, Vector v)
        {
            return new Vector(u.X + v.X, u.Y + v.Y, u.Z + v.Z);
        }

        public static Vector operator -(Vector u, Vector v)
        {
            return new Vector(u.X - v.X, u.Y - v.Y, u.Z - v.Z);
        }

        public static Vector operator -(Vector u)
        {
            return new Vector(-u.X, -u.Y, -u.Z);
        }

        public static Vector operator *(double s, Vector u)
        {
            return new Vector(s * u.X, s * u.Y, s * u.Z);
        }

        public static Vector operator *(int s, Vector u)
        {
            return new Vector(s * u.X, s * u.Y, s * u.Z);
        }

        public static Vector operator /(Vector u, double s)
        {
            return new Vector(u.X / s, u.Y / s, u.Z / s);
        }

        public static double operator *(Vector u, Vector v)
        {
            return (((u.X * v.X) + (u.Y * v.Y)) + (u.Z * v.Z));
        }

        public Vector(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Vector(double x, double y)
        {
            this.X = x;
            this.Y = y;
            this.Z = 0.0;
        }

        public Vector(double[] coords)
        {
            this.X = coords[0];
            this.Y = coords[1];
            this.Z = coords[2];
        }

        public Vector(Snap.NX.Point pt)
        {
            NXOpen.Point nXOpenPoint = pt.NXOpenPoint;
            this.X = nXOpenPoint.Coordinates.X;
            this.Y = nXOpenPoint.Coordinates.Y;
            this.Z = nXOpenPoint.Coordinates.Z;
        }

        public Vector(NXOpen.Point pt)
        {
            this.X = pt.Coordinates.X;
            this.Y = pt.Coordinates.Y;
            this.Z = pt.Coordinates.Z;
        }

        public Vector(Point3d pt)
        {
            this.X = pt.X;
            this.Y = pt.Y;
            this.Z = pt.Z;
        }

        public Vector(Vector3d v)
        {
            this.X = v.X;
            this.Y = v.Y;
            this.Z = v.Z;
        }

        public static implicit operator Vector(double[] doubleArray)
        {
            return new Vector(doubleArray[0], doubleArray[1], doubleArray[2]);
        }

        public static implicit operator Vector(int[] intArray)
        {
            return new Vector((double) intArray[0], (double) intArray[1], (double) intArray[2]);
        }

        public static explicit operator double[](Vector v)
        {
            return new double[] { v.X, v.Y, v.Z };
        }

        public static implicit operator Vector(Vector3d vec)
        {
            return new Vector(vec);
        }

        public static implicit operator Vector3d(Vector v)
        {
            return new Vector3d(v.X, v.Y, v.Z);
        }

        public override string ToString()
        {
            return ("( " + Snap.Number.ToString(this.X) + " , " + Snap.Number.ToString(this.Y) + " , " + Snap.Number.ToString(this.Z) + " )");
        }

        public string ToString(string format)
        {
            return ("( " + this.X.ToString(format) + " , " + this.Y.ToString(format) + " , " + this.Z.ToString(format) + " )");
        }

        public static Vector Cross(Vector u, Vector v)
        {
            return new Vector((u.Y * v.Z) - (u.Z * v.Y), (u.Z * v.X) - (u.X * v.Z), (u.X * v.Y) - (u.Y * v.X));
        }

        public static Vector UnitCross(Vector u, Vector v)
        {
            return Unit(Cross(u, v));
        }

        public static Vector Unit(Vector u)
        {
            double num = 1.0 / Norm(u);
            return new Vector(num * u.X, num * u.Y, num * u.Z);
        }

        public static double Norm2(Vector u)
        {
            return (((u.X * u.X) + (u.Y * u.Y)) + (u.Z * u.Z));
        }

        public static double Norm(Vector u)
        {
            return System.Math.Sqrt(((u.X * u.X) + (u.Y * u.Y)) + (u.Z * u.Z));
        }

        public static double Angle(Vector u, Vector v)
        {
            double num = (double) (Unit(u) * Unit(v));
            num = System.Math.Min(1.0, num);
            return ((System.Math.Acos(System.Math.Max(-1.0, num)) * 180.0) / 3.1415926535897931);
        }

        public static Vector AxisX
        {
            get
            {
                return new Vector(1.0, 0.0, 0.0);
            }
        }
        public static Vector AxisY
        {
            get
            {
                return new Vector(0.0, 1.0, 0.0);
            }
        }
        public static Vector AxisZ
        {
            get
            {
                return new Vector(0.0, 0.0, 1.0);
            }
        }
        public double PolarTheta
        {
            get
            {
                return Snap.Math.Atan2D(this.Y, this.X);
            }
        }
        public double PolarPhi
        {
            get
            {
                double x = this.X;
                double y = this.Y;
                return Snap.Math.Atan2D(this.Z, System.Math.Sqrt((x * x) + (y * y)));
            }
        }
        public static double[] Components(Vector r, Vector u, Vector v, Vector w)
        {
            return Snap.Math.LinearAlgebra.LinearSystemSolve(r, u, v, w);
        }

        public static double[] Components(Vector r, Vector u, Vector v)
        {
            Vector w = UnitCross(u, v);
            return Snap.Math.LinearAlgebra.LinearSystemSolve(r, u, v, w);
        }

        public static double[] GetX(Vector[] vectors)
        {
            int length = vectors.Length;
            double[] numArray = new double[length];
            for (int i = 0; i < length; i++)
            {
                numArray[i] = vectors[i].X;
            }
            return numArray;
        }

        public static double[,] GetX(Vector[,] vectors)
        {
            int length = vectors.GetLength(0);
            int num2 = vectors.GetLength(1);
            double[,] numArray = new double[length, num2];
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < num2; j++)
                {
                    numArray[i, j] = vectors[i, j].X;
                }
            }
            return numArray;
        }

        public static double[] GetY(Vector[] vectors)
        {
            int length = vectors.Length;
            double[] numArray = new double[length];
            for (int i = 0; i < length; i++)
            {
                numArray[i] = vectors[i].Y;
            }
            return numArray;
        }

        public static double[,] GetY(Vector[,] vectors)
        {
            int length = vectors.GetLength(0);
            int num2 = vectors.GetLength(1);
            double[,] numArray = new double[length, num2];
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < num2; j++)
                {
                    numArray[i, j] = vectors[i, j].Y;
                }
            }
            return numArray;
        }

        public static double[] GetZ(Vector[] vectors)
        {
            int length = vectors.Length;
            double[] numArray = new double[length];
            for (int i = 0; i < length; i++)
            {
                numArray[i] = vectors[i].Z;
            }
            return numArray;
        }

        public static double[,] GetZ(Vector[,] vectors)
        {
            int length = vectors.GetLength(0);
            int num2 = vectors.GetLength(1);
            double[,] numArray = new double[length, num2];
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < num2; j++)
                {
                    numArray[i, j] = vectors[i, j].Z;
                }
            }
            return numArray;
        }

        public static double[] GetXYZ(Vector[] vectors)
        {
            int length = vectors.Length;
            double[] numArray = new double[3 * length];
            for (int i = 0; i < length; i++)
            {
                numArray[3 * i] = vectors[i].X;
                numArray[(3 * i) + 1] = vectors[i].Y;
                numArray[(3 * i) + 2] = vectors[i].Z;
            }
            return numArray;
        }

        public static Vector[] VectorsFromCoordinates(double[] coords)
        {
            int num = coords.Length / 3;
            Vector[] vectorArray = new Vector[num];
            for (int i = 0; i < num; i++)
            {
                vectorArray[i].X = coords[3 * i];
                vectorArray[i].Y = coords[(3 * i) + 1];
                vectorArray[i].Z = coords[(3 * i) + 2];
            }
            return vectorArray;
        }

        public static Vector[] VectorsFromCoordinates(double[] x, double[] y, double[] z)
        {
            int num = (int) Snap.Math.Min(new double[] { (double) x.Length, (double) y.Length, (double) z.Length });
            Vector[] vectorArray = new Vector[num];
            for (int i = 0; i < num; i++)
            {
                vectorArray[i].X = x[i];
                vectorArray[i].Y = y[i];
                vectorArray[i].Z = z[i];
            }
            return vectorArray;
        }

        public static Vector[,] VectorsFromCoordinates(double[,] x, double[,] y, double[,] z)
        {
            int num = (int) Snap.Math.Min(new double[] { (double) x.GetLength(0), (double) y.GetLength(0), (double) z.GetLength(0) });
            int num2 = (int) Snap.Math.Min(new double[] { (double) x.GetLength(1), (double) y.GetLength(1), (double) z.GetLength(1) });
            Vector[,] vectorArray = new Vector[num, num2];
            for (int i = 0; i < num; i++)
            {
                for (int j = 0; j < num2; j++)
                {
                    vectorArray[i, j].X = x[i, j];
                    vectorArray[i, j].Y = y[i, j];
                    vectorArray[i, j].Z = z[i, j];
                }
            }
            return vectorArray;
        }

        public Vector Copy()
        {
            return new Vector(this.X, this.Y, this.Z);
        }

        public Vector Copy(Transform xform)
        {
            double[] matrix = xform.Matrix;
            double x = this.X;
            double y = this.Y;
            double z = this.Z;
            double num4 = ((x * matrix[0]) + (y * matrix[1])) + (z * matrix[2]);
            double num5 = ((x * matrix[4]) + (y * matrix[5])) + (z * matrix[6]);
            return new Vector(num4, num5, ((x * matrix[8]) + (y * matrix[9])) + (z * matrix[10]));
        }

        public void Move(Transform xform)
        {
            double[] matrix = xform.Matrix;
            double x = this.X;
            double y = this.Y;
            double z = this.Z;
            double num4 = ((x * matrix[0]) + (y * matrix[1])) + (z * matrix[2]);
            double num5 = ((x * matrix[4]) + (y * matrix[5])) + (z * matrix[6]);
            double num6 = ((x * matrix[8]) + (y * matrix[9])) + (z * matrix[10]);
            this.X = num4;
            this.Y = num5;
            this.Z = num6;
        }

        public static Vector[] Copy(params Vector[] original)
        {
            int length = original.Length;
            Vector[] vectorArray = new Vector[length];
            for (int i = 0; i < length; i++)
            {
                vectorArray[i] = original[i];
            }
            return vectorArray;
        }

        public static Vector[] Copy(Transform xform, params Vector[] original)
        {
            Vector[] vectorArray = new Vector[original.Length];
            for (int i = 0; i < original.Length; i++)
            {
                vectorArray[i] = original[i].Copy(xform);
            }
            return vectorArray;
        }

        public static void Move(Transform xform, params Vector[] original)
        {
            for (int i = 0; i < original.Length; i++)
            {
                original[i].Move(xform);
            }
        }

        public Vector Unitize()
        {
            this = Unit(this);
            return this;
        }
    }
}

