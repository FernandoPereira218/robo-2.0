using OpenQA.Selenium;
using Robo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robo.Control
{
    /// <summary>
    /// Métodos comuns para serem utilizados no site da Caixa
    /// </summary>
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

        /// <summary>
        /// Espera até algum elemento da página existir ou deixar de existir
        /// </summary>
        /// <param name="Driver"></param>
        /// <param name="element"></param>
        /// <param name="exist">'true' para esperar até o elemento existir 'false' para esperar até que desapareça</param>
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

        /// <summary>
        /// Clica em algum botão do menu da página inicial do site
        /// </summary>
        /// <param name="Driver"></param>
        /// <param name="idMenu">ID do botão desejado</param>
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
        /// Abre menu de consulta de alunos para aditamentos e DRM
        /// </summary>
        /// <param name="Driver"></param>
        public void ClicarMenuAditamento(IWebDriver Driver)
        {
            ClicarBotaoMenuPaginaInicial(Driver, "btnAdmnstrcManutenccedilatildeoAditamentoRenovaccedilatildeo");
        }

        /// <summary>
        /// Abre menu histório de reparcelamento da coparticipação
        /// </summary>
        /// <param name="Driver"></param>
        public void ClicarMenuHistoricoReparcelamentoCopartipacao(IWebDriver Driver)
        {
            ClicarBotaoMenuPaginaInicial(Driver, "btnAdmnstrcReparcelamentodaCoparticipaccedilatildeoHistoacutericodoReparcelamentodaCoparticipaccedilatildeo");
        }

        /// <summary>
        /// Abre menu de consulta de contrato
        /// </summary>
        /// <param name="Driver"></param>
        public void ClicarMenuConsultaContrato(IWebDriver Driver)
        {
            ClicarBotaoMenuPaginaInicial(Driver, "btnAdmnstrcProcessodeFinanciamentoConsultarContratoEstudante");
        }

        /// <summary>
        /// Abre menu de validar reparcelamentos
        /// </summary>
        /// <param name="Driver"></param>
        public void ClicarMenuValidarReparcelamento(IWebDriver Driver)
        {
            ClicarBotaoMenuPaginaInicial(Driver, "btnAdmnstrcReparcelamentodaCoparticipaccedilatildeoValidarReparcelamentodaCoparticipaccedilatildeo");
        }

        /// <summary>
        /// Abre menu de dilatação
        /// </summary>
        /// <param name="Driver"></param>
        public void ClicarMenuDilatacao(IWebDriver Driver)
        {
            ClicarBotaoMenuPaginaInicial(Driver, "btnAdmnstrcManutenccedilatildeoAprovarDilataccedilatildeo");
        }

        /// <summary>
        /// Abre menu de suspensão
        /// </summary>
        /// <param name="Driver"></param>
        public void ClicarMenuSuspensao(IWebDriver Driver)
        {
            ClicarBotaoMenuPaginaInicial(Driver, "btnAdmnstrcManutenccedilatildeoAprovarSuspensatildeo");
        }

        /// <summary>
        /// Abre menu de transferência
        /// </summary>
        /// <param name="Driver"></param>
        public void ClicarMenuTransferencia(IWebDriver Driver)
        {
            ClicarBotaoMenuPaginaInicial(Driver, "btnAdmnstrcManutenccedilatildeoAprovarTransferecircncia");
        }

        /// <summary>
        /// Abre menu de inadimplência
        /// </summary>
        /// <param name="Driver"></param>
        public void ClicarMenuInadimplencia(IWebDriver Driver)
        {
            ClicarBotaoMenuPaginaInicial(Driver, "btnAdmnstrcRelatoacuteriosAgenteFinanceiroInadimplecircnciaAgecircnciaEstudante");
        }

        /// <summary>
        /// Abre menu de repasse
        /// </summary>
        /// <param name="Driver"></param>
        public void ClicarMenuRepasse(IWebDriver Driver)
        {
            ClicarBotaoMenuPaginaInicial(Driver, "btnAdmnstrcRelatrioRelatriodeContrataoAnaltico");
        }

        /// <summary>
        /// Abre menu de coparticipação
        /// </summary>
        /// <param name="Driver"></param>
        public void ClicarMenuCoparticipacao(IWebDriver Driver)
        {
            ClicarBotaoMenuPaginaInicial(Driver, "btnAdmnstrcRelatriosNovoFIESRelatriodeRepasseAnaliacuteticoIES");
        }

        /// <summary>
        /// Consulta CPF do aluno na página de aditamento
        /// </summary>
        /// <param name="Driver"></param>
        /// <param name="aluno"></param>
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
            ClickButtonsByCss(Driver, "body");
            string erro = BuscarMensagemDeErro(Driver);
            if (erro.Contains("CPF com Dígito Verificador inválido. Redigite-o!") == true)
            {
                Util.EditarConclusaoAluno(aluno, erro);
                return;
            }
            ClickButtonsById(Driver, "btnConsultar");
            erro = BuscarMensagemDeErro(Driver);
            if (erro.Contains("Ocorreu um erro na consulta, tente novamente."))
            {
                throw new Exception("O site da caixa parece estar instável. Tente novamente mais tarde.");
            }
            WaitForLoading(Driver);

            
        }

        /// <summary>
        /// Procura mensagem de erro do topo da página
        /// </summary>
        /// <returns>A mensagem encontrada</returns>
        protected string BuscarMensagemDeErro(IWebDriver Driver)
        {
            if (Driver.PageSource.Contains("alert alert-error"))
            {
                IWebElement msgErro = Driver.FindElement(By.ClassName("alert-error"));
                return msgErro.Text.Replace("x\r\n", "");
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Clica em algum botão utilizando JavaScriptExecutor
        /// </summary>
        /// <param name="Driver"></param>
        /// <param name="id">ID do botão</param>
        public void ClickButtonByIdWithJavaScript(IWebDriver Driver, string id)
        {
            IWebElement element = Driver.FindElement(By.XPath(string.Format("//*[@id=\"{0}\"]", id)));
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", element);
        }

        /// <summary>
        /// Consultar aluno e abrir DRM do semestre selecionado
        /// </summary>
        /// <param name="Driver"></param>
        /// <param name="aluno"></param>
        /// <param name="semestreAtual">Semestre selecionado (semestre/ano)</param>
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

        /// <summary>
        /// Verifica se a página atual contém "Nenhuma informação disponível"
        /// </summary>
        /// <param name="Driver"></param>
        /// <returns>True se houver e false se não</returns>
        public bool VerificarNenhumaInformacaoDisponivel(IWebDriver Driver)
        {
            if (Driver.PageSource.Contains("Nenhuma informação disponível") == true)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Procura botão de aditamento que possui atributos do semestre selecionado
        /// </summary>
        /// <param name="Driver"></param>
        /// <param name="semestreAtual">Semestre atual (semestre/ano)</param>
        /// <returns></returns>
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
        /// <param name="semestreAtual">Semestre atual (semestre/ano)</param>
        /// <returns>Conclusão encontrada</returns>
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
