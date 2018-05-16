namespace Snap.NX
{
    using System;
    using System.Runtime.InteropServices;

    public static class ObjectTypes
    {
        [Obsolete("Deprecated in NX8.5, please use Snap.UI.Selection.Dialog.SetCurveFilter() to select the curve.")]
        public static Type[] AllCurves
        {
            get
            {
                return AllCurvesPrivate;
            }
        }

        internal static Type[] AllCurvesPrivate
        {
            get
            {
                return new Type[] { Type.Line, Type.Arc, Type.Conic, Type.Spline };
            }
        }

        public enum SubType
        {
            BodyGeneral = 0x1b58,
            BodySheet = 0x1b7b,
            BodySolid = 0x1b7c,
            ComponentGeneral = 0x189c,
            ConicEllipse = 0x25a,
            ConicHyperbola = 0x25c,
            ConicParabola = 0x25b,
            CsysCylindrical = 0x1196,
            CsysGeneral = 0x1194,
            CsysSpherical = 0x1197,
            CsysWcs = 0x1195,
            DimensionAngularMajor = 0xa2f,
            DimensionAngularMinor = 0xa2e,
            DimensionArclength = 0xa30,
            DimensionAssortedParts = 0xa37,
            DimensionConcCircle = 0xa34,
            DimensionCylindrical = 0xa2c,
            DimensionDiameter = 0xa32,
            DimensionHole = 0xa33,
            DimensionHorizontal = 0xa29,
            DimensionOrdinateHoriz = 0xa35,
            DimensionOrdinateVert = 0xa36,
            DimensionParallel = 0xa2b,
            DimensionPerpendicular = 0xa2d,
            DimensionRadius = 0xa31,
            DimensionVertical = 0xa2a,
            DraftingEntityAssortedParts = 0x9cb,
            DraftingEntityCenterline = 0x9c9,
            DraftingEntityCrosshatch = 0x9ca,
            DraftingEntityFpt = 0x9c8,
            DraftingEntityIdSymbol = 0x9c7,
            DraftingEntityLabel = 0x9c6,
            DraftingEntityNote = 0x9c5,
            EdgeArc = 0x1876b,
            EdgeCircle = 0x1876b,
            EdgeEllipse = 0x1876c,
            EdgeIntersection = 0x1876d,
            EdgeIsoCurve = 0x18771,
            EdgeLine = 0x1876a,
            EdgeSpCurve = 0x1876f,
            EdgeSpline = 0x1876e,
            EdgeUnknown = 0x18772,
            FaceBlend = 0x1871f,
            FaceBsurface = 0x1871e,
            FaceCone = 0x1871b,
            FaceCylinder = 0x1871a,
            FaceExtruded = 0x18721,
            FaceOffset = 0x18720,
            FacePlane = 0x18719,
            FaceRevolved = 0x18722,
            FaceSphere = 0x1871c,
            FaceTorus = 0x1871d,
            FaceUnknown = 0x18723,
            LayoutCanned = 0x17d5,
            LayoutGeneral = 0x17d4,
            LineGeneral = 0x12d,
            LineInfinite = 0x12e,
            PartAttributeCache = 0x44d,
            PartAttributeGeneral = 0x44c,
            PartOccurrenceGeneral = 0x189d,
            PartOccurrenceShadow = 0x189e,
            PatternGeneral = 0x3e8,
            PatternPoint = 0x3e9,
            ReferenceDesign = 0x1900,
            ViewAuxiliary = 0x1775,
            ViewBaseMember = 0x1773,
            ViewDetail = 0x1776,
            ViewImported = 0x1772,
            ViewInstance = 0x1771,
            ViewModeling = 0x1777,
            ViewOrthographic = 0x1774,
            ViewSection = 0x1770
        }

        public enum Type
        {
            Arc = 5,
            Body = 70,
            Camera = 0x3b,
            Circle = 5,
            Component = 0x3f,
            Conic = 6,
            Constraint = 160,
            CoordinateSystem = 0x2d,
            DatumAxis = 0x4ac,
            DatumPlane = 0x4ad,
            Dimension = 0x1a,
            Direction = 0xd9,
            DraftingEntity = 0x19,
            Drawing = 0x3e,
            Edge = 0x3ea,
            Face = 0x3e9,
            FacettedModel = 0x8b,
            Feature = 0xcd,
            Group = 15,
            LayerCategory = 12,
            Layout = 0x3d,
            LightSource = 0xb6,
            Line = 3,
            Matrix = 0x37,
            ParameterObject = 0x35,
            PartAttribute = 11,
            Pattern = 10,
            Plane = 0x2e,
            Point = 2,
            ReferenceSet = 0x40,
            Scalar = 0xd7,
            Skeleton = 0x34,
            Spline = 9,
            TraceLine = 0xa4,
            View = 60
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct TypeCombo
        {
            public Snap.NX.ObjectTypes.Type Type;
            public Snap.NX.ObjectTypes.SubType SubType;
            public TypeCombo(Snap.NX.ObjectTypes.Type type, Snap.NX.ObjectTypes.SubType subtype)
            {
                this.Type = type;
                this.SubType = subtype;
            }

            public TypeCombo(Snap.NX.ObjectTypes.Type type)
            {
                this.Type = type;
                this.SubType = (Snap.NX.ObjectTypes.SubType) 0;
            }
        }
    }
}

