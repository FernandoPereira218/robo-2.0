using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using robo.Utils;

namespace robo.Modos_de_Execucao.FIES_Legado
{
    public class ExportarDRI : UtilFiesLegado
    {
        public void ExportarDRILegado(string campus, string situacaoDRI)
        {
            ClickDropDown( "id", "co_situacao_inscricao", situacaoDRI);

            ClickButtonsById( "excel");

            Util.ExportarDocumento("DRI_" + situacaoDRI, campus);
        }
    }
}
