using OpenQA.Selenium;
using robo.Excessoes;
using robo.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robo.Utils
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
        public void FazerLogin(TOLogin login)
        {
            if (Program.versaoApresentacao == true)
            {
                var jsExecutor = (IJavaScriptExecutor)Driver;
                jsExecutor.ExecuteScript("document.getElementById('username').setAttribute('type', 'password')");
            }
            ClicarEEscrever(By.Id("username"), login.Usuario);
            ClicarElemento(By.Id("button-submit"));
            ClicarEEscrever(By.Id("password"), login.Senha);
            ClicarElemento(By.CssSelector("button:nth-child(1)"));

            EsperarElementoVisivel(By.XPath("//p[text()='Quadro de Avisos']"));
        }

        /// <summary>
        /// Espera até algum elemento da página existir ou deixar de existir
        /// </summary>
        /// <param name="Driver"></param>
        /// <param name="element"></param>
        /// <param name="exist">'true' para esperar até o elemento existir 'false' para esperar até que desapareça</param>
        private void EsperarCarregamento(string element, bool exist)
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
        public void EsperarPaginaCarregando()
        {
            EsperarCarregamento("modal-backdrop fade in", true);
            EsperarCarregamento("modal-backdrop fade", true);
        }

        /// <summary>
        /// Clica em algum botão do menu da página inicial do site
        /// </summary>
        /// <param name="Driver"></param>
        /// <param name="idMenu">ID do botão desejado</param>
        private void ClicarBotaoMenuPaginaInicial(string idMenu)
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
        public void ClicarMenuAditamento()
        {
            ClicarBotaoMenuPaginaInicial("btnAdmnstrcManutenccedilatildeoAditamentoRenovaccedilatildeo");
        }

        /// <summary>
        /// Abre menu histório de reparcelamento da coparticipação
        /// </summary>
        /// <param name="Driver"></param>
        public void ClicarMenuHistoricoReparcelamentoCopartipacao()
        {
            ClicarBotaoMenuPaginaInicial("btnAdmnstrcReparcelamentodaCoparticipaccedilatildeoHistoacutericodoReparcelamentodaCoparticipaccedilatildeo");
        }

        /// <summary>
        /// Abre menu de consulta de contrato
        /// </summary>
        /// <param name="Driver"></param>
        public void ClicarMenuConsultaContrato()
        {
            ClicarBotaoMenuPaginaInicial("btnAdmnstrcProcessodeFinanciamentoConsultarContratoEstudante");
        }

        /// <summary>
        /// Abre menu de validar reparcelamentos
        /// </summary>
        /// <param name="Driver"></param>
        public void ClicarMenuValidarReparcelamento()
        {
            ClicarBotaoMenuPaginaInicial("btnAdmnstrcReparcelamentodaCoparticipaccedilatildeoValidarReparcelamentodaCoparticipaccedilatildeo");
        }

        /// <summary>
        /// Abre menu de dilatação
        /// </summary>
        /// <param name="Driver"></param>
        public void ClicarMenuDilatacao()
        {
            ClicarBotaoMenuPaginaInicial("btnAdmnstrcManutenccedilatildeoAprovarDilataccedilatildeo");
        }

        /// <summary>
        /// Abre menu de suspensão
        /// </summary>
        /// <param name="Driver"></param>
        public void ClicarMenuSuspensao()
        {
            ClicarBotaoMenuPaginaInicial("btnAdmnstrcManutenccedilatildeoAprovarSuspensatildeo");
        }

        /// <summary>
        /// Abre menu de transferência
        /// </summary>
        /// <param name="Driver"></param>
        public void ClicarMenuTransferencia()
        {
            ClicarBotaoMenuPaginaInicial("btnAdmnstrcManutenccedilatildeoAprovarTransferecircncia");
        }

        /// <summary>
        /// Abre menu de inadimplência
        /// </summary>
        /// <param name="Driver"></param>
        public void ClicarMenuInadimplencia()
        {
            ClicarBotaoMenuPaginaInicial("btnAdmnstrcRelatoacuteriosAgenteFinanceiroInadimplecircnciaAgecircnciaEstudante");
        }

        /// <summary>
        /// Abre menu de repasse
        /// </summary>
        /// <param name="Driver"></param>
        public void ClicarMenuRepasse()
        {
            ClicarBotaoMenuPaginaInicial("btnAdmnstrcRelatrioRelatriodeContrataoAnaltico");
        }

        /// <summary>
        /// Abre menu de coparticipação
        /// </summary>
        /// <param name="Driver"></param>
        public void ClicarMenuCoparticipacao()
        {
            ClicarBotaoMenuPaginaInicial("btnAdmnstrcRelatriosNovoFIESRelatriodeRepasseAnaliacuteticoIES");
        }

        /// <summary>
        /// Consulta CPF do aluno na página de aditamento
        /// </summary>
        /// <param name="Driver"></param>
        /// <param name="aluno"></param>
        public void ConsultarAluno(TOAluno aluno)
        {
            EsperarPaginaCarregando();
            EsperarCarregamento("input-medium cpf", false);

            ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(0, 0)");

            ClicarElemento(By.CssSelector("body"));

            ClicarElemento(By.Id("btnLimpar"));

            if (Program.versaoApresentacao == true)
            {
                var jsExecutor = (IJavaScriptExecutor)Driver;
                jsExecutor.ExecuteScript("document.getElementById('cpf').setAttribute('type', 'password')");
            }
            ClicarElemento(By.Id("cpf"));

            var executor = (IJavaScriptExecutor)Driver;
            executor.ExecuteScript($@"document.getElementById(""cpf"").value = ""{aluno.Cpf}"";");

            while (Driver.PageSource.Contains("Selecione") == false)
            {
                System.Threading.Thread.Sleep(500);
            }
            ClicarElemento(By.CssSelector("body"));
            string erro = BuscarMensagemDeErro();
            if (erro.Contains("CPF com Dígito Verificador inválido. Redigite-o!") == true)
            {
                Util.EditarConclusaoAluno(aluno, erro);
                throw new PararExecucaoException();
            }
            ClicarElemento(By.Id("btnConsultar"));
            if (Program.versaoApresentacao == true)
            {
                var jsExecutor = (IJavaScriptExecutor)Driver;
                jsExecutor.ExecuteScript("document.getElementById('gridResult').setAttribute('style', 'color: rgba(0, 0, 0, 0)!important;')");
            }
            erro = BuscarMensagemDeErro();
            if (erro.Contains("Ocorreu um erro na consulta, tente novamente."))
            {
                throw new Exception("O site da caixa parece estar instável. Tente novamente mais tarde.");
            }
            EsperarPaginaCarregando();
            if (VerificarNenhumaInformacaoDisponivel() == true)
            {
                //Util.EditarConclusaoAluno(aluno, "Nenhuma informação disponível");
               aluno.Temporario = "Feito";
               Util.EditarConclusaoAluno(aluno, aluno.Conclusao);
               throw new PararExecucaoException();
            }

            
        }

        /// <summary>
        /// Procura mensagem de erro do topo da página
        /// </summary>
        /// <returns>A mensagem encontrada</returns>
        protected string BuscarMensagemDeErro()
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
        public void ClicarElementoPorIDJavaScript(string id)
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
        public void BuscarEAbrirDRM(TOAluno aluno, string semestreAtual)
        {
            ConsultarAluno(aluno);
            if (VerificarNenhumaInformacaoDisponivel() == true)
            {
                aluno.Temporario = "Feito";
                Util.EditarConclusaoAluno(aluno, aluno.Conclusao);
                return;
            }

            IWebElement botaoImprimirTermo = BuscarBotaoSemestre(semestreAtual);

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
                    ScrollParaElemento(By.Id(botaoImprimirTermo.GetAttribute("id")));
                    botaoImprimirTermo.Click();

                    System.Threading.Thread.Sleep(1000);

                    ClicarElemento(By.Id("btnConfirmar"));

                    msgErro = (bool)executor.ExecuteScript("return $('.alert.alert-error').is(':visible');");
                    if (msgErro == true)
                    {
                        ClicarElemento(By.Id("btnConsultar"));
                        EsperarPaginaCarregando();
                    }
                }

                //Verificação em caso de alerta do site
                if (Driver.PageSource.Contains("MDLalerta_") == true)
                {
                    aluno.Conclusao = Driver.FindElement(By.XPath("/html/body/div[7]/div[2]/p")).Text;
                    ClicarElemento(By.Id("btnConfirmar"));
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
                    situacaoAluno = BuscarSituacaoAluno(semestreAtual);
                }
                else
                {
                    situacaoAluno = "Não Feito";
                }

                aluno.Conclusao = situacaoAluno;
                aluno.Temporario = "Feito";
            }
        }

        /// <summary>
        /// Verifica se a página atual contém "Nenhuma informação disponível"
        /// </summary>
        /// <param name="Driver"></param>
        /// <returns>True se houver e false se não</returns>
        public bool VerificarNenhumaInformacaoDisponivel()
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
        private IWebElement BuscarBotaoSemestre(string semestreAtual)
        {
            string semestreFormatado = semestreAtual.Split('/')[0];
            string anoFormatado = semestreAtual.Split('/')[1];

            //Busca o botao que representa o ano/semestre correto
            if (Driver.PageSource.Contains(semestreAtual) == true)
            {
                if (Driver.PageSource.Contains("btnImprimirTermo"))
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
                else
                {
                    return null;
                }
            }
            return null;
        }

        /// <summary>
        /// Retorna situacao do semestre especificado do aluno no datagrid do site
        /// </summary>
        /// <param name="semestreAtual">Semestre atual (semestre/ano)</param>
        /// <returns>Conclusão encontrada</returns>
        public string BuscarSituacaoAluno(string semestreAtual)
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
