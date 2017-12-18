namespace Projeto_KB.Migrations.Identity
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newParameters_User : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "AccountName", c => c.String());
            AddColumn("dbo.AspNetUsers", "ContactName", c => c.String());
            AddColumn("dbo.AspNetUsers", "Country", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Country");
            DropColumn("dbo.AspNetUsers", "ContactName");
            DropColumn("dbo.AspNetUsers", "AccountName");
        }
    }
}
