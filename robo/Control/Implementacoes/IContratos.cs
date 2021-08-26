using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robo.Control.Implementacoes
{
    public interface IContratos
    {

        interface IMainForms
        {

        }
        interface IPresenter
        {
            List<string> PreencherListaSemestre();
            void SetForm(IContratos.IMainForms forms);
            string BuscarNunSemestre(string semestreAno);
        }
    }
}
