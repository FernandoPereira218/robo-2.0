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
        /// Clica em um campo de texto e escreve o valor enviado
        /// </summary>
        /// <param name="by">Elemento que deve ser clicado</param>
        /// <param name="valor">O que deve ser escrito</param>
        protected void ClicarEEscrever(By by, string valor)
        {
            IWebElement elementoAtual = Driver.FindElement(by);
            elementoAtual.Click();
            elementoAtual.Clear();
            elementoAtual.SendKeys(valor);
            Sleep();
        }

        /// <summary>
        /// Clica em algum elemento da página HTML
        /// </summary>
        /// <param name="by">Elemento que deve ser clicado</param>
        protected void ClicarElemento(By by)
        {
            Driver.FindElement(by).Click();
            Sleep();
        }

        /// <summary>
        /// Clica em um elemento dropdown e seleciona o elemento desejado
        /// </summary>
        /// <param name="metodo">id, xpath, name, classname</param>
        /// <param name="valorMetodo">valor do metodo selecionado</param>
        /// <param name="valorEscolha">texto que a opção deverá conter</param>
        protected void SelecionarOpcaoDropDown(string metodo, string valorMetodo, string valorEscolha)
        {
            Driver.FindElement(By.XPath("//select[@" + metodo + "='" + valorMetodo + "']/option[contains(.,'" + valorEscolha + "')]")).Click();
            Sleep();
        }

        /// <summary>
        /// Clica na opção baseada em seu texto
        /// </summary>
        /// <param name="metodo">id, xpath, name, classname</param>
        /// <param name="valorMetodo">valor do metodo selecionado</param>
        /// <param name="valorEscolha">texto exato que a opção deverá conter</param>
        protected void SelecionarOpcaoDropDownExato(string metodo, string valorMetodo, string valorEscolha)
        {
            Driver.FindElement(By.XPath("//select[@" + metodo + "='" + valorMetodo + "']/option[@" + "value ='" + valorEscolha + "']")).Click();
            Sleep();
        }

        /// <summary>
        /// Dá scroll até algum elemento
        /// </summary>
        /// <param name="by">Elemento desejado</param>
        protected void ScrollParaElemento(By by)
        {
            var element = Driver.FindElement(by);
            ((IJavaScriptExecutor)Driver).ExecuteScript(string.Format("window.scrollTo({0}, {1})", element.Location.X, element.Location.Y - 150));
        }

        /// <summary>
        /// Verifica se um elemento HTML existe na página
        /// </summary>
        /// <param name="by">Elemento desejado</param>
        /// <returns>Elemento encontrado ou null se não existir</returns>
        protected IWebElement VerificarElementoExiste(By by)
        {
            try
            {
                IWebElement elemento = Driver.FindElement(by);
                return elemento;
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }

        /// <summary>
        /// Espera até que a propriedade ReadyState da página seja igual a "complete"
        /// </summary>
        /// <param name="Driver"></param>
        protected void EsperarReadyState()
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
        protected bool VerificarAlertaNavegador()
        {
            try
            {
                Driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        /// <summary>
        /// Clica em algum elemento por Xpath
        /// </summary>
        /// <param name="elemento">Tag HTML do elemento buscado</param>
        /// <param name="atributo">Atributo do elemento buscado</param>
        /// <param name="valor">Valor buscado</param>
        protected void ClicarElementoComXpath(string elemento, string atributo, string valor)
        {
            Driver.FindElement(By.XPath("//" + elemento + "[@" + atributo + "='" + valor + "']")).Click();
        }

        /// <summary>
        /// Busca um elemento por xpath à partir do texto
        /// </summary>
        /// <param name="tag">Tag HTML</param>
        /// <param name="texto">Texto que deve existir no elemento</param>
        /// <returns></returns>
        protected IWebElement BuscarElementoPorTextoXpath(string tag, string texto)
        {
            return Driver.FindElement(By.XPath("//" + tag + "[contains(text(),'" + texto + "')]"));
        }
    }
}

