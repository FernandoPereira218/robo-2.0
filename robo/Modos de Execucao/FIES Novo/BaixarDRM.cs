using OpenQA.Selenium;
using robo.TO;
using robo.Utils;
using System;
using System.Diagnostics;
using System.IO;
using robo.Contratos;

namespace robo.Modos_de_Execucao.FIES_Novo
{
    public class BaixarDRM : UtilFiesNovo, IModosDeExecucao.IModoComAlunos
    {
        private string semestre;
        public BaixarDRM(string semestre)
        {
            this.semestre = semestre;
        }

        public void BaixarDRMFiesNovo(TOAluno aluno)
        {
            string janelaInicial = Driver.CurrentWindowHandle;
            BuscarEAbrirDRM( aluno, semestre);

            if (aluno.Conclusao != "Não Feito")
            {
                return;
            }
            string simplificado = BuscarDRMSimplificado();

            string fileName = aluno.Nome + "_" + aluno.Cpf;
            string nomePasta = "DRM FIES Novo";
            SalvarDRMComoPdf(fileName, janelaInicial, nomePasta);

            aluno.Extraido = "Sim";
            Util.EditarConclusaoAluno(aluno, "DRM Baixado - " + simplificado);
        }

        private string BuscarDRMSimplificado()
        {
            string informacao = Driver.PageSource;
            string simplificado = string.Empty;
            if (informacao.ToUpper().Contains("ADITAMENTO SIMPLIFICADO DE CONTRATO DE FINANCIAMENTO") == true)
            {
                simplificado = "Simplificado";
            }
            else
            {
                simplificado = "Não Simplificado";
            }

            return simplificado;
        }

        private void SalvarDRMComoPdf(string fileName, string windowOriginal, string nomePasta)
        {
            string informacao = Driver.PageSource;
            Driver.Close();
            Driver.SwitchTo().Window(windowOriginal);

            //Criação das pastas necessárias
            Util.CriarDiretorioCasoNaoExista("html");
            string downloadFolder = Util.GetDownloadsFolderPath();
            string diretorioDRM = downloadFolder + "\\" + nomePasta + "\\";
            Util.CriarDiretorioCasoNaoExista(diretorioDRM);

            //Buscar texto do HTML
            string htmlDirectory = "html\\contrato.html";
            string[] temp = new string[2];
            if (informacao.Contains("<head>") == true)
            {
                temp = informacao.Split(new string[] { "<head>" }, StringSplitOptions.None);
                temp[0] += "<meta charset = \"utf-8\">";
            }
            informacao = temp[0] + temp[1];
            File.WriteAllText(htmlDirectory, informacao);

            string DataDirectory = diretorioDRM;
            DataDirectory += fileName;
            DataDirectory += "_DRM.pdf";

            //Chamada do arquivo em Python para salvar a página como pdf
            SaveHtmlAsPdf(htmlDirectory, DataDirectory);
        }

        private static void SaveHtmlAsPdf(string htmlPath, string pdfFilePath)
        {
            string err = "";
            try
            {
                ProcessStartInfo processInfo = new ProcessStartInfo();
                processInfo.FileName = "HtmlToPdf.exe";
                processInfo.Arguments = $"\"{htmlPath}\" \"{pdfFilePath}\"";
                processInfo.UseShellExecute = false;
                processInfo.CreateNoWindow = true;
                processInfo.RedirectStandardOutput = true;
                processInfo.RedirectStandardError = true;
                string results = "";
                using (var process = Process.Start(processInfo))
                {
                    err = process.StandardError.ReadToEnd();
                    results = process.StandardOutput.ReadToEnd();
                }
            }
            catch
            {
                throw new Exception(err);
            }
        }

        public void SetWebDriver(IWebDriver driver)
        {
            Driver = driver;
        }

        public void Executar(TOAluno aluno)
        {
            BaixarDRMFiesNovo(aluno);
        }

        public void SelecionarMenu()
        {
            ClicarMenuAditamento();
        }

        public void SetDriver(IWebDriver Driver)
        {
            this.Driver = Driver;
        }
    }
}
