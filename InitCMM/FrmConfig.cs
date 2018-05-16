using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InitCMM
{
    public partial class FrmConfig : Form
    {
        public FrmConfig()
        {
            InitializeComponent();
            Text = "EACT_配置工具";
            Snap.UI.WinForm.SetApplicationIcon(this);
            Snap.UI.WinForm.ReparentForm(this);
        }
    }
}
