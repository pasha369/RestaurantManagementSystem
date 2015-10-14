namespace DataModel.RestorauntContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModReservation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserInfo", "Email", c => c.String());
            AddColumn("dbo.Reservation", "DateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Reservation", "PeopleCount", c => c.Int(nullable: false));
            DropColumn("dbo.Reservation", "From");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservation", "From", c => c.DateTime(nullable: false));
            DropColumn("dbo.Reservation", "PeopleCount");
            DropColumn("dbo.Reservation", "DateTime");
            DropColumn("dbo.UserInfo", "Email");
        }
    }
}
