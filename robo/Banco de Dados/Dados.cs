using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using LiteDB;
using System.Linq.Expressions;
using System.IO;
using System.Text;
using CsvHelper;
using System.Globalization;
using robo.TO;
using robo.Utils;

namespace robo.Banco_de_Dados
{
    /// <summary>
    /// Classe utilizada para operações envolvendo banco de dados
    /// </summary>
    public static class Dados
    {
        private const string CAMINHO_BANCO = "Filename = data/bdbot.db; Password=AlunosBrilhantes;";

        //CSV
        /// <summary>
        /// Importa um arquivo CSV para o banco de dados
        /// </summary>
        /// <param name="caminhoArquivo">Caminho para o arquivo .csv</param>
        /// <param name="tipo">FIES Legado ou FIES Novo</param>
        public static void ImportaAlunos(string caminhoArquivo, string tipo)
        {
            List<TOAluno> alunos = BuscarListaAlunos(caminhoArquivo, tipo);
            InsertListLite(alunos);
            VerificarCPFDuplicado(alunos);
        }

        /// <summary>
        /// Busca todos alunos na planilha Excel.
        /// </summary>
        /// <param name="diretorio">Diretório da planilha excel.</param>
        /// <param name="tipo">FIES Legado ou FIES Novo</param>
        /// <returns>Lista de alunos.</returns>
        public static List<TOAluno> BuscarListaAlunos(string diretorio, string tipo)
        {
            List<TOAluno> alunos = LerCSVAlunos(diretorio);
            RemoverLinhasVazias(alunos);
            TratarPropriedadesAlunos(tipo, alunos);
            return alunos;
        }

        /// <summary>
        /// Remove linhas que não possuem CPF do CSV
        /// </summary>
        private static void RemoverLinhasVazias(List<TOAluno> alunos)
        {
            for (int i = alunos.Count() - 1; i >= 0; i--)
            {
                if (alunos[i].Cpf == string.Empty)
                {
                    alunos.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Corrige propriedades dos alunos
        /// </summary>
        private static void TratarPropriedadesAlunos(string tipo, List<TOAluno> alunos)
        {
            for (int i = alunos.Count() - 1; i >= 0; i--)
            {
                alunos[i].TratarCpf();
                alunos[i].TratarTextoReceitas();
                alunos[i].TratarVirgulaReceitas();
                alunos[i].TratarCampus();
                alunos[i].TratarTipoFies(tipo);
            }
        }

        /// <summary>
        /// Retorna lista de alunos de um arquivo CSV
        /// </summary>
        private static List<TOAluno> LerCSVAlunos(string caminhoArquivo)
        {
            List<TOAluno> alunos;
            try
            {
                using (StreamReader sr = new StreamReader(caminhoArquivo, Encoding.UTF7))
                {
                    using (CsvReader csv = new CsvReader(sr, CultureInfo.CurrentCulture))
                    {
                        List<TOAluno> registros = csv.GetRecords<TOAluno>().ToList();
                        alunos = registros;
                    }
                }
            }
            catch (IOException)
            {
                throw new Exception("O arquivo escolhido ainda está aberto. Por favor feche o arquivo antes de importar.");
            }
            return alunos;
        }

        //Tratamentos de valores
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
        /// Verifica se o banco de dados possui algum CPF duplicado
        /// </summary>
        public static void VerificarCPFDuplicado(List<TOAluno> alunos)
        {
            var duplicado = alunos.GroupBy(x => x.Cpf).Where(x => x.Skip(1).Any()).ToList();
            string mensagem = "CPF(s) duplicados: ";
            mensagem = MarcarConclusaoAlunosDuplicados(duplicado, mensagem);

            if (duplicado.Count() > 0)
            {
                MessageBox.Show(mensagem);
            }
        }

        /// <summary>
        /// Marca todos os alunos duplicados no banco de dados
        /// </summary>
        private static string MarcarConclusaoAlunosDuplicados(List<IGrouping<string, TOAluno>> duplicado, string mensagem)
        {
            foreach (var item in duplicado)
            {
                MarcarConclusaoDuplicada(item.Key);
                mensagem += item.Key + "\n";
            }
            return mensagem;
        }

        //Validar Login
        /// <summary>
        /// Valida se o login digitado pelo usuário é válido
        /// </summary>
        /// <returns>Usuário que corresponde ao login e senha digitados</returns>
        public static TOUsuario ValidarLogin(string usuario, string senha)
        {
            string senhaCriptografada = Util.CriptografarSenha(senha);
            List<TOUsuario> usuarios = SelectWhere<TOUsuario>(x => x.Usuario == usuario && x.Senha == senhaCriptografada);
            if (usuarios.Count != 0)
            {
                return usuarios[0];
            }
            return null;
        }
        /// <summary>
        /// Verifica se o usuário e senha do arquivo session.dat é válido
        /// </summary>
        /// <returns>Usuário que corresponde ao login e senha encontrados no arquivo</returns>
        public static TOUsuario ValidarSessao(string usuario, string senha)
        {
            List<TOUsuario> usuarios = SelectWhere<TOUsuario>(x => x.Usuario == usuario && x.Senha == senha);
            if (usuarios.Count != 0)
            {
                return usuarios[0];
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
        public static bool VerificarDRI(string cpf)
        {
            List<TODRI> lista = SelectWhere<TODRI>(x => x.Cpf == cpf);
            if (lista.Count == 1)
            {
                return true;
            }
            else if (lista.Count == 0)
            {
                return false;
            }
            throw new Exception("Existem dados repetidos na tabela das DRIs.");
        }
        /// <summary>
        /// Busca a DRI desejada no banco de dados
        /// </summary>
        /// <returns>A DRI correspondente ao CPF buscado</returns>
        public static TODRI BuscarDRI(string cpf)
        {
            List<TODRI> lista = SelectWhere<TODRI>(x => x.Cpf == cpf);
            if (lista.Count == 1)
            {
                return lista[0];
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

                var ultimoSemestre = SelectAll<TOSemestre>();
                semestre.numSemestre = (Convert.ToInt32(ultimoSemestre[ultimoSemestre.Count - 1].numSemestre) + 1).ToString();
                InsertDocumento<TOSemestre>(semestre);
            }
        }
        /// <summary>
        /// Verifica se a quantidade de alunos no banco de dados é maior que 0, se for pergunta se o banco de dados deve ser limpo
        /// </summary>
        /// <returns>True se for < 0 e false se for > 0</returns>
        public static int VerificaQtdAlunos()
        {
            return Count<TOAluno>();
        }


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
        /// <param name="expressao">Expressão lambda com a consulta desejada, exemplo: x => x.nomeDaPropriedade == valorDaPropriedade</param>
        /// <returns>Lista com os registros encontrados</returns>
        public static List<T> SelectWhere<T>(Expression<Func<T, bool>> expressao)
        {
            using (var db = new LiteDatabase(CAMINHO_BANCO))
            {
                var colecao = db.GetCollection<T>();
                return colecao.Query().Where(expressao).ToList();
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

        /// <summary>
        /// Busca lista de campus existentes no banco de dados baseado na IES desejada
        /// </summary>
        /// <returns>Lista de nomes de todos os campus da IES selecionada</returns>
        public static List<string> SelectLoginTOIES(string IES)
        {
            List<TOLogin> listlogin = SelectWhere<TOLogin>(x => x.Plataforma.ToUpper() == "FIES LEGADO" && x.Faculdade == IES);
            List<string> listCampus = new List<string>
            {
                ""
            };
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
        public static void MarcarConclusaoDuplicada(string Cpf)
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

        /// <summary>
        /// Atualiza todos as conclusões diferentes de Não Feito ou Duplicado para a conclusão desejada
        /// </summary>
        /// <param name="conclusao">Conclusão desejada</param>
        public static void UpdateTemporarioAluno()
        {
            using (var db = new LiteDatabase(CAMINHO_BANCO))
            {
                var colecao = db.GetCollection<TOAluno>();
                colecao.UpdateMany(x => new TOAluno { Temporario = string.Empty }, x => x.Temporario == "Feito");
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