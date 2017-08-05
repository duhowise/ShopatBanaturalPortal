namespace ShopatBanaturalPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InventoryItems", "ShipmentHistory", c => c.String());
            AddColumn("dbo.InventoryItems", "TransactionHistory", c => c.String());
            DropColumn("dbo.InventoryItems", "TypeDescription");
            DropColumn("dbo.InventoryItems", "MoreDescription");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InventoryItems", "MoreDescription", c => c.String());
            AddColumn("dbo.InventoryItems", "TypeDescription", c => c.String());
            DropColumn("dbo.InventoryItems", "TransactionHistory");
            DropColumn("dbo.InventoryItems", "ShipmentHistory");
        }
    }
}
