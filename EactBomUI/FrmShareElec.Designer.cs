namespace EactBom
{
    partial class FrmShareElec
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.cbbJH = new System.Windows.Forms.ComboBox();
            this.cbbMH = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.cbbJH);
            this.panel1.Controls.Add(this.cbbMH);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(623, 46);
            this.panel1.TabIndex = 2;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(273, 15);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(29, 12);
            this.label15.TabIndex = 73;
            this.label15.Text = "件号";
            // 
            // cbbJH
            // 
            this.cbbJH.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbJH.FormattingEnabled = true;
            this.cbbJH.Location = new System.Drawing.Point(344, 12);
            this.cbbJH.Name = "cbbJH";
            this.cbbJH.Size = new System.Drawing.Size(100, 20);
            this.cbbJH.TabIndex = 72;
            // 
            // cbbMH
            // 
            this.cbbMH.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbMH.FormattingEnabled = true;
            this.cbbMH.Location = new System.Drawing.Point(124, 12);
            this.cbbMH.Name = "cbbMH";
            this.cbbMH.Size = new System.Drawing.Size(100, 20);
            this.cbbMH.TabIndex = 71;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(53, 15);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(29, 12);
            this.label14.TabIndex = 70;
            this.label14.Text = "模号";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnOK);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 239);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(623, 35);
            this.panel2.TabIndex = 3;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(524, 6);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 46);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(623, 193);
            this.dataGridView1.TabIndex = 4;
            // 
            // FrmShareElec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 274);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FrmShareElec";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置共用电极";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cbbJH;
        private System.Windows.Forms.ComboBox cbbMH;
        private System.Windows.Forms.Label label14;
    }
}