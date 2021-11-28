using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robo.Excessoes
{
    class PararExecucaoException : Exception
    {
        public PararExecucaoException() : base()
        {

        }

        public PararExecucaoException(string mensagem) : base(mensagem)
        {
        }
    }
}
