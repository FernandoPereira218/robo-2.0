using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Security.Cryptography;
using robo.Control.Update;
using CsvHelper;
using System.Globalization;

namespace Robo
{
    class Util
    {
        /// <summary>
        /// Inicia nova sessão do browser.
        /// </summary>
        public static IWebDriver StartBrowser(string url, bool downloadFldr = false, bool firefox = true, bool headless = false)
        {
            IWebDriver driver;

            string downloadFolder = "";
            if (downloadFldr == true)
            {
                CreateDirectoryIfNotExists("RelatorioExportacao\\");
                downloadFolder = Directory.GetCurrentDirectory() + "\\RelatorioExportacao\\";
            }
            else
            {
                downloadFolder = GetDownloadsFolderPath();
            }

            if (firefox == true)
            {

                //Firefox
                var firefoxDriverService = FirefoxDriverService.CreateDefaultService(Environment.CurrentDirectory + @"\driver");
                firefoxDriverService.HideCommandPromptWindow = true;

                FirefoxOptions firefoxOptions = new FirefoxOptions();
                firefoxOptions.AcceptInsecureCertificates = true;
                if (RoboForm.versaoRobo != "operacoesFinanceiras" || headless == true)
                {
                    firefoxOptions.AddArgument("--headless");
                }
                var firefoxProfile = new FirefoxProfile();

                //profile.SetPreference("browser.download.downloadDir", downloadFolder);
                //profile.SetPreference("browser.download.defaultFolder", downloadFolder);
                //profile.SetPreference("browser.helperApps.neverAsk.openFile", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                //profile.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/pdf;text/plain;application/text;text/xml;application/xml");
                firefoxProfile.SetPreference("browser.helperApps.neverAsk.saveToDisk", "text/plain,application/octet-stream,application/pdf,application/x-pdf,application/vnd.pdf,application/zip,text/csv,application/csv,application/vnd.ms-excel,text/comma-separat‌​ed-values,application/excel,text/x-server-parsed-html,application/vnd.ms-excel,application/msexcel");
                firefoxProfile.SetPreference("browser.download.folderList", 2);
                firefoxProfile.SetPreference("browser.download.dir", downloadFolder);
                firefoxProfile.SetPreference("browser.helperApps.alwaysAsk.force", false);
                firefoxProfile.SetPreference("browser.download.manager.useWindow", false);
                firefoxProfile.SetPreference("browser.download.manager.focusWhenStarting", false);
                firefoxProfile.SetPreference("browser.download.manager.showAlertOnComplete", false);
                firefoxProfile.SetPreference("browser.download.manager.closeWhenDone", true);
                firefoxProfile.SetPreference("security.tls.version.min", 1);
                firefoxProfile.SetPreference("security.tls.version.max", 4);
                firefoxProfile.SetPreference("dom.enable_window_print", false);
                //firefoxProfile.SetPreference("print.tab_modal.enabled", true);
                firefoxOptions.Profile = firefoxProfile;
                driver = new FirefoxDriver(firefoxDriverService, firefoxOptions);
            }
            else
            {
                //Chrome
                var chromeDriverService = ChromeDriverService.CreateDefaultService(Environment.CurrentDirectory + @"\driver");
                chromeDriverService.HideCommandPromptWindow = true;
                ChromeOptions chromeOptions = new ChromeOptions();
                if (RoboForm.versaoRobo != "operacoesFinanceiras")
                {
                    chromeOptions.AddArgument("--headless");
                }
                chromeOptions.AddUserProfilePreference("download.default_directory", downloadFolder);
                try
                {

                    driver = new ChromeDriver(chromeDriverService, chromeOptions);
                }
                catch
                {
                    MessageBox.Show("Versão do Google Chrome desatualizada. Clique em 'Ok' para atualizar e continuar o processo.");
                    chromeDriverService.Dispose();

                    UpdateChromedriver.DownloadChromedriver();
                    chromeDriverService = ChromeDriverService.CreateDefaultService(Environment.CurrentDirectory + @"\driver");
                    chromeDriverService.HideCommandPromptWindow = true;
                    driver = new ChromeDriver(chromeDriverService, chromeOptions);
                    //throw new Exception("Versão do chromedriver foi atualizada, favor tentar novamente!");
                }
            }
            driver.Manage().Window.Maximize();
            driver.Url = url;
            //driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 3, 0);
            if (headless == true)
            {
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
            }
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);

            return driver;
        }

        public static void AcertaBarraR(TOAluno infs)
        {
            foreach (var propriedade in infs.GetType().GetProperties())
            {
                if (propriedade.Name != "Id")
                {
                    infs.GetType().GetProperty(propriedade.Name).SetValue(infs, infs.GetType().GetProperty(propriedade.Name).GetValue(infs).ToString().Replace("\r", ""));
                    infs.GetType().GetProperty(propriedade.Name).SetValue(infs, infs.GetType().GetProperty(propriedade.Name).GetValue(infs).ToString().Replace("\n", ""));
                }
            }
        }

        /// <summary>
        /// Cria um diretorio
        /// </summary>
        /// <param name="driver">webdriver</param>
        /// <param name="metodo">qual metodo a ser usado, id, name, css, ou xpath</param>
        /// <param name="valorMetodo">qual o id, name, css ou xpath a procurar</param>
        /// <param name="valorEscolha">qual a opção a ser escolhida</param>
        public static void CreateDirectoryIfNotExists(params string[] directories)
        {
            foreach (String directory in directories)
            {
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
            }
        }

        public static string GetDownloadsFolderPath()
        {
            string userRoot = Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadFolder = Path.Combine(userRoot, "Downloads");
            return downloadFolder;
        }

        public static string VerificaSemestreAtual()
        {
            DateTime aDate = DateTime.Now;
            string sem = string.Empty;
            if (aDate.Month > 6)
            {
                sem += "2";
            }
            else
            {
                sem += "1";
            }
            sem += "/" + aDate.Year.ToString();

            return sem;
        }

        public static int BuscarMesAtual()
        {
            DateTime aDate = DateTime.Now;
            return aDate.Month;
        }

        public static string GetMD5(string password)
        {
            MD5 md5password = MD5.Create();
            //Conversão a String para array de bytes, que é como a biblioteca trabalha.
            byte[] data = md5password.ComputeHash(Encoding.UTF8.GetBytes(password));

            //Criação de um stringBuilder para concatenar os dados
            StringBuilder sBuilder = new StringBuilder();

            //Loop para formatar cada byte como uma String em hexadecimal
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        public static void ExportarCSV(int countDataGrid, string tipo)
        {
            if (countDataGrid > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "CSV (*.csv)|*.csv";
                sfd.FileName = "Exportado_Robo.csv";
                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("Arquivo já existe, e está aberto em outro aplicativo" + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            ExportarAlunosParaCSV(sfd.FileName, tipo);
                            MessageBox.Show("Dados Exportados com Sucesso!!!", "Info");
                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show("Error :" + ex.Message);
                        }


                    }
                }
            }
            else
            {
                MessageBox.Show("Sem Registro para Exportar!!!", "Info");
            }

        }

        public static void ExportarAlunosParaCSV(string fileName, string tipo)
        {
            if (fileName.Contains(".csv") == false)
            {
                fileName += ".csv";
            }
            List<TOAluno> alunoParaExportar = Dados.SelectAll<TOAluno>();
            using (StreamWriter exportaInf = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                using (CsvWriter csv = new CsvWriter(exportaInf, CultureInfo.CurrentCulture))
                {
                    var mapa = new TOAlunoMap(tipo);
                    csv.Context.RegisterClassMap(mapa);
                    csv.WriteRecords(alunoParaExportar);
                }
            }
        }

        public static void EditarConclusaoAluno(TOAluno aluno, string conclusao, string tipoAluno = "ALUNO")
        {
            aluno.Conclusao = conclusao;
            aluno.HorarioConclusao = string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now);

            Dados.UpdateDocumento<TOAluno>(aluno);
        }

        public static void ApagaArquivos(string path)
        {
            string[] arquivos = Directory.GetFiles(path);
            for (int i = 0; i < arquivos.Length; i++)
            {
                File.Delete(arquivos[i]);
            }
        }

    }
}