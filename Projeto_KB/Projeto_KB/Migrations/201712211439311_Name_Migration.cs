namespace Projeto_KB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Name_Migration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Subject", "Name", c => c.String(maxLength: 12));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Subject", "Name", c => c.String());
        }
    }
}
