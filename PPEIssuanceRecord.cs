// PPEIssuanceRecord.cs
namespace PPERPSystem
{
    public class PPEIssuanceRecord
    {
        public int IssueID { get; set; } // 主键, 数据库自动递增, 添加时不需要用户输入
        public string EmployeeID { get; set; } // 领用员工的工号
        public string ItemName { get; set; }   // 劳保用品名称
        public string ItemCode { get; set; }   // 唯一编码 (可为空)
        public string ItemSize { get; set; }  // 新增：尺码属性
        public string IssueDate { get; set; }  // 发放日期 "YYYY-MM-DD"
        public string IssueType { get; set; }  // 发放类型 ("初次发放", "更换")
        public string Remarks { get; set; }    // 备注 (可为空)
        public string EmployeeName { get; set; } // 新增员工姓名字段

        public PPEIssuanceRecord() { }
    }
}