using Robo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace robo.View
{
    public partial class FormConfiguracoes : Form
    {
        public FormConfiguracoes()
        {
            InitializeComponent();
        }

        private void btLogins_Click(object sender, EventArgs e)
        {
            panelLogins.BringToFront();
            AtualizarListViewLogins();
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            panelUsuarios.BringToFront();
            AtualizarListViewUsuarios();
        }
        public void AtualizarListViewLogins()
        {
            var source = new BindingSource();

            if (Dados.Count<TOLogin>() == 0)
            {
                dgvLogins.Visible = false;
            }
            else
            {
                dgvLogins.Visible = true;
                source.DataSource = Dados.SelectAll<TOLogin>();
                dgvLogins.AutoGenerateColumns = true;
                dgvLogins.DataSource = source;
                dgvLogins.Columns[dgvLogins.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }
        public void AtualizarListViewUsuarios()
        {
            var source = new BindingSource();
            List<TOUsuario> Usuarios = Dados.SelectUsuarioWhereIES(Program.login.IES.ToUpper());

            if (Usuarios.Count == 0)
            {
                dgvUsuarios.Visible = false;
            }
            else
            {
                dgvUsuarios.Visible = true;
                source.DataSource = Usuarios;
                dgvUsuarios.AutoGenerateColumns = true;
                dgvUsuarios.DataSource = source;
                dgvUsuarios.Columns[dgvUsuarios.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }
        //Modificar Logins
        private void btnAdicionarLogin_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm(this.Location);
            loginForm.ShowDialog();
            AtualizarListViewLogins();


        }
        private void btnModificarLogin_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm(this.Location, dgvLogins.CurrentRow.DataBoundItem as TOLogin);
            loginForm.ShowDialog();
            AtualizarListViewLogins();
        }
        private void btnExcluirLogin_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir este usuário?", "Excluir usuário", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Dados.DeleteLite<TOLogin>(dgvLogins.CurrentRow.DataBoundItem as TOLogin);
                MessageBox.Show("Login excluido com sucesso!", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AtualizarListViewLogins();
            }
        }
        // Mudar? - Modificações Usuarios
        private void btAddUsuario_Click(object sender, EventArgs e)
        {
            UsuarioForm usuarioForm = new UsuarioForm(this.Location);
            usuarioForm.ShowDialog();
            AtualizarListViewUsuarios();
        }
        private void btModUsuario_Click(object sender, EventArgs e)
        {
            UsuarioForm usuarioForm = new UsuarioForm(this.Location, dgvUsuarios.CurrentRow.DataBoundItem as TOUsuario);
            usuarioForm.ShowDialog();
            AtualizarListViewUsuarios();

        }
        private void btExcUsuario_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir este usuário?", "Excluir usuário", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Dados.DeleteLite<TOUsuario>(dgvUsuarios.CurrentRow.DataBoundItem as TOUsuario);
                MessageBox.Show("Login excluido com sucesso!", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AtualizarListViewUsuarios();
            }
        }

        private void btBackup_Click(object sender, EventArgs e)
        {
            string verificacao = string.Empty;

            
            panelBackup.BringToFront();
            OpenFileDialog backup = new OpenFileDialog();
            backup.Filter  = "DB (*.db)|*.db";
            backup.InitialDirectory = Directory.GetCurrentDirectory() + "\\Backup\\";
            if (Dados.Count<TOAluno>() > 0)
            {
                if (MessageBox.Show("Existe dados no banco. Deseja exportar ?", "Exportar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    verificacao = "Se sim os dados atuais serão perdidos !";
                    Util.ExportarCSV(Dados.Count<TOAluno>(), "Alunos");
                }
            }

            if (MessageBox.Show("Deseja realizar o backup ?  " + verificacao, "Backup", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (backup.ShowDialog() == DialogResult.OK)
                {
                    File.Delete("Data/bdbot1.db");
                    File.Copy(backup.FileName, "Data/bdbot1.db");
                    MessageBox.Show("Backup Executado com Sucesso");
                }
            }



            
        }
    }
}
