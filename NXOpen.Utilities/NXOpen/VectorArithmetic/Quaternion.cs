namespace NXOpen.VectorArithmetic
{
    using System;

    public class Quaternion
    {
        public double q0 = 1.0;
        public double q1 = 0.0;
        public double q2 = 0.0;
        public double q3 = 0.0;

        public static Quaternion CalcAxisRot(double x, double y, double z, double angle)
        {
            Quaternion quaternion = new Quaternion();
            double d = ((x * x) + (y * y)) + (z * z);
            if (d > 0.0)
            {
                angle *= 0.5;
                quaternion.q0 = Math.Cos(angle);
                d = Math.Sin(angle) / Math.Sqrt(d);
                quaternion.q1 = x * d;
                quaternion.q2 = y * d;
                quaternion.q3 = z * d;
            }
            return quaternion;
        }

        public Matrix3 GetMatrix()
        {
            Matrix3 matrix = new Matrix3();
            double num = (2.0 * this.q1) * this.q1;
            double num2 = (2.0 * this.q2) * this.q2;
            double num3 = (2.0 * this.q3) * this.q3;
            matrix.m[0] = (1.0 - num2) - num3;
            matrix.m[1] = 2.0 * ((this.q1 * this.q2) - (this.q0 * this.q3));
            matrix.m[2] = 2.0 * ((this.q1 * this.q3) + (this.q0 * this.q2));
            matrix.m[3] = 2.0 * ((this.q1 * this.q2) + (this.q0 * this.q3));
            matrix.m[4] = (1.0 - num) - num3;
            matrix.m[5] = 2.0 * ((this.q2 * this.q3) - (this.q0 * this.q1));
            matrix.m[6] = 2.0 * ((this.q1 * this.q3) - (this.q0 * this.q2));
            matrix.m[7] = 2.0 * ((this.q2 * this.q3) + (this.q0 * this.q1));
            matrix.m[8] = (1.0 - num) - num2;
            return matrix;
        }
    }
}

