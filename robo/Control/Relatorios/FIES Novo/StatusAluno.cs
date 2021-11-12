using OpenQA.Selenium;
using Robo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robo.Control.Relatorios.FIES_Novo
{
    class StatusAluno : UtilFiesNovo
    {
        private IWebDriver Driver;

        public void BuscarStatusAluno(TOAluno aluno, string semestre)
        {
            while (Driver.PageSource.Contains("Consultar Contrato Estudante") == false)
            {
                System.Threading.Thread.Sleep(100);
            }
            ((IJavaScriptExecutor)Driver).ExecuteScript($@"document.getElementById(""cpf"").value = ""{aluno.Cpf}"";");
            ClickButtonsById(Driver, "btnConfirmar");
            WaitForLoading(Driver);
            ClickButtonsById(Driver, "lnkTipoProcesso");
            WaitForLoading(Driver);

            ClickButtonByIdWithJavaScript(Driver, "tab-Aditamento");
            WaitForLoading(Driver);

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
            ClickButtonsById(Driver, "btn-voltar");
            WaitForLoading(Driver);
        }
        public void SetDriver(IWebDriver driver)
        {
            Driver = driver;
        }

        private string CorrigirSemestreAlunoConsultaNovo(string semestre)
        {
            semestre = semestre.Replace("1/", "1º/");
            semestre = semestre.Replace("2/", "2º/");

            return semestre;
        }
    }
}
