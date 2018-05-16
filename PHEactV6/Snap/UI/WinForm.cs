namespace Snap.UI
{
    using NXOpenUI;
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class WinForm : Form
    {
        public WinForm()
        {
            SetApplicationIcon(this);
            ReparentForm(this);
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.ClientSize = new Size(0x11c, 0x106);
            base.Name = "WinForm";
            base.Load += new EventHandler(this.WinForm_Load);
            base.ResumeLayout(false);
        }

        public static void ReparentForm(Form form)
        {
            FormUtilities.ReparentForm(form);
        }

        public static void SetApplicationIcon(Form form)
        {
            FormUtilities.SetApplicationIcon(form);
        }

        private void WinForm_Load(object sender, EventArgs e)
        {
        }
    }
}

