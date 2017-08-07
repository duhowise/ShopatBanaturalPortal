namespace ShopatBanaturalPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InventoryItems", "Price", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.InventoryItems", "Price");
        }
    }
}
