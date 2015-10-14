namespace DataModel.RestorauntContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class favorite2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Favorite",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Restaurant_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Restaurant", t => t.Restaurant_Id)
                .ForeignKey("dbo.UserInfo", t => t.User_Id)
                .Index(t => t.Restaurant_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Favorite", "User_Id", "dbo.UserInfo");
            DropForeignKey("dbo.Favorite", "Restaurant_Id", "dbo.Restaurant");
            DropIndex("dbo.Favorite", new[] { "User_Id" });
            DropIndex("dbo.Favorite", new[] { "Restaurant_Id" });
            DropTable("dbo.Favorite");
        }
    }
}
