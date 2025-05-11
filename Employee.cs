// Employee.cs
namespace PPERPSystem // 确保命名空间与您的项目一致
{
    public class Employee
    {
        public string EmployeeID { get; set; } // 工号
        public string Name { get; set; }       // 姓名
        public string HireDate { get; set; }   // 入职时间 (存储为 "YYYY-MM-DD" 格式的字符串)
        public string WorkProcess { get; set; } // 分配工序

        // 可以添加一个构造函数方便创建对象
        public Employee() { }

        public Employee(string employeeID, string name, string hireDate, string workProcess)
        {
            EmployeeID = employeeID;
            Name = name;
            HireDate = hireDate;
            WorkProcess = workProcess;
        }
    }
}