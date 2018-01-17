using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto_KB.Models
{
    public class Topic
    {
        public int ID { get; set; }
        [DisplayName("Tópico")]
        public string Name { get; set; }
        public virtual ICollection<Faq> Faqs { get; set; }


    }
}