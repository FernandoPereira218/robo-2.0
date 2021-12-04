using OpenQA.Selenium;
using robo.TO;
using robo.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using robo.Contratos;
using System.Threading.Tasks;

namespace robo.Modos_de_Execucao.FIES_Novo
{
    class StatusAluno : UtilFiesNovo, IModosDeExecucao.IModoComAlunos
    {
        private string semestre;
        public StatusAluno(string semestre)
        {
            this.semestre = semestre;
        }

        public void BuscarStatusAluno(TOAluno aluno)
        {
            WaitForLoading();
            while (Driver.PageSource.Contains("Consultar Contrato Estudante") == false)
            {
                System.Threading.Thread.Sleep(100);
            }
            ((IJavaScriptExecutor)Driver).ExecuteScript($@"document.getElementById(""cpf"").value = ""{aluno.Cpf}"";");
            ClickButtonsById( "btnConfirmar");
            WaitForLoading();
            ClickButtonsById( "lnkTipoProcesso");
            WaitForLoading();

            ClickButtonByIdWithJavaScript( "tab-Aditamento");
            WaitForLoading();

            IWebElement elementoTabela = Driver.FindElement(By.Id("gridAditamento"));
            List<IWebElement> dados = elementoTabela.FindElements(By.TagName("td")).ToList();
            for (int j = 0; j < dados.Count(); j++)
            {
                if (dados[j].Text == semestre)
                {
                    aluno.SemestreAno = dados[j].Text;
                    aluno.Finalidade = dados[j + 1].Text;
                    aluno.Situacao = dados[j + 2].Text;
                    aluno.TipoAditamento = dados[j + 3].Text;
                    aluno.ProUni = dados[j + 4].Text;
                    aluno.DataInclusao = dados[j + 5].Text;
                    aluno.DataConclusao = dados[j + 6].Text;
                    aluno.HorarioConclusao = string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now);
                    break;
                }
            }
            if (aluno.SemestreAno != string.Empty)
            {
                aluno.SemestreAno = CorrigirSemestreAlunoConsultaNovo(aluno.SemestreAno);
                Util.EditarConclusaoAluno(aluno, "Status Atualizado");
            }
            else
            {
                aluno.SemestreAno = "N/A";
                aluno.Finalidade = "N/A";
                aluno.Situacao = "Semestre não encontrado";
                aluno.TipoAditamento = "N/A";
                aluno.ProUni = "N/A";
                aluno.DataInclusao = "N/A";
                aluno.DataConclusao = "N/A";
                Util.EditarConclusaoAluno(aluno, "Semestre não encontrado");
            }


            var element = Driver.FindElement(By.Id("btn-voltar"));
            ((IJavaScriptExecutor)Driver).ExecuteScript(string.Format("window.scrollTo({0}, {1})", element.Location.X, element.Location.Y - 100));
            ClickButtonsById( "btn-voltar");
            WaitForLoading();
        }

        public void Executar(TOAluno aluno)
        {
            BuscarStatusAluno(aluno);
        }

        public void SelecionarMenu()
        {
            ClicarMenuConsultaContrato();
        }

        public void SetWebDriver(IWebDriver Driver)
        {
            this.Driver = Driver;
        }

        private string CorrigirSemestreAlunoConsultaNovo(string semestre)
        {
            semestre = semestre.Replace("1/", "1º/");
            semestre = semestre.Replace("2/", "2º/");

            return semestre;
        }
    }
}
