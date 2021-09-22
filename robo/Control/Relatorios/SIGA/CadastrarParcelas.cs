using OpenQA.Selenium;
using Robo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robo.Control.Relatorios.SIGA
{
    class CadastrarParcelas : UtilSiga
    {
        private IWebDriver Driver;
        public void CadastrarParcelasSiga(TOAluno aluno, IWebDriver driver)
        {
            Driver = driver;
            // Ajeitar o cpf
            ClickAndWriteById(Driver, "pess_cpf", aluno.Cpf);
            ClickButtonsById(Driver, "btn_filtrar");

            if (Driver.PageSource.Contains("btn_editar") == true)
            {
                ClickButtonsById(Driver, "btn_editar#0");

                ClickButtonsById(Driver, "btnComplementos");

                ClickButtonsById(Driver, "btnAdicionaComplemento");


                //Sempre o mesmo
                ClickDropDown(Driver, "id", "nens_id", "GRADUAÇÃO");

                //Buscar da planilha do aluno
                ClickDropDown(Driver, "id", "curs_id", aluno.CursoSiga.ToUpper());

                //Opção no combobox 19 -> FIES || 1133 -> FIES CONTRATADO
                ClickDropDownExact(Driver, "id", "lanc_id", "19");

                //Sempre o mesmo
                ClickDropDownExact(Driver, "id", "tipo_valor", "moeda");

                //Arredondar valor para duas casas decimais
                ClickAndWriteById(Driver, "moeda", aluno.ValorDeRepasse);

                ScrollToElementByID(Driver, "periodo[2021210]");
                //Criar id composto = ano+semestre+10 : 2021-2 -> 2021210
                ClickButtonsById(Driver, "periodo[2021210]");

                ClickButtonsById(Driver, "btnAdicionaComplemento");
            }
        }
    }
}
