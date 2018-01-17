namespace Projeto_KB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ValidacãoAssunto : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Subject", "Name", c => c.String(maxLength: 18));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Subject", "Name", c => c.String(maxLength: 17));
        }
    }
}
