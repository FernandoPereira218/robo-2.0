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
    public class ExportarRelatorios
    {
        private IWebDriver Driver;
        private UtilFiesLegado fiesLegadoutil = new UtilFiesLegado();
        public void ExportarDocumentosFiesLegado(IWebDriver driver, string semestre, string tipoRelatorio, string campus)
        {
            Driver = driver;
            string selRelatorio = fiesLegadoutil.SelecionarTipoRelatorio(Driver, tipoRelatorio);
            Util.ClickDropDown(Driver, "id", "co_finalidade_aditamento", selRelatorio);
            fiesLegadoutil.WaitinLoading(Driver);
            Util.ClickDropDown(Driver, "id", "coSemestreAditamento", semestre);

            semestre = semestre.Replace('/', '-');
            Driver.FindElement(By.Name("export-excel")).Click();
            Util.SalvarArquivos(Driver, tipoRelatorio, campus, semestre);
        }
    }
}
