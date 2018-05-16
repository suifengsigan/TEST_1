namespace NXOpenUI
{
    using System;
    using System.Windows.Forms;

    internal class RestoreParentOnClose
    {
        private IntPtr formHandle;
        private IntPtr oldParentHandle;

        internal RestoreParentOnClose(Form form, IntPtr parentHandle)
        {
            this.oldParentHandle = parentHandle;
            this.formHandle = form.Handle;
        }

        internal void FormClosed(object sender, EventArgs e)
        {
            Win32Utilities.ReparentWindow(this.formHandle, this.oldParentHandle);
        }
    }
}

