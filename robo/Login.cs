using robo.View;
using Robo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace robo
{
    public partial class Login : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        private static string sessionFile = "session.dat";
        public Login()
        {
            InitializeComponent();
            Program.formLogin = this;
            FormBorderStyle = FormBorderStyle.None;
        }
        private void VerifySession()
        {
            if (File.Exists(sessionFile) == true)
            {
                string[] temp = File.ReadAllText(sessionFile).Split('\n');
                Program.login = Dados.ValidateSession(temp[0], temp[1]);
                if (Program.login != null)
                {
                    FormInterface2 formSearch = new FormInterface2();
                    this.Hide();
                    formSearch.Show();
                }
            }
        }

        private void btConfirma_Click(object sender, EventArgs e)
        {
            Program.login = Dados.ValidateLogin(txtUsuario.Text, txtSenha.Text);
            if (Program.login != null)
            {
                if (cbManterLogado.Checked == true)
                {
                    if (File.Exists(sessionFile) == false)
                    {
                        File.WriteAllText(sessionFile, Program.login.Usuario + "\n" + Program.login.Senha);
                    }
                }
                else
                {
                    if (File.Exists(sessionFile) == true)
                    {
                        File.Delete(sessionFile);
                    }
                }

                FormInterface2 formUsuarios = new FormInterface2();
                this.Hide();
                formUsuarios.Show();
            }
            else
            {
                MessageBox.Show("Usuário e/ou senha incorreto(s).");
            }

        }
        private void Login_Shown(object sender, EventArgs e)
        {
            VerifySession();
        }





        private void cbManterLogado_CheckedChanged(object sender, EventArgs e)
        {
            if (cbManterLogado.Checked)
                cbManterLogado.ForeColor = Color.Black;
            else
                cbManterLogado.ForeColor = Color.White;
        }

        private void txtValidacaoCampos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtSenha.Text != null
                    && txtUsuario.Text != null)
                {
                    btConfirma.PerformClick();
                }
            }
        }

        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
