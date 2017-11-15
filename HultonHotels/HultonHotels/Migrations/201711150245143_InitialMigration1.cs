namespace HultonHotels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Breakfasts",
                c => new
                    {
                        HotelId = c.Int(nullable: false),
                        BreakfastType = c.String(nullable: false, maxLength: 128),
                        BreakfastCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                    })
                .PrimaryKey(t => new { t.HotelId, t.BreakfastType })
                .ForeignKey("dbo.Hotels", t => t.HotelId, cascadeDelete: true)
                .Index(t => t.HotelId);
            
            CreateTable(
                "dbo.CreditCards",
                c => new
                    {
                        CreditCardId = c.Int(nullable: false, identity: true),
                        ExpDate = c.DateTime(nullable: false),
                        Type = c.String(),
                        SecurityCode = c.Int(nullable: false),
                        NameOnCard = c.String(),
                        BillingAddress = c.String(),
                    })
                .PrimaryKey(t => t.CreditCardId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Address = c.String(),
                        PhoneNo = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ReservationId = c.Int(nullable: false, identity: true),
                        ReservationDate = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ReservationId);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewId = c.Int(nullable: false, identity: true),
                        Ratin = c.Int(nullable: false),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.ReviewId);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        HotelId = c.Int(nullable: false),
                        ServiceType = c.String(nullable: false, maxLength: 128),
                        ServiceCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.HotelId, t.ServiceType })
                .ForeignKey("dbo.Hotels", t => t.HotelId, cascadeDelete: true)
                .Index(t => t.HotelId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Services", "HotelId", "dbo.Hotels");
            DropForeignKey("dbo.Breakfasts", "HotelId", "dbo.Hotels");
            DropIndex("dbo.Services", new[] { "HotelId" });
            DropIndex("dbo.Breakfasts", new[] { "HotelId" });
            DropTable("dbo.Services");
            DropTable("dbo.Reviews");
            DropTable("dbo.Reservations");
            DropTable("dbo.Customers");
            DropTable("dbo.CreditCards");
            DropTable("dbo.Breakfasts");
        }
    }
}
