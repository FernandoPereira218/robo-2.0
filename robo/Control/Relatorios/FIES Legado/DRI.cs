using OpenQA.Selenium;
using robo.Control.Legado;
using Robo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robo.Control.Relatorios
{
    public class DRI : UtilFiesLegado
    {
        private IWebDriver Driver;
        public void DRIFiesLegado(IWebDriver driver, TOAluno aluno, TOLogin login, bool baixar, string situacaoDRI)
        {
            //Driver = driver;

            ClickDropDown(Driver, "id", "co_situacao_inscricao", situacaoDRI);

            if (!Dados.DRIExists(aluno.Cpf) || baixar == true)
            {
                WaitinLoading(Driver);

                ClickAndWriteById(Driver, "nu_cpf", aluno.Cpf);
                ClickButtonsById(Driver, "consulta");

                WaitinLoading(Driver);

                if (Driver.PageSource.Contains("sorterdocuments"))
                {
                    ClickButtonsByCss(Driver, ".even:nth-child(1) img");

                    if (!Driver.PageSource.Contains("Voltar para a página principal"))
                    {
                        if (!Driver.PageSource.Contains("Inscrição incompleta."))
                        {
                            if (Driver.PageSource.Contains("Imprimir DRI"))
                            {
                                if (baixar == true)
                                {
                                    BaixarDRI(aluno);
                                }
                                else
                                {
                                    SalvarDRIAluno(aluno, login.Campus);
                                }
                            }
                        }
                    }
                }
            }
        }
        private void BaixarDRI(TOAluno aluno)
        {
            ScrollToElementByID(Driver, "imprimir_dri");
            ClickButtonsById(Driver, "imprimir_dri");
            if (!Driver.PageSource.Contains("Voltar para a página principal"))
            {
                string downloadFolder = Util.GetDownloadsFolderPath();
                DirectoryInfo directory = new DirectoryInfo(downloadFolder);

                FileInfo myFile = directory.GetFiles().OrderByDescending(f => f.LastWriteTime).First();
                bool downloading = true;
                while (downloading)
                {
                    System.Threading.Thread.Sleep(1000);
                    myFile = directory.GetFiles().OrderByDescending(f => f.LastWriteTime).First();
                    downloading = myFile.Name.EndsWith(".crdownload");
                }

                string diretorioDRI = downloadFolder + "\\DRI";

                Util.CreateDirectory(diretorioDRI);

                File.Move(myFile.FullName, diretorioDRI + "\\" + aluno.Cpf + "DRI.zip");
                Util.EditarConclusaoAluno(aluno, "DRI Baixada");
                ClickButtonsById(Driver, "voltar");
            }

        }
        private void SalvarDRIAluno(TOAluno aluno, string loginCampus)
        {
            if (!Driver.PageSource.Contains("Voltar para a página principal"))
            {
                var coInscricao = Driver.FindElement(By.Id("co_inscricao"));
                string nroDRI = coInscricao.GetAttribute("value");

                TODRI dri = new TODRI();
                dri.DRI = nroDRI;
                dri.Cpf = aluno.Cpf;
                dri.CampusAditado = loginCampus;

                if (RoboForm.versaoRobo == "operacoesFinanceiras")
                {
                    Dados.InsertDocumento<TODRI>(dri);
                }
                Util.EditarConclusaoAluno(aluno, "DRI Baixada");
                ClickButtonsById(Driver, "voltar");
            }
        }
        public void SetDriver(IWebDriver driver)
        {
            Driver = driver;
        }
    }
}