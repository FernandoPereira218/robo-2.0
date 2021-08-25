using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Robo
{
    /// <summary>
    /// Tranfer Object de Login.
    /// </summary>
    public class TOLogin
    {
        /// <summary>
        /// Usuário para log in.
        /// </summary>
        public String Usuario
        {
            get;
            set;
        }

        /// <summary>
        /// Senha para Log in.
        /// </summary>       
        public String Senha
        {
            get;
            set;
        }

        /// <summary>
        /// Faculdade do Login.
        /// </summary>
        public String Faculdade
        {
            get;
            set;
        }

        /// <summary>
        /// Campus do Login.
        /// </summary>
        public String Campus
        {
            get;
            set;
        }

        /// <summary>
        /// Faculdade do Login.
        /// </summary>
        public String Plataforma
        {
            get;
            set;
        }

        /// <summary>
        /// Faculdade do Login.
        /// </summary>
        public String Numero
        {
            get;
            set;
        }
        /// <summary>
        /// Faculdade do Regional.
        /// </summary>
        public String Regional
        {
            get;
            set;
        }

        public string IES { get; set; }
    }
}
