using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto_KB.Models
{
    public class Phase
    {
        public int ID { get; set; }
        [DisplayName("Etapas BEST")]
        public string Name { get; set; }
        public int Order { get; set; }
        public virtual ICollection<Concept> Concepts { get; set; }


    }
}