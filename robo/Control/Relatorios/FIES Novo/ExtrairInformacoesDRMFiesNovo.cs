using OpenQA.Selenium;
using Robo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robo.Control.Relatorios.FIES_Novo
{
    public class ExtrairInformacoesDRMFiesNovo : UtilFiesNovo
    {
        private IWebDriver Driver;

        public void ExtrairInformacoesDRM(TOAluno aluno, string semestre)
        {
            string janelaInicial = Driver.CurrentWindowHandle;
            BuscarEAbrirDRM(Driver, aluno, semestre);
            if (aluno.Conclusao != "Não Feito")
            {
                Util.EditarConclusaoAluno(aluno, aluno.Conclusao);
                return;
            }

            string informacao = SalvarTextoPagina();
            Driver.Close();
            Driver.SwitchTo().Window(janelaInicial);

            ProcessarInfsFiesNovo(informacao, aluno);

            Util.EditarConclusaoAluno(aluno, "DRM Baixado", "ALUNOINF");
        }

        public void SetDriver(IWebDriver driver)
        {
            Driver = driver;
        }

        private string SalvarTextoPagina()
        {
            string alltext = Driver.FindElement(By.TagName("body")).Text;

            return alltext;
        }

        private void ProcessarInfsFiesNovo(string inf, TOAluno aluno)
        {
            try
            {
                string depoisTitulo;
                List<string> infs = new List<string>();
                infs.Add(aluno.Cpf);
                if (inf.Contains("TERMO ADITIVO AO CONTRATO") == true)
                {
                    return;
                }
                try
                {
                    depoisTitulo = inf.Split(new string[] { "Data da DRM:" }, StringSplitOptions.None)[1];
                }
                catch (IndexOutOfRangeException ex)
                {
                    if (inf.Contains("Data de validação do aditamento:"))
                    {
                        depoisTitulo = inf.Split(new string[] { "Data de validação do aditamento:" }, StringSplitOptions.None)[1];
                    }
                    else
                    {
                        depoisTitulo = inf.Split(new string[] { "Data da validação do aditamento:" }, StringSplitOptions.None)[1];
                    }
                }

                string depoisNovaLinha = depoisTitulo.Split('\n')[1];
                infs.Add(depoisNovaLinha.Split(':')[1]);
                aluno.SemestreAditar = infs[infs.Count() - 1];

                depoisTitulo = inf.Split(new string[] { "\nCurso:" }, StringSplitOptions.None)[1];
                infs.Add(depoisTitulo.Split('\n')[0]);
                aluno.Curso = infs[infs.Count() - 1];

                depoisTitulo = inf.Split(new string[] { "Duração regular:" }, StringSplitOptions.None)[1];

                infs.Add(depoisTitulo.Split(new string[] { "Total de semestre(s) do financiamento:" }, StringSplitOptions.None)[0]);
                aluno.DuracaoRegular = infs[infs.Count() - 1];

                depoisTitulo = inf.Split(new string[] { "Total de semestres suspensos:" }, StringSplitOptions.None)[1];
                infs.Add(depoisTitulo.Split('\n')[0]);
                aluno.TotalDeSemestresSuspensos = infs[infs.Count() - 1];

                depoisTitulo = inf.Split(new string[] { "Total de semestres dilatados:" }, StringSplitOptions.None)[1];
                infs.Add(depoisTitulo.Split('\n')[0]);
                aluno.TotalDeSemestresDilatados = infs[infs.Count() - 1];

                depoisTitulo = inf.Split(new string[] { "Total de semestres já concluídos e/ou aproveitados nesta IES/curso:" }, StringSplitOptions.None)[1];
                infs.Add(depoisTitulo.Split('\n')[0]);
                aluno.TotalDeSemestresConcluidos = infs[infs.Count() - 1];

                depoisTitulo = inf.Split(new string[] { "Semestre a ser cursado pelo estudante:" }, StringSplitOptions.None)[1];
                infs.Add(depoisTitulo.Split('\n')[0]);
                aluno.SemestreSerCursadoPeloEstudante = infs[infs.Count() - 1];

                depoisTitulo = inf.Split(new string[] { "Total de semestre já financiados:" }, StringSplitOptions.None)[1];
                infs.Add(depoisTitulo.Split('\n')[0]);
                aluno.TotalDeSemestresJaFinanciados = infs[infs.Count() - 1];

                depoisTitulo = inf.Split(new string[] { "Percentual de financiamento solicitado:" }, StringSplitOptions.None)[1];
                infs.Add(depoisTitulo.Split('\n')[0]);
                aluno.PercentualDeFinanciamentoSolicitado = infs[infs.Count() - 1];

                if (inf.Contains("Grade Atual") == true)
                {
                    depoisTitulo = inf.Split(new string[] { "Grade Atual" }, StringSplitOptions.None)[1];
                }
                else
                {
                    depoisTitulo = inf.Split(new string[] { "Grade atual" }, StringSplitOptions.None)[1];
                }
                infs.Add(depoisTitulo.Split('\n')[3]);
                aluno.GradeAtualComDesconto = infs[infs.Count() - 1];
                infs.Add(depoisTitulo.Split('\n')[5]);
                aluno.GradeAtualFinanciadoFIES = infs[infs.Count() - 1];
                infs.Add(depoisTitulo.Split('\n')[7]);
                aluno.GradeAtualCoparticipacao = infs[infs.Count() - 1];

                for (int i = 0; i < infs.Count; i++)
                {
                    infs[i] = infs[i].Replace("\r", string.Empty);
                }
                Util.AcertaBarraR(aluno);
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Erro não esperado encontrado.\n Contate os alunos brilhantes.\n\n{0}", e.Message));
            }
        }

    }
}
