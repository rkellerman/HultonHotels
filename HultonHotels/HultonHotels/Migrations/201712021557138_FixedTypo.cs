namespace HultonHotels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedTypo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "Rating", c => c.Int(nullable: false));
            DropColumn("dbo.Reviews", "Ratin");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reviews", "Ratin", c => c.Int(nullable: false));
            DropColumn("dbo.Reviews", "Rating");
        }
    }
}
