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
    public class Machine_CraneController : Controller
    {
        private DB_Entities db = new DB_Entities();

        // GET: Machine_Crane
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

                var mcShow = from mc in db.Machine_Cranes select mc;

                if (!String.IsNullOrEmpty(searchString))
                {
                    mcShow = mcShow.Where(s => s.MachineName.Contains(searchString));
                }
                switch (Sorting_Order)
                {
                    case "Name_Description":
                        mcShow = mcShow.OrderByDescending(mc => mc.MachineName);
                        break;
                    default:
                        mcShow = mcShow.OrderBy(mc => mc.MachineName);
                        break;
                }
                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return View(mcShow.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        // GET: Machine_Crane/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine_Crane machine_Crane = db.Machine_Cranes.Find(id);
            if (machine_Crane == null)
            {
                return HttpNotFound();
            }
            return View(machine_Crane);
        }

        // GET: Machine_Crane/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Machine_Crane/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MachineName,MachineRentalPrice")] Machine_Crane machine_Crane)
        {
            if (ModelState.IsValid)
            {
                db.Machine_Cranes.Add(machine_Crane);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(machine_Crane);
        }

        // GET: Machine_Crane/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine_Crane machine_Crane = db.Machine_Cranes.Find(id);
            if (machine_Crane == null)
            {
                return HttpNotFound();
            }
            return View(machine_Crane);
        }

        // POST: Machine_Crane/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MachineName,MachineRentalPrice")] Machine_Crane machine_Crane)
        {
            if (ModelState.IsValid)
            {
                db.Entry(machine_Crane).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(machine_Crane);
        }

        // GET: Machine_Crane/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine_Crane machine_Crane = db.Machine_Cranes.Find(id);
            if (machine_Crane == null)
            {
                return HttpNotFound();
            }
            return View(machine_Crane);
        }

        // POST: Machine_Crane/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Machine_Crane machine_Crane = db.Machine_Cranes.Find(id);
            db.Machine_Cranes.Remove(machine_Crane);
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
