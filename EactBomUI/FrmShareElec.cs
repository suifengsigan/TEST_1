using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EactBom
{
    public partial class FrmShareElec : Form
    {
        private FrmShareElec()
        {
            InitializeComponent();
            Snap.UI.WinForm.SetApplicationIcon(this);
            Snap.UI.WinForm.ReparentForm(this);
            InitEvent();
            InitUI();
        }

        List<DataAccess.Model.EACT_CUPRUM> _dataSource = new List<DataAccess.Model.EACT_CUPRUM>();
        Action<DataAccess.Model.EACT_CUPRUM> _action = null;

        public FrmShareElec(List<DataAccess.Model.EACT_CUPRUM> cuprums,Action<DataAccess.Model.EACT_CUPRUM> action=null)
            : this()
        {
            _dataSource = cuprums;
            _action = action;
            dataGridView1.DataSource = new List<ShareElecInfo>();
            var mhStrs = new List<string>();
            cuprums.ForEach(u =>
            {
                var sn = u.STEELMODELSN;
                if (!string.IsNullOrEmpty(sn) && !mhStrs.Contains(sn))
                {
                    mhStrs.Add(sn);
                }
            });
            mhStrs.ForEach(u => { cbbMH.Items.Add(new ComboBoxItem() { Text = u, Value = u }); });

            if (mhStrs.Count > 0) { cbbMH.Text = mhStrs.FirstOrDefault(); }
        }

        void InitUI() 
        {
            InitDgv(dataGridView1);

            //dataGridView1.ReadOnly = false;
            //var ChCol = new DataGridViewCheckBoxColumn();
            //ChCol.HeaderText = "选择";
            //ChCol.Width = 40;
            //ChCol.DataPropertyName = "Checked";
            //ChCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //dataGridView1.Columns.Add(ChCol);
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

        void InitEvent() 
        {
            cbbMH.SelectedValueChanged += cbbMH_SelectedValueChanged;
            cbbJH.SelectedValueChanged += cbbJH_SelectedValueChanged;
        }

        void cbbJH_SelectedValueChanged(object sender, EventArgs e)
        {
            var infos = new List<ShareElecInfo>();

            _dataSource.Where(u => u.STEELMODELSN == cbbMH.Text && u.STEELMODULESN == cbbJH.Text).ToList().ForEach(u => {
                infos.Add(new ShareElecInfo { EACT_CUPRUM = u });
            });
            dataGridView1.DataSource = infos;
        }

        void cbbMH_SelectedValueChanged(object sender, EventArgs e)
        {
            var jhStrs = new List<string>();
            cbbJH.Items.Clear();
            _dataSource.Where(u => u.STEELMODELSN == cbbMH.Text).ToList().ForEach(u =>
            {
                var sn = u.STEELMODULESN;
                if (!string.IsNullOrEmpty(sn) && !jhStrs.Contains(sn))
                {
                    jhStrs.Add(sn);
                }
            });

            jhStrs.ForEach(u => { cbbJH.Items.Add(new ComboBoxItem() { Text = u, Value = u }); });

            if (jhStrs.Count > 0) { cbbJH.Text = jhStrs.FirstOrDefault(); }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.DataBoundItem is ShareElecInfo)
            {
                if (_action != null) 
                {
                    _action((dataGridView1.CurrentRow.DataBoundItem as ShareElecInfo).EACT_CUPRUM);
                }
                this.Close();
            }
            else 
            {
                MessageBox.Show("未选择电极");
            }
        }
    }
}
