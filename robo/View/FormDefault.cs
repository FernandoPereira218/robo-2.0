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
        private string tipoFies;
        private FormInterface2 formPrincipal;

        public FormDefault(FormInterface2 formAnterior)
        {
            this.formPrincipal = formAnterior;
            presenter = new ImplementacaoPresenter(this);

            Dados.VerificaSemestre();
            InitializeComponent();

            cbSemestre.DataSource = presenter.PreencherListaSemestre();
            cbSemestre.SelectedIndex = cbSemestre.Items.Count - 1;
            cbAno.DataSource = presenter.PreencherListaAno();
            cbAno.SelectedIndex = cbAno.Items.Count - 1;
            cbMes.SelectedIndex = Util.BuscarMesAtual() - 1;
            PreencherListaIES();
        }

        private void PreencherListaIES()
        {
            List<TOLogin> loginsRegiao;
            List<string> faculdades = new List<string>();
            if (Program.login.Usuario == "Admin")
            {
                loginsRegiao = Dados.SelectAll<TOLogin>();
            }
            else
            {
                loginsRegiao = Dados.SelectWhere<TOLogin>(x => x.Regional == Program.login.Regional);
            }

            foreach (var item in loginsRegiao)
            {
                if (faculdades.Contains(item.Faculdade) == false)
                {
                    faculdades.Add(item.Faculdade);
                }
            }

            cbIES.DataSource = faculdades;
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
            this.tipoFies = tipoFies;
            LimparForm();

            foreach (var item in menuSelecionado.Paineis)
            {
                flowLayoutPanel1.Controls.Find(item, false)[0].Visible = true;
            }


            if (tipoFies.ToUpper() == "FIES LEGADO")
            {
                panelCampus.Visible = true;
            }

            if (Program.login.Permissao == "CAE")
            {
                panelCPF.Visible = true;
                panelImportar.Visible = false;
                txtCPF.Text = string.Empty;
                txtCPF.Mask = "";

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

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            RodarPrograma();
        }
        private void RodarPrograma()
        {
            if (Program.login.Permissao == "CAE")
            {
                presenter.CPFCae = txtCPF.Text;
                if (Util.VerificaCPFValido(presenter.CPFCae) == false)
                {
                    MessageBox.Show("Por favor digite um CPF válido!");
                    return;
                }
            }
            switch (lblExecucao.Text.ToUpper())
            {
                case "ADITAMENTO":
                    if (tipoFies.ToUpper() == "FIES LEGADO")
                    {
                        presenter.ExecutarAditamentoLegado(cbSemestre.Text, cbIES.Text, tipoFies, cbCampus.Text);
                    }
                    else
                    {
                        presenter.ExecutarAditamentoNovo(cbIES.Text, tipoFies, cbSemestre.Text);
                    }
                    break;
                case "IMPORTAR DRI PARA BANCO DE DADOS":
                    presenter.ExecutarDRI(cbIES.Text, tipoFies, cbCampus.Text, cbSituacao.Text, baixarDRI: false);
                    break;
                case "BAIXAR DRI":
                    presenter.ExecutarDRI(cbIES.Text, tipoFies, cbCampus.Text, cbSituacao.Text, baixarDRI: true);
                    break;
                case "BAIXAR DRM":
                    if (tipoFies.ToUpper() == "FIES LEGADO")
                    {
                        presenter.ExecutarBaixarDocumentoLegado(cbIES.Text, tipoFies, cbCampus.Text, cbSemestre.Text, "DRM");
                    }
                    else
                    {
                        presenter.ExecutarBaixarDRMFiesNovo(cbIES.Text, tipoFies, cbSemestre.Text);
                    }
                    break;
                case "BAIXAR DRT":
                    if (tipoFies.ToUpper() == "FIES LEGADO")
                    {
                        presenter.ExecutarBaixarDocumentoLegado(cbIES.Text, tipoFies, cbCampus.Text, cbSemestre.Text, "DRT");
                    }
                    else
                    {

                    }
                    break;
                case "BAIXAR DRD":
                    if (tipoFies.ToUpper() == "FIES LEGADO")
                    {
                        presenter.ExecutarBaixarDocumentoLegado(cbIES.Text, tipoFies, cbCampus.Text, cbSemestre.Text, "DRD");
                    }
                    else
                    {

                    }
                    break;
                case "SUSPENSÃO":
                    if (tipoFies.ToUpper() == "FIES LEGADO")
                    {
                        presenter.ExecutarBaixarDocumentoLegado(cbIES.Text, tipoFies, cbCampus.Text, cbSemestre.Text, "Suspensao");
                    }
                    else
                    {

                    }
                    break;
                case "EXPORTAR DRM":
                    if (tipoFies.ToUpper() == "FIES LEGADO")
                    {
                        presenter.ExecutarExportarRelatoriosLegado(cbIES.Text, tipoFies, cbCampus.Text, cbSemestre.Text, "DRM");
                    }
                    else
                    {
                        presenter.ExportarRelatorioFiesNovo(cbIES.Text, tipoFies, "DRM");
                    }
                    break;
                case "EXPORTAR DRD":
                    if (tipoFies.ToUpper() == "FIES LEGADO")
                    {
                        presenter.ExecutarExportarRelatoriosLegado(cbIES.Text, tipoFies, cbCampus.Text, cbSemestre.Text, "DRD");
                    }
                    else
                    {
                        presenter.ExportarRelatorioFiesNovo(cbIES.Text, tipoFies, "DRD");
                    }
                    break;
                case "EXPORTAR DRT":
                    if (tipoFies.ToUpper() == "FIES LEGADO")
                    {
                        presenter.ExecutarExportarRelatoriosLegado(cbIES.Text, tipoFies, cbCampus.Text, cbSemestre.Text, "DRT");
                    }
                    else
                    {
                        presenter.ExportarRelatorioFiesNovo(cbIES.Text, tipoFies, "DRT");
                    }
                    break;
                case "EXPORTAR SUSPENSÃO":
                    if (tipoFies.ToUpper() == "FIES LEGADO")
                    {
                        presenter.ExecutarExportarRelatoriosLegado(cbIES.Text, tipoFies, cbCampus.Text, cbSemestre.Text, "SUSPENSÃO");
                    }
                    else
                    {
                        presenter.ExportarRelatorioFiesNovo(cbIES.Text, tipoFies, "SUSPENSÃO");
                    }
                    break;
                case "EXTRAIR INFORMAÇÕES DRM":
                    if (tipoFies.ToUpper() == "FIES LEGADO")
                    {
                        presenter.ExtrairInformacoesDRMLegado(cbIES.Text, tipoFies, cbCampus.Text, cbSemestre.Text);
                    }
                    else
                    {
                        presenter.ExtrairInformacoesDRMFiesNovo(cbIES.Text, tipoFies, cbSemestre.Text);
                    }
                    break;
                case "EXPORTAR DRI":
                    presenter.ExecutarExportarDRILegado(cbIES.Text, tipoFies, cbCampus.Text, cbSituacao.Text);
                    break;
                case "EXPORTAR REPASSE":
                    if (tipoFies.ToUpper() == "FIES LEGADO")
                    {
                        presenter.ExportarExtratoMensalDeRepasseLegado(cbIES.Text, tipoFies, cbCampus.Text, cbAno.Text, cbMes.Text);
                    }
                    else
                    {
                        presenter.ExportarRepasseFiesNovo(cbIES.Text, cbMes.Text, cbAno.Text);
                    }
                    break;
                case "ABRIR SITE":
                    presenter.ExecutarAbrirSite(cbIES.Text, cbCampus.Text, tipoFies);
                    break;
                case "BUSCAR STATUS ADITAMENTO":
                    presenter.ExecutarBuscarStatusAditamentoNovo(cbIES.Text, tipoFies, cbSemestre.Text);
                    break;
                case "STATUS ALUNO":
                    presenter.ExecutarStatusAluno(cbIES.Text, tipoFies, cbSemestre.Text);
                    break;
                case "EXPORTAR INADIMPLÊNCIA":
                    presenter.ExportarInadimplencia(cbIES.Text, cbMes.Text, cbAno.Text);
                    break;
                case "EXPORTAR COPARTICIPAÇÃO":
                    presenter.ExportarCoparticipacaoFiesNovo(cbIES.Text, dtpDataInicial.Text, dtpDataFinal.Text);
                    break;
                case "LANÇAMENTO FIES SIGA":
                    presenter.ExecutarLancamentoFiesSiga(cbSemestre.Text, cbFiesSiga.Text);
                    break;
                case "GERAÇÃO PARCELAS FIES SIGA":
                    presenter.GeracaoParcelasFies(cbSemestre.Text);
                    break;
                case "HISTÓRICO DE REPARCELAMENTO DA COPARTICIPAÇÃO":
                    presenter.ExecutarHistoricoReparcelamentoCoparticipacao(cbIES.Text, tipoFies);
                    break;
                case "VALIDAR REPARCELAMENTO":
                    presenter.ValidarReparcelamento(cbIES.Text, tipoFies);
                    break;
                default:
                    MessageBox.Show("Favor criar o CASE antes de rodar!!!!!");
                    break;
            }
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            formPrincipal.btnSelectPath.PerformClick();
            int countAlunos = Dados.Count<TOAluno>();
            setText(countAlunos);
        }

        private void txtCPF_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtCPF.TextLength >= 11 && e.KeyChar != (char)Keys.Back && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtCPF_MouseClick(object sender, MouseEventArgs e)
        {
            txtCPF.Mask = "000,000,000-00";
            txtCPF.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            for (int i = 0; i < 100; i++)
            {
                backgroundWorker.ReportProgress(i);
                System.Threading.Thread.Sleep(500);
            }
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            formPrincipal.flpModosDeExecucao.Enabled = false;
            btnImportar.Enabled = false;
            btnIniciar.Enabled = false;
            circularProgressBar1.Visible = true;
            circularProgressBar1.Value = e.ProgressPercentage;

            if (e.ProgressPercentage == 0)
            {
                circularProgressBar1.Text = "Abrindo site...";
            }
            else
            {
                circularProgressBar1.Text = e.ProgressPercentage.ToString() + "%";
            }
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Processamentos concluídos com sucesso!");
            btnImportar.Enabled = true;
            btnIniciar.Enabled = true;
            formPrincipal.flpModosDeExecucao.Enabled = true;
        }
    }
}
