using OpenQA.Selenium;
using robo.Banco_de_Dados;
using robo.Interface;
using robo.TO;
using robo.Utils;
using System.IO;
using System.Linq;

namespace robo.Modos_de_Execucao.FIES_Legado
{
    public class DRI : UtilFiesLegado
    {
        private IWebDriver Driver;
        public void DRIFiesLegado(TOAluno aluno, TOLogin login, bool baixar, string situacaoDRI)
        {
            ClickDropDown(Driver, "id", "co_situacao_inscricao", situacaoDRI);

            if (!Dados.DRIExists(aluno.Cpf) || baixar == true)
            {
                ConsultarAluno(aluno);

                if (Driver.PageSource.Contains("sorterdocuments"))
                {
                    ClickButtonsByCss(Driver, ".even:nth-child(1) img");

                    if (!Driver.PageSource.Contains("Voltar para a página principal"))
                    {
                        string mensagem = VerificarMensagem(Driver);
                        if (mensagem != "")
                        {
                            Util.EditarConclusaoAluno(aluno, mensagem);
                            return;
                        }

                        if (Driver.PageSource.Contains("Imprimir DRI"))
                        {
                            if (baixar == true)
                            {
                                BaixarDRI(aluno);
                            }
                            else
                            {
                                SalvarDRIAluno(aluno, login.Campus);
                            }

                            Util.EditarConclusaoAluno(aluno, "DRI Baixada");
                            ClickButtonsById(Driver, "voltar");
                        }
                    }
                }
            }
            else
            {
                Util.EditarConclusaoAluno(aluno, "DRI Baixada anteriormente");
            }
        }

        private void ConsultarAluno(TOAluno aluno)
        {
            WaitinLoading(Driver);

            ClickAndWriteById(Driver, "nu_cpf", aluno.Cpf);
            ClickButtonsById(Driver, "consulta");

            WaitinLoading(Driver);
        }

        private void BaixarDRI(TOAluno aluno)
        {
            IWebElement elementoNome = Driver.FindElement(By.XPath("/html/body/div[3]/div[4]/div[2]/div[2]/div[3]/div[1]/span[2]"));
            string nome = elementoNome.Text.Replace("Nome Completo: ", "");
            aluno.Nome = nome;
            ScrollToElementByID(Driver, "imprimir_dri");
            ClickButtonsById(Driver, "imprimir_dri");

            Util.BaixarDocumento(aluno.Nome + "_" + aluno.Cpf + "_DRI", "DRI", "");
        }
        private void SalvarDRIAluno(TOAluno aluno, string loginCampus)
        {
            var coInscricao = Driver.FindElement(By.Id("co_inscricao"));
            string nroDRI = coInscricao.GetAttribute("value");

            TODRI dri = new TODRI();
            dri.DRI = nroDRI;
            dri.Cpf = aluno.Cpf;
            dri.CampusAditado = loginCampus;

            if (FormInterface.versaoRobo == "operacoesFinanceiras")
            {
                Dados.InsertDocumento<TODRI>(dri);
            }
        }
        public void SetDriver(IWebDriver driver)
        {
            Driver = driver;
        }
    }
}