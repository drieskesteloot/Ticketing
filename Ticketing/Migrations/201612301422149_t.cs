namespace Ticketing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class t : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Issues", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Issues", "UserId");
            AddForeignKey("dbo.Issues", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Issues", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Issues", new[] { "UserId" });
            DropColumn("dbo.Issues", "UserId");
        }
    }
}
