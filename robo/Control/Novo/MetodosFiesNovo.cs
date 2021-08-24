using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using robo.Model.TO;
using robo.pgm;
using Robo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace robo.Control.Novo
{
    class MetodosFiesNovo
    {
        private static IWebDriver Driver;
        static string CampusAditado;
        static List<Aluno> alunos;
        private static string semestreAtual = string.Empty;
        static int qtdWindows = 1;
        static bool emLote = false;
        private static string IES;
        public static void OpenFiesNovo(List<TOLogin> logins, List<Aluno> alunosParametros, string tipoExecucao, string Semestre, bool buscarStatus, string parametroIES = "")
        {
            IES = parametroIES;
            semestreAtual = Semestre;
            alunos = alunosParametros;
            if (alunos.Count == 1)
            {
                emLote = false;
            }
            else
            {
                emLote = true;
            }

            try
            {
                Driver = Util.StartBrowser("http://sifesweb.caixa.gov.br");
                if (tipoExecucao.ToUpper().Equals("INADIMPLÊNCIA") || tipoExecucao.ToUpper().Equals("REPASSE") ||
                        tipoExecucao.ToUpper().Equals("COPARTICIPAÇÃO") || tipoExecucao.ToUpper().Equals("HISTÓRICO COPARTICIPAÇÃO") ||
                        tipoExecucao.ToUpper().Equals("VALIDAR REPARCELAMENTO"))
                {
                    OpenLogin(logins[0], true);
                }
                else
                {
                    OpenLogin(logins[0]);
                }

                if (buscarStatus == true)
                {
                    ClicarBotaoMenuPaginaInicial("btnAdmnstrcProcessodeFinanciamentoConsultarContratoEstudante");
                    for (int i = alunos.Count - 1; i >= 0; i--)
                    {
                        ConsultarAluno(alunos[i]);

                        if (Driver.PageSource.Contains("Tipo de Processo FIES") == true)
                        {
                            WaitForLoading();
                            Util.ClickButtonsById(Driver, "lnkTipoProcesso");
                            WaitForLoading();
                            Util.ClickButtonsById(Driver, "tab-Aditamento");
                            WaitForLoading();

                            ListaParaCSV("Tabela", "gridAditamento_length", "gridAditamento", false);
                        }
                    }
                }
                else
                {
                    WaitForLoading();
                    switch (tipoExecucao.ToUpper())
                    {
                        case "ADITAMENTO":
                            MetodoAditamento();
                            break;
                        case "BAIXAR DRM":
                            MetodoDRM();
                            break;
                        case "BAIXAR DRT":
                            //Está sendo usado?
                            MetodoDRT();
                            break;
                        case "SUSPENSÃO":
                            MetodoSuspensao();
                            break;
                        case "HISTÓRICO COPARTICIPAÇÃO":
                            break;
                        case "STATUS ALUNO":
                            break;
                        case "EXTRAIR INFORMAÇÕES DRM":
                            MetodoExtrairInfsDRM();
                            break;
                        case "BUSCAR STATUS ADITAMENTO":
                            buscarStatusAditamento();
                            break;
                        default:
                            throw new Exception("Tipo de execução não existe");
                    }
                }
            }
            catch (Exception e)
            {
                Util.getDriverDisposeAndDriverCloseWithThrow(e, Driver);
            }
            Driver.Close();
            Driver.Dispose();
        }

        private static void MetodoDRT()
        {
            ClicarBotaoMenuPaginaInicial("btnAdmnstrcManutenccedilatildeoAprovarTransferecircncia");
            foreach (TOAluno aluno in alunos)
            {
                ConsultarAluno(aluno);
            }
        }


        private static void buscarStatusAditamento()
        {
            ClicarBotaoMenuPaginaInicial("btnAdmnstrcManutenccedilatildeoAditamentoRenovaccedilatildeo");
            foreach (TOAluno aluno in alunos)
            {
                if (aluno.Conclusao.ToUpper() != "NÃO FEITO")
                {
                    continue;
                }
                ConsultarAluno(aluno);
                WaitForLoading();

                if (VerificarNenhumaInformacaoDisponivel() == true)
                {
                    aluno.Conclusao = "Nenhuma informação disponível";
                    aluno.HorarioConclusao = string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now);
                    Dados.UpdateAluno(aluno);
                    continue;
                }
                else
                {
                    string situacaoAluno = string.Empty;
                    IWebElement grid = Driver.FindElement(By.Id("gridResult"));
                    if (grid.Text.Contains(semestreAtual) == true)
                    {
                        situacaoAluno = BuscarSituacaoAluno();
                    }
                    else
                    {
                        situacaoAluno = "Semestre não encontrado.";
                    }
                    aluno.Conclusao = situacaoAluno;
                    aluno.HorarioConclusao = string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now);
                    Dados.UpdateAluno(aluno);
                }
            }
        }

        private static void MetodoSuspensao()
        {
            WaitForLoading();
            ClicarBotaoMenuPaginaInicial("btnAdmnstrcManutenccedilatildeoAprovarSuspensatildeo");
            foreach (TOAluno aluno in alunos)
            {
                ConsultarAluno(aluno);

                string janelaInicial = Driver.CurrentWindowHandle;

                if (VerificarNenhumaInformacaoDisponivel() == true)
                {
                    aluno.Conclusao = "Nenhuma informação disponível";
                    aluno.HorarioConclusao = string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now);
                    Dados.UpdateAluno(aluno);
                    return;
                }
                var elementoSelect = Driver.FindElement(By.Name("gridResult_length"));
                var selectElement = new SelectElement(elementoSelect);

                selectElement.SelectByValue("100");
                string semestreAno = semestreAtual.Replace("1/", "1º/");
                semestreAno = semestreAno.Replace("2/", "2º/");

                IWebElement element = Driver.FindElement(By.XPath($"//td[contains(text(), '{semestreAno}')]"));
                var elements = Driver.FindElements(By.TagName("tr"));
                for (int i = 0; i < elements.Count(); i++)
                {
                    
                }
                var find = element.FindElement(By.TagName("td"));
            }
        }

        //private static void MetodoSuspensao(TOAluno aluno, string nomePasta)
        //{
        //    if (Driver.PageSource.Contains("btnDetalhar") == true)
        //    {
        //        int linha = GetLinhaIndexMaior("º");
        //        IWebElement elementoLinha = Driver.FindElement(By.XPath(String.Format("//table[@id='gridResult']/tbody/tr[{0}]", linha)));
        //        IWebElement element = elementoLinha.FindElement(By.Id("btnDetalhar"));
        //        ((IJavaScriptExecutor)Driver).ExecuteScript(string.Format("window.scrollTo({0}, {1})", element.Location.X, element.Location.Y - 100));
        //        element.Click();
        //        WaitPageLoading("modal-backdrop fade in", true);
        //        WaitPageLoading("modal-backdrop fade", true);
        //        element = Driver.FindElement(By.Id("btnImprimirTermo"));
        //        ((IJavaScriptExecutor)Driver).ExecuteScript(string.Format("window.scrollTo({0}, {1})", element.Location.X, element.Location.Y - 100));
        //        element.Click();
        //        System.Threading.Thread.Sleep(1000);
        //        if (Driver.PageSource.Contains("btnConfirmar") == true)
        //        {
        //            element = Driver.FindElement(By.XPath("//a[contains(text(), 'OK')]"));
        //            element.Click();
        //        }


        //        string windowOriginal = Driver.CurrentWindowHandle;
        //        var executor = (IJavaScriptExecutor)Driver;

        //        while (Driver.WindowHandles.Last() == windowOriginal)
        //        {
        //            System.Threading.Thread.Sleep(100);
        //        }
        //        qtdWindows++;

        //        Driver.SwitchTo().Window(Driver.WindowHandles.Last());

        //        string fileName = aluno.Cpf + "_" + aluno.Cpf;
        //        string simplificado = string.Empty;
        //        SaveSite(fileName, windowOriginal, nomePasta);

        //        aluno.Conclusao = "Termo Suspensão Baixado";
        //        aluno.Extraido = "Sim";
        //    }
        //    else
        //    {
        //        throw new Exception("Documento não pode ser baixado");
        //    }
        //}

        private static void MetodoAditamento()
        {
            ClicarBotaoMenuPaginaInicial("btnAdmnstrcManutenccedilatildeoAditamentoRenovaccedilatildeo");
            foreach (TOAluno aluno in alunos)
            {
                if (aluno.Conclusao.ToUpper() != "NÃO FEITO")
                {
                    continue;
                }
                ConsultarAluno(aluno);
                WaitForLoading();

                if (VerificarNenhumaInformacaoDisponivel() == true)
                {
                    aluno.Conclusao = "Nenhuma informação disponível";
                    aluno.HorarioConclusao = string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now);
                    Dados.UpdateAluno(aluno);
                    continue;
                }

                if (Driver.PageSource.Contains("Não iniciado pela CPSA") == true)
                {
                    if (aluno.HistoricoAproveitamento.Contains("Encerrado"))
                    {
                        string temp = "aawdawdawd";
                    }
                    Util.ScrollToElementByID(Driver, "btnAditarEstudante");
                    Util.ClickButtonsById(Driver, "btnAditarEstudante");

                    WaitForLoading();
                    var executor = (IJavaScriptExecutor)Driver;

                    if (Driver.PageSource.ToUpper().Contains("ESTUDANTE TRANSFERIDO NO SEMESTRE") == true)
                    {
                        aluno.Conclusao = "Estudante transferido no semestre.";
                        Driver.FindElement(By.ClassName("btn-ok")).Click();

                        Util.ScrollToElementByID(Driver, "btnVoltar");
                        Driver.FindElement(By.Id("btnVoltar")).Click();

                        aluno.HorarioConclusao = DateTime.Now.ToString();
                        Dados.UpdateAluno(aluno);
                        Util.ScrollToElementByID(Driver, "btnVoltar");
                        Util.ClickButtonsById(Driver, "btnVoltar");
                        continue;
                    }

                    string erro = VerificarErroAditamento();
                    if (erro != string.Empty)
                    {
                        aluno.Conclusao = erro;
                        aluno.HorarioConclusao = string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now);
                        Dados.UpdateAluno(aluno);
                        Util.ScrollToElementByID(Driver, "btnVoltar");
                        Util.ClickButtonsById(Driver, "btnVoltar");
                        WaitForLoading();
                        ClicarBotaoMenuPaginaInicial("btnAdmnstrcManutenccedilatildeoAditamentoRenovaccedilatildeo");
                        continue;
                    }

                    string alerta = VerificarAlertaAditamento();
                    if (alerta != string.Empty)
                    {
                        aluno.Conclusao = alerta;
                        aluno.HorarioConclusao = string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now);
                        Dados.UpdateAluno(aluno);
                        Util.ScrollToElementByID(Driver, "btnVoltar");
                        Util.ClickButtonsById(Driver, "btnVoltar");
                        continue;
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

                    WaitForLoading();
                    string alertMessage = string.Empty;
                    //Verificação alerta percentual estudante
                    if (Driver.PageSource.Contains("MDLalerta_"))
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

                    //Prouni
                    string possuiProuni = Driver.FindElement(By.Id("prouni")).Text;

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
                        aluno.Conclusao = "Aproveitamento: " + aluno.AproveitamentoAtual;
                        aluno.HorarioConclusao = DateTime.Now.ToString();
                        Dados.UpdateAluno(aluno);
                        Util.ScrollToElementByID(Driver, "btnVoltar");
                        Util.ClickButtonsById(Driver, "btnVoltar");
                        continue;
                    }

                    string valorPagoRecursosEstudante = Driver.FindElement(By.Id("vlrPagoRecursoEstudante")).Text;
                    string valorPagoRecursosFIES = Driver.FindElement(By.Id("vlrPagoRecursoFinanciamento")).Text;

                    while (Driver.PageSource.Contains("btnConsultar") == false)
                    {
                        if (alertMessage == "")
                        {
                            Util.ScrollToElementByID(Driver, "btnConfirmar");
                            Util.ClickButtonsById(Driver, "btnConfirmar");
                            WaitForLoading();

                            if (Driver.PageSource.Contains("alert alert-error"))
                            {
                                IWebElement alertElement = Driver.FindElement(By.ClassName("alert-error"));
                                alertMessage = alertElement.Text;
                                alertMessage = alertMessage.Replace("x\r\n", "");

                                Util.ScrollToElementByID(Driver, "btnVoltar");
                                Util.ClickButtonsById(Driver, "btnVoltar");
                                break;
                            }
                            else
                            {
                                Util.ScrollToElementByID(Driver, "btnVoltar");
                                Util.ClickButtonsById(Driver, "btnVoltar");
                                break;
                            }

                        }
                        else
                        {
                            Util.ScrollToElementByID(Driver, "btnVoltar");
                            Util.ClickButtonsById(Driver, "btnVoltar");
                            break;
                        }
                    }
                    if (alertMessage != "")
                    {
                        aluno.Conclusao = alertMessage;
                    }
                    else
                    {
                        aluno.Conclusao = "Aditado com sucesso";
                    }
                    aluno.ValorAditadoFinanciamento = valorPagoRecursosFIES;
                    aluno.ValorPagoRecursoEstudante = valorPagoRecursosEstudante;
                    aluno.HorarioConclusao = DateTime.Now.ToString();
                    Dados.UpdateAluno(aluno);
                }
                else
                {
                    string situacaoAluno = string.Empty;
                    IWebElement grid = Driver.FindElement(By.Id("gridResult"));
                    if (grid.Text.Contains(semestreAtual) == true)
                    {
                        situacaoAluno = BuscarSituacaoAluno();
                    }
                    else
                    {
                        situacaoAluno = "Semestre não encontrado.";
                    }

                    aluno.Conclusao = situacaoAluno;
                    aluno.HorarioConclusao = string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now);
                    Dados.UpdateAluno(aluno);
                }
            }
        }

        private static void AproveitamentoMaiorDe75(TOAluno aluno, string possuiProuni)
        {
            Driver.FindElement(By.Id("aproveitamento75S")).Click();

            QuestionamentoPadrao(aluno, possuiProuni);
        }

        private static void QuestionamentoPadrao(TOAluno aluno, string possuiProuni)
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

        private static void AproveitamentoMenorDe75(TOAluno aluno, string possuiProuni)
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

        private static void BuscarEAbrirDRM(Aluno aluno)
        {
            ConsultarAluno(aluno);
            if (VerificarNenhumaInformacaoDisponivel() == true)
            {
                aluno.Conclusao = "Nenhuma informação disponível";
                aluno.HorarioConclusao = string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now);
                return;
            }

            IWebElement botaoImprimirTermo = BuscarBotaoSemestre();

            //Caso haja um botao com ano/semestre correto
            if (botaoImprimirTermo != null)
            {
                string janelaInicial = Driver.CurrentWindowHandle;
                bool msgErro = true;
                var executor = (IJavaScriptExecutor)Driver;

                //Buscar nome aluno
                string name = Driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/form/div[7]/fieldset/div/table/tbody/tr[1]/td[2]")).Text;
                aluno.Nome = name;

                //Verificacao de erro ao clicar no botao
                while (msgErro == true)
                {
                    Util.ScrollToElementByID(Driver, botaoImprimirTermo.GetAttribute("id"));
                    botaoImprimirTermo.Click();

                    System.Threading.Thread.Sleep(1000);

                    Util.ClickButtonsById(Driver, "btnConfirmar");

                    msgErro = (bool)executor.ExecuteScript("return $('.alert.alert-error').is(':visible');");
                    if (msgErro == true)
                    {
                        Util.ClickButtonsById(Driver, "btnConsultar");
                        WaitForLoading();
                    }
                }

                //Verificação em caso de alerta do site
                if (Driver.PageSource.Contains("MDLalerta_") == true)
                {
                    aluno.Conclusao = Driver.FindElement(By.XPath("/html/body/div[7]/div[2]/p")).Text;
                    Util.ClickButtonsById(Driver, "btnConfirmar");
                    return;
                }

                //Esperar a janela de DRM abrir
                while (Driver.WindowHandles.Last() == janelaInicial)
                {
                    System.Threading.Thread.Sleep(100);
                }
                Driver.SwitchTo().Window(Driver.WindowHandles.Last());
            }
            else //Caso não seja encontrado o ano/semestre desejado
            {
                string situacaoAluno = string.Empty;

                IWebElement grid = Driver.FindElement(By.Id("gridResult"));
                if (grid.Text.Contains(semestreAtual) == true)
                {
                    situacaoAluno = BuscarSituacaoAluno();
                }
                else
                {
                    situacaoAluno = "Semestre não encontrado.";
                }

                aluno.Conclusao = situacaoAluno;
            }
        }

        private static void MetodoDRM()
        {
            ClicarBotaoMenuPaginaInicial("btnAdmnstrcManutenccedilatildeoAditamentoRenovaccedilatildeo");
            foreach (TOAluno aluno in alunos)
            {
                string janelaInicial = Driver.CurrentWindowHandle;
                BuscarEAbrirDRM(aluno);

                if (aluno.Conclusao.ToUpper() != "NÃO FEITO")
                {
                    Dados.UpdateAluno((Aluno)aluno);
                    continue;
                }

                //Buscar se DRM é Simplificado ou Não Simplificado
                string simplificado = BuscarDRMSimplificado();

                string fileName = aluno.Nome + "_" + aluno.Cpf;
                string nomePasta = "DRM FIES Novo";

                SaveSite(fileName, janelaInicial, nomePasta);
                aluno.Conclusao = "DRM Baixado - " + simplificado;
                aluno.Extraido = "Sim";


                aluno.HorarioConclusao = string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now);
                Dados.UpdateAluno((Aluno)aluno);
            }
        }

        private static void MetodoExtrairInfsDRM()
        {
            ClicarBotaoMenuPaginaInicial("btnAdmnstrcManutenccedilatildeoAditamentoRenovaccedilatildeo");
            foreach (TOAlunoInf aluno in alunos)
            {
                string janelaInicial = Driver.CurrentWindowHandle;
                BuscarEAbrirDRM(aluno);

                if (aluno.Conclusao.ToUpper() != "NÃO FEITO")
                {
                    Dados.UpdateAluno((Aluno)aluno);
                    continue;
                }

                string informacao = SaveInfs(aluno.Cpf);
                Driver.Close();
                Driver.SwitchTo().Window(janelaInicial);
                ProcessarInfsFiesNovo(informacao, aluno);

                aluno.HorarioConclusao = string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now);
                Dados.UpdateAluno(aluno);
            }
        }

        private static bool VerificarNenhumaInformacaoDisponivel()
        {
            if (Driver.PageSource.Contains("Nenhuma informação disponível") == true)
            {
                return true;
            }
            return false;
        }

        private static string BuscarDRMSimplificado()
        {
            string informacao = Driver.PageSource;
            string simplificado = string.Empty;
            if (informacao.ToUpper().Contains("ADITAMENTO SIMPLIFICADO DE CONTRATO DE FINANCIAMENTO") == true)
            {
                simplificado = "Simplificado";
            }
            else
            {
                simplificado = "Não Simplificado";
            }

            return simplificado;
        }

        private static IWebElement BuscarBotaoSemestre()
        {
            IWebElement botaoImprimirTermo = null;
            string semestreFormatado = semestreAtual.Split('/')[0];
            string anoFormatado = semestreAtual.Split('/')[1];

            //Busca o botao que representa o ano/semestre correto
            if (Driver.PageSource.Contains(semestreAtual) == true)
            {
                var botoes = Driver.FindElements(By.Id("btnImprimirTermo"));

                foreach (var botao in botoes)
                {
                    string dataAno = botao.GetAttribute("data-ano");
                    string dataSemestre = botao.GetAttribute("data-semestre");
                    if (dataAno == anoFormatado && dataSemestre == semestreFormatado)
                    {
                        botaoImprimirTermo = botao;
                        return botao;
                    }
                }
            }
            return null;
        }

        private static string BuscarSituacaoAluno()
        {
            string situacaoAluno = string.Empty;
            situacaoAluno = Driver.PageSource.Split(new string[] { semestreAtual }, StringSplitOptions.None)[3];
            situacaoAluno = situacaoAluno.Split(new string[] { "td class=" }, StringSplitOptions.None)[1];
            situacaoAluno = situacaoAluno.Replace(">", "");
            situacaoAluno = situacaoAluno.Replace("<", "");
            situacaoAluno = situacaoAluno.Replace("\"", "");
            situacaoAluno = situacaoAluno.Replace("/td", "");

            return situacaoAluno;
        }

        public static void AbrirPaginaAditamento()
        {
            ClicarBotaoMenuPaginaInicial("btnAdmnstrcManutenccedilatildeoAditamentoRenovaccedilatildeo");
        }

        private static void ClicarBotaoMenuPaginaInicial(string idMenu, bool inadimplencia = false)
        {
            if (!Driver.PageSource.Contains("código autenticador"))
            {
                while (!Driver.PageSource.Contains("dropdown contratacao") || Driver.PageSource.Contains("modal-backdrop fade in") || Driver.PageSource.Contains("modal-backdrop fade"))
                {
                    System.Threading.Thread.Sleep(1000);
                }
                IWebElement sAdminMenuJobTitle = Driver.FindElement(By.XPath(string.Format("//*[@id=\"{0}\"]", idMenu)));
                ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", sAdminMenuJobTitle);
            }
            else
            {
                throw new Exception("Problema com código autenticador");
            }
        }

        static void ConsultarAluno(Aluno aluno)
        {
            WaitForLoading();
            WaitPageLoading("input-medium cpf", false);

            ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(0, 0)");

            Util.ClickButtonsByCss(Driver, "body");

            Util.ClickButtonsById(Driver, "btnLimpar");

            Util.ClickButtonsById(Driver, "cpf");

            var executor = (IJavaScriptExecutor)Driver;
            executor.ExecuteScript($@"document.getElementById(""cpf"").value = ""{aluno.Cpf}"";");

            Util.ClickButtonsById(Driver, "btnConsultar");

            WaitForLoading();
        }

        private static void WaitPageLoading(string element, bool exist)
        {
            while (Driver.PageSource.Contains(element) == exist)
            {
                System.Threading.Thread.Sleep(1000);
            }
        }

        private static void WaitForLoading()
        {
            WaitPageLoading("modal-backdrop fade in", true);
            WaitPageLoading("modal-backdrop fade", true);
        }

        public static void ProcessarInfsFiesNovo(string inf, TOAlunoInf aluno)
        {
            try
            {
                string depoisTitulo;
                List<string> infs = new List<string>();
                infs.Add(aluno.Cpf);
                if (inf.Contains("TERMO ADITIVO AO CONTRATO") == true)
                {
                    return;
                }
                depoisTitulo = inf.Split(new string[] { "Data da DRM:" }, StringSplitOptions.None)[1];
                string depoisNovaLinha = depoisTitulo.Split('\n')[1];
                infs.Add(depoisNovaLinha.Split(':')[1]);
                aluno.SemestreAditar = infs[infs.Count() - 1];

                depoisTitulo = inf.Split(new string[] { "\nCurso:" }, StringSplitOptions.None)[1];
                infs.Add(depoisTitulo.Split('\n')[0]);
                aluno.Curso = infs[infs.Count() - 1];

                depoisTitulo = inf.Split(new string[] { "Duração regular:" }, StringSplitOptions.None)[1];

                infs.Add(depoisTitulo.Split(new string[] { "Total de semestre(s) do financiamento:" }, StringSplitOptions.None)[0]);
                aluno.DuracaoRegular = infs[infs.Count() - 1];
                //aluno.financiam = infs[infs.Count() - 1];

                depoisTitulo = inf.Split(new string[] { "Total de semestres suspensos:" }, StringSplitOptions.None)[1];
                infs.Add(depoisTitulo.Split('\n')[0]);
                aluno.TotalDeSemestresSuspensos = infs[infs.Count() - 1];

                depoisTitulo = inf.Split(new string[] { "Total de semestres dilatados:" }, StringSplitOptions.None)[1];
                infs.Add(depoisTitulo.Split('\n')[0]);
                aluno.TotalDeSemestresDilatados = infs[infs.Count() - 1];

                depoisTitulo = inf.Split(new string[] { "Total de semestres já concluídos e/ou aproveitados nesta IES/curso:" }, StringSplitOptions.None)[1];
                infs.Add(depoisTitulo.Split('\n')[0]);
                aluno.TotalDeSemestresConcluidos = infs[infs.Count() - 1];

                depoisTitulo = inf.Split(new string[] { "Semestre a ser cursado pelo estudante:" }, StringSplitOptions.None)[1];
                infs.Add(depoisTitulo.Split('\n')[0]);
                aluno.SemestreSerCursadoPeloEstudante = infs[infs.Count() - 1];

                depoisTitulo = inf.Split(new string[] { "Total de semestre já financiados:" }, StringSplitOptions.None)[1];
                infs.Add(depoisTitulo.Split('\n')[0]);
                aluno.TotalDeSemestresJaFinanciados = infs[infs.Count() - 1];

                depoisTitulo = inf.Split(new string[] { "Percentual de financiamento solicitado:" }, StringSplitOptions.None)[1];
                infs.Add(depoisTitulo.Split('\n')[0]);
                aluno.PercentualDeFinanciamentoSolicitado = infs[infs.Count() - 1];

                if (inf.Contains("Grade Atual") == true) 
                {
                    depoisTitulo = inf.Split(new string[] { "Grade Atual" }, StringSplitOptions.None)[1];
                }
                else
                {
                    depoisTitulo = inf.Split(new string[] { "Grade atual" }, StringSplitOptions.None)[1];
                }
                infs.Add(depoisTitulo.Split('\n')[3]);
                aluno.GradeAtualComDesconto = infs[infs.Count() - 1];
                infs.Add(depoisTitulo.Split('\n')[5]);
                aluno.GradeAtualFinanciadoFIES = infs[infs.Count() - 1];
                infs.Add(depoisTitulo.Split('\n')[7]);
                aluno.GradeAtualCoparticipacao = infs[infs.Count() - 1];

                for (int i = 0; i < infs.Count; i++)
                {
                    infs[i] = infs[i].Replace("\r", string.Empty);
                }
                Util.AcertaBarraR(aluno);
                aluno.HorarioConclusao = DateTime.Now.ToString();
                aluno.Conclusao = "DRM Baixado";


            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Erro não esperado encontrado.\n Contate os alunos brilhantes.\n\n{0}", e.Message));
            }
        }

        static string SaveInfs(string filename)
        {
            string alltext = Driver.FindElement(By.TagName("body")).Text;

            return alltext;
        }

        private static void FazerLogin(TOLogin login)
        {
            Util.ClickAndWriteById(Driver, "username", login.User);
            Util.ClickButtonsById(Driver, "button-submit");
            Util.ClickAndWriteById(Driver, "password", login.Password);
            Util.ClickButtonsByCss(Driver, "button:nth-child(1)");
        }

        public static void OpenLogin(TOLogin login, bool loginAdmin = false)
        {
            List<TOLoginAdmin> temp = new List<TOLoginAdmin>();
            if (loginAdmin == true)
            {
                temp = Dados.SelectLoginAdmin();
            }
            foreach (var loginAdminItem in temp)
            {
                if (loginAdminItem.IES.ToUpper() != IES.ToUpper())
                {
                    continue;
                }
                login.Password = loginAdminItem.Senha;
                login.User = loginAdminItem.Usuario;
            }
            FazerLogin(login);

        }

        private static string VerificarAlertaAditamento()
        {
            string alertMessage = "";
            if (Driver.PageSource.Contains("alert alert-warn"))
            {
                IWebElement temp = Driver.FindElement(By.ClassName("alert-warn"));
                alertMessage = temp.Text;
                Driver.FindElement(By.ClassName("close")).Click();
                alertMessage = alertMessage.Replace("x\r\n", "");
            }
            return alertMessage;

        }

        private static string VerificarErroAditamento()
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
                    ClicarBotaoMenuPaginaInicial("btnAdmnstrcManutenccedilatildeoAditamentoRenovaccedilatildeo");
                }
            }
            return error;
        }

        private static void ListaParaCSV(string fileName, string idDropdown, string idTabela, bool status)
        {
            Util.ClickDropDown(Driver, "name", idDropdown, "100");
            IWebElement elementoTabela = Driver.FindElement(By.Id(idTabela));
            List<IWebElement> cabecalhos = elementoTabela.FindElements(By.TagName("th")).ToList();
            List<IWebElement> dados = elementoTabela.FindElements(By.TagName("td")).ToList();
            string arquivo;
            if (status == true)
            {
                String userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
                string downloadFolder = System.IO.Path.Combine(userRoot, "Downloads");
                arquivo = downloadFolder + "\\" + fileName + ".csv";
            }
            else
            {
                arquivo = fileName + ".csv";
            }
            if (File.Exists(arquivo))
            {
                File.Delete(arquivo);
            }
            for (int i = 0; i < cabecalhos.Count(); i++)
            {
                using (StreamWriter t = new StreamWriter(arquivo, true, UTF8Encoding.UTF8))
                {
                    if (i == cabecalhos.Count() - 1)
                    {
                        t.Write(cabecalhos[i].Text);
                        t.Write("\n");
                    }
                    else
                    {
                        t.Write(cabecalhos[i].Text + ";");
                    }
                }

            }
            int contador = 0;
            for (int i = 0; i < dados.Count(); i++)
            {
                using (StreamWriter t = new StreamWriter(arquivo, true, UTF8Encoding.UTF8))
                {
                    if (contador == cabecalhos.Count() - 1)
                    {
                        t.Write(dados[i].Text);
                        t.Write("\n");
                    }
                    else
                    {
                        t.Write(dados[i].Text + ";");
                    }
                }
                if (contador == cabecalhos.Count() - 1)
                {
                    contador = 0;
                }
                else
                {

                    contador++;
                }


            }
            if (status == true)
            {
                Process.Start(arquivo);
            }
        }

        private static void SaveSite(string fileName, string windowOriginal, string nomePasta)
        {
            string informacao = Driver.PageSource;
            Driver.Close();
            Driver.SwitchTo().Window(windowOriginal);

            //Criação das pastas necessárias
            Util.CreateDirectory("html");
            string downloadFolder = Util.GetDownloadsFolderPath();
            string diretorioDRM = downloadFolder + "\\" + nomePasta + "\\";
            Util.CreateDirectory(diretorioDRM);

            //Buscar texto do HTML
            string htmlDirectory = "html\\contrato.html";
            string[] temp = new string[2];
            if (informacao.Contains("<head>") == true)
            {
                temp = informacao.Split(new string[] { "<head>" }, StringSplitOptions.None);
                temp[0] += "<meta charset = \"utf-8\">";
            }
            informacao = temp[0] + temp[1];
            File.WriteAllText(htmlDirectory, informacao);

            string DataDirectory = diretorioDRM;
            DataDirectory += fileName;
            DataDirectory += "_DRM.pdf";

            //Chamada do arquivo em Python para salvar a página como pdf
            SaveHtmlAsPdf(htmlDirectory, DataDirectory);

            //Caso seja da versão CAE, copia para uma pasta local e abre automaticamente o pdf
            if (emLote == false)
            {
                Util.CreateDirectory("Temp");
                DirectoryInfo di = new DirectoryInfo("Temp");
                foreach (var item in di.GetFiles())
                {
                    item.Delete();
                }
                File.Copy(DataDirectory, "Temp\\" + fileName + ".pdf");
                Process.Start("Temp\\" + fileName + ".pdf");
            }
        }

        private static void SaveHtmlAsPdf(string htmlPath, string pdfFilePath)
        {
            string err = "";
            try
            {
                ProcessStartInfo processInfo = new ProcessStartInfo();
                processInfo.FileName = "HtmlToPdf.exe";
                processInfo.Arguments = $"\"{htmlPath}\" \"{pdfFilePath}\"";
                processInfo.UseShellExecute = false;
                processInfo.CreateNoWindow = true;
                processInfo.RedirectStandardOutput = true;
                processInfo.RedirectStandardError = true;
                string results = "";
                using (var process = Process.Start(processInfo))
                {
                    err = process.StandardError.ReadToEnd();
                    results = process.StandardOutput.ReadToEnd();
                }
            }
            catch
            {
                throw new Exception(err);
            }
        }
    }
}
