namespace HultonHotels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VariousReviews : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BreakfastReviewAssessesBreakfasts",
                c => new
                    {
                        HotelId = c.Int(nullable: false),
                        BreakfastType = c.String(maxLength: 128),
                        ReviewId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReviewId)
                .ForeignKey("dbo.Breakfasts", t => new { t.HotelId, t.BreakfastType })
                .ForeignKey("dbo.BreakfastReviews", t => t.ReviewId)
                .Index(t => new { t.HotelId, t.BreakfastType })
                .Index(t => t.ReviewId);
            
            CreateTable(
                "dbo.BreakfastReviews",
                c => new
                    {
                        ReviewId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReviewId)
                .ForeignKey("dbo.Reviews", t => t.ReviewId)
                .Index(t => t.ReviewId);
            
            CreateTable(
                "dbo.RoomReviewEvaluatesRooms",
                c => new
                    {
                        HotelId = c.Int(nullable: false),
                        RoomNo = c.Int(nullable: false),
                        ReviewId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReviewId)
                .ForeignKey("dbo.Rooms", t => new { t.HotelId, t.RoomNo }, cascadeDelete: true)
                .ForeignKey("dbo.RoomReviews", t => t.ReviewId)
                .Index(t => new { t.HotelId, t.RoomNo })
                .Index(t => t.ReviewId);
            
            CreateTable(
                "dbo.RoomReviews",
                c => new
                    {
                        ReviewId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReviewId)
                .ForeignKey("dbo.Reviews", t => t.ReviewId)
                .Index(t => t.ReviewId);
            
            CreateTable(
                "dbo.ServiceReviewRatesServices",
                c => new
                    {
                        HotelId = c.Int(nullable: false),
                        ServiceType = c.String(maxLength: 128),
                        ReviewId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReviewId)
                .ForeignKey("dbo.Services", t => new { t.HotelId, t.ServiceType })
                .ForeignKey("dbo.ServiceReviews", t => t.ReviewId)
                .Index(t => new { t.HotelId, t.ServiceType })
                .Index(t => t.ReviewId);
            
            CreateTable(
                "dbo.ServiceReviews",
                c => new
                    {
                        ReviewId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReviewId)
                .ForeignKey("dbo.Reviews", t => t.ReviewId)
                .Index(t => t.ReviewId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServiceReviewRatesServices", "ReviewId", "dbo.ServiceReviews");
            DropForeignKey("dbo.ServiceReviews", "ReviewId", "dbo.Reviews");
            DropForeignKey("dbo.ServiceReviewRatesServices", new[] { "HotelId", "ServiceType" }, "dbo.Services");
            DropForeignKey("dbo.RoomReviewEvaluatesRooms", "ReviewId", "dbo.RoomReviews");
            DropForeignKey("dbo.RoomReviews", "ReviewId", "dbo.Reviews");
            DropForeignKey("dbo.RoomReviewEvaluatesRooms", new[] { "HotelId", "RoomNo" }, "dbo.Rooms");
            DropForeignKey("dbo.BreakfastReviewAssessesBreakfasts", "ReviewId", "dbo.BreakfastReviews");
            DropForeignKey("dbo.BreakfastReviews", "ReviewId", "dbo.Reviews");
            DropForeignKey("dbo.BreakfastReviewAssessesBreakfasts", new[] { "HotelId", "BreakfastType" }, "dbo.Breakfasts");
            DropIndex("dbo.ServiceReviews", new[] { "ReviewId" });
            DropIndex("dbo.ServiceReviewRatesServices", new[] { "ReviewId" });
            DropIndex("dbo.ServiceReviewRatesServices", new[] { "HotelId", "ServiceType" });
            DropIndex("dbo.RoomReviews", new[] { "ReviewId" });
            DropIndex("dbo.RoomReviewEvaluatesRooms", new[] { "ReviewId" });
            DropIndex("dbo.RoomReviewEvaluatesRooms", new[] { "HotelId", "RoomNo" });
            DropIndex("dbo.BreakfastReviews", new[] { "ReviewId" });
            DropIndex("dbo.BreakfastReviewAssessesBreakfasts", new[] { "ReviewId" });
            DropIndex("dbo.BreakfastReviewAssessesBreakfasts", new[] { "HotelId", "BreakfastType" });
            DropTable("dbo.ServiceReviews");
            DropTable("dbo.ServiceReviewRatesServices");
            DropTable("dbo.RoomReviews");
            DropTable("dbo.RoomReviewEvaluatesRooms");
            DropTable("dbo.BreakfastReviews");
            DropTable("dbo.BreakfastReviewAssessesBreakfasts");
        }
    }
}
