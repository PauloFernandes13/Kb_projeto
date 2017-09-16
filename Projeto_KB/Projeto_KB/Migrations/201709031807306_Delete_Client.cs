namespace Projeto_KB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Delete_Client : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Client", "JourneyID", "dbo.Journey");
            DropForeignKey("dbo.Milestone", "ClientID", "dbo.Client");
            DropIndex("dbo.Client", new[] { "JourneyID" });
            DropIndex("dbo.Milestone", new[] { "ClientID" });
            DropTable("dbo.Client");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.Milestone", "ClientID");
            CreateIndex("dbo.Client", "JourneyID");
            AddForeignKey("dbo.Milestone", "ClientID", "dbo.Client", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Client", "JourneyID", "dbo.Journey", "ID", cascadeDelete: true);
        }
    }
}
