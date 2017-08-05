namespace ShopatBanaturalPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InventoryItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ItemName = c.String(),
                        ItemNumber = c.Int(nullable: false),
                        SoldtoDate = c.Int(nullable: false),
                        SoldthisMonth = c.Int(nullable: false),
                        SoldthisYear = c.Int(nullable: false),
                        QuantityLeft = c.Int(nullable: false),
                        LastShipmentRecieved = c.DateTime(nullable: false),
                        Type = c.String(),
                        TypeDescription = c.String(),
                        GeneralDescription = c.String(),
                        MoreDescription = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.InventoryItems");
        }
    }
}
