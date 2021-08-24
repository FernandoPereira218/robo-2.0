using robo.Model.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robo.pgm
{
    public class TOAlunoInf : Aluno
    {
        //public string Cpf { get; set; }
        //public string Nome { get; set; }
        public string Campus { get; set; }
        //public string Conclusao { get; set; }
        //public string HorarioConclusao { get; set; }
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
        //public string Tipo { get; set; }

        /// <summary>
        /// Contrutor da classe TOAluno que garante que não exista propriedade nula.
        /// </summary>
        public TOAlunoInf()
        {
            this.Cpf = String.Empty;
            this.Nome = String.Empty;
            this.Campus = String.Empty;
            this.Conclusao = "Não Feito";
            this.HorarioConclusao = String.Empty;
            this.SemestreAditar = String.Empty;
            this.Curso = String.Empty;
            this.DuracaoRegular = String.Empty;
            this.TotalDeSemestresSuspensos = String.Empty;
            this.TotalDeSemestresDilatados = String.Empty;
            this.TotalDeSemestresConcluidos = String.Empty;
            this.SemestreSerCursadoPeloEstudante = String.Empty;
            this.TotalDeSemestresJaFinanciados = String.Empty;
            this.PercentualDeFinanciamentoSolicitado = String.Empty;
            this.GradeAtualComDesconto = String.Empty;
            this.GradeAtualFinanciadoFIES = String.Empty;
            this.GradeAtualCoparticipacao = String.Empty;
            this.Tipo = String.Empty;
        }
    }
}
