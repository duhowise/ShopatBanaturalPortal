using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace ShopatBanaturalPortal.Models
{
    public class InventoryItem
    {
        public int ID { get; set; }
        public string ItemName {get; set; }
        public int ItemNumber { get; set; }
        public int SoldtoDate { get; set; }
        public int SoldthisMonth { get; set; }
        public int SoldthisYear { get; set; }
        public int QuantityLeft { get; set; }
        public string LastShipmentRecieved { get; set; }

        //medicine, food, herb
        public string Type { get; set; }
        public string ShipmentHistory { get; set; }
        public string GeneralDescription { get; set; }
        public string TransactionHistory { get; set; }

        //other qualities
        public float Price { get; set; }
        public string CustomID { get; set; }
        public string Brand { get; set; }


    }

    public class InventoryItemDbContext : System.Data.Entity.DbContext
    {
        
        public InventoryItemDbContext() : base("DefaultConnection")
        {

        }
        

        public DbSet<InventoryItem> InventoryItemDatabase { get; set; }
    }

}