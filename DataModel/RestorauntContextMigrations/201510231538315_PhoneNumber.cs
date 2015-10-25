namespace DataModel.RestorauntContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PhoneNumber : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Restaurant", "PhoneNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Restaurant", "PhoneNumber", c => c.Int(nullable: false));
        }
    }
}
