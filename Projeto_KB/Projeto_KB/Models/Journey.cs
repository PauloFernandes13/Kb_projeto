using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Projeto_KB.Models                     // code first using EntityFramework
{
    
    public class Journey
    {   
        public int ID { get; set; }
        [DisplayName("Journey")]//navigation Property -- can hold multiple entities
        public  string Name { get; set; }                                   //Icollection define methods to manipulate collections
                                                                           //virtual to take advantage from lazy loading
        public virtual ICollection<Concept> Concepts { get; set; }     //hold multiple entities Concept
        public virtual ICollection<Phase> Phases { get; set; }
            
    }                                                              
                                                                   
}