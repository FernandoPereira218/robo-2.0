using robo.Banco_de_Dados;
using robo.TO;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace robo.Interface
{
    public partial class LoginForm : Form
    {
        public LoginForm(Point location, TOLogin login = null)
        {
            InitializeComponent();
            this.Location = location;
            if (login != null)
            {
                txtID.Text = login.Id.ToString();
                txtUser.Text = login.Usuario;
                txtSenhaLogin.Text = login.Senha;
                txtFaculdadeLogin.Text = login.Faculdade;
                txtCampusLogin.Text = login.Campus;
                txtPlataformaLogin.Text = login.Plataforma;
                txtRegionalLogin.Text = login.Regional;

                this.btnOKLogin.Text = "Atualizar";
                this.txtID.Enabled = false;
                this.btnOKLogin.Click -= new EventHandler(this.btnOKLogin_Click);
                this.btnOKLogin.Click += new EventHandler(this.btnAtualizarLogin_Click);
            }
            else
            {
                txtUser.Text = string.Empty;
                txtSenhaLogin.Text = string.Empty;
                txtFaculdadeLogin.Text = string.Empty;
                txtCampusLogin.Text = string.Empty;
                txtPlataformaLogin.Text = string.Empty;
                txtRegionalLogin.Text = string.Empty;

                this.btnOKLogin.Text = "Aceitar";
                this.txtUser.Enabled = true;
                this.btnOKLogin.Click -= new EventHandler(this.btnAtualizarLogin_Click);
                this.btnOKLogin.Click += new EventHandler(this.btnOKLogin_Click);
                

            }
        }

        private void btnCancelLogin_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOKLogin_Click(object sender, EventArgs e)
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
            TOLogin login = new TOLogin();
            login.Id = Convert.ToInt32(txtID.Text);
            login.Usuario = txtUser.Text;
            login.Senha = txtSenhaLogin.Text;
            login.Faculdade = txtFaculdadeLogin.Text;
            login.Campus = txtCampusLogin.Text;
            login.Plataforma = txtPlataformaLogin.Text;
            login.Regional = txtRegionalLogin.Text;
     
            return login;
        }
    }
}
