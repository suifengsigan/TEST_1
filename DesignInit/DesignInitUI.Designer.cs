namespace DesignInit
{
    partial class DesignInitUI
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
            this.txtSteelName = new DevExpress.XtraEditors.TextEdit();
            this.btnSelectSteel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtSteelName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSteelName
            // 
            this.txtSteelName.Location = new System.Drawing.Point(202, 15);
            this.txtSteelName.Name = "txtSteelName";
            this.txtSteelName.Size = new System.Drawing.Size(222, 20);
            this.txtSteelName.TabIndex = 2;
            // 
            // btnSelectSteel
            // 
            this.btnSelectSteel.Location = new System.Drawing.Point(15, 12);
            this.btnSelectSteel.Name = "btnSelectSteel";
            this.btnSelectSteel.Size = new System.Drawing.Size(147, 27);
            this.btnSelectSteel.TabIndex = 3;
            this.btnSelectSteel.Text = "选择钢件";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(351, 191);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(73, 28);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.txtSteelName);
            this.panelControl1.Controls.Add(this.btnOK);
            this.panelControl1.Controls.Add(this.btnSelectSteel);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(459, 224);
            this.panelControl1.TabIndex = 5;
            // 
            // DesignInitUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 224);
            this.Controls.Add(this.panelControl1);
            this.Name = "DesignInitUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TestEx";
            ((System.ComponentModel.ISupportInitialize)(this.txtSteelName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtSteelName;
        private DevExpress.XtraEditors.SimpleButton btnSelectSteel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.PanelControl panelControl1;
    }
}