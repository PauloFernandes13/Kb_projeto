namespace Projeto_KB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Client",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Adress = c.String(),
                        JourneyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Journey", t => t.JourneyID, cascadeDelete: true)
                .Index(t => t.JourneyID);
            
            CreateTable(
                "dbo.Journey",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Concept",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Text = c.String(),
                        ContentDate = c.DateTime(nullable: false),
                        KeyWords = c.String(),
                        UrlContent = c.String(),
                        JourneyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Journey", t => t.JourneyID, cascadeDelete: true)
                .Index(t => t.JourneyID);
            
            CreateTable(
                "dbo.Image",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UrlImage = c.String(),
                        ConceptID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Concept", t => t.ConceptID, cascadeDelete: true)
                .Index(t => t.ConceptID);
            
            CreateTable(
                "dbo.Milestone",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        MilestoneDate = c.DateTime(nullable: false),
                        ClientID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Client", t => t.ClientID, cascadeDelete: true)
                .Index(t => t.ClientID);
            
            CreateTable(
                "dbo.Faq",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Question = c.String(),
                        Answer = c.String(),
                        UrlFaq = c.String(),
                        TopicID = c.Int(nullable: false),
                        SubjectID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Subject", t => t.SubjectID, cascadeDelete: true)
                .ForeignKey("dbo.Topic", t => t.TopicID, cascadeDelete: true)
                .Index(t => t.TopicID)
                .Index(t => t.SubjectID);
            
            CreateTable(
                "dbo.Subject",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Topic",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Faq", "TopicID", "dbo.Topic");
            DropForeignKey("dbo.Faq", "SubjectID", "dbo.Subject");
            DropForeignKey("dbo.Milestone", "ClientID", "dbo.Client");
            DropForeignKey("dbo.Concept", "JourneyID", "dbo.Journey");
            DropForeignKey("dbo.Image", "ConceptID", "dbo.Concept");
            DropForeignKey("dbo.Client", "JourneyID", "dbo.Journey");
            DropIndex("dbo.Faq", new[] { "SubjectID" });
            DropIndex("dbo.Faq", new[] { "TopicID" });
            DropIndex("dbo.Milestone", new[] { "ClientID" });
            DropIndex("dbo.Image", new[] { "ConceptID" });
            DropIndex("dbo.Concept", new[] { "JourneyID" });
            DropIndex("dbo.Client", new[] { "JourneyID" });
            DropTable("dbo.Topic");
            DropTable("dbo.Subject");
            DropTable("dbo.Faq");
            DropTable("dbo.Milestone");
            DropTable("dbo.Image");
            DropTable("dbo.Concept");
            DropTable("dbo.Journey");
            DropTable("dbo.Client");
        }
    }
}
