using OpenQA.Selenium;
using robo.Control.Legado;
using Robo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robo.Control.Relatorios
{
    public class ExportarRelatorios : UtilFiesLegado
    {
        private IWebDriver Driver;
        public void ExportarDocumentosFiesLegado(IWebDriver driver, string semestre, string tipoRelatorio, string campus)
        {
            Driver = driver;
            string selRelatorio = SelecionarTipoRelatorio(Driver, tipoRelatorio);
            ClickDropDown(Driver, "id", "co_finalidade_aditamento", selRelatorio);
            WaitinLoading(Driver);
            ClickDropDown(Driver, "id", "coSemestreAditamento", semestre);

            semestre = semestre.Replace('/', '-');
            Driver.FindElement(By.Name("export-excel")).Click();
            Util.ExportarDocumento(tipoRelatorio, campus, semestre);
        }
    }
}
