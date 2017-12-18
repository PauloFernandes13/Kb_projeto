namespace Projeto_KB.Migrations.Identity
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fixed_ViewModel : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.UserListViewModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserListViewModels",
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
