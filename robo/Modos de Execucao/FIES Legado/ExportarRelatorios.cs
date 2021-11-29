using OpenQA.Selenium;
using robo.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robo.Modos_de_Execucao.FIES_Legado
{
    public class ExportarRelatorios : UtilFiesLegado
    {
        public void ExportarDocumentosFiesLegado(string semestre, string tipoRelatorio, string campus)
        {
            string selRelatorio = SelecionarTipoRelatorio( tipoRelatorio);
            ClickDropDown("id", "co_finalidade_aditamento", selRelatorio);
            WaitinLoading();
            ClickDropDown( "id", "coSemestreAditamento", semestre);

            semestre = semestre.Replace('/', '-');
            Driver.FindElement(By.Name("export-excel")).Click();
            Util.ExportarDocumento(tipoRelatorio, campus, semestre);
        }
    }
}
