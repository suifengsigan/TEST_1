namespace NXOpen.Utilities
{
    using NXOpen;
    using System;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using System.Text;

    public class JAM
    {
        private static Exception callbackException;
        private static Encoding localeEncoding = null;

        public static void ClearCallbackException()
        {
            callbackException = null;
            JAM_clear_callback_exception();
        }

        public static void EndCall()
        {
            JAM_end_wrapped_call();
        }

        public static void EndUFCall()
        {
            JAM_end_wrapped_uf_call();
        }

        [DllImport("libjam", EntryPoint="JAM_env_translate_variable", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        private static extern IntPtr ENV_translate_variable(string s);
        public static void FreeLocaleString(IntPtr p)
        {
            SM_free(p);
        }

        public static void FreeLocaleStringArray(IntPtr[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                FreeLocaleString(array[i]);
            }
        }

        public static void FreeTextArray(IntPtr[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                TextFree(array[i]);
            }
        }

        public static void FreeVariant(Variant v)
        {
            if ((v.type & VariantType.Array) != VariantType.Int)
            {
                VariantType type = v.type & ~VariantType.Array;
                if (type == VariantType.String)
                {
                    int num = v.array_length;
                    for (int i = 0; i < num; i++)
                    {
                        SM_free(Marshal.ReadIntPtr(v.val.ptr, i * System.Runtime.InteropServices.Marshal.SizeOf(v.val.ptr)));
                    }
                }
                SM_free(v.val.ptr);
            }
            else if (v.type == VariantType.String)
            {
                SM_free(v.val.ptr);
            }
        }

        public static void FreeVariantArray(Variant[] v)
        {
            for (int i = 0; i < v.Length; i++)
            {
                if ((v[i].type & VariantType.Array) != VariantType.Int)
                {
                    SM_free(v[i].val.ptr);
                }
            }
        }

        public static string GetLicenseContext()
        {
            return (AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.FriendlyName);
        }

        private static Encoding GetLocaleEncoding()
        {
            if (localeEncoding == null)
            {
                localeEncoding = JAM_is_in_utf8_mode() ? Encoding.UTF8 : Encoding.GetEncoding(CultureInfo.CurrentCulture.TextInfo.ANSICodePage);
            }
            return localeEncoding;
        }

        public static Tag GetSingletonTag(string className)
        {
            return (Tag) JAM_lookup_singleton_tag(className);
        }

        [DllImport("libjam", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        private static extern uint JAM_ask_object_tag(IntPtr ptr);
        [DllImport("libjam", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        private static extern IntPtr JAM_ask_object_tags(int nPtr, IntPtr ptr);
        [DllImport("libjam", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        private static extern void JAM_clear_callback_exception();
        [DllImport("libjam", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        private static extern void JAM_end_wrapped_call();
        [DllImport("libjam", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        private static extern void JAM_end_wrapped_uf_call();
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport("libjam", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        private static extern bool JAM_is_in_utf8_mode();
        [DllImport("libjam", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        private static extern uint JAM_lookup_singleton_tag(string className);
        [DllImport("libjam", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        private static extern IntPtr JAM_lookup_tag(uint tag);
        [DllImport("libjam", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        private static extern int JAM_reserve_license(string licence, string context);
        [DllImport("libjam", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        private static extern int JAM_reserve_license_eitheror(int nLicenses, IntPtr[] licenses, string context);
        [DllImport("libjam", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        private static extern int JAM_reserve_license_pair(string license1, string license2, string context);
        [DllImport("libjam", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        private static extern void JAM_set_callback_exception(string s);
        [DllImport("libjam", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        private static extern void JAM_set_current_license_context(string context);
        [DllImport("libjam", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        private static extern void JAM_start_wrapped_call();
        [DllImport("libjam", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        private static extern void JAM_start_wrapped_uf_call(string context);
        public static IntPtr Lookup(Tag tag)
        {
            return JAM_lookup_tag((uint) tag);
        }

        public static Tag Lookup(IntPtr ptr)
        {
            return (Tag) JAM_ask_object_tag(ptr);
        }

        public static void SetCallbackException(Exception e)
        {
            callbackException = e;
            JAM_set_callback_exception(e.Message);
        }

        public static void SetLicenseContext()
        {
            JAM_set_current_license_context(GetLicenseContext());
        }

        [DllImport("libjam", EntryPoint="JAM_sm_alloc", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        private static extern IntPtr SM_alloc(int nbytes);
        [DllImport("libjam", EntryPoint="JAM_sm_free", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        private static extern void SM_free(IntPtr ptr);
        public static void SMFree(IntPtr p)
        {
            SM_free(p);
        }

        public static void StartCall()
        {
            JAM_start_wrapped_call();
        }

        public static void StartCall(string license)
        {
            int status = JAM_reserve_license(license, GetLicenseContext());
            if (status != 0)
            {
                throw NXException.Create(status);
            }
            JAM_start_wrapped_call();
        }

        public static void StartCall(string[] licenses)
        {
            IntPtr[] ptrArray = ToLocaleStringArray(licenses);
            int status = JAM_reserve_license_eitheror(licenses.Length, ptrArray, GetLicenseContext());
            FreeLocaleStringArray(ptrArray);
            if (status != 0)
            {
                throw NXException.Create(status);
            }
            JAM_start_wrapped_call();
        }

        public static void StartCall(string license1, string license2)
        {
            int status = JAM_reserve_license_pair(license1, license2, GetLicenseContext());
            if (status != 0)
            {
                throw NXException.Create(status);
            }
            JAM_start_wrapped_call();
        }

        public static void StartUFCall()
        {
            JAM_start_wrapped_uf_call(GetLicenseContext());
        }

        private static unsafe int strlen(IntPtr ip)
        {
            byte* numPtr = (byte*) ip.ToPointer();
            byte* numPtr2 = numPtr;
            while (numPtr2[0] != 0)
            {
                numPtr2++;
            }
            return (int) ((long) ((numPtr2 - numPtr) / 1));
        }

        [DllImport("libjam", EntryPoint="JAM_text_create_string", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        private static extern IntPtr TEXT_create_string(IntPtr ptr);
        [DllImport("libjam", EntryPoint="JAM_text_free", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        private static extern void TEXT_free(IntPtr ptr);
        public static void TextFree(IntPtr text)
        {
            TEXT_free(text);
        }

        public static unsafe bool[] ToBoolArray(int count, IntPtr logicals)
        {
            bool[] flagArray = new bool[count];
            byte* numPtr = (byte*) logicals.ToPointer();
            for (int i = 0; i < count; i++)
            {
                flagArray[i] = numPtr[i] != 0;
            }
            SM_free(logicals);
            return flagArray;
        }

        public static byte[] ToByteArray(bool[] flags)
        {
            int length = flags.Length;
            byte[] buffer = new byte[length];
            for (int i = 0; i < length; i++)
            {
                buffer[i] = Convert.ToByte(flags[i]);
            }
            return buffer;
        }

        public static double[] ToDoubleArray(int count, IntPtr doubles)
        {
            double[] destination = new double[count];
            if (count > 0)
            {
                Marshal.Copy(doubles, destination, 0, count);
            }
            SM_free(doubles);
            return destination;
        }

        public static unsafe Array ToEnumArray(Type elemType, int count, IntPtr enums)
        {
            int* numPtr = (int*) enums.ToPointer();
            Array array = Array.CreateInstance(elemType, count);
            for (int i = 0; i < count; i++)
            {
                array.SetValue(Enum.ToObject(elemType, numPtr[i]), i);
            }
            SM_free(enums);
            return array;
        }

        public static int[] ToIdArray(IHasHandle[] objects)
        {
            int length = objects.Length;
            int[] numArray = new int[length];
            for (int i = 0; i < length; i++)
            {
                numArray[i] = (objects[i] == null) ? 0 : objects[i].Handle;
            }
            return numArray;
        }

        public static int[] ToIntArray(int count, IntPtr ints)
        {
            int[] destination = new int[count];
            if (count > 0)
            {
                Marshal.Copy(ints, destination, 0, count);
            }
            SM_free(ints);
            return destination;
        }

        public static IntPtr ToLocaleString(string s)
        {
            if (s == null)
            {
                return IntPtr.Zero;
            }
            Encoding localeEncoding = GetLocaleEncoding();
            byte[] bytes = localeEncoding.GetBytes(s);
            if ((localeEncoding != Encoding.UTF8) && !localeEncoding.GetString(bytes).Equals(s))
            {
                throw NXException.Create(0x170a7a);
            }
            IntPtr destination = SM_alloc(bytes.Length + 1);
            Marshal.Copy(bytes, 0, destination, bytes.Length);
            Marshal.WriteByte(destination, bytes.Length, 0);
            return destination;
        }

        public static IntPtr[] ToLocaleStringArray(string[] strings)
        {
            IntPtr[] ptrArray = new IntPtr[strings.Length];
            for (int i = 0; i < strings.Length; i++)
            {
                ptrArray[i] = ToLocaleString(strings[i]);
            }
            return ptrArray;
        }

        public static object ToObject(Variant v)
        {
            switch ((v.type & ~VariantType.Array))
            {
                case VariantType.Int:
                    if ((v.type & VariantType.Array) == VariantType.Int)
                    {
                        return v.val.i;
                    }
                    return ToIntArray(v.array_length, v.val.ptr);

                case VariantType.Double:
                    if ((v.type & VariantType.Array) == VariantType.Int)
                    {
                        return v.val.d;
                    }
                    return ToDoubleArray(v.array_length, v.val.ptr);

                case VariantType.Logical:
                    if ((v.type & VariantType.Array) == VariantType.Int)
                    {
                        return (v.val.l != 0);
                    }
                    return ToBoolArray(v.array_length, v.val.ptr);

                case VariantType.String:
                    if ((v.type & VariantType.Array) == VariantType.Int)
                    {
                        return ToStringFromUTF8(v.val.ptr);
                    }
                    return ToStringArrayFromUTF8(v.array_length, v.val.ptr);

                case VariantType.Tag:
                    if ((v.type & VariantType.Array) == VariantType.Int)
                    {
                        return NXObjectManager.Get((Tag) v.val.i);
                    }
                    return ToObjectArray(typeof(object), v.array_length, v.val.ptr);

                case VariantType.Empty:
                    if ((v.type & VariantType.Array) != VariantType.Int)
                    {
                        throw new ArgumentException("array of Variant.Empty not allowed");
                    }
                    return null;

                case VariantType.Variant:
                    if ((v.type & VariantType.Array) == VariantType.Int)
                    {
                        throw new ArgumentException("VariantType.Variant only valid for arrays");
                    }
                    return ToObjectArray(v.array_length, v.val.ptr);
            }
            throw new ArgumentException("bad Variant object");
        }

        public static object[] ToObjectArray(Tag[] tags)
        {
            object[] objArray = new object[tags.Length];
            for (int i = 0; i < tags.Length; i++)
            {
                objArray[i] = NXObjectManager.Get(tags[i]);
            }
            return objArray;
        }

        public static object[] ToObjectArray(int count, IntPtr variants)
        {
            if (variants == IntPtr.Zero)
            {
                return null;
            }
            object[] objArray = new object[count];
            IntPtr ptr = variants;
            int num = Marshal.SizeOf(typeof(Variant));
            for (int i = 0; i < count; i++)
            {
                Variant v = (Variant) Marshal.PtrToStructure(ptr, typeof(Variant));
                objArray[i] = ToObject(v);
                ptr = (IntPtr) (ptr + num);
            }
            SM_free(variants);
            return objArray;
        }

        public static unsafe Array ToObjectArray(Type elemType, int count, IntPtr tags)
        {
            uint* numPtr = (uint*) tags.ToPointer();
            Array array = Array.CreateInstance(elemType, count);
            for (int i = 0; i < count; i++)
            {
                if (numPtr[i] != 0)
                {
                    array.SetValue(NXObjectManager.Get(*((Tag*) (numPtr + i))), i);
                }
            }
            SM_free(tags);
            return array;
        }

        public static Array ToObjectArray(Type elemType, int count, IntPtr tagsOrObjectArray, bool isNXTagObject)
        {
            IntPtr zero = IntPtr.Zero;
            if (isNXTagObject)
            {
                zero = JAM_ask_object_tags(count, tagsOrObjectArray);
                SM_free(tagsOrObjectArray);
            }
            else
            {
                zero = tagsOrObjectArray;
            }
            return ToObjectArray(elemType, count, zero);
        }

        public static IntPtr[] ToPointerArray(TaggedObject[] objects)
        {
            int length = objects.Length;
            IntPtr[] ptrArray = new IntPtr[length];
            for (int i = 0; i < length; i++)
            {
                ptrArray[i] = (objects[i] == null) ? IntPtr.Zero : Lookup(objects[i].Tag);
            }
            return ptrArray;
        }

        public static IntPtr[] ToPointerArray(TransientObject[] objects)
        {
            int length = objects.Length;
            IntPtr[] ptrArray = new IntPtr[length];
            for (int i = 0; i < length; i++)
            {
                ptrArray[i] = (objects[i] == null) ? IntPtr.Zero : objects[i].Handle;
            }
            return ptrArray;
        }

        public static unsafe string[] ToStringArrayFromLocale(int count, IntPtr strings)
        {
            string[] strArray = new string[count];
            IntPtr* ptrPtr = (IntPtr*) strings.ToPointer();
            for (int i = 0; i < count; i++)
            {
                strArray[i] = ToStringFromLocale(ptrPtr[i]);
            }
            SM_free(strings);
            return strArray;
        }

        public static unsafe string[] ToStringArrayFromText(int count, IntPtr strings)
        {
            string[] strArray = new string[count];
            IntPtr* ptrPtr = (IntPtr*) strings.ToPointer();
            for (int i = 0; i < count; i++)
            {
                strArray[i] = ToStringFromText(ptrPtr[i]);
            }
            SM_free(strings);
            return strArray;
        }

        internal static unsafe string[] ToStringArrayFromUTF8(int count, IntPtr strings)
        {
            string[] strArray = new string[count];
            byte** numPtr = (byte**) strings.ToPointer();
            for (int i = 0; i < count; i++)
            {
                strArray[i] = ToStringFromUTF8(*((IntPtr*) (numPtr + i)));
            }
            SM_free(strings);
            return strArray;
        }

        public static string ToStringFromLocale(IntPtr s)
        {
            return ToStringFromLocale(s, true);
        }

        public static string ToStringFromLocale(IntPtr s, bool doFree)
        {
            if (s == IntPtr.Zero)
            {
                return null;
            }
            int length = strlen(s);
            byte[] destination = new byte[length];
            if (length > 0)
            {
                Marshal.Copy(s, destination, 0, length);
            }
            if (doFree)
            {
                SM_free(s);
            }
            return GetLocaleEncoding().GetString(destination);
        }

        public static string ToStringFromText(IntPtr text)
        {
            if (text == IntPtr.Zero)
            {
                return null;
            }
            int length = strlen(text);
            byte[] destination = new byte[length];
            if (length > 0)
            {
                Marshal.Copy(text, destination, 0, length);
            }
            TEXT_free(text);
            return Encoding.UTF8.GetString(destination);
        }

        internal static string ToStringFromUTF8(IntPtr s)
        {
            if (s == IntPtr.Zero)
            {
                return null;
            }
            int length = strlen(s);
            byte[] destination = new byte[length];
            if (length > 0)
            {
                Marshal.Copy(s, destination, 0, length);
            }
            SM_free(s);
            return Encoding.UTF8.GetString(destination);
        }

        public static Array ToStructureArray(Type elemType, int count, IntPtr structs)
        {
            if (structs == IntPtr.Zero)
            {
                return null;
            }
            Array array = Array.CreateInstance(elemType, count);
            IntPtr ptr = structs;
            int num = Marshal.SizeOf(elemType);
            for (int i = 0; i < count; i++)
            {
                array.SetValue(Marshal.PtrToStructure(ptr, elemType), i);
                ptr = (IntPtr) (ptr + num);
            }
            return array;
        }

        public static Tag[] ToTagArray(TaggedObject[] objects)
        {
            int length = objects.Length;
            Tag[] tagArray = new Tag[length];
            for (int i = 0; i < length; i++)
            {
                tagArray[i] = (objects[i] == null) ? Tag.Null : objects[i].Tag;
            }
            return tagArray;
        }

        public static Tag[] ToTagArray(object[] objects)
        {
            int length = objects.Length;
            Tag[] tagArray = new Tag[length];
            for (int i = 0; i < length; i++)
            {
                tagArray[i] = (objects[i] == null) ? Tag.Null : ((TaggedObject) objects[i]).Tag;
            }
            return tagArray;
        }

        public static unsafe IntPtr ToText(string s)
        {
            if (s == null)
            {
                return IntPtr.Zero;
            }
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            byte* numPtr = stackalloc byte[(bytes.Length + 1)];
            for (int i = 0; i < bytes.Length; i++)
            {
                numPtr[i] = bytes[i];
            }
            numPtr[bytes.Length] = 0;
            return TEXT_create_string((IntPtr) numPtr);
        }

        public static IntPtr[] ToTextArray(string[] strings)
        {
            IntPtr[] ptrArray = new IntPtr[strings.Length];
            for (int i = 0; i < strings.Length; i++)
            {
                ptrArray[i] = ToText(strings[i]);
            }
            return ptrArray;
        }

        internal static IntPtr ToUTF8String(string s)
        {
            if (s == null)
            {
                return IntPtr.Zero;
            }
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            IntPtr destination = SM_alloc(bytes.Length + 1);
            Marshal.Copy(bytes, 0, destination, bytes.Length);
            Marshal.WriteByte(destination, bytes.Length, 0);
            return destination;
        }

        public static unsafe Variant ToVariant(object o)
        {
            if (o == null)
            {
                return new Variant(VariantType.Empty);
            }
            if (o is int)
            {
                return new Variant((int) o);
            }
            if (o is double)
            {
                return new Variant((double) o);
            }
            if (o is bool)
            {
                return new Variant((bool) o);
            }
            if (o is TaggedObject)
            {
                return new Variant(((TaggedObject) o).Tag);
            }
            if (o is string)
            {
                return new Variant((string) o);
            }
            if (!(o is Array))
            {
                throw new ArgumentException("Can not handle object type: " + o.GetType().ToString());
            }
            Type elementType = o.GetType().GetElementType();
            if (elementType == typeof(int))
            {
                int[] source = (int[]) o;
                IntPtr destination = SM_alloc(source.Length * 4);
                Marshal.Copy(source, 0, destination, source.Length);
                return new Variant(source.Length, destination, VariantType.Int);
            }
            if (elementType == typeof(bool))
            {
                bool[] flagArray = (bool[]) o;
                IntPtr ptr = SM_alloc(flagArray.Length);
                for (int j = 0; j < flagArray.Length; j++)
                {
                    Marshal.WriteByte(ptr, j, flagArray[j] ? ((byte) 1) : ((byte) 0));
                }
                return new Variant(flagArray.Length, ptr, VariantType.Logical);
            }
            if (elementType == typeof(double))
            {
                double[] numArray2 = (double[]) o;
                IntPtr ptr3 = SM_alloc(numArray2.Length * 8);
                Marshal.Copy(numArray2, 0, ptr3, numArray2.Length);
                return new Variant(numArray2.Length, ptr3, VariantType.Double);
            }
            if (elementType == typeof(string))
            {
                string[] strArray = (string[]) o;
                IntPtr ptr4 = SM_alloc(strArray.Length * sizeof(IntPtr));
                for (int k = 0; k < strArray.Length; k++)
                {
                    Marshal.WriteIntPtr(ptr4, k * sizeof(IntPtr), ToUTF8String(strArray[k]));
                }
                return new Variant(strArray.Length, ptr4, VariantType.String);
            }
            if (typeof(TaggedObject).IsAssignableFrom(elementType))
            {
                object[] objArray = (object[]) o;
                IntPtr ptr5 = SM_alloc(objArray.Length * 4);
                for (int m = 0; m < objArray.Length; m++)
                {
                    Marshal.WriteInt32(ptr5, m * 4, (int) ((TaggedObject) objArray[m]).Tag);
                }
                return new Variant(objArray.Length, ptr5, VariantType.Tag);
            }
            object[] objArray2 = (object[]) o;
            int num4 = Marshal.SizeOf(typeof(Variant));
            IntPtr ptr6 = SM_alloc(objArray2.Length * num4);
            byte* numPtr = (byte*) ptr6.ToPointer();
            for (int i = 0; i < objArray2.Length; i++)
            {
                Marshal.StructureToPtr(ToVariant(objArray2[i]), new IntPtr((void*) numPtr), true);
                numPtr += num4;
            }
            return new Variant(objArray2.Length, ptr6, VariantType.Variant);
        }

        public static Variant[] ToVariantArray(object[] objects)
        {
            if (objects == null)
            {
                return null;
            }
            int length = objects.Length;
            Variant[] variantArray = new Variant[length];
            for (int i = 0; i < length; i++)
            {
                variantArray[i] = ToVariant(objects[i]);
            }
            return variantArray;
        }

        internal static string TranslateVariable(string s)
        {
            return ToStringFromLocale(ENV_translate_variable(s));
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Variant
        {
            internal JAM.VariantType type;
            internal int array_length;
            internal JAM.VariantValue val;
            internal Variant(int i)
            {
                this.type = JAM.VariantType.Int;
                this.array_length = 0;
                this.val.l = 0;
                this.val.d = 0.0;
                this.val.ptr = IntPtr.Zero;
                this.val.i = i;
            }

            internal Variant(double d)
            {
                this.type = JAM.VariantType.Double;
                this.array_length = 0;
                this.val.l = 0;
                this.val.i = 0;
                this.val.ptr = IntPtr.Zero;
                this.val.d = d;
            }

            internal Variant(bool b)
            {
                this.type = JAM.VariantType.Logical;
                this.array_length = 0;
                this.val.d = 0.0;
                this.val.i = 0;
                this.val.ptr = IntPtr.Zero;
                this.val.l = b ? ((byte) 1) : ((byte) 0);
            }

            internal Variant(Tag t)
            {
                this.type = JAM.VariantType.Tag;
                this.array_length = 0;
                this.val.l = 0;
                this.val.d = 0.0;
                this.val.ptr = IntPtr.Zero;
                this.val.i = (int) t;
            }

            internal Variant(string s)
            {
                this.type = JAM.VariantType.String;
                this.array_length = 0;
                this.val.l = 0;
                this.val.d = 0.0;
                this.val.i = 0;
                this.val.ptr = JAM.ToUTF8String(s);
            }

            internal Variant(int array_length, IntPtr ptr, JAM.VariantType elemType)
            {
                this.type = elemType | JAM.VariantType.Array;
                this.array_length = array_length;
                this.val.l = 0;
                this.val.d = 0.0;
                this.val.i = 0;
                this.val.ptr = ptr;
            }

            internal Variant(JAM.VariantType t)
            {
                this.type = JAM.VariantType.Empty;
                this.array_length = 0;
                this.val.l = 0;
                this.val.d = 0.0;
                this.val.i = 0;
                this.val.ptr = IntPtr.Zero;
            }
        }

        public enum VariantType
        {
            Array = 0x1000,
            Double = 1,
            Empty = 5,
            Int = 0,
            Logical = 2,
            String = 3,
            Tag = 4,
            Variant = 6
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct VariantValue
        {
            [FieldOffset(0)]
            internal double d;
            [FieldOffset(0)]
            internal int i;
            [FieldOffset(0)]
            internal byte l;
            [FieldOffset(0)]
            internal IntPtr ptr;
        }
    }
}

