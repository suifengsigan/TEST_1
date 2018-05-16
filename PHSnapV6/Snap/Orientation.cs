namespace Snap
{
    using NXOpen;
    using System;

    public class Orientation
    {
        private double m_Xx;
        private double m_Xy;
        private double m_Xz;
        private double m_Yx;
        private double m_Yy;
        private double m_Yz;
        private double m_Zx;
        private double m_Zy;
        private double m_Zz;

        public Orientation()
        {
            this.m_Xx = 1.0;
            this.m_Xy = 0.0;
            this.m_Xz = 0.0;
            this.m_Yx = 0.0;
            this.m_Yy = 1.0;
            this.m_Yz = 0.0;
            this.m_Zx = 0.0;
            this.m_Zy = 0.0;
            this.m_Zz = 1.0;
        }

        public Orientation(Matrix3x3 matrix)
        {
            this.m_Xx = matrix.Xx;
            this.m_Xy = matrix.Xy;
            this.m_Xz = matrix.Xz;
            this.m_Yx = matrix.Yx;
            this.m_Yy = matrix.Yy;
            this.m_Yz = matrix.Yz;
            this.m_Zx = matrix.Zx;
            this.m_Zy = matrix.Zy;
            this.m_Zz = matrix.Zz;
        }

        public Orientation(Vector axisZ)
        {
            Vector vector;
            if (System.Math.Abs(axisZ.X) < System.Math.Abs(axisZ.Y))
            {
                vector = new Vector(1.0, 0.0, 0.0);
            }
            else
            {
                vector = new Vector(0.0, 1.0, 0.0);
            }
            axisZ = Vector.Unit(axisZ);
            Vector v = Vector.Unit(Vector.Cross(vector, axisZ));
            Vector vector3 = Vector.Unit(Vector.Cross(axisZ, v));
            this.m_Xx = v.X;
            this.m_Xy = v.Y;
            this.m_Xz = v.Z;
            this.m_Yx = vector3.X;
            this.m_Yy = vector3.Y;
            this.m_Yz = vector3.Z;
            this.m_Zx = axisZ.X;
            this.m_Zy = axisZ.Y;
            this.m_Zz = axisZ.Z;
        }

        public Orientation(Vector axisX, Vector axisY)
        {
            Vector u = Vector.UnitCross(axisX, axisY);
            axisX = Vector.Unit(axisX);
            axisY = Vector.Cross(u, axisX);
            this.m_Xx = axisX.X;
            this.m_Xy = axisX.Y;
            this.m_Xz = axisX.Z;
            this.m_Yx = axisY.X;
            this.m_Yy = axisY.Y;
            this.m_Yz = axisY.Z;
            this.m_Zx = u.X;
            this.m_Zy = u.Y;
            this.m_Zz = u.Z;
        }

        public Orientation(Vector axisX, Vector axisY, Vector axisZ)
        {
            this.m_Xx = axisX.X;
            this.m_Xy = axisX.Y;
            this.m_Xz = axisX.Z;
            this.m_Yx = axisY.X;
            this.m_Yy = axisY.Y;
            this.m_Yz = axisY.Z;
            this.m_Zx = axisZ.X;
            this.m_Zy = axisZ.Y;
            this.m_Zz = axisZ.Z;
        }

        public static implicit operator Orientation(Matrix3x3 matrix)
        {
            return new Orientation(matrix);
        }

        public static implicit operator Matrix3x3(Orientation matrix)
        {
            return new Matrix3x3 { Xx = matrix.AxisX.X, Xy = matrix.AxisX.Y, Xz = matrix.AxisX.Z, Yx = matrix.AxisY.X, Yy = matrix.AxisY.Y, Yz = matrix.AxisY.Z, Zx = matrix.AxisZ.X, Zy = matrix.AxisZ.Y, Zz = matrix.AxisZ.Z };
        }

        public double[,] Array
        {
            get
            {
                return new double[,] { { this.m_Xx, this.m_Xy, this.m_Xz }, { this.m_Yx, this.m_Yy, this.m_Yz }, { this.m_Zx, this.m_Zy, this.m_Zz } };
            }
            set
            {
                this.m_Xx = value[0, 0];
                this.m_Xy = value[0, 1];
                this.m_Xz = value[0, 1];
                this.m_Yx = value[0, 0];
                this.m_Yy = value[0, 1];
                this.m_Yz = value[0, 1];
                this.m_Zx = value[0, 0];
                this.m_Zy = value[0, 1];
                this.m_Zz = value[0, 1];
            }
        }

        public Vector AxisX
        {
            get
            {
                return new Vector(this.m_Xx, this.m_Xy, this.m_Xz);
            }
            set
            {
                this.m_Xx = this.AxisX.X;
                this.m_Xy = this.AxisX.Y;
                this.m_Xz = this.AxisX.Z;
            }
        }

        public Vector AxisY
        {
            get
            {
                return new Vector(this.m_Yx, this.m_Yy, this.m_Yz);
            }
            set
            {
                this.m_Yx = this.AxisY.X;
                this.m_Yy = this.AxisY.Y;
                this.m_Yz = this.AxisY.Z;
            }
        }

        public Vector AxisZ
        {
            get
            {
                return new Vector(this.m_Zx, this.m_Zy, this.m_Zz);
            }
            set
            {
                this.m_Zx = this.AxisZ.X;
                this.m_Zy = this.AxisZ.Y;
                this.m_Zz = this.AxisZ.Z;
            }
        }

        public static Orientation Identity
        {
            get
            {
                return new Orientation();
            }
        }
    }
}

