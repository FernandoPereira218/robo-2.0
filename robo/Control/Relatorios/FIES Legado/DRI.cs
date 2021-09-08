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
    public class DRI
    {
        private IWebDriver Driver;
        private UtilFiesLegado fiesLegadoutil = new UtilFiesLegado();
        public void DRIFiesLegado(IWebDriver driver, TOAluno aluno, TOLogin login, bool baixar, string situacaoDRI)
        {
            Driver = driver;

            Util.ClickDropDown(Driver, "id", "co_situacao_inscricao", situacaoDRI);

            if (!Dados.DRIExists(aluno.Cpf) || baixar == true)
            {
                fiesLegadoutil.WaitinLoading(Driver);

                Util.ClickAndWriteById(Driver, "nu_cpf", aluno.Cpf);
                Util.ClickButtonsById(Driver, "consulta");

                fiesLegadoutil.WaitinLoading(Driver);

                if (Driver.PageSource.Contains("sorterdocuments"))
                {
                    Util.ClickButtonsByCss(Driver, ".even:nth-child(1) img");

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
            Util.ScrollToElementByID(Driver, "imprimir_dri");
            Util.ClickButtonsById(Driver, "imprimir_dri");
            if (!Driver.PageSource.Contains("Voltar para a página principal"))
            {
                string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
                string downloadFolder = System.IO.Path.Combine(userRoot, "Downloads");
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
                Util.ClickButtonsById(Driver, "voltar");
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
                    Dados.InsertDRI(dri);
                }
                Util.EditarConclusaoAluno(aluno, "DRI Baixada");
                Util.ClickButtonsById(Driver, "voltar");
            }
        }
    }
}