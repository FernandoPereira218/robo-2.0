using AForge.Imaging.Filters;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net.Http;
using System.Text;
using System.Threading;
using robo.Utils;
using System.Threading.Tasks;
using robo.TO;
using robo.Contratos;
using robo.Banco_de_Dados;

namespace robo.Modos_de_Execucao.FIES_Legado
{
    public class AditamentoLegado : UtilFiesLegado, IModosDeExecucao.IModoComAlunos
    {
        private string numSemestre;
        public AditamentoLegado(string numSemestre)
        {
            this.numSemestre = numSemestre;
        }
        public void AditamentoFiesLegado(TOAluno aluno)
        {

            
            if (Dados.VerificarDRI(aluno.Cpf) == false)
            {
                Util.EditarConclusaoAluno(aluno, "DRI não encontrada");
                return;
            }

            AcessarPaginaAditamento(aluno, numSemestre);

            if (Driver.PageSource.Contains("Voltar para a página principal"))
            {
                Util.EditarConclusaoAluno(aluno, "Página não encontrada");
                Driver.Url = "http://sisfies.mec.gov.br/cpsa/aditamento";
                return;
            }

            WaitinLoading();

            RealizarAditamento(aluno);
        }
        private void RealizarAditamento(TOAluno aluno)
        {
            string mensagem = VerificarMensagem();
            if (mensagem == string.Empty)
            {
                //Mensagem que não aparece somente quando o aluno já foi aditado anteriomente
                if (Driver.PageSource.Contains("igual ou superior a 75% no semestre"))
                {
                    while (aluno.Conclusao == "Não Feito")
                    {
                        PreencherFormulario(aluno);
                    }
                }
                else
                {
                    Util.EditarConclusaoAluno(aluno, "Acadêmico aditado anteriormente");
                }
            }
            else
            {
                Util.EditarConclusaoAluno(aluno, mensagem);
            }
        }
        private void AcessarPaginaAditamento(TOAluno aluno, string numSemestre)
        {
            TODRI driAtual = Dados.BuscarDRI(aluno.Cpf);
            string url = string.Format("http://sisfies.mec.gov.br/cpsa/aditamento/formulario/co_inscricao/{0}/sem/{1}", driAtual.DRI, numSemestre);
            Driver.Url = url;
        }
        private void PreencherFormulario(TOAluno aluno)
        {
            WaitinLoading();

            PreencheReceitas(aluno);
            PreencherAproveitamentoAluno(aluno);

            if (aluno.Conclusao != "Não Feito")
            {
                return;
            }

            SalvarImagemCaptcha();
            LimparImagemCaptcha();

            Thread thread = new Thread(PreencherCaptcha);
            thread.Start();
            thread.Join();

            bool possuiErros = false;
            while (Driver.Url.StartsWith("http://sisfies.mec.gov.br/cpsa/aditamento/formulario/") == true && possuiErros == false)
            {
                possuiErros = PossuiErros(Driver.PageSource);
                Thread.Sleep(100);
            }
            ChecarSePossuiErros(possuiErros, aluno);

            string mensagemAditamento = VerificarMensagem();
            if (mensagemAditamento != "")
            {
                Util.EditarConclusaoAluno(aluno, mensagemAditamento);
            }
        }
        private void SalvarImagemCaptcha()
        {
            ScrollToElementByID( "captcha-imagem");
            var element = Driver.FindElement(By.Id("captcha-imagem"));
            Screenshot scr = ((ITakesScreenshot)element).GetScreenshot();
            Util.CriarDiretorioCasoNaoExista("img");
            scr.SaveAsFile("img\\teste.png");
        }
        private void PreencherAproveitamentoAluno(TOAluno aluno)
        {
            if ("Aproveitamento Superior a 75%".Equals(aluno.AproveitamentoAtual.Trim()) || "Aproveitamento em análise (estágio)".Equals(aluno.AproveitamentoAtual.Trim()))
            {
                CasoComAproveitamento(aluno.Justificativa);
            }
            else if (aluno.HistoricoAproveitamento.Contains("Excesso de reprovação") == true)
            {
                Util.EditarConclusaoAluno(aluno, "Rejeitou excesso de reprovação");
            }
            else
            {
                ClickButtonsByCss( "#divAproveitamentoAcademico input:nth-of-type(1)");
                CasoSemAproveitamento(aluno);
            }
        }
        private void PreencheReceitas(TOAluno aluno)
        {
            //Clica e Digita no Valor da Semestralidade SEM desconto – Grade Curricular Regular
            ClickAndWriteById( "vl_semestre_sem_desconto", aluno.ReceitaBruta);

            //Clica e Digita no Valor da Semestralidade COM desconto – Grade Curricular Regular
            ClickAndWriteById( "vl_semestre_com_desconto", aluno.ReceitaLiquida);

            //Clica e Digita no Valor da semestralidade para o FIES R$
            ClickAndWriteById( "vl_semestralidade_para_fies", aluno.ReceitaFies);

            //Clica e Digita no Valor da Semestralidade ATUAL COM desconto - Grade Curricular a ser Cursada
            ClickAndWriteById( "vl_semestre_atual", aluno.ReceitaFies);

            //Pegar Valor a ser financiado no semestre ATUAL com recursos do FIES - Valor drm financiamento
            aluno.ValorAditadoFinanciamento = Driver.FindElement(By.Id("vl_financiado_semestre")).Text;

            //Pegar Valor a ser pago no semestre ATUAL com recursos do estudante - 
            aluno.ValorPagoRecursoEstudante = Driver.FindElement(By.Id("vlMesSemestreEstudante")).Text;
        }
        private void CasoComAproveitamento(string justificativaAluno)
        {
            //O estudante teve aproveitamento acadêmico igual ou superior a 75% no semestre ? SIM
            ClickButtonsByCss( "#divAproveitamentoAcademico input:nth-child(3)");

            //O estudante está regularmente matriculado? SIM
            ClickButtonsByCss( "#divRegularidadeMatricula input:nth-child(3)");

            //O estudante possui benefício simultâneo de FIES e de bolsa ProUni em local de oferta ou curso distinto? NAO
            ClickButtonsByXpath( "(//input[@name=\'beneficio\'])[1]");

            //O prazo de duração regular do curso encontra-se vigente? SIM
            ClickButtonsByCss( "#divPrazoCurso input:nth-child(3)");

            //O estudante transferiu de curso mais de uma vez nessa IES? NAO
            ClickButtonsByCss( "#divMudancaCurso input:nth-child(2)");

            //Duração regular do curso MARCAR A CHECKBOX (SE APARECER)
            if (Driver.FindElement(By.Name("checkNaoAlteraCurso[]")).Displayed)
            {
                ClickButtonsByName( "checkNaoAlteraCurso[]");
            }

            //checa se existe e escreve a justificativa
            EscreverJustificativa(justificativaAluno);

            //Duração regular do curso MARCAR A CHECKBOX (SE APARECER)
            if (Driver.FindElement(By.Name("checkNaoAlteraCurso[]")).Displayed)
            {
                if (!Driver.FindElement(By.Name("checkNaoAlteraCurso[]")).Selected)
                {
                    ClickButtonsByName( "checkNaoAlteraCurso[]");
                }
            }
        }
        private void EscreverJustificativa(string justificativaAluno)
        {
            IWebElement justificativa = Driver.FindElement(By.Id("divVariacao"));
            if (justificativa.Displayed)
            {
                if (justificativa.Text.Contains("Valor da semestralidade COM desconto"))
                {
                    if (justificativaAluno == string.Empty)
                    {
                        ClickAndWriteById( "ds_justificativa", "Alteração na grade curricular em relação ao semestre anterior");
                    }
                    else
                    {
                        ClickAndWriteById( "ds_justificativa", justificativaAluno);
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
                ClickButtonsByCss( "span:nth-child(4) > input:nth-child(2)");

                //Justificativa: ESCREVER "Nrº reconsideração" OU ALGO DO TIPO
                ClickAndWriteByName( "justificativa", aluno.HistoricoAproveitamento);

                //O estudante está regularmente matriculado? SIM
                ClickButtonsByCss( "#divRegularidadeMatricula input:nth-child(3)");

                //O estudante possui benefício simultâneo de FIES e de bolsa ProUni em local de oferta ou curso distinto? NAO
                ClickButtonsByXpath( "(//input[@name=\'beneficio\'])[1]");

                //O prazo de duração regular do curso encontra-se vigente? SIM
                ClickButtonsByCss( "#divPrazoCurso input:nth-child(3)");

                //O estudante transferiu de curso mais de uma vez nessa IES? NAO
                ClickButtonsByCss( "#divMudancaCurso input:nth-child(2)");

                //Duração regular do curso MARCAR A CHECKBOX SE APARECER
                if (Driver.FindElement(By.Name("checkNaoAlteraCurso[]")).Displayed)
                {
                    ClickButtonsByName( "checkNaoAlteraCurso[]");
                }

                //checa se existe e escreve a justificativa
                EscreverJustificativa(aluno.Justificativa);

                //Duração regular do curso MARCAR A CHECKBOX (SE APARECER)
                if (Driver.FindElement(By.Name("checkNaoAlteraCurso[]")).Displayed)
                {
                    if (!Driver.FindElement(By.Name("checkNaoAlteraCurso[]")).Selected)
                    {
                        ClickButtonsByName( "checkNaoAlteraCurso[]");
                    }
                }
            }
            else
            {
                Util.EditarConclusaoAluno(aluno, "Rejeitou excesso de reprovação");
                Driver.Url = "http://sisfies.mec.gov.br/cpsa/aditamento";
            }
        }
        private void LimparImagemCaptcha()
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
        private async void PreencherCaptcha()
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
                form.Add(new ByteArrayContent(imageData, 0, imageData.Length), "image", "img\\captchaLimpo.png");
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

                ScrollToElementByID( "captcha");
                ClickAndWriteById( "captcha", resultado);
                ClickButtonsById( "validar");
                ClickButtonsByXpath( "/html/body/div[8]/div[3]/div/button[2]/span");
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        private bool PossuiErros(string pageSource)
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
                }
                else
                {
                    string erro = VerificarMensagem();
                    Util.EditarConclusaoAluno(aluno, erro);
                }
            }
        }

        public void Executar(TOAluno aluno)
        {
            AditamentoFiesLegado(aluno);
        }

        public void SelecionarMenu()
        {
            ClickButtonsByCss( "div:nth-child(3) > ul > .menu-button:nth-child(2) > a");
        }

        public void SetWebDriver(IWebDriver Driver)
        {
            this.Driver = Driver;
        }
    }
}