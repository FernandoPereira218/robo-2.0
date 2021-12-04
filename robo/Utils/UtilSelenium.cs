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
        protected IWebDriver Driver;

        public void SetDriver(IWebDriver Driver)
        {
            this.Driver = Driver;
        }

        /// <summary>
        /// Espera até um elemento específico ser visível
        /// </summary>
        /// <param name="tipo">By.(Id, Xpath, Css)("elementoDesejado")</param>
        /// <param name="segundos">Tempo limite de espera em segundos</param>
        protected void EsperarElementoVisivel(By tipo, int segundos = 60)
        {
            TimeSpan span = TimeSpan.FromSeconds(segundos);
            wait = new WebDriverWait(Driver, span);
            wait.Until(ExpectedConditions.ElementIsVisible(tipo));
        }
        /// <summary>
        /// Clica e manda dados ao TextBox.
        /// </summary>
        /// <param name="id">ID do TextBox</param>
        /// <param name="valor">Valor a ser escrito.</param>
        protected void ClickAndWriteById(string id, string valor)
        {
            IWebElement elementoAtual = Driver.FindElement(By.Id(id));
            //WaitLogoLoading();
            elementoAtual.Click();
            //WaitLogoLoading();
            elementoAtual.Clear();
            elementoAtual.SendKeys(valor);
            Sleep();
        }

        /// <summary>
        /// Clica e manda dados ao TextBox.
        /// </summary>
        /// <param name="nome">Name do TextBox</param>
        /// <param name="valor">Valor a ser escrito.</param>
        protected void ClickAndWriteByName(string nome, string valor)
        {
            IWebElement elementoAtual = Driver.FindElement(By.Name(nome));
            //WaitLogoLoading();
            elementoAtual.Click();
            //WaitLogoLoading();
            elementoAtual.SendKeys(valor);
            Sleep();
        }

        /// <summary>
        /// Clica pelo CSSSelector.
        /// </summary>
        /// <param css="css">Css do Elemento.</param>
        protected void ClickButtonsByCss(string css)
        {
            //WaitLogoLoading();
            Driver.FindElement(By.CssSelector(css)).Click();
        }

        /// <summary>
        /// Clica no elemento pelo ID.
        /// </summary>
        /// <param name="id">Id do elemento.</param>
        protected void ClickButtonsById(string id)
        {
            Driver.FindElement(By.Id(id)).Click();
            Sleep();
        }

        /// <summary>
        /// Clica no elemento pelo nome.
        /// </summary>
        /// <param name="nome">Nome do elemento.</param>
        protected void ClickButtonsByName(string nome)
        {
            //WaitLogoLoading();
            Driver.FindElement(By.Name(nome)).Click();
            Sleep();
        }

        /// <summary>
        /// Clica no elemento por xpath.
        /// </summary>
        /// <param name="xPath">xpath do elemento.</param>
        protected void ClickButtonsByXpath(string xPath)
        {
            //WaitLogoLoading();
            Driver.FindElement(By.XPath(xPath)).Click();
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
        /// Clica em um elemento dropdown e seleciona o elemento desejado
        /// </summary>
        /// <param name="Driver"></param>
        /// <param name="metodo">id, xpath, name, classname</param>
        /// <param name="valorMetodo">valor do metodo selecionado</param>
        /// <param name="valorEscolha">texto que a opção deverá conter</param>
        protected void ClickDropDown(string metodo, string valorMetodo, string valorEscolha)
        {
            //WaitLogoLoading();
            Driver.FindElement(By.XPath("//select[@" + metodo + "='" + valorMetodo + "']/option[contains(.,'" + valorEscolha + "')]")).Click();
            Sleep();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Driver"></param>
        /// <param name="metodo">id, xpath, name, classname</param>
        /// <param name="valorMetodo">valor do metodo selecionado</param>
        /// <param name="valorEscolha">texto exato que a opção deverá conter</param>
        protected void ClickDropDownExact(string metodo, string valorMetodo, string valorEscolha)
        {
            Driver.FindElement(By.XPath("//select[@" + metodo + "='" + valorMetodo + "']/option[@" + "value ='" + valorEscolha + "']")).Click();
            Sleep();
        }

        /// <summary>
        /// Dá scroll até algum elemento
        /// </summary>
        /// <param name="Driver"></param>
        /// <param name="id">ID do elemento desejado</param>
        protected void ScrollToElementByID(string id)
        {
            var element = Driver.FindElement(By.Id(id));
            ((IJavaScriptExecutor)Driver).ExecuteScript(string.Format("window.scrollTo({0}, {1})", element.Location.X, element.Location.Y - 150));
        }

        /// <summary>
        /// Dá scroll até algum elemento
        /// </summary>
        /// <param name="Driver"></param>
        /// <param name="xpath">Xpath do elemento desejado</param>
        protected void ScrollToElementByXpath(string xpath)
        {
            var element = Driver.FindElement(By.XPath(xpath));
            ((IJavaScriptExecutor)Driver).ExecuteScript(string.Format("window.scrollTo({0}, {1})", element.Location.X, element.Location.Y - 100));
        }

        /// <summary>
        /// Verifica se o elemento existe na página
        /// </summary>
        /// <param name="Driver"></param>
        /// <param name="tipo">id, classname ou name</param>
        /// <param name="identificador">elemento desejado</param>
        /// <returns>O elemento caso exista e null caso não exista</returns>
        protected IWebElement VerificarElementoExiste(string tipo, string identificador)
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
        protected void WaitPageToLoad()
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
        protected bool isAlertPresent()
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
        /// <param name="atributo">Atributo do elemento buscado</param>
        /// <param name="value">Valor buscado</param>
        protected void ClickElementByXPath(string elemento, string atributo, string value)
        {
            Driver.FindElement(By.XPath("//" + elemento + "[@" + atributo + "='" + value + "']")).Click();
        }

        /// <summary>
        /// Busca o texto de um elemento por Xpath
        /// </summary>
        /// <param name="Driver"></param>
        /// <param name="elemento">Tag HTML do elemento buscado</param>
        /// <param name="atributo">Atributo do elemento buscado</param>
        /// <param name="value">Valor buscado</param>
        /// <returns>Texto encontrado</returns>
        protected string TakeTextByXPath(string elemento, string atributo, string value)
        {
            return Driver.FindElement(By.XPath("//" + elemento + "[@" + atributo + "='" + value + "']")).Text;
        }

        /// <summary>
        /// Busca um elemento por xpath à partir do texto
        /// </summary>
        /// <param name="tag">Tag HTML</param>
        /// <param name="texto">Texto que deve existir no elemento</param>
        /// <returns></returns>
        protected IWebElement FindElementByXpathText(string tag, string texto)
        {
            return Driver.FindElement(By.XPath("//" + tag + "[contains(text(),'" + texto + "')]"));
        }
    }
}

