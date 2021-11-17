using OpenQA.Selenium;
using robo.Control.Legado;
using Robo;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace robo.Control.Relatorios
{
    public class BaixarDocumentos: UtilFiesLegado
    {
        private IWebDriver Driver;

        public void BaixarDocumentoFiesLegado(IWebDriver driver, TOAluno aluno, string semestre, string tipoRelatorio)
        {
            Driver = driver;
            ConsultarDocumentoAluno(aluno, semestre, tipoRelatorio);
            string situacaoAluno;
            if (Driver.PageSource.Contains("Lista de Aditamentos"))
            {
                situacaoAluno = Driver.FindElement(By.XPath("/html/body/div[3]/div[4]/div[2]/div[2]/div[4]/table/tbody/tr/td[6]")).Text;
                ClickButtonsByCss(Driver, "td > a > img");
                IWebElement botaoImprimir = BuscarBotaoImprimir(tipoRelatorio);
                BaixarDocumentoAluno(aluno, semestre, tipoRelatorio, situacaoAluno, botaoImprimir);
            }
        }
        private void BaixarDocumentoAluno(TOAluno aluno, string semestre, string tipoRelatorio, string situacaoAluno, IWebElement botaoImprimir)
        {
            if (botaoImprimir != null)
            {
                WaitinLoading(Driver);
                string msgErro = VerificaMensagem(Driver);
                if (msgErro == string.Empty)
                {
                    BaixarDocumento(aluno, semestre, tipoRelatorio);
                    ClickButtonsById(Driver, "voltar");
                }
            }
            else
            {
                ScrollToElementByID(Driver, "voltar");
                ClickButtonsById(Driver, "voltar");
                Util.EditarConclusaoAluno(aluno, situacaoAluno);
            }
        }
        private void BaixarDocumento(TOAluno aluno, string semestre, string tipoRelatorio)
        {
            aluno.Nome = BuscarNomeAluno();
            string simplificado = string.Empty;
            if (tipoRelatorio == "DRM")
            {
                ClickButtonsById(Driver, "imprimirDrm");
                simplificado = FindElementByXpathText(Driver, "span", "Simplificado").Text;
                simplificado = simplificado.Replace("Tipo de Aditamento: ", "");
            }
            else
            {
                ClickButtonsById(Driver, "imprimir");
            }
            
            Util.BaixarDocumento(aluno.Nome + "_" + aluno.Cpf + "_" + semestre.Replace("/", "-") + "_" + tipoRelatorio, tipoRelatorio, simplificado);
            
            Util.EditarConclusaoAluno(aluno, string.Format("{0} - {1}", tipoRelatorio + " Baixado", simplificado.Trim()));

            
        }
        private IWebElement BuscarBotaoImprimir(string tipoRelatorio)
        {
            IWebElement botaoImprimir;
            if (tipoRelatorio == "DRM")
            {
                botaoImprimir = VerificarElementoExiste(Driver, "ID", "imprimirDrm");
            }
            else
            {
                botaoImprimir = VerificarElementoExiste(Driver, "ID", "imprimir");
            }

            return botaoImprimir;
        }
        private void ConsultarDocumentoAluno(TOAluno aluno, string semestre, string tipoRelatorio)
        {
            SelecionarTipoRelatorio(Driver, tipoRelatorio);
            WaitinLoading(Driver);
            ClickAndWriteById(Driver, "cpf", aluno.Cpf);
            ClickDropDown(Driver, "id", "coSemestreAditamento", semestre);
            WaitinLoading(Driver);
            ClickButtonsById(Driver, "consultar");
        }
        private static void MoverArquivoParaDownloads(TOAluno aluno, string semestre, string tipoRelatorio, FileInfo ultimoArquivo, string diretorioDestino)
        {
            string semestreFormatado = semestre.Replace("/", "-");
            File.Copy(ultimoArquivo.FullName, diretorioDestino + "\\" + aluno.Nome + "_" + aluno.Cpf + "_" + semestreFormatado + "_" + tipoRelatorio + ".zip", true);
            File.Delete(ultimoArquivo.FullName);
        }
        private static void ExcluirArquivosTemporarios()
        {
            Util.CriarDiretorioCasoNaoExista("Temp");
            DirectoryInfo di = new DirectoryInfo("Temp");
            foreach (var item in di.GetFiles())
            {
                item.Delete();
            }
        }
        private static void EsperarDownloadArquivo(out FileInfo ultimoArquivo)
        {
            DirectoryInfo pastaDownloads = new DirectoryInfo(Util.GetDownloadsFolderPath());
            ultimoArquivo = pastaDownloads.GetFiles().OrderByDescending(f => f.LastWriteTime).First();
            bool baixando = true;
            while (ultimoArquivo.Name.EndsWith(".zip") == false)
            {
                System.Threading.Thread.Sleep(1000);
                ultimoArquivo = pastaDownloads.GetFiles().OrderByDescending(f => f.LastWriteTime).First();
                baixando = ultimoArquivo.Name.EndsWith(".crdownload");
            }
        }
        private static string ExtrairArquivoDocumento(string tipoRelatorio, string downloadFolder, FileInfo ultimoArquivo)
        {
            string diretorioDestino;
            using (ZipArchive archive = new ZipArchive(File.OpenRead(ultimoArquivo.FullName), ZipArchiveMode.Read))
            {
                archive.ExtractToDirectory("Temp");
            }
            string diretorio = downloadFolder + "\\" + tipoRelatorio + " FIES Legado";
            Util.CriarDiretorioCasoNaoExista(diretorio);
            diretorioDestino = diretorio;
            return diretorioDestino;
        }
        private static void ExtrairArquivoDRM(FileInfo ultimoArquivo, out string diretorioDestino, out string complemento)
        {
            string diretorioDRM = Util.GetDownloadsFolderPath() + "\\DRM FIES Legado";
            string diretorioSimplificado = diretorioDRM + "\\Simplificados";
            string diretorioNaoSimplificado = diretorioDRM + "\\Nao-Simplificados";

            Util.CriarDiretorioCasoNaoExista(diretorioDRM, diretorioSimplificado, diretorioNaoSimplificado);
            using (ZipArchive zip = new ZipArchive(File.OpenRead(ultimoArquivo.FullName), ZipArchiveMode.Read))
            {
                zip.ExtractToDirectory("Temp");
                ZipArchiveEntry arquivoCompactado = zip.Entries[0];

                if (arquivoCompactado.Name.Contains("nao"))
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
        private string BuscarNomeAluno()
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
            return nome;
        }
    }
}