namespace Snap
{
    using NXOpen;
    using NXOpen.UF;
    using Snap.Geom;
    using Snap.NX;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public static class Compute
    {
        public static double ArcLength(params Snap.NX.ICurve[] icurves)
        {
            double num = 0.0;
            foreach (Snap.NX.ICurve curve in icurves)
            {
                num += curve.ArcLength;
            }
            return num;
        }

        public static double Area(params Snap.NX.Face[] faces)
        {
            NXOpen.Unit unit;
            NXOpen.Unit unit2;
            NXOpen.Part nXOpenWorkPart = Globals.NXOpenWorkPart;
            MeasureFaceBuilder builder = nXOpenWorkPart.MeasureManager.CreateMeasureFaceBuilder(null);
            builder.InfoWindow = false;
            for (int i = 0; i < faces.Length; i++)
            {
                builder.FaceObjects.Add((DisplayableObject) faces[i]);
            }
            if (Globals.UnitType == Globals.Unit.Millimeter)
            {
                unit = GetUnit("mm^2");
                unit2 = GetUnit("mm");
            }
            else
            {
                unit = GetUnit("in^2");
                unit2 = GetUnit("in");
            }
            IParameterizedSurface[] objects = new IParameterizedSurface[faces.Length];
            for (int j = 0; j < faces.Length; j++)
            {
                objects[j] = faces[j].NXOpenFace;
            }
            MeasureFaces faces2 = nXOpenWorkPart.MeasureManager.NewFaceProperties(unit, unit2, 0.99, objects);
            double area = faces2.Area;
            faces2.Dispose();
            builder.FaceObjects.Clear();
            builder.Destroy();
            return area;
        }

        internal static Snap.Position[] ClipRay(Snap.Geom.Curve.Ray ray)
        {
            double num4;
            double num5;
            double num = -1000000.0;
            double num2 = 1000000.0;
            double num3 = 1E-16;
            double num8 = 200000.0 * Globals.MillimetersPerUnit;
            double x = ray.Axis.X;
            double y = ray.Origin.X;
            if (System.Math.Abs(x) > num3)
            {
                num4 = System.Math.Min((double) ((-num8 - y) / x), (double) ((num8 - y) / x));
                num5 = System.Math.Max((double) ((-num8 - y) / x), (double) ((num8 - y) / x));
                num = System.Math.Max(num4, num);
                num2 = System.Math.Min(num5, num2);
            }
            x = ray.Axis.Y;
            y = ray.Origin.Y;
            if (System.Math.Abs(x) > num3)
            {
                num4 = System.Math.Min((double) ((-num8 - y) / x), (double) ((num8 - y) / x));
                num5 = System.Math.Max((double) ((-num8 - y) / x), (double) ((num8 - y) / x));
                num = System.Math.Max(num4, num);
                num2 = System.Math.Min(num5, num2);
            }
            x = ray.Axis.Z;
            y = ray.Origin.Z;
            if (System.Math.Abs(x) > num3)
            {
                num4 = System.Math.Min((double) ((-num8 - y) / x), (double) ((num8 - y) / x));
                num5 = System.Math.Max((double) ((-num8 - y) / x), (double) ((num8 - y) / x));
                num = System.Math.Max(num4, num);
                num2 = System.Math.Min(num5, num2);
            }
            Snap.Position position = ray.Origin + ((Snap.Position) (num * ray.Axis));
            Snap.Position position2 = ray.Origin + ((Snap.Position) (num2 * ray.Axis));
            return new Snap.Position[] { position, position2 };
        }

        public static DistanceResult ClosestPoints(Snap.Geom.Curve.Ray ray1, Snap.Geom.Curve.Ray ray2)
        {
            Snap.Position[] positionArray = ClipRay(ray1);
            Snap.NX.Line line = Create.Line(positionArray[0], positionArray[1]);
            Snap.Position[] positionArray2 = ClipRay(ray2);
            Snap.NX.Line line2 = Create.Line(positionArray2[0], positionArray2[1]);
            DistanceResult result = ClosestPoints(line, line2);
            if (Vector.Angle(ray1.Axis, ray2.Axis) < 1E-05)
            {
                Snap.Position position = ClosestPoints(Snap.Position.Origin, ray1).Point2;
                Snap.Position position2 = ClosestPoints(Snap.Position.Origin, ray2).Point2;
                result.Point1 = position;
                result.Point2 = position2;
            }
            Snap.NX.NXObject.Delete(new Snap.NX.NXObject[] { line, line2 });
            return result;
        }

        public static DistanceResult ClosestPoints(Snap.Geom.Curve.Ray ray, Snap.NX.NXObject nxObject)
        {
            if (IsWrongType(nxObject))
            {
                throw new ArgumentException("The input object is not a curve, edge, face, or body.");
            }
            Snap.Position[] positionArray = ClipRay(ray);
            Snap.NX.Line line = Create.Line(positionArray[0], positionArray[1]);
            DistanceResult result = ClosestPoints(line, nxObject);
            Snap.NX.NXObject.Delete(new Snap.NX.NXObject[] { line });
            return result;
        }

        public static DistanceResult ClosestPoints(Surface.Plane plane, Snap.NX.NXObject nxObject)
        {
            if (IsWrongType(nxObject))
            {
                throw new ArgumentException("The input object is not a curve, edge, face, or body.");
            }
            Snap.Position[] points = CreateShadow(plane, nxObject);
            Session.UndoMarkId markId = Globals.Session.SetUndoMark(Session.MarkVisibility.Invisible, "TmpDistanceMark_999");
            DistanceResult result = ClosestPoints(Create.BoundedPlane(Create.Polygon(points)), nxObject);
            Globals.Session.UndoToMark(markId, "TmpDistanceMark_999");
            return result;
        }

        public static DistanceResult ClosestPoints(Snap.NX.NXObject nxObject1, Snap.NX.NXObject nxObject2)
        {
            double num2;
            double num3;
            if (IsWrongType(nxObject1))
            {
                throw new ArgumentException("The first input object is not a curve, edge, face, or body.");
            }
            if (IsWrongType(nxObject2))
            {
                throw new ArgumentException("The second input object is not a curve, edge, face, or body.");
            }
            int num = 2;
            double[] numArray = new double[3];
            double[] numArray2 = new double[3];
            Globals.UFSession.Modl.AskMinimumDist3(num, nxObject1.NXOpenTag, nxObject2.NXOpenTag, 0, numArray, 0, numArray2, out num2, numArray, numArray2, out num3);
            Snap.Position position = new Snap.Position(numArray);
            Snap.Position position2 = new Snap.Position(numArray2);
            return new DistanceResult(position, position2, Snap.Position.Distance(position, position2));
        }

        public static DistanceResult ClosestPoints(Snap.Position point, Snap.Geom.Curve.Ray ray)
        {
            Vector vector = (Vector) (point - ray.Origin);
            double num = (double) (vector * ray.Axis);
            Snap.Position position = ray.Origin + ((Snap.Position) (num * ray.Axis));
            return new DistanceResult(point, position, Snap.Position.Distance(point, position));
        }

        public static DistanceResult ClosestPoints(Snap.Position point, Surface.Plane plane)
        {
            Vector vector = (Vector) (point - plane.Origin);
            double num = (double) (vector * plane.Normal);
            return new DistanceResult(point, (Snap.Position)(point - ((Snap.Position)(num * plane.Normal))), System.Math.Abs(num));
        }

        public static DistanceResult ClosestPoints(Snap.Position point, Snap.NX.NXObject nxObject)
        {
            double num2;
            double num3;
            if (IsWrongType(nxObject))
            {
                throw new ArgumentException("The input object is not a curve, edge, face, or body.");
            }
            int num = 2;
            double[] numArray = new double[3];
            double[] numArray2 = new double[3];
            Tag @null = Tag.Null;
            Globals.UFSession.Modl.AskMinimumDist3(num, @null, nxObject.NXOpenTag, 0, point.Array, 0, point.Array, out num2, numArray, numArray2, out num3);
            Snap.Position position = new Snap.Position(numArray2);
            return new DistanceResult(point, position, Snap.Position.Distance(point, position));
        }

        public static MassPropertiesResult Combine(params MassPropertiesResult[] inputResults)
        {
            int length = inputResults.Length;
            UFWeight.Properties[] propertiesArray = new UFWeight.Properties[length];
            for (int i = 0; i < length; i++)
            {
                propertiesArray[i] = MassPropsToWeightProps(inputResults[i]);
            }
            UFWeight.Properties properties = new UFWeight.Properties();
            Globals.UFSession.Weight.SumProps(length, propertiesArray, out properties);
            return WeightPropsToMassProps(properties);
        }

        private static MassPropertiesResult ConvertUnits(MassPropertiesResult input, UFWeight.UnitsType outUnits, ObjectTypes.SubType subType)
        {
            UFWeight.Properties properties = new UFWeight.Properties();
            UFWeight.Properties properties2 = new UFWeight.Properties();
            properties = MassPropsToWeightProps(input);
            Globals.UFSession.Weight.ConvertPropUnits(ref properties, outUnits, out properties2);
            MassPropertiesResult result = WeightPropsToMassProps(properties2);
            if (Globals.UnitType == Globals.Unit.Millimeter)
            {
                result.RadiusOfGyration = 10.0 * input.RadiusOfGyration;
                return result;
            }
            result.RadiusOfGyration = input.RadiusOfGyration;
            return result;
        }

        private static Snap.Position[] CreateShadow(Surface.Plane plane, Snap.NX.NXObject nxObject)
        {
            Snap.Position[] corners = nxObject.Box.Corners;
            Snap.Position origin = plane.Origin;
            Snap.Orientation orientation = new Snap.Orientation(plane.Normal);
            Vector axisX = orientation.AxisX;
            Vector axisY = orientation.AxisY;
            double num = 100000.0;
            double num2 = -100000.0;
            double num3 = 0.0;
            double num4 = 100000.0;
            double num5 = -100000.0;
            double num6 = 0.0;
            for (int i = 0; i < 8; i++)
            {
                Snap.Position position2 = ClosestPoints(corners[i], plane).Point2;
                num3 = (double) ((position2 - origin) * axisX);
                if (num3 < num)
                {
                    num = num3;
                }
                if (num3 > num2)
                {
                    num2 = num3;
                }
                num6 = (double) ((position2 - origin) * axisY);
                if (num6 < num4)
                {
                    num4 = num6;
                }
                if (num6 > num5)
                {
                    num5 = num6;
                }
            }
            double num8 = (num + num2) / 2.0;
            double num9 = 10.0 + ((1.1 * (num2 - num)) / 2.0);
            double num10 = (num4 + num5) / 2.0;
            double num11 = 10.0 + ((1.1 * (num5 - num4)) / 2.0);
            num = num8 - num9;
            num2 = num8 + num9;
            num4 = num10 - num11;
            num5 = num10 + num11;
            Snap.Position position3 = (Snap.Position) ((origin + (num * axisX)) + (num4 * axisY));
            Snap.Position position4 = (Snap.Position) ((origin + (num * axisX)) + (num5 * axisY));
            Snap.Position position5 = (Snap.Position) ((origin + (num2 * axisX)) + (num5 * axisY));
            Snap.Position position6 = (Snap.Position) ((origin + (num2 * axisX)) + (num4 * axisY));
            return new Snap.Position[] { position3, position4, position5, position6 };
        }

        public static double Deviation(Snap.NX.Curve curve, Snap.NX.Face face, int numCheckPoints)
        {
            return DeviationInfo(curve, face, numCheckPoints).MaximumDistanceError;
        }

        public static double Deviation(Snap.NX.ICurve icurve1, Snap.NX.ICurve icurve2, int numCheckPoints)
        {
            return DeviationInfo(icurve1, icurve2, numCheckPoints).MaximumDistanceError;
        }

        public static double Deviation(Snap.NX.Edge edge, Snap.NX.Face faceNearEdge, Snap.NX.Face face, int numCheckPoints)
        {
            return DeviationInfo(edge, faceNearEdge, face, numCheckPoints).MaximumDistanceError;
        }

        public static double Deviation(Snap.NX.Face face1, Snap.NX.Face face2, int numCheckPointsU, int numCheckPointsV)
        {
            return DeviationInfo(face1, face2, numCheckPointsU, numCheckPointsV).MaximumDistanceError;
        }

        public static DeviationResult DeviationInfo(Snap.NX.Curve curve, Snap.NX.Face face, int numCheckPoints)
        {
            //UFModl.DeviationCheckData data;
            //Globals.UFSession.Modl.DevchkCurveToFace(curve.NXOpenTag, face.NXOpenTag, numCheckPoints, out data);
            //return new DeviationResult { MaximumDistanceError = data.maximum_distance_error, AverageDistanceError = data.average_distance_error, MaximumAngleError = data.maximum_angle_error, AverageAngleError = data.average_angle_error, DistanceErrors = data.distance_errors, AngleErrors = data.angle_errors };
            return new DeviationResult();
        }

        public static DeviationResult DeviationInfo(Snap.NX.ICurve icurve1, Snap.NX.ICurve icurve2, int numCheckPoints)
        {
            //UFModl.DeviationCheckData data;
            //Globals.UFSession.Modl.DevchkCurveToCurve(icurve1.NXOpenTag, icurve2.NXOpenTag, numCheckPoints, out data);
            //return new DeviationResult { MaximumDistanceError = data.maximum_distance_error, AverageDistanceError = data.average_distance_error, MaximumAngleError = data.maximum_angle_error, AverageAngleError = data.average_angle_error, DistanceErrors = data.distance_errors, AngleErrors = data.angle_errors };
            return new DeviationResult();
        }

        public static DeviationResult DeviationInfo(Snap.NX.Edge edge, Snap.NX.Face faceNearEdge, Snap.NX.Face face, int numCheckPoints)
        {
            //UFModl.DeviationCheckData data;
            //Globals.UFSession.Modl.DevchkEdgeToFace(edge.NXOpenTag, faceNearEdge.NXOpenTag, face.NXOpenTag, numCheckPoints, out data);
            //return new DeviationResult { MaximumDistanceError = data.maximum_distance_error, AverageDistanceError = data.average_distance_error, MaximumAngleError = data.maximum_angle_error, AverageAngleError = data.average_angle_error, DistanceErrors = data.distance_errors, AngleErrors = data.angle_errors };
            return new DeviationResult();
        }

        public static DeviationResult DeviationInfo(Snap.NX.Face face1, Snap.NX.Face face2, int numCheckPointsU, int numCheckPointsV)
        {
            //UFModl.DeviationCheckData data;
            //Globals.UFSession.Modl.DevchkFaceToFace(face1.NXOpenTag, face2.NXOpenTag, numCheckPointsU, numCheckPointsV, out data);
            //return new DeviationResult { MaximumDistanceError = data.maximum_distance_error, AverageDistanceError = data.average_distance_error, MaximumAngleError = data.maximum_angle_error, AverageAngleError = data.average_angle_error, DistanceErrors = data.distance_errors, AngleErrors = data.angle_errors };
            return new DeviationResult();
        }

        public static DeviationResult DeviationInfo(Snap.NX.Edge edge1, Snap.NX.Face face1, Snap.NX.Edge edge2, Snap.NX.Face face2, int numCheckPoints)
        {
            //UFModl.DeviationCheckData data;
            //Globals.UFSession.Modl.DevchkEdgeToEdge(edge1.NXOpenTag, face1.NXOpenTag, edge2.NXOpenTag, face2.NXOpenTag, numCheckPoints, out data);
            //return new DeviationResult { MaximumDistanceError = data.maximum_distance_error, AverageDistanceError = data.average_distance_error, MaximumAngleError = data.maximum_angle_error, AverageAngleError = data.average_angle_error, DistanceErrors = data.distance_errors, AngleErrors = data.angle_errors };
            return new DeviationResult();
        }

        public static double Distance(Snap.Geom.Curve.Ray ray1, Snap.Geom.Curve.Ray ray2)
        {
            return ClosestPoints(ray1, ray2).Distance;
        }

        public static double Distance(Snap.Geom.Curve.Ray ray, Snap.NX.NXObject nxObject)
        {
            return ClosestPoints(ray, nxObject).Distance;
        }

        public static double Distance(Surface.Plane plane, Snap.NX.NXObject nxObject)
        {
            return ClosestPoints(plane, nxObject).Distance;
        }

        public static double Distance(Snap.NX.NXObject nxObject1, Snap.NX.NXObject nxObject2)
        {
            return ClosestPoints(nxObject1, nxObject2).Distance;
        }

        public static double Distance(Snap.Position point, Snap.Geom.Curve.Ray ray)
        {
            return ClosestPoints(point, ray).Distance;
        }

        public static double Distance(Snap.Position point, Surface.Plane plane)
        {
            return ClosestPoints(point, plane).Distance;
        }

        public static double Distance(Snap.Position point, Snap.NX.NXObject nxObject)
        {
            return ClosestPoints(point, nxObject).Distance;
        }

        private static Snap.Position[] GetPosition(IntersectionResult[] result)
        {
            if (result == null)
            {
                return null;
            }
            Snap.Position[] positionArray = new Snap.Position[result.Length];
            for (int i = 0; i < positionArray.Length; i++)
            {
                positionArray[i] = result[i].Position.Value;
            }
            return positionArray;
        }

        private static Snap.Position? GetPosition(IntersectionResult result)
        {
            if (result == null)
            {
                return null;
            }
            return result.Position;
        }

        internal static NXOpen.Unit GetUnit(string abbreviation)
        {
            UnitCollection unitCollection = Globals.NXOpenWorkPart.UnitCollection;
            Dictionary<string, NXOpen.Unit> dictionary = new Dictionary<string, NXOpen.Unit>(500);
            foreach (NXOpen.Unit unit in unitCollection)
            {
                dictionary.Add(unit.Abbreviation, unit);
            }
            return dictionary[abbreviation];
        }

        private static double IcurveParameter(Snap.NX.ICurve icurve, double param)
        {
            if (icurve is Snap.NX.Curve)
            {
                Snap.NX.Curve curve = (Snap.NX.Curve) icurve;
                if (curve.Factor != 1.0)
                {
                    return (param * curve.Factor);
                }
                return ((param * (curve.MaxU - curve.MinU)) + curve.MinU);
            }
            Snap.NX.Edge edge = (Snap.NX.Edge) icurve;
            if (edge.Factor != 1.0)
            {
                return (param * edge.Factor);
            }
            return ((param * (edge.MaxU - edge.MinU)) + edge.MinU);
        }

        public static Snap.Position? Intersect(Snap.Geom.Curve.Ray ray, Surface.Plane plane)
        {
            return GetPosition(IntersectInfo(ray, plane));
        }

        public static Snap.Position[] Intersect(Snap.Geom.Curve.Ray ray, Snap.NX.Face face)
        {
            return GetPosition(IntersectInfo(ray, face));
        }

        public static Snap.Geom.Curve.Ray Intersect(Surface.Plane p1, Surface.Plane p2)
        {
            Snap.Geom.Curve.Ray ray = null;
            Vector u = Vector.Cross(p1.Normal, p2.Normal);
            if (System.Math.Abs(Vector.Norm(u)) > 1E-14)
            {
                Surface.Plane plane = new Surface.Plane(Snap.Position.Origin, u);
                ray = new Snap.Geom.Curve.Ray(Intersect(p1, p2, plane).Value, u);
            }
            return ray;
        }

        public static Snap.Position[] Intersect(Snap.NX.ICurve icurve, Surface.Plane plane)
        {
            return GetPosition(IntersectInfo(icurve, plane));
        }

        public static Snap.Position[] Intersect(Snap.NX.ICurve icurve, Snap.NX.Face face)
        {
            return GetPosition(IntersectInfo(icurve, face));
        }

        public static Snap.Position[] Intersect(Snap.NX.ICurve curve1, Snap.NX.ICurve curve2)
        {
            return GetPosition(IntersectInfo(curve1, curve2));
        }

        public static Snap.Position? Intersect(Snap.Geom.Curve.Ray ray, Snap.NX.Body body, Snap.Position nearPoint)
        {
            return GetPosition(IntersectInfo(ray, body, nearPoint));
        }

        public static Snap.Position? Intersect(Snap.Geom.Curve.Ray ray, Snap.NX.Face face, Snap.Position nearPoint)
        {
            return GetPosition(IntersectInfo(ray, face, nearPoint));
        }

        public static Snap.Position? Intersect(Surface.Plane p1, Surface.Plane p2, Surface.Plane p3)
        {
            Snap.Position? nullable = null;
            double d = p1.D;
            Vector normal = p1.Normal;
            double num2 = p2.D;
            Vector u = p2.Normal;
            double num3 = p3.D;
            Vector v = p3.Normal;
            double num4 = (double) (normal * Vector.Cross(u, v));
            if (System.Math.Abs(num4) > 1E-14)
            {
                Vector vector4 = (Vector) (d * Vector.Cross(u, v));
                Vector vector5 = (Vector) (num2 * Vector.Cross(v, normal));
                Vector vector6 = (Vector) (num3 * Vector.Cross(normal, u));
                Vector vector7 = (Vector) (((vector4 + vector5) + vector6) / num4);
                nullable = new Snap.Position?((Snap.Position) vector7);
            }
            return nullable;
        }

        public static Snap.Position? Intersect(Snap.NX.ICurve icurve, Surface.Plane plane, Snap.Position nearPoint)
        {
            return GetPosition(IntersectInfo(icurve, plane, nearPoint));
        }

        public static Snap.Position? Intersect(Snap.NX.ICurve icurve, Snap.NX.Face face, Snap.Position nearPoint)
        {
            return GetPosition(IntersectInfo(icurve, face, nearPoint));
        }

        public static Snap.Position? Intersect(Snap.NX.ICurve curve1, Snap.NX.ICurve curve2, Snap.Position nearPoint)
        {
            return GetPosition(IntersectInfo(curve1, curve2, nearPoint));
        }

        public static IntersectionResult IntersectInfo(Snap.Geom.Curve.Ray ray, Surface.Plane plane)
        {
            Snap.Position? nullable = null;
            double num = plane.D - ((ray.Origin - Snap.Position.Origin) * plane.Normal);
            double num2 = (double) (ray.Axis * plane.Normal);
            if (System.Math.Abs(num2) < 1E-12)
            {
                if (System.Math.Abs(num) < 1E-14)
                {
                    DistanceResult result = ClosestPoints(Snap.Position.Origin, ray);
                    nullable = new Snap.Position?(result.Point2);
                }
            }
            else
            {
                double num3 = num / num2;
                nullable = new Snap.Position?(ray.Origin + ((Snap.Position) (num3 * ray.Axis)));
            }
            NXOpen.UF.UFCurve.IntersectInfo info = new NXOpen.UF.UFCurve.IntersectInfo();
            Snap.Position? nullable2 = nullable;
            Snap.Position origin = ray.Origin;
            Vector? nullable4 = nullable2.HasValue ? new Vector?(nullable2.GetValueOrDefault() - origin) : null;
            Vector axis = ray.Axis;
            double? nullable6 = nullable4.HasValue ? new double?(nullable4.GetValueOrDefault() * axis) : null;
            info.curve_parm = nullable6.Value;
            info.curve_point = nullable.Value.Array;
            info.entity_parms = new double[2];
            info.type_of_intersection = 1;
            return IntersectionResult.Create(info);
        }

        public static IntersectionResult[] IntersectInfo(Snap.Geom.Curve.Ray ray, Snap.NX.Face face)
        {
            int num;
            double[] numArray;
            Session.UndoMarkId markId = Globals.Session.SetUndoMark(Session.MarkVisibility.Invisible, "TmpIntersectMark_999");
            Snap.Position[] positionArray = ClipRay(ray);
            Snap.NX.Line line = Create.Line(positionArray[0], positionArray[1]);
            Globals.UFSession.Modl.IntersectCurveToFace(line.NXOpenTag, face.NXOpenTag, out num, out numArray);
            IntersectionResult[] resultArray = null;
            if (num != 0)
            {
                resultArray = new IntersectionResult[num];
                for (int i = 0; i < resultArray.Length; i++)
                {
                    resultArray[i] = new IntersectionResult();
                }
                for (int j = 0; j < num; j++)
                {
                    resultArray[j].Position = new Snap.Position(numArray[6 * j], numArray[(6 * j) + 1], numArray[(6 * j) + 2]);
                    resultArray[j].CurveParameter = (double) ((line.Position(numArray[(6 * j) + 3]) - ray.Origin) * ray.Axis);
                    resultArray[j].ObjectParameters = new double[] { face.Parameters(resultArray[j].Position.Value)[0], face.Parameters(resultArray[j].Position.Value)[1] };
                }
                Globals.Session.UndoToMark(markId, "TmpIntersectMark_999");
            }
            return resultArray;
        }

        public static IntersectionResult[] IntersectInfo(Snap.NX.ICurve icurve, Surface.Plane plane)
        {
            int num;
            double[] numArray;
            Session.UndoMarkId markId = Globals.Session.SetUndoMark(Session.MarkVisibility.Invisible, "TmpIntersectMark_999");
            Tag @null = Tag.Null;
            Globals.UFSession.Modl.CreatePlane(plane.Origin.Array, plane.Normal.Array, out @null);
            Globals.UFSession.Modl.IntersectCurveToPlane(icurve.NXOpenTag, @null, out num, out numArray);
            Globals.Session.UndoToMark(markId, "TmpIntersectMark_999");
            IntersectionResult[] resultArray = null;
            if (num != 0)
            {
                resultArray = new IntersectionResult[num];
                for (int i = 0; i < resultArray.Length; i++)
                {
                    resultArray[i] = new IntersectionResult();
                }
                for (int j = 0; j < num; j++)
                {
                    resultArray[j].Position = new Snap.Position(numArray[4 * j], numArray[(4 * j) + 1], numArray[(4 * j) + 2]);
                    resultArray[j].CurveParameter = icurve.Parameter(resultArray[j].Position.Value);
                }
            }
            return resultArray;
        }

        public static IntersectionResult[] IntersectInfo(Snap.NX.ICurve icurve, Snap.NX.Face face)
        {
            int num;
            double[] numArray;
            Globals.UFSession.Modl.IntersectCurveToFace(icurve.NXOpenTag, face.NXOpenTag, out num, out numArray);
            IntersectionResult[] resultArray = null;
            if (num != 0)
            {
                resultArray = new IntersectionResult[num];
                for (int i = 0; i < resultArray.Length; i++)
                {
                    resultArray[i] = new IntersectionResult();
                }
                for (int j = 0; j < num; j++)
                {
                    resultArray[j].Position = new Snap.Position(numArray[6 * j], numArray[(6 * j) + 1], numArray[(6 * j) + 2]);
                    resultArray[j].CurveParameter = icurve.Parameter(resultArray[j].Position.Value);
                    resultArray[j].ObjectParameters = new double[] { face.Parameters(resultArray[j].Position.Value)[0], face.Parameters(resultArray[j].Position.Value)[1] };
                }
            }
            return resultArray;
        }

        public static IntersectionResult[] IntersectInfo(Snap.NX.ICurve icurve1, Snap.NX.ICurve icurve2)
        {
            int num;
            double[] numArray;
            Globals.UFSession.Modl.IntersectCurveToCurve(icurve1.NXOpenTag, icurve2.NXOpenTag, out num, out numArray);
            IntersectionResult[] resultArray = null;
            if (num != 0)
            {
                resultArray = new IntersectionResult[num];
                for (int i = 0; i < resultArray.Length; i++)
                {
                    resultArray[i] = new IntersectionResult();
                }
                for (int j = 0; j < num; j++)
                {
                    resultArray[j].Position = new Snap.Position(numArray[5 * j], numArray[(5 * j) + 1], numArray[(5 * j) + 2]);
                    resultArray[j].CurveParameter = icurve1.Parameter(resultArray[j].Position.Value);
                    resultArray[j].ObjectParameters = new double[] { icurve2.Parameter(resultArray[j].Position.Value), 0.0 };
                }
            }
            return resultArray;
        }

        public static IntersectionResult IntersectInfo(Snap.Geom.Curve.Ray ray, Snap.NX.Body body, Snap.Position nearPoint)
        {
            UFSession uFSession = Globals.UFSession;
            Session.UndoMarkId markId = Globals.Session.SetUndoMark(Session.MarkVisibility.Invisible, "TmpIntersectMark_999");
            Snap.Position[] positionArray = ClipRay(ray);
            Snap.NX.Line line = Create.Line(positionArray[0], positionArray[1]);
            double[] array = nearPoint.Array;
            NXOpen.UF.UFCurve.IntersectInfo info = new NXOpen.UF.UFCurve.IntersectInfo();
            uFSession.Curve.Intersect(line.NXOpenTag, body.NXOpenTag, array, out info);
            Globals.Session.UndoToMark(markId, "TmpIntersectMark_999");
            IntersectionResult result = IntersectionResult.Create(info);
            if (result != null)
            {
                Snap.Position? nullable = result.Position;
                Snap.Position origin = ray.Origin;
                Vector? nullable3 = nullable.HasValue ? new Vector?(nullable.GetValueOrDefault() - origin) : null;
                Vector axis = ray.Axis;
                double? nullable5 = nullable3.HasValue ? new double?(nullable3.GetValueOrDefault() * axis) : null;
                result.CurveParameter = nullable5.Value;
            }
            return result;
        }

        public static IntersectionResult IntersectInfo(Snap.Geom.Curve.Ray ray, Snap.NX.Face face, Snap.Position nearPoint)
        {
            UFSession uFSession = Globals.UFSession;
            Session.UndoMarkId markId = Globals.Session.SetUndoMark(Session.MarkVisibility.Invisible, "TmpIntersectMark_999");
            Snap.Position[] positionArray = ClipRay(ray);
            Snap.NX.Line line = Create.Line(positionArray[0], positionArray[1]);
            double[] array = nearPoint.Array;
            NXOpen.UF.UFCurve.IntersectInfo info = new NXOpen.UF.UFCurve.IntersectInfo();
            uFSession.Curve.Intersect(line.NXOpenTag, face.NXOpenTag, array, out info);
            Globals.Session.UndoToMark(markId, "TmpIntersectMark_999");
            IntersectionResult result = IntersectionResult.Create(info);
            if (result != null)
            {
                Snap.Position? nullable = result.Position;
                Snap.Position origin = ray.Origin;
                Vector? nullable3 = nullable.HasValue ? new Vector?(nullable.GetValueOrDefault() - origin) : null;
                Vector axis = ray.Axis;
                double? nullable5 = nullable3.HasValue ? new double?(nullable3.GetValueOrDefault() * axis) : null;
                result.CurveParameter = nullable5.Value;
                result.ObjectParameters[0] *= face.FactorU;
                result.ObjectParameters[1] *= face.FactorV;
            }
            return result;
        }

        public static IntersectionResult IntersectInfo(Snap.NX.ICurve icurve, Surface.Plane plane, Snap.Position nearPoint)
        {
            Tag tag2;
            UFSession uFSession = Globals.UFSession;
            Tag nXOpenTag = icurve.NXOpenTag;
            double[] array = plane.Normal.Array;
            Vector vector2 = (Vector) (plane.D * plane.Normal);
            double[] numArray2 = vector2.Array;
            Session.UndoMarkId markId = Globals.Session.SetUndoMark(Session.MarkVisibility.Invisible, "TmpIntersectMark_999");
            uFSession.Modl.CreatePlane(numArray2, array, out tag2);
            double[] numArray3 = nearPoint.Array;
            NXOpen.UF.UFCurve.IntersectInfo info = new NXOpen.UF.UFCurve.IntersectInfo();
            uFSession.Curve.Intersect(nXOpenTag, tag2, numArray3, out info);
            Globals.Session.UndoToMark(markId, "TmpIntersectMark_999");
            info.curve_parm = IcurveParameter(icurve, info.curve_parm);
            return IntersectionResult.Create(info);
        }

        public static IntersectionResult IntersectInfo(Snap.NX.ICurve icurve, Snap.NX.Face face, Snap.Position nearPoint)
        {
            UFSession uFSession = Globals.UFSession;
            Tag nXOpenTag = icurve.NXOpenTag;
            double[] array = nearPoint.Array;
            NXOpen.UF.UFCurve.IntersectInfo info = new NXOpen.UF.UFCurve.IntersectInfo();
            uFSession.Curve.Intersect(nXOpenTag, face.NXOpenTag, array, out info);
            info.curve_parm = IcurveParameter(icurve, info.curve_parm);
            info.entity_parms = new double[] { face.FactorU * info.entity_parms[0], face.FactorV * info.entity_parms[1] };
            return IntersectionResult.Create(info);
        }

        public static IntersectionResult IntersectInfo(Snap.NX.ICurve icurve1, Snap.NX.ICurve icurve2, Snap.Position nearPoint)
        {
            UFSession uFSession = Globals.UFSession;
            Tag nXOpenTag = icurve1.NXOpenTag;
            Tag entity = icurve2.NXOpenTag;
            double[] array = nearPoint.Array;
            NXOpen.UF.UFCurve.IntersectInfo info = new NXOpen.UF.UFCurve.IntersectInfo();
            uFSession.Curve.Intersect(nXOpenTag, entity, array, out info);
            info.curve_parm = IcurveParameter(icurve1, info.curve_parm);
            info.entity_parms[0] = IcurveParameter(icurve2, info.entity_parms[0]);
            return IntersectionResult.Create(info);
        }

        private static bool IsWrongType(Snap.NX.NXObject nxObject)
        {
            bool flag = false;
            if (nxObject.ObjectType == ObjectTypes.Type.Line)
            {
                flag = true;
            }
            if (nxObject.ObjectType == ObjectTypes.Type.Arc)
            {
                flag = true;
            }
            if (nxObject.ObjectType == ObjectTypes.Type.Conic)
            {
                flag = true;
            }
            if (nxObject.ObjectType == ObjectTypes.Type.Spline)
            {
                flag = true;
            }
            if (nxObject.ObjectType == ObjectTypes.Type.Edge)
            {
                flag = true;
            }
            if (nxObject.ObjectType == ObjectTypes.Type.Face)
            {
                flag = true;
            }
            if (nxObject.ObjectType == ObjectTypes.Type.Body)
            {
                flag = true;
            }
            return !flag;
        }

        private static MassPropertiesResult MakeResult(double[] mp)
        {
            MassPropertiesResult result = new MassPropertiesResult {
                Area = mp[0],
                Volume = mp[1],
                Mass = mp[2],
                Centroid = new Snap.Position(mp[3], mp[4], mp[5]),
                MomentsOfInertia = new double[] { mp[9], mp[10], mp[11] },
                ProductsOfInertia = new double[] { mp[0x10], mp[0x11], mp[0x12] }
            };
            Vector vector = new Vector(mp[0x16], mp[0x17], mp[0x18]);
            Vector vector2 = new Vector(mp[0x19], mp[0x1a], mp[0x1b]);
            Vector vector3 = new Vector(mp[0x1c], mp[0x1d], mp[30]);
            result.PrincipalAxes = new Vector[] { vector, vector2, vector3 };
            result.PrincipalMoments = new double[] { mp[0x1f], mp[0x20], mp[0x21] };
            result.RadiusOfGyration = mp[40];
            result.Density = mp[0x2e];
            return result;
        }

        public static double Mass(params Snap.NX.Body[] bodies)
        {
            return MassProperties(bodies).Mass;
        }

        public static MassPropertiesResult MassProperties(params Snap.NX.Body[] bodies)
        {
            Snap.NX.Body[] bodyArray = Enumerable.Where<Snap.NX.Body>(bodies,u=>true).ToArray<Snap.NX.Body>();
            Snap.NX.Body[] bodyArray2 = Enumerable.Where<Snap.NX.Body>(bodies, u => true).ToArray<Snap.NX.Body>();
            List<MassPropertiesResult> list = new List<MassPropertiesResult>();
            if (bodyArray.Length > 0)
            {
                list.Add(MassProperties2(bodyArray));
            }
            foreach (Snap.NX.Body body in bodyArray2)
            {
                list.Add(MassProperties2(new Snap.NX.Body[] { body }));
            }
            MassPropertiesResult result = new MassPropertiesResult();
            if (list.Count > 1)
            {
                result = Combine(list.ToArray());
            }
            else
            {
                result = list[0];
            }
            result.CompleteResults();
            return result;
        }

        internal static MassPropertiesResult MassProperties2(params Snap.NX.Body[] bodies)
        {
            Globals.Unit unitType = Globals.UnitType;
            UFSession uFSession = Globals.UFSession;
            int type = 1;
            double density = -939.0;
            if (bodies[0].ObjectSubType == ObjectTypes.SubType.BodySheet)
            {
                type = 2;
                if (unitType == Globals.Unit.Millimeter)
                {
                    density = bodies[0].Density * 100.0;
                }
                else
                {
                    density = bodies[0].Density;
                }
            }
            int units = 0;
            if (unitType == Globals.Unit.Millimeter)
            {
                units = 3;
            }
            else
            {
                units = 1;
            }
            int accuracy = 1;
            double[] numArray4 = new double[11];
            numArray4[0] = 0.99;
            double[] numArray = numArray4;
            double[] numArray2 = new double[0x2f];
            double[] statistics = new double[13];
            int length = bodies.Length;
            Tag[] objects = Enumerable.Select<Snap.NX.Body, Tag>(bodies, u => u.NXOpenTag).ToArray<Tag>();
            Snap.NX.CoordinateSystem wcs = Globals.Wcs;
            Globals.Wcs = Create.CoordinateSystem(Snap.Position.Origin, Snap.Orientation.Identity);
            uFSession.Modl.AskMassProps3d(objects, length, type, units, density, accuracy, numArray, numArray2, statistics);
            Globals.Wcs = wcs;
            MassPropertiesResult input = MakeResult(numArray2);
            MassPropertiesResult result2 = new MassPropertiesResult();
            if (unitType == Globals.Unit.Millimeter)
            {
                input.Units = UFWeight.UnitsType.UnitsGc;
                return ConvertUnits(input, UFWeight.UnitsType.UnitsGm, bodies[0].ObjectSubType);
            }
            input.Units = UFWeight.UnitsType.UnitsLi;
            return input;
        }

        private static UFWeight.Properties MassPropsToWeightProps(MassPropertiesResult mpr)
        {
            return new UFWeight.Properties { cache_state = UFWeight.StateType.Cached, accuracy = 0.99, area = mpr.Area, area_state = UFWeight.StateType.Cached, center_of_mass = mpr.Centroid.Array, cofm_state = UFWeight.StateType.Cached, density = mpr.Density, density_state = UFWeight.StateType.Cached, mass = mpr.Mass, mass_state = UFWeight.StateType.Cached, moments_of_inertia = mpr.MomentsOfInertia, mofi_state = UFWeight.StateType.Cached, volume = mpr.Volume, volume_state = UFWeight.StateType.Cached, products_of_inertia = mpr.ProductsOfInertia, units = mpr.Units };
        }

        public static MinimumRadiusResult[] MinimumRadius(Snap.NX.Face face)
        {
            int num;
            double[] radii = new double[2];
            double[] positions = new double[6];
            double[] numArray3 = new double[4];
            Globals.UFSession.Modl.AskFaceMinRadii(face.NXOpenTag, out num, radii, positions, numArray3);
            MinimumRadiusResult result = new MinimumRadiusResult();
            MinimumRadiusResult result2 = new MinimumRadiusResult();
            switch (num)
            {
                case 1:
                {
                    result.Length = System.Math.Abs(radii[0]);
                    result.Position = new Snap.Position(positions[0], positions[1], positions[2]);
                    double[] numArray4 = new double[] { face.FactorU * numArray3[0], face.FactorV * numArray3[1] };
                    result.U = numArray4[0];
                    result.V = numArray4[1];
                    return new MinimumRadiusResult[] { result };
                }
                case 2:
                {
                    result.Length = System.Math.Abs(radii[0]);
                    result2.Length = System.Math.Abs(radii[1]);
                    result.Position = new Snap.Position(positions[0], positions[1], positions[2]);
                    result2.Position = new Snap.Position(positions[3], positions[4], positions[5]);
                    double[] numArray5 = new double[] { face.FactorU * numArray3[0], face.FactorV * numArray3[1] };
                    result.U = numArray5[0];
                    result.V = numArray5[1];
                    double[] numArray6 = new double[] { face.FactorU * numArray3[2], face.FactorV * numArray3[3] };
                    result2.U = numArray6[0];
                    result2.V = numArray6[1];
                    return new MinimumRadiusResult[] { result, result2 };
                }
            }
            return null;
        }

        public static double MomentOfInertia(Snap.Position axisPoint, Vector axisVector, params Snap.NX.Body[] bodies)
        {
            MassPropertiesResult result = MassProperties(bodies);
            double[,] inertiaTensor = result.InertiaTensor;
            Vector axis = Vector.Unit(axisVector);
            Vector vector2 = new Vector(inertiaTensor[0, 0], inertiaTensor[1, 0], inertiaTensor[2, 0]);
            Vector vector3 = new Vector(inertiaTensor[0, 1], inertiaTensor[1, 1], inertiaTensor[2, 1]);
            Vector vector4 = new Vector(inertiaTensor[0, 2], inertiaTensor[1, 2], inertiaTensor[2, 2]);
            Vector vector5 = new Vector((double) (axis * vector2), (double) (axis * vector3), (double) (axis * vector4));
            double num = (double) (vector5 * axis);
            double num2 = Distance(result.Centroid, new Snap.Geom.Curve.Ray(axisPoint, axis));
            return (num + ((result.Mass * num2) * num2));
        }

        public static double Perimeter(params Snap.NX.Face[] faces)
        {
            NXOpen.Unit unit;
            NXOpen.Unit unit2;
            NXOpen.Part nXOpenWorkPart = Globals.NXOpenWorkPart;
            MeasureFaceBuilder builder = nXOpenWorkPart.MeasureManager.CreateMeasureFaceBuilder(null);
            builder.InfoWindow = false;
            for (int i = 0; i < faces.Length; i++)
            {
                builder.FaceObjects.Add((DisplayableObject) faces[i]);
            }
            if (Globals.UnitType == Globals.Unit.Millimeter)
            {
                unit = GetUnit("mm^2");
                unit2 = GetUnit("mm");
            }
            else
            {
                unit = GetUnit("in^2");
                unit2 = GetUnit("in");
            }
            IParameterizedSurface[] objects = new IParameterizedSurface[faces.Length];
            for (int j = 0; j < faces.Length; j++)
            {
                objects[j] = faces[j].NXOpenFace;
            }
            MeasureFaces faces2 = nXOpenWorkPart.MeasureManager.NewFaceProperties(unit, unit2, 0.99, objects);
            double perimeter = faces2.Perimeter;
            faces2.Dispose();
            builder.FaceObjects.Clear();
            builder.Destroy();
            return perimeter;
        }

        private static MassPropertiesResult Transform(MassPropertiesResult input, Snap.Geom.Transform xform)
        {
            UFWeight.Properties properties = new UFWeight.Properties();
            UFWeight.Properties properties2 = new UFWeight.Properties();
            properties = MassPropsToWeightProps(input);
            double[] matrix = xform.Matrix;
            double[,] numArray3 = new double[1, 0x10];
            numArray3[0, 0] = matrix[0];
            numArray3[0, 1] = matrix[1];
            numArray3[0, 2] = matrix[2];
            numArray3[0, 3] = matrix[3];
            numArray3[0, 4] = matrix[4];
            numArray3[0, 5] = matrix[5];
            numArray3[0, 6] = matrix[6];
            numArray3[0, 7] = matrix[7];
            numArray3[0, 8] = matrix[8];
            numArray3[0, 9] = matrix[9];
            numArray3[0, 10] = matrix[10];
            numArray3[0, 11] = matrix[11];
            numArray3[0, 15] = 1.0;
            double[,] transform = numArray3;
            Globals.UFSession.Weight.TransformProps(transform, ref properties, out properties2);
            return WeightPropsToMassProps(properties2);
        }

        public static double Volume(params Snap.NX.Body[] bodies)
        {
            return MassProperties(bodies).Volume;
        }

        private static MassPropertiesResult WeightPropsToMassProps(UFWeight.Properties wp)
        {
            MassPropertiesResult result = new MassPropertiesResult {
                Area = wp.area,
                Centroid = new Snap.Position(wp.center_of_mass),
                Density = wp.density,
                Mass = wp.mass,
                MomentsOfInertia = wp.moments_of_inertia,
                ProductsOfInertia = wp.products_of_inertia,
                Units = wp.units,
                Volume = wp.volume,
                RadiusOfGyration = -1.0
            };
            if (wp.volume == 0.0)
            {
                result.Density = result.Mass / result.Area;
            }
            return result;
        }

        public class DeviationResult
        {
            public double[] AngleErrors { get; internal set; }

            public double AverageAngleError { get; internal set; }

            public double AverageDistanceError { get; internal set; }

            public double[] DistanceErrors { get; internal set; }

            public double MaximumAngleError { get; internal set; }

            public double MaximumDistanceError { get; internal set; }
        }

        public class DistanceResult
        {
            internal DistanceResult(Snap.Position p1, Snap.Position p2, double distance)
            {
                this.Point1 = p1;
                this.Point2 = p2;
                this.Distance = distance;
            }

            public double Distance { get; internal set; }

            public Snap.Position Point1 { get; internal set; }

            public Snap.Position Point2 { get; internal set; }
        }

        public class IntersectionResult
        {
            internal static Compute.IntersectionResult Create(UFCurve.IntersectInfo info)
            {
                if (info.type_of_intersection != 1)
                {
                    return null;
                }
                return new Compute.IntersectionResult { Position = new Snap.Position(info.curve_point), CurveParameter = info.curve_parm, ObjectParameters = info.entity_parms };
            }

            public double CurveParameter { get; internal set; }

            public double[] ObjectParameters { get; internal set; }

            public Snap.Position? Position { get; internal set; }
        }

        public class MassPropertiesResult
        {
            internal UFWeight.UnitsType Units;

            internal void CompleteResults()
            {
                double[,] numArray = new double[3, 3];
                numArray[0, 0] = this.MomentsOfInertia[0];
                numArray[1, 1] = this.MomentsOfInertia[1];
                numArray[2, 2] = this.MomentsOfInertia[2];
                numArray[0, 1] = -this.ProductsOfInertia[2];
                numArray[0, 2] = -this.ProductsOfInertia[1];
                numArray[1, 2] = -this.ProductsOfInertia[0];
                double mass = this.Mass;
                Vector u = (Vector) (this.Centroid - Snap.Position.Origin);
                double num2 = Vector.Norm2(u);
                double[,] a = new double[3, 3];
                a[0, 0] = numArray[0, 0] - (mass * (num2 - (u.X * u.X)));
                a[1, 1] = numArray[1, 1] - (mass * (num2 - (u.Y * u.Y)));
                a[2, 2] = numArray[2, 2] - (mass * (num2 - (u.Z * u.Z)));
                a[0, 1] = numArray[0, 1] + ((mass * u.X) * u.Y);
                a[1, 0] = a[0, 1];
                a[0, 2] = numArray[0, 2] + ((mass * u.X) * u.Z);
                a[2, 0] = a[0, 2];
                a[1, 2] = numArray[1, 2] + ((mass * u.Y) * u.Z);
                a[2, 1] = a[1, 2];
                Snap.Math.LinearAlgebra.EigenSystemResult[] resultArray = Snap.Math.LinearAlgebra.EigenSystem(a);
                this.PrincipalMoments = new double[] { resultArray[0].Eigenvalue, resultArray[1].Eigenvalue, resultArray[2].Eigenvalue };
                this.PrincipalAxes = new Vector[] { resultArray[0].Eigenvector, resultArray[1].Eigenvector, resultArray[2].Eigenvector };
                for (int i = 0; i < 3; i++)
                {
                    this.PrincipalAxes[i] = (Vector) (-1 * this.PrincipalAxes[i]);
                }
                this.InertiaTensor = a;
            }

            public double Area { get; internal set; }

            public Snap.Position Centroid { get; internal set; }

            public double Density { get; internal set; }

            public double[,] InertiaTensor { get; internal set; }

            public double Mass { get; internal set; }

            internal double[] MomentsOfInertia { get; set; }

            public Vector[] PrincipalAxes { get; internal set; }

            public double[] PrincipalMoments { get; internal set; }

            internal double[] ProductsOfInertia { get; set; }

            public double RadiusOfGyration { get; internal set; }

            public double Volume { get; internal set; }
        }

        public class MinimumRadiusResult
        {
            public double Length { get; internal set; }

            public Snap.Position Position { get; internal set; }

            public double U { get; internal set; }

            public double V { get; internal set; }
        }
    }
}

