using System;
using System.Collections.Generic;
using System.Linq;
using robo.pgm;
using robo;
using System.Windows.Forms;
using LiteDB;
using System.Linq.Expressions;
using System.IO;
using System.Text;
using CsvHelper;
using System.Globalization;

namespace Robo
{
    /// <summary>
    /// Classe utilizada para operações envolvendo banco de dados
    /// </summary>
    public static class Dados
    {
        private const string CAMINHO_BANCO = "Filename = data/bdbot1.db; Password=AlunosBrilhantes;";


        //CSV
        /// <summary>
        /// Importa um arquivo CSV para o banco de dados
        /// </summary>
        /// <param name="filePath">Caminho para o arquivo .csv</param>
        /// <param name="tipo">FIES Legado ou FIES Novo</param>
        public static void ImportaAlunos(string filePath, string tipo)
        {
            List<TOAluno> alunos = BuscarListaAlunos(filePath, tipo);
            InsertListLite(alunos);
            VerificarCPFDuplicado(alunos);
        }
        /// <summary>
        /// Busca todos alunos na planilha Excel.
        /// </summary>
        /// <param name="directory">Diretório da planilha excel.</param>
        /// <param name="tipo">FIES Legado ou FIES Novo</param>
        /// <returns>Lista de alunos.</returns>
        public static List<TOAluno> BuscarListaAlunos(string directory, string tipo)
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
                    if (alunos[i].Cpf == string.Empty)
                    {
                        alunos.RemoveAt(i);
                    }
                    else
                    {
                        TratarCpf(alunos[i]);
                        TratarTextoReceitas(alunos[i]);
                        TratarVirgulaReceitas(alunos[i]);
                        TratarCampusAluno(alunos[i]);
                        TratarTipoFiesAluno(tipo, alunos[i]);
                    }
                }

                return alunos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        

        //Tratamentos de valores
        /// <summary>
        /// Remove . e - e adiciona a quantidade de zeros necessária
        /// </summary>
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
        public static void TratarVirgulaReceitas(TOAluno aluno)
        {
            aluno.ReceitaBruta = FormatarReceitas(aluno.ReceitaBruta);
            aluno.ReceitaLiquida = FormatarReceitas(aluno.ReceitaLiquida);
            aluno.ReceitaFies = FormatarReceitas(aluno.ReceitaFies);
            aluno.ValorAditado = FormatarReceitas(aluno.ValorAditado);

            aluno.ValorDeRepasse = FormatarReceitas(aluno.ValorDeRepasse);
        }
        /// <summary>
        /// Corrige o campus vindo da planilha de acordo com alguns casos conhecidos
        /// </summary>
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
        /// <summary>
        /// Corrige o tipo de FIES do aluno vindo da planilha para o formato utilizado
        /// </summary>
        private static void TratarTipoFiesAluno(string tipo, TOAluno aluno)
        {
            if (string.IsNullOrEmpty(aluno.Tipo))
            {
                aluno.Tipo = tipo;
                aluno.Tipo = aluno.Tipo.ToUpper().Trim();
            }
            else
            {
                if (aluno.Tipo.ToUpper().Contains("NOVO"))
                {
                    aluno.Tipo = "FIES NOVO";
                }
                else
                {
                    aluno.Tipo = "FIES LEGADO";
                }
            }
        }
        /// <summary>
        /// Formata a receita para a maneira aceita pelos sites
        /// </summary>
        /// <param name="valor">Receita a ser formatada</param>
        /// <returns>Receita formatada no formato 0,00</returns>
        public static string FormatarReceitas(string valor)
        {
            if (string.IsNullOrEmpty(valor))
            {
                return null;
            }
            double valorDouble = Convert.ToDouble(valor);
            valorDouble = Math.Round(valorDouble, 2);
            valor = valorDouble.ToString("0.00");

            return valor;
        }
        /// <summary>
        /// Remove os possíveis textos (R$) que possam vir da planilha importada
        /// </summary>
        public static void TratarTextoReceitas(TOAluno aluno)
        {
            if (string.IsNullOrEmpty(aluno.ReceitaBruta) && string.IsNullOrEmpty(aluno.ReceitaLiquida) &&
                string.IsNullOrEmpty(aluno.ReceitaFies) && string.IsNullOrEmpty(aluno.ValorDeRepasse))
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

        /// <summary>
        /// Verifica se o banco de dados possui algum CPF duplicado
        /// </summary>
        public static void VerificarCPFDuplicado(List<TOAluno> alunos)
        {
            //Verificar se existem CPFs duplicados e marca as conclus�es
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
        /// <summary>
        /// Valida se o login digitado pelo usuário é válido
        /// </summary>
        /// <returns>Usuário que corresponde ao login e senha digitados</returns>
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
        /// <summary>
        /// Verifica se o usuário e senha do arquivo session.dat é válido
        /// </summary>
        /// <returns>Usuário que corresponde ao login e senha encontrados no arquivo</returns>
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
        /// <summary>
        /// Verifica se o semestre já está presente no banco de dados
        /// </summary>
        /// <returns>0 se o semestre não for encontrado e 1 se for</returns>
        public static int VerificarSemestreExiste(string semestre)
        {
            return SelectWhere<TOSemestre>(x => x.Semestre == semestre).Count;
        }
        /// <summary>
        /// Verifica se existe uma DRI do CPF desejado no banco de dados
        /// </summary>
        /// <returns>true se for encontrada e false se não for</returns>
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
        /// <summary>
        /// Busca a DRI desejada no banco de dados
        /// </summary>
        /// <returns>A DRI correspondente ao CPF buscado</returns>
        public static TODRI GetDRI(string cpf)
        {
            List<TODRI> list = SelectWhere<TODRI>(x => x.Cpf == cpf);
            if (list.Count == 1)
            {
                return list[0];
            }
            throw new Exception("Existem dados repetidos na tabela das DRIs.");

        }
        /// <summary>
        /// Verifica se o semestre atual está presente no banco de dados e o adiciona caso não esteja
        /// </summary>
        public static void VerificaSemestre()
        {
            string verificarSemestre = Util.VerificaSemestreAtual();

            if (VerificarSemestreExiste(verificarSemestre) == 0)
            {
                TOSemestre semestre = new TOSemestre();
                semestre.Semestre = verificarSemestre;
                InsertDocumento<TOSemestre>(semestre);
            }
        }
        /// <summary>
        /// Verifica se a quantidade de alunos no banco de dados é maior que 0, se for pergunta se o banco de dados deve ser limpo
        /// </summary>
        /// <returns>True se for < 0 e false se for > 0</returns>
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


                    File.Copy("data/bdbot1.db", "backup/BACKUP_BDBOT " + DateTime.Now.ToString("dd_MM_yy HH-mm-ss") + ".db");
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
        /// <typeparam name="T">Tipo de dado desejado</typeparam>
        /// <returns>Lista com todos os registros do tipo de dado desejado</returns>
        public static List<T> SelectAll<T>()
        {
            using (var db = new LiteDatabase(CAMINHO_BANCO))
            {
                var colecao = db.GetCollection<T>();
                return colecao.Query().ToList();
            }
        }

        /// <summary>
        /// Select usuário baseado na IES
        /// </summary>
        /// <param name="IES">IES desejado</param>
        /// <returns>Lista com todos os usuários da IES selecionada</returns>
        public static List<TOUsuario> SelectUsuarioWhereIES(string IES)
        {
            using (var db = new LiteDatabase(CAMINHO_BANCO))
            {
                var colecao = db.GetCollection<TOUsuario>();
                return colecao.Query().Where(x => x.IES == IES).ToList();
            }
        }

        /// <summary>
        /// Select aluno baseado no tipo do FIES e que possuem conclusao = Não Feito
        /// </summary>
        /// <returns>Lista de alunos do tipo FIES selecionado</returns>
        public static List<TOAluno> SelectAlunoWhere(string tipoFies)
        {
            using (var db = new LiteDatabase(CAMINHO_BANCO))
            {
                var colecao = db.GetCollection<TOAluno>();
                return colecao.Query().Where(x => x.Tipo.ToUpper().Contains(tipoFies) && x.Conclusao == "Não Feito").ToList();
            }
        }

        /// <summary>
        /// Select where genérico
        /// </summary>
        /// <typeparam name="T">Classe da coleção de dados desejada</typeparam>
        /// <param name="expression">Expressão lambda com a consulta desejada, exemplo: x => x.nomeDaPropriedade == valorDaPropriedade</param>
        /// <returns>Lista com os registros encontrados</returns>
        public static List<T> SelectWhere<T>(Expression<Func<T, bool>> expression)
        {
            using (var db = new LiteDatabase(CAMINHO_BANCO))
            {
                var colecao = db.GetCollection<T>();
                return colecao.Query().Where(expression).ToList();
            }
        }

        /// <summary>
        /// Select lista de menus baseado na plataforma
        /// </summary>
        /// <param name="plataforma">NOVO ou LEGADO</param>
        /// <returns>Lista de menus encontrados</returns>
        public static List<TOMenus> SelectMenuWhereLite(string plataforma)
        {
            var lista = new List<TOMenus>();
            using (var db = new LiteDatabase(CAMINHO_BANCO))
            {
                var colecao = db.GetCollection<TOMenus>();
                return colecao.Query().Where(x => x.Modalidade.Contains(plataforma) && x.Permissao.Contains(Program.login.Permissao)).OrderBy(x => x.Ordem).ToList();
            }
        }

        //Remover parâmetro plataforma após finalização da interface

        /// <summary>
        /// Busca lista de campus existentes no banco de dados baseado na IES desejada
        /// </summary>
        /// <returns>Lista de nomes de todos os campus da IES selecionada</returns>
        public static List<string> SelectLoginTOIES(string IES, string plataforma)
        {
            List<TOLogin> listlogin;
            if (IES == "TODOS")
            {
                listlogin = SelectWhere<TOLogin>(x => x.Plataforma.ToUpper() == "FIES LEGADO");
            }
            else
            {
                listlogin = SelectWhere<TOLogin>(x => x.Plataforma.ToUpper() == "FIES LEGADO" && x.Faculdade == IES);
            }

            List<string> listCampus = new List<string>();
            listCampus.Add("");
            foreach (var item in listlogin)
            {
                listCampus.Add(item.Campus);
            }
            return listCampus;

        }
        /// <summary>
        /// Busca logins de acordo com a IES e o tipo FIES selecionados
        /// </summary>
        /// <param name="IES">IES desejada</param>
        /// <param name="plataforma">Tipo de FIES</param>
        /// <param name="campus">Campus para buscar logins do FIES Legado</param>
        /// <param name="admin">True se o usuário deve ser obrigatoriamente um admin</param>
        /// <returns>Lista com todos os logins encontrados</returns>
        public static List<TOLogin> SelectLoginPorIESePlataforma(string IES, string plataforma, string campus, bool admin)
        {
            List<TOLogin> listlogin;

            if (IES == "TODOS")
            {
                listlogin = SelectWhere<TOLogin>(x => x.Plataforma.ToUpper().Contains(plataforma));
            }
            else
            {
                if (plataforma.Contains("LEGADO") && campus != string.Empty)
                {
                    listlogin = SelectWhere<TOLogin>(x => x.Faculdade == IES && x.Plataforma.ToUpper().Contains(plataforma) && x.Campus == campus && x.Admin == "Não");
                }
                else
                {
                    if (admin == true)
                    {
                        listlogin = SelectWhere<TOLogin>(x => x.Faculdade == IES && x.Plataforma.ToUpper().Contains(plataforma) && x.Admin == "Sim");
                    }
                    else
                    {
                        listlogin = SelectWhere<TOLogin>(x => x.Faculdade == IES && x.Plataforma.ToUpper().Contains(plataforma) && x.Admin == "Não");
                    }
                }

                if (listlogin.Count == 0)
                {
                    listlogin = SelectWhere<TOLogin>(x => x.Faculdade == IES && x.Plataforma.ToUpper().Contains(plataforma) && x.Admin == "Sim");

                    //Caso ainda n�o tenha nenhum login mostra exception
                    if (listlogin.Count == 0)
                    {
                        throw new Exception("Login n�o encontrado!");
                    }
                }
            }
            return listlogin;
        }


        //DELETE

        /// <summary>
        /// Delete de todos os documentos da coleção desejada
        /// </summary>
        /// <typeparam name="T">Classe da coleção desejada</typeparam>
        public static void DeleteAllLite<T>()
        {
            using (var db = new LiteDatabase(CAMINHO_BANCO))
            {
                var colecao = db.GetCollection<T>();
                colecao.DeleteAll();
            }
        }
        /// <summary>
        /// Delete de um único documento do banco de dados
        /// </summary>
        /// <typeparam name="T">Classe do objeto desejado</typeparam>
        /// <param name="objeto">Objeto que se deseja deletar</param>
        public static void DeleteLite<T>(T objeto)
        {
            using (var db = new LiteDatabase(CAMINHO_BANCO))
            {
                var colecao = db.GetCollection<T>();
                colecao.Delete(Convert.ToInt32(objeto.GetType().GetProperty("Id").GetValue(objeto)));
            }
        }

        //UPDATES

        /// <summary>
        /// Update em um documento
        /// </summary>
        /// <typeparam name="T">Classe do documento desejado</typeparam>
        /// <param name="objeto">Documento que irá substituir o documento com mesmo ID no banco de dados</param>
        public static void UpdateDocumento<T>(T objeto)
        {
            using (var db = new LiteDatabase(CAMINHO_BANCO))
            {
                var colecao = db.GetCollection<T>();
                colecao.Update(objeto);
            }
        }
        /// <summary>
        /// Update conclusão de aluno para Duplicado
        /// </summary>
        public static void UpdateConclusao(string Cpf)
        {
            using (var db = new LiteDatabase(CAMINHO_BANCO))
            {
                var colecao = db.GetCollection<TOAluno>();
                colecao.UpdateMany(x => new TOAluno { Conclusao = "Duplicado" }, x => x.Cpf == Cpf);
            }
        }

        /// <summary>
        /// Atualiza todos as conclusões diferentes de Não Feito ou Duplicado para a conclusão desejada
        /// </summary>
        /// <param name="conclusao">Conclusão desejada</param>
        public static void UpdateConclusaoAluno(string conclusao)
        {
            using (var db = new LiteDatabase(CAMINHO_BANCO))
            {
                var colecao = db.GetCollection<TOAluno>();
                colecao.UpdateMany(x => new TOAluno { Conclusao = conclusao }, x => x.Conclusao != "Não Feito" && x.Conclusao != "Duplicado");
            }
        }

        //Insert

        /// <summary>
        /// Insert de uma lista de documentos no banco de dados
        /// </summary>
        /// <typeparam name="T">Classe da lista desejada</typeparam>
        /// <param name="lista">Lista de objetos que será inserida</param>
        public static void InsertListLite<T>(List<T> lista) where T : class, new()
        {
            using (var db = new LiteDatabase(CAMINHO_BANCO))
            {
                var colecao = db.GetCollection<T>();
                colecao.InsertBulk(lista);
            }
        }
        /// <summary>
        /// Insert de um único documento no banco de dados
        /// </summary>
        /// <typeparam name="T">Classe do documento desejado</typeparam>
        /// <param name="objeto">Documento que será inserido no banco de dados</param>
        public static void InsertDocumento<T>(T objeto)
        {
            using (var db = new LiteDatabase(CAMINHO_BANCO))
            {
                var colecao = db.GetCollection<T>();
                colecao.Insert(objeto);
            }
        }

        //Count

        /// <summary>
        /// Count de alguma coleção do banco de dados
        /// </summary>
        /// <typeparam name="T">Classe da coleção desejada</typeparam>
        /// <returns>Quantidade de documentos da coleção encontrados no banco de dados</returns>
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