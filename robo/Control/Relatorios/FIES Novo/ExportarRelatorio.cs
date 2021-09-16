using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robo;
using System.IO;

namespace robo.Control.Relatorios.FIES_Novo
{
    public class ExportarRelatorio
    {
        private IWebDriver Driver;
        private UtilFiesNovo utilFiesNovo = new UtilFiesNovo();
        public void ExportarRelatorioFiesNovo(IWebDriver driver, string tipoRelatorio)
        {
            Driver = driver;
            Util.ClickButtonsById(Driver, "btnConsultar");
            utilFiesNovo.WaitForLoading(Driver);

            Util.ClickDropDown(Driver, "name", "gridResult_length", "100");
            string source = Driver.PageSource.Split(new string[] { "Mostrando" }, StringSplitOptions.None)[1];
            source = source.Split(new string[] { "registros" }, StringSplitOptions.None)[0];
            string quantidade = source.Split(new string[] { "de " }, StringSplitOptions.None)[1];
            int qtdLinhas = Convert.ToInt32(quantidade);
            int qtdPaginas = Convert.ToInt32(Math.Ceiling(qtdLinhas / 100f));
            StringBuilder sb = new StringBuilder();
            string pagina = string.Empty;
            for (int i = 0; i < qtdPaginas; i++)
            {
                if (i == 0)
                {
                    sb.Append(ListaParaString("gridResult_length", "gridResult", true, pagina));
                }
                else
                {
                    sb.Append(ListaParaString("gridResult_length", "gridResult", false, pagina));
                }
                if (qtdPaginas > 1)
                {
                    IWebElement sAdminMenuJobTitle = Driver.FindElement(By.XPath(string.Format("//*[@id=\"{0}\"]", "gridResult_next")));
                    ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", sAdminMenuJobTitle);
                }
                
            }

            String userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadFolder = System.IO.Path.Combine(userRoot, "Downloads");
            string arquivo = downloadFolder + "\\Relatório_" + tipoRelatorio + ".csv";
            using (StreamWriter sw = new StreamWriter(arquivo, false, UTF8Encoding.UTF8))
            {
                sw.Write(sb.ToString());
            }
        }

        private string ListaParaString(string idDropdown, string idTabela, bool buscarCabecalhos, string paginaAnterior)
        {
            //Util.ClickDropDown(Driver, "name", idDropdown, "100");
            IWebElement elementoTabela = Driver.FindElement(By.Id(idTabela));
            List<IWebElement> cabecalhos = elementoTabela.FindElements(By.TagName("th")).ToList();
            List<IWebElement> dados = elementoTabela.FindElements(By.TagName("td")).ToList();
            string final = paginaAnterior;
            StringBuilder t = new StringBuilder();
            if (buscarCabecalhos == true)
            {
                for (int i = 0; i < cabecalhos.Count(); i++)
                {

                    if (i == cabecalhos.Count() - 1)
                    {
                        t.Append(" " + cabecalhos[i].Text + " ");
                        t.Append("\n");
                    }
                    else
                    {
                        t.Append(" " + cabecalhos[i].Text + " " + ";");
                    }


                }
            }
            int contador = 0;
            for (int i = 0; i < dados.Count(); i++)
            {
                //StringBuilder t = new StringBuilder();

                if (contador == cabecalhos.Count() - 1)
                {
                    t.Append(" " + dados[i].Text + " ");
                    t.Append("\n");
                }
                else
                {
                    t.Append(" " + dados[i].Text + " " + ";");
                }

                if (contador == cabecalhos.Count() - 1)
                {
                    contador = 0;
                }
                else
                {

                    contador++;
                }


            }
            return t.ToString();
        }

    }
}
