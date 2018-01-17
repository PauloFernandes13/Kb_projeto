namespace Projeto_KB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TopicConcAllowNull : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Concept", "TopicConceptID", "dbo.TopicConcept");
            DropIndex("dbo.Concept", new[] { "TopicConceptID" });
            AlterColumn("dbo.Concept", "TopicConceptID", c => c.Int());
            CreateIndex("dbo.Concept", "TopicConceptID");
            AddForeignKey("dbo.Concept", "TopicConceptID", "dbo.TopicConcept", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Concept", "TopicConceptID", "dbo.TopicConcept");
            DropIndex("dbo.Concept", new[] { "TopicConceptID" });
            AlterColumn("dbo.Concept", "TopicConceptID", c => c.Int(nullable: false));
            CreateIndex("dbo.Concept", "TopicConceptID");
            AddForeignKey("dbo.Concept", "TopicConceptID", "dbo.TopicConcept", "ID", cascadeDelete: true);
        }
    }
}
