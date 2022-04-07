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
    public class DRI : UtilFiesLegado, IModosDeExecucao.IModoComAlunos, IModosDeExecucao.IFiesLegado
    {
        private bool baixar;
        private string campus;
        private string situacaoDRI;
        public DRI(bool baixar, string situacaoDRI)
        {
            this.baixar = baixar;
            this.situacaoDRI = situacaoDRI;
        }
        public void DRIFiesLegado(TOAluno aluno)
        {
            Driver.Url = "http://sisfies.mec.gov.br//inscricao/principal/";
            SelecionarOpcaoDropDown( "id", "co_situacao_inscricao", situacaoDRI);

            if (!Dados.VerificarDRI(aluno.Cpf) || baixar == true)
            {
                ConsultarAluno(aluno);
                while (!Driver.Url.Contains("?page"))
                {
                    Sleep();
                }
                if (Driver.PageSource.Contains("sorterdocuments"))
                {
                    ClicarElemento(By.CssSelector(".even:nth-child(1) img"));    
                    while(VerificarMensagem() == string.Empty && !Driver.PageSource.Contains("Imprimir DRI"))
                    {
                        Sleep();
                    }
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
                            ClicarElemento(By.Id("voltar"));
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
            EsperarLoading();

            ClicarEEscrever(By.Id("nu_cpf"), aluno.Cpf);
            ClicarElemento(By.Id("consulta"));

            EsperarLoading();
        }

        private void BaixarDRI(TOAluno aluno)
        {
            IWebElement elementoNome = Driver.FindElement(By.XPath("/html/body/div[3]/div[4]/div[2]/div[2]/div[3]/div[1]/span[2]"));
            string nome = elementoNome.Text.Replace("Nome Completo: ", "");
            aluno.Nome = nome;
            ScrollParaElemento(By.Id("imprimir_dri"));
            ClicarElemento(By.Id("imprimir_dri"));

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
            SelecionarMenuDRI();
        }

        public void SetWebDriver(IWebDriver Driver)
        {
            this.Driver = Driver;
        }

        public void TrocarCampus(string campus)
        {
            this.campus = campus;
        }
    }
}