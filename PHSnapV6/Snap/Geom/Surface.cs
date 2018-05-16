namespace Snap.Geom
{
    using Snap;
    using System;
    using System.Runtime.CompilerServices;

    public class Surface
    {
        internal Surface()
        {
        }

        public class Blend : Surface
        {
            internal Blend(double radius)
            {
                this.Radius = radius;
            }

            public double Radius { get; set; }
        }

        public class Bsurface : Surface
        {
            internal Bsurface(Position[,] poles, double[] knotsU, double[] knotsV)
            {
                this.Poles = poles;
                this.KnotsU = knotsU;
                this.KnotsV = knotsV;
                this.Weights = this.UnitWeights(this.OrderU, this.OrderV);
            }

            internal Bsurface(Position[,] poles, double[,] weights, double[] knotsU, double[] knotsV)
            {
                this.Poles = poles;
                this.Weights = weights;
                this.KnotsU = knotsU;
                this.KnotsV = knotsV;
            }

            private double[,] UnitWeights(int nu, int nv)
            {
                double[,] numArray = new double[nu, nv];
                for (int i = 0; i < nu; i++)
                {
                    for (int j = 0; j < nv; j++)
                    {
                        numArray[i, j] = 1.0;
                    }
                }
                return numArray;
            }

            public int DegreeU
            {
                get
                {
                    return (this.OrderU - 1);
                }
            }

            public int DegreeV
            {
                get
                {
                    return (this.OrderV - 1);
                }
            }

            public double[] KnotsU { get; set; }

            public double[] KnotsV { get; set; }

            public int OrderU
            {
                get
                {
                    return this.Poles.GetLength(0);
                }
            }

            public int OrderV
            {
                get
                {
                    return this.Poles.GetLength(1);
                }
            }

            public Position[,] Poles { get; set; }

            public double[,] Weights { get; set; }
        }

        public class Cone : Surface
        {
            private Vector axisVector;

            internal Cone(Position axisPoint, Vector axisVector, double radius, double halfAngle)
            {
                this.AxisVector = axisVector;
                this.AxisPoint = axisPoint;
                this.Radius = radius;
                this.HalfAngle = halfAngle;
            }

            public Position AxisPoint { get; set; }

            public Vector AxisVector
            {
                get
                {
                    return this.axisVector;
                }
                set
                {
                    this.axisVector = Vector.Unit(value);
                }
            }

            public double HalfAngle { get; set; }

            public double Radius { get; set; }
        }

        public class Cylinder : Surface
        {
            private Vector axisVector;

            internal Cylinder(Position axisPoint, Vector axisVector, double diameter)
            {
                this.AxisVector = axisVector;
                this.AxisPoint = axisPoint;
                this.Diameter = diameter;
            }

            public Position AxisPoint { get; set; }

            public Vector AxisVector
            {
                get
                {
                    return this.axisVector;
                }
                set
                {
                    this.axisVector = Vector.Unit(value);
                }
            }

            public double Diameter { get; set; }
        }

        public class Extrude : Surface
        {
            private Vector direction;

            internal Extrude(Vector direction)
            {
                this.Direction = direction;
            }

            public Vector Direction
            {
                get
                {
                    return this.direction;
                }
                set
                {
                    this.direction = Vector.Unit(value);
                }
            }
        }

        public class Offset : Surface
        {
            internal Offset(double distance)
            {
                this.Distance = distance;
            }

            public double Distance { get; set; }
        }

        public class Plane : Surface
        {
            private Vector normal;

            public Plane(Position point, Vector normal)
            {
                this.Normal = normal;
                this.D = (double) ((point - Position.Origin) * this.Normal);
            }

            public Plane(Vector normal, double distance)
            {
                this.Normal = normal;
                this.D = distance;
            }

            public Plane(Position p0, Position p1, Position p2)
            {
                this.Normal = Vector.UnitCross((Vector) (p1 - p0), (Vector) (p2 - p0));
                this.D = (double) ((p0 - Position.Origin) * this.Normal);
            }

            public Plane(double a, double b, double c, double d)
            {
                Vector u = new Vector(a, b, c);
                double num = Vector.Norm(u);
                this.Normal = (Vector) (u / num);
                this.D = d / num;
            }

            public double D { get; set; }

            public Vector Normal
            {
                get
                {
                    return this.normal;
                }
                set
                {
                    this.normal = Vector.Unit(value);
                }
            }

            public Position Origin
            {
                get
                {
                    return (Position.Origin + ((Position) (this.D * this.Normal)));
                }
            }
        }

        public class Revolve : Surface
        {
            private Vector axisVector;

            internal Revolve(Vector axisVector, Position axisPoint)
            {
                this.AxisVector = axisVector;
                this.AxisPoint = axisPoint;
            }

            public Position AxisPoint { get; set; }

            public Vector AxisVector
            {
                get
                {
                    return this.axisVector;
                }
                set
                {
                    this.axisVector = Vector.Unit(value);
                }
            }
        }

        public class Sphere : Surface
        {
            internal Sphere(Position center, double diameter)
            {
                this.Center = center;
                this.Diameter = diameter;
            }

            public Position Center { get; set; }

            public double Diameter { get; set; }
        }

        public class Torus : Surface
        {
            private Vector axisVector;

            internal Torus(Vector axisVector, Position axisPoint, double majorRadius, double minorRadius)
            {
                this.AxisVector = axisVector;
                this.AxisPoint = axisPoint;
                this.MajorRadius = majorRadius;
                this.MinorRadius = minorRadius;
            }

            public Position AxisPoint { get; set; }

            public Vector AxisVector
            {
                get
                {
                    return this.axisVector;
                }
                set
                {
                    this.axisVector = Vector.Unit(value);
                }
            }

            public double MajorRadius { get; set; }

            public double MinorRadius { get; set; }
        }
    }
}

