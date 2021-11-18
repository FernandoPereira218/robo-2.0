﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Robo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robo.Control.Relatorios.SIGA
{
    class LancamentoFiesSiga : UtilSiga
    {
        private IWebDriver Driver;
        public void ExecutarLancamentoFiesSiga(TOAluno aluno, IWebDriver driver, string semestreAno, string tipoFies)
        {
            Driver = driver;
            FiltraAluno(driver, aluno);

            if (Driver.PageSource.Contains("btn_editar") == true)
            {
                ClickButtonsById(Driver, "btn_editar#0");

                ClickButtonsById(Driver, "btnComplementos");

                //Filtrar pelo semestre escolhido
                string semestreSiga = BuscarSemestreSiga(semestreAno);
                ClickDropDownExact(driver, "id", "peri_id", semestreSiga);
                string lancamentoPrimeiraLinha = "";
                var elemento = VerificarElementoExiste(Driver, "xpath", "/html/body/table/tbody/tr/td/table/tbody/tr[6]/td/div/form/table[2]/tbody/tr[3]/td[3]/span");
                if (elemento == null)
                {

                }
                else
                {
                    lancamentoPrimeiraLinha = Driver.FindElement(By.XPath("/html/body/table/tbody/tr/td/table/tbody/tr[6]/td/div/form/table[2]/tbody/tr[3]/td[3]/span")).Text;
                }

                if (lancamentoPrimeiraLinha != "FIES" && lancamentoPrimeiraLinha != "FIES CONTRATADO")
                {
                    ClickButtonsById(Driver, "btnAdicionaComplemento");

                    //Sempre o mesmo
                    ClickDropDown(Driver, "id", "nens_id", "GRADUAÇÃO");

                    //Verificação se o curso está disponível no site
                    SelectElement selectElement = new SelectElement(Driver.FindElement(By.Id("curs_id")));
                    if (selectElement.Options.Count == 1)
                    {
                        Util.EditarConclusaoAluno(aluno, "Curso não disponível no SIGA");
                        Driver.Url = Driver.Url;
                        return;
                    }
                    //Buscar da planilha do aluno
                    ClickDropDown(Driver, "id", "curs_id", aluno.CursoSiga.ToUpper());

                    //Opção no combobox 19 -> FIES || 1133 -> FIES CONTRATADO
                    if (tipoFies == "FIES")
                    {
                        ClickDropDownExact(Driver, "id", "lanc_id", "19");
                    }
                    else if (tipoFies == "FIES CONTRATADO")
                    {
                        ClickDropDownExact(Driver, "id", "lanc_id", "1133");
                    }

                    //Sempre o mesmo
                    ClickDropDownExact(Driver, "id", "tipo_valor", "moeda");

                    // Sempre o mesmo na observação
                    ClickAndWriteById(Driver, "clmt_observacao", "Lançamento Automatizado");

                    //Arredondar valor para duas casas decimais
                    ClickAndWriteById(Driver, "moeda", aluno.ValorDeRepasse);

                    ScrollToElementByID(Driver, "periodo[" + semestreSiga + "]");
                    //Criar id composto = ano+semestre+10 : 2021-2 -> 2021210
                    ClickButtonsById(Driver, "periodo[" + semestreSiga + "]");

                    ClickButtonsById(Driver, "btnAdicionaComplemento");
                }

                string parcelasJaVinculadas = Driver.FindElement(By.XPath("/html/body/table/tbody/tr/td/table/tbody/tr[6]/td/div/form/table[2]/tbody/tr[3]/td[9]")).Text;

                if (parcelasJaVinculadas == string.Empty)
                {


                    ClickButtonsByXpath(driver, "/html/body/table/tbody/tr/td/table/tbody/tr[6]/td/div/form/table[2]/tbody/tr[3]/td[10]/div/img[1]");

                    SelectElement select = new SelectElement(Driver.FindElement(By.Id("parcelas[]")));
                    select.DeselectAll();
                    select.SelectByText("1");
                    select.SelectByText("2");
                    select.SelectByText("3");
                    select.SelectByText("4");
                    select.SelectByText("5");
                    select.SelectByText("6");

                    string valorAluno = string.Empty;
                    double valorCompleto = Convert.ToDouble(aluno.ValorDeRepasse);
                    valorAluno = Math.Round((valorCompleto / 6), 2).ToString();
                    valorAluno = Dados.FormatarReceitas(valorAluno);
                    ClickAndWriteById(driver, "moeda", valorAluno);

                    ScrollToElementByID(driver, "btnVinculaComplemento");
                    ClickButtonsById(driver, "btnVinculaComplemento");

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
                else
                {
                    Util.EditarConclusaoAluno(aluno, "CADASTRO EFETUADO ANTERIORMENTE");
                }
                //Voltar para página de consulta de alunos
                Driver.Url = Driver.Url;
            }
        }
    }
}