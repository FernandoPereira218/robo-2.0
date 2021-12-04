using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using robo.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using robo.Contratos;

namespace robo.Modos_de_Execucao.FIES_Novo
{
    class ValidarReparcelamento : UtilFiesNovo, IModosDeExecucao.IModoSemAlunos
    {
        private WebDriverWait wait;
        public void ExecutarValidarReparcelamento()
        {
            WaitForLoading();
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

        public void Executar()
        {
            ExecutarValidarReparcelamento();
        }

        public void SelecionarMenu()
        {
            ClicarMenuValidarReparcelamento();
            WaitForLoading();
        }

        public void SetWebDriver(IWebDriver Driver)
        {
            this.Driver = Driver;
        }
    }
}
