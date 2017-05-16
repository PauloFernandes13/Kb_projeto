using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto_KB.Models
{
    public class Milestone
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime MilestoneDate { get; set; }
        public int ClientID { get; set; }

        public virtual Client Client { get; set; }
    }
}