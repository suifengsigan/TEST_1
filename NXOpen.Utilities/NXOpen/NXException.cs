namespace NXOpen
{
    using NXOpen.Utilities;
    using System;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization;

    [Serializable]
    public class NXException : ApplicationException
    {
        private const int ErrorBase = 0x35b600;
        private const int ErrorUnexpectedSuccess = 0x35b639;
        private string m_message;
        private int m_status;
        private int m_undo_mark;

        private NXException(int status, int undo_mark) : base("NX error status: " + status)
        {
            this.m_status = status;
            this.m_undo_mark = undo_mark;
        }

        private NXException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.m_status = info.GetInt32("m_status");
            this.m_undo_mark = info.GetInt32("m_undo_mark");
            this.m_message = info.GetString("m_message");
        }

        public void AssertErrorCode(int status)
        {
            if (this.m_status != status)
            {
                throw new ApplicationException("Unexpected error code: " + status, this);
            }
        }

        public static NXException Create(int status)
        {
            return new NXException(status, JAM_get_exception_undo_mark());
        }

        public static NXException CreateWithoutUndoMark(int status)
        {
            return new NXException(status, 0);
        }

        public override void GetObjectData(SerializationInfo si, StreamingContext context)
        {
            this.initMessage();
            si.AddValue("m_status", this.m_status);
            si.AddValue("m_undo_mark", this.m_undo_mark);
            si.AddValue("m_message", this.m_message);
            base.GetObjectData(si, context);
        }

        private void initMessage()
        {
            IntPtr ptr;
            JAM_decode_error(this.m_status, out ptr);
            if (ptr == IntPtr.Zero)
            {
                this.m_message = base.Message;
            }
            else
            {
                this.m_message = JAM.ToStringFromLocale(ptr, false);
            }
        }

        [DllImport("libjam", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        private static extern void JAM_decode_error(int code, out IntPtr s);
        [DllImport("libjam", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        private static extern int JAM_get_exception_undo_mark();
        internal static void ThrowBadTagException()
        {
            throw new Exception("Attempt to use deleted object");
        }

        public static void ThrowUnexpectedSuccess()
        {
            throw CreateWithoutUndoMark(0x35b639);
        }

        public int ErrorCode
        {
            get
            {
                return this.m_status;
            }
        }

        public override string Message
        {
            get
            {
                if (this.m_message == null)
                {
                    this.initMessage();
                }
                return this.m_message;
            }
        }

        public int UndoMark
        {
            get
            {
                return this.m_undo_mark;
            }
        }
    }
}

