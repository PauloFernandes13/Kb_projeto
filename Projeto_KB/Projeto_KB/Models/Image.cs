using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto_KB.Models
{
    public class Image
    {
        public int ID { get;  set}
        public string Name { get; set; }
        public string UrlImage { get; set; }
        public int ConceptID { get; set; }

        public virtual Concept Concept { get; set; } //navigation property from foreign Key, one image associated to one Concept

    }
}
