using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robo;

namespace robo.Control.Relatorios.FIES_Novo
{
    public class ExportarCoparticipacao : UtilFiesNovo
    {
        private IWebDriver Driver;
        public void ExportarRelatórioCoparticipacao(IWebDriver driver, string IES, string dataInicial, string dataFinal)
        {
            Driver = driver;
            WaitForLoading(driver);
            if (IES.ToUpper().Equals("UNIRITTER") || IES.ToUpper().Equals("FADERGS"))
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
            ClickAndWriteById(Driver, "dataInicio", dataInicial);
            ClickAndWriteById(Driver, "dataFim", dataFinal);
            ClickButtonsById(Driver, "btnExportarRelatorio");
            string erro = BuscarMensagemDeErro(Driver);
            if (erro == string.Empty)
            {
                SalvarArquivos(Driver, "COPARTICIPAÇÃO", nomeArquivo: IES + "_" + Convert.ToDateTime(dataInicial).ToString("dd-MM-yyyy") + " - " + Convert.ToDateTime(dataFinal).ToString("dd-MM-yyyy") + ".xls");
            }
            else
            {
                throw new Exception(erro);
            }
        }
    }
}
