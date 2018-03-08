namespace Projeto_KB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KeywordsConceptsModeltoOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Concept", "Order", c => c.String());
            DropColumn("dbo.Concept", "KeyWords");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Concept", "KeyWords", c => c.String());
            DropColumn("dbo.Concept", "Order");
        }
    }
}
