using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.AdvTree;
using DevComponents.DotNetBar;

namespace Demo1
{
    public partial class Form1 : Form
    {

        private Dictionary<string, string> _dicLayoutPanelData;

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _dicLayoutPanelData = new Dictionary<string, string>();
            _dicLayoutPanelData.Add("0,0", "闭环总数量");
            //_dicLayoutPanelData.Add("0,1", "150");
            //_dicLayoutPanelData.Add("0,2", "在用的小闭环数量");
            //_dicLayoutPanelData.Add("0,3", "220");
            //_dicLayoutPanelData.Add("0,4", "控制指令总数");
            //_dicLayoutPanelData.Add("0,6", "移动端发出控制指令");
            //_dicLayoutPanelData.Add("1,0", "小闭环总数量");
            //_dicLayoutPanelData.Add("1,2", "在用的大闭环数量");
            //_dicLayoutPanelData.Add("1,4", "程序发出控制指令");
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            try
            {
                int rowCount = int.Parse(txtRowCount.Text.Trim());
                int columnCount = int.Parse(txtColumnCount.Text.Trim());

                LoadLayOutPanel(pnlMain, rowCount, columnCount);
            }
            catch (Exception ex)
            {

            }
        }


        private void LoadLayOutPanel(Control parentCtrl, int rowCount, int columnCount)
        {
            parentCtrl.Controls.Clear();
            TableLayoutPanel layoutPanel = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                //一定要设置CellBorderStyle属性   不然运行后，看不到TableLayoutPanel控件
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
                ColumnCount = columnCount,
                RowCount = rowCount
            };
            layoutPanel.SuspendLayout();

            //每2列算1列    每2列的宽度百分比
            float perTwoColWidth = 100f / (columnCount / 2);
            for (int i = 0; i < columnCount; i++)
            {
                float width = perTwoColWidth * 0.8f;
                if (i % 2 != 0)
                    width = perTwoColWidth * 0.2f;
                ColumnStyle colStyle = new ColumnStyle(SizeType.Percent, width);
                layoutPanel.ColumnStyles.Add(colStyle);
            }

            //每行高度百分比
            float perRowHeight = 100F / rowCount;
            for (int i = 0; i < rowCount; i++)
            {
                layoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, perRowHeight));
            }

            RetrieveData(layoutPanel);

            layoutPanel.ResumeLayout(false);
            parentCtrl.Controls.Add(layoutPanel);
        }

        private void RetrieveData(TableLayoutPanel layoutPanel)
        {
            if (_dicLayoutPanelData.Count == 0)
                return;

            int rowCount = layoutPanel.RowCount;
            int columnCount = layoutPanel.ColumnCount;

            layoutPanel.Controls.Clear();
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    string key = string.Format("{0},{1}", i, j);
                    if (_dicLayoutPanelData.ContainsKey(key))
                    {
                        EditableLabel lbl = new EditableLabel(_dicLayoutPanelData[key]);
                        //LabelX lbl = new LabelX
                        //{
                        //    Text = _dicLayoutPanelData[key],
                        //    Dock = DockStyle.Fill,
                        //    TextAlignment = StringAlignment.Far
                        //};
                        layoutPanel.Controls.Add(lbl, j, i);
                    }
                }
            }
        }

        /// <summary>
        /// 再增加1行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddOneRow_Click(object sender, EventArgs e)
        {
            if (pnlMain.Controls.Count == 0)
                return;
            TableLayoutPanel pnl = pnlMain.Controls[0] as TableLayoutPanel;
            if (pnl == null)
                return;
            int count = pnl.RowCount;
            // pnl.RowCount = 0;
            pnl.RowCount = count + 1;

            //每行高度百分比
            float perRowHeight = 100F / pnl.RowCount;
            for (int i = 0; i < pnl.RowCount; i++)
            {
                pnl.RowStyles.Add(new RowStyle(SizeType.Percent, perRowHeight));
            }

            RetrieveData(pnl);
        }

        /// <summary>
        /// 再增加2列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddOneColumn_Click(object sender, EventArgs e)
        {
            if (pnlMain.Controls.Count == 0)
                return;
            TableLayoutPanel pnl = pnlMain.Controls[0] as TableLayoutPanel;
            if (pnl == null)
                return;
            int columnCount = pnl.ColumnCount;
            //pnl.ColumnCount = 0;
            pnl.ColumnCount = columnCount + 2;

            columnCount = pnl.ColumnCount;
            float perTwoColWidth = 100f / (columnCount / 2);
            for (int i = 0; i < columnCount; i++)
            {
                float width = perTwoColWidth * 0.6666f;
                if (i % 2 != 0)
                    width = perTwoColWidth * 0.3333f;
                ColumnStyle colStyle = new ColumnStyle(SizeType.Percent, width);
                pnl.ColumnStyles.Add(colStyle);
            }

            RetrieveData(pnl);
        }
    }
}
