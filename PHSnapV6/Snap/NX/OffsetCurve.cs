namespace Snap.NX
{
    using NXOpen;
    using NXOpen.Features;
    using NXOpen.GeometricUtilities;
    using Snap;
    using System;

    public class OffsetCurve : Snap.NX.Feature
    {
        internal OffsetCurve(NXOpen.Features.OffsetCurve offsetCurve) : base(offsetCurve)
        {
            this.NXOpenOffsetCurve = offsetCurve;
        }

        internal static Snap.NX.OffsetCurve CreateOffsetCurve(Snap.NX.ICurve[] curves, Snap.Number distance, bool reverseDirection)
        {
            NXOpen.Features.OffsetCurveBuilder featureBuilder = Globals.WorkPart.NXOpenPart.Features.CreateOffsetCurveBuilder(null);
            featureBuilder.Type = NXOpen.Features.OffsetCurveBuilder.Types.Distance;
            featureBuilder.InputCurvesOptions.InputCurveOption = CurveOptions.InputCurve.Retain;
            featureBuilder.Tolerance = Globals.DistanceTolerance;
            featureBuilder.TrimMethod = NXOpen.Features.OffsetCurveBuilder.TrimOption.ExtendTangents;
            featureBuilder.CurvesToOffset.DistanceTolerance = Globals.DistanceTolerance;
            featureBuilder.CurvesToOffset.AngleTolerance = Globals.AngleTolerance;
            featureBuilder.CurvesToOffset.ChainingTolerance = (Globals.UnitType == Globals.Unit.Millimeter) ? 0.02413 : 0.00095;
            featureBuilder.CurvesToOffset.SetAllowedEntityTypes(NXOpen.SectionEx.AllowTypes.OnlyCurves);
            featureBuilder.CurvesToOffset.AllowSelfIntersection(true);
            featureBuilder.OffsetDistance.RightHandSide = distance.ToString();
            featureBuilder.ReverseDirection = reverseDirection;
            Snap.NX.Section curvesToOffset = featureBuilder.CurvesToOffset;
            for (int i = 0; i < curves.Length; i++)
            {
                curvesToOffset.AddICurve(curves);
            }
            NXOpen.Features.Feature feature = (NXOpen.Features.Feature) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.Destroy();
            return (feature as NXOpen.Features.OffsetCurve);
        }

        internal static Snap.NX.OffsetCurve CreateOffsetCurve(Snap.NX.ICurve[] icurves, Snap.Number height, Snap.Number angle, bool reverseDirection)
        {
            NXOpen.Features.OffsetCurveBuilder featureBuilder = Globals.WorkPart.NXOpenPart.Features.CreateOffsetCurveBuilder(null);
            featureBuilder.Type = NXOpen.Features.OffsetCurveBuilder.Types.Draft;
            featureBuilder.InputCurvesOptions.InputCurveOption = CurveOptions.InputCurve.Retain;
            featureBuilder.Tolerance = Globals.DistanceTolerance;
            featureBuilder.DraftHeight.RightHandSide = height.ToString();
            featureBuilder.DraftAngle.RightHandSide = angle.ToString();
            featureBuilder.ReverseDirection = reverseDirection;
            Snap.NX.Section curvesToOffset = featureBuilder.CurvesToOffset;
            for (int i = 0; i < icurves.Length; i++)
            {
                curvesToOffset.AddICurve(icurves);
            }
            NXOpen.Features.Feature feature = (NXOpen.Features.Feature) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.CurvesToOffset.CleanMappingData();
            featureBuilder.Destroy();
            return (feature as NXOpen.Features.OffsetCurve);
        }

        internal static Snap.NX.OffsetCurve CreateOffsetCurve(Snap.NX.ICurve[] curves, Snap.Number distance, Position helpPoint, Vector helpVector)
        {
            NXOpen.Features.OffsetCurveBuilder builder = Globals.WorkPart.NXOpenPart.Features.CreateOffsetCurveBuilder(null);
            builder.Type = NXOpen.Features.OffsetCurveBuilder.Types.Distance;
            builder.InputCurvesOptions.InputCurveOption = CurveOptions.InputCurve.Retain;
            builder.Tolerance = Globals.DistanceTolerance;
            //builder.CurveFitData.AngleTolerance = Globals.AngleTolerance;
            builder.CurvesToOffset.SetAllowedEntityTypes(NXOpen.SectionEx.AllowTypes.OnlyCurves);
            builder.CurvesToOffset.AllowSelfIntersection(true);
            builder.OffsetDistance.RightHandSide = distance.ToString();
            ((Snap.NX.Section)builder.CurvesToOffset).AddICurve(curves);
            builder.ReverseDirection = IsReverseDirection(builder, curves, helpPoint, helpVector);
            NXOpen.Features.Feature feature = (NXOpen.Features.Feature) Snap.NX.Feature.CommitFeature(builder);
            builder.Destroy();
            return (feature as NXOpen.Features.OffsetCurve);
        }

        internal static Snap.NX.OffsetCurve CreateOffsetCurve(Snap.NX.ICurve[] curves, Snap.Number height, Snap.Number angle, Position helpPoint, Vector helpVector)
        {
            NXOpen.Features.OffsetCurveBuilder builder = Globals.WorkPart.NXOpenPart.Features.CreateOffsetCurveBuilder(null);
            builder.Type = NXOpen.Features.OffsetCurveBuilder.Types.Draft;
            builder.InputCurvesOptions.InputCurveOption = CurveOptions.InputCurve.Retain;
            builder.Tolerance = Globals.DistanceTolerance;
            builder.DraftHeight.RightHandSide = height.ToString();
            builder.DraftAngle.RightHandSide = angle.ToString();
            ((Snap.NX.Section)builder.CurvesToOffset).AddICurve(curves);
            builder.ReverseDirection = IsReverseDirection(builder, curves, helpPoint, helpVector);
            NXOpen.Features.Feature feature = (NXOpen.Features.Feature) Snap.NX.Feature.CommitFeature(builder);
            builder.CurvesToOffset.CleanMappingData();
            builder.Destroy();
            return (feature as NXOpen.Features.OffsetCurve);
        }

        internal static Snap.NX.OffsetCurve CreateOffsetLine(Snap.NX.ICurve icurve, Snap.NX.Point point, string distance, bool reverseDirection)
        {
            NXOpen.Features.OffsetCurveBuilder featureBuilder = Globals.WorkPart.NXOpenPart.Features.CreateOffsetCurveBuilder(null);
            featureBuilder.Type = NXOpen.Features.OffsetCurveBuilder.Types.Distance;
            featureBuilder.InputCurvesOptions.InputCurveOption = CurveOptions.InputCurve.Retain;
            featureBuilder.Tolerance = Globals.DistanceTolerance;
            featureBuilder.OffsetDistance.RightHandSide = distance;
            featureBuilder.ReverseDirection = reverseDirection;
            featureBuilder.PointOnOffsetPlane = (NXOpen.Point) point;
            ((Snap.NX.Section)featureBuilder.CurvesToOffset).AddICurve(new Snap.NX.ICurve[] { icurve });
            NXOpen.Features.Feature feature = (NXOpen.Features.Feature) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.CurvesToOffset.CleanMappingData();
            featureBuilder.Destroy();
            return (feature as NXOpen.Features.OffsetCurve);
        }

        internal static Snap.NX.OffsetCurve CreateOffsetLine(Snap.NX.ICurve icurve, Snap.NX.Point point, string height, string angle, bool reverseDirection)
        {
            NXOpen.Features.OffsetCurveBuilder featureBuilder = Globals.WorkPart.NXOpenPart.Features.CreateOffsetCurveBuilder(null);
            featureBuilder.Type = NXOpen.Features.OffsetCurveBuilder.Types.Draft;
            featureBuilder.InputCurvesOptions.InputCurveOption = CurveOptions.InputCurve.Retain;
            featureBuilder.Tolerance = Globals.DistanceTolerance;
            featureBuilder.PointOnOffsetPlane = (NXOpen.Point) point;
            featureBuilder.DraftHeight.RightHandSide = height;
            featureBuilder.DraftAngle.RightHandSide = angle;
            featureBuilder.ReverseDirection = reverseDirection;
            ((Snap.NX.Section)featureBuilder.CurvesToOffset).AddICurve(new Snap.NX.ICurve[] { icurve });
            NXOpen.Features.Feature feature = (NXOpen.Features.Feature) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.CurvesToOffset.CleanMappingData();
            featureBuilder.Destroy();
            return (feature as NXOpen.Features.OffsetCurve);
        }

        private static bool IsReverseDirection(NXOpen.Features.OffsetCurveBuilder builder, Snap.NX.ICurve[] icurves, Position pos, Vector helpVector)
        {
            Position position;
            Point3d pointd;
            Vector3d vectord;
            int index = -1;
            double num2 = -1.0;
            for (int i = 0; i < icurves.Length; i++)
            {
                double num4 = -1.0;
                num4 = Compute.Distance(pos, (Snap.NX.NXObject) icurves[i]);
                if (num4 > num2)
                {
                    num2 = num4;
                    index = i;
                }
            }
            if (icurves[index].ObjectType == ObjectTypes.Type.Edge)
            {
                position = Compute.ClosestPoints(pos, (Snap.NX.Edge) icurves[index]).Point2;
            }
            else
            {
                position = Compute.ClosestPoints(pos, (Snap.NX.Curve) icurves[index]).Point2;
            }
            builder.ComputeOffsetDirection(icurves[index].NXOpenICurve, (Point3d) position, out vectord, out pointd);
            return ((vectord * helpVector) < 0.0);
        }

        public static implicit operator Snap.NX.OffsetCurve(NXOpen.Features.OffsetCurve offsetCurve)
        {
            return new Snap.NX.OffsetCurve(offsetCurve);
        }

        public static implicit operator NXOpen.Features.OffsetCurve(Snap.NX.OffsetCurve offsetCurve)
        {
            return offsetCurve.NXOpenOffsetCurve;
        }

        public static Snap.NX.OffsetCurve Wrap(Tag nxopenOffsetCurveTag)
        {
            if (nxopenOffsetCurveTag == Tag.Null)
            {
                throw new ArgumentException("Input tag is NXOpen.Tag.Null");
            }
            NXOpen.Features.OffsetCurve objectFromTag = Snap.NX.NXObject.GetObjectFromTag(nxopenOffsetCurveTag) as NXOpen.Features.OffsetCurve;
            if (objectFromTag == null)
            {
                throw new ArgumentException("Input tag doesn't belong to an NXOpen.Features.OffsetCurve object");
            }
            return objectFromTag;
        }

        public Snap.NX.Curve[] Curves
        {
            get
            {
                NXOpen.NXObject[] entities = this.NXOpenOffsetCurve.GetEntities();
                Snap.NX.Curve[] curveArray = new Snap.NX.Curve[entities.Length];
                for (int i = 0; i < entities.Length; i++)
                {
                    curveArray[i] = (NXOpen.Curve) entities[i];
                }
                return curveArray;
            }
        }

        public NXOpen.Features.OffsetCurve NXOpenOffsetCurve
        {
            get
            {
                return (NXOpen.Features.OffsetCurve) base.NXOpenTaggedObject;
            }
            private set
            {
                base.NXOpenTaggedObject = value;
            }
        }

        public NXOpen.Features.OffsetCurveBuilder OffsetCurveBuilder
        {
            get
            {
                NXOpen.Part owningPart = (NXOpen.Part) this.NXOpenOffsetCurve.OwningPart;
                return owningPart.Features.CreateOffsetCurveBuilder(this.NXOpenOffsetCurve);
            }
        }
    }
}

