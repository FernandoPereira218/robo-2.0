using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robo;

namespace robo.Control.Relatorios.FIES_Novo
{
    public class BuscarStatusAditamento
    {
        private IWebDriver Driver;
        private UtilFiesNovo utilFiesNovo = new UtilFiesNovo();

        public void BuscarStatus(TOAluno aluno, string semestreAtual)
        {
            utilFiesNovo.ConsultarAluno(Driver, aluno);
           utilFiesNovo.WaitForLoading(Driver);

            if (utilFiesNovo.VerificarNenhumaInformacaoDisponivel(Driver) == true)
            {
                Util.EditarConclusaoAluno(aluno, "Nenhuma informação disponível");
                return;
            }
            else
            {
                string situacaoAluno = string.Empty;
                IWebElement grid = Driver.FindElement(By.Id("gridResult"));
                if (grid.Text.Contains(semestreAtual) == true)
                {
                    situacaoAluno = utilFiesNovo.BuscarSituacaoAluno(Driver, semestreAtual);
                }
                else
                {
                    situacaoAluno = "Semestre não encontrado.";
                }
                Util.EditarConclusaoAluno(aluno, situacaoAluno);
            }
        }

        public void SetDriver(IWebDriver driver)
        {
            Driver = driver;
        }
    }
}
