using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using Keys = OpenQA.Selenium.Keys;
using System.IO.Compression;
using System.Text;
using System.Diagnostics;

namespace Robo
{
    public static class FiesVelho
    {
        const String S0005 = "(S0005) Aditamento confirmado pela CPSA.";
        const String ERRO0336 = "(E0336) Código de verificação inválido.";
        const String ERRO0329 = "(E0329) O valor do campo “Valor da semestralidade para o FIES” não pode ultrapassar 95% do campo “Valor da Semestralidade COM desconto”.";
        const String ERRO0019 = "(E0019) - O valor da semestralidade com desconto não pode ser superior a";

        static Boolean PossuiErro0329, PossuiErroSiteCaiu, PossuiErroSuperior;
        static Boolean emLote = false;

        private static bool listaEncontrada = false;

        static IWebDriver Driver;

        /// <summary>
        /// Abre e popula FiesVelhometodo
        /// </summary>
        /// <param name="aluno"></param>
        public static void OpenFiesVelho(List<TOLogin> logins, List<TOAluno> alunos, string tipoExecucao, string campusSelecionado, string numSemestre, string semestre, bool buscarStatus, string situacaoDRI)
        {
            if (alunos.Count == 1)
            {
                emLote = false;
            }
            else
            {
                emLote = true;
            }
            if (campusSelecionado != "")
            {
                for (int i = logins.Count - 1; i >= 0; i--)
                {
                    if (campusSelecionado.ToUpper() != logins[i].Campus.ToUpper())
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
                        Driver.Close();
                        Driver.Dispose();
                        return;
                    }
                    if (RealizarLoginSucesso(login))
                    {
                        while (Driver.PageSource.Contains("Aditamentos FIES") == false)
                        {
                            Driver.FindElement(By.XPath("//select[@name='co_perfil']/option[contains(.,'CPSA Presidência')]")).Click();
                            System.Threading.Thread.Sleep(500);
                        }
                        switch (tipoExecucao.ToUpper())
                        {
                            case "ADITAMENTO":
                                MetodoAditamento(login, alunos, numSemestre);
                                break;

                            case "BAIXAR DRM":
                                if (buscarStatus == false)
                                {
                                    BuscarDocumento(alunos, login.Campus, semestre, "DRM");
                                }
                                else
                                {
                                    BuscarStatusAluno(alunos, login.Campus, semestre, "DRM");
                                }
                                break;

                            case "BAIXAR DRT":
                                if (buscarStatus == false)
                                {
                                    BuscarDocumento(alunos, login.Campus, semestre, "DRT");
                                }
                                else
                                {
                                    BuscarStatusAluno(alunos, login.Campus, semestre, "DRT");
                                }
                                break;

                            case "BAIXAR DRD":
                                if (buscarStatus == false)
                                {
                                    BuscarDocumento(alunos, login.Campus, semestre, "DRD");
                                }
                                else
                                {
                                    BuscarStatusAluno(alunos, login.Campus, semestre, "DRD");
                                }
                                break;

                            case "SUSPENSÃO":
                                if (buscarStatus == false)
                                {
                                    BuscarDocumento(alunos, login.Campus, semestre, "Suspensao");
                                }
                                else
                                {
                                    BuscarStatusAluno(alunos, login.Campus, semestre, "Suspensao");
                                }
                                break;
                            case "BAIXAR DRI":
                                if (buscarStatus == false)
                                {
                                    MetodoDRI(alunos, login, true, situacaoDRI);
                                }
                                else
                                {
                                    BuscarStatusDRI(alunos, login);
                                }
                                break;
                            case "IMPORTAR DRI PARA BANCO DE DADOS":
                                if (buscarStatus == false)
                                {
                                    MetodoDRI(alunos, login, false, situacaoDRI);
                                }
                                else
                                {
                                    BuscarStatusDRI(alunos, login);
                                }
                                break;

                            default:
                                break;
                        }
                    }
                    Util.ClickButtonsByXpath(Driver, "//a[contains(text(),'Sair')]");

                }
                Driver.Quit();
                Driver.Dispose();
            }
            catch (Exception e)
            {
                Driver.Quit();
                Driver.Dispose();
                throw e;
            }
            finally
            {
                //Driver.Quit();
                //Driver.Dispose();
            }
        }

        #region Aditamento
        public static void MetodoAditamento(TOLogin login, List<TOAluno> alunos, string nrSemestre = "65")
        {
            System.Threading.Thread.Sleep(1000);
            Util.ClickButtonsByCss(Driver, "div:nth-child(3) > ul > .menu-button:nth-child(2) > a");

            for (int i = alunos.Count - 1; i >= 0; i--)
            {
                if (Dados.DRIExists(alunos[i].Cpf))
                {
                    TODRI driAtual = Dados.GetDRI(alunos[i].Cpf);
                    if (alunos[i].Campus.Equals(login.Campus.ToUpper()) /*|| (alunos[i].NumCampusAtual.Equals(login.Numero) && String.Empty.Equals(driAtual.CampusAditado))*/)
                    {
                        string driNum = driAtual.DRI;
                        string url = string.Format("http://sisfies.mec.gov.br/cpsa/aditamento/formulario/co_inscricao/{0}/sem/{1}", driNum, nrSemestre);
                        Driver.Url = url;

                        System.Threading.Thread.Sleep(1000);
                        IWebElement element = Driver.FindElement(By.Id("lista-mensageiro-erros"));
                        if (element.Displayed == false)
                        {
                            VerificarEAditar(login, alunos[i], url);
                        }
                        else
                        {
                            alunos[i].Conclusao = element.Text;
                        }

                        Dados.UpdateAluno(alunos[i]);
                        alunos.RemoveAt(i);
                    }
                }
                else
                {
                    EditarConclusaoAluno(alunos[i], "DRI não encontrada");

                    alunos.RemoveAt(i);

                    //dri não cadastrada
                    //if (alunos[i].NumCampusAtual.Equals(login.Numero))
                    //{
                    //    if (alunos[i].CampusAditado.Equals(login.Campus) || String.Empty.Equals(alunos[i].CampusAditado))
                    //    {
                    //        TOAluno alunoAtual = AditamentoAlunoSucesso(alunos[i]);
                    //        alunoAtual.CampusAditado = login.Campus;

                    //        Dados.UpdateAluno(alunoAtual);
                    //        alunos.RemoveAt(i);
                    //    }
                    //}
                }
            }
        }

        public static void VerificarEAditar(TOLogin login, TOAluno aluno, string url)
        {
            if (!Driver.PageSource.Contains("Voltar para a página principal") && Driver.Url == url)
            {
                if (Driver.PageSource.Contains("igual ou superior a 75% no semestre"))
                {
                    VerificarEPreencherFormulario(aluno, login.Campus);
                    //alunos[i].CampusAditado = login.Campus;
                    //alunos[i].HorarioConclusao = string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now);
                }
                else
                {
                    //alunos[i].Conclusao = "Acadêmico aditado anteriormente";
                    //alunos[i].CampusAditado = login.Campus;
                    //alunos[i].HorarioConclusao = string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now);
                    EditarConclusaoAluno(aluno, "Acadêmico aditado anteriormente", login.Campus);
                }
            }
            else
            {
                if (!Driver.PageSource.Contains("Voltar para a página principal"))
                {
                    IWebElement listaME = Driver.FindElement(By.Id("lista-mensageiro-erros"));

                    if (listaME.Displayed)
                    {
                        IWebElement listaF = listaME.FindElement(By.XPath(".//li"));
                        if (listaF.Text.Contains("E0037"))
                        {
                            //alunos[i].Conclusao = "Erro dilatação";
                            EditarConclusaoAluno(aluno, "Erro dilatação", login.Campus);
                        }
                    }
                    else
                    {
                        //alunos[i].Conclusao = "Pagina de aditamento não encontrada";
                        EditarConclusaoAluno(aluno, "Pagina de aditamento não encontrada", login.Campus);
                    }
                }
                else
                {
                    //alunos[i].Conclusao = "Pagina de aditamento não encontrada";
                    EditarConclusaoAluno(aluno, "Pagina de aditamento não encontrada", login.Campus);
                }

                //aluno.CampusAditado = login.Campus;
                //alunos[i].HorarioConclusao = string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now);
            }
        }

        /// <summary>
        /// CODIGO LEGADO??? Realiza Login e retorna se achou o aluno e se será realizado o aditamento desse aluno (Apenas se nao achar dri)
        /// </summary>
        /// <param name="logins">Lista de Logins</param>
        /// <param name="aluno">TOAluno para aditamento</param>
        /// <returns></returns>
        public static TOAluno AditamentoAlunoSucesso(TOAluno aluno, string campusAtual)
        {
            Util.ClickAndWriteById(Driver, "cpf", aluno.Cpf);
            String urlOld = Driver.Url;
            while (urlOld == Driver.Url)
            {
                System.Threading.Thread.Sleep(1000);
            }

            if (Driver.PageSource.Contains("Lista de Aditamentos"))
            {
                IWebElement lista = Driver.FindElement(By.Id("lista"));
                if (lista.Text.Contains("Não iniciado pela CPSA") || lista.Text.Contains("Rejeitado pelo estudante") || lista.Text.Contains("Cancelado por decurso de prazo do banco"))
                {
                    Util.ClickButtonsByCss(Driver, "#solicita-aditamento > img");

                    VerificarEPreencherFormulario(aluno);
                }
                else if (lista.Text.Contains("Pendente de validação"))
                {
                    EditarConclusaoAluno(aluno, "Pendente de validação");
                }
                else if (lista.Text.Contains("Validado para contratação"))
                {
                    EditarConclusaoAluno(aluno, "Validado para contratação");
                }
                else if (lista.Text.Contains("Enviado ao banco"))
                {
                    EditarConclusaoAluno(aluno, "Enviado ao banco");
                }
                else if (lista.Text.Contains("Recebido pelo banco"))
                {
                    EditarConclusaoAluno(aluno, "Recebido pelo banco");
                }
                else if (lista.Text.Contains("Prazo para Aditamento Expirado"))
                {
                    EditarConclusaoAluno(aluno, "Prazo para Aditamento Expirado");
                }
                else if (lista.Text.Contains("Cancelado por decurso de prazo do estudante"))
                {
                    EditarConclusaoAluno(aluno, "Cancelado por decurso de prazo do estudante");
                }
                else if (lista.Text.Contains("Rejeitado pela CPSA"))
                {
                    EditarConclusaoAluno(aluno, "Rejeitado pela CPSA");
                }
                else if (lista.Text.Contains("Reaberto para correção"))
                {
                    EditarConclusaoAluno(aluno, "Reaberto para correção");
                }
                else if (lista.Text.Contains("Aditamento Pendente de Correção pelo Banco"))
                {
                    EditarConclusaoAluno(aluno, "Aditamento Pendente de Correção pelo Banco");
                }
                else if (lista.Text.Contains("Contratado"))
                {
                    EditarConclusaoAluno(aluno, "Contratado");
                }
                else if (lista.Text.Contains("Aditamento preliminar"))
                {
                    EditarConclusaoAluno(aluno, "Aditamento preliminar");
                }
                else
                {
                    EditarConclusaoAluno(aluno, "Aluno não dispónivel para edição por motivo desconhecido");
                }
                return aluno;
            }
            EditarConclusaoAluno(aluno, "Pendente");
            //Aumenta o numero do numcampus atual para continuar tentando esse aluno
            int n = int.Parse(aluno.NumCampusAtual) + 1;
            aluno.NumCampusAtual = n.ToString();

            return aluno;
        }

        private static void VerificarEPreencherFormulario(TOAluno aluno, string CampusAtual = "Campus nao informado pelo codigo")
        {
            IWebElement listaME = Driver.FindElement(By.Id("lista-mensageiro-erros"));
            while (!listaME.Displayed && !Util.IsFormFillingFiesVelho(Driver))
            {
                System.Threading.Thread.Sleep(10);
            }
            if (listaME.Displayed)
            {
                IWebElement listaF = listaME.FindElement(By.XPath(".//li"));
                EditarConclusaoAluno(aluno, listaF.Text);
            }
            else if (Driver.PageSource.Contains("cancelar-rejeicao"))
            {
                EditarConclusaoAluno(aluno, "Rejeitado Anteriormente");
            }
            else if (Util.IsFormFillingFiesVelho(Driver))
            {
                PreencherFormulario(aluno);
            }
            else
            {
                throw new Exception("erro de logica");
            }
        }

        /// <summary>
        /// Preenche o formulario
        /// </summary>
        /// <param name="aluno">TOAluno para aditamento</param>
        static void PreencherFormulario(TOAluno aluno, bool erro95 = false)
        {
            //Clica no Turno, caos seja um dropdown
            if (Driver.FindElement(By.Name("co_turno")).Displayed)
            {
                Driver.FindElement(By.Name("co_turno")).Click();
                IWebElement dd = Driver.FindElement(By.Name("co_turno"));
                dd.SendKeys(Keys.Down);
                dd.SendKeys(Keys.Enter);
            }

            Util.WaitLogoLoading(Driver);

            PreencheReceitas(aluno, erro95);

            if ("Aproveitamento Superior a 75%".Equals(aluno.AproveitamentoAtual.Trim()) || "Aproveitamento em análise (estágio)".Equals(aluno.AproveitamentoAtual.Trim()))
            {
                CasoComAproveitamento(aluno.Justificativa);
            }
            else
            {
                Util.ClickButtonsByCss(Driver, "#divAproveitamentoAcademico input:nth-of-type(1)"); //O estudante teve aproveitamento acadêmico igual ou superior a 75% no semestre ? NAO

                //switch (aluno.HistoricoAproveitamento)
                //{
                //
                //    case "1º Reconsideração": // Vai passar
                //    case "2º Reconsideração": // Vai passar
                //    case "Reprovação em semestre anterior":
                //        //aluno.Conclusao = CasoSemAproveitamento(aluno);
                //        EditarConclusaoAluno(aluno, CasoSemAproveitamento(aluno));
                //        break;
                //    case "Excesso de reprovação":
                //        //aluno.Conclusao = CasoExcessoReprovacao();
                //        EditarConclusaoAluno(aluno, CasoExcessoReprovacao());
                //        break;
                //
                //    default:
                //        throw new Exception("Jeny escreveu errado.");
                //}
                if (aluno.HistoricoAproveitamento.Contains("Excesso de reprovação") == true)
                {
                    EditarConclusaoAluno(aluno, CasoExcessoReprovacao(aluno.Justificativa));
                }
                else
                {
                    EditarConclusaoAluno(aluno, CasoSemAproveitamento(aluno));
                }
            }
            SystemSounds.Beep.Play();

            bool possuiErros = false;

            //quando um deles virar true para de rodar
            while (!possuiErros && Util.IsFormFillingFiesVelho(Driver))
            {
                possuiErros = PossuiErros(Driver.PageSource);
                System.Threading.Thread.Sleep(100); //timer de 100 millisegundos para não sobrecarregar os sistema             
            }

            ChecarSePossuiErros(possuiErros, aluno);
        }

        /// <summary>
        /// Preenche os campos de receita.
        /// </summary>
        /// <param name="aluno">Transfer Object TOAluno</param>
        static void PreencheReceitas(TOAluno aluno, bool erro95)
        {
            //Clica e Digita no Valor da Semestralidade SEM desconto – Grade Curricular Regular
            Util.ClickAndWriteById(Driver, "vl_semestre_sem_desconto", aluno.ReceitaBruta);

            //Clica e Digita no Valor da Semestralidade COM desconto – Grade Curricular Regular
            if (erro95)
            {
                Util.ClickAndWriteById(Driver, "vl_semestre_com_desconto", aluno.ValorAditado);
            }
            else
            {
                Util.ClickAndWriteById(Driver, "vl_semestre_com_desconto", aluno.ReceitaLiquida);
            }

            //Clica e Digita no Valor da semestralidade para o FIES R$
            Util.ClickAndWriteById(Driver, "vl_semestralidade_para_fies", aluno.ReceitaFies);

            //Clica e Digita no Valor da Semestralidade ATUAL COM desconto - Grade Curricular a ser Cursada
            Util.ClickAndWriteById(Driver, "vl_semestre_atual", aluno.ReceitaFies);


            //Pegar Valor a ser financiado no semestre ATUAL com recursos do FIES - Valor drm financiamento
            aluno.ValorAditadoFinanciamento = Driver.FindElement(By.Id("vl_financiado_semestre")).Text;

            //Pegar Valor a ser pago no semestre ATUAL com recursos do estudante - 
            aluno.ValorPagoRecursoEstudante = Driver.FindElement(By.Id("vlMesSemestreEstudante")).Text;
        }

        static void ChecarSePossuiErros(bool possuiErros, TOAluno aluno)
        {
            if (possuiErros)
            {
                Driver.Navigate().Refresh();
                if (PossuiErro0329)
                {
                    //Dados.TratarPorcentagemReceita(aluno, false);
                    //PreencherFormulario(aluno);
                    EditarConclusaoAluno(aluno, "Erro 95%");
                }
                else if (PossuiErroSiteCaiu)
                {
                    //aluno.Conclusao = "Site caiu";
                    EditarConclusaoAluno(aluno, "Site caiu");
                }
                else if (PossuiErroSuperior)
                {
                    //aluno.Conclusao = "O valor da semestralidade com desconto não pode ser superior a R$ 42.983,70.";
                    EditarConclusaoAluno(aluno, "O valor da semestralidade com desconto não pode ser superior a R$ 42.983,70.");
                }
                else
                {
                    PreencherFormulario(aluno);
                }
            }
            else
            {
                //aluno.Conclusao = GoBackToLogin();
                EditarConclusaoAluno(aluno, GoBackToLogin());
            }
        }

        /// <summary>
        /// Preenche Justificativa do Valor da semestralidade COM desconto.
        /// </summary>
        static void Justificativa(string justificativaAluno)
        {
            IWebElement justificativa = Driver.FindElement(By.Id("divVariacao"));
            if (justificativa.Displayed)
            {
                if (justificativa.Text.Contains("Valor da semestralidade COM desconto"))
                {
                    //Alteração na grade curricular em relação ao semestre anterior
                    //Util.ClickAndWriteById(Driver, "ds_justificativa", "                                                                                    "); // Sim, é pra ser assim
                    if (justificativaAluno == string.Empty)
                    {
                        Util.ClickAndWriteById(Driver, "ds_justificativa", "Alteração na grade curricular em relação ao semestre anterior"); // Sim, é pra ser assim
                    }
                    else
                    {
                        Util.ClickAndWriteById(Driver, "ds_justificativa", justificativaAluno);
                    }
                }
            }
        }

        /// <summary>
        /// Preenche no padrão de aproveitamento a cima de 75%.
        /// </summary>
        static void CasoComAproveitamento(string justificativaAluno)
        {
            //O estudante teve aproveitamento acadêmico igual ou superior a 75% no semestre ? SIM
            Util.ClickButtonsByCss(Driver, "#divAproveitamentoAcademico input:nth-child(3)");

            //O estudante está regularmente matriculado? SIM
            Util.ClickButtonsByCss(Driver, "#divRegularidadeMatricula input:nth-child(3)");

            //O estudante possui benefício simultâneo de FIES e de bolsa ProUni em local de oferta ou curso distinto? NAO
            Util.ClickButtonsByXpath(Driver, "(//input[@name=\'beneficio\'])[1]");

            //O prazo de duração regular do curso encontra-se vigente? SIM
            Util.ClickButtonsByCss(Driver, "#divPrazoCurso input:nth-child(3)");

            //O estudante transferiu de curso mais de uma vez nessa IES? NAO
            Util.ClickButtonsByCss(Driver, "#divMudancaCurso input:nth-child(2)");

            //Duração regular do curso MARCAR A CHECKBOX (SE APARECER)
            if (Driver.FindElement(By.Name("checkNaoAlteraCurso[]")).Displayed)
            {
                Util.ClickButtonsByName(Driver, "checkNaoAlteraCurso[]");
            }

            //checa se existe e escreve a justificativa
            Justificativa(justificativaAluno);

            //Duração regular do curso MARCAR A CHECKBOX (SE APARECER)
            if (Driver.FindElement(By.Name("checkNaoAlteraCurso[]")).Displayed)
            {
                if (!Driver.FindElement(By.Name("checkNaoAlteraCurso[]")).Selected)
                {
                    Util.ClickButtonsByName(Driver, "checkNaoAlteraCurso[]");
                }
            }
        }

        /// <summary>
        /// Preenche no padrão de aproveitamento a baixo de 75%.
        /// </summary>
        static String CasoSemAproveitamento(TOAluno aluno)
        {
            //tratamento do erro de caso a pessoa estiver na 3º reconsideração na realidade
            if (!Driver.FindElement(By.Id("divRejeicaoAutomatica")).Displayed)
            {
                //A CPSA irá liberar o aditamento nesta situação? SIM
                Util.ClickButtonsByCss(Driver, "span:nth-child(4) > input:nth-child(2)");

                //Justificativa: ESCREVER "Nrº reconsideração" OU ALGO DO TIPO
                Util.ClickAndWriteByName(Driver, "justificativa", aluno.HistoricoAproveitamento);

                //O estudante está regularmente matriculado? SIM
                Util.ClickButtonsByCss(Driver, "#divRegularidadeMatricula input:nth-child(3)");

                //O estudante possui benefício simultâneo de FIES e de bolsa ProUni em local de oferta ou curso distinto? NAO
                Util.ClickButtonsByXpath(Driver, "(//input[@name=\'beneficio\'])[1]");

                //O prazo de duração regular do curso encontra-se vigente? SIM
                Util.ClickButtonsByCss(Driver, "#divPrazoCurso input:nth-child(3)");

                //O estudante transferiu de curso mais de uma vez nessa IES? NAO
                Util.ClickButtonsByCss(Driver, "#divMudancaCurso input:nth-child(2)");

                //Duração regular do curso MARCAR A CHECKBOX SE APARECER
                if (Driver.FindElement(By.Name("checkNaoAlteraCurso[]")).Displayed)
                {
                    Util.ClickButtonsByName(Driver, "checkNaoAlteraCurso[]");
                }

                //checa se existe e escreve a justificativa
                Justificativa(aluno.Justificativa);

                //Duração regular do curso MARCAR A CHECKBOX (SE APARECER)
                if (Driver.FindElement(By.Name("checkNaoAlteraCurso[]")).Displayed)
                {
                    if (!Driver.FindElement(By.Name("checkNaoAlteraCurso[]")).Selected)
                    {
                        Util.ClickButtonsByName(Driver, "checkNaoAlteraCurso[]");
                    }
                }

                return aluno.Conclusao;
            }
            return CasoExcessoReprovacao(aluno.Justificativa);
        }

        /// <summary>
        /// Preenche no padrão de excesso de reprovações.
        /// </summary>
        /// <returns>Mensagem de Status</returns>
        static String CasoExcessoReprovacao(string justificativaAluno)
        {
            //A CPSA irá liberar o aditamento nesta situação? NAO
            Util.ClickButtonsByCss(Driver, "span:nth-child(4) > input:nth-child(1)");
            //checa se existe e escreve a justificativa
            Justificativa(justificativaAluno);

            return "Rejeitou execesso de reprovação";
        }

        /// <summary>
        /// Retorna a página de login.
        /// </summary>
        /// <returns>Mensagem de status de aditamento.</returns>
        static String GoBackToLogin()
        {
            if (!Util.IsFormFillingFiesVelho(Driver))
            {
                IWebElement listaMensagem = Driver.FindElement(By.Id("lista-mensageiro-erros"));
                if (listaMensagem.Text.Contains(S0005))
                {
                    //fazer logout da pag
                    //Util.ClickButtonsByXpath(Driver, "//a[contains(text(),'Sair')]");
                    return "Aditamento Realizado";
                }
            }
            return "Aditamento rejeitado ou incorreto";
        }

        /// <summary>
        /// Checa se possui um dos erros a serem tratados. E Popula variavel global do erro 0329.
        /// </summary>
        /// <param name="pageSource">Page Source a ser analisado.</param>
        /// <returns>Boolean</returns>
        static Boolean PossuiErros(String pageSource)
        {
            if (pageSource.Contains("lista-mensageiro-erros"))
            {
                PossuiErro0329 = pageSource.Contains(ERRO0329);
                PossuiErroSiteCaiu = pageSource.Contains("Acesso restrito.");
                PossuiErroSuperior = pageSource.Contains(ERRO0019);
                return pageSource.Contains(ERRO0336) || PossuiErro0329 || PossuiErroSiteCaiu || PossuiErroSuperior;
            }
            return false;
        }
        #endregion


        public static void BuscarDocumento(List<TOAluno> alunos, string campusAtual, string semestre, string tipoRelatorio)
        {
            System.Threading.Thread.Sleep(1000);
            Util.ClickButtonsByCss(Driver, "div:nth-child(3) > ul > .menu-button:nth-child(3) > a");

            string selRelatorio = SelecionarTipoRelatorio(tipoRelatorio);

            for (int i = alunos.Count - 1; i >= 0; i--)
            {
                DocumentoAlunoSucesso(alunos[i], campusAtual, semestre, tipoRelatorio);

                if (!"Não Feito".Equals(alunos[i].Conclusao))
                {
                    WaitinLoading();
                    Util.ClickDropDown(Driver, "id", "co_finalidade_aditamento", selRelatorio);
                    alunos.RemoveAt(i);
                }
            }
        }

        public static void BuscarStatusAluno(List<TOAluno> alunos, string campusAtual, string semestre, string tipoRelatorio)
        {
            System.Threading.Thread.Sleep(1000);
            Util.ClickButtonsByCss(Driver, "div:nth-child(3) > ul > .menu-button:nth-child(3) > a");

            string selRelatorio = SelecionarTipoRelatorio(tipoRelatorio);

            for (int i = alunos.Count - 1; i >= 0; i--)
            {
                BuscarStatus(alunos[i], campusAtual, "[Todos]", tipoRelatorio);

                if (!"Não Feito".Equals(alunos[i].Conclusao))
                {
                    WaitinLoading();
                    Util.ClickDropDown(Driver, "id", "co_finalidade_aditamento", selRelatorio);
                    alunos.RemoveAt(i);
                }
            }
        }
        public static void BuscarStatus(TOAluno aluno, string campusAtual, string semestre, string tipoRelatorio)
        {
            WaitinLoading();
            Util.ClickAndWriteById(Driver, "cpf", aluno.Cpf);

            Util.ClickDropDown(Driver, "id", "coSemestreAditamento", semestre);

            Util.ClickButtonsById(Driver, "consultar");
            listaEncontrada = ListaParaCSV();
        }

        private static string SelecionarTipoRelatorio(string tipoRelatorio)
        {
            switch (tipoRelatorio)
            {
                case "DRM":
                    Util.ClickDropDown(Driver, "id", "co_finalidade_aditamento", "Aditamento de Renovação");
                    return "Aditamento de Renovação";
                    break;
                case "DRT":
                    Util.ClickDropDown(Driver, "id", "co_finalidade_aditamento", "Aditamento de Transferência");
                    return "Aditamento de Transferência";
                    break;
                case "DRD":
                    Util.ClickDropDown(Driver, "id", "co_finalidade_aditamento", "Aditamento de Dilatação");
                    return "Aditamento de Dilatação";
                    break;
                case "Suspensao":
                    Util.ClickDropDown(Driver, "id", "co_finalidade_aditamento", "Suspensão");
                    return "Suspensão";
                    break;

                default:
                    return string.Empty;
                    break;
            }
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

        public static void DocumentoAlunoSucesso(TOAluno aluno, string campusAtual, string semestre, string tipoRelatorio)
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
                        string conclusao = BaixarDocumento(aluno, semestre, tipoRelatorio);
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

                #region ComentariosInuteis
                /*                else if (lista.Text.Contains("Pendente de validação"))
                                {
                                    EditarConclusaoAluno(aluno, "Pendente de validação", campusAtual);
                                }
                                else if (lista.Text.Contains("Não iniciado pela CPSA"))
                                {
                                    EditarConclusaoAluno(aluno, "Não iniciado pela CPSA", campusAtual);
                                }
                                else if (lista.Text.Contains("Rejeitado pelo estudante"))
                                {
                                    EditarConclusaoAluno(aluno, "Rejeitado pelo estudante", campusAtual);
                                }
                                else if (lista.Text.Contains("Cancelado por decurso de prazo do banco"))
                                {
                                    EditarConclusaoAluno(aluno, "Cancelado por decurso de prazo do banco", campusAtual);
                                }
                                else if (lista.Text.Contains("Prazo para Aditamento Expirado"))
                                {
                                    EditarConclusaoAluno(aluno, "Prazo para Aditamento Expirado", campusAtual);
                                }
                                else if (lista.Text.Contains("Cancelado por decurso de prazo do estudante"))
                                {
                                    EditarConclusaoAluno(aluno, "Cancelado por decurso de prazo do estudante", campusAtual);
                                }
                                else if (lista.Text.Contains("Rejeitado pela CPSA"))
                                {
                                    EditarConclusaoAluno(aluno, "Rejeitado pela CPSA", campusAtual);
                                }
                                else if (lista.Text.Contains("Reaberto para correção"))
                                {
                                    EditarConclusaoAluno(aluno, "Reaberto para correção", campusAtual);
                                }
                                else if (lista.Text.Contains("Aditamento Pendente de Correção pelo Banco"))
                                {
                                    EditarConclusaoAluno(aluno, "Aditamento Pendente de Correção pelo Banco", campusAtual);
                                }
                                else if (lista.Text.Contains("Aditamento preliminar"))
                                {
                                    EditarConclusaoAluno(aluno, "Aditamento preliminar", campusAtual);
                                }
                                else if (lista.Text.Contains("Validado para contratação"))
                                {
                                    EditarConclusaoAluno(aluno, "Validado para contratação", campusAtual);
                                }
                                else if (lista.Text.Contains("Enviado ao banco"))
                                {
                                    EditarConclusaoAluno(aluno, "Enviado ao banco", campusAtual);
                                }
                                else if (lista.Text.Contains("Recebido pelo banco"))
                                {
                                    EditarConclusaoAluno(aluno, "Recebido pelo banco", campusAtual);
                                }
                                else if (lista.Text.Contains("Contratado"))
                                {
                                    EditarConclusaoAluno(aluno, "Contratado", campusAtual);
                                }
                                else
                                {
                                    EditarConclusaoAluno(aluno, "Aluno não dispónivel para edição por motivo desconhecido", campusAtual);
                                }*/
                #endregion
            }
            //EditarConclusaoAluno(aluno, "Pendente", campusAtual);
        }

        private static bool ListaParaCSV(string idTabela = "lista")
        {
            if (Driver.PageSource.Contains("Lista de Aditamentos") || Driver.PageSource.Contains("sorterdocuments"))
            {
                IWebElement elementoTabela = Driver.FindElement(By.Id(idTabela));
                List<IWebElement> cabecalhos = Driver.FindElements(By.TagName("th")).ToList();
                List<IWebElement> dados = elementoTabela.FindElements(By.TagName("td")).ToList();
                string arquivo = "Tabela.csv";
                if (File.Exists(arquivo))
                {
                    File.Delete(arquivo);
                }
                for (int i = 0; i < cabecalhos.Count(); i++)
                {
                    using (StreamWriter t = new StreamWriter(arquivo, true, UTF8Encoding.UTF8))
                    {
                        if (cabecalhos[i].Text != " ")
                        {
                            if (i == cabecalhos.Count() - 1)
                            {
                                t.Write(cabecalhos[i].Text);
                                t.Write("\n");
                            }
                            else
                            {
                                t.Write(cabecalhos[i].Text + ";");
                            }
                        }

                    }

                }
                int contador = 0;
                for (int i = 0; i < dados.Count(); i++)
                {
                    using (StreamWriter t = new StreamWriter(arquivo, true, UTF8Encoding.UTF8))
                    {
                        if (dados[i].Text != "")
                        {
                            if (contador == cabecalhos.Count() - 1)
                            {
                                t.Write(dados[i].Text);
                                t.Write("\n");
                            }
                            else
                            {
                                t.Write(dados[i].Text + ";");
                            }
                        }


                    }
                    if (contador == cabecalhos.Count() - 1)
                    {
                        contador = 0;
                    }
                    else
                    {

                        contador++;
                    }


                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string BaixarDocumento(TOAluno aluno, string semestre, string tipoRelatorio)
        {
            string text = Driver.PageSource;
            if (Driver.PageSource.Contains("Nome completo:</strong>") == true)
            {
                text = text.Split(new string[] { "Nome completo:</strong>" }, StringSplitOptions.None)[1];
            }
            else
            {
                text = text.Split(new string[] { "Nome Completo:</strong>" }, StringSplitOptions.None)[1];
            }
            text = text.Split(new string[] { "</span>" }, StringSplitOptions.None)[0];
            aluno.Nome = text;
            if (tipoRelatorio == "DRM")
            {
                Util.ClickButtonsById(Driver, "imprimirDrm");
            }
            else
            {
                Util.ClickButtonsById(Driver, "imprimir");
            }

            String userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            String downloadFolder = System.IO.Path.Combine(userRoot, "Downloads");
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
            string complemento = String.Empty;
            Util.CreateDirectory("Temp");
            System.IO.DirectoryInfo di = new DirectoryInfo("Temp");
            foreach (var item in di.GetFiles())
            {
                item.Delete();
            }
            if (tipoRelatorio == "DRM")
            {
                String diretorioDRM = downloadFolder + "\\DRM FIES Legado";
                String diretorioSimplificado = diretorioDRM + "\\Simplificados";
                String diretorioNaoSimplificado = diretorioDRM + "\\Nao-Simplificados";

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
                String diretorio = downloadFolder + "\\" + tipoRelatorio + " FIES Legado";
                Util.CreateDirectory(diretorio);
                diretorioDestino = diretorio;
                complemento = string.Empty;
            }

            string tempSemestre = semestre.Replace("/", "-");
            //File.Move(myFile.FullName, diretorioDestino + "\\" + aluno.Nome + "_" + aluno.Cpf + "_" + tempSemestre + "_" + tipoRelatorio + ".zip");
            File.Copy(myFile.FullName, diretorioDestino + "\\" + aluno.Nome + "_" + aluno.Cpf + "_" + tempSemestre + "_" + tipoRelatorio + ".zip", true);
            File.Delete(myFile.FullName);

            if (emLote == false)
            {
                System.IO.DirectoryInfo diTemp = new DirectoryInfo("Temp");
                Process.Start(diTemp.GetFiles()[0].FullName);
            }
            Util.ClickButtonsById(Driver, "voltar");

            string conclusao = String.Format("{0} - {1}", tipoRelatorio + " Baixado", complemento);
            aluno.Extraido = complemento;
            return conclusao;
        }

        public static void MetodoDRI(List<TOAluno> alunos, TOLogin login, bool baixar, string situacaoDRI)
        {
            System.Threading.Thread.Sleep(1000);
            Util.ClickButtonsByXpath(Driver, "//a[contains(text(),'Validação pela CPSA Fies')]");


            Util.ClickDropDown(Driver, "id", "co_situacao_inscricao", situacaoDRI);

            //cpf do aluno atual
            for (int i = alunos.Count - 1; i >= 0; i--)
            {
                if (!Dados.DRIExists(alunos[i].Cpf) || baixar == true)
                {

                    //if (alunos[i].NumCampusAtual.Equals(login.Numero))
                    //{
                    WaitPageToLoad();
                    Util.ClickAndWriteById(Driver, "nu_cpf", alunos[i].Cpf);

                    Util.ClickButtonsById(Driver, "consulta");
                    WaitPageToLoad();
                    //IWait<IWebDriver> wait = new OpenQA.Selenium.Support.UI.WebDriverWait(Driver, TimeSpan.FromSeconds(30.00));

                    //wait.Until(driver1 => ((IJavaScriptExecutor)Driver).ExecuteScript("return document.readyState").Equals("complete"));
                    //se tiver resultado 
                    if (Driver.PageSource.Contains("sorterdocuments"))
                    {
                        Util.ClickButtonsByCss(Driver, ".even:nth-child(1) img");

                        if (!Driver.PageSource.Contains("Voltar para a página principal"))
                        {
                            if (!Driver.PageSource.Contains("Inscrição incompleta."))
                            {
                                if (Driver.PageSource.Contains("Imprimir DRI"))
                                {
                                    if (baixar)
                                    {
                                        baixarDRI(alunos[i], login.Campus);
                                    }
                                    else
                                    {
                                        salvarDRIAlunos(alunos[i], login.Campus);
                                    }
                                    alunos.RemoveAt(i);
                                }
                                else
                                {
                                    if (RoboForm.versaoRobo == "operacoesFinanceiras")
                                    {
                                        ErroInscricaoDRI(alunos, i, "Impossivel baixar");
                                    }
                                }
                            }
                            else
                            {
                                if (RoboForm.versaoRobo == "operacoesFinanceiras")
                                {
                                    ErroInscricaoDRI(alunos, i, "Inscrição incompleta");
                                }
                            }
                        }
                        else
                        {
                            ErroEstranhoDRI(alunos[i]);
                            Dados.UpdateAluno(alunos[i]);
                            alunos.RemoveAt(i);
                        }
                        //}
                        //  else
                        /*{
                            int num = int.Parse(alunos[i].NumCampusAtual) + 1;
                            alunos[i].NumCampusAtual = num.ToString();
                            Dados.UpdateAluno(alunos[i]);
                        */
                    }
                    //}
                }
                else
                {
                    alunos[i].Conclusao = "DRI Baixada Anteriormente";
                    Dados.UpdateAluno(alunos[i]);
                    alunos.RemoveAt(i);
                }
            }
        }

        public static void BuscarStatusDRI(List<TOAluno> alunos, TOLogin login)
        {
            System.Threading.Thread.Sleep(1000);
            Util.ClickButtonsByXpath(Driver, "//a[contains(text(),'Validação pela CPSA Fies')]");
            IWebElement t = Driver.FindElement(By.Id("co_situacao_inscricao"));
            string[] temp = t.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            for (int i = 1; i < temp.Length; i++)
            {
                Util.ClickDropDown(Driver, "id", "co_situacao_inscricao", temp[i]);
                Util.ClickAndWriteById(Driver, "nu_cpf", alunos[0].Cpf);
                Util.ClickButtonsById(Driver, "consulta");
                //System.Threading.Thread.Sleep(100);
                if (Driver.PageSource.Contains("sorterdocuments") == true)
                {
                    listaEncontrada = ListaParaCSV("sorterdocuments");
                    return;
                }
            }
        }

        static void baixarDRI(TOAluno aluno, string loginCampus)
        {

            Util.ScrollToElementByID(Driver, "imprimir_dri");
            Util.ClickButtonsById(Driver, "imprimir_dri");
            if (!Driver.PageSource.Contains("Voltar para a página principal"))
            {
                String userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
                String downloadFolder = System.IO.Path.Combine(userRoot, "Downloads");
                DirectoryInfo directory = new DirectoryInfo(downloadFolder);

                FileInfo myFile = directory.GetFiles().OrderByDescending(f => f.LastWriteTime).First();
                bool downloading = true;
                while (downloading)
                {
                    System.Threading.Thread.Sleep(1000);
                    myFile = directory.GetFiles().OrderByDescending(f => f.LastWriteTime).First();
                    downloading = myFile.Name.EndsWith(".crdownload");
                }
/*                Util.CreateDirectory("Temp");
                System.IO.DirectoryInfo di = new DirectoryInfo("Temp");
                foreach (var item in di.GetFiles())
                {
                    item.Delete();
                }
                using (ZipArchive archive = new ZipArchive(File.OpenRead(myFile.FullName), ZipArchiveMode.Read))
                {
                    archive.ExtractToDirectory("Temp");
                }
                if (emLote == false)
                {
                    System.IO.DirectoryInfo diTemp = new DirectoryInfo("Temp");
                    Process.Start(diTemp.GetFiles()[0].FullName);
                }
                TODRI dri = new TODRI();
                //apaguei pq aluno nao tem mais nome, tirar nomne da dri
                dri.Nome = aluno.Nome;
                dri.DRI = myFile.Name.Split('-')[2];
                dri.DRI = nroDRI;
                dri.CampusAditado = loginCampus;

                if (RoboForm.versaoRobo == "operacoesFinanceiras")
                {
                    Dados.InsertDRI(dri);
                }*/
                //Bloaqueado até gerar nossa versão de baixar DRIs

                String diretorioDRI = downloadFolder + "\\DRI";

                Util.CreateDirectory(diretorioDRI);

                File.Move(myFile.FullName, diretorioDRI + "\\" + aluno.Cpf + "DRI.zip");
                aluno.Conclusao = "DRI Baixada";
                Dados.UpdateAluno(aluno);
                Util.ClickButtonsById(Driver, "voltar");
            }

        }

        static void salvarDRIAlunos(TOAluno aluno, string loginCampus)
        {
            System.Threading.Thread.Sleep(500);

            if (!Driver.PageSource.Contains("Voltar para a página principal"))
            {
                var coInscricao = Driver.FindElement(By.Id("co_inscricao"));
                string nroDRI = coInscricao.GetAttribute("value");
                // String userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
                // String downloadFolder = System.IO.Path.Combine(userRoot, "Downloads");
                // DirectoryInfo directory = new DirectoryInfo(downloadFolder);
                //
                // FileInfo myFile = directory.GetFiles().OrderByDescending(f => f.LastWriteTime).First();
                // bool downloading = true;
                // while (downloading)
                // {
                //     System.Threading.Thread.Sleep(1000);
                //     myFile = directory.GetFiles().OrderByDescending(f => f.LastWriteTime).First();
                //     downloading = myFile.Name.EndsWith(".crdownload");
                // }
                // Util.CreateDirectory("Temp");
                // System.IO.DirectoryInfo di = new DirectoryInfo("Temp");
                // foreach (var item in di.GetFiles())
                // {
                //     item.Delete();
                // }
                // using (ZipArchive archive = new ZipArchive(File.OpenRead(myFile.FullName), ZipArchiveMode.Read))
                // {
                //     archive.ExtractToDirectory("Temp");
                // }
                // if (emLote == false)
                // {
                //     System.IO.DirectoryInfo diTemp = new DirectoryInfo("Temp");
                //     Process.Start(diTemp.GetFiles()[0].FullName);
                // }
                TODRI dri = new TODRI();
                //apaguei pq aluno nao tem mais nome, tirar nomne da dri
                //dri.Nome = aluno.Nome;
                //dri.DRI = myFile.Name.Split('-')[2];
                dri.DRI = nroDRI;
                dri.Cpf = aluno.Cpf;
                dri.CampusAditado = loginCampus;

                if (RoboForm.versaoRobo == "operacoesFinanceiras")
                {
                    Dados.InsertDRI(dri);
                }
                //Bloaqueado até gerar nossa versão de baixar DRIs

                //  String diretorioDRI = downloadFolder + "\\DRI";
                //
                //  Util.CreateDirectory(diretorioDRI);
                //
                //  File.Move(myFile.FullName, diretorioDRI + "\\DRI_NR_" + dri.DRI + "_CPF_" + dri.Cpf + ".zip");
                aluno.Conclusao = "DRI Baixada";
                Dados.UpdateAluno(aluno);
                Util.ClickButtonsById(Driver, "voltar");
            }
            else
            {
                ErroEstranhoDRI(aluno);
            }
        }

        static void ErroEstranhoDRI(TOAluno aluno)
        {
            aluno.Conclusao = "Erro estranho";

            Util.ClickButtonsByXpath(Driver, "//a[contains(text(),\'Voltar para a página principal\')]");

            Util.ClickButtonsByXpath(Driver, "//a[contains(text(),'Validação pela CPSA Fies')]");
            Util.ClickDropDown(Driver, "id", "co_situacao_inscricao", "Contratado");
        }

        static void ErroInscricaoDRI(List<TOAluno> alunos, int i, string erro)
        {
            alunos[i].Conclusao = erro;
            Dados.UpdateAluno(alunos[i]);
            alunos.RemoveAt(i);

            Util.ClickButtonsById(Driver, "voltar");
            System.Threading.Thread.Sleep(500);
            Util.ClickButtonsByXpath(Driver, "//a[contains(text(),'Validação pela CPSA Fies')]");
            Util.ClickDropDown(Driver, "id", "co_situacao_inscricao", "Contratado");
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

        static void EditarConclusaoAluno(TOAluno aluno, string conclusao, string campusAditado = null)
        {
            aluno.Conclusao = conclusao;
            aluno.HorarioConclusao = string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now);

            if (campusAditado != null)
            {
                aluno.CampusAditado = campusAditado;
            }

            Dados.UpdateAluno(aluno);
        }

        static void WaitPageToLoad()
        {
            string result = (string)((IJavaScriptExecutor)Driver).ExecuteScript("return document.readyState");
            while (result != "complete")
            {
                System.Threading.Thread.Sleep(100);
                result = (string)((IJavaScriptExecutor)Driver).ExecuteScript("return document.readyState");
            }
        }
    }
}