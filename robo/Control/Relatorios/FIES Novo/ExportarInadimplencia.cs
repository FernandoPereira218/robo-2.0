using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Robo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robo.Control.Relatorios.FIES_Novo
{
    class ExportarInadimplencia : UtilFiesNovo
    {
        private IWebDriver Driver;
        public void Inadimplencia(IWebDriver driver, string mes, string ano)
        {
            Driver = driver;
            ClickDropDown(Driver, "id", "selectMesMovimento", mes);
            ClickDropDown(Driver, "id", "selectAnoMovimento", ano);

            ClickButtonsById(Driver, "btnConsultar");
            WaitForLoading(Driver);

            ClickButtonsById(Driver, "btnExportar");
            WaitForLoading(Driver);

            //SalvarArquivos(Driver, "Inadimplência", nomeArquivo: mes + "_" + ano + ".xls");
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
                ClickDropDown(Driver, "id", "selectAnoMovimento", anoSelecionado.ToString());
                int contador = 12;
                foreach (var mes in selectMes.Options)
                {
                    if (contador == 0)
                    {
                        break;
                    }
                    string mesSelecionado = selectMes.Options[contador].Text;
                    ClickDropDown(Driver, "id", "selectMesMovimento", mesSelecionado);

                    ClickButtonsById(Driver, "btnConsultar");
                    WaitForLoading(Driver);

                    if (Driver.PageSource.Contains("Nenhuma informação disponível") == true)
                    {
                        contador--;
                        continue;
                    }

                    ClickButtonsById(Driver, "btnExportar");
                    WaitForLoading(Driver);

                    //SalvarArquivos(Driver, "Inadimplência", nomeArquivo: mesSelecionado + "_" + anoSelecionado + ".xls");
                    Util.ExportarDocumento("Inadimplência", nomeArquivo: mesSelecionado + "_" + anoSelecionado + ".xls");

                    contador--;
                }
                anoSelecionado--;
            }
        }
    }
}
