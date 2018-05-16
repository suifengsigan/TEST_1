namespace Snap.NX
{
    using NXOpen;
    using NXOpen.Features;
    using Snap;
    using Snap.Geom;
    using System;

    public class ProjectCurve : Snap.NX.Feature
    {
        internal ProjectCurve(NXOpen.Features.ProjectCurve projectCurve) : base(projectCurve)
        {
            this.NXOpenProjectCurve = projectCurve;
        }

        internal static Snap.NX.ProjectCurve CreateProjectCurve(Snap.NX.Curve[] curves, Snap.NX.Point[] points, Surface.Plane geomPlane)
        {
            NXOpen.Features.ProjectCurveBuilder featureBuilder = Globals.NXOpenWorkPart.Features.CreateProjectCurveBuilder(null);
            featureBuilder.Tolerance = Globals.DistanceTolerance;
            featureBuilder.AngleToProjectionVector.RightHandSide = "0";
            featureBuilder.SectionToProject.DistanceTolerance = Globals.DistanceTolerance;
            featureBuilder.SectionToProject.ChainingTolerance = (Globals.UnitType == Globals.Unit.Millimeter) ? 0.02413 : 0.00095;
            featureBuilder.SectionToProject.SetAllowedEntityTypes(NXOpen.SectionEx.AllowTypes.CurvesAndPoints);
            Snap.NX.Section sectionToProject = featureBuilder.SectionToProject;
            sectionToProject.AddICurve(curves);
            sectionToProject.AddPoints(points);
            Position origin = geomPlane.Origin;
            Vector normal = geomPlane.Normal;
            NXOpen.Plane plane = Globals.NXOpenWorkPart.Planes.CreatePlane((Point3d) origin, (Vector3d) normal, SmartObject.UpdateOption.WithinModeling);
            featureBuilder.PlaneToProjectTo = plane;
            NXOpen.Features.Feature feature = (NXOpen.Features.Feature) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.SectionToProject.CleanMappingData();
            featureBuilder.Destroy();
            return (NXOpen.Features.ProjectCurve) feature;
        }

        internal static Snap.NX.ProjectCurve CreateProjectCurve(Snap.NX.Curve[] curves, Snap.NX.Point[] points, Snap.NX.DatumPlane datumPlane)
        {
            NXOpen.Features.ProjectCurveBuilder featureBuilder = Globals.WorkPart.NXOpenPart.Features.CreateProjectCurveBuilder(null);
            //featureBuilder.CurveFitData.Tolerance = Globals.DistanceTolerance;
            featureBuilder.Tolerance = Globals.DistanceTolerance;
            featureBuilder.AngleToProjectionVector.RightHandSide = "0";
            featureBuilder.SectionToProject.DistanceTolerance = Globals.DistanceTolerance;
            featureBuilder.SectionToProject.ChainingTolerance = (Globals.UnitType == Globals.Unit.Millimeter) ? 0.02413 : 0.00095;
            featureBuilder.SectionToProject.SetAllowedEntityTypes(NXOpen.SectionEx.AllowTypes.CurvesAndPoints);
            Snap.NX.Section sectionToProject = featureBuilder.SectionToProject;
            sectionToProject.AddICurve(curves);
            sectionToProject.AddPoints(points);
            Position origin = datumPlane.Origin;
            Vector normal = datumPlane.Normal;
            NXOpen.Plane plane = Globals.NXOpenWorkPart.Planes.CreatePlane((Point3d) origin, (Vector3d) normal, SmartObject.UpdateOption.WithinModeling);
            featureBuilder.PlaneToProjectTo = plane;
            NXOpen.Features.Feature feature = (NXOpen.Features.Feature) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.SectionToProject.CleanMappingData();
            featureBuilder.Destroy();
            return (NXOpen.Features.ProjectCurve) feature;
        }

        internal static Snap.NX.ProjectCurve CreateProjectCurve(Snap.NX.Curve[] curves, Snap.NX.Point[] points, Snap.NX.Face face)
        {
            NXOpen.Part workPart = (NXOpen.Part) Globals.WorkPart;
            NXOpen.Features.ProjectCurveBuilder featureBuilder = workPart.Features.CreateProjectCurveBuilder(null);
            //featureBuilder.CurveFitData.Tolerance = Globals.DistanceTolerance;
            featureBuilder.Tolerance = Globals.DistanceTolerance;
            featureBuilder.AngleToProjectionVector.RightHandSide = "0";
            featureBuilder.SectionToProject.DistanceTolerance = Globals.DistanceTolerance;
            featureBuilder.SectionToProject.ChainingTolerance = (Globals.UnitType == Globals.Unit.Millimeter) ? 0.02413 : 0.00095;
            featureBuilder.SectionToProject.SetAllowedEntityTypes(NXOpen.SectionEx.AllowTypes.CurvesAndPoints);
            featureBuilder.ProjectionDirectionMethod = NXOpen.Features.ProjectCurveBuilder.DirectionType.AlongFaceNormal;
            Snap.NX.Section sectionToProject = featureBuilder.SectionToProject;
            sectionToProject.AddICurve(curves);
            sectionToProject.AddPoints(points);
            ScCollector collector = workPart.ScCollectors.CreateCollector();
            NXOpen.Face[] faces = new NXOpen.Face[] { face };
            FaceDumbRule rule = workPart.ScRuleFactory.CreateRuleFaceDumb(faces);
            SelectionIntentRule[] rules = new SelectionIntentRule[] { rule };
            collector.ReplaceRules(rules, false);
            featureBuilder.FaceToProjectTo.Add(collector);
            NXOpen.Features.Feature feature = (NXOpen.Features.Feature) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.SectionToProject.CleanMappingData();
            featureBuilder.Destroy();
            return (NXOpen.Features.ProjectCurve) feature;
        }

        internal static Snap.NX.ProjectCurve CreateProjectCurve2(Snap.NX.DatumPlane datumPlane, Snap.NX.Curve[] curves, Snap.NX.Point[] points)
        {
            NXOpen.Part work = Globals.Session.Parts.Work;
            NXOpen.Features.ProjectCurveBuilder featureBuilder = work.Features.CreateProjectCurveBuilder(null);
            //featureBuilder.CurveFitData.Tolerance = Globals.DistanceTolerance;
            featureBuilder.Tolerance = Globals.DistanceTolerance;
            featureBuilder.AngleToProjectionVector.RightHandSide = "0";
            featureBuilder.SectionToProject.DistanceTolerance = Globals.DistanceTolerance;
            featureBuilder.SectionToProject.ChainingTolerance = (Globals.UnitType == Globals.Unit.Millimeter) ? 0.02413 : 0.00095;
            featureBuilder.SectionToProject.SetAllowedEntityTypes(NXOpen.SectionEx.AllowTypes.CurvesAndPoints);
            if (curves != null)
            {
                Snap.NX.Section sectionToProject = featureBuilder.SectionToProject;
                for (int i = 0; i < curves.Length; i++)
                {
                    sectionToProject.AddICurve(new Snap.NX.ICurve[] { curves[i] });
                }
            }
            if (points != null)
            {
                NXOpen.Point[] pointArray = new NXOpen.Point[points.Length];
                for (int j = 0; j < pointArray.Length; j++)
                {
                    pointArray[j] = (NXOpen.Point) points[j];
                }
                CurveDumbRule rule = work.ScRuleFactory.CreateRuleCurveDumbFromPoints(pointArray);
                featureBuilder.SectionToProject.AllowSelfIntersection(true);
                SelectionIntentRule[] rules = new SelectionIntentRule[] { rule };
                Point3d helpPoint = new Point3d(0.0, 0.0, 0.0);
                featureBuilder.SectionToProject.AddToSection(rules, null, null, null, helpPoint, NXOpen.Section.Mode.Create, false);
            }
            Position origin = datumPlane.Origin;
            Vector normal = datumPlane.Normal;
            NXOpen.Plane plane = Globals.NXOpenWorkPart.Planes.CreatePlane((Point3d) origin, (Vector3d) normal, SmartObject.UpdateOption.WithinModeling);
            featureBuilder.PlaneToProjectTo = plane;
            NXOpen.Features.Feature feature = (NXOpen.Features.Feature) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.SectionToProject.CleanMappingData();
            featureBuilder.Destroy();
            return (NXOpen.Features.ProjectCurve) feature;
        }

        internal static Snap.NX.ProjectCurve CreateProjectCurve2(Snap.NX.Curve[] curves, Snap.NX.Point[] points, Snap.NX.Face face)
        {
            NXOpen.Part work = Globals.Session.Parts.Work;
            NXOpen.Features.ProjectCurveBuilder featureBuilder = work.Features.CreateProjectCurveBuilder(null);
            featureBuilder.Tolerance = Globals.DistanceTolerance;
            featureBuilder.AngleToProjectionVector.RightHandSide = "0";
            featureBuilder.SectionToProject.DistanceTolerance = Globals.DistanceTolerance;
            featureBuilder.SectionToProject.ChainingTolerance = (Globals.UnitType == Globals.Unit.Millimeter) ? 0.02413 : 0.00095;
            featureBuilder.SectionToProject.SetAllowedEntityTypes(NXOpen.SectionEx.AllowTypes.CurvesAndPoints);
            if (curves != null)
            {
                Snap.NX.Section sectionToProject = featureBuilder.SectionToProject;
                for (int i = 0; i < curves.Length; i++)
                {
                    sectionToProject.AddICurve(new Snap.NX.ICurve[] { curves[i] });
                }
            }
            if (points != null)
            {
                NXOpen.Point[] pointArray = new NXOpen.Point[points.Length];
                for (int j = 0; j < pointArray.Length; j++)
                {
                    pointArray[j] = (NXOpen.Point) points[j];
                }
                CurveDumbRule rule = work.ScRuleFactory.CreateRuleCurveDumbFromPoints(pointArray);
                featureBuilder.SectionToProject.AllowSelfIntersection(true);
                SelectionIntentRule[] ruleArray = new SelectionIntentRule[] { rule };
                Point3d helpPoint = new Point3d(0.0, 0.0, 0.0);
                featureBuilder.SectionToProject.AddToSection(ruleArray, null, null, null, helpPoint, NXOpen.Section.Mode.Create, false);
            }
            ScCollector collector = work.ScCollectors.CreateCollector();
            NXOpen.Face[] faces = new NXOpen.Face[] { face };
            FaceDumbRule rule2 = work.ScRuleFactory.CreateRuleFaceDumb(faces);
            SelectionIntentRule[] rules = new SelectionIntentRule[] { rule2 };
            collector.ReplaceRules(rules, false);
            featureBuilder.FaceToProjectTo.Add(collector);
            NXOpen.Features.Feature feature = (NXOpen.Features.Feature) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.SectionToProject.CleanMappingData();
            featureBuilder.Destroy();
            return (NXOpen.Features.ProjectCurve) feature;
        }

        public static implicit operator Snap.NX.ProjectCurve(NXOpen.Features.ProjectCurve projectCurve)
        {
            return new Snap.NX.ProjectCurve(projectCurve);
        }

        public static implicit operator NXOpen.Features.ProjectCurve(Snap.NX.ProjectCurve projectCurve)
        {
            return (NXOpen.Features.ProjectCurve) projectCurve.NXOpenTaggedObject;
        }

        public static Snap.NX.ProjectCurve Wrap(Tag nxopenProjectCurveTag)
        {
            if (nxopenProjectCurveTag == Tag.Null)
            {
                throw new ArgumentException("Input tag is NXOpen.Tag.Null");
            }
            NXOpen.Features.ProjectCurve objectFromTag = Snap.NX.NXObject.GetObjectFromTag(nxopenProjectCurveTag) as NXOpen.Features.ProjectCurve;
            if (objectFromTag == null)
            {
                throw new ArgumentException("Input tag doesn't belong to an NXOpen.Features.ProjectCurve object");
            }
            return objectFromTag;
        }

        public Snap.NX.Curve[] Curves
        {
            get
            {
                NXOpen.NXObject[] entities = this.NXOpenProjectCurve.GetEntities();
                Snap.NX.Curve[] curveArray = new Snap.NX.Curve[entities.Length];
                int index = 0;
                for (int i = 0; i < entities.Length; i++)
                {
                    if (entities[i] is NXOpen.Curve)
                    {
                        curveArray[index] = (NXOpen.Curve) entities[i];
                        index++;
                    }
                }
                if (index == 0)
                {
                    return null;
                }
                Snap.NX.Curve[] curveArray2 = new Snap.NX.Curve[index];
                for (int j = 0; j < curveArray2.Length; j++)
                {
                    curveArray2[j] = curveArray[j];
                }
                return curveArray2;
            }
        }

        public NXOpen.Features.ProjectCurve NXOpenProjectCurve
        {
            get
            {
                return (NXOpen.Features.ProjectCurve) base.NXOpenTaggedObject;
            }
            private set
            {
                base.NXOpenTaggedObject = value;
            }
        }

        public Snap.NX.Point[] Points
        {
            get
            {
                NXOpen.NXObject[] entities = this.NXOpenProjectCurve.GetEntities();
                Snap.NX.Point[] pointArray = new Snap.NX.Point[entities.Length];
                int index = 0;
                for (int i = 0; i < entities.Length; i++)
                {
                    if (entities[i] is NXOpen.Point)
                    {
                        pointArray[index] = (NXOpen.Point) entities[i];
                        index++;
                    }
                }
                if (index == 0)
                {
                    return null;
                }
                Snap.NX.Point[] pointArray2 = new Snap.NX.Point[index];
                for (int j = 0; j < pointArray2.Length; j++)
                {
                    pointArray2[j] = pointArray[j];
                }
                return pointArray2;
            }
        }

        public NXOpen.Features.ProjectCurveBuilder ProjectCurveBuilder
        {
            get
            {
                NXOpen.Part owningPart = (NXOpen.Part) this.NXOpenProjectCurve.OwningPart;
                return owningPart.Features.CreateProjectCurveBuilder(this.NXOpenProjectCurve);
            }
        }
    }
}

