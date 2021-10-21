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

namespace robo.View
{
    public partial class FormInterface2 : Form
    {
        FormDefault formulario;
        public FormInterface2()
        {
            InitializeComponent();
        }

        private void btnFiesNovo_Click(object sender, EventArgs e)
        {

            LimparModosDeExecucao();

            SelecionarMenusPorTipoFies("FIES NOVO");
        }
        private void SelecionarMenusPorTipoFies(string tipoFies)
        {
            List<TOMenus> menusFIESNovo = Dados.SelectMenuWhereLite(tipoFies);
            labelTipoFies.Text = tipoFies;

            foreach (var menu in menusFIESNovo)
            {
                Button btn = new Button();
                btn.Text = menu.Item;
                btn.Font = new Font("Century Gothic ", 9.75f);
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.Size = new Size(flpModosDeExecucao.Width - 23, 40);

                //btn_1.Size = new Size(width, height);
                btn.Tag = menu;
                btn.Click += ExecutarAlgumaCoisa;
                flpModosDeExecucao.Controls.Add(btn);
            }
            //flpModosDeExecucao.Controls[1].Visible = false;
            //flpModosDeExecucao.Controls[2].Visible = false;
        }

        private void ExecutarAlgumaCoisa(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            StartForm((TOMenus)btn.Tag, labelTipoFies.Text.ToUpper());
        }

        private void StartForm(TOMenus menu, string tipoFies)
        {
            int cont = Dados.Count<TOAluno>();
            //panelCadastro.Controls.Clear();
            if (formulario == null)
            {
                formulario = new FormDefault();
                formulario.TopLevel = false;
                formulario.FormBorderStyle = FormBorderStyle.None;
                formulario.Dock = DockStyle.Fill;
                panelCadastro.Controls.Add(formulario);
                formulario.Show();
                formulario.BringToFront();
            }
            lblExecucao.Text = "SISTEMA DE " + menu.Item;
            if (menu.Planilha == true)
            {
                btnPlanilha.Visible = true;
                formulario.setText(cont);
            }
            else
            {
                btnPlanilha.Visible = false;
            }
            formulario.Visible = true;
            formulario.UpdateForm(menu, tipoFies);
            panelCadastro.BringToFront();
            if (cont == 0 && menu.Planilha == true)
            {
                panelErroNenhumAluno.BringToFront();
            }
        }

        private void LimparModosDeExecucao()
        {
            foreach (System.Windows.Forms.Control item in panelCadastro.Controls)
            {
                if (item.GetType() == typeof(FormDefault))
                {
                    item.Visible = false;
                    break;
                }
            }
            if (flpModosDeExecucao.Controls.Count > 0)
            {
                flpModosDeExecucao.Controls.Clear();
            }
        }

        private void btnFiesLegado_Click(object sender, EventArgs e)
        {
            LimparModosDeExecucao();
            SelecionarMenusPorTipoFies("FIES LEGADO");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSiga_Click(object sender, EventArgs e)
        {
            LimparModosDeExecucao();
            SelecionarMenusPorTipoFies("SIGA");
        }

        private void btnPlanilha_Click(object sender, EventArgs e)
        {
            panelExcel.BringToFront();
        }

        private void btnExecucao_Click(object sender, EventArgs e)
        {
            formulario.Visible = true;
            panelCadastro.BringToFront();
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            panelExcel.BringToFront();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            panelCadastro.BringToFront();
        }
    }
}
