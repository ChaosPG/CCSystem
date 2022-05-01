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
    public class Machine_RollerController : Controller
    {
        private DB_Entities db = new DB_Entities();

        // GET: Machine_Roller
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

                var mrShow = from mr in db.Machine_Rollers select mr;

                if (!String.IsNullOrEmpty(searchString))
                {
                    mrShow = mrShow.Where(s => s.MachineName.Contains(searchString));
                }
                switch (Sorting_Order)
                {
                    case "Name_Description":
                        mrShow = mrShow.OrderByDescending(mr => mr.MachineName);
                        break;
                    default:
                        mrShow = mrShow.OrderBy(mr => mr.MachineName);
                        break;
                }
                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return View(mrShow.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        // GET: Machine_Roller/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine_Roller machine_Roller = db.Machine_Rollers.Find(id);
            if (machine_Roller == null)
            {
                return HttpNotFound();
            }
            return View(machine_Roller);
        }

        // GET: Machine_Roller/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Machine_Roller/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MachineName,MachineRentalPrice")] Machine_Roller machine_Roller)
        {
            if (ModelState.IsValid)
            {
                db.Machine_Rollers.Add(machine_Roller);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(machine_Roller);
        }

        // GET: Machine_Roller/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine_Roller machine_Roller = db.Machine_Rollers.Find(id);
            if (machine_Roller == null)
            {
                return HttpNotFound();
            }
            return View(machine_Roller);
        }

        // POST: Machine_Roller/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MachineName,MachineRentalPrice")] Machine_Roller machine_Roller)
        {
            if (ModelState.IsValid)
            {
                db.Entry(machine_Roller).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(machine_Roller);
        }

        // GET: Machine_Roller/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine_Roller machine_Roller = db.Machine_Rollers.Find(id);
            if (machine_Roller == null)
            {
                return HttpNotFound();
            }
            return View(machine_Roller);
        }

        // POST: Machine_Roller/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Machine_Roller machine_Roller = db.Machine_Rollers.Find(id);
            db.Machine_Rollers.Remove(machine_Roller);
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
