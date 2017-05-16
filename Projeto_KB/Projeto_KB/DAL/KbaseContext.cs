using System;
using Projeto_KB.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto_KB.DAL
{
    public class KbaseContext : DbContext
    {
        public KbaseContext() : base("KbaseContext") //name of connection string is passed to Constructor
        {

        }
        public DbSet<Journey> Journeys { get; set; }
        public DbSet<Concept> Concepts { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet <Faq> Faqs { get; set; }
        public DbSet<Milestone> Milestones { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}