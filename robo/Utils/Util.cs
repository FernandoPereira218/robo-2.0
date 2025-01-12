﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Security.Cryptography;
using CsvHelper;
using System.Globalization;
using robo.Banco_de_Dados;
using System.Linq;
using System.IO.Compression;
using robo.TO;
using robo.Interface;
using robo.Update;

namespace robo.Utils
{
    /// <summary>
    /// Métodos comuns que podem ser utilizados no código como um todo
    /// </summary>
    class Util
    {
        /// <summary>
        /// Inicia nova sessão do browser.
        /// <param name="url">URL que será aberta</param>
        /// <param name="downloadFldr">TRUE se for necessário utilizar a pasta RelatorioExportacao</param>
        /// <param name="firefox">false para utilizar o Google Chrome</param>
        /// <param name="headless">Rodar sem abrir navegador (versão CAE)</param>
        /// </summary>
        public static IWebDriver StartBrowser(string url, bool downloadFldr = false, bool firefox = true, bool headless = false)
        {
            IWebDriver driver;
            string downloadFolder = DefinirPastaDownloads(downloadFldr);

            if (firefox == true)
            {
                driver = CriarDriverFirefox(headless, downloadFolder);
            }
            else
            {
                driver = CriarDriverChrome(downloadFolder);
            }
            driver.Manage().Window.Maximize();
            driver.Manage().Cookies.DeleteAllCookies();
            driver.Url = url;
            //driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(500);
            //if (headless == true)
            //{
                
            //}
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(500);
            

            return driver;
        }

        private static IWebDriver CriarDriverFirefox(bool headless, string downloadFolder)
        {
            IWebDriver driver;
            //Firefox
            var firefoxDriverService = FirefoxDriverService.CreateDefaultService(Environment.CurrentDirectory + @"\driver");
            firefoxDriverService.HideCommandPromptWindow = true;

            FirefoxOptions firefoxOptions = new FirefoxOptions();
            firefoxOptions.AcceptInsecureCertificates = true;
            if (FormInterface.versaoRobo != "operacoesFinanceiras" || headless == true)
            {
                firefoxOptions.AddArgument("--headless");
            }
            var firefoxProfile = new FirefoxProfile();

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
            firefoxOptions.Profile = firefoxProfile;
            driver = new FirefoxDriver(firefoxDriverService, firefoxOptions);
            return driver;
        }

        private static IWebDriver CriarDriverChrome(string downloadFolder)
        {
            IWebDriver driver;
            //Chrome
            var chromeDriverService = ChromeDriverService.CreateDefaultService(Environment.CurrentDirectory + @"\driver");
            chromeDriverService.HideCommandPromptWindow = true;
            ChromeOptions chromeOptions = new ChromeOptions();
            if (FormInterface.versaoRobo != "operacoesFinanceiras")
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
            }

            return driver;
        }

        private static string DefinirPastaDownloads(bool downloadFldr)
        {
            string downloadFolder = "";
            if (downloadFldr == true)
            {
                CriarDiretorioCasoNaoExista("RelatorioExportacao\\");
                downloadFolder = Directory.GetCurrentDirectory() + "\\RelatorioExportacao\\";
            }
            else
            {
                CriarDiretorioCasoNaoExista("DocumentosBaixados\\");
                downloadFolder = Directory.GetCurrentDirectory() + "\\DocumentosBaixados\\";
            }

            return downloadFolder;
        }

        public static IWebDriver CriarBrowserVazio(bool firefox = true)
        {

            if (firefox == true)
            {
                var firefoxDriverService = FirefoxDriverService.CreateDefaultService(Environment.CurrentDirectory + @"\driver");
                firefoxDriverService.HideCommandPromptWindow = true;
                return new FirefoxDriver(firefoxDriverService);
            }
            else
            {
                var chromeDriverService = ChromeDriverService.CreateDefaultService(Environment.CurrentDirectory + @"\driver");
                chromeDriverService.HideCommandPromptWindow = true;
                return new ChromeDriver(chromeDriverService);
            }
        }

        /// <summary>
        /// Cria um diretorio caso não exista
        /// </summary>
        /// <param name="directories">Conjunto de diretórios que devem ser criados</param>
        public static void CriarDiretorioCasoNaoExista(params string[] directories)
        {
            foreach (String directory in directories)
            {
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
            }
        }

        /// <summary>
        /// Busca o diretório de download do usuário
        /// </summary>
        /// <returns>Caminho da pasta de download do usuário</returns>
        public static string BuscarDiretorioDownloads()
        {
            string userRoot = Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadFolder = Path.Combine(userRoot, "Downloads");
            return downloadFolder;
        }

        /// <summary>
        /// Verifica qual o semestre atual
        /// </summary>
        /// <returns>Semestre atual no formato semestre/ano</returns>
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

        /// <summary>
        /// Busca o mes atual
        /// </summary>
        /// <returns></returns>
        public static int BuscarMesAtual()
        {
            DateTime aDate = DateTime.Now;
            return aDate.Month;
        }

        /// <summary>
        /// Retorna o MD5 de uma string
        /// </summary>
        /// <param name="senha">String que será convertida</param>
        /// <returns></returns>
        public static string CriptografarSenha(string senha)
        {
            MD5 md5 = MD5.Create();
            //Conversão a String para array de bytes, que é como a biblioteca trabalha.
            byte[] senhaBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(senha));

            //Criação de um stringBuilder para concatenar os dados
            StringBuilder sb = new StringBuilder();

            //Loop para formatar cada byte como uma String em hexadecimal
            for (int i = 0; i < senhaBytes.Length; i++)
            {
                sb.Append(senhaBytes[i].ToString("x2"));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Exporta todos os alunos do banco de dados para um CSV
        /// </summary>
        /// <param name="countDataGrid">Quantidade de alunos</param>
        /// <param name="tipo">Alunos ou Informações</param>
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
                            MessageBox.Show("Arquivo já existe, e está aberto em outro aplicativo " + ex.Message);
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

        /// <summary>
        /// Exporta todos os alunos do banco de dados para um arquivo CSV
        /// </summary>
        /// <param name="fileName">Nome do arquivo</param>
        /// <param name="tipo">Alunos ou Informações</param>
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

        /// <summary>
        /// Muda a conclusão de um aluno no banco
        /// </summary>
        /// <param name="aluno"></param>
        /// <param name="conclusao"></param>
        /// <param name="tipoAluno"></param>
        public static void EditarConclusaoAluno(TOAluno aluno, string conclusao)
        {
            aluno.Conclusao = conclusao;
            aluno.HorarioConclusao = string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now);

            Dados.UpdateDocumento<TOAluno>(aluno);
        }

        /// <summary>
        /// Apaga todos os arquivos de um diretório
        /// </summary>
        /// <param name="path">Caminho do diretório</param>
        public static void ApagaArquivos(string path)
        {
            string[] arquivos = Directory.GetFiles(path);
            for (int i = 0; i < arquivos.Length; i++)
            {
                File.Delete(arquivos[i]);
            }
        }

        /// <summary>
        /// Verifica se o CPF é válido
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        public static bool VerificaCPFValido(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
            {
                return false;
            }
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            }
            resto = soma % 11;
            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            }
            resto = soma % 11;
            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        /// <summary>
        /// Salva arquivos baixados na pasta "DocumentosBaixados" e os coloca na pasta de Downloads
        /// </summary>
        /// <param name="nomeArquivo">Nome desejado para o arquivo</param>
        /// <param name="tipoDocumento">Tipo de documento baixado</param>
        /// <param name="simplificado">Para DRM, se o arquivo é simplificado ou não</param>
        public static void BaixarDocumento(string nomeArquivo, string tipoDocumento, string simplificado)
        {
            string caminhoDownloads = BuscarDiretorioDownloads();
            DirectoryInfo pastaDownloads = new DirectoryInfo(Directory.GetCurrentDirectory() + "\\DocumentosBaixados");
            EsperarDownload(pastaDownloads);
            FileInfo arquivoBaixado = pastaDownloads.GetFiles().OrderByDescending(f => f.LastWriteTime).First();
            string diretorioFinal;

            if (arquivoBaixado.FullName.EndsWith(".zip"))
            {
                if (tipoDocumento == "DRM")
                {
                    string diretorioDRM = BuscarDiretorioDownloads() + "\\DRM FIES Legado";
                    string diretorioSimplificado = diretorioDRM + "\\Simplificados";
                    string diretorioNaoSimplificado = diretorioDRM + "\\Nao-Simplificados";
                    CriarDiretorioCasoNaoExista(diretorioDRM, diretorioSimplificado, diretorioNaoSimplificado);

                    if (simplificado.Trim() == "Simplificado")
                    {
                        diretorioFinal = diretorioSimplificado;
                    }
                    else
                    {
                        diretorioFinal = diretorioNaoSimplificado;
                    }
                }
                else
                {
                    string diretorio = caminhoDownloads + "\\" + tipoDocumento + " FIES Legado";
                    CriarDiretorioCasoNaoExista(diretorio);
                    diretorioFinal = diretorio;
                }

                File.Copy(arquivoBaixado.FullName, diretorioFinal + "\\" + nomeArquivo + ".zip", true);
            }

            foreach (FileInfo item in pastaDownloads.GetFiles())
            {
                item.Delete();
            }
        }

        /// <summary>
        /// Aguarda até que haja algum arquivo na pasta selecionada
        /// </summary>
        /// <param name="pastaDownloads">Pasta que terá o arquivo baixado</param>
        public static void EsperarDownload(DirectoryInfo pastaDownloads)
        {
            while (pastaDownloads.GetFiles().Count() == 0)
            {
                System.Threading.Thread.Sleep(100);
            }

            while (pastaDownloads.GetFiles().Last().Name.EndsWith("zip") == false)
            {
                System.Threading.Thread.Sleep(100);
            }

            FileInfo ultimoArquivo = pastaDownloads.GetFiles().OrderByDescending(f => f.LastWriteTime).First();
            while (ultimoArquivo.Name.EndsWith(".crdownload") == true || ultimoArquivo.Name.EndsWith(".part") == true)
            {
                System.Threading.Thread.Sleep(1000);
                ultimoArquivo = pastaDownloads.GetFiles().OrderByDescending(f => f.LastWriteTime).First();
            }
        }

        /// <summary>
        /// Salva arquivos exportados na pasta "RelatorioExportacao" e os coloca na pasta de Downloads
        /// </summary>
        /// <param name="Driver"></param>
        /// <param name="tipoRelatorio"></param>
        /// <param name="campus"></param>
        /// <param name="semestre"></param>
        public static void ExportarDocumento(string tipoRelatorio, string campus = "", string semestre = "", string nomeArquivo = "")
        {
            while (Directory.GetFiles("RelatorioExportacao\\").Count() == 0)
            {
                System.Threading.Thread.Sleep(100);
            }

            DirectoryInfo directory = new DirectoryInfo("RelatorioExportacao\\");
            FileInfo myFile = directory.GetFiles().OrderByDescending(f => f.LastWriteTime).First();
            EsperarDownload(directory);

            foreach (var item in Directory.GetFiles("RelatorioExportacao\\"))
            {
                string downloadFolder = BuscarDiretorioDownloads();
                if (nomeArquivo == "")
                {
                    nomeArquivo = DateTime.Now.ToString("dd-MM-yy") + campus + "_" + semestre + ".xls";
                }
                CriarDiretorioCasoNaoExista(downloadFolder + "\\Relatório Exportacao");
                string caminho = downloadFolder + "\\Relatório Exportacao\\" + tipoRelatorio + " " + nomeArquivo;
                File.Copy(item, caminho, true);
                File.Delete(item);
            }
        }

        public static void CriarBackupDB()
        {
            CriarDiretorioCasoNaoExista("backup");
            DirectoryInfo directory = new DirectoryInfo("backup");
            if (directory.GetFiles().Count() >= 5)
            {
                FileInfo myFile = directory.GetFiles().OrderByDescending(f => f.LastWriteTime).Last();
                myFile.Delete();
            }
            File.Copy("data/bdbot.db", "backup/BACKUP_BDBOT " + DateTime.Now.ToString("dd_MM_yy HH-mm-ss") + ".db");
        }
    }
}