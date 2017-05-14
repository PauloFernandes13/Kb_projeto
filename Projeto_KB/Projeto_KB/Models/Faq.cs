using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto_KB.Models
{
    public class Faq
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int JourneyID { get; set; }

        public virtual Journey Journey { get; set; }

    }
}