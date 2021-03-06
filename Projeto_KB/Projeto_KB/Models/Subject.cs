﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto_KB.Models
{
    public class Subject
    {
        public int ID { get; set; }
        [StringLength(18, ErrorMessage = "O campo relativo a Assunto não pode ter mais de 18 caracteres")]
        [DisplayName("Assunto")]
        public string Name { get; set; }
        public virtual ICollection<Faq> Faqs { get; set; }
     


    }
}