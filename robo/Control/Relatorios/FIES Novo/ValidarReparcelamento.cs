using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robo.Control.Relatorios.FIES_Novo
{
    class ValidarReparcelamento : UtilFiesNovo
    {
        private IWebDriver Driver;
        private WebDriverWait wait;
        public void ExecutarValidarReparcelamento(IWebDriver driver)
        {
            Driver = driver;
            WaitForLoading(Driver);
            EsperarElementoExiste(By.Id("divResultado"));

            IWebElement divResultado = Driver.FindElement(By.Id("divResultado"));
            if (divResultado.Displayed == true)
            {
                EsperarElementoFicarInvisivel(By.Id("divResultado"));
            }
        }

        private void EsperarElementoFicarInvisivel(By tipo)
        {
            wait = new WebDriverWait(Driver, TimeSpan.FromMinutes(5));
            wait.Until(x => x.FindElement(tipo).Displayed == false);
        }

        private void EsperarElementoExiste(By tipo)
        {
            wait = new WebDriverWait(Driver, TimeSpan.FromMinutes(5));
            wait.Until(x => x.FindElement(tipo) != null);
            
        }
    }
}
