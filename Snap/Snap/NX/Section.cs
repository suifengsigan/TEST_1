namespace Snap.NX
{
    using NXOpen;
    using Snap;
    using System;
    using System.Collections.Generic;

    internal class Section : Snap.NX.NXObject
    {
        private Section(NXOpen.Section section) : base(section)
        {
            this.NXOpenSection = section;
        }

        internal void AddICurve(params Snap.NX.ICurve[] icurves)
        {
            if (icurves != null)
            {
                Snap.NX.Part workPart = Globals.WorkPart;
                NXOpen.Section nXOpenSection = this.NXOpenSection;
                nXOpenSection.AllowSelfIntersection(false);
                for (int i = 0; i < icurves.Length; i++)
                {
                    SelectionIntentRule[] rules = CreateSelectionIntentRule(new Snap.NX.ICurve[] { icurves[i] });
                    nXOpenSection.AddToSection(rules, (NXOpen.NXObject) icurves[i].NXOpenTaggedObject, null, null, (Point3d) Position.Origin, NXOpen.Section.Mode.Create, false);
                }
            }
        }

        internal void AddPoints(params Snap.NX.Point[] points)
        {
            if (points != null)
            {
                Snap.NX.Part workPart = Globals.WorkPart;
                NXOpen.Section nXOpenSection = this.NXOpenSection;
                nXOpenSection.AllowSelfIntersection(false);
                SelectionIntentRule[] rules = CreateSelectionIntentRule(points);
                nXOpenSection.AddToSection(rules, (NXOpen.NXObject) points[0].NXOpenTaggedObject, null, null, (Point3d) Position.Origin, NXOpen.Section.Mode.Create, false);
            }
        }

        internal static Snap.NX.Section CreateSection(params Snap.NX.ICurve[] icurves)
        {
            NXOpen.Section section = Globals.WorkPart.NXOpenPart.Sections.CreateSection(0.02413, Globals.DistanceTolerance, Globals.AngleTolerance);
            section.AllowSelfIntersection(false);
            section.SetAllowedEntityTypes(NXOpen.Section.AllowTypes.CurvesAndPoints);
            for (int i = 0; i < icurves.Length; i++)
            {
                SelectionIntentRule[] rules = CreateSelectionIntentRule(new Snap.NX.ICurve[] { icurves[i] });
                section.AddToSection(rules, (NXOpen.NXObject) icurves[i].NXOpenTaggedObject, null, null, (Point3d) Position.Origin, NXOpen.Section.Mode.Create);
            }
            return section;
        }

        internal static Snap.NX.Section CreateSection(params Snap.NX.Point[] points)
        {
            NXOpen.Section section = Globals.WorkPart.NXOpenPart.Sections.CreateSection(0.02413, Globals.DistanceTolerance, Globals.AngleTolerance);
            section.AllowSelfIntersection(false);
            section.SetAllowedEntityTypes(NXOpen.Section.AllowTypes.CurvesAndPoints);
            SelectionIntentRule[] rules = CreateSelectionIntentRule(points);
            section.AddToSection(rules, (NXOpen.NXObject) points[0].NXOpenTaggedObject, null, null, (Point3d) Position.Origin, NXOpen.Section.Mode.Create);
            return section;
        }

        internal static SelectionIntentRule[] CreateSelectionIntentRule(params Snap.NX.ICurve[] icurves)
        {
            List<SelectionIntentRule> list = new List<SelectionIntentRule>();
            for (int i = 0; i < icurves.Length; i++)
            {
                if (icurves[i] is Snap.NX.Curve)
                {
                    NXOpen.Curve[] curves = new NXOpen.Curve[] { icurves[i] as Snap.NX.Curve };
                    CurveDumbRule rule = Globals.NXOpenWorkPart.ScRuleFactory.CreateRuleCurveDumb(curves);
                    SelectionIntentRule[] ruleArray = new SelectionIntentRule[] { rule };
                    for (int j = 0; j < ruleArray.Length; j++)
                    {
                        list.Add(ruleArray[j]);
                    }
                }
                else
                {
                    NXOpen.Edge[] edges = new NXOpen.Edge[] { icurves[i] as Snap.NX.Edge };
                    EdgeDumbRule rule2 = Globals.NXOpenWorkPart.ScRuleFactory.CreateRuleEdgeDumb(edges);
                    SelectionIntentRule[] ruleArray2 = new SelectionIntentRule[] { rule2 };
                    for (int k = 0; k < ruleArray2.Length; k++)
                    {
                        list.Add(ruleArray2[k]);
                    }
                }
            }
            return list.ToArray();
        }

        internal static SelectionIntentRule[] CreateSelectionIntentRule(params Snap.NX.Point[] points)
        {
            if (points == null)
            {
                return null;
            }
            NXOpen.Point[] pointArray = new NXOpen.Point[points.Length];
            for (int i = 0; i < pointArray.Length; i++)
            {
                pointArray[i] = (NXOpen.Point) points[i];
            }
            CurveDumbRule rule = Globals.NXOpenWorkPart.ScRuleFactory.CreateRuleCurveDumbFromPoints(pointArray);
            return new SelectionIntentRule[] { rule };
        }

        internal Snap.NX.ICurve[] GetIcurves()
        {
            SectionData[] dataArray;
            List<Snap.NX.ICurve> list = new List<Snap.NX.ICurve>();
            this.NXOpenSection.GetSectionData(out dataArray);
            for (int i = 0; i < dataArray.Length; i++)
            {
                SelectionIntentRule[] ruleArray;
                dataArray[i].GetRules(out ruleArray);
                for (int j = 0; j < ruleArray.Length; j++)
                {
                    if (ruleArray[j].Type == SelectionIntentRule.RuleType.CurveDumb)
                    {
                        NXOpen.Curve[] curveArray;
                        ((CurveDumbRule) ruleArray[j]).GetData(out curveArray);
                        foreach (NXOpen.Curve curve in curveArray)
                        {
                            list.Add((Snap.NX.ICurve) curve);
                        }
                    }
                    else if (ruleArray[j].Type == SelectionIntentRule.RuleType.EdgeDumb)
                    {
                        NXOpen.Edge[] edgeArray;
                        ((EdgeDumbRule) ruleArray[j]).GetData(out edgeArray);
                        foreach (NXOpen.Edge edge in edgeArray)
                        {
                            list.Add((Snap.NX.ICurve) edge);
                        }
                    }
                }
            }
            return list.ToArray();
        }

        public static implicit operator Snap.NX.Section(NXOpen.Section section)
        {
            return new Snap.NX.Section(section);
        }

        public static implicit operator Snap.NX.Section(Snap.NX.Curve curve)
        {
            return CreateSection(new Snap.NX.ICurve[] { curve });
        }

        public static implicit operator Snap.NX.Section(Snap.NX.Edge edge)
        {
            return CreateSection(new Snap.NX.ICurve[] { edge });
        }

        public static implicit operator NXOpen.Section(Snap.NX.Section section)
        {
            return section.NXOpenSection;
        }

        public static implicit operator Snap.NX.Section(Snap.NX.Curve[] curves)
        {
            return CreateSection(curves);
        }

        public static implicit operator Snap.NX.Section(Snap.NX.Edge[] edges)
        {
            return CreateSection(edges);
        }

        internal NXOpen.Section NXOpenSection
        {
            get
            {
                return (NXOpen.Section) base.NXOpenTaggedObject;
            }
            private set
            {
                base.NXOpenTaggedObject = value;
            }
        }
    }
}

