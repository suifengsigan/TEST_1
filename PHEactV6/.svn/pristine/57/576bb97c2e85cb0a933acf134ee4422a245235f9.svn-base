namespace NXOpen.BlockStyler
{
    using NXOpen;
    using NXOpen.Utilities;
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    public class SelectPartFromList : UIBlock
    {
        protected internal SelectPartFromList()
        {
        }

        public TaggedObject[] GetSelectedObjects()
        {
            int num;
            JAM.StartCall();
            IntPtr zero = IntPtr.Zero;
            int status = JA_BLOCK_STYLER_SELECT_PART_FROM_LIST_get_selected_objects(JAM.Lookup(base.Tag), out num, out zero);
            if (status != 0)
            {
                throw NXException.Create(status);
            }
            return (TaggedObject[])JAM.ToObjectArray(typeof(TaggedObject), num, zero);
        }

        internal void initialize()
        {
            base.initialize();
        }

        [SuppressUnmanagedCodeSecurity, DllImport("libnxblockstyler", EntryPoint = "XJA_BLOCK_STYLER_SELECT_PART_FROM_LIST_get_selected_objects", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int JA_BLOCK_STYLER_SELECT_PART_FROM_LIST_get_selected_objects(IntPtr selectPart, out int numObject, out IntPtr objectVector);
        [SuppressUnmanagedCodeSecurity, DllImport("libnxblockstyler", EntryPoint = "XJA_BLOCK_STYLER_SELECT_PART_FROM_LIST_set_selected_objects", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int JA_BLOCK_STYLER_SELECT_PART_FROM_LIST_set_selected_objects(IntPtr selectPart, int numObject, Tag[] objectVector);
        public void SetSelectedObjects(TaggedObject[] objectVector)
        {
            JAM.StartCall();
            int status = JA_BLOCK_STYLER_SELECT_PART_FROM_LIST_set_selected_objects(JAM.Lookup(base.Tag), objectVector.Length, JAM.ToTagArray(objectVector));
            if (status != 0)
            {
                throw NXException.Create(status);
            }
        }
    }
}

