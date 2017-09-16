namespace Projeto_KB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Concept_Subject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Concept", "SubjectID", c => c.Int());
            CreateIndex("dbo.Concept", "SubjectID");
            AddForeignKey("dbo.Concept", "SubjectID", "dbo.Subject", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Concept", "SubjectID", "dbo.Subject");
            DropIndex("dbo.Concept", new[] { "SubjectID" });
            DropColumn("dbo.Concept", "SubjectID");
        }
    }
}
