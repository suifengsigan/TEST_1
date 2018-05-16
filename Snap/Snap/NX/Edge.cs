namespace Snap.NX
{
    using NXOpen;
    using NXOpen.Features;
    using NXOpen.GeometricUtilities;
    using NXOpen.UF;
    using Snap;
    using Snap.Geom;
    using System;
    using System.Runtime.InteropServices;

    public class Edge : Snap.NX.NXObject, Snap.NX.ICurve
    {
        protected Edge(NXOpen.Edge nxopenEdge) : base(nxopenEdge)
        {
            base.NXOpenTaggedObject = nxopenEdge;
        }

        public Vector Binormal(double value)
        {
            IntPtr ptr;
            UFEval eval = Globals.UFSession.Eval;
            eval.Initialize2(this.NXOpenTag, out ptr);
            double[] point = new double[3];
            double[] tangent = new double[3];
            double[] normal = new double[3];
            double[] binormal = new double[3];
            value /= this.Factor;
            eval.EvaluateUnitVectors(ptr, value, point, tangent, normal, binormal);
            return new Vector(binormal);
        }

        internal static Snap.NX.Edge CreateEdge(NXOpen.Edge nxopenEdge)
        {
            Snap.NX.Edge snapEdge = new Snap.NX.Edge(nxopenEdge);
            switch (GetEdgeType(snapEdge))
            {
                case ObjectTypes.SubType.EdgeLine:
                    return new Line(nxopenEdge);

                case ObjectTypes.SubType.EdgeArc:
                    return new Arc(nxopenEdge);

                case ObjectTypes.SubType.EdgeSpline:
                    return new Spline(nxopenEdge);

                case ObjectTypes.SubType.EdgeEllipse:
                    return new Ellipse(nxopenEdge);
            }
            return snapEdge;
        }

        public double Curvature(double value)
        {
            Vector[] vectorArray = this.Derivatives(value, 2);
            double num = Vector.Norm(Vector.Cross(vectorArray[1], vectorArray[2]));
            double num2 = Vector.Norm(vectorArray[1]);
            double num3 = (num2 * num2) * num2;
            return (num / num3);
        }

        public Vector Derivative(double value)
        {
            IntPtr ptr;
            UFEval eval = Globals.UFSession.Eval;
            eval.Initialize2(this.NXOpenTag, out ptr);
            double[] point = new double[3];
            double[] derivatives = new double[3];
            value /= this.Factor;
            eval.Evaluate(ptr, 1, value, point, derivatives);
            Vector vector = new Vector(derivatives);
            return (Vector) (vector / this.Factor);
        }

        public Vector[] Derivatives(double value, int order)
        {
            IntPtr ptr;
            UFEval eval = Globals.UFSession.Eval;
            eval.Initialize2(this.NXOpenTag, out ptr);
            double[] point = new double[3];
            double[] derivatives = new double[3 * order];
            value = this.Factor * value;
            eval.Evaluate(ptr, order, value, point, derivatives);
            Vector[] vectorArray = new Vector[order + 1];
            vectorArray[0] = point;
            for (int i = 0; i < order; i++)
            {
                vectorArray[i + 1] = new Vector(derivatives[3 * i], derivatives[(3 * i) + 1], derivatives[(3 * i) + 2]);
                if ((this.NXOpenEdge.SolidEdgeType == NXOpen.Edge.EdgeType.Circular) || (this.NXOpenEdge.SolidEdgeType == NXOpen.Edge.EdgeType.Elliptical))
                {
                    vectorArray[i + 1] = (Vector) (System.Math.Pow(0.017453292519943295, (double) (i + 1)) * vectorArray[i + 1]);
                }
            }
            return vectorArray;
        }

        internal static void GetEdgeData(Snap.NX.Edge edge, out Snap.Position startPoint, out Snap.Position endPoint)
        {
            IntPtr ptr;
            UFEval.Line line;
            UFSession uFSession = Globals.UFSession;
            uFSession.Eval.Initialize2(edge.NXOpenTag, out ptr);
            uFSession.Eval.AskLine(ptr, out line);
            startPoint = new Snap.Position(line.start);
            endPoint = new Snap.Position(line.end);
        }

        internal static void GetEdgeData(Snap.NX.Edge edge, out Snap.Position[] poles, out double[] knots, out double[] weights)
        {
            IntPtr ptr;
            double[] numArray;
            double[] numArray2;
            UFSession uFSession = Globals.UFSession;
            uFSession.Eval.Initialize2(edge.NXOpenTag, out ptr);
            int num = -1;
            int num2 = -1;
            uFSession.Eval.AskSplineControlPts(ptr, out num, out numArray);
            uFSession.Eval.AskSplineKnots(ptr, out num2, out numArray2);
            Snap.Position[] positionArray = new Snap.Position[num];
            double[] numArray3 = new double[num];
            for (int i = 0; i < num; i++)
            {
                positionArray[i] = new Snap.Position(numArray[i * 4], numArray[(i * 4) + 1], numArray[(i * 4) + 2]);
                numArray3[i] = numArray[(i * 4) + 3];
            }
            poles = positionArray;
            weights = numArray3;
            knots = numArray2;
        }

        internal static void GetEdgeData(Snap.NX.Edge edge, out double radius, out double startAngle, out double endAngle, out Snap.Position center, out Vector axisX, out Vector axisY)
        {
            IntPtr ptr;
            UFEval.Arc arc;
            UFSession uFSession = Globals.UFSession;
            uFSession.Eval.Initialize2(edge.NXOpenTag, out ptr);
            uFSession.Eval.AskArc(ptr, out arc);
            startAngle = edge.Factor * arc.limits[0];
            endAngle = edge.Factor * arc.limits[1];
            center = new Snap.Position(arc.center);
            axisX = new Vector(arc.x_axis);
            axisY = new Vector(arc.y_axis);
            radius = arc.radius;
        }

        internal static void GetEdgeData(Snap.NX.Edge edge, out Snap.Position center, out Vector axisX, out Vector axisY, out double majorRadius, out double minorRadius, out double angle1, out double angle2)
        {
            IntPtr ptr;
            UFEval.Ellipse ellipse;
            UFSession uFSession = Globals.UFSession;
            uFSession.Eval.Initialize2(edge.NXOpenTag, out ptr);
            uFSession.Eval.AskEllipse(ptr, out ellipse);
            center = ellipse.center;
            axisX = ellipse.x_axis;
            axisY = ellipse.y_axis;
            majorRadius = ellipse.major;
            minorRadius = ellipse.minor;
            angle1 = ellipse.limits[0] * edge.Factor;
            angle2 = ellipse.limits[1] * edge.Factor;
        }

        private static ObjectTypes.SubType GetEdgeType(Snap.NX.Edge snapEdge)
        {
            ObjectTypes.SubType edgeUnknown = ObjectTypes.SubType.EdgeUnknown;
            NXOpen.Edge.EdgeType undefined = NXOpen.Edge.EdgeType.Undefined;
            try
            {
                undefined = snapEdge.NXOpenEdge.SolidEdgeType;
            }
            catch
            {
            }
            switch (undefined)
            {
                case NXOpen.Edge.EdgeType.Linear:
                    edgeUnknown = ObjectTypes.SubType.EdgeLine;
                    break;

                case NXOpen.Edge.EdgeType.Circular:
                    edgeUnknown = ObjectTypes.SubType.EdgeArc;
                    break;

                case NXOpen.Edge.EdgeType.Elliptical:
                    edgeUnknown = ObjectTypes.SubType.EdgeEllipse;
                    break;

                case NXOpen.Edge.EdgeType.Intersection:
                    edgeUnknown = ObjectTypes.SubType.EdgeIntersection;
                    break;

                case NXOpen.Edge.EdgeType.Spline:
                    edgeUnknown = ObjectTypes.SubType.EdgeSpline;
                    break;

                case NXOpen.Edge.EdgeType.SpCurve:
                    edgeUnknown = ObjectTypes.SubType.EdgeSpCurve;
                    break;

                case NXOpen.Edge.EdgeType.ConstantParameter:
                    edgeUnknown = ObjectTypes.SubType.EdgeIsoCurve;
                    break;
            }
            return edgeUnknown;
        }

        public Vector Normal(double value)
        {
            IntPtr ptr;
            UFEval eval = Globals.UFSession.Eval;
            eval.Initialize2(this.NXOpenTag, out ptr);
            double[] point = new double[3];
            double[] tangent = new double[3];
            double[] normal = new double[3];
            double[] binormal = new double[3];
            value /= this.Factor;
            eval.EvaluateUnitVectors(ptr, value, point, tangent, normal, binormal);
            return new Vector(normal);
        }

        public static implicit operator Snap.NX.Edge(NXOpen.Edge edge)
        {
            return CreateEdge(edge);
        }

        public static implicit operator NXOpen.Edge(Snap.NX.Edge edge)
        {
            return (NXOpen.Edge) edge.NXOpenTaggedObject;
        }

        public double Parameter(Snap.Position point)
        {
            double num4;
            UFSession uFSession = Globals.UFSession;
            Tag nXOpenTag = this.NXOpenTag;
            double[] array = point.Array;
            int direction = 1;
            double offset = 0.0;
            double[] numArray2 = new double[3];
            double tolerance = 0.0001;
            uFSession.Modl.AskPointAlongCurve2(array, nXOpenTag, offset, direction, tolerance, numArray2, out num4);
            return (((1.0 - num4) * this.MinU) + (num4 * this.MaxU));
        }

        public double Parameter(double arclengthFraction)
        {
            double arclength = this.ArcLength * arclengthFraction;
            return this.Parameter(this.MinU, arclength);
        }

        public double Parameter(double baseParameter, double arclength)
        {
            int direction = 1;
            if (arclength < 0.0)
            {
                direction = -1;
            }
            double parameter = 0.0;
            double tolerance = 0.0001;
            double[] numArray = new double[3];
            double[] array = this.Position(baseParameter).Array;
            Globals.UFSession.Modl.AskPointAlongCurve2(array, this.NXOpenTag, System.Math.Abs(arclength), direction, tolerance, numArray, out parameter);
            return ((parameter * (this.MaxU - this.MinU)) + this.MinU);
        }

        internal static Snap.Position[] PointSet(Snap.NX.ICurve icurve, int pointCount)
        {
            Globals.UndoMarkId markId = Globals.SetUndoMark(Globals.MarkVisibility.Invisible, "Snap_PositionArray_EqualParameter999");
            PointSetBuilder builder = Globals.WorkPart.NXOpenPart.Features.CreatePointSetBuilder(null);
            builder.Type = PointSetBuilder.Types.CurvePoints;
            builder.CurvePointsBy = PointSetBuilder.CurvePointsType.EqualArcLength;
            builder.Associative = false;
            builder.NumberOfPointsExpression.RightHandSide = pointCount.ToString();
            builder.StartPercentage.RightHandSide = "0";
            builder.EndPercentage.RightHandSide = "100";
            SelectionIntentRule[] rules = Snap.NX.Section.CreateSelectionIntentRule(new Snap.NX.ICurve[] { icurve });
            NXOpen.NXObject nXOpenTaggedObject = (NXOpen.NXObject) icurve.NXOpenTaggedObject;
            builder.SingleCurveOrEdgeCollector.AddToSection(rules, nXOpenTaggedObject, null, null, (Point3d) icurve.StartPoint, NXOpen.Section.Mode.Create, false);
            builder.Commit();
            NXOpen.NXObject[] committedObjects = builder.GetCommittedObjects();
            builder.Destroy();
            Snap.Position[] positionArray = new Snap.Position[committedObjects.Length];
            for (int i = 0; i < positionArray.Length; i++)
            {
                TaggedObject obj3 = committedObjects[i];
                Snap.NX.Point point = (NXOpen.Point) obj3;
                positionArray[i] = point.Position;
            }
            Globals.UndoToMark(markId, "Snap_PositionArray_EqualParameter999");
            Globals.DeleteUndoMark(markId, "Snap_PositionArray_EqualParameter999");
            return positionArray;
        }

        internal static Snap.Position[] PointSet(Snap.NX.ICurve icurve, double chordalTolerance, double angularTolerance, double stepTolerance)
        {
            double[] numArray;
            int numpts = 0;
            angularTolerance *= 0.017453292519943295;
            Globals.UFSession.Modl.AskCurvePoints(icurve.NXOpenTag, chordalTolerance, angularTolerance, stepTolerance, out numpts, out numArray);
            return Snap.Position.PositionsFromCoordinates(numArray);
        }

        public Snap.Position Position(double value)
        {
            IntPtr ptr;
            UFEval eval = Globals.UFSession.Eval;
            eval.Initialize2(this.NXOpenTag, out ptr);
            double[] point = new double[3];
            double[] derivatives = new double[3];
            value /= this.Factor;
            eval.Evaluate(ptr, 0, value, point, derivatives);
            return new Snap.Position(point);
        }

        public Snap.Position[] PositionArray(double chordalTolerance)
        {
            return PointSet(this, chordalTolerance, 0.0, 0.0);
        }

        public Snap.Position[] PositionArray(int pointCount)
        {
            return PointSet(this, pointCount);
        }

        public Snap.Position[] PositionArray(double chordalTolerance, double angularTolerance, double stepTolerance)
        {
            return PointSet(this, chordalTolerance, angularTolerance, stepTolerance);
        }

        public Vector Tangent(double value)
        {
            IntPtr ptr;
            UFEval eval = Globals.UFSession.Eval;
            eval.Initialize2(this.NXOpenTag, out ptr);
            double[] point = new double[3];
            double[] tangent = new double[3];
            double[] normal = new double[3];
            double[] binormal = new double[3];
            value /= this.Factor;
            eval.EvaluateUnitVectors(ptr, value, point, tangent, normal, binormal);
            return new Vector(tangent);
        }

        public Snap.NX.Spline ToSpline()
        {
            JoinCurvesBuilder builder = Globals.WorkPart.NXOpenPart.Features.CreateJoinCurvesBuilder(null);
            builder.DistanceTolerance = Globals.DistanceTolerance;
            builder.AngleTolerance = Globals.AngleTolerance;
            builder.Section.DistanceTolerance = Globals.DistanceTolerance;
            builder.Section.AngleTolerance = Globals.AngleTolerance;
            builder.Section.ChainingTolerance = (Globals.UnitType == Globals.Unit.Millimeter) ? 0.02413 : 0.00095;
            builder.Section.SetAllowedEntityTypes(NXOpen.Section.AllowTypes.OnlyCurves);
            builder.CurveOptions.Associative = false;
            builder.OutputCurveType = JoinCurvesBuilder.OutputCurve.General;
            builder.CurveOptions.InputCurveOption = CurveOptions.InputCurve.Retain;
            SelectionIntentRule[] rules = Snap.NX.Section.CreateSelectionIntentRule(new Snap.NX.ICurve[] { this });
            builder.Section.AddToSection(rules, (NXOpen.NXObject) this, null, null, (Point3d) Snap.Position.Origin, NXOpen.Section.Mode.Create, false);
            builder.Commit();
            NXOpen.NXObject obj2 = builder.GetCommittedObjects()[0];
            builder.Destroy();
            return new Snap.NX.Spline((NXOpen.Spline) obj2);
        }

        public static Snap.NX.Edge Wrap(Tag nxopenEdgeTag)
        {
            if (nxopenEdgeTag == Tag.Null)
            {
                throw new ArgumentException("Input tag is NXOpen.Tag.Null");
            }
            if (Snap.NX.NXObject.GetTypeFromTag(nxopenEdgeTag) != ObjectTypes.Type.Edge)
            {
                throw new ArgumentException("Input tag doesn't belong to an NXOpen.Edge object");
            }
            NXOpen.Edge objectFromTag = (NXOpen.Edge) Snap.NX.NXObject.GetObjectFromTag(nxopenEdgeTag);
            return CreateEdge(objectFromTag);
        }

        public double ArcLength
        {
            get
            {
                return this.NXOpenEdge.GetLength();
            }
        }

        public Snap.NX.Body Body
        {
            get
            {
                return this.NXOpenEdge.GetBody();
            }
        }

        public override Box3d Box
        {
            get
            {
                double[] numArray = new double[3];
                double[,] directions = new double[3, 3];
                double[] distances = new double[3];
                Tag tag = this.NXOpenEdge.Tag;
                Globals.UFSession.Modl.AskBoundingBoxExact(tag, Tag.Null, numArray, directions, distances);
                Snap.Position minXYZ = new Snap.Position(numArray);
                Vector vector = new Vector(directions[0, 0], directions[0, 1], directions[0, 2]);
                Vector vector2 = new Vector(directions[1, 0], directions[1, 1], directions[1, 2]);
                Vector vector3 = new Vector(directions[2, 0], directions[2, 1], directions[2, 2]);
                Vector vector4 = (Vector) (((numArray + (distances[0] * vector)) + (distances[1] * vector2)) + (distances[2] * vector3));
                return new Box3d(minXYZ, new Snap.Position(vector4.Array));
            }
        }

        public Snap.Position EndPoint
        {
            get
            {
                Point3d pointd;
                Point3d pointd2;
                this.NXOpenEdge.GetVertices(out pointd, out pointd2);
                return new Snap.Position(pointd2);
            }
        }

        public Snap.NX.Face[] Faces
        {
            get
            {
                int length = this.NXOpenEdge.GetFaces().Length;
                Snap.NX.Face[] faceArray = new Snap.NX.Face[length];
                NXOpen.Face[] faces = this.NXOpenEdge.GetFaces();
                for (int i = 0; i < length; i++)
                {
                    faceArray[i] = Snap.NX.Face.CreateFace(faces[i]);
                }
                return faceArray;
            }
        }

        internal virtual double Factor
        {
            get
            {
                if ((this.NXOpenEdge.SolidEdgeType != NXOpen.Edge.EdgeType.Elliptical) && (this.NXOpenEdge.SolidEdgeType != NXOpen.Edge.EdgeType.Circular))
                {
                    return 1.0;
                }
                return 57.295779513082323;
            }
        }

        public bool IsClosed
        {
            get
            {
                int status = 0;
                Globals.UFSession.Modl.AskCurvePeriodicity(this.NXOpenEdge.Tag, out status);
                if (status == 0)
                {
                    return false;
                }
                return true;
            }
        }

        public double MaxU
        {
            get
            {
                IntPtr ptr;
                UFEval eval = Globals.UFSession.Eval;
                eval.Initialize2(this.NXOpenTag, out ptr);
                double[] numArray2 = new double[2];
                numArray2[1] = 1.0;
                double[] limits = numArray2;
                eval.AskLimits(ptr, limits);
                return (this.Factor * limits[1]);
            }
        }

        public double MinU
        {
            get
            {
                IntPtr ptr;
                UFEval eval = Globals.UFSession.Eval;
                eval.Initialize2(this.NXOpenTag, out ptr);
                double[] numArray2 = new double[2];
                numArray2[1] = 1.0;
                double[] limits = numArray2;
                eval.AskLimits(ptr, limits);
                return (this.Factor * limits[0]);
            }
        }

        public override DisplayableObject NXOpenDisplayableObject
        {
            get
            {
                return base.NXOpenDisplayableObject;
            }
        }

        public NXOpen.Edge NXOpenEdge
        {
            get
            {
                return (NXOpen.Edge) base.NXOpenTaggedObject;
            }
        }

        public NXOpen.ICurve NXOpenICurve
        {
            get
            {
                return (NXOpen.ICurve) base.NXOpenTaggedObject;
            }
        }

        public Tag NXOpenTag
        {
            get
            {
                return base.NXOpenTag;
            }
        }

        public override ObjectTypes.SubType ObjectSubType
        {
            get
            {
                return GetEdgeType(this);
            }
        }

        public override ObjectTypes.Type ObjectType
        {
            get
            {
                return ObjectTypes.Type.Edge;
            }
        }

        public Snap.Geom.Surface.Plane Plane
        {
            get
            {
                int num;
                UFSession uFSession = Globals.UFSession;
                double[] data = new double[6];
                uFSession.Modl.AskObjDimensionality(this.NXOpenTag, out num, data);
                Snap.Geom.Surface.Plane plane = null;
                if (num == 2)
                {
                    Snap.Position point = new Snap.Position(data[0], data[1], data[2]);
                    Vector normal = new Vector(data[3], data[4], data[5]);
                    plane = new Snap.Geom.Surface.Plane(point, normal);
                }
                return plane;
            }
        }

        public Snap.NX.Edge Prototype
        {
            get
            {
                Tag protoTagFromOccTag = base.GetProtoTagFromOccTag(this.NXOpenTag);
                Snap.NX.Edge edge = null;
                if (protoTagFromOccTag != Tag.Null)
                {
                    edge = Wrap(protoTagFromOccTag);
                }
                return edge;
            }
        }

        public Snap.Position StartPoint
        {
            get
            {
                Point3d pointd;
                Point3d pointd2;
                this.NXOpenEdge.GetVertices(out pointd, out pointd2);
                return new Snap.Position(pointd);
            }
        }

        public class Arc : Snap.NX.Edge
        {
            internal Arc(NXOpen.Edge nxopenEdge) : base(nxopenEdge)
            {
                if (nxopenEdge.SolidEdgeType != NXOpen.Edge.EdgeType.Circular)
                {
                    throw new ArgumentException("Input object must be of type NXOpen.Edge.EdgeType.Circular");
                }
                base.NXOpenTaggedObject = nxopenEdge;
            }

            internal Arc(Snap.NX.Edge edge) : base((NXOpen.Edge) edge)
            {
                base.NXOpenTaggedObject = edge.NXOpenTaggedObject;
            }

            internal override double Factor
            {
                get
                {
                    return 57.295779513082323;
                }
            }

            public Snap.Geom.Curve.Arc Geometry
            {
                get
                {
                    Snap.Position position;
                    Vector vector;
                    Vector vector2;
                    double radius = -1.0;
                    double startAngle = -1.0;
                    double endAngle = -1.0;
                    Snap.NX.Edge.GetEdgeData(this, out radius, out startAngle, out endAngle, out position, out vector, out vector2);
                    return new Snap.Geom.Curve.Arc(position, vector, vector2, radius, startAngle, endAngle);
                }
            }
        }

        public class Ellipse : Snap.NX.Edge
        {
            internal Ellipse(NXOpen.Edge nxopenEdge) : base(nxopenEdge)
            {
                if (nxopenEdge.SolidEdgeType != NXOpen.Edge.EdgeType.Elliptical)
                {
                    throw new ArgumentException("Input object must be of type NXOpen.Edge.EdgeType.Elliptical");
                }
                base.NXOpenTaggedObject = nxopenEdge;
            }

            internal Ellipse(Snap.NX.Edge edge) : base((NXOpen.Edge) edge)
            {
                base.NXOpenTaggedObject = edge.NXOpenTaggedObject;
            }

            public Snap.Geom.Curve.Ellipse Geometry
            {
                get
                {
                    Snap.Position position;
                    Vector vector;
                    Vector vector2;
                    double num;
                    double num2;
                    double num3;
                    double num4;
                    Snap.NX.Edge.GetEdgeData(this, out position, out vector, out vector2, out num, out num2, out num3, out num4);
                    double startAngle = Vector.Angle((Vector) (base.StartPoint - position), vector);
                    return new Snap.Geom.Curve.Ellipse(position, vector, vector2, num, num2, startAngle, Vector.Angle((Vector) (base.EndPoint - position), vector));
                }
            }
        }

        public class Line : Snap.NX.Edge
        {
            internal Line(NXOpen.Edge nxopenEdge) : base(nxopenEdge)
            {
                if (nxopenEdge.SolidEdgeType != NXOpen.Edge.EdgeType.Linear)
                {
                    throw new ArgumentException("Input object must be of type NXOpen.Edge.EdgeType.Linear");
                }
                base.NXOpenTaggedObject = nxopenEdge;
            }

            internal Line(Snap.NX.Edge edge) : base((NXOpen.Edge) edge)
            {
                base.NXOpenTaggedObject = edge.NXOpenTaggedObject;
            }

            public Snap.Geom.Curve.Line Geometry
            {
                get
                {
                    Snap.Position position;
                    Snap.Position position2;
                    Snap.NX.Edge.GetEdgeData(this, out position, out position2);
                    return new Snap.Geom.Curve.Line(position, position2);
                }
            }
        }

        public class Spline : Snap.NX.Edge
        {
            internal Spline(NXOpen.Edge nxopenEdge) : base(nxopenEdge)
            {
                if (nxopenEdge.SolidEdgeType != NXOpen.Edge.EdgeType.Spline)
                {
                    throw new ArgumentException("Input object must be of type NXOpen.Edge.EdgeType.Elliptical");
                }
                base.NXOpenTaggedObject = nxopenEdge;
            }

            public Snap.Geom.Curve.Spline Geometry
            {
                get
                {
                    Snap.Position[] positionArray;
                    double[] numArray;
                    double[] numArray2;
                    Snap.NX.Edge.GetEdgeData(this, out positionArray, out numArray, out numArray2);
                    return new Snap.Geom.Curve.Spline(numArray, positionArray, numArray2);
                }
            }
        }
    }
}

