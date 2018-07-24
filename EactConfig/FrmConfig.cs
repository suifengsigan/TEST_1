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
        public static int ColorIndex(System.Drawing.Color windowsColor)
        {
            double num = ((double)windowsColor.R) / 255.0;
            double num2 = ((double)windowsColor.G) / 255.0;
            double num3 = ((double)windowsColor.B) / 255.0;
            double[] numArray = new double[] { num, num2, num3 };
            var ufSession = NXOpen.UF.UFSession.GetUFSession();
            var disp = ufSession.Disp;
            int num4 = 0;
            int num5 = 0;
            int num6 = 1;
            disp.AskClosestColor(num5, numArray, num6, out num4);
            return num4;
        }
        void InitDgv(DataGridView view)
        {
            view.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            view.ReadOnly = true;
            //view.ColumnHeadersVisible = false;
            view.RowHeadersVisible = false;
            view.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            view.MultiSelect = false;
        }

        void InitUI() 
        {
            cbbFtpPathType.Items.Add(new ComboBoxItem { Text = "默认", Value = 0 });
            cbbFtpPathType.Items.Add(new ComboBoxItem { Text = "FZ", Value = 1 });
            cbbQuadrantType.Items.Add(new ComboBoxItem { Text = "第一象限", Value = QuadrantType.First });
            cbbQuadrantType.Items.Add(new ComboBoxItem { Text = "第二象限", Value = QuadrantType.Second });
            cbbQuadrantType.Items.Add(new ComboBoxItem { Text = "第三象限", Value = QuadrantType.Three });
            cbbQuadrantType.Items.Add(new ComboBoxItem { Text = "第四象限", Value = QuadrantType.Four });
            cbbLicenseType.Items.Add(new ComboBoxItem { Text = "Sense4user", Value = 0 });
            cbbLicenseType.Items.Add(new ComboBoxItem { Text = "slm_runtime_easy", Value =1 });
            cbbElecNameRule.Items.Add(new ComboBoxItem { Text = "A-B-C", Value = 0 });
            cbbElecNameRule.Items.Add(new ComboBoxItem { Text = "C", Value = 1 });
            cbbElecNameRule.Items.Add(new ComboBoxItem { Text = "C(A-C)", Value = 2 });
            cbbEdmTransRule.Items.Add(new ComboBoxItem { Text = "默认矩阵(沿X轴)", Value = 0 });
            cbbEdmTransRule.Items.Add(new ComboBoxItem { Text = "默认矩阵(沿Y轴)", Value = 4 });
            cbbEdmTransRule.Items.Add(new ComboBoxItem { Text = "长度矩阵(沿X轴)", Value = 1 });
            cbbEdmTransRule.Items.Add(new ComboBoxItem { Text = "长度矩阵(沿Y轴)", Value = 2 });
            cbbEdmTransRule.Items.Add(new ComboBoxItem { Text = "JR", Value = 3 });
            cbbCNCTransRule.Items.Add(new ComboBoxItem { Text = "默认矩阵(沿X轴)", Value = 0 });
            cbbCNCTransRule.Items.Add(new ComboBoxItem { Text = "长度矩阵(沿X轴)", Value = 1 });
            cbbCNCTransRule.Items.Add(new ComboBoxItem { Text = "长度矩阵(沿Y轴_基准台底面)", Value = 2 });
            //cbbUGVersion.Items.Add(new ComboBoxItem { Text = "UG6.0", Value = 0 });
            //cbbUGVersion.Items.Add(new ComboBoxItem { Text = "UG9.0", Value = 1 });
        }
        public FrmConfig()
        {
            InitializeComponent();
            InitDgv(dataGridViewPoperty);
            InitDgv(dataGridViewPSelection);
            InitUI();
            //cbExportCMM.CheckedChanged += cbExportPrt_CheckedChanged;
            this.Load += FrmConfig_Load;
            btnPopertyAdd.Click += btnPopertyAdd_Click;
            btnPopertyDelete.Click += btnPopertyDelete_Click;
            dataGridViewPoperty.SelectionChanged += dataGridView1_SelectionChanged;
            btnPopertyUpate.Click += btnPopertyUpate_Click;
            btnPSelectionAdd.Click += btnPSelectionAdd_Click;
            //dataGridViewPSelection.SelectionChanged += dataGridView2_SelectionChanged;
            btnPSelectionDelete.Click += btnPSelectionDelete_Click;
            btnPSelectionUpdate.Click += btnPSelectionUpdate_Click;
            dataGridViewPSelection.CellMouseDown += DataGridViewPSelection_CellMouseDown;
            dataGridViewPSelection.CellPainting += DataGridViewPSelection_CellPainting;
        }

        private void DataGridViewPSelection_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            var dataGridView1 = dataGridViewPSelection;
            if (e.RowIndex > -1)
            {
                var obj = dataGridViewPSelection.Rows[e.RowIndex].DataBoundItem as ConfigData.PopertySelection;
                if (obj.IsDefault)
                {
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                }
                else
                {
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Brown;
                }
            }

        }

        ContextMenuStrip _cms;
        private void DataGridViewPSelection_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            var dataGridView1 = dataGridViewPSelection;
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    var obj = dataGridViewPSelection.Rows[e.RowIndex].DataBoundItem as ConfigData.PopertySelection;
                    if (_cms == null)
                    {
                        _cms = new ContextMenuStrip();
                        _cms.Items.Add("设为默认");
                        _cms.ItemClicked += _cms_ItemClicked;
                    }
                    //弹出操作菜单
                    _cms.Show(MousePosition.X, MousePosition.Y);
                }
            }
        }

        private void _cms_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
           
            dataGridViewPSelection.Rows.Cast<DataGridViewRow>().ToList().ForEach(u => {
                var data = u.DataBoundItem as ConfigData.PopertySelection;
                var currentRow = dataGridViewPSelection.CurrentRow;
                if (currentRow == u)
                {
                    data.IsDefault = true;
                }
                else
                {
                    data.IsDefault = false;
                }
            });

            
        }

        void btnPSelectionUpdate_Click(object sender, EventArgs e)
        { 
            bool temp = dataGridViewPSelection.CurrentRow != null && dataGridViewPSelection.CurrentRow.Index >= 0;
            bool temp1 = dataGridViewPoperty.CurrentRow != null && dataGridViewPoperty.CurrentRow.Index >= 0;
            if (temp && temp1) 
            {
                var obj = dataGridViewPSelection.CurrentRow.DataBoundItem as ConfigData.PopertySelection;
                var newObj = new ConfigData.PopertySelection();
                newObj.Value = obj.Value;
                var poperty = dataGridViewPoperty.CurrentRow.DataBoundItem as ConfigData.Poperty;
                new FrmAddOrEdit(newObj, () =>
                {
                    obj.Value = newObj.Value;
                }).ShowDialog();
            }

            dataGridViewPSelection.Refresh();
        }

        void btnPSelectionDelete_Click(object sender, EventArgs e)
        {
            bool temp = dataGridViewPSelection.CurrentRow != null && dataGridViewPSelection.CurrentRow.Index >= 0;
            bool temp1 = dataGridViewPoperty.CurrentRow != null && dataGridViewPoperty.CurrentRow.Index >= 0;
            if (temp&&temp1) 
            {
                var poperty = dataGridViewPoperty.CurrentRow.DataBoundItem as ConfigData.Poperty;
                var selections = poperty.Selections.ToList();
                selections.Remove(
                    dataGridViewPSelection.CurrentRow.DataBoundItem as ConfigData.PopertySelection
                    );
                poperty.Selections = selections;
                dataGridViewPSelection.DataSource = selections;
            }
        }

        //void dataGridView2_SelectionChanged(object sender, EventArgs e)
        //{
        //    bool temp = dataGridViewPSelection.CurrentRow != null && dataGridViewPSelection.CurrentRow.Index >= 0;
        //    btnPSelectionUpdate.Enabled = temp;
        //    btnPSelectionDelete.Enabled = temp;
        //}

        void btnPSelectionAdd_Click(object sender, EventArgs e)
        {
            bool temp = dataGridViewPoperty.CurrentRow != null && dataGridViewPoperty.CurrentRow.Index >= 0;
            if (temp) 
            {
                var obj = new ConfigData.PopertySelection();
                var poperty = dataGridViewPoperty.CurrentRow.DataBoundItem as ConfigData.Poperty;
                new FrmAddOrEdit(obj, () =>
                {
                    poperty.Selections.Add(obj);
                    dataGridViewPSelection.DataSource = poperty.Selections.ToList();
                }).ShowDialog();
            }
           
        }

        void btnPopertyUpate_Click(object sender, EventArgs e)
        {
            var poperties = dataGridViewPoperty.DataSource as List<ConfigData.Poperty> ?? new List<ConfigData.Poperty>();
            if (dataGridViewPoperty.CurrentRow != null && dataGridViewPoperty.CurrentRow.Index >= 0)
            {
                var obj = dataGridViewPoperty.CurrentRow.DataBoundItem as ConfigData.Poperty;
                var newObj = new ConfigData.Poperty();
                newObj.DisplayName = obj.DisplayName;
                //newObj.Name = obj.Name;
                new FrmAddOrEdit(newObj, () => {
                    obj.DisplayName = newObj.DisplayName;
                    //obj.Name = newObj.Name;
                }).ShowDialog();
            }

            dataGridViewPoperty.Refresh();
        }

        void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            bool temp = dataGridViewPoperty.CurrentRow != null && dataGridViewPoperty.CurrentRow.Index >= 0;
            //btnPopertyDelete.Enabled = temp;
            //btnPopertyUpate.Enabled = temp;
            //btnPSelectionAdd.Enabled = temp;
            dataGridViewPSelection.DataSource = null;
            if (temp)
            {
                dataGridViewPSelection.DataSource = (dataGridViewPoperty.CurrentRow.DataBoundItem as ConfigData.Poperty).Selections;
            }
        }

        void btnPopertyDelete_Click(object sender, EventArgs e)
        {
            var poperties = dataGridViewPoperty.DataSource as List<ConfigData.Poperty> ?? new List<ConfigData.Poperty>();
            if (dataGridViewPoperty.CurrentRow != null && dataGridViewPoperty.CurrentRow.Index >= 0) 
            {
                poperties = poperties.ToList();
                poperties.Remove(dataGridViewPoperty.CurrentRow.DataBoundItem as ConfigData.Poperty);
                dataGridViewPoperty.DataSource=poperties;

            }
        }

        void btnPopertyAdd_Click(object sender, EventArgs e)
        {
            var obj=new ConfigData.Poperty();
            var poperties = dataGridViewPoperty.DataSource as List<ConfigData.Poperty> ?? new List<ConfigData.Poperty>();
            new FrmAddOrEdit(obj, () => {
                poperties.Add(obj);
            }).ShowDialog();
            dataGridViewPoperty.DataSource = poperties.ToList();
        }

        void FrmConfig_Load(object sender, EventArgs e)
        {
            var data = ConfigData.GetInstance();
            cbIsSetPropertyAllowMultiple.Checked = data.IsSetPropertyAllowMultiple;
            cbIsSetPrtColor.Checked = data.IsSetPrtColor;
            txtEleFType.Text = data.EleFType;
            txtEleMType.Text = data.EleMType;
            txtEleRType.Text = data.EleRType;
            txtDatabaseName.Text = data.DataBaseInfo.Name;
            txtDatabaseIP.Text = data.DataBaseInfo.IP;
            txtDataBaseUser.Text = data.DataBaseInfo.User;
            txtDatabasePass.Text = data.DataBaseInfo.Pass;
            txtDbLoginUser.Text = data.DataBaseInfo.LoginUser;
            txtDbLoginPass.Text = data.DataBaseInfo.LoginPass;
            txtFtpAddress.Text = data.FTP.Address;
            txtFtpUser.Text = data.FTP.User;
            txtFtpPass.Text = data.FTP.Pass;
            cbExportCMM.Checked = data.ExportPrt;
            cbbExportCNC.Checked = data.ExportCNCPrt;
            cbExportStp.Checked = data.ExportStp;
            cbImportEman.Checked = data.IsImportEman;
            dataGridViewPoperty.DataSource = data.Poperties;
            rbCanPUpdate.Checked = data.IsCanPropertyUpdate;
            cbShareElec.Checked = data.ShareElec;
            btnSetPrtColor.BackColor = System.Drawing.Color.FromArgb(data.EDMColor);
            label13.Text = ColorIndex(System.Drawing.Color.FromArgb(data.EDMColor)).ToString();
            var items= cbbQuadrantType.Items.Cast<ComboBoxItem>().ToList();
            cbbQuadrantType.SelectedIndex = items.IndexOf(items.FirstOrDefault(u => (QuadrantType)u.Value == data.QuadrantType));
            var licenseItems = cbbLicenseType.Items.Cast<ComboBoxItem>().ToList();
            cbbLicenseType.SelectedIndex = licenseItems.IndexOf(licenseItems.FirstOrDefault(u => (int)u.Value == data.LicenseType));
            var elecNameRuleItems = cbbElecNameRule.Items.Cast<ComboBoxItem>().ToList();
            cbbElecNameRule.SelectedIndex = elecNameRuleItems.IndexOf(elecNameRuleItems.FirstOrDefault(u => (int)u.Value == data.ElecNameRule));
            var EdmTransRuleItems = cbbEdmTransRule.Items.Cast<ComboBoxItem>().ToList();
            cbbEdmTransRule.SelectedIndex = EdmTransRuleItems.IndexOf(EdmTransRuleItems.FirstOrDefault(u => (int)u.Value == data.EDMTranRule));
            var CNCTransRuleItems = cbbCNCTransRule.Items.Cast<ComboBoxItem>().ToList();
            cbbCNCTransRule.SelectedIndex = CNCTransRuleItems.IndexOf(CNCTransRuleItems.FirstOrDefault(u => (int)u.Value == data.CNCTranRule));
            var FtpPathType = cbbFtpPathType.Items.Cast<ComboBoxItem>().ToList();
            cbbFtpPathType.SelectedIndex = FtpPathType.IndexOf(FtpPathType.FirstOrDefault(u => (int)u.Value == data.FtpPathType));
            //var ugVersionItems = cbbUGVersion.Items.Cast<ComboBoxItem>().ToList();
            //cbbUGVersion.SelectedIndex = ugVersionItems.IndexOf(ugVersionItems.FirstOrDefault(u => (int)u.Value == data.UGVersion));
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var data = new ConfigData();
                data.IsSetPropertyAllowMultiple = cbIsSetPropertyAllowMultiple.Checked;
                data.IsSetPrtColor = cbIsSetPrtColor.Checked;
                data.EleFType = txtEleFType.Text;
                data.EleMType = txtEleMType.Text;
                data.EleRType = txtEleRType.Text;
                data.DataBaseInfo.Name = txtDatabaseName.Text;
                data.DataBaseInfo.IP = txtDatabaseIP.Text;
                data.DataBaseInfo.User = txtDataBaseUser.Text;
                data.DataBaseInfo.Pass = txtDatabasePass.Text;
                data.DataBaseInfo.LoginUser = txtDbLoginUser.Text;
                data.DataBaseInfo.LoginPass = txtDbLoginPass.Text;
                data.FTP.Address = txtFtpAddress.Text;
                data.FTP.User = txtFtpUser.Text;
                data.FTP.Pass = txtFtpPass.Text;
                data.ExportPrt = cbExportCMM.Checked;
                data.ExportStp = cbExportStp.Checked;
                data.ExportCNCPrt = cbbExportCNC.Checked;
                data.IsImportEman = cbImportEman.Checked;
                data.IsCanPropertyUpdate = rbCanPUpdate.Checked;
                data.ShareElec = cbShareElec.Checked;
                data.EDMColor = btnSetPrtColor.BackColor.ToArgb();
                data.QuadrantType = (QuadrantType)(cbbQuadrantType.SelectedItem as ComboBoxItem).Value;
                //data.UGVersion = (int)(cbbUGVersion.SelectedItem as ComboBoxItem).Value;
                data.LicenseType = (int)(cbbLicenseType.SelectedItem as ComboBoxItem).Value;
                data.ElecNameRule = (int)(cbbElecNameRule.SelectedItem as ComboBoxItem).Value;
                data.EDMTranRule = (int)(cbbEdmTransRule.SelectedItem as ComboBoxItem).Value;
                data.CNCTranRule = (int)(cbbCNCTransRule.SelectedItem as ComboBoxItem).Value;
                data.FtpPathType = (int)(cbbFtpPathType.SelectedItem as ComboBoxItem).Value;
                data.Poperties = dataGridViewPoperty.DataSource as List<ConfigData.Poperty> ?? new List<ConfigData.Poperty>();
                ConfigData.WriteConfig(data);
                MessageBox.Show("保存成功");
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSetPrtColor_Click(object sender, EventArgs e)
        {
            var color = new ColorDialog();
            if (color.ShowDialog() == DialogResult.OK) 
            {
                btnSetPrtColor.BackColor = color.Color;
                label13.Text = ColorIndex(color.Color).ToString();
            }
        }
    }
}
