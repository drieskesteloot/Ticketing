namespace Ticketing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IssueTrace : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IssueTraces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LogDateTime = c.DateTime(nullable: false),
                        Onderwerp = c.String(),
                        Status = c.String(),
                        Omschrijving = c.String(),
                        Prioriteit = c.String(),
                        DatumAangemaakt = c.DateTime(nullable: false),
                        DatumAanpassing = c.DateTime(nullable: false),
                        Teruggestuurd = c.Boolean(nullable: false),
                        RedenTeruggestuurd = c.String(),
                        ApplicationUser = c.String(),
                        CreationGebruiker = c.String(),
                        SolverGebruiker = c.String(),
                        Issue_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Issues", t => t.Issue_Id)
                .Index(t => t.Issue_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IssueTraces", "Issue_Id", "dbo.Issues");
            DropIndex("dbo.IssueTraces", new[] { "Issue_Id" });
            DropTable("dbo.IssueTraces");
        }
    }
}
