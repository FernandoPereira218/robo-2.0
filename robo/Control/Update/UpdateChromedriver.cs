using OpenQA.Selenium;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace robo.Control.Update
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
            Driver = Robo.Util.StartBrowser("https://chromedriver.chromium.org/downloads", downloadFldr:true, headless:true);
            var downloadLinks = Driver.FindElements(By.ClassName("XqQF9c"));
            IWebElement element = downloadLinks[1];
            string versao = element.Text;
            versao = versao.Split(' ')[1];
            try
            {
                Driver.Url = "https://chromedriver.storage.googleapis.com/" + versao + "/chromedriver_win32.zip";
            }
            catch (Exception e)
            {
                DirectoryInfo directory = new DirectoryInfo("RelatorioExportacao");
                FileInfo myFile = directory.GetFiles().OrderByDescending(f => f.LastWriteTime).First();

                bool downloading = true;
                while (myFile.Name.EndsWith(".zip") == false)
                {
                    System.Threading.Thread.Sleep(1000);
                    myFile = directory.GetFiles().OrderByDescending(f => f.LastWriteTime).First();
                    downloading = myFile.Name.EndsWith(".crdownload");
                }
                DirectoryInfo driverDir = new DirectoryInfo("driver");
                foreach (var item in driverDir.GetFiles())
                {
                    if (item.Name.Contains("chromedriver") == true)
                    {
                        File.Delete(item.FullName);
                    }
                }
                using (ZipArchive archive = new ZipArchive(File.OpenRead(myFile.FullName), ZipArchiveMode.Read))
                {
                    DirectoryInfo driverFile = new DirectoryInfo("driver");
                    archive.ExtractToDirectory("driver");
                }
                foreach (var item in directory.GetFiles())
                {
                    File.Delete(item.FullName);
                }

                Driver.Close();
                Driver.Dispose();
            }
        }
    }
}
