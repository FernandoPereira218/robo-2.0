using robo;
using robo.TO;
using System;
using System.Drawing;
using robo.Utils;
using System.Windows.Forms;
using robo.Banco_de_Dados;
using System.Collections.Generic;

namespace robo.Interface
{
    public partial class UsuarioForm : Form
    {
        public UsuarioForm(Point location, TOUsuario usuario = null)
        {
            InitializeComponent();
            this.Location = location;
            PreencherCbIES();
            PreencherCbRegional();
            if (usuario != null)
            {
                InicializarAtualizarUsuario(usuario);
            }
            else
            {
                InicializarNovoUsuario();
            }
        }

        private void PreencherCbIES()
        {
            if (Program.login.Usuario != "Admin")
            {
                List<TOUsuario> usuarios = Dados.SelectWhere<TOUsuario>(x => x.Regional == Program.login.Regional);
                cbIES.DataSource = PreencherLista(usuarios, nameof(TOUsuario.IES));
            }
            else
            {
                List<TOUsuario> usuarios = Dados.SelectAll<TOUsuario>();
                cbIES.DataSource = PreencherLista(usuarios, nameof(TOUsuario.IES));
            }
        }

        private void PreencherCbRegional()
        {
            if (Program.login.Usuario != "Admin")
            {
                cbRegional.Text = Program.login.Regional;
                cbRegional.Enabled = false;
            }
            else
            {
                List<TOUsuario> usuarios = Dados.SelectAll<TOUsuario>();
                cbRegional.DataSource = PreencherLista(usuarios, nameof(TOUsuario.Regional));
            }
        }

        private List<string> PreencherLista(List<TOUsuario> usuarios, string nomePropriedade)
        {
            List<string> lista = new List<string>();
            foreach (TOUsuario usuario in usuarios)
            {
                string valorPropriedade = (string)usuario.GetType().GetProperty(nomePropriedade).GetValue(usuario);
                if (lista.Contains(valorPropriedade) == false)
                {
                    lista.Add(valorPropriedade);
                }
            }
            return lista;
        }

        private void InicializarAtualizarUsuario(TOUsuario usuario)
        {
            txtId.Text = usuario.Id.ToString();
            txtUser.Text = usuario.Usuario;
            txtSenhaUsuario.Text = string.Empty;
            cbPermissoes.Text = usuario.Permissao;
            cbRegional.Text = usuario.Regional;

            this.btnOKLogin.Text = "Atualizar";
            this.txtId.Enabled = false;
            this.txtUser.Enabled = false;
            this.cbRegional.Enabled = false;
            this.btnOKLogin.Click -= btnNovoUsuario_Click;
            this.btnOKLogin.Click += btnAtualizarUsuario_Click;
        }

        private void InicializarNovoUsuario()
        {
            this.txtId.Text = Convert.ToString(Dados.Count<TOUsuario>() + 1);
            this.txtId.Enabled = false;
            this.btnOKLogin.Text = "Aceitar";
            this.btnOKLogin.Click -= btnAtualizarUsuario_Click;
            this.btnOKLogin.Click += btnNovoUsuario_Click;
        }

        private void btnCancelLogin_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNovoUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                Dados.InsertDocumento<TOUsuario>(UsuarioPreenchido());
                MessageBox.Show("Usuario adicionado com sucesso.");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            finally
            {
                this.Close();
            }
        }

        private void btnAtualizarUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                Dados.UpdateDocumento<TOUsuario>(UsuarioPreenchido());
                MessageBox.Show("Login atualizado com sucesso.");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            finally
            {
                this.Close();
            }
        }

        private TOUsuario UsuarioPreenchido()
        {
            TOUsuario Usuario = new TOUsuario()
            {
                Id = Convert.ToInt32(txtId.Text),
                Usuario = txtUser.Text,
                Senha = Util.GetMD5(txtSenhaUsuario.Text),
                Permissao = cbPermissoes.Text,
                IES = cbIES.Text,
                Regional = cbRegional.Text
            };
            return Usuario;
        }
    }
}
