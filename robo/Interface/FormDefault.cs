using OpenQA.Selenium;
using robo.Banco_de_Dados;
using robo.Contratos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using robo.Utils;
using System.Windows.Forms;
using robo.TO;

namespace robo.Interface
{
    public partial class FormDefault : Form
    {
        private static ImplementacaoPresenter presenter;
        private FormInterface formPrincipal;
        private string tipoFies;
        private string execucao;
        private string excessaoCausada = string.Empty;
        public FormDefault(FormInterface formAnterior)
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
            panelCadastro.Visible = true;
            lblStatus.Text = "Pronto";
            PreencherListaIES();

            tooltip.SetToolTip(btnHelp, "Clique uma vez para ajuda ou clique novamente para fechar");
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
            btnHelp.Location = new Point(lblExecucao.Size.Width + 10, 6);
            wbHelp.Visible = false;
        }
        public void setText(int cont)
        {
            if (cont == 0)
            {
                lblAlunosImportados.Text = "Nenhum aluno importado!";
                lblStatus.Text = "Sem alunos para importar.";
            }
            else
            {
                lblAlunosImportados.Text = "Clique em ATUALIZAR para Importar os novos Alunos a Processar.";
                lblStatus.Text = cont + " alunos importados no Banco de Dados";
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
            excessaoCausada = string.Empty;
            if (Program.login.Permissao == "CAE")
            {
                presenter.CPFCae = txtCPF.Text;
                if (Util.VerificaCPFValido(presenter.CPFCae) == false)
                {
                    MessageBox.Show("Por favor digite um CPF válido!");
                    lblStatus.Text = "O CPF digitado está incorreto, por favor digite novamente";
                    return;
                }
            }
            string IES = cbIES.Text;
            string semestre = cbSemestre.Text;
            execucao = lblExecucao.Text.ToUpper();
            string campus = cbCampus.Text;
            string situacao = cbSituacao.Text;
            string mes = cbMes.Text;
            string ano = cbAno.Text;
            string dataInicio = dtpDataInicial.Text;
            string dataFim = dtpDataFinal.Text;
            string fiesSiga = cbFiesSiga.Text;
            bool todosMeses = checkBoxTodosMeses.Checked;
            backgroundWorker.Dispose();
            GC.Collect();
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;
            backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
            backgroundWorker.DoWork += (object sender, DoWorkEventArgs e) =>
            {
                try
                {
                    backgroundWorker.ReportProgress(0);
                    switch (execucao)
                    {
                        case "ADITAMENTO":
                            if (tipoFies.ToUpper() == "FIES LEGADO")
                            {
                                presenter.ExecutarAditamentoLegado(semestre, IES, tipoFies, campus);
                            }
                            else
                            {
                                presenter.ExecutarAditamentoNovo(IES, tipoFies, semestre);
                            }
                            break;
                        case "IMPORTAR DRI PARA BANCO DE DADOS":
                            presenter.ExecutarDRI(IES, tipoFies, campus, "Contratado", baixarDRI: false);
                            break;
                        case "BAIXAR DRI":
                            presenter.ExecutarDRI(IES, tipoFies, campus, situacao, baixarDRI: true);
                            break;
                        case "BAIXAR DRM":
                            if (tipoFies.ToUpper() == "FIES LEGADO")
                            {
                                presenter.ExecutarBaixarDocumentoLegado(IES, tipoFies, campus, semestre, "DRM");
                            }
                            else
                            {
                                presenter.ExecutarBaixarDRMFiesNovo(IES, tipoFies, semestre);
                            }
                            break;
                        case "BAIXAR DRT":
                            if (tipoFies.ToUpper() == "FIES LEGADO")
                            {
                                presenter.ExecutarBaixarDocumentoLegado(IES, tipoFies, campus, semestre, "DRT");
                            }
                            else
                            {

                            }
                            break;
                        case "BAIXAR DRD":
                            if (tipoFies.ToUpper() == "FIES LEGADO")
                            {
                                presenter.ExecutarBaixarDocumentoLegado(IES, tipoFies, campus, semestre, "DRD");
                            }
                            else
                            {

                            }
                            break;
                        case "SUSPENSÃO":
                            if (tipoFies.ToUpper() == "FIES LEGADO")
                            {
                                presenter.ExecutarBaixarDocumentoLegado(IES, tipoFies, campus, semestre, "Suspensao");
                            }
                            else
                            {

                            }
                            break;
                        case "EXPORTAR DRM":
                            if (tipoFies.ToUpper() == "FIES LEGADO")
                            {
                                presenter.ExecutarExportarRelatoriosLegado(IES, tipoFies, campus, semestre, "DRM");
                            }
                            else
                            {
                                presenter.ExportarRelatorioFiesNovo(IES, tipoFies, "DRM");
                            }
                            break;
                        case "EXPORTAR DRD":
                            if (tipoFies.ToUpper() == "FIES LEGADO")
                            {
                                presenter.ExecutarExportarRelatoriosLegado(IES, tipoFies, campus, semestre, "DRD");
                            }
                            else
                            {
                                presenter.ExportarRelatorioFiesNovo(IES, tipoFies, "DRD");
                            }
                            break;
                        case "EXPORTAR DRT":
                            if (tipoFies.ToUpper() == "FIES LEGADO")
                            {
                                presenter.ExecutarExportarRelatoriosLegado(IES, tipoFies, campus, semestre, "DRT");
                            }
                            else
                            {
                                presenter.ExportarRelatorioFiesNovo(IES, tipoFies, "DRT");
                            }
                            break;
                        case "EXPORTAR SUSPENSÃO":
                            if (tipoFies.ToUpper() == "FIES LEGADO")
                            {
                                presenter.ExecutarExportarRelatoriosLegado(IES, tipoFies, campus, semestre, "SUSPENSÃO");
                            }
                            else
                            {
                                presenter.ExportarRelatorioFiesNovo(IES, tipoFies, "SUSPENSÃO");
                            }
                            break;
                        case "EXTRAIR INFORMAÇÕES DRM":
                            if (tipoFies.ToUpper() == "FIES LEGADO")
                            {
                                presenter.ExtrairInformacoesDRMLegado(IES, tipoFies, campus, semestre);
                            }
                            else
                            {
                                presenter.ExtrairInformacoesDRMFiesNovo(IES, tipoFies, semestre);
                            }
                            break;
                        case "EXPORTAR DRI":
                            presenter.ExecutarExportarDRILegado(IES, tipoFies, campus, situacao);
                            break;
                        case "EXPORTAR REPASSE":
                            presenter.ExportarRepasseFiesNovo(IES, mes, ano);
                            break;
                        case "EXPORTAR EXTRATO MENSAL DE REPASSE":
                            presenter.ExportarExtratoMensalDeRepasseLegado(IES, tipoFies, campus, ano, mes);
                            break;
                        case "ABRIR SITE":
                            presenter.ExecutarAbrirSite(IES, campus, tipoFies);
                            break;
                        case "BUSCAR STATUS ADITAMENTO":
                            presenter.ExecutarBuscarStatusAditamentoNovo(IES, tipoFies, semestre);
                            break;
                        case "STATUS ALUNO":
                            presenter.ExecutarStatusAluno(IES, tipoFies, semestre);
                            break;
                        case "EXPORTAR INADIMPLÊNCIA":
                            presenter.ExportarInadimplencia(IES, mes, ano, todosMeses);
                            break;
                        case "EXPORTAR COPARTICIPAÇÃO":
                            presenter.ExportarCoparticipacaoFiesNovo(IES, dataInicio, dataFim);
                            break;
                        case "LANÇAMENTO FIES SIGA":
                            presenter.ExecutarLancamentoFiesSiga(semestre, fiesSiga);
                            break;
                        case "GERAÇÃO PARCELAS FIES SIGA":
                            presenter.GeracaoParcelasFies(semestre);
                            break;
                        case "HISTÓRICO DE REPARCELAMENTO DA COPARTICIPAÇÃO":
                            presenter.ExecutarHistoricoReparcelamentoCoparticipacao(IES, tipoFies);
                            break;
                        case "VALIDAR REPARCELAMENTO":
                            presenter.ValidarReparcelamento(IES, tipoFies);
                            break;
                        default:
                            MessageBox.Show("Favor criar o CASE antes de rodar!!!!!");
                            break;
                    }
                }
                catch (ElementClickInterceptedException ex)
                {
                    excessaoCausada = "Ocorreu algum problema ao clicar em um elemento da página. Tente executar o programa novamente.";
                    backgroundWorker.ReportProgress(404);
                }
                catch (Exception ex)
                {
                    excessaoCausada = ex.Message;
                    backgroundWorker.ReportProgress(404);
                }
                finally
                {
                    presenter.EncerrarDriver();
                }
            };

            backgroundWorker.RunWorkerAsync();
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            formPrincipal.btnSelectPath.PerformClick();
            int countAlunos = Dados.Count<TOAluno>();
            setText(countAlunos);
        }

        private void txtCPF_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtCPF.TextLength >= 11 && e.KeyChar != (char)System.Windows.Forms.Keys.Back && !char.IsControl(e.KeyChar))
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

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 404)
            {
                lblStatus.Text = "Ocorreu um erro não esperado no site. Por favor tente novamente em alguns instantes.";
                btnImportar.Enabled = true;
                btnIniciar.Enabled = true;
                btnHelp.Enabled = true;
                formPrincipal.flpModosDeExecucao.Enabled = true;
                formPrincipal.panelSubMenu.Enabled = true;
                formPrincipal.btnPlanilha.Enabled = true;
                formPrincipal.btnClose.Enabled = true;

                circularProgressBar1.Style = ProgressBarStyle.Continuous;
                circularProgressBar1.Visible = false;
                return;
            }
            if (execucao.Contains("EXPORTAR"))
            {
                circularProgressBar1.Value = 25;
                circularProgressBar1.Style = ProgressBarStyle.Marquee;
            }
            else
            {
                circularProgressBar1.Value = e.ProgressPercentage;
            }

            if (e.ProgressPercentage == 0)
            {
                formPrincipal.flpModosDeExecucao.Enabled = false;
                btnImportar.Enabled = false;
                btnIniciar.Enabled = false;
                circularProgressBar1.Visible = true;
                btnHelp.Enabled = false;
                formPrincipal.panelSubMenu.Enabled = false;
                formPrincipal.btnPlanilha.Enabled = false;
                formPrincipal.btnClose.Enabled = false;
                circularProgressBar1.Text = "Abrindo site...";
                lblStatus.Text = "Processando " + execucao;
            }
            else
            {
                circularProgressBar1.Text = e.ProgressPercentage.ToString() + "%";
            }
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (excessaoCausada == string.Empty)
            {
                MessageBox.Show("Processamentos concluídos com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information) ;
                lblStatus.Text = "Processamentos concluídos com sucesso!";
            }
            else
            {
                MessageBox.Show(excessaoCausada, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = excessaoCausada;
            }
            btnImportar.Enabled = true;
            btnIniciar.Enabled = true;
            btnHelp.Enabled = true;
            formPrincipal.flpModosDeExecucao.Enabled = true;
            formPrincipal.panelSubMenu.Enabled = true;
            formPrincipal.btnPlanilha.Enabled = true;
            formPrincipal.btnClose.Enabled = true;

            circularProgressBar1.Style = ProgressBarStyle.Continuous;
            circularProgressBar1.Visible = false;
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            if (wbHelp.Visible == true)
            {
                wbHelp.Visible = false;
            }
            else
            {
                wbHelp.Visible = true;
                string execucao = lblExecucao.Text;
                string diretorioAtual = Directory.GetCurrentDirectory();
                string caminhoArquivo = diretorioAtual + "\\help\\" + execucao.ToLower() + ".html";
                wbHelp.Url = new Uri(string.Format("file:///{0}", caminhoArquivo));
            }
        }
    }
}
