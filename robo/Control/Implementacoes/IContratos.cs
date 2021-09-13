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
            void ExecutarAditamentoLegado(string semestreAtual, string faculdade, string tipoFies, string campus);
            void ExecutarAditamentoNovo(string faculdade, string tipoFies);
            void ExecutarDRI(string faculdade, string tipoFies, string campus, string situacaoDRI, bool baixarDRI);
            void ExecutarBaixarDocumentoLegado(string faculdade, string tipoFies, string campus, string semestre, string tipoDocumento);
            void ExecutarExportarRelatoriosLegado(string faculdade, string tipoFies, string campus, string semestre, string tipoDocumento);
            void ExtrairInformacoesDRMLegado(string faculdade, string tipoFies, string campus, string semestre);
            void ExtrairInformacoesDRILegado(string faculdade, string tipoFies, string campus, string situacaoDRI);
            void ExecutarExportarDRILegado(string faculdade, string tipoFies, string campus, string situacaoDRI);
            void ExportarExtratoMensalDeRepasseLegado(string faculdade, string tipoFies, string campus, string ano, string mes);

            void ExecutarBaixarDRMFiesNovo(string faculdade, string tipoFies, string semestre);
        }
    }
}
