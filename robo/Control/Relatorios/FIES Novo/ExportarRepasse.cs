using OpenQA.Selenium;
using Robo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robo.Control.Relatorios.FIES_Novo
{
    public class ExportarRepasse
    {
        private IWebDriver Driver;
        UtilFiesNovo utilFiesNovo = new UtilFiesNovo();
        public void ExportarRepasseFiesNovo(IWebDriver driver, string ano, string mes)
        {
            Driver = driver;
            Util.ClickDropDown(Driver, "id", "selectMes", mes);
            Util.ClickDropDown(Driver, "id", "selectAno", ano);

            Util.ClickButtonsById(Driver, "btnConsultar");
            utilFiesNovo.WaitForLoading(Driver);

            Util.ClickButtonsById(Driver, "btnExportar");
            utilFiesNovo.WaitForLoading(Driver);
        }
    }
}
