using Robo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.IO;
using System.Diagnostics;

namespace robo.pgm
{
    class FiesNovoExp
    {
        private static IWebDriver Driver;
        private static string IES;
        public static void OpenFiesNovo(List<TOLogin> logins, string tipoExecucao, string Semestre, string parametroIES, string ano, string mes, string dataInicial, string dataFinal, string IESCoparticipacao)
        {
            IES = parametroIES;
            Driver = Util.StartBrowser("http://sifesweb.caixa.gov.br", "temp", false);
            try
            {
                foreach (TOLogin login in logins)
                {
                    //OpenLogin(login, "btnAdmnstrcProcessodeFinanciamentoConsultarContratoEstudante");

                    OpenLogin(login);


                    switch (tipoExecucao.ToUpper())
                    {
                        case "EXPORTAR ADITAMENTO":
                            ClicarBotaoMenu("btnAdmnstrcManutenccedilatildeoAditamentoRenovaccedilatildeo");
                            //Ver como fazer - Muitas páginas para buscar html, mas relatório não mostra semestre
                            break;
                        case "EXPORTAR DRM":
                            ClicarBotaoMenu("btnAdmnstrcManutenccedilatildeoAditamentoRenovaccedilatildeo");
                            WaitForLoading();
                            Util.ClickButtonsById(Driver, "btnConsultar");
                            WaitForLoading();
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
                                IWebElement sAdminMenuJobTitle = Driver.FindElement(By.XPath(string.Format("//*[@id=\"{0}\"]", "gridResult_next")));
                                ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", sAdminMenuJobTitle);
                            }

                            String userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
                            string downloadFolder = System.IO.Path.Combine(userRoot, "Downloads");
                            string arquivo = downloadFolder + "\\Relatório_Aditamentos.csv";
                            using (StreamWriter sw = new StreamWriter(arquivo, false, UTF8Encoding.UTF8))
                            {
                                sw.Write(sb.ToString());
                            }
                            //Mesma coisa que o de aditamento
                            break;
                        case "EXPORTAR DRD":
                            ClicarBotaoMenu("btnAdmnstrcManutenccedilatildeoAprovarDilataccedilatildeo");
                            Util.ClickButtonsById(Driver, "btnConsultar");
                            WaitForLoading();
                            ListaParaCSV("Dilatações", "gridResult_length", "gridResult", true);
                            break;
                        case "EXPORTAR DRT":
                            ClicarBotaoMenu("btnAdmnstrcManutenccedilatildeoAprovarTransferecircncia");
                            ConsultarEExportar();
                            Util.SalvarArquivos(Driver, "DRT");
                            break;
                        case "EXPORTAR SUSPENSÃO":
                            ClicarBotaoMenu("btnAdmnstrcManutenccedilatildeoAprovarSuspensatildeo");
                            ConsultarEExportar();
                            Util.SalvarArquivos(Driver, "SUSPENSÃO");
                            break;
                        case "EXPORTAR INADIMPLÊNCIA":
                            ClicarBotaoMenu("btnAdmnstrcRelatoacuteriosAgenteFinanceiroInadimplecircnciaMantenedoraIES");
                            Util.ClickDropDown(Driver, "id", "selectTipoConsolidacao", "Mantenedora");
                            Util.ClickDropDown(Driver, "id", "selectTipoRelatorio", "Utilização");
                            Util.ClickButtonsById(Driver, "btnConsultar");
                            WaitForLoading();
                            IWebElement btnDetalhar = Driver.FindElement(By.XPath("//*[@id=\"btnDetalhar\"]"));
                            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", btnDetalhar);
                            WaitForLoading();
                            IWebElement btnExportar = Driver.FindElement(By.XPath("//*[@id=\"btnExportar\"]"));
                            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", btnExportar);
                            Util.SalvarArquivos(Driver, "INADIMPLÊNCIA");
                            break;
                        case "EXPORTAR REPASSE":
                            ClicarBotaoMenu("btnAdmnstrcRelatrioRelatriodeContrataoAnaltico");
                            Util.ClickDropDown(Driver, "id", "selectAno", ano);
                            Util.ClickDropDown(Driver, "id", "selectMes", mes);
                            ConsultarEExportar();
                            Util.SalvarArquivos(Driver, "REPASSE");
                            break;
                        case "EXPORTAR COPARTICIPAÇÃO":
                            ClicarBotaoMenu("btnAdmnstrcRelatriosNovoFIESRelatriodeRepasseAnaliacuteticoIES");
                            WaitForLoading();
                            if (IES.ToUpper().Equals("UNIRITTER") || IES.ToUpper().Equals("FADERGS"))
                            {
                                IWebElement dropDownIES = Driver.FindElement(By.Id("ies_chosen"));
                                dropDownIES.Click();
                                if (IESCoparticipacao.Equals("488 - CENTRO UNIVERSITÁRIO RITTER DOS REIS") || IESCoparticipacao.Equals("2950 - Centro Universitário FADERGS"))
                                {
                                    Driver.FindElement(By.XPath("/html/body/div[2]/div/form/div[1]/div/div[1]/div/div/ul/li[2]")).Click();
                                }
                                else
                                {
                                    Driver.FindElement(By.XPath("/html/body/div[2]/div/form/div[1]/div/div[1]/div/div/ul/li[3]")).Click();
                                }
                            }
                            Util.ClickAndWriteById(Driver, "dataInicio", dataInicial);
                            Util.ClickAndWriteById(Driver, "dataFim", dataFinal);
                            Util.ClickButtonsById(Driver, "btnExportarRelatorio");
                            Util.SalvarArquivos(Driver, "COPARTICIPAÇÃO");
                            break;
                        case "VALIDAR REPARCELAMENTO":
                            ClicarBotaoMenu("btnAdmnstrcReparcelamentodaCoparticipaccedilatildeoValidarReparcelamentodaCoparticipaccedilatildeo");
                            while (Driver.PageSource.Contains("Nenhuma informação disponível") == false || Driver.PageSource.Contains("btnValidar") == true)
                            {
                                System.Threading.Thread.Sleep(100);
                            }
                            break;
                        default:
                            throw new Exception("Tipo de execução não existe");
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Driver.Close();
                Driver.Dispose();
                throw e;
            }
            Util.EndBrowser(Driver);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName">nome do arquivo sem extensão</param>
        private static void ListaParaCSV(string fileName, string idDropdown, string idTabela, bool status)
        {
            Util.ClickDropDown(Driver, "name", idDropdown, "100");
            IWebElement elementoTabela = Driver.FindElement(By.Id(idTabela));
            List<IWebElement> cabecalhos = elementoTabela.FindElements(By.TagName("th")).ToList();
            List<IWebElement> dados = elementoTabela.FindElements(By.TagName("td")).ToList();
            string arquivo;
            if (status == true)
            {
                String userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
                string downloadFolder = System.IO.Path.Combine(userRoot, "Downloads");
                arquivo = downloadFolder + "\\" + fileName + ".csv";
            }
            else
            {
                arquivo = fileName + ".csv";
            }
            if (File.Exists(arquivo))
            {
                File.Delete(arquivo);
            }
            for (int i = 0; i < cabecalhos.Count(); i++)
            {
                using (StreamWriter t = new StreamWriter(arquivo, true, UTF8Encoding.UTF8))
                {
                    if (i == cabecalhos.Count() - 1)
                    {
                        t.Write(cabecalhos[i].Text);
                        t.Write("\n");
                    }
                    else
                    {
                        t.Write(cabecalhos[i].Text + ";");
                    }
                }

            }
            int contador = 0;
            for (int i = 0; i < dados.Count(); i++)
            {
                using (StreamWriter t = new StreamWriter(arquivo, true, UTF8Encoding.UTF8))
                {
                    if (contador == cabecalhos.Count() - 1)
                    {
                        t.Write(dados[i].Text);
                        t.Write("\n");
                    }
                    else
                    {
                        t.Write(dados[i].Text + ";");
                    }
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
            if (status == true)
            {
                Process.Start(arquivo);
            }
        }

        private static string ListaParaString(string idDropdown, string idTabela, bool buscarCabecalhos, string paginaAnterior)
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


        private static void ConsultarEExportar()
        {
            WaitForLoading();
            Util.ClickButtonsById(Driver, "btnConsultar");
            WaitForLoading();
            Util.ClickButtonsById(Driver, "btnExportar");
        }

        public static void OpenLogin(TOLogin login, bool loginAdmin = false)
        {
            FazerLogin(login);

        }
        private static void FazerLogin(TOLogin login)
        {
            Util.ClickAndWriteById(Driver, "username", login.Usuario);
            Util.ClickButtonsById(Driver, "button-submit");
            Util.ClickAndWriteById(Driver, "password", login.Senha);
            Util.ClickButtonsByCss(Driver, "button:nth-child(1)");
        }
        private static void ClicarBotaoMenu(string idMenu, bool inadimplencia = false)
        {
            if (!Driver.PageSource.Contains("código autenticador"))
            {

                while (!Driver.PageSource.Contains("dropdown contratacao") || Driver.PageSource.Contains("modal-backdrop fade in") || Driver.PageSource.Contains("modal-backdrop fade"))
                {
                    System.Threading.Thread.Sleep(1000);
                }
                IWebElement sAdminMenuJobTitle = Driver.FindElement(By.XPath(string.Format("//*[@id=\"{0}\"]", idMenu)));
                ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", sAdminMenuJobTitle);
            }
            else
            {
                //Problema de código autenticador 
                //CampusAditado = "Problema Codigo autentidor"; 
                //Util.ClickButtonsById(Driver, "kc-cancel"); 
                throw new Exception("Problema com código autenticador");
            }
        }
        private static void WaitPageLoading(string element, bool exist)
        {
            while (Driver.PageSource.Contains(element) == exist)
            {
                System.Threading.Thread.Sleep(1000);
            }
        }

        private static void WaitForLoading()
        {
            WaitPageLoading("modal-backdrop fade in", true);
            WaitPageLoading("modal-backdrop fade", true);
        }
    }
}
