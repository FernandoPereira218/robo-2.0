using OpenQA.Selenium;
using Robo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robo.pgm
{
    public static class FiesNovoInf
    {
        static IWebDriver driver;
        private static TOLogin login;
        static List<TOAlunoInf> alunos;
        static string semestreAtual;
        public static void OpenFiesNovo(List<TOLogin> loginParameters, List<TOAlunoInf> alunosParametrs, string semestre)
        {
            for (int i = loginParameters.Count - 1; i >= 0; i--)
            {
                if (loginParameters[i].Plataforma == "Antigo")
                {
                    loginParameters.RemoveAt(i);
                }
            }
            semestreAtual = semestre;
            alunos = alunosParametrs;
            try
            {
                driver = Util.StartBrowser("http://sifesweb.caixa.gov.br/");
                foreach (TOLogin loginAtual in loginParameters)
                {
                    login = loginAtual;
                    AbrirLogin();
                    ClicarBotaoMenu("btnAdmnstrcManutenccedilatildeoAditamentoRenovaccedilatildeo");
                    ExecutarAlunos();
                }

                //Exportar();
                driver.Quit();
                driver.Dispose();
            }
            catch (Exception)
            {
                driver.Quit();
                driver.Dispose();
                throw;
            }
        }
        private static void AbrirLogin()
        {
            Util.ClickAndWriteById(driver, "username", login.User);
            Util.ClickButtonsById(driver, "button-submit");
            Util.ClickAndWriteById(driver, "password", login.Password);
            Util.ClickButtonsByCss(driver, "button:nth-child(1)");
        }
        private static void ClicarBotaoMenu(string idMenu)
        {
            if (!driver.PageSource.Contains("código autenticador"))
            {

                while (!driver.PageSource.Contains("dropdown contratacao") || driver.PageSource.Contains("modal-backdrop fade in") || driver.PageSource.Contains("modal-backdrop fade"))
                {
                    System.Threading.Thread.Sleep(1000);
                }
                IWebElement sAdminMenuJobTitle = driver.FindElement(By.XPath(string.Format("//*[@id=\"{0}\"]", idMenu)));
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", sAdminMenuJobTitle);
            }
            else
            {
                throw new Exception("Problema com código autenticador");
            }
        }
        static void ExecutarAlunos()
        {
            for (int i = alunos.Count - 1; i >= 0; i--)
            {
                if (alunos[i].Conclusao == "Não Feito")
                {
                    ConsultarAluno(alunos[i]);
                    PegarInf(alunos[i], semestreAtual);
                }
            }
        }
        static void ConsultarAluno(TOAlunoInf aluno)
        {
            WaitPageLoading("modal-backdrop fade in", true);
            WaitPageLoading("modal-backdrop fade", true);
            WaitPageLoading("input-medium cpf", false);

            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, 0)");

            //click no body para limpar
            Util.ClickButtonsByCss(driver, "body");

            //clicar em limpar 
            Util.ClickButtonsById(driver, "btnLimpar");

            Util.ClickButtonsById(driver, "cpf");

            //driver.FindElement(By.Id("cpf")).SendKeys(aluno.Cpf);
            var executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript($@"document.getElementById(""cpf"").value = ""{aluno.Cpf}"";");

            //as vezes nao clica
            Util.ClickButtonsById(driver, "btnConsultar");
        }
        private static void WaitPageLoading(string element, bool exist)
        {
            while (driver.PageSource.Contains(element) == exist)
            {
                System.Threading.Thread.Sleep(1000);
            }
        }
        static bool PegarInf(TOAlunoInf aluno, string semestreAtual)
        {
            WaitPageLoading("modal-backdrop fade in", true);
            WaitPageLoading("modal-backdrop fade", true);

            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight - 1)");

            if (driver.PageSource.Contains("Nenhuma informação disponível") == false)
            {
                int linha = GetLinhaIndexMaior();
                IWebElement elementoLinha = driver.FindElement(By.XPath(String.Format("//table[@id='gridResult']/tbody/tr[{0}]", linha)));
                //if (elementoLinha.Text.Contains("Contratado"))
                //{
                string windowOriginal = driver.CurrentWindowHandle;
                int qtdWindows = 1;

                string[] temp = semestreAtual.Split('/');
                string nroSemestre = temp[0];
                string anoSemestre = temp[1];

                if (driver.PageSource.Contains(string.Format("data-semestre=\"{0}\" data-ano=\"{1}\"", nroSemestre, anoSemestre)))
                {
                    var buttons = driver.FindElements(By.Id("btnImprimirTermo"));
                    IWebElement botaoSemestre = null;
                    foreach (var item in buttons)
                    {
                        string dataAno = item.GetAttribute("data-ano");
                        string dataSemestre = item.GetAttribute("data-semestre");
                        if (dataAno == anoSemestre && dataSemestre == nroSemestre)
                        {
                            //ano = item.GetAttribute("data-ano");
                            //semestre = item.GetAttribute("data-semestre");
                            botaoSemestre = item;
                        }
                    }
                    // = driver.FindElement(By.XPath(string.Format("//a[(@data-semestre='{0}') and (@data-ano='{1}')]", nroSemestre, anoSemestre)));
                    if (botaoSemestre != null)
                    {

                        botaoSemestre.Click();


                        System.Threading.Thread.Sleep(1000);
                        Util.ClickButtonsById(driver, "btnConfirmar");
                        System.Threading.Thread.Sleep(1000);
                        if (driver.PageSource.Contains("MDLalerta_") == true)
                        {
                            aluno.Conclusao = driver.FindElement(By.XPath("/html/body/div[7]/div[2]/p")).Text;
                            Util.ClickButtonsById(driver, "btnConfirmar");
                            Dados.UpdateAluno(aluno);
                            return false;
                        }

                        System.Threading.Thread.Sleep(1000);
                        while (driver.WindowHandles.Last() == windowOriginal)
                        {
                            System.Threading.Thread.Sleep(100);
                        }
                        qtdWindows++;

                        driver.SwitchTo().Window(driver.WindowHandles.Last());

                        System.Threading.Thread.Sleep(1000);

                        string informacao = SaveInfs(aluno.Cpf);
                        driver.Close();
                        driver.SwitchTo().Window(windowOriginal);
                        ProcessarInfsFiesNovo(informacao, aluno);

                        //Driver.Manage().Window.Minimize();

                        return true;
                    }
                    else
                    {
                        aluno.Conclusao = "Nenhum Aluno encontrado no semestre selecionado";
                        Dados.UpdateAluno(aluno);
                    }
                }
                else if(driver.PageSource.Contains(semestreAtual) == true)
                {
                    string situacaoAluno = string.Empty;
                    situacaoAluno = driver.PageSource.Split(new string[] { semestreAtual }, StringSplitOptions.None)[3];
                    situacaoAluno = situacaoAluno.Split(new string[] { "td class=" }, StringSplitOptions.None)[1];
                    situacaoAluno = situacaoAluno.Replace(">", "");
                    situacaoAluno = situacaoAluno.Replace("<", "");
                    situacaoAluno = situacaoAluno.Replace("\"", "");
                    situacaoAluno = situacaoAluno.Replace("/td", "");
                    aluno.Conclusao = situacaoAluno;
                    Dados.UpdateAluno(aluno);
                    return false;
                }
            }
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, 0)");

            return false;
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

                if (inf.Contains("Valor da semestralidade e da mensalidade atual - Grade Curricular a ser cursada no semestre aditado:") == true)
                {

                    depoisTitulo = inf.Split(new string[] { "Valor da semestralidade e da mensalidade atual - Grade Curricular a ser cursada no semestre aditado:" }, StringSplitOptions.None)[1];
                }
                else
                {
                    depoisTitulo = inf.Split(new string[] { "Valor da semestralidade e da mensalidade Atual - Grade Curricular a ser cursada no semestre a aditar:" }, StringSplitOptions.None)[1];
                }
                infs.Add(depoisTitulo.Split('\n')[5]);
                aluno.GradeAtualComDesconto = infs[infs.Count() - 1];
                infs.Add(depoisTitulo.Split('\n')[7]);
                aluno.GradeAtualFinanciadoFIES = infs[infs.Count() - 1];
                infs.Add(depoisTitulo.Split('\n')[9]);
                aluno.GradeAtualCoparticipacao = infs[infs.Count() - 1];

                for (int i = 0; i < infs.Count; i++)
                {
                    infs[i] = infs[i].Replace("\r", string.Empty);
                }
                Util.AcertaBarraR(aluno);
                aluno.HorarioConclusao = DateTime.Now.ToString();
                aluno.Conclusao = "DRM Baixado";

                Dados.UpdateAluno(aluno);
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Erro não esperado encontrado.\n Contate os alunos brilhantes.\n\n{0}", e.Message));
            }
        }
        static string SaveInfs(string filename)
        {
            string alltext = driver.FindElement(By.TagName("body")).Text;

            return alltext;
        }
        private static int GetLinhaIndexMaior(string replace = "")
        {
            int indexMaior = -1;
            int[] semestreAnoMaior = { 0, 0 };
            var elementosTd = driver.FindElements(By.XPath("//table[@id='gridResult']/tbody/tr/td[3]"));

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
    }

}
