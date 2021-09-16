using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Robo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robo.Control.Relatorios.FIES_Novo
{
    class ExportarInadimplencia
    {
        private IWebDriver Driver;
        UtilFiesNovo utilFiesNovo = new UtilFiesNovo();
        public void Inadimplencia(IWebDriver driver, string mes, string ano)
        {
            Driver = driver;
            Util.ClickDropDown(Driver, "id", "selectMesMovimento", mes);
            Util.ClickDropDown(Driver, "id", "selectAnoMovimento", ano);

            Util.ClickButtonsById(Driver, "btnConsultar");
            utilFiesNovo.WaitForLoading(Driver);

            Util.ClickButtonsById(Driver, "btnExportar");
            utilFiesNovo.WaitForLoading(Driver);
        }

        public void Inadimplencia(IWebDriver driver)
        {
            Driver = driver;
            SelectElement selectMes = new SelectElement(Driver.FindElement(By.Id("selectMesMovimento")));
            SelectElement selectAno = new SelectElement(Driver.FindElement(By.Id("selectAnoMovimento")));
            int anoSelecionado = Convert.ToInt32(selectAno.Options[1].Text);
            foreach (var ano in selectAno.Options)
            {
                if (anoSelecionado == 2017)
                {
                    break;
                }
                Util.ClickDropDown(Driver, "id", "selectAnoMovimento", anoSelecionado.ToString());
                int contador = 12;
                foreach (var mes in selectMes.Options)
                {
                    if (contador == 0)
                    {
                        break;
                    }
                    Util.ClickDropDown(Driver, "id", "selectMesMovimento", selectMes.Options[contador].Text);

                    Util.ClickButtonsById(Driver, "btnConsultar");
                    utilFiesNovo.WaitForLoading(Driver);

                    Util.ClickButtonsById(Driver, "btnExportar");
                    utilFiesNovo.WaitForLoading(Driver);

                    contador--;
                }
                anoSelecionado--;
            }
        }
    }
}
