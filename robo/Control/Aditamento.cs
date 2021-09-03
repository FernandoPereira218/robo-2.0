using OpenQA.Selenium;
using robo.Control.Legado;
using Robo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace robo.Control
{
    public class Aditamento
    {
        static IWebDriver Driver;
        private UtilFiesLegado fiesLegadoutil = new UtilFiesLegado();
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

                    fiesLegadoutil.WaitinLoading(Driver);

                    if (VerificaErro(aluno) == false)
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
        private bool VerificaErro(TOAluno aluno)
        {
            IWebElement listaME = Driver.FindElement(By.Id("lista-mensageiro-erros"));
            if (listaME.Displayed)
            {
                IWebElement listaF = listaME.FindElement(By.XPath(".//li"));
                Util.EditarConclusaoAluno(aluno, listaF.Text);
                return true;
            }
            return false;
        }
        private void PreencherFormulario(TOAluno aluno)
        {
            /*Clica no Turno, casos seja um dropdown
            if (Driver.FindElement(By.Name("co_turno")).Displayed)
            {
                Driver.FindElement(By.Name("co_turno")).Click();
                IWebElement dd = Driver.FindElement(By.Name("co_turno"));
                dd.SendKeys(Keys.Down);
                dd.SendKeys(Keys.Enter);
            }*/

            fiesLegadoutil.WaitinLoading(Driver);

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

            while (Driver.Url.StartsWith("http://sisfies.mec.gov.br/cpsa/aditamento/formulario/") == true)
            {
                System.Threading.Thread.Sleep(100);
            }

            //Marca resultado aditamento
            VerificaErro(aluno);
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
            Util.EditarConclusaoAluno(aluno, "Rejeitou excesso de reprovação");
            Driver.Url = "http://sisfies.mec.gov.br/cpsa/aditamento";
        }
    }
}