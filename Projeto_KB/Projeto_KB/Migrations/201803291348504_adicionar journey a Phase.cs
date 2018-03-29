namespace Projeto_KB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adicionarjourneyaPhase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Phase", "JourneyID", c => c.Int());
            CreateIndex("dbo.Phase", "JourneyID");
            AddForeignKey("dbo.Phase", "JourneyID", "dbo.Journey", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Phase", "JourneyID", "dbo.Journey");
            DropIndex("dbo.Phase", new[] { "JourneyID" });
            DropColumn("dbo.Phase", "JourneyID");
        }
    }
}
