using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DatabaseLayer;

namespace CareerCatalyst.Controllers
{
    public class CountryTablesController : Controller
    {
        private CareerCatalystDBEntities db = new CareerCatalystDBEntities();

        // GET: CountryTables
        public ActionResult Index()
        {
            return View(db.CountryTables.ToList());
        }

        // GET: CountryTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CountryTable countryTable = db.CountryTables.Find(id);
            if (countryTable == null)
            {
                return HttpNotFound();
            }
            return View(countryTable);
        }

        // GET: CountryTables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CountryTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CountryID,Country")] CountryTable countryTable)
        {
            if (ModelState.IsValid)
            {
                db.CountryTables.Add(countryTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(countryTable);
        }

        // GET: CountryTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CountryTable countryTable = db.CountryTables.Find(id);
            if (countryTable == null)
            {
                return HttpNotFound();
            }
            return View(countryTable);
        }

        // POST: CountryTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CountryID,Country")] CountryTable countryTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(countryTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(countryTable);
        }

        // GET: CountryTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CountryTable countryTable = db.CountryTables.Find(id);
            if (countryTable == null)
            {
                return HttpNotFound();
            }
            return View(countryTable);
        }

        // POST: CountryTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CountryTable countryTable = db.CountryTables.Find(id);
            db.CountryTables.Remove(countryTable);
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
