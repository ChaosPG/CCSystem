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
    public class Machine_BulldozerController : Controller
    {
        private DB_Entities db = new DB_Entities();

        // GET: Machine_Bulldozer
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

                var mb01Show = from mb01 in db.Machine_Asphalts select mb01;

                if (!String.IsNullOrEmpty(searchString))
                {
                    mb01Show = mb01Show.Where(s => s.MachineName.Contains(searchString));
                }
                switch (Sorting_Order)
                {
                    case "Name_Description":
                        mb01Show = mb01Show.OrderByDescending(mb01 => mb01.MachineName);
                        break;
                    default:
                        mb01Show = mb01Show.OrderBy(mb01 => mb01.MachineName);
                        break;
                }
                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return View(mb01Show.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        // GET: Machine_Bulldozer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine_Bulldozer machine_Bulldozer = db.Machine_Bulldozers.Find(id);
            if (machine_Bulldozer == null)
            {
                return HttpNotFound();
            }
            return View(machine_Bulldozer);
        }

        // GET: Machine_Bulldozer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Machine_Bulldozer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MachineName,MachineRentalPrice")] Machine_Bulldozer machine_Bulldozer)
        {
            if (ModelState.IsValid)
            {
                db.Machine_Bulldozers.Add(machine_Bulldozer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(machine_Bulldozer);
        }

        // GET: Machine_Bulldozer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine_Bulldozer machine_Bulldozer = db.Machine_Bulldozers.Find(id);
            if (machine_Bulldozer == null)
            {
                return HttpNotFound();
            }
            return View(machine_Bulldozer);
        }

        // POST: Machine_Bulldozer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MachineName,MachineRentalPrice")] Machine_Bulldozer machine_Bulldozer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(machine_Bulldozer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(machine_Bulldozer);
        }

        // GET: Machine_Bulldozer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine_Bulldozer machine_Bulldozer = db.Machine_Bulldozers.Find(id);
            if (machine_Bulldozer == null)
            {
                return HttpNotFound();
            }
            return View(machine_Bulldozer);
        }

        // POST: Machine_Bulldozer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Machine_Bulldozer machine_Bulldozer = db.Machine_Bulldozers.Find(id);
            db.Machine_Bulldozers.Remove(machine_Bulldozer);
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
