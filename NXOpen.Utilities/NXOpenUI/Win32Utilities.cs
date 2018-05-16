namespace NXOpenUI
{
    using System;
    using System.Runtime.InteropServices;

    internal class Win32Utilities
    {
        private const int GWL_HWNDPARENT = -8;

        internal static IntPtr GetDefaultParentWindowHandle()
        {
            return UI_JAM_get_default_parent();
        }

        [DllImport("KERNEL32.DLL")]
        private static extern IntPtr GetModuleHandle(string module);
        [DllImport("USER32.DLL")]
        private static extern IntPtr GetParent(IntPtr child);
        internal static IntPtr GetParentHandle(IntPtr childHandle)
        {
            return GetParent(childHandle);
        }

        internal static IntPtr GetWindowsModuleHandle(string module)
        {
            return GetModuleHandle(module);
        }

        [DllImport("USER32.DLL")]
        private static extern IntPtr LoadIcon(IntPtr instance, IntPtr resource);
        internal static IntPtr LoadIconFromResourceHandle(IntPtr instance, IntPtr resourceHandle)
        {
            return LoadIcon(instance, resourceHandle);
        }

        internal static IntPtr ReparentWindow(IntPtr childHandle, IntPtr newParentHandle)
        {
            return SetWindowLong(childHandle, -8, newParentHandle);
        }

        [DllImport("USER32.DLL")]
        private static extern void SetParent(IntPtr child, IntPtr newParent);
        internal static void SetParentHandle(IntPtr childHandle, IntPtr newParentHandle)
        {
            SetParent(childHandle, newParentHandle);
        }

        [DllImport("USER32.DLL")]
        private static extern IntPtr SetWindowLong(IntPtr h, int tok, IntPtr val);
        [DllImport("libugui.dll")]
        private static extern IntPtr UI_JAM_get_default_parent();
    }
}

