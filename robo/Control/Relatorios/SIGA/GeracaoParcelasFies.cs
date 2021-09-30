using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Robo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robo.Control.Relatorios.SIGA
{
    class GeracaoParcelasFies : UtilSiga
    {
        private IWebDriver Driver;
        public void GeraParcelaFies(IWebDriver driver, TOAluno aluno, string Semestre)
        {
            Driver = driver;
            //var exdate = new Date();
            //exdate.setDate(exdate.getDate() + 365);
            //document.cookie = encodeURIComponent("GUICHE") + "=" + encodeURIComponent("GUICHE_GENERICO") + ";expires=" + exdate.toUTCString() + ";path=/;"
            FiltraAluno(driver, aluno);
            string parcelas;
            try
            {
                 parcelas = driver.FindElement(By.XPath("/html/body/table/tbody/tr/td/table/tbody/tr[6]/td/div/form[2]/table/tbody/tr[3]/td[7]")).Text;

            }
            catch (NoSuchElementException)
            {
                Sleep();
                while (driver.FindElement(By.XPath("/html/body/table/tbody/tr/td/table/tbody/tr[6]/td/div/form[2]/table/tbody/tr[3]/td[7]")) == null)
                {
                }
                parcelas = driver.FindElement(By.XPath("/html/body/table/tbody/tr/td/table/tbody/tr[6]/td/div/form[2]/table/tbody/tr[3]/td[7]")).Text;
            }
            if (parcelas == string.Empty)
            {
                Util.EditarConclusaoAluno(aluno, "Sem parcelas disponiveis!");
                Driver.Url = Driver.Url;
                return;
            }

            if (Driver.PageSource.Contains("btn_editar") == true)
            {
                ClickButtonsById(Driver, "btn_editar#0");

                SelectElement select = new SelectElement(Driver.FindElement(By.Id("num_parcela")));
                int num_parcelas = select.Options.Count();
                string semetreSiga = BuscarSemestreSiga(Semestre);
                DateTime dataAtual = DateTime.Now;
                if (num_parcelas <= 2)
                {
                    Util.EditarConclusaoAluno(aluno, "Sem parcela disponíveis!");
                    Driver.Url = Driver.Url;
                    return;
                }
                for (int i = 2; i < num_parcelas; i++)
                {
                    WaitLoading(driver);
                    try
                    {
                        select = new SelectElement(Driver.FindElement(By.Id("num_parcela")));
                    }
                    catch (UnexpectedTagNameException)
                    {
                        WaitLoading(driver);
                        select = new SelectElement(Driver.FindElement(By.Id("num_parcela")));
                    }
                    if (select.Options[2].Text.ToUpper().Contains("PARCELA"))
                    {
                        ClickDropDownExact(driver, "id", "peri_id", semetreSiga);
                        select = new SelectElement(Driver.FindElement(By.Id("num_parcela")));
                        string ParcelaSelecionada = select.Options[2].Text;
                        ClickDropDown(driver, "id", "num_parcela", select.Options[2].Text);
                        SelectElement nomeOrigem = new SelectElement(Driver.FindElement(By.Id("doc_id_origem")));
                        if (nomeOrigem.Options.Count > 1)
                        {
                            nomeOrigem.SelectByIndex(1);

                        }
                        DateTime dataCalculo;
                        switch (ParcelaSelecionada.ToUpper())
                        {
                            case "PARCELA 1":
                                dataCalculo = dataAtual.AddDays(3);
                                break;
                            case "PARCELA 2":
                                dataCalculo = dataAtual.AddDays(30);
                                break;
                            case "PARCELA 3":
                                dataCalculo = dataAtual.AddDays(60);
                                break;
                            case "PARCELA 4":
                                dataCalculo = dataAtual.AddDays(90);
                                break;
                            case "PARCELA 5":
                                dataCalculo = dataAtual.AddDays(120);
                                break;
                            case "PARCELA 6":
                                dataCalculo = dataAtual.AddDays(150);
                                break;
                            default:
                                Util.EditarConclusaoAluno(aluno, "Número de parcelas não previsto");
                                return;
                        }

                        ClickAndWriteById(driver, "id_dt_vencimento", dataCalculo.ToString("dd/MM/yyyy"));
                        ClickElementByXPath(driver, "input", "value", "Gerar Mensalidade");
                        WaitLoading(driver);
                        IWebElement Mensagem;
                        try
                        {
                            Mensagem = driver.FindElement(By.Id("msg_1"));
                        }
                        catch (NoSuchElementException)
                        {
                            Mensagem = driver.FindElement(By.Id("msg_1"));
                        }
                        if (Mensagem.Text.Contains("Esta parcela já foi faturada. Deseja gerar a parcela como um ajuste?"))
                        {
                            ClickElementByXPath(driver, "input", "value", "Sim");
                        }
                        IWebElement Erro = driver.FindElement(By.Id("msg_1"));
                        if (Erro.Text.Contains("Houve um erro ao efetuar a operação!"))
                        {
                            Util.EditarConclusaoAluno(aluno, "Houve um erro ao efetuar a operação!");
                            Driver.Url = Driver.Url;
                            return;
                        }
                        else
                        {
                            if (aluno.Conclusao == "Não Feito")
                            {
                                aluno.Conclusao = "";
                            }
                            aluno.Conclusao = aluno.Conclusao + ParcelaSelecionada + " OK" + ", ";
                            Util.EditarConclusaoAluno(aluno, aluno.Conclusao);
                        }

                    }

                }
                aluno.Conclusao = aluno.Conclusao.Substring(0, aluno.Conclusao.Length - 2);
                Util.EditarConclusaoAluno(aluno, aluno.Conclusao);
                Driver.Url = Driver.Url;
            }

        }
        public void ExecutarCookieGuiche(IWebDriver driver)
        {
            var executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("var exdate = new Date(); exdate.setDate(exdate.getDate() + 365); document.cookie = encodeURIComponent(\"GUICHE\") + \"=\" + encodeURIComponent(\"GUICHE_GENERICO\") + \"; expires=\" + exdate.toUTCString() + \";path=/;\"");
        }
    }
}