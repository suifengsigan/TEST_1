namespace Demo1
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlTop = new DevComponents.DotNetBar.PanelEx();
            this.btnAddOneColumn = new DevComponents.DotNetBar.ButtonX();
            this.btnAddOneRow = new DevComponents.DotNetBar.ButtonX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtColumnCount = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.txtRowCount = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.pnlMain = new DevComponents.DotNetBar.PanelEx();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.CanvasColor = System.Drawing.SystemColors.Control;
            this.pnlTop.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.pnlTop.Controls.Add(this.btnAddOneColumn);
            this.pnlTop.Controls.Add(this.btnAddOneRow);
            this.pnlTop.Controls.Add(this.labelX2);
            this.pnlTop.Controls.Add(this.txtColumnCount);
            this.pnlTop.Controls.Add(this.labelX1);
            this.pnlTop.Controls.Add(this.buttonX1);
            this.pnlTop.Controls.Add(this.txtRowCount);
            this.pnlTop.DisabledBackColor = System.Drawing.Color.Empty;
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(704, 47);
            this.pnlTop.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pnlTop.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.pnlTop.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.pnlTop.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pnlTop.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pnlTop.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pnlTop.Style.GradientAngle = 90;
            this.pnlTop.TabIndex = 0;
            // 
            // btnAddOneColumn
            // 
            this.btnAddOneColumn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAddOneColumn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnAddOneColumn.Location = new System.Drawing.Point(434, 11);
            this.btnAddOneColumn.Name = "btnAddOneColumn";
            this.btnAddOneColumn.Size = new System.Drawing.Size(75, 23);
            this.btnAddOneColumn.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnAddOneColumn.TabIndex = 6;
            this.btnAddOneColumn.Text = "再增加2列";
            this.btnAddOneColumn.Click += new System.EventHandler(this.btnAddOneColumn_Click);
            // 
            // btnAddOneRow
            // 
            this.btnAddOneRow.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAddOneRow.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnAddOneRow.Location = new System.Drawing.Point(344, 11);
            this.btnAddOneRow.Name = "btnAddOneRow";
            this.btnAddOneRow.Size = new System.Drawing.Size(75, 23);
            this.btnAddOneRow.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnAddOneRow.TabIndex = 5;
            this.btnAddOneRow.Text = "再增加1行";
            this.btnAddOneRow.Click += new System.EventHandler(this.btnAddOneRow_Click);
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(287, 14);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(19, 18);
            this.labelX2.TabIndex = 4;
            this.labelX2.Text = "列";
            // 
            // txtColumnCount
            // 
            // 
            // 
            // 
            this.txtColumnCount.Border.Class = "TextBoxBorder";
            this.txtColumnCount.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtColumnCount.Location = new System.Drawing.Point(182, 13);
            this.txtColumnCount.Name = "txtColumnCount";
            this.txtColumnCount.PreventEnterBeep = true;
            this.txtColumnCount.Size = new System.Drawing.Size(100, 21);
            this.txtColumnCount.TabIndex = 3;
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(146, 14);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(19, 18);
            this.labelX1.TabIndex = 2;
            this.labelX1.Text = "行";
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(605, 13);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(75, 23);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 1;
            this.buttonX1.Text = "创建";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // txtRowCount
            // 
            // 
            // 
            // 
            this.txtRowCount.Border.Class = "TextBoxBorder";
            this.txtRowCount.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtRowCount.Location = new System.Drawing.Point(41, 13);
            this.txtRowCount.Name = "txtRowCount";
            this.txtRowCount.PreventEnterBeep = true;
            this.txtRowCount.Size = new System.Drawing.Size(100, 21);
            this.txtRowCount.TabIndex = 0;
            // 
            // pnlMain
            // 
            this.pnlMain.CanvasColor = System.Drawing.SystemColors.Control;
            this.pnlMain.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.pnlMain.DisabledBackColor = System.Drawing.Color.Empty;
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 47);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(704, 235);
            this.pnlMain.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pnlMain.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.pnlMain.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.pnlMain.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pnlMain.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pnlMain.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pnlMain.Style.GradientAngle = 90;
            this.pnlMain.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 282);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlTop);
            this.Name = "Form1";
            this.Text = "Form1";
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx pnlTop;
        private DevComponents.DotNetBar.PanelEx pnlMain;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtRowCount;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtColumnCount;
        private DevComponents.DotNetBar.ButtonX btnAddOneColumn;
        private DevComponents.DotNetBar.ButtonX btnAddOneRow;
    }
}

