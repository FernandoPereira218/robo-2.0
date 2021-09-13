﻿using OpenQA.Selenium;
using Robo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robo.Control
{
    public class UtilFiesNovo
    {
        /// <summary>
        /// Faz login na página da Caixa
        /// </summary>
        /// <param name="Driver"></param>
        /// <param name="login"></param>
        public void FazerLogin(IWebDriver Driver, TOLogin login)
        {
            Util.ClickAndWriteById(Driver, "username", login.Usuario);
            Util.ClickButtonsById(Driver, "button-submit");
            Util.ClickAndWriteById(Driver, "password", login.Senha);
            Util.ClickButtonsByCss(Driver, "button:nth-child(1)");
        }

        public void FazerLogout(IWebDriver Driver)
        {

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

        public void ConsultarAluno(IWebDriver Driver, TOAluno aluno)
        {
            WaitForLoading(Driver);
            WaitPageLoading(Driver, "input-medium cpf", false);

            ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(0, 0)");

            Util.ClickButtonsByCss(Driver, "body");

            Util.ClickButtonsById(Driver, "btnLimpar");

            Util.ClickButtonsById(Driver, "cpf");

            var executor = (IJavaScriptExecutor)Driver;
            executor.ExecuteScript($@"document.getElementById(""cpf"").value = ""{aluno.Cpf}"";");

            while (Driver.PageSource.Contains("Selecione") == false)
            {
                System.Threading.Thread.Sleep(500);
            }
            Util.ClickButtonsById(Driver, "btnConsultar");

            WaitForLoading(Driver);
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
                    Util.ScrollToElementByID(Driver, botaoImprimirTermo.GetAttribute("id"));
                    botaoImprimirTermo.Click();

                    System.Threading.Thread.Sleep(1000);

                    Util.ClickButtonsById(Driver, "btnConfirmar");

                    msgErro = (bool)executor.ExecuteScript("return $('.alert.alert-error').is(':visible');");
                    if (msgErro == true)
                    {
                        Util.ClickButtonsById(Driver, "btnConsultar");
                        WaitForLoading(Driver);
                    }
                }

                //Verificação em caso de alerta do site
                if (Driver.PageSource.Contains("MDLalerta_") == true)
                {
                    aluno.Conclusao = Driver.FindElement(By.XPath("/html/body/div[7]/div[2]/p")).Text;
                    Util.ClickButtonsById(Driver, "btnConfirmar");
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

        private bool VerificarNenhumaInformacaoDisponivel(IWebDriver Driver)
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
        private static string BuscarSituacaoAluno(IWebDriver Driver, string semestreAtual)
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