namespace Projeto_KB.Migrations.Identity
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteUserViewModel : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.UserViewModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserViewModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        UserName = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
