namespace EactConfig
{
    partial class FrmConfig
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
            this.btnSave = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtDbLoginPass = new System.Windows.Forms.TextBox();
            this.txtDbLoginUser = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDatabasePass = new System.Windows.Forms.TextBox();
            this.txtDataBaseUser = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDatabaseIP = new System.Windows.Forms.TextBox();
            this.txtDatabaseName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtFtpPass = new System.Windows.Forms.TextBox();
            this.txtFtpUser = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFtpAddress = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridViewPSelection = new System.Windows.Forms.DataGridView();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnPSelectionDelete = new System.Windows.Forms.Button();
            this.btnPSelectionUpdate = new System.Windows.Forms.Button();
            this.btnPSelectionAdd = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridViewPoperty = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnPopertyDelete = new System.Windows.Forms.Button();
            this.btnPopertyUpate = new System.Windows.Forms.Button();
            this.btnPopertyAdd = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label13 = new System.Windows.Forms.Label();
            this.cbbCNCTransRule = new System.Windows.Forms.ComboBox();
            this.cbbExportCNC = new System.Windows.Forms.CheckBox();
            this.cbbEdmTransRule = new System.Windows.Forms.ComboBox();
            this.btnSetPrtColor = new System.Windows.Forms.Button();
            this.cbbElecNameRule = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.rbCanPUpdate = new System.Windows.Forms.CheckBox();
            this.cbShareElec = new System.Windows.Forms.CheckBox();
            this.cbExportStp = new System.Windows.Forms.CheckBox();
            this.cbExportCMM = new System.Windows.Forms.CheckBox();
            this.cbbLicenseType = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cbbQuadrantType = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbImportEman = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPSelection)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPoperty)).BeginInit();
            this.panel4.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 381);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(719, 30);
            this.panel1.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(591, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(719, 381);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cbImportEman);
            this.tabPage1.Controls.Add(this.txtDbLoginPass);
            this.tabPage1.Controls.Add(this.txtDbLoginUser);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.txtDatabasePass);
            this.tabPage1.Controls.Add(this.txtDataBaseUser);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.txtDatabaseIP);
            this.tabPage1.Controls.Add(this.txtDatabaseName);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(711, 355);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "数据库配置";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtDbLoginPass
            // 
            this.txtDbLoginPass.Location = new System.Drawing.Point(118, 232);
            this.txtDbLoginPass.Name = "txtDbLoginPass";
            this.txtDbLoginPass.Size = new System.Drawing.Size(372, 21);
            this.txtDbLoginPass.TabIndex = 11;
            // 
            // txtDbLoginUser
            // 
            this.txtDbLoginUser.Location = new System.Drawing.Point(118, 192);
            this.txtDbLoginUser.Name = "txtDbLoginUser";
            this.txtDbLoginUser.Size = new System.Drawing.Size(372, 21);
            this.txtDbLoginUser.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(31, 235);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 9;
            this.label8.Text = "登录密码";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(31, 195);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 8;
            this.label9.Text = "登录用户名";
            // 
            // txtDatabasePass
            // 
            this.txtDatabasePass.Location = new System.Drawing.Point(118, 147);
            this.txtDatabasePass.Name = "txtDatabasePass";
            this.txtDatabasePass.Size = new System.Drawing.Size(372, 21);
            this.txtDatabasePass.TabIndex = 7;
            // 
            // txtDataBaseUser
            // 
            this.txtDataBaseUser.Location = new System.Drawing.Point(118, 107);
            this.txtDataBaseUser.Name = "txtDataBaseUser";
            this.txtDataBaseUser.Size = new System.Drawing.Size(372, 21);
            this.txtDataBaseUser.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "服务器密码";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "服务器账号";
            // 
            // txtDatabaseIP
            // 
            this.txtDatabaseIP.Location = new System.Drawing.Point(118, 64);
            this.txtDatabaseIP.Name = "txtDatabaseIP";
            this.txtDatabaseIP.Size = new System.Drawing.Size(372, 21);
            this.txtDatabaseIP.TabIndex = 3;
            // 
            // txtDatabaseName
            // 
            this.txtDatabaseName.Location = new System.Drawing.Point(118, 24);
            this.txtDatabaseName.Name = "txtDatabaseName";
            this.txtDatabaseName.Size = new System.Drawing.Size(372, 21);
            this.txtDatabaseName.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "服务器地址";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据库名称";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtFtpPass);
            this.tabPage2.Controls.Add(this.txtFtpUser);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.txtFtpAddress);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(711, 355);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "FTP配置";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtFtpPass
            // 
            this.txtFtpPass.Location = new System.Drawing.Point(127, 122);
            this.txtFtpPass.Name = "txtFtpPass";
            this.txtFtpPass.Size = new System.Drawing.Size(372, 21);
            this.txtFtpPass.TabIndex = 15;
            // 
            // txtFtpUser
            // 
            this.txtFtpUser.Location = new System.Drawing.Point(127, 82);
            this.txtFtpUser.Name = "txtFtpUser";
            this.txtFtpUser.Size = new System.Drawing.Size(372, 21);
            this.txtFtpUser.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(40, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "密码";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(40, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "用户名";
            // 
            // txtFtpAddress
            // 
            this.txtFtpAddress.Location = new System.Drawing.Point(127, 39);
            this.txtFtpAddress.Name = "txtFtpAddress";
            this.txtFtpAddress.Size = new System.Drawing.Size(372, 21);
            this.txtFtpAddress.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(40, 42);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "服务器地址";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.panel3);
            this.tabPage3.Controls.Add(this.panel2);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(711, 355);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "电极属性配置";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(439, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(269, 349);
            this.panel3.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridViewPSelection);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(269, 313);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "选项";
            // 
            // dataGridViewPSelection
            // 
            this.dataGridViewPSelection.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPSelection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewPSelection.Location = new System.Drawing.Point(3, 17);
            this.dataGridViewPSelection.Name = "dataGridViewPSelection";
            this.dataGridViewPSelection.RowTemplate.Height = 23;
            this.dataGridViewPSelection.Size = new System.Drawing.Size(263, 293);
            this.dataGridViewPSelection.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnPSelectionDelete);
            this.panel5.Controls.Add(this.btnPSelectionUpdate);
            this.panel5.Controls.Add(this.btnPSelectionAdd);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 313);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(269, 36);
            this.panel5.TabIndex = 0;
            // 
            // btnPSelectionDelete
            // 
            this.btnPSelectionDelete.Location = new System.Drawing.Point(194, 7);
            this.btnPSelectionDelete.Name = "btnPSelectionDelete";
            this.btnPSelectionDelete.Size = new System.Drawing.Size(47, 23);
            this.btnPSelectionDelete.TabIndex = 6;
            this.btnPSelectionDelete.Text = "删除";
            this.btnPSelectionDelete.UseVisualStyleBackColor = true;
            // 
            // btnPSelectionUpdate
            // 
            this.btnPSelectionUpdate.Location = new System.Drawing.Point(107, 7);
            this.btnPSelectionUpdate.Name = "btnPSelectionUpdate";
            this.btnPSelectionUpdate.Size = new System.Drawing.Size(44, 23);
            this.btnPSelectionUpdate.TabIndex = 4;
            this.btnPSelectionUpdate.Text = "修改";
            this.btnPSelectionUpdate.UseVisualStyleBackColor = true;
            // 
            // btnPSelectionAdd
            // 
            this.btnPSelectionAdd.Location = new System.Drawing.Point(18, 7);
            this.btnPSelectionAdd.Name = "btnPSelectionAdd";
            this.btnPSelectionAdd.Size = new System.Drawing.Size(43, 23);
            this.btnPSelectionAdd.TabIndex = 3;
            this.btnPSelectionAdd.Text = "新增";
            this.btnPSelectionAdd.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(436, 349);
            this.panel2.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridViewPoperty);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(436, 313);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "属性列表";
            // 
            // dataGridViewPoperty
            // 
            this.dataGridViewPoperty.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPoperty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewPoperty.Location = new System.Drawing.Point(3, 17);
            this.dataGridViewPoperty.Name = "dataGridViewPoperty";
            this.dataGridViewPoperty.RowTemplate.Height = 23;
            this.dataGridViewPoperty.Size = new System.Drawing.Size(430, 293);
            this.dataGridViewPoperty.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnPopertyDelete);
            this.panel4.Controls.Add(this.btnPopertyUpate);
            this.panel4.Controls.Add(this.btnPopertyAdd);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 313);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(436, 36);
            this.panel4.TabIndex = 0;
            // 
            // btnPopertyDelete
            // 
            this.btnPopertyDelete.Location = new System.Drawing.Point(306, 7);
            this.btnPopertyDelete.Name = "btnPopertyDelete";
            this.btnPopertyDelete.Size = new System.Drawing.Size(47, 23);
            this.btnPopertyDelete.TabIndex = 2;
            this.btnPopertyDelete.Text = "删除";
            this.btnPopertyDelete.UseVisualStyleBackColor = true;
            // 
            // btnPopertyUpate
            // 
            this.btnPopertyUpate.Location = new System.Drawing.Point(192, 7);
            this.btnPopertyUpate.Name = "btnPopertyUpate";
            this.btnPopertyUpate.Size = new System.Drawing.Size(44, 23);
            this.btnPopertyUpate.TabIndex = 1;
            this.btnPopertyUpate.Text = "修改";
            this.btnPopertyUpate.UseVisualStyleBackColor = true;
            // 
            // btnPopertyAdd
            // 
            this.btnPopertyAdd.Location = new System.Drawing.Point(75, 7);
            this.btnPopertyAdd.Name = "btnPopertyAdd";
            this.btnPopertyAdd.Size = new System.Drawing.Size(43, 23);
            this.btnPopertyAdd.TabIndex = 0;
            this.btnPopertyAdd.Text = "新增";
            this.btnPopertyAdd.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.label13);
            this.tabPage4.Controls.Add(this.cbbCNCTransRule);
            this.tabPage4.Controls.Add(this.cbbExportCNC);
            this.tabPage4.Controls.Add(this.cbbEdmTransRule);
            this.tabPage4.Controls.Add(this.btnSetPrtColor);
            this.tabPage4.Controls.Add(this.cbbElecNameRule);
            this.tabPage4.Controls.Add(this.label12);
            this.tabPage4.Controls.Add(this.rbCanPUpdate);
            this.tabPage4.Controls.Add(this.cbShareElec);
            this.tabPage4.Controls.Add(this.cbExportStp);
            this.tabPage4.Controls.Add(this.cbExportCMM);
            this.tabPage4.Controls.Add(this.cbbLicenseType);
            this.tabPage4.Controls.Add(this.label11);
            this.tabPage4.Controls.Add(this.cbbQuadrantType);
            this.tabPage4.Controls.Add(this.label10);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(711, 355);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "基本配置";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(329, 162);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(47, 12);
            this.label13.TabIndex = 47;
            this.label13.Text = "label13";
            // 
            // cbbCNCTransRule
            // 
            this.cbbCNCTransRule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbCNCTransRule.FormattingEnabled = true;
            this.cbbCNCTransRule.Location = new System.Drawing.Point(171, 195);
            this.cbbCNCTransRule.Name = "cbbCNCTransRule";
            this.cbbCNCTransRule.Size = new System.Drawing.Size(392, 20);
            this.cbbCNCTransRule.TabIndex = 46;
            // 
            // cbbExportCNC
            // 
            this.cbbExportCNC.AutoSize = true;
            this.cbbExportCNC.Location = new System.Drawing.Point(80, 197);
            this.cbbExportCNC.Name = "cbbExportCNC";
            this.cbbExportCNC.Size = new System.Drawing.Size(66, 16);
            this.cbbExportCNC.TabIndex = 45;
            this.cbbExportCNC.Text = "CNC图档";
            this.cbbExportCNC.UseVisualStyleBackColor = true;
            // 
            // cbbEdmTransRule
            // 
            this.cbbEdmTransRule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbEdmTransRule.FormattingEnabled = true;
            this.cbbEdmTransRule.Location = new System.Drawing.Point(407, 157);
            this.cbbEdmTransRule.Name = "cbbEdmTransRule";
            this.cbbEdmTransRule.Size = new System.Drawing.Size(156, 20);
            this.cbbEdmTransRule.TabIndex = 44;
            // 
            // btnSetPrtColor
            // 
            this.btnSetPrtColor.Location = new System.Drawing.Point(171, 157);
            this.btnSetPrtColor.Name = "btnSetPrtColor";
            this.btnSetPrtColor.Size = new System.Drawing.Size(130, 23);
            this.btnSetPrtColor.TabIndex = 43;
            this.btnSetPrtColor.Text = "设置电打面颜色";
            this.btnSetPrtColor.UseVisualStyleBackColor = true;
            this.btnSetPrtColor.Click += new System.EventHandler(this.btnSetPrtColor_Click);
            // 
            // cbbElecNameRule
            // 
            this.cbbElecNameRule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbElecNameRule.FormattingEnabled = true;
            this.cbbElecNameRule.Location = new System.Drawing.Point(171, 119);
            this.cbbElecNameRule.Name = "cbbElecNameRule";
            this.cbbElecNameRule.Size = new System.Drawing.Size(392, 20);
            this.cbbElecNameRule.TabIndex = 42;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(78, 122);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 12);
            this.label12.TabIndex = 41;
            this.label12.Text = "电极名称规则";
            // 
            // rbCanPUpdate
            // 
            this.rbCanPUpdate.AutoSize = true;
            this.rbCanPUpdate.Location = new System.Drawing.Point(80, 299);
            this.rbCanPUpdate.Name = "rbCanPUpdate";
            this.rbCanPUpdate.Size = new System.Drawing.Size(72, 16);
            this.rbCanPUpdate.TabIndex = 40;
            this.rbCanPUpdate.Text = "属性修改";
            this.rbCanPUpdate.UseVisualStyleBackColor = true;
            // 
            // cbShareElec
            // 
            this.cbShareElec.AutoSize = true;
            this.cbShareElec.Location = new System.Drawing.Point(80, 266);
            this.cbShareElec.Name = "cbShareElec";
            this.cbShareElec.Size = new System.Drawing.Size(72, 16);
            this.cbShareElec.TabIndex = 39;
            this.cbShareElec.Text = "共用电极";
            this.cbShareElec.UseVisualStyleBackColor = true;
            // 
            // cbExportStp
            // 
            this.cbExportStp.AutoSize = true;
            this.cbExportStp.Location = new System.Drawing.Point(80, 230);
            this.cbExportStp.Name = "cbExportStp";
            this.cbExportStp.Size = new System.Drawing.Size(42, 16);
            this.cbExportStp.TabIndex = 38;
            this.cbExportStp.Text = "Stp";
            this.cbExportStp.UseVisualStyleBackColor = true;
            // 
            // cbExportCMM
            // 
            this.cbExportCMM.AutoSize = true;
            this.cbExportCMM.Location = new System.Drawing.Point(80, 161);
            this.cbExportCMM.Name = "cbExportCMM";
            this.cbExportCMM.Size = new System.Drawing.Size(66, 16);
            this.cbExportCMM.TabIndex = 37;
            this.cbExportCMM.Text = "CMM图档";
            this.cbExportCMM.UseVisualStyleBackColor = true;
            // 
            // cbbLicenseType
            // 
            this.cbbLicenseType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbLicenseType.FormattingEnabled = true;
            this.cbbLicenseType.Location = new System.Drawing.Point(171, 75);
            this.cbbLicenseType.Name = "cbbLicenseType";
            this.cbbLicenseType.Size = new System.Drawing.Size(392, 20);
            this.cbbLicenseType.TabIndex = 30;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(78, 78);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 29;
            this.label11.Text = "加密狗类型";
            // 
            // cbbQuadrantType
            // 
            this.cbbQuadrantType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbQuadrantType.FormattingEnabled = true;
            this.cbbQuadrantType.Location = new System.Drawing.Point(171, 31);
            this.cbbQuadrantType.Name = "cbbQuadrantType";
            this.cbbQuadrantType.Size = new System.Drawing.Size(392, 20);
            this.cbbQuadrantType.TabIndex = 28;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(78, 34);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 0;
            this.label10.Text = "默认象限角";
            // 
            // cbImportEman
            // 
            this.cbImportEman.AutoSize = true;
            this.cbImportEman.Location = new System.Drawing.Point(33, 274);
            this.cbImportEman.Name = "cbImportEman";
            this.cbImportEman.Size = new System.Drawing.Size(72, 16);
            this.cbImportEman.TabIndex = 38;
            this.cbImportEman.Text = "导入EMan";
            this.cbImportEman.UseVisualStyleBackColor = true;
            // 
            // FrmConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 411);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Name = "FrmConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "配置工具";
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPSelection)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPoperty)).EndInit();
            this.panel4.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtDatabaseIP;
        private System.Windows.Forms.TextBox txtDatabaseName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtDatabasePass;
        private System.Windows.Forms.TextBox txtDataBaseUser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFtpPass;
        private System.Windows.Forms.TextBox txtFtpUser;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtFtpAddress;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridViewPoperty;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnPopertyUpate;
        private System.Windows.Forms.Button btnPopertyAdd;
        private System.Windows.Forms.Button btnPSelectionUpdate;
        private System.Windows.Forms.Button btnPSelectionAdd;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridViewPSelection;
        private System.Windows.Forms.Button btnPopertyDelete;
        private System.Windows.Forms.TextBox txtDbLoginPass;
        private System.Windows.Forms.TextBox txtDbLoginUser;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnPSelectionDelete;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbbQuadrantType;
        private System.Windows.Forms.ComboBox cbbLicenseType;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox cbExportStp;
        private System.Windows.Forms.CheckBox cbExportCMM;
        private System.Windows.Forms.CheckBox cbShareElec;
        private System.Windows.Forms.CheckBox rbCanPUpdate;
        private System.Windows.Forms.ComboBox cbbElecNameRule;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnSetPrtColor;
        private System.Windows.Forms.ComboBox cbbEdmTransRule;
        private System.Windows.Forms.ComboBox cbbCNCTransRule;
        private System.Windows.Forms.CheckBox cbbExportCNC;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox cbImportEman;
    }
}