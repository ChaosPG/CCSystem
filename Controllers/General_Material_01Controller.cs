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
    public class General_Material_01Controller : Controller
    {
        private DB_Entities db = new DB_Entities();

        // GET: General_Material_01
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

                var gm01Show = from gm01 in db.General_Material_01 select gm01;

                if (!String.IsNullOrEmpty(searchString))
                {
                    gm01Show = gm01Show.Where(s => s.MaterialName.Contains(searchString));
                }
                switch (Sorting_Order)
                {
                    case "Name_Description":
                        gm01Show = gm01Show.OrderByDescending(gm01 => gm01.MaterialName);
                        break;
                    default:
                        gm01Show = gm01Show.OrderBy(gm01 => gm01.MaterialName);
                        break;
                }
                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return View(gm01Show.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: General_Material_01/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            General_Material_01 general_Material_01 = db.General_Material_01.Find(id);
            if (general_Material_01 == null)
            {
                return HttpNotFound();
            }
            return View(general_Material_01);
        }

        // GET: General_Material_01/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: General_Material_01/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MaterialName,MaterialPrice")] General_Material_01 general_Material_01)
        {
            if (ModelState.IsValid)
            {
                db.General_Material_01.Add(general_Material_01);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(general_Material_01);
        }

        // GET: General_Material_01/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            General_Material_01 general_Material_01 = db.General_Material_01.Find(id);
            if (general_Material_01 == null)
            {
                return HttpNotFound();
            }
            return View(general_Material_01);
        }

        // POST: General_Material_01/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MaterialName,MaterialPrice")] General_Material_01 general_Material_01)
        {
            if (ModelState.IsValid)
            {
                db.Entry(general_Material_01).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(general_Material_01);
        }

        // GET: General_Material_01/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            General_Material_01 general_Material_01 = db.General_Material_01.Find(id);
            if (general_Material_01 == null)
            {
                return HttpNotFound();
            }
            return View(general_Material_01);
        }

        // POST: General_Material_01/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            General_Material_01 general_Material_01 = db.General_Material_01.Find(id);
            db.General_Material_01.Remove(general_Material_01);
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
