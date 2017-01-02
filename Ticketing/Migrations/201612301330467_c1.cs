namespace Ticketing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class c1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Issues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Onderwerp = c.String(),
                        StatusInfoId = c.Int(nullable: false),
                        Omschrijving = c.String(),
                        PrioriteitInfoId = c.Int(nullable: false),
                        DatumAanpassing = c.DateTime(nullable: false),
                        DatumAangemaakt = c.DateTime(nullable: false),
                        Teruggestuurd = c.Boolean(nullable: false),
                        RedenTeruggestuurd = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PrioriteitInfoes", t => t.PrioriteitInfoId, cascadeDelete: true)
                .ForeignKey("dbo.StatusInfoes", t => t.StatusInfoId, cascadeDelete: true)
                .Index(t => t.StatusInfoId)
                .Index(t => t.PrioriteitInfoId);
            
            DropColumn("dbo.AspNetUsers", "Gebruikersnaam");
            DropColumn("dbo.AspNetUsers", "EmailAdres");
            DropColumn("dbo.AspNetUsers", "Straatnaam");
            DropColumn("dbo.AspNetUsers", "Huisnummer");
            DropColumn("dbo.AspNetUsers", "Bus");
            DropColumn("dbo.AspNetUsers", "Postcode");
            DropColumn("dbo.AspNetUsers", "Gemeente");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Gemeente", c => c.String());
            AddColumn("dbo.AspNetUsers", "Postcode", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Bus", c => c.String());
            AddColumn("dbo.AspNetUsers", "Huisnummer", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Straatnaam", c => c.String());
            AddColumn("dbo.AspNetUsers", "EmailAdres", c => c.String());
            AddColumn("dbo.AspNetUsers", "Gebruikersnaam", c => c.String());
            DropForeignKey("dbo.Issues", "StatusInfoId", "dbo.StatusInfoes");
            DropForeignKey("dbo.Issues", "PrioriteitInfoId", "dbo.PrioriteitInfoes");
            DropIndex("dbo.Issues", new[] { "PrioriteitInfoId" });
            DropIndex("dbo.Issues", new[] { "StatusInfoId" });
            DropTable("dbo.Issues");
        }
    }
}
