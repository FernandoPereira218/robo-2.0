using OpenQA.Selenium;
using Robo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robo.Control
{
    public class UtilFiesNovo : UtilSelenium
    {
        /// <summary>
        /// Faz login na página da Caixa
        /// </summary>
        /// <param name="Driver"></param>
        /// <param name="login"></param>
        public void FazerLogin(IWebDriver Driver, TOLogin login)
        {
            ClickAndWriteById(Driver, "username", login.Usuario);
            ClickButtonsById(Driver, "button-submit");
            ClickAndWriteById(Driver, "password", login.Senha);
            ClickButtonsByCss(Driver, "button:nth-child(1)");

            WaitElementIsVisible(Driver, By.XPath("//p[text()='Quadro de Avisos']"));
        }

        private void WaitPageLoading(IWebDriver Driver, string element, bool exist)
        {
            while (Driver.PageSource.Contains(element) == exist)
            {
                System.Threading.Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// Espera até o ícone de "carregando" sumir da tela
        /// </summary>
        /// <param name="Driver"></param>
        public void WaitForLoading(IWebDriver Driver)
        {
            WaitPageLoading(Driver, "modal-backdrop fade in", true);
            WaitPageLoading(Driver, "modal-backdrop fade", true);
        }

        private void ClicarBotaoMenuPaginaInicial(IWebDriver Driver, string idMenu)
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
                throw new Exception("Problema com código autenticador");
            }
        }

        /// <summary>
        /// Menu de consulta de alunos para aditamentos e DRM
        /// </summary>
        /// <param name="Driver"></param>
        public void ClicarMenuAditamento(IWebDriver Driver)
        {
            ClicarBotaoMenuPaginaInicial(Driver, "btnAdmnstrcManutenccedilatildeoAditamentoRenovaccedilatildeo");
        }
        public void ClicarMenuHistoricoReparcelamentoCopartipacao(IWebDriver Driver)
        {
            ClicarBotaoMenuPaginaInicial(Driver, "btnAdmnstrcReparcelamentodaCoparticipaccedilatildeoHistoacutericodoReparcelamentodaCoparticipaccedilatildeo");
        }
        public void ClicarMenuConsultaContrato(IWebDriver Driver)
        {
            ClicarBotaoMenuPaginaInicial(Driver, "btnAdmnstrcProcessodeFinanciamentoConsultarContratoEstudante");
        }
        public void ClicarMenuValidarReparcelamento(IWebDriver Driver)
        {
            ClicarBotaoMenuPaginaInicial(Driver, "btnAdmnstrcReparcelamentodaCoparticipaccedilatildeoValidarReparcelamentodaCoparticipaccedilatildeo");
        }
        public void ClicarMenuDilatacao(IWebDriver Driver)
        {
            ClicarBotaoMenuPaginaInicial(Driver, "btnAdmnstrcManutenccedilatildeoAprovarDilataccedilatildeo");
        }
        public void ClicarMenuSuspensao(IWebDriver Driver)
        {
            ClicarBotaoMenuPaginaInicial(Driver, "btnAdmnstrcManutenccedilatildeoAprovarSuspensatildeo");
        }
        public void ClicarMenuTransferencia(IWebDriver Driver)
        {
            ClicarBotaoMenuPaginaInicial(Driver, "btnAdmnstrcManutenccedilatildeoAprovarTransferecircncia");
        }
        public void ClicarMenuInadimplencia(IWebDriver Driver)
        {
            ClicarBotaoMenuPaginaInicial(Driver, "btnAdmnstrcRelatoacuteriosAgenteFinanceiroInadimplecircnciaAgecircnciaEstudante");
        }
        public void ClicarMenuRepasse(IWebDriver Driver)
        {
            ClicarBotaoMenuPaginaInicial(Driver, "btnAdmnstrcRelatrioRelatriodeContrataoAnaltico");
        }

        public void ClicarMenuCoparticipacao(IWebDriver Driver)
        {
            ClicarBotaoMenuPaginaInicial(Driver, "btnAdmnstrcRelatriosNovoFIESRelatriodeRepasseAnaliacuteticoIES");
        }

        public void ConsultarAluno(IWebDriver Driver, TOAluno aluno)
        {
            WaitForLoading(Driver);
            WaitPageLoading(Driver, "input-medium cpf", false);

            ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(0, 0)");

            ClickButtonsByCss(Driver, "body");

            ClickButtonsById(Driver, "btnLimpar");

            ClickButtonsById(Driver, "cpf");

            var executor = (IJavaScriptExecutor)Driver;
            executor.ExecuteScript($@"document.getElementById(""cpf"").value = ""{aluno.Cpf}"";");

            while (Driver.PageSource.Contains("Selecione") == false)
            {
                System.Threading.Thread.Sleep(500);
            }
            ClickButtonsById(Driver, "btnConsultar");

            WaitForLoading(Driver);
        }

        public void ClickButtonByIdWithJavaScript(IWebDriver Driver, string id)
        {
            IWebElement element = Driver.FindElement(By.XPath(string.Format("//*[@id=\"{0}\"]", id)));
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", element);
        }

        public void BuscarEAbrirDRM(IWebDriver Driver, TOAluno aluno, string semestreAtual)
        {
            ConsultarAluno(Driver, aluno);
            if (VerificarNenhumaInformacaoDisponivel(Driver) == true)
            {
                Util.EditarConclusaoAluno(aluno, "Nenhuma informação disponível");
                return;
            }

            IWebElement botaoImprimirTermo = BuscarBotaoSemestre(Driver, semestreAtual);

            //Caso haja um botao com ano/semestre correto
            if (botaoImprimirTermo != null)
            {
                string janelaInicial = Driver.CurrentWindowHandle;
                bool msgErro = true;
                var executor = (IJavaScriptExecutor)Driver;

                //Buscar nome aluno
                string name = Driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/form/div[7]/fieldset/div/table/tbody/tr[1]/td[2]")).Text;
                aluno.Nome = name;

                //Verificacao de erro ao clicar no botao
                while (msgErro == true)
                {
                    ScrollToElementByID(Driver, botaoImprimirTermo.GetAttribute("id"));
                    botaoImprimirTermo.Click();

                    System.Threading.Thread.Sleep(1000);

                    ClickButtonsById(Driver, "btnConfirmar");

                    msgErro = (bool)executor.ExecuteScript("return $('.alert.alert-error').is(':visible');");
                    if (msgErro == true)
                    {
                        ClickButtonsById(Driver, "btnConsultar");
                        WaitForLoading(Driver);
                    }
                }

                //Verificação em caso de alerta do site
                if (Driver.PageSource.Contains("MDLalerta_") == true)
                {
                    aluno.Conclusao = Driver.FindElement(By.XPath("/html/body/div[7]/div[2]/p")).Text;
                    ClickButtonsById(Driver, "btnConfirmar");
                    return;
                }

                //Esperar a janela de DRM abrir
                while (Driver.WindowHandles.Last() == janelaInicial)
                {
                    System.Threading.Thread.Sleep(100);
                }
                Driver.SwitchTo().Window(Driver.WindowHandles.Last());
            }
            else //Caso não seja encontrado o ano/semestre desejado
            {
                string situacaoAluno = string.Empty;

                IWebElement grid = Driver.FindElement(By.Id("gridResult"));
                if (grid.Text.Contains(semestreAtual) == true)
                {
                    situacaoAluno = BuscarSituacaoAluno(Driver, semestreAtual);
                }
                else
                {
                    situacaoAluno = "Semestre não encontrado.";
                }

                aluno.Conclusao = situacaoAluno;
            }
        }

        public bool VerificarNenhumaInformacaoDisponivel(IWebDriver Driver)
        {
            if (Driver.PageSource.Contains("Nenhuma informação disponível") == true)
            {
                return true;
            }
            return false;
        }

        private IWebElement BuscarBotaoSemestre(IWebDriver Driver, string semestreAtual)
        {
            string semestreFormatado = semestreAtual.Split('/')[0];
            string anoFormatado = semestreAtual.Split('/')[1];

            //Busca o botao que representa o ano/semestre correto
            if (Driver.PageSource.Contains(semestreAtual) == true)
            {
                var botoes = Driver.FindElements(By.Id("btnImprimirTermo"));

                foreach (var botao in botoes)
                {
                    string dataAno = botao.GetAttribute("data-ano");
                    string dataSemestre = botao.GetAttribute("data-semestre");
                    if (dataAno == anoFormatado && dataSemestre == semestreFormatado)
                    {
                        return botao;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Retorna situacao do semestre especificado do aluno no datagrid do site
        /// </summary>
        /// <returns></returns>
        public string BuscarSituacaoAluno(IWebDriver Driver, string semestreAtual)
        {
            string situacaoAluno = string.Empty;
            situacaoAluno = Driver.PageSource.Split(new string[] { semestreAtual }, StringSplitOptions.None)[3];
            situacaoAluno = situacaoAluno.Split(new string[] { "td class=" }, StringSplitOptions.None)[1];
            situacaoAluno = situacaoAluno.Replace(">", "");
            situacaoAluno = situacaoAluno.Replace("<", "");
            situacaoAluno = situacaoAluno.Replace("\"", "");
            situacaoAluno = situacaoAluno.Replace("/td", "");

            return situacaoAluno;
        }
    }
}
