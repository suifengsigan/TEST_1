namespace Snap
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class Math
    {
        public static double AcosD(double x)
        {
            return ((System.Math.Acos(x) * 180.0) / 3.1415926535897931);
        }

        public static double AsinD(double x)
        {
            return ((System.Math.Asin(x) * 180.0) / 3.1415926535897931);
        }

        public static double Atan2D(double y, double x)
        {
            return ((System.Math.Atan2(y, x) * 180.0) / 3.1415926535897931);
        }

        public static double AtanD(double x)
        {
            return ((System.Math.Atan(x) * 180.0) / 3.1415926535897931);
        }

        public static double CosD(double angle)
        {
            return System.Math.Cos((angle * 3.1415926535897931) / 180.0);
        }

        public static double Max(params double[] values)
        {
            return values[MaxIndex(values)];
        }

        public static int MaxIndex(params double[] values)
        {
            int index = 0;
            for (int i = 1; i < values.Length; i++)
            {
                if (values[i] > values[index])
                {
                    index = i;
                }
            }
            return index;
        }

        public static double Mean(params double[] values)
        {
            return (Sum(values) / ((double) values.Length));
        }

        public static double Min(params double[] values)
        {
            return values[MaxIndex(values)];
        }

        public static int MinIndex(params double[] values)
        {
            int index = 0;
            for (int i = 1; i < values.Length; i++)
            {
                if (values[i] > values[index])
                {
                    index = i;
                }
            }
            return index;
        }

        public static double SinD(double angle)
        {
            return System.Math.Sin((angle * 3.1415926535897931) / 180.0);
        }

        public static double Sum(params double[] values)
        {
            double num = 0.0;
            foreach (double num2 in values)
            {
                num += num2;
            }
            return num;
        }

        public static double TanD(double angle)
        {
            return System.Math.Tan((angle * 3.1415926535897931) / 180.0);
        }

        public static class LinearAlgebra
        {
            public static double[,] Adjugate3(double[,] a)
            {
                return new double[,] { { ((-a[1, 2] * a[2, 1]) + (a[1, 1] * a[2, 2])), ((a[0, 2] * a[2, 1]) - (a[0, 1] * a[2, 2])), ((-a[0, 2] * a[1, 1]) + (a[0, 1] * a[1, 2])) }, { ((a[1, 2] * a[2, 0]) - (a[1, 0] * a[2, 2])), ((-a[0, 2] * a[2, 0]) + (a[0, 0] * a[2, 2])), ((a[0, 2] * a[1, 0]) - (a[0, 0] * a[1, 2])) }, { ((-a[1, 1] * a[2, 0]) + (a[1, 0] * a[2, 1])), ((a[0, 1] * a[2, 0]) - (a[0, 0] * a[2, 1])), ((-a[0, 1] * a[1, 0]) + (a[0, 0] * a[1, 1])) } };
            }

            public static double[] BackSolve(double[,] a, int[] index, double[] b)
            {
                double[] numArray = Snap.Math.MatrixMath.Copy(b);
                BackSubstitution(a, index, numArray);
                return numArray;
            }

            public static void BackSubstitution(double[,] a, int[] index, double[] b)
            {
                int num;
                int num4;
                double num5;
                int num2 = 0;
                int length = a.GetLength(0);
                for (num = 0; num < length; num++)
                {
                    int num3 = index[num];
                    num5 = b[num3];
                    b[num3] = b[num];
                    if (num2 != 0)
                    {
                        num4 = num2 - 1;
                        while (num4 < num)
                        {
                            num5 -= a[num, num4] * b[num4];
                            num4++;
                        }
                    }
                    else if (num5 != 0.0)
                    {
                        num2 = num + 1;
                    }
                    b[num] = num5;
                }
                for (num = length - 1; num >= 0; num--)
                {
                    num5 = b[num];
                    for (num4 = num + 1; num4 < length; num4++)
                    {
                        num5 -= a[num, num4] * b[num4];
                    }
                    b[num] = num5 / a[num, num];
                }
            }

            public static double Determinant3(double[,] u)
            {
                double num = u[0, 0] * ((u[1, 1] * u[2, 2]) - (u[1, 2] * u[2, 1]));
                double num2 = u[0, 1] * ((u[1, 0] * u[2, 2]) - (u[1, 2] * u[2, 0]));
                double num3 = u[0, 2] * ((u[1, 0] * u[2, 1]) - (u[1, 1] * u[2, 0]));
                return ((num - num2) + num3);
            }

            public static EigenSystemResult[] EigenSystem(double[,] A)
            {
                EigenSolver3 solver = new EigenSolver3();
                solver.ComputeEigenStuff(A);
                EigenSystemResult[] resultArray = new EigenSystemResult[3];
                for (int i = 0; i < 3; i++)
                {
                    resultArray[2 - i] = new EigenSystemResult(solver.mEigenvalue[i], solver.mEigenvector[i].Array);
                }
                return resultArray;
            }

            public static double[,] Inverse(double[,] a)
            {
                int length = a.GetLength(0);
                double[,] numArray = Snap.Math.MatrixMath.Copy(a);
                double[,] numArray2 = new double[length, length];
                double[] b = new double[length];
                int[] index = new int[length];
                double d = 1.0;
                LUDecomposition(numArray, index, d);
                for (int i = 0; i < length; i++)
                {
                    int num3 = 0;
                    while (num3 < length)
                    {
                        b[num3] = 0.0;
                        num3++;
                    }
                    b[i] = 1.0;
                    BackSubstitution(numArray, index, b);
                    for (num3 = 0; num3 < length; num3++)
                    {
                        numArray2[num3, i] = b[num3];
                    }
                }
                return numArray2;
            }

            public static double[,] Inverse3(double[,] a)
            {
                double num = Determinant3(a);
                return Snap.Math.MatrixMath.Multiply(Adjugate3(a), (double) (1.0 / num));
            }

            public static double[] LinearSystemSolve(double[,] a, double[] b)
            {
                int[] index = new int[a.GetLength(0)];
                double[,] numArray2 = Snap.Math.MatrixMath.Copy(a);
                double d = 1.0;
                LUDecomposition(numArray2, index, d);
                return BackSolve(numArray2, index, b);
            }

            public static double[] LinearSystemSolve(Vector r, Vector u, Vector v, Vector w)
            {
                double num = (double) (u * Vector.Cross(v, w));
                double num2 = 1.0 / num;
                Vector vector = (Vector) (num2 * Vector.Cross(v, w));
                Vector vector2 = (Vector) (num2 * Vector.Cross(w, u));
                Vector vector3 = (Vector) (num2 * Vector.Cross(u, v));
                double num3 = (double) (r * vector);
                double num4 = (double) (r * vector2);
                double num5 = (double) (r * vector3);
                return new double[] { num3, num4, num5 };
            }

            public static double[] LinearSystemSolve(double a, double b, double c, double d, double h, double k)
            {
                double num = (a * d) - (b * c);
                double num2 = ((d * h) - (b * k)) / num;
                double num3 = ((a * k) - (c * h)) / num;
                return new double[] { num2, num3 };
            }

            public static void LUDecomposition(double[,] a, int[] index, double d)
            {
                int num3;
                double num5;
                int num = 0;
                int length = a.GetLength(0);
                double[] numArray = new double[length];
                d = 1.0;
                int num2 = 0;
                while (num2 < length)
                {
                    num5 = 0.0;
                    num3 = 0;
                    while (num3 < length)
                    {
                        double num8 = System.Math.Abs(a[num2, num3]);
                        if (num8 > num5)
                        {
                            num5 = num8;
                        }
                        num3++;
                    }
                    if (num5 == 0.0)
                    {
                        throw new Exception("Singular matrix");
                    }
                    numArray[num2] = 1.0 / num5;
                    num2++;
                }
                for (num3 = 0; num3 < length; num3++)
                {
                    int num4;
                    double num6;
                    double num7;
                    num2 = 0;
                    while (num2 < num3)
                    {
                        num7 = a[num2, num3];
                        num4 = 0;
                        while (num4 < num2)
                        {
                            num7 -= a[num2, num4] * a[num4, num3];
                            num4++;
                        }
                        a[num2, num3] = num7;
                        num2++;
                    }
                    num5 = 0.0;
                    num2 = num3;
                    while (num2 < length)
                    {
                        num7 = a[num2, num3];
                        num4 = 0;
                        while (num4 < num3)
                        {
                            num7 -= a[num2, num4] * a[num4, num3];
                            num4++;
                        }
                        a[num2, num3] = num7;
                        num6 = numArray[num2] * System.Math.Abs(num7);
                        if (num6 >= num5)
                        {
                            num5 = num6;
                            num = num2;
                        }
                        num2++;
                    }
                    if (num3 != num)
                    {
                        for (num4 = 0; num4 < length; num4++)
                        {
                            num6 = a[num, num4];
                            a[num, num4] = a[num3, num4];
                            a[num3, num4] = num6;
                        }
                        d = -d;
                        numArray[num] = numArray[num3];
                    }
                    index[num3] = num;
                    if (a[num3, num3] == 0.0)
                    {
                        a[num3, num3] = 1E-20;
                    }
                    if (num3 != (length - 1))
                    {
                        num6 = 1.0 / a[num3, num3];
                        for (num2 = num3 + 1; num2 < length; num2++)
                        {
                            a[num2, num3] *= num6;
                        }
                    }
                }
            }

            internal class EigenSolver3
            {
                internal double[] mEigenvalue = new double[3];
                internal Vector[] mEigenvector = new Vector[3];
                private const double oneThird = 0.33333333333333331;
                private const double ZERO_TOLERANCE = 1E-08;

                internal void ComputeEigenStuff(double[,] A)
                {
                    double num = System.Math.Abs(A[0, 0]);
                    double num2 = System.Math.Abs(A[0, 1]);
                    double num3 = System.Math.Abs(A[0, 2]);
                    double num4 = System.Math.Abs(A[1, 1]);
                    double num5 = System.Math.Abs(A[1, 2]);
                    double num6 = System.Math.Abs(A[2, 2]);
                    double num7 = Snap.Math.Max(new double[] { num, num2, num3, num4, num5, num6 });
                    double[,] a = A;
                    if (num7 > 1.0)
                    {
                        double s = 1.0 / num7;
                        a = Snap.Math.MatrixMath.Multiply(A, s);
                    }
                    double[] root = new double[3];
                    this.ComputeRoots(a, root);
                    this.mEigenvalue[0] = root[0];
                    this.mEigenvalue[1] = root[1];
                    this.mEigenvalue[2] = root[2];
                    double[] numArray3 = new double[3];
                    Vector[] vectorArray = new Vector[3];
                    for (int i = 0; i < 3; i++)
                    {
                        double[,] m = (double[,]) a.Clone();
                        double num1 = m[0, 0];
                        num1 -= this.mEigenvalue[i];
                        double num14 = m[1, 1];
                        num14 -= this.mEigenvalue[i];
                        double num15 = m[2, 2];
                        num15 -= this.mEigenvalue[i];
                        if (!this.IsPositiveRank(m, ref numArray3[i], ref vectorArray[i]))
                        {
                            if (num7 > 1.0)
                            {
                                for (int j = 0; j < 3; j++)
                                {
                                    this.mEigenvalue[j] *= num7;
                                }
                            }
                            this.mEigenvector[0] = Vector.AxisX;
                            this.mEigenvector[1] = Vector.AxisY;
                            this.mEigenvector[2] = Vector.AxisZ;
                            return;
                        }
                    }
                    double num11 = numArray3[0];
                    int num12 = 0;
                    if (numArray3[1] > num11)
                    {
                        num11 = numArray3[1];
                        num12 = 1;
                    }
                    if (numArray3[2] > num11)
                    {
                        num12 = 2;
                    }
                    if (num12 == 0)
                    {
                        vectorArray[0].Unitize();
                        this.ComputeVectors(a, vectorArray[0], 1, 2, 0);
                    }
                    else if (num12 == 1)
                    {
                        vectorArray[1].Unitize();
                        this.ComputeVectors(a, vectorArray[1], 2, 0, 1);
                    }
                    else
                    {
                        vectorArray[2].Unitize();
                        this.ComputeVectors(a, vectorArray[2], 0, 1, 2);
                    }
                    if (num7 > 1.0)
                    {
                        for (int k = 0; k < 3; k++)
                        {
                            this.mEigenvalue[k] *= num7;
                        }
                    }
                }

                private void ComputeRoots(double[,] A, double[] root)
                {
                    double num = A[0, 0];
                    double num2 = A[0, 1];
                    double num3 = A[0, 2];
                    double num4 = A[1, 1];
                    double num5 = A[1, 2];
                    double num6 = A[2, 2];
                    double num7 = (((((num * num4) * num6) + (((2.0 * num2) * num3) * num5)) - ((num * num5) * num5)) - ((num4 * num3) * num3)) - ((num6 * num2) * num2);
                    double num8 = (((((num * num4) - (num2 * num2)) + (num * num6)) - (num3 * num3)) + (num4 * num6)) - (num5 * num5);
                    double num9 = (num + num4) + num6;
                    double num10 = 0.33333333333333331 * num9;
                    double num11 = 0.33333333333333331 * (num8 - (num9 * num10));
                    if (num11 > 0.0)
                    {
                        num11 = 0.0;
                    }
                    double x = 0.5 * (num7 + (num10 * (((2.0 * num10) * num10) - num8)));
                    double num13 = (x * x) + ((num11 * num11) * num11);
                    if (num13 > 0.0)
                    {
                        num13 = 0.0;
                    }
                    double num14 = System.Math.Sqrt(-num11);
                    double d = 0.33333333333333331 * System.Math.Atan2(System.Math.Sqrt(-num13), x);
                    double num16 = System.Math.Cos(d);
                    double num17 = System.Math.Sin(d);
                    double num18 = num10 + ((2.0 * num14) * num16);
                    double num19 = System.Math.Sqrt(3.0);
                    double num20 = num10 - (num14 * (num16 + (num19 * num17)));
                    double num21 = num10 - (num14 * (num16 - (num19 * num17)));
                    if (num20 >= num18)
                    {
                        root[0] = num18;
                        root[1] = num20;
                    }
                    else
                    {
                        root[0] = num20;
                        root[1] = num18;
                    }
                    if (num21 >= root[1])
                    {
                        root[2] = num21;
                    }
                    else
                    {
                        root[2] = root[1];
                        if (num21 >= root[0])
                        {
                            root[1] = num21;
                        }
                        else
                        {
                            root[1] = root[0];
                            root[0] = num21;
                        }
                    }
                }

                private void ComputeVectors(double[,] A, Vector U2, int i0, int i1, int i2)
                {
                    Vector vector;
                    Vector vector2;
                    double num4;
                    this.GenerateComplementBasis(out vector, out vector2, U2);
                    Vector vector3 = new Vector(Snap.Math.MatrixMath.Multiply(A, vector.Array));
                    double num = this.mEigenvalue[i2] - (vector * vector3);
                    double num2 = (double) (vector2 * vector3);
                    Vector vector4 = new Vector(Snap.Math.MatrixMath.Multiply(A, vector2.Array));
                    double num3 = this.mEigenvalue[i2] - (vector2 * vector4);
                    double num5 = System.Math.Abs(num);
                    int num6 = 0;
                    double num7 = System.Math.Abs(num2);
                    if (num7 > num5)
                    {
                        num5 = num7;
                    }
                    num7 = System.Math.Abs(num3);
                    if (num7 > num5)
                    {
                        num5 = num7;
                        num6 = 1;
                    }
                    if (num5 >= 1E-08)
                    {
                        if (num6 == 0)
                        {
                            num4 = 1.0 / System.Math.Sqrt((num * num) + (num2 * num2));
                            num *= num4;
                            num2 *= num4;
                            this.mEigenvector[i2] = (Vector) ((num2 * vector) + (num * vector2));
                        }
                        else
                        {
                            num4 = 1.0 / System.Math.Sqrt((num3 * num3) + (num2 * num2));
                            num3 *= num4;
                            num2 *= num4;
                            this.mEigenvector[i2] = (Vector) ((num3 * vector) + (num2 * vector2));
                        }
                    }
                    else if (num6 == 0)
                    {
                        this.mEigenvector[i2] = vector2;
                    }
                    else
                    {
                        this.mEigenvector[i2] = vector;
                    }
                    Vector vector5 = Vector.Cross(U2, this.mEigenvector[i2]);
                    vector3 = new Vector(Snap.Math.MatrixMath.Multiply(A, U2.Array));
                    num = this.mEigenvalue[i0] - (U2 * vector3);
                    num2 = (double) (vector5 * vector3);
                    Vector vector6 = new Vector(Snap.Math.MatrixMath.Multiply(A, vector5.Array));
                    num3 = this.mEigenvalue[i0] - (vector5 * vector6);
                    num5 = System.Math.Abs(num);
                    num6 = 0;
                    num7 = System.Math.Abs(num2);
                    if (num7 > num5)
                    {
                        num5 = num7;
                    }
                    num7 = System.Math.Abs(num3);
                    if (num7 > num5)
                    {
                        num5 = num7;
                        num6 = 1;
                    }
                    if (num5 >= 1E-08)
                    {
                        if (num6 == 0)
                        {
                            num4 = 1.0 / System.Math.Sqrt((num * num) + (num2 * num2));
                            num *= num4;
                            num2 *= num4;
                            this.mEigenvector[i0] = (Vector) ((num2 * U2) + (num * vector5));
                        }
                        else
                        {
                            num4 = 1.0 / System.Math.Sqrt((num3 * num3) + (num2 * num2));
                            num3 *= num4;
                            num2 *= num4;
                            this.mEigenvector[i0] = (Vector) ((num3 * U2) + (num2 * vector5));
                        }
                    }
                    else if (num6 == 0)
                    {
                        this.mEigenvector[i0] = vector5;
                    }
                    else
                    {
                        this.mEigenvector[i0] = U2;
                    }
                    this.mEigenvector[i1] = Vector.Cross(this.mEigenvector[i2], this.mEigenvector[i0]);
                }

                private void GenerateComplementBasis(out Vector vec0, out Vector vec1, Vector vec2)
                {
                    double num;
                    if (System.Math.Abs(vec2.X) >= System.Math.Abs(vec2.Y))
                    {
                        num = 1.0 / System.Math.Sqrt((vec2.X * vec2.X) + (vec2.Z * vec2.Z));
                        vec0.X = -vec2.Z * num;
                        vec0.Y = 0.0;
                        vec0.Z = vec2.X * num;
                        vec1.X = vec2.Y * vec0.Z;
                        vec1.Y = (vec2.Z * vec0.X) - (vec2.X * vec0.Z);
                        vec1.Z = -vec2.Y * vec0.X;
                    }
                    else
                    {
                        num = 1.0 / System.Math.Sqrt((vec2.Y * vec2.Y) + (vec2.Z * vec2.Z));
                        vec0.X = 0.0;
                        vec0.Y = vec2.Z * num;
                        vec0.Z = -vec2.Y * num;
                        vec1.X = (vec2.Y * vec0.Z) - (vec2.Z * vec0.Y);
                        vec1.Y = -vec2.X * vec0.Z;
                        vec1.Z = vec2.X * vec0.Y;
                    }
                }

                private bool IsPositiveRank(double[,] M, ref double maxEntry, ref Vector maxRow)
                {
                    maxEntry = -1.0;
                    int i = -1;
                    for (int j = 0; j < 3; j++)
                    {
                        for (int k = j; k < 3; k++)
                        {
                            double num4 = System.Math.Abs(M[j, k]);
                            if (num4 > maxEntry)
                            {
                                maxEntry = num4;
                                i = j;
                            }
                        }
                    }
                    maxRow = new Vector(Snap.Math.MatrixMath.GetRow(M, i));
                    return (maxEntry >= 1E-08);
                }
            }

            public class EigenSystemResult
            {
                internal EigenSystemResult(double eigenvalue, double[] eigenvector)
                {
                    this.Eigenvalue = eigenvalue;
                    this.Eigenvector = eigenvector;
                }

                public double Eigenvalue { get; internal set; }

                public double[] Eigenvector { get; internal set; }
            }
        }

        public static class MatrixMath
        {
            public static double[,] Add(double[,] a, double[,] b)
            {
                int num = RowCount(a);
                int num2 = ColumnCount(a);
                double[,] numArray = new double[num, num2];
                for (int i = 0; i < num; i++)
                {
                    for (int j = 0; j < num2; j++)
                    {
                        numArray[i, j] = a[i, j] + b[i, j];
                    }
                }
                return numArray;
            }

            public static int ColumnCount(Position[,] a)
            {
                return a.GetLength(1);
            }

            public static int ColumnCount(Vector[,] a)
            {
                return a.GetLength(1);
            }

            public static int ColumnCount(double[,] a)
            {
                return a.GetLength(1);
            }

            public static double[] Copy(double[] original)
            {
                int length = original.Length;
                double[] numArray = new double[length];
                for (int i = 0; i < length; i++)
                {
                    numArray[i] = original[i];
                }
                return numArray;
            }

            public static double[,] Copy(double[,] original)
            {
                int num = RowCount(original);
                int num2 = ColumnCount(original);
                double[,] numArray = new double[num, num2];
                for (int i = 0; i < num; i++)
                {
                    for (int j = 0; j < num2; j++)
                    {
                        numArray[i, j] = original[i, j];
                    }
                }
                return numArray;
            }

            public static Position[] GetColumn(Position[,] a, int j)
            {
                int num = RowCount(a);
                Position[] positionArray = new Position[num];
                for (int i = 0; i < num; i++)
                {
                    positionArray[i] = a[i, j];
                }
                return positionArray;
            }

            public static Vector[] GetColumn(Vector[,] a, int j)
            {
                int num = RowCount(a);
                Vector[] vectorArray = new Vector[num];
                for (int i = 0; i < num; i++)
                {
                    vectorArray[i] = a[i, j];
                }
                return vectorArray;
            }

            public static double[] GetColumn(double[,] a, int j)
            {
                int num = RowCount(a);
                double[] numArray = new double[num];
                for (int i = 0; i < num; i++)
                {
                    numArray[i] = a[i, j];
                }
                return numArray;
            }

            public static Position[] GetRow(Position[,] a, int i)
            {
                int num = ColumnCount(a);
                Position[] positionArray = new Position[num];
                for (int j = 0; j < num; j++)
                {
                    positionArray[j] = a[i, j];
                }
                return positionArray;
            }

            public static Vector[] GetRow(Vector[,] a, int i)
            {
                int num = ColumnCount(a);
                Vector[] vectorArray = new Vector[num];
                for (int j = 0; j < num; j++)
                {
                    vectorArray[j] = a[i, j];
                }
                return vectorArray;
            }

            public static double[] GetRow(double[,] a, int i)
            {
                int num = ColumnCount(a);
                double[] numArray = new double[num];
                for (int j = 0; j < num; j++)
                {
                    numArray[j] = a[i, j];
                }
                return numArray;
            }

            public static double[,] IdentityMatrix(int n)
            {
                double[,] numArray = ZeroMatrix(n);
                for (int i = 0; i < n; i++)
                {
                    numArray[i, i] = 1.0;
                }
                return numArray;
            }

            public static double[] MatrixToVector(double[,] matrix)
            {
                int length = matrix.GetLength(0);
                int num2 = matrix.GetLength(1);
                double[] numArray = new double[length * num2];
                for (int i = 0; i < length; i++)
                {
                    for (int j = 0; j < num2; j++)
                    {
                        numArray[(i * num2) + j] = matrix[i, j];
                    }
                }
                return numArray;
            }

            public static int[] MatrixToVector(int[,] matrix)
            {
                int length = matrix.GetLength(0);
                int num2 = matrix.GetLength(1);
                int[] numArray = new int[length * num2];
                for (int i = 0; i < length; i++)
                {
                    for (int j = 0; j < num2; j++)
                    {
                        numArray[(i * num2) + j] = matrix[i, j];
                    }
                }
                return numArray;
            }

            public static Vector Multiply(Vector[] u, double[] v)
            {
                return Multiply(v, u);
            }

            public static Vector Multiply(double[] u, Vector[] v)
            {
                int length = u.Length;
                Vector vector = new Vector();
                for (int i = 0; i < length; i++)
                {
                    vector += (Vector) (u[i] * v[i]);
                }
                return vector;
            }

            public static Vector[,] Multiply(double[,] a, Vector[,] b)
            {
                int num = RowCount(a);
                ColumnCount(a);
                int num2 = ColumnCount(b);
                Vector[,] vectorArray = new Vector[num, num2];
                for (int i = 0; i < num; i++)
                {
                    for (int j = 0; j < num2; j++)
                    {
                        vectorArray[i, j] = Multiply(GetRow(a, i), GetColumn(b, j));
                    }
                }
                return vectorArray;
            }

            public static double[,] Multiply(double[,] a, double[,] b)
            {
                int num = RowCount(a);
                ColumnCount(a);
                int num2 = ColumnCount(b);
                double[,] numArray = new double[num, num2];
                for (int i = 0; i < num; i++)
                {
                    for (int j = 0; j < num2; j++)
                    {
                        numArray[i, j] = Multiply(GetRow(a, i), GetColumn(b, j));
                    }
                }
                return numArray;
            }

            public static double Multiply(double[] u, double[] v)
            {
                int length = u.Length;
                double num2 = 0.0;
                for (int i = 0; i < length; i++)
                {
                    num2 += u[i] * v[i];
                }
                return num2;
            }

            public static Vector[,] Multiply(Vector[,] a, double[,] b)
            {
                int num = RowCount(a);
                ColumnCount(a);
                int num2 = ColumnCount(b);
                Vector[,] vectorArray = new Vector[num, num2];
                for (int i = 0; i < num; i++)
                {
                    for (int j = 0; j < num2; j++)
                    {
                        vectorArray[i, j] = Multiply(GetRow(a, i), GetColumn(b, j));
                    }
                }
                return vectorArray;
            }

            public static double[] Multiply(double[,] a, double[] b)
            {
                int num = RowCount(a);
                ColumnCount(a);
                double[] numArray = new double[num];
                for (int i = 0; i < num; i++)
                {
                    numArray[i] = Multiply(GetRow(a, i), b);
                }
                return numArray;
            }

            public static double[,] Multiply(double[,] a, double s)
            {
                int num = RowCount(a);
                int num2 = ColumnCount(a);
                double[,] numArray = new double[num, num2];
                for (int i = 0; i < num; i++)
                {
                    for (int j = 0; j < num2; j++)
                    {
                        numArray[i, j] = s * a[i, j];
                    }
                }
                return numArray;
            }

            public static int RowCount(Position[,] a)
            {
                return a.GetLength(0);
            }

            public static int RowCount(Vector[,] a)
            {
                return a.GetLength(0);
            }

            public static int RowCount(double[,] a)
            {
                return a.GetLength(0);
            }

            public static Position[,] Transpose(Position[,] a)
            {
                int num = RowCount(a);
                int num2 = ColumnCount(a);
                Position[,] positionArray = new Position[num2, num];
                for (int i = 0; i < num; i++)
                {
                    for (int j = 0; j < num2; j++)
                    {
                        positionArray[j, i] = a[i, j];
                    }
                }
                return positionArray;
            }

            public static double[,] Transpose(double[,] a)
            {
                int num = RowCount(a);
                int num2 = ColumnCount(a);
                double[,] numArray = new double[num2, num];
                for (int i = 0; i < num; i++)
                {
                    for (int j = 0; j < num2; j++)
                    {
                        numArray[j, i] = a[i, j];
                    }
                }
                return numArray;
            }

            public static double[,] VectorToMatrix(double[] vector, int nrows, int ncols)
            {
                double[,] numArray = new double[nrows, ncols];
                for (int i = 0; i < nrows; i++)
                {
                    for (int j = 0; j < ncols; j++)
                    {
                        numArray[i, j] = vector[(i * ncols) + j];
                    }
                }
                return numArray;
            }

            public static int[,] VectorToMatrix(int[] vector, int nrows, int ncols)
            {
                int[,] numArray = new int[nrows, ncols];
                for (int i = 0; i < nrows; i++)
                {
                    for (int j = 0; j < ncols; j++)
                    {
                        numArray[i, j] = vector[(i * ncols) + j];
                    }
                }
                return numArray;
            }

            public static double[,] ZeroMatrix(int n)
            {
                double[,] numArray = new double[n, n];
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        numArray[i, j] = 0.0;
                    }
                }
                return numArray;
            }
        }

        public static class SplineMath
        {
            public static double[,] BasisMatrix(double[] knots, int k, double[] nodes)
            {
                int length = knots.Length;
                int num2 = length - k;
                double[,] numArray = new double[num2, num2];
                for (int i = 0; i < num2; i++)
                {
                    for (int j = 0; j < num2; j++)
                    {
                        numArray[j, i] = EvaluateBasisFunction(knots, i, k, nodes[j]);
                    }
                }
                return numArray;
            }

            public static double[] BezierKnots(int m)
            {
                int num;
                double[] numArray = new double[2 * (m + 1)];
                for (num = 0; num <= m; num++)
                {
                    numArray[num] = 0.0;
                }
                for (num = 0; num <= m; num++)
                {
                    numArray[((2 * m) + 1) - num] = 1.0;
                }
                return numArray;
            }

            public static Position[] BsplineInterpolation(Position[] intPoints, double[] nodes, double[] knots)
            {
                int length = intPoints.Length;
                int num2 = knots.Length;
                int k = num2 - length;
                double[,] a = BasisMatrix(knots, k, nodes);
                int[] index = new int[length];
                double d = 1.0;
                Snap.Math.LinearAlgebra.LUDecomposition(a, index, d);
                double[] x = Position.GetX(intPoints);
                double[] y = Position.GetY(intPoints);
                double[] z = Position.GetZ(intPoints);
                Snap.Math.LinearAlgebra.BackSubstitution(a, index, x);
                Snap.Math.LinearAlgebra.BackSubstitution(a, index, y);
                Snap.Math.LinearAlgebra.BackSubstitution(a, index, z);
                return Position.PositionsFromCoordinates(x, y, z);
            }

            public static double[] BsplineInterpolation(double[] intValues, double[] nodes, double[] knots)
            {
                int length = intValues.Length;
                int num2 = knots.Length;
                int k = num2 - length;
                double[,] a = BasisMatrix(knots, k, nodes);
                int[] index = new int[length];
                double d = 1.0;
                Snap.Math.LinearAlgebra.LUDecomposition(a, index, d);
                double[] array = new double[length];
                intValues.CopyTo(array, 0);
                Snap.Math.LinearAlgebra.BackSubstitution(a, index, array);
                return array;
            }

            public static double[] CentripedalNodes(Position[] intPoints)
            {
                int length = intPoints.Length;
                double[] numArray = new double[length];
                numArray[0] = 0.0;
                for (int i = 0; i < (length - 1); i++)
                {
                    numArray[i + 1] = numArray[i] + System.Math.Sqrt(Vector.Norm((Vector) (intPoints[i + 1] - intPoints[i])));
                }
                double[] numArray2 = new double[length];
                for (int j = 0; j < length; j++)
                {
                    numArray2[j] = numArray[j] / numArray[length - 1];
                }
                return numArray2;
            }

            public static double[] ChordalNodes(Position[] intPoints)
            {
                int length = intPoints.Length;
                double[] numArray = new double[length];
                numArray[0] = 0.0;
                for (int i = 0; i < (length - 1); i++)
                {
                    numArray[i + 1] = numArray[i] + Vector.Norm((Vector) (intPoints[i + 1] - intPoints[i]));
                }
                double[] numArray2 = new double[length];
                for (int j = 0; j < length; j++)
                {
                    numArray2[j] = numArray[j] / numArray[length - 1];
                }
                return numArray2;
            }

            public static double[][] ChordalNodes(Position[,] intPoints)
            {
                double[] numArray = ChordalNodesU(intPoints);
                double[] numArray2 = ChordalNodesU(Snap.Math.MatrixMath.Transpose(intPoints));
                return new double[][] { numArray2, numArray };
            }

            private static double[] ChordalNodesU(Position[,] intPoints)
            {
                int length = intPoints.GetLength(0);
                int num2 = intPoints.GetLength(1);
                Position[] positionArray = new Position[num2];
                double[,] a = new double[length, num2];
                for (int i = 0; i < length; i++)
                {
                    double[] numArray2 = ChordalNodes(Snap.Math.MatrixMath.GetRow(intPoints, i));
                    for (int k = 0; k < num2; k++)
                    {
                        a[i, k] = numArray2[k];
                    }
                }
                double[] numArray3 = new double[num2];
                for (int j = 0; j < num2; j++)
                {
                    numArray3[j] = Snap.Math.Mean(Snap.Math.MatrixMath.GetColumn(a, j));
                }
                return numArray3;
            }

            public static double EvaluateBasisFunction(double[] knots, int i, int k, double nodes)
            {
                double[] numArray = new double[1];
                return EvaluateBasisFunction(knots, i, k, nodes, 0)[0];
            }

            private static double[] EvaluateBasisFunction(double[] knots, int i, int k, double t, int derivs)
            {
                double num8;
                int num11;
                int length = knots.Length;
                double[] numArray = new double[derivs + 1];
                int num2 = 0x19;
                double num3 = 1E-07;
                double num4 = 1E-11;
                double[,] numArray2 = new double[num2, num2];
                if (derivs >= k)
                {
                    for (num11 = k; num11 <= derivs; num11++)
                    {
                        numArray[k] = 0.0;
                    }
                    derivs = k - 1;
                }
                if (System.Math.Abs((double) (t - knots[length - 1])) > num4)
                {
                    num8 = t;
                }
                else
                {
                    num8 = knots[length - 1] - num4;
                }
                int index = i;
                while (index < (i + k))
                {
                    numArray2[0, index - i] = ((knots[index] <= num8) && (knots[index + 1] > num8)) ? 1.0 : 0.0;
                    index++;
                }
                num11 = 1;
                while (num11 <= derivs)
                {
                    index = 0;
                    while (index < k)
                    {
                        numArray2[num11, index] = numArray2[0, index];
                        index++;
                    }
                    num11++;
                }
                for (index = 2; index <= k; index++)
                {
                    num11 = 0;
                    while (num11 <= derivs)
                    {
                        double num7;
                        double num5 = knots[(i + index) - 1] - knots[i];
                        if (System.Math.Abs(num5) <= num3)
                        {
                            num7 = 0.0;
                        }
                        else
                        {
                            num7 = numArray2[num11, 0] / num5;
                        }
                        for (int j = 0; j <= (k - index); j++)
                        {
                            double num6;
                            num5 = knots[(i + j) + index] - knots[(i + j) + 1];
                            if (System.Math.Abs(num5) <= num3)
                            {
                                num6 = 0.0;
                            }
                            else
                            {
                                num6 = numArray2[num11, j + 1] / num5;
                            }
                            if (index < ((k - num11) + 1))
                            {
                                numArray2[num11, j] = (num7 * (t - knots[i + j])) + (num6 * (knots[(i + j) + index] - t));
                            }
                            else
                            {
                                numArray2[num11, j] = (((2 * k) - index) - num11) * (num7 - num6);
                            }
                            num7 = num6;
                        }
                        num11++;
                    }
                }
                for (num11 = 0; num11 <= derivs; num11++)
                {
                    numArray[num11] = numArray2[num11, 0];
                }
                return numArray;
            }

            public static double[] GrevilleKnots(double[] nodes, int m)
            {
                int num = m + 1;
                int length = nodes.Length;
                double[] numArray = new double[length + num];
                for (int i = 0; i < num; i++)
                {
                    numArray[i] = 0.0;
                }
                for (int j = num; j < length; j++)
                {
                    numArray[j] = Sum(nodes, j - m, j - 1) / ((double) m);
                }
                for (int k = length; k < (length + num); k++)
                {
                    numArray[k] = 1.0;
                }
                return numArray;
            }

            private static double Sum(double[] tau, int i, int j)
            {
                double num = 0.0;
                for (int k = i; k <= j; k++)
                {
                    num += tau[k];
                }
                return num;
            }
        }
    }
}

