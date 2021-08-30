using robo.pgm;
using Robo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robo.Control.Implementacoes
{
    public class ImplementacaoPresenter : IContratos.IPresenter
    {
        private IContratos.IMainForms forms;
        public ImplementacaoPresenter(IContratos.IMainForms forms)
        {
            SetForm(forms);
        }
            
        public string BuscarNunSemestre(string semestreAno)
        {
            List<TOSemestre> semestre = Dados.SelectSemestre();
            foreach (var item in semestre)
            {
                if (semestreAno == item.Semestre)
                {
                    return item.numSemestre;
                }
            }
            return null;
        }

        public List<string> PreencherListaExecucao()
        {
            List<TOMenus> menus = Dados.SelectMenus();
            List<string> nomeMenu = new List<string>();
            foreach (var item in menus)
            {
                nomeMenu.Add(item.Item);
            }
            return nomeMenu;
        }

        public List<string> PreencherListaExecucaoPorPlataforma(string plataforma)
        {
            List<TOMenus> menus = Dados.SelectMenuWhere(plataforma);
            List<string> nomeMenu = new List<string>();
            foreach (var item in menus)
            {
                nomeMenu.Add(item.Item);
            }
            return nomeMenu;
        }

        public List<string> PreencherListaSemestre()
        {
            List<TOSemestre> semestre = Dados.SelectSemestre();
            List<string> nomeSemestre = new List<string>();
            foreach (var item in semestre)
            {
                nomeSemestre.Add(item.Semestre);
            }
            return nomeSemestre;
        }

        public void SetForm(IContratos.IMainForms forms)
        {
            this.forms = forms;
        }

        public void TratarDadosAluno(TOAluno aluno)
        {
            Dados.TratarCpf(aluno);
            Dados.TratarTextoReceitas(aluno);
            Dados.TratarVirgulaReceitas(aluno);
            Dados.TratarCampusAluno(aluno);
            Dados.TratarTipoFIES(aluno);
        }
    }
}
