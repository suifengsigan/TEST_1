namespace NXOpen.VectorArithmetic
{
    using System;

    public class Matrix3
    {
        public double[] m;

        public Matrix3()
        {
            this.m = new double[] { 1.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 1.0 };
        }

        public Matrix3(double m11, double m12, double m13, double m21, double m22, double m23, double m31, double m32, double m33)
        {
            this.m = new double[] { m11, m12, m13, m21, m22, m23, m31, m32, m33 };
        }

        public Vector3 Apply(Vector3 vector)
        {
            return new Vector3 { x = ((this.m[0] * vector.x) + (this.m[1] * vector.y)) + (this.m[2] * vector.z), y = ((this.m[3] * vector.x) + (this.m[4] * vector.y)) + (this.m[5] * vector.z), z = ((this.m[6] * vector.x) + (this.m[7] * vector.y)) + (this.m[8] * vector.z) };
        }

        public static Matrix3 Compose(Vector3 vecX, Vector3 vecY, Vector3 vecZ)
        {
            Matrix3 matrix = new Matrix3();
            matrix.m[0] = vecX.x;
            matrix.m[1] = vecY.x;
            matrix.m[2] = vecZ.x;
            matrix.m[3] = vecX.y;
            matrix.m[4] = vecY.y;
            matrix.m[5] = vecZ.y;
            matrix.m[6] = vecX.z;
            matrix.m[7] = vecY.z;
            matrix.m[8] = vecZ.z;
            return matrix;
        }

        public Vector3 GetColumn(int nCol)
        {
            if ((nCol < 0) || (nCol > 2))
            {
                throw new ArgumentOutOfRangeException("nCol", nCol, "column parameter must be from 0 to 2");
            }
            return new Vector3 { x = this.m[nCol], y = this.m[3 + nCol], z = this.m[6 + nCol] };
        }

        public void MultFront(Matrix3 matrix)
        {
            double num = ((matrix.m[0] * this.m[0]) + (matrix.m[1] * this.m[3])) + (matrix.m[2] * this.m[6]);
            double num2 = ((matrix.m[0] * this.m[1]) + (matrix.m[1] * this.m[4])) + (matrix.m[2] * this.m[7]);
            double num3 = ((matrix.m[0] * this.m[2]) + (matrix.m[1] * this.m[5])) + (matrix.m[2] * this.m[8]);
            double num4 = ((matrix.m[3] * this.m[0]) + (matrix.m[4] * this.m[3])) + (matrix.m[5] * this.m[6]);
            double num5 = ((matrix.m[3] * this.m[1]) + (matrix.m[4] * this.m[4])) + (matrix.m[5] * this.m[7]);
            double num6 = ((matrix.m[3] * this.m[2]) + (matrix.m[4] * this.m[5])) + (matrix.m[5] * this.m[8]);
            double num7 = ((matrix.m[6] * this.m[0]) + (matrix.m[7] * this.m[3])) + (matrix.m[8] * this.m[6]);
            double num8 = ((matrix.m[6] * this.m[1]) + (matrix.m[7] * this.m[4])) + (matrix.m[8] * this.m[7]);
            double num9 = ((matrix.m[6] * this.m[2]) + (matrix.m[7] * this.m[5])) + (matrix.m[8] * this.m[8]);
            this.m[0] = num;
            this.m[1] = num2;
            this.m[2] = num3;
            this.m[3] = num4;
            this.m[4] = num5;
            this.m[5] = num6;
            this.m[6] = num7;
            this.m[7] = num8;
            this.m[8] = num9;
        }

        public void MultRear(Matrix3 matrix)
        {
            double num = ((this.m[0] * matrix.m[0]) + (this.m[1] * matrix.m[3])) + (this.m[2] * matrix.m[6]);
            double num2 = ((this.m[0] * matrix.m[1]) + (this.m[1] * matrix.m[4])) + (this.m[2] * matrix.m[7]);
            double num3 = ((this.m[0] * matrix.m[2]) + (this.m[1] * matrix.m[5])) + (this.m[2] * matrix.m[8]);
            double num4 = ((this.m[3] * matrix.m[0]) + (this.m[4] * matrix.m[3])) + (this.m[5] * matrix.m[6]);
            double num5 = ((this.m[3] * matrix.m[1]) + (this.m[4] * matrix.m[4])) + (this.m[5] * matrix.m[7]);
            double num6 = ((this.m[3] * matrix.m[2]) + (this.m[4] * matrix.m[5])) + (this.m[5] * matrix.m[8]);
            double num7 = ((this.m[6] * matrix.m[0]) + (this.m[7] * matrix.m[3])) + (this.m[8] * matrix.m[6]);
            double num8 = ((this.m[6] * matrix.m[1]) + (this.m[7] * matrix.m[4])) + (this.m[8] * matrix.m[7]);
            double num9 = ((this.m[6] * matrix.m[2]) + (this.m[7] * matrix.m[5])) + (this.m[8] * matrix.m[8]);
            this.m[0] = num;
            this.m[1] = num2;
            this.m[2] = num3;
            this.m[3] = num4;
            this.m[4] = num5;
            this.m[5] = num6;
            this.m[6] = num7;
            this.m[7] = num8;
            this.m[8] = num9;
        }

        public static Matrix3 operator *(Matrix3 op1, Matrix3 op2)
        {
            Matrix3 matrix = new Matrix3();
            matrix.m[0] = ((op1.m[0] * op2.m[0]) + (op1.m[1] * op2.m[3])) + (op1.m[2] * op2.m[6]);
            matrix.m[1] = ((op1.m[0] * op2.m[1]) + (op1.m[1] * op2.m[4])) + (op1.m[2] * op2.m[7]);
            matrix.m[2] = ((op1.m[0] * op2.m[2]) + (op1.m[1] * op2.m[5])) + (op1.m[2] * op2.m[8]);
            matrix.m[3] = ((op1.m[3] * op2.m[0]) + (op1.m[4] * op2.m[3])) + (op1.m[5] * op2.m[6]);
            matrix.m[4] = ((op1.m[3] * op2.m[1]) + (op1.m[4] * op2.m[4])) + (op1.m[5] * op2.m[7]);
            matrix.m[5] = ((op1.m[3] * op2.m[2]) + (op1.m[4] * op2.m[5])) + (op1.m[5] * op2.m[8]);
            matrix.m[6] = ((op1.m[6] * op2.m[0]) + (op1.m[7] * op2.m[3])) + (op1.m[8] * op2.m[6]);
            matrix.m[7] = ((op1.m[6] * op2.m[1]) + (op1.m[7] * op2.m[4])) + (op1.m[8] * op2.m[7]);
            matrix.m[8] = ((op1.m[6] * op2.m[2]) + (op1.m[7] * op2.m[5])) + (op1.m[8] * op2.m[8]);
            return matrix;
        }

        public void Rotate(double x, double y, double z, double angle)
        {
            Matrix3 matrix = Quaternion.CalcAxisRot(x, y, z, angle).GetMatrix();
            this.MultFront(matrix);
        }

        public Matrix3 Transpose
        {
            get
            {
                Matrix3 matrix = new Matrix3();
                matrix.m[0] = this.m[0];
                matrix.m[1] = this.m[3];
                matrix.m[2] = this.m[6];
                matrix.m[3] = this.m[1];
                matrix.m[4] = this.m[4];
                matrix.m[5] = this.m[7];
                matrix.m[6] = this.m[2];
                matrix.m[7] = this.m[5];
                matrix.m[8] = this.m[8];
                return matrix;
            }
        }
    }
}

