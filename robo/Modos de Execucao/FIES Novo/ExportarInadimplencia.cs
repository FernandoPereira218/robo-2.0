using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using robo.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robo.Modos_de_Execucao.FIES_Novo
{
    class ExportarInadimplencia : UtilFiesNovo
    {
        public void Inadimplencia(string mes, string ano)
        {
            ClickDropDown( "id", "selectMesMovimento", mes);
            ClickDropDown( "id", "selectAnoMovimento", ano);

            ClickButtonsById( "btnConsultar");
            WaitForLoading();

            ClickButtonsById( "btnExportar");
            WaitForLoading();

            if (Driver.PageSource.Contains("Nenhuma informação disponível") == true)
            {
                throw new Exception("Nenhuma informação disponível");
            }
            Util.ExportarDocumento("Inadimplência", nomeArquivo: mes + "_" + ano + ".xls");
        }

        public void Inadimplencia(IWebDriver driver)
        {
            Driver = driver;
            SelectElement selectMes = new SelectElement(Driver.FindElement(By.Id("selectMesMovimento")));
            SelectElement selectAno = new SelectElement(Driver.FindElement(By.Id("selectAnoMovimento")));
            int anoSelecionado = Convert.ToInt32(selectAno.Options[1].Text);
            foreach (var ano in selectAno.Options)
            {
                if (anoSelecionado == 2017)
                {
                    break;
                }
                ClickDropDown( "id", "selectAnoMovimento", anoSelecionado.ToString());
                int contador = 12;
                foreach (var mes in selectMes.Options)
                {
                    if (contador == 0)
                    {
                        break;
                    }
                    string mesSelecionado = selectMes.Options[contador].Text;
                    ClickDropDown( "id", "selectMesMovimento", mesSelecionado);

                    ClickButtonsById( "btnConsultar");
                    WaitForLoading();

                    if (Driver.PageSource.Contains("Nenhuma informação disponível") == true)
                    {
                        contador--;
                        continue;
                    }

                    ClickButtonsById( "btnExportar");
                    WaitForLoading();

                    Util.ExportarDocumento("Inadimplência", nomeArquivo: mesSelecionado + "_" + anoSelecionado + ".xls");

                    contador--;
                }
                anoSelecionado--;
            }
        }
    }
}
