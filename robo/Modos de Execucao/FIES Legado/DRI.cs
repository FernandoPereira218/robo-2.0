using OpenQA.Selenium;
using robo.Banco_de_Dados;
using robo.Contratos;
using robo.Excessoes;
using robo.Interface;
using robo.TO;
using robo.Utils;
using System.IO;
using System.Linq;

namespace robo.Modos_de_Execucao.FIES_Legado
{
    public class DRI : UtilFiesLegado, IModosDeExecucao.IModoComAlunos
    {
        private bool baixar;
        private string campus;
        private string situacaoDRI;
        public DRI(bool baixar, string situacaoDRI, string campus)
        {
            this.baixar = baixar;
            this.situacaoDRI = situacaoDRI;
            this.campus = campus;
        }
        public void DRIFiesLegado(TOAluno aluno)
        {
            ClickDropDown( "id", "co_situacao_inscricao", situacaoDRI);

            if (!Dados.VerificarDRI(aluno.Cpf) || baixar == true)
            {
                ConsultarAluno(aluno);

                if (Driver.PageSource.Contains("sorterdocuments"))
                {
                    ClickButtonsByCss( ".even:nth-child(1) img");

                    if (!Driver.PageSource.Contains("Voltar para a página principal"))
                    {
                        VerificarMensagemDeErro(aluno);

                        if (Driver.PageSource.Contains("Imprimir DRI"))
                        {
                            if (baixar == true)
                            {
                                BaixarDRI(aluno);
                            }
                            else
                            {
                                SalvarDRIAluno(aluno, campus);
                            }

                            Util.EditarConclusaoAluno(aluno, "DRI Baixada");
                            ClickButtonsById( "voltar");
                        }
                    }
                }
            }
            else
            {
                Util.EditarConclusaoAluno(aluno, "DRI Baixada anteriormente");
            }
        }

        private void VerificarMensagemDeErro(TOAluno aluno)
        {
            string mensagem = VerificarMensagem();
            if (mensagem != "")
            {
                Util.EditarConclusaoAluno(aluno, mensagem);
                throw new PararExecucaoException();
            }
        }

        private void ConsultarAluno(TOAluno aluno)
        {
            WaitinLoading();

            ClickAndWriteById( "nu_cpf", aluno.Cpf);
            ClickButtonsById( "consulta");

            WaitinLoading();
        }

        private void BaixarDRI(TOAluno aluno)
        {
            IWebElement elementoNome = Driver.FindElement(By.XPath("/html/body/div[3]/div[4]/div[2]/div[2]/div[3]/div[1]/span[2]"));
            string nome = elementoNome.Text.Replace("Nome Completo: ", "");
            aluno.Nome = nome;
            ScrollToElementByID( "imprimir_dri");
            ClickButtonsById( "imprimir_dri");

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

        public void Executar(TOAluno aluno)
        {
            DRIFiesLegado(aluno);
        }

        public void SelecionarMenu()
        {
            throw new System.NotImplementedException();
        }

        public void SetWebDriver(IWebDriver Driver)
        {
            this.Driver = Driver;
        }
    }
}