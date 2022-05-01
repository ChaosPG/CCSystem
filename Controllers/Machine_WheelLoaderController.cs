using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CCSystem_New.Models;
using PagedList;

namespace CCSystem_New.Controllers
{
    public class Machine_WheelLoaderController : Controller
    {
        private DB_Entities db = new DB_Entities();

        // GET: Machine_WheelLoader
        public ActionResult Index(string Sorting_Order, string currentFilter, string searchString, int? page)
        {
            if (Session["idUser"] != null)
            {
                ViewBag.CurrentSort = Sorting_Order;
                ViewBag.SortingName = String.IsNullOrEmpty(Sorting_Order) ? "Name_Description" : "";

                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                ViewBag.CurrentFilter = searchString;

                var mwShow = from mw in db.Machine_WheelLoaders select mw;

                if (!String.IsNullOrEmpty(searchString))
                {
                    mwShow = mwShow.Where(s => s.MachineName.Contains(searchString));
                }
                switch (Sorting_Order)
                {
                    case "Name_Description":
                        mwShow = mwShow.OrderByDescending(mw => mw.MachineName);
                        break;
                    default:
                        mwShow = mwShow.OrderBy(mw => mw.MachineName);
                        break;
                }
                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return View(mwShow.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        // GET: Machine_WheelLoader/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine_WheelLoader machine_WheelLoader = db.Machine_WheelLoaders.Find(id);
            if (machine_WheelLoader == null)
            {
                return HttpNotFound();
            }
            return View(machine_WheelLoader);
        }

        // GET: Machine_WheelLoader/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Machine_WheelLoader/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MachineName,MachineRentalPrice")] Machine_WheelLoader machine_WheelLoader)
        {
            if (ModelState.IsValid)
            {
                db.Machine_WheelLoaders.Add(machine_WheelLoader);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(machine_WheelLoader);
        }

        // GET: Machine_WheelLoader/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine_WheelLoader machine_WheelLoader = db.Machine_WheelLoaders.Find(id);
            if (machine_WheelLoader == null)
            {
                return HttpNotFound();
            }
            return View(machine_WheelLoader);
        }

        // POST: Machine_WheelLoader/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MachineName,MachineRentalPrice")] Machine_WheelLoader machine_WheelLoader)
        {
            if (ModelState.IsValid)
            {
                db.Entry(machine_WheelLoader).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(machine_WheelLoader);
        }

        // GET: Machine_WheelLoader/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine_WheelLoader machine_WheelLoader = db.Machine_WheelLoaders.Find(id);
            if (machine_WheelLoader == null)
            {
                return HttpNotFound();
            }
            return View(machine_WheelLoader);
        }

        // POST: Machine_WheelLoader/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Machine_WheelLoader machine_WheelLoader = db.Machine_WheelLoaders.Find(id);
            db.Machine_WheelLoaders.Remove(machine_WheelLoader);
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
