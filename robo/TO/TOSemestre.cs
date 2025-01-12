﻿using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robo.TO
{
    /// <summary>
    /// Semestres para extração Docs
    /// </summary>
    public class TOSemestre
    {

        public TOSemestre()
        {
  
        }

        /// <summary>Id para LiteDB</summary>
        [Ignore]
        public int Id { get; set; }
        /// <summary>
        /// Semestres para extração Docs.
        /// </summary>
        public String Semestre
        {
            get;
            set;
        }
        /// <summary>
        /// Semestres para extração Docs.
        /// </summary>
        public String numSemestre
        {
            get;
            set;
        }
    }
}
