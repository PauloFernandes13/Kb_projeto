using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto_KB.Models
{
    public class Faq
    {
        public int ID { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public string Question { get; set; }
        [DataType(DataType.MultilineText)]
        public string Answer { get; set; }
        public int TopicID { get; set; }
        public int SubjectID { get; set; }
        public virtual Topic Topic { get; set; } 
        public virtual Subject Subject { get; set; }

    }
}