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
    public class Roof_Material_02Controller : Controller
    {
        private DB_Entities db = new DB_Entities();

        // GET: Roof_Material_02
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

                var rm02Show = from rm02 in db.Roof_Material_02 select rm02;

                if (!String.IsNullOrEmpty(searchString))
                {
                    rm02Show = rm02Show.Where(s => s.MaterialName.Contains(searchString));
                }
                switch (Sorting_Order)
                {
                    case "Name_Description":
                        rm02Show = rm02Show.OrderByDescending(rm02 => rm02.MaterialName);
                        break;
                    default:
                        rm02Show = rm02Show.OrderBy(rm02 => rm02.MaterialName);
                        break;
                }
                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return View(rm02Show.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        // GET: Roof_Material_02/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roof_Material_02 roof_Material_02 = db.Roof_Material_02.Find(id);
            if (roof_Material_02 == null)
            {
                return HttpNotFound();
            }
            return View(roof_Material_02);
        }

        // GET: Roof_Material_02/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Roof_Material_02/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MaterialName,MaterialPrice")] Roof_Material_02 roof_Material_02)
        {
            if (ModelState.IsValid)
            {
                db.Roof_Material_02.Add(roof_Material_02);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(roof_Material_02);
        }

        // GET: Roof_Material_02/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roof_Material_02 roof_Material_02 = db.Roof_Material_02.Find(id);
            if (roof_Material_02 == null)
            {
                return HttpNotFound();
            }
            return View(roof_Material_02);
        }

        // POST: Roof_Material_02/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MaterialName,MaterialPrice")] Roof_Material_02 roof_Material_02)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roof_Material_02).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(roof_Material_02);
        }

        // GET: Roof_Material_02/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roof_Material_02 roof_Material_02 = db.Roof_Material_02.Find(id);
            if (roof_Material_02 == null)
            {
                return HttpNotFound();
            }
            return View(roof_Material_02);
        }

        // POST: Roof_Material_02/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Roof_Material_02 roof_Material_02 = db.Roof_Material_02.Find(id);
            db.Roof_Material_02.Remove(roof_Material_02);
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
