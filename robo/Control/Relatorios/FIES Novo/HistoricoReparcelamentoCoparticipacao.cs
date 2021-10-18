﻿using OpenQA.Selenium;
using Robo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robo.Control.Relatorios.FIES_Novo
{
    class HistoricoReparcelamentoCoparticipacao : UtilFiesNovo
    {
        IWebDriver Driver;
        public void ExecutarHistoricoReparcelamentoCoparticipacao(IWebDriver driver, TOAluno aluno)
        {
            Driver = driver;



            WaitForLoading(driver);
            ClickButtonsById(driver, "btnLimpar");
            WaitForLoading(driver);

            ClickAndWriteById(driver, "cpfEstudante", aluno.Cpf);
            ClickButtonsById(Driver, "btnConsultar");
            WaitForLoading(driver);


            var verificarErro = VerificarElementoExiste(driver, "CLASSNAME", "alert alert-error");
            string responseVerificarErro = verificarErro.Text.Replace("x\r\n", "");
            if (verificarErro != null)
            {
                Util.EditarConclusaoAluno(aluno, "Erro na Busca: " + responseVerificarErro);
                return;
            }
            string nome = Driver.FindElement(By.XPath("//*[@id=\"gridResult\"]/tbody/tr[1]/td[2]")).Text;
            if (Driver.PageSource.Contains("Nenhuma informação disponível") == false)
            {
                ListaParaCSV(nome + "_Histórico_Coparticipação", "gridResult_length", "gridResult", true);
                Util.EditarConclusaoAluno(aluno, "Histórico do Aluno Processado com Sucesso");
            }
            else
            {
                Util.EditarConclusaoAluno(aluno, "Nenhum Histórico do Aluno não encontrado");
            }
        }
        private void ListaParaCSV(string fileName, string idDropdown, string idTabela, bool status)
        {
            ClickDropDown(Driver, "name", idDropdown, "100");
            IWebElement elementoTabela = Driver.FindElement(By.Id(idTabela));
            List<IWebElement> cabecalhos = elementoTabela.FindElements(By.TagName("th")).ToList();
            List<IWebElement> dados = elementoTabela.FindElements(By.TagName("td")).ToList();
            string arquivo;
            if (status == true)
            {
                var downloadFolder = Util.GetDownloadsFolderPath();
                downloadFolder = downloadFolder + "\\Historico Reparcelamento Coparticipação";
                Util.CreateDirectory(downloadFolder);
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
    }
}