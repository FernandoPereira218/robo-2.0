using System;

namespace Robo
{
    public class TOAluno
    {
        /// <summary>CPF do aluno.</summary>  
        public string Cpf { get; set; }

        /// <summary>Nome do aluno.</summary>  
        public string Nome { get; set; }

        /// <summary>Conclusão atual do aluno </summary>
        public string Conclusao { get; set; }

        /// <summary>Data e hora da conclusao do procedimento</summary>
        public string HorarioConclusao { get; set; }

        /// <summary>FIES Legado ou Novo</summary>
        public string Tipo { get; set; }

        /// <summary>Campus no qual o aluno estuda.</summary>  
        public string Campus { get; set; }

        /// <summary>aproveitamento do semestre atual do aluno </summary>
        public string AproveitamentoAtual { get; set; }

        /// <summary>historico de reconsiderações do aluno</summary> 
        public string HistoricoAproveitamento { get; set; }

        /// <summary>Receita bruta.</summary>  
        public string ReceitaBruta { get; set; }

        /// <summary>Receita liquida.</summary>  
        public string ReceitaLiquida { get; set; }

        /// <summary>Receita liquida.</summary>
        public string ReceitaFies { get; set; }

        /// <summary>Campus Aditado.</summary>
        public string CampusAditado { get; set; }

        /// <summary>Receita liquida.</summary>
        public string ValorAditado { get; set; }

        /// <summary></summary>
        public string ValorAditadoComDesconto { get; set; }

        /// <summary></summary>
        public string ValorAditadoFinanciamento { get; set; }

        /// <summary></summary>
        public string ValorPagoRecursoEstudante { get; set; }

        /// <summary>Se aluno possui desconto de liberalidade ou não</summary>
        public string DescontoLiberalidade { get; set; }

        /// <summary>Se documento do aluno foi baixado ou não</summary>
        public string Extraido { get; set; }

        /// <summary>Justificativa para concluir aditamento.</summary>
        public string Justificativa { get; set; }

        //Dados para Extrair Informações da DRM/DRI do aluno
        public string SemestreAditar { get; set; }
        public string Curso { get; set; }
        public string DuracaoRegular { get; set; }
        public string TotalDeSemestresSuspensos { get; set; }
        public string TotalDeSemestresDilatados { get; set; }
        public string TotalDeSemestresConcluidos { get; set; }
        public string SemestreSerCursadoPeloEstudante { get; set; }
        public string TotalDeSemestresJaFinanciados { get; set; }
        public string PercentualDeFinanciamentoSolicitado { get; set; }
        public string GradeAtualComDesconto { get; set; }
        public string GradeAtualFinanciadoFIES { get; set; }
        public string GradeAtualCoparticipacao { get; set; }

        //Dados para consultar informações de aditamento de aluno FIES Novo
        public string SemestreAno { get; set; }
        public string Finalidade { get; set; }
        public string Situacao { get; set; }
        public string ProUni { get; set; }
        public string DataInclusao { get; set; }
        public string DataConclusao { get; set; }

        public string CursoSiga { get; set; }

        public string ValorDeRepasse { get; set; }
        public string ParcelaSiga1 { get; set; }
        public string ParcelaSiga2 { get; set; }
        public string ParcelaSiga3 { get; set; }
        public string ParcelaSiga4 { get; set; }
        public string ParcelaSiga5 { get; set; }
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
            this.ReceitaBruta = string.Empty;
            this.ReceitaLiquida = string.Empty;
            this.ReceitaFies = string.Empty;
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
            this.ValorDeRepasse = string.Empty;
            this.ParcelaSiga1 = string.Empty;
            this.ParcelaSiga2 = string.Empty;
            this.ParcelaSiga3 = string.Empty;
            this.ParcelaSiga4 = string.Empty;
            this.ParcelaSiga5 = string.Empty;
            this.ParcelaSiga6 = string.Empty;
        }
    }
}