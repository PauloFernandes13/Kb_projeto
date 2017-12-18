namespace Projeto_KB.Migrations.Identity
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Name_Migration : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserViewModels", "RoleName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserViewModels", "RoleName", c => c.String());
        }
    }
}
