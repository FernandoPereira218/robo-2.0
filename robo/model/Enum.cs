using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robo
{
    public enum TipoFinanciamento
    {
        Antigo = 0,
        Novo = 1,
        //Pravaler = 2,
        //Credies = 3
    }

    public enum TipoExecucao
    {
        Aditamento = 0,
        DRM = 1,
        DRI = 2,
        DRT = 3,
        DRD = 4,
        Suspensao = 5
    }
}