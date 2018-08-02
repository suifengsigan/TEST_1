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
    public partial class FrmMatchJiaju : Form
    {
        ConfigData.PopertySelection _ps;

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

        public FrmMatchJiaju(ConfigData.PopertySelection p) 
        {
            _ps = p;
            InitializeComponent();
            InitDgv(dataGridViewJSelection);
            InitEvent();
        }

        private void InitEvent()
        {
            this.Load += FrmMatchJiaju_Load;
            btnAdd.Click += BtnAdd_Click;
            btnDelete.Click += BtnDelete_Click;
            btnUpate.Click += BtnUpate_Click;
            dataGridViewJSelection.SelectionChanged += DataGridViewPSelection_SelectionChanged;
            this.FormClosing += FrmMatchJiaju_FormClosing;
        }

        private void FrmMatchJiaju_Load(object sender, EventArgs e)
        {
            lbJiajuName.Text = _ps.Value;
            var datasourcce = MatchJiaju.DeserializeObject(_ps.Ex1);
            dataGridViewJSelection.DataSource = datasourcce;
            txtJiajuMatchValue.Text = _ps.Ex2;
        }
  

        private void FrmMatchJiaju_FormClosing(object sender, FormClosingEventArgs e)
        {
            var datasourcce = dataGridViewJSelection.DataSource as List<MatchJiaju> ?? new List<MatchJiaju>();
            _ps.Ex1 = MatchJiaju.SerializeObject(datasourcce);
            _ps.Ex2 = MatchJiaju.GetDouble(txtJiajuMatchValue.Text).ToString();
        }

        private void DataGridViewPSelection_SelectionChanged(object sender, EventArgs e)
        {
            bool temp = dataGridViewJSelection.CurrentRow != null && dataGridViewJSelection.CurrentRow.Index >= 0;
            var datasourcce = dataGridViewJSelection.DataSource as List<MatchJiaju> ?? new List<MatchJiaju>();
            if (temp && datasourcce.Count > 0 && datasourcce.Count > dataGridViewJSelection.CurrentRow.Index)
            {
                var data = dataGridViewJSelection.CurrentRow.DataBoundItem as MatchJiaju;
                txtX.Text = data.X.ToString();
                txtY.Text = data.Y.ToString();
            }
        }

        private void BtnUpate_Click(object sender, EventArgs e)
        {
            double x = 0, y = 0;
            bool temp = dataGridViewJSelection.CurrentRow != null && dataGridViewJSelection.CurrentRow.Index >= 0;
            if (temp)
            {
                var data = dataGridViewJSelection.CurrentRow.DataBoundItem as MatchJiaju;
                if (double.TryParse(txtX.Text, out x) && double.TryParse(txtY.Text, out y))
                {
                    data.X = x;
                    data.Y = y;
                    dataGridViewJSelection.Refresh();
                }
                else
                {
                    MessageBox.Show("数据错误");
                }
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            bool temp = dataGridViewJSelection.CurrentRow != null && dataGridViewJSelection.CurrentRow.Index >= 0;
            if (temp)
            {
                var datasourcce = dataGridViewJSelection.DataSource as List<MatchJiaju> ?? new List<MatchJiaju>();
                var data = dataGridViewJSelection.CurrentRow.DataBoundItem as MatchJiaju;
                datasourcce.Remove(data);
                dataGridViewJSelection.DataSource = datasourcce.ToList();
                dataGridViewJSelection.Refresh();
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            double x = 0, y = 0;
            if (double.TryParse(txtX.Text, out x) && double.TryParse(txtY.Text, out y))
            {
                var datasourcce=dataGridViewJSelection.DataSource as List<MatchJiaju> ?? new List<MatchJiaju>();
                datasourcce.Add(new MatchJiaju { X=x, Y=y });
                dataGridViewJSelection.DataSource = datasourcce.ToList();
                dataGridViewJSelection.Refresh();
            }
            else
            {
                MessageBox.Show("数据错误");
            }
        }
    }

    public class MatchJiaju
    {
        [DisplayName("长")]
        public double X { get; set; }
        [DisplayName("宽")]
        public double Y { get; set; }

        public static List<MatchJiaju> DeserializeObject(string s)
        {
            var json = s;
            if (!string.IsNullOrEmpty(json))
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<MatchJiaju>>(json) ?? new List<MatchJiaju>();
            }
            return new List<MatchJiaju>();
        }

        public static string SerializeObject(List<MatchJiaju> data)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            return json;
        }

        public static double GetMatchJiajuValue(string ELEC_CLAMP_GENERAL_TYPE)
        {
            double matchJiajuValue = 0;
            var poperies = ConfigData.GetInstance().Poperties.FirstOrDefault(p => p.DisplayName == "夹具类型");
            if (poperies != null)
            {
                var selections = poperies.Selections.FirstOrDefault(s => s.Value == ELEC_CLAMP_GENERAL_TYPE);
                if (selections != null)
                {
                    matchJiajuValue = EactConfig.MatchJiaju.GetDouble(selections.Ex2);
                }
            }
            return matchJiajuValue;
        }

        public static double GetDouble(string str, double defaultValue = 0)
        {
            double.TryParse(str, out defaultValue);
            return defaultValue;
        }

        //public override string ToString()
        //{
        //    return string.Format("{0}*{1}", X,Y);
        //}
    }
}
