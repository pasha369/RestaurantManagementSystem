namespace DataModel.RestorauntContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.City",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 180),
                        Country_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Country", t => t.Country_Id)
                .Index(t => t.Country_Id);
            
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 180),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ClientInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Restaurant_Id = c.Int(),
                        UserInfo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Restaurant", t => t.Restaurant_Id)
                .ForeignKey("dbo.UserInfo", t => t.UserInfo_Id)
                .Index(t => t.Restaurant_Id)
                .Index(t => t.UserInfo_Id);
            
            CreateTable(
                "dbo.Restaurant",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        PhoneNumber = c.Int(nullable: false),
                        Adress_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Address", t => t.Adress_Id)
                .Index(t => t.Adress_Id);
            
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Street = c.String(),
                        City_Id = c.Int(),
                        Country_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.City", t => t.City_Id)
                .ForeignKey("dbo.Country", t => t.Country_Id)
                .Index(t => t.City_Id)
                .Index(t => t.Country_Id);
            
            CreateTable(
                "dbo.Cuisine",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Hall",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(),
                        Restaurant_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Restaurant", t => t.Restaurant_Id)
                .Index(t => t.Restaurant_Id);
            
            CreateTable(
                "dbo.DinnerTable",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        IsReserved = c.Boolean(nullable: false),
                        Hall_Id = c.Int(),
                        Restaurant_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hall", t => t.Hall_Id)
                .ForeignKey("dbo.Restaurant", t => t.Restaurant_Id)
                .Index(t => t.Hall_Id)
                .Index(t => t.Restaurant_Id);
            
            CreateTable(
                "dbo.Review",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Stars = c.Int(nullable: false),
                        Food = c.Int(nullable: false),
                        Service = c.Int(nullable: false),
                        Ambience = c.Int(nullable: false),
                        ShortDesc = c.String(maxLength: 50),
                        Comment = c.String(nullable: false, maxLength: 250),
                        ReviewTime = c.DateTime(nullable: false),
                        Author = c.String(maxLength: 60),
                        Status = c.Int(nullable: false),
                        Restaurant_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Restaurant", t => t.Restaurant_Id)
                .Index(t => t.Restaurant_Id);
            
            CreateTable(
                "dbo.UserInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Login = c.String(maxLength: 60),
                        Password = c.String(maxLength: 60),
                        Phone = c.Int(nullable: false),
                        IsBanned = c.Boolean(nullable: false),
                        Position = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Menu",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 180),
                        Cuisine_Id = c.Int(),
                        Menu_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cuisine", t => t.Cuisine_Id)
                .ForeignKey("dbo.Menu", t => t.Menu_Id)
                .Index(t => t.Cuisine_Id)
                .Index(t => t.Menu_Id);
            
            CreateTable(
                "dbo.Dish",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 180),
                        Cost = c.Int(nullable: false),
                        Category_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.Category_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.Receipt",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CurrentDateTime = c.DateTime(nullable: false),
                        Client_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClientInfo", t => t.Client_Id)
                .Index(t => t.Client_Id);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderDish_Id = c.Int(),
                        Receipt_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dish", t => t.OrderDish_Id)
                .ForeignKey("dbo.Receipt", t => t.Receipt_Id)
                .Index(t => t.OrderDish_Id)
                .Index(t => t.Receipt_Id);
            
            CreateTable(
                "dbo.Reservation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        From = c.DateTime(nullable: false),
                        To = c.DateTime(nullable: false),
                        SpecialRequest = c.String(),
                        Table_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DinnerTable", t => t.Table_Id, cascadeDelete: true)
                .ForeignKey("dbo.UserInfo", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Table_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.CuisineRestaurants",
                c => new
                    {
                        Cuisine_Id = c.Int(nullable: false),
                        Restaurant_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Cuisine_Id, t.Restaurant_Id })
                .ForeignKey("dbo.Cuisine", t => t.Cuisine_Id, cascadeDelete: true)
                .ForeignKey("dbo.Restaurant", t => t.Restaurant_Id, cascadeDelete: true)
                .Index(t => t.Cuisine_Id)
                .Index(t => t.Restaurant_Id);
            
            CreateTable(
                "dbo.CustomerInfo",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Detail = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserInfo", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerInfo", "Id", "dbo.UserInfo");
            DropForeignKey("dbo.Reservation", "User_Id", "dbo.UserInfo");
            DropForeignKey("dbo.Reservation", "Table_Id", "dbo.DinnerTable");
            DropForeignKey("dbo.Order", "Receipt_Id", "dbo.Receipt");
            DropForeignKey("dbo.Order", "OrderDish_Id", "dbo.Dish");
            DropForeignKey("dbo.Receipt", "Client_Id", "dbo.ClientInfo");
            DropForeignKey("dbo.Category", "Menu_Id", "dbo.Menu");
            DropForeignKey("dbo.Dish", "Category_Id", "dbo.Category");
            DropForeignKey("dbo.Category", "Cuisine_Id", "dbo.Cuisine");
            DropForeignKey("dbo.ClientInfo", "UserInfo_Id", "dbo.UserInfo");
            DropForeignKey("dbo.ClientInfo", "Restaurant_Id", "dbo.Restaurant");
            DropForeignKey("dbo.Review", "Restaurant_Id", "dbo.Restaurant");
            DropForeignKey("dbo.DinnerTable", "Restaurant_Id", "dbo.Restaurant");
            DropForeignKey("dbo.DinnerTable", "Hall_Id", "dbo.Hall");
            DropForeignKey("dbo.Hall", "Restaurant_Id", "dbo.Restaurant");
            DropForeignKey("dbo.CuisineRestaurants", "Restaurant_Id", "dbo.Restaurant");
            DropForeignKey("dbo.CuisineRestaurants", "Cuisine_Id", "dbo.Cuisine");
            DropForeignKey("dbo.Restaurant", "Adress_Id", "dbo.Address");
            DropForeignKey("dbo.Address", "Country_Id", "dbo.Country");
            DropForeignKey("dbo.Address", "City_Id", "dbo.City");
            DropForeignKey("dbo.City", "Country_Id", "dbo.Country");
            DropIndex("dbo.CustomerInfo", new[] { "Id" });
            DropIndex("dbo.CuisineRestaurants", new[] { "Restaurant_Id" });
            DropIndex("dbo.CuisineRestaurants", new[] { "Cuisine_Id" });
            DropIndex("dbo.Reservation", new[] { "User_Id" });
            DropIndex("dbo.Reservation", new[] { "Table_Id" });
            DropIndex("dbo.Order", new[] { "Receipt_Id" });
            DropIndex("dbo.Order", new[] { "OrderDish_Id" });
            DropIndex("dbo.Receipt", new[] { "Client_Id" });
            DropIndex("dbo.Dish", new[] { "Category_Id" });
            DropIndex("dbo.Category", new[] { "Menu_Id" });
            DropIndex("dbo.Category", new[] { "Cuisine_Id" });
            DropIndex("dbo.Review", new[] { "Restaurant_Id" });
            DropIndex("dbo.DinnerTable", new[] { "Restaurant_Id" });
            DropIndex("dbo.DinnerTable", new[] { "Hall_Id" });
            DropIndex("dbo.Hall", new[] { "Restaurant_Id" });
            DropIndex("dbo.Address", new[] { "Country_Id" });
            DropIndex("dbo.Address", new[] { "City_Id" });
            DropIndex("dbo.Restaurant", new[] { "Adress_Id" });
            DropIndex("dbo.ClientInfo", new[] { "UserInfo_Id" });
            DropIndex("dbo.ClientInfo", new[] { "Restaurant_Id" });
            DropIndex("dbo.City", new[] { "Country_Id" });
            DropTable("dbo.CustomerInfo");
            DropTable("dbo.CuisineRestaurants");
            DropTable("dbo.Reservation");
            DropTable("dbo.Order");
            DropTable("dbo.Receipt");
            DropTable("dbo.Dish");
            DropTable("dbo.Category");
            DropTable("dbo.Menu");
            DropTable("dbo.UserInfo");
            DropTable("dbo.Review");
            DropTable("dbo.DinnerTable");
            DropTable("dbo.Hall");
            DropTable("dbo.Cuisine");
            DropTable("dbo.Address");
            DropTable("dbo.Restaurant");
            DropTable("dbo.ClientInfo");
            DropTable("dbo.Country");
            DropTable("dbo.City");
        }
    }
}
