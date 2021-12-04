using OpenQA.Selenium;
using robo.TO;
using robo.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using robo.Contratos;
using System.Text;
using System.Threading.Tasks;

namespace robo.Modos_de_Execucao.FIES_Novo
{
    class HistoricoReparcelamentoCoparticipacao : UtilFiesNovo, IModosDeExecucao.IModoComAlunos
    {

        public HistoricoReparcelamentoCoparticipacao()
        {
        }
        public void ExecutarHistoricoReparcelamentoCoparticipacao(TOAluno aluno)
        {
            WaitForLoading();
            ClickButtonsById( "btnLimpar");
            WaitForLoading();

            ClickAndWriteById( "cpfEstudante", aluno.Cpf);
            ClickButtonsById("btnConsultar");
            WaitForLoading();

            string erro = BuscarMensagemDeErro();
            if (erro != string.Empty)
            {
                Util.EditarConclusaoAluno(aluno, "Erro na Busca: " + erro);
                return;
            }
            if (this.Driver.PageSource.Contains("Nenhuma informação disponível") == false)
            {
                string nome = this.Driver.FindElement(By.XPath("//*[@id=\"gridResult\"]/tbody/tr[1]/td[2]")).Text;
                ListaParaCSV(nome + "_Histórico_Coparticipação", "gridResult_length", "gridResult", true);
                Util.EditarConclusaoAluno(aluno, "Histórico do Aluno Processado com Sucesso");
            }
            else
            {
                Util.EditarConclusaoAluno(aluno, "Nenhuma informação disponível");
            }
        }
        private void ListaParaCSV(string fileName, string idDropdown, string idTabela, bool status)
        {
            ClickDropDown( "name", idDropdown, "100");
            IWebElement elementoTabela = Driver.FindElement(By.Id(idTabela));
            List<IWebElement> cabecalhos = elementoTabela.FindElements(By.TagName("th")).ToList();
            List<IWebElement> dados = elementoTabela.FindElements(By.TagName("td")).ToList();
            string arquivo;
            if (status == true)
            {
                var downloadFolder = Util.GetDownloadsFolderPath();
                downloadFolder = downloadFolder + "\\Historico Reparcelamento Coparticipação";
                Util.CriarDiretorioCasoNaoExista(downloadFolder);
                arquivo = downloadFolder + "\\" + fileName + ".csv";
            }
            else
            {
                arquivo = fileName + ".csv";
            }
            if (File.Exists(arquivo))
            {
                File.Delete(arquivo);
            }
            EscreverCabecalhos(cabecalhos, arquivo);
            EscreverDados(cabecalhos, dados, arquivo);
        }

        private static void EscreverDados(List<IWebElement> cabecalhos, List<IWebElement> dados, string arquivo)
        {
            int contador = 0;
            for (int i = 0; i < dados.Count(); i++)
            {
                using (StreamWriter t = new StreamWriter(arquivo, true, UTF8Encoding.UTF8))
                {
                    if (contador == cabecalhos.Count() - 1)
                    {
                        t.Write(dados[i].Text);
                        t.Write("\n");
                    }
                    else
                    {
                        t.Write(dados[i].Text + ";");
                    }
                }
                if (contador == cabecalhos.Count() - 1)
                {
                    contador = 0;
                }
                else
                {
                    contador++;
                }
            }
        }

        private static void EscreverCabecalhos(List<IWebElement> cabecalhos, string arquivo)
        {
            for (int i = 0; i < cabecalhos.Count(); i++)
            {
                using (StreamWriter t = new StreamWriter(arquivo, true, UTF8Encoding.UTF8))
                {
                    if (i == cabecalhos.Count() - 1)
                    {
                        t.Write(cabecalhos[i].Text);
                        t.Write("\n");
                    }
                    else
                    {
                        t.Write(cabecalhos[i].Text + ";");
                    }
                }

            }
        }

        public void Executar(TOAluno aluno)
        {
            ExecutarHistoricoReparcelamentoCoparticipacao(aluno);
        }

        public void SelecionarMenu()
        {
            ClicarMenuHistoricoReparcelamentoCopartipacao();
        }

        public void SetWebDriver(IWebDriver Driver)
        {
            this.Driver = Driver;
        }
    }
}
