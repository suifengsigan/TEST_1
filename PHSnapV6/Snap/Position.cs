namespace Snap
{
    using NXOpen;
    using Snap.Geom;
    using Snap.NX;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct Position
    {
        public double X;
        public double Y;
        public double Z;
        public static readonly Position Origin;
        public double[] Array
        {
            get
            {
                return new double[] { this.X, this.Y, this.Z };
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
        public static implicit operator Position(double[] doubleArray)
        {
            return new Position(doubleArray[0], doubleArray[1], doubleArray[2]);
        }

        public static implicit operator Position(int[] intArray)
        {
            return new Position((double) intArray[0], (double) intArray[1], (double) intArray[2]);
        }

        public static implicit operator Position(Point3d pt)
        {
            return new Position(pt);
        }

        public static implicit operator Point3d(Position p)
        {
            return new Point3d(p.X, p.Y, p.Z);
        }

        public static explicit operator Position(Vector v)
        {
            return new Position(v.X, v.Y, v.Z);
        }

        public static explicit operator Vector(Position p)
        {
            return new Vector(p.X, p.Y, p.Z);
        }

        public static Position operator +(Position u, Position v)
        {
            return new Position(u.X + v.X, u.Y + v.Y, u.Z + v.Z);
        }

        public static Position operator +(Position u, Vector v)
        {
            return new Position(u.X + v.X, u.Y + v.Y, u.Z + v.Z);
        }

        public static Vector operator -(Position u, Position v)
        {
            return new Vector(u.X - v.X, u.Y - v.Y, u.Z - v.Z);
        }

        public static Position operator -(Position p, Vector v)
        {
            return new Position(p.X - v.X, p.Y - v.Y, p.Z - v.Z);
        }

        public static Position operator -(Position u)
        {
            return new Position(-u.X, -u.Y, -u.Z);
        }

        public static Position operator *(double s, Position u)
        {
            return new Position(s * u.X, s * u.Y, s * u.Z);
        }

        public static Position operator *(int s, Position u)
        {
            return new Position(s * u.X, s * u.Y, s * u.Z);
        }

        public static Position operator /(Position u, double s)
        {
            return new Position(u.X / s, u.Y / s, u.Z / s);
        }

        public Position(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Position(double x, double y)
        {
            this.X = x;
            this.Y = y;
            this.Z = 0.0;
        }

        public Position(double[] coords)
        {
            this.X = coords[0];
            this.Y = coords[1];
            this.Z = coords[2];
        }

        public Position(Snap.NX.Point pt)
        {
            NXOpen.Point nXOpenPoint = pt.NXOpenPoint;
            this.X = nXOpenPoint.Coordinates.X;
            this.Y = nXOpenPoint.Coordinates.Y;
            this.Z = nXOpenPoint.Coordinates.Z;
        }

        public Position(NXOpen.Point pt)
        {
            this.X = pt.Coordinates.X;
            this.Y = pt.Coordinates.Y;
            this.Z = pt.Coordinates.Z;
        }

        public Position(Point3d pt)
        {
            this.X = pt.X;
            this.Y = pt.Y;
            this.Z = pt.Z;
        }

        public override string ToString()
        {
            return ("( " + Snap.Number.ToString(this.X) + " , " + Snap.Number.ToString(this.Y) + " , " + Snap.Number.ToString(this.Z) + " )");
        }

        public string ToString(string format)
        {
            return ("( " + this.X.ToString(format) + " , " + this.Y.ToString(format) + " , " + this.Z.ToString(format) + " )");
        }

        public static double Distance2(Position p, Position q)
        {
            return Vector.Norm2((Vector) (p - q));
        }

        public static double Distance(Position p, Position q)
        {
            return Vector.Norm((Vector) (p - q));
        }

        public static double[] GetX(Position[] positions)
        {
            int length = positions.Length;
            double[] numArray = new double[length];
            for (int i = 0; i < length; i++)
            {
                numArray[i] = positions[i].X;
            }
            return numArray;
        }

        public static double[,] GetX(Position[,] positions)
        {
            int length = positions.GetLength(0);
            int num2 = positions.GetLength(1);
            double[,] numArray = new double[length, num2];
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < num2; j++)
                {
                    numArray[i, j] = positions[i, j].X;
                }
            }
            return numArray;
        }

        public static double[] GetY(Position[] positions)
        {
            int length = positions.Length;
            double[] numArray = new double[length];
            for (int i = 0; i < length; i++)
            {
                numArray[i] = positions[i].Y;
            }
            return numArray;
        }

        public static double[,] GetY(Position[,] positions)
        {
            int length = positions.GetLength(0);
            int num2 = positions.GetLength(1);
            double[,] numArray = new double[length, num2];
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < num2; j++)
                {
                    numArray[i, j] = positions[i, j].Y;
                }
            }
            return numArray;
        }

        public static double[] GetZ(Position[] positions)
        {
            int length = positions.Length;
            double[] numArray = new double[length];
            for (int i = 0; i < length; i++)
            {
                numArray[i] = positions[i].Z;
            }
            return numArray;
        }

        public static double[,] GetZ(Position[,] positions)
        {
            int length = positions.GetLength(0);
            int num2 = positions.GetLength(1);
            double[,] numArray = new double[length, num2];
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < num2; j++)
                {
                    numArray[i, j] = positions[i, j].Z;
                }
            }
            return numArray;
        }

        public static double[] GetXYZ(Position[] positions)
        {
            int length = positions.Length;
            double[] numArray = new double[3 * length];
            for (int i = 0; i < length; i++)
            {
                numArray[3 * i] = positions[i].X;
                numArray[(3 * i) + 1] = positions[i].Y;
                numArray[(3 * i) + 2] = positions[i].Z;
            }
            return numArray;
        }

        public static Position[] PositionsFromCoordinates(double[] coords)
        {
            int num = coords.Length / 3;
            Position[] positionArray = new Position[num];
            for (int i = 0; i < num; i++)
            {
                positionArray[i].X = coords[3 * i];
                positionArray[i].Y = coords[(3 * i) + 1];
                positionArray[i].Z = coords[(3 * i) + 2];
            }
            return positionArray;
        }

        public static Position[] PositionsFromCoordinates(double[] x, double[] y, double[] z)
        {
            int num = (int) Snap.Math.Min(new double[] { (double) x.Length, (double) y.Length, (double) z.Length });
            Position[] positionArray = new Position[num];
            for (int i = 0; i < num; i++)
            {
                positionArray[i].X = x[i];
                positionArray[i].Y = y[i];
                positionArray[i].Z = z[i];
            }
            return positionArray;
        }

        public static Position[,] PositionsFromCoordinates(double[,] x, double[,] y, double[,] z)
        {
            int num = (int) Snap.Math.Min(new double[] { (double) x.GetLength(0), (double) y.GetLength(0), (double) z.GetLength(0) });
            int num2 = (int) Snap.Math.Min(new double[] { (double) x.GetLength(1), (double) y.GetLength(1), (double) z.GetLength(1) });
            Position[,] positionArray = new Position[num, num2];
            for (int i = 0; i < num; i++)
            {
                for (int j = 0; j < num2; j++)
                {
                    positionArray[i, j].X = x[i, j];
                    positionArray[i, j].Y = y[i, j];
                    positionArray[i, j].Z = z[i, j];
                }
            }
            return positionArray;
        }

        public static Position[] Copy(params Position[] original)
        {
            int length = original.Length;
            Position[] positionArray = new Position[length];
            for (int i = 0; i < length; i++)
            {
                positionArray[i] = original[i];
            }
            return positionArray;
        }

        public Position Copy()
        {
            return new Position(this.X, this.Y, this.Z);
        }

        public Position Copy(Transform xform)
        {
            double[] matrix = xform.Matrix;
            double x = this.X;
            double y = this.Y;
            double z = this.Z;
            double num4 = (((x * matrix[0]) + (y * matrix[1])) + (z * matrix[2])) + matrix[3];
            double num5 = (((x * matrix[4]) + (y * matrix[5])) + (z * matrix[6])) + matrix[7];
            return new Position(num4, num5, (((x * matrix[8]) + (y * matrix[9])) + (z * matrix[10])) + matrix[11]);
        }

        public static Position[] Copy(Transform xform, params Position[] original)
        {
            Position[] positionArray = new Position[original.Length];
            for (int i = 0; i < original.Length; i++)
            {
                positionArray[i] = original[i].Copy(xform);
            }
            return positionArray;
        }

        public void Move(Transform xform)
        {
            double[] matrix = xform.Matrix;
            double x = this.X;
            double y = this.Y;
            double z = this.Z;
            double num4 = (((x * matrix[0]) + (y * matrix[1])) + (z * matrix[2])) + matrix[3];
            double num5 = (((x * matrix[4]) + (y * matrix[5])) + (z * matrix[6])) + matrix[7];
            double num6 = (((x * matrix[8]) + (y * matrix[9])) + (z * matrix[10])) + matrix[11];
            this.X = num4;
            this.Y = num5;
            this.Z = num6;
        }

        public static void Move(Transform xform, params Position[] original)
        {
            for (int i = 0; i < original.Length; i++)
            {
                original[i].Move(xform);
            }
        }

        static Position()
        {
            Origin = new Position(0.0, 0.0, 0.0);
        }
    }
}

