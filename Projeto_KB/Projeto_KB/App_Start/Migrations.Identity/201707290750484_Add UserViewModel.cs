namespace Projeto_KB.Migrations.Identity
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserViewModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserViewModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        UserName = c.String(),
                        PhoneNumber = c.String(),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserViewModels");
        }
    }
}
