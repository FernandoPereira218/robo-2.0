using System;
using System.Collections.Generic;
using System.Linq;
using robo.pgm;
using robo;
using System.Windows.Forms;
using LiteDB;
using System.Reflection;
using System.Linq.Expressions;
using System.IO;
using System.Text;
using CsvHelper;
using System.Globalization;

//using ExcelManager;

namespace Robo
{
    public static class Dados
    {
        private const string CAMINHO_BANCO = "data/bdbot1.db";


        //INSERTS
        public static void ImportaAlunos(string filePath)
        {
            List<TOAluno> alunos = BuscarListaAlunos(filePath);
            AtualizarAlunosBD(alunos);
            VerificarCPFDuplicado(alunos);

        }

        //CSV
        /// <summary>
        /// Busca todos alunos na planilha Excel.
        /// </summary>
        /// <param name="directory">Diretório da planilha excel.</param>
        /// <returns>Lista de alunos.</returns>
        public static List<TOAluno> BuscarListaAlunos(string directory)
        {
            try
            {

                List<TOAluno> alunos;
                using (var sr = new StreamReader(directory, Encoding.UTF7))
                {
                    using (var csv = new CsvReader(sr, CultureInfo.CurrentCulture))
                    {
                        var registros = csv.GetRecords<TOAluno>().ToList();
                        alunos = registros;
                    }
                }



                for (int i = alunos.Count() - 1; i >= 0; i--)
                {
                    alunos[i].Tipo = alunos[i].Tipo.Trim();
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

        //Tratamentos de valores
        public static void TratarCpf(TOAluno aluno)
        {
            aluno.Cpf = aluno.Cpf.Replace(".", "");
            aluno.Cpf = aluno.Cpf.Replace("-", "");

            while (aluno.Cpf.Length != 11)
            {
                aluno.Cpf = "0" + aluno.Cpf;
            }

            aluno.Tipo = aluno.Tipo.ToUpper();
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

            aluno.ValorDeRepasse = FormatarReceitas(aluno.ValorDeRepasse);
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
                    if (aluno.Campus != null)
                    {
                        aluno.Campus = aluno.Campus.Trim();
                        aluno.Campus = aluno.Campus.ToUpper();
                    }
                    break;
            }
        }
        public static void TratarTipoFIES(TOAluno aluno)
        {
            if (aluno.Tipo.ToUpper().Contains("NOVO") == true)
            {
                aluno.Tipo = "FIES Novo".ToUpper();
            }
            else
            {
                aluno.Tipo = "FIES Legado".ToUpper();
            }
        }
        public static string FormatarReceitas(string valor)
        {
            if (valor == null)
            {
                return null;
            }
            double valorDouble = Convert.ToDouble(valor);
            valorDouble = Math.Round(valorDouble, 2);
            valor = valorDouble.ToString("0.00");

            return valor;
        }
        public static void TratarTextoReceitas(TOAluno aluno)
        {
            if (aluno.ReceitaBruta == null && aluno.ReceitaLiquida == null &&
                aluno.ReceitaFies == null && aluno.ValorDeRepasse == null)
            {
                return;
            }

            if (aluno.ReceitaBruta != null)
            {
                aluno.ReceitaBruta = double.Parse(aluno.ReceitaBruta, NumberStyles.Currency).ToString();
                aluno.ReceitaBruta = Math.Round(Convert.ToDouble(aluno.ReceitaBruta), 2).ToString();
            }
            if (aluno.ReceitaLiquida != null)
            {
                aluno.ReceitaLiquida = double.Parse(aluno.ReceitaLiquida, NumberStyles.Currency).ToString();
                aluno.ReceitaLiquida = Math.Round(Convert.ToDouble(aluno.ReceitaLiquida), 2).ToString();
            }
            if (aluno.ReceitaFies != null)
            {
                aluno.ReceitaFies = double.Parse(aluno.ReceitaFies, NumberStyles.Currency).ToString();
                aluno.ReceitaFies = Math.Round(Convert.ToDouble(aluno.ReceitaFies), 2).ToString();
            }
            if (aluno.ValorDeRepasse != null)
            {
                aluno.ValorDeRepasse = double.Parse(aluno.ValorDeRepasse, NumberStyles.Currency).ToString();
                aluno.ValorDeRepasse = Math.Round(Convert.ToDouble(aluno.ValorDeRepasse), 2).ToString();
            }
        }

        //Atualizar BD
        public static void AtualizarAlunosBD(List<TOAluno> alunos)
        {
            InsertListLite(alunos);
        }

        public static void VerificarCPFDuplicado(List<TOAluno> alunos)
        {
            //Verificar se existem CPFs duplicados e marca as conclusões
            var duplicado = alunos.GroupBy(x => new { x.Cpf }).Where(x => x.Skip(1).Any()).ToList();
            string mensagem = "CPF(s) duplicados: ";
            foreach (var item in duplicado)
            {
                UpdateConclusao(item.Key.Cpf);
                mensagem += item.Key.Cpf + "\n";
            }
            if (duplicado.Count() > 0)
            {
                MessageBox.Show(mensagem);
            }
        }

        //Validar Login
        public static TOUsuario ValidateLogin(string user, string password)
        {
            string hashedPassword = Util.GetMD5(password);
            List<TOUsuario> temp = Dados.SelectWhere<TOUsuario>(x => x.Usuario == user && x.Senha == hashedPassword);
            if (temp.Count != 0)
            {
                return temp[0];
            }
            return null;
        }
        public static TOUsuario ValidateSession(string user, string password)
        {
            List<TOUsuario> temp = Dados.SelectWhere<TOUsuario>(x => x.Usuario == user && x.Senha == password);
            if (temp.Count != 0)
            {
                return temp[0];
            }
            return null;
        }

        //Verificações
        public static int verficarSemestre(string semestre)
        {
            return SelectWhere<TOSemestre>(x => x.Semestre == semestre).Count;
        }
        public static bool DRIExists(string cpf)
        {
            List<TODRI> list = SelectWhere<TODRI>(x => x.Cpf == cpf);
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
            List<TODRI> list = SelectWhere<TODRI>(x => x.Cpf == cpf);
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
                InsertDocumento<TOSemestre>(semestre);
            }
        }
        public static bool VerificaQtdAlunos()
        {
            int countAlunoTO = Count<TOAluno>();
            if (countAlunoTO > 0)
            {
                string mensagem = "Tem certeza que deseja excluir o banco de dados?" +
                    "\n\nCertifique-se de já ter exportado antes para que nenhuma informação seja perdida!";

                if (MessageBox.Show(mensagem, "Limpar Banco de Dados", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {

                    Util.CreateDirectoryIfNotExists("backup");
                    DirectoryInfo directory = new DirectoryInfo("backup");
                    if (directory.GetFiles().Count() >= 5)
                    {
                        FileInfo myFile = directory.GetFiles().OrderByDescending(f => f.LastWriteTime).Last();
                        myFile.Delete();
                    }


                    File.Copy(CAMINHO_BANCO, "backup/BACKUP_BDBOT " + DateTime.Now.ToString("dd_MM_yy HH-mm-ss") + ".db");
                    DeleteAllLite<TOAluno>();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }


        //LITE

        //Selects
        /// <summary>
        /// Select All de qualquer coleção, basta especificar a classe
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> SelectAll<T>()
        {
            using (var db = new LiteDatabase(CAMINHO_BANCO))
            {
                var colecao = db.GetCollection<T>();
                return colecao.Query().ToList();
            }
        }

        public static List<TOUsuario> SelectUsuarioWhereIES(string IES)
        {
            using (var db = new LiteDatabase(CAMINHO_BANCO))
            {
                var colecao = db.GetCollection<TOUsuario>();
                return colecao.Query().Where(x => x.IES == IES).ToList();
            }
        }

        public static List<TOAluno> SelectAlunoWhere(string tipoFies)
        {
            using (var db = new LiteDatabase(CAMINHO_BANCO))
            {
                var colecao = db.GetCollection<TOAluno>();
                return colecao.Query().Where(x => x.Tipo.ToUpper().Contains(tipoFies) && x.Conclusao == "Não Feito").ToList();
            }
        }

        public static List<TOLogin> SelectLoginWhere(string plataforma)
        {
            using (var db = new LiteDatabase(CAMINHO_BANCO))
            {
                var colecao = db.GetCollection<TOLogin>();
                return colecao.Query().Where(x => x.Plataforma == plataforma).ToList();
            }
        }

        public static List<T> SelectWhere<T>(Expression<Func<T, bool>> expression)
        {
            using (var db = new LiteDatabase(CAMINHO_BANCO))
            {
                var colecao = db.GetCollection<T>();
                return colecao.Query().Where(expression).ToList();
            }
        }

        public static List<TOMenus> SelectMenuWhereLite(string plataforma)
        {
            var lista = new List<TOMenus>();
            using (var db = new LiteDatabase(CAMINHO_BANCO))
            {
                var colecao = db.GetCollection<TOMenus>();
                return colecao.Query().Where(x => x.Modalidade == plataforma || x.Modalidade == null || x.Modalidade == "").ToList();
            }
        }

        public static List<string> SelectLoginTOIES(string IES, string plataforma)
        {
            List<TOLogin> listlogin;
            if (IES == "TODOS")
            {
                listlogin = SelectWhere<TOLogin>(x => x.Plataforma == plataforma);
            }
            else
            {
                listlogin = SelectWhere<TOLogin>(x => x.Plataforma == plataforma && x.Faculdade == IES);
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
                listlogin = SelectWhere<TOLogin>(x => x.Plataforma == plataforma);
            }
            else
            {
                if (plataforma == "FIES Legado" && campus != string.Empty)
                {
                    listlogin = SelectWhere<TOLogin>(x => x.Faculdade == IES && x.Plataforma == plataforma && x.Campus == campus && x.Admin == "Não");
                }
                else
                {
                    if (admin == true)
                    {
                        listlogin = SelectWhere<TOLogin>(x => x.Faculdade == IES && x.Plataforma == plataforma && x.Admin == "Sim");
                    }
                    else
                    {
                        listlogin = SelectWhere<TOLogin>(x => x.Faculdade == IES && x.Plataforma == plataforma && x.Admin == "Não");
                    }
                }

                if (listlogin.Count == 0)
                {
                    listlogin = SelectWhere<TOLogin>(x => x.Faculdade == IES && x.Plataforma == plataforma && x.Admin == "Sim");

                    //Caso ainda não tenha nenhum login mostra exception
                    if (listlogin.Count == 0)
                    {
                        throw new Exception("Login não encontrado!");
                    }
                }
            }
            return listlogin;
        }
        //DELETE
        public static void DeleteAllLite<T>()
        {
            using (var db = new LiteDatabase(CAMINHO_BANCO))
            {
                var colecao = db.GetCollection<T>();
                colecao.DeleteAll();
            }
        }
        public static void DeleteLite<T>(T objeto)
        {
            using (var db = new LiteDatabase(CAMINHO_BANCO))
            {
                var colecao = db.GetCollection<T>();
                colecao.Delete(Convert.ToInt32(objeto.GetType().GetProperty("Id").GetValue(objeto)));
            }
        }

        //UPDATES
        public static void UpdateDocumento<T>(T objeto)
        {
            using (var db = new LiteDatabase(CAMINHO_BANCO))
            {
                var colecao = db.GetCollection<T>();
                colecao.Update(objeto);
            }
        }

        public static void UpdateConclusao(string Cpf)
        {
            using (var db = new LiteDatabase(CAMINHO_BANCO))
            {
                var colecao = db.GetCollection<TOAluno>();
                colecao.UpdateMany(x => new TOAluno { Conclusao = "Duplicado" }, x => x.Cpf == Cpf);
            }
        }

        public static void UpdateConclusaoAluno(string conclusao)
        {
            using (var db = new LiteDatabase(CAMINHO_BANCO))
            {
                var colecao = db.GetCollection<TOAluno>();
                colecao.UpdateMany(x => new TOAluno { Conclusao = conclusao }, x => x.Conclusao != "Não Feito" && x.Conclusao != "Duplicado");
            }
        }

        //Insert
        public static void InsertListLite<T>(List<T> lista) where T : class, new()
        {
            using (var db = new LiteDatabase(CAMINHO_BANCO))
            {
                var colecao = db.GetCollection<T>();
                colecao.InsertBulk(lista);
            }
        }
        public static void InsertDocumento<T>(T objeto)
        {
            using (var db = new LiteDatabase(CAMINHO_BANCO))
            {
                var colecao = db.GetCollection<T>();
                colecao.Insert(objeto);
            }
        }

        //Count
        public static int Count<T>()
        {
            using (var db = new LiteDatabase(CAMINHO_BANCO))
            {
                var colecao = db.GetCollection<T>();
                return colecao.Count();
            }
        }
    }
}