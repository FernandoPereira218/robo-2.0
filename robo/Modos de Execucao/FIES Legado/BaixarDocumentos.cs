using OpenQA.Selenium;
using robo.TO;
using robo.Utils;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace robo.Modos_de_Execucao.FIES_Legado
{
    public class BaixarDocumentos: UtilFiesLegado
    {
        private IWebDriver Driver;

        public void BaixarDocumentoFiesLegado(IWebDriver driver, TOAluno aluno, string semestre, string tipoRelatorio)
        {
            Driver = driver;
            ConsultarDocumentoAluno(aluno, semestre, tipoRelatorio);
            string situacaoAluno;
            if (Driver.PageSource.Contains("Lista de Aditamentos"))
            {
                situacaoAluno = Driver.FindElement(By.XPath("/html/body/div[3]/div[4]/div[2]/div[2]/div[4]/table/tbody/tr/td[6]")).Text;
                ClickButtonsByCss(Driver, "td > a > img");
                IWebElement botaoImprimir = BuscarBotaoImprimir(tipoRelatorio);
                BaixarDocumentoAluno(aluno, semestre, tipoRelatorio, situacaoAluno, botaoImprimir);
            }
        }
        private void BaixarDocumentoAluno(TOAluno aluno, string semestre, string tipoRelatorio, string situacaoAluno, IWebElement botaoImprimir)
        {
            if (botaoImprimir != null)
            {
                WaitinLoading(Driver);
                string msgErro = VerificarMensagem(Driver);
                if (msgErro == string.Empty)
                {
                    BaixarDocumento(aluno, semestre, tipoRelatorio);
                    ClickButtonsById(Driver, "voltar");
                }
            }
            else
            {
                ScrollToElementByID(Driver, "voltar");
                ClickButtonsById(Driver, "voltar");
                Util.EditarConclusaoAluno(aluno, situacaoAluno);
            }
        }
        private void BaixarDocumento(TOAluno aluno, string semestre, string tipoRelatorio)
        {
            aluno.Nome = BuscarNomeAluno();
            string simplificado = string.Empty;
            if (tipoRelatorio == "DRM")
            {
                ClickButtonsById(Driver, "imprimirDrm");
                simplificado = FindElementByXpathText(Driver, "span", "Simplificado").Text;
                simplificado = simplificado.Replace("Tipo de Aditamento: ", "");
            }
            else
            {
                ClickButtonsById(Driver, "imprimir");
            }
            
            Util.BaixarDocumento(aluno.Nome + "_" + aluno.Cpf + "_" + semestre.Replace("/", "-") + "_" + tipoRelatorio, tipoRelatorio, simplificado);
            
            Util.EditarConclusaoAluno(aluno, string.Format("{0} - {1}", tipoRelatorio + " Baixado", simplificado.Trim()));
        }
        private IWebElement BuscarBotaoImprimir(string tipoRelatorio)
        {
            IWebElement botaoImprimir;
            if (tipoRelatorio == "DRM")
            {
                botaoImprimir = VerificarElementoExiste(Driver, "ID", "imprimirDrm");
            }
            else
            {
                botaoImprimir = VerificarElementoExiste(Driver, "ID", "imprimir");
            }

            return botaoImprimir;
        }
        private void ConsultarDocumentoAluno(TOAluno aluno, string semestre, string tipoRelatorio)
        {
            SelecionarTipoRelatorio(Driver, tipoRelatorio);
            WaitinLoading(Driver);
            ClickAndWriteById(Driver, "cpf", aluno.Cpf);
            ClickDropDown(Driver, "id", "coSemestreAditamento", semestre);
            WaitinLoading(Driver);
            ClickButtonsById(Driver, "consultar");
        }
        private string BuscarNomeAluno()
        {
            string nome = Driver.PageSource;
            if (Driver.PageSource.Contains("Nome completo:</strong>") == true)
            {
                nome = nome.Split(new string[] { "Nome completo:</strong>" }, StringSplitOptions.None)[1];
            }
            else
            {
                nome = nome.Split(new string[] { "Nome Completo:</strong>" }, StringSplitOptions.None)[1];
            }
            nome = nome.Split(new string[] { "</span>" }, StringSplitOptions.None)[0];
            return nome;
        }
    }
}