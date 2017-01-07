namespace Ticketing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ratinginissue : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Issues", "Rating", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Issues", "Rating");
        }
    }
}
