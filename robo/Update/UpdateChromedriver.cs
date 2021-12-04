using OpenQA.Selenium;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using robo.Utils;

namespace robo.Update
{
    /// <summary>
    /// Métodos utilizados para buscar a versão atual do chromedriver
    /// </summary>
    public static class UpdateChromedriver
    {
        private static IWebDriver Driver;

        /// <summary>
        /// Busca a versão mais recente do ChromeDriver e atualiza o arquivo na pasta Driver
        /// </summary>
        public static void DownloadChromedriver()
        {
            Driver = Util.StartBrowser("https://chromedriver.chromium.org/downloads", downloadFldr:true, headless:true);
            var linksDownload = Driver.FindElements(By.ClassName("XqQF9c"));
            IWebElement elemento = linksDownload[1];
            string versao = elemento.Text;
            versao = versao.Split(' ')[1];
            try
            {
                Driver.Url = "https://chromedriver.storage.googleapis.com/" + versao + "/chromedriver_win32.zip";
            }
            catch (Exception e)
            {
                DirectoryInfo directory = new DirectoryInfo("RelatorioExportacao");
                Util.EsperarDownload(directory);
                ExcluirVersaoAnterior();
                CopiarParaPastaDriver(directory);
                DeletarDownload(directory);

                Driver.Close();
                Driver.Dispose();
            }
        }

        private static void DeletarDownload(DirectoryInfo diretorio)
        {
            foreach (var item in diretorio.GetFiles())
            {
                File.Delete(item.FullName);
            }
        }

        private static void CopiarParaPastaDriver(DirectoryInfo diretorio)
        {
            FileInfo aquivoDriver = diretorio.GetFiles().OrderByDescending(f => f.LastWriteTime).First();
            using (ZipArchive za = new ZipArchive(File.OpenRead(aquivoDriver.FullName), ZipArchiveMode.Read))
            {
                DirectoryInfo pastaDriver = new DirectoryInfo("driver");
                za.ExtractToDirectory("driver");
            }
        }

        private static void ExcluirVersaoAnterior()
        {
            DirectoryInfo diretorioDriver = new DirectoryInfo("driver");
            foreach (var item in diretorioDriver.GetFiles())
            {
                if (item.Name.Contains("chromedriver") == true)
                {
                    File.Delete(item.FullName);
                }
            }
        }
    }
}
