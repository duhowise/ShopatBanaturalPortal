using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopatBanaturalPortal.Models;

namespace ShopatBanaturalPortal.Controllers
{
    public class InventoryItemsController : Controller
    {
        private InventoryItemDbContext db = new InventoryItemDbContext();

        // GET: InventoryItems
        public ActionResult Index(string searchString)
        {

            var ItemsSelected = from m in db.InventoryItemDatabase
                                  orderby m.Type, m.QuantityLeft
                                  select m;



            if (!String.IsNullOrEmpty(searchString))
            {
                ItemsSelected = from m in db.InventoryItemDatabase
                                where m.ItemName == searchString || m.Type == searchString || (m.QuantityLeft.ToString()).Contains(searchString)
                                orderby m.Type, m.QuantityLeft
                                select m;
            }

            return View(ItemsSelected);
        }

        public ActionResult TransactionHistory(int ID)
        {
            var SelectedItem = from S in db.InventoryItemDatabase
                               where S.ID == ID
                               select S;
            InventoryItem ItemModel = (InventoryItem)SelectedItem.First<InventoryItem>();

            string[] History = ItemModel.TransactionHistory.Split('*');

            return View(History);
        }
        public ActionResult ShipmentHistory(int ID)
        {
            var SelectedItem = from S in db.InventoryItemDatabase
                               where S.ID == ID
                               select S;
            InventoryItem ItemModel = (InventoryItem)SelectedItem.First<InventoryItem>();

            string[] History = ItemModel.ShipmentHistory.Split('*');
            return View(History);
        }

        // GET: InventoryItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventoryItem inventoryItem = db.InventoryItemDatabase.Find(id);
            if (inventoryItem == null)
            {
                return HttpNotFound();
            }
            return View(inventoryItem);
        }

        // GET: InventoryItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InventoryItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string Type, [Bind(Include = "ID,ItemName,Price,QuantityLeft,Type,GeneralDescription")] InventoryItem inventoryItem)
        {
            if (ModelState.IsValid)
            {
                inventoryItem.Type = Type;
                inventoryItem.LastShipmentRecieved = "Not Yet Stocked";
                inventoryItem.ItemNumber = inventoryItem.ID;

                db.InventoryItemDatabase.Add(inventoryItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(inventoryItem);
        }

        // GET: InventoryItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventoryItem inventoryItem = db.InventoryItemDatabase.Find(id);
            if (inventoryItem == null)
            {
                return HttpNotFound();
            }
            return View(inventoryItem);
        }

        // POST: InventoryItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ItemName,ItemNumber,SoldtoDate,SoldthisMonth,SoldthisYear,QuantityLeft,LastShipmentRecieved,Type,TypeDescription,GeneralDescription,MoreDescription")] InventoryItem inventoryItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inventoryItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(inventoryItem);
        }

        

        public ActionResult UpdateShipments(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventoryItem inventoryItem = db.InventoryItemDatabase.Find(id);
            if (inventoryItem == null)
            {
                return HttpNotFound();
            }
            return View(inventoryItem);
        }

        // POST: InventoryItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateShipments(int ID, string Restock)
        {
            var SelectedItem = from S in db.InventoryItemDatabase
                               where S.ID == ID
                               select S;
            InventoryItem ItemModel = (InventoryItem)SelectedItem.First<InventoryItem>();

            if (ModelState.IsValid)
            {
                ItemModel.LastShipmentRecieved = DateTime.Now.ToShortDateString();
                ItemModel.QuantityLeft = ItemModel.QuantityLeft + Convert.ToInt32(Restock);
                //split by **
                ItemModel.ShipmentHistory = ItemModel.ShipmentHistory + "[Restocked] " + Restock + " " + ItemModel.ItemName + ". [Restocked on] " + DateTime.Now.ToShortDateString() + "*";


                db.Entry(ItemModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ItemModel);
        }

        public ActionResult ItemSold(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventoryItem inventoryItem = db.InventoryItemDatabase.Find(id);
            if (inventoryItem == null)
            {
                return HttpNotFound();
            }
            return View(inventoryItem);
        }

        // POST: InventoryItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ItemSold(int ID, string Sold)
        {
            var SelectedItem = from S in db.InventoryItemDatabase
                               where S.ID == ID
                               select S;

            InventoryItem ItemModel = (InventoryItem)SelectedItem.First<InventoryItem>();

            if (ModelState.IsValid)
            {
                ItemModel.QuantityLeft = ItemModel.QuantityLeft - Convert.ToInt32(Sold);
                //split by **
                ItemModel.TransactionHistory = ItemModel.TransactionHistory + "[Sold] " + Sold + " " + ItemModel.ItemName + ". [Date] " + DateTime.Now.ToLocalTime() + ". [Revenue] " + (Convert.ToInt32(Sold) * ItemModel.Price) + " $" + "*";
                db.Entry(ItemModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ItemModel);
        }

        // GET: InventoryItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventoryItem inventoryItem = db.InventoryItemDatabase.Find(id);
            if (inventoryItem == null)
            {
                return HttpNotFound();
            }
            return View(inventoryItem);
        }

        // POST: InventoryItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InventoryItem inventoryItem = db.InventoryItemDatabase.Find(id);
            db.InventoryItemDatabase.Remove(inventoryItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
