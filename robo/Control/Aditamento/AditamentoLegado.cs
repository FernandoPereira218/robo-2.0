using AForge.Imaging.Filters;
using OpenQA.Selenium;
using robo.Control.Legado;
using Robo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace robo.Control
{
    public class AditamentoLegado : UtilFiesLegado
    {
        static IWebDriver Driver;
        public void AditamentoFiesLegado(IWebDriver driver, TOLogin login, TOAluno aluno, string numSemestre)
        {
            Driver = driver;
            try
            {
                //Aqui começa o aditamento
                //            MetodoAditamento(login, alunos, numSemestre);

                Util.ClickButtonsByCss(Driver, "div:nth-child(3) > ul > .menu-button:nth-child(2) > a");
                if (Dados.DRIExists(aluno.Cpf))
                {
                    TODRI driAtual = Dados.GetDRI(aluno.Cpf);
                    string url = string.Format("http://sisfies.mec.gov.br/cpsa/aditamento/formulario/co_inscricao/{0}/sem/{1}", driAtual.DRI, numSemestre);
                    Driver.Url = url;

                    if (Driver.PageSource.Contains("Voltar para a página principal"))
                    {
                        Util.EditarConclusaoAluno(aluno, "Página não encontrada");
                        Driver.Url = "http://sisfies.mec.gov.br/cpsa/aditamento";
                        return;
                    }

                    WaitinLoading(Driver);

                    if (VerificaErro(Driver, aluno) == false)
                    {
                        //Mensagem que não aparece somente quando o aluno já foi aditado anteriomente
                        if (Driver.PageSource.Contains("igual ou superior a 75% no semestre"))
                        {
                            PreencherFormulario(aluno);
                        }
                        else
                        {
                            Util.EditarConclusaoAluno(aluno, "Acadêmico aditado anteriormente");
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    Util.EditarConclusaoAluno(aluno, "DRI não encontrada");
                }

            }
            catch (Exception e)
            {
                Driver.Quit();
                Driver.Dispose();
                throw e;
            }
            finally
            {
                //Driver.Quit();
                //Driver.Dispose();
            }

        }

        private void PreencherFormulario(TOAluno aluno)
        {


            WaitinLoading(Driver);

            PreencheReceitas(aluno);

            if ("Aproveitamento Superior a 75%".Equals(aluno.AproveitamentoAtual.Trim()) || "Aproveitamento em análise (estágio)".Equals(aluno.AproveitamentoAtual.Trim()))
            {
                CasoComAproveitamento(aluno.Justificativa);
            }
            else
            {
                Util.ClickButtonsByCss(Driver, "#divAproveitamentoAcademico input:nth-of-type(1)"); //O estudante teve aproveitamento acadêmico igual ou superior a 75% no semestre ? NAO
                if (aluno.HistoricoAproveitamento.Contains("Excesso de reprovação") == true)
                {
                    Util.EditarConclusaoAluno(aluno, "Rejeitou excesso de reprovação");
                }
                else
                {
                    CasoSemAproveitamento(aluno);
                }

            }
            SystemSounds.Beep.Play();

            ScrollToElementByID(Driver, "captcha-imagem");
            var element = Driver.FindElement(By.Id("captcha-imagem"));
            Screenshot scr = ((ITakesScreenshot)element).GetScreenshot();
            scr.SaveAsFile("img\\teste.png");
            LimparCaptcha();

            Thread thread = new Thread(LerCaptcha);
            thread.Start();
            thread.Join();

            bool possuiErros = false;
            while (Driver.Url.StartsWith("http://sisfies.mec.gov.br/cpsa/aditamento/formulario/") == true && possuiErros == false)
            {
                possuiErros = PossuiErros(Driver.PageSource);
                System.Threading.Thread.Sleep(100);
            }
            ChecarSePossuiErros(possuiErros, aluno);
            //Marca resultado aditamento
            VerificaErro(Driver, aluno);

        }
        private void PreencheReceitas(TOAluno aluno)
        {
            //Clica e Digita no Valor da Semestralidade SEM desconto – Grade Curricular Regular
            Util.ClickAndWriteById(Driver, "vl_semestre_sem_desconto", aluno.ReceitaBruta);

            //Clica e Digita no Valor da Semestralidade COM desconto – Grade Curricular Regular
            Util.ClickAndWriteById(Driver, "vl_semestre_com_desconto", aluno.ReceitaLiquida);

            //Clica e Digita no Valor da semestralidade para o FIES R$
            Util.ClickAndWriteById(Driver, "vl_semestralidade_para_fies", aluno.ReceitaFies);

            //Clica e Digita no Valor da Semestralidade ATUAL COM desconto - Grade Curricular a ser Cursada
            Util.ClickAndWriteById(Driver, "vl_semestre_atual", aluno.ReceitaFies);

            //Pegar Valor a ser financiado no semestre ATUAL com recursos do FIES - Valor drm financiamento
            aluno.ValorAditadoFinanciamento = Driver.FindElement(By.Id("vl_financiado_semestre")).Text;

            //Pegar Valor a ser pago no semestre ATUAL com recursos do estudante - 
            aluno.ValorPagoRecursoEstudante = Driver.FindElement(By.Id("vlMesSemestreEstudante")).Text;
        }
        private void CasoComAproveitamento(string justificativaAluno)
        {
            //O estudante teve aproveitamento acadêmico igual ou superior a 75% no semestre ? SIM
            Util.ClickButtonsByCss(Driver, "#divAproveitamentoAcademico input:nth-child(3)");

            //O estudante está regularmente matriculado? SIM
            Util.ClickButtonsByCss(Driver, "#divRegularidadeMatricula input:nth-child(3)");

            //O estudante possui benefício simultâneo de FIES e de bolsa ProUni em local de oferta ou curso distinto? NAO
            Util.ClickButtonsByXpath(Driver, "(//input[@name=\'beneficio\'])[1]");

            //O prazo de duração regular do curso encontra-se vigente? SIM
            Util.ClickButtonsByCss(Driver, "#divPrazoCurso input:nth-child(3)");

            //O estudante transferiu de curso mais de uma vez nessa IES? NAO
            Util.ClickButtonsByCss(Driver, "#divMudancaCurso input:nth-child(2)");

            //Duração regular do curso MARCAR A CHECKBOX (SE APARECER)
            if (Driver.FindElement(By.Name("checkNaoAlteraCurso[]")).Displayed)
            {
                Util.ClickButtonsByName(Driver, "checkNaoAlteraCurso[]");
            }

            //checa se existe e escreve a justificativa
            Justificativa(justificativaAluno);

            //Duração regular do curso MARCAR A CHECKBOX (SE APARECER)
            if (Driver.FindElement(By.Name("checkNaoAlteraCurso[]")).Displayed)
            {
                if (!Driver.FindElement(By.Name("checkNaoAlteraCurso[]")).Selected)
                {
                    Util.ClickButtonsByName(Driver, "checkNaoAlteraCurso[]");
                }
            }
        }
        private void Justificativa(string justificativaAluno)
        {
            IWebElement justificativa = Driver.FindElement(By.Id("divVariacao"));
            if (justificativa.Displayed)
            {
                if (justificativa.Text.Contains("Valor da semestralidade COM desconto"))
                {
                    //Alteração na grade curricular em relação ao semestre anterior
                    //Util.ClickAndWriteById(Driver, "ds_justificativa", "                                                                                    "); // Sim, é pra ser assim
                    if (justificativaAluno == string.Empty)
                    {
                        Util.ClickAndWriteById(Driver, "ds_justificativa", "Alteração na grade curricular em relação ao semestre anterior"); // Sim, é pra ser assim
                    }
                    else
                    {
                        Util.ClickAndWriteById(Driver, "ds_justificativa", justificativaAluno);
                    }
                }
            }
        }
        private void CasoSemAproveitamento(TOAluno aluno)
        {
            //tratamento do erro de caso a pessoa estiver na 3º reconsideração na realidade
            if (!Driver.FindElement(By.Id("divRejeicaoAutomatica")).Displayed)
            {
                //A CPSA irá liberar o aditamento nesta situação? SIM
                Util.ClickButtonsByCss(Driver, "span:nth-child(4) > input:nth-child(2)");

                //Justificativa: ESCREVER "Nrº reconsideração" OU ALGO DO TIPO
                Util.ClickAndWriteByName(Driver, "justificativa", aluno.HistoricoAproveitamento);

                //O estudante está regularmente matriculado? SIM
                Util.ClickButtonsByCss(Driver, "#divRegularidadeMatricula input:nth-child(3)");

                //O estudante possui benefício simultâneo de FIES e de bolsa ProUni em local de oferta ou curso distinto? NAO
                Util.ClickButtonsByXpath(Driver, "(//input[@name=\'beneficio\'])[1]");

                //O prazo de duração regular do curso encontra-se vigente? SIM
                Util.ClickButtonsByCss(Driver, "#divPrazoCurso input:nth-child(3)");

                //O estudante transferiu de curso mais de uma vez nessa IES? NAO
                Util.ClickButtonsByCss(Driver, "#divMudancaCurso input:nth-child(2)");

                //Duração regular do curso MARCAR A CHECKBOX SE APARECER
                if (Driver.FindElement(By.Name("checkNaoAlteraCurso[]")).Displayed)
                {
                    Util.ClickButtonsByName(Driver, "checkNaoAlteraCurso[]");
                }

                //checa se existe e escreve a justificativa
                Justificativa(aluno.Justificativa);

                //Duração regular do curso MARCAR A CHECKBOX (SE APARECER)
                if (Driver.FindElement(By.Name("checkNaoAlteraCurso[]")).Displayed)
                {
                    if (!Driver.FindElement(By.Name("checkNaoAlteraCurso[]")).Selected)
                    {
                        Util.ClickButtonsByName(Driver, "checkNaoAlteraCurso[]");
                    }
                }
            }
            else
            {
                Util.EditarConclusaoAluno(aluno, "Rejeitou excesso de reprovação");
                Driver.Url = "http://sisfies.mec.gov.br/cpsa/aditamento";
            }
        }
            
           
        private void LimparCaptcha()
        {
            Bitmap imagem = new Bitmap("img\\teste.png");
            imagem = imagem.Clone(new Rectangle(0, 0, imagem.Width, imagem.Height), System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            Invert inverter = new Invert();
            Opening open = new Opening();
            BlobsFiltering bc = new BlobsFiltering();
            GaussianSharpen gs = new GaussianSharpen();
            ContrastCorrection cc = new ContrastCorrection();
            bc.MinHeight = 10;
            FiltersSequence seq = new FiltersSequence(gs, inverter, open, inverter, bc, inverter, cc, bc, inverter);
            seq.Apply(imagem).Save("img\\captchaLimpo.png");
        }

        private async void LerCaptcha()
        {

            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.Timeout = new TimeSpan(1, 1, 1);


                MultipartFormDataContent form = new MultipartFormDataContent();
                form.Add(new StringContent("991ab1ca4c88957"), "apikey"); //Added api key in form data
                form.Add(new StringContent("eng"), "language");

                form.Add(new StringContent("2"), "ocrengine");
                form.Add(new StringContent("true"), "scale");
                form.Add(new StringContent("true"), "istable");


                byte[] imageData = File.ReadAllBytes("img\\captchaLimpo.png");
                form.Add(new ByteArrayContent(imageData, 0, imageData.Length), "image", "captchaLimpo.png");
                HttpResponseMessage response = await httpClient.PostAsync("https://api.ocr.space/Parse/Image", form);

                string strContent = await response.Content.ReadAsStringAsync();
                string resultado = strContent.Split(new string[] { "WordText" }, StringSplitOptions.None)[1];
                resultado = resultado.Split(',')[0];
                resultado = resultado.Replace("\"", "");
                resultado = resultado.Replace(":", "");
                resultado = resultado.Replace("0", "o");
                resultado = resultado.Replace("$", "5");
                resultado = resultado.Replace(")", "j");
                resultado = resultado.ToLower();
                Util.ScrollToElementByID(Driver, "captcha");
                Util.ClickAndWriteById(Driver, "captcha", resultado);
                Util.ClickButtonsById(Driver, "validar");
                Util.ClickButtonsByXpath(Driver, "/html/body/div[8]/div[3]/div/button[2]/span");




            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        private Boolean PossuiErros(String pageSource)
        {
            if (pageSource.Contains("lista-mensageiro-erros"))
            {

                return pageSource.Contains("(E0336) Código de verificação inválido.") ||
                       pageSource.Contains("(E0019) - O valor da semestralidade com desconto não pode ser superior a") ||
                       pageSource.Contains(" O valor do campo “Valor da semestralidade para o FIES” não pode ultrapassar 95% do campo “Valor da Semestralidade COM desconto”.");

            }
            return false;
        }

        private void ChecarSePossuiErros(bool possuiErros, TOAluno aluno)
        {
            if (possuiErros)
            {
                if (Driver.PageSource.Contains("(E0336) Código de verificação inválido."))
                {
                    Driver.Navigate().Refresh();
                    PreencherFormulario(aluno);
                }
                else
                {
                    IWebElement listaMensageirosErros = Driver.FindElement(By.Id("lista-mensageiro-erros"));
                    IWebElement lista = listaMensageirosErros.FindElement(By.XPath(".//li"));
                    Util.EditarConclusaoAluno(aluno, lista.Text);
                }
               
            }

        }
    }
}