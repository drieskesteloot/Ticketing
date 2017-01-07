namespace Ticketing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StatusTrace : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StatusTraces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LogDateTime = c.DateTime(nullable: false),
                        StatusInfoId = c.String(),
                        StatusInfo_Id = c.Int(),
                        Issue_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StatusInfoes", t => t.StatusInfo_Id)
                .ForeignKey("dbo.Issues", t => t.Issue_Id)
                .Index(t => t.StatusInfo_Id)
                .Index(t => t.Issue_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StatusTraces", "Issue_Id", "dbo.Issues");
            DropForeignKey("dbo.StatusTraces", "StatusInfo_Id", "dbo.StatusInfoes");
            DropIndex("dbo.StatusTraces", new[] { "Issue_Id" });
            DropIndex("dbo.StatusTraces", new[] { "StatusInfo_Id" });
            DropTable("dbo.StatusTraces");
        }
    }
}
