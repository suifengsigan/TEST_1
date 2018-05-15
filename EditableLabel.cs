using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo1
{
    /// <summary>
    /// 可即时编辑Label
    /// </summary>
    public partial class EditableLabel : UserControl
    {
        private string _context;
        public EditableLabel(string context)
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            _context = context;
            this.Load += EditableLabel_Load;
        }

        private void EditableLabel_Load(object sender, EventArgs e)
        {
            Bitmap bgImg = new Bitmap(this.Width, this.Height);
            this.BackgroundImage = bgImg;
            RetrieveDrawCtrl();
        }

        private void RetrieveDrawCtrl()
        {
            Image bgImg = this.BackgroundImage;
            Graphics g = Graphics.FromImage(bgImg);
            g.Clear(this.BackColor);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            Draw(g);
            g.Dispose();
        }

        private void Draw(Graphics g)
        {
            //设置文本的字体+颜色
            SolidBrush fontbrush = new SolidBrush(SystemColors.ControlText);
            Font font = new Font("宋体", 9f);

            SizeF size = GetLblSize(_context, font);
            float locationX = this.Width - size.Width;
            float locationY = this.Height / 2 - size.Height / 2;
            PointF p = new PointF(locationX, locationY);
            //绘制文字
            g.DrawString(_context, font, fontbrush, p);
        }

        /// <summary>
        /// 获取文本大小
        /// </summary>
        /// <param name="txtName"></param>
        /// <param name="font"></param>
        /// <returns></returns>
        private SizeF GetLblSize(string txtName, Font font)
        {
            Label lbl = new Label();
            Graphics g = Graphics.FromHwnd(lbl.Handle);
            SizeF sizef = g.MeasureString(txtName, font);
            g.Dispose();
            lbl.Dispose();
            return sizef;
        }
    }
}
