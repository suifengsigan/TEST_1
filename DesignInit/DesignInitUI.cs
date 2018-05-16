using NXOpen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DesignInit
{
    public partial class DesignInitUI:Form
    {
        public DesignInitUI()
        {
            InitializeComponent();
            InitEvent();
            Init();
        }

        string _eactString = "EACT";

        void Init() 
        {
            Snap.UI.WinForm.SetApplicationIcon(this);
            Snap.UI.WinForm.ReparentForm(this);
            txtSteelName.ReadOnly = true;
            Text = "设计初始化";
        }

        void InitEvent() 
        {
            btnSelectSteel.Click += btnSelectSteel_Click;
            btnOK.Click += btnOK_Click;
        }

        void btnOK_Click(object sender, EventArgs e)
        {
            TryHandle(() => {
                var fileName = txtSteelName.Tag.ToString();
                if (string.IsNullOrEmpty(fileName))
                {
                    MessageBox.Show("未选择钢件！");
                    return;
                }
                var dir = Path.Combine(Path.GetDirectoryName(fileName), _eactString);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                var newFileName = Path.Combine(dir, Path.GetFileName(fileName));
                if (!File.Exists(newFileName))
                {
                    File.Copy(fileName, newFileName, true);
                }
                var part = Session.GetSession().Parts.ToArray().ToList().FirstOrDefault(u=>u.FullPath==newFileName);
                if (part != null)
                {
                    Snap.NX.Part snapPart = part;
                    snapPart.Close(true, true);
                }
                Snap.NX.Part.OpenPart(newFileName);
                Close();
            });
        }

        void btnSelectSteel_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "钢件文件|*.prt";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var file = dialog.FileName;
                txtSteelName.Text = System.IO.Path.GetFileNameWithoutExtension(file);
                txtSteelName.Tag = file;
            }
        }

        void TryHandle(Action action) 
        {
            try
            {
                action();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

     
}
