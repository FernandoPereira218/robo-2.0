using OpenQA.Selenium;
using robo.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using robo.Contratos;

namespace robo.Modos_de_Execucao.FIES_Novo
{
    public class ExportarRepasse : UtilFiesNovo, IModosDeExecucao.IModoSemAlunos
    {
        private string ano;
        private string mes;
        public ExportarRepasse(string ano, string mes)
        {
            this.ano = ano;
            this.mes = mes;
        }

        public void Executar()
        {
            ExportarRepasseFiesNovo();
        }

        public void ExportarRepasseFiesNovo()
        {
            ClickDropDown( "id", "selectMes", mes);
            ClickDropDown( "id", "selectAno", ano);

            ClickButtonsById( "btnConsultar");
            EsperarPaginaCarregando();

            ClickButtonsById( "btnExportar");
            EsperarPaginaCarregando();

            if (Driver.PageSource.Contains("Nenhuma informação disponível") == true)
            {
                throw new Exception("Nenhuma informação disponível");
            }
            Util.ExportarDocumento("Repasse", nomeArquivo: mes + "_" + ano + ".xls");
        }

        public void SelecionarMenu()
        {
            ClicarMenuRepasse();
            EsperarPaginaCarregando();
        }

        public void SetWebDriver(IWebDriver Driver)
        {
            this.Driver = Driver;
        }
    }
}
