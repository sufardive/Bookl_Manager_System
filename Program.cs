using Bookl_Manager_System.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var contextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite("Data Source=library.db")
            .Options;
            Application.Run(new MainForm(contextOptions));
            
        }
    }
}
