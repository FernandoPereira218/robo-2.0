using OpenQA.Selenium;
using robo.Control.Legado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Robo;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;

namespace robo.Control.Relatorios.FIES_Legado
{
    public class ExtrairInformacoesDRI
    {
        private IWebDriver Driver;
        private UtilFiesLegado utilFiesLegado = new UtilFiesLegado();

        public void ExecutarExtrairInformacoesDRI(IWebDriver driver, TOAluno aluno, string situacao)
        {
            Driver = driver;

            Util.ClickAndWriteById(Driver, "nu_cpf", aluno.Cpf);
            Util.ClickDropDown(Driver, "id", "co_situacao_inscricao", situacao);
            Util.ClickButtonsById(Driver, "consulta");
            utilFiesLegado.WaitinLoading(Driver);

            if (Driver.PageSource.Contains("sorterdocuments"))
            {
                Util.ClickButtonsByCss(Driver, ".even:nth-child(1) img");

                if (!Driver.PageSource.Contains("Voltar para a página principal"))
                {
                    if (!Driver.PageSource.Contains("Inscrição incompleta."))
                    {
                        string CodigoFonte = Driver.FindElement(By.TagName("body")).Text;

                        // Começa 
                        string semestraAditar = CodigoFonte.Split(new string[] { "Semestre a que se refere esta inscrição:" }, StringSplitOptions.None)[1];
                        string curso = CodigoFonte.Split(new string[] { "Curso:" }, StringSplitOptions.None)[1];
                        string duracao = CodigoFonte.Split(new string[] { "Duração Regular do Curso:" }, StringSplitOptions.None)[1];

                        // A Atribuir
                        string select;
                        string selectFinanciadoSemestre;


                        if (Util.VerificarElementoExiste(Driver, "id", "qt_semestre_concluido") == null)
                        {
                            select = CodigoFonte.Split(new string[] { "Total de semestres já concluídos:*" }, StringSplitOptions.None)[1];
                            select = select.Split(new string[] { "\r\n" }, StringSplitOptions.None)[0];
                        }
                        else
                        {
                            var Concluidos = Driver.FindElement(By.Id("qt_semestre_concluido"));
                            select = new SelectElement(Concluidos).SelectedOption.Text;
                        }

                        // Texto padrao 
                        string aSerCursado = Driver.FindElement(By.Id("nu_semestre_a_cursar")).Text;
                        string jaFianciados = Driver.FindElement(By.Id("qt_semestre_financiamento")).Text;
                        string percentual = Driver.FindElement(By.Id("nuPercentualFinanciamento")).Text;
                        string gradeAtualComDesconto;

                        if (Util.VerificarElementoExiste(Driver, "id", "vl_semestre_atual") == null)
                        {
                            gradeAtualComDesconto = CodigoFonte.Split(new string[] { "Valor da semestralidade a ser cursado com desconto - Grade Curricular a ser Cursada:*" }, StringSplitOptions.None)[1];
                            gradeAtualComDesconto = gradeAtualComDesconto.Split(new string[] { "\r\n" }, StringSplitOptions.None)[0];
                        }
                        else
                        {
                            var InputSemestreAtual = Driver.FindElement(By.Id("vl_semestre_atual"));
                            gradeAtualComDesconto = InputSemestreAtual.GetAttribute("value");
                        }

                        if (Util.VerificarElementoExiste(Driver, "id", "vl_financiado_semestre") == null)
                        {
                            selectFinanciadoSemestre = CodigoFonte.Split(new string[] { "Valor a ser financiado no semestre a ser cursado com recursos do FIES:*" }, StringSplitOptions.None)[1];
                            selectFinanciadoSemestre = selectFinanciadoSemestre.Split(new string[] { "\r\n" }, StringSplitOptions.None)[0];
                        }
                        else
                        {
                            var Concluidos = Driver.FindElement(By.Id("vl_financiado_semestre"));
                            selectFinanciadoSemestre = new SelectElement(Concluidos).SelectedOption.Text;
                        }
                        string Coparticipacao = Driver.FindElement(By.Id("vlMesSemestreEstudante")).Text;

                        aluno.SemestreAditar = semestraAditar.Split(new string[] { "\r\n" }, StringSplitOptions.None)[0];
                        aluno.Curso = curso.Split(new string[] { "Turno:" }, StringSplitOptions.None)[0];
                        aluno.DuracaoRegular = duracao.Split(new string[] { "\r\n" }, StringSplitOptions.None)[0];
                        aluno.TotalDeSemestresConcluidos = select;
                        aluno.TotalDeSemestresJaFinanciados = jaFianciados;
                        aluno.PercentualDeFinanciamentoSolicitado = percentual;
                        aluno.GradeAtualComDesconto = gradeAtualComDesconto;
                        aluno.GradeAtualFinanciadoFIES = selectFinanciadoSemestre;
                        aluno.GradeAtualCoparticipacao = Coparticipacao;

                        Util.EditarConclusaoAluno(aluno, "DRI Baixado", "ALUNOINF");

                        Util.ScrollToElementByID(Driver, "voltar");
                        Util.ClickButtonsById(Driver, "voltar");

                    }
                }
            }
        }
    }
}
