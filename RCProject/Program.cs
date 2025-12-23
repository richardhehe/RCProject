using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RCProject
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool isRuned;
            Mutex mutex = new Mutex(true, "RCProject", out isRuned);
            if (isRuned)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Frm_Main());
            }
            else
            {
                MessageBox.Show("程序已启动！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
    }
}
