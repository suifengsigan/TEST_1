namespace NXOpen
{
    using NXOpen.Utilities;
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    public abstract class TransientObject : NXRemotableObject, IDisposable
    {
        internal IntPtr pointer;

        public TransientObject()
        {
            this.pointer = IntPtr.Zero;
        }

        public TransientObject(IntPtr ptr)
        {
            this.pointer = ptr;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.DoFreeResource();
        }

        private void DoFreeResource()
        {
            if (this.pointer != IntPtr.Zero)
            {
                this.FreeResource();
                this.pointer = IntPtr.Zero;
            }
        }

        ~TransientObject()
        {
            if (this.pointer != IntPtr.Zero)
            {
                this.DoFreeResource();
            }
        }

        protected abstract void FreeResource();
        [DllImport("libjam", EntryPoint="JAM_clr_test_print_ptr_of_class", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        private static extern int JAM_test_print_ptr_of_class(IntPtr ptr, string className, string varName, int lineNumber);
        public void PrintTestData(string variableName)
        {
            string className = base.GetType().ToString();
            StackTrace trace = new StackTrace(true);
            int fileLineNumber = trace.GetFrame(1).GetFileLineNumber();
            int status = JAM_test_print_ptr_of_class(this.pointer, className, variableName, fileLineNumber);
            if (status != 0)
            {
                throw NXException.Create(status);
            }
        }

        [DllImport("libjam", EntryPoint="JAM_sm_free", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        private static extern void SM_free(IntPtr ptr);
        public string ToString()
        {
            return (base.GetType().Name + " " + this.pointer.ToString());
        }

        public IntPtr Handle
        {
            get
            {
                return this.pointer;
            }
        }
    }
}

