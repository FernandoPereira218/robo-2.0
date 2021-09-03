using Robo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robo.Control.Implementacoes
{
    public interface IContratos
    {

        interface IMainForms
        {

        }
        interface IPresenter
        {
            List<string> PreencherListaSemestre();
            void SetForm(IContratos.IMainForms forms);
            string BuscarNunSemestre(string semestreAno);
            List<string> PreencherListaExecucao();
            List<string> PreencherListaExecucaoPorPlataforma(string plataforma);
            void TratarDadosAluno(TOAluno aluno);
            void ExecutarAditamento(string semestreAtual, string faculdade, string tipoFies, string campus);
        }
    }
}
