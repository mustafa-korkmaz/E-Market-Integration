namespace Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIntegrationType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Integrations", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Integrations", "Type", c => c.Byte(nullable: false));
        }
    }
}
