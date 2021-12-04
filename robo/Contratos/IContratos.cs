using robo.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robo.Contratos
{
    public interface IContratos
    {
        interface IPresenter
        {
            void ExecutarAbrirSite(string faculdade, string campus, string plataforma);
            List<string> PreencherListaSemestre();
            List<string> PreencherListaAno();
            string BuscarNumSemestre(string semestreAno);
            List<string> PreencherListaExecucao();
            List<string> PreencherListaExecucaoPorPlataforma(string plataforma);
            void ExecutarAditamentoLegado(string semestreAtual, string faculdade, string tipoFies, string campus);
            void ExecutarAditamentoNovo(string faculdade, string tipoFies, string semestreAtual);
            void ExecutarDRI(string faculdade, string tipoFies, string campus, string situacaoDRI, bool baixarDRI);
            void ExecutarBaixarDocumentoLegado(string faculdade, string tipoFies, string campus, string semestre, string tipoDocumento);
            void ExecutarExportarRelatoriosLegado(string faculdade, string tipoFies, string campus, string semestre, string tipoDocumento);
            void ExtrairInformacoesDRMLegado(string faculdade, string tipoFies, string campus, string semestre);
            void ExecutarExportarDRILegado(string faculdade, string tipoFies, string campus, string situacaoDRI);
            void ExportarExtratoMensalDeRepasseLegado(string faculdade, string tipoFies, string campus, string ano, string mes);
            void ExecutarBaixarDRMFiesNovo(string faculdade, string tipoFies, string semestre);
            void ExtrairInformacoesDRMFiesNovo(string faculdade, string tipoFies, string semestre);
            void ExecutarBuscarStatusAditamentoNovo(string faculdade, string tipoFies, string semestre);
            void ExecutarStatusAluno(string faculdade, string tipoFies, string semestre);
            void ExportarRelatorioFiesNovo(string faculdade, string tipoFies, string tipoRelatorio);
            void ExportarInadimplencia(string faculdade, string mes, string ano, bool todosMeses);
            void ExportarRepasseFiesNovo(string faculdade, string mes, string ano);
            void ExportarCoparticipacaoFiesNovo(string faculdade, string dataInicial, string dataFinal);
            void ExecutarLancamentoFiesSiga(string semestre, string tipoFies);
            void GeracaoParcelasFies(string semestre);
            void ExecutarHistoricoReparcelamentoCoparticipacao(string faculdade, string tipoFIES);
            void ValidarReparcelamento(string faculdade, string tipoFIES);
        }
    }
}
