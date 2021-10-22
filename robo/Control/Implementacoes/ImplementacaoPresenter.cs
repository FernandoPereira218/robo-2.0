﻿using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using robo.Control.Aditamento;
using robo.Control.Legado;
using robo.Control.Relatorios;
using robo.Control.Relatorios.FIES_Legado;
using robo.Control.Relatorios.FIES_Novo;
using robo.Control.Relatorios.SIGA;
using robo.pgm;
using Robo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace robo.Control.Implementacoes
{
    public class ImplementacaoPresenter : IContratos.IPresenter
    {
        private IContratos.IMainForms forms;
        private List<TOAluno> listaAlunos;
        private List<TOLogin> listaLogins;
        private const string FIES_NOVO = "NOVO";
        private const string FIES_LEGADO = "LEGADO";
        public ImplementacaoPresenter(IContratos.IMainForms forms)
        {
            SetForm(forms);
        }

        private void BuscarLoginsEAlunos(string faculdade, string tipoFies, string campus, ref List<TOAluno> alunos, ref List<TOLogin> logins, bool admin, bool exportar)
        {
            if (exportar == false)
            {
                alunos = SelecionarAlunosPorPlataforma(tipoFies);
            }
            if (admin == true)
            {
                logins = Dados.SelectLoginPorIESePlataforma(faculdade, tipoFies, campus, true);
            }
            else
            {
                logins = Dados.SelectLoginPorIESePlataforma(faculdade, tipoFies, campus, false);
            }
        }

        //Processamentos
        public void ExecutarAditamentoLegado(string semestreAtual, string faculdade, string tipoFies, string campus)
        {
            string numSemestre = BuscarNunSemestre(semestreAtual);
            BuscarLoginsEAlunos(faculdade, FIES_LEGADO, campus, ref listaAlunos, ref listaLogins, admin: false, exportar: false);
            UtilFiesLegado fiesLegadoutil = new UtilFiesLegado();
            AditamentoLegado aditamento = new AditamentoLegado();

            IWebDriver Driver = Util.StartBrowser("http://sisfies.mec.gov.br/");
            foreach (TOLogin login in listaLogins)
            {
                fiesLegadoutil.RealizarLoginSucesso(login, Driver);
                fiesLegadoutil.SelecionarPerfilPresidencia(Driver);
                foreach (TOAluno aluno in listaAlunos)
                {
                    if (aluno.Campus.ToUpper() == login.Campus.ToUpper())
                    {
                        aditamento.AditamentoFiesLegado(Driver, login, aluno, numSemestre);
                    }
                }
                fiesLegadoutil.FazerLogout(Driver);
            }
            Driver.Close();
            Driver.Dispose();
        }
        public void ExecutarAditamentoNovo(string faculdade, string tipoFies, string semestreAtual)
        {
            BuscarLoginsEAlunos(faculdade, FIES_NOVO, "", ref listaAlunos, ref listaLogins, admin: false, exportar: false);

            UtilFiesNovo utilFiesNovo = new UtilFiesNovo();
            IWebDriver Driver = Util.StartBrowser("http://sifesweb.caixa.gov.br");
            AditamentoNovo aditamento = new AditamentoNovo();
            aditamento.SetDriver(Driver);

            utilFiesNovo.FazerLogin(Driver, listaLogins[0]);
            utilFiesNovo.WaitForLoading(Driver);
            utilFiesNovo.ClicarMenuAditamento(Driver);

            foreach (TOAluno aluno in listaAlunos)
            {
                aditamento.AditamentoFiesNovo(aluno, listaLogins[0].Faculdade, semestreAtual);
            }
            Driver.Close();
            Driver.Dispose();
        }
        public void ExecutarDRI(string faculdade, string tipoFies, string campus, string situacaoDRI, bool baixarDRI)
        {
            BuscarLoginsEAlunos(faculdade, FIES_LEGADO, campus, ref listaAlunos, ref listaLogins, admin: false, exportar: false);
            UtilFiesLegado fiesLegadoUtil = new UtilFiesLegado();

            IWebDriver Driver = Util.StartBrowser("http://sisfies.mec.gov.br/");
            DRI dri = new DRI();
            dri.SetDriver(Driver);
            foreach (TOLogin login in listaLogins)
            {
                fiesLegadoUtil.RealizarLoginSucesso(login, Driver);
                fiesLegadoUtil.SelecionarPerfilPresidencia(Driver);
                fiesLegadoUtil.SelecionarMenuDRI(Driver);
                foreach (TOAluno aluno in listaAlunos)
                {
                    dri.DRIFiesLegado(Driver, aluno, login, baixarDRI, situacaoDRI);
                }

                fiesLegadoUtil.FazerLogout(Driver);
            }
            Driver.Close();
            Driver.Dispose();
        }
        public void ExecutarBaixarDocumentoLegado(string faculdade, string tipoFies, string campus, string semestre, string tipoDocumento)
        {
            BuscarLoginsEAlunos(faculdade, FIES_LEGADO, campus, ref listaAlunos, ref listaLogins, admin: false, exportar: false);
            UtilFiesLegado fiesLegadoUtil = new UtilFiesLegado();

            semestre = semestre.Replace("1/", "1º/");
            semestre = semestre.Replace("2/", "2º/");
            BaixarDocumentos baixarDocumentos = new BaixarDocumentos();
            IWebDriver Driver = Util.StartBrowser("http://sisfies.mec.gov.br/");

            foreach (TOLogin login in listaLogins)
            {
                fiesLegadoUtil.RealizarLoginSucesso(login, Driver);
                fiesLegadoUtil.SelecionarPerfilPresidencia(Driver);
                fiesLegadoUtil.SelecionarMenuBaixarDocumentos(Driver);
                foreach (TOAluno aluno in listaAlunos)
                {
                    baixarDocumentos.BaixarDocumentoFiesLegado(Driver, aluno, semestre, tipoDocumento);
                }

                fiesLegadoUtil.FazerLogout(Driver);
            }
            Driver.Close();
            Driver.Dispose();
        }
        public void ExecutarExportarRelatoriosLegado(string faculdade, string tipoFies, string campus, string semestre, string tipoRelatorio)
        {
            BuscarLoginsEAlunos(faculdade, FIES_LEGADO, campus, ref listaAlunos, ref listaLogins, admin: false, exportar: true);
            UtilFiesLegado fiesLegadoUtil = new UtilFiesLegado();
            ExportarRelatorios exportarRelatorios = new ExportarRelatorios();
            semestre = semestre.Replace("1/", "1º/");
            semestre = semestre.Replace("2/", "2º/");

            IWebDriver Driver = Util.StartBrowser("http://sisfies.mec.gov.br/", downloadFldr: true);

            foreach (TOLogin login in listaLogins)
            {
                fiesLegadoUtil.RealizarLoginSucesso(login, Driver);
                fiesLegadoUtil.SelecionarPerfilPresidencia(Driver);
                fiesLegadoUtil.SelecionarMenuBaixarDocumentos(Driver);

                exportarRelatorios.ExportarDocumentosFiesLegado(Driver, semestre, tipoRelatorio, login.Campus);

                fiesLegadoUtil.FazerLogout(Driver);
            }
            Driver.Close();
            Driver.Dispose();
        }
        public void ExtrairInformacoesDRMLegado(string faculdade, string tipoFies, string campus, string semestre)
        {
            BuscarLoginsEAlunos(faculdade, FIES_LEGADO, campus, ref listaAlunos, ref listaLogins, admin: false, exportar: false);
            UtilFiesLegado fiesLegadoUtil = new UtilFiesLegado();
            ExtrairInformacoesDRM extrairInformacoesDRM = new ExtrairInformacoesDRM();
            semestre = semestre.Replace("1/", "1º/");
            semestre = semestre.Replace("2/", "2º/");

            IWebDriver Driver = Util.StartBrowser("http://sisfies.mec.gov.br/");

            foreach (TOLogin login in listaLogins)
            {
                fiesLegadoUtil.RealizarLoginSucesso(login, Driver);
                fiesLegadoUtil.SelecionarPerfilPresidencia(Driver);
                fiesLegadoUtil.SelecionarMenuBaixarDocumentos(Driver);
                foreach (TOAluno aluno in listaAlunos)
                {
                    extrairInformacoesDRM.ExtrairDRM(Driver, aluno, login.Campus, semestre);
                }

                fiesLegadoUtil.FazerLogout(Driver);
            }
            Driver.Close();
            Driver.Dispose();
        }
        public void ExecutarExportarDRILegado(string faculdade, string tipoFies, string campus, string situacaoDRI)
        {
            BuscarLoginsEAlunos(faculdade, FIES_LEGADO, campus, ref listaAlunos, ref listaLogins, admin: false, exportar: true);
            UtilFiesLegado fiesLegadoUtil = new UtilFiesLegado();

            IWebDriver Driver = Util.StartBrowser("http://sisfies.mec.gov.br/", downloadFldr: true);
            ExportarDRI exportarDRI = new ExportarDRI();
            foreach (TOLogin login in listaLogins)
            {
                fiesLegadoUtil.RealizarLoginSucesso(login, Driver);
                fiesLegadoUtil.SelecionarPerfilPresidencia(Driver);
                fiesLegadoUtil.SelecionarMenuDRI(Driver);

                exportarDRI.ExportarDRILegado(Driver, login.Campus, situacaoDRI);

                fiesLegadoUtil.FazerLogout(Driver);
            }
            Driver.Close();
            Driver.Dispose();
        }
        public void ExportarExtratoMensalDeRepasseLegado(string faculdade, string tipoFies, string campus, string ano, string mes)
        {
            BuscarLoginsEAlunos(faculdade, FIES_LEGADO, campus, ref listaAlunos, ref listaLogins, admin: false, exportar: true);
            UtilFiesLegado fiesLegadoUtil = new UtilFiesLegado();

            IWebDriver Driver = Util.StartBrowser("http://sisfies.mec.gov.br/", downloadFldr: true);
            ExportarExtratoMensalDeRepasse exportarExtratoMensalDeRepasse = new ExportarExtratoMensalDeRepasse();
            foreach (TOLogin login in listaLogins)
            {
                fiesLegadoUtil.RealizarLoginSucesso(login, Driver);
                fiesLegadoUtil.SelecionarPerfilPresidencia(Driver);
                fiesLegadoUtil.SelecionarMenuExtratoMensalDeRepasse(Driver);

                exportarExtratoMensalDeRepasse.ExtratoMensalDeRepasseLegado(Driver, login.Campus, ano, mes);

                fiesLegadoUtil.FazerLogout(Driver);
            }
            Driver.Close();
            Driver.Dispose();
        }
        public void ExecutarBaixarDRMFiesNovo(string faculdade, string tipoFies, string semestre)
        {
            BuscarLoginsEAlunos(faculdade, FIES_NOVO, campus: "", ref listaAlunos, ref listaLogins, admin: false, exportar: false);
            UtilFiesNovo utilFiesNovo = new UtilFiesNovo();
            IWebDriver Driver = Util.StartBrowser("http://sifesweb.caixa.gov.br");
            BaixarDRM drm = new BaixarDRM();
            drm.SetDriver(Driver);
            foreach (TOLogin login in listaLogins)
            {
                utilFiesNovo.FazerLogin(Driver, login);
                utilFiesNovo.WaitForLoading(Driver);
                utilFiesNovo.ClicarMenuAditamento(Driver);
                foreach (TOAluno aluno in listaAlunos)
                {
                    drm.BaixarDRMFiesNovo(aluno, semestre);
                }

            }
            Driver.Close();
            Driver.Dispose();
        }
        public void ExtrairInformacoesDRMFiesNovo(string faculdade, string tipoFies, string semestre)
        {
            BuscarLoginsEAlunos(faculdade, FIES_NOVO, campus: "", ref listaAlunos, ref listaLogins, admin: false, exportar: false);
            UtilFiesNovo utilFiesNovo = new UtilFiesNovo();
            IWebDriver Driver = Util.StartBrowser("http://sifesweb.caixa.gov.br");
            ExtrairInformacoesDRMFiesNovo extrairInformacoesDRM = new ExtrairInformacoesDRMFiesNovo();
            extrairInformacoesDRM.SetDriver(Driver);
            foreach (TOLogin login in listaLogins)
            {
                utilFiesNovo.FazerLogin(Driver, login);
                utilFiesNovo.WaitForLoading(Driver);
                utilFiesNovo.ClicarMenuAditamento(Driver);
                foreach (TOAluno aluno in listaAlunos)
                {
                    extrairInformacoesDRM.ExtrairInformacoesDRM(aluno, semestre);
                }

            }
            Driver.Close();
            Driver.Dispose();
        }
        public void ExecutarBuscarStatusAditamentoNovo(string faculdade, string tipoFies, string semestre)
        {
            BuscarLoginsEAlunos(faculdade, FIES_NOVO, "", ref listaAlunos, ref listaLogins, admin: false, exportar: false);

            UtilFiesNovo utilFiesNovo = new UtilFiesNovo();
            IWebDriver Driver = Util.StartBrowser("http://sifesweb.caixa.gov.br");
            BuscarStatusAditamento statusAditamento = new BuscarStatusAditamento();
            statusAditamento.SetDriver(Driver);
            utilFiesNovo.FazerLogin(Driver, listaLogins[0]);
            utilFiesNovo.WaitForLoading(Driver);
            utilFiesNovo.ClicarMenuAditamento(Driver);

            foreach (TOAluno aluno in listaAlunos)
            {
                statusAditamento.BuscarStatus(aluno, semestre);
            }
            Driver.Close();
            Driver.Dispose();
        }
        public void ExecutarStatusAluno(string faculdade, string tipoFies, string semestre)
        {
            BuscarLoginsEAlunos(faculdade, FIES_NOVO, "", ref listaAlunos, ref listaLogins, admin: false, exportar: false);

            UtilFiesNovo utilFiesNovo = new UtilFiesNovo();
            StatusAluno statusAluno = new StatusAluno();
            IWebDriver Driver = Util.StartBrowser("http://sifesweb.caixa.gov.br");
            statusAluno.SetDriver(Driver);
            utilFiesNovo.FazerLogin(Driver, listaLogins[0]);
            utilFiesNovo.WaitForLoading(Driver);
            utilFiesNovo.ClicarMenuConsultaContrato(Driver);

            foreach (TOAluno aluno in listaAlunos)
            {
                statusAluno.BuscarStatusAluno(aluno, semestre);
            }
            Driver.Close();
            Driver.Dispose();
        }
        public void ExportarRelatorioFiesNovo(string faculdade, string tipoFies, string tipoRelatorio)
        {
            BuscarLoginsEAlunos(faculdade, FIES_NOVO, "", ref listaAlunos, ref listaLogins, admin: false, exportar: true);
            UtilFiesNovo utilFiesNovo = new UtilFiesNovo();
            ExportarRelatorio exportarRelatorio = new ExportarRelatorio();
            IWebDriver Driver = Util.StartBrowser("http://sifesweb.caixa.gov.br");
            utilFiesNovo.FazerLogin(Driver, listaLogins[0]);
            utilFiesNovo.WaitForLoading(Driver);

            switch (tipoRelatorio)
            {
                case "DRM":
                    utilFiesNovo.ClicarMenuAditamento(Driver);
                    break;
                case "DRT":
                    utilFiesNovo.ClicarMenuTransferencia(Driver);
                    break;
                case "DRD":
                    utilFiesNovo.ClicarMenuDilatacao(Driver);
                    break;
                case "SUSPENSÃO":
                    utilFiesNovo.ClicarMenuSuspensao(Driver);
                    break;
                default:
                    throw new Exception("Como?");
            }
            utilFiesNovo.WaitForLoading(Driver);
            exportarRelatorio.ExportarRelatorioFiesNovo(Driver, tipoRelatorio);

            Driver.Close();
            Driver.Dispose();

        }
        public void ExportarInadimplencia(string faculdade, string mes, string ano)
        {
            BuscarLoginsEAlunos(faculdade, FIES_NOVO, "", ref listaAlunos, ref listaLogins, admin: true, exportar: true);
            UtilFiesNovo utilFiesNovo = new UtilFiesNovo();
            ExportarInadimplencia exportarInadimplencia = new ExportarInadimplencia();
            IWebDriver Driver = Util.StartBrowser("http://sifesweb.caixa.gov.br", firefox: false);
            utilFiesNovo.FazerLogin(Driver, listaLogins[0]);
            utilFiesNovo.WaitForLoading(Driver);
            utilFiesNovo.ClicarMenuInadimplencia(Driver);
            utilFiesNovo.WaitForLoading(Driver);

            //IMPLEMENTAR PASTA DE DOWLOAD

            //string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            //string downloadFolder = System.IO.Path.Combine(userRoot, "Downloads");
            //Util.CreateDirectory(downloadFolder + "\\Relatório Inadimplência");

            exportarInadimplencia.Inadimplencia(Driver, mes, ano);
            //exportarInadimplencia.Inadimplencia(Driver);

            Driver.Close();
            Driver.Dispose();
        }
        public void ExportarRepasseFiesNovo(string faculdade, string mes, string ano)
        {
            BuscarLoginsEAlunos(faculdade, FIES_NOVO, "", ref listaAlunos, ref listaLogins, admin: true, exportar: true);
            UtilFiesNovo utilFiesNovo = new UtilFiesNovo();
            ExportarRepasse exportarRepasse = new ExportarRepasse();
            IWebDriver Driver = Util.StartBrowser("http://sifesweb.caixa.gov.br", firefox: false);
            utilFiesNovo.FazerLogin(Driver, listaLogins[0]);
            utilFiesNovo.WaitForLoading(Driver);
            utilFiesNovo.ClicarMenuRepasse(Driver);
            utilFiesNovo.WaitForLoading(Driver);

            exportarRepasse.ExportarRepasseFiesNovo(Driver, ano, mes);

            Driver.Close();
            Driver.Dispose();
        }
        public void ExportarCoparticipacaoFiesNovo(string faculdade, string dataInicial, string dataFinal)
        {
            BuscarLoginsEAlunos(faculdade, FIES_NOVO, "", ref listaAlunos, ref listaLogins, admin: true, exportar: true);
            UtilFiesNovo utilFiesNovo = new UtilFiesNovo();
            IWebDriver Driver = Util.StartBrowser("http://sifesweb.caixa.gov.br", firefox: false);
            utilFiesNovo.FazerLogin(Driver, listaLogins[0]);
            utilFiesNovo.WaitForLoading(Driver);
            utilFiesNovo.ClicarMenuCoparticipacao(Driver);
            utilFiesNovo.WaitForLoading(Driver);
            ExportarCoparticipacao exportarCoparticipacao = new ExportarCoparticipacao();
            exportarCoparticipacao.ExportarRelatórioCoparticipacao(Driver, faculdade, dataInicial, dataFinal);
            Driver.Close();
            Driver.Dispose();
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
                TratarDadosAluno(aluno);
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

        public void SetForm(IContratos.IMainForms forms)
        {
            this.forms = forms;
        }

        public void TratarDadosAluno(TOAluno aluno)
        {
            Dados.TratarCpf(aluno);
            Dados.TratarTextoReceitas(aluno);
            Dados.TratarVirgulaReceitas(aluno);
            Dados.TratarCampusAluno(aluno);
            Dados.TratarTipoFIES(aluno);
        }

        public void ExecutarAbrirSite(string faculdade, string campus, string plataforma)
        {

            BuscarLoginsEAlunos(faculdade, plataforma, campus, ref listaAlunos, ref listaLogins, admin: false, exportar: true);

            if (plataforma.ToUpper() == "FIES LEGADO")
            {
                UtilFiesLegado fiesLegadoUtil = new UtilFiesLegado();
                IWebDriver Driver = Util.StartBrowser("http://sisfies.mec.gov.br/");
                fiesLegadoUtil.RealizarLoginSucesso(listaLogins[0], Driver);
                fiesLegadoUtil.SelecionarPerfilPresidencia(Driver);
            }
            else
            {
                UtilFiesNovo fiesNovoUtil = new UtilFiesNovo();
                IWebDriver Driver = Util.StartBrowser("http://sifesweb.caixa.gov.br");
                fiesNovoUtil.FazerLogin(Driver, listaLogins[0]);
            }
        }

        public void ExecutarLancamentoFiesSiga(string semestre, string tipoFies)
        {
            listaAlunos = Dados.SelectAll<TOAluno>();
            listaLogins = Dados.SelectLoginPorIESePlataforma("", "SIGA", "", admin: false);
            foreach (TOAluno aluno in listaAlunos)
            {
                Dados.TratarTextoReceitas(aluno);
                Dados.TratarVirgulaReceitas(aluno);
            }

            UtilSiga utilsiga = new UtilSiga();
            IWebDriver Driver = utilsiga.FazerLogin("https://siga.uniritter.edu.br/financeiro/fichaFinanceira.php", listaLogins[0]);
            // Não caiu
            if (Driver == null || Driver.PageSource.Contains("Você precisa realizar a validação \"Não sou um robô\".Tente novamente marcando esta opção!"))
            {
                return;
            }
            while (Driver.PageSource.ToUpper().Contains("FILTRO POR DADOS DE ALUNO") == false)
            {
                System.Threading.Thread.Sleep(250);
            }
            LancamentoFiesSiga lancamentoFiesSiga = new LancamentoFiesSiga();
            foreach (TOAluno aluno in listaAlunos)
            {
                if (aluno.Conclusao == "Não Feito")
                {
                    lancamentoFiesSiga.ExecutarLancamentoFiesSiga(aluno, Driver, semestre, tipoFies);
                }
            }
        }

        public void GeracaoParcelasFies(string semestre)
        {
            listaAlunos = Dados.SelectAll<TOAluno>();
            listaLogins = Dados.SelectLoginPorIESePlataforma("", "SIGA", "", admin: false);
            UtilSiga utilsiga = new UtilSiga();
            IWebDriver Driver = utilsiga.FazerLogin("https://siga.uniritter.edu.br/financeiro/geracaoIndividualParcela.php", listaLogins[0]);

            if (Driver == null || Driver.PageSource.Contains("Você precisa realizar a validação \"Não sou um robô\"."))
            {
                Driver.Close();
                Driver.Dispose();
                return;
            }


            GeracaoParcelasFies GeracaoParcelasFies = new GeracaoParcelasFies();
            GeracaoParcelasFies.ExecutarCookieGuiche(Driver);
            foreach (TOAluno aluno in listaAlunos)
            {
                // Isso deu pau
                int resultado = 7;
                // Pode dar problema futuro
                if (aluno.Conclusao.Contains("PARCELA"))
                {
                    resultado = aluno.Conclusao.Split(new string[] { "PARCELA" }, StringSplitOptions.None).Length;
                    var teste = aluno.Conclusao.Split(new string[] { "PARCELA" }, StringSplitOptions.None);
                }
                if (aluno.Conclusao == "Não Feito" || resultado < 6)
                {
                    GeracaoParcelasFies.GeraParcelaFies(Driver, aluno, semestre);
                }
            }
        }

        public void ExecutarHistoricoReparcelamentoCoparticipacao(string faculdade, string tipoFIES)
        {

            HistoricoReparcelamentoCoparticipacao historico = new HistoricoReparcelamentoCoparticipacao();

            BuscarLoginsEAlunos(faculdade, tipoFIES, "", ref listaAlunos, ref listaLogins, admin: false, exportar: false);

            UtilFiesNovo utilFiesNovo = new UtilFiesNovo();
            IWebDriver Driver = Util.StartBrowser("http://sifesweb.caixa.gov.br");

            utilFiesNovo.FazerLogin(Driver, listaLogins[0]);
            utilFiesNovo.WaitForLoading(Driver);
            utilFiesNovo.ClicarMenuHistoricoReparcelamentoCopartipacao(Driver);

            foreach (TOAluno aluno in listaAlunos)
            {
                if (aluno.Conclusao == "Não Feito")
                {
                    historico.ExecutarHistoricoReparcelamentoCoparticipacao(Driver, aluno);
                }
            }
            Driver.Close();
            Driver.Dispose();
        }

        public void ValidarReparcelamento(string faculdade, string tipoFIES)
        {
            ValidarReparcelamento validar = new ValidarReparcelamento();


            BuscarLoginsEAlunos(faculdade, tipoFIES, "", ref listaAlunos, ref listaLogins, admin: true, exportar: false);

            UtilFiesNovo utilFiesNovo = new UtilFiesNovo();
            IWebDriver Driver = Util.StartBrowser("http://sifesweb.caixa.gov.br");

            utilFiesNovo.FazerLogin(Driver, listaLogins[0]);
            utilFiesNovo.WaitForLoading(Driver);
            utilFiesNovo.ClicarMenuValidarReparcelamento(Driver);

            validar.ExecutarValidarReparcelamento(Driver);

            Driver.Close();
            Driver.Dispose();
        }
    }
}
