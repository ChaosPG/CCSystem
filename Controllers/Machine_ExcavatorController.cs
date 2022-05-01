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
    public class Machine_ExcavatorController : Controller
    {
        private DB_Entities db = new DB_Entities();

        // GET: Machine_Excavator
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

                var meShow = from me in db.Machine_Excavators select me;

                if (!String.IsNullOrEmpty(searchString))
                {
                    meShow = meShow.Where(s => s.MachineName.Contains(searchString));
                }
                switch (Sorting_Order)
                {
                    case "Name_Description":
                        meShow = meShow.OrderByDescending(me => me.MachineName);
                        break;
                    default:
                        meShow = meShow.OrderBy(me => me.MachineName);
                        break;
                }
                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return View(meShow.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        // GET: Machine_Excavator/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine_Excavator machine_Excavator = db.Machine_Excavators.Find(id);
            if (machine_Excavator == null)
            {
                return HttpNotFound();
            }
            return View(machine_Excavator);
        }

        // GET: Machine_Excavator/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Machine_Excavator/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MachineName,MachineRentalPrice")] Machine_Excavator machine_Excavator)
        {
            if (ModelState.IsValid)
            {
                db.Machine_Excavators.Add(machine_Excavator);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(machine_Excavator);
        }

        // GET: Machine_Excavator/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine_Excavator machine_Excavator = db.Machine_Excavators.Find(id);
            if (machine_Excavator == null)
            {
                return HttpNotFound();
            }
            return View(machine_Excavator);
        }

        // POST: Machine_Excavator/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MachineName,MachineRentalPrice")] Machine_Excavator machine_Excavator)
        {
            if (ModelState.IsValid)
            {
                db.Entry(machine_Excavator).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(machine_Excavator);
        }

        // GET: Machine_Excavator/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine_Excavator machine_Excavator = db.Machine_Excavators.Find(id);
            if (machine_Excavator == null)
            {
                return HttpNotFound();
            }
            return View(machine_Excavator);
        }

        // POST: Machine_Excavator/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Machine_Excavator machine_Excavator = db.Machine_Excavators.Find(id);
            db.Machine_Excavators.Remove(machine_Excavator);
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
