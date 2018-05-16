namespace Snap.NX
{
    using NXOpen;
    using NXOpen.UF;
    using NXOpen.Utilities;
    using Snap;
    using System;
    using System.Runtime.CompilerServices;

    public class Matrix
    {
        internal Matrix(NXMatrix matrix)
        {
            this.NXOpenMatrix = matrix;
        }

        internal Matrix(Snap.Orientation rotation)
        {
            this.NXOpenMatrix = CreateMatrix(rotation.AxisX, rotation.AxisY, rotation.AxisZ);
        }

        internal Matrix(Vector axisX, Vector axisY, Vector axisZ)
        {
            this.NXOpenMatrix = CreateMatrix(axisX, axisY, axisZ);
        }

        internal static NXMatrix CreateMatrix(Vector axisX, Vector axisY, Vector axisZ)
        {
            Matrix3x3 element = new Matrix3x3 {
                Xx = axisX.X,
                Xy = axisX.Y,
                Xz = axisX.Z,
                Yx = axisY.X,
                Yy = axisY.Y,
                Yz = axisY.Z,
                Zx = axisZ.X,
                Zy = axisZ.Y,
                Zz = axisZ.Z
            };
            return Globals.NXOpenWorkPart.NXMatrices.Create(element);
        }

        internal static NXMatrix CreateMatrix2(Vector axisX, Vector axisY, Vector axisZ)
        {
            Tag tag;
            UFSession uFSession = Globals.UFSession;
            double[] numArray = new double[] { axisX.X, axisX.Y, axisX.Z, axisY.X, axisY.Y, axisY.Z, axisZ.X, axisZ.Y, axisZ.Z };
            uFSession.Csys.CreateMatrix(numArray, out tag);
            return (NXMatrix) NXObjectManager.Get(tag);
        }

        public static implicit operator Matrix(NXMatrix matrix)
        {
            return new Matrix(matrix);
        }

        public static implicit operator NXMatrix(Matrix matrix)
        {
            return matrix.NXOpenMatrix;
        }

        public static Matrix Wrap(Tag nxopenMatrixTag)
        {
            if (nxopenMatrixTag == Tag.Null)
            {
                throw new ArgumentException("Input tag is NXOpen.Tag.Null");
            }
            NXMatrix objectFromTag = Snap.NX.NXObject.GetObjectFromTag(nxopenMatrixTag) as NXMatrix;
            if (objectFromTag == null)
            {
                throw new ArgumentException("Input tag doesn't belong to an NXOpen.NXMatrix object");
            }
            return objectFromTag;
        }

        public Vector AxisX
        {
            get
            {
                return this.Orientation.AxisX;
            }
        }

        public Vector AxisY
        {
            get
            {
                return this.Orientation.AxisY;
            }
        }

        public Vector AxisZ
        {
            get
            {
                return this.Orientation.AxisZ;
            }
        }

        public NXMatrix NXOpenMatrix { get; internal set; }

        public Tag NXOpenTag
        {
            get
            {
                return this.NXOpenMatrix.Tag;
            }
        }

        public Snap.Orientation Orientation
        {
            get
            {
                return this.NXOpenMatrix.Element;
            }
        }
    }
}

