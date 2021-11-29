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
        public void ExportarRepasseFiesNovo(string ano, string mes)
        {
            ClickDropDown( "id", "selectMes", mes);
            ClickDropDown( "id", "selectAno", ano);

            ClickButtonsById( "btnConsultar");
            WaitForLoading();

            ClickButtonsById( "btnExportar");
            WaitForLoading();

            if (Driver.PageSource.Contains("Nenhuma informação disponível") == true)
            {
                throw new Exception("Nenhuma informação disponível");
            }
            Util.ExportarDocumento("Repasse", nomeArquivo: mes + "_" + ano + ".xls");
        }
    }
}
