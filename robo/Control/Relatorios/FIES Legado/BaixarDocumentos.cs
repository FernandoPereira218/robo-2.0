using OpenQA.Selenium;
using robo.Control.Legado;
using Robo;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robo.Control.Relatorios
{
    public class BaixarDocumentos: UtilFiesLegado
    {
        private IWebDriver Driver;

        public void BaixarDocumentoFiesLegado(IWebDriver driver, TOAluno aluno, string semestre, string tipoRelatorio)
        {
            Driver = driver;
            string selRelatorio = SelecionarTipoRelatorio(Driver, tipoRelatorio);
            WaitinLoading(Driver);
            ClickAndWriteById(Driver, "cpf", aluno.Cpf);
            ClickDropDown(Driver, "id", "coSemestreAditamento", semestre);
            ClickDropDown(Driver, "id", "co_finalidade_aditamento", selRelatorio);
            WaitinLoading(Driver);
            ClickButtonsById(Driver, "consultar");
            string situacaoAluno;
            if (Driver.PageSource.Contains("Lista de Aditamentos"))
            {
                situacaoAluno = Driver.FindElement(By.XPath("/html/body/div[3]/div[4]/div[2]/div[2]/div[4]/table/tbody/tr/td[6]")).Text;
                ClickButtonsByCss(Driver, "td > a > img");
                IWebElement botaoImprimir;
                if (tipoRelatorio == "DRM")
                {
                    botaoImprimir = VerificarElementoExiste(Driver, "ID", "imprimirDrm");
                }
                else
                {
                    botaoImprimir = VerificarElementoExiste(Driver, "ID", "imprimir");
                }
                if (botaoImprimir != null)
                {
                    WaitinLoading(Driver);

                    if (VerificaErro(Driver, aluno) == false)
                    {
                        BaixarDocumento(aluno, semestre, tipoRelatorio);
                    }
                }
                else
                {
                    ScrollToElementByID(Driver, "voltar");
                    ClickButtonsById(Driver, "voltar");
                    Util.EditarConclusaoAluno(aluno, situacaoAluno);
                }
            }
            else
            {
                Util.EditarConclusaoAluno(aluno, "Nenhum registro encontrado.");
            }
        }
        private void BaixarDocumento(TOAluno aluno, string semestre, string tipoRelatorio)
        {
            string nome = Driver.PageSource;
            if (Driver.PageSource.Contains("Nome completo:</strong>") == true)
            {
                nome = nome.Split(new string[] { "Nome completo:</strong>" }, StringSplitOptions.None)[1];
            }
            else
            {
                nome = nome.Split(new string[] { "Nome Completo:</strong>" }, StringSplitOptions.None)[1];
            }
            nome = nome.Split(new string[] { "</span>" }, StringSplitOptions.None)[0];
            aluno.Nome = nome;
            if (tipoRelatorio == "DRM")
            {
                ClickButtonsById(Driver, "imprimirDrm");
            }
            else
            {
                ClickButtonsById(Driver, "imprimir");
            }
            string downloadFolder = Util.GetDownloadsFolderPath();
            DirectoryInfo directory = new DirectoryInfo(downloadFolder);

            FileInfo myFile = directory.GetFiles().OrderByDescending(f => f.LastWriteTime).First();

            bool downloading = true;
            while (myFile.Name.EndsWith(".zip") == false)
            {
                System.Threading.Thread.Sleep(1000);
                myFile = directory.GetFiles().OrderByDescending(f => f.LastWriteTime).First();
                downloading = myFile.Name.EndsWith(".crdownload");
            }
            string diretorioDestino;
            string complemento = string.Empty;
            Util.CreateDirectory("Temp");
            System.IO.DirectoryInfo di = new DirectoryInfo("Temp");
            foreach (var item in di.GetFiles())
            {
                item.Delete();
            }
            if (tipoRelatorio == "DRM")
            {
                string diretorioDRM = downloadFolder + "\\DRM FIES Legado";
                string diretorioSimplificado = diretorioDRM + "\\Simplificados";
                string diretorioNaoSimplificado = diretorioDRM + "\\Nao-Simplificados";

                Util.CreateDirectory(diretorioDRM, diretorioSimplificado, diretorioNaoSimplificado);
                using (ZipArchive archive = new ZipArchive(File.OpenRead(myFile.FullName), ZipArchiveMode.Read))
                {
                    archive.ExtractToDirectory("Temp");
                    ZipArchiveEntry arquivozipado = archive.Entries[0];

                    if (arquivozipado.Name.Contains("nao"))
                    {
                        diretorioDestino = diretorioNaoSimplificado;
                        complemento = "Não Simplificado";
                    }
                    else
                    {
                        diretorioDestino = diretorioSimplificado;
                        complemento = "Simplificado";
                    }
                }
            }
            else
            {
                using (ZipArchive archive = new ZipArchive(File.OpenRead(myFile.FullName), ZipArchiveMode.Read))
                {
                    archive.ExtractToDirectory("Temp");
                }
                string diretorio = downloadFolder + "\\" + tipoRelatorio + " FIES Legado";
                Util.CreateDirectory(diretorio);
                diretorioDestino = diretorio;
            }

            string tempSemestre = semestre.Replace("/", "-");
            File.Copy(myFile.FullName, diretorioDestino + "\\" + aluno.Nome + "_" + aluno.Cpf + "_" + tempSemestre + "_" + tipoRelatorio + ".zip", true);
            File.Delete(myFile.FullName);

            ClickButtonsById(Driver, "voltar");

            string conclusao = string.Format("{0} - {1}", tipoRelatorio + " Baixado", complemento);
            aluno.Extraido = complemento;
            Util.EditarConclusaoAluno(aluno, conclusao);
        }
        
    }
}