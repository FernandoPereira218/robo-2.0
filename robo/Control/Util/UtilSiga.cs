using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Robo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace robo
{
    /// <summary>
    /// Métodos comuns que podem ser utilizados no site do SIGA
    /// </summary>
    class UtilSiga : UtilSelenium
    {
        /// <summary>
        /// Busca um aluno por CPF
        /// </summary>
        /// <param name="Driver"></param>
        /// <param name="aluno"></param>
        protected void FiltraAluno(IWebDriver Driver, TOAluno aluno)
        {
            try
            {
                WaitLoading(Driver);
                ClickAndWriteById(Driver, "pess_cpf", aluno.Cpf);
                ClickButtonsById(Driver, "btn_filtrar");
            }
            catch (NoSuchElementException)  
            {
                WaitLoading(Driver);
                ClickAndWriteById(Driver, "pess_cpf", aluno.Cpf);
                ClickButtonsById(Driver, "btn_filtrar");
            }
            catch (ElementClickInterceptedException)
            {
                WaitLoading(Driver);
                ClickAndWriteById(Driver, "pess_cpf", aluno.Cpf);
                ClickButtonsById(Driver, "btn_filtrar");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Busca se
        /// </summary>
        /// <param name="semestreAno">Semestre utilizado (semestre/ano)</param>
        /// <returns>Semestre no formato utilizado nos atributos do SIGA -> Ex.: 2/2021 -> 2021210</returns>
        protected string BuscarSemestreSiga(string semestreAno)
        {
            // 2/2021 -> 2021210
            string semestre = semestreAno.Split('/')[0];
            string ano = semestreAno.Split('/')[1];

            string final = ano + semestre + "10";

            return final;
        }

        /// <summary>
        /// Abre site do SIGA e tenta fazer login após usuário ter clicado no Captcha
        /// </summary>
        /// <param name="url"></param>
        /// <param name="login"></param>
        /// <returns></returns>
        public IWebDriver FazerLogin(string url, TOLogin login)
        {
            IWebDriver Driver;

            var firefoxDriverService = FirefoxDriverService.CreateDefaultService(Environment.CurrentDirectory + @"\driver");
            firefoxDriverService.HideCommandPromptWindow = true;
            Driver = new FirefoxDriver(firefoxDriverService);

            ((IJavaScriptExecutor)Driver).ExecuteScript("alert('" + url + "')");
            DialogResult resultado = MessageBox.Show("Abra o site na mensagem de alerta do navegador e clique no captcha.\nApós isso, volte para esta mensagem e clique em 'Ok'.", "Clique no captcha!", MessageBoxButtons.OKCancel);
            if (resultado == DialogResult.Cancel)
            {
                return null;
            }

            ClickAndWriteByName(Driver, "login", login.Usuario);
            ClickAndWriteById(Driver, "senha_ls", login.Senha);

            ClickButtonsByXpath(Driver, "/html/body/table/tbody/tr/td/table/tbody/tr[6]/td/div/form/div/table/tbody/tr[2]/td/table/tbody/tr/td[2]/table/tbody/tr[6]/td/input");

            return Driver;
        }

        /// <summary>
        /// Espera até o elemento "divCarregando" não estar mais presente na página
        /// </summary>
        /// <param name="driver"></param>
        protected void WaitLoading(IWebDriver driver)
        {
            IWebElement carregando;
            try
            {
                carregando = driver.FindElement(By.Id("divCarregando"));
            }
            catch (NoSuchElementException)
            {
                while (driver.PageSource.Contains("divCarregando") == false)
                {
                    Sleep();
                }
                carregando = driver.FindElement(By.Id("divCarregando"));

            }
            while (carregando.Displayed == true)
            {
                Sleep();
            }
        }
    }
}
