using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SplashScreen
{
    public partial class FrmSplashScreen : Form, ISplashForm
    {
        public FrmSplashScreen()
        {
            InitializeComponent();
            this.Load += FrmSplashScreen_Load;
        }

        void FrmSplashScreen_Load(object sender, EventArgs e)
        {
        }

        //实现接口方法，主要用于接口的反射调用
        #region ISplashForm

        void ISplashForm.SetStatusInfo(string NewStatusInfo)
        {
            lbStatusInfo.Text = NewStatusInfo;
        }

        #endregion
    }
}