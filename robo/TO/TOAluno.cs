using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using System;

namespace robo.TO
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
        [Ignore]
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
        public string TipoAditamento { get; set; }
        [Optional]
        public string Situacao { get; set; }
        [Optional]
        public string ProUni { get; set; }
        [Optional]
        public string DataInclusao { get; set; }
        [Optional]
        public string DataConclusao { get; set; }
        [Optional]
        [Name("Curso")]
        public string CursoSiga { get; set; }
        [Optional]
        [Name("VALOR DE REPASSE", "Valor de Repasse")]
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
            this.Cpf = null;
            this.Campus = null;
            this.Nome = null;
            this.HistoricoAproveitamento = null; 
            this.ReceitaBruta = null;
            this.ReceitaLiquida = null;
            this.ReceitaFies = null;
            this.ValorAditado = null;
            this.CampusAditado = null;
            this.Tipo = null;
            this.Conclusao = "Não Feito";
            this.AproveitamentoAtual = null;
            this.ValorAditadoComDesconto = null;
            this.ValorAditadoFinanciamento = null;
            this.ValorPagoRecursoEstudante = null;
            this.HorarioConclusao = null;
            this.DescontoLiberalidade = null;
            this.Extraido = "Não";
            this.Justificativa = null;

            //Dados Extrair Informações DRM/DRI
            this.SemestreAditar = null;
            this.Curso = null;
            this.DuracaoRegular = null;
            this.TotalDeSemestresSuspensos = null;
            this.TotalDeSemestresDilatados = null;
            this.TotalDeSemestresConcluidos = null;
            this.SemestreSerCursadoPeloEstudante = null;
            this.TotalDeSemestresJaFinanciados = null;
            this.PercentualDeFinanciamentoSolicitado = null;
            this.GradeAtualComDesconto = null;
            this.GradeAtualFinanciadoFIES = null;
            this.GradeAtualCoparticipacao = null;
            this.Tipo = null;

            //Dados Consultar Status Aditamento Aluno FIES Novo
            this.SemestreAno = null;
            this.Finalidade = null;
            this.Situacao = null;
            this.TipoAditamento = null;
            this.ProUni = null;
            this.DataInclusao = null;
            this.DataConclusao = null;

            //Dados SIGA
            this.CursoSiga = null;
            this.ValorDeRepasse = null;
            this.ParcelaSiga1 = null;
            this.ParcelaSiga2 = null;
            this.ParcelaSiga3 = null;
            this.ParcelaSiga4 = null;
            this.ParcelaSiga5 = null;
            this.ParcelaSiga6 = null;
        }
    }

    public class TOAlunoMap : ClassMap<TOAluno>
    {
        public TOAlunoMap(string tipo)
        {
            if (tipo == "Informações")
            {
                MapaInformacoes();
            }
            else if(tipo == "Status Aluno")
            {
                MapaStatusAluno();
            }
            else if (tipo == "SIGA")
            {
                MapaSIGA();
            }
            else
            {
                MapaPadrao();
            }
        }

        private void MapaPadrao()
        {
            Map(x => x.Cpf).Name(nameof(TOAluno.Cpf).ToUpper());
            Map(x => x.Nome).Name(nameof(TOAluno.Nome));
            Map(x => x.Tipo).Name(nameof(TOAluno.Tipo));
            Map(x => x.Conclusao).Name(nameof(TOAluno.Conclusao));
            Map(x => x.HorarioConclusao).Name(nameof(TOAluno.HorarioConclusao));
            Map(x => x.Campus).Name(nameof(TOAluno.Campus));
            Map(x => x.AproveitamentoAtual).Name(nameof(TOAluno.AproveitamentoAtual));
            Map(x => x.HistoricoAproveitamento).Name(nameof(TOAluno.HistoricoAproveitamento));
            Map(x => x.ReceitaBruta).Name(nameof(TOAluno.ReceitaBruta));
            Map(x => x.ReceitaLiquida).Name(nameof(TOAluno.ReceitaLiquida));
            Map(x => x.ReceitaFies).Name(nameof(TOAluno.ReceitaFies));
            Map(x => x.CampusAditado).Name(nameof(TOAluno.CampusAditado));
            Map(x => x.ValorAditado).Name(nameof(TOAluno.ValorAditado));
            Map(x => x.ValorAditadoComDesconto).Name(nameof(TOAluno.ValorAditadoComDesconto));
            Map(x => x.ValorAditadoFinanciamento).Name(nameof(TOAluno.ValorAditadoFinanciamento));
            Map(x => x.ValorPagoRecursoEstudante).Name(nameof(TOAluno.ValorPagoRecursoEstudante));
            Map(x => x.DescontoLiberalidade).Name(nameof(TOAluno.DescontoLiberalidade));
            Map(x => x.Extraido).Name(nameof(TOAluno.Extraido));
            Map(x => x.Justificativa).Name(nameof(TOAluno.Justificativa));
        }

        private void MapaSIGA()
        {
            Map(x => x.Cpf).Name("CPF");
            Map(x => x.Nome).Name("Nome");
            Map(x => x.Tipo).Name("Tipo");
            Map(x => x.Conclusao).Name("Conclusao");
            Map(x => x.HorarioConclusao).Name("HorarioConclusao");
            Map(x => x.CursoSiga).Name("Curso");
            Map(x => x.ParcelaSiga1).Name("PARCELA 1");
            Map(x => x.ParcelaSiga2).Name("PARCELA 2");
            Map(x => x.ParcelaSiga3).Name("PARCELA 3");
            Map(x => x.ParcelaSiga4).Name("PARCELA 4");
            Map(x => x.ParcelaSiga5).Name("PARCELA 5");
            Map(x => x.ParcelaSiga6).Name("PARCELA 6");
        }

        private void MapaStatusAluno()
        {
            Map(x => x.Cpf).Name("CPF");
            Map(x => x.SemestreAno).Name("Semestre/Ano");
            Map(x => x.Finalidade).Name("Finalidade");
            Map(x => x.Situacao).Name("Situação");
            Map(x => x.TipoAditamento).Name("Tipo");
            Map(x => x.ProUni).Name("ProUni");
            Map(x => x.DataInclusao).Name("Data Inclusão");
            Map(x => x.DataConclusao).Name("Data Conclusão");
        }

        private void MapaInformacoes()
        {
            Map(x => x.Cpf).Name("CPF");
            Map(x => x.SemestreAditar).Name("Semestre a Aditar");
            Map(x => x.Curso).Name("Curso");
            Map(x => x.DuracaoRegular).Name("Duração regular");
            Map(x => x.TotalDeSemestresSuspensos).Name("Total de semestres suspensos");
            Map(x => x.TotalDeSemestresDilatados).Name("Total de semestres dilatados");
            Map(x => x.TotalDeSemestresConcluidos).Name("Total de semestres já concluidos e/ou aproveitados nesta IES/curso");
            Map(x => x.SemestreSerCursadoPeloEstudante).Name("Semestre a ser cursado pelo estudante");
            Map(x => x.TotalDeSemestresJaFinanciados).Name("Total de semestres já financiados");
            Map(x => x.PercentualDeFinanciamentoSolicitado).Name("Percentual de financiamento solicitado");
            Map(x => x.GradeAtualComDesconto).Name("Grade Atual Semestralidade (R$) com desconto");
            Map(x => x.GradeAtualFinanciadoFIES).Name("Grade Atual Semestralidade (R$) Financiado FIES");
            Map(x => x.GradeAtualCoparticipacao).Name("Grade Atual Semestralidade (R$) Coparticipação");
        }
    }
}