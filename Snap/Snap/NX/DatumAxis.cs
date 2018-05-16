namespace Snap.NX
{
    using NXOpen;
    using NXOpen.Features;
    using NXOpen.GeometricUtilities;
    using NXOpen.UF;
    using Snap;
    using System;

    public class DatumAxis : Snap.NX.Feature
    {
        internal DatumAxis(DatumAxisFeature datumAxisFeature) : base(datumAxisFeature)
        {
            this.NXOpenDatumAxisFeature = datumAxisFeature;
        }

        private static double ArcPercentage(Snap.NX.ICurve curve, Snap.Position point1, Snap.Position point2)
        {
            IntPtr ptr;
            double num = 0.0;
            double num2 = curve.MaxU - curve.MinU;
            double num3 = (curve.Parameter(point1) - curve.MinU) / num2;
            double num4 = (curve.Parameter(point2) - curve.MinU) / num2;
            UFEval eval = Globals.UFSession.Eval;
            eval.Initialize2(curve.NXOpenTag, out ptr);
            double[] numArray2 = new double[2];
            numArray2[1] = 1.0;
            double[] limits = numArray2;
            eval.AskLimits(ptr, limits);
            Globals.UFSession.Curve.AskArcLength(curve.NXOpenTag, num3, num4, ModlUnits.ModlMmeter, out num);
            return ((num / curve.ArcLength) * 100.0);
        }

        internal static Snap.NX.DatumAxis CreateDatumAxis(Snap.Position startPoint, Snap.Position endPoint)
        {
            NXOpen.Part workPart = (NXOpen.Part) Globals.WorkPart;
            NXOpen.Features.DatumAxisBuilder featureBuilder = workPart.Features.CreateDatumAxisBuilder(null);
            featureBuilder.Type = NXOpen.Features.DatumAxisBuilder.Types.TwoPoints;
            featureBuilder.IsAssociative = true;
            featureBuilder.IsAxisReversed = false;
            featureBuilder.Point1 = workPart.Points.CreatePoint((Point3d) startPoint);
            featureBuilder.Point2 = workPart.Points.CreatePoint((Point3d) endPoint);
            NXOpen.Features.Feature feature = (NXOpen.Features.Feature) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.Destroy();
            return new Snap.NX.DatumAxis((DatumAxisFeature) feature);
        }

        internal static Snap.NX.DatumAxis CreateDatumAxis(Snap.Position origin, Vector direction)
        {
            NXOpen.Part workPart = (NXOpen.Part) Globals.WorkPart;
            NXOpen.Features.DatumAxisBuilder featureBuilder = workPart.Features.CreateDatumAxisBuilder(null);
            featureBuilder.Type = NXOpen.Features.DatumAxisBuilder.Types.PointAndDir;
            NXOpen.Direction direction2 = workPart.Directions.CreateDirection((Point3d) origin, (Vector3d) direction, SmartObject.UpdateOption.WithinModeling);
            featureBuilder.IsAssociative = true;
            featureBuilder.IsAxisReversed = false;
            featureBuilder.Vector = direction2;
            featureBuilder.Point = workPart.Points.CreatePoint((Point3d) origin);
            NXOpen.Features.Feature feature = (NXOpen.Features.Feature) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.Destroy();
            return new Snap.NX.DatumAxis((DatumAxisFeature) feature);
        }

        internal static Snap.NX.DatumAxis CreateDatumAxis(Snap.NX.ICurve icurve, Snap.Number arcLength, CurveOrientations curveOrientation)
        {
            NXOpen.Features.DatumAxisBuilder featureBuilder = Globals.WorkPart.NXOpenPart.Features.CreateDatumAxisBuilder(null);
            featureBuilder.Type = NXOpen.Features.DatumAxisBuilder.Types.OnCurveVector;
            featureBuilder.ArcLength.IsPercentUsed = true;
            featureBuilder.ArcLength.Expression.RightHandSide = arcLength.ToString();
            featureBuilder.CurveOrientation = (NXOpen.Features.DatumAxisBuilder.CurveOrientations) curveOrientation;
            featureBuilder.IsAssociative = true;
            featureBuilder.IsAxisReversed = false;
            featureBuilder.Curve.Value = icurve.NXOpenICurve;
            featureBuilder.ArcLength.Path.Value = icurve.NXOpenTaggedObject;
            featureBuilder.ArcLength.Update(OnPathDimensionBuilder.UpdateReason.Path);
            NXOpen.Features.Feature feature = (NXOpen.Features.Feature) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.Destroy();
            return new Snap.NX.DatumAxis((DatumAxisFeature) feature);
        }

        public static implicit operator Snap.NX.DatumAxis(DatumAxisFeature datumAxisFeature)
        {
            return new Snap.NX.DatumAxis(datumAxisFeature);
        }

        public static implicit operator DatumAxisFeature(Snap.NX.DatumAxis datumAxis)
        {
            return (DatumAxisFeature) datumAxis.NXOpenTaggedObject;
        }

        public static Snap.NX.DatumAxis Wrap(Tag nxopenDatumAxisTag)
        {
            if (nxopenDatumAxisTag == Tag.Null)
            {
                throw new ArgumentException("Input tag is NXOpen.Tag.Null");
            }
            DatumAxisFeature objectFromTag = Snap.NX.NXObject.GetObjectFromTag(nxopenDatumAxisTag) as DatumAxisFeature;
            if (objectFromTag == null)
            {
                throw new ArgumentException("Input tag doesn't belong to an NXOpen.Features.DatumAxisFeature object");
            }
            return objectFromTag;
        }

        public NXOpen.Features.DatumAxisBuilder DatumAxisBuilder
        {
            get
            {
                NXOpen.Part owningPart = (NXOpen.Part) this.NXOpenDatumAxisFeature.OwningPart;
                DatumAxisFeature datumAxis = (DatumAxisFeature) this;
                return owningPart.Features.CreateDatumAxisBuilder(datumAxis);
            }
        }

        public Vector Direction
        {
            get
            {
                return this.NXOpenDatumAxisFeature.DatumAxis.Direction;
            }
        }

        public Snap.Position EndPoint
        {
            get
            {
                Point3d pointd;
                Point3d pointd2;
                this.NXOpenDatumAxisFeature.DatumAxis.GetEndPoints(out pointd, out pointd2);
                return pointd2;
            }
        }

        public DatumAxisFeature NXOpenDatumAxisFeature
        {
            get
            {
                return (DatumAxisFeature) base.NXOpenTaggedObject;
            }
            private set
            {
                base.NXOpenTaggedObject = value;
            }
        }

        public override DisplayableObject[] NXOpenDisplayableObjects
        {
            get
            {
                NXOpen.DatumAxis datumAxis = this.NXOpenDatumAxisFeature.DatumAxis;
                return new DisplayableObject[] { datumAxis };
            }
        }

        public override ObjectTypes.Type ObjectType
        {
            get
            {
                return ObjectTypes.Type.DatumAxis;
            }
        }

        public Snap.Position Origin
        {
            get
            {
                return this.NXOpenDatumAxisFeature.DatumAxis.Origin;
            }
            set
            {
                Session.UndoMarkId undoMark = Globals.Session.SetUndoMark(Session.MarkVisibility.Invisible, "");
                NXOpen.Part workPart = (NXOpen.Part) Globals.WorkPart;
                NXOpen.Features.DatumAxisBuilder featureBuilder = workPart.Features.CreateDatumAxisBuilder(this.NXOpenDatumAxisFeature);
                if (featureBuilder.Type == NXOpen.Features.DatumAxisBuilder.Types.TwoPoints)
                {
                    featureBuilder.Point1 = workPart.Points.CreatePoint((Point3d) value);
                    featureBuilder.Point2 = workPart.Points.CreatePoint((Point3d) this.EndPoint);
                }
                else if (featureBuilder.Type == NXOpen.Features.DatumAxisBuilder.Types.PointAndDir)
                {
                    featureBuilder.Point = workPart.Points.CreatePoint((Point3d) value);
                    NXOpen.Direction vector = featureBuilder.Vector;
                    featureBuilder.Vector = vector;
                }
                else
                {
                    SectionData[] dataArray;
                    SelectionIntentRule[] ruleArray;
                    featureBuilder.Section.GetSectionData(out dataArray);
                    dataArray[0].GetRules(out ruleArray);
                    if (ruleArray[0].Type == SelectionIntentRule.RuleType.CurveDumb)
                    {
                        NXOpen.Curve[] curveArray;
                        ((CurveDumbRule) ruleArray[0]).GetData(out curveArray);
                        Snap.NX.Curve curve = curveArray[0];
                        double number = ArcPercentage(curve, curve.StartPoint, value);
                        featureBuilder.ArcLength.Expression.RightHandSide = Snap.Number.ToString(number);
                    }
                    else
                    {
                        NXOpen.Edge[] edgeArray;
                        ((EdgeDumbRule) ruleArray[0]).GetData(out edgeArray);
                        Snap.NX.Edge edge = edgeArray[0];
                        double num2 = ArcPercentage(edge, edge.StartPoint, value);
                        featureBuilder.ArcLength.Expression.RightHandSide = Snap.Number.ToString(num2);
                    }
                }
                Snap.NX.Feature.CommitFeature(featureBuilder);
                featureBuilder.Destroy();
                Globals.Session.UpdateManager.DoUpdate(undoMark);
                Globals.Session.DeleteUndoMark(undoMark, "");
            }
        }

        public Snap.Position StartPoint
        {
            get
            {
                Point3d pointd;
                Point3d pointd2;
                this.NXOpenDatumAxisFeature.DatumAxis.GetEndPoints(out pointd, out pointd2);
                return pointd;
            }
        }

        public enum CurveOrientations
        {
            Tangent,
            Normal,
            Binormal
        }
    }
}

