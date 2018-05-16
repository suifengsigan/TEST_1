namespace NXOpen
{
    using NXOpen.Utilities;
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    public class TaggedObject : NXRemotableObject
    {
        private const int ERROR_JAM_base = 0x372918;
        private const int JAM_ERR_OBJECT_NOT_ALIVE = 0x372925;
        private NXOpen.Tag m_tag;

        protected void initialize()
        {
            base.initialize();
        }

        [DllImport("libjam", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        private static extern IntPtr JAM_lookup_tag(uint tag);
        [DllImport("libjam", EntryPoint="JAM_clr_test_print_tag_of_class", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        private static extern int JAM_test_print_tag_of_class(int tag, string className, string varName, int lineNumber);
        public void PrintTestData(string variableName)
        {
            string className = base.GetType().ToString();
            StackTrace trace = new StackTrace(true);
            int fileLineNumber = trace.GetFrame(1).GetFileLineNumber();
            if (fileLineNumber == 0)
            {
                fileLineNumber = -1;
            }
            int status = JAM_test_print_tag_of_class((int) this.Tag, className, variableName, fileLineNumber);
            if (status != 0)
            {
                throw NXException.Create(status);
            }
        }

        internal void SetTag(NXOpen.Tag tag)
        {
            this.m_tag = tag;
        }

        public override string ToString()
        {
            return (base.GetType().Name + " " + this.Tag);
        }

        public NXOpen.Tag Tag
        {
            get
            {
                if (this.m_tag == NXOpen.Tag.Null)
                {
                    throw NXException.CreateWithoutUndoMark(0x372925);
                }
                return this.m_tag;
            }
        }
    }
}

