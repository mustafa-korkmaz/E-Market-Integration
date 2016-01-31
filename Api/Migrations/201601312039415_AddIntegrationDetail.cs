namespace Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIntegrationDetail : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IntegrationDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IntegrationId = c.Int(nullable: false),
                        Url = c.String(nullable: false, maxLength: 250, storeType: "varchar"),
                        ExportType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Integrations", t => t.IntegrationId, cascadeDelete: true)
                .Index(t => t.IntegrationId);
            
            DropColumn("dbo.Integrations", "Url");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Integrations", "Url", c => c.String(nullable: false, maxLength: 250, storeType: "nvarchar"));
            DropForeignKey("dbo.IntegrationDetails", "IntegrationId", "dbo.Integrations");
            DropIndex("dbo.IntegrationDetails", new[] { "IntegrationId" });
            DropTable("dbo.IntegrationDetails");
        }
    }
}
