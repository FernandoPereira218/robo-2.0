using OpenQA.Selenium;
using Robo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robo.Control.Relatorios.FIES_Novo
{
    public class BaixarDRM : UtilFiesNovo
    {
        private IWebDriver Driver;

        public void BaixarDRMFiesNovo(TOAluno aluno, string semestre)
        {
            string janelaInicial = Driver.CurrentWindowHandle;
            BuscarEAbrirDRM(Driver, aluno, semestre);

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
            Util.CreateDirectoryIfNotExists("html");
            string downloadFolder = Util.GetDownloadsFolderPath();
            string diretorioDRM = downloadFolder + "\\" + nomePasta + "\\";
            Util.CreateDirectoryIfNotExists(diretorioDRM);

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

            //Caso seja da versão CAE, copia para uma pasta local e abre automaticamente o pdf
            //if (emLote == false)
            //{
            //    CreateDirectory("Temp");
            //    DirectoryInfo di = new DirectoryInfo("Temp");
            //    foreach (var item in di.GetFiles())
            //    {
            //        item.Delete();
            //    }
            //    File.Copy(DataDirectory, "Temp\\" + fileName + ".pdf");
            //    Process.Start("Temp\\" + fileName + ".pdf");
            //}
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

        public void SetDriver(IWebDriver driver)
        {
            Driver = driver;
        }

    }
}
