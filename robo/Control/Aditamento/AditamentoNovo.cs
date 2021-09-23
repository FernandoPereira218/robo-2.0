using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robo;
using System.Windows.Forms;

namespace robo.Control.Aditamento
{
    public class AditamentoNovo : UtilFiesNovo
    {
        private IWebDriver Driver;
        public void AditamentoFiesNovo(TOAluno aluno, string IES, string semestreAtual)
        {
            ConsultarAluno(Driver, aluno);
            WaitForLoading(Driver);

            if (VerificarNenhumaInformacaoDisponivel(Driver) == true)
            {
                Util.EditarConclusaoAluno(aluno, "Nenhuma informação disponível");
                return;
            }

            if (Driver.PageSource.Contains("Não iniciado pela CPSA") == true)
            {
                Util.ScrollToElementByID(Driver, "btnAditarEstudante");
                Util.ClickButtonsById(Driver, "btnAditarEstudante");

                IWebElement ajax = Driver.FindElement(By.Id("ajaxStatus"));
                while (ajax.Displayed == true)
                {
                    System.Threading.Thread.Sleep(1000);
                }
                var executor = (IJavaScriptExecutor)Driver;

                
                WaitForLoading(Driver);
                string erro = VerificarErroAditamento();
                if (erro != string.Empty)
                {
                    Util.EditarConclusaoAluno(aluno, erro);
                    Util.ScrollToElementByID(Driver, "btnVoltar");
                    Util.ClickButtonsById(Driver, "btnVoltar");
                    WaitForLoading(Driver);
                    ClicarMenuAditamento(Driver);
                    return;
                }

                if (Driver.PageSource.ToUpper().Contains("ESTUDANTE TRANSFERIDO NO SEMESTRE") == true)
                {
                    Driver.FindElement(By.ClassName("btn-ok")).Click();
                }

                string alerta = VerificarAlertaAditamento();
                if (alerta != string.Empty)
                {
                    if (alerta.ToUpper().Contains("TRANSFERIDO NO SEMESTRE") == false)
                    {
                        aluno.Conclusao = alerta;
                        aluno.HorarioConclusao = string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now);
                        Dados.UpdateAluno(aluno);
                        Util.ScrollToElementByID(Driver, "btnVoltar");
                        Util.ClickButtonsById(Driver, "btnVoltar");
                        return;
                    }
                }

                //Definição semestre atual
                Util.ScrollToElementByID(Driver, "qtSemestreACursar");
                int semestreASerCursado = Convert.ToInt32(Driver.FindElement(By.Id("totalSemestresFinanciados")).Text);
                executor.ExecuteScript($@"document.getElementById(""qtSemestreACursar"").value = ""{semestreASerCursado + 1}"";");

                //Semestralidade Atual 
                Util.ScrollToElementByID(Driver, "semestralidadeAtualComDescGradeASerCursada");
                Util.ClickAndWriteById(Driver, "semestralidadeAtualComDescGradeASerCursada", aluno.ReceitaFies);

                //Click para atualizar a página
                Driver.FindElement(By.Id("semestralidadeAtualComDescGradeASerCursadaLabel")).Click();
                WaitForLoading(Driver);

                if (Driver.PageSource.Contains("inferior ao valor mínimo"))
                {
                    alerta = VerificarAlertaAditamento();
                    Util.EditarConclusaoAluno(aluno, alerta);
                    Util.ScrollToElementByID(Driver, "btnVoltar");
                    Util.ClickButtonsById(Driver, "btnVoltar");
                    return;
                }

                WaitForLoading(Driver);

                string alertMessage = string.Empty;
                if (Driver.PageSource.Contains("MDLalerta_"))
                {
                    //driver.FindElement(By.XPath("//select[@" + metodo + "='" + valorMetodo + "']/option[contains(.,'" + valorEscolha + "')]")).Click();
                    var elementosAlerta = Driver.FindElements(By.XPath("//div[contains(@id,\"MDLalerta_\")]"));
                    if (elementosAlerta.Count == 1)
                    {
                        if (elementosAlerta[0].Displayed == true)
                        {

                            if (Driver.PageSource.ToUpper().Contains("INFERIOR AO PERCENTUAL MÍNIMO DE SEMESTRALIDADE ATUAL") == false)
                            {

                                Driver.FindElement(By.ClassName("btn-ok")).Click();
                                if (IES.ToUpper() == "UNIRITTER" || IES.ToUpper() == "FADERGS")
                                {
                                    if (aluno.Justificativa == string.Empty)
                                    {
                                        Util.ClickAndWriteById(Driver, "justificativaAcimaLimite", "Valores conforme o número de créditos financeiros matriculados no semestre");
                                    }
                                    else
                                    {
                                        Util.ClickAndWriteById(Driver, "justificativaAcimaLimite", aluno.Justificativa);
                                    }
                                }
                                else
                                {
                                    Util.ClickAndWriteById(Driver, "justificativaAcimaLimite", "Alteração na grade curricular em relação ao semestre anterior");
                                }
                            }
                            else
                            {
                                IWebElement alertElement = Driver.FindElement(By.XPath("//div[@class='modal hide fade in']/div[2]"));
                                alertMessage = alertElement.Text;
                                Driver.FindElement(By.ClassName("btn-ok")).Click();
                            }
                        }
                    }
                }

                //Prouni
                string possuiProuni = Driver.FindElement(By.Id("prouni")).Text;

                //Marcar CheckBox
                Util.ScrollToElementByID(Driver, "aproveitamento75S");
                if (aluno.AproveitamentoAtual.ToUpper().Contains("SUPERIOR A 75%") == true)
                {
                    AproveitamentoMaiorDe75(aluno, possuiProuni);
                }
                else if (aluno.AproveitamentoAtual.ToUpper().Contains("INFERIOR A 75%") == true)
                {
                    AproveitamentoMenorDe75(aluno, possuiProuni);
                }
                else
                {
                    Util.EditarConclusaoAluno(aluno, "Aproveitamento: " + aluno.AproveitamentoAtual);
                    Util.ScrollToElementByID(Driver, "btnVoltar");
                    Util.ClickButtonsById(Driver, "btnVoltar");
                    return;
                }

                aluno.ValorPagoRecursoEstudante = Driver.FindElement(By.Id("vlrPagoRecursoEstudante")).Text;
                aluno.ValorAditadoFinanciamento = Driver.FindElement(By.Id("vlrPagoRecursoFinanciamento")).Text;

                //Aguarda até voltar a página de consulta
                while (Driver.PageSource.Contains("btnConsultar") == false)
                {
                    if (alertMessage == "")
                    {
                        Util.ScrollToElementByID(Driver, "btnConfirmar");
                        Util.ClickButtonsById(Driver, "btnConfirmar");
                        WaitForLoading(Driver);

                        if (Driver.PageSource.Contains("alert alert-error"))
                        {
                            IWebElement alertElement = Driver.FindElement(By.ClassName("alert-error"));
                            alertMessage = alertElement.Text;
                            alertMessage = alertMessage.Replace("x\r\n", "");

                            break;

                        }
                        break;
                    }
                    break;
                }
                if (alertMessage != "")
                {
                    Util.EditarConclusaoAluno(aluno, alertMessage);
                }
                else
                {
                    Util.EditarConclusaoAluno(aluno, "Aditado com sucesso");
                }
                Util.ScrollToElementByID(Driver, "btnVoltar");
                Util.ClickButtonsById(Driver, "btnVoltar");
            }
            else
            {
                string situacaoAluno = string.Empty;
                IWebElement grid = Driver.FindElement(By.Id("gridResult"));
                if (grid.Text.Contains(semestreAtual) == true)
                {
                    situacaoAluno = BuscarSituacaoAluno(Driver, semestreAtual);
                }
                else
                {
                    situacaoAluno = "Semestre não encontrado.";
                }

                Util.EditarConclusaoAluno(aluno, situacaoAluno);
            }
        }
        private void QuestionamentoPadrao(TOAluno aluno, string possuiProuni)
        {
            Driver.FindElement(By.Id("regularmenteMatriculadoS")).Click();

            if (possuiProuni.ToUpper().Contains("SIM") == true)
            {
                Driver.FindElement(By.Id("beneficioSimultaneoS")).Click();
            }
            else
            {
                Driver.FindElement(By.Id("beneficioSimultaneoN")).Click();
            }
            Util.ScrollToElementByID(Driver, "duracaoCursoS");

            Driver.FindElement(By.Id("duracaoCursoS")).Click();
            Driver.FindElement(By.Id("transferiuCursoN")).Click();

            //Se aparecer: 
            if (Driver.FindElement(By.Id("erroValidarTotSemestresN")).Displayed)
            {
                //Houve erro do estudante e da CPSA ao registrar e validar, respectivamente, o total de semestres j� conclu�dos do curso: N�O 
                Util.ClickButtonsById(Driver, "erroValidarTotSemestresN");
            }
            if (aluno.DescontoLiberalidade.Equals("Sim") == true)
            {
                Util.ScrollToElementByID(Driver, "descontoLiberalidadeS");
                Driver.FindElement(By.Id("descontoLiberalidadeS")).Click();

                Util.ScrollToElementByID(Driver, "motivoDesconto8");
                Util.ClickButtonsById(Driver, "motivoDesconto8");

                //Caixa de texto separada
                var executor = (IJavaScriptExecutor)Driver;
                executor.ExecuteScript($@"document.getElementById(""justificativaDesconto"").value = ""Desconto Semestral"";");
            }
            else
            {
                Driver.FindElement(By.Id("descontoLiberalidadeN")).Click();
            }
        }
        private void AproveitamentoMaiorDe75(TOAluno aluno, string possuiProuni)
        {
            Driver.FindElement(By.Id("aproveitamento75S")).Click();

            QuestionamentoPadrao(aluno, possuiProuni);
        }
        private void AproveitamentoMenorDe75(TOAluno aluno, string possuiProuni)
        {
            if (aluno.HistoricoAproveitamento.Contains("Excesso de reprovação"))
            {
                MessageBox.Show("Avisar os guris que aconteceu pela 1º vez Excesso de reprovação.");
                //O estudante teve aproveitamento acad�mico igual ou superior a 75% no semestre ? NAO 
                Util.ClickButtonsById(Driver, "aproveitamento75N");
                aluno.Conclusao = "Rejeitou execesso de reprovação";
            }
            else
            {
                //A CPSA irá liberar o aditamento nesta situação? SIM  
                Util.ScrollToElementByID(Driver, "aproveitamento75N");
                Util.ClickButtonsById(Driver, "aproveitamento75N");

                //Justificativa 
                Util.ScrollToElementByID(Driver, "justificativa");
                Util.ClickAndWriteById(Driver, "justificativa", aluno.HistoricoAproveitamento);


                Util.ClickButtonsById(Driver, "aproveitamentoInferior75S");

                QuestionamentoPadrao(aluno, possuiProuni);
            }
        }
        private string VerificarErroAditamento()
        {
            var executor = (IJavaScriptExecutor)Driver;
            string error = string.Empty;
            if (Driver.PageSource.Contains("alert-error"))
            {
                var errorMsg = Driver.FindElement(By.XPath("/html/body/div[1]/div"));
                error = errorMsg.Text;
                error = error.Replace("x\r\n", "");
                Util.ClickButtonsByXpath(Driver, "/html/body/div[1]/div/button");
                var cpf = Driver.FindElement(By.Id("cpf"));
                ((IJavaScriptExecutor)Driver).ExecuteScript(string.Format("window.scrollTo({0}, {1})", cpf.Location.X, cpf.Location.Y - 100));

                if (error.Contains("Contrato possui parcela(s) vencida(s) e não paga(s).") == false)
                {
                    ClicarMenuAditamento(Driver);
                }
            }
            return error;
        }
        private string VerificarAlertaAditamento()
        {
            string alertMessage = "";
            if (Driver.PageSource.Contains("alert alert-warn"))
            {
                IWebElement temp = Driver.FindElement(By.ClassName("alert-warn"));
                alertMessage = temp.Text;
                Driver.FindElement(By.ClassName("close")).Click();
                alertMessage = alertMessage.Replace("x\r\n", "");
            }
            if (Driver.PageSource.Contains("modal-body"))
            {
                IWebElement temp = Driver.FindElement(By.ClassName("modal-body"));
                alertMessage = temp.Text;
                Driver.FindElement(By.ClassName("close")).Click();
                alertMessage = alertMessage.Replace("x\r\n", "");
            }

            return alertMessage;

        }
        public void SetDriver(IWebDriver driver)
        {
            Driver = driver;
        }
    }
}