namespace ShopatBanaturalPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.InventoryItems", "LastShipmentRecieved", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.InventoryItems", "LastShipmentRecieved", c => c.DateTime(nullable: false));
        }
    }
}
