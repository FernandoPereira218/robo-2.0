using OpenQA.Selenium;
using robo.Contratos;
using robo.TO;
using robo.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robo.Modos_de_Execucao.FIES_Novo
{
    public class BuscarStatusAditamento : UtilFiesNovo, IModosDeExecucao.IModoComAlunos
    {
        private string semestreAtual;

        public BuscarStatusAditamento(string semestreAtual)
        {
            this.semestreAtual = semestreAtual;
        }

        public void BuscarStatus(TOAluno aluno)
        {
            ConsultarAluno( aluno);
            WaitForLoading();

            if (VerificarNenhumaInformacaoDisponivel() == true)
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
                    situacaoAluno = BuscarSituacaoAluno( semestreAtual);
                }
                else
                {
                    situacaoAluno = "Semestre não encontrado.";
                }
                Util.EditarConclusaoAluno(aluno, situacaoAluno);
            }
        }

        public void ExecucaoComListaDeAlunos(TOAluno aluno)
        {
            BuscarStatus(aluno);
        }

        public void SelecionarMenu()
        {
            ClicarMenuAditamento();
        }

        public void SetWebDriver(IWebDriver Driver)
        {
            this.Driver = Driver;
        }
    }
}
