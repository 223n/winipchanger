using System;
using System.Windows.Forms;
using WinIPChanger.Desktop.Forms;

[assembly: CLSCompliant(false)]
namespace WinIPChanger.Desktop
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                using (var form = new MainForm())
                {
                    form.LoadInfoFile();
                    Application.Run();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
