namespace HultonHotels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
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
                "dbo.Hotels",
                c => new
                    {
                        HotelId = c.Int(nullable: false, identity: true),
                        Street = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Country = c.String(),
                        Zip = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.HotelId);
            
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
                "dbo.CustomerMakesReservationWithCreditCards",
                c => new
                    {
                        ReservationId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        CreditCardId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReservationId)
                .ForeignKey("dbo.CreditCards", t => t.CreditCardId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Reservations", t => t.ReservationId)
                .Index(t => t.ReservationId)
                .Index(t => t.CustomerId)
                .Index(t => t.CreditCardId);
            
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
                "dbo.CustomerWritesReviews",
                c => new
                    {
                        ReviewId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReviewId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Reviews", t => t.ReviewId)
                .Index(t => t.ReviewId)
                .Index(t => t.CustomerId);
            
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
                "dbo.ReservationContainsServices",
                c => new
                    {
                        ReservationId = c.Int(nullable: false),
                        HotelId = c.Int(nullable: false),
                        ServiceType = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ReservationId, t.HotelId, t.ServiceType })
                .ForeignKey("dbo.Reservations", t => t.ReservationId, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => new { t.HotelId, t.ServiceType }, cascadeDelete: true)
                .Index(t => t.ReservationId)
                .Index(t => new { t.HotelId, t.ServiceType });
            
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
            
            CreateTable(
                "dbo.ReservationIncludesBreakfasts",
                c => new
                    {
                        ReservationId = c.Int(nullable: false),
                        HotelId = c.Int(nullable: false),
                        BreakfastType = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ReservationId, t.HotelId, t.BreakfastType })
                .ForeignKey("dbo.Breakfasts", t => new { t.HotelId, t.BreakfastType }, cascadeDelete: true)
                .ForeignKey("dbo.Reservations", t => t.ReservationId, cascadeDelete: true)
                .Index(t => t.ReservationId)
                .Index(t => new { t.HotelId, t.BreakfastType });
            
            CreateTable(
                "dbo.ReservationReservesRooms",
                c => new
                    {
                        ReservationId = c.Int(nullable: false),
                        HotelId = c.Int(nullable: false),
                        RoomNo = c.Int(nullable: false),
                        OutDate = c.DateTime(nullable: false),
                        InDate = c.DateTime(nullable: false),
                        NumberOfDays = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ReservationId, t.HotelId, t.RoomNo })
                .ForeignKey("dbo.Reservations", t => t.ReservationId, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => new { t.HotelId, t.RoomNo }, cascadeDelete: true)
                .Index(t => t.ReservationId)
                .Index(t => new { t.HotelId, t.RoomNo });
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        HotelId = c.Int(nullable: false),
                        RoomNo = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Capacity = c.Int(nullable: false),
                        Description = c.String(),
                        Type = c.String(),
                    })
                .PrimaryKey(t => new { t.HotelId, t.RoomNo })
                .ForeignKey("dbo.Hotels", t => t.HotelId, cascadeDelete: true)
                .Index(t => t.HotelId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ReservationReservesRooms", new[] { "HotelId", "RoomNo" }, "dbo.Rooms");
            DropForeignKey("dbo.Rooms", "HotelId", "dbo.Hotels");
            DropForeignKey("dbo.ReservationReservesRooms", "ReservationId", "dbo.Reservations");
            DropForeignKey("dbo.ReservationIncludesBreakfasts", "ReservationId", "dbo.Reservations");
            DropForeignKey("dbo.ReservationIncludesBreakfasts", new[] { "HotelId", "BreakfastType" }, "dbo.Breakfasts");
            DropForeignKey("dbo.ReservationContainsServices", new[] { "HotelId", "ServiceType" }, "dbo.Services");
            DropForeignKey("dbo.Services", "HotelId", "dbo.Hotels");
            DropForeignKey("dbo.ReservationContainsServices", "ReservationId", "dbo.Reservations");
            DropForeignKey("dbo.CustomerWritesReviews", "ReviewId", "dbo.Reviews");
            DropForeignKey("dbo.CustomerWritesReviews", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CustomerMakesReservationWithCreditCards", "ReservationId", "dbo.Reservations");
            DropForeignKey("dbo.CustomerMakesReservationWithCreditCards", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CustomerMakesReservationWithCreditCards", "CreditCardId", "dbo.CreditCards");
            DropForeignKey("dbo.Breakfasts", "HotelId", "dbo.Hotels");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Rooms", new[] { "HotelId" });
            DropIndex("dbo.ReservationReservesRooms", new[] { "HotelId", "RoomNo" });
            DropIndex("dbo.ReservationReservesRooms", new[] { "ReservationId" });
            DropIndex("dbo.ReservationIncludesBreakfasts", new[] { "HotelId", "BreakfastType" });
            DropIndex("dbo.ReservationIncludesBreakfasts", new[] { "ReservationId" });
            DropIndex("dbo.Services", new[] { "HotelId" });
            DropIndex("dbo.ReservationContainsServices", new[] { "HotelId", "ServiceType" });
            DropIndex("dbo.ReservationContainsServices", new[] { "ReservationId" });
            DropIndex("dbo.CustomerWritesReviews", new[] { "CustomerId" });
            DropIndex("dbo.CustomerWritesReviews", new[] { "ReviewId" });
            DropIndex("dbo.CustomerMakesReservationWithCreditCards", new[] { "CreditCardId" });
            DropIndex("dbo.CustomerMakesReservationWithCreditCards", new[] { "CustomerId" });
            DropIndex("dbo.CustomerMakesReservationWithCreditCards", new[] { "ReservationId" });
            DropIndex("dbo.Breakfasts", new[] { "HotelId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Rooms");
            DropTable("dbo.ReservationReservesRooms");
            DropTable("dbo.ReservationIncludesBreakfasts");
            DropTable("dbo.Services");
            DropTable("dbo.ReservationContainsServices");
            DropTable("dbo.Reviews");
            DropTable("dbo.CustomerWritesReviews");
            DropTable("dbo.Reservations");
            DropTable("dbo.Customers");
            DropTable("dbo.CustomerMakesReservationWithCreditCards");
            DropTable("dbo.CreditCards");
            DropTable("dbo.Hotels");
            DropTable("dbo.Breakfasts");
        }
    }
}
