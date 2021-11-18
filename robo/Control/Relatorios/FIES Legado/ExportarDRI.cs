using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using robo.Control.Legado;
using Robo;

namespace robo.Control.Relatorios.FIES_Legado
{
    public class ExportarDRI : UtilFiesLegado
    {
        private IWebDriver Driver;
        public void ExportarDRILegado(IWebDriver driver, string campus, string situacaoDRI)
        {
            Driver = driver;

            ClickDropDown(Driver, "id", "co_situacao_inscricao", situacaoDRI);

            ClickButtonsById(Driver, "excel");

            if (Driver.PageSource.Contains("Nenhuma informação disponível") == true)
            {
                throw new Exception("Nenhuma informação disponível");
            }

            //SalvarArquivos(Driver, "DRI_" + situacaoDRI, campus);
            Util.ExportarDocumento("DRI_" + situacaoDRI, campus);
        }
    }
}
