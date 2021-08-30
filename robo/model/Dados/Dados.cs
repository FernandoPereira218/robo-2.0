using System;
using System.Collections.Generic;
using System.Linq;
using robo.pgm;
using robo;
using System.Windows.Forms;

//using ExcelManager;

namespace Robo
{
    public static class Dados
    {
        private static readonly Dictionary<string, string> insertTOAluno = new Dictionary<string, string>()
        {
            {"Cpf", "Cpf" },
            {"Nome", "Nome"},
            {"Campus", "Campus"},
            {"AproveitamentoAtual", "AproveitamentoAtual"},
            {"HistoricoAproveitamento", "HistoricoAproveitamento"},
            {"ReceitaLiquida", "ReceitaLiquida"},
            {"ReceitaBruta", "ReceitaBruta"},
            {"ReceitaFies", "ReceitaFies"},
            {"Tipo", "Tipo"},
            {"Conclusao", "Conclusao"},
            {"CampusAditado", "CampusAditado"},
            {"ValorAditado", "ValorAditado"},
            {"ValorAditadoFinanciamento", "ValorAditadoFinanciamento"},
            {"ValorPagoRecursoEstudante", "ValorPagoRecursoEstudante"},
            {"HorarioConclusao", "HorarioConclusao"},
            {"DescontoLiberalidade", "DescontoLiberalidade"},
            {"Extraido", "Extraido"},
            {"Justificativa", "Justificativa"}
        };

        private static readonly Dictionary<string, string> insertTOAlunoInf = new Dictionary<string, string>()
        {
            {"Cpf", "Cpf"},
            {"Nome", "Nome"},
            {"Campus", "Campus"},
            {"Conclusao", "Conclusao"}
        };

        private static readonly Dictionary<string, string> insertAlunoConsultaNovoTO = new Dictionary<string, string>()
        {
            {"Cpf", "Cpf"},
            {"SemestreAno", "SemestreAno"},
            {"Finalidade", "Finalidade"},
            {"Situacao", "Situacao"},
            {"Tipo", "Tipo"},
            {"ProUni", "ProUni"},
            {"DataInclusao", "DataInclusao"},
            {"DataConclusao", "DataConclusao"},
            {"HorarioConclusao", "HorarioConclusao"}
        };

        private static readonly Dictionary<string, string> updateAlunoInf = new Dictionary<string, string>()
        {
            {"Cpf", "Cpf" },
            {"Curso", "Curso" },
            {"HorarioConclusao", "HorarioConclusao" },
            {"SemestreAditar", "SemestreAditar" },
            {"DuracaoRegular", "DuracaoRegular" },
            {"TotalDeSemestresSuspensos", "TotalDeSemestresSuspensos"},
            {"TotalDeSemestresDilatados", "TotalDeSemestresDilatados"},
            {"TotalDeSemestresConcluidos", "TotalDeSemestresConcluidos"},
            {"SemestreSerCursadoPeloEstudante", "SemestreSerCursadoPeloEstudante" },
            {"TotalDeSemestresJaFinanciados", "TotalDeSemestresJaFinanciados"},
            {"PercentualDeFinanciamentoSolicitado", "PercentualDeFinanciamentoSolicitado" },
            {"GradeAtualComDesconto", "GradeAtualComDesconto" },
            {"GradeAtualFinanciadoFIES", "GradeAtualFinanciadoFIES" },
            {"GradeAtualCoparticipacao", "GradeAtualCoparticipacao" },
            {"Conclusao", "Conclusao" }
        };
        //SELECTS
        public static List<TOSemestre> SelectSemestre()
        {
            return Database.Acess.SelectAll<TOSemestre>("SEMESTRES");
        }
        public static List<TOLogin> SelectLogins()
        {
            return Database.Acess.SelectAll<TOLogin>("LOGIN", "ID", true);
        }
        public static List<TOUsuario> SelectUsuarios()
        {
            return Database.Acess.SelectAll<TOUsuario>("USUARIO");
        }
        public static List<TOUsuario> SelectUsuarioWhereIES(string IES)
        {
            return Database.Acess.SelectWhere<TOUsuario>("USUARIO", "IES", IES);
        }
        public static List<TOAluno> SelectAlunos()
        {
            return Database.Acess.SelectAll<TOAluno>("ALUNO");
        }
        public static List<TOAluno> SelectAlunoWhere(string plataforma)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("Tipo", plataforma);
            dic.Add("Conclusao", "Não Feito");
            return Database.Acess.SelectWhere<TOAluno>("ALUNO", dic, "and");
        }
        public static List<TOMenus> SelectMenus()
        {
            return Database.Acess.SelectAll<TOMenus>("MENUS");
        }
        public static List<TOMenus> SelectMenuWhere(string plataforma)
        {
            //Duas keys iguais do dictionary dá erro, por isso o espaço
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("Modalidade", plataforma);
            dic.Add("Modalidade ", "");
            List<TOMenus> menusGeral = Database.Acess.SelectWhere<TOMenus>("MENUS", dic, "or ");
            return menusGeral;

        }
        public static List<string> SelectLoginTOIES(string IES, string plataforma)
        {
            List<TOLogin> listlogin;
            if (IES == "TODOS")
            {
                listlogin = Database.Acess.SelectWhere<TOLogin>("LOGIN", "Plataforma", plataforma);
            }
            else
            {
                listlogin = Database.Acess.SelectWhere<TOLogin>("LOGIN", "Faculdade", "Plataforma", IES, plataforma);
            }
            List<string> listCampus = new List<string>();
            listCampus.Add("");
            foreach (var item in listlogin)
            {
                listCampus.Add(item.Campus);
            }
            return listCampus;
        }
        public static List<TOLogin> SelectLoginPorIESePlataforma(string IES, string plataforma, string campus, bool admin)
        {
            List<TOLogin> listlogin;

            if (IES == "TODOS")
            {
                listlogin = Database.Acess.SelectWhere<TOLogin>("LOGIN", "Plataforma", plataforma);
            }
            else
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("Faculdade", IES);
                dic.Add("Plataforma", plataforma);
                dic.Add("Admin", "Não");
                if (plataforma == "FIES Legado" && campus != string.Empty)
                {
                    dic.Add("Campus", campus);
                }
                listlogin = Database.Acess.SelectWhere<TOLogin>("LOGIN", dic, "and");
            }
            if (admin == true)
            {
                listlogin = Database.Acess.SelectWhere<TOLogin>("LOGIN", "Faculdade", "Admin", IES, "Sim");
            }
            return listlogin;
        }

        //DELETES
        public static void DeleteLogin(TOLogin login)
        {
            List<TOLogin> loginList = new List<TOLogin>();
            loginList.Add(login);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("Usuario", "Usuario");
            Database.Acess.Delete<TOLogin>("LOGIN", loginList, dic);
        }
        public static void DeleteUsuario(TOUsuario usuario)
        {
            List<TOUsuario> usuarioList = new List<TOUsuario>();
            usuarioList.Add(usuario);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("Usuario", "Usuario");
            Database.Acess.Delete<TOUsuario>("USUARIO", usuarioList, dic);
        }
        public static void DeleteTodosAlunos()
        {
            Database.Acess.DeleteAll("ALUNO");
        }

        //UPDATES
        public static void UpdateAluno(TOAluno aluno)
        {
            Dictionary<string, string> columnAndProperty = new Dictionary<string, string>();
            columnAndProperty.Add("Cpf", "Cpf");
            columnAndProperty.Add("Nome", "Nome");
            //columnAndProperty.Add("Curso", "Curso");
            columnAndProperty.Add("Campus", "Campus");
            columnAndProperty.Add("AproveitamentoAtual", "AproveitamentoAtual");
            columnAndProperty.Add("HistoricoAproveitamento", "HistoricoAproveitamento");
            columnAndProperty.Add("ReceitaLiquida", "ReceitaLiquida");
            columnAndProperty.Add("ReceitaBruta", "ReceitaBruta");
            columnAndProperty.Add("ReceitaFies", "ReceitaFies");
            columnAndProperty.Add("Tipo", "Tipo");
            columnAndProperty.Add("Conclusao", "Conclusao");
            columnAndProperty.Add("CampusAditado", "CampusAditado");
            columnAndProperty.Add("ValorAditado", "ValorAditado");
            columnAndProperty.Add("ValorAditadoFinanciamento", "ValorAditadoFinanciamento");
            columnAndProperty.Add("ValorPagoRecursoEstudante", "ValorPagoRecursoEstudante");
            columnAndProperty.Add("HorarioConclusao", "HorarioConclusao");
            columnAndProperty.Add("Extraido", "Extraido");
            Database.Acess.Update<TOAluno>("ALUNO", columnAndProperty, aluno, "Cpf", "Cpf");
        }
        public static void UpdateLogin(TOLogin login)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("Usuario", "Usuario");
            dic.Add("Senha", "Senha");
            dic.Add("Campus", "Campus");
            dic.Add("Plataforma", "Plataforma");
            dic.Add("Faculdade", "Faculdade");
            dic.Add("ID", "ID");
            Database.Acess.Update("LOGIN", dic, login, "ID", "ID");
        }
        public static void UpdateUsuario(TOUsuario usuario)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("Usuario", "Usuario");
            dic.Add("Senha", "Senha");
            dic.Add("Permissao", "Permissao");
            dic.Add("IES", "IES");
            Database.Acess.Update("USUARIO", dic, usuario, "Usuario", "Usuario");
        }
        public static void UpdateAluno(TOAluno aluno, string tipoAluno)
        {
            if (tipoAluno == "ALUNO")
            {
                Database.Acess.Update<TOAluno>("ALUNO", insertTOAluno, (TOAluno)aluno, "Cpf", "Cpf");
            }
            if (tipoAluno == "ALUNOINF")
            {
                Database.Acess.Update<TOAluno>("ALUNO", updateAlunoInf, (TOAluno)aluno, "Cpf", "Cpf");
            }
        }

        //INSERTS
        public static void InsertAluno(TOAluno aluno)
        {
            if (aluno is TOAluno)
            {
                List<TOAluno> alunos = new List<TOAluno>();
                alunos.Add(aluno as TOAluno);

                Database.Acess.InsertClassInBd("ALUNO", insertTOAluno, alunos);
            }
        }
        public static void InsertLogin(TOLogin login)
        {
            List<TOLogin> listLogin = new List<TOLogin>();
            listLogin.Add(login);

            Database.Acess.InsertClassInBd<TOLogin>("LOGIN", listLogin);
        }
        public static void InsertUsuario(TOUsuario usuario)
        {
            List<TOUsuario> listUsuario = new List<TOUsuario>();
            listUsuario.Add(usuario);
            Database.Acess.InsertClassInBd<TOUsuario>("USUARIO", listUsuario);
        }
        public static void InsertDRI(TODRI dri)
        {
            List<TODRI> listTODRI = new List<TODRI>();
            listTODRI.Add(dri);

            Database.Acess.InsertClassInBd<TODRI>("DRI", listTODRI);
        }
        public static void InsertSemestre(TOSemestre semestre)
        {
            Dictionary<string, string> dicSemestre = new Dictionary<string, string>();
            dicSemestre.Add("Semestre", "Semestre");
            List<TOSemestre> listTOSemestre = new List<TOSemestre>();
            listTOSemestre.Add(semestre);
            Database.Acess.InsertClassInBd("SEMESTRES", dicSemestre, listTOSemestre);
        }
        public static void ImportaAlunos(string filePath)
        {
            List<TOAluno> alunos = BuscarListaAlunos(filePath);
            AtualizarAlunosBD(alunos);
        }


        //COUNTS
        public static int CountAluno()
        {
            return Database.Acess.SelectCount("ALUNO");
        }
        public static int CountLogins()
        {
            return Database.Acess.SelectCount("LOGIN");
        }


        //CSV
        /// <summary>
        /// Busca todos alunos na planilha Excel.
        /// </summary>
        /// <param name="directory">Diretório da planilha excel.</param>
        /// <returns>Lista de alunos.</returns>
        public static List<TOAluno> BuscarListaAlunos(String directory)
        {
            try
            {
                TOAluno aluno = new TOAluno();
                List<String> headers = new List<String>();
                Dictionary<String, String> propriedades = new Dictionary<String, String>();

                propriedades.Add("CPF", "Cpf");
                propriedades.Add("NOME", "Nome");
                propriedades.Add("CAMPUS ADITADO", "Campus");
                propriedades.Add("APROVEITAMENTO ATUAL", "AproveitamentoAtual");
                propriedades.Add("HISTÓRICO DE APROVEITAMENTO", "HistoricoAproveitamento");
                propriedades.Add("RECEITA BRUTA", "ReceitaBruta");
                propriedades.Add("RECEITA LIQUIDA", "ReceitaLiquida");
                propriedades.Add("RECEITA FIES", "ReceitaFies");
                propriedades.Add("MODALIDADE FIES", "Tipo");
                propriedades.Add("TIPO", "Tipo");
                propriedades.Add("Desconto Liberalidade", "DescontoLiberalidade");
                propriedades.Add("Justificativa", "Justificativa");

                List<TOAluno> alunos = CSVManager.CSVManager.ImportCSV<TOAluno>(directory, propriedades);

                for (int i = alunos.Count() - 1; i >= 0; i--)
                {
                    if (alunos[i].Cpf == string.Empty)
                    {
                        alunos.RemoveAt(i);
                    }
                    else
                    {
                        TratarCpf(alunos[i]);
                    }
                }

                //return TratarCpf(alunos);
                return alunos;
            }
            catch (Exception e)
            {
                //throw new Exception("Erro na busca de dados no excel.");
                throw new Exception(e.Message);
            }
        }
        public static void ExportarInformacoes_CSV(string fileName)
        {
            if (fileName.Contains(".csv") == false)
            {
                fileName += ".csv";
            }
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

            List<TOAluno> alunoParaExportar = Database.Acess.SelectAll<TOAluno>("ALUNO");

            string[] arquivoSalvar = fileName.Split('\\');
            string[] diretorio = fileName.Split('\\');
            string nomeTemp = diretorio[diretorio.Length - 1];
            fileName = fileName.Replace(nomeTemp, string.Empty);
            CSVManager.CSVManager.ExportCSV<TOAluno>(fileName, nomeTemp, dic, alunoParaExportar);
        }

        //Tratamentos de valores
        public static void TratarCpf(TOAluno aluno)
        {
            aluno.Cpf = aluno.Cpf.Replace(".", "");
            aluno.Cpf = aluno.Cpf.Replace("-", "");

            while (aluno.Cpf.Length != 11)
            {
                aluno.Cpf = "0" + aluno.Cpf;
            }

        }
        /// <summary>
        /// Arredonda para duas casas decimais e remove caracteres desnecessários
        /// </summary>
        /// <param name="aluno"></param>
        /// <param name="chamadaInicial">se foi chamado no util (true) ou pelo tratamento de erro no formulario (false)</param>
        public static void TratarVirgulaReceitas(TOAluno aluno, bool chamadaInicial = true, float valorNovo = 0)
        {
            aluno.ReceitaBruta = FormatarReceitas(aluno.ReceitaBruta);
            aluno.ReceitaLiquida = FormatarReceitas(aluno.ReceitaLiquida);
            aluno.ReceitaFies = FormatarReceitas(aluno.ReceitaFies);
            aluno.ValorAditado = FormatarReceitas(aluno.ValorAditado);
        }
        public static void TratarCampusAluno(TOAluno aluno)
        {
            switch (aluno.Campus)
            {
                case "ZS":
                    aluno.Campus = "ZONA SUL";
                    break;
                case "Fapa_Fapa":
                    aluno.Campus = "FAPA-FAPA";
                    break;
                case "GL":
                    aluno.Campus = "GALERIA LUSA";
                    break;
                case "GV":
                    aluno.Campus = "GENERAL VITORINO";
                    break;
                case "Andradas":
                    aluno.Campus = "URUGUAI";
                    break;
                case "LF":
                    aluno.Campus = "LUIS AFONSO";
                    break;
                case "LA":
                    aluno.Campus = "LUIS AFONSO";
                    break;
                case "Fadergs_Fapa":
                    aluno.Campus = "MANOEL ELIAS";
                    break;
                case "Salgado Fillho":
                    aluno.Campus = "SALGADO FILHO";
                    break;
                default:
                    aluno.Campus = aluno.Campus.Trim();
                    aluno.Campus = aluno.Campus.ToUpper();
                    break;
            }
        }
        public static void TratarTipoFIES(TOAluno aluno)
        {
            if (aluno.Tipo.ToUpper().Contains("NOVO") == true)
            {
                aluno.Tipo = "FIES Novo";
            }
            else
            {
                aluno.Tipo = "FIES Legado";
            }
        }
        static string FormatarReceitas(string valor)
        {
            if (valor.Contains(","))
            {
                string[] split = valor.Split(new Char[] { ',' });
                if (split[1].Length == 1)
                {
                    valor += "0";
                }
            }
            else
            {
                valor += ",00";
            }
            return valor;
        }
        public static void TratarTextoReceitas(TOAluno aluno)
        {
            aluno.ReceitaBruta = aluno.ReceitaBruta.Replace("R$", "");
            aluno.ReceitaBruta = aluno.ReceitaBruta.Replace(" ", "");
            aluno.ReceitaLiquida = aluno.ReceitaLiquida.Replace("R$", "");
            aluno.ReceitaLiquida = aluno.ReceitaLiquida.Replace(" ", "");
            aluno.ReceitaFies = aluno.ReceitaFies.Replace("R$", "");
            aluno.ReceitaFies = aluno.ReceitaFies.Replace(" ", "");

            if (aluno.ReceitaBruta != string.Empty)
            {
                aluno.ReceitaBruta = Math.Round(Convert.ToDouble(aluno.ReceitaLiquida), 2).ToString();
            }
            if (aluno.ReceitaLiquida != string.Empty)
            {
                aluno.ReceitaLiquida = Math.Round(Convert.ToDouble(aluno.ReceitaLiquida), 2).ToString();
            }
            if (aluno.ReceitaFies != string.Empty)
            {
                aluno.ReceitaFies = Math.Round(Convert.ToDouble(aluno.ReceitaFies), 2).ToString();
            }
        }

        //Atualizar BD
        public static void AtualizarAlunosBD(List<TOAluno> alunos)
        {
            foreach (TOAluno aluno in alunos)
            {
                //if (CountAluno(aluno) > 0)
                //{
                //    UpdateAluno(aluno);
                //}
                //else
                //{
                //}
                InsertAluno(aluno);
            }
        }

        //Validar Login
        public static TOUsuario ValidateLogin(string user, string password)
        {
            string hashedPassword = Util.GetMD5(password);
            List<TOUsuario> temp = Database.Acess.SelectWhere<TOUsuario>("USUARIO", "Usuario", "Senha", user, hashedPassword);
            if (temp.Count != 0)
            {
                return temp[0];
            }
            return null;
        }
        public static TOUsuario ValidateSession(string user, string password)
        {
            List<TOUsuario> temp = Database.Acess.SelectWhere<TOUsuario>("USUARIO", "Usuario", "Senha", user, password);
            if (temp.Count != 0)
            {
                return temp[0];
            }
            return null;
        }

        //Verificações
        public static int verficarSemestre(string semestre)
        {
            return Database.Acess.SelectWhere<TOLogin>("SEMESTRES", "Semestre", semestre).Count;
        }
        public static bool DRIExists(string cpf)
        {
            List<TODRI> list = Database.Acess.SelectWhere<TODRI>("DRI", "Cpf", cpf);
            if (list.Count == 1)
            {
                return true;
            }
            else if (list.Count == 0)
            {
                return false;
            }
            throw new Exception("Existem dados repetidos na tabela das DRIs.");
        }
        public static TODRI GetDRI(string cpf)
        {
            List<TODRI> list = Database.Acess.SelectWhere<TODRI>("DRI", "Cpf", cpf);
            if (list.Count == 1)
            {
                return list[0];
            }
            throw new Exception("Existem dados repetidos na tabela das DRIs.");

        }
        public static void VerificaSemestre()
        {
            string verificarSemestre = Util.VerificaSemestreAtual();

            if (verficarSemestre(verificarSemestre) == 0)
            {
                TOSemestre semestre = new TOSemestre();
                semestre.Semestre = verificarSemestre;
                InsertSemestre(semestre);
            }
        }
        public static bool VerificaQtdAlunos()
        {
            int countAlunoTO = CountAluno();
            if (countAlunoTO > 0)
            {
                string mensagem = "Tem certeza que deseja excluir o banco de dados?";
                mensagem += "\n\nCertifique-se de já ter exportado antes para que nenhuma informação seja perdida!";

                if (MessageBox.Show(mensagem, "Limpar Banco de Dados", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    DeleteTodosAlunos();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}