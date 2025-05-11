namespace PPERPSystem
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
            this.groupBoxStatistics = new System.Windows.Forms.GroupBox();
            this.tvStatistics = new System.Windows.Forms.TreeView();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabEmployeeManagement = new System.Windows.Forms.TabPage();
            this.dgvEmployees = new System.Windows.Forms.DataGridView();
            this.panelEmployeeInput = new System.Windows.Forms.Panel();
            this.btnClearForm = new System.Windows.Forms.Button();
            this.btnDeleteEmployee = new System.Windows.Forms.Button();
            this.btnUpdateEmployee = new System.Windows.Forms.Button();
            this.btnAddEmployee = new System.Windows.Forms.Button();
            this.dtpHireDate = new System.Windows.Forms.DateTimePicker();
            this.txtEmpWorkProcess = new System.Windows.Forms.TextBox();
            this.txtEmpName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEmpID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPPEIssuance = new System.Windows.Forms.TabPage();
            this.panelPPEInput = new System.Windows.Forms.Panel();
            this.dgvPPEIssuanceRecords = new System.Windows.Forms.DataGridView();
            this.cmbPPEClothSize = new System.Windows.Forms.ComboBox();
            this.txtPPEShoeSize = new System.Windows.Forms.TextBox();
            this.lblPPEItemSize = new System.Windows.Forms.Label();
            this.btnClearPPEForm = new System.Windows.Forms.Button();
            this.btnIssuePPE = new System.Windows.Forms.Button();
            this.txtPPERemarks = new System.Windows.Forms.TextBox();
            this.cmbPPEIssueType = new System.Windows.Forms.ComboBox();
            this.dtpPPEIssueDate = new System.Windows.Forms.DateTimePicker();
            this.txtPPEItemCode = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblPPEItemCode = new System.Windows.Forms.Label();
            this.cmbPPEItemName = new System.Windows.Forms.ComboBox();
            this.cmbPPEEmployee = new System.Windows.Forms.ComboBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.tabQueryRecords = new System.Windows.Forms.TabPage();
            this.dgvQueryResults = new System.Windows.Forms.DataGridView();
            this.panelQueryInput = new System.Windows.Forms.Panel();
            this.btnQuerySearch = new System.Windows.Forms.Button();
            this.txtQuerySearchTerm = new System.Windows.Forms.ComboBox();
            this.cmbQuerySearchType = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBoxStatistics.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabEmployeeManagement.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployees)).BeginInit();
            this.panelEmployeeInput.SuspendLayout();
            this.tabPPEIssuance.SuspendLayout();
            this.panelPPEInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPPEIssuanceRecords)).BeginInit();
            this.tabQueryRecords.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueryResults)).BeginInit();
            this.panelQueryInput.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxStatistics
            // 
            this.groupBoxStatistics.Controls.Add(this.tvStatistics);
            this.groupBoxStatistics.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBoxStatistics.Location = new System.Drawing.Point(504, 3);
            this.groupBoxStatistics.Name = "groupBoxStatistics";
            this.groupBoxStatistics.Size = new System.Drawing.Size(300, 597);
            this.groupBoxStatistics.TabIndex = 0;
            this.groupBoxStatistics.TabStop = false;
            this.groupBoxStatistics.Text = "实时统计汇总";
            this.groupBoxStatistics.Enter += new System.EventHandler(this.groupBoxStatistics_Enter);
            // 
            // tvStatistics
            // 
            this.tvStatistics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvStatistics.Location = new System.Drawing.Point(3, 19);
            this.tvStatistics.Name = "tvStatistics";
            this.tvStatistics.Size = new System.Drawing.Size(294, 575);
            this.tvStatistics.TabIndex = 0;
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabEmployeeManagement);
            this.tabControlMain.Controls.Add(this.tabPPEIssuance);
            this.tabControlMain.Controls.Add(this.tabQueryRecords);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(815, 633);
            this.tabControlMain.TabIndex = 1;
            // 
            // tabEmployeeManagement
            // 
            this.tabEmployeeManagement.Controls.Add(this.groupBoxStatistics);
            this.tabEmployeeManagement.Controls.Add(this.dgvEmployees);
            this.tabEmployeeManagement.Controls.Add(this.panelEmployeeInput);
            this.tabEmployeeManagement.Location = new System.Drawing.Point(4, 26);
            this.tabEmployeeManagement.Name = "tabEmployeeManagement";
            this.tabEmployeeManagement.Padding = new System.Windows.Forms.Padding(3);
            this.tabEmployeeManagement.Size = new System.Drawing.Size(807, 603);
            this.tabEmployeeManagement.TabIndex = 0;
            this.tabEmployeeManagement.Text = "员工管理";
            this.tabEmployeeManagement.UseVisualStyleBackColor = true;
            // 
            // dgvEmployees
            // 
            this.dgvEmployees.AllowUserToAddRows = false;
            this.dgvEmployees.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEmployees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEmployees.Location = new System.Drawing.Point(3, 183);
            this.dgvEmployees.Name = "dgvEmployees";
            this.dgvEmployees.ReadOnly = true;
            this.dgvEmployees.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEmployees.Size = new System.Drawing.Size(480, 412);
            this.dgvEmployees.TabIndex = 2;
            this.dgvEmployees.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // panelEmployeeInput
            // 
            this.panelEmployeeInput.Controls.Add(this.btnClearForm);
            this.panelEmployeeInput.Controls.Add(this.btnDeleteEmployee);
            this.panelEmployeeInput.Controls.Add(this.btnUpdateEmployee);
            this.panelEmployeeInput.Controls.Add(this.btnAddEmployee);
            this.panelEmployeeInput.Controls.Add(this.dtpHireDate);
            this.panelEmployeeInput.Controls.Add(this.txtEmpWorkProcess);
            this.panelEmployeeInput.Controls.Add(this.txtEmpName);
            this.panelEmployeeInput.Controls.Add(this.label4);
            this.panelEmployeeInput.Controls.Add(this.label3);
            this.panelEmployeeInput.Controls.Add(this.label2);
            this.panelEmployeeInput.Controls.Add(this.txtEmpID);
            this.panelEmployeeInput.Controls.Add(this.label1);
            this.panelEmployeeInput.Location = new System.Drawing.Point(3, 3);
            this.panelEmployeeInput.Name = "panelEmployeeInput";
            this.panelEmployeeInput.Size = new System.Drawing.Size(480, 174);
            this.panelEmployeeInput.TabIndex = 1;
            this.panelEmployeeInput.Paint += new System.Windows.Forms.PaintEventHandler(this.panelEmployeeInput_Paint);
            // 
            // btnClearForm
            // 
            this.btnClearForm.Location = new System.Drawing.Point(285, 3);
            this.btnClearForm.Name = "btnClearForm";
            this.btnClearForm.Size = new System.Drawing.Size(75, 23);
            this.btnClearForm.TabIndex = 5;
            this.btnClearForm.Text = "清空表单";
            this.btnClearForm.UseVisualStyleBackColor = true;
            this.btnClearForm.Click += new System.EventHandler(this.btnClearForm_Click);
            // 
            // btnDeleteEmployee
            // 
            this.btnDeleteEmployee.Location = new System.Drawing.Point(195, 3);
            this.btnDeleteEmployee.Name = "btnDeleteEmployee";
            this.btnDeleteEmployee.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteEmployee.TabIndex = 5;
            this.btnDeleteEmployee.Text = "删除员工";
            this.btnDeleteEmployee.UseVisualStyleBackColor = true;
            this.btnDeleteEmployee.Click += new System.EventHandler(this.btnDeleteEmployee_Click);
            // 
            // btnUpdateEmployee
            // 
            this.btnUpdateEmployee.Location = new System.Drawing.Point(114, 3);
            this.btnUpdateEmployee.Name = "btnUpdateEmployee";
            this.btnUpdateEmployee.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateEmployee.TabIndex = 5;
            this.btnUpdateEmployee.Text = "保存更改";
            this.btnUpdateEmployee.UseVisualStyleBackColor = true;
            this.btnUpdateEmployee.Click += new System.EventHandler(this.btnUpdateEmployee_Click);
            // 
            // btnAddEmployee
            // 
            this.btnAddEmployee.Location = new System.Drawing.Point(21, 3);
            this.btnAddEmployee.Name = "btnAddEmployee";
            this.btnAddEmployee.Size = new System.Drawing.Size(75, 23);
            this.btnAddEmployee.TabIndex = 5;
            this.btnAddEmployee.Text = "添加员工";
            this.btnAddEmployee.UseVisualStyleBackColor = true;
            this.btnAddEmployee.Click += new System.EventHandler(this.btnAddEmployee_Click);
            // 
            // dtpHireDate
            // 
            this.dtpHireDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHireDate.Location = new System.Drawing.Point(92, 103);
            this.dtpHireDate.Name = "dtpHireDate";
            this.dtpHireDate.Size = new System.Drawing.Size(123, 23);
            this.dtpHireDate.TabIndex = 4;
            this.dtpHireDate.Value = new System.DateTime(2025, 5, 10, 0, 0, 0, 0);
            // 
            // txtEmpWorkProcess
            // 
            this.txtEmpWorkProcess.Location = new System.Drawing.Point(92, 132);
            this.txtEmpWorkProcess.Name = "txtEmpWorkProcess";
            this.txtEmpWorkProcess.Size = new System.Drawing.Size(123, 23);
            this.txtEmpWorkProcess.TabIndex = 3;
            // 
            // txtEmpName
            // 
            this.txtEmpName.Location = new System.Drawing.Point(92, 73);
            this.txtEmpName.Name = "txtEmpName";
            this.txtEmpName.Size = new System.Drawing.Size(123, 23);
            this.txtEmpName.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "分配工序:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "入职时间:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "姓      名:";
            // 
            // txtEmpID
            // 
            this.txtEmpID.Location = new System.Drawing.Point(92, 45);
            this.txtEmpID.Name = "txtEmpID";
            this.txtEmpID.Size = new System.Drawing.Size(123, 23);
            this.txtEmpID.TabIndex = 1;
            this.txtEmpID.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "工      号：";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // tabPPEIssuance
            // 
            this.tabPPEIssuance.Controls.Add(this.panelPPEInput);
            this.tabPPEIssuance.Location = new System.Drawing.Point(4, 26);
            this.tabPPEIssuance.Name = "tabPPEIssuance";
            this.tabPPEIssuance.Padding = new System.Windows.Forms.Padding(3);
            this.tabPPEIssuance.Size = new System.Drawing.Size(807, 603);
            this.tabPPEIssuance.TabIndex = 1;
            this.tabPPEIssuance.Text = "劳保用品发放";
            this.tabPPEIssuance.UseVisualStyleBackColor = true;
            this.tabPPEIssuance.Enter += new System.EventHandler(this.tabPPEIssuance_Enter);
            // 
            // panelPPEInput
            // 
            this.panelPPEInput.Controls.Add(this.dgvPPEIssuanceRecords);
            this.panelPPEInput.Controls.Add(this.cmbPPEClothSize);
            this.panelPPEInput.Controls.Add(this.txtPPEShoeSize);
            this.panelPPEInput.Controls.Add(this.lblPPEItemSize);
            this.panelPPEInput.Controls.Add(this.btnClearPPEForm);
            this.panelPPEInput.Controls.Add(this.btnIssuePPE);
            this.panelPPEInput.Controls.Add(this.txtPPERemarks);
            this.panelPPEInput.Controls.Add(this.cmbPPEIssueType);
            this.panelPPEInput.Controls.Add(this.dtpPPEIssueDate);
            this.panelPPEInput.Controls.Add(this.txtPPEItemCode);
            this.panelPPEInput.Controls.Add(this.label8);
            this.panelPPEInput.Controls.Add(this.label10);
            this.panelPPEInput.Controls.Add(this.label9);
            this.panelPPEInput.Controls.Add(this.lblPPEItemCode);
            this.panelPPEInput.Controls.Add(this.cmbPPEItemName);
            this.panelPPEInput.Controls.Add(this.cmbPPEEmployee);
            this.panelPPEInput.Controls.Add(this.Label6);
            this.panelPPEInput.Controls.Add(this.Label5);
            this.panelPPEInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPPEInput.Location = new System.Drawing.Point(3, 3);
            this.panelPPEInput.Name = "panelPPEInput";
            this.panelPPEInput.Size = new System.Drawing.Size(801, 604);
            this.panelPPEInput.TabIndex = 0;
            // 
            // dgvPPEIssuanceRecords
            // 
            this.dgvPPEIssuanceRecords.AllowUserToAddRows = false;
            this.dgvPPEIssuanceRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPPEIssuanceRecords.Location = new System.Drawing.Point(3, 226);
            this.dgvPPEIssuanceRecords.Name = "dgvPPEIssuanceRecords";
            this.dgvPPEIssuanceRecords.ReadOnly = true;
            this.dgvPPEIssuanceRecords.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPPEIssuanceRecords.Size = new System.Drawing.Size(796, 374);
            this.dgvPPEIssuanceRecords.TabIndex = 15;
            // 
            // cmbPPEClothSize
            // 
            this.cmbPPEClothSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPPEClothSize.FormattingEnabled = true;
            this.cmbPPEClothSize.Location = new System.Drawing.Point(95, 85);
            this.cmbPPEClothSize.Name = "cmbPPEClothSize";
            this.cmbPPEClothSize.Size = new System.Drawing.Size(102, 25);
            this.cmbPPEClothSize.TabIndex = 14;
            this.cmbPPEClothSize.Visible = false;
            // 
            // txtPPEShoeSize
            // 
            this.txtPPEShoeSize.Location = new System.Drawing.Point(96, 85);
            this.txtPPEShoeSize.Name = "txtPPEShoeSize";
            this.txtPPEShoeSize.Size = new System.Drawing.Size(45, 23);
            this.txtPPEShoeSize.TabIndex = 13;
            this.txtPPEShoeSize.Visible = false;
            // 
            // lblPPEItemSize
            // 
            this.lblPPEItemSize.AutoSize = true;
            this.lblPPEItemSize.Location = new System.Drawing.Point(30, 85);
            this.lblPPEItemSize.Name = "lblPPEItemSize";
            this.lblPPEItemSize.Size = new System.Drawing.Size(59, 17);
            this.lblPPEItemSize.TabIndex = 12;
            this.lblPPEItemSize.Text = "尺      码:";
            // 
            // btnClearPPEForm
            // 
            this.btnClearPPEForm.Location = new System.Drawing.Point(371, 66);
            this.btnClearPPEForm.Name = "btnClearPPEForm";
            this.btnClearPPEForm.Size = new System.Drawing.Size(75, 23);
            this.btnClearPPEForm.TabIndex = 10;
            this.btnClearPPEForm.Text = "清空表单";
            this.btnClearPPEForm.UseVisualStyleBackColor = true;
            this.btnClearPPEForm.Click += new System.EventHandler(this.btnClearPPEForm_Click);
            // 
            // btnIssuePPE
            // 
            this.btnIssuePPE.Location = new System.Drawing.Point(371, 36);
            this.btnIssuePPE.Name = "btnIssuePPE";
            this.btnIssuePPE.Size = new System.Drawing.Size(75, 23);
            this.btnIssuePPE.TabIndex = 9;
            this.btnIssuePPE.Text = "确认发放";
            this.btnIssuePPE.UseVisualStyleBackColor = true;
            this.btnIssuePPE.Click += new System.EventHandler(this.btnIssuePPE_Click);
            // 
            // txtPPERemarks
            // 
            this.txtPPERemarks.Location = new System.Drawing.Point(97, 195);
            this.txtPPERemarks.Multiline = true;
            this.txtPPERemarks.Name = "txtPPERemarks";
            this.txtPPERemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPPERemarks.Size = new System.Drawing.Size(222, 22);
            this.txtPPERemarks.TabIndex = 8;
            // 
            // cmbPPEIssueType
            // 
            this.cmbPPEIssueType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPPEIssueType.FormattingEnabled = true;
            this.cmbPPEIssueType.Location = new System.Drawing.Point(97, 164);
            this.cmbPPEIssueType.Name = "cmbPPEIssueType";
            this.cmbPPEIssueType.Size = new System.Drawing.Size(100, 25);
            this.cmbPPEIssueType.TabIndex = 7;
            // 
            // dtpPPEIssueDate
            // 
            this.dtpPPEIssueDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPPEIssueDate.Location = new System.Drawing.Point(97, 123);
            this.dtpPPEIssueDate.Name = "dtpPPEIssueDate";
            this.dtpPPEIssueDate.Size = new System.Drawing.Size(100, 23);
            this.dtpPPEIssueDate.TabIndex = 6;
            // 
            // txtPPEItemCode
            // 
            this.txtPPEItemCode.Location = new System.Drawing.Point(205, 87);
            this.txtPPEItemCode.Name = "txtPPEItemCode";
            this.txtPPEItemCode.Size = new System.Drawing.Size(91, 23);
            this.txtPPEItemCode.TabIndex = 5;
            this.txtPPEItemCode.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(30, 127);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 17);
            this.label8.TabIndex = 4;
            this.label8.Text = "发放日期:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(31, 200);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 17);
            this.label10.TabIndex = 4;
            this.label10.Text = "备     注:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(31, 164);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 17);
            this.label9.TabIndex = 4;
            this.label9.Text = "发放类型:";
            // 
            // lblPPEItemCode
            // 
            this.lblPPEItemCode.AutoSize = true;
            this.lblPPEItemCode.Location = new System.Drawing.Point(214, 51);
            this.lblPPEItemCode.Name = "lblPPEItemCode";
            this.lblPPEItemCode.Size = new System.Drawing.Size(59, 17);
            this.lblPPEItemCode.TabIndex = 4;
            this.lblPPEItemCode.Text = "唯一编码:";
            this.lblPPEItemCode.Click += new System.EventHandler(this.lblPPEItemCode_Click);
            // 
            // cmbPPEItemName
            // 
            this.cmbPPEItemName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPPEItemName.FormattingEnabled = true;
            this.cmbPPEItemName.Location = new System.Drawing.Point(97, 48);
            this.cmbPPEItemName.Name = "cmbPPEItemName";
            this.cmbPPEItemName.Size = new System.Drawing.Size(100, 25);
            this.cmbPPEItemName.TabIndex = 3;
            this.cmbPPEItemName.SelectedIndexChanged += new System.EventHandler(this.cmbPPEItemName_SelectedIndexChanged);
            // 
            // cmbPPEEmployee
            // 
            this.cmbPPEEmployee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPPEEmployee.FormattingEnabled = true;
            this.cmbPPEEmployee.Location = new System.Drawing.Point(97, 20);
            this.cmbPPEEmployee.Name = "cmbPPEEmployee";
            this.cmbPPEEmployee.Size = new System.Drawing.Size(100, 25);
            this.cmbPPEEmployee.TabIndex = 2;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(31, 50);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(59, 17);
            this.Label6.TabIndex = 1;
            this.Label6.Text = "用品种类:";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(31, 20);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(59, 17);
            this.Label5.TabIndex = 0;
            this.Label5.Text = "选择员工:";
            // 
            // tabQueryRecords
            // 
            this.tabQueryRecords.Controls.Add(this.dgvQueryResults);
            this.tabQueryRecords.Controls.Add(this.panelQueryInput);
            this.tabQueryRecords.Location = new System.Drawing.Point(4, 26);
            this.tabQueryRecords.Name = "tabQueryRecords";
            this.tabQueryRecords.Padding = new System.Windows.Forms.Padding(3);
            this.tabQueryRecords.Size = new System.Drawing.Size(807, 603);
            this.tabQueryRecords.TabIndex = 2;
            this.tabQueryRecords.Text = "记录查询";
            this.tabQueryRecords.UseVisualStyleBackColor = true;
            this.tabQueryRecords.Enter += new System.EventHandler(this.tabQueryRecords_Enter);
            // 
            // dgvQueryResults
            // 
            this.dgvQueryResults.AllowUserToAddRows = false;
            this.dgvQueryResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvQueryResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQueryResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvQueryResults.Location = new System.Drawing.Point(3, 142);
            this.dgvQueryResults.Name = "dgvQueryResults";
            this.dgvQueryResults.ReadOnly = true;
            this.dgvQueryResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvQueryResults.Size = new System.Drawing.Size(801, 458);
            this.dgvQueryResults.TabIndex = 1;
            // 
            // panelQueryInput
            // 
            this.panelQueryInput.Controls.Add(this.btnQuerySearch);
            this.panelQueryInput.Controls.Add(this.txtQuerySearchTerm);
            this.panelQueryInput.Controls.Add(this.cmbQuerySearchType);
            this.panelQueryInput.Controls.Add(this.label11);
            this.panelQueryInput.Controls.Add(this.label7);
            this.panelQueryInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelQueryInput.Location = new System.Drawing.Point(3, 3);
            this.panelQueryInput.Name = "panelQueryInput";
            this.panelQueryInput.Size = new System.Drawing.Size(801, 139);
            this.panelQueryInput.TabIndex = 0;
            // 
            // btnQuerySearch
            // 
            this.btnQuerySearch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnQuerySearch.Location = new System.Drawing.Point(34, 82);
            this.btnQuerySearch.Name = "btnQuerySearch";
            this.btnQuerySearch.Size = new System.Drawing.Size(180, 23);
            this.btnQuerySearch.TabIndex = 3;
            this.btnQuerySearch.Text = "开始查询";
            this.btnQuerySearch.UseVisualStyleBackColor = true;
            this.btnQuerySearch.Click += new System.EventHandler(this.btnQuerySearch_Click);
            // 
            // txtQuerySearchTerm
            // 
            this.txtQuerySearchTerm.FormattingEnabled = true;
            this.txtQuerySearchTerm.Location = new System.Drawing.Point(93, 51);
            this.txtQuerySearchTerm.Name = "txtQuerySearchTerm";
            this.txtQuerySearchTerm.Size = new System.Drawing.Size(121, 25);
            this.txtQuerySearchTerm.TabIndex = 2;
            // 
            // cmbQuerySearchType
            // 
            this.cmbQuerySearchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQuerySearchType.FormattingEnabled = true;
            this.cmbQuerySearchType.Location = new System.Drawing.Point(93, 16);
            this.cmbQuerySearchType.Name = "cmbQuerySearchType";
            this.cmbQuerySearchType.Size = new System.Drawing.Size(121, 25);
            this.cmbQuerySearchType.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(31, 54);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 17);
            this.label11.TabIndex = 0;
            this.label11.Text = "查询内容:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(31, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 17);
            this.label7.TabIndex = 0;
            this.label7.Text = "查询方式:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 633);
            this.Controls.Add(this.tabControlMain);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "Form1";
            this.Text = "个人劳保用品管理系统";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBoxStatistics.ResumeLayout(false);
            this.tabControlMain.ResumeLayout(false);
            this.tabEmployeeManagement.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployees)).EndInit();
            this.panelEmployeeInput.ResumeLayout(false);
            this.panelEmployeeInput.PerformLayout();
            this.tabPPEIssuance.ResumeLayout(false);
            this.panelPPEInput.ResumeLayout(false);
            this.panelPPEInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPPEIssuanceRecords)).EndInit();
            this.tabQueryRecords.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueryResults)).EndInit();
            this.panelQueryInput.ResumeLayout(false);
            this.panelQueryInput.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxStatistics;
        private System.Windows.Forms.TreeView tvStatistics;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabEmployeeManagement;
        private System.Windows.Forms.TabPage tabPPEIssuance;
        private System.Windows.Forms.Panel panelEmployeeInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEmpID;
        private System.Windows.Forms.TextBox txtEmpName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpHireDate;
        private System.Windows.Forms.TextBox txtEmpWorkProcess;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnClearForm;
        private System.Windows.Forms.Button btnDeleteEmployee;
        private System.Windows.Forms.Button btnUpdateEmployee;
        private System.Windows.Forms.Button btnAddEmployee;
        private System.Windows.Forms.DataGridView dgvEmployees;
        private System.Windows.Forms.Panel panelPPEInput;
        private System.Windows.Forms.Label Label5;
        private System.Windows.Forms.Label Label6;
        private System.Windows.Forms.ComboBox cmbPPEEmployee;
        private System.Windows.Forms.ComboBox cmbPPEItemName;
        private System.Windows.Forms.ComboBox cmbPPEIssueType;
        private System.Windows.Forms.DateTimePicker dtpPPEIssueDate;
        private System.Windows.Forms.TextBox txtPPEItemCode;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblPPEItemCode;
        private System.Windows.Forms.TextBox txtPPERemarks;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnClearPPEForm;
        private System.Windows.Forms.Button btnIssuePPE;
        private System.Windows.Forms.Label lblPPEItemSize;
        private System.Windows.Forms.ComboBox cmbPPEClothSize;
        private System.Windows.Forms.TextBox txtPPEShoeSize;
        private System.Windows.Forms.DataGridView dgvPPEIssuanceRecords;
        private System.Windows.Forms.TabPage tabQueryRecords;
        private System.Windows.Forms.Panel panelQueryInput;
        private System.Windows.Forms.Button btnQuerySearch;
        private System.Windows.Forms.ComboBox txtQuerySearchTerm;
        private System.Windows.Forms.ComboBox cmbQuerySearchType;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dgvQueryResults;
    }
}

