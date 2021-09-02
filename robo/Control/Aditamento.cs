using OpenQA.Selenium;
using robo.Control.Legado;
using Robo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robo.Control
{
    public class Aditamento
    {

        static IWebDriver Driver;
        private UtilFiesLegado fiesLegadoutil = new UtilFiesLegado();
        private void AditamentoFiesLegado(TOLogin login, TOAluno aluno, string numSemestre)
        {
            Driver = Util.StartBrowser("http://sisfies.mec.gov.br/");

            fiesLegadoutil.RealizarLoginSucesso(login, Driver);

            fiesLegadoutil.SelecionarPerfilPresidencia(Driver);

            //Aqui começa o aditamento
            //            MetodoAditamento(login, alunos, numSemestre);

            Util.ClickButtonsByCss(Driver, "div:nth-child(3) > ul > .menu-button:nth-child(2) > a");
            if (Dados.DRIExists(aluno.Cpf))
            {
                TODRI driAtual = Dados.GetDRI(aluno.Cpf);
                string url = string.Format("http://sisfies.mec.gov.br/cpsa/aditamento/formulario/co_inscricao/{0}/sem/{1}", driAtual.DRI, numSemestre);
                Driver.Url = url;



            }
        }
    }
}
