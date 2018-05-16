namespace NXOpen.Utilities
{
    using NXOpen;
    using System;
    using System.Runtime.InteropServices;

    internal class InstanceFunc
    {
        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_runtime_access_add_reference", CallingConvention=CallingConvention.Cdecl)]
        public static extern void ExAddReference(IntPtr pItem);
        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_runtime_access_ask_assembly", CallingConvention=CallingConvention.Cdecl)]
        public static extern IntPtr ExAskAssembly(IntPtr pItem);
        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_define_results_get_object_in_model", CallingConvention=CallingConvention.Cdecl)]
        public static extern Tag ExAskObjectPersistentTag(IntPtr pItem);
        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_runtime_access_copy", CallingConvention=CallingConvention.Cdecl)]
        public static extern IntPtr ExCopy(IntPtr pItem);
        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_runtime_access_destroy", CallingConvention=CallingConvention.Cdecl)]
        public static extern void ExDestroy(IntPtr pItem);
        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_runtime_access_get_class", CallingConvention=CallingConvention.Cdecl)]
        public static extern int ExGetClass(IntPtr pItem);
        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_get_control_force", CallingConvention=CallingConvention.Cdecl)]
        public static extern double ExGetControlForce(IntPtr pItem, double stepSize, int indx);
        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_get_control_torque", CallingConvention=CallingConvention.Cdecl)]
        public static extern double ExGetControlTorque(IntPtr pItem, double stepSize, int indx);
        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_get_coupling_master_force", CallingConvention=CallingConvention.Cdecl)]
        public static extern double ExGetCouplingMasterForce(IntPtr pItem, double stepSize, int indx);
        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_get_coupling_master_torque", CallingConvention=CallingConvention.Cdecl)]
        public static extern double ExGetCouplingMasterTorque(IntPtr pItem, double stepSize, int indx);
        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_get_coupling_slave_force", CallingConvention=CallingConvention.Cdecl)]
        public static extern double ExGetCouplingSlaveForce(IntPtr pItem, double stepSize, int indx);
        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_get_coupling_slave_torque", CallingConvention=CallingConvention.Cdecl)]
        public static extern double ExGetCouplingSlaveTorque(IntPtr pItem, double stepSize, int indx);
        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_get_force", CallingConvention=CallingConvention.Cdecl)]
        public static extern double ExGetForce(IntPtr pItem, double stepSize, int indx);
        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_get_torque", CallingConvention=CallingConvention.Cdecl)]
        public static extern double ExGetTorque(IntPtr pItem, double stepSize, int indx);
        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_runtime_access_is_destroyed", CallingConvention=CallingConvention.Cdecl)]
        public static extern int ExIsDestroyed(IntPtr pItem);
        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_runtime_access_release", CallingConvention=CallingConvention.Cdecl)]
        public static extern void ExRelease(IntPtr pItem);
    }
}

