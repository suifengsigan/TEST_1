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
        public static System.Drawing.Color WindowsColor(int colorIndex)
        {
            string str;
            double[] numArray = new double[3];
            int num = 0;
            if (NXOpen.Session.GetSession().Parts.Work != null)
            {
                var ufSession = NXOpen.UF.UFSession.GetUFSession();
                var disp = ufSession.Disp;
                disp.AskColor(colorIndex, num, out str, numArray);
            }
            int red = (int)(numArray[0] * 255.0);
            int green = (int)(numArray[1] * 255.0);
            int blue = (int)(numArray[2] * 255.0);
            return System.Drawing.Color.FromArgb(red, green, blue);
        }
        public static int ColorIndex(System.Drawing.Color windowsColor)
        {
            double num = ((double)windowsColor.R) / 255.0;
            double num2 = ((double)windowsColor.G) / 255.0;
            double num3 = ((double)windowsColor.B) / 255.0;
            double[] numArray = new double[] { num, num2, num3 };
            int num4 = 0;
            int num5 = 0;
            int num6 = 1;
            if (NXOpen.Session.GetSession().Parts.Work != null)
            {
                var ufSession = NXOpen.UF.UFSession.GetUFSession();
                var disp = ufSession.Disp;
                disp.AskClosestColor(num5, numArray, num6, out num4);
            }
            return num4;
        }
        void InitDgv(DataGridView view)
        {
            view.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            view.ReadOnly = false;
            //view.ColumnHeadersVisible = false;
            view.RowHeadersVisible = false;
            view.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            view.AllowUserToResizeRows = false;
            view.MultiSelect = false;
        }

        void InitUI() 
        {
            cbEdition.Items.Add(new ComboBoxItem { Text = "默认", Value = 0 });
            cbEdition.Items.Add(new ComboBoxItem { Text = "PZ", Value = 1 });
            cbEdition.Items.Add(new ComboBoxItem { Text = "HTUP", Value = 2 });
            cbEdition.Items.Add(new ComboBoxItem { Text = "BX", Value = 3 });
            cbbFtpPathType.Items.Add(new ComboBoxItem { Text = @"模号\电极编号\文件", Value = 0 });
            cbbFtpPathType.Items.Add(new ComboBoxItem { Text = @"模号\文件", Value = 2 });
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
            cbbEdmTransRule.Items.Add(new ComboBoxItem { Text = "BX", Value = 5 });
            cbbCNCTransRule.Items.Add(new ComboBoxItem { Text = "默认矩阵(沿X轴)", Value = 0 });
            cbbCNCTransRule.Items.Add(new ComboBoxItem { Text = "长度矩阵(沿X轴)", Value = 1 });
            cbbCNCTransRule.Items.Add(new ComboBoxItem { Text = "长度矩阵(沿Y轴_基准台底面)", Value = 2 });
            cbbCNCTransRule.Items.Add(new ComboBoxItem { Text = "长度矩阵(沿Y轴)", Value = 3 });
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
            //btnPopertyAdd.Click += btnPopertyAdd_Click;
            //btnPopertyDelete.Click += btnPopertyDelete_Click;
            dataGridViewPoperty.SelectionChanged += dataGridView1_SelectionChanged;
            //btnPopertyUpate.Click += btnPopertyUpate_Click;
            //btnPSelectionAdd.Click += btnPSelectionAdd_Click;
            //dataGridViewPSelection.SelectionChanged += dataGridView2_SelectionChanged;
            //btnPSelectionDelete.Click += btnPSelectionDelete_Click;
            //btnPSelectionUpdate.Click += btnPSelectionUpdate_Click;
            //dataGridViewPSelection.CellMouseDown += DataGridViewPSelection_CellMouseDown;
            dataGridViewPSelection.MouseDown += DataGridViewPSelection_MouseDown;
            dataGridViewPoperty.MouseDown += DataGridViewPoperty_MouseDown;
            dataGridViewPSelection.CellPainting += DataGridViewPSelection_CellPainting;
            btnSetPrtColor.Click += btnSetPrtColor_Click;
        }

        private void DataGridViewPoperty_MouseDown(object sender, MouseEventArgs e)
        {
            var dataGridViewPSelection = dataGridViewPoperty;
            if (e.Button == MouseButtons.Right)
            {
                _cms = new ContextMenuStrip();
                _cms.Items.Add("新增属性");
                var list = dataGridViewPSelection.DataSource as List<EactConfig.ConfigData.Poperty> ?? new List<EactConfig.ConfigData.Poperty>();
                bool temp = dataGridViewPSelection.CurrentRow != null && dataGridViewPSelection.CurrentRow.Index >= 0 && dataGridViewPSelection.CurrentRow.Index < list.Count;
                if (temp)
                {
                    //_cms.Items.Add("修改加长杆");
                    _cms.Items.Add("删除属性");
                }

                _cms.ItemClicked += _cms_ItemClicked;
                //弹出操作菜单
                _cms.Show(MousePosition.X, MousePosition.Y);
            }
        }

        private void DataGridViewPSelection_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _cms = new ContextMenuStrip();
                _cms.Items.Add("新增选项");
                _cms.Items.Add("设为默认");
                var list = dataGridViewPSelection.DataSource as List<EactConfig.ConfigData.PopertySelection> ?? new List<EactConfig.ConfigData.PopertySelection>();
                bool temp = dataGridViewPSelection.CurrentRow != null && dataGridViewPSelection.CurrentRow.Index >= 0 && dataGridViewPSelection.CurrentRow.Index < list.Count;
                if (temp)
                {
                    if (dataGridViewPoperty.CurrentRow != null)
                    {
                        var objPoperty = dataGridViewPoperty.CurrentRow.DataBoundItem as ConfigData.Poperty;
                        if (objPoperty != null && objPoperty.DisplayName == "夹具类型")
                        {
                            _cms.Items.Add("夹具设置");
                        }
                    }
                   
                    _cms.Items.Add("删除选项");
                }

                _cms.ItemClicked += _cms_ItemClicked;
                //弹出操作菜单
                _cms.Show(MousePosition.X, MousePosition.Y);
            }
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
                    _cms = new ContextMenuStrip();
                    _cms.Items.Add("设为默认");
                    if (dataGridViewPoperty.CurrentRow != null)
                    {
                        var objPoperty = dataGridViewPoperty.CurrentRow.DataBoundItem as ConfigData.Poperty;
                        if (objPoperty != null && objPoperty.DisplayName == "夹具类型")
                        {
                            _cms.Items.Add("夹具设置");
                        }
                    }
                    _cms.ItemClicked += _cms_ItemClicked;
                    //弹出操作菜单
                    _cms.Show(MousePosition.X, MousePosition.Y);
                }
            }
        }

        private void _cms_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var dataGridViewPSelection1 = dataGridViewPoperty;
            var datasource1 = dataGridViewPSelection1.DataSource as List<EactConfig.ConfigData.Poperty> ?? new List<EactConfig.ConfigData.Poperty>();

            var dataGridViewPSelection2 = dataGridViewPSelection;
            var datasource2 = dataGridViewPSelection2.DataSource as List<EactConfig.ConfigData.PopertySelection> ?? new List<EactConfig.ConfigData.PopertySelection>();
            if (e.ClickedItem.Text == "夹具设置")
            {
                if (dataGridViewPSelection.CurrentRow != null) {
                    var obj = dataGridViewPSelection.CurrentRow.DataBoundItem as ConfigData.PopertySelection;
                    if (obj != null)
                    {
                        new FrmMatchJiaju(obj).ShowDialog();
                    }
                }
                
            }
            else if (e.ClickedItem.Text == "新增属性")
            {
                datasource1.Add(new EactConfig.ConfigData.Poperty { });
                dataGridViewPSelection1.DataSource = datasource1.ToList();
            }
            else if (e.ClickedItem.Text == "删除属性")
            {
                if (dataGridViewPSelection1.CurrentRow != null)
                {
                    var obj = dataGridViewPSelection1.CurrentRow.DataBoundItem as EactConfig.ConfigData.Poperty;
                    if (obj != null)
                    {
                        datasource1.Remove(obj);
                        dataGridViewPSelection1.DataSource = datasource1.ToList();
                    }
                }

            }
            else if (e.ClickedItem.Text == "新增选项")
            {
                datasource2.Add(new EactConfig.ConfigData.PopertySelection { });
                dataGridViewPSelection2.DataSource = datasource2.ToList();
            }
            else if (e.ClickedItem.Text == "删除选项")
            {
                if (dataGridViewPSelection2.CurrentRow != null)
                {
                    var obj = dataGridViewPSelection2.CurrentRow.DataBoundItem as EactConfig.ConfigData.PopertySelection;
                    if (obj != null)
                    {
                        datasource2.Remove(obj);
                        dataGridViewPSelection2.DataSource = datasource2.ToList();
                    }
                }

            }
            else
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
            var data = dataGridViewPoperty.DataSource as List<ConfigData.Poperty> ?? new List<ConfigData.Poperty>();
            bool temp = dataGridViewPoperty.CurrentRow != null
                && dataGridViewPoperty.CurrentRow.Index >= 0
                && dataGridViewPoperty.CurrentRow.Index < data.Count
                ;
            //btnPopertyDelete.Enabled = temp;
            //btnPopertyUpate.Enabled = temp;
            //btnPSelectionAdd.Enabled = temp;
            dataGridViewPSelection.DataSource = null;
            if (temp)
            {
                var poperty = (dataGridViewPoperty.CurrentRow.DataBoundItem as ConfigData.Poperty);
                dataGridViewPSelection.DataSource = poperty.Selections;
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
            cbIsExportEDM.Checked = data.IsExportEDM;
            cbIsElecSetDefault.Checked = data.IsElecSetDefault;
            cbIsDistinguishSideElec.Checked = data.IsDistinguishSideElec;
            cbIsAutoCMM.Checked = data.IsAutoCMM;
            cbIsAutoPrtTool.Checked = data.IsAutoPrtTool;
            cbIsDeleteDraft.Checked = data.IsDeleteDraft;
            cbIsMatNameSel.Checked = data.IsMatNameSel;
            CBSpecialshapedElec.Checked = data.SpecialshapedElec;
            txtPQBlankStock.Text = data.PQBlankStock.ToString();
            cbIsExportBomXls.Checked = data.IsExportBomXls;
            cbIsCanSelElecInBom.Checked = data.IsCanSelElecInBom;
            cbIsCanSelLayerInBom.Checked = data.isCanSelLayerInBom;
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
            txtEmanWebPath.Text = data.txtEmanWebPath;
            cbShareElec.Checked = data.ShareElec;
            try
            {
                btnSetPrtColor.BackColor = WindowsColor(data.EDMColor);
                label13.Text = data.EDMColor.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                btnSetPrtColor.BackColor = System.Drawing.Color.Red;
                label13.Text = ColorIndex(System.Drawing.Color.Red).ToString();
            }
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
            var Edition = cbEdition.Items.Cast<ComboBoxItem>().ToList();
            cbEdition.SelectedIndex = Edition.IndexOf(Edition.FirstOrDefault(u => (int)u.Value == data.Edition));
            //var ugVersionItems = cbbUGVersion.Items.Cast<ComboBoxItem>().ToList();
            //cbbUGVersion.SelectedIndex = ugVersionItems.IndexOf(ugVersionItems.FirstOrDefault(u => (int)u.Value == data.UGVersion));
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var data = new ConfigData();
                data.IsExportEDM = cbIsExportEDM.Checked;
                data.IsElecSetDefault = cbIsElecSetDefault.Checked;
                data.IsDistinguishSideElec = cbIsDistinguishSideElec.Checked;
                data.IsAutoCMM = cbIsAutoCMM.Checked;
                data.IsAutoPrtTool=cbIsAutoPrtTool.Checked;
                data.IsDeleteDraft= cbIsDeleteDraft.Checked;
                data.IsMatNameSel= cbIsMatNameSel.Checked;
                data.SpecialshapedElec = CBSpecialshapedElec.Checked;
                var PQBlankStock = 1.5;
                double.TryParse(txtPQBlankStock.Text, out PQBlankStock);
                data.PQBlankStock = PQBlankStock;
                data.IsExportBomXls = cbIsExportBomXls.Checked;
                data.IsCanSelElecInBom= cbIsCanSelElecInBom.Checked;
                data.isCanSelLayerInBom = cbIsCanSelLayerInBom.Checked;
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
                data.txtEmanWebPath = txtEmanWebPath.Text;
                data.EDMColor = ColorIndex(btnSetPrtColor.BackColor);
                data.QuadrantType = (QuadrantType)(cbbQuadrantType.SelectedItem as ComboBoxItem).Value;
                //data.UGVersion = (int)(cbbUGVersion.SelectedItem as ComboBoxItem).Value;
                data.LicenseType = (int)(cbbLicenseType.SelectedItem as ComboBoxItem).Value;
                data.ElecNameRule = (int)(cbbElecNameRule.SelectedItem as ComboBoxItem).Value;
                data.EDMTranRule = (int)(cbbEdmTransRule.SelectedItem as ComboBoxItem).Value;
                data.CNCTranRule = (int)(cbbCNCTransRule.SelectedItem as ComboBoxItem).Value;
                data.FtpPathType = (int)(cbbFtpPathType.SelectedItem as ComboBoxItem).Value;
                data.Edition = (int)(cbEdition.SelectedItem as ComboBoxItem).Value;
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
