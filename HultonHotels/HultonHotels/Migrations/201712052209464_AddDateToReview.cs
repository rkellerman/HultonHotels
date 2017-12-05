namespace HultonHotels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDateToReview : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reviews", "Date");
        }
    }
}
