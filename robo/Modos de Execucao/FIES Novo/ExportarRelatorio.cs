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
    public class ExportarRelatorio : UtilFiesNovo
    {
        private IWebDriver Driver;
        public void ExportarRelatorioFiesNovo(IWebDriver driver, string tipoRelatorio)
        {
            Driver = driver;
            ClickButtonsById(Driver, "btnConsultar");
            WaitForLoading(Driver);

            ClickDropDown(Driver, "name", "gridResult_length", "100");
            string source = Driver.PageSource.Split(new string[] { "Mostrando" }, StringSplitOptions.None)[1];
            source = source.Split(new string[] { "registros" }, StringSplitOptions.None)[0];
            string quantidade = source.Split(new string[] { "de " }, StringSplitOptions.None)[1];
            int qtdLinhas = Convert.ToInt32(quantidade);
            int qtdPaginas = Convert.ToInt32(Math.Ceiling(qtdLinhas / 100f));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < qtdPaginas; i++)
            {
                if (i == 0)
                {
                    sb.Append(ListaParaString("gridResult", true));
                }
                else
                {
                    sb.Append(ListaParaString("gridResult", false));
                }
                if (qtdPaginas > 1)
                {
                    IWebElement sAdminMenuJobTitle = Driver.FindElement(By.XPath(string.Format("//*[@id=\"{0}\"]", "gridResult_next")));
                    ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", sAdminMenuJobTitle);
                }
            }
            string downloadFolder = Directory.GetCurrentDirectory() + "\\RelatorioExportacao\\";
            string nomeArquivo = "Relatório_" + tipoRelatorio + ".csv";
            string arquivo = downloadFolder + nomeArquivo;
            using (StreamWriter sw = new StreamWriter(arquivo, false, UTF8Encoding.UTF8))
            {
                sw.Write(sb.ToString());
            }

            Util.ExportarDocumento("", nomeArquivo: nomeArquivo);
        }

        private string ListaParaString(string idTabela, bool buscarCabecalhos)
        {
            IWebElement elementoTabela = Driver.FindElement(By.Id(idTabela));
            List<IWebElement> cabecalhos = elementoTabela.FindElements(By.TagName("th")).ToList();
            List<IWebElement> dados = elementoTabela.FindElements(By.TagName("td")).ToList();
            StringBuilder sb = new StringBuilder();
            if (buscarCabecalhos == true)
            {
                EscreverCabecalhos(cabecalhos, sb);
            }
            EscreverDados(cabecalhos, dados, sb);
            return sb.ToString();
        }

        private void EscreverDados(List<IWebElement> cabecalhos, List<IWebElement> dados, StringBuilder sb)
        {
            int contador = 0;
            for (int i = 0; i < dados.Count(); i++)
            {

                if (contador == cabecalhos.Count() - 1)
                {
                    sb.Append(" " + dados[i].Text + " ");
                    sb.Append("\n");
                }
                else
                {
                    sb.Append(" " + dados[i].Text + " " + ";");
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
        }

        private void EscreverCabecalhos(List<IWebElement> cabecalhos, StringBuilder sb)
        {
            for (int i = 0; i < cabecalhos.Count(); i++)
            {

                if (i == cabecalhos.Count() - 1)
                {
                    sb.Append(" " + cabecalhos[i].Text + " ");
                    sb.Append("\n");
                }
                else
                {
                    sb.Append(" " + cabecalhos[i].Text + " " + ";");
                }
            }
        }
    }
}
