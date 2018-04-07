namespace Projeto_KB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddtopicConcepttophase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TopicConcept", "PhaseID", c => c.Int());
            CreateIndex("dbo.TopicConcept", "PhaseID");
            AddForeignKey("dbo.TopicConcept", "PhaseID", "dbo.Phase", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TopicConcept", "PhaseID", "dbo.Phase");
            DropIndex("dbo.TopicConcept", new[] { "PhaseID" });
            DropColumn("dbo.TopicConcept", "PhaseID");
        }
    }
}
