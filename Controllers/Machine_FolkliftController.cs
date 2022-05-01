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
    public class Machine_FolkliftController : Controller
    {
        private DB_Entities db = new DB_Entities();

        // GET: Machine_Folklift
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

                var mfShow = from mf in db.Machine_Folklifts select mf;

                if (!String.IsNullOrEmpty(searchString))
                {
                    mfShow = mfShow.Where(s => s.MachineName.Contains(searchString));
                }
                switch (Sorting_Order)
                {
                    case "Name_Description":
                        mfShow = mfShow.OrderByDescending(mf => mf.MachineName);
                        break;
                    default:
                        mfShow = mfShow.OrderBy(mf => mf.MachineName);
                        break;
                }
                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return View(mfShow.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        // GET: Machine_Folklift/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine_Folklift machine_Folklift = db.Machine_Folklifts.Find(id);
            if (machine_Folklift == null)
            {
                return HttpNotFound();
            }
            return View(machine_Folklift);
        }

        // GET: Machine_Folklift/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Machine_Folklift/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MachineName,MachineRentalPrice")] Machine_Folklift machine_Folklift)
        {
            if (ModelState.IsValid)
            {
                db.Machine_Folklifts.Add(machine_Folklift);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(machine_Folklift);
        }

        // GET: Machine_Folklift/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine_Folklift machine_Folklift = db.Machine_Folklifts.Find(id);
            if (machine_Folklift == null)
            {
                return HttpNotFound();
            }
            return View(machine_Folklift);
        }

        // POST: Machine_Folklift/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MachineName,MachineRentalPrice")] Machine_Folklift machine_Folklift)
        {
            if (ModelState.IsValid)
            {
                db.Entry(machine_Folklift).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(machine_Folklift);
        }

        // GET: Machine_Folklift/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine_Folklift machine_Folklift = db.Machine_Folklifts.Find(id);
            if (machine_Folklift == null)
            {
                return HttpNotFound();
            }
            return View(machine_Folklift);
        }

        // POST: Machine_Folklift/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Machine_Folklift machine_Folklift = db.Machine_Folklifts.Find(id);
            db.Machine_Folklifts.Remove(machine_Folklift);
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
