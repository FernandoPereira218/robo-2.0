﻿using OpenQA.Selenium;
using robo.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robo.Utils
{
    /// <summary>
    /// Métodos comuns para serem utilizados no site do MEC
    /// </summary>
    public class UtilFiesLegado : UtilSelenium
    {
        /// <summary>
        /// Tenta realizar login no site do MEC
        /// </summary>
        /// <param name="login"></param>
        /// <param name="Driver"></param>
        /// <returns>Retorna true se o login foi realizado com sucesso</returns>
        public void RealizarLoginSucesso(TOLogin login)
        {
            while (Driver.PageSource.Contains("img/titAcessoInstituicao.gif") == false)
            {
                System.Threading.Thread.Sleep(500);
            }
            ClicarElemento(By.CssSelector("#link-instituicao img:nth-child(1)"));

            ClicarElemento(By.CssSelector("center:nth-child(10) td:nth-child(2) .guest-box:nth-child(1) span:nth-child(2)"));
            while (Driver.Url.Contains("InitAuthenticationByIdentifierAndPassword") == false)
            {
                System.Threading.Thread.Sleep(100);
            }
            ClicarEEscrever(By.Id("id"), login.Usuario);
            ClicarEEscrever(By.Id("pw"), login.Senha);

            ClicarElemento(By.Id("botoes"));
            if (!Driver.PageSource.Contains("A senha informada não confere. Número de tentativas restAes:"))//Ocorreu uma falha na execução da aplicação. A caixa de erro ao lado mostra o motivo da falha. Provavelmente alguma informação incorreta foi processada.
            {
                SelecionarPerfilPresidencia();
            }
            else
            {
                throw new Exception("A senha informada não confere. Por favor, cheque se todos logins foram inseridos corretamente.");
            }

        }

        /// <summary>
        /// Seleciona o perfil correto de "Presidência" no site do MEC
        /// </summary>
        /// <param name="Driver"></param>
        public void SelecionarPerfilPresidencia()
        {
            while (Driver.PageSource.Contains("Aditamentos FIES") == false)
            {
                try
                {
                    Driver.FindElement(By.XPath("//select[@name='co_perfil']/option[contains(.,'CPSA Presidência')]")).Click();
                }
                catch (NoSuchElementException)
                {
                    break;
                }
            }
            EsperarReadyState();
        }

        /// <summary>
        /// Espera até a tela de carregando do site não ser mais visível
        /// </summary>
        /// <param name="Driver"></param>
        public void EsperarLoading()
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

        /// <summary>
        /// Realiza logout do site
        /// </summary>
        /// <param name="Driver"></param>
        public void FazerLogout()
        {
            ClicarElemento(By.XPath("//a[contains(text(),'Sair')]"));
        }

        /// <summary>
        /// Abre menu de DRIs
        /// </summary>
        /// <param name="Driver"></param>
        public void SelecionarMenuDRI()
        {
            ClicarElemento(By.XPath("//a[contains(text(),'Validação pela CPSA Fies')]"));
        }

        /// <summary>
        /// Abre menu de baixar documentos (DRM, DRD, DRT e Suspensão)
        /// </summary>
        /// <param name="Driver"></param>
        public void SelecionarMenuBaixarDocumentos()
        {
            ClicarElemento(By.CssSelector("div:nth-child(3) > ul > .menu-button:nth-child(3) > a"));
        }
        
        /// <summary>
        /// Abre menu de extrato mensal de repasse
        /// </summary>
        /// <param name="Driver"></param>
        public void SelecionarMenuExtratoMensalDeRepasse()
        {
            ClicarElemento(By.XPath("/html/body/div[3]/div[4]/div[1]/div[4]/ul/li[1]/a"));
        }

        /// <summary>
        /// Verifica se há alguma mensagem no topo da página e retorna a mensagem encontrada
        /// </summary>
        /// <returns></returns>
        public string VerificarMensagem()
        {
            IWebElement listaME = Driver.FindElement(By.Id("lista-mensageiro-erros"));
            if(listaME.Displayed)
            {
                IWebElement listaF = listaME.FindElement(By.XPath(".//li"));
                return listaF.Text;
            }
            return string.Empty;
        }

        /// <summary>
        /// Clica no drop down do menu selecionado
        /// </summary>
        /// <param name="Driver"></param>
        /// <param name="tipoRelatorio">DRM, DRT, DRD ou Suspensao</param>
        /// <returns></returns>
        public string SelecionarTipoRelatorio(string tipoRelatorio)
        {
            switch (tipoRelatorio)
            {
                case "DRM":
                    SelecionarOpcaoDropDown("id", "co_finalidade_aditamento", "Aditamento de Renovação");
                    return "Aditamento de Renovação";
                case "DRT":
                    SelecionarOpcaoDropDown("id", "co_finalidade_aditamento", "Aditamento de Transferência");
                    return "Aditamento de Transferência";
                case "DRD":
                    SelecionarOpcaoDropDown("id", "co_finalidade_aditamento", "Aditamento de Dilatação");
                    return "Aditamento de Dilatação";
                case "SUSPENSÃO":
                    SelecionarOpcaoDropDown("id", "co_finalidade_aditamento", "Suspensão");
                    return "Suspensão";
                default:
                    return string.Empty;
            }
        }
    }
}
