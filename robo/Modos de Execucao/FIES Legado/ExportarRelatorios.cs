using OpenQA.Selenium;
using robo.Contratos;
using robo.TO;
using robo.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robo.Modos_de_Execucao.FIES_Legado
{
    public class ExportarRelatorios : UtilFiesLegado, IModosDeExecucao.IModoSemAlunos, IModosDeExecucao.IFiesLegado
    {
        private string semestre;
        private string tipoRelatorio;
        private string campus;
        public ExportarRelatorios(string semestre, string tipoRelatorio, string campus)
        {
            this.semestre = semestre;
            this.tipoRelatorio = tipoRelatorio;
            this.campus = campus;
        }

        public void Executar()
        {
            ExportarDocumentosFiesLegado();
        }

        public void ExportarDocumentosFiesLegado()
        {
            string selRelatorio = SelecionarTipoRelatorio( tipoRelatorio);
            ClickDropDown("id", "co_finalidade_aditamento", selRelatorio);
            WaitinLoading();
            ClickDropDown( "id", "coSemestreAditamento", semestre);

            string nomeSemestre = semestre.Replace('/', '-');
            Driver.FindElement(By.Name("export-excel")).Click();
            Util.ExportarDocumento(tipoRelatorio, campus, nomeSemestre);
        }

        public void SelecionarMenu()
        {
            SelecionarMenuBaixarDocumentos();
        }

        public void SetWebDriver(IWebDriver Driver)
        {
            this.Driver = Driver;
        }

        public void TrocarCampus(string campus)
        {
            this.campus = campus;
        }
    }
}
