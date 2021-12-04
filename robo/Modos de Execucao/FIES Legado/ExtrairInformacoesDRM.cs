using OpenQA.Selenium;
using robo.TO;
using robo.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using robo.Contratos;
using TikaOnDotNet.TextExtraction;

namespace robo.Modos_de_Execucao.FIES_Legado
{
    class ExtrairInformacoesDRM : UtilFiesLegado, IModosDeExecucao.IModoComAlunos
    {
        private string semestre;
        public ExtrairInformacoesDRM(string semestre)
        {
            this.semestre = semestre;
        }
        public void ExtrairDRM(TOAluno aluno)
        {
            ConsultarAluno(aluno, semestre);
            string situacaoAluno;
            if (Driver.PageSource.Contains("Lista de Aditamentos"))
            {
                situacaoAluno = Driver.FindElement(By.XPath("/html/body/div[3]/div[4]/div[2]/div[2]/div[4]/table/tbody/tr/td[6]")).Text;
                ClickButtonsByCss( "td > a > img");
                IWebElement botaoImprimir;
                botaoImprimir = VerificarElementoExiste( "ID", "imprimirDrm");
                if (botaoImprimir != null)
                {
                    EsperarLoading();

                    string erro = VerificarMensagem();
                    if (erro == string.Empty)
                    {
                        BaixarDRM(ref aluno);
                        Util.EditarConclusaoAluno(aluno, "DRM Baixado");
                    }
                    else
                    {
                        Util.EditarConclusaoAluno(aluno, erro);
                    }
                }
                else
                {
                    ScrollToElementByID( "voltar");
                    ClickButtonsById( "voltar");
                    Util.EditarConclusaoAluno(aluno, situacaoAluno);
                }
            }
        }

        private void ConsultarAluno(TOAluno aluno, string semestre)
        {
            ClickDropDown( "id", "co_finalidade_aditamento", "Aditamento de Renovação");
            EsperarLoading();
            ClickDropDown( "id", "coSemestreAditamento", semestre);
            ClickAndWriteById( "cpf", aluno.Cpf);
            ClickDropDown( "id", "coSemestreAditamento", semestre);
            ClickButtonsById( "consultar");
        }

        private void BaixarDRM(ref TOAluno aluno)
        {
            ClickButtonsById( "imprimirDrm");
            string downloadFolder = Directory.GetCurrentDirectory() + "\\DocumentosBaixados\\";
            DirectoryInfo directory = new DirectoryInfo(downloadFolder);
            string sourcecode = Driver.PageSource;
            FileInfo myFile = directory.GetFiles().OrderByDescending(f => f.LastWriteTime).First();
            bool downloading = true;
            while (myFile.Name.EndsWith(".zip") == false)
            {
                System.Threading.Thread.Sleep(1000);
                myFile = directory.GetFiles().OrderByDescending(f => f.LastWriteTime).First();
                downloading = myFile.Name.EndsWith(".crdownload");
            }
            string diretorioDRM = "DRM_Informações";
            Util.CriarDiretorioCasoNaoExista(diretorioDRM);
            Util.ApagaArquivos(diretorioDRM);


            File.Move(myFile.FullName, diretorioDRM + "\\DRM_ALUNO.zip");

            ClickButtonsById( "voltar");

            ZipFile.ExtractToDirectory(diretorioDRM + "\\DRM_ALUNO.zip", diretorioDRM);
            string[] arquivoZIP = Directory.GetFiles(diretorioDRM);
            string arquivoCPS = string.Empty;
            foreach (string item in arquivoZIP)
            {
                if (item.Contains("CPSA") == true)
                {
                    arquivoCPS = item;
                    break;
                }
            }
            string informacoes = new TextExtractor().Extract(arquivoCPS).Text;
            ProcessarInfsFiesVelho(informacoes, ref aluno, sourcecode);
            Util.ApagaArquivos(diretorioDRM);
        }
        private void ProcessarInfsFiesVelho(string inf, ref TOAluno aluno, string percentualFinanciamento)
        {
            try
            {
                string depoisTitulo;
                if (inf.Contains("TERMO ADITIVO AO CONTRATO") == true)
                {
                    return;
                }
                //Semestre a aditar
                depoisTitulo = inf.Split(new string[] { "Data do DRM:" }, StringSplitOptions.None)[1];
                string depoisNovaLinha = depoisTitulo.Split('\n')[1];
                aluno.SemestreAditar = depoisNovaLinha.Split(':')[1];
                //Curso
                depoisTitulo = inf.Split(new string[] { "Curso:" }, StringSplitOptions.None)[1];
                aluno.Curso = depoisTitulo.Split('\n')[0];
                //Duraçao Regular
                depoisTitulo = inf.Split(new string[] { "Duração regular:" }, StringSplitOptions.None)[1];
                aluno.DuracaoRegular = depoisTitulo.Split(' ')[1].Split(' ')[0];
                //Total de semestres suspensos
                depoisTitulo = inf.Split(new string[] { "Total de semestres suspensos:" }, StringSplitOptions.None)[1];
                aluno.TotalDeSemestresSuspensos = depoisTitulo.Split('\n')[0];
                //Total de semestres dilatados
                depoisTitulo = inf.Split(new string[] { "Total de semestres dilatados:" }, StringSplitOptions.None)[1];
                aluno.TotalDeSemestresDilatados = depoisTitulo.Split('\n')[0];
                //Total de semestres já concluídos e/ou aproveitados nesta IES/curso
                depoisTitulo = inf.Split(new string[] { "IES/curso:" }, StringSplitOptions.None)[1];
                aluno.TotalDeSemestresConcluidos = depoisTitulo.Split('\n')[0];
                //Semestre a ser cursado pelo estudante
                depoisTitulo = inf.Split(new string[] { "Semestre a ser cursado pelo estudante:" }, StringSplitOptions.None)[1];
                aluno.SemestreSerCursadoPeloEstudante = depoisTitulo.Split('\n')[0];
                //Total de semestre já financiados:
                depoisTitulo = inf.Split(new string[] { "Total de semestres já financiados:" }, StringSplitOptions.None)[1];
                aluno.TotalDeSemestresJaFinanciados = depoisTitulo.Split('\n')[0];
                //Percentual de financiamento solicitado:
                depoisTitulo = percentualFinanciamento.Split(new string[] { "Percentual de Financiamento solicitado:</strong>" }, StringSplitOptions.None)[1];
                aluno.PercentualDeFinanciamentoSolicitado = depoisTitulo.Split('\n')[1];
                //Valor da semestralidade e da mensalidade do curso - Grade Curricular Regular:
                depoisTitulo = inf.Split(new string[] { "Valor da semestralidade e da mensalidade atual - Grade Curricular a ser cursada no semestre" }, StringSplitOptions.None)[1];
                aluno.GradeAtualComDesconto = depoisTitulo.Split(' ')[8];
                aluno.GradeAtualFinanciadoFIES = depoisTitulo.Split(' ')[11];
                aluno.GradeAtualCoparticipacao = depoisTitulo.Split(' ')[14];
                //infs.Conclusao = "DRM Baixada";

                //Util.AcertaBarraR(aluno);
                aluno.CorrigirBarraR();

            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Erro não esperado encontrado.\n Contate os alunos brilhantes.\n\n{0}", e.Message));
            }
        }

        public void SetWebDriver(IWebDriver Driver)
        {
            this.Driver = Driver;
        }

        public void Executar(TOAluno aluno)
        {
            ExtrairDRM(aluno);
        }

        public void SelecionarMenu()
        {
            SelecionarMenuBaixarDocumentos();
        }

        public void SetDriver(IWebDriver Driver)
        {
            this.Driver = Driver;
        }
    }
}
