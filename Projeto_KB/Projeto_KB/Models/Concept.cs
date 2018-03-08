using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto_KB.Models
{
    public class Concept
    { 
        public int ID { get; set; }
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
        public DateTime ContentDate { get; set; }
        [DisplayName("Order Content")]
        public string Order { get; set; }
        public int JourneyID { get; set; }
        public int PhaseID { get; set; }
        public int? TopicConceptID { get; set; }
        public virtual ICollection<Image> Images { get; set; }     //hold multiple entities Image
        public virtual Phase Phase { get; set; }
        public virtual Journey Journey { get; set; }               //navigation property from foreign Key, one Concept associated to one Journey
        public virtual TopicConcept TopicConcept { get; set; }
     

    }
}  