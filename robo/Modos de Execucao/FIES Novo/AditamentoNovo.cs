using OpenQA.Selenium;
using robo.TO;
using robo.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using robo.Contratos;
using robo.Excessoes;

namespace robo.Modos_de_Execucao.FIES_Novo
{
    public class AditamentoNovo : UtilFiesNovo, IModosDeExecucao.IModoComAlunos
    {
        private string IES;
        private string semestreAtual;
        public AditamentoNovo(string IES, string semestreAtual)
        {
            this.IES = IES;
            this.semestreAtual = semestreAtual;
        }
        public void AditamentoFiesNovo(TOAluno aluno)
        {
            ConsultarAluno(aluno);
            EsperarPaginaCarregando();
            if (Driver.PageSource.Contains("Não iniciado pela CPSA") == true)
            {
                ScrollParaElemento(By.Id("btnAditarEstudante"));
                ClicarElemento(By.Id("btnAditarEstudante"));
                EsperarAjax();
                EsperarPaginaCarregando();
                VerificarAditamentoNaoDisponivel(aluno);
                VerificarEstudanteTransferidoNoSemestre();
                VerificarAlertaAlunoTransferido(aluno);
                PreencherReceitaAluno(aluno);
                EsperarPaginaCarregando();
                string alertMessage = VerificarAlertaReceitaAluno(aluno, IES);
                PreencherQuestionamentoAluno(aluno);
                BuscarValoresAditamento(aluno);
                ConfirmarAditamento(aluno, alertMessage);
                ScrollParaElemento(By.Id("btnVoltar"));
                ClicarElemento(By.Id("btnVoltar"));
            }
            else
            {
                MarcarSituacaoAtualAluno(aluno, semestreAtual);
            }
        }

        private void BuscarValoresAditamento(TOAluno aluno)
        {
            aluno.ValorPagoRecursoEstudante = Driver.FindElement(By.Id("vlrPagoRecursoEstudante")).Text;
            aluno.ValorAditadoFinanciamento = Driver.FindElement(By.Id("vlrPagoRecursoFinanciamento")).Text;
        }
        private void ConfirmarAditamento(TOAluno aluno, string alertMessage)
        {
            if (alertMessage == "")
            {
                ScrollParaElemento(By.Id("btnConfirmar"));
                ClicarElemento(By.Id("btnConfirmar"));
                EsperarPaginaCarregando();

                alertMessage = BuscarMensagemDeErro();
            }

            if (alertMessage != "")
            {
                Util.EditarConclusaoAluno(aluno, alertMessage);
            }
            else
            {
                Util.EditarConclusaoAluno(aluno, "Aditado com sucesso");
            }
        }
        private void VerificarAlertaAlunoTransferido(TOAluno aluno)
        {
            string alerta = VerificarAlertaAditamento();
            if (alerta != string.Empty)
            {
                if (alerta.ToUpper().Contains("TRANSFERIDO NO SEMESTRE") == false)
                {
                    Util.EditarConclusaoAluno(aluno, alerta);
                    ScrollParaElemento(By.Id("btnVoltar"));
                    ClicarElemento(By.Id("btnVoltar"));
                }
            }
        }
        private void VerificarAditamentoNaoDisponivel(TOAluno aluno)
        {
            string erro = VerificarErroAditamento();
            if (erro != string.Empty)
            {
                Util.EditarConclusaoAluno(aluno, erro);
                ScrollParaElemento(By.Id("btnVoltar"));
                ClicarElemento(By.Id("btnVoltar"));
                EsperarPaginaCarregando();
                ClicarMenuAditamento();
                throw new PararExecucaoException();
            }
        }
        private void MarcarSituacaoAtualAluno(TOAluno aluno, string semestreAtual)
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
        private void PreencherQuestionamentoAluno(TOAluno aluno)
        {
            //Prouni
            string possuiProuni = Driver.FindElement(By.Id("prouni")).Text;
            ScrollParaElemento(By.Id("aproveitamento75S"));
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
                ScrollParaElemento(By.Id("btnVoltar"));
                ClicarElemento(By.Id("btnVoltar"));
                throw new PararExecucaoException();
            }
        }
        private string VerificarAlertaReceitaAluno(TOAluno aluno, string IES)
        {
            string alertMessage = string.Empty;
            if (Driver.PageSource.Contains("MDLalerta_"))
            {
                var elementosAlerta = Driver.FindElements(By.XPath("//div[contains(@id,\"MDLalerta_\")]"));
                if (elementosAlerta.Count == 1 && elementosAlerta[0].Displayed == true)
                {
                    if (Driver.PageSource.ToUpper().Contains("INFERIOR AO PERCENTUAL MÍNIMO DE SEMESTRALIDADE ATUAL") == false)
                    {
                        Driver.FindElement(By.ClassName("btn-ok")).Click();
                        EscreverJustificativaAluno(aluno, IES);
                        return string.Empty;
                    }

                    IWebElement alertElement = Driver.FindElement(By.XPath("//div[@class='modal hide fade in']/div[2]"));
                    alertMessage = alertElement.Text;
                    Driver.FindElement(By.ClassName("btn-ok")).Click();

                }
            }

            return alertMessage;
        }
        private void EscreverJustificativaAluno(TOAluno aluno, string IES)
        {
            if (IES.ToUpper() == "UNIRITTER" || IES.ToUpper() == "FADERGS")
            {
                if (aluno.Justificativa == string.Empty)
                {
                    ClicarEEscrever(By.Id("justificativaAcimaLimite"), "Valores conforme o número de créditos financeiros matriculados no semestre");
                }
                else
                {
                    ClicarEEscrever(By.Id("justificativaAcimaLimite"), aluno.Justificativa);
                }
            }
            else
            {
                ClicarEEscrever(By.Id("justificativaAcimaLimite"), "Alteração na grade curricular em relação ao semestre anterior");
            }
        }
        private void PreencherReceitaAluno(TOAluno aluno)
        {
            //Definição semestre atual
            ScrollParaElemento(By.Id("qtSemestreACursar"));
            int semestreASerCursado = Convert.ToInt32(Driver.FindElement(By.Id("totalSemestresFinanciados")).Text);
            ((IJavaScriptExecutor)Driver).ExecuteScript($@"document.getElementById(""qtSemestreACursar"").value = ""{semestreASerCursado + 1}"";");

            //Semestralidade Atual 
            ScrollParaElemento(By.Id("semestralidadeAtualComDescGradeASerCursada"));
            ClicarEEscrever(By.Id("semestralidadeAtualComDescGradeASerCursada"), aluno.ReceitaFies);

            //Click para atualizar a página
            Driver.FindElement(By.Id("semestralidadeAtualComDescGradeASerCursadaLabel")).Click();
            EsperarPaginaCarregando();

            if (Driver.PageSource.Contains("inferior ao valor mínimo"))
            {
                string alerta = VerificarAlertaAditamento();
                Util.EditarConclusaoAluno(aluno, alerta);
                ScrollParaElemento(By.Id("btnVoltar"));
                ClicarElemento(By.Id("btnVoltar"));
                throw new PararExecucaoException();
            }
        }
        private void VerificarEstudanteTransferidoNoSemestre()
        {
            if (Driver.PageSource.ToUpper().Contains("ESTUDANTE TRANSFERIDO NO SEMESTRE") == true)
            {
                Driver.FindElement(By.ClassName("btn-ok")).Click();
            }
        }
        private void EsperarAjax()
        {
            IWebElement ajax = Driver.FindElement(By.Id("ajaxStatus"));
            while (ajax.Displayed == true)
            {
                System.Threading.Thread.Sleep(1000);
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
            ScrollParaElemento(By.Id("duracaoCursoS"));

            Driver.FindElement(By.Id("duracaoCursoS")).Click();
            Driver.FindElement(By.Id("transferiuCursoN")).Click();

            //Se aparecer: 
            if (Driver.FindElement(By.Id("erroValidarTotSemestresN")).Displayed)
            {
                //Houve erro do estudante e da CPSA ao registrar e validar, respectivamente, o total de semestres j� conclu�dos do curso: N�O 
                ClicarElemento(By.Id("erroValidarTotSemestresN"));
            }
            if (aluno.DescontoLiberalidade.Equals("Sim") == true)
            {
                ScrollParaElemento(By.Id("descontoLiberalidadeS"));
                Driver.FindElement(By.Id("descontoLiberalidadeS")).Click();

                ScrollParaElemento(By.Id("motivoDesconto8"));
                ClicarElemento(By.Id("motivoDesconto8"));

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
                ClicarElemento(By.Id("aproveitamento75N"));
                aluno.Conclusao = "Rejeitou execesso de reprovação";
            }
            else
            {
                //A CPSA irá liberar o aditamento nesta situação? SIM  
                ScrollParaElemento(By.Id("aproveitamento75N"));
                ClicarElemento(By.Id("aproveitamento75N"));
                //Justificativa 
                ScrollParaElemento(By.Id("justificativa"));
                ClicarEEscrever(By.Id("justificativa"), aluno.HistoricoAproveitamento);


                ClicarElemento(By.Id("aproveitamentoInferior75S"));

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
                ClicarElemento(By.XPath("/html/body/div[1]/div/button"));
                var cpf = Driver.FindElement(By.Id("cpf"));
                ((IJavaScriptExecutor)Driver).ExecuteScript(string.Format("window.scrollTo({0}, {1})", cpf.Location.X, cpf.Location.Y - 100));

                if (error.Contains("Contrato possui parcela(s) vencida(s) e não paga(s).") == false)
                {
                    ClicarMenuAditamento();
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
        public void Executar(TOAluno aluno)
        {
            AditamentoFiesNovo(aluno);
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