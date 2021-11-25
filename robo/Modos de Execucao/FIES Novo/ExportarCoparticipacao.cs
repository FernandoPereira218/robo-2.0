using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using robo.Utils;

namespace robo.Modos_de_Execucao.FIES_Novo
{
    public class ExportarCoparticipacao : UtilFiesNovo
    {
        private IWebDriver Driver;
        public void ExportarRelatórioCoparticipacao(IWebDriver driver, string IES, string dataInicial, string dataFinal)
        {
            Driver = driver;
            WaitForLoading(driver);

            SelecionarOpcaoMenu();

            ClickAndWriteById(Driver, "dataInicio", dataInicial);
            ClickAndWriteById(Driver, "dataFim", dataFinal);
            ClickButtonsById(Driver, "btnExportarRelatorio");
            WaitForLoading(Driver);
            string erro = BuscarMensagemDeErro(Driver);
            if (erro == string.Empty)
            {
                if (Driver.PageSource.Contains("Nenhuma informação disponível") == true)
                {
                    throw new Exception("Nenhuma informação disponível");
                }
                Util.ExportarDocumento("COPARTICIPAÇÃO", nomeArquivo: IES + "_" +
                    Convert.ToDateTime(dataInicial).ToString("dd-MM-yyyy") + " - " +
                    Convert.ToDateTime(dataFinal).ToString("dd-MM-yyyy") + ".xls");
            }
            else
            {
                throw new Exception(erro);
            }
        }

        private void SelecionarOpcaoMenu()
        {
            try
            {
                SelectElement select = new SelectElement(Driver.FindElement(By.Id("ies")));
                if (select.Options.Count > 2)
                {
                    IWebElement dropDownIES = Driver.FindElement(By.Id("ies_chosen"));
                    dropDownIES.Click();
                    ((IJavaScriptExecutor)Driver).ExecuteScript("alert(\"Por favor selecione uma IES\")");
                    while (isAlertPresent(Driver))
                    {
                        System.Threading.Thread.Sleep(100);
                    }
                    while (dropDownIES.Text.ToUpper().Contains("SELECIONE"))
                    {
                        System.Threading.Thread.Sleep(100);
                    }
                }
                else if (select.Options.Count == 2)
                {
                    IWebElement dropDownIES = Driver.FindElement(By.Id("ies_chosen"));
                    dropDownIES.Click();
                    IWebElement opcao = Driver.FindElement(By.XPath("//li[@data-option-array-index=1]"));
                    opcao.Click();
                }
            }
            catch (NoSuchElementException)
            {

            }

        }
    }
}
