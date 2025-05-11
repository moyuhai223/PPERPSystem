using System;
using System.Collections.Generic;
using System.Data.SQLite; // 确保已通过 NuGet 安装 System.Data.SQLite.Core
using System.IO;
using System.Windows.Forms; // 用于 MessageBox，用于简单的错误提示

namespace PPERPSystem // 请确保这里的命名空间与您的项目一致
{
    // ***********************************************************************************
    // 重要提示：
    // 以下 DatabaseHelper 类依赖于 Employee, PPEWorkProcessDetail, 和 PPEIssuanceRecord 这三个类。
    // 请确保您已经在项目中单独的文件（例如 Employee.cs, PPEWorkProcessDetail.cs, PPEIssuanceRecord.cs
    // 或一个统一的 Models.cs 文件）中定义了这些类，并且它们位于 PPERPSystem 命名空间下。
    //
    // 例如，您的 Employee.cs 文件可能如下：
    // namespace PPERPSystem
    // {
    //     public class Employee
    //     {
    //         public string EmployeeID { get; set; }
    //         public string Name { get; set; }
    //         public string HireDate { get; set; }
    //         public string WorkProcess { get; set; }
    //     }
    // }
    // (PPEWorkProcessDetail 和 PPEIssuanceRecord 类也类似地在单独文件中定义)
    // ***********************************************************************************

    public static class DatabaseHelper // 设为 static 类，因为所有方法都是 static
    {
        private static readonly string dbFileName = "ppedata.sqlite";
        private static readonly string dbFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dbFileName);
        private static readonly string connectionString = $"Data Source={dbFilePath};Version=3;Foreign Keys=True;";

        public static void InitializeDatabase()
        {
            if (!File.Exists(dbFilePath))
            {
                SQLiteConnection.CreateFile(dbFilePath);
                MessageBox.Show($"数据库文件 '{dbFileName}' 已在程序目录下创建。", "数据库初始化", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string createEmployeesTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Employees (
                        EmployeeID TEXT PRIMARY KEY,
                        Name TEXT NOT NULL,
                        HireDate TEXT NOT NULL,
                        WorkProcess TEXT
                    );";
                    using (SQLiteCommand cmd = new SQLiteCommand(createEmployeesTableQuery, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    string createPPEItemsTableQuery = @"
                    CREATE TABLE IF NOT EXISTS PPEItems (
                        IssueID INTEGER PRIMARY KEY AUTOINCREMENT,
                        EmployeeID TEXT NOT NULL,
                        ItemName TEXT NOT NULL,
                        ItemCode TEXT,
                        ItemSize TEXT,                             -- 尺码字段
                        IssueDate TEXT NOT NULL,
                        IssueType TEXT NOT NULL,
                        Remarks TEXT,
                        FOREIGN KEY (EmployeeID) REFERENCES Employees (EmployeeID) ON DELETE CASCADE ON UPDATE CASCADE
                    );";
                    using (SQLiteCommand cmd = new SQLiteCommand(createPPEItemsTableQuery, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"数据库初始化或表创建失败: {ex.Message}\n{ex.StackTrace}", "数据库严重错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public static string GetConnectionString()
        {
            return connectionString;
        }

        #region Statistics Methods
        public static Dictionary<string, int> GetEmployeeCountPerWorkProcess()
        {
            var counts = new Dictionary<string, int>();
            using (SQLiteConnection conn = new SQLiteConnection(GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT IFNULL(NULLIF(WorkProcess, ''), '未分配') AS Process, COUNT(EmployeeID) FROM Employees GROUP BY Process;";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string workProcess = reader.GetString(0);
                                int count = Convert.ToInt32(reader[1]);
                                counts[workProcess] = count;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"查询各工序总人数失败: {ex.Message}", "数据库查询错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            return counts;
        }

        public static List<string> GetAllWorkProcesses()
        {
            var processes = new List<string>();
            using (SQLiteConnection conn = new SQLiteConnection(GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT DISTINCT IFNULL(NULLIF(WorkProcess, ''), '未分配') AS ProcessName FROM Employees ORDER BY ProcessName;";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                processes.Add(reader.GetString(0));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"查询工序列表失败: {ex.Message}", "数据库查询错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            return processes;
        }

        // 此方法依赖于 PPEWorkProcessDetail 类的定义
        public static List<PPEWorkProcessDetail> GetDetailedPPECountsPerWorkProcess()
        {
            var details = new List<PPEWorkProcessDetail>();
            using (SQLiteConnection conn = new SQLiteConnection(GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            IFNULL(NULLIF(e.WorkProcess, ''), '未分配') AS Process, 
                            p.ItemName, 
                            COUNT(p.IssueID) AS ItemCount
                        FROM Employees e
                        JOIN PPEItems p ON e.EmployeeID = p.EmployeeID
                        GROUP BY Process, p.ItemName
                        ORDER BY Process, p.ItemName;";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                details.Add(new PPEWorkProcessDetail
                                {
                                    WorkProcess = reader.GetString(0),
                                    ItemName = reader.GetString(1),
                                    Quantity = Convert.ToInt32(reader[2])
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"查询各工序详细劳保发放数量失败: {ex.Message}", "数据库查询错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            return details;
        }
        #endregion

        #region Employee Management Methods
        // 以下方法依赖于 Employee 类的定义
        public static bool AddEmployee(Employee employee)
        {
            using (SQLiteConnection conn = new SQLiteConnection(GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Employees (EmployeeID, Name, HireDate, WorkProcess) VALUES (@EmployeeID, @Name, @HireDate, @WorkProcess);";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);
                        cmd.Parameters.AddWithValue("@Name", employee.Name);
                        cmd.Parameters.AddWithValue("@HireDate", employee.HireDate);
                        cmd.Parameters.AddWithValue("@WorkProcess", employee.WorkProcess);
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
                catch (SQLiteException ex)
                {
                    if (ex.ErrorCode == (int)SQLiteErrorCode.Constraint_PrimaryKey || ex.Message.ToLower().Contains("unique constraint failed: employees.employeeid"))
                    {
                        MessageBox.Show($"添加失败：工号 '{employee.EmployeeID}' 已存在。", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show($"添加员工数据库操作失败: {ex.Message}", "数据库错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"添加员工失败: {ex.Message}", "程序错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public static bool UpdateEmployee(Employee employee)
        {
            using (SQLiteConnection conn = new SQLiteConnection(GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Employees SET Name = @Name, HireDate = @HireDate, WorkProcess = @WorkProcess WHERE EmployeeID = @EmployeeID;";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", employee.Name);
                        cmd.Parameters.AddWithValue("@HireDate", employee.HireDate);
                        cmd.Parameters.AddWithValue("@WorkProcess", employee.WorkProcess);
                        cmd.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"更新员工信息失败: {ex.Message}", "数据库错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public static bool DeleteEmployee(string employeeID)
        {
            using (SQLiteConnection conn = new SQLiteConnection(GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM Employees WHERE EmployeeID = @EmployeeID;";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"删除员工失败: {ex.Message}", "数据库错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public static List<Employee> GetAllEmployees()
        {
            var employees = new List<Employee>();
            using (SQLiteConnection conn = new SQLiteConnection(GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT EmployeeID, Name, HireDate, WorkProcess FROM Employees ORDER BY EmployeeID;";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                employees.Add(new Employee
                                {
                                    EmployeeID = reader["EmployeeID"].ToString(),
                                    Name = reader["Name"].ToString(),
                                    HireDate = reader["HireDate"].ToString(),
                                    WorkProcess = reader["WorkProcess"].ToString()
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"查询所有员工信息失败: {ex.Message}", "数据库查询错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            return employees;
        }

        public static Employee GetEmployeeById(string employeeID)
        {
            Employee employee = null;
            using (SQLiteConnection conn = new SQLiteConnection(GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT EmployeeID, Name, HireDate, WorkProcess FROM Employees WHERE EmployeeID = @EmployeeID;";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                employee = new Employee
                                {
                                    EmployeeID = reader["EmployeeID"].ToString(),
                                    Name = reader["Name"].ToString(),
                                    HireDate = reader["HireDate"].ToString(),
                                    WorkProcess = reader["WorkProcess"].ToString()
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"根据工号查询员工信息失败: {ex.Message}", "数据库查询错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            return employee;
        }
        #endregion

        #region PPE Issuance Methods
        // 以下方法依赖于 PPEIssuanceRecord 类的定义
        public static bool AddPPEItemRecord(PPEIssuanceRecord record)
        {
            using (SQLiteConnection conn = new SQLiteConnection(GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        INSERT INTO PPEItems (EmployeeID, ItemName, ItemCode, ItemSize, IssueDate, IssueType, Remarks) 
                        VALUES (@EmployeeID, @ItemName, @ItemCode, @ItemSize, @IssueDate, @IssueType, @Remarks);";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeID", record.EmployeeID);
                        cmd.Parameters.AddWithValue("@ItemName", record.ItemName);
                        cmd.Parameters.AddWithValue("@ItemCode", string.IsNullOrWhiteSpace(record.ItemCode) ? (object)DBNull.Value : record.ItemCode);
                        cmd.Parameters.AddWithValue("@ItemSize", string.IsNullOrWhiteSpace(record.ItemSize) ? (object)DBNull.Value : record.ItemSize);
                        cmd.Parameters.AddWithValue("@IssueDate", record.IssueDate);
                        cmd.Parameters.AddWithValue("@IssueType", record.IssueType);
                        cmd.Parameters.AddWithValue("@Remarks", string.IsNullOrWhiteSpace(record.Remarks) ? (object)DBNull.Value : record.Remarks);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"添加劳保用品发放记录失败: {ex.Message}", "数据库错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public static List<PPEIssuanceRecord> GetAllPPEItemRecords()
        {
            var records = new List<PPEIssuanceRecord>();
            using (SQLiteConnection conn = new SQLiteConnection(GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string query = @"
                SELECT p.IssueID, p.EmployeeID, e.Name AS EmployeeName, 
                       p.ItemName, p.ItemCode, p.ItemSize, p.IssueDate, p.IssueType, p.Remarks 
                FROM PPEItems p
                JOIN Employees e ON p.EmployeeID = e.EmployeeID
                ORDER BY p.IssueDate DESC, p.IssueID DESC;";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                records.Add(new PPEIssuanceRecord
                                {
                                    IssueID = Convert.ToInt32(reader["IssueID"]),
                                    EmployeeID = reader["EmployeeID"].ToString(),
                                    EmployeeName = reader["EmployeeName"].ToString(), // 读取员工姓名
                                    ItemName = reader["ItemName"].ToString(),
                                    ItemCode = reader["ItemCode"] == DBNull.Value ? null : reader["ItemCode"].ToString(),
                                    ItemSize = reader["ItemSize"] == DBNull.Value ? null : reader["ItemSize"].ToString(),
                                    IssueDate = reader["IssueDate"].ToString(),
                                    IssueType = reader["IssueType"].ToString(),
                                    Remarks = reader["Remarks"] == DBNull.Value ? null : reader["Remarks"].ToString()
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"查询所有劳保用品发放记录失败: {ex.Message}", "数据库查询错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            return records;
        }
        #endregion
        #region Query Methods

        // 根据员工姓名或工号查询劳保用品发放记录
        // PPEIssuanceRecord 模型应已包含 EmployeeName 属性
        public static List<PPEIssuanceRecord> SearchPPEIssuanceRecords(string searchTerm, PPESearchType searchType)
        {
            var records = new List<PPEIssuanceRecord>();
            if (string.IsNullOrWhiteSpace(searchTerm)) // 如果搜索词为空，可以返回空列表或所有记录
            {
                // return GetAllPPEItemRecords(); // 或者返回空，取决于需求
                return records; // 当前选择返回空
            }

            using (SQLiteConnection conn = new SQLiteConnection(GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string queryBase = @"
                    SELECT p.IssueID, p.EmployeeID, e.Name AS EmployeeName, 
                           p.ItemName, p.ItemCode, p.ItemSize, p.IssueDate, p.IssueType, p.Remarks 
                    FROM PPEItems p
                    JOIN Employees e ON p.EmployeeID = e.EmployeeID ";

                    string whereClause = "";
                    SQLiteCommand cmd = new SQLiteCommand(conn);

                    if (searchType == PPESearchType.ByEmployeeName)
                    {
                        whereClause = "WHERE e.Name LIKE @SearchTerm ";
                        cmd.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%"); // 模糊查询
                    }
                    else // ByEmployeeID
                    {
                        whereClause = "WHERE p.EmployeeID = @SearchTerm "; // 精确查询工号
                        cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);
                    }

                    cmd.CommandText = queryBase + whereClause + "ORDER BY p.IssueDate DESC, p.IssueID DESC;";

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            records.Add(new PPEIssuanceRecord
                            {
                                IssueID = Convert.ToInt32(reader["IssueID"]),
                                EmployeeID = reader["EmployeeID"].ToString(),
                                EmployeeName = reader["EmployeeName"].ToString(),
                                ItemName = reader["ItemName"].ToString(),
                                ItemCode = reader["ItemCode"] == DBNull.Value ? null : reader["ItemCode"].ToString(),
                                ItemSize = reader["ItemSize"] == DBNull.Value ? null : reader["ItemSize"].ToString(),
                                IssueDate = reader["IssueDate"].ToString(),
                                IssueType = reader["IssueType"].ToString(),
                                Remarks = reader["Remarks"] == DBNull.Value ? null : reader["Remarks"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"查询劳保用品发放记录失败: {ex.Message}", "数据库查询错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return records;
        }

        #endregion
    }
    public enum PPESearchType
    {
        ByEmployeeName,
        ByEmployeeID
    }

}