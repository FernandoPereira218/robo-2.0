using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robo
{
    public class TOMenus
    {
        public string ComboBox { get; set; }
        public string Item { get; set; }
        public int Ordem { get; set; }
        public string Permisao { get; set; }
        public string Modalidade { get; set; }
        public string Regional { get; set; }

        /// <summary>
        /// Contrutor da classe TOAluno que garante que não exista propriedade nula.
        /// </summary>
        public TOMenus()
        {
            this.ComboBox = string.Empty;
            this.Item = string.Empty;
            this.Ordem = 1;
            this.Permisao = string.Empty;
            this.Modalidade = string.Empty;
            this.Regional = string.Empty;
        }
    }
}
