using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Robo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TikaOnDotNet.TextExtraction;

namespace robo.pgm
{
    public static class FiesVelhoInf
    {
        private static IWebDriver Driver;
        public static void OpenFiesVelho(List<TOLogin> logins, List<TOAlunoInf> alunos, string campusSelecionado, string semestre, string tipoExecucao, string situacao)
        {
            if (campusSelecionado != "")
            {
                for (int i = logins.Count - 1; i >= 0; i--)
                {
                    if (logins[i].Plataforma == "Novo")
                    {
                        logins.RemoveAt(i);
                    }
                    else if (campusSelecionado.ToUpper() != logins[i].Campus.ToUpper())
                    {
                        logins.RemoveAt(i);
                    }

                }
            }
            try
            {
                Driver = Util.StartBrowser("http://sisfies.mec.gov.br/");
                foreach (TOLogin login in logins)
                {
                    if (alunos.Count == 0)
                    {
                        Driver.Quit();
                        Driver.Dispose();
                        return;
                    }

                    if (RealizarLoginSucesso(login))
                    {
                        if (Driver.PageSource.Contains("co_perfil"))
                        {
                            while (Driver.PageSource.Contains("Aditamentos FIES") == false)
                            {
                                Driver.FindElement(By.XPath("//select[@name='co_perfil']/option[contains(.,'CPSA Presidência')]")).Click();
                                System.Threading.Thread.Sleep(500);
                            }
                            Driver.FindElement(By.XPath("//select[@name='co_perfil']/option[contains(.,'CPSA Presidência')]")).Click();
                        }
                        switch (tipoExecucao.ToUpper())
                        {
                            case "EXTRAIR INFORMAÇÕES DRI":
                                MetodoDRI(alunos, login.Campus, semestre, situacao);
                                break;
                            case "EXTRAIR INFORMAÇÕES DRM":
                                MetodoDRM(alunos, login.Campus, semestre);
                                break;
                            default:
                                break;
                        }


                    }
                    Util.ClickButtonsByXpath(Driver, "//a[contains(text(),'Sair')]");
                }
                if (alunos.Count > 0)
                {
                    foreach (TOAlunoInf aluno in alunos)
                    {
                        //EditarConclusaoAluno(aluno, "Aluno não encontrado em nenhuma conta");
                    }
                }
                Driver.Close();
                Driver.Dispose();
            }
            catch (Exception e)
            {
                Driver.Close();
                Driver.Dispose();
                throw e;
            }
            finally
            {
                // Util.EndBrowser(Driver);
            }
        }

        static Boolean RealizarLoginSucesso(TOLogin login)
        {
            while (Driver.PageSource.Contains("img/titAcessoInstituicao.gif") == false)
            {
                System.Threading.Thread.Sleep(500);
            }
            Util.ClickButtonsByCss(Driver, "#link-instituicao img:nth-child(1)");

            Util.ClickButtonsByCss(Driver, "center:nth-child(10) td:nth-child(2) .guest-box:nth-child(1) span:nth-child(2)");
            while (Driver.Url.Contains("InitAuthenticationByIdentifierAndPassword") == false)
            {
                System.Threading.Thread.Sleep(100);
            }
            Util.ClickAndWriteById(Driver, "id", login.User);
            Util.ClickAndWriteById(Driver, "pw", login.Password);

            Util.ClickButtonsById(Driver, "botoes");
            if (!Driver.PageSource.Contains("A senha informada não confere. Número de tentativas restAes:"))//Ocorreu uma falha na execução da aplicação. A caixa de erro ao lado mostra o motivo da falha. Provavelmente alguma informação incorreta foi processada.
            {
                return true;
            }
            else
            {
                throw new Exception("A senha informada não confere. Por favor, cheque se todos logins foram inseridos corretamente.");
            }
        }
        public static void MetodoDRI(List<TOAlunoInf> alunos, string campusAtual, string semestreAtual, string situacao)
        {
            System.Threading.Thread.Sleep(1000);
            Util.ClickButtonsByXpath(Driver, "//a[contains(text(),'Validação pela CPSA Fies')]");

            Util.ClickDropDown(Driver, "id", "co_situacao_inscricao", situacao);

            for (int i = 0; i < alunos.Count - 1; i++)
            {
                if (alunos[i].Conclusao == "DRI Baixado")
                {
                    continue;
                }

                Util.ClickAndWriteById(Driver, "nu_cpf", alunos[i].Cpf);
                Util.ClickButtonsById(Driver, "consulta");
                WaitinLoading();

                if (Driver.PageSource.Contains("sorterdocuments"))
                {
                    Util.ClickButtonsByCss(Driver, ".even:nth-child(1) img");

                    if (!Driver.PageSource.Contains("Voltar para a página principal"))
                    {
                        if (!Driver.PageSource.Contains("Inscrição incompleta."))
                        {
                            string CodigoFonte = Driver.FindElement(By.TagName("body")).Text;

                            // Começa 
                            string semestraAditar = CodigoFonte.Split(new string[] { "Semestre a que se refere esta inscrição:" }, StringSplitOptions.None)[1];
                            string curso = CodigoFonte.Split(new string[] { "Curso:" }, StringSplitOptions.None)[1];
                            string duracao = CodigoFonte.Split(new string[] { "Duração Regular do Curso:" }, StringSplitOptions.None)[1];
                            
                            // A Atribuir
                            string select;
                            string selectFinanciadoSemestre;


                            if (Util.VerificarElementoExiste(Driver, "id", "qt_semestre_concluido") == null)
                            {
                                select = CodigoFonte.Split(new string[] { "Total de semestres já concluídos:*" }, StringSplitOptions.None)[1];
                                select = select.Split(new string[] { "\r\n" }, StringSplitOptions.None)[0];
                            }
                            else
                            {
                                var Concluidos = Driver.FindElement(By.Id("qt_semestre_concluido"));
                                select = new SelectElement(Concluidos).SelectedOption.Text;
                            }

                            // Texto padrao 
                            string aSerCursado = Driver.FindElement(By.Id("nu_semestre_a_cursar")).Text;
                            string jaFianciados = Driver.FindElement(By.Id("qt_semestre_financiamento")).Text;
                            string percentual = Driver.FindElement(By.Id("nuPercentualFinanciamento")).Text;
                            string gradeAtualComDesconto;

                            if (Util.VerificarElementoExiste(Driver, "id", "vl_semestre_atual") == null)
                            {
                                gradeAtualComDesconto = CodigoFonte.Split(new string[] { "Valor da semestralidade a ser cursado com desconto - Grade Curricular a ser Cursada:*" }, StringSplitOptions.None)[1];
                                gradeAtualComDesconto = gradeAtualComDesconto.Split(new string[] { "\r\n" }, StringSplitOptions.None)[0];
                            }
                            else
                            {
                                var InputSemestreAtual = Driver.FindElement(By.Id("vl_semestre_atual"));
                                gradeAtualComDesconto = InputSemestreAtual.GetAttribute("value");
                            }
                            
                            if (Util.VerificarElementoExiste(Driver, "id", "vl_financiado_semestre") == null)
                            {
                                selectFinanciadoSemestre = CodigoFonte.Split(new string[] { "Valor a ser financiado no semestre a ser cursado com recursos do FIES:*" }, StringSplitOptions.None)[1];
                                selectFinanciadoSemestre = selectFinanciadoSemestre.Split(new string[] { "\r\n" }, StringSplitOptions.None)[0];
                            }
                            else
                            {
                                var Concluidos = Driver.FindElement(By.Id("vl_financiado_semestre"));
                                selectFinanciadoSemestre = new SelectElement(Concluidos).SelectedOption.Text;
                            }
                            string Coparticipacao = Driver.FindElement(By.Id("vlMesSemestreEstudante")).Text;

                            alunos[i].SemestreAditar = semestraAditar.Split(new string[] { "\r\n" }, StringSplitOptions.None)[0];
                            alunos[i].Curso = curso.Split(new string[] { "Turno:" }, StringSplitOptions.None)[0];
                            alunos[i].DuracaoRegular = duracao.Split(new string[] { "\r\n" }, StringSplitOptions.None)[0];
                            alunos[i].TotalDeSemestresConcluidos = select;
                            alunos[i].TotalDeSemestresJaFinanciados = jaFianciados;
                            alunos[i].PercentualDeFinanciamentoSolicitado = percentual;
                            alunos[i].GradeAtualComDesconto = gradeAtualComDesconto;
                            alunos[i].GradeAtualFinanciadoFIES = selectFinanciadoSemestre;
                            alunos[i].GradeAtualCoparticipacao = Coparticipacao;

                            EditarConclusaoAluno(alunos[i], "DRI Baixado");

                            Util.ScrollToElementByID(Driver, "voltar");
                            Util.ClickButtonsById(Driver, "voltar");


                        }
                    }
                }
            }
        }
        public static void MetodoDRM(List<TOAlunoInf> alunos, string campusAtual, string semestreAtual)
        {
            Util.ClickButtonsByCss(Driver, "div:nth-child(3) > ul > .menu-button:nth-child(3) > a");
            WaitinLoading();
            System.Threading.Thread.Sleep(200);

            for (int i = alunos.Count - 1; i >= 0; i--)
            {
                if (alunos[i].Conclusao == "DRM Baixado")
                {
                    alunos.RemoveAt(i);
                    continue;
                }
                Util.ClickDropDown(Driver, "id", "co_finalidade_aditamento", "Aditamento de Renovação");
                WaitinLoading();
                semestreAtual = semestreAtual.Replace("1/", "1º/");
                semestreAtual = semestreAtual.Replace("2/", "2º/");

                Util.ClickDropDown(Driver, "id", "coSemestreAditamento", semestreAtual);
                DRMAlunoSucesso(alunos[i], campusAtual, semestreAtual);

                if (alunos[i].Conclusao == "DRM Baixado")
                {
                    WaitinLoading();
                    Util.ClickDropDown(Driver, "id", "co_finalidade_aditamento", "Aditamento de Renovação");
                    alunos.RemoveAt(i);
                }
            }
            Exportar();
        }

        public static void Exportar()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CPF", "Cpf");
            dic.Add("Semestre a Aditar", "SemestreAditar");
            dic.Add("Curso", "Curso");
            dic.Add("Duração regular", "DuracaoRegular");
            dic.Add("Total de semestres suspensos", "TotalDeSemestresSuspensos");
            dic.Add("Total de semestres dilatados", "TotalDeSemestresDilatados");
            dic.Add("Total de semestres já concluidos e/ou aproveitadosnesta IES/curso", "TotalDeSemestresConcluidos");
            dic.Add("Semestre a ser cursado pelo estudante", "SemestreSerCursadoPeloEstudante");
            dic.Add("Total de semestres já financiados", "TotalDeSemestresJaFinanciados");
            dic.Add("Percentual de financiamento solicitado", "PercentualDeFinanciamentoSolicitado");
            dic.Add("Grade Atual Semestralidade (R$) com desconto", "GradeAtualComDesconto");
            dic.Add("Grade Atual Semestralidade (R$) Financiado FIES", "GradeAtualFinanciadoFIES");
            dic.Add("Grade Atual Semestralidade (R$) Coparticipação", "GradeAtualCoparticipacao");

            List<TOAlunoInf> alunoParaExportar = Database.Acess.SelectAll<TOAlunoInf>("ALUNOINF");

            foreach (var item in alunoParaExportar)
            {
                Util.AcertaBarraR(item);
            }

            CSVManager.CSVManager.ExportCSV<TOAlunoInf>(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads", "InformacoesAlunos_Parcial.csv", dic, alunoParaExportar);
        }
        public static void WaitinLoading()
        {
            IWebElement Carregando = Driver.FindElement(By.ClassName("background-grey"));
            bool carr = Carregando.Displayed;
            while (carr == true)
            {
                System.Threading.Thread.Sleep(1000);
                Carregando = Driver.FindElement(By.ClassName("background-grey"));
                carr = Carregando.Displayed;
            }
        }

        public static void DRMAlunoSucesso(TOAlunoInf aluno, string campusAtual, string semestre)
        {
            WaitinLoading();
            Util.ClickAndWriteById(Driver, "cpf", aluno.Cpf);

            semestre = semestre.Replace("1/", "1º/");
            semestre = semestre.Replace("2/", "2º/");
            Util.ClickDropDown(Driver, "id", "coSemestreAditamento", semestre);

            Util.ClickButtonsById(Driver, "consultar");

            if (Driver.PageSource.Contains("Lista de Aditamentos"))
            {
                IWebElement lista = Driver.FindElement(By.Id("lista"));
                if (lista.Text.Contains("Contratado") || lista.Text.Contains("Enviado ao banco") || lista.Text.Contains("Recebido pelo banco") || lista.Text.Contains("Validado para contratação"))
                {
                    Util.ClickButtonsByCss(Driver, "td > a > img");

                    IWebElement listaME = Driver.FindElement(By.Id("lista-mensageiro-erros"));
                    while (!listaME.Displayed && !Driver.PageSource.Contains("imprimir"))
                    {
                        System.Threading.Thread.Sleep(500);
                    }
                    if (listaME.Displayed)
                    {
                        IWebElement listaF = listaME.FindElement(By.XPath(".//li"));

                        EditarConclusaoAluno(aluno, listaF.Text, campusAtual);
                    }
                    else if (Driver.PageSource.Contains("imprimir"))
                    {
                        string conclusao = BaixarDRM(ref aluno);
                        EditarConclusaoAluno(aluno, conclusao, campusAtual);
                    }
                    else
                    {
                        throw new Exception("erro de logica");
                    }

                }
                else
                {
                    IWebElement tabela = Driver.FindElement(By.XPath("//table/tbody/tr[1]/td[6]"));
                    EditarConclusaoAluno(aluno, tabela.Text, campusAtual);
                }
            }
        }

        static void EditarConclusaoAluno(TOAlunoInf aluno, string conclusao, string campusAditado = null)
        {
            aluno.Conclusao = conclusao;
            aluno.HorarioConclusao = string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now);

            Dados.UpdateAluno(aluno);
        }

        public static string BaixarDRM(ref TOAlunoInf aluno)
        {
            Util.ClickButtonsById(Driver, "imprimirDrm");
            String userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            String downloadFolder = Path.Combine(userRoot, "Downloads");
            DirectoryInfo directory = new DirectoryInfo(downloadFolder);
            string sourcecode = Driver.PageSource;
            FileInfo myFile = directory.GetFiles().OrderByDescending(f => f.LastWriteTime).First();
            bool downloading = true;
            //while (downloading)
            //{
            //    System.Threading.Thread.Sleep(1000);
            //    myFile = directory.GetFiles().OrderByDescending(f => f.LastWriteTime).First();
            //    downloading = myFile.Name.EndsWith(".crdownload");
            //}
            while (myFile.Name.EndsWith(".zip") == false)
            {
                System.Threading.Thread.Sleep(1000);
                myFile = directory.GetFiles().OrderByDescending(f => f.LastWriteTime).First();
                downloading = myFile.Name.EndsWith(".crdownload");
            }

            String diretorioDRM = "DRM_Informações";
            Util.CreateDirectory(diretorioDRM);
            ApagaArquivos(diretorioDRM);


            File.Move(myFile.FullName, diretorioDRM + "\\DRM_ALUNO.zip");

            Util.ClickButtonsById(Driver, "voltar");


            string conclusao = "DRM Baixado";

            ZipFile.ExtractToDirectory(diretorioDRM + "\\DRM_ALUNO.zip", diretorioDRM);
            String[] arquivoZIP = Directory.GetFiles(diretorioDRM);
            string arquivoCPS = string.Empty;
            foreach (string item in arquivoZIP)
            {
                if (item.Contains("CPSA") == true)
                {
                    arquivoCPS = item;
                    break;
                }
            }
            string informacoes = new TextExtractor().Extract(arquivoCPS).Text;
            //string informacoes = ExtrairTexto_PDF(arquivoCPS);
            ProcessarInfsFiesVelho(informacoes, ref aluno, sourcecode);
            ApagaArquivos(diretorioDRM);
            return conclusao;
        }

        public static void ProcessarInfsFiesVelho(string inf, ref TOAlunoInf aluno, string percentualFinanciamento)
        {
            try
            {
                string depoisTitulo;
                TOAlunoInf infs = new TOAlunoInf();
                infs.Cpf = aluno.Cpf;
                if (inf.Contains("TERMO ADITIVO AO CONTRATO") == true)
                {
                    return;
                }
                //Semestre a aditar
                depoisTitulo = inf.Split(new string[] { "Data do DRM:" }, StringSplitOptions.None)[1];
                string depoisNovaLinha = depoisTitulo.Split('\n')[1];
                infs.SemestreAditar = depoisNovaLinha.Split(':')[1];
                //Curso
                depoisTitulo = inf.Split(new string[] { "Curso:" }, StringSplitOptions.None)[1];
                infs.Curso = depoisTitulo.Split('\n')[0];
                //Duraçao Regular
                depoisTitulo = inf.Split(new string[] { "Duração regular:" }, StringSplitOptions.None)[1];
                infs.DuracaoRegular = depoisTitulo.Split(' ')[1].Split(' ')[0];
                //Total de semestres suspensos
                depoisTitulo = inf.Split(new string[] { "Total de semestres suspensos:" }, StringSplitOptions.None)[1];
                infs.TotalDeSemestresSuspensos = depoisTitulo.Split('\n')[0];
                //Total de semestres dilatados
                depoisTitulo = inf.Split(new string[] { "Total de semestres dilatados:" }, StringSplitOptions.None)[1];
                infs.TotalDeSemestresDilatados = depoisTitulo.Split('\n')[0];
                //Total de semestres já concluídos e/ou aproveitados nesta IES/curso
                depoisTitulo = inf.Split(new string[] { "IES/curso:" }, StringSplitOptions.None)[1];
                infs.TotalDeSemestresConcluidos = depoisTitulo.Split('\n')[0];
                //Semestre a ser cursado pelo estudante
                depoisTitulo = inf.Split(new string[] { "Semestre a ser cursado pelo estudante:" }, StringSplitOptions.None)[1];
                infs.SemestreSerCursadoPeloEstudante = depoisTitulo.Split('\n')[0];
                //Total de semestre já financiados:
                depoisTitulo = inf.Split(new string[] { "Total de semestres já financiados:" }, StringSplitOptions.None)[1];
                infs.TotalDeSemestresJaFinanciados = depoisTitulo.Split('\n')[0];
                //Percentual de financiamento solicitado:
                depoisTitulo = percentualFinanciamento.Split(new string[] { "Percentual de Financiamento solicitado:</strong>" }, StringSplitOptions.None)[1];
                infs.PercentualDeFinanciamentoSolicitado = depoisTitulo.Split('\n')[1];
                //Valor da semestralidade e da mensalidade do curso - Grade Curricular Regular:
                depoisTitulo = inf.Split(new string[] { "Valor da semestralidade e da mensalidade atual - Grade Curricular a ser cursada no semestre" }, StringSplitOptions.None)[1];
                infs.GradeAtualComDesconto = depoisTitulo.Split(' ')[8];
                infs.GradeAtualFinanciadoFIES = depoisTitulo.Split(' ')[11];
                infs.GradeAtualCoparticipacao = depoisTitulo.Split(' ')[14];
                //infs.Conclusao = "DRM Baixada";

                aluno = infs;

                Util.AcertaBarraR(aluno);

            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Erro não esperado encontrado.\n Contate os alunos brilhantes.\n\n{0}", e.Message));
            }
        }
        public static string ExtrairTexto_PDF(string caminho)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.FileName = "main2.exe";
            string pdfPath = caminho;
            processInfo.Arguments = $"\"{pdfPath}\"";
            processInfo.UseShellExecute = false;
            processInfo.CreateNoWindow = true;
            processInfo.RedirectStandardOutput = true;
            processInfo.RedirectStandardError = true;
            string results = "";

            using (var process = Process.Start(processInfo))
            {
                results = process.StandardOutput.ReadToEnd();
                if (results != "")
                {
                    //throw new Exception("RESULTADO: " + results);
                }
                else
                {

                    string error = process.StandardError.ReadToEnd();
                    throw new Exception("ERRO: " + error);
                }

            }
            return results;
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
