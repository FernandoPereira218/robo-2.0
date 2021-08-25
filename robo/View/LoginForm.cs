using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Robo
{
    public partial class LoginForm : Form
    {
        public LoginForm(Point location, TOLogin login = null)
        {
            InitializeComponent();
            this.Location = location;
            if (login != null)
            {
                txtUser.Text = login.Usuario;
                txtSenhaLogin.Text = login.Senha;
                txtFaculdadeLogin.Text = login.Faculdade;
                txtCampusLogin.Text = login.Campus;
                txtPlataformaLogin.Text = login.Plataforma;
                txtRegionalLogin.Text = login.Regional;

                this.btnOKLogin.Text = "Atualizar";
                this.txtUser.Enabled = false;
                this.btnOKLogin.Click -= new System.EventHandler(this.btnOKLogin_Click);
                this.btnOKLogin.Click += new System.EventHandler(this.btnAtualizarLogin_Click);
            }
            else
            {
                txtUser.Text = String.Empty;
                txtSenhaLogin.Text = String.Empty;
                txtFaculdadeLogin.Text = String.Empty;
                txtCampusLogin.Text = String.Empty;
                txtPlataformaLogin.Text = String.Empty;
                txtRegionalLogin.Text = String.Empty;

                this.btnOKLogin.Text = "Aceitar";
                this.txtUser.Enabled = true;
                this.btnOKLogin.Click -= new System.EventHandler(this.btnAtualizarLogin_Click);
                this.btnOKLogin.Click += new System.EventHandler(this.btnOKLogin_Click);
                

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
                Dados.InsertLogin(LoginPreenchido());
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
                Dados.UpdateLogin(LoginPreenchido());
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
