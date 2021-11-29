using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using robo.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace robo.Utils
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
        protected void FiltraAluno(TOAluno aluno)
        {
            WaitLoading();
            try
            {
                ClickAndWriteById("pess_cpf", aluno.Cpf);
                ClickButtonsById("btn_filtrar");
            }
            catch (Exception e)
            {
                if (e is NoSuchElementException || e is ElementClickInterceptedException)
                {
                    FiltraAluno(aluno);
                    return;
                }
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
        public void FazerLogin(TOLogin login)
        {
            ClickAndWriteByName("login", login.Usuario);
            ClickAndWriteById("senha_ls", login.Senha);
            ClickButtonsByXpath("/html/body/table/tbody/tr/td/table/tbody/tr[6]/td/div/form/div/table/tbody/tr[2]/td/table/tbody/tr/td[2]/table/tbody/tr[6]/td/input");
        }

        /// <summary>
        /// Espera até o elemento "divCarregando" não estar mais presente na página
        /// </summary>
        /// <param name="Driver"></param>
        protected void WaitLoading()
        {
            IWebElement carregando;
            try
            {
                carregando = Driver.FindElement(By.Id("divCarregando"));
            }
            catch (NoSuchElementException)
            {
                while (Driver.PageSource.Contains("divCarregando") == false)
                {
                    Sleep();
                }
                carregando = Driver.FindElement(By.Id("divCarregando"));

            }
            while (carregando.Displayed == true)
            {
                Sleep();
            }
        }
    }
}
