using System;
using System.Windows.Forms;

namespace PPERPSystem // 确保这里的命名空间与您的项目名称一致
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1()); // Form1 是我们的主窗体
        }
    }
}