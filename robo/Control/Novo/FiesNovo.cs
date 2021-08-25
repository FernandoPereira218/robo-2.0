using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using robo;
using robo.pgm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Robo
{
    public static class FiesNovo
    {
        static IWebDriver Driver;
        static string CampusAditado;
        static List<TOAluno> alunos;
        private static string semestreAtual = string.Empty;
        static int qtdWindows = 1;
        static bool emLote = false;
        private static string IES;
        public static void OpenFiesNovo(List<TOLogin> logins, List<TOAluno> alunosParametros, string tipoExecucao, string Semestre, bool buscarStatus, string parametroIES = "")
        {
            IES = parametroIES;
            semestreAtual = Semestre;
            alunos = alunosParametros;
            Driver = Util.StartBrowser("http://sifesweb.caixa.gov.br");
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

                foreach (TOLogin login in logins)
                {
                    OpenLogin(login);

                    if (buscarStatus == true)
                    {

                        //OpenLogin(login, "btnAdmnstrcProcessodeFinanciamentoConsultarContratoEstudante", buscarStatus);
                        ClicarBotaoMenu("btnAdmnstrcProcessodeFinanciamentoConsultarContratoEstudante");
                        for (int i = alunos.Count - 1; i >= 0; i--)
                        {
                            WaitForLoading();
                            WaitPageLoading("btnConfirmar", false);
                            ((IJavaScriptExecutor)Driver).ExecuteScript($@"document.getElementById(""cpf"").value = ""{alunos[i].Cpf}"";");
                            Util.ClickButtonsById(Driver, "btnConfirmar");

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
                        switch (tipoExecucao.ToUpper())
                        {
                            case "ADITAMENTO":
                                MetodoAditamento();
                                break;
                            case "DRM":
                                MetodoDRM(login);
                                break;
                            case "DRD":
                                MetodoDRD();
                                break;
                            case "DRT":
                                MetodoDRT();
                                break;
                            case "SUSPENSÃO":
                                MetodoSuspensao();
                                break;
                            case "HISTÓRICO COPARTICIPAÇÃO":
                                ClicarBotaoMenu("btnAdmnstrcReparcelamentodaCoparticipaccedilatildeoHistoacutericodoReparcelamentodaCoparticipaccedilatildeo");
                                WaitForLoading();
                                for (int i = alunos.Count() - 1; i >= 0; i--)
                                {
                                    ((IJavaScriptExecutor)Driver).ExecuteScript($@"document.getElementById(""cpfEstudante"").value = ""{alunos[i].Cpf}"";");
                                    Util.ClickButtonsById(Driver, "btnConsultar");
                                    WaitForLoading();
                                    string nome = Driver.FindElement(By.XPath("//*[@id=\"gridResult\"]/tbody/tr[1]/td[2]")).Text;
                                    if (Driver.PageSource.Contains("Nenhuma informação disponível") == false)
                                    {
                                        ListaParaCSV(nome + "_Histórico_Coparticipação", "gridResult_length", "gridResult", true);
                                    }
                                }
                                break;
                            case "STATUS ALUNO":
                                ConsultaStatusAlunoNovo(Semestre);
                                break;
                            default:
                                throw new Exception("Tipo de execução não existe");
                                break;
                        }
                    }
                    //else if (tipoExecucao.ToUpper().Equals("ADITAMENTO") == true)
                    //{
                    //    //TryToOpenAluno(login, alunos); 
                    //    //OpenLogin(login, "btnAdmnstrcManutenccedilatildeoAditamentoRenovaccedilatildeo");
                    //    MetodoAditamento();
                    //}
                    //else if (tipoExecucao.ToUpper().Equals("DRM") == true)
                    //{
                    //    MetodoDRM(login);
                    //}
                    //else if (tipoExecucao.ToUpper().Equals("DRD") == true)
                    //{
                    //    MetodoDRD();
                    //}
                    //else if (tipoExecucao.ToUpper().Equals("DRT") == true)
                    //{
                    //    //OpenLogin(login, "btnAdmnstrcManutenccedilatildeoAprovarTransferecircncia");
                    //    MetodoDRT();
                    //}
                    //else if (tipoExecucao.ToUpper().Equals("SUSPENSÃO") == true)
                    //{
                    //    // OpenLogin(login, "btnAdmnstrcManutenccedilatildeoAprovarSuspensatildeo");
                    //    MetodoSuspensao();
                    //}
                }
            }
            catch (Exception e)
            {
                Util.getDriverDisposeAndDriverCloseWithThrow(e, Driver);
            }
            Driver.Close();
            Driver.Dispose();
        }

        private static void ConsultaStatusAlunoNovo(string Semestre)
        {
            ClicarBotaoMenu("btnAdmnstrcProcessodeFinanciamentoConsultarContratoEstudante");
            WaitForLoading();
            for (int i = alunos.Count() - 1; i >= 0; i--)
            {
                ((IJavaScriptExecutor)Driver).ExecuteScript($@"document.getElementById(""cpf"").value = ""{alunos[i].Cpf}"";");
                Util.ClickButtonsById(Driver, "btnConfirmar");
                WaitForLoading();
                Util.ClickButtonsById(Driver, "lnkTipoProcesso");
                WaitForLoading();
                // while (Driver.PageSource.Contains("Aditamento") == false)
                // {
                //     System.Threading.Thread.Sleep(1000);
                // }
                WaitPageLoading("tab-Aditamento", false);
                //Util.ClickButtonsById(Driver, "tab-Aditamento");
                ClickButtonByJavaScript("tab-Aditamento");
                WaitForLoading();
                IWebElement elementoTabela = Driver.FindElement(By.Id("gridAditamento"));
                List<IWebElement> dados = elementoTabela.FindElements(By.TagName("td")).ToList();
                TOAluno aluno = new TOAluno();

                aluno.Cpf = alunos[i].Cpf;
                for (int j = 0; j < dados.Count(); j++)
                {
                    if (dados[j].Text == Semestre)
                    {
                        aluno.SemestreAno = dados[j].Text;
                        aluno.Finalidade = dados[j + 1].Text;
                        aluno.Situacao = dados[j + 2].Text;
                        aluno.Tipo = dados[j + 3].Text;
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
                    Dados.InsertAluno(aluno);

                    alunos[i].Conclusao = "Status Atualizado";
                    alunos[i].HorarioConclusao = string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now);
                    // Dados.UpdateAluno(alunos[i]);
                }
                else
                {
                    aluno.SemestreAno = "N/A";
                    aluno.Finalidade = "N/A";
                    aluno.Situacao = "Semestre não encontrado";
                    aluno.Tipo = "N/A";
                    aluno.ProUni = "N/A";
                    aluno.DataInclusao = "N/A";
                    aluno.DataConclusao = "N/A";
                    aluno.HorarioConclusao = string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now);
                    Dados.InsertAluno(aluno);
                    alunos[i].Conclusao = "Semestre não encontrado";
                    alunos[i].HorarioConclusao = string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now);
                    // Dados.UpdateAluno(alunos[i]);
                }


                var element = Driver.FindElement(By.Id("btn-voltar"));
                ((IJavaScriptExecutor)Driver).ExecuteScript(string.Format("window.scrollTo({0}, {1})", element.Location.X, element.Location.Y - 100));
                Util.ClickButtonsById(Driver, "btn-voltar");
                WaitForLoading();
            }
            ExportarAlunosConsultaNovo();
            //Dados.DeleteTodosAlunosConsultaNovo();
        }

        private static string CorrigirSemestreAlunoConsultaNovo(string semestre)
        {
            semestre = semestre.Replace("1/", "1º/");
            semestre = semestre.Replace("2/", "2º/");

            return semestre;
        }

        private static void ExportarAlunosConsultaNovo()
        {
            List<TOAluno> list = new List<TOAluno>();
            list = Dados.SelectAlunos();
            String userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadFolder = System.IO.Path.Combine(userRoot, "Downloads");
            string arquivo = downloadFolder + "\\Consulta_FIES_Novo.csv";
            Dictionary<string, string> columnAndProperty = new Dictionary<string, string>();
            columnAndProperty.Add("Cpf", "Cpf");
            columnAndProperty.Add("SemestreAno", "SemestreAno");
            columnAndProperty.Add("Finalidade", "Finalidade");
            columnAndProperty.Add("Situacao", "Situacao");
            columnAndProperty.Add("Tipo", "Tipo");
            columnAndProperty.Add("ProUni", "ProUni");
            columnAndProperty.Add("DataInclusao", "DataInclusao");
            columnAndProperty.Add("DataConclusao", "DataConclusao");
            columnAndProperty.Add("HorarioConclusao", "HorarioConclusao");
            CSVManager.CSVManager.ExportCSV(downloadFolder, "Consulta_FIES_Novo.csv", columnAndProperty, list);
        }

        private static void MetodoSuspensao()
        {
            ClicarBotaoMenu("btnAdmnstrcManutenccedilatildeoAprovarSuspensatildeo");
            for (int i = alunos.Count() - 1; i >= 0; i--)
            {
                ConsultarAlunoAditamento(alunos[i]);
                MetodoSuspensao(i, "Suspensão Novo");
                alunos.RemoveAt(i);
            }
            Logout();
        }

        private static void MetodoDRT()
        {
            ClicarBotaoMenu("btnAdmnstrcManutenccedilatildeoAprovarTransferecircncia");
            for (int i = alunos.Count() - 1; i >= 0; i--)
            {
                ConsultarAlunoAditamento(alunos[i]);
                MetodoSuspensao(i, "DRT Novo");
                alunos.RemoveAt(i);
            }
        }

        private static void MetodoDRD()
        {
            ClicarBotaoMenu("btnAdmnstrcManutenccedilatildeoAprovarDilataccedilatildeo");
            for (int i = alunos.Count() - 1; i >= 0; i--)
            {
                ConsultarAlunoAditamento(alunos[i]);
                alunos.RemoveAt(i);
            }
        }

        private static void MetodoDRM(TOLogin login)
        {
            ClicarBotaoMenu("btnAdmnstrcManutenccedilatildeoAditamentoRenovaccedilatildeo");
            for (int i = alunos.Count - 1; i >= 0; i--)
            {
                // if (BaixarDRM(alunos[i]))
                //{
                bool teste = BaixarDRM(alunos[i]);
                alunos[i].CampusAditado = login.Campus;
                alunos[i].HorarioConclusao = string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now);
                // Dados.UpdateAluno(alunos[i]);
                alunos.RemoveAt(i);
                //}
            }
        }

        private static void MetodoAditamento()
        {
            ClicarBotaoMenu("btnAdmnstrcManutenccedilatildeoAditamentoRenovaccedilatildeo");
            for (int i = alunos.Count - 1; i >= 0; i--)
            {
                ConsultarAlunoAditamento(alunos[i]);
                WaitForLoading();
                if (Driver.PageSource.Contains("Não iniciado pela CPSA") == true)
                {
                    System.Threading.Thread.Sleep(1000);
                    ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight - 1)");
                    //var btnConsultar = Driver.FindElement(By.Id("btnConsultar")); 
                    Util.ClickButtonsById(Driver, "btnAditarEstudante");
                    AditamentoNovo(alunos[i]);
                }
                else
                {
                    string situacaoAluno = string.Empty;
                    if (Driver.PageSource.Contains("Nenhuma informação disponível") == true)
                    {
                        situacaoAluno = "Nenhuma informação disponível";
                    }
                    else if (Driver.PageSource.Contains(semestreAtual) == true)
                    {
                        situacaoAluno = Driver.PageSource.Split(new string[] { semestreAtual }, StringSplitOptions.None)[3];
                        situacaoAluno = situacaoAluno.Split(new string[] { "td class=" }, StringSplitOptions.None)[1];
                        situacaoAluno = situacaoAluno.Replace(">", "");
                        situacaoAluno = situacaoAluno.Replace("<", "");
                        situacaoAluno = situacaoAluno.Replace("\"", "");
                        situacaoAluno = situacaoAluno.Replace("/td", "");
                    }

                    EditarConclusaoAluno(alunos[i], situacaoAluno);
                    Util.ClickButtonsById(Driver, "btnLimpar");

                }
            }
        }

        private static void WaitForLoading()
        {
            WaitPageLoading("modal-backdrop fade in", true);
            WaitPageLoading("modal-backdrop fade", true);
        }

        private static void ClicarBotaoMenu(string idMenu, bool inadimplencia = false)
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
                //Problema de código autenticador 
                //CampusAditado = "Problema Codigo autentidor"; 
                //Util.ClickButtonsById(Driver, "kc-cancel"); 
                throw new Exception("Problema com código autenticador");
            }
        }

        private static void ClickButtonByJavaScript(string id)
        {
            IWebElement element = Driver.FindElement(By.XPath(string.Format("//*[@id=\"{0}\"]", id)));
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", element);
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

        private static void MetodoSuspensao(int i, string nomePasta)
        {
            if (Driver.PageSource.Contains("btnDetalhar") == true)
            {
                int linha = GetLinhaIndexMaior("º");
                IWebElement elementoLinha = Driver.FindElement(By.XPath(String.Format("//table[@id='gridResult']/tbody/tr[{0}]", linha)));
                IWebElement element = elementoLinha.FindElement(By.Id("btnDetalhar"));
                ((IJavaScriptExecutor)Driver).ExecuteScript(string.Format("window.scrollTo({0}, {1})", element.Location.X, element.Location.Y - 100));
                element.Click();
                WaitPageLoading("modal-backdrop fade in", true);
                WaitPageLoading("modal-backdrop fade", true);
                element = Driver.FindElement(By.Id("btnImprimirTermo"));
                ((IJavaScriptExecutor)Driver).ExecuteScript(string.Format("window.scrollTo({0}, {1})", element.Location.X, element.Location.Y - 100));
                element.Click();
                System.Threading.Thread.Sleep(1000);
                if (Driver.PageSource.Contains("btnConfirmar") == true)
                {
                    element = Driver.FindElement(By.XPath("//a[contains(text(), 'OK')]"));
                    element.Click();
                }


                string windowOriginal = Driver.CurrentWindowHandle;
                var executor = (IJavaScriptExecutor)Driver;

                while (Driver.WindowHandles.Last() == windowOriginal)
                {
                    System.Threading.Thread.Sleep(100);
                }
                qtdWindows++;

                Driver.SwitchTo().Window(Driver.WindowHandles.Last());

                string fileName = alunos[i].Cpf + "_" + alunos[i].Cpf;
                string simplificado = string.Empty;
                SaveSite(fileName, windowOriginal, nomePasta, ref simplificado);

                alunos[i].Conclusao = "Termo Suspensão Baixado";
                alunos[i].Extraido = "Sim";
            }
            else
            {
                throw new Exception("Documento não pode ser baixado");
            }
        }

        private static void ConsultarAlunoAditamento(TOAluno aluno)
        {
            WaitPageLoading("cpf", false);
            IWebElement element = Driver.FindElement(By.Id("cpf"));
            ((IJavaScriptExecutor)Driver).ExecuteScript(string.Format("window.scrollTo({0}, {1})", element.Location.X, element.Location.Y - 100));
            WaitPageLoading("modal-backdrop fade in", true);
            WaitPageLoading("modal-backdrop fade", true);
            Util.ClickButtonsById(Driver, "cpf");
            var executor = (IJavaScriptExecutor)Driver;
            executor.ExecuteScript($@"document.getElementById(""cpf"").value = ""{aluno.Cpf}"";");
            Util.ClickButtonsById(Driver, "btnConsultar");
            WaitPageLoading("modal-backdrop fade in", true);
            WaitPageLoading("modal-backdrop fade", true);
        }

        public static void AditamentoNovo(TOAluno aluno)
        {
            WaitPageLoading("modal-backdrop fade in", true);
            WaitPageLoading("modal-backdrop fade", true);
            string alertMessage = "";
            if (Driver.PageSource.Contains("alert alert-warn"))
            {
                IWebElement temp = Driver.FindElement(By.ClassName("alert-warn"));
                alertMessage = temp.Text;
                Driver.FindElement(By.ClassName("close")).Click();
                alertMessage = alertMessage.Replace("x\r\n", "");
                System.Threading.Thread.Sleep(1000);
            }
            var executor = (IJavaScriptExecutor)Driver;

            if (Driver.PageSource.Contains("alert-error"))
            {
                var errorMsg = Driver.FindElement(By.XPath("/html/body/div[1]/div"));
                string error = errorMsg.Text;
                error = error.Replace("x\r\n", "");
                aluno.Conclusao = error;
                //  Dados.UpdateAluno(aluno);
                Util.ClickButtonsByXpath(Driver, "/html/body/div[1]/div/button");
                var cpf = Driver.FindElement(By.Id("cpf"));
                ((IJavaScriptExecutor)Driver).ExecuteScript(string.Format("window.scrollTo({0}, {1})", cpf.Location.X, cpf.Location.Y - 100));
                if (error.Contains("Contrato possui parcela(s) vencida(s) e não paga(s).") == false)
                {
                    ClicarBotaoMenu("btnAdmnstrcManutenccedilatildeoAditamentoRenovaccedilatildeo");
                }
                return;
            }
            //Semestre A Ser Cursado 
            var element = Driver.FindElement(By.Id("qtSemestreACursar"));
            ((IJavaScriptExecutor)Driver).ExecuteScript(string.Format("window.scrollTo({0}, {1})", element.Location.X, element.Location.Y - 100));
            int semestreASerCursado = Convert.ToInt32(Driver.FindElement(By.Id("totalSemestresFinanciados")).Text);
            executor.ExecuteScript($@"document.getElementById(""qtSemestreACursar"").value = ""{semestreASerCursado + 1}"";");

            System.Threading.Thread.Sleep(1000);

            //Semestralidade Atual 
            element = Driver.FindElement(By.Id("semestralidadeAtualComDescGradeASerCursada"));
            ((IJavaScriptExecutor)Driver).ExecuteScript(string.Format("window.scrollTo({0}, {1})", element.Location.X, element.Location.Y - 100));
            Util.ClickAndWriteById(Driver, "semestralidadeAtualComDescGradeASerCursada", aluno.ReceitaFies);
            Driver.FindElement(By.Id("semestralidadeAtualComDescGradeASerCursadaLabel")).Click();


            WaitPageLoading("modal-backdrop fade in", true);
            WaitPageLoading("modal-backdrop fade", true);
            if (Driver.PageSource.Contains("MDLalerta_"))
            {
                //((IJavaScriptExecutor)Driver).ExecuteScript("document.querySelector('btn btn-primary btn-ok').click();");
                if (Driver.PageSource.Contains("Estudante transferido no semestre.") == true)
                {
                    aluno.Conclusao = "Estudante transferido no semestre.";
                    Driver.FindElement(By.ClassName("btn-ok")).Click();
                    element = Driver.FindElement(By.Id("btnVoltar"));
                    ((IJavaScriptExecutor)Driver).ExecuteScript(string.Format("window.scrollTo({0}, {1})", element.Location.X, element.Location.Y - 100));
                    Driver.FindElement(By.Id("btnVoltar")).Click();
                    //aluno.ValorAditadoFinanciamento = valorPagoRecursosFIES;
                    //aluno.ValorPagoRecursoEstudante = valorPagoRecursosEstudante;
                    aluno.HorarioConclusao = DateTime.Now.ToString();
                    // Dados.UpdateAluno(aluno);
                    return;
                }
                if (Driver.PageSource.Contains("inferior ao Percentual mínimo de semestralidade atual") == false)
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
            var prouniElement = Driver.FindElement(By.XPath("//label[contains(text(), 'ProUni')]"));
            string possuiProuni = prouniElement.FindElement(By.XPath("..")).Text;

            //Scroll até checkbox 75% 
            element = Driver.FindElement(By.Id("aproveitamento75S"));
            ((IJavaScriptExecutor)Driver).ExecuteScript(string.Format("window.scrollTo({0}, {1})", element.Location.X, element.Location.Y - 100));
            if (aluno.AproveitamentoAtual.Contains("Superior a 75%") == true)
            {
                element = AproveitamentoMaiorDe75(aluno, executor, possuiProuni);
            }
            else
            {
                AproveitamentoMenorDe75(aluno);
            }

            string valorPagoRecursosEstudante = Driver.FindElement(By.Id("vlrPagoRecursoEstudante")).Text;
            string valorPagoRecursosFIES = Driver.FindElement(By.Id("vlrPagoRecursoFinanciamento")).Text;

            //ReadOnlyCollection<IWebElement> alertObject = (ReadOnlyCollection<IWebElement>)executor.ExecuteScript("return document.getElementsByClassName(\"modal hide fade in\");");


            //WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(60));
            //wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("btnConsultar")));
            while (Driver.PageSource.Contains("btnConsultar") == false)
            {
                System.Threading.Thread.Sleep(100);
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
            // Dados.UpdateAluno(aluno);
            //wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("container-fluid msg-fixed-top")));
            //wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("alert alert-error")));
            //bool errorMessage = Convert.ToBoolean(executor.ExecuteScript($@"document.getElementsByClassName(""alert.alert-error"");"));
            //IWebElement teste = (IWebElement)executor.ExecuteScript("return document.getElementById(\"motivoDesconto8\");");
            //IWebElement teste2 = (IWebElement)executor.ExecuteScript("return document.getElementById(\"aaaaaaaaaa\");");
            //var teste3 = executor.ExecuteScript("return document.getElementsByClassName(\"alert-error\");");

            //element = Driver.FindElement(By.Id("btnVoltar")); 
            //((IJavaScriptExecutor)Driver).ExecuteScript(string.Format("window.scrollTo({0}, {1})", element.Location.X, element.Location.Y - 100)); 
        }

        static void EditarConclusaoAluno(TOAluno aluno, string conclusao)
        {
            aluno.Conclusao = conclusao;
            aluno.HorarioConclusao = string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now);

            // Dados.UpdateAluno(aluno);
        }

        private static IWebElement AproveitamentoMaiorDe75(TOAluno aluno, IJavaScriptExecutor executor, string possuiProuni)
        {
            IWebElement element;
            Driver.FindElement(By.Id("aproveitamento75S")).Click();
            Driver.FindElement(By.Id("regularmenteMatriculadoS")).Click();
            if (possuiProuni.Contains("Sim") == true)
            {
                Driver.FindElement(By.Id("beneficioSimultaneoS")).Click();
            }
            else
            {
                Driver.FindElement(By.Id("beneficioSimultaneoN")).Click();
            }
            element = Driver.FindElement(By.Id("duracaoCursoS"));
            ((IJavaScriptExecutor)Driver).ExecuteScript(string.Format("window.scrollTo({0}, {1})", element.Location.X, element.Location.Y - 100));

            Driver.FindElement(By.Id("duracaoCursoS")).Click();
            Driver.FindElement(By.Id("transferiuCursoN")).Click();
            //Se aparecer: 
            if (Driver.FindElement(By.Id("erroValidarTotSemestresN")).Displayed)
            {//Houve erro do estudante e da CPSA ao registrar e validar, respectivamente, o total de semestres j� conclu�dos do curso: N�O 
                //System.Threading.Thread.Sleep(1000);
                Util.ClickButtonsById(Driver, "erroValidarTotSemestresN");
            }
            if (aluno.DescontoLiberalidade.Equals("Sim") == true)
            {
                element = Driver.FindElement(By.Id("descontoLiberalidadeS"));
                ((IJavaScriptExecutor)Driver).ExecuteScript(string.Format("window.scrollTo({0}, {1})", element.Location.X, element.Location.Y - 100));

                Driver.FindElement(By.Id("descontoLiberalidadeS")).Click();

                element = Driver.FindElement(By.Id("motivoDesconto8"));
                ((IJavaScriptExecutor)Driver).ExecuteScript(string.Format("window.scrollTo({0}, {1})", element.Location.X, element.Location.Y - 100));
                Util.ClickButtonsById(Driver, "motivoDesconto8");
                executor.ExecuteScript($@"document.getElementById(""justificativaDesconto"").value = ""Desconto Semestral"";");
            }
            else
            {
                Driver.FindElement(By.Id("descontoLiberalidadeN")).Click();
            }

            return element;
        }

        private static void AproveitamentoMenorDe75(TOAluno aluno)
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
                //A CPSA ir� liberar o aditamento nesta situa��o? SIM  
                Util.ClickButtonsById(Driver, "aproveitamento75N");
                ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollBy(0,50)");

                //Justificativa 
                Util.ClickAndWriteById(Driver, "justificativa", aluno.HistoricoAproveitamento);
                ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollBy(0,50)");
                Util.ClickButtonsById(Driver, "aproveitamentoInferior75S");

                //O estudante est� regularmente matriculado? SIM
                var element = Driver.FindElement(By.Id("regularmenteMatriculadoS"));
                ((IJavaScriptExecutor)Driver).ExecuteScript(string.Format("window.scrollTo({0}, {1})", element.Location.X, element.Location.Y - 100));
                Util.ClickButtonsById(Driver, "regularmenteMatriculadoS");
                ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollBy(0,50)");

                //O estudante possui benef�cio simult�neo de FIES e de bolsa ProUni em local de oferta ou curso distinto? NAO 
                Util.ClickButtonsById(Driver, "beneficioSimultaneoN");
                ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollBy(0,50)");

                //O  prazo de dura��o regular do curso encontra-se vigente? SIM 
                Util.ClickButtonsById(Driver, "duracaoCursoS");
                ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollBy(0,50)");

                //O estudante transferiu de curso mais de uma vez nessa IES? N�O 
                Util.ClickButtonsById(Driver, "transferiuCursoN");
                ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollBy(0,50)");

                //Houve erro do estudante e da CPSA ao registrar e validar... (Novo) 
                if (Driver.FindElement(By.Id("erroValidarTotSemestresN")).Displayed)
                {//Houve erro do estudante e da CPSA ao registrar e validar, respectivamente, o total de semestres j� conclu�dos do curso: N�O 
                 //System.Threading.Thread.Sleep(1000);
                    Util.ClickButtonsById(Driver, "erroValidarTotSemestresN");
                }

                //O estudante possui algum desconto por liberalidade da IES? N�O 
                if (aluno.DescontoLiberalidade.Equals("Sim") == true)
                {
                    var executor = (IJavaScriptExecutor)Driver;
                    element = Driver.FindElement(By.Id("descontoLiberalidadeS"));
                    ((IJavaScriptExecutor)Driver).ExecuteScript(string.Format("window.scrollTo({0}, {1})", element.Location.X, element.Location.Y - 100));
                    Driver.FindElement(By.Id("descontoLiberalidadeS")).Click();
                    element = Driver.FindElement(By.Id("motivoDesconto8"));
                    ((IJavaScriptExecutor)Driver).ExecuteScript(string.Format("window.scrollTo({0}, {1})", element.Location.X, element.Location.Y - 100));
                    Util.ClickButtonsById(Driver, "motivoDesconto8");
                    executor.ExecuteScript($@"document.getElementById(""justificativaDesconto"").value = ""Desconto Semestral"";");
                }
                else
                {
                    var executor = (IJavaScriptExecutor)Driver;
                    element = Driver.FindElement(By.Id("descontoLiberalidadeN"));
                    ((IJavaScriptExecutor)Driver).ExecuteScript(string.Format("window.scrollTo({0}, {1})", element.Location.X, element.Location.Y - 100));
                    Driver.FindElement(By.Id("descontoLiberalidadeN")).Click();
                }
                        ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollBy(0,50)");

                aluno.Conclusao = "Aditamento Realizado";
            }
        }
        /// <summary> 
        /// Realiza Login 
        /// </summary> 
        /// <param name="logins">Lista de Logins</param> 
        /// <returns></returns> 
        public static void OpenLogin(TOLogin login, string idMenu, bool buscarStatus = false)
        {
            FazerLogin(login);
            if (buscarStatus == false)
            {

                ClicarMenuAditamento(login, idMenu);
            }
            else
            {
                ClicarMenuConsultas(login, idMenu);
            }
        }

        public static void OpenLogin(TOLogin login, bool loginAdmin = false)
        {
            FazerLogin(login);

        }

        private static void ClicarMenuAditamento(TOLogin login, string idMenu)
        {
            if (!Driver.PageSource.Contains("código autenticador"))
            {

                while (!Driver.PageSource.Contains("dropdown contratacao") || Driver.PageSource.Contains("modal-backdrop fade in") || Driver.PageSource.Contains("modal-backdrop fade"))
                {
                    System.Threading.Thread.Sleep(1000);
                }
                Util.ClickButtonsByXpath(Driver, "//a[contains(text(),'Contrato FIES')]");
                Actions actions = new Actions(Driver);
                actions.MoveToElement(Driver.FindElement(By.CssSelector(".contratacao .dropdown-submenu > .links-menu"))).Perform();
                Util.ClickButtonsById(Driver, idMenu);
                CampusAditado = login.Campus;
            }
            else
            {
                //Problema de código autenticador 
                //CampusAditado = "Problema Codigo autentidor"; 
                //Util.ClickButtonsById(Driver, "kc-cancel"); 
                throw new Exception("Problema com código autenticador");
            }
        }

        private static void ClicarMenuConsultas(TOLogin login, string idMenu)
        {
            if (!Driver.PageSource.Contains("código autenticador"))
            {

                while (!Driver.PageSource.Contains("dropdown contratacao") || Driver.PageSource.Contains("modal-backdrop fade in") || Driver.PageSource.Contains("modal-backdrop fade"))
                {
                    System.Threading.Thread.Sleep(1000);
                }
                Util.ClickButtonsByXpath(Driver, "//a[contains(text(),'Consultas')]");
                Actions actions = new Actions(Driver);
                actions.MoveToElement(Driver.FindElement(By.CssSelector(".menuConsultas .dropdown-submenu > .links-menu"))).Perform();
                Util.ClickButtonsById(Driver, idMenu);
                CampusAditado = login.Campus;
            }
            else
            {
                //Problema de código autenticador 
                //CampusAditado = "Problema Codigo autentidor"; 
                //Util.ClickButtonsById(Driver, "kc-cancel"); 
                throw new Exception("Problema com código autenticador");
            }
        }

        private static void FazerLogin(TOLogin login)
        {
            Util.ClickAndWriteById(Driver, "username", login.Usuario);
            Util.ClickButtonsById(Driver, "button-submit");
            Util.ClickAndWriteById(Driver, "password", login.Senha);
            Util.ClickButtonsByCss(Driver, "button:nth-child(1)");
        }

        /// <summary> 
        /// Tenta editar e confere se já foi editado cada aluno 
        /// </summary> 
        /// <param name="logins"></param> 
        /// <param name="alunos"></param> 
        public static void TryToOpenAluno(TOLogin login, List<TOAluno> alunos)
        {
            for (int i = alunos.Count - 1; i >= 0; i--)
            {
                //se retornar falso ele vai deslogar do site 
                if (OpenAlunoSucess(alunos[i]) == false)
                {
                    //logar dnv 
                    //OpenLogin(login, "btnAdmnstrcManutenccedilatildeoAditamentoRenovaccedilatildeo");

                    //e tentar mais uma vez 
                    TryToOpenAluno(login, alunos);
                }
                else
                {
                    alunos[i].CampusAditado = CampusAditado;
                    alunos[i].HorarioConclusao = string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now);
                    //  Dados.UpdateAluno(alunos[i]);
                    alunos.RemoveAt(i);
                }
            }
        }

        /// <summary> 
        /// Coloca o cpf do aluno, verifica e tenta aditar o aluno 
        /// </summary> 
        /// <param name="aluno">TOAluno para aditamento</param> 
        static bool OpenAlunoSucess(TOAluno aluno)
        {
            ConsultarAluno(aluno);

            WaitPageLoading("Mostrando", false);
            WaitPageLoading("modal-backdrop fade in", true);
            WaitPageLoading("modal-backdrop fade", true);

            string listaText = Driver.FindElement(By.Id("gridResult_wrapper")).Text;

            string statusDoAluno = VerificarStatusDoAluno(listaText);

            WaitPageLoading("Mostrando", false);
            WaitPageLoading("modal-backdrop fade in", true);
            WaitPageLoading("modal-backdrop fade", true);

            return ExecutarStatusDoAluno(statusDoAluno, listaText, aluno);
        }

        static string VerificarStatusDoAluno(string listaText)
        {
            bool booleanaVerificadora = true;

            while (booleanaVerificadora)
            {
                booleanaVerificadora = false;
                try
                {
                    listaText = Driver.FindElement(By.Id("gridResult_wrapper")).Text;
                    if (listaText.Contains("Não iniciado pela CPSA") || listaText.Contains("Rejeitado pelo estudante") || listaText.Contains("Cancelado por decurso de prazo do banco"))
                    {
                        return "statusAditavel";
                    }
                    else if (listaText.Contains("Nenhuma informação disponível"))
                    {
                        return "alunoNaoEncontrado";
                    }
                    else if (Driver.PageSource.Contains("alert alert-error"))//texto do erro: Ocorreu um erro na consulta e não foi possível recuperar os dados solicitados. Por favor, consulte novamente! 
                    {
                        //RELOAD DA PAG 
                        //click na carinha 
                        Util.ClickButtonsByCss(Driver, ".dropdown:nth-child(2) > .dropdown-toggle");
                        //clique em sair 
                        Driver.FindElement(By.LinkText("Sair")).Click();

                        //click na carinha 
                        Util.ClickButtonsByCss(Driver, ".dropdown:nth-child(2) > .dropdown-toggle");
                        //clique em sair 
                        Driver.FindElement(By.LinkText("Sair")).Click();

                        return "false";
                    }
                    else if (!listaText.Contains("Contratado") && !listaText.Contains("Pendente de validacao"))
                    {
                        booleanaVerificadora = true;
                    }
                    else
                    {
                        //aluno não encontrado nesse semestre 
                        return "alunoNaoEncontrado";
                    }
                }
                catch (Exception)
                {
                    booleanaVerificadora = true;
                }
            }
            return string.Empty;
        }

        static bool ExecutarStatusDoAluno(string statusDoAluno, string listaText, TOAluno aluno)
        {
            switch (statusDoAluno)
            {
                case "false":
                    //clicar para deslogar??? 
                    return false;
                case "statusAditavel":
                    ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");

                    if (listaText.Contains("Cancelado por decurso de prazo do banco") && !listaText.Contains("btnAditarEstudante"))
                    {
                        aluno.Conclusao = "Aditamento Não Realizado por decurso de prazo do banco";
                        //alunos.Remove(aluno); 
                        return true;
                    }
                    //div fade in modalbackdrop 
                    Util.ClickButtonsById(Driver, "btnAditarEstudante");

                    //wait   ->esse wait pode demorar 
                    WaitPageLoading("Nome completo:", false);

                    FillForm(aluno);

                    SystemSounds.Beep.Play();

                    bool sucesso = false;
                    bool alerta = false;
                    bool alerta2 = false;
                    //wait até aparecer o ok 
                    while (!sucesso && !alerta && !alerta2)
                    {
                        sucesso = Driver.PageSource.Contains("Operação realizada com sucesso.");
                        alerta = Driver.PageSource.Contains("MDLalerta_");
                        alerta2 = Driver.PageSource.Contains("alert alert-error");
                        System.Threading.Thread.Sleep(1000);
                    }
                    //WaitPageLoading("Operação realizada com sucesso.", false); 

                    if (sucesso)
                    {
                        aluno.Conclusao = "Aditamento Realizado";
                        //alunos.Remove(aluno); 

                        ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");

                        //clicar em voltar 
                        Util.ClickButtonsById(Driver, "btnVoltar");

                        return true;
                        //verifica se deu erro 
                        //bool certo = false; 
                        //bool erroDemora = false; 
                        //while (!certo/* && !erroDemora*/) 
                        //{ 
                        //    //erroDemora = Driver.PageSource.Contains("O campo Valor da Semestralidade ATUAL COM desconto ultrapassou o Percentual máximo de semestralidade atual."); 
                        //    certo = Driver.PageSource.Contains("Operação realizada com sucesso."); 

                        //    System.Threading.Thread.Sleep(500); 
                        //} 
                        //if (certo) 
                        //{ 
                        //    return true; 
                        //    Dados.UpdateAluno(aluno); 
                        //    alunos.Remove(aluno); 
                        //} 
                        //else if(erroDemora) 
                        //{ 
                        //    MessageBox.Show("começar denovo"); 
                        //} 
                    }
                    else if (alerta)
                    {
                        if (Driver.PageSource.Contains("O campo Valor da Semestralidade ATUAL COM desconto ultrapassou o Percentual máximo de semestralidade atual."))
                        {
                            /* 
                            //erro dos 20% 
                            float valorTela = float.Parse(Driver.FindElement(By.Id("semestralidadeParaFIES")).GetAttribute("value")); 
                            Dados.TratarPorcentagemReceita(aluno, false, valorTela); 

                            */
                            //click na carinha 
                            aluno.Conclusao = "O campo Valor da Semestralidade ATUAL COM desconto ultrapassou o Percentual máximo de semestralidade atual.";
                            //Dados.UpdateAluno(aluno); 
                            //alunos.Remove(aluno); 

                            Util.ClickButtonsByCss(Driver, ".dropdown:nth-child(2) > .dropdown-toggle");
                            //clique em sair 
                            Driver.FindElement(By.LinkText("Sair")).Click();
                            return true;
                        }
                        else
                        {
                            //outros tipos de erros nao descobertos ainda 
                            aluno.Conclusao = "Novo erro encontrado";
                            //Dados.UpdateAluno(aluno); 
                            //alunos.Remove(aluno); 

                            MessageBox.Show("Novo erro encontrado");
                            return true;
                        }
                    }
                    else if (alerta2)
                    {
                        //click na carinha 
                        Util.ClickButtonsByCss(Driver, ".dropdown:nth-child(2) > .dropdown-toggle");
                        //clique em sair 
                        Driver.FindElement(By.LinkText("Sair")).Click();
                        return false;
                    }
                    else
                    {
                        //outros tipos de erros nao descobertos ainda/Erro de lógica 
                        aluno.Conclusao = "Novo erro encontrado/Erro de lógica";
                        //Dados.UpdateAluno(aluno); 
                        //alunos.Remove(aluno); 

                        MessageBox.Show("Novo erro encontrado/Erro de lógica");
                        return true;
                    }
                case "alunoNãoEncontrado":
                    aluno.Conclusao = "Aluno não encontrado";
                    //Dados.UpdateAluno(aluno); 
                    //alunos.Remove(aluno); 
                    return true;
                default:
                    aluno.Conclusao = Driver.FindElement(By.XPath(String.Format("//table[@id='gridResult']/tbody/tr[{0}]/td[4]", GetLinhaIndexMaior()))).Text.Trim();
                    //Dados.UpdateAluno(aluno); 
                    //alunos.Remove(aluno); 
                    return true;
            }
        }

        /// <summary> 
        /// Metodo para o codigo esperar por algum elemento existir/parar de existir na pagina 
        /// </summary> 
        /// <param name="element">elemento a ser aguardado</param> 
        /// <param name="exist">booleana para verificar se a espera é para o elemento existir ou para o elemento sumir</param> 
        private static void WaitPageLoading(string element, bool exist)
        {
            while (Driver.PageSource.Contains(element) == exist)
            {
                System.Threading.Thread.Sleep(1000);
            }
        }

        /// <summary> 
        /// Preenche o formulario para o aditamento do aluno 
        /// </summary> 
        /// <param name="aluno">TOAluno para aditamento</param> 
        static void FillForm(TOAluno aluno)
        {
            //Digita no Valor da Semestralidade ATUAL COM desconto - Grade Curricular a ser Cursada 
            //if (aluno.PossuiAlteracaoPorcentagemReceita)
            //{
            //    Driver.FindElement(By.Id("semestralidadeAtualComDescGradeASerCursada")).SendKeys(aluno.ValorAditado);
            //}
            //else
            //{
            Driver.FindElement(By.Id("semestralidadeAtualComDescGradeASerCursada")).SendKeys(aluno.ReceitaLiquida);
            //}

            //click no body para limpar 
            Driver.FindElement(By.CssSelector("body")).Click();

            //wait 
            WaitPageLoading("modal-backdrop fade in", true);
            WaitPageLoading("modal-backdrop fade", true);
            // 
            ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");

            if ("Aproveitamento Superior a 75%".Equals(aluno.AproveitamentoAtual))
            {
                //O estudante teve aproveitamento acad�mico igual ou superior a 75% no semestre ? SIM 
                Util.ClickButtonsById(Driver, "aproveitamento75S");
                ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollBy(0,50)");

                //O estudante est� regularmente matriculado? SIM 
                var element = Driver.FindElement(By.Id("regularmenteMatriculadoS"));
                ((IJavaScriptExecutor)Driver).ExecuteScript(string.Format("window.scrollTo({0}, {1})", element.Location.X, element.Location.Y - 100));
                Util.ClickButtonsById(Driver, "regularmenteMatriculadoS");
                ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollBy(0,50)");


                //O estudante possui benef�cio simult�neo de FIES e de bolsa ProUni em local de oferta ou curso distinto? NAO 
                Util.ClickButtonsById(Driver, "beneficioSimultaneoN");
                ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollBy(0,50)");

                //O prazo de dura��o regular do curso encontra-se vigente? SIM 
                Util.ClickButtonsById(Driver, "duracaoCursoS");
                ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollBy(0,50)");

                //O estudante transferiu de curso mais de uma vez nessa IES? NAO 
                Util.ClickButtonsById(Driver, "transferiuCursoN");
                ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollBy(0,50)");

                //Se aparecer: 
                if (Driver.FindElement(By.Id("erroValidarTotSemestresN")).Displayed)
                {//Houve erro do estudante e da CPSA ao registrar e validar, respectivamente, o total de semestres j� conclu�dos do curso: N�O 
                    Util.ClickButtonsById(Driver, "erroValidarTotSemestresN");
                }

                //O estudante possui algum desconto por liberalidade da IES? NAO 
                Util.ClickButtonsById(Driver, "descontoLiberalidadeN");
                ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
            }
            else
            {
                switch (aluno.HistoricoAproveitamento)
                {
                    case "1º Reconsideração":
                    case "2º Reconsideração":
                        //O estudante teve aproveitamento acad�mico igual ou superior a 75% no semestre ? NAO 
                        Util.ClickButtonsById(Driver, "aproveitamento75N");
                        ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollBy(0,50)");

                        //tratamento do erro de caso a pessoa estiver na 3� reconsidera��o na realidade 
                        if (/*Driver.FindElement(By.Id("divRejeicaoAutomatica")).Displayed && */false)//trocar isso 
                        {
                            //aluno.Conclusao = "Rejeitou execesso de reprovação"; 
                        }
                        else
                        {
                            //A CPSA ir� liberar o aditamento nesta situa��o? SIM  
                            Util.ClickButtonsById(Driver, "aproveitamentoInferior75S");
                            ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollBy(0,50)");

                            //Justificativa 
                            Util.ClickAndWriteById(Driver, "justificativa", aluno.HistoricoAproveitamento);
                            ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollBy(0,50)");

                            //O estudante est� regularmente matriculado? SIM 
                            var element = Driver.FindElement(By.Id("regularmenteMatriculadoS"));
                            ((IJavaScriptExecutor)Driver).ExecuteScript(string.Format("window.scrollTo({0}, {1})", element.Location.X, element.Location.Y - 100));
                            Util.ClickButtonsById(Driver, "regularmenteMatriculadoS");
                            ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollBy(0,50)");

                            //O estudante possui benef�cio simult�neo de FIES e de bolsa ProUni em local de oferta ou curso distinto? NAO 
                            Util.ClickButtonsById(Driver, "beneficioSimultaneoN");
                            ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollBy(0,50)");

                            //O  prazo de dura��o regular do curso encontra-se vigente? SIM 
                            Util.ClickButtonsById(Driver, "duracaoCursoS");
                            ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollBy(0,50)");

                            //O estudante transferiu de curso mais de uma vez nessa IES? N�O 
                            Util.ClickButtonsById(Driver, "transferiuCursoN");
                            ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollBy(0,50)");

                            //Houve erro do estudante e da CPSA ao registrar e validar... (Novo) 
                            //... 

                            //O estudante possui algum desconto por liberalidade da IES? N�O 
                            Util.ClickButtonsById(Driver, "descontoLiberalidadeN");
                            ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollBy(0,50)");

                            aluno.Conclusao = "Aditamento Realizado";
                        }
                        break;
                    case "Excesso de reprovação":
                        MessageBox.Show("Avisar os guris que aconteceu pela 1º vez Excesso de reprovação.");
                        //O estudante teve aproveitamento acad�mico igual ou superior a 75% no semestre ? NAO 
                        Util.ClickButtonsById(Driver, "aproveitamento75N");

                        aluno.Conclusao = "Rejeitou execesso de reprovação";
                        break;
                    default: throw new Exception(String.Format("Aproveitamento atual do aluno {0} não identificado.", aluno.Cpf));
                }
            }
        }

        public static bool BaixarDRM(TOAluno aluno)
        {
            ConsultarAlunoAditamento(aluno);
            WaitPageLoading("modal-backdrop fade in", true);
            WaitPageLoading("modal-backdrop fade", true);

            ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight - 1)");
            string semestreFormatado = semestreAtual.Split('/')[0];
            string anoFormatado = semestreAtual.Split('/')[1];

            string ano = string.Empty;
            string semestre = string.Empty;
            IWebElement button = null;
            if (Driver.PageSource.Contains(semestreAtual) == true)
            {
                var buttons = Driver.FindElements(By.Id("btnImprimirTermo"));

                foreach (var item in buttons)
                {
                    string dataAno = item.GetAttribute("data-ano");
                    string dataSemestre = item.GetAttribute("data-semestre");
                    if (dataAno == anoFormatado && dataSemestre == semestreFormatado)
                    {
                        ano = item.GetAttribute("data-ano");
                        semestre = item.GetAttribute("data-semestre");
                        button = item;
                    }
                }
                //IWebElement elemento = Driver.FindElement(By.Id("btnImprimirTermo"));

                //elemento = elemento.FindElement(By.Id("btnImprimirTermo"));
                //ano = elemento.GetAttribute("data-ano");
                //semestre = elemento.GetAttribute("data-semestre");
            }
            //int linha;
            if (Driver.PageSource.Contains("Nenhuma informação disponível") == true)
            {
                //linha = GetLinhaIndexMaior();
                aluno.Conclusao = "Nenhuma informação disponível";
                // Dados.UpdateAluno(aluno);
                return false;
            }
            //IWebElement elementoLinha = Driver.FindElement(By.XPath(String.Format("//table[@id='gridResult']/tbody/tr[{0}]", linha)));
            if (ano != string.Empty)
            {
                string windowOriginal = Driver.CurrentWindowHandle;
                bool errorMsg = true;
                var executor = (IJavaScriptExecutor)Driver;

                while (errorMsg == true)
                {
                    string name = Driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/form/div[7]/fieldset/div/table/tbody/tr[1]/td[2]")).Text;
                    aluno.Nome = name;
                    //IWebElement elementoTD = Driver.FindElement(By.XPath(String.Format("//table[@id='gridResult']/tbody/tr[{0}]/td[6]", linha)));
                    //elementoTD.FindElement(By.Id("btnImprimirTermo")).Click();
                    button.Click();

                    System.Threading.Thread.Sleep(1000);
                    Util.ClickButtonsById(Driver, "btnConfirmar");

                    System.Threading.Thread.Sleep(1000);
                    errorMsg = (bool)executor.ExecuteScript("return $('.alert.alert-error').is(':visible');");
                    if (errorMsg == true)
                    {
                        Util.ClickButtonsById(Driver, "btnConsultar");
                        WaitPageLoading("modal-backdrop fade in", true);
                        WaitPageLoading("modal-backdrop fade", true);
                    }
                }
                if (Driver.PageSource.Contains("MDLalerta_") == true)
                {
                    aluno.Conclusao = Driver.FindElement(By.XPath("/html/body/div[7]/div[2]/p")).Text;
                    Util.ClickButtonsById(Driver, "btnConfirmar");
                    //Dados.UpdateAluno(aluno);
                    return false;
                }

                System.Threading.Thread.Sleep(1000);
                while (Driver.WindowHandles.Last() == windowOriginal)
                {
                    System.Threading.Thread.Sleep(100);
                }
                qtdWindows++;

                Driver.SwitchTo().Window(Driver.WindowHandles.Last());

                //System.Threading.Thread.Sleep(1000);
                string fileName = aluno.Nome + "_" + aluno.Cpf;
                string nomePasta = "DRM FIES Novo";
                string simplificado = string.Empty;
                SaveSite(fileName, windowOriginal, nomePasta, ref simplificado);

                //Driver.Manage().Window.Minimize(); 
                //Driver.SwitchTo().Window(windowOriginal); 

                //String userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE"); 
                //String downloadFolder = Path.Combine(userRoot, "Downloads"); 
                //DirectoryInfo directory = new DirectoryInfo(downloadFolder); 

                //FileInfo myFile = directory.GetFiles().OrderByDescending(f => f.LastWriteTime).First(); 
                //bool downloading = true; 
                //while (downloading) 
                //{ 
                //    System.Threading.Thread.Sleep(1000); 
                //    myFile = directory.GetFiles().OrderByDescending(f => f.LastWriteTime).First(); 
                //    downloading = myFile.Name.EndsWith(".crdownload"); 
                //} 
                //File.Move(myFile.FullName, myFile.DirectoryName + "\\DRM_ALUNO_" + aluno.Cpf + ".zip"); 
                //Util.ClickButtonsById(Driver, "voltar"); 

                aluno.Conclusao = "DRM Baixado - " + simplificado;
                aluno.Extraido = "Sim";
                return true;
            }
            else
            {
                string situacaoAluno = string.Empty;
                if (Driver.PageSource.Contains("Nenhuma informação disponível") == true)
                {
                    situacaoAluno = "Nenhuma informação disponível";
                }
                else if (Driver.PageSource.Contains(semestreAtual) == true)
                {
                    situacaoAluno = Driver.PageSource.Split(new string[] { semestreAtual }, StringSplitOptions.None)[3];
                    situacaoAluno = situacaoAluno.Split(new string[] { "td class=" }, StringSplitOptions.None)[1];
                    situacaoAluno = situacaoAluno.Replace(">", "");
                    situacaoAluno = situacaoAluno.Replace("<", "");
                    situacaoAluno = situacaoAluno.Replace("\"", "");
                    situacaoAluno = situacaoAluno.Replace("/td", "");
                }

                aluno.Conclusao = situacaoAluno;

            }
            ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(0, 0)");
            //aluno.Conclusao = "Nenhuma informação disponível"; 
            return false;
        }

        private static void ConsultarAluno(TOAluno aluno)
        {
            //WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5)); 
            //wait.Until(driver => Driver.PageSource.Contains("input-medium cpf")); 

            WaitPageLoading("modal-backdrop fade in", true);
            WaitPageLoading("modal-backdrop fade", true);
            WaitPageLoading("username", false);

            //click no body para limpar 
            Driver.FindElement(By.CssSelector("body")).Click();

            //clicar em limpar  
            Util.ClickButtonsById(Driver, "btnLimpar");

            Util.ClickButtonsById(Driver, "cpf");
            //Driver.FindElement(By.Id("cpf")).SendKeys(aluno.Cpf); 
            var executor = (IJavaScriptExecutor)Driver;
            executor.ExecuteScript($@"document.getElementById(""cpf"").value = ""{aluno.Cpf}"";");

            //string semestreAno; 
            //if (DateTime.Now.Month <= 6) 
            //{ 
            //    semestreAno = "1/"; 
            //} 
            //else 
            //{ 
            //    semestreAno = "2/"; 
            //} 
            //semestreAno += DateTime.Now.Year; 
            //Util.ClickDropDown(Driver, "id", "selectSemestreAno", semestreAno); 

            //as vezes nao clica 
            Util.ClickButtonsById(Driver, "btnConsultar");
        }

        private static void SaveSite(string fileName, string windowOriginal, string nomePasta, ref string simplificado)
        {
            string informacao = Driver.PageSource;
            if (informacao.Contains("ADITAMENTO SIMPLIFICADO DE CONTRATO DE FINANCIAMENTO") == true)
            {
                simplificado = "Simplificado";
            }
            else
            {
                simplificado = "Não Simplificado";
            }
            //System.Threading.Thread.Sleep(1000);
            Driver.Close();
            Driver.SwitchTo().Window(windowOriginal);
            if (Directory.Exists("html") == false)
            {
                Directory.CreateDirectory("html");
            }
            string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadFolder = System.IO.Path.Combine(userRoot, "Downloads");
            string diretorioDRM = downloadFolder + "\\" + nomePasta + "\\";
            Util.CreateDirectory(diretorioDRM);

            string htmlDirectory = "html\\contrato.html";
            string[] temp = new string[2];
            if (informacao.Contains("<head>") == true)
            {
                temp = informacao.Split(new string[] { "<head>" }, StringSplitOptions.None);
                temp[0] += "<meta charset = \"utf-8\">";
            }
            informacao = temp[0] + temp[1];
            File.WriteAllText(htmlDirectory, informacao);

            //string DataDirectory = System.Environment.ExpandEnvironmentVariables("%userprofile%\\downloads\\"); 
            string DataDirectory = diretorioDRM;
            DataDirectory += fileName;
            DataDirectory += "_DRM";
            DataDirectory += ".pdf";

            SaveHtmlAsPdf(htmlDirectory, DataDirectory);
            Util.CreateDirectory("Temp");
            DirectoryInfo di = new DirectoryInfo("Temp");
            foreach (var item in di.GetFiles())
            {
                item.Delete();
            }
            File.Copy(DataDirectory, "Temp\\" + fileName + ".pdf");
            if (emLote == false)
            {
                Process.Start("Temp\\" + fileName + ".pdf");
            }

            // SendKeys.Send("^{a}"); 
            // System.Threading.Thread.Sleep(100); 
            // 
            // SendKeys.Send("^{c}"); 
            // System.Threading.Thread.Sleep(100); 

            // Write the string array to a new file named "WriteLines.txt". 
            //  using (StreamWriter outputFile = new StreamWriter(DataDirectory)) 
            //  { 
            //      outputFile.WriteLine(System.Windows.Forms.Clipboard.GetText()); 
            //  } 
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

        private static int GetLinhaIndexMaior(string replace = "")
        {
            int indexMaior = -1;
            int[] semestreAnoMaior = { 0, 0 };
            var elementosTd = Driver.FindElements(By.XPath("//table[@id='gridResult']/tbody/tr/td[3]"));

            for (int i = 0; i < elementosTd.Count; i++)
            {
                String[] dataSemestre = elementosTd[i].Text.Split('/');
                if (replace != "")
                {
                    for (int j = 0; j < dataSemestre.Length; j++)
                    {
                        dataSemestre[j] = dataSemestre[j].Replace(replace, "");
                    }
                }

                if (indexMaior == -1)
                {
                    semestreAnoMaior[0] = int.Parse(dataSemestre[0]);
                    semestreAnoMaior[1] = int.Parse(dataSemestre[1]);
                    indexMaior = i;
                }
                else
                {
                    if (semestreAnoMaior[1] <= int.Parse(dataSemestre[1]))
                    {
                        if (semestreAnoMaior[1] < int.Parse(dataSemestre[1]))
                        {
                            semestreAnoMaior[0] = int.Parse(dataSemestre[0]);
                            semestreAnoMaior[1] = int.Parse(dataSemestre[1]);
                            indexMaior = i;
                        }
                        else
                        {
                            if (semestreAnoMaior[0] < int.Parse(dataSemestre[0]))
                            {
                                semestreAnoMaior[0] = int.Parse(dataSemestre[0]);
                                semestreAnoMaior[1] = int.Parse(dataSemestre[1]);
                                indexMaior = i;
                            }
                        }
                    }
                }
            }
            return indexMaior + 1;
        }

        private static void Logout()
        {
            //clicar na cara 
            //Util.ClickButtonsByCss(Driver, ".panel-body"); 
            //Util.ClickButtonsByXpath(Driver, "//div[@id='accordion']/div"); 
            Util.ClickButtonsByXpath(Driver, "//div/div[2]/div");

            //clica em algo 
            //Util.ClickButtonsByCss(Driver, ".dropdown:nth-child(2) > .dropdown-toggle"); 
            //Util.ClickButtonsByXpath(Driver, "//header[@id='header']/div/div/div/div/ul[2]/li[2]/a"); 
            Util.ClickButtonsByXpath(Driver, "(//a[contains(@href, '#')])[24]");

            //clica em sair 
            //Util.ClickButtonsByCss(Driver, ".open > .dropdown-menu a"); 
            //Util.ClickButtonsByXpath(Driver, "//a[contains(text(),'Sair')]"); 
            Util.ClickButtonsByXpath(Driver, "//header[@id='header']/div/div/div/div/ul[2]/li[2]/ul/li[3]/a");
        }
    }
}
