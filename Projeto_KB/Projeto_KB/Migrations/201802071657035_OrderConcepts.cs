namespace Projeto_KB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderConcepts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Phase", "Order", c => c.Int(nullable: false));
            AddColumn("dbo.TopicConcept", "order", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TopicConcept", "order");
            DropColumn("dbo.Phase", "Order");
        }
    }
}
