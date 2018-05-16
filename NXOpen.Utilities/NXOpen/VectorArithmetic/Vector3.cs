namespace NXOpen.VectorArithmetic
{
    using System;

    public class Vector3
    {
        public double x;
        public double y;
        public double z;

        public Vector3()
        {
            this.x = 0.0;
            this.y = 0.0;
            this.z = 0.0;
        }

        public Vector3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3 Cross(Vector3 vector)
        {
            return new Vector3 { x = (this.y * vector.z) - (this.z * vector.y), y = (this.z * vector.x) - (this.x * vector.z), z = (this.x * vector.y) - (this.y * vector.x) };
        }

        public double Dot(Vector3 vector)
        {
            return (((vector.x * this.x) + (vector.y * this.y)) + (vector.z * this.z));
        }

        public double LengthSqr()
        {
            return (((this.x * this.x) + (this.y * this.y)) + (this.z * this.z));
        }

        public void Normalize()
        {
            double d = this.LengthSqr();
            if (d != 0.0)
            {
                double num2 = Math.Sqrt(d);
                this.x /= num2;
                this.y /= num2;
                this.z /= num2;
            }
        }

        public static Vector3 operator +(Vector3 vec1, Vector3 vec2)
        {
            return new Vector3 { x = vec1.x + vec2.x, y = vec1.y + vec2.y, z = vec1.z + vec2.z };
        }

        public static Vector3 operator /(Vector3 vec, double fDiv)
        {
            return new Vector3 { x = vec.x / fDiv, y = vec.y / fDiv, z = vec.z / fDiv };
        }

        public static Vector3 operator *(Vector3 vec, double fMult)
        {
            return new Vector3 { x = fMult * vec.x, y = fMult * vec.y, z = fMult * vec.z };
        }

        public static Vector3 operator *(double fMult, Vector3 vec)
        {
            return new Vector3 { x = fMult * vec.x, y = fMult * vec.y, z = fMult * vec.z };
        }

        public static Vector3 operator -(Vector3 vec1, Vector3 vec2)
        {
            return new Vector3 { x = vec1.x - vec2.x, y = vec1.y - vec2.y, z = vec1.z - vec2.z };
        }

        public static Vector3 operator -(Vector3 vec)
        {
            return new Vector3 { x = -vec.x, y = -vec.y, z = -vec.z };
        }
    }
}

