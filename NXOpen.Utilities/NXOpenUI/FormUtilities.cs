namespace NXOpenUI
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class FormUtilities
    {
        public static IntPtr GetDefaultParentWindowHandle()
        {
            return Win32Utilities.GetDefaultParentWindowHandle();
        }

        public static Icon GetNXIcon()
        {
            return Icon.FromHandle(Win32Utilities.LoadIconFromResourceHandle(Win32Utilities.GetWindowsModuleHandle("windowsui"), (IntPtr) 0xf7));
        }

        public static void ReparentForm(Form myForm)
        {
            IntPtr parentHandle = Win32Utilities.ReparentWindow(myForm.Handle, Win32Utilities.GetDefaultParentWindowHandle());
            RestoreParentOnClose close = new RestoreParentOnClose(myForm, parentHandle);
            myForm.Closed += new EventHandler(close.FormClosed);
        }

        public static void SetApplicationIcon(Form form)
        {
            form.Icon = Icon.FromHandle(Win32Utilities.LoadIconFromResourceHandle(Win32Utilities.GetWindowsModuleHandle("windowsui"), (IntPtr) 0xf7));
        }
    }
}

