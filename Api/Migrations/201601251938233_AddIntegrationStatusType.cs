namespace Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddIntegrationStatusType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Integrations", "Status", c => c.Int(nullable: false, defaultValue: 1)); // default status is Active
        }

        public override void Down()
        {
            DropColumn("dbo.Integrations", "Status");
        }
    }
}
