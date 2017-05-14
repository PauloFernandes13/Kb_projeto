using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto_KB.Models
{
    public class Concept
    { 
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public DateTime ContentDate { get; set; }
        public string KeyWords { get; set; }
        public int JourneyID { get; set; }
        
        public virtual ICollection<Image> Images { get; set; }     //hold multiple entities Image
        public virtual Journey Journey { get; set; }               //navigation property from foreign Key, one Concept associated to one Journey


    }
} 