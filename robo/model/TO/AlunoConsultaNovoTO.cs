using robo.Model.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robo
{
    public class AlunoConsultaNovoTO : Aluno
    { 
        /// <summary>Cpf do aluno</summary>
        //public string Cpf { get; set; }

        /// <summary></summary>
        public string SemestreAno { get; set; }
        public string Finalidade { get; set; }
        public string Situacao { get; set; }
        //public string Tipo { get; set; }
        public string ProUni { get; set; }
        public string DataInclusao { get; set; }
        public string DataConclusao { get; set; }
        //public string HorarioConclusao { get; set; }

        public AlunoConsultaNovoTO()
        {   
            Cpf = string.Empty;
            SemestreAno = string.Empty;
            Finalidade = string.Empty;
            Situacao = string.Empty;
            Tipo = string.Empty;
            ProUni = string.Empty;
            DataInclusao = string.Empty;
            DataConclusao = string.Empty;
            HorarioConclusao = string.Empty;
        }
    }
}
