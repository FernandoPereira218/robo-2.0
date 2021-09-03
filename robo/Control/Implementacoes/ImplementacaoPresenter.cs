using OpenQA.Selenium;
using robo.Control.Legado;
using robo.Control.Relatorios;
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
            BuscarLoginsEAlunos(faculdade, tipoFies, campus, ref listaAlunos, ref listaLogins, admin:false, exportar:false);
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
            BuscarLoginsEAlunos(faculdade, tipoFies, "", ref listaAlunos, ref listaLogins, admin:false, exportar:false);

            //Iniciar navegador
            foreach (TOAluno aluno in listaAlunos)
            {
                //Aditamento
            }
            //Fechar navegador
        }
        public void ExecutarDRI(string faculdade, string tipoFies, string campus, string situacaoDRI, bool baixarDRI)
        {
            BuscarLoginsEAlunos(faculdade, tipoFies, campus, ref listaAlunos, ref listaLogins, admin:false, exportar:false);
            UtilFiesLegado fiesLegadoUtil = new UtilFiesLegado();

            IWebDriver Driver = Util.StartBrowser("http://sisfies.mec.gov.br/");
            DRI dri = new DRI();
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
            BuscarLoginsEAlunos(faculdade, tipoFies, campus, ref listaAlunos, ref listaLogins, admin:false, exportar:false);
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
            BuscarLoginsEAlunos(faculdade, tipoFies, campus, ref listaAlunos, ref listaLogins, admin:false, exportar:true);
            UtilFiesLegado fiesLegadoUtil = new UtilFiesLegado();
            ExportarRelatorios exportarRelatorios = new ExportarRelatorios();
            semestre = semestre.Replace("1/", "1º/");
            semestre = semestre.Replace("2/", "2º/");

            IWebDriver Driver = Util.StartBrowser("http://sisfies.mec.gov.br/", downloadFldr:true);

            foreach (TOLogin login in listaLogins)
            {
                fiesLegadoUtil.RealizarLoginSucesso(login, Driver);
                fiesLegadoUtil.SelecionarPerfilPresidencia(Driver);
                fiesLegadoUtil.SelecionarMenuBaixarDocumentos(Driver);

                exportarRelatorios.ExportarDocumentosFiesLegado(Driver, semestre, tipoRelatorio, campus);

                fiesLegadoUtil.FazerLogout(Driver);
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


    }
}
