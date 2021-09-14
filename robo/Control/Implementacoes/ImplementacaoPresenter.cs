using OpenQA.Selenium;
using robo.Control.Legado;
using robo.Control.Relatorios;
using robo.Control.Relatorios.FIES_Legado;
using robo.Control.Relatorios.FIES_Novo;
using robo.pgm;
using Robo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robo.Control.Implementacoes
{
    public class ImplementacaoPresenter : IContratos.IPresenter
    {
        private IContratos.IMainForms forms;
        private List<TOAluno> listaAlunos;
        private List<TOLogin> listaLogins;
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
            BuscarLoginsEAlunos(faculdade, tipoFies, campus, ref listaAlunos, ref listaLogins, admin: false, exportar: false);
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
        public void ExecutarAditamentoNovo(string faculdade, string tipoFies)
        {
            BuscarLoginsEAlunos(faculdade, tipoFies, "", ref listaAlunos, ref listaLogins, admin: false, exportar: false);

            //Iniciar navegador
            foreach (TOAluno aluno in listaAlunos)
            {
                //Aditamento
            }
            //Fechar navegador
        }
        public void ExecutarDRI(string faculdade, string tipoFies, string campus, string situacaoDRI, bool baixarDRI)
        {
            BuscarLoginsEAlunos(faculdade, tipoFies, campus, ref listaAlunos, ref listaLogins, admin: false, exportar: false);
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
            BuscarLoginsEAlunos(faculdade, tipoFies, campus, ref listaAlunos, ref listaLogins, admin: false, exportar: false);
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
            BuscarLoginsEAlunos(faculdade, tipoFies, campus, ref listaAlunos, ref listaLogins, admin: false, exportar: true);
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
            BuscarLoginsEAlunos(faculdade, tipoFies, campus, ref listaAlunos, ref listaLogins, admin: false, exportar: false);
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
        public void ExtrairInformacoesDRILegado(string faculdade, string tipoFies, string campus, string situacaoDRI)
        {
            BuscarLoginsEAlunos(faculdade, tipoFies, campus, ref listaAlunos, ref listaLogins, admin: false, exportar: false);
            UtilFiesLegado fiesLegadoUtil = new UtilFiesLegado();

            IWebDriver Driver = Util.StartBrowser("http://sisfies.mec.gov.br/");
            ExtrairInformacoesDRI extrairInformacoesDRI = new ExtrairInformacoesDRI();
            foreach (TOLogin login in listaLogins)
            {
                fiesLegadoUtil.RealizarLoginSucesso(login, Driver);
                fiesLegadoUtil.SelecionarPerfilPresidencia(Driver);
                fiesLegadoUtil.SelecionarMenuDRI(Driver);
                foreach (TOAluno aluno in listaAlunos)
                {
                    extrairInformacoesDRI.ExecutarExtrairInformacoesDRI(Driver, aluno, situacaoDRI);
                }

                fiesLegadoUtil.FazerLogout(Driver);
            }
            Driver.Close();
            Driver.Dispose();
        }
        public void ExecutarExportarDRILegado(string faculdade, string tipoFies, string campus, string situacaoDRI)
        {
            BuscarLoginsEAlunos(faculdade, tipoFies, campus, ref listaAlunos, ref listaLogins, admin: false, exportar: true);
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
            BuscarLoginsEAlunos(faculdade, tipoFies, campus, ref listaAlunos, ref listaLogins, admin: false, exportar: true);
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
            BuscarLoginsEAlunos(faculdade, tipoFies, campus: "", ref listaAlunos, ref listaLogins, admin: false, exportar: false);
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
            BuscarLoginsEAlunos(faculdade, tipoFies, campus: "", ref listaAlunos, ref listaLogins, admin: false, exportar: false);
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


        public string BuscarNunSemestre(string semestreAno)
        {
            List<TOSemestre> semestre = Dados.SelectSemestre();
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
            alunosFies = Dados.SelectAlunoWhere(plataforma);
            foreach (TOAluno aluno in alunosFies)
            {
                TratarDadosAluno(aluno);

                if (aluno.AproveitamentoAtual.Contains("TRANCADO") == true)
                {
                    aluno.Conclusao = "Trancado";
                    Dados.UpdateAluno(aluno);
                }

            }

            if (alunosFies.Count == 0)
            {
                throw new Exception(String.Format("Nenhum aluno encontrado na plataforma escolhida ({0}). Cheque se o banco de dados contém alunos da plataforma que deseja realizar os aditamentos.", plataforma));
            }
            return alunosFies;
        }
        public List<string> PreencherListaExecucao()
        {
            List<TOMenus> menus = Dados.SelectMenus();
            List<string> nomeMenu = new List<string>();
            foreach (var item in menus)
            {
                nomeMenu.Add(item.Item);
            }
            return nomeMenu;
        }

        public List<string> PreencherListaExecucaoPorPlataforma(string plataforma)
        {
            List<TOMenus> menus = Dados.SelectMenuWhere(plataforma);
            List<string> nomeMenu = new List<string>();
            foreach (var item in menus)
            {
                nomeMenu.Add(item.Item);
            }
            return nomeMenu;
        }

        public List<string> PreencherListaSemestre()
        {
            List<TOSemestre> semestre = Dados.SelectSemestre();
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
 
    }
}
