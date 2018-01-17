namespace Projeto_KB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NovastabelasrelaçãoConceitoBest : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Concept", "SubjectID", "dbo.Subject");
            DropForeignKey("dbo.Concept", "TopicID", "dbo.Topic");
            DropIndex("dbo.Concept", new[] { "TopicID" });
            DropIndex("dbo.Concept", new[] { "SubjectID" });
            CreateTable(
                "dbo.Phase",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TopicConcept",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Concept", "PhaseID", c => c.Int(nullable: false));
            AddColumn("dbo.Concept", "TopicConceptID", c => c.Int(nullable: false));
            CreateIndex("dbo.Concept", "PhaseID");
            CreateIndex("dbo.Concept", "TopicConceptID");
            AddForeignKey("dbo.Concept", "PhaseID", "dbo.Phase", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Concept", "TopicConceptID", "dbo.TopicConcept", "ID", cascadeDelete: true);
            DropColumn("dbo.Concept", "TopicID");
            DropColumn("dbo.Concept", "SubjectID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Concept", "SubjectID", c => c.Int(nullable: false));
            AddColumn("dbo.Concept", "TopicID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Concept", "TopicConceptID", "dbo.TopicConcept");
            DropForeignKey("dbo.Concept", "PhaseID", "dbo.Phase");
            DropIndex("dbo.Concept", new[] { "TopicConceptID" });
            DropIndex("dbo.Concept", new[] { "PhaseID" });
            DropColumn("dbo.Concept", "TopicConceptID");
            DropColumn("dbo.Concept", "PhaseID");
            DropTable("dbo.TopicConcept");
            DropTable("dbo.Phase");
            CreateIndex("dbo.Concept", "SubjectID");
            CreateIndex("dbo.Concept", "TopicID");
            AddForeignKey("dbo.Concept", "TopicID", "dbo.Topic", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Concept", "SubjectID", "dbo.Subject", "ID", cascadeDelete: true);
        }
    }
}
