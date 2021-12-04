using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using robo.Utils;
using robo.Contratos;
using System.Text;
using System.Threading.Tasks;
using robo.TO;
using robo.Excessoes;

namespace robo.Modos_de_Execucao.SIGA
{
    class GeracaoParcelasFies : UtilSiga, IModosDeExecucao.IModoComAlunos
    {
        private int opcaoParcela = 2;
        private string semestre;
        public GeracaoParcelasFies(string semestre)
        {
            this.semestre = semestre;
        }
        public void GeraParcelaFies(TOAluno aluno)
        {
            EsperarElementoVisivel(By.Id("pess_cpf"));
            FiltraAluno(aluno);
            string semestreCorreto = FormatarSemestreSiga(semestre);
            BuscarLinhaCorreta(aluno, out IWebElement tdParcelas, out string botaoId);
            VerificarQtdParcelas(aluno, semestreCorreto, tdParcelas);

            if (this.Driver.PageSource.Contains("btn_editar") == true)
            {
                ClickButtonsById(botaoId);

                SelectElement select = BuscarSelectElement("num_parcela");
                int numParcelas = select.Options.Count();
                if (numParcelas <= 2)
                {
                    Util.EditarConclusaoAluno(aluno, "Sem parcelas disponíveis!");
                    this.Driver.Url = this.Driver.Url;
                    return;
                }

                GerarTodasMensalidades(aluno, numParcelas, semestre);

                if (aluno.Conclusao == "Não Feito")
                {
                    Util.EditarConclusaoAluno(aluno, "Processo finalizado.");
                }
                this.Driver.Url = this.Driver.Url;
            }

        }

        private void VerificarQtdParcelas(TOAluno aluno, string semestreCorreto, IWebElement tdParcelas)
        {
            string parcelas = BuscarQuantidadeParcelas(semestreCorreto, tdParcelas);
            if (parcelas == string.Empty)
            {
                Util.EditarConclusaoAluno(aluno, "Todas as parcelas geradas anteriormente.");
                throw new PararExecucaoException();
            }
        }

        private SelectElement BuscarSelectElement(string elemento)
        {
            SelectElement select;
            try
            {
                select = new SelectElement(Driver.FindElement(By.Id(elemento)));
            }
            catch (UnexpectedTagNameException)
            {
                WaitLoading();
                select = new SelectElement(Driver.FindElement(By.Id(elemento)));
            }
            return select;
        }

        private bool ContemAglutinacao(out IWebElement opcaoComAglutinacao)
        {
            SelectElement select = BuscarSelectElement("num_parcela");
            var opcoesSelect = select.Options;
            foreach (IWebElement option in opcoesSelect)
            {
                if (option.Text.Contains("AGLUTINAÇÃO"))
                {
                    opcaoComAglutinacao = option;
                    return true;
                }
            }
            opcaoComAglutinacao = null;
            return false;
        }

        private void GerarTodasMensalidades(TOAluno aluno, int numParcelas, string semestre)
        {
            IWebElement opcaoComAglutinacao;
            if (ContemAglutinacao(out opcaoComAglutinacao) == true)
            {
                Util.EditarConclusaoAluno(aluno, opcaoComAglutinacao.Text);
                return;
            }
            for (int i = 2; i < numParcelas; i++)
            {
                WaitLoading();

                SelectElement select = BuscarSelectElement("num_parcela");

                if (select.Options[opcaoParcela].Text.ToUpper().Contains("AGLUTINAÇÃO"))
                {
                    Util.EditarConclusaoAluno(aluno, select.Options[opcaoParcela].Text);
                    return;
                }

                if (select.Options[opcaoParcela].Text.ToUpper().Contains("PARCELA"))
                {
                    GerarMensalidade(aluno, DateTime.Now, BuscarSemestreSiga(semestre));
                }
            }
        }

        private void GerarMensalidade(TOAluno aluno, DateTime dataAtual, string semestreSiga)
        {
            //Clica no semestre correto
            ClickDropDownExact("id", "peri_id", semestreSiga);

            SelectElement select = BuscarSelectElement("num_parcela");
            string ParcelaSelecionada = select.Options[opcaoParcela].Text;

            //Clicar na parcela
            ClickDropDown("id", "num_parcela", select.Options[opcaoParcela].Text);

            //Clicar em número do documento
            SelecionarNumeroDocumento();

            DateTime dataVencimento = BuscarDataVencimento(aluno, dataAtual, ParcelaSelecionada);

            //Escrever data vencimento
            ClickAndWriteById("id_dt_vencimento", dataVencimento.ToString("dd/MM/yyyy"));

            //Gerar mensalidade
            ClickElementByXPath("input", "value", "Gerar Mensalidade");

            EsperarElementoVisivel(By.Id("msg_1"));

            string textoMensagem = ConfirmacaoGravacaoParcelaAjuste(Driver);
            VerificarErro(Driver, aluno, ParcelaSelecionada, textoMensagem);
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
                string conclusao = "Parcela gerada com sucesso.";

                PreencherParcelas(aluno, ParcelaSelecionada, conclusao);

                Util.EditarConclusaoAluno(aluno, aluno.Conclusao);
            }
            else
            {
                string mensagemSistema = msgSistema.Text.Replace("\n", "");
                mensagemSistema = mensagemSistema.Replace("\r", "");
                mensagemSistema = mensagemSistema.Replace("ocultar", "");
                mensagemSistema = mensagemSistema.Replace("Mensagem do Sistema", "");

                PreencherParcelas(aluno, ParcelaSelecionada, mensagemSistema);

                opcaoParcela++;
                Util.EditarConclusaoAluno(aluno, aluno.Conclusao);
            }
        }

        private void PreencherParcelas(TOAluno aluno, string parcelaSelecionada, string conclusaoParcela)
        {
            switch (parcelaSelecionada.ToUpper())
            {
                case "PARCELA 1":
                    aluno.ParcelaSiga1 = conclusaoParcela;
                    break;
                case "PARCELA 2":
                    aluno.ParcelaSiga2 = conclusaoParcela;
                    break;
                case "PARCELA 3":
                    aluno.ParcelaSiga3 = conclusaoParcela;
                    break;
                case "PARCELA 4":
                    aluno.ParcelaSiga4 = conclusaoParcela;
                    break;
                case "PARCELA 5":
                    aluno.ParcelaSiga5 = conclusaoParcela;
                    break;
                case "PARCELA 6":
                    aluno.ParcelaSiga6 = conclusaoParcela;
                    break;
                default:
                    throw new Exception("Número de parcelas não previsto");
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
                ClickElementByXPath("input", "value", "Sim");
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
            SelectElement nomeOrigem = BuscarSelectElement("doc_id_origem");
            if (nomeOrigem.Options.Count > 1)
            {
                nomeOrigem.SelectByIndex(1);
            }
        }

        private string BuscarQuantidadeParcelas(string semestreCorreto, IWebElement tdParcelas)
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

        public void Executar(TOAluno aluno)
        {
            GeraParcelaFies(aluno);
        }

        public void SelecionarMenu()
        {
            throw new NotImplementedException();
        }

        public void SetWebDriver(IWebDriver Driver)
        {
            this.Driver = Driver;
        }
    }
}