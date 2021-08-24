using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robo.Model.TO
{
    public class Aluno
    {
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Conclusao { get; set; }
        public string HorarioConclusao { get; set; }
        public string Tipo { get; set; }

        public Aluno()
        {
            Cpf = string.Empty;
            Nome = string.Empty;
            Conclusao = "Não Feito";
            HorarioConclusao = string.Empty;
            Tipo = string.Empty;
        }
    }
}
