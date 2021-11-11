using robo;
using robo.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Robo
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static TOUsuario login;
        public static Login formLogin;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
        }
    }
}