using OpenQA.Selenium;
using robo.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robo.Modos_de_Execucao.FIES_Novo
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

            if (Driver.PageSource.Contains("Nenhuma informação disponível") == true)
            {
                throw new Exception("Nenhuma informação disponível");
            }
            Util.ExportarDocumento("Repasse", nomeArquivo: mes + "_" + ano + ".xls");
        }
    }
}
