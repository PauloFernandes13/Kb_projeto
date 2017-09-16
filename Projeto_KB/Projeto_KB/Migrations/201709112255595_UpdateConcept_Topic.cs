namespace Projeto_KB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateConcept_Topic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Concept", "TopicID", c => c.Int());
            CreateIndex("dbo.Concept", "TopicID");
            AddForeignKey("dbo.Concept", "TopicID", "dbo.Topic", "ID");
            DropColumn("dbo.Concept", "Description");
            DropColumn("dbo.Concept", "UrlContent");
            DropColumn("dbo.Milestone", "ClientID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Milestone", "ClientID", c => c.Int(nullable: false));
            AddColumn("dbo.Concept", "UrlContent", c => c.String());
            AddColumn("dbo.Concept", "Description", c => c.String());
            DropForeignKey("dbo.Concept", "TopicID", "dbo.Topic");
            DropIndex("dbo.Concept", new[] { "TopicID" });
            DropColumn("dbo.Concept", "TopicID");
        }
    }
}
