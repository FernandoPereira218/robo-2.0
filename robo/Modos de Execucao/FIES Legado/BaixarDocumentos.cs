using OpenQA.Selenium;
using robo.Contratos;
using robo.TO;
using robo.Utils;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace robo.Modos_de_Execucao.FIES_Legado
{
    public class BaixarDocumentos: UtilFiesLegado, IModosDeExecucao.IModoComAlunos
    {
        private string semestre;
        private string tipoRelatorio;

        public BaixarDocumentos(string semestre, string tipoRelatorio)
        {
            this.semestre = semestre;
            this.tipoRelatorio = tipoRelatorio;
        }

        public void BaixarDocumentoFiesLegado(TOAluno aluno)
        {
            ConsultarDocumentoAluno(aluno, semestre, tipoRelatorio);
            string situacaoAluno;
            if (Driver.PageSource.Contains("Lista de Aditamentos"))
            {
                situacaoAluno = Driver.FindElement(By.XPath("/html/body/div[3]/div[4]/div[2]/div[2]/div[4]/table/tbody/tr/td[6]")).Text;
                ClickButtonsByCss( "td > a > img");
                IWebElement botaoImprimir = BuscarBotaoImprimir(tipoRelatorio);
                BaixarDocumentoAluno(aluno, semestre, tipoRelatorio, situacaoAluno, botaoImprimir);
            }
        }
        private void BaixarDocumentoAluno(TOAluno aluno, string semestre, string tipoRelatorio, string situacaoAluno, IWebElement botaoImprimir)
        {
            if (botaoImprimir != null)
            {
                WaitinLoading();
                string msgErro = VerificarMensagem();
                if (msgErro == string.Empty)
                {
                    BaixarDocumento(aluno, semestre, tipoRelatorio);
                    ClickButtonsById( "voltar");
                }
            }
            else
            {
                ScrollToElementByID( "voltar");
                ClickButtonsById( "voltar");
                Util.EditarConclusaoAluno(aluno, situacaoAluno);
            }
        }
        private void BaixarDocumento(TOAluno aluno, string semestre, string tipoRelatorio)
        {
            aluno.Nome = BuscarNomeAluno();
            string simplificado = string.Empty;
            if (tipoRelatorio == "DRM")
            {
                ClickButtonsById( "imprimirDrm");
                simplificado = FindElementByXpathText( "span", "Simplificado").Text;
                simplificado = simplificado.Replace("Tipo de Aditamento: ", "");
            }
            else
            {
                ClickButtonsById( "imprimir");
            }
            
            Util.BaixarDocumento(aluno.Nome + "_" + aluno.Cpf + "_" + semestre.Replace("/", "-") + "_" + tipoRelatorio, tipoRelatorio, simplificado);
            
            Util.EditarConclusaoAluno(aluno, string.Format("{0} - {1}", tipoRelatorio + " Baixado", simplificado.Trim()));
        }
        private IWebElement BuscarBotaoImprimir(string tipoRelatorio)
        {
            IWebElement botaoImprimir;
            if (tipoRelatorio == "DRM")
            {
                botaoImprimir = VerificarElementoExiste( "ID", "imprimirDrm");
            }
            else
            {
                botaoImprimir = VerificarElementoExiste( "ID", "imprimir");
            }

            return botaoImprimir;
        }
        private void ConsultarDocumentoAluno(TOAluno aluno, string semestre, string tipoRelatorio)
        {
            SelecionarTipoRelatorio( tipoRelatorio);
            WaitinLoading();
            ClickAndWriteById( "cpf", aluno.Cpf);
            ClickDropDown( "id", "coSemestreAditamento", semestre);
            WaitinLoading();
            ClickButtonsById( "consultar");
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

        public void Executar(TOAluno aluno)
        {
            BaixarDocumentoFiesLegado(aluno);
        }

        public void SelecionarMenu()
        {
            SelecionarMenuBaixarDocumentos();
        }

        public void SetWebDriver(IWebDriver Driver)
        {
            this.Driver = Driver;
        }
    }
}