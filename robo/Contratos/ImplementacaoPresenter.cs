using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using robo.Banco_de_Dados;
using robo.Interface;
using robo.TO;
using robo.Utils;
using System;
using System.Collections.Generic;
using robo.Modos_de_Execucao.FIES_Legado;
using robo.Modos_de_Execucao.FIES_Novo;
using robo.Modos_de_Execucao.SIGA;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using robo.Excessoes;

namespace robo.Contratos
{
    public class ImplementacaoPresenter : IContratos.IPresenter
    {
        private FormDefault forms;
        private List<TOAluno> listaAlunos;
        private List<TOLogin> listaLogins;
        private const string FIES_NOVO = "NOVO";
        private const string FIES_LEGADO = "LEGADO";
        private int contador;
        private float progresso = 0;
        public string CPFCae { get; set; }
        private IWebDriver Driver;
        public ImplementacaoPresenter(FormDefault forms)
        {
            this.forms = forms;
        }

        private void BuscarLoginsEAlunos(string faculdade, string tipoFies, string campus, ref List<TOAluno> alunos, ref List<TOLogin> logins, bool admin, bool exportar)
        {
            if (exportar == false)
            {
                if (Program.login.Permissao == "CAE")
                {
                    alunos = new List<TOAluno>() { new TOAluno() { Cpf = CPFCae, Tipo = tipoFies } };
                    contador = 1;
                }
                else
                {
                    alunos = SelecionarAlunosPorPlataforma(tipoFies);
                    contador = alunos.Count;
                }
            }
            if (admin == true)
            {
                logins = Dados.SelectLoginPorIESePlataforma(faculdade, tipoFies, campus, admin: true);
            }
            else
            {
                logins = Dados.SelectLoginPorIESePlataforma(faculdade, tipoFies, campus, admin: false);
            }
            progresso = 0;
        }

        //Processamentos
        public void ExecutarAditamentoLegado(string semestreAtual, string faculdade, string tipoFies, string campus)
        {
            string numSemestre = BuscarNunSemestre(semestreAtual);
            BuscarLoginsEAlunos(faculdade, FIES_LEGADO, campus, ref listaAlunos, ref listaLogins, admin: false, exportar: false);
            AditamentoLegado aditamento = new AditamentoLegado(numSemestre);
            RodarModoDeExecucaoComAlunosFIESLegado(listaLogins, aditamento);
        }
        public void ExecutarAditamentoNovo(string faculdade, string tipoFies, string semestreAtual)
        {
            BuscarLoginsEAlunos(faculdade, FIES_NOVO, "", ref listaAlunos, ref listaLogins, admin: false, exportar: false);
            AditamentoNovo aditamento = new AditamentoNovo(listaLogins[0].Faculdade, semestreAtual);
            RodarModoDeExecucaoComAlunosFIESNovo(listaAlunos, aditamento);
        }
        public void ExecutarDRI(string faculdade, string tipoFies, string campus, string situacaoDRI, bool baixarDRI)
        {
            BuscarLoginsEAlunos(faculdade, FIES_LEGADO, campus, ref listaAlunos, ref listaLogins, admin: false, exportar: false);

            Driver = Util.StartBrowser("http://sisfies.mec.gov.br/");
            UtilFiesLegado fiesLegadoUtil = new UtilFiesLegado();
            fiesLegadoUtil.SetDriver(Driver);
            foreach (TOLogin login in listaLogins)
            {
                DRI dri = new DRI(baixarDRI, situacaoDRI, login.Campus);
                dri.SetWebDriver(Driver);
                fiesLegadoUtil.RealizarLoginSucesso(login);
                fiesLegadoUtil.SelecionarMenuDRI();
                ForeachDeAlunos(listaAlunos, dri);

                fiesLegadoUtil.FazerLogout();
            }


        }
        public void ExecutarBaixarDocumentoLegado(string faculdade, string tipoFies, string campus, string semestre, string tipoDocumento)
        {
            BuscarLoginsEAlunos(faculdade, FIES_LEGADO, campus, ref listaAlunos, ref listaLogins, admin: false, exportar: false);

            semestre = semestre.Replace("1/", "1º/");
            semestre = semestre.Replace("2/", "2º/");
            BaixarDocumentos baixarDocumentos = new BaixarDocumentos(semestre, tipoDocumento);

            RodarModoDeExecucaoComAlunosFIESLegado(listaLogins, baixarDocumentos);


        }
        public void ExecutarExportarRelatoriosLegado(string faculdade, string tipoFies, string campus, string semestre, string tipoRelatorio)
        {
            BuscarLoginsEAlunos(faculdade, FIES_LEGADO, campus, ref listaAlunos, ref listaLogins, admin: false, exportar: true);
            ExportarRelatorios exportarRelatorios = new ExportarRelatorios();
            semestre = semestre.Replace("1/", "1º/");
            semestre = semestre.Replace("2/", "2º/");

            Driver = Util.StartBrowser("http://sisfies.mec.gov.br/", downloadFldr: true);
            UtilFiesLegado fiesLegadoUtil = new UtilFiesLegado();
            fiesLegadoUtil.SetDriver(Driver);
            exportarRelatorios.SetDriver(Driver);
            foreach (TOLogin login in listaLogins)
            {
                fiesLegadoUtil.RealizarLoginSucesso(login);
                fiesLegadoUtil.SelecionarMenuBaixarDocumentos();

                exportarRelatorios.ExportarDocumentosFiesLegado(semestre, tipoRelatorio, login.Campus);

                fiesLegadoUtil.FazerLogout();
            }


        }
        public void ExtrairInformacoesDRMLegado(string faculdade, string tipoFies, string campus, string semestre)
        {
            BuscarLoginsEAlunos(faculdade, FIES_LEGADO, campus, ref listaAlunos, ref listaLogins, admin: false, exportar: false);
            semestre = semestre.Replace("1/", "1º/");
            semestre = semestre.Replace("2/", "2º/");
            ExtrairInformacoesDRM extrairInformacoesDRM = new ExtrairInformacoesDRM(semestre);
            RodarModoDeExecucaoComAlunosFIESLegado(listaLogins, extrairInformacoesDRM);
        }
        public void ExecutarExportarDRILegado(string faculdade, string tipoFies, string campus, string situacaoDRI)
        {
            BuscarLoginsEAlunos(faculdade, FIES_LEGADO, campus, ref listaAlunos, ref listaLogins, admin: false, exportar: true);

            Driver = Util.StartBrowser("http://sisfies.mec.gov.br/", downloadFldr: true);
            UtilFiesLegado fiesLegadoUtil = new UtilFiesLegado();
            fiesLegadoUtil.SetDriver(Driver);
            ExportarDRI exportarDRI = new ExportarDRI();
            exportarDRI.SetDriver(Driver);
            foreach (TOLogin login in listaLogins)
            {
                fiesLegadoUtil.RealizarLoginSucesso(login);
                fiesLegadoUtil.SelecionarMenuDRI();

                exportarDRI.ExportarDRILegado(login.Campus, situacaoDRI);

                fiesLegadoUtil.FazerLogout();
            }
        }
        public void ExportarExtratoMensalDeRepasseLegado(string faculdade, string tipoFies, string campus, string ano, string mes)
        {
            BuscarLoginsEAlunos(faculdade, FIES_LEGADO, campus, ref listaAlunos, ref listaLogins, admin: false, exportar: true);

            Driver = Util.StartBrowser("http://sisfies.mec.gov.br/", downloadFldr: true);
            UtilFiesLegado fiesLegadoUtil = new UtilFiesLegado();
            fiesLegadoUtil.SetDriver(Driver);
            ExportarExtratoMensalDeRepasse exportarExtratoMensalDeRepasse = new ExportarExtratoMensalDeRepasse();
            exportarExtratoMensalDeRepasse.SetDriver(Driver);
            foreach (TOLogin login in listaLogins)
            {
                fiesLegadoUtil.RealizarLoginSucesso(login);
                fiesLegadoUtil.SelecionarMenuExtratoMensalDeRepasse();

                exportarExtratoMensalDeRepasse.ExtratoMensalDeRepasseLegado(login.Campus, ano, mes);

                fiesLegadoUtil.FazerLogout();
            }


        }
        public void ExecutarBaixarDRMFiesNovo(string faculdade, string tipoFies, string semestre)
        {
            BuscarLoginsEAlunos(faculdade, FIES_NOVO, campus: "", ref listaAlunos, ref listaLogins, admin: false, exportar: false);
            BaixarDRM drm = new BaixarDRM(semestre);
            RodarModoDeExecucaoComAlunosFIESNovo(listaAlunos, drm);
        }
        public void ExtrairInformacoesDRMFiesNovo(string faculdade, string tipoFies, string semestre)
        {
            BuscarLoginsEAlunos(faculdade, FIES_NOVO, campus: "", ref listaAlunos, ref listaLogins, admin: false, exportar: false);
            ExtrairInformacoesDRMFiesNovo extrairInformacoesDRM = new ExtrairInformacoesDRMFiesNovo(semestre);
            RodarModoDeExecucaoComAlunosFIESNovo(listaAlunos, extrairInformacoesDRM);
        }
        public void ExecutarBuscarStatusAditamentoNovo(string faculdade, string tipoFies, string semestre)
        {
            BuscarLoginsEAlunos(faculdade, FIES_NOVO, "", ref listaAlunos, ref listaLogins, admin: false, exportar: false);
            BuscarStatusAditamento statusAditamento = new BuscarStatusAditamento(semestre);
            RodarModoDeExecucaoComAlunosFIESNovo(listaAlunos, statusAditamento);
        }
        public void ExecutarStatusAluno(string faculdade, string tipoFies, string semestre)
        {
            BuscarLoginsEAlunos(faculdade, FIES_NOVO, "", ref listaAlunos, ref listaLogins, admin: false, exportar: false);
            StatusAluno statusAluno = new StatusAluno(semestre);
            RodarModoDeExecucaoComAlunosFIESNovo(listaAlunos, statusAluno);
        }
        public void ExportarRelatorioFiesNovo(string faculdade, string tipoFies, string tipoRelatorio)
        {
            BuscarLoginsEAlunos(faculdade, FIES_NOVO, "", ref listaAlunos, ref listaLogins, admin: false, exportar: true);
            UtilFiesNovo utilFiesNovo = new UtilFiesNovo();
            ExportarRelatorio exportarRelatorio = new ExportarRelatorio();
            Driver = Util.StartBrowser("http://sifesweb.caixa.gov.br");
            exportarRelatorio.SetDriver(Driver);
            utilFiesNovo.SetDriver(Driver);
            utilFiesNovo.FazerLogin(listaLogins[0]);
            utilFiesNovo.WaitForLoading();

            switch (tipoRelatorio)
            {
                case "DRM":
                    utilFiesNovo.ClicarMenuAditamento();
                    break;
                case "DRT":
                    utilFiesNovo.ClicarMenuTransferencia();
                    break;
                case "DRD":
                    utilFiesNovo.ClicarMenuDilatacao();
                    break;
                case "SUSPENSÃO":
                    utilFiesNovo.ClicarMenuSuspensao();
                    break;
                default:
                    throw new Exception("Como?");
            }
            utilFiesNovo.WaitForLoading();
            exportarRelatorio.ExportarRelatorioFiesNovo(tipoRelatorio);
        }
        public void ExportarInadimplencia(string faculdade, string mes, string ano, bool todosMeses)
        {
            BuscarLoginsEAlunos(faculdade, FIES_NOVO, "", ref listaAlunos, ref listaLogins, admin: true, exportar: true);
            UtilFiesNovo utilFiesNovo = new UtilFiesNovo();
            ExportarInadimplencia exportarInadimplencia = new ExportarInadimplencia();
            Driver = Util.StartBrowser("http://sifesweb.caixa.gov.br", firefox: false, downloadFldr: true);
            exportarInadimplencia.SetDriver(Driver);
            utilFiesNovo.SetDriver(Driver);
            utilFiesNovo.FazerLogin(listaLogins[0]);
            utilFiesNovo.WaitForLoading();
            utilFiesNovo.ClicarMenuInadimplencia();
            utilFiesNovo.WaitForLoading();

            if (todosMeses == true)
            {
                exportarInadimplencia.Inadimplencia(Driver);
            }
            else
            {
                exportarInadimplencia.Inadimplencia(mes, ano);
            }
        }
        public void ExportarRepasseFiesNovo(string faculdade, string mes, string ano)
        {
            BuscarLoginsEAlunos(faculdade, FIES_NOVO, "", ref listaAlunos, ref listaLogins, admin: true, exportar: true);
            UtilFiesNovo utilFiesNovo = new UtilFiesNovo();
            ExportarRepasse exportarRepasse = new ExportarRepasse();
            Driver = Util.StartBrowser("http://sifesweb.caixa.gov.br", firefox: false, downloadFldr: true);
            exportarRepasse.SetDriver(Driver);
            utilFiesNovo.SetDriver(Driver);
            utilFiesNovo.FazerLogin(listaLogins[0]);
            utilFiesNovo.WaitForLoading();
            utilFiesNovo.ClicarMenuRepasse();
            utilFiesNovo.WaitForLoading();

            exportarRepasse.ExportarRepasseFiesNovo(ano, mes);



        }
        public void ExportarCoparticipacaoFiesNovo(string faculdade, string dataInicial, string dataFinal)
        {
            BuscarLoginsEAlunos(faculdade, FIES_NOVO, "", ref listaAlunos, ref listaLogins, admin: true, exportar: true);
            UtilFiesNovo utilFiesNovo = new UtilFiesNovo();
            Driver = Util.StartBrowser("http://sifesweb.caixa.gov.br", firefox: false, downloadFldr: true);
            utilFiesNovo.SetDriver(Driver);
            utilFiesNovo.FazerLogin(listaLogins[0]);
            utilFiesNovo.WaitForLoading();
            utilFiesNovo.ClicarMenuCoparticipacao();
            utilFiesNovo.WaitForLoading();
            ExportarCoparticipacao exportarCoparticipacao = new ExportarCoparticipacao();
            exportarCoparticipacao.SetDriver(Driver);
            exportarCoparticipacao.ExportarRelatorioCoparticipacao(faculdade, dataInicial, dataFinal);
        }
        public void ExecutarAbrirSite(string faculdade, string campus, string plataforma)
        {
            BuscarLoginsEAlunos(faculdade, plataforma, campus, ref listaAlunos, ref listaLogins, admin: false, exportar: true);

            if (plataforma.ToUpper() == "FIES LEGADO")
            {
                Driver = Util.StartBrowser("http://sisfies.mec.gov.br/");
                UtilFiesLegado fiesLegadoUtil = new UtilFiesLegado();
                fiesLegadoUtil.SetDriver(Driver);
                fiesLegadoUtil.RealizarLoginSucesso(listaLogins[0]);
            }
            else
            {
                UtilFiesNovo fiesNovoUtil = new UtilFiesNovo();
                fiesNovoUtil.SetDriver(Driver);
                Driver = Util.StartBrowser("http://sifesweb.caixa.gov.br");
                fiesNovoUtil.FazerLogin(listaLogins[0]);
            }
        }
        public void ExecutarLancamentoFiesSiga(string semestre, string tipoFies)
        {
            listaAlunos = Dados.SelectWhere<TOAluno>(x => x.Conclusao == "Não Feito");
            listaLogins = Dados.SelectWhere<TOLogin>(x => x.Plataforma == "SIGA");
            progresso = 0;
            contador = listaAlunos.Count;

            UtilSiga utilsiga = new UtilSiga();
            IWebDriver Driver = Util.CriarBrowserVazio(firefox: true);
            ((IJavaScriptExecutor)Driver).ExecuteScript("alert('https://siga.uniritter.edu.br/financeiro/fichaFinanceira.php')");
            DialogResult resultado = MessageBox.Show("Abra o site na mensagem de alerta do navegador e clique no captcha.\nApós isso, volte para esta mensagem e clique em 'Ok'.", "Clique no captcha!", MessageBoxButtons.OKCancel);
            if (resultado == DialogResult.Cancel)
            {
                return;
            }
            utilsiga.SetDriver(Driver);
            utilsiga.FazerLogin(listaLogins[0]);
            // Não caiu
            if (Driver.PageSource.Contains("Você precisa realizar a validação \"Não sou um robô\".Tente novamente marcando esta opção!"))
            {
                return;
            }
            while (Driver.PageSource.ToUpper().Contains("FILTRO POR DADOS DE ALUNO") == false)
            {
                System.Threading.Thread.Sleep(250);
            }
            LancamentoFiesSiga lancamentoFiesSiga = new LancamentoFiesSiga(semestre, tipoFies);
            lancamentoFiesSiga.SetWebDriver(Driver);
            ForeachDeAlunos(listaAlunos, lancamentoFiesSiga);
        }
        public void GeracaoParcelasFies(string semestre)
        {
            listaAlunos = Dados.SelectWhere<TOAluno>(x => x.Conclusao == "Não Feito");
            listaLogins = Dados.SelectWhere<TOLogin>(x => x.Plataforma == "SIGA");

            progresso = 0;
            contador = listaAlunos.Count;
            UtilSiga utilsiga = new UtilSiga();
            IWebDriver Driver = Util.CriarBrowserVazio(firefox: true);
            utilsiga.SetDriver(Driver);
            ((IJavaScriptExecutor)Driver).ExecuteScript("alert('https://siga.uniritter.edu.br/financeiro/geracaoIndividualParcela.php')");

            DialogResult resultado = MessageBox.Show("Abra o site na mensagem de alerta do navegador e clique no captcha.\nApós isso, volte para esta mensagem e clique em 'Ok'.", "Clique no captcha!", MessageBoxButtons.OKCancel);
            if (resultado == DialogResult.Cancel)
            {
                return;
            }
            utilsiga.FazerLogin(listaLogins[0]);

            if (Driver.PageSource.Contains("Você precisa realizar a validação \"Não sou um robô\"."))
            {
                return;
            }

            GeracaoParcelasFies GeracaoParcelasFies = new GeracaoParcelasFies(semestre);
            GeracaoParcelasFies.SetWebDriver(Driver);
            GeracaoParcelasFies.ExecutarCookieGuiche(Driver);
            ForeachDeAlunos(listaAlunos, GeracaoParcelasFies);
        }
        public void ExecutarHistoricoReparcelamentoCoparticipacao(string faculdade, string tipoFIES)
        {
            BuscarLoginsEAlunos(faculdade, tipoFIES, "", ref listaAlunos, ref listaLogins, admin: false, exportar: false);
            HistoricoReparcelamentoCoparticipacao historico = new HistoricoReparcelamentoCoparticipacao();
            RodarModoDeExecucaoComAlunosFIESNovo(listaAlunos, historico);
        }
        public void ValidarReparcelamento(string faculdade, string tipoFIES)
        {
            ValidarReparcelamento validar = new ValidarReparcelamento();
            BuscarLoginsEAlunos(faculdade, tipoFIES, "", ref listaAlunos, ref listaLogins, admin: true, exportar: true);
            UtilFiesNovo utilFiesNovo = new UtilFiesNovo();
            Driver = Util.StartBrowser("http://sifesweb.caixa.gov.br");
            utilFiesNovo.SetDriver(Driver);
            utilFiesNovo.FazerLogin(listaLogins[0]);
            utilFiesNovo.WaitForLoading();
            utilFiesNovo.ClicarMenuValidarReparcelamento();
            validar.SetDriver(Driver);
            validar.ExecutarValidarReparcelamento();
        }

        private void ForeachDeAlunos(List<TOAluno> alunos, IModosDeExecucao.IModoComAlunos modosDeExecucao)
        {
            foreach (TOAluno aluno in alunos)
            {
                try
                {
                    modosDeExecucao.ExecucaoComListaDeAlunos(aluno);
                }
                catch (PararExecucaoException)
                {
                    continue;
                }
                finally
                {
                    UpdateProgresso(ref progresso, contador);
                }
            }
        }
        private void RodarModoDeExecucaoComAlunosFIESLegado(List<TOLogin> logins, IModosDeExecucao.IModoComAlunos modosDeExecucao)
        {
            Driver = Util.StartBrowser("http://sisfies.mec.gov.br/");
            UtilFiesLegado fiesLegadoutil = new UtilFiesLegado();
            fiesLegadoutil.SetDriver(Driver);
            modosDeExecucao.SetWebDriver(Driver);
            foreach (TOLogin login in logins)
            {
                fiesLegadoutil.RealizarLoginSucesso(login);
                modosDeExecucao.SelecionarMenu();
                ForeachDeAlunos(listaAlunos, modosDeExecucao);
                fiesLegadoutil.FazerLogout();
            }
        }
        private void RodarModoDeExecucaoComAlunosFIESNovo(List<TOAluno> alunos, IModosDeExecucao.IModoComAlunos modoDeExecucao)
        {
            UtilFiesNovo utilFiesNovo = new UtilFiesNovo();
            Driver = Util.StartBrowser("http://sifesweb.caixa.gov.br");
            utilFiesNovo.SetDriver(Driver);
            modoDeExecucao.SetWebDriver(Driver);
            utilFiesNovo.FazerLogin(listaLogins[0]);
            utilFiesNovo.WaitForLoading();
            modoDeExecucao.SelecionarMenu();
            ForeachDeAlunos(alunos, modoDeExecucao);
        }
        private void UpdateProgresso(ref float progresso, int listCount)
        {
            if (progresso >= 100)
            {
                progresso = 0;
            }
            progresso += 100.0f / listCount;
            forms.backgroundWorker.ReportProgress(Convert.ToInt32(progresso));
        }
        public string BuscarNunSemestre(string semestreAno)
        {
            List<TOSemestre> semestre = Dados.SelectAll<TOSemestre>();
            foreach (var item in semestre)
            {
                if (semestreAno == item.Semestre)
                {
                    return item.numSemestre;
                }
            }
            return null;
        }
        private List<TOAluno> SelecionarAlunosPorPlataforma(string plataforma)
        {
            List<TOAluno> alunosFies = new List<TOAluno>();
            alunosFies = Dados.SelectAlunoWhere(plataforma.ToUpper());

            if (alunosFies.Count == 0)
            {
                throw new Exception(string.Format("Nenhum aluno encontrado na plataforma escolhida ({0}). Cheque se o banco de dados contém alunos da plataforma que deseja realizar os aditamentos.", plataforma));
            }

            foreach (TOAluno aluno in alunosFies)
            {
                if (aluno.AproveitamentoAtual == null)
                {
                    continue;
                }
                if (aluno.AproveitamentoAtual.Contains("TRANCADO") == true)
                {
                    aluno.Conclusao = "Trancado";
                    Dados.UpdateDocumento<TOAluno>(aluno);
                }

            }

            return alunosFies;
        }
        public List<string> PreencherListaExecucao()
        {
            List<TOMenus> menus = Dados.SelectAll<TOMenus>();
            List<string> nomeMenu = new List<string>();
            foreach (var item in menus)
            {
                nomeMenu.Add(item.Item);
            }
            return nomeMenu;
        }
        public List<string> PreencherListaExecucaoPorPlataforma(string plataforma)
        {
            List<TOMenus> menus = Dados.SelectMenuWhereLite(plataforma);
            List<string> nomeMenu = new List<string>();
            foreach (var item in menus)
            {
                nomeMenu.Add(item.Item);
            }
            return nomeMenu;
        }
        public List<string> PreencherListaSemestre()
        {
            List<TOSemestre> semestre = Dados.SelectAll<TOSemestre>();
            List<string> nomeSemestre = new List<string>();
            foreach (var item in semestre)
            {
                nomeSemestre.Add(item.Semestre);
            }
            return nomeSemestre;
        }
        public List<string> PreencherListaAno()
        {
            List<TOSemestre> semestre = Dados.SelectAll<TOSemestre>();
            List<string> nomeAno = new List<string>();
            foreach (var item in semestre)
            {
                if (item.Semestre.Contains("1/"))
                {
                    string ano = item.Semestre.Replace("1/", "");
                    nomeAno.Add(ano);
                }
            }
            return nomeAno;
        }
        public void EncerrarDriver()
        {
            if (Driver != null)
            {
                Driver.Close();
                Driver.Dispose();
            }
        }
    }
}
