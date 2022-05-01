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
    public class Machine_AsphaltController : Controller
    {
        private DB_Entities db = new DB_Entities();
        public static decimal maForCal;

        // GET: Machine_Asphalt
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

                var ma01Show = from ma01 in db.Machine_Asphalts select ma01;

                if (!String.IsNullOrEmpty(searchString))
                {
                    ma01Show = ma01Show.Where(s => s.MachineName.Contains(searchString));
                }
                switch (Sorting_Order)
                {
                    case "Name_Description":
                        ma01Show = ma01Show.OrderByDescending(ma01 => ma01.MachineName);
                        break;
                    default:
                        ma01Show = ma01Show.OrderBy(ma01 => ma01.MachineName);
                        break;
                }
                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return View(ma01Show.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
           
        }

        // GET: Machine_Asphalt/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine_Asphalt machine_Asphalt = db.Machine_Asphalts.Find(id);
            if (machine_Asphalt == null)
            {
                return HttpNotFound();
            }
            return View(machine_Asphalt);
        }

        // GET: Machine_Asphalt/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Machine_Asphalt/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MachineName,MachineRentalPrice")] Machine_Asphalt machine_Asphalt)
        {
            if (ModelState.IsValid)
            {
                db.Machine_Asphalts.Add(machine_Asphalt);
                maForCal = machine_Asphalt.MachineRentalPrice;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(machine_Asphalt);
        }

        // GET: Machine_Asphalt/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine_Asphalt machine_Asphalt = db.Machine_Asphalts.Find(id);
            if (machine_Asphalt == null)
            {
                return HttpNotFound();
            }
            return View(machine_Asphalt);
        }

        // POST: Machine_Asphalt/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MachineName,MachineRentalPrice")] Machine_Asphalt machine_Asphalt)
        {
            if (ModelState.IsValid)
            {
                db.Entry(machine_Asphalt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(machine_Asphalt);
        }

        // GET: Machine_Asphalt/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine_Asphalt machine_Asphalt = db.Machine_Asphalts.Find(id);
            if (machine_Asphalt == null)
            {
                return HttpNotFound();
            }
            return View(machine_Asphalt);
        }

        // POST: Machine_Asphalt/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Machine_Asphalt machine_Asphalt = db.Machine_Asphalts.Find(id);
            db.Machine_Asphalts.Remove(machine_Asphalt);
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
