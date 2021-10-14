using CsvHelper.Configuration.Attributes;
using System;

namespace Robo
{
    public class TOAluno
    {
        [Optional]
        [Ignore]
        public int Id { get; set; }

        [Name("CPF")]
        public string Cpf { get; set; }

        /// <summary>Nome do aluno.</summary>  
        [Optional]
        [Name("NOME")]
        public string Nome { get; set; }

        /// <summary>Conclusão atual do aluno </summary>
        [Optional]
        public string Conclusao { get; set; }

        /// <summary>Data e hora da conclusao do procedimento</summary>
        [Optional]
        public string HorarioConclusao { get; set; }

        /// <summary>FIES Legado ou Novo</summary>
        [Optional]
        [Name("TIPO", "MODALIDADE FIES")]
        public string Tipo { get; set; }

        /// <summary>Campus no qual o aluno estuda.</summary>  
        [Optional]
        [Name("CAMPUS ADITADO")]
        public string Campus { get; set; }

        /// <summary>aproveitamento do semestre atual do aluno </summary>
        [Optional]
        [Name("APROVEITAMENTO ATUAL")]
        public string AproveitamentoAtual { get; set; }

        /// <summary>historico de reconsiderações do aluno</summary> 
        [Optional]
        [Name("HISTÓRICO DE APROVEITAMENTO")]
        public string HistoricoAproveitamento { get; set; }

        /// <summary>Receita bruta.</summary>  
        [Optional]
        [Name("RECEITA BRUTA")]
        public string ReceitaBruta { get; set; }

        /// <summary>Receita liquida.</summary>  
        [Optional]
        [Name("RECEITA LIQUIDA")]
        public string ReceitaLiquida { get; set; }

        /// <summary>Receita liquida.</summary>
        [Optional]
        [Name("RECEITA FIES")]
        public string ReceitaFies { get; set; }

        /// <summary>Campus Aditado.</summary>
        [Optional]
        public string CampusAditado { get; set; }
        [Optional]
        /// <summary>Receita liquida.</summary>
        public string ValorAditado { get; set; }

        /// <summary></summary>
        [Optional]
        public string ValorAditadoComDesconto { get; set; }

        /// <summary></summary>
        [Optional]
        public string ValorAditadoFinanciamento { get; set; }

        /// <summary></summary>
        [Optional]
        public string ValorPagoRecursoEstudante { get; set; }

        /// <summary>Se aluno possui desconto de liberalidade ou não</summary>
        [Optional]
        [Name("DESCONTO LIBERALIDADE")]
        public string DescontoLiberalidade { get; set; }

        /// <summary>Se documento do aluno foi baixado ou não</summary>
        [Optional]
        public string Extraido { get; set; }

        /// <summary>Justificativa para concluir aditamento.</summary>
        [Name("JUSTIFICATIVA")]
        [Optional]
        public string Justificativa { get; set; }

        //Dados para Extrair Informações da DRM/DRI do aluno
        [Optional]
        public string SemestreAditar { get; set; }
        [Optional]
        public string Curso { get; set; }
        [Optional]
        public string DuracaoRegular { get; set; }
        [Optional]
        public string TotalDeSemestresSuspensos { get; set; }
        [Optional]
        public string TotalDeSemestresDilatados { get; set; }
        [Optional]
        public string TotalDeSemestresConcluidos { get; set; }
        [Optional]
        public string SemestreSerCursadoPeloEstudante { get; set; }
        [Optional]
        public string TotalDeSemestresJaFinanciados { get; set; }
        [Optional]
        public string PercentualDeFinanciamentoSolicitado { get; set; }
        [Optional]
        public string GradeAtualComDesconto { get; set; }
        [Optional]
        public string GradeAtualFinanciadoFIES { get; set; }
        [Optional]
        public string GradeAtualCoparticipacao { get; set; }

        //Dados para consultar informações de aditamento de aluno FIES Novo
        [Optional]
        public string SemestreAno { get; set; }
        [Optional]
        public string Finalidade { get; set; }
        [Optional]
        public string Situacao { get; set; }
        [Optional]
        public string ProUni { get; set; }
        [Optional]
        public string DataInclusao { get; set; }
        [Optional]
        public string DataConclusao { get; set; }
        [Optional]
        [Name("CURSO SIGA")]
        public string CursoSiga { get; set; }
        [Optional]
        [Name("VALOR DE REPASSE")]
        public string ValorDeRepasse { get; set; }
        [Optional]
        public string ParcelaSiga1 { get; set; }
        [Optional]
        public string ParcelaSiga2 { get; set; }
        [Optional]
        public string ParcelaSiga3 { get; set; }
        [Optional]
        public string ParcelaSiga4 { get; set; }
        [Optional]
        public string ParcelaSiga5 { get; set; }
        [Optional]
        public string ParcelaSiga6 { get; set; }



        /// <summary>
        /// Contrutor da classe TOAluno que garante que não exista propriedade nula.
        /// </summary>
        public TOAluno()
        {
            this.Cpf = string.Empty;
            this.Campus = string.Empty;
            this.Nome = string.Empty;
            this.HistoricoAproveitamento = string.Empty;
            this.ReceitaBruta = "-";
            this.ReceitaLiquida = "-";
            this.ReceitaFies = "-";
            this.ValorAditado = string.Empty;
            this.CampusAditado = string.Empty;
            this.Tipo = string.Empty;
            this.Conclusao = "Não Feito";
            this.AproveitamentoAtual = string.Empty;
            this.ValorAditadoComDesconto = string.Empty;
            this.ValorAditadoFinanciamento = string.Empty;
            this.ValorPagoRecursoEstudante = string.Empty;
            this.HorarioConclusao = string.Empty;
            this.DescontoLiberalidade = string.Empty;
            this.Extraido = "Não";
            this.Justificativa = string.Empty;

            //Dados Extrair Informações DRM/DRI
            this.SemestreAditar = string.Empty;
            this.Curso = string.Empty;
            this.DuracaoRegular = string.Empty;
            this.TotalDeSemestresSuspensos = string.Empty;
            this.TotalDeSemestresDilatados = string.Empty;
            this.TotalDeSemestresConcluidos = string.Empty;
            this.SemestreSerCursadoPeloEstudante = string.Empty;
            this.TotalDeSemestresJaFinanciados = string.Empty;
            this.PercentualDeFinanciamentoSolicitado = string.Empty;
            this.GradeAtualComDesconto = string.Empty;
            this.GradeAtualFinanciadoFIES = string.Empty;
            this.GradeAtualCoparticipacao = string.Empty;
            this.Tipo = string.Empty;

            //Dados Consultar Status Aditamento Aluno FIES Novo
            this.SemestreAno = string.Empty;
            this.Finalidade = string.Empty;
            this.Situacao = string.Empty;
            this.ProUni = string.Empty;
            this.DataInclusao = string.Empty;
            this.DataConclusao = string.Empty;

            //Dados SIGA
            this.CursoSiga = string.Empty;
            this.ValorDeRepasse = "-";
            this.ParcelaSiga1 = string.Empty;
            this.ParcelaSiga2 = string.Empty;
            this.ParcelaSiga3 = string.Empty;
            this.ParcelaSiga4 = string.Empty;
            this.ParcelaSiga5 = string.Empty;
            this.ParcelaSiga6 = string.Empty;
        }
    }
}