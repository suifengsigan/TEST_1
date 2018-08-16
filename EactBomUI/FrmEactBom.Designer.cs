namespace EactBom
{
    partial class FrmEactBom
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvCuprums = new System.Windows.Forms.DataGridView();
            this.panel6 = new System.Windows.Forms.Panel();
            this.labelCuprumNum = new System.Windows.Forms.Label();
            this.ckCuprumSelectAll = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvSteels = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.dgvPositions = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBoxElecInfo = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cbbChuckType = new System.Windows.Forms.ComboBox();
            this.cbbRock = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cbbProdirection = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cbbElecType = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtElecSize = new System.Windows.Forms.TextBox();
            this.cbbRSmoth = new System.Windows.Forms.ComboBox();
            this.cbbMSmoth = new System.Windows.Forms.ComboBox();
            this.cbbFSmoth = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label333333 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtROUGH_SPACE = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtROUGH_NUMBER = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMIDDLE_SPACE = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMIDDLE_NUMBER = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFINISH_SPACE = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFINISH_NUMBER = new System.Windows.Forms.TextBox();
            this.cboxMAT_NAME = new System.Windows.Forms.ComboBox();
            this.groupShareElec = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.cboxR_MAT_NAME = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.cboxM_MAT_NAME = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCuprums)).BeginInit();
            this.panel6.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSteels)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPositions)).BeginInit();
            this.panel4.SuspendLayout();
            this.groupBoxElecInfo.SuspendLayout();
            this.groupShareElec.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(239, 550);
            this.panel1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvCuprums);
            this.groupBox2.Controls.Add(this.panel6);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 100);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(239, 450);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "电极列表";
            // 
            // dgvCuprums
            // 
            this.dgvCuprums.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCuprums.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCuprums.Location = new System.Drawing.Point(3, 46);
            this.dgvCuprums.Name = "dgvCuprums";
            this.dgvCuprums.RowTemplate.Height = 23;
            this.dgvCuprums.Size = new System.Drawing.Size(233, 401);
            this.dgvCuprums.TabIndex = 0;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.labelCuprumNum);
            this.panel6.Controls.Add(this.ckCuprumSelectAll);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(3, 17);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(233, 29);
            this.panel6.TabIndex = 1;
            // 
            // labelCuprumNum
            // 
            this.labelCuprumNum.AutoSize = true;
            this.labelCuprumNum.Location = new System.Drawing.Point(70, 9);
            this.labelCuprumNum.Name = "labelCuprumNum";
            this.labelCuprumNum.Size = new System.Drawing.Size(77, 12);
            this.labelCuprumNum.TabIndex = 2;
            this.labelCuprumNum.Text = "电极总数量：";
            // 
            // ckCuprumSelectAll
            // 
            this.ckCuprumSelectAll.AutoSize = true;
            this.ckCuprumSelectAll.Checked = true;
            this.ckCuprumSelectAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckCuprumSelectAll.Location = new System.Drawing.Point(9, 9);
            this.ckCuprumSelectAll.Name = "ckCuprumSelectAll";
            this.ckCuprumSelectAll.Size = new System.Drawing.Size(15, 14);
            this.ckCuprumSelectAll.TabIndex = 1;
            this.ckCuprumSelectAll.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvSteels);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(239, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "模仁信息";
            // 
            // dgvSteels
            // 
            this.dgvSteels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSteels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSteels.Location = new System.Drawing.Point(3, 17);
            this.dgvSteels.Name = "dgvSteels";
            this.dgvSteels.RowTemplate.Height = 23;
            this.dgvSteels.Size = new System.Drawing.Size(233, 80);
            this.dgvSteels.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(239, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(649, 550);
            this.panel2.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.dgvPositions);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 384);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(649, 127);
            this.panel5.TabIndex = 5;
            // 
            // dgvPositions
            // 
            this.dgvPositions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPositions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPositions.Location = new System.Drawing.Point(0, 0);
            this.dgvPositions.Name = "dgvPositions";
            this.dgvPositions.RowTemplate.Height = 23;
            this.dgvPositions.Size = new System.Drawing.Size(649, 127);
            this.dgvPositions.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.groupBoxElecInfo);
            this.panel4.Controls.Add(this.groupShareElec);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(649, 384);
            this.panel4.TabIndex = 4;
            // 
            // groupBoxElecInfo
            // 
            this.groupBoxElecInfo.Controls.Add(this.label16);
            this.groupBoxElecInfo.Controls.Add(this.cboxM_MAT_NAME);
            this.groupBoxElecInfo.Controls.Add(this.label9);
            this.groupBoxElecInfo.Controls.Add(this.cboxR_MAT_NAME);
            this.groupBoxElecInfo.Controls.Add(this.label15);
            this.groupBoxElecInfo.Controls.Add(this.cbbChuckType);
            this.groupBoxElecInfo.Controls.Add(this.cbbRock);
            this.groupBoxElecInfo.Controls.Add(this.label14);
            this.groupBoxElecInfo.Controls.Add(this.cbbProdirection);
            this.groupBoxElecInfo.Controls.Add(this.label13);
            this.groupBoxElecInfo.Controls.Add(this.label12);
            this.groupBoxElecInfo.Controls.Add(this.cbbElecType);
            this.groupBoxElecInfo.Controls.Add(this.label11);
            this.groupBoxElecInfo.Controls.Add(this.txtElecSize);
            this.groupBoxElecInfo.Controls.Add(this.cbbRSmoth);
            this.groupBoxElecInfo.Controls.Add(this.cbbMSmoth);
            this.groupBoxElecInfo.Controls.Add(this.cbbFSmoth);
            this.groupBoxElecInfo.Controls.Add(this.label8);
            this.groupBoxElecInfo.Controls.Add(this.label333333);
            this.groupBoxElecInfo.Controls.Add(this.label10);
            this.groupBoxElecInfo.Controls.Add(this.label7);
            this.groupBoxElecInfo.Controls.Add(this.label5);
            this.groupBoxElecInfo.Controls.Add(this.txtROUGH_SPACE);
            this.groupBoxElecInfo.Controls.Add(this.label6);
            this.groupBoxElecInfo.Controls.Add(this.txtROUGH_NUMBER);
            this.groupBoxElecInfo.Controls.Add(this.label2);
            this.groupBoxElecInfo.Controls.Add(this.txtMIDDLE_SPACE);
            this.groupBoxElecInfo.Controls.Add(this.label4);
            this.groupBoxElecInfo.Controls.Add(this.txtMIDDLE_NUMBER);
            this.groupBoxElecInfo.Controls.Add(this.label3);
            this.groupBoxElecInfo.Controls.Add(this.txtFINISH_SPACE);
            this.groupBoxElecInfo.Controls.Add(this.label1);
            this.groupBoxElecInfo.Controls.Add(this.txtFINISH_NUMBER);
            this.groupBoxElecInfo.Controls.Add(this.cboxMAT_NAME);
            this.groupBoxElecInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxElecInfo.Location = new System.Drawing.Point(0, 106);
            this.groupBoxElecInfo.Name = "groupBoxElecInfo";
            this.groupBoxElecInfo.Size = new System.Drawing.Size(649, 278);
            this.groupBoxElecInfo.TabIndex = 41;
            this.groupBoxElecInfo.TabStop = false;
            this.groupBoxElecInfo.Text = "电极信息";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(449, 195);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 12);
            this.label15.TabIndex = 69;
            this.label15.Text = "夹具类型";
            // 
            // cbbChuckType
            // 
            this.cbbChuckType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbChuckType.FormattingEnabled = true;
            this.cbbChuckType.Location = new System.Drawing.Point(520, 192);
            this.cbbChuckType.Name = "cbbChuckType";
            this.cbbChuckType.Size = new System.Drawing.Size(100, 20);
            this.cbbChuckType.TabIndex = 68;
            // 
            // cbbRock
            // 
            this.cbbRock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbRock.FormattingEnabled = true;
            this.cbbRock.Location = new System.Drawing.Point(300, 231);
            this.cbbRock.Name = "cbbRock";
            this.cbbRock.Size = new System.Drawing.Size(100, 20);
            this.cbbRock.TabIndex = 67;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(229, 234);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 66;
            this.label14.Text = "摇摆方式";
            // 
            // cbbProdirection
            // 
            this.cbbProdirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbProdirection.FormattingEnabled = true;
            this.cbbProdirection.Location = new System.Drawing.Point(90, 231);
            this.cbbProdirection.Name = "cbbProdirection";
            this.cbbProdirection.Size = new System.Drawing.Size(100, 20);
            this.cbbProdirection.TabIndex = 65;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(31, 234);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 64;
            this.label13.Text = "加工方向";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(229, 192);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 63;
            this.label12.Text = "电极类型";
            // 
            // cbbElecType
            // 
            this.cbbElecType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbElecType.FormattingEnabled = true;
            this.cbbElecType.Location = new System.Drawing.Point(300, 189);
            this.cbbElecType.Name = "cbbElecType";
            this.cbbElecType.Size = new System.Drawing.Size(100, 20);
            this.cbbElecType.TabIndex = 62;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(31, 192);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 12);
            this.label11.TabIndex = 61;
            this.label11.Text = "尺寸";
            // 
            // txtElecSize
            // 
            this.txtElecSize.Location = new System.Drawing.Point(90, 188);
            this.txtElecSize.Name = "txtElecSize";
            this.txtElecSize.ReadOnly = true;
            this.txtElecSize.Size = new System.Drawing.Size(100, 21);
            this.txtElecSize.TabIndex = 60;
            // 
            // cbbRSmoth
            // 
            this.cbbRSmoth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbRSmoth.FormattingEnabled = true;
            this.cbbRSmoth.Location = new System.Drawing.Point(520, 105);
            this.cbbRSmoth.Name = "cbbRSmoth";
            this.cbbRSmoth.Size = new System.Drawing.Size(100, 20);
            this.cbbRSmoth.TabIndex = 59;
            // 
            // cbbMSmoth
            // 
            this.cbbMSmoth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbMSmoth.FormattingEnabled = true;
            this.cbbMSmoth.Location = new System.Drawing.Point(520, 68);
            this.cbbMSmoth.Name = "cbbMSmoth";
            this.cbbMSmoth.Size = new System.Drawing.Size(100, 20);
            this.cbbMSmoth.TabIndex = 58;
            // 
            // cbbFSmoth
            // 
            this.cbbFSmoth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbFSmoth.FormattingEnabled = true;
            this.cbbFSmoth.Location = new System.Drawing.Point(520, 24);
            this.cbbFSmoth.Name = "cbbFSmoth";
            this.cbbFSmoth.Size = new System.Drawing.Size(100, 20);
            this.cbbFSmoth.TabIndex = 57;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(449, 109);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 56;
            this.label8.Text = "粗公光洁度";
            // 
            // label333333
            // 
            this.label333333.AutoSize = true;
            this.label333333.Location = new System.Drawing.Point(449, 66);
            this.label333333.Name = "label333333";
            this.label333333.Size = new System.Drawing.Size(65, 12);
            this.label333333.TabIndex = 55;
            this.label333333.Text = "中公光洁度";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(449, 26);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 54;
            this.label10.Text = "精公光洁度";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(31, 150);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 53;
            this.label7.Text = "精公材质";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(229, 115);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 52;
            this.label5.Text = "粗公火花位";
            // 
            // txtROUGH_SPACE
            // 
            this.txtROUGH_SPACE.Location = new System.Drawing.Point(300, 110);
            this.txtROUGH_SPACE.Name = "txtROUGH_SPACE";
            this.txtROUGH_SPACE.ReadOnly = true;
            this.txtROUGH_SPACE.Size = new System.Drawing.Size(100, 21);
            this.txtROUGH_SPACE.TabIndex = 51;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(31, 113);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 50;
            this.label6.Text = "粗公数量";
            // 
            // txtROUGH_NUMBER
            // 
            this.txtROUGH_NUMBER.Location = new System.Drawing.Point(90, 110);
            this.txtROUGH_NUMBER.Name = "txtROUGH_NUMBER";
            this.txtROUGH_NUMBER.ReadOnly = true;
            this.txtROUGH_NUMBER.Size = new System.Drawing.Size(100, 21);
            this.txtROUGH_NUMBER.TabIndex = 49;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(229, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 48;
            this.label2.Text = "中公火花位";
            // 
            // txtMIDDLE_SPACE
            // 
            this.txtMIDDLE_SPACE.Location = new System.Drawing.Point(300, 67);
            this.txtMIDDLE_SPACE.Name = "txtMIDDLE_SPACE";
            this.txtMIDDLE_SPACE.ReadOnly = true;
            this.txtMIDDLE_SPACE.Size = new System.Drawing.Size(100, 21);
            this.txtMIDDLE_SPACE.TabIndex = 47;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 46;
            this.label4.Text = "中公数量";
            // 
            // txtMIDDLE_NUMBER
            // 
            this.txtMIDDLE_NUMBER.Location = new System.Drawing.Point(90, 67);
            this.txtMIDDLE_NUMBER.Name = "txtMIDDLE_NUMBER";
            this.txtMIDDLE_NUMBER.ReadOnly = true;
            this.txtMIDDLE_NUMBER.Size = new System.Drawing.Size(100, 21);
            this.txtMIDDLE_NUMBER.TabIndex = 45;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(229, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 44;
            this.label3.Text = "精公火花位";
            // 
            // txtFINISH_SPACE
            // 
            this.txtFINISH_SPACE.Location = new System.Drawing.Point(300, 27);
            this.txtFINISH_SPACE.Name = "txtFINISH_SPACE";
            this.txtFINISH_SPACE.ReadOnly = true;
            this.txtFINISH_SPACE.Size = new System.Drawing.Size(100, 21);
            this.txtFINISH_SPACE.TabIndex = 43;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 42;
            this.label1.Text = "精公数量";
            // 
            // txtFINISH_NUMBER
            // 
            this.txtFINISH_NUMBER.Location = new System.Drawing.Point(90, 27);
            this.txtFINISH_NUMBER.Name = "txtFINISH_NUMBER";
            this.txtFINISH_NUMBER.ReadOnly = true;
            this.txtFINISH_NUMBER.Size = new System.Drawing.Size(100, 21);
            this.txtFINISH_NUMBER.TabIndex = 41;
            // 
            // cboxMAT_NAME
            // 
            this.cboxMAT_NAME.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxMAT_NAME.FormattingEnabled = true;
            this.cboxMAT_NAME.Location = new System.Drawing.Point(90, 147);
            this.cboxMAT_NAME.Name = "cboxMAT_NAME";
            this.cboxMAT_NAME.Size = new System.Drawing.Size(100, 20);
            this.cboxMAT_NAME.TabIndex = 40;
            // 
            // groupShareElec
            // 
            this.groupShareElec.Controls.Add(this.dataGridView1);
            this.groupShareElec.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupShareElec.Location = new System.Drawing.Point(0, 0);
            this.groupShareElec.Name = "groupShareElec";
            this.groupShareElec.Size = new System.Drawing.Size(649, 106);
            this.groupShareElec.TabIndex = 40;
            this.groupShareElec.TabStop = false;
            this.groupShareElec.Text = "共用电极";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 17);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(643, 86);
            this.dataGridView1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnSave);
            this.panel3.Controls.Add(this.btnExport);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 511);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(649, 39);
            this.panel3.TabIndex = 3;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(411, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "保存属性";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Visible = false;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(564, 6);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 0;
            this.btnExport.Text = "导入Eact";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(449, 153);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 71;
            this.label9.Text = "粗公材质";
            // 
            // cboxR_MAT_NAME
            // 
            this.cboxR_MAT_NAME.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxR_MAT_NAME.FormattingEnabled = true;
            this.cboxR_MAT_NAME.Location = new System.Drawing.Point(520, 150);
            this.cboxR_MAT_NAME.Name = "cboxR_MAT_NAME";
            this.cboxR_MAT_NAME.Size = new System.Drawing.Size(100, 20);
            this.cboxR_MAT_NAME.TabIndex = 70;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(229, 150);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 12);
            this.label16.TabIndex = 73;
            this.label16.Text = "中公材质";
            // 
            // cboxM_MAT_NAME
            // 
            this.cboxM_MAT_NAME.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxM_MAT_NAME.FormattingEnabled = true;
            this.cboxM_MAT_NAME.Location = new System.Drawing.Point(300, 147);
            this.cboxM_MAT_NAME.Name = "cboxM_MAT_NAME";
            this.cboxM_MAT_NAME.Size = new System.Drawing.Size(100, 20);
            this.cboxM_MAT_NAME.TabIndex = 72;
            // 
            // FrmEactBom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 550);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FrmEactBom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmEactBom";
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCuprums)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSteels)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPositions)).EndInit();
            this.panel4.ResumeLayout(false);
            this.groupBoxElecInfo.ResumeLayout(false);
            this.groupBoxElecInfo.PerformLayout();
            this.groupShareElec.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvSteels;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgvCuprums;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.DataGridView dgvPositions;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label labelCuprumNum;
        private System.Windows.Forms.CheckBox ckCuprumSelectAll;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBoxElecInfo;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cbbChuckType;
        private System.Windows.Forms.ComboBox cbbRock;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cbbProdirection;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cbbElecType;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtElecSize;
        private System.Windows.Forms.ComboBox cbbRSmoth;
        private System.Windows.Forms.ComboBox cbbMSmoth;
        private System.Windows.Forms.ComboBox cbbFSmoth;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label333333;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtROUGH_SPACE;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtROUGH_NUMBER;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMIDDLE_SPACE;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMIDDLE_NUMBER;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFINISH_SPACE;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFINISH_NUMBER;
        private System.Windows.Forms.ComboBox cboxMAT_NAME;
        private System.Windows.Forms.GroupBox groupShareElec;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cboxM_MAT_NAME;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cboxR_MAT_NAME;
    }
}