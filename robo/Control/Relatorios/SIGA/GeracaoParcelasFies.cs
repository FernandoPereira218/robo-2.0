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

        public void GeraParcelaFies(IWebDriver driver, TOAluno aluno, string semestre)
        {
            Driver = driver;
            WaitElementIsVisible(driver, By.Id("pess_cpf"));
            FiltraAluno(driver, aluno);
            string semestreCorreto = FormatarSemestreSiga(semestre);
            IWebElement tdParcelas;
            string botaoId;
            BuscarLinhaCorreta(aluno, out tdParcelas, out botaoId);
            string parcelas = BuscarQuantidadeParcelas(semestreCorreto, tdParcelas);

            if (parcelas == string.Empty)
            {
                Util.EditarConclusaoAluno(aluno, "Todas as parcelas geradas anteriormente.");
                return;
            }

            if (Driver.PageSource.Contains("btn_editar") == true)
            {
                ClickButtonsById(Driver, botaoId);

                SelectElement select = new SelectElement(Driver.FindElement(By.Id("num_parcela")));
                int numParcelas = select.Options.Count();
                string semetreSiga = BuscarSemestreSiga(semestre);
                DateTime dataAtual = DateTime.Now;
                if (numParcelas <= 2)
                {
                    Util.EditarConclusaoAluno(aluno, "Sem parcelas disponíveis!");
                    Driver.Url = Driver.Url;
                    return;
                }

                for (int i = 2; i < numParcelas; i++)
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
                        //Clica no semestre correto
                        ClickDropDownExact(driver, "id", "peri_id", semetreSiga);

                        select = new SelectElement(Driver.FindElement(By.Id("num_parcela")));
                        string ParcelaSelecionada = select.Options[2].Text;

                        //Clicar na parcela
                        ClickDropDown(driver, "id", "num_parcela", select.Options[2].Text);

                        //Clicar em número do documento
                        SelecionarNumeroDocumento();

                        DateTime dataVencimento = BuscarDataVencimento(aluno, dataAtual, ParcelaSelecionada);

                        //Escrever data vencimento
                        ClickAndWriteById(driver, "id_dt_vencimento", dataVencimento.ToString("dd/MM/yyyy"));

                        //Gerar mensalidade
                        ClickElementByXPath(driver, "input", "value", "Gerar Mensalidade");

                        WaitElementIsVisible(driver, By.Id("msg_1"));

                        string textoMensagem = ConfirmacaoGravacaoParcelaAjuste(driver);
                        VerificarErro(driver, aluno, ParcelaSelecionada, textoMensagem);

                    }

                }
                //Remover ", " do final da conclusao
                aluno.Conclusao = aluno.Conclusao.Substring(0, aluno.Conclusao.Length - 2);
                Util.EditarConclusaoAluno(aluno, aluno.Conclusao);
                Driver.Url = Driver.Url;
            }

        }

        private void VerificarErro(IWebDriver driver, TOAluno aluno, string ParcelaSelecionada, string textoMensagem)
        {
            IWebElement msgSistema = driver.FindElement(By.Id("msg_1"));
            while (Driver.PageSource.Contains(textoMensagem) == true)
            {
                Sleep();
            }

            if (msgSistema.Text.Contains("Geração de mensalidade efetuada com sucesso!"))
            {
                if (aluno.Conclusao == "Não Feito")
                {
                    aluno.Conclusao = "";
                }
                aluno.Conclusao = aluno.Conclusao + ParcelaSelecionada + " OK" + ", ";
                Util.EditarConclusaoAluno(aluno, aluno.Conclusao);
            }
            else
            {
                string mensagemSistema = msgSistema.Text.Replace("\n", "");
                mensagemSistema = mensagemSistema.Replace("\r", "");
                Util.EditarConclusaoAluno(aluno, mensagemSistema);
            }
        }

        private string ConfirmacaoGravacaoParcelaAjuste(IWebDriver driver)
        {
            IWebElement mensagem;
            string textoMensagem;
            mensagem = driver.FindElement(By.Id("msg_1"));
            textoMensagem = mensagem.Text;

            if (mensagem.Text.Contains("Esta parcela já foi faturada. Deseja gerar a parcela como um ajuste?"))
            {
                ClickElementByXPath(driver, "input", "value", "Sim");
            }

            return textoMensagem;
        }

        private DateTime BuscarDataVencimento(TOAluno aluno, DateTime dataAtual, string ParcelaSelecionada)
        {
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
                    throw new Exception("Número de parcelas não previsto");
            }

            return dataCalculo;
        }

        private void SelecionarNumeroDocumento()
        {
            SelectElement nomeOrigem = new SelectElement(Driver.FindElement(By.Id("doc_id_origem")));
            if (nomeOrigem.Options.Count > 1)
            {
                nomeOrigem.SelectByIndex(1);
            }
        }

        private static string BuscarQuantidadeParcelas(string semestreCorreto, IWebElement tdParcelas)
        {
            string parcelas;
            try
            {
                parcelas = tdParcelas.Text;
                if (parcelas.Contains(semestreCorreto) == false && parcelas != "")
                {
                    parcelas = string.Empty;
                }
            }
            catch (NoSuchElementException)
            {
                parcelas = tdParcelas.Text;
            }

            return parcelas;
        }

        private void BuscarLinhaCorreta(TOAluno aluno, out IWebElement tdParcelas, out string botaoId)
        {
            string curso;
            var tdAtivos = Driver.FindElements(By.XPath("//span[text()='Ativo']/.."));
            if (tdAtivos.Count > 1)
            {
                int ativoCorreto = 0;
                for (int i = 0; i < tdAtivos.Count(); i++)
                {
                    var tdCurso = tdAtivos[i].FindElements(By.XPath("//span[text()='Ativo']/preceding::span[4]"))[i];
                    curso = tdCurso.Text;
                    if (aluno.CursoSiga.ToUpper() == curso)
                    {
                        ativoCorreto = i;
                        break;
                    }
                }
                tdParcelas = Driver.FindElements(By.XPath("//span[text()='Ativo']/preceding::span[1]"))[ativoCorreto];
                var tdBotao = Driver.FindElements(By.XPath("//span[text()='Ativo']/following::input"))[ativoCorreto];
                botaoId = tdBotao.GetAttribute("id");

            }
            else
            {

                tdParcelas = Driver.FindElement(By.XPath("//span[text()='Ativo']/preceding::span[1]"));
                var tdBotao = Driver.FindElement(By.XPath("//span[text()='Ativo']/following::input"));
                botaoId = tdBotao.GetAttribute("id");
            }
        }

        private string FormatarSemestreSiga(string anoSemestre)
        {
            string[] semestreAno = anoSemestre.Split('/');
            string semestre = semestreAno[0];
            string ano = semestreAno[1];

            return ano + "-" + semestre;
        }

        public void ExecutarCookieGuiche(IWebDriver driver)
        {
            var executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("var exdate = new Date(); exdate.setDate(exdate.getDate() + 365); document.cookie = encodeURIComponent(\"GUICHE\") + \"=\" + encodeURIComponent(\"GUICHE_GENERICO\") + \"; expires=\" + exdate.toUTCString() + \";path=/;\"");
        }
    }
}