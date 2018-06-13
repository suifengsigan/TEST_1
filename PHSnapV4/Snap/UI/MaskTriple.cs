namespace Snap.UI
{
    using NXOpen;
    using Snap.NX;
    using System;

    internal static class MaskTriple
    {
        internal static NXOpen.Selection.MaskTriple[] BuildFromICurveType(ObjectTypes.Type curveType)
        {
            ObjectTypes.TypeCombo[] combos = new ObjectTypes.TypeCombo[2];
            if (curveType == ObjectTypes.Type.Line)
            {
                combos[0] = new ObjectTypes.TypeCombo(ObjectTypes.Type.Line, (ObjectTypes.SubType) 0);
                combos[1] = new ObjectTypes.TypeCombo(ObjectTypes.Type.Edge, ObjectTypes.SubType.EdgeLine);
            }
            if (curveType == ObjectTypes.Type.Arc)
            {
                combos[0] = new ObjectTypes.TypeCombo(ObjectTypes.Type.Arc, (ObjectTypes.SubType) 0);
                combos[1] = new ObjectTypes.TypeCombo(ObjectTypes.Type.Edge, ObjectTypes.SubType.EdgeArc);
            }
            if (curveType == ObjectTypes.Type.Conic)
            {
                combos[0] = new ObjectTypes.TypeCombo(ObjectTypes.Type.Conic, (ObjectTypes.SubType) 0);
                combos[1] = new ObjectTypes.TypeCombo(ObjectTypes.Type.Edge, ObjectTypes.SubType.EdgeEllipse);
            }
            if (curveType == ObjectTypes.Type.Spline)
            {
                combos[0] = new ObjectTypes.TypeCombo(ObjectTypes.Type.Spline, (ObjectTypes.SubType) 0);
                combos[1] = new ObjectTypes.TypeCombo(ObjectTypes.Type.Edge, ObjectTypes.SubType.EdgeSpline);
            }
            return BuildFromTypeCombos(combos);
        }

        internal static NXOpen.Selection.MaskTriple[] BuildFromICurveTypes(params ObjectTypes.Type[] curveTypes)
        {
            int length = curveTypes.Length;
            NXOpen.Selection.MaskTriple[] tripleArray = new NXOpen.Selection.MaskTriple[2];
            NXOpen.Selection.MaskTriple[] tripleArray2 = new NXOpen.Selection.MaskTriple[2 * length];
            for (int i = 0; i < length; i++)
            {
                tripleArray = BuildFromICurveType(curveTypes[i]);
                tripleArray2[2 * i] = tripleArray[0];
                tripleArray2[(2 * i) + 1] = tripleArray[1];
            }
            return tripleArray2;
        }

        internal static NXOpen.Selection.MaskTriple BuildFromType(ObjectTypes.Type type)
        {
            NXOpen.Selection.MaskTriple triple = new NXOpen.Selection.MaskTriple();
            if (type == ObjectTypes.Type.Body)
            {
                triple.Type = 70;
                triple.Subtype = 0;
                triple.SolidBodySubtype = 0;
                return triple;
            }
            if (type == ObjectTypes.Type.Face)
            {
                triple.Type = 70;
                triple.Subtype = 2;
                triple.SolidBodySubtype = 20;
                return triple;
            }
            if (type == ObjectTypes.Type.Edge)
            {
                triple.Type = 70;
                triple.Subtype = 3;
                triple.SolidBodySubtype = 1;
                return triple;
            }
            triple.Type = (int) type;
            triple.Subtype = 0;
            triple.SolidBodySubtype = 0;
            return triple;
        }

        internal static NXOpen.Selection.MaskTriple BuildFromTypeCombo(ObjectTypes.TypeCombo combo)
        {
            ObjectTypes.Type type = combo.Type;
            ObjectTypes.SubType subType = combo.SubType;
            NXOpen.Selection.MaskTriple triple = BuildFromType(type);
            if (type == ObjectTypes.Type.Body)
            {
                triple.Subtype = 0;
                triple.SolidBodySubtype = 0x24;
                switch (subType)
                {
                    case ObjectTypes.SubType.BodySolid:
                        triple.SolidBodySubtype = 0x24;
                        break;

                    case ObjectTypes.SubType.BodySheet:
                        triple.SolidBodySubtype = 0x23;
                        break;
                }
                return triple;
            }
            if (type == ObjectTypes.Type.Face)
            {
                triple.Subtype = 2;
                triple.SolidBodySubtype = ((int)combo.SubType) % ((int)type);
                return triple;
            }
            if (type == ObjectTypes.Type.Edge)
            {
                triple.Subtype = 3;
                triple.SolidBodySubtype = ((int)combo.SubType % (int)type);
                return triple;
            }
            triple.Type = (int) type;
            triple.Subtype = (int)combo.SubType % (int)type;
            triple.SolidBodySubtype = 0;
            return triple;
        }

        internal static NXOpen.Selection.MaskTriple[] BuildFromTypeCombos(ObjectTypes.TypeCombo[] combos)
        {
            int length = combos.Length;
            NXOpen.Selection.MaskTriple[] tripleArray = new NXOpen.Selection.MaskTriple[length];
            for (int i = 0; i < length; i++)
            {
                tripleArray[i] = BuildFromTypeCombo(combos[i]);
            }
            return tripleArray;
        }
    }
}

