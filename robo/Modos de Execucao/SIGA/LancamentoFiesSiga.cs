using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using robo.Banco_de_Dados;
using robo.TO;
using robo.Utils;
using System;
using robo.Contratos;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robo.Modos_de_Execucao.SIGA
{
    class LancamentoFiesSiga : UtilSiga, IModosDeExecucao.IModoComAlunos
    {
        private string semestreAno;
        private string tipoLancamento;
        private string tipoValor;
        public LancamentoFiesSiga(string semestreAno, string tipoLancamento, string tipoValor)
        {
            this.semestreAno = semestreAno;
            this.tipoLancamento = tipoLancamento;
            this.tipoValor = tipoValor;
        }
        public void ExecutarLancamentoFiesSiga(TOAluno aluno)
        {
            FiltraAluno(aluno);

            if (Driver.PageSource.Contains("btn_editar") == true)
            {
                ClicarElemento(By.Id("btn_editar#0"));

                ClicarElemento(By.Id("btnComplementos"));

                //Filtrar pelo semestre escolhido
                string semestreSiga = BuscarSemestreSiga(semestreAno);
                SelecionarOpcaoDropDownExato("id", "peri_id", semestreSiga);
                string lancamentoPrimeiraLinha = BuscarLancamentoPrimeiraLinha();

                //if (lancamentoPrimeiraLinha != "FIES" && lancamentoPrimeiraLinha != "FIES CONTRATADO")
                //{
                //    AdicionarComplemento(aluno, tipoLancamento, semestreSiga);
                //}

                string lancamentoFormatado = FormatarLancamento(tipoLancamento);
                if (lancamentoPrimeiraLinha != lancamentoFormatado)
                {
                    AdicionarComplemento(aluno, tipoLancamento, semestreSiga);
                }

                if (aluno.Conclusao != "Não Feito")
                {
                    return;
                }

                string parcelasJaVinculadas = Driver.FindElement(By.XPath("/html/body/table/tbody/tr/td/table/tbody/tr[6]/td/div/form/table[2]/tbody/tr[3]/td[9]")).Text;

                if (parcelasJaVinculadas == string.Empty)
                {
                    AdicionarParcelas(aluno);
                }
                else
                {
                    Util.EditarConclusaoAluno(aluno, "CADASTRO EFETUADO ANTERIORMENTE");
                }
                //Voltar para página de consulta de alunos
                Driver.Url = Driver.Url;
            }
        }

        private string FormatarLancamento(string tipoLancamento)
        {
            tipoLancamento = tipoLancamento.Replace(" (-)", "");
            tipoLancamento = tipoLancamento.Replace(" (+)", "");

            return tipoLancamento;
        }

        private string BuscarLancamentoPrimeiraLinha()
        {
            string lancamentoPrimeiraLinha = "";
            var elemento = VerificarElementoExiste(By.XPath("/html/body/table/tbody/tr/td/table/tbody/tr[6]/td/div/form/table[2]/tbody/tr[3]/td[3]/span"));
            if (elemento != null)
            {
                lancamentoPrimeiraLinha = Driver.FindElement(By.XPath("/html/body/table/tbody/tr/td/table/tbody/tr[6]/td/div/form/table[2]/tbody/tr[3]/td[3]/span")).Text;
            }

            return lancamentoPrimeiraLinha;
        }

        private void AdicionarParcelas(TOAluno aluno)
        {
            ClicarElemento(By.XPath("/html/body/table/tbody/tr/td/table/tbody/tr[6]/td/div/form/table[2]/tbody/tr[3]/td[10]/div/img[1]"));

            SelectElement select = new SelectElement(Driver.FindElement(By.Id("parcelas[]")));
            select.DeselectAll();
            select.SelectByText("1");
            select.SelectByText("2");
            select.SelectByText("3");
            select.SelectByText("4");
            select.SelectByText("5");
            select.SelectByText("6");

            double valorCompleto = Convert.ToDouble(aluno.ValorDeRepasse);
            string valorAluno = Math.Round(valorCompleto / 6, 2).ToString();
            valorAluno = Dados.FormatarReceitas(valorAluno);
            //aluno.FormatarReceitas(valorAluno);
            ClicarEEscrever(By.Id("moeda"), valorAluno);
            ScrollParaElemento(By.Id("btnVinculaComplemento"));
            ClicarElemento(By.Id("btnVinculaComplemento"));

            IWebElement mensagemDoSistema = Driver.FindElement(By.Id("msg_1"));
            if (mensagemDoSistema.Text.ToUpper().Contains("CADASTRO EFETUADO COM SUCESSO"))
            {
                Util.EditarConclusaoAluno(aluno, "CADASTRO EFETUADO COM SUCESSO");
            }
            else
            {
                Util.EditarConclusaoAluno(aluno, "Erro");
            }
        }

        private void AdicionarComplemento(TOAluno aluno, string tipoLancamento, string semestreSiga)
        {
            ClicarElemento(By.Id("btnAdicionaComplemento"));

            //Sempre o mesmo
            SelecionarOpcaoDropDown("id", "nens_id", "GRADUAÇÃO");

            //Verificação se o curso está disponível no site
            SelectElement selectElement = new SelectElement(Driver.FindElement(By.Id("curs_id")));
            if (selectElement.Options.Count == 1)
            {
                Util.EditarConclusaoAluno(aluno, "Curso não disponível no SIGA");
                Driver.Url = Driver.Url;
                return;
            }
            //Buscar da planilha do aluno
            SelecionarOpcaoDropDown("id", "curs_id", aluno.CursoSiga.ToUpper());

            //Opção no combobox 19 -> FIES || 1133 -> FIES CONTRATADO
            //SelecionarOpcaoDropDown("id", "lanc_id", tipoLancamento);
            SelectElement selectLancamento = new SelectElement(Driver.FindElement(By.Id("lanc_id")));
            selectLancamento.SelectByText(tipoLancamento);

            //Sempre o mesmo
            //SelecionarOpcaoDropDownExato("id", "tipo_valor", "moeda");
            SelecionarOpcaoDropDownPorTexto(By.Id("tipo_valor"), tipoValor);

            // Sempre o mesmo na observação
            ClicarEEscrever(By.Id("clmt_observacao"), "Lançamento Automatizado");

            //Arredondar valor para duas casas decimais
            if (tipoValor == "Valor")
            {
                ClicarEEscrever(By.Id("moeda"), aluno.ValorDeRepasse);
            }
            else
            {
                throw new Exception("Ainda não feito");
            }

            ScrollParaElemento(By.Id("periodo[" + semestreSiga + "]"));
            //Criar id composto = ano+semestre+10 : 2021-2 -> 2021210
            ClicarElemento(By.Id("periodo[" + semestreSiga + "]"));

            ClicarElemento(By.Id("btnAdicionaComplemento"));
        }

        public void Executar(TOAluno aluno)
        {
            ExecutarLancamentoFiesSiga(aluno);
        }

        public void SelecionarMenu()
        {
            throw new NotImplementedException();
        }

        public void SetWebDriver(IWebDriver Driver)
        {
            this.Driver = Driver;
        }
    }
}
