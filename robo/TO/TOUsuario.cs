using CsvHelper.Configuration.Attributes;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robo
{
    public class TOUsuario
    {
        /// <summary>Id para LiteDB</summary>
        [Ignore]
        public int Id { get; set; }
        public string Usuario
        {
            get;
            set;
        }
        public string Senha
        {
            get;
            set;
        }
        public string Permissao
        {
            get;
            set;
        }
        public string IES
        {
            get;
            set;
        }
        [Ignore]
        public string Regional
        {
            get;
            set;
        }
    }
}
