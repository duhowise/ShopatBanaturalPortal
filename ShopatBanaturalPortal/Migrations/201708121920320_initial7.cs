namespace ShopatBanaturalPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InventoryItems", "CustomID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.InventoryItems", "CustomID");
        }
    }
}
