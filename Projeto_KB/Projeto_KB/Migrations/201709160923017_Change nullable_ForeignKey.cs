namespace Projeto_KB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changenullable_ForeignKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Concept", "SubjectID", "dbo.Subject");
            DropForeignKey("dbo.Concept", "TopicID", "dbo.Topic");
            DropIndex("dbo.Concept", new[] { "TopicID" });
            DropIndex("dbo.Concept", new[] { "SubjectID" });
            AlterColumn("dbo.Concept", "TopicID", c => c.Int(nullable: false));
            AlterColumn("dbo.Concept", "SubjectID", c => c.Int(nullable: false));
            CreateIndex("dbo.Concept", "TopicID");
            CreateIndex("dbo.Concept", "SubjectID");
            AddForeignKey("dbo.Concept", "SubjectID", "dbo.Subject", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Concept", "TopicID", "dbo.Topic", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Concept", "TopicID", "dbo.Topic");
            DropForeignKey("dbo.Concept", "SubjectID", "dbo.Subject");
            DropIndex("dbo.Concept", new[] { "SubjectID" });
            DropIndex("dbo.Concept", new[] { "TopicID" });
            AlterColumn("dbo.Concept", "SubjectID", c => c.Int());
            AlterColumn("dbo.Concept", "TopicID", c => c.Int());
            CreateIndex("dbo.Concept", "SubjectID");
            CreateIndex("dbo.Concept", "TopicID");
            AddForeignKey("dbo.Concept", "TopicID", "dbo.Topic", "ID");
            AddForeignKey("dbo.Concept", "SubjectID", "dbo.Subject", "ID");
        }
    }
}
