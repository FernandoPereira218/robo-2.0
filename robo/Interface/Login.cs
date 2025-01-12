﻿using robo.Banco_de_Dados;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace robo.Interface
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
                Program.login = Dados.ValidarSessao(temp[0], temp[1]);
                if (Program.login != null)
                {
                    FormInterface formSearch = new FormInterface();
                    this.Shown += Login_Shown;
                    formSearch.Show();
                }
            }
        }

        private void btConfirma_Click(object sender, EventArgs e)
        {
            Program.login = Dados.ValidarLogin(txtUsuario.Text, txtSenha.Text);
            if (Program.login != null)
            {
                CriarArquivoDeSessao();
                FormInterface formUsuarios = new FormInterface();
                this.Hide();
                formUsuarios.Show();
            }
            else
            {
                MessageBox.Show("Usuário e/ou senha incorreto(s).");
            }

        }

        private void CriarArquivoDeSessao()
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
        }

        private void Login_Shown(object sender, EventArgs e)
        {
            this.Hide();
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
                if (txtSenha.Text != string.Empty
                    && txtUsuario.Text != string.Empty)
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
            VerifySession();
        }
    }
}
