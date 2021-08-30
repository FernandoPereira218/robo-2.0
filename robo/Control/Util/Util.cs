using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Security.Cryptography;
using robo.Control.Update;

namespace Robo
{
    static class Util
    {
        /// <summary>
        /// Clica e manda dados ao TextBox.
        /// </summary>
        /// <param name="id">ID do TextBox</param>
        /// <param name="valor">Valor a ser escrito.</param>
        public static void ClickAndWriteById(IWebDriver driver, string id, string valor)
        {
            IWebElement elementoAtual = driver.FindElement(By.Id(id));

            WaitLogoLoading(driver);

            elementoAtual.Click();
            //var jse = (IJavaScriptExecutor)driver;
            //jse.ExecuteScript(string.Format("document.getElementById('{0}').click();", id));

            WaitLogoLoading(driver);

            elementoAtual.SendKeys(valor);
            //jse.ExecuteScript(string.Format("document.getElementById('{0}').value = '{1}';", id, valor));

            //clica fora do ultimo campo para que não de problemas
            ClearClickFiesVelho(driver);

            Util.Sleep();
        }

        /// <summary>
        /// Clica e manda dados ao TextBox.
        /// </summary>
        /// <param name="name">Name do TextBox</param>
        /// <param name="valor">Valor a ser escrito.</param>
        public static void ClickAndWriteByName(IWebDriver driver, string name, string valor)
        {
            IWebElement elementoAtual = driver.FindElement(By.Name(name));

            WaitLogoLoading(driver);

            elementoAtual.Click();

            WaitLogoLoading(driver);

            elementoAtual.SendKeys(valor);

            //clica fora do ultimo campo para que não de problemas
            ClearClickFiesVelho(driver);

            Util.Sleep();
        }

        /// <summary>
        /// Clica pelo CSSSelector.
        /// </summary>
        /// <param css="css">Css do Elemento.</param>
        public static void ClickButtonsByCss(IWebDriver driver, string css)
        {
            WaitLogoLoading(driver);
            driver.FindElement(By.CssSelector(css)).Click();

            Util.Sleep();
        }

        /// <summary>
        /// Clica no elemento pelo ID.
        /// </summary>
        /// <param name="id">Id do elemento.</param>
        public static void ClickButtonsById(IWebDriver driver, string id)
        {
            WaitLogoLoading(driver);
            driver.FindElement(By.Id(id)).Click();

            Util.Sleep();
        }

        /// <summary>
        /// Clica no elemento pelo nome.
        /// </summary>
        /// <param name="name">Nome do elemento.</param>
        public static void ClickButtonsByName(IWebDriver driver, string name)
        {
            WaitLogoLoading(driver);
            driver.FindElement(By.Name(name)).Click();

            Util.Sleep();
        }

        /// <summary>
        /// Clica no elemento por xpath.
        /// </summary>
        /// <param name="xPath">xpath do elemento.</param>
        public static void ClickButtonsByXpath(IWebDriver driver, string xPath)
        {
            WaitLogoLoading(driver);
            driver.FindElement(By.XPath(xPath)).Click();

            Util.Sleep();
        }

        /// <summary>
        /// Inicia nova sessão do browser.
        /// </summary>
        public static IWebDriver StartBrowser(string url, string downloadFldr = "", bool firefox = true, bool headless = false)
        {
            IWebDriver driver;

            string downloadFolder = "";
            if (downloadFldr != "")
            {
                CreateDirectory("RelatorioExportacao\\");
                downloadFolder = Directory.GetCurrentDirectory() + "\\RelatorioExportacao\\";
            }
            else
            {
                String userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
                downloadFolder = System.IO.Path.Combine(userRoot, "Downloads");
            }

            if (firefox == true)
            {

                //Firefox
                var firefoxDriverService = FirefoxDriverService.CreateDefaultService(Environment.CurrentDirectory + @"\driver");
                firefoxDriverService.HideCommandPromptWindow = true;

                FirefoxOptions firefoxOptions = new FirefoxOptions();
                firefoxOptions.AcceptInsecureCertificates = true;
                if (RoboForm.versaoRobo != "operacoesFinanceiras" || headless == true)
                {
                    firefoxOptions.AddArgument("--headless");
                }
                var firefoxProfile = new FirefoxProfile();


                //profile.SetPreference("browser.download.downloadDir", downloadFolder);
                //profile.SetPreference("browser.download.defaultFolder", downloadFolder);
                //profile.SetPreference("browser.helperApps.neverAsk.openFile", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                //profile.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/pdf;text/plain;application/text;text/xml;application/xml");
                firefoxProfile.SetPreference("browser.helperApps.neverAsk.saveToDisk", "text/plain,application/octet-stream,application/pdf,application/x-pdf,application/vnd.pdf,application/zip,text/csv,application/csv,application/vnd.ms-excel,text/comma-separat‌​ed-values,application/excel,text/x-server-parsed-html,application/vnd.ms-excel,application/msexcel");
                firefoxProfile.SetPreference("browser.download.folderList", 2);
                firefoxProfile.SetPreference("browser.download.dir", downloadFolder);
                firefoxProfile.SetPreference("browser.helperApps.alwaysAsk.force", false);
                firefoxProfile.SetPreference("browser.download.manager.useWindow", false);
                firefoxProfile.SetPreference("browser.download.manager.focusWhenStarting", false);
                firefoxProfile.SetPreference("browser.download.manager.showAlertOnComplete", false);
                firefoxProfile.SetPreference("browser.download.manager.closeWhenDone", true);
                firefoxProfile.SetPreference("security.tls.version.min", 1);
                firefoxProfile.SetPreference("security.tls.version.max", 4);
                firefoxProfile.SetPreference("print.tab_modal.enabled", false);
                firefoxOptions.Profile = firefoxProfile;
                driver = new FirefoxDriver(firefoxDriverService, firefoxOptions);
            }
            else
            {
                //Chrome
                var chromeDriverService = ChromeDriverService.CreateDefaultService(Environment.CurrentDirectory + @"\driver");
                chromeDriverService.HideCommandPromptWindow = true;
                ChromeOptions chromeOptions = new ChromeOptions();
                if (RoboForm.versaoRobo != "operacoesFinanceiras")
                {
                    chromeOptions.AddArgument("--headless");
                }
                chromeOptions.AddUserProfilePreference("download.default_directory", downloadFolder);
                try
                {

                    driver = new ChromeDriver(chromeDriverService, chromeOptions);
                }
                catch
                {
                    MessageBox.Show("Versão do Google Chrome desatualizada. Clique em 'Ok' para atualizar e continuar o processo.");
                    chromeDriverService.Dispose();

                    UpdateChromedriver.DownloadChromedriver();
                    chromeDriverService = ChromeDriverService.CreateDefaultService(Environment.CurrentDirectory + @"\driver");
                    chromeDriverService.HideCommandPromptWindow = true;
                    driver = new ChromeDriver(chromeDriverService, chromeOptions);
                    //throw new Exception("Versão do chromedriver foi atualizada, favor tentar novamente!");
                }
            }
            driver.Manage().Window.Maximize();
            driver.Url = url;
            //driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 3, 0);
            if (headless == true)
            {
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
            }
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);

            return driver;
        }

        public static void EndBrowser(IWebDriver driver)
        {
            driver.Quit();
        }

        private static void ClearClickFiesVelho(IWebDriver driver)
        {
            if (IsFormFillingFiesVelho(driver))
            {
                driver.FindElement(By.CssSelector("body")).Click();
            }
        }

        public static void AcertaBarraR(TOAluno infs)
        {
            infs.SemestreAditar = infs.SemestreAditar.Replace("\r", "");
            infs.Campus = infs.Campus.Replace("\r", "");
            infs.Conclusao = infs.Conclusao.Replace("\r", "");
            infs.Cpf = infs.Cpf.Replace("\r", "");
            infs.Curso = infs.Curso.Replace("\r", "");
            infs.DuracaoRegular = infs.DuracaoRegular.Replace("\r", "");
            infs.GradeAtualComDesconto = infs.GradeAtualComDesconto.Replace("\r", "");
            infs.GradeAtualCoparticipacao = infs.GradeAtualCoparticipacao.Replace("\r", "");
            infs.GradeAtualFinanciadoFIES = infs.GradeAtualFinanciadoFIES.Replace("\r", "");
            infs.HorarioConclusao = infs.HorarioConclusao.Replace("\r", "");
            infs.PercentualDeFinanciamentoSolicitado = infs.PercentualDeFinanciamentoSolicitado.Replace("\r", "");
            infs.SemestreSerCursadoPeloEstudante = infs.SemestreSerCursadoPeloEstudante.Replace("\r", "");
            infs.TotalDeSemestresConcluidos = infs.TotalDeSemestresConcluidos.Replace("\r", "");
            infs.TotalDeSemestresDilatados = infs.TotalDeSemestresDilatados.Replace("\r", "");
            infs.TotalDeSemestresJaFinanciados = infs.TotalDeSemestresJaFinanciados.Replace("\r", "");
            infs.TotalDeSemestresSuspensos = infs.TotalDeSemestresSuspensos.Replace("\r", "");


            infs.SemestreAditar = infs.SemestreAditar.Replace("\n", "");
            infs.Campus = infs.Campus.Replace("\n", "");
            infs.Conclusao = infs.Conclusao.Replace("\n", "");
            infs.Cpf = infs.Cpf.Replace("\n", "");
            infs.Curso = infs.Curso.Replace("\n", "");
            infs.DuracaoRegular = infs.DuracaoRegular.Replace("\n", "");
            infs.GradeAtualComDesconto = infs.GradeAtualComDesconto.Replace("\n", "");
            infs.GradeAtualCoparticipacao = infs.GradeAtualCoparticipacao.Replace("\n", "");
            infs.GradeAtualFinanciadoFIES = infs.GradeAtualFinanciadoFIES.Replace("\n", "");
            infs.HorarioConclusao = infs.HorarioConclusao.Replace("\n", "");
            infs.PercentualDeFinanciamentoSolicitado = infs.PercentualDeFinanciamentoSolicitado.Replace("\n", "");
            infs.SemestreSerCursadoPeloEstudante = infs.SemestreSerCursadoPeloEstudante.Replace("\n", "");
            infs.TotalDeSemestresConcluidos = infs.TotalDeSemestresConcluidos.Replace("\n", "");
            infs.TotalDeSemestresDilatados = infs.TotalDeSemestresDilatados.Replace("\n", "");
            infs.TotalDeSemestresJaFinanciados = infs.TotalDeSemestresJaFinanciados.Replace("\n", "");
            infs.TotalDeSemestresSuspensos = infs.TotalDeSemestresSuspensos.Replace("\n", "");
        }

        /// <summary>
        /// Escolhe uma opção de um dropdown
        /// </summary>
        /// <param name="driver">webdriver</param>
        /// <param name="metodo">qual metodo a ser usado, id, name, css, ou xpath</param>
        /// <param name="valorMetodo">qual o id, name, css ou xpath a procurar</param>
        /// <param name="valorEscolha">qual a opção a ser escolhida</param>
        public static void ClickDropDown(IWebDriver driver, string metodo, string valorMetodo, string valorEscolha)
        {
            WaitLogoLoading(driver);
            driver.FindElement(By.XPath("//select[@" + metodo + "='" + valorMetodo + "']/option[contains(.,'" + valorEscolha + "')]")).Click();

            Util.Sleep();
        }
        public static void CreateDirectory(params String[] directories)
        {
            foreach (String directory in directories)
            {
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
            }
        }

        public static string GetDownloadsFolderPath()
        {
            string userRoot = Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadFolder = Path.Combine(userRoot, "Downloads");
            return downloadFolder;
        }

        /// <summary>
        /// Espera o logoLoading terminar.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="cadastrando">se esta na parte do login ou da pagina das opções do aditamento</param>
        public static void WaitLogoLoading(IWebDriver driver)
        {
            if (IsFormFillingFiesVelho(driver))
            {
                IWebElement logoLoading = driver.FindElement(By.ClassName("background-grey"));
                int cont = int.MaxValue;
                while (logoLoading.Displayed == true)//so vai sair do metodo quando parar de carregar
                {
                    cont--;
                    if (cont < 0)
                    {
                        throw new Exception("ocorreu algum tipo de erro");
                    }
                }
            }
            else if (IsFormFillingFiesNovo(driver))
            {
                //((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight - 1)");
            }
        }

        /// <summary>
        /// Checa se não esta na página de login (unica que não tem o loading)
        /// </summary>
        /// <param name="driver">webdriver</param>
        /// <returns></returns>
        public static Boolean IsFormFillingFiesVelho(IWebDriver driver)
        {
            //string pagesource = driver.PageSource;
            return driver.Url.StartsWith("http://sisfies.mec.gov.br/cpsa/aditamento/formulario/");

        }

        /// <summary>
        /// Checa se esta na pagina de preencher o formulario
        /// </summary>
        /// <param name="driver">webdriver</param>
        /// <returns></returns>
        public static Boolean IsFormFillingFiesNovo(IWebDriver driver)
        {
            //string pagesource = driver.PageSource;
            return driver.Url.StartsWith("http://sifesweb.caixa.gov.br/fes-web/?session_state=") ||
                driver.Url.StartsWith("https://sifesweb.caixa.gov.br/fes-web/?session_state=");

        }

        public static void Sleep()
        {
            System.Threading.Thread.Sleep(250);
            //System.Threading.Thread.Sleep(500);
            //System.Threading.Thread.Sleep(1000);
        }
        public static string VerificaSemestreAtual()
        {
            DateTime aDate = DateTime.Now;
            string sem = string.Empty;
            if (aDate.Month > 6)
            {
                sem += "2";
            }
            else
            {
                sem += "1";
            }
            sem += "/" + aDate.Year.ToString();

            return sem;
        }
        public static void gravaSenha()
        {
            //Database.Acess.SetPwd("AlunosBrilhantes");
            Database.Acess.SetPwd(string.Empty);
        }
        public static bool VerificaCPFValido(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
            {
                return false;
            }
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            }
            resto = soma % 11;
            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            }
            resto = soma % 11;
            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        public static void SalvarArquivos(IWebDriver Driver, string tipoRelatorio, string campus = "")
        {
            if (Driver.PageSource.Contains("Nenhuma informação disponível") == true)
            {
                throw new Exception("Nenhuma informação disponível");
            }
            while (Directory.GetFiles("RelatorioExportacao\\").Count() == 0)
            {
                System.Threading.Thread.Sleep(100);
            }

            DirectoryInfo directory = new DirectoryInfo("RelatorioExportacao\\");
            FileInfo myFile = directory.GetFiles().OrderByDescending(f => f.LastWriteTime).First();
            bool downloading = true;
            while (downloading)
            {
                System.Threading.Thread.Sleep(1000);
                myFile = directory.GetFiles().OrderByDescending(f => f.LastWriteTime).First();
                downloading = myFile.Name.EndsWith(".crdownload");
            }

            foreach (var item in Directory.GetFiles("RelatorioExportacao\\"))
            {
                String userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
                String downloadFolder = System.IO.Path.Combine(userRoot, "Downloads");
                string nomeArquivo = DateTime.Now.ToString("dd-MM-yy") + campus + ".xls";
                CreateDirectory(downloadFolder + "\\Relatório Exportacao");
                string caminho = downloadFolder + "\\Relatório Exportacao\\" + tipoRelatorio + " " + nomeArquivo;
                File.Copy(item, caminho, true);
                File.Delete(item);
                //Process.Start(caminho);
            }
        }

        public static void getDriverDisposeAndDriverCloseWithThrow(Exception motivo, IWebDriver Driver)
        {
            Driver.Close();
            Driver.Dispose();
            throw motivo;
        }

        public static string GetMD5(string password)
        {
            MD5 md5password = MD5.Create();
            //Conversão a String para array de bytes, que é como a biblioteca trabalha.
            byte[] data = md5password.ComputeHash(Encoding.UTF8.GetBytes(password));

            //Criação de um stringBuilder para concatenar os dados
            StringBuilder sBuilder = new StringBuilder();

            //Loop para formatar cada byte como uma String em hexadecimal
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
        public static void ScrollToElementByID(IWebDriver Driver, string button)
        {
            var element = Driver.FindElement(By.Id(button));
            ((IJavaScriptExecutor)Driver).ExecuteScript(string.Format("window.scrollTo({0}, {1})", element.Location.X, element.Location.Y - 100));
        }

        public static void ScrollToElementByXpath(IWebDriver Driver, string button)
        {
            var element = Driver.FindElement(By.XPath(button));
            ((IJavaScriptExecutor)Driver).ExecuteScript(string.Format("window.scrollTo({0}, {1})", element.Location.X, element.Location.Y - 100));
        }

        public static IWebElement VerificarElementoExiste(IWebDriver Driver, string tipo, string identificador)
        {
            var executor = (IJavaScriptExecutor)Driver;
            switch (tipo.ToUpper())
            {
                case "ID":
                    return (IWebElement)executor.ExecuteScript($@"return document.getElementById(""{identificador}"");");
                case "CLASSNAME":
                    var element = (IWebElement)executor.ExecuteScript($@"return document.getElementsByClassName(""{identificador}"")[0];");
                    return element;
                case "NAME":
                    return (IWebElement)executor.ExecuteScript($@"return document.getElementByName(""{identificador}"");");
                default:
                    return null;
            }

        }

        public static void ExportarCSV(int countDataGrid)
        {
            if (countDataGrid > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "CSV (*.csv)|*.csv";
                sfd.FileName = "Exportado_Robo.csv";
                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("Arquivo já existe, e está aberto em outro aplicativo" + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            ExportarAlunosParaCSV(sfd.FileName);
                            MessageBox.Show("Dados Exportados com Sucesso!!!", "Info");
                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show("Error :" + ex.Message);
                        }


                    }
                }
            }
            else
            {
                MessageBox.Show("Sem Registro para Exportar!!!", "Info");
            }

        }
        public static void ExportarAlunosParaCSV(string fileName)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("Cpf", "Cpf");
            dic.Add("Nome", "Nome");
            dic.Add("Tipo", "Tipo");
            dic.Add("Conclusao", "Conclusao");
            dic.Add("HorarioConclusao", "HorarioConclusao");
            dic.Add("Campus", "Campus");
            dic.Add("AproveitamentoAtual", "AproveitamentoAtual");
            dic.Add("HistoricoAproveitamento", "HistoricoAproveitamento");
            dic.Add("ReceitaBruta", "ReceitaBruta");
            dic.Add("ReceitaLiquida", "ReceitaLiquida");
            dic.Add("ReceitaFies", "ReceitaFies");
            dic.Add("CampusAditado", "CampusAditado");
            dic.Add("ValorAditado", "ValorAditado");
            dic.Add("ValorAditadoComDesconto", "ValorAditadoComDesconto");
            dic.Add("ValorAditadoFinanciamento", "ValorAditadoFinanciamento");
            dic.Add("ValorPagoRecursoEstudante", "ValorPagoRecursoEstudante");
            dic.Add("DescontoLiberalidade", "DescontoLiberalidade");
            dic.Add("Extraido", "Extraido");
            dic.Add("Justificativa", "Justificativa");


            List<TOAluno> alunos = Database.Acess.SelectAll<TOAluno>("ALUNO");
            CSVManager.CSVManager.ExportCSV<TOAluno>(fileName, dic, alunos);
        }
    }
}