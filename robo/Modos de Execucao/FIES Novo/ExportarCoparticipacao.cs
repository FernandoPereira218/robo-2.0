using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using robo.Utils;
using robo.Contratos;

namespace robo.Modos_de_Execucao.FIES_Novo
{
    public class ExportarCoparticipacao : UtilFiesNovo, IModosDeExecucao.IModoSemAlunos
    {
        private string IES;
        private string dataInicial;
        private string dataFinal;
        public ExportarCoparticipacao(string IES, string dataInicial, string dataFinal)
        {
            this.IES = IES;
            this.dataInicial = dataInicial;
            this.dataFinal = dataFinal;
        }

        public void Executar()
        {
            ExportarRelatorioCoparticipacao();
        }

        public void ExportarRelatorioCoparticipacao()
        {
            EsperarPaginaCarregando();

            SelecionarOpcaoMenu();

            ClickAndWriteById( "dataInicio", dataInicial);
            ClickAndWriteById( "dataFim", dataFinal);
            ClickButtonsById( "btnExportarRelatorio");
            EsperarPaginaCarregando();
            string erro = BuscarMensagemDeErro();
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

        public void SelecionarMenu()
        {
            ClicarMenuCoparticipacao();
            EsperarPaginaCarregando();
        }

        public void SetWebDriver(IWebDriver Driver)
        {
            this.Driver = Driver;
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
                    while (isAlertPresent())
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
