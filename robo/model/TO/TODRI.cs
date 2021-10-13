using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robo
{
    public class TODRI
    {
        /// <summary>Id para LiteDB</summary>
        [Ignore]
        public int Id { get; set; }
        public string Nome
        {
            get;
            set;
        }

        public string DRI
        {
            get;
            set;
        }

        public string Cpf
        {
            get;
            set;
        }

        public string CampusAditado
        {
            get;
            set;
        }
    }
}
