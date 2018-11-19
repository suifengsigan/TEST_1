using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EactConfig
{
    public partial class FrmConfig : Form
    {
        ConfigUserControl _control = new ConfigUserControl();
        public FrmConfig()
        {
            InitializeComponent();
            _control.Dock = DockStyle.Fill;
            this.Controls.Add(_control);
            this.FormClosing += FrmConfig_FormClosing;
        }

        private void FrmConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            _control.Save();
        }
    }
}
