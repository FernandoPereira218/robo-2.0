using robo.Banco_de_Dados;
using robo.TO;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace robo.Interface
{
    public partial class LoginForm : Form
    {
        private string loginAdmin;
        public LoginForm(Point location, TOLogin login = null)
        {
            InitializeComponent();
            this.Location = location;
            InicializarCbPlataforma();
            InicializarCbRegional();
            if (login != null)
            {
                InicializarAtualizarLogin(login);
            }
            else
            {
                InicializarCriarNovoLogin();
            }
        }

        private void InicializarCbPlataforma()
        {
            List<TOLogin> logins = Dados.SelectAll<TOLogin>();
            this.cbPlataformaLogin.DataSource = PreencherLista(logins, nameof(TOLogin.Plataforma));
        }

        private void InicializarCbRegional()
        {
            if (Program.login.Usuario != "Admin")
            {
                this.cbRegionalLogin.Text = Program.login.Regional;
                this.cbRegionalLogin.Enabled = false;
            }
            else
            {
                List<TOLogin> logins = Dados.SelectAll<TOLogin>();
                this.cbRegionalLogin.DataSource = PreencherLista(logins, nameof(TOLogin.Regional));
            }
        }
        private List<string> PreencherLista(List<TOLogin> logins, string nomePropriedade)
        {
            List<string> lista = new List<string>();
            foreach (TOLogin login in logins)
            {
                string valorPropriedade = (string)login.GetType().GetProperty(nomePropriedade).GetValue(login);
                if (lista.Contains(valorPropriedade) == false)
                {
                    lista.Add(valorPropriedade);
                }
            }
            return lista;
        }

        private void InicializarCriarNovoLogin()
        {
            this.txtID.Text = Convert.ToString(Dados.Count<TOLogin>() + 1);
            loginAdmin = "Não";
            this.txtID.Enabled = false;
            this.btnOKLogin.Text = "Aceitar";
            this.btnOKLogin.Click -= btnAtualizarLogin_Click;
            this.btnOKLogin.Click += btnInserirLogin_Click;
        }

        private void InicializarAtualizarLogin(TOLogin login)
        {
            txtID.Text = login.Id.ToString();
            txtUser.Text = login.Usuario;
            txtSenhaLogin.Text = login.Senha;
            txtFaculdadeLogin.Text = login.Faculdade;
            txtCampusLogin.Text = login.Campus;
            cbPlataformaLogin.Text = login.Plataforma;
            cbRegionalLogin.Text = login.Regional;
            loginAdmin = login.Admin;

            this.btnOKLogin.Text = "Atualizar";
            this.txtID.Enabled = false;
            this.btnOKLogin.Click -= btnInserirLogin_Click;
            this.btnOKLogin.Click += btnAtualizarLogin_Click;
        }

        private void btnCancelLogin_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInserirLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Dados.InsertDocumento<TOLogin>(LoginPreenchido());
                MessageBox.Show("Login adicionado com sucesso.");
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

        private void btnAtualizarLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Dados.UpdateDocumento<TOLogin>(LoginPreenchido());
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

        private TOLogin LoginPreenchido()
        {
            TOLogin login = new TOLogin()
            {
                Id = Convert.ToInt32(txtID.Text),
                Usuario = txtUser.Text,
                Senha = txtSenhaLogin.Text,
                Faculdade = txtFaculdadeLogin.Text,
                Campus = txtCampusLogin.Text,
                Plataforma = cbPlataformaLogin.Text,
                Regional = cbRegionalLogin.Text,
                Admin = loginAdmin
            };

            return login;
        }
    }
}
