namespace Snap.NX
{
    using NXOpen;
    using NXOpen.Features;
    using NXOpen.GeometricUtilities;
    using NXOpen.UF;
    using Snap;
    using Snap.Geom;
    using System;

    public class Curve : Snap.NX.NXObject, Snap.NX.ICurve
    {
        protected Curve(NXOpen.Edge nxopenCurve) : base(nxopenCurve)
        {
            base.NXOpenTaggedObject = nxopenCurve;
        }

        protected Curve(NXOpen.NXObject nxopenObject) : base(nxopenObject)
        {
            base.NXOpenTaggedObject = nxopenObject;
        }

        public Curve(Tag objectTag) : this(Snap.NX.NXObject.GetObjectFromTag(objectTag))
        {
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

        public Snap.NX.Curve Copy()
        {
            Transform xform = Transform.CreateTranslation(0.0, 0.0, 0.0);
            return this.Copy(xform);
        }

        public Snap.NX.Curve Copy(Transform xform)
        {
            NXOpen.Curve nXOpenTaggedObject = (NXOpen.Curve) Snap.NX.NXObject.Wrap(this.NXOpenCurve.Tag).Copy(xform).NXOpenTaggedObject;
            return CreateCurve(nXOpenTaggedObject);
        }

        public static Snap.NX.Curve[] Copy(params Snap.NX.Curve[] original)
        {
            Snap.NX.Curve[] curveArray = new Snap.NX.Curve[original.Length];
            for (int i = 0; i < original.Length; i++)
            {
                curveArray[i] = original[i].Copy();
            }
            return curveArray;
        }

        public static Snap.NX.Curve[] Copy(Transform xform, params Snap.NX.Curve[] original)
        {
            Snap.NX.Curve[] curveArray = new Snap.NX.Curve[original.Length];
            for (int i = 0; i < original.Length; i++)
            {
                curveArray[i] = original[i].Copy(xform);
            }
            return curveArray;
        }

        internal static Snap.NX.Curve CreateCurve(NXOpen.Curve nxopenCurve)
        {
            Snap.NX.Curve curve = new Snap.NX.Curve(nxopenCurve);
            ObjectTypes.Type objectType = curve.ObjectType;
            ObjectTypes.SubType objectSubType = curve.ObjectSubType;
            switch (objectType)
            {
                case ObjectTypes.Type.Line:
                    return new Snap.NX.Line((NXOpen.Line) nxopenCurve);

                case ObjectTypes.Type.Arc:
                    return new Snap.NX.Arc((NXOpen.Arc) nxopenCurve);

                case ObjectTypes.Type.Spline:
                    return new Snap.NX.Spline((NXOpen.Spline) nxopenCurve);
            }
            if (objectSubType == ObjectTypes.SubType.ConicEllipse)
            {
                curve = new Snap.NX.Ellipse((NXOpen.Ellipse) nxopenCurve);
            }
            return curve;
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
            bool flag = this.ObjectType == ObjectTypes.Type.Arc;
            UFEval eval = Globals.UFSession.Eval;
            eval.Initialize2(this.NXOpenTag, out ptr);
            double[] point = new double[3];
            double[] derivatives = new double[3 * order];
            value /= this.Factor;
            eval.Evaluate(ptr, order, value, point, derivatives);
            Vector[] vectorArray = new Vector[order + 1];
            vectorArray[0] = point;
            for (int i = 0; i < order; i++)
            {
                vectorArray[i + 1] = new Vector(derivatives[3 * i], derivatives[(3 * i) + 1], derivatives[(3 * i) + 2]);
                if (flag)
                {
                    vectorArray[i + 1] = (Vector) (System.Math.Pow(0.017453292519943295, (double) (i + 1)) * vectorArray[i + 1]);
                }
            }
            return vectorArray;
        }

        public virtual Snap.NX.Curve[] Divide(params double[] parameters)
        {
            Snap.NX.Curve curve = this.Copy();
            Snap.NX.Part workPart = Globals.WorkPart;
            //DivideCurveBuilder builder = workPart.NXOpenPart.BaseFeatures.CreateDivideCurveBuilder(null);
            //builder.Type = DivideCurveBuilder.Types.ByBoundingObjects;
            //BoundingObjectBuilder[] builderArray = new BoundingObjectBuilder[parameters.Length];
            //Snap.NX.Point[] nxObjects = new Snap.NX.Point[parameters.Length];
            //for (int i = 0; i < parameters.Length; i++)
            //{
            //    builderArray[i] = workPart.NXOpenPart.CreateBoundingObjectBuilder();
            //    builderArray[i].BoundingPlane = null;
            //    builderArray[i].BoundingObjectMethod = BoundingObjectBuilder.Method.ProjectPoint;
            //    nxObjects[i] = Create.Point(curve.Position(parameters[i]));
            //    builderArray[i].BoundingProjectPoint = (NXOpen.Point) nxObjects[i];
            //    builder.BoundingObjects.Append(builderArray[i]);
            //}
            //View workView = workPart.NXOpenPart.ModelingViews.WorkView;
            //builder.DividingCurve.SetValue((NXOpen.Curve) curve, workView, (Point3d) curve.StartPoint);
            //builder.Commit();
            //NXOpen.NXObject[] committedObjects = builder.GetCommittedObjects();
            //builder.Destroy();
            Snap.NX.Point[] nxObjects = new Snap.NX.Point[parameters.Length];
            var committedObjects = new System.Collections.Generic.List<NXOpen.NXObject>();
            for (int i = 0; i < parameters.Length; i++) 
            {
                DivideCurveBuilder builder = workPart.NXOpenPart.BaseFeatures.CreateDivideCurveBuilder(null);
                builder.Type = DivideCurveBuilder.Types.ByBoundingObjects;

                builder.BoundingObjectMethod = DivideCurveBuilder.BoundingObjectOption.ProjectPoint;
                builder.BoundingProjectPoint = Create.Point(curve.Position(parameters[i]));

                View workView = workPart.NXOpenPart.ModelingViews.WorkView;
                builder.DividingCurve.SetValue((NXOpen.Curve)curve, workView, (Point3d)curve.StartPoint);
                builder.Commit();
                committedObjects.AddRange(builder.GetCommittedObjects());
                builder.Destroy();
            }
            Snap.NX.Curve[] curveArray = new Snap.NX.Curve[committedObjects.Count];
            for (int j = 0; j < curveArray.Length; j++)
            {
                curveArray[j] = CreateCurve((NXOpen.Curve) committedObjects[j]);
            }
            Snap.NX.NXObject.Delete(nxObjects);
            return curveArray;
        }

        public virtual Snap.NX.Curve[] Divide(Snap.Geom.Surface.Plane geomPlane, Snap.Position helpPoint)
        {
            Compute.IntersectionResult result = Compute.IntersectInfo(this, geomPlane, helpPoint);
            return this.Divide(new double[] { result.CurveParameter });
        }

        public virtual Snap.NX.Curve[] Divide(Snap.NX.Face face, Snap.Position helpPoint)
        {
            Compute.IntersectionResult result = Compute.IntersectInfo(this, face, helpPoint);
            return this.Divide(new double[] { result.CurveParameter });
        }

        public virtual Snap.NX.Curve[] Divide(Snap.NX.ICurve boundingCurve, Snap.Position helpPoint)
        {
            Compute.IntersectionResult result = Compute.IntersectInfo(this, boundingCurve, helpPoint);
            return this.Divide(new double[] { result.CurveParameter });
        }

        private static double[] GetParameter(Compute.IntersectionResult[] result)
        {
            if (result == null)
            {
                throw NXException.Create(0xa396b);
            }
            double[] numArray = new double[result.Length];
            for (int i = 0; i < numArray.Length; i++)
            {
                numArray[i] = result[i].CurveParameter;
            }
            return numArray;
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

        public static implicit operator Snap.NX.Curve(NXOpen.Curve curve)
        {
            return new Snap.NX.Curve(curve);
        }

        public static implicit operator NXOpen.Curve(Snap.NX.Curve curve)
        {
            return (NXOpen.Curve) curve.NXOpenTaggedObject;
        }

        public double Parameter(Snap.Position point)
        {
            double num4;
            UFSession uFSession = Globals.UFSession;
            Tag nXOpenTag = this.NXOpenTag;
            double[] array = point.Array;
            int direction = 1;
            double offset = 0.0;
            double tolerance = 0.0001;
            double[] numArray2 = new double[3];
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
            double[] array = this.Position(baseParameter).Array;
            double parameter = 0.0;
            double tolerance = 0.0001;
            double[] numArray2 = new double[3];
            Globals.UFSession.Modl.AskPointAlongCurve2(array, this.NXOpenTag, System.Math.Abs(arclength), direction, tolerance, numArray2, out parameter);
            return ((parameter * (this.MaxU - this.MinU)) + this.MinU);
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

        public Snap.Position[] PositionArray(double[] values)
        {
            IntPtr ptr;
            UFEval eval = Globals.UFSession.Eval;
            eval.Initialize2(this.NXOpenTag, out ptr);
            double[] point = new double[3];
            double[] derivatives = new double[3];
            double num2 = 1.0 / this.Factor;
            Snap.Position[] positionArray = new Snap.Position[values.LongLength];
            for (long i = 0L; i < values.LongLength; i += 1L)
            {
                double parm = values[(int) ((IntPtr) i)] * num2;
                eval.Evaluate(ptr, 0, parm, point, derivatives);
                positionArray[(int) ((IntPtr) i)] = new Snap.Position(point);
            }
            return positionArray;
        }

        public Snap.Position[] PositionArray(double chordalTolerance)
        {
            return Snap.NX.Edge.PointSet(this, chordalTolerance, 0.0, 0.0);
        }

        public Snap.Position[] PositionArray(int pointCount)
        {
            return Snap.NX.Edge.PointSet(this, pointCount);
        }

        public Snap.Position[] PositionArray(double chordalTolerance, double angularTolerance, double stepTolerance)
        {
            return Snap.NX.Edge.PointSet(this, chordalTolerance, angularTolerance, stepTolerance);
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
            builder.Section.SetAllowedEntityTypes(NXOpen.SectionEx.AllowTypes.OnlyCurves);
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

        public virtual void Trim(double lowerParam, double upperParam)
        {
            Snap.NX.Part workPart = Globals.WorkPart;
            TrimCurve trimCurve = null;
            TrimCurveBuilder builder = workPart.NXOpenPart.Features.CreateTrimCurveBuilder(trimCurve);
            builder.InteresectionMethod = TrimCurveBuilder.InteresectionMethods.UserDefined;
            builder.InteresectionDirectionOption = TrimCurveBuilder.InteresectionDirectionOptions.RelativeToWcs;
            builder.CurvesToTrim.AllowSelfIntersection(true);
            builder.CurvesToTrim.SetAllowedEntityTypes(NXOpen.SectionEx.AllowTypes.CurvesAndPoints);
            builder.CurveOptions.Associative = false;
            builder.CurveOptions.InputCurveOption = CurveOptions.InputCurve.Replace;
            builder.CurveExtensionType = TrimCurveBuilder.CurveExtensionTypes.Natural;
            Snap.NX.Point point = Create.Point(this.Position(lowerParam));
            Snap.NX.Point point2 = Create.Point(this.Position(upperParam));
            Snap.NX.Section section = Snap.NX.Section.CreateSection(new Snap.NX.Point[] { point });
            Snap.NX.Section section2 = Snap.NX.Section.CreateSection(new Snap.NX.Point[] { point2 });
            builder.CurveList.Add(base.NXOpenTaggedObject, null, (Point3d) this.StartPoint);
            SelectionIntentRule[] rules = Snap.NX.Section.CreateSelectionIntentRule(new Snap.NX.ICurve[] { this });
            builder.CurvesToTrim.AddToSection(rules, (NXOpen.NXObject) this, null, null, (Point3d) this.StartPoint, NXOpen.Section.Mode.Create, false);
            builder.FirstBoundingObject.Add(section.NXOpenSection);
            builder.SecondBoundingObject.Add(section2.NXOpenSection);
            builder.Commit();
            builder.Destroy();
            section.NXOpenSection.Destroy();
            section2.NXOpenSection.Destroy();
            Snap.NX.NXObject.Delete(new Snap.NX.NXObject[] { point });
            Snap.NX.NXObject.Delete(new Snap.NX.NXObject[] { point2 });
        }

        public static Snap.NX.Curve Wrap(Tag nxopenCurveTag)
        {
            if (nxopenCurveTag == Tag.Null)
            {
                throw new ArgumentException("Input tag is NXOpen.Tag.Null");
            }
            NXOpen.Curve objectFromTag = Snap.NX.NXObject.GetObjectFromTag(nxopenCurveTag) as NXOpen.Curve;
            if (objectFromTag == null)
            {
                throw new ArgumentException("Input tag doesn't belong to an NXOpen.Curve object");
            }
            return objectFromTag;
        }

        public double ArcLength
        {
            get
            {
                return this.NXOpenCurve.GetLength();
            }
        }

        public override Box3d Box
        {
            get
            {
                double[] numArray = new double[3];
                double[,] directions = new double[3, 3];
                double[] distances = new double[3];
                Tag tag = this.NXOpenCurve.Tag;
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
                return this.Position(this.MaxU);
            }
        }

        internal virtual double Factor
        {
            get
            {
                return 1.0;
            }
        }

        public bool IsClosed
        {
            get
            {
                int status = 0;
                Globals.UFSession.Modl.AskCurvePeriodicity(this.NXOpenCurve.Tag, out status);
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

        public NXOpen.Curve NXOpenCurve
        {
            get
            {
                return (NXOpen.Curve) base.NXOpenTaggedObject;
            }
        }

        public override DisplayableObject NXOpenDisplayableObject
        {
            get
            {
                return base.NXOpenDisplayableObject;
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

        public Snap.NX.Curve Prototype
        {
            get
            {
                Tag protoTagFromOccTag = base.GetProtoTagFromOccTag(this.NXOpenTag);
                Snap.NX.Curve curve = null;
                if (protoTagFromOccTag != Tag.Null)
                {
                    curve = Wrap(protoTagFromOccTag);
                }
                return curve;
            }
        }

        public Snap.Position StartPoint
        {
            get
            {
                return this.Position(this.MinU);
            }
        }
    }
}

