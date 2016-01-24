namespace Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIndexToMarketUsers : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.MarketUsers", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.MarketUsers", new[] { "Name" });
        }
    }
}
