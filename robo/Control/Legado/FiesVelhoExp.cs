using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Robo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robo.pgm
{
    class FiesVelhoExp
    {
        static IWebDriver Driver;

        public static void OpenFiesVelho(List<TOLogin> logins, string tipoExecucao, string campusSelecionado, string semestre, string situacaoDRI, string ano, string mes)
        {
            if (campusSelecionado != "")
            {
                for (int i = logins.Count - 1; i >= 0; i--)
                {
                    if (campusSelecionado.ToUpper() != logins[i].Campus.ToUpper())
                    {
                        logins.RemoveAt(i);
                    }
                }
            }
            try
            {
                Driver = Util.StartBrowser("http://sisfies.mec.gov.br/", "temp");

                foreach (TOLogin login in logins)
                {
                    if (RealizarLoginSucesso(login))
                    {
                        while (Driver.PageSource.Contains("Aditamentos FIES") == false)
                        {
                            Driver.FindElement(By.XPath("//select[@name='co_perfil']/option[contains(.,'CPSA Presidência')]")).Click();
                            System.Threading.Thread.Sleep(500);
                        }
                        switch (tipoExecucao.ToUpper())
                        {
                            case "EXPORTAR DRM":
                                ExportarDocumento(semestre, "DRM", login.Campus);
                                break;

                            case "EXPORTAR DRT":
                                ExportarDocumento(semestre, "DRT", login.Campus);
                                break;

                            case "EXPORTAR DRD":
                                ExportarDocumento(semestre, "DRD", login.Campus);
                                break;

                            case "EXPORTAR SUSPENSÃO":
                                ExportarDocumento(semestre, "Suspensao", login.Campus);
                                break;
                            case "EXPORTAR DRI":
                                MetodoDRIExp(situacaoDRI, login.Campus);
                                break;
                            case "EXPORTAR EXTRATO MENSAL DE REPASSE":
                                ExtratoMensalRepasse(ano, mes);
                                break;

                            default:
                                break;
                        }
                    }
                }
                Driver.Quit();
                Driver.Dispose();
            }
            catch (Exception e)
            {
                Driver.Quit();
                Driver.Dispose();
                throw e;
            }
            finally
            {
                //Driver.Quit();
                //Driver.Dispose();
            }
        }
        private static bool isAlertPresent()
        {
            try
            {
                Driver.SwitchTo().Alert();
                return true;
            }   // try 
            catch (NoAlertPresentException Ex)
            {
                return false;
            }   // catch 
        }

        private static void FazerLogout()
        {
            Util.ScrollToElementByXpath(Driver, "//a[contains(text(),'Sair')]");
            Util.ClickButtonsByXpath(Driver, "//a[contains(text(),'Sair')]");
        }

        private static void ExtratoMensalRepasse(string ano, string mes)
        {
            System.Threading.Thread.Sleep(1000);
            Util.ClickButtonsByXpath(Driver, "/html/body/div[3]/div[4]/div[1]/div[4]/ul/li[1]/a");
            Util.ClickDropDown(Driver, "id", "nu_ano", ano);
            Util.ClickDropDown(Driver, "id", "nu_mes", mes);
            SelectElement select = new SelectElement(Driver.FindElement(By.Id("dt_repasse")));
            if (select.Options.Count > 2)
            {
                ((IJavaScriptExecutor)Driver).ExecuteScript("alert(\"Por favor selecione uma data\")");
                while (isAlertPresent())
                {
                    System.Threading.Thread.Sleep(100);
                }
                while (select.SelectedOption.Text == "Selecione")
                {
                    System.Threading.Thread.Sleep(500);
                }
            }
            else
            {
                select.SelectByIndex(1);
            }
            Driver.FindElement(By.Id("btn_excel")).Click();
            Util.SalvarArquivos(Driver, "Extrato_Mensal_Repasse");
        }

        static Boolean RealizarLoginSucesso(TOLogin login)
        {
            while (Driver.PageSource.Contains("img/titAcessoInstituicao.gif") == false)
            {
                System.Threading.Thread.Sleep(500);
            }
            Util.ClickButtonsByCss(Driver, "#link-instituicao img:nth-child(1)");

            Util.ClickButtonsByCss(Driver, "center:nth-child(10) td:nth-child(2) .guest-box:nth-child(1) span:nth-child(2)");
            while (Driver.Url.Contains("InitAuthenticationByIdentifierAndPassword") == false)
            {
                System.Threading.Thread.Sleep(100);
            }
            Util.ClickAndWriteById(Driver, "id", login.Usuario);
            Util.ClickAndWriteById(Driver, "pw", login.Senha);

            Util.ClickButtonsById(Driver, "botoes");
            if (!Driver.PageSource.Contains("A senha informada não confere. Número de tentativas restAes:"))//Ocorreu uma falha na execução da aplicação. A caixa de erro ao lado mostra o motivo da falha. Provavelmente alguma informação incorreta foi processada.
            {
                return true;
            }
            else
            {
                throw new Exception("A senha informada não confere. Por favor, cheque se todos logins foram inseridos corretamente.");
            }
        }
        public static void WaitinLoadingExp()
        {
            IWebElement Carregando = Driver.FindElement(By.ClassName("background-grey"));
            bool carr = Carregando.Displayed;
            while (carr == true)
            {
                System.Threading.Thread.Sleep(1000);
                Carregando = Driver.FindElement(By.ClassName("background-grey"));
                carr = Carregando.Displayed;
            }
        }

        public static void ExportarDocumento(string semestre, string tipoRelatorio, string campus)
        {
            System.Threading.Thread.Sleep(1000);
            Util.ClickButtonsByCss(Driver, "div:nth-child(3) > ul > .menu-button:nth-child(3) > a");

            string selRelatorio = SelecionarTipoRelatorio(tipoRelatorio);

            Util.ClickDropDown(Driver, "id", "co_finalidade_aditamento", selRelatorio);

            WaitinLoadingExp();

            semestre = semestre.Replace("1/", "1º/");
            semestre = semestre.Replace("2/", "2º/");
            Util.ClickDropDown(Driver, "id", "coSemestreAditamento", semestre);

            Driver.FindElement(By.Name("export-excel")).Click();
            Util.SalvarArquivos(Driver, tipoRelatorio, campus);
            FazerLogout();
        }



        private static string SelecionarTipoRelatorio(string tipoRelatorio)
        {
            switch (tipoRelatorio)
            {
                case "DRM":
                    Util.ClickDropDown(Driver, "id", "co_finalidade_aditamento", "Aditamento de Renovação");
                    return "Aditamento de Renovação";
                    break;
                case "DRT":
                    Util.ClickDropDown(Driver, "id", "co_finalidade_aditamento", "Aditamento de Transferência");
                    return "Aditamento de Transferência";
                    break;
                case "DRD":
                    Util.ClickDropDown(Driver, "id", "co_finalidade_aditamento", "Aditamento de Dilatação");
                    return "Aditamento de Dilatação";
                    break;
                case "Suspensao":
                    Util.ClickDropDown(Driver, "id", "co_finalidade_aditamento", "Suspensão");
                    return "Suspensão";
                    break;

                default:
                    return string.Empty;
                    break;
            }
        }
        public static void MetodoDRIExp(string SituacaoDRI, string campus)
        {
            System.Threading.Thread.Sleep(1000);
            Util.ClickButtonsByXpath(Driver, "//a[contains(text(),'Validação pela CPSA Fies')]");

            Util.ClickDropDown(Driver, "id", "co_situacao_inscricao", SituacaoDRI);

            Util.ClickButtonsById(Driver, "excel");

            Util.SalvarArquivos(Driver, "DRI_" + SituacaoDRI, campus);

            FazerLogout();
        }
    }
}