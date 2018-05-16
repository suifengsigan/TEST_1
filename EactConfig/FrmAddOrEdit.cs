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
    public partial class FrmAddOrEdit : Form
    {
        public FrmAddOrEdit()
        {
            InitializeComponent();
            Text = "EACT益模智能加工系统";
            btnSave.Click += btnSave_Click;
        }

        Action _action = null;
        object _obj;

        public FrmAddOrEdit(object obj, Action action)
            : this()
        {
            _action = action;
            _obj = obj;
            if (obj is ConfigData.Poperty)
            {
                var poperty = obj as ConfigData.Poperty;
                label3.Visible = false;
                textBox3.Visible = false;
                textBox1.Text = poperty.DisplayName;
                //textBox2.Text = poperty.Name;
                
            }
            else if (obj is ConfigData.PopertySelection)
            {
                var pSelection = obj as ConfigData.PopertySelection;
                label1.Visible = false;
                textBox1.Visible = false;
                //label2.Visible = false;
                //textBox2.Visible = false;
                textBox3.Text = pSelection.Value;
            }
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            if (_obj is ConfigData.Poperty)
            {
                var poperty = _obj as ConfigData.Poperty;
                poperty.DisplayName = textBox1.Text;
                //poperty.Name = textBox2.Text;
                if (string.IsNullOrEmpty(poperty.DisplayName) 
                    //|| string.IsNullOrEmpty(poperty.Name)
                    )
                {
                    MessageBox.Show("名称或属性名称不能为空");
                    return;
                }

            }
            else if (_obj is ConfigData.PopertySelection)
            {
                var pSelection = _obj as ConfigData.PopertySelection;
                pSelection.Value = textBox3.Text;
                if (string.IsNullOrEmpty(pSelection.Value) )
                {
                    MessageBox.Show("选项不能为空");
                    return;
                }
            }
            if (_action != null)
            {
                _action();
            }
            this.Close();
        }
    }
}
