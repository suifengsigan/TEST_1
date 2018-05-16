namespace Snap.Geom
{
    using Snap;
    using System;
    using System.Runtime.CompilerServices;

    public class Curve
    {
        internal Curve()
        {
        }

        public class Arc : Curve
        {
            private Vector axisX;
            private Vector axisY;

            internal Arc(Position center, Vector axisX, Vector axisY, double radius, double startAngle, double endAngle)
            {
                this.Center = center;
                this.AxisX = axisX;
                this.AxisY = axisY;
                this.Radius = radius;
                this.StartAngle = startAngle;
                this.EndAngle = endAngle;
            }

            public static Curve.Arc Fillet(Position p0, Position pa, Position p1, double radius)
            {
                Vector u = Vector.Unit((Vector) (p0 - pa));
                Vector v = Vector.Unit((Vector) (p1 - pa));
                Vector vector3 = Vector.Unit(u + v);
                double angle = Vector.Angle(u, v) / 2.0;
                double num3 = radius / Snap.Math.SinD(angle);
                Position center = pa + ((Position) (num3 * vector3));
                Vector axisX = -vector3;
                Vector axisY = Vector.Unit(v - u);
                double endAngle = 90.0 - angle;
                double startAngle = -endAngle;
                return new Curve.Arc(center, axisX, axisY, radius, startAngle, endAngle);
            }

            public Vector AxisX
            {
                get
                {
                    return this.axisX;
                }
                set
                {
                    this.axisX = Vector.Unit(value);
                }
            }

            public Vector AxisY
            {
                get
                {
                    return this.axisY;
                }
                set
                {
                    this.axisY = Vector.Unit(value);
                }
            }

            public Position Center { get; set; }

            public double EndAngle { get; set; }

            public double Radius { get; set; }

            public double StartAngle { get; set; }
        }

        public class Ellipse : Curve
        {
            internal Ellipse(Position center, Vector axisX, Vector axisY, double majorRadius, double minorRadius, double startAngle, double endAngle)
            {
                this.Center = center;
                this.RadiusX = majorRadius;
                this.RadiusY = minorRadius;
                this.AxisX = axisX;
                this.AxisY = axisY;
                this.StartAngle = startAngle;
                this.EndAngle = endAngle;
            }

            public Vector AxisX { get; set; }

            public Vector AxisY { get; set; }

            public Position Center { get; set; }

            public double EndAngle { get; set; }

            public double RadiusX { get; set; }

            public double RadiusY { get; set; }

            public double StartAngle { get; set; }
        }

        public class Line : Curve
        {
            internal Line(Position startPoint, Position endPoint)
            {
                this.StartPoint = startPoint;
                this.EndPoint = endPoint;
            }

            public Position EndPoint { get; set; }

            public Position StartPoint { get; set; }
        }

        public class Ray : Curve
        {
            private Vector axis;

            public Ray(Position origin, Vector axis)
            {
                this.Origin = origin;
                this.Axis = Vector.Unit(axis);
            }

            public Vector Axis
            {
                get
                {
                    return this.axis;
                }
                set
                {
                    this.axis = Vector.Unit(value);
                }
            }

            public Position Origin { get; set; }
        }

        public class Spline : Curve
        {
            internal Spline(double[] knots, Position[] poles, double[] weights)
            {
                this.Poles = poles;
                this.Weights = weights;
                this.Knots = knots;
            }

            public int Degree
            {
                get
                {
                    return (this.Order - 1);
                }
            }

            public double[] Knots { get; set; }

            public int Order
            {
                get
                {
                    int length = this.Poles.Length;
                    int num2 = this.Knots.Length;
                    return (num2 - length);
                }
            }

            public Position[] Poles { get; set; }

            public double[] Weights { get; set; }
        }
    }
}

