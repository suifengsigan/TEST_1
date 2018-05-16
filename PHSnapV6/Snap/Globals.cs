namespace Snap
{
    using NXOpen;
    using NXOpen.Layer;
    using NXOpen.Preferences;
    using NXOpen.UF;
    using Snap.NX;
    using System;
    using System.Drawing;
    using System.Reflection;

    public static class Globals
    {
        private static System.Drawing.Color ColorOfAllButDefault;
        private static bool StateOfAllButDefault = false;

        private static PartObject.LineFontType ConvertLineFontType(DisplayableObject.ObjectFont lineFontType)
        {
            if (lineFontType == DisplayableObject.ObjectFont.Centerline)
            {
                return PartObject.LineFontType.Centerline;
            }
            if (lineFontType == DisplayableObject.ObjectFont.Dashed)
            {
                return PartObject.LineFontType.Dashed;
            }
            if (lineFontType == DisplayableObject.ObjectFont.Dotted)
            {
                return PartObject.LineFontType.Dotted;
            }
            if (lineFontType == DisplayableObject.ObjectFont.DottedDashed)
            {
                return PartObject.LineFontType.DottedDashed;
            }
            if (lineFontType == DisplayableObject.ObjectFont.LongDashed)
            {
                return PartObject.LineFontType.LongDashed;
            }
            if (lineFontType == DisplayableObject.ObjectFont.Phantom)
            {
                return PartObject.LineFontType.Phantom;
            }
            return PartObject.LineFontType.Solid;
        }

        private static DisplayableObject.ObjectFont ConvertLineFontType(PartObject.LineFontType lineFontType)
        {
            if (lineFontType == PartObject.LineFontType.Centerline)
            {
                return DisplayableObject.ObjectFont.Centerline;
            }
            if (lineFontType == PartObject.LineFontType.Dashed)
            {
                return DisplayableObject.ObjectFont.Dashed;
            }
            if (lineFontType == PartObject.LineFontType.Dotted)
            {
                return DisplayableObject.ObjectFont.Dotted;
            }
            if (lineFontType == PartObject.LineFontType.DottedDashed)
            {
                return DisplayableObject.ObjectFont.DottedDashed;
            }
            if (lineFontType == PartObject.LineFontType.LongDashed)
            {
                return DisplayableObject.ObjectFont.LongDashed;
            }
            if (lineFontType == PartObject.LineFontType.Phantom)
            {
                return DisplayableObject.ObjectFont.Phantom;
            }
            return DisplayableObject.ObjectFont.Solid;
        }

        private static PartObject.WidthType ConvertLineWidthType(DisplayableObject.ObjectWidth widthType)
        {
            if (widthType == DisplayableObject.ObjectWidth.Normal)
            {
                return PartObject.WidthType.NormalWidth;
            }
            if (widthType == DisplayableObject.ObjectWidth.Thick)
            {
                return PartObject.WidthType.ThickWidth;
            }
            if (widthType == DisplayableObject.ObjectWidth.Thin)
            {
                return PartObject.WidthType.ThinWidth;
            }
            //if (widthType == DisplayableObject.ObjectWidth.One)
            //{
            //    return PartObject.WidthType.WidthOne;
            //}
            //if (widthType == DisplayableObject.ObjectWidth.Two)
            //{
            //    return PartObject.WidthType.WidthTwo;
            //}
            //if (widthType == DisplayableObject.ObjectWidth.Three)
            //{
            //    return PartObject.WidthType.WidthThree;
            //}
            //if (widthType == DisplayableObject.ObjectWidth.Four)
            //{
            //    return PartObject.WidthType.WidthFour;
            //}
            //if (widthType == DisplayableObject.ObjectWidth.Five)
            //{
            //    return PartObject.WidthType.WidthFive;
            //}
            //if (widthType == DisplayableObject.ObjectWidth.Six)
            //{
            //    return PartObject.WidthType.WidthSix;
            //}
            //if (widthType == DisplayableObject.ObjectWidth.Seven)
            //{
            //    return PartObject.WidthType.WidthSeven;
            //}
            //if (widthType == DisplayableObject.ObjectWidth.Eight)
            //{
            //    return PartObject.WidthType.WidthEight;
            //}
            return PartObject.WidthType.PartDefault;
        }

        private static DisplayableObject.ObjectWidth ConvertLineWidthType(PartObject.WidthType widthType)
        {
            if (widthType == PartObject.WidthType.NormalWidth)
            {
                return DisplayableObject.ObjectWidth.Normal;
            }
            if (widthType == PartObject.WidthType.ThickWidth)
            {
                return DisplayableObject.ObjectWidth.Thick;
            }
            if (widthType == PartObject.WidthType.ThinWidth)
            {
                return DisplayableObject.ObjectWidth.Thin;
            }
            //if (widthType == PartObject.WidthType.WidthOne)
            //{
            //    return DisplayableObject.ObjectWidth.One;
            //}
            //if (widthType == PartObject.WidthType.WidthTwo)
            //{
            //    return DisplayableObject.ObjectWidth.Two;
            //}
            //if (widthType == PartObject.WidthType.WidthThree)
            //{
            //    return DisplayableObject.ObjectWidth.Three;
            //}
            //if (widthType == PartObject.WidthType.WidthFour)
            //{
            //    return DisplayableObject.ObjectWidth.Four;
            //}
            //if (widthType == PartObject.WidthType.WidthFive)
            //{
            //    return DisplayableObject.ObjectWidth.Five;
            //}
            //if (widthType == PartObject.WidthType.WidthSix)
            //{
            //    return DisplayableObject.ObjectWidth.Six;
            //}
            //if (widthType == PartObject.WidthType.WidthSeven)
            //{
            //    return DisplayableObject.ObjectWidth.Seven;
            //}
            //if (widthType == PartObject.WidthType.WidthEight)
            //{
            //    return DisplayableObject.ObjectWidth.Eight;
            //}
            return DisplayableObject.ObjectWidth.Thin;
        }

        public static void DeleteUndoMark(UndoMarkId markId, string markName)
        {
            Session.DeleteUndoMark((NXOpen.Session.UndoMarkId) markId, markName);
        }

        public static System.Drawing.Color GetColor(DisplayType type)
        {
            NXOpen.Part workPart = (NXOpen.Part) WorkPart;
            PartObject.ObjectType type2 = (PartObject.ObjectType) type;
            int color = workPart.Preferences.ObjectPreferences.GetColor(type2);
            if (color > 0xff)
            {
                return Color;
            }
            if (color == 0)
            {
                return ColorOfAllButDefault;
            }
            return Snap.Color.WindowsColor(color);
        }

        public static Font GetLineFont(DisplayType type)
        {
            NXOpen.Part workPart = (NXOpen.Part) WorkPart;
            PartObject.ObjectType type2 = (PartObject.ObjectType) type;
            return (Font) ConvertLineFontType(workPart.Preferences.ObjectPreferences.GetLineFont(type2));
        }

        public static Width GetLineWidth(DisplayType type)
        {
            NXOpen.Part workPart = (NXOpen.Part) WorkPart;
            PartObject.ObjectType type2 = (PartObject.ObjectType) type;
            return (Width) ConvertLineWidthType(workPart.Preferences.ObjectPreferences.GetWidth(type2));
        }

        public static int LayerObjectCount(int layer)
        {
            return WorkPart.NXOpenPart.Layers.GetAllObjectsOnLayer(layer).Length;
        }

        public static void SetColor(DisplayType type, System.Drawing.Color color)
        {
            int num = Snap.Color.ColorIndex(color);
            NXOpen.Part workPart = (NXOpen.Part) WorkPart;
            PartObject.ObjectType type2 = (PartObject.ObjectType) type;
            workPart.Preferences.ObjectPreferences.SetColor(type2, num);
            if (type == DisplayType.AllButDefault)
            {
                ColorOfAllButDefault = color;
                StateOfAllButDefault = true;
            }
        }

        public static void SetLineFont(DisplayType type, Font lineFontType)
        {
            PartObject.LineFontType lineFont = ConvertLineFontType((DisplayableObject.ObjectFont) lineFontType);
            NXOpen.Part workPart = (NXOpen.Part) WorkPart;
            PartObject.ObjectType type3 = (PartObject.ObjectType) type;
            workPart.Preferences.ObjectPreferences.SetLineFont(type3, lineFont);
        }

        public static void SetLineWidth(DisplayType type, Width widthType)
        {
            PartObject.WidthType width = ConvertLineWidthType((DisplayableObject.ObjectWidth) widthType);
            NXOpen.Part workPart = (NXOpen.Part) WorkPart;
            PartObject.ObjectType type3 = (PartObject.ObjectType) type;
            workPart.Preferences.ObjectPreferences.SetWidth(type3, width);
        }

        public static UndoMarkId SetUndoMark(MarkVisibility markVisibility, string name)
        {
            return (UndoMarkId) Session.SetUndoMark((NXOpen.Session.MarkVisibility) markVisibility, name);
        }

        public static void UndoToMark(UndoMarkId markId, string markName)
        {
            Session.UndoToMark((NXOpen.Session.UndoMarkId) markId, markName);
        }

        public static double AngleTolerance
        {
            get
            {
                return NXOpenWorkPart.Preferences.Modeling.AngleToleranceData;
            }
            set
            {
                NXOpenWorkPart.Preferences.Modeling.AngleToleranceData = value;
            }
        }

        public static System.Drawing.Color Color
        {
            get
            {
                return Snap.Color.WindowsColor(WorkPart.NXOpenPart.Preferences.ObjectPreferences.GetColor(PartObject.ObjectType.General));
            }
            set
            {
                int color = Snap.Color.ColorIndex(value);
                WorkPart.NXOpenPart.Preferences.ObjectPreferences.SetColor(PartObject.ObjectType.General, color);
                if (!StateOfAllButDefault)
                {
                    ColorOfAllButDefault = value;
                }
            }
        }

        public static Snap.NX.Part DisplayPart
        {
            get
            {
                return Session.Parts.Display;
            }
            set
            {
                PartLoadStatus status;
                Session.Parts.SetDisplay((BasePart) value, true, true, out status);
                status.Dispose();
            }
        }

        public static double DistanceTolerance
        {
            get
            {
                return NXOpenWorkPart.Preferences.Modeling.DistanceToleranceData;
            }
            set
            {
                NXOpenWorkPart.Preferences.Modeling.DistanceToleranceData = value;
            }
        }

        public static bool HistoryMode
        {
            get
            {
                return NXOpenWorkPart.Preferences.Modeling.GetHistoryMode();
            }
            set
            {
                if (value)
                {
                    NXOpenWorkPart.Preferences.Modeling.SetHistoryMode();
                }
                else
                {
                    NXOpenWorkPart.Preferences.Modeling.SetHistoryFreeMode();
                }
            }
        }

        public static double InchesPerUnit
        {
            get
            {
                if (UnitType != Unit.Millimeter)
                {
                    return 1.0;
                }
                return 25.4;
            }
        }

        public static LayerStatesArray LayerStates
        {
            get
            {
                return new LayerStatesArray();
            }
        }

        public static Font LineFont
        {
            get
            {
                return (Font) ConvertLineFontType(NXOpenWorkPart.Preferences.ObjectPreferences.GetLineFont(PartObject.ObjectType.General));
            }
            set
            {
                PartObject.LineFontType lineFont = ConvertLineFontType((DisplayableObject.ObjectFont) value);
                NXOpenWorkPart.Preferences.ObjectPreferences.SetLineFont(PartObject.ObjectType.General, lineFont);
            }
        }

        public static Width LineWidth
        {
            get
            {
                return (Width) ConvertLineWidthType(WorkPart.NXOpenPart.Preferences.ObjectPreferences.GetWidth(PartObject.ObjectType.General));
            }
            set
            {
                PartObject.WidthType width = ConvertLineWidthType((DisplayableObject.ObjectWidth) value);
                WorkPart.NXOpenPart.Preferences.ObjectPreferences.SetWidth(PartObject.ObjectType.General, width);
            }
        }

        public static bool ManagedMode
        {
            get
            {
                bool flag = false;
                UFSession.UF.IsUgmanagerActive(out flag);
                return flag;
            }
        }

        public static double MillimetersPerUnit
        {
            get
            {
                if (UnitType != Unit.Millimeter)
                {
                    return 0.0393700787402;
                }
                return 1.0;
            }
        }

        public static NXOpen.Part NXOpenWorkPart
        {
            get
            {
                return Session.Parts.Work;
            }
        }

        public static Snap.NX.Unit PartUnit
        {
            get
            {
                Snap.NX.Unit millimeter = Snap.NX.Unit.Millimeter;
                if (UnitType == Unit.Inch)
                {
                    millimeter = Snap.NX.Unit.Inch;
                }
                return millimeter;
            }
        }

        internal static NXOpen.Session Session
        {
            get
            {
                return NXOpen.Session.GetSession();
            }
        }

        public static int Translucency
        {
            get
            {
                return NXOpenWorkPart.Preferences.ObjectPreferences.Translucency;
            }
            set
            {
                NXOpenWorkPart.Preferences.ObjectPreferences.Translucency = value;
            }
        }

        internal static NXOpen.UF.UFSession UFSession
        {
            get
            {
                return NXOpen.UF.UFSession.GetUFSession();
            }
        }

        public static Unit UnitType
        {
            get
            {
                Snap.NX.Part workPart = WorkPart;
                int num = 0;
                UFSession.Part.AskUnits(workPart.NXOpenTag, out num);
                if (num == 1)
                {
                    return Unit.Millimeter;
                }
                return Unit.Inch;
            }
        }

        public static Snap.NX.CoordinateSystem Wcs
        {
            get
            {
                Tag tag;
                UFSession.Csys.AskWcs(out tag);
                return new Snap.NX.CoordinateSystem((NXOpen.CoordinateSystem) Snap.NX.NXObject.GetObjectFromTag(tag));
            }
            set
            {
                Tag nXOpenTag = value.NXOpenTag;
                UFSession.Csys.SetWcs(nXOpenTag);
            }
        }

        public static Snap.Orientation WcsOrientation
        {
            get
            {
                return Wcs.Orientation;
            }
            set
            {
                NXOpenWorkPart.WCS.SetOriginAndMatrix((Point3d) Wcs.Origin, (Matrix3x3) value);
            }
        }

        public static int WorkLayer
        {
            get
            {
                return NXOpenWorkPart.Layers.WorkLayer;
            }
            set
            {
                NXOpenWorkPart.Layers.WorkLayer = value;
            }
        }

        public static Snap.NX.Part WorkPart
        {
            get
            {
                return NXOpenWorkPart;
            }
            set
            {
                NXOpen.Part nXOpenWorkPart = NXOpenWorkPart;
                NXOpen.Part displayPart = (NXOpen.Part) DisplayPart;
                if (nXOpenWorkPart == null)
                {
                    PartLoadStatus status;
                    Session.Parts.SetDisplay((BasePart) value, true, true, out status);
                    status.Dispose();
                    Session.Parts.SetWork((BasePart) value);
                }
                else if (nXOpenWorkPart.Tag != value.NXOpenTag)
                {
                    if (displayPart.ComponentAssembly.RootComponent == null)
                    {
                        PartLoadStatus status2;
                        Session.Parts.SetDisplay((BasePart) value, false, false, out status2);
                        status2.Dispose();
                        Session.Parts.SetWork((BasePart) value);
                    }
                    else
                    {
                        Tag[] tagArray;
                        UFSession.Assem.AskOccsOfPart(displayPart.Tag, value.NXOpenTag, out tagArray);
                        if (tagArray.Length == 0)
                        {
                            PartLoadStatus status3;
                            Session.Parts.SetDisplay((BasePart) value, false, false, out status3);
                            status3.Dispose();
                            Session.Parts.SetWork((BasePart) value);
                        }
                        else
                        {
                            Session.Parts.SetWork((BasePart) value);
                        }
                    }
                }
            }
        }

        public enum DisplayType
        {
            General,
            Line,
            Arc,
            Conic,
            Spline,
            Solidbody,
            Sheetbody,
            Datum,
            Point,
            CoordinateSystem,
            AllButDefault,
            DatumCsys,
            Traceline,
            InfiniteLine
        }

        public enum Font
        {
            Centerline = 4,
            Dashed = 2,
            Dotted = 5,
            DottedDashed = 7,
            LongDashed = 6,
            Phantom = 3,
            Solid = 1
        }

        public enum LayerState
        {
            WorkLayer,
            Selectable,
            Visible,
            Hidden
        }

        public class LayerStatesArray
        {
            public Globals.LayerState this[int n]
            {
                get
                {
                    return (Globals.LayerState) Globals.NXOpenWorkPart.Layers.GetState(n);
                }
                set
                {
                    NXOpen.Layer.State state = (NXOpen.Layer.State) value;
                    NXOpen.Layer.StateInfo[] stateArray = new NXOpen.Layer.StateInfo[1];
                    stateArray[0].State = state;
                    stateArray[0].Layer = n;
                    Globals.NXOpenWorkPart.Layers.ChangeStates(stateArray, false);
                }
            }
        }

        public enum MarkVisibility
        {
            Visible,
            Invisible,
            AnyVisibility
        }

        public enum UndoMarkId
        {
        }

        public enum Unit
        {
            Inch = 2,
            Millimeter = 1
        }

        public enum Width
        {
            Normal = 0,
            Thick = 1,
            Thin = 2,
            Width013 = 2,
            Width018 = 0,
            Width025 = 1,
            Width035 = 8,
            Width050 = 9,
            Width070 = 10,
            Width100 = 11,
            Width140 = 12,
            Width200 = 13
        }
    }
}

