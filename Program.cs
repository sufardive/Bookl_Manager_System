using Bookl_Manager_System.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Windows.Forms;

namespace Bookl_Manager_System
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string dbPath = Path.Combine(Path.GetDirectoryName(typeof(AppDbContext).Assembly.Location), "library.db");
            var contextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite($"Data Source={dbPath}")
            .Options;
            Application.Run(new MainForm(contextOptions));
            
        }
    }
}
