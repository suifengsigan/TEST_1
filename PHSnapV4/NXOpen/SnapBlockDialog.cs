namespace NXOpen.BlockStyler
{
    using NXOpen;
    using NXOpen.Utilities;
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    public class SnapBlockDialog : BlockDialog
    {
        protected internal SnapBlockDialog(IntPtr ptr)
            : base(ptr)
        {
        }

        public string Add(string itemType, string itemTitle, string itemValue)
        {
            IntPtr ptr4;
            JAM.StartCall();
            IntPtr ptr = JAM.ToLocaleString(itemType);
            IntPtr ptr2 = JAM.ToLocaleString(itemTitle);
            IntPtr ptr3 = JAM.ToLocaleString(itemValue);
            int status = JA_BLOCK_STYLER_SNAP_DIALOG_add(base.Handle, ptr, ptr2, ptr3, out ptr4);
            JAM.FreeLocaleString(ptr);
            JAM.FreeLocaleString(ptr2);
            JAM.FreeLocaleString(ptr3);
            if (status != 0)
            {
                throw NXException.Create(status);
            }
            return JAM.ToStringFromLocale(ptr4);
        }

        public void AddItem(string itemType, string itemID)
        {
            JAM.StartCall();
            IntPtr ptr = JAM.ToLocaleString(itemType);
            IntPtr ptr2 = JAM.ToLocaleString(itemID);
            int status = JA_BLOCK_STYLER_SNAP_DIALOG_add_item(base.Handle, ptr, ptr2);
            JAM.FreeLocaleString(ptr);
            JAM.FreeLocaleString(ptr2);
            if (status != 0)
            {
                throw NXException.Create(status);
            }
        }

        protected override void FreeResource()
        {
            JAM.StartCall();
            int status = JA_BLOCK_STYLER_SNAP_DIALOG_dispose(base.Handle);
            if (status != 0)
            {
                throw NXException.Create(status);
            }
        }

        internal void initialize()
        {
            base.initialize();
        }

        [SuppressUnmanagedCodeSecurity, DllImport("libnxblockstyler", EntryPoint = "XJA_BLOCK_STYLER_SNAP_DIALOG_add", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int JA_BLOCK_STYLER_SNAP_DIALOG_add(IntPtr dialog, IntPtr itemType, IntPtr itemTitle, IntPtr itemValue, out IntPtr itemId);
        [SuppressUnmanagedCodeSecurity, DllImport("libnxblockstyler", EntryPoint = "XJA_BLOCK_STYLER_SNAP_DIALOG_add_item", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int JA_BLOCK_STYLER_SNAP_DIALOG_add_item(IntPtr dialog, IntPtr itemType, IntPtr itemID);
        [SuppressUnmanagedCodeSecurity, DllImport("libnxblockstyler", EntryPoint = "XJA_BLOCK_STYLER_SNAP_DIALOG_dispose", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int JA_BLOCK_STYLER_SNAP_DIALOG_dispose(IntPtr self);
    }
}

