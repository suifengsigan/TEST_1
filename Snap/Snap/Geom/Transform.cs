﻿namespace Snap.Geom
{
    using NXOpen.UF;
    using Snap;
    using System;
    using System.Runtime.CompilerServices;

    public class Transform
    {
        private Transform(double[] array)
        {
            this.Matrix = array;
        }

        public static Transform Composition(Transform xform1, Transform xform2)
        {
            UFSession uFSession = Globals.UFSession;
            double[] product = new double[12];
            uFSession.Trns.MultiplyMatrices(xform1.Matrix, xform2.Matrix, product);
            return new Transform(product);
        }

        public static Transform CreateReflection(Surface.Plane plane)
        {
            return CreateReflection(plane.Normal, plane.D);
        }

        public static Transform CreateReflection(Vector normal, double d)
        {
            double[] array = new double[12];
            double x = normal.X;
            double y = normal.Y;
            double z = normal.Z;
            array[0] = 1.0 - ((2.0 * x) * x);
            array[1] = (-2.0 * x) * y;
            array[2] = (-2.0 * x) * z;
            array[3] = (2.0 * x) * d;
            array[4] = (-2.0 * y) * x;
            array[5] = 1.0 - ((2.0 * y) * y);
            array[6] = (-2.0 * y) * z;
            array[7] = (2.0 * y) * d;
            array[8] = (-2.0 * z) * x;
            array[9] = (-2.0 * z) * y;
            array[10] = 1.0 - ((2.0 * z) * z);
            array[11] = (2.0 * z) * d;
            return new Transform(array);
        }

        /// <summary>
        /// TODO PH ADD
        /// </summary>
        public static Transform CreateRotation(Snap.Orientation orientation1, Snap.Orientation orientation2)
        {
            int num;
            UFSession uFSession = Globals.UFSession;
            double[] matrix = new double[12];
            double[] numArray2 = Snap.Math.MatrixMath.MatrixToVector(orientation1.Array);
            double[] numArray3 = Snap.Math.MatrixMath.MatrixToVector(orientation2.Array);
            uFSession.Trns.CreateCsysMappingMatrix(numArray2, numArray3, matrix, out num);
            return new Transform(matrix);
        }

        public static Transform CreateRotation(Snap.Orientation orientation)
        {
            int num;
            UFSession uFSession = Globals.UFSession;
            double[] matrix = new double[12];
            double[] numArray2 = Snap.Math.MatrixMath.MatrixToVector(Snap.Orientation.Identity.Array);
            double[] numArray3 = Snap.Math.MatrixMath.MatrixToVector(orientation.Array);
            uFSession.Trns.CreateCsysMappingMatrix(numArray2, numArray3, matrix, out num);
            return new Transform(matrix);
        }

        public static Transform CreateRotation(Snap.Position basePoint, double angle)
        {
            return CreateRotation(basePoint, Vector.AxisZ, angle);
        }

        public static Transform CreateRotation(Snap.Position basePoint, Vector axis, double angle)
        {
            int num;
            UFSession uFSession = Globals.UFSession;
            double[] matrix = new double[12];
            uFSession.Trns.CreateRotationMatrix(basePoint.Array, axis.Array, ref angle, matrix, out num);
            return new Transform(matrix);
        }

        public static Transform CreateScale(Snap.Position basePoint, double[] scaleFactors)
        {
            int num;
            UFSession uFSession = Globals.UFSession;
            double[] matrix = new double[12];
            int type = 2;
            uFSession.Trns.CreateScalingMatrix(ref type, scaleFactors, basePoint.Array, matrix, out num);
            return new Transform(matrix);
        }

        public static Transform CreateScale(Snap.Position basePoint, double scaleFactor)
        {
            int num;
            UFSession uFSession = Globals.UFSession;
            double[] matrix = new double[12];
            int type = 1;
            double[] scales = new double[] { scaleFactor, 1.0, 1.0 };
            uFSession.Trns.CreateScalingMatrix(ref type, scales, basePoint.Array, matrix, out num);
            return new Transform(matrix);
        }

        public static Transform CreateTranslation()
        {
            return CreateTranslation(0.0, 0.0, 0.0);
        }

        public static Transform CreateTranslation(Vector v)
        {
            return CreateTranslation(v.X, v.Y, v.Z);
        }

        public static Transform CreateTranslation(double dx, double dy, double dz)
        {
            return new Transform(new double[] { 1.0, 0.0, 0.0, dx, 0.0, 1.0, 0.0, dy, 0.0, 0.0, 1.0, dz });
        }

        public double[] Matrix { get; internal set; }
    }
}

