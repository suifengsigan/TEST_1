namespace Snap.UI.Block
{
    using NXOpen;
    using NXOpen.BlockStyler;
    using Snap;
    using Snap.NX;
    using System;
    using System.Collections.Generic;

    internal static class PropertyAccess
    {
        internal static void AddProperty(General block, PropertyKey propKey, object propValue)
        {
            block.PropertyDictionary.Add(propKey, propValue);
        }

        internal static void BlockToDictionary(General block, PropertyKey propKey)
        {
            string name = propKey.Name;
            Snap.UI.Block.PropertyType propType = propKey.Type;
            object obj2 = GetNxopenBlockValue(block, propType, name);
            block.PropertyDictionary[propKey] = obj2;
        }

        internal static void DictionaryToBlock(General block, PropertyKey propKey)
        {
            object propValue = block.PropertyDictionary[propKey];
            SetNxopenBlockValue(block, propKey.Type, propKey.Name, propValue);
        }

        internal static object GetNxopenBlockValue(General block, Snap.UI.Block.PropertyType propType, string propName)
        {
            UIBlock nXOpenBlock = block.NXOpenBlock;
            object obj2 = null;
            if (propType == Snap.UI.Block.PropertyType.Double)
            {
                obj2 = NxGetDouble(nXOpenBlock, propName);
            }
            if (propType == Snap.UI.Block.PropertyType.Integer)
            {
                obj2 = NxGetInteger(nXOpenBlock, propName);
            }
            if (propType == Snap.UI.Block.PropertyType.Logical)
            {
                obj2 = NxGetLogical(nXOpenBlock, propName);
            }
            if (propType == Snap.UI.Block.PropertyType.String)
            {
                obj2 = NxGetString(nXOpenBlock, propName);
            }
            if (propType == Snap.UI.Block.PropertyType.Enum)
            {
                obj2 = NxGetEnum(nXOpenBlock, propName);
            }
            if (propType == Snap.UI.Block.PropertyType.EnumAsString)
            {
                obj2 = NxGetEnumAsString(nXOpenBlock, propName);
            }
            if (propType == Snap.UI.Block.PropertyType.EnumMembers)
            {
                obj2 = NxGetEnumMembers(nXOpenBlock, propName);
            }
            if (propType == Snap.UI.Block.PropertyType.DoubleArray)
            {
                obj2 = NxGetDoubleArray(nXOpenBlock, propName);
            }
            if (propType == Snap.UI.Block.PropertyType.IntegerArray)
            {
                obj2 = NxGetIntegerArray(nXOpenBlock, propName);
            }
            if (propType == Snap.UI.Block.PropertyType.DoubleMatrix)
            {
                obj2 = NxGetDoubleMatrix(nXOpenBlock, propName);
            }
            if (propType == Snap.UI.Block.PropertyType.IntegerMatrix)
            {
                obj2 = NxGetIntegerMatrix(nXOpenBlock, propName);
            }
            if (propType == Snap.UI.Block.PropertyType.StringArray)
            {
                obj2 = NxGetStringArray(nXOpenBlock, propName);
            }
            if (propType == Snap.UI.Block.PropertyType.ObjectArray)
            {
                obj2 = NxGetObjectArray(nXOpenBlock, propName);
            }
            if (propType == Snap.UI.Block.PropertyType.Point)
            {
                obj2 = NxGetPoint(nXOpenBlock, propName);
            }
            if (propType == Snap.UI.Block.PropertyType.Vector)
            {
                obj2 = NxGetVector(nXOpenBlock, propName);
            }
            if (propType == Snap.UI.Block.PropertyType.Object)
            {
                obj2 = NxGetObject(nXOpenBlock, propName);
            }
            if (propType == Snap.UI.Block.PropertyType.BitArray)
            {
                obj2 = NxGetBits(nXOpenBlock, propName);
            }
            if (propType == Snap.UI.Block.PropertyType.File)
            {
                obj2 = NxGetFile(nXOpenBlock, propName);
            }
            return obj2;
        }

        internal static object GetPropertyValue(General block, Snap.UI.Block.PropertyType propType, string propName)
        {
            UIBlock nXOpenBlock = block.NXOpenBlock;
            PropertyKey key = new PropertyKey(propType, propName);
            if (nXOpenBlock == null)
            {
                return block.PropertyDictionary[key];
            }
            return GetNxopenBlockValue(block, key.Type, key.Name);
        }

        internal static int NxGetBits(UIBlock block, string propertyName)
        {
            PropertyList properties = block.GetProperties();
            int num2 = SnapPointsStateSet.Chop17(properties.GetBits(propertyName));
            properties.Dispose();
            return num2;
        }

        internal static double NxGetDouble(UIBlock block, string propertyName)
        {
            PropertyList properties = block.GetProperties();
            double num = properties.GetDouble(propertyName);
            properties.Dispose();
            return num;
        }

        internal static double[] NxGetDoubleArray(UIBlock block, string propertyName)
        {
            PropertyList properties = block.GetProperties();
            double[] doubleVector = properties.GetDoubleVector(propertyName);
            properties.Dispose();
            return doubleVector;
        }

        internal static double[,] NxGetDoubleMatrix(UIBlock block, string propertyName)
        {
            int num;
            int num2;
            PropertyList properties = block.GetProperties();
            double[,] numArray2 = Snap.Math.MatrixMath.VectorToMatrix(properties.GetDoubleMatrix(propertyName, out num, out num2), num, num2);
            properties.Dispose();
            return numArray2;
        }

        internal static int NxGetEnum(UIBlock block, string propertyName)
        {
            PropertyList properties = block.GetProperties();
            int num = properties.GetEnum(propertyName);
            properties.Dispose();
            return num;
        }

        internal static string NxGetEnumAsString(UIBlock block, string propertyName)
        {
            PropertyList properties = block.GetProperties();
            string enumAsString = properties.GetEnumAsString(propertyName);
            properties.Dispose();
            return enumAsString;
        }

        internal static string[] NxGetEnumMembers(UIBlock block, string propertyName)
        {
            PropertyList properties = block.GetProperties();
            string[] enumMembers = properties.GetEnumMembers(propertyName);
            properties.Dispose();
            return enumMembers;
        }

        internal static string NxGetFile(UIBlock block, string propertyName)
        {
            PropertyList properties = block.GetProperties();
            string file = properties.GetFile(propertyName);
            properties.Dispose();
            return file;
        }

        internal static int NxGetInteger(UIBlock block, string propertyName)
        {
            PropertyList properties = block.GetProperties();
            int integer = properties.GetInteger(propertyName);
            properties.Dispose();
            return integer;
        }

        internal static int[] NxGetIntegerArray(UIBlock block, string propertyName)
        {
            PropertyList properties = block.GetProperties();
            int[] integerVector = properties.GetIntegerVector(propertyName);
            properties.Dispose();
            return integerVector;
        }

        internal static int[,] NxGetIntegerMatrix(UIBlock block, string propertyName)
        {
            int num;
            int num2;
            PropertyList properties = block.GetProperties();
            int[,] numArray2 = Snap.Math.MatrixMath.VectorToMatrix(properties.GetIntegerMatrix(propertyName, out num, out num2), num, num2);
            properties.Dispose();
            return numArray2;
        }

        internal static bool NxGetLogical(UIBlock block, string propertyName)
        {
            PropertyList properties = block.GetProperties();
            bool logical = properties.GetLogical(propertyName);
            properties.Dispose();
            return logical;
        }

        internal static TaggedObject NxGetObject(UIBlock block, string propertyName)
        {
            PropertyList properties = block.GetProperties();
            TaggedObject taggedObject = properties.GetTaggedObject(propertyName);
            properties.Dispose();
            return taggedObject;
        }

        internal static TaggedObject[] NxGetObjectArray(UIBlock block, string propertyName)
        {
            PropertyList properties = block.GetProperties();
            TaggedObject[] taggedObjectVector = properties.GetTaggedObjectVector(propertyName);
            properties.Dispose();
            return taggedObjectVector;
        }

        internal static Position NxGetPoint(UIBlock block, string propertyName)
        {
            PropertyList properties = block.GetProperties();
            Position point = properties.GetPoint(propertyName);
            properties.Dispose();
            return point;
        }

        internal static string NxGetString(UIBlock block, string propertyName)
        {
            PropertyList properties = block.GetProperties();
            string str = properties.GetString(propertyName);
            properties.Dispose();
            return str;
        }

        internal static string[] NxGetStringArray(UIBlock block, string propertyName)
        {
            PropertyList properties = block.GetProperties();
            string[] strings = properties.GetStrings(propertyName);
            properties.Dispose();
            return strings;
        }

        internal static Vector NxGetVector(UIBlock block, string propertyName)
        {
            PropertyList properties = block.GetProperties();
            Vector vector = properties.GetVector(propertyName);
            properties.Dispose();
            return vector;
        }

        internal static void NxSetBits(UIBlock block, string propertyName, int value)
        {
            PropertyList properties = block.GetProperties();
            int num2 = SnapPointsStateSet.Chop17(properties.GetBits(propertyName));
            int bitsSc = SnapPointsStateSet.Chop17(value);
            if (bitsSc != num2)
            {
                properties.SetBits(propertyName, bitsSc);
            }
            properties.Dispose();
        }

        internal static void NxSetDouble(UIBlock block, string propertyName, double value)
        {
            PropertyList properties = block.GetProperties();
            properties.SetDouble(propertyName, value);
            properties.Dispose();
        }

        internal static void NxSetDoubleArray(UIBlock block, string propertyName, double[] value)
        {
            PropertyList properties = block.GetProperties();
            if (value == null)
            {
                value = new double[0];
            }
            var intValue = new List<int>();
            for (int i = 0; i < value.Length; i++) 
            {
                intValue.Add((int)value[i]);
            }
                properties.SetDoubleVector(propertyName, intValue.ToArray());
            properties.Dispose();
        }

        internal static void NxSetDoubleMatrix(UIBlock block, string propertyName, double[,] value)
        {
            if (value == null)
            {
                string message = "Property named " + propertyName + " is null (Nothing). ";
                throw new ArgumentNullException("value", message);
            }
            int length = value.GetLength(0);
            int num2 = value.GetLength(1);
            if (System.Math.Min(length, num2) == 0)
            {
                throw new ArgumentException("Property named " + propertyName + " is an array with a zero dimension. ", "value");
            }
            double[] matrixValue = Snap.Math.MatrixMath.MatrixToVector(value);
            PropertyList properties = block.GetProperties();
            properties.SetDoubleMatrix(propertyName, length, num2, matrixValue);
            properties.Dispose();
        }

        internal static void NxSetEnum(UIBlock block, string propertyName, int value)
        {
            PropertyList properties = block.GetProperties();
            properties.SetEnum(propertyName, value);
            properties.Dispose();
        }

        internal static void NxSetEnumAsString(UIBlock block, string propertyName, string value)
        {
            PropertyList properties = block.GetProperties();
            properties.SetEnumAsString(propertyName, value);
            properties.Dispose();
        }

        internal static void NxSetEnumMembers(UIBlock block, string propertyName, string[] value)
        {
            PropertyList properties = block.GetProperties();
            properties.SetEnumMembers(propertyName, value);
            properties.Dispose();
        }

        internal static void NxSetFile(UIBlock block, string propertyName, string value)
        {
            PropertyList properties = block.GetProperties();
            properties.SetFile(propertyName, value);
            properties.Dispose();
        }

        internal static void NxSetFilter(UIBlock block, string propertyName, Selection.MaskTriple[] value)
        {
            if (value != null)
            {
                PropertyList properties = block.GetProperties();
                Selection.SelectionAction clearAndEnableSpecific = Selection.SelectionAction.ClearAndEnableSpecific;
                properties.SetSelectionFilter("SelectionFilter", clearAndEnableSpecific, value);
                properties.Dispose();
            }
        }

        internal static void NxSetInteger(UIBlock block, string propertyName, int value)
        {
            PropertyList properties = block.GetProperties();
            properties.SetInteger(propertyName, value);
            properties.Dispose();
        }

        internal static void NxSetIntegerArray(UIBlock block, string propertyName, int[] value)
        {
            PropertyList properties = block.GetProperties();
            if (value == null)
            {
                value = new int[0];
            }
            properties.SetIntegerVector(propertyName, value);
            properties.Dispose();
        }

        internal static void NxSetIntegerMatrix(UIBlock block, string propertyName, int[,] value)
        {
            if (value == null)
            {
                string message = "Property named " + propertyName + " is null (Nothing). ";
                throw new ArgumentNullException("value", message);
            }
            int length = value.GetLength(0);
            int num2 = value.GetLength(1);
            if (System.Math.Min(length, num2) == 0)
            {
                throw new ArgumentException("Property named " + propertyName + " is an array with a zero dimension. ", "value");
            }
            int[] matrixValue = Snap.Math.MatrixMath.MatrixToVector(value);
            PropertyList properties = block.GetProperties();
            properties.SetIntegerMatrix(propertyName, length, num2, matrixValue);
            properties.Dispose();
        }

        internal static void NxSetLogical(UIBlock block, string propertyName, bool value)
        {
            PropertyList properties = block.GetProperties();
            properties.GetPropertyNames();
            properties.SetLogical(propertyName, value);
            properties.Dispose();
        }

        internal static void NxSetObject(UIBlock block, string propertyName, TaggedObject value)
        {
            PropertyList properties = block.GetProperties();
            if (Snap.NX.NXObject.IsAlive(value))
            {
                properties.SetTaggedObject(propertyName, value);
            }
            properties.Dispose();
        }

        internal static void NxSetObjectArray(UIBlock block, string propertyName, TaggedObject[] value)
        {
            PropertyList properties = block.GetProperties();
            List<TaggedObject> list2 = new List<TaggedObject>();
            foreach (TaggedObject obj2 in value)
            {
                if (Snap.NX.NXObject.IsAlive(obj2))
                {
                    list2.Add(obj2);
                }
            }
            properties.SetTaggedObjectVector(propertyName, list2.ToArray());
            properties.Dispose();
        }

        internal static void NxSetPoint(UIBlock block, string propertyName, Position value)
        {
            PropertyList properties = block.GetProperties();
            Point3d pointSc = new Point3d(value.X, value.Y, value.Z);
            properties.SetPoint(propertyName, pointSc);
            properties.Dispose();
        }

        internal static void NxSetString(UIBlock block, string propertyName, string value)
        {
            if (propertyName != "BlockID")
            {
                PropertyList properties = block.GetProperties();
                properties.SetString(propertyName, value);
                properties.Dispose();
            }
        }

        internal static void NxSetStringArray(UIBlock block, string propertyName, string[] value)
        {
            PropertyList properties = block.GetProperties();
            if (value == null)
            {
                value = new string[0];
            }
            properties.SetStrings(propertyName, value);
            properties.Dispose();
        }

        internal static void NxSetVector(UIBlock block, string propertyName, Vector value)
        {
            PropertyList properties = block.GetProperties();
            Vector3d vector = new Vector3d(value.X, value.Y, value.Z);
            properties.SetVector(propertyName, vector);
            properties.Dispose();
        }

        internal static void SetDictionaryValue(General block, Snap.UI.Block.PropertyType propType, string propName, object propValue, Dictionary<PropertyKey, object> dict)
        {
            PropertyKey key = new PropertyKey(propType, propName);
            if (!dict.ContainsKey(key))
            {
                dict.Add(key, propValue);
            }
            if ((propType == Snap.UI.Block.PropertyType.IntegerArray) && (propValue != null))
            {
                int[] numArray = (int[]) propValue;
                int[] numArray2 = (int[]) numArray.Clone();
                dict[key] = numArray2;
            }
            else if ((propType == Snap.UI.Block.PropertyType.DoubleArray) && (propValue != null))
            {
                double[] numArray3 = (double[]) propValue;
                double[] numArray4 = (double[]) numArray3.Clone();
                dict[key] = numArray4;
            }
            if ((propType == Snap.UI.Block.PropertyType.IntegerMatrix) && (propValue != null))
            {
                int[,] numArray5 = (int[,]) propValue;
                int[,] numArray6 = (int[,]) numArray5.Clone();
                dict[key] = numArray6;
            }
            else if ((propType == Snap.UI.Block.PropertyType.DoubleMatrix) && (propValue != null))
            {
                double[,] numArray7 = (double[,]) propValue;
                double[,] numArray8 = (double[,]) numArray7.Clone();
                dict[key] = numArray8;
            }
            else if ((propType == Snap.UI.Block.PropertyType.StringArray) && (propValue != null))
            {
                string[] strArray = (string[]) propValue;
                string[] strArray2 = (string[]) strArray.Clone();
                dict[key] = strArray2;
            }
            else if ((propType == Snap.UI.Block.PropertyType.ObjectArray) && (propValue != null))
            {
                TaggedObject[] objArray = (TaggedObject[]) propValue;
                TaggedObject[] objArray2 = (TaggedObject[]) objArray.Clone();
                dict[key] = objArray2;
            }
            else if ((propType == Snap.UI.Block.PropertyType.EnumMembers) && (propValue != null))
            {
                string[] strArray3 = (string[]) propValue;
                string[] strArray4 = (string[]) strArray3.Clone();
                dict[key] = strArray4;
            }
            else if ((propType == Snap.UI.Block.PropertyType.Filter) && (propValue != null))
            {
                Selection.MaskTriple[] tripleArray = (Selection.MaskTriple[]) propValue;
                Selection.MaskTriple[] tripleArray2 = (Selection.MaskTriple[]) tripleArray.Clone();
                dict[key] = tripleArray2;
            }
            else
            {
                dict[key] = propValue;
            }
        }

        private static void SetNxopenBlockValue(General block, Snap.UI.Block.PropertyType propType, string propName, object propValue)
        {
            UIBlock nXOpenBlock = block.NXOpenBlock;
            if (propType == Snap.UI.Block.PropertyType.Double)
            {
                NxSetDouble(nXOpenBlock, propName, (double) propValue);
            }
            if (propType == Snap.UI.Block.PropertyType.Integer)
            {
                NxSetInteger(nXOpenBlock, propName, (int) propValue);
            }
            if (propType == Snap.UI.Block.PropertyType.Logical)
            {
                NxSetLogical(nXOpenBlock, propName, (bool) propValue);
            }
            if (propType == Snap.UI.Block.PropertyType.String)
            {
                NxSetString(nXOpenBlock, propName, (string) propValue);
            }
            if (propType == Snap.UI.Block.PropertyType.Enum)
            {
                NxSetEnum(nXOpenBlock, propName, (int) propValue);
            }
            if (propType == Snap.UI.Block.PropertyType.EnumAsString)
            {
                NxSetEnumAsString(nXOpenBlock, propName, (string) propValue);
            }
            if (propType == Snap.UI.Block.PropertyType.EnumMembers)
            {
                NxSetEnumMembers(nXOpenBlock, propName, (string[]) propValue);
            }
            if (propType == Snap.UI.Block.PropertyType.DoubleArray)
            {
                NxSetDoubleArray(nXOpenBlock, propName, (double[]) propValue);
            }
            if (propType == Snap.UI.Block.PropertyType.IntegerArray)
            {
                NxSetIntegerArray(nXOpenBlock, propName, (int[]) propValue);
            }
            if (propType == Snap.UI.Block.PropertyType.DoubleMatrix)
            {
                NxSetDoubleMatrix(nXOpenBlock, propName, (double[,]) propValue);
            }
            if (propType == Snap.UI.Block.PropertyType.IntegerMatrix)
            {
                NxSetIntegerMatrix(nXOpenBlock, propName, (int[,]) propValue);
            }
            if (propType == Snap.UI.Block.PropertyType.StringArray)
            {
                NxSetStringArray(nXOpenBlock, propName, (string[]) propValue);
            }
            if (propType == Snap.UI.Block.PropertyType.ObjectArray)
            {
                NxSetObjectArray(nXOpenBlock, propName, (TaggedObject[]) propValue);
            }
            if (propType == Snap.UI.Block.PropertyType.Point)
            {
                NxSetPoint(nXOpenBlock, propName, (Position) propValue);
            }
            if (propType == Snap.UI.Block.PropertyType.Vector)
            {
                NxSetVector(nXOpenBlock, propName, (Vector) propValue);
            }
            if (propType == Snap.UI.Block.PropertyType.Object)
            {
                NxSetObject(nXOpenBlock, propName, (TaggedObject) propValue);
            }
            if (propType == Snap.UI.Block.PropertyType.BitArray)
            {
                NxSetBits(nXOpenBlock, propName, (int) propValue);
            }
            if (propType == Snap.UI.Block.PropertyType.File)
            {
                NxSetFile(nXOpenBlock, propName, (string) propValue);
            }
            if (propType == Snap.UI.Block.PropertyType.Filter)
            {
                NxSetFilter(nXOpenBlock, propName, (Selection.MaskTriple[]) propValue);
            }
        }

        internal static void SetPropertyValue(General block, Snap.UI.Block.PropertyType propType, string propName, object propValue)
        {
            Dictionary<PropertyKey, object> propertyDictionary = block.PropertyDictionary;
            if (propertyDictionary != null)
            {
                SetDictionaryValue(block, propType, propName, propValue, propertyDictionary);
            }
            if (block.NXOpenBlock != null)
            {
                SetNxopenBlockValue(block, propType, propName, propValue);
            }
        }
    }
}

