namespace Snap.NX
{
    using NXOpen;
    using NXOpen.Features;
    using NXOpen.UF;
    using NXOpen.Utilities;
    using Snap;
    using Snap.Geom;
    using System;
    using System.Linq;
    using System.Runtime.InteropServices;

    public class Face : Snap.NX.NXObject
    {
        protected Face(NXOpen.Face nxopenFace) : base(nxopenFace)
        {
            base.NXOpenTaggedObject = nxopenFace;
        }

        internal static Snap.NX.Face CreateFace(NXOpen.Face nxopenFace)
        {
            Snap.NX.Face snapFace = new Snap.NX.Face(nxopenFace);
            switch (GetFaceType(snapFace))
            {
                case ObjectTypes.SubType.FaceBlend:
                    return new Blend(nxopenFace);

                case ObjectTypes.SubType.FaceBsurface:
                    return new Bsurface(nxopenFace);

                case ObjectTypes.SubType.FaceCone:
                    return new Cone(nxopenFace);

                case ObjectTypes.SubType.FaceCylinder:
                    return new Cylinder(nxopenFace);

                case ObjectTypes.SubType.FaceExtruded:
                    return new Extruded(nxopenFace);

                case ObjectTypes.SubType.FaceOffset:
                    return new Offset(nxopenFace);

                case ObjectTypes.SubType.FacePlane:
                    return new Plane(nxopenFace);

                case ObjectTypes.SubType.FaceRevolved:
                    return new Revolved(nxopenFace);

                case ObjectTypes.SubType.FaceSphere:
                    return new Sphere(nxopenFace);

                case ObjectTypes.SubType.FaceTorus:
                    return new Torus(nxopenFace);
            }
            return snapFace;
        }

        public Vector DerivDu(params double[] uv)
        {
            if (uv.Length != 2)
            {
                throw new ArgumentException("The uv array must have length = 2");
            }
            int mode = 1;
            return SurfaceEvaluate(this, mode, uv[0], uv[1])[1];
        }

        public Vector DerivDv(params double[] uv)
        {
            if (uv.Length != 2)
            {
                throw new ArgumentException("The uv array must have length = 2");
            }
            int mode = 1;
            return SurfaceEvaluate(this, mode, uv[0], uv[1])[2];
        }

        private static UFModl.Bsurface GetBsurfaceData(Snap.NX.Face face)
        {
            UFModl.Bsurface bsurf = new UFModl.Bsurface();
            Globals.UFSession.Modl.AskBsurf(face.NXOpenTag, out bsurf);
            return bsurf;
        }

        private static double[] GetBsurfaceKnotsU(Snap.NX.Face face)
        {
            return GetBsurfaceData(face).knots_u;
        }

        private static double[] GetBsurfaceKnotsV(Snap.NX.Face face)
        {
            return GetBsurfaceData(face).knots_v;
        }

        private static Snap.Position[,] GetBsurfacePoles(Snap.NX.Face face)
        {
            UFModl.Bsurface bsurfaceData = GetBsurfaceData(face);
            int num = bsurfaceData.num_poles_u;
            int num2 = bsurfaceData.num_poles_v;
            Snap.Position[,] positionArray = new Snap.Position[num, num2];
            for (int i = 0; i < num2; i++)
            {
                for (int j = 0; j < num; j++)
                {
                    int num5 = j + (i * num);
                    positionArray[j, i].X = bsurfaceData.poles[num5, 0];
                    positionArray[j, i].Y = bsurfaceData.poles[num5, 1];
                    positionArray[j, i].Z = bsurfaceData.poles[num5, 2];
                }
            }
            return positionArray;
        }

        private static double[,] GetBsurfaceWeights(Snap.NX.Face face)
        {
            UFModl.Bsurface bsurfaceData = GetBsurfaceData(face);
            int num = bsurfaceData.num_poles_u;
            int num2 = bsurfaceData.num_poles_v;
            double[,] numArray = new double[num, num2];
            for (int i = 0; i < num2; i++)
            {
                for (int j = 0; j < num; j++)
                {
                    int num5 = j + (i * num);
                    numArray[j, i] = bsurfaceData.poles[num5, 3];
                }
            }
            return numArray;
        }

        private static ObjectTypes.SubType GetFaceType(Snap.NX.Face snapFace)
        {
            ObjectTypes.SubType faceUnknown = ObjectTypes.SubType.FaceUnknown;
            NXOpen.Face.FaceType undefined = NXOpen.Face.FaceType.Undefined;
            try
            {
                undefined = snapFace.NXOpenFace.SolidFaceType;
            }
            catch
            {
            }
            switch (undefined)
            {
                case NXOpen.Face.FaceType.Planar:
                    faceUnknown = ObjectTypes.SubType.FacePlane;
                    break;

                case NXOpen.Face.FaceType.Cylindrical:
                    faceUnknown = ObjectTypes.SubType.FaceCylinder;
                    break;

                case NXOpen.Face.FaceType.Conical:
                    faceUnknown = ObjectTypes.SubType.FaceCone;
                    break;

                case NXOpen.Face.FaceType.Spherical:
                    faceUnknown = ObjectTypes.SubType.FaceSphere;
                    break;

                case NXOpen.Face.FaceType.Parametric:
                    faceUnknown = ObjectTypes.SubType.FaceBsurface;
                    break;

                case NXOpen.Face.FaceType.Blending:
                    faceUnknown = ObjectTypes.SubType.FaceBlend;
                    break;

                case NXOpen.Face.FaceType.Offset:
                    faceUnknown = ObjectTypes.SubType.FaceOffset;
                    break;

                case NXOpen.Face.FaceType.Swept:
                    faceUnknown = ObjectTypes.SubType.FaceExtruded;
                    break;
            }
            if (undefined != NXOpen.Face.FaceType.SurfaceOfRevolution)
            {
                return faceUnknown;
            }
            double num = GetSurfaceRadii(snapFace)[1];
            if (num != 0.0)
            {
                return ObjectTypes.SubType.FaceTorus;
            }
            return ObjectTypes.SubType.FaceRevolved;
        }

        private static Snap.Position GetSurfaceAxisPoint(Snap.NX.Face face)
        {
            int num;
            Snap.Position position;
            Vector vector;
            double num2;
            double num3;
            int num4;
            GetSurfaceData(face, out num, out position, out vector, out num2, out num3, out num4);
            return position;
        }

        private static Vector GetSurfaceAxisVector(Snap.NX.Face face)
        {
            int num;
            Snap.Position position;
            Vector vector;
            double num2;
            double num3;
            int num4;
            GetSurfaceData(face, out num, out position, out vector, out num2, out num3, out num4);
            return vector;
        }

        private static void GetSurfaceData(Snap.NX.Face face, out int surfaceType, out Snap.Position axisPoint, out Vector axisVector, out double radius1, out double radius2, out int normalFlip)
        {
            UFSession uFSession = Globals.UFSession;
            double[] point = new double[3];
            double[] dir = new double[3];
            double[] box = new double[6];
            uFSession.Modl.AskFaceData(face.NXOpenTag, out surfaceType, point, dir, box, out radius1, out radius2, out normalFlip);
            axisPoint = new Snap.Position(point);
            axisVector = new Vector(dir);
        }

        private static double[] GetSurfaceRadii(Snap.NX.Face face)
        {
            int num;
            Snap.Position position;
            Vector vector;
            int num2;
            double[] numArray = new double[2];
            GetSurfaceData(face, out num, out position, out vector, out numArray[0], out numArray[1], out num2);
            return numArray;
        }

        public Vector Normal(params double[] uv)
        {
            if (uv.Length != 2)
            {
                throw new ArgumentException("The uv array must have length = 2");
            }
            int mode = 10;
            return SurfaceEvaluate(this, mode, uv[0], uv[1])[3];
        }

        public static implicit operator Snap.NX.Face(NXOpen.Face face)
        {
            return CreateFace(face);
        }

        public static implicit operator NXOpen.Face(Snap.NX.Face face)
        {
            return (NXOpen.Face) face.NXOpenTaggedObject;
        }

        public double[] Parameters(Snap.Position point)
        {
            IntPtr ptr;
            UFEvalsf evalsf = Globals.UFSession.Evalsf;
            evalsf.Initialize2(base.NXOpenTag, out ptr);
            double[] array = point.Array;
            UFEvalsf.Pos3 pos = new UFEvalsf.Pos3();
            evalsf.FindClosestPoint(ptr, array, out pos);
            double[] uv = pos.uv;
            uv[0] = this.FactorU * uv[0];
            uv[1] = this.FactorV * uv[1];
            return uv;
        }

        public Snap.Position Position(params double[] uv)
        {
            if (uv.Length != 2)
            {
                throw new ArgumentException("The uv array must have length = 2");
            }
            int mode = 0;
            Vector[] vectorArray = SurfaceEvaluate(this, mode, uv[0], uv[1]);
            return new Snap.Position(vectorArray[0].X, vectorArray[0].Y, vectorArray[0].Z);
        }

        public Snap.Position[,] PositionArray(double[] paramsU, double[] paramsV)
        {
            int length = paramsU.Length;
            int num2 = paramsV.Length;
            Snap.Position[,] positionArray = new Snap.Position[length, num2];
            for (int i = 0; i < length; i++)
            {
                double num3 = paramsU[i];
                for (int j = 0; j < num2; j++)
                {
                    double num4 = paramsV[j];
                    positionArray[i, j] = this.Position(new double[] { num3, num4 });
                }
            }
            return positionArray;
        }

        public Snap.Position[,] PositionArray(Box2d boxUV, int numU, int numV)
        {
            double minU = boxUV.MinU;
            double maxU = boxUV.MaxU;
            double minV = boxUV.MinV;
            double maxV = boxUV.MaxV;
            double num5 = (maxU - minU) / ((double) (numU - 1));
            double num6 = (maxV - minV) / ((double) (numV - 1));
            double[] paramsU = new double[numU];
            double[] paramsV = new double[numV];
            for (int i = 0; i < (numU - 1); i++)
            {
                paramsU[i] = minU + (i * num5);
            }
            for (int j = 0; j < (numV - 1); j++)
            {
                paramsV[j] = minV + (j * num6);
            }
            paramsU[numU - 1] = maxU;
            paramsV[numV - 1] = maxV;
            return this.PositionArray(paramsU, paramsV);
        }

        private static Vector[] SurfaceEvaluate(Snap.NX.Face face, int mode, double u, double v)
        {
            IntPtr ptr;
            double[] numArray = new double[] { u / face.FactorU, v / face.FactorV };
            UFEvalsf evalsf = Globals.UFSession.Evalsf;
            Tag nXOpenTag = face.NXOpenTag;
            evalsf.Initialize2(nXOpenTag, out ptr);
            ModlSrfValue value2 = new ModlSrfValue();
            evalsf.Evaluate(ptr, mode, numArray, out value2);
            Vector vector = new Vector(value2.srf_pos);
            Vector vector2 = new Vector(value2.srf_du);
            Vector vector3 = new Vector(value2.srf_dv);
            Vector vector4 = new Vector(value2.srf_unormal);
            vector2 = (Vector) ((1.0 / face.FactorU) * vector2);
            vector3 = (Vector) ((1.0 / face.FactorV) * vector3);
            return new Vector[] { vector, vector2, vector3, vector4 };
        }

        public static Snap.NX.Face Wrap(Tag nxopenFaceTag)
        {
            if (nxopenFaceTag == Tag.Null)
            {
                return null;
            }
            if (Snap.NX.NXObject.GetTypeFromTag(nxopenFaceTag) != ObjectTypes.Type.Face)
            {
                throw new ArgumentException("Input tag doesn't belong to an NXOpen.Face object");
            }
            NXOpen.Face nxopenFace = (NXOpen.Face) NXObjectManager.Get(nxopenFaceTag);
            return CreateFace(nxopenFace);
        }

        public double Area
        {
            get
            {
                return Compute.Area(new Snap.NX.Face[] { this });
            }
        }

        public Snap.NX.Body Body
        {
            get
            {
                return this.NXOpenFace.GetBody();
            }
        }

        public override Box3d Box
        {
            get
            {
                double[] numArray = new double[3];
                double[,] directions = new double[3, 3];
                double[] distances = new double[3];
                Tag tag = this.NXOpenFace.Tag;
                Globals.UFSession.Modl.AskBoundingBoxExact(tag, Tag.Null, numArray, directions, distances);
                Snap.Position minXYZ = new Snap.Position(numArray);
                Vector vector = new Vector(directions[0, 0], directions[0, 1], directions[0, 2]);
                Vector vector2 = new Vector(directions[1, 0], directions[1, 1], directions[1, 2]);
                Vector vector3 = new Vector(directions[2, 0], directions[2, 1], directions[2, 2]);
                Vector vector4 = (Vector) (((numArray + (distances[0] * vector)) + (distances[1] * vector2)) + (distances[2] * vector3));
                return new Box3d(minXYZ, new Snap.Position(vector4.Array));
            }
        }

        public Box2d BoxUV
        {
            get
            {
                Box2d boxd;
                Tag tag = this.NXOpenFace.Tag;
                double[] numArray = new double[4];
                Globals.UFSession.Modl.AskFaceUvMinmax(tag, numArray);
                boxd = new Box2d(numArray[0], numArray[2], numArray[1], numArray[3]);
                return new Box2d(numArray[0], numArray[2], numArray[1], numArray[3]) { MinU = this.FactorU * boxd.MinU, MaxU = this.FactorU * boxd.MaxU, MinV = this.FactorV * boxd.MinV, MaxV = this.FactorV * boxd.MaxV };
            }
        }

        public Snap.NX.Curve[] EdgeCurves
        {
            get
            {
                Tag[] tagArray;
                Globals.UFSession.Modl.AskFaceEdges(base.NXOpenTag, out tagArray);
                Snap.NX.Curve[] curveArray = new Snap.NX.Curve[tagArray.Length];
                for (int i = 0; i < tagArray.Length; i++)
                {
                    Tag tag;
                    Globals.UFSession.Modl.CreateCurveFromEdge(tagArray[i], out tag);
                    curveArray[i] = new Snap.NX.Curve(tag);
                }
                return curveArray;
            }
        }

        public Snap.NX.Edge[] Edges
        {
            get
            {
                NXOpen.Edge[] edges = this.NXOpenFace.GetEdges();
                Snap.NX.Edge[] edgeArray2 = new Snap.NX.Edge[edges.Length];
                for (int i = 0; i < edges.Length; i++)
                {
                    edgeArray2[i] = Snap.NX.Edge.CreateEdge(edges[i]);
                }
                return edgeArray2;
            }
        }

        internal virtual double FactorU
        {
            get
            {
                return 1.0;
            }
        }

        internal virtual double FactorV
        {
            get
            {
                return 1.0;
            }
        }

        public NXOpen.Face NXOpenFace
        {
            get
            {
                return (NXOpen.Face) base.NXOpenTaggedObject;
            }
        }

        public override ObjectTypes.SubType ObjectSubType
        {
            get
            {
                return GetFaceType(this);
            }
        }

        public override ObjectTypes.Type ObjectType
        {
            get
            {
                return ObjectTypes.Type.Face;
            }
        }

        public Snap.NX.Face Prototype
        {
            get
            {
                Tag protoTagFromOccTag = base.GetProtoTagFromOccTag(base.NXOpenTag);
                Snap.NX.Face face = null;
                if (protoTagFromOccTag != Tag.Null)
                {
                    face = Wrap(protoTagFromOccTag);
                }
                return face;
            }
        }

        public class Blend : Snap.NX.Face
        {
            internal Blend(NXOpen.Face nxopenFace) : base(nxopenFace)
            {
                if (nxopenFace.SolidFaceType != NXOpen.Face.FaceType.Blending)
                {
                    throw new ArgumentException("Input object must be of type NXOpen.Face.FaceType.Blending");
                }
                base.NXOpenTaggedObject = nxopenFace;
            }

            public Snap.NX.Arc[] IsoCurveU(double constantU)
            {
                Snap.NX.Curve[] curveArray = Create.IsoparametricCurve(this, Create.DirectionUV.U, constantU, 0.0254);
                double radius = Snap.NX.Face.GetSurfaceRadii(this)[0];

                Snap.NX.Spline[] nxObjects = Enumerable.Select<Snap.NX.Curve, Snap.NX.Spline>(curveArray, (u)=> {
                    return Snap.NX.Spline.Wrap(u.NXOpenTag);
                }).ToArray();
                Snap.NX.Arc[] arcArray = new Snap.NX.Arc[nxObjects.Length];
                for (int i = 0; i < nxObjects.Length; i++)
                {
                    arcArray[i] = SplineToArc(nxObjects[i], radius);
                }
                Snap.NX.NXObject.Delete(nxObjects);
                return arcArray;
            }

            private static Snap.NX.Arc SplineToArc(Snap.NX.Spline spline, double radius)
            {
                Vector vector = spline.Tangent(0.0);
                Snap.Position startPoint = spline.Position(0.0);
                Vector vector2 = spline.Tangent(1.0);
                Snap.Position endPoint = spline.Position(1.0);
                double num = (double) (-vector * vector2);
                double num2 = System.Math.Sqrt((1.0 - num) / 2.0);
                Vector vector3 = Vector.Unit(vector - vector2);
                Snap.Position p = (Snap.Position) (((startPoint + endPoint) / 2.0) + ((radius * (1.0 - num2)) * vector3));
                Snap.Position position4 = (Snap.Position)(p - ((Snap.Position) ((2.0 * radius) * vector3)));
                Snap.Position q = spline.Position(0.5);
                Snap.Position throughPoint = p;
                double num3 = Snap.Position.Distance(p, q);
                if (Snap.Position.Distance(position4, q) < num3)
                {
                    throughPoint = position4;
                }
                return Create.Arc(startPoint, throughPoint, endPoint);
            }

            public Surface.Blend Geometry
            {
                get
                {
                    return new Surface.Blend(Snap.NX.Face.GetSurfaceRadii(this)[0]);
                }
            }
        }

        public class Bsurface : Snap.NX.Face
        {
            internal Bsurface(NXOpen.Face nxopenFace) : base(nxopenFace)
            {
                if (nxopenFace.SolidFaceType != NXOpen.Face.FaceType.Parametric)
                {
                    throw new ArgumentException("Input object must be of type NXOpen.Face.FaceType.Parametric");
                }
                base.NXOpenTaggedObject = nxopenFace;
            }

            public Snap.NX.Spline[] IsoCurveU(double constantU)
            {
                return Enumerable.Select<Snap.NX.Curve, Snap.NX.Spline>(Create.IsoparametricCurve(this, Create.DirectionUV.U, constantU, 0.0254),
                    u=>Snap.NX.Spline.Wrap(u.NXOpenTag)
                    ).ToArray<Snap.NX.Spline>();
            }

            public Snap.NX.Spline[] IsoCurveV(double constantV)
            {
                return Enumerable.Select<Snap.NX.Curve, Snap.NX.Spline>(Create.IsoparametricCurve(this, Create.DirectionUV.V, constantV, 0.0254), u => Snap.NX.Spline.Wrap(u.NXOpenTag)).ToArray<Snap.NX.Spline>();
            }

            public Surface.Bsurface Geometry
            {
                get
                {
                    Tag tag;
                    Globals.UFSession.Modl.AskFaceBody(base.NXOpenTag, out tag);
                    NXObjectManager.Get(tag);
                    Snap.Position[,] bsurfacePoles = Snap.NX.Face.GetBsurfacePoles(this);
                    double[,] bsurfaceWeights = Snap.NX.Face.GetBsurfaceWeights(this);
                    double[] bsurfaceKnotsU = Snap.NX.Face.GetBsurfaceKnotsU(this);
                    return new Surface.Bsurface(bsurfacePoles, bsurfaceWeights, bsurfaceKnotsU, Snap.NX.Face.GetBsurfaceKnotsV(this));
                }
            }
        }

        public class Cone : Snap.NX.Face
        {
            internal Cone(NXOpen.Face nxopenFace) : base(nxopenFace)
            {
                if (nxopenFace.SolidFaceType != NXOpen.Face.FaceType.Conical)
                {
                    throw new ArgumentException("Input object must be of type NXOpen.Face.FaceType.Conical");
                }
                base.NXOpenTaggedObject = nxopenFace;
            }

            public Snap.NX.Line[] IsoCurveU(double constantU)
            {
                return Enumerable.Select<Snap.NX.Curve, Snap.NX.Line>(Create.IsoparametricCurve(this, Create.DirectionUV.U, constantU, 0.0254),
                      u => Snap.NX.Line.Wrap(u.NXOpenTag)
                    ).ToArray<Snap.NX.Line>();
            }

            public Snap.NX.Arc[] IsoCurveV(double constantV)
            {
                return Enumerable.Select<Snap.NX.Curve, Snap.NX.Arc>(Create.IsoparametricCurve(this, Create.DirectionUV.V, constantV, 0.0254),
                    u => Snap.NX.Arc.Wrap(u.NXOpenTag)
                    ).ToArray<Snap.NX.Arc>();
            }

            internal override double FactorU
            {
                get
                {
                    return 57.295779513082323;
                }
            }

            internal override double FactorV
            {
                get
                {
                    double num = 39.3700787401575;
                    if (Globals.UnitType == Globals.Unit.Millimeter)
                    {
                        num = 1000.0;
                    }
                    return num;
                }
            }

            public Surface.Cone Geometry
            {
                get
                {
                    Snap.Position surfaceAxisPoint = Snap.NX.Face.GetSurfaceAxisPoint(this);
                    Vector surfaceAxisVector = Snap.NX.Face.GetSurfaceAxisVector(this);
                    double radius = Snap.NX.Face.GetSurfaceRadii(this)[0];
                    return new Surface.Cone(surfaceAxisPoint, surfaceAxisVector, radius, Snap.NX.Face.GetSurfaceRadii(this)[1] * this.FactorU);
                }
            }
        }

        public class Cylinder : Snap.NX.Face
        {
            internal Cylinder(NXOpen.Face nxopenFace) : base(nxopenFace)
            {
                if (nxopenFace.SolidFaceType != NXOpen.Face.FaceType.Cylindrical)
                {
                    throw new ArgumentException("Input object must be of type NXOpen.Face.FaceType.Cylindrical");
                }
                base.NXOpenTaggedObject = nxopenFace;
            }

            public Snap.NX.Line[] IsoCurveU(double constantU)
            {
                return Enumerable.Select<Snap.NX.Curve, Snap.NX.Line>(Create.IsoparametricCurve(this, Create.DirectionUV.U, constantU, 0.0254),
                    u=>Snap.NX.Line.Wrap(u.NXOpenTag)
                    ).ToArray<Snap.NX.Line>();
            }

            public Snap.NX.Arc[] IsoCurveV(double constantV)
            {
                return Enumerable.Select<Snap.NX.Curve, Snap.NX.Arc>(Create.IsoparametricCurve(this, Create.DirectionUV.V, constantV, 0.0254),
                       u => Snap.NX.Arc.Wrap(u.NXOpenTag)).ToArray<Snap.NX.Arc>();
            }

            internal override double FactorU
            {
                get
                {
                    return 57.295779513082323;
                }
            }

            internal override double FactorV
            {
                get
                {
                    double num = 39.3700787401575;
                    if (Globals.UnitType == Globals.Unit.Millimeter)
                    {
                        num = 1000.0;
                    }
                    return num;
                }
            }

            public Surface.Cylinder Geometry
            {
                get
                {
                    Snap.Position surfaceAxisPoint = Snap.NX.Face.GetSurfaceAxisPoint(this);
                    Vector surfaceAxisVector = Snap.NX.Face.GetSurfaceAxisVector(this);
                    return new Surface.Cylinder(surfaceAxisPoint, surfaceAxisVector, 2.0 * Snap.NX.Face.GetSurfaceRadii(this)[0]);
                }
            }
        }

        public class Extruded : Snap.NX.Face
        {
            internal Extruded(NXOpen.Face nxopenFace) : base(nxopenFace)
            {
                if (nxopenFace.SolidFaceType != NXOpen.Face.FaceType.Swept)
                {
                    throw new ArgumentException("Input object must be of type NXOpen.Face.FaceType.Swept");
                }
                base.NXOpenTaggedObject = nxopenFace;
            }

            public Snap.NX.Line[] IsoCurveU(double constantU)
            {
                Snap.NX.Curve[] nxObjects = Create.IsoparametricCurve(this, Create.DirectionUV.U, constantU, 0.0254);
                Snap.NX.Line[] lineArray = new Snap.NX.Line[nxObjects.Length];
                for (int i = 0; i < nxObjects.Length; i++)
                {
                    lineArray[i] = Create.Line(nxObjects[i].StartPoint, nxObjects[i].EndPoint);
                }
                Snap.NX.NXObject.Delete(nxObjects);
                return lineArray;
            }

            public Snap.NX.Spline[] IsoCurveV(double constantV)
            {
                return Enumerable.Select<Snap.NX.Curve, Snap.NX.Spline>(Create.IsoparametricCurve(this, Create.DirectionUV.V, constantV, 0.0254),
                       u => Snap.NX.Spline.Wrap(u.NXOpenTag)).ToArray<Snap.NX.Spline>();
            }

            internal override double FactorV
            {
                get
                {
                    double num = 39.3700787401575;
                    if (Globals.UnitType == Globals.Unit.Millimeter)
                    {
                        num = 1000.0;
                    }
                    return num;
                }
            }

            public Surface.Extrude Geometry
            {
                get
                {
                    Tag[] tagArray;
                    Globals.UFSession.Modl.AskFaceFeats(base.NXOpenTag, out tagArray);
                    Snap.NX.Extrude extrude = (NXOpen.Features.Extrude) NXObjectManager.Get(tagArray[0]);
                    return new Surface.Extrude(extrude.Direction);
                }
            }
        }

        public class Offset : Snap.NX.Face
        {
            internal Offset(NXOpen.Face nxopenFace) : base(nxopenFace)
            {
                if (nxopenFace.SolidFaceType != NXOpen.Face.FaceType.Offset)
                {
                    throw new ArgumentException("Input object must be of type NXOpen.Face.FaceType.Offset");
                }
                base.NXOpenTaggedObject = nxopenFace;
            }

            public Snap.NX.Spline[] IsoCurveU(double constantU, double tolerance = 0.0254)
            {
                return Enumerable.Select<Snap.NX.Curve, Snap.NX.Spline>(Create.IsoparametricCurve(this, Create.DirectionUV.U, constantU, 0.0254),
                       u => Snap.NX.Spline.Wrap(u.NXOpenTag)).ToArray<Snap.NX.Spline>();
            }

            public Snap.NX.Spline[] IsoCurveV(double constantV, double tolerance = 0.0254)
            {
                return Enumerable.Select<Snap.NX.Curve, Snap.NX.Spline>(Create.IsoparametricCurve(this, Create.DirectionUV.V, constantV, 0.0254),
                       u => Snap.NX.Spline.Wrap(u.NXOpenTag)).ToArray<Snap.NX.Spline>();
            }

            public Surface.Offset Geometry
            {
                get
                {
                    Tag[] tagArray;
                    Globals.UFSession.Modl.AskFaceFeats(base.NXOpenTag, out tagArray);
                    Snap.NX.OffsetFace face = (NXOpen.Features.OffsetFace) NXObjectManager.Get(tagArray[0]);
                    return new Surface.Offset(face.Distance);
                }
            }
        }

        public class Plane : Snap.NX.Face
        {
            internal Plane(NXOpen.Face nxopenFace) : base(nxopenFace)
            {
                if (nxopenFace.SolidFaceType != NXOpen.Face.FaceType.Planar)
                {
                    throw new ArgumentException("Input object must be of type NXOpen.Face.FaceType.Planar");
                }
                base.NXOpenTaggedObject = nxopenFace;
            }

            public Snap.NX.Line[] IsoCurveU(double constantU)
            {
                return Enumerable.Select<Snap.NX.Curve, Snap.NX.Line>(Create.IsoparametricCurve(this, Create.DirectionUV.U, constantU, 0.0254),
                       u => Snap.NX.Line.Wrap(u.NXOpenTag)).ToArray<Snap.NX.Line>();
            }

            public Snap.NX.Line[] IsoCurveV(double constantV)
            {
                return Enumerable.Select<Snap.NX.Curve, Snap.NX.Line>(Create.IsoparametricCurve(this, Create.DirectionUV.V, constantV, 0.0254),
                       u => Snap.NX.Line.Wrap(u.NXOpenTag)).ToArray<Snap.NX.Line>();
            }

            public Surface.Plane Geometry
            {
                get
                {
                    Snap.Position surfaceAxisPoint = Snap.NX.Face.GetSurfaceAxisPoint(this);
                    return new Surface.Plane(surfaceAxisPoint, Snap.NX.Face.GetSurfaceAxisVector(this));
                }
            }
        }

        public class Revolved : Snap.NX.Face
        {
            internal Revolved(NXOpen.Face nxopenFace) : base(nxopenFace)
            {
                if (nxopenFace.SolidFaceType != NXOpen.Face.FaceType.SurfaceOfRevolution)
                {
                    throw new ArgumentException("Input object must be of type NXOpen.Face.FaceType.SurfaceOfRevolution");
                }
                base.NXOpenTaggedObject = nxopenFace;
            }

            public Snap.NX.Arc[] IsoCurveU(double constantU)
            {
                Snap.NX.Curve[] nxObjects = Create.IsoparametricCurve(this, Create.DirectionUV.U, constantU, 0.0254);
                Snap.NX.Arc[] arcArray = new Snap.NX.Arc[nxObjects.Length];
                for (int i = 0; i < nxObjects.Length; i++)
                {
                    Snap.Position startPoint = nxObjects[i].StartPoint;
                    Snap.Position endPoint = nxObjects[i].EndPoint;
                    Snap.Position position3 = nxObjects[i].Position(((nxObjects[i].MaxU - nxObjects[i].MinU) / 2.0) + nxObjects[i].MinU);
                    if (Snap.Position.Distance(startPoint, endPoint) <= 0.0001)
                    {
                        Snap.Position position4 = nxObjects[i].Position(nxObjects[i].MaxU / 3.0);
                        arcArray[i] = Create.Circle(startPoint, position3, position4);
                    }
                    else
                    {
                        arcArray[i] = Create.Arc(startPoint, position3, endPoint);
                    }
                }
                Snap.NX.NXObject.Delete(nxObjects);
                return arcArray;
            }

            public Snap.NX.Spline[] IsoCurveV(double constantV)
            {
                return Enumerable.Select<Snap.NX.Curve, Snap.NX.Spline>(Create.IsoparametricCurve(this, Create.DirectionUV.V, constantV, 0.0254),
                       u => Snap.NX.Spline.Wrap(u.NXOpenTag)).ToArray<Snap.NX.Spline>();
            }

            internal override double FactorV
            {
                get
                {
                    return 57.295779513082323;
                }
            }

            public Surface.Revolve Geometry
            {
                get
                {
                    Vector surfaceAxisVector = Snap.NX.Face.GetSurfaceAxisVector(this);
                    return new Surface.Revolve(surfaceAxisVector, Snap.NX.Face.GetSurfaceAxisPoint(this));
                }
            }
        }

        public class Sphere : Snap.NX.Face
        {
            internal Sphere(NXOpen.Face nxopenFace) : base(nxopenFace)
            {
                if (nxopenFace.SolidFaceType != NXOpen.Face.FaceType.Spherical)
                {
                    throw new ArgumentException("Input object must be of type NXOpen.Face.FaceType.Spherical");
                }
                base.NXOpenTaggedObject = nxopenFace;
            }

            public Snap.NX.Arc[] IsoCurveU(double constantU)
            {
                return Enumerable.Select<Snap.NX.Curve, Snap.NX.Arc>(Create.IsoparametricCurve(this, Create.DirectionUV.U, constantU, 0.0254),
                       u => Snap.NX.Arc.Wrap(u.NXOpenTag)).ToArray<Snap.NX.Arc>();
            }

            public Snap.NX.Arc[] IsoCurveV(double constantV)
            {
                return Enumerable.Select<Snap.NX.Curve, Snap.NX.Arc>(Create.IsoparametricCurve(this, Create.DirectionUV.V, constantV, 0.0254),
                       u => Snap.NX.Arc.Wrap(u.NXOpenTag)).ToArray<Snap.NX.Arc>();
            }

            internal override double FactorU
            {
                get
                {
                    return 57.295779513082323;
                }
            }

            internal override double FactorV
            {
                get
                {
                    return 57.295779513082323;
                }
            }

            public Surface.Sphere Geometry
            {
                get
                {
                    Snap.Position surfaceAxisPoint = Snap.NX.Face.GetSurfaceAxisPoint(this);
                    return new Surface.Sphere(surfaceAxisPoint, 2.0 * Snap.NX.Face.GetSurfaceRadii(this)[0]);
                }
            }
        }

        public class Torus : Snap.NX.Face
        {
            internal Torus(NXOpen.Face nxopenFace) : base(nxopenFace)
            {
                NXOpen.Face.FaceType solidFaceType = nxopenFace.SolidFaceType;
                base.NXOpenTaggedObject = nxopenFace;
            }

            public Snap.NX.Arc[] IsoCurveU(double constantU)
            {
                return Enumerable.Select<Snap.NX.Curve, Snap.NX.Arc>(Create.IsoparametricCurve(this, Create.DirectionUV.U, constantU, 0.0254),
                    u=>Snap.NX.Arc.Wrap(u.NXOpenTag)).ToArray<Snap.NX.Arc>();
            }

            public Snap.NX.Arc[] IsoCurveV(double constantV)
            {
                return Enumerable.Select<Snap.NX.Curve, Snap.NX.Arc>(Create.IsoparametricCurve(this, Create.DirectionUV.V, constantV, 0.0254),
                    u => Snap.NX.Arc.Wrap(u.NXOpenTag)).ToArray<Snap.NX.Arc>();
            }

            internal override double FactorU
            {
                get
                {
                    return 57.295779513082323;
                }
            }

            internal override double FactorV
            {
                get
                {
                    return 57.295779513082323;
                }
            }

            public Surface.Torus Geometry
            {
                get
                {
                    Vector surfaceAxisVector = Snap.NX.Face.GetSurfaceAxisVector(this);
                    Snap.Position surfaceAxisPoint = Snap.NX.Face.GetSurfaceAxisPoint(this);
                    double majorRadius = Snap.NX.Face.GetSurfaceRadii(this)[0];
                    return new Surface.Torus(surfaceAxisVector, surfaceAxisPoint, majorRadius, Snap.NX.Face.GetSurfaceRadii(this)[1]);
                }
            }
        }
    }
}

