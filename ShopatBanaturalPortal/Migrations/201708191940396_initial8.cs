namespace ShopatBanaturalPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InventoryItems", "Brand", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.InventoryItems", "Brand");
        }
    }
}
