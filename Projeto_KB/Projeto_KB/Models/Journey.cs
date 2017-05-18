using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto_KB.Models                     // code first using EntityFramework
{
    
    public class Journey
    {   
        public int ID { get; set; }                                        //navigation Property -- can hold multiple entities
        public  string Name { get; set; }                                   //Icollection define methods to manipulate collections
                                                                           //virtual to take advantage from lazy loading
        public virtual ICollection<Concept> Concepts { get; set; }     //hold multiple entities Concept
        public virtual ICollection<Client> Clients { get; set; }       //hold multiple entities Clients
        public virtual ICollection<Faq> Faqs { get; set; }             //hold multiple entities Faqs
    }                                                              
                                                                   
}