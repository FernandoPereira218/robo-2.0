using OpenQA.Selenium;
using Robo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robo.Control.Relatorios.FIES_Novo
{
    public class ExportarRepasse : UtilFiesNovo
    {
        private IWebDriver Driver;
        public void ExportarRepasseFiesNovo(IWebDriver driver, string ano, string mes)
        {
            Driver = driver;
            ClickDropDown(Driver, "id", "selectMes", mes);
            ClickDropDown(Driver, "id", "selectAno", ano);

            ClickButtonsById(Driver, "btnConsultar");
            WaitForLoading(Driver);

            ClickButtonsById(Driver, "btnExportar");
            WaitForLoading(Driver);
        }
    }
}
