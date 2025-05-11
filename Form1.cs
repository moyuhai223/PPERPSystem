using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PPERPSystem // 确保这里的命名空间与您的项目名称一致
{
    public partial class Form1 : Form
    {
        // --- Class Member Variables for PPE Logic ---
        private List<string> ppeTypesWithUniqueCode = new List<string> { "分体洁净服", "洁净帽" };
        private List<string> shoeTypes = new List<string> { "室内安全鞋", "白色帆布鞋" };
        private List<string> clothAndHatTypes = new List<string> { "分体洁净服", "洁净帽" };

        public Form1()
        {
            InitializeComponent(); // 这个方法由设计器生成和管理

            DatabaseHelper.InitializeDatabase();
            LoadStatistics();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadEmployeesToGrid(); // 加载员工列表到员工管理选项卡的DataGridView

            // 初始化员工管理选项卡中的DateTimePicker
            if (this.dtpHireDate != null)
            {
                this.dtpHireDate.Format = DateTimePickerFormat.Short;
                this.dtpHireDate.Value = DateTime.Today;
            }

            // （劳保用品发放选项卡的控件初始化放到了 tabPPEIssuance_Enter 事件中）
        }

        private void LoadStatistics()
        {
            if (this.tvStatistics == null)
            {
                // MessageBox.Show("TreeView 'tvStatistics' 未在设计器中初始化。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // 避免在设计阶段或控件未正确添加时出错
            }
            this.tvStatistics.Nodes.Clear();

            List<string> allWorkProcessesFromEmployees = DatabaseHelper.GetAllWorkProcesses();
            Dictionary<string, int> employeeCounts = DatabaseHelper.GetEmployeeCountPerWorkProcess();
            List<PPEWorkProcessDetail> ppeDetails = DatabaseHelper.GetDetailedPPECountsPerWorkProcess();

            var distinctProcessesFromDetails = ppeDetails.Select(d => d.WorkProcess).Distinct();
            var combinedProcesses = allWorkProcessesFromEmployees.Union(distinctProcessesFromDetails).Distinct().OrderBy(p => p).ToList();

            if (!combinedProcesses.Any())
            {
                if (employeeCounts.ContainsKey("未分配") && employeeCounts["未分配"] > 0)
                {
                    combinedProcesses.Add("未分配");
                }
                else if (!ppeDetails.Any() && !employeeCounts.Any())
                {
                    this.tvStatistics.Nodes.Add("尚无统计数据");
                    return;
                }
            }

            foreach (string processName in combinedProcesses)
            {
                int empCount = employeeCounts.ContainsKey(processName) ? employeeCounts[processName] : 0;
                TreeNode processNode = new TreeNode($"工序: {processName} (总人数: {empCount})");
                this.tvStatistics.Nodes.Add(processNode);

                var itemsForThisProcess = ppeDetails.Where(p => p.WorkProcess == processName).ToList();
                if (itemsForThisProcess.Any())
                {
                    foreach (var ppeItem in itemsForThisProcess)
                    {
                        string unit = "件";
                        if (ppeItem.ItemName.Contains("鞋")) unit = "双";
                        else if (ppeItem.ItemName.Contains("帽")) unit = "顶";
                        TreeNode itemNode = new TreeNode($"{ppeItem.ItemName}: {ppeItem.Quantity} {unit}");
                        processNode.Nodes.Add(itemNode);
                    }
                }
                else
                {
                    bool hasEmployeesInProcess = employeeCounts.ContainsKey(processName) && employeeCounts[processName] > 0;
                    if (hasEmployeesInProcess || allWorkProcessesFromEmployees.Contains(processName))
                    {
                        processNode.Nodes.Add("无此工序的劳保用品发放记录");
                    }
                }
            }
            if (this.tvStatistics.Nodes.Count > 0)
            {
                this.tvStatistics.ExpandAll();
            }
        }

        // --- 可能未使用的事件处理程序 (来自之前的代码) ---
        private void groupBox1_Enter(object sender, EventArgs e) { }
        private void groupBoxStatistics_Enter(object sender, EventArgs e) { }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void panelEmployeeInput_Paint(object sender, PaintEventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        // --- 可能未使用的事件处理程序结束 ---

        #region Employee Management Tab Logic
        private void tabEmployeeManagement_Enter(object sender, EventArgs e)
        {
            LoadEmployeesToGrid();
            ClearEmployeeFormFields();
        }

        private void LoadEmployeesToGrid()
        {
            if (this.dgvEmployees == null) return;

            List<Employee> employees = DatabaseHelper.GetAllEmployees();
            dgvEmployees.DataSource = null;
            dgvEmployees.AutoGenerateColumns = false;

            if (dgvEmployees.Columns.Count == 0)
            {
                dgvEmployees.Columns.Add(new DataGridViewTextBoxColumn { Name = "colEmpID", HeaderText = "工号", DataPropertyName = "EmployeeID", ReadOnly = true, Width = 80 });
                dgvEmployees.Columns.Add(new DataGridViewTextBoxColumn { Name = "colEmpName", HeaderText = "姓名", DataPropertyName = "Name", ReadOnly = true, Width = 120 });
                dgvEmployees.Columns.Add(new DataGridViewTextBoxColumn { Name = "colHireDate", HeaderText = "入职时间", DataPropertyName = "HireDate", ReadOnly = true, Width = 100 });
                dgvEmployees.Columns.Add(new DataGridViewTextBoxColumn { Name = "colWorkProcess", HeaderText = "分配工序", DataPropertyName = "WorkProcess", ReadOnly = true, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            }
            dgvEmployees.DataSource = employees;
        }

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            if (this.txtEmpID == null || this.txtEmpName == null || this.dtpHireDate == null || this.txtEmpWorkProcess == null) return;
            if (string.IsNullOrWhiteSpace(txtEmpID.Text) || string.IsNullOrWhiteSpace(txtEmpName.Text))
            {
                MessageBox.Show("工号和姓名不能为空！", "验证错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Employee newEmployee = new Employee
            {
                EmployeeID = txtEmpID.Text.Trim(),
                Name = txtEmpName.Text.Trim(),
                HireDate = dtpHireDate.Value.ToString("yyyy-MM-dd"),
                WorkProcess = txtEmpWorkProcess.Text.Trim()
            };
            if (DatabaseHelper.AddEmployee(newEmployee))
            {
                MessageBox.Show("员工添加成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadEmployeesToGrid();
                ClearEmployeeFormFields();
                LoadStatistics();
            }
        }

        private void btnUpdateEmployee_Click(object sender, EventArgs e)
        {
            if (this.dgvEmployees == null || this.txtEmpID == null || this.txtEmpName == null || this.dtpHireDate == null || this.txtEmpWorkProcess == null) return;
            if (dgvEmployees.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先从列表中选择一个员工进行修改。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string selectedEmployeeID = dgvEmployees.SelectedRows[0].Cells["colEmpID"].Value.ToString();
            if (string.IsNullOrWhiteSpace(txtEmpName.Text))
            {
                MessageBox.Show("姓名不能为空！", "验证错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Employee updatedEmployee = new Employee
            {
                EmployeeID = selectedEmployeeID,
                Name = txtEmpName.Text.Trim(),
                HireDate = dtpHireDate.Value.ToString("yyyy-MM-dd"),
                WorkProcess = txtEmpWorkProcess.Text.Trim()
            };
            if (DatabaseHelper.UpdateEmployee(updatedEmployee))
            {
                MessageBox.Show("员工信息更新成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadEmployeesToGrid();
                ClearEmployeeFormFields();
                LoadStatistics();
            }
        }

        private void btnDeleteEmployee_Click(object sender, EventArgs e)
        {
            if (this.dgvEmployees == null) return;
            if (dgvEmployees.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先从列表中选择要删除的员工。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string employeeIDToDelete = dgvEmployees.SelectedRows[0].Cells["colEmpID"].Value.ToString();
            string employeeNameToDelete = dgvEmployees.SelectedRows[0].Cells["colEmpName"].Value.ToString();
            DialogResult confirmation = MessageBox.Show($"确定要删除员工 '{employeeNameToDelete}' (工号: {employeeIDToDelete}) 吗？\n删除员工将同时删除其所有劳保用品发放记录！",
                                                      "确认删除", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmation == DialogResult.Yes)
            {
                if (DatabaseHelper.DeleteEmployee(employeeIDToDelete))
                {
                    MessageBox.Show("员工删除成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadEmployeesToGrid();
                    ClearEmployeeFormFields();
                    LoadStatistics();
                }
            }
        }

        private void dgvEmployees_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dgvEmployees == null || this.txtEmpID == null || this.txtEmpName == null || this.dtpHireDate == null || this.txtEmpWorkProcess == null || this.btnUpdateEmployee == null || this.btnDeleteEmployee == null) return;
            if (dgvEmployees.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvEmployees.SelectedRows[0];
                txtEmpID.Text = selectedRow.Cells["colEmpID"].Value.ToString();
                txtEmpName.Text = selectedRow.Cells["colEmpName"].Value.ToString();
                if (DateTime.TryParse(selectedRow.Cells["colHireDate"].Value.ToString(), out DateTime hireDate))
                {
                    dtpHireDate.Value = hireDate;
                }
                else { dtpHireDate.Value = DateTime.Today; }
                txtEmpWorkProcess.Text = selectedRow.Cells["colWorkProcess"].Value.ToString();
                txtEmpID.ReadOnly = true;
                btnUpdateEmployee.Enabled = true;
                btnDeleteEmployee.Enabled = true;
            }
            else
            {
                txtEmpID.ReadOnly = false;
                btnUpdateEmployee.Enabled = false;
                btnDeleteEmployee.Enabled = false;
            }
        }

        private void btnClearForm_Click(object sender, EventArgs e)
        {
            ClearEmployeeFormFields();
        }

        private void ClearEmployeeFormFields()
        {
            if (this.txtEmpID == null || this.txtEmpName == null || this.dtpHireDate == null || this.txtEmpWorkProcess == null || this.dgvEmployees == null || this.btnUpdateEmployee == null || this.btnDeleteEmployee == null) return;
            txtEmpID.Clear();
            txtEmpName.Clear();
            dtpHireDate.Value = DateTime.Today;
            txtEmpWorkProcess.Clear();
            txtEmpID.ReadOnly = false;
            if (dgvEmployees.SelectedRows.Count > 0) { dgvEmployees.ClearSelection(); }
            btnUpdateEmployee.Enabled = false;
            btnDeleteEmployee.Enabled = false;
            txtEmpID.Focus();
        }
        #endregion

        #region PPE Issuance Tab Logic

        private void tabPPEIssuance_Enter(object sender, EventArgs e)
        {
            LoadEmployeesToPPEComboBox();
            LoadPPEItemNamesToComboBox();
            LoadPPEIssueTypesToComboBox();

            if (dtpPPEIssueDate != null)
            {
                dtpPPEIssueDate.Value = DateTime.Today;
                dtpPPEIssueDate.Format = DateTimePickerFormat.Short;
            }

            UpdateDynamicPPEFields();
            ClearPPEFormFieldsInternal();
            LoadPPEIssuanceRecordsToGrid(); // <-- 新增调用
        }

        private void LoadEmployeesToPPEComboBox()
        {
            if (cmbPPEEmployee == null) return;
            List<Employee> employees = DatabaseHelper.GetAllEmployees();
            cmbPPEEmployee.DataSource = null;
            cmbPPEEmployee.DisplayMember = "Name";
            cmbPPEEmployee.ValueMember = "EmployeeID";
            cmbPPEEmployee.DataSource = employees;
            if (cmbPPEEmployee.Items.Count > 0) { cmbPPEEmployee.SelectedIndex = 0; }
            else { cmbPPEEmployee.Text = "没有员工信息"; }
        }

        private void LoadPPEItemNamesToComboBox()
        {
            if (cmbPPEItemName == null) return;
            cmbPPEItemName.Items.Clear();
            cmbPPEItemName.Items.AddRange(new string[] { "室内安全鞋", "白色帆布鞋", "分体洁净服", "洁净帽" });
            if (cmbPPEItemName.Items.Count > 0) { cmbPPEItemName.SelectedIndex = 0; }
        }

        private void LoadPPEIssueTypesToComboBox()
        {
            if (cmbPPEIssueType == null) return;
            cmbPPEIssueType.Items.Clear();
            cmbPPEIssueType.Items.AddRange(new string[] { "初次发放", "更换" });
            if (cmbPPEIssueType.Items.Count > 0) { cmbPPEIssueType.SelectedIndex = 0; }
        }

        private void cmbPPEItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDynamicPPEFields();
        }

        private void UpdateDynamicPPEFields()
        {
            if (cmbPPEItemName == null || lblPPEItemCode == null || txtPPEItemCode == null ||
                lblPPEItemSize == null || txtPPEShoeSize == null || cmbPPEClothSize == null) return;

            string selectedPPE = cmbPPEItemName.SelectedItem?.ToString();
            bool requiresCode = ppeTypesWithUniqueCode.Contains(selectedPPE);

            lblPPEItemCode.Visible = requiresCode;
            txtPPEItemCode.Visible = requiresCode;
            if (!requiresCode) { txtPPEItemCode.Clear(); }

            bool isShoe = shoeTypes.Contains(selectedPPE);
            bool isClothOrHat = clothAndHatTypes.Contains(selectedPPE);

            lblPPEItemSize.Visible = isShoe || isClothOrHat;
            txtPPEShoeSize.Visible = isShoe;
            cmbPPEClothSize.Visible = isClothOrHat;

            if (isShoe)
            {
                txtPPEShoeSize.Clear();
                cmbPPEClothSize.SelectedIndex = -1;
                // cmbPPEClothSize.DataSource = null; // Only if it was databound, for Items.Add, this is not needed.
                cmbPPEClothSize.Items.Clear(); // Ensure it's empty if not used for clothes
            }
            else if (isClothOrHat)
            {
                if (cmbPPEClothSize.Items.Count == 0) // Populate only if empty, to avoid re-adding on every change
                {
                    cmbPPEClothSize.Items.AddRange(new string[] { "S", "M", "L", "XL", "XXL", "XXXL", "均码" });
                }
                if (cmbPPEClothSize.Items.Count > 0 && cmbPPEClothSize.SelectedIndex == -1) { cmbPPEClothSize.SelectedIndex = 0; }
                txtPPEShoeSize.Clear();
            }
            else // Neither shoe nor cloth/hat, hide both size inputs
            {
                txtPPEShoeSize.Clear();
                cmbPPEClothSize.Items.Clear();
                cmbPPEClothSize.SelectedIndex = -1;
                // lblPPEItemSize.Visible = false; // Already handled by the (isShoe || isClothOrHat) condition
            }
        }

        private void btnIssuePPE_Click(object sender, EventArgs e)
        {
            if (cmbPPEEmployee == null || cmbPPEItemName == null || txtPPEItemCode == null ||
                dtpPPEIssueDate == null || cmbPPEIssueType == null || txtPPERemarks == null ||
                txtPPEShoeSize == null || cmbPPEClothSize == null)
            {
                MessageBox.Show("界面控件未能正确初始化。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cmbPPEEmployee.SelectedValue == null)
            { MessageBox.Show("请选择一个员工。", "验证错误", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (cmbPPEItemName.SelectedItem == null)
            { MessageBox.Show("请选择劳保用品种类。", "验证错误", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (cmbPPEIssueType.SelectedItem == null)
            { MessageBox.Show("请选择发放类型。", "验证错误", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            string selectedItemName = cmbPPEItemName.SelectedItem.ToString();
            string itemCode = txtPPEItemCode.Text.Trim();

            if (ppeTypesWithUniqueCode.Contains(selectedItemName) && string.IsNullOrWhiteSpace(itemCode))
            {
                MessageBox.Show($"“{selectedItemName}”需要填写唯一编码。", "验证错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPPEItemCode.Focus(); return;
            }

            string itemSize = null;
            if (shoeTypes.Contains(selectedItemName))
            {
                if (string.IsNullOrWhiteSpace(txtPPEShoeSize.Text))
                { MessageBox.Show("请输入鞋子尺码。", "验证错误", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtPPEShoeSize.Focus(); return; }
                itemSize = txtPPEShoeSize.Text.Trim();
            }
            else if (clothAndHatTypes.Contains(selectedItemName))
            {
                if (cmbPPEClothSize.SelectedItem == null || string.IsNullOrWhiteSpace(cmbPPEClothSize.SelectedItem.ToString()))
                { MessageBox.Show("请选择衣服/帽子尺码。", "验证错误", MessageBoxButtons.OK, MessageBoxIcon.Warning); cmbPPEClothSize.Focus(); return; }
                itemSize = cmbPPEClothSize.SelectedItem.ToString();
            }

            PPEIssuanceRecord record = new PPEIssuanceRecord
            {
                EmployeeID = cmbPPEEmployee.SelectedValue.ToString(),
                ItemName = selectedItemName,
                ItemCode = ppeTypesWithUniqueCode.Contains(selectedItemName) ? itemCode : null,
                ItemSize = itemSize,
                IssueDate = dtpPPEIssueDate.Value.ToString("yyyy-MM-dd"),
                IssueType = cmbPPEIssueType.SelectedItem.ToString(),
                Remarks = txtPPERemarks.Text.Trim()
            };

            if (DatabaseHelper.AddPPEItemRecord(record))
            {
                MessageBox.Show("劳保用品发放成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearPPEFormFieldsInternal();
                LoadStatistics();
                LoadPPEIssuanceRecordsToGrid(); // <-- 新增调用
            }
        }

        private void btnClearPPEForm_Click(object sender, EventArgs e)
        {
            ClearPPEFormFieldsInternal();
        }

        private void ClearPPEFormFieldsInternal()
        {
            if (cmbPPEEmployee != null && cmbPPEEmployee.Items.Count > 0) cmbPPEEmployee.SelectedIndex = 0; else if (cmbPPEEmployee != null) cmbPPEEmployee.Text = "";
            if (cmbPPEItemName != null && cmbPPEItemName.Items.Count > 0) cmbPPEItemName.SelectedIndex = 0;
            if (txtPPEItemCode != null) txtPPEItemCode.Clear();
            if (txtPPEShoeSize != null) txtPPEShoeSize.Clear();
            if (cmbPPEClothSize != null)
            {
                // cmbPPEClothSize.DataSource = null; // Only needed if databound
                cmbPPEClothSize.Items.Clear(); // Clear existing items before potentially re-populating in UpdateDynamicPPEFields
                cmbPPEClothSize.Text = ""; // Clear displayed text
                cmbPPEClothSize.SelectedIndex = -1; // Ensure no selection
            }
            if (dtpPPEIssueDate != null) dtpPPEIssueDate.Value = DateTime.Today;
            if (cmbPPEIssueType != null && cmbPPEIssueType.Items.Count > 0) cmbPPEIssueType.SelectedIndex = 0;
            if (txtPPERemarks != null) txtPPERemarks.Clear();

            UpdateDynamicPPEFields(); // Call this at the end to set correct visibility after clearing
        }
        #endregion
        private void LoadPPEIssuanceRecordsToGrid()
        {
            // 确保 dgvPPEIssuanceRecords 控件已在设计器中正确添加和命名
            if (this.dgvPPEIssuanceRecords == null)
            {
                // 如果您不打算在这个选项卡上显示发放记录，可以忽略此消息或移除此方法调用
                // MessageBox.Show("DataGridView 'dgvPPEIssuanceRecords' 未在设计器中初始化。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            List<PPEIssuanceRecord> records = DatabaseHelper.GetAllPPEItemRecords();
            dgvPPEIssuanceRecords.DataSource = null; // 清除旧数据绑定
            dgvPPEIssuanceRecords.AutoGenerateColumns = false; // 手动定义列

            // 如果是第一次加载，或者列还没有定义，则定义列
            if (dgvPPEIssuanceRecords.Columns.Count == 0)
            {
                // 根据 PPEIssuanceRecord 类的属性和 PPEItems 表的字段来定义列
                // 注意：DataPropertyName 必须与 PPEIssuanceRecord 类中的属性名完全一致
                dgvPPEIssuanceRecords.Columns.Add(new DataGridViewTextBoxColumn { Name = "colIssueID", HeaderText = "记录ID", DataPropertyName = "IssueID", Width = 60, ReadOnly = true });
                dgvPPEIssuanceRecords.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPPEEmpName", HeaderText = "员工姓名", DataPropertyName = "EmployeeName", Width = 100, ReadOnly = true });
                // **增强提示**：这里显示的是工号，如果想显示员工姓名，需要修改 DatabaseHelper.GetAllPPEItemRecords() 
                //          使其JOIN Employees表，并在PPEIssuanceRecord模型中添加EmployeeName属性。
                dgvPPEIssuanceRecords.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPPEItemName", HeaderText = "用品名称", DataPropertyName = "ItemName", Width = 120, ReadOnly = true });
                dgvPPEIssuanceRecords.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPPEItemCode", HeaderText = "唯一编码", DataPropertyName = "ItemCode", Width = 100, ReadOnly = true });
                dgvPPEIssuanceRecords.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPPEItemSize", HeaderText = "尺码", DataPropertyName = "ItemSize", Width = 60, ReadOnly = true });
                dgvPPEIssuanceRecords.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPPEIssueDate", HeaderText = "发放日期", DataPropertyName = "IssueDate", Width = 100, ReadOnly = true });
                dgvPPEIssuanceRecords.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPPEIssueType", HeaderText = "发放类型", DataPropertyName = "IssueType", Width = 80, ReadOnly = true });
                dgvPPEIssuanceRecords.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPPERemarks", HeaderText = "备注", DataPropertyName = "Remarks", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, ReadOnly = true });
            }

            dgvPPEIssuanceRecords.DataSource = records; // 绑定数据
        }

        private void lblPPEItemCode_Click(object sender, EventArgs e)
        {

        }
        #region Query Records Tab Logic

        // 当用户进入“记录查询”选项卡时执行
        private void tabQueryRecords_Enter(object sender, EventArgs e)
        {
            LoadQuerySearchTypes();
            // 可选：清空上一次的搜索词和结果
            // if (txtQuerySearchTerm != null) txtQuerySearchTerm.Clear();
            if (dgvQueryResults != null) dgvQueryResults.DataSource = null;
        }

        private void LoadQuerySearchTypes()
        {
            if (cmbQuerySearchType == null) return;

            cmbQuerySearchType.Items.Clear(); // <--- 正确的清空 ComboBox 项的方法

            cmbQuerySearchType.DisplayMember = "Key";
            cmbQuerySearchType.ValueMember = "Value";

            cmbQuerySearchType.Items.Add(new KeyValuePair<string, PPESearchType>("按姓名查询", PPESearchType.ByEmployeeName));
            cmbQuerySearchType.Items.Add(new KeyValuePair<string, PPESearchType>("按工号查询", PPESearchType.ByEmployeeID));

            if (cmbQuerySearchType.Items.Count > 0)
            {
                cmbQuerySearchType.SelectedIndex = 0;
            }
        }

        private void btnQuerySearch_Click(object sender, EventArgs e)
        {
            if (cmbQuerySearchType == null || txtQuerySearchTerm == null || dgvQueryResults == null)
            {
                MessageBox.Show("查询界面控件尚未正确初始化。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string searchTerm = txtQuerySearchTerm.Text.Trim();
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                MessageBox.Show("请输入查询内容。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvQueryResults.DataSource = null; // 清空结果
                return;
            }

            if (cmbQuerySearchType.SelectedItem == null)
            {
                MessageBox.Show("请选择查询方式。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            PPESearchType searchType = ((KeyValuePair<string, PPESearchType>)cmbQuerySearchType.SelectedItem).Value;

            List<PPEIssuanceRecord> results = DatabaseHelper.SearchPPEIssuanceRecords(searchTerm, searchType);

            dgvQueryResults.DataSource = null; // 清除旧数据绑定
            dgvQueryResults.AutoGenerateColumns = false;

            if (dgvQueryResults.Columns.Count == 0) // 定义列 (与 dgvPPEIssuanceRecords 类似)
            {
                dgvQueryResults.Columns.Add(new DataGridViewTextBoxColumn { Name = "colQueryIssueID", HeaderText = "记录ID", DataPropertyName = "IssueID", Width = 60, ReadOnly = true });
                dgvQueryResults.Columns.Add(new DataGridViewTextBoxColumn { Name = "colQueryEmpID", HeaderText = "工号", DataPropertyName = "EmployeeID", Width = 80, ReadOnly = true });
                dgvQueryResults.Columns.Add(new DataGridViewTextBoxColumn { Name = "colQueryEmpName", HeaderText = "员工姓名", DataPropertyName = "EmployeeName", Width = 100, ReadOnly = true }); // 依赖 EmployeeName 属性
                dgvQueryResults.Columns.Add(new DataGridViewTextBoxColumn { Name = "colQueryItemName", HeaderText = "用品名称", DataPropertyName = "ItemName", Width = 120, ReadOnly = true });
                dgvQueryResults.Columns.Add(new DataGridViewTextBoxColumn { Name = "colQueryItemCode", HeaderText = "唯一编码", DataPropertyName = "ItemCode", Width = 100, ReadOnly = true });
                dgvQueryResults.Columns.Add(new DataGridViewTextBoxColumn { Name = "colQueryItemSize", HeaderText = "尺码", DataPropertyName = "ItemSize", Width = 60, ReadOnly = true });
                dgvQueryResults.Columns.Add(new DataGridViewTextBoxColumn { Name = "colQueryIssueDate", HeaderText = "发放日期", DataPropertyName = "IssueDate", Width = 100, ReadOnly = true });
                dgvQueryResults.Columns.Add(new DataGridViewTextBoxColumn { Name = "colQueryIssueType", HeaderText = "发放类型", DataPropertyName = "IssueType", Width = 80, ReadOnly = true });
                dgvQueryResults.Columns.Add(new DataGridViewTextBoxColumn { Name = "colQueryRemarks", HeaderText = "备注", DataPropertyName = "Remarks", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, ReadOnly = true });
            }

            dgvQueryResults.DataSource = results;

            if (results == null || !results.Any())
            {
                MessageBox.Show("未查询到相关记录。", "查询结果", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion
    }


}