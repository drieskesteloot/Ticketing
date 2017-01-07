namespace Ticketing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StatusTrace2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StatusTraces", "StatusInfo_Id", "dbo.StatusInfoes");
            DropIndex("dbo.StatusTraces", new[] { "StatusInfo_Id" });
            AddColumn("dbo.StatusTraces", "Status", c => c.String());
            AddColumn("dbo.StatusTraces", "IssueId", c => c.String());
            DropColumn("dbo.StatusTraces", "StatusInfoId");
            DropColumn("dbo.StatusTraces", "StatusInfo_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StatusTraces", "StatusInfo_Id", c => c.Int());
            AddColumn("dbo.StatusTraces", "StatusInfoId", c => c.String());
            DropColumn("dbo.StatusTraces", "IssueId");
            DropColumn("dbo.StatusTraces", "Status");
            CreateIndex("dbo.StatusTraces", "StatusInfo_Id");
            AddForeignKey("dbo.StatusTraces", "StatusInfo_Id", "dbo.StatusInfoes", "Id");
        }
    }
}
