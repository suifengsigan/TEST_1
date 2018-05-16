namespace NXOpen.Utilities
{
    using System;
    using System.Runtime.InteropServices;

    public class PropertyFunc
    {
        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_runtime_access_debug", CallingConvention=CallingConvention.Cdecl)]
        public static extern void ExDebug(int nValue);
        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_runtime_access_func_invoke_bool", CallingConvention=CallingConvention.Cdecl)]
        public static extern int ExFuncInvokeBool(IntPtr pFunc);
        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_runtime_access_func_invoke_float", CallingConvention=CallingConvention.Cdecl)]
        public static extern double ExFuncInvokeFloat(IntPtr pFunc);
        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_runtime_access_func_invoke_int", CallingConvention=CallingConvention.Cdecl)]
        public static extern int ExFuncInvokeInt(IntPtr pFunc);
        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_runtime_access_func_invoke_object", CallingConvention=CallingConvention.Cdecl)]
        public static extern IntPtr ExFuncInvokeObject(IntPtr pFunc);
        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_runtime_access_func_invoke_void", CallingConvention=CallingConvention.Cdecl)]
        public static extern void ExFuncInvokeVoid(IntPtr pFunc);
        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_runtime_access_func_release", CallingConvention=CallingConvention.Cdecl)]
        public static extern void ExFuncRelease(IntPtr pFunc);
        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_runtime_access_get_bool", CallingConvention=CallingConvention.Cdecl)]
        public static extern int ExGetBool(IntPtr pObject, int nProp);
        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_runtime_access_get_float", CallingConvention=CallingConvention.Cdecl)]
        public static extern double ExGetFloat(IntPtr pObject, int nProp);
        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_runtime_access_get_func", CallingConvention=CallingConvention.Cdecl)]
        public static extern IntPtr ExGetFunc(IntPtr pObject, int nFunc);
        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_runtime_access_get_int", CallingConvention=CallingConvention.Cdecl)]
        public static extern int ExGetInt(IntPtr pObject, int nProp);
        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_runtime_access_get_num_props", CallingConvention=CallingConvention.Cdecl)]
        public static extern int ExGetNumProps(IntPtr pObject);
        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_runtime_access_get_object", CallingConvention=CallingConvention.Cdecl)]
        public static extern IntPtr ExGetObject(IntPtr pObject, int nProp);
        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_runtime_access_get_prop_type", CallingConvention=CallingConvention.Cdecl)]
        public static extern int ExGetPropType(IntPtr pObject, int nProp);
        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_runtime_access_set_bool", CallingConvention=CallingConvention.Cdecl)]
        public static extern int ExSetBool(IntPtr pObject, int nProp, int nValue);
        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_runtime_access_set_dirty", CallingConvention=CallingConvention.Cdecl)]
        public static extern void ExSetDirty(IntPtr pObject, int nProp);
        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_runtime_access_set_float", CallingConvention=CallingConvention.Cdecl)]
        public static extern int ExSetFloat(IntPtr pObject, int nProp, double fValue);
        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_runtime_access_set_func_arg_bool", CallingConvention=CallingConvention.Cdecl)]
        public static extern void ExSetFuncArgBool(IntPtr pFunc, int nArg, int nValue);
        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_runtime_access_set_func_arg_float", CallingConvention=CallingConvention.Cdecl)]
        public static extern void ExSetFuncArgFloat(IntPtr pFunc, int nArg, double fValue);
        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_runtime_access_set_func_arg_int", CallingConvention=CallingConvention.Cdecl)]
        public static extern void ExSetFuncArgInt(IntPtr pFunc, int nArg, int nValue);
        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_runtime_access_set_func_arg_object", CallingConvention=CallingConvention.Cdecl)]
        public static extern void ExSetFuncArgObject(IntPtr pFunc, int nArg, IntPtr pValue);
        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_runtime_access_set_int", CallingConvention=CallingConvention.Cdecl)]
        public static extern int ExSetInt(IntPtr pObject, int nProp, int nValue);
        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_runtime_access_set_object", CallingConvention=CallingConvention.Cdecl)]
        public static extern int ExSetObject(IntPtr pObject, int nProp, IntPtr pValue);
    }
}

