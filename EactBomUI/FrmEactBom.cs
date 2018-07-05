using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SnapEx;

namespace EactBom
{
    public partial class FrmEactBom : Form
    {
        public FrmEactBom()
        {
            InitializeComponent();
            InitUI();
            InitEvent();
            this.Shown += FrmEactBom_Shown;
        }

        public FrmEactBom(ElecManage.MouldInfo mouldInfo) 
        {
            InitializeComponent();
            InitUI();
            InitEvent();
            this.Shown += (sender,e) => {
                SplashScreen.Splasher.Show(typeof(SplashScreen.FrmSplashScreen));
                //System.Threading.Thread.Sleep(800);
                SplashScreen.Splasher.Status = "正在加载电极数据......";
                System.Threading.Thread.Sleep(800);
                try 
                {
                    dgvSteels.DataSource = new List<ElecManage.MouldInfo>() { mouldInfo };
                    SplashScreen.Splasher.Status = "加载完毕............";
                    System.Threading.Thread.Sleep(800);

                    SplashScreen.Splasher.Close();
                }
                catch (Exception ex)
                {
                    SplashScreen.Splasher.Close();
                    throw ex;
                }
            };
        }

        void InitUI() 
        {
            Snap.UI.WinForm.SetApplicationIcon(this);
            Snap.UI.WinForm.ReparentForm(this);
            Text = "EACT-益模智能加工系统";

            var configData = EactBomBusiness.Instance.ConfigData;
            btnSave.Visible = configData.IsCanPropertyUpdate;
            //btnShareElec.Visible = configData.ShareElec;
            groupShareElec.Visible = configData.ShareElec;
            configData.Poperties.ForEach(u => {
                ComboBox cbb = null;
                if (u.DisplayName == "电极材质") 
                {
                    cbb = cboxMAT_NAME;
                }
                else if (u.DisplayName == "加工方向")
                {
                    cbb = cbbProdirection;
                }
                else if (u.DisplayName == "电极类型")
                {
                    cbb = cbbElecType;
                }
                else if (u.DisplayName == "摇摆方式")
                {
                    cbb = cbbRock;
                }
                else if (u.DisplayName == "精公光洁度")
                {
                    cbb = cbbFSmoth;
                }
                else if (u.DisplayName == "中公光洁度")
                {
                    cbb = cbbMSmoth;
                }
                else if (u.DisplayName == "粗公光洁度")
                {
                    cbb = cbbRSmoth;
                }
                else if (u.DisplayName == "夹具类型")
                {
                    cbb = cbbChuckType;
                }

                if (cbb != null) 
                {
                    cbb.Items.Add(new ComboBoxItem { Text = string.Empty, Value = string.Empty });
                    u.Selections.ForEach(m =>
                    {
                        cbb.Items.Add(new ComboBoxItem { Text = m.Value, Value = m.Value });
                    });
                }
            });

            InitDgv(dgvCuprums);

            dgvCuprums.ReadOnly = false;
            var ChCol = new DataGridViewCheckBoxColumn();
            ChCol.HeaderText = "选择";
            ChCol.Width = 40;
            ChCol.DataPropertyName = "Checked";
            ChCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvCuprums.Columns.Add(ChCol);
            
            var txtCol = new DataGridViewTextBoxColumn();
            txtCol.DataPropertyName = "ElectName";
            txtCol.HeaderText = "电极名称";
            txtCol.ReadOnly = true;
            txtCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvCuprums.Columns.Add(txtCol);

            var txtShareElec = new DataGridViewTextBoxColumn();
            txtShareElec.DataPropertyName = "ShareElecStr";
            txtShareElec.HeaderText = "共用电极";
            txtShareElec.Width = 80;
            txtShareElec.ReadOnly = true;
            txtShareElec.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            txtShareElec.Visible = configData.ShareElec;
            dgvCuprums.Columns.Add(txtShareElec);

            InitDgv(dgvSteels);
            InitDgv(dgvPositions);
            InitDgv(dataGridView1);
        }

        void InitDgv(DataGridView view)
        {
            view.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            view.ReadOnly = true;
            //view.ColumnHeadersVisible = false;
            view.RowHeadersVisible = false;
            view.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            view.AllowUserToResizeRows = false; 
            view.MultiSelect = false;
        }

        void RefreshUI() 
        {
            groupShareElec.Visible = false;
            groupBoxElecInfo.Enabled = true;
            dataGridView1.DataSource = null;
            //当前行
            var currentRow = dgvCuprums.CurrentRow;
            if (currentRow != null && currentRow.Index >= 0)
            {
                var item = currentRow.DataBoundItem as ViewElecInfo;
                var shareElecList = new List<ShareElecInfo>();
                item.ShareElecList.ForEach(u => {
                    shareElecList.Add(new ShareElecInfo { EACT_CUPRUM = u });
                });
                dataGridView1.DataSource = shareElecList;
                groupShareElec.Visible = EactBomBusiness.Instance.ConfigData.ShareElec && item.ShareElec();
                groupBoxElecInfo.Enabled = !groupShareElec.Visible;
            }
        }

        void InitEvent() 
        {
            dgvSteels.SelectionChanged += dgvSteels_SelectionChanged;
            dgvCuprums.SelectionChanged += dgvCuprums_SelectionChanged;
            ckCuprumSelectAll.CheckedChanged += ckCuprumSelectAll_CheckedChanged;
            dgvCuprums.DataSourceChanged += dgvCuprums_DataSourceChanged;
            btnSave.Click += btnSave_Click;
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var positions = (dgvPositions.DataSource as List<ElecManage.PositioningInfo>) ?? new List<ElecManage.PositioningInfo>();
                if (positions.Count > 0)
                {
                    positions.ForEach(u =>
                    {
                        var info = u.Electrode.GetElectrodeInfo();
                        info.EDMPROCDIRECTION = cbbProdirection.Text;
                        info.EDMROCK = cbbRock.Text;
                        info.UNIT = cbbElecType.Text;
                        info.F_SMOOTH = cbbFSmoth.Text;
                        info.M_SMOOTH = cbbMSmoth.Text;
                        info.R_SMOOTH = cbbRSmoth.Text;
                        info.MAT_NAME = cboxMAT_NAME.Text;
                        info.ELEC_CLAMP_GENERAL_TYPE = cbbChuckType.Text;
                    });
                    MessageBox.Show("保存属性成功");
                    //新增记忆功能
                    EactBomBusiness.Instance.ConfigData.Poperties.ForEach(u => {
                        ComboBox cbb = null;
                        if (u.DisplayName == "电极材质")
                        {
                            cbb = cboxMAT_NAME;
                        }
                        else if (u.DisplayName == "加工方向")
                        {
                            cbb = cbbProdirection;
                        }
                        else if (u.DisplayName == "电极类型")
                        {
                            cbb = cbbElecType;
                        }
                        else if (u.DisplayName == "摇摆方式")
                        {
                            cbb = cbbRock;
                        }
                        else if (u.DisplayName == "精公光洁度")
                        {
                            cbb = cbbFSmoth;
                        }
                        else if (u.DisplayName == "中公光洁度")
                        {
                            cbb = cbbMSmoth;
                        }
                        else if (u.DisplayName == "粗公光洁度")
                        {
                            cbb = cbbRSmoth;
                        }
                        else if (u.DisplayName == "夹具类型")
                        {
                            cbb = cbbChuckType;
                        }

                        if (cbb!=null&&!string.IsNullOrEmpty(cbb.Text))
                        {
                            var selection = u.Selections.FirstOrDefault(s => s.Value == cbb.Text);
                            if (selection != null)
                            {
                                u.Selections.ForEach(s => { s.IsDefault = false; });
                                selection.IsDefault = true;
                            }
                        }
                    });
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void dgvCuprums_DataSourceChanged(object sender, EventArgs e)
        {
            var cuprums = dgvCuprums.DataSource as List<ViewElecInfo> ?? new List<ViewElecInfo>();
            labelCuprumNum.Text = string.Format("电极总数量:{0}",cuprums.Count);
        }

        void ckCuprumSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            var cuprums = dgvCuprums.DataSource as List<ViewElecInfo> ?? new List<ViewElecInfo>();
            cuprums.ForEach(u => {
                u.Checked = ckCuprumSelectAll.Checked;
            });
            dgvCuprums.Refresh();
        }

        ViewElecInfo _curCuprum;
        void dgvCuprums_SelectionChanged(object sender, EventArgs e)
        {
            //TODO 清空属性区域


            //当前行
            var currentRow = dgvCuprums.CurrentRow;
            var steelInfo = dgvSteels.CurrentRow.DataBoundItem as ElecManage.MouldInfo;
            if (steelInfo != null && currentRow != null && currentRow.Index >= 0)
            {
                var item = currentRow.DataBoundItem as ViewElecInfo;
                if (_curCuprum != item)
                {
                    var list=EactBomBusiness.Instance.GetPositioningInfos(item,steelInfo);
                    dgvPositions.DataSource =list ;
                    if (list.Count > 0)
                    {
                        var pos = list.First();
                        var info = pos.Electrode.GetElectrodeInfo();
                        txtFINISH_NUMBER.Text = info.FINISH_NUMBER;
                        txtMIDDLE_NUMBER.Text = info.MIDDLE_NUMBER;
                        txtROUGH_NUMBER.Text = info.ROUGH_NUMBER;
                        txtFINISH_SPACE.Text = info.FINISH_SPACE;
                        txtMIDDLE_SPACE.Text = info.MIDDLE_SPACE;
                        txtROUGH_SPACE.Text = info.ROUGH_SPACE;
                        cboxMAT_NAME.Text = info.MAT_NAME;
                        cbbElecType.Text = info.UNIT;
                        cbbFSmoth.Text = info.F_SMOOTH;
                        cbbMSmoth.Text = info.M_SMOOTH;
                        cbbRSmoth.Text = info.R_SMOOTH;
                        cbbRock.Text = info.EDMROCK;
                        cbbChuckType.Text = info.ELEC_CLAMP_GENERAL_TYPE;
                        cbbProdirection.Text = info.EDMPROCDIRECTION;
                        txtElecSize.Text = info.ElecSize;

                        EactBomBusiness.Instance.ConfigData.Poperties.ForEach(u => {
                            ComboBox cbb = null;
                            if (u.DisplayName == "电极材质")
                            {
                                cbb = cboxMAT_NAME;
                            }
                            else if (u.DisplayName == "加工方向")
                            {
                                cbb = cbbProdirection;
                            }
                            else if (u.DisplayName == "电极类型")
                            {
                                cbb = cbbElecType;
                            }
                            else if (u.DisplayName == "摇摆方式")
                            {
                                cbb = cbbRock;
                            }
                            else if (u.DisplayName == "精公光洁度")
                            {
                                cbb = cbbFSmoth;
                            }
                            else if (u.DisplayName == "中公光洁度")
                            {
                                cbb = cbbMSmoth;
                            }
                            else if (u.DisplayName == "粗公光洁度")
                            {
                                cbb = cbbRSmoth;
                            }
                            else if (u.DisplayName == "夹具类型")
                            {
                                cbb = cbbChuckType;
                            }

                            if (cbb != null)
                            {
                                if (string.IsNullOrEmpty(cbb.Text))
                                {
                                    var selection = u.Selections.FirstOrDefault(f => f.IsDefault) ?? u.Selections.FirstOrDefault();
                                    if (selection != null)
                                    {
                                        cbb.Text = selection.Value;
                                    }
                                }
                            }
                        });
                    }
                    _curCuprum = item;
                }

            }
            else
            {
                dgvPositions.DataSource = null;
                _curCuprum = null;
            }

           
            RefreshUI();
        }

        ElecManage.MouldInfo _curSteel;
        void dgvSteels_SelectionChanged(object sender, EventArgs e)
        {
            //当前行
            var currentRow = dgvSteels.CurrentRow;
            if (currentRow != null && currentRow.Index >= 0)
            {
                var item = currentRow.DataBoundItem as ElecManage.MouldInfo;
                if (_curSteel != item)
                {
                    dgvCuprums.DataSource = EactBomBusiness.Instance.GetElecList(item, (s) => {
                        SplashScreen.Splasher.Status = string.Format("正在加载电极：{0}", s);
                    });
                }
                _curSteel = item;
            }
            else 
            {
                _curSteel = null;
                dgvCuprums.DataSource = null;
            }
        }


        void FrmEactBom_Shown(object sender, EventArgs e)
        {
            var list = EactBomBusiness.Instance.GetMouldInfo();
            dgvSteels.DataSource = list;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dgvSteels.CurrentRow == null) return;
            var steelInfo = dgvSteels.CurrentRow.DataBoundItem as ElecManage.MouldInfo;
            var positions = dgvPositions.DataSource as List<ElecManage.PositioningInfo>;
            var cuprums = dgvCuprums.DataSource as List<ViewElecInfo> ?? new List<ViewElecInfo>();
            if (steelInfo !=null&& cuprums.Where(u=>u.Checked).Count()>0)
            {
                SplashScreen.Splasher.Show(typeof(SplashScreen.FrmSplashScreen));
                //System.Threading.Thread.Sleep(800);
                SplashScreen.Splasher.Status = "正在导入EACT......";
                System.Threading.Thread.Sleep(800);
                try
                {
                    EactBomBusiness.Instance.ExportEact(cuprums, steelInfo, (s) => {
                        if (this.InvokeRequired)
                        {
                            this.Invoke(new Action(() => { SplashScreen.Splasher.Status = s; }));
                        }
                        else 
                        {
                            SplashScreen.Splasher.Status = s;
                        }
                    }, EactBomBusiness.Instance.ConfigData.ExportPrt, EactBomBusiness.Instance.ConfigData.ExportStp
                    , EactBomBusiness.Instance.ConfigData.ExportCNCPrt
                    );
                    SplashScreen.Splasher.Status = "导入完毕............";
                    System.Threading.Thread.Sleep(800);
                    SplashScreen.Splasher.Close();
                    MessageBox.Show("导入成功");
                    this.BringToFront();
                }
                catch(Exception ex)
                {
                    SplashScreen.Splasher.Close();
                    throw ex;
                }
            }
            else 
            {
                MessageBox.Show("未选择电极");
            }
        }
    }
}
