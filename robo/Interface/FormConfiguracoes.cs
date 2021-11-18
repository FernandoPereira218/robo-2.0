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

namespace robo.View
{
    public partial class FormConfiguracoes : Form
    {
        public static bool verificacao { get; private set; }
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public FormConfiguracoes()
        {
            InitializeComponent();
            verificacao = true;
            this.Text = string.Empty;
            AtualizarDataGridLogins();
        }

        public void AtualizarDataGridLogins()
        {
            List<TOLogin> source;

            dgvLogins.Visible = true;
            if (Program.login.Usuario == "Admin")
            {
                source = Dados.SelectAll<TOLogin>();
            }
            else
            {
                source = Dados.SelectWhere<TOLogin>(x => x.Faculdade == Program.login.IES);
            }

            dgvLogins.AutoGenerateColumns = true;
            dgvLogins.DataSource = source;
            dgvLogins.Columns["ID_Legado"].Visible = false;
        }
        public void AtualizarDataGridUsuarios()
        {
            List<TOUsuario> usuarios;

            dgvUsuarios.Visible = true;
            dgvUsuarios.AutoGenerateColumns = true;

            if (Program.login.Usuario == "Admin")
            {
                usuarios = Dados.SelectAll<TOUsuario>();
            }
            else
            {
                usuarios = Dados.SelectUsuarioWhereIES(Program.login.IES.ToUpper());
            }
            dgvUsuarios.DataSource = usuarios;
            dgvUsuarios.Columns["Senha"].Visible = false;
        }

        //Eventos do form
        private void panelMenuBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void FormConfiguracoes_FormClosing(object sender, FormClosingEventArgs e)
        {
            verificacao = false;
        }

        //Eventos de click
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btBackup_Click(object sender, EventArgs e)
        {
            string verificacao = string.Empty;
            OpenFileDialog backup = new OpenFileDialog();
            backup.Filter = "DB (*.db)|*.db";
            backup.InitialDirectory = Directory.GetCurrentDirectory() + "\\Backup\\";
            if (Dados.Count<TOAluno>() > 0)
            {
                if (MessageBox.Show("Existe dados no banco. Deseja exportar?", "Exportar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    verificacao = "Se sim os dados atuais serão perdidos !";
                    Util.ExportarCSV(Dados.Count<TOAluno>(), "Alunos");
                }
            }

            if (MessageBox.Show("Deseja realizar o backup?  " + verificacao, "Backup", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (backup.ShowDialog() == DialogResult.OK)
                {
                    File.Delete("Data/bdbot.db");
                    File.Copy(backup.FileName, "Data/bdbot.db");
                    MessageBox.Show("Backup Executado com Sucesso");
                }
            }




        }
        private void btLogins_Click(object sender, EventArgs e)
        {
            panelLogins.Visible = true;
            panelUsuarios.Visible = false;
            AtualizarDataGridLogins();
        }
        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            panelUsuarios.Visible = true;
            panelLogins.Visible = false;
            AtualizarDataGridUsuarios();
        }
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        //Logins
        private void btnAdicionarLogin_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm(this.Location);
            loginForm.ShowDialog();
            AtualizarDataGridLogins();
        }
        private void btnModificarLogin_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm(this.Location, dgvLogins.CurrentRow.DataBoundItem as TOLogin);
            loginForm.ShowDialog();
            AtualizarDataGridLogins();
        }
        private void btnExcluirLogin_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir este usuário?", "Excluir usuário", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Dados.DeleteLite<TOLogin>(dgvLogins.CurrentRow.DataBoundItem as TOLogin);
                MessageBox.Show("Login excluido com sucesso!", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AtualizarDataGridLogins();
            }
        }
        //Usuários
        private void btAddUsuario_Click(object sender, EventArgs e)
        {
            UsuarioForm usuarioForm = new UsuarioForm(this.Location);
            usuarioForm.ShowDialog();
            AtualizarDataGridUsuarios();
        }
        private void btModUsuario_Click(object sender, EventArgs e)
        {
            UsuarioForm usuarioForm = new UsuarioForm(this.Location, dgvUsuarios.CurrentRow.DataBoundItem as TOUsuario);
            usuarioForm.ShowDialog();
            AtualizarDataGridUsuarios();

        }
        private void btExcUsuario_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir este usuário?", "Excluir usuário", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Dados.DeleteLite<TOUsuario>(dgvUsuarios.CurrentRow.DataBoundItem as TOUsuario);
                MessageBox.Show("Login excluido com sucesso!", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AtualizarDataGridUsuarios();
            }
        }
    }
}
