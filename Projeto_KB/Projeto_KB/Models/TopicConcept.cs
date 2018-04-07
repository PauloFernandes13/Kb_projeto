using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto_KB.Models
{
    public class TopicConcept
    {
        public int ID { get; set; }
        [DisplayName("Tópicos BEST")]
        public string Name { get; set; }
        public int Order { get; set; }
        public int? PhaseID { get; set; }

        public virtual ICollection<Concept> Concepts { get; set; }
        public virtual Phase Phases { get; set; }
    }
}