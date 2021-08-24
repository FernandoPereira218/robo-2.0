using System;
using System.ComponentModel;
using robo.Model.TO;

namespace Robo
{
    public class TOAluno : Aluno
    {
        /// <summary>CPF do aluno.</summary>  
        //public string Cpf { get; set; }

        /// <summary>Campus no qual o aluno estuda.</summary>  
        public string Campus { get; set; }

        /// <summary>Nome do aluno.</summary>  
        //public string Nome { get; set; }

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

        /// <summary>Tipo do fies.</summary>
        //public string Tipo { get; set; }

        /// <summary>Status de retorno do antedimento.</summary>
        //public string Conclusao { get; set; }

        /// <summary>Campus Aditado.</summary>
        public string CampusAditado { get; set; }

        /// <summary>Quais campus esse aluno ja foi verificado.</summary>
        public string NumCampusAtual { get; set; }

        /// <summary>Receita liquida.</summary>
        public string ValorAditado { get; set; }
        public string ValorAditadoComDesconto { get; set; }
        public string ValorAditadoFinanciamento { get; set; }
        public string ValorPagoRecursoEstudante { get; set; }
        //public string HorarioConclusao { get; set; }

        /// <summary>Possui alteração na receita liquida para se encaixar nos 95%? </summary>
        //public bool PossuiAlteracaoPorcentagemReceita { get; set; }
        public string DescontoLiberalidade { get; set; }
        public string Extraido { get; set; }
        public string Justificativa { get; set; }

        /// <summary>
        /// Contrutor da classe TOAluno que garante que não exista propriedade nula.
        /// </summary>
        public TOAluno()
        {
            this.Cpf = String.Empty;
            //this.Curso = String.Empty;
            this.Campus = String.Empty;
            this.Nome = String.Empty;
            this.HistoricoAproveitamento = String.Empty;
            this.ReceitaBruta = String.Empty;
            this.ReceitaLiquida = String.Empty;
            this.ReceitaFies = String.Empty;
            this.ValorAditado = String.Empty;
            this.CampusAditado = String.Empty;
            this.NumCampusAtual = "1";
            this.Tipo = String.Empty;
            this.Conclusao = "Não Feito";
            this.AproveitamentoAtual = String.Empty;
            this.ValorAditadoComDesconto = String.Empty;
            this.ValorAditadoFinanciamento = String.Empty;
            this.ValorPagoRecursoEstudante = String.Empty;
            this.HorarioConclusao = String.Empty;
            this.DescontoLiberalidade = String.Empty;
            this.Extraido = "Não";
            this.Justificativa = String.Empty;
        }
    }
}