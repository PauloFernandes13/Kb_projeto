using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto_KB.Models
{
    public class Client
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public int JourneyID { get; set; }

        public virtual ICollection<Milestone> Milestones { get; set; } 
        public virtual Journey Journey { get; set; }
             

    }
}