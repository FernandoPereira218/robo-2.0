using MetroFramework.Controls;
using robo.Control.Implementacoes;
using Robo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static robo.View.FormInterface2;

namespace robo.View
{
    public partial class FormDefault : Form, IContratos.IMainForms
    {
        private static ImplementacaoPresenter presenter;
        public FormDefault()
        {
            presenter = new ImplementacaoPresenter(this);

            Dados.VerificaSemestre();
            InitializeComponent();

            cbSemestre.DataSource = presenter.PreencherListaSemestre();
            cbSemestre.SelectedIndex = cbSemestre.Items.Count - 1;
        }
        private void LimparForm()
        {
            foreach (System.Windows.Forms.Control control in flowLayoutPanel1.Controls)
            {
                control.Visible = false;
            }
        }
        public void UpdateForm(TOMenus menuSelecionado, string tipoFies)
        {
            LimparForm();

            foreach (var item in menuSelecionado.Paineis)
            {
                flowLayoutPanel1.Controls.Find(item, false)[0].Visible = true;
            }


            if (tipoFies.ToUpper() == "FIES LEGADO")
            {
                panelCampus.Visible = true;
            }

            lblExecucao.Text = menuSelecionado.Item;

        }
        public void setText(int cont)
        {

            if (cont == 0)
            {
                lblAlunosImportados.Text = "Nenhum aluno importado!";
            }
            else
            {
                lblAlunosImportados.Text = cont + " alunos importados no Banco de Dados";
            }
        }

        private void cbIES_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbCampus.DataSource = Dados.SelectLoginTOIES(cbIES.Text, "FIES LEGADO");
        }
    }
}
