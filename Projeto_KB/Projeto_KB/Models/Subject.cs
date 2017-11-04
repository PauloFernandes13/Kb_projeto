using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Projeto_KB.Models
{
    public class Subject
    {
        public int ID { get; set; }
        [DisplayName("Subject")]
        public string Name { get; set; }

        public virtual ICollection<Faq> Faqs { get; set; }
        public virtual ICollection<Concept> Concepts {get; set;}
     


    }
}