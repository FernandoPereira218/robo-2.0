using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using System.Windows.Forms;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using robo.Interface;
using robo.Update;

namespace robo.Utils
{
    /// <summary>
    /// Possui métodos comuns que podem ser utilizados durante a navegação em um site
    /// </summary>
    public class UtilSelenium
    {
        private WebDriverWait wait;

        /// <summary>
        /// Espera até um elemento específico ser visível
        /// </summary>
        /// <param name="tipo">By.(Id, Xpath, Css)("elementoDesejado")</param>
        /// <param name="segundos">Tempo limite de espera em segundos</param>
        protected void WaitElementIsVisible(IWebDriver driver, By tipo, int segundos = 60)
        {
            TimeSpan span = TimeSpan.FromSeconds(segundos);
            wait = new WebDriverWait(driver, span);
            wait.Until(ExpectedConditions.ElementIsVisible(tipo));
        }
        /// <summary>
        /// Clica e manda dados ao TextBox.
        /// </summary>
        /// <param name="id">ID do TextBox</param>
        /// <param name="valor">Valor a ser escrito.</param>
        protected void ClickAndWriteById(IWebDriver driver, string id, string valor)
        {
            IWebElement elementoAtual = driver.FindElement(By.Id(id));

            WaitLogoLoading(driver);

            elementoAtual.Click();
            //var jse = (IJavaScriptExecutor)driver;
            //jse.ExecuteScript(string.Format("document.getElementById('{0}').click();", id));

            WaitLogoLoading(driver);
            elementoAtual.Clear();
            elementoAtual.SendKeys(valor);
            //jse.ExecuteScript(string.Format("document.getElementById('{0}').value = '{1}';", id, valor));

            //clica fora do ultimo campo para que não de problemas
            ClearClickFiesVelho(driver);

            Sleep();
        }

        /// <summary>
        /// Clica e manda dados ao TextBox.
        /// </summary>
        /// <param name="name">Name do TextBox</param>
        /// <param name="valor">Valor a ser escrito.</param>
        protected void ClickAndWriteByName(IWebDriver driver, string name, string valor)
        {
            IWebElement elementoAtual = driver.FindElement(By.Name(name));

            WaitLogoLoading(driver);

            elementoAtual.Click();

            WaitLogoLoading(driver);

            elementoAtual.SendKeys(valor);

            //clica fora do ultimo campo para que não de problemas
            ClearClickFiesVelho(driver);

            Sleep();
        }

        /// <summary>
        /// Clica pelo CSSSelector.
        /// </summary>
        /// <param css="css">Css do Elemento.</param>
        protected void ClickButtonsByCss(IWebDriver driver, string css)
        {
            WaitLogoLoading(driver);
            driver.FindElement(By.CssSelector(css)).Click();

        }

        /// <summary>
        /// Clica no elemento pelo ID.
        /// </summary>
        /// <param name="id">Id do elemento.</param>
        protected void ClickButtonsById(IWebDriver driver, string id)
        {
            //WaitLogoLoading(driver);
            driver.FindElement(By.Id(id)).Click();

            Sleep();
        }

        /// <summary>
        /// Clica no elemento pelo nome.
        /// </summary>
        /// <param name="name">Nome do elemento.</param>
        protected void ClickButtonsByName(IWebDriver driver, string name)
        {
            WaitLogoLoading(driver);
            driver.FindElement(By.Name(name)).Click();

            Sleep();
        }

        /// <summary>
        /// Clica no elemento por xpath.
        /// </summary>
        /// <param name="xPath">xpath do elemento.</param>
        protected void ClickButtonsByXpath(IWebDriver driver, string xPath)
        {
            WaitLogoLoading(driver);
            driver.FindElement(By.XPath(xPath)).Click();

            Sleep();
        }

        /// <summary>
        /// Inicia nova sessão do browser.
        /// </summary>
        protected IWebDriver StartBrowser(string url, bool downloadFldr = false, bool firefox = true, bool headless = false)
        {
            IWebDriver driver;

            string downloadFolder = "";
            if (downloadFldr == true)
            {
                Util.CriarDiretorioCasoNaoExista("RelatorioExportacao\\");
                downloadFolder = Directory.GetCurrentDirectory() + "\\RelatorioExportacao\\";
            }
            else
            {
                downloadFolder = Util.GetDownloadsFolderPath();
            }

            if (firefox == true)
            {

                //Firefox
                var firefoxDriverService = FirefoxDriverService.CreateDefaultService(Environment.CurrentDirectory + @"\driver");
                firefoxDriverService.HideCommandPromptWindow = true;

                FirefoxOptions firefoxOptions = new FirefoxOptions();
                firefoxOptions.AcceptInsecureCertificates = true;
                if (FormInterface.versaoRobo != "operacoesFinanceiras" || headless == true)
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
                firefoxProfile.SetPreference("dom.enable_window_print", false);
                //firefoxProfile.SetPreference("print.tab_modal.enabled", true);
                firefoxOptions.Profile = firefoxProfile;
                driver = new FirefoxDriver(firefoxDriverService, firefoxOptions);
            }
            else
            {
                //Chrome
                var chromeDriverService = ChromeDriverService.CreateDefaultService(Environment.CurrentDirectory + @"\driver");
                chromeDriverService.HideCommandPromptWindow = true;
                ChromeOptions chromeOptions = new ChromeOptions();
                if (FormInterface.versaoRobo != "operacoesFinanceiras")
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

        /// <summary>
        /// Encerra a sessão atual do browser
        /// </summary>
        /// <param name="driver"></param>
        protected void EndBrowser(IWebDriver driver)
        {
            driver.Quit();
        }

        /// <summary>
        /// Clica no elemento 'body' do site do MEC
        /// </summary>
        /// <param name="driver"></param>
        private void ClearClickFiesVelho(IWebDriver driver)
        {
            if (IsFormFillingFiesVelho(driver))
            {
                driver.FindElement(By.CssSelector("body")).Click();
            }
        }

        /// <summary>
        /// Clica em um elemento dropdown e seleciona o elemento desejado
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="metodo">id, xpath, name, classname</param>
        /// <param name="valorMetodo">valor do metodo selecionado</param>
        /// <param name="valorEscolha">texto que a opção deverá conter</param>
        protected void ClickDropDown(IWebDriver driver, string metodo, string valorMetodo, string valorEscolha)
        {
            WaitLogoLoading(driver);
            driver.FindElement(By.XPath("//select[@" + metodo + "='" + valorMetodo + "']/option[contains(.,'" + valorEscolha + "')]")).Click();

            Sleep();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="metodo">id, xpath, name, classname</param>
        /// <param name="valorMetodo">valor do metodo selecionado</param>
        /// <param name="valorEscolha">texto exato que a opção deverá conter</param>
        protected void ClickDropDownExact(IWebDriver driver, string metodo, string valorMetodo, string valorEscolha)
        {
            driver.FindElement(By.XPath("//select[@" + metodo + "='" + valorMetodo + "']/option[@" + "value ='" + valorEscolha + "']")).Click();

            Sleep();
        }

        /// <summary>
        /// Espera até a página de carregando do site do MEC desaparecer
        /// </summary>
        /// <param name="driver"></param>
        protected void WaitLogoLoading(IWebDriver driver)
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
        /// Checa se a página inicia com a URL de uma página de Aditamento
        /// </summary>
        /// <param name="driver">webdriver</param>
        /// <returns>True se a página é a página de aditamento</returns>
        protected Boolean IsFormFillingFiesVelho(IWebDriver driver)
        {
            //string pagesource = driver.PageSource;
            return driver.Url.StartsWith("http://sisfies.mec.gov.br/cpsa/aditamento/formulario/");

        }

        /// <summary>
        /// Checa se esta na pagina de preencher o formulario
        /// </summary>
        /// <param name="driver">webdriver</param>
        /// <returns></returns>
        protected Boolean IsFormFillingFiesNovo(IWebDriver driver)
        {
            //string pagesource = driver.PageSource;
            return driver.Url.StartsWith("http://sifesweb.caixa.gov.br/fes-web/?session_state=") ||
                   driver.Url.StartsWith("https://sifesweb.caixa.gov.br/fes-web/?session_state=");

        }

        /// <summary>
        /// Fecha o Driver, o remove da memória e dá throw na excessão gerada
        /// </summary>
        /// <param name="motivo"></param>
        /// <param name="Driver"></param>
        protected void getDriverDisposeAndDriverCloseWithThrow(Exception motivo, IWebDriver Driver)
        {
            Driver.Close();
            Driver.Dispose();
            throw motivo;
        }

        /// <summary>
        /// Dá scroll até algum elemento
        /// </summary>
        /// <param name="Driver"></param>
        /// <param name="button">ID do elemento desejado</param>
        protected void ScrollToElementByID(IWebDriver Driver, string button)
        {
            var element = Driver.FindElement(By.Id(button));
            ((IJavaScriptExecutor)Driver).ExecuteScript(string.Format("window.scrollTo({0}, {1})", element.Location.X, element.Location.Y - 150));
        }

        /// <summary>
        /// Dá scroll até algum elemento
        /// </summary>
        /// <param name="Driver"></param>
        /// <param name="button">Xpath do elemento desejado</param>
        protected void ScrollToElementByXpath(IWebDriver Driver, string button)
        {
            var element = Driver.FindElement(By.XPath(button));
            ((IJavaScriptExecutor)Driver).ExecuteScript(string.Format("window.scrollTo({0}, {1})", element.Location.X, element.Location.Y - 100));
        }

        /// <summary>
        /// Verifica se o elemento existe na página
        /// </summary>
        /// <param name="Driver"></param>
        /// <param name="tipo">id, classname ou name</param>
        /// <param name="identificador">elemento desejado</param>
        /// <returns>O elemento caso exista e null caso não exista</returns>
        protected IWebElement VerificarElementoExiste(IWebDriver Driver, string tipo, string identificador)
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

        /// <summary>
        /// Espera até que a propriedade ReadyState da página seja igual a "complete"
        /// </summary>
        /// <param name="Driver"></param>
        protected void WaitPageToLoad(IWebDriver Driver)
        {
            string result = (string)((IJavaScriptExecutor)Driver).ExecuteScript("return document.readyState");
            while (result != "complete")
            {
                System.Threading.Thread.Sleep(100);
                result = (string)((IJavaScriptExecutor)Driver).ExecuteScript("return document.readyState");
            }
        }

        /// <summary>
        /// Pausa a thread atual
        /// </summary>
        protected void Sleep()
        {
            System.Threading.Thread.Sleep(250);
        }

        /// <summary>
        /// Verifica se há algum alerta presente na página
        /// </summary>
        /// <param name="Driver"></param>
        /// <returns>True caso haja false caso não</returns>
        protected bool isAlertPresent(IWebDriver Driver)
        {
            try
            {
                Driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException Ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Clica em algum elemento por Xpath
        /// </summary>
        /// <param name="Driver"></param>
        /// <param name="elemento">Tag HTML do elemento buscado</param>
        /// <param name="atributte">Atributo do elemento buscado</param>
        /// <param name="value">Valor buscado</param>
        protected void ClickElementByXPath(IWebDriver Driver, string elemento, string atributte, string value)
        {
            Driver.FindElement(By.XPath("//" + elemento + "[@" + atributte + "='" + value + "']")).Click();
        }

        /// <summary>
        /// Busca o texto de um elemento por Xpath
        /// </summary>
        /// <param name="Driver"></param>
        /// <param name="elemento">Tag HTML do elemento buscado</param>
        /// <param name="atributte">Atributo do elemento buscado</param>
        /// <param name="value">Valor buscado</param>
        /// <returns>Texto encontrado</returns>
        protected string TakeTextByXPath(IWebDriver Driver, string elemento, string atributte, string value)
        {
            return Driver.FindElement(By.XPath("//" + elemento + "[@" + atributte + "='" + value + "']")).Text;
        }

        /// <summary>
        /// Busca um elemento por xpath à partir do texto
        /// </summary>
        /// <param name="tag">Tag HTML</param>
        /// <param name="texto">Texto que deve existir no elemento</param>
        /// <returns></returns>
        protected IWebElement FindElementByXpathText(IWebDriver Driver, string tag, string texto)
        {
            return Driver.FindElement(By.XPath("//" + tag + "[contains(text(),'" + texto + "')]"));
        }
    }
}

