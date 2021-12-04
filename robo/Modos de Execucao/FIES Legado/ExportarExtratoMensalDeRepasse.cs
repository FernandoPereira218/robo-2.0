using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using robo.Utils;
using robo.Contratos;

namespace robo.Modos_de_Execucao.FIES_Legado
{
    class ExportarExtratoMensalDeRepasse : UtilFiesLegado, IModosDeExecucao.IModoSemAlunos
    {
        private string campus;
        private string ano;
        private string mes;
        public ExportarExtratoMensalDeRepasse(string campus, string ano, string mes)
        {
            this.campus = campus;
            this.ano = ano;
            this.mes = mes;
        }

        public void Executar()
        {
            ExtratoMensalDeRepasseLegado();
        }

        public void ExtratoMensalDeRepasseLegado()
        {
            ClickDropDown( "id", "nu_ano", ano);
            ClickDropDown( "id", "nu_mes", mes);
            SelectElement select = new SelectElement(Driver.FindElement(By.Id("dt_repasse")));
            if (select.Options.Count > 2)
            {
                EsperarSelecaoIES(select);
            }
            else if (select.Options.Count == 1)
            {
                throw new Exception("IES não disponível.");
            }
            else
            {
                select.SelectByIndex(1);
            }
            Driver.FindElement(By.Id("btn_excel")).Click();
            Util.ExportarDocumento("Extrato_Mensal_Repasse_", campus);
        }

        public void SelecionarMenu()
        {
            SelecionarMenuExtratoMensalDeRepasse();
        }

        public void SetWebDriver(IWebDriver Driver)
        {
            this.Driver = Driver;
        }

        private void EsperarSelecaoIES(SelectElement select)
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript("alert(\"Por favor selecione uma data\")");
            while (isAlertPresent())
            {
                System.Threading.Thread.Sleep(100);
            }
            while (select.SelectedOption.Text == "Selecione")
            {
                System.Threading.Thread.Sleep(500);
            }
        }
    }
}
