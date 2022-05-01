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
    public class General_Material_02Controller : Controller
    {
        private DB_Entities db = new DB_Entities();

        // GET: General_Material_02
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

                var gm02Show = from gm02 in db.General_Material_02 select gm02;

                if (!String.IsNullOrEmpty(searchString))
                {
                    gm02Show = gm02Show.Where(s => s.MaterialName.Contains(searchString));
                }
                switch (Sorting_Order)
                {
                    case "Name_Description":
                        gm02Show = gm02Show.OrderByDescending(gm02 => gm02.MaterialName);
                        break;
                    default:
                        gm02Show = gm02Show.OrderBy(gm02 => gm02.MaterialName);
                        break;
                }
                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return View(gm02Show.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        // GET: General_Material_02/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            General_Material_02 general_Material_02 = db.General_Material_02.Find(id);
            if (general_Material_02 == null)
            {
                return HttpNotFound();
            }
            return View(general_Material_02);
        }

        // GET: General_Material_02/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: General_Material_02/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MaterialName,MaterialPrice")] General_Material_02 general_Material_02)
        {
            if (ModelState.IsValid)
            {
                db.General_Material_02.Add(general_Material_02);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(general_Material_02);
        }

        // GET: General_Material_02/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            General_Material_02 general_Material_02 = db.General_Material_02.Find(id);
            if (general_Material_02 == null)
            {
                return HttpNotFound();
            }
            return View(general_Material_02);
        }

        // POST: General_Material_02/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MaterialName,MaterialPrice")] General_Material_02 general_Material_02)
        {
            if (ModelState.IsValid)
            {
                db.Entry(general_Material_02).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(general_Material_02);
        }

        // GET: General_Material_02/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            General_Material_02 general_Material_02 = db.General_Material_02.Find(id);
            if (general_Material_02 == null)
            {
                return HttpNotFound();
            }
            return View(general_Material_02);
        }

        // POST: General_Material_02/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            General_Material_02 general_Material_02 = db.General_Material_02.Find(id);
            db.General_Material_02.Remove(general_Material_02);
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
