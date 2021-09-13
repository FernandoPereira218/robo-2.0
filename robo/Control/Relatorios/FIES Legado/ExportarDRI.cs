using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Robo;

namespace robo.Control.Relatorios.FIES_Legado
{
    public class ExportarDRI
    {
        private IWebDriver Driver;
        public void ExportarDRILegado(IWebDriver driver, string campus, string situacaoDRI)
        {
            Driver = driver;

            Util.ClickDropDown(Driver, "id", "co_situacao_inscricao", situacaoDRI);

            Util.ClickButtonsById(Driver, "excel");

            Util.SalvarArquivos(Driver, "DRI_" + situacaoDRI, campus);
        }
    }
}
