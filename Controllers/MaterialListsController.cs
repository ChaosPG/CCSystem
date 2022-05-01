using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CCSystem_New.Models;

namespace CCSystem_New.Controllers
{
    public class MaterialListsController : Controller
    {
        private DB_Entities db = new DB_Entities();

        // GET: MaterialLists
        public ActionResult Index()
        {
            if (Session["idUser"] != null)
            {
                var materailLists = db.MaterailLists.Include(m => m.Floor_Material_01).Include(m => m.Floor_Material_02).Include(m => m.General_Material_01).Include(m => m.General_Material_02).Include(m => m.Project).Include(m => m.Roof_Material_01).Include(m => m.Roof_Material_02);
                return View(materailLists.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }

        // GET: MaterialLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaterialList materialList = db.MaterailLists.Find(id);
            if (materialList == null)
            {
                return HttpNotFound();
            }
            return View(materialList);
        }

        // GET: MaterialLists/Create
        public ActionResult Create()
        {
            ViewBag.Floor_01_ID = new SelectList(db.Floor_Material_01, "ID", "MaterialName");
            ViewBag.Floor_02_ID = new SelectList(db.Floor_Material_02, "ID", "MaterialName");
            ViewBag.General_01_ID = new SelectList(db.General_Material_01, "ID", "MaterialName");
            ViewBag.General_02_ID = new SelectList(db.General_Material_02, "ID", "MaterialName");
            ViewBag.ProjectID = new SelectList(db.Projects, "ID", "ProjectName");
            ViewBag.Roof_01_ID = new SelectList(db.Roof_Material_01, "ID", "MaterialName");
            ViewBag.Roof_02_ID = new SelectList(db.Roof_Material_02, "ID", "MaterialName");
            return View();
        }

        // POST: MaterialLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ProjectID,Floor_01_ID,Material_01_quantity,Floor_02_ID,Material_02_quantity,General_01_ID,Material_03_quantity,General_02_ID,Material_04_quantity,Roof_01_ID,Material_05_quantity,Roof_02_ID,Material_06_quantity,MaterialCost")] MaterialList materialList)
        {
            if (ModelState.IsValid)
            {
                decimal mrCost1 = (from mr in db.MaterailLists
                                   join fm1 in db.Floor_Material_01
                                   on mr.Floor_01_ID equals fm1.ID
                                   select mr.MaterialCost).FirstOrDefault();

                decimal mrCost2 = (from mr in db.MaterailLists
                                   join fm2 in db.Floor_Material_02
                                   on mr.Floor_02_ID equals fm2.ID
                                   select mr.MaterialCost).FirstOrDefault();

                decimal mrCost3 = (from mr in db.MaterailLists
                                   join gm1 in db.General_Material_01
                                   on mr.General_01_ID equals gm1.ID
                                   select mr.MaterialCost).FirstOrDefault();

                decimal mrCost4 = (from mr in db.MaterailLists
                                   join gm2 in db.General_Material_02
                                   on mr.General_02_ID equals gm2.ID
                                   select mr.MaterialCost).FirstOrDefault();

                decimal mrCost5 = (from mr in db.MaterailLists
                                   join rm1 in db.Roof_Material_01
                                   on mr.Roof_01_ID equals rm1.ID
                                   select mr.MaterialCost).FirstOrDefault();

                decimal mrCost6 = (from mr in db.MaterailLists
                                   join rm2 in db.Roof_Material_02
                                   on mr.Roof_02_ID equals rm2.ID
                                   select mr.MaterialCost).FirstOrDefault();

                if (materialList.Material_02_quantity != null)
                {
                    materialList.MaterialCost = Convert.ToDecimal(materialList.Material_01_quantity * mrCost1) +
                                                Convert.ToDecimal(materialList.Material_02_quantity * mrCost2);

                    if (materialList.Material_03_quantity != null)
                    {
                        materialList.MaterialCost = Convert.ToDecimal(materialList.Material_01_quantity * mrCost1) +
                                                    Convert.ToDecimal(materialList.Material_02_quantity * mrCost2) +
                                                    Convert.ToDecimal(materialList.Material_03_quantity * mrCost3);

                        if (materialList.Material_04_quantity != null)
                        {
                            materialList.MaterialCost = Convert.ToDecimal(materialList.Material_01_quantity * mrCost1) +
                                                        Convert.ToDecimal(materialList.Material_02_quantity * mrCost2) +
                                                        Convert.ToDecimal(materialList.Material_03_quantity * mrCost3) +
                                                        Convert.ToDecimal(materialList.Material_04_quantity * mrCost4);

                            if (materialList.Material_05_quantity != null)
                            {
                                materialList.MaterialCost = Convert.ToDecimal(materialList.Material_01_quantity * mrCost1) +
                                                            Convert.ToDecimal(materialList.Material_02_quantity * mrCost2) +
                                                            Convert.ToDecimal(materialList.Material_03_quantity * mrCost3) +
                                                            Convert.ToDecimal(materialList.Material_04_quantity * mrCost4) +
                                                            Convert.ToDecimal(materialList.Material_05_quantity * mrCost5);

                                if (materialList.Material_06_quantity != null)
                                {
                                    materialList.MaterialCost = Convert.ToDecimal(materialList.Material_01_quantity * mrCost1) +
                                                                Convert.ToDecimal(materialList.Material_02_quantity * mrCost2) +
                                                                Convert.ToDecimal(materialList.Material_03_quantity * mrCost3) +
                                                                Convert.ToDecimal(materialList.Material_04_quantity * mrCost4) +
                                                                Convert.ToDecimal(materialList.Material_05_quantity * mrCost5) +
                                                                Convert.ToDecimal(materialList.Material_06_quantity * mrCost6);
                                }
                            }
                        }
                    }
                }

                ViewBag.materialCost = materialList.MaterialCost;

                db.MaterailLists.Add(materialList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Floor_01_ID = new SelectList(db.Floor_Material_01, "ID", "MaterialName", materialList.Floor_01_ID);
            ViewBag.Floor_02_ID = new SelectList(db.Floor_Material_02, "ID", "MaterialName", materialList.Floor_02_ID);
            ViewBag.General_01_ID = new SelectList(db.General_Material_01, "ID", "MaterialName", materialList.General_01_ID);
            ViewBag.General_02_ID = new SelectList(db.General_Material_02, "ID", "MaterialName", materialList.General_02_ID);
            ViewBag.ProjectID = new SelectList(db.Projects, "ID", "ProjectName", materialList.ProjectID);
            ViewBag.Roof_01_ID = new SelectList(db.Roof_Material_01, "ID", "MaterialName", materialList.Roof_01_ID);
            ViewBag.Roof_02_ID = new SelectList(db.Roof_Material_02, "ID", "MaterialName", materialList.Roof_02_ID);
            return View(materialList);
        }

        // GET: MaterialLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaterialList materialList = db.MaterailLists.Find(id);
            if (materialList == null)
            {
                return HttpNotFound();
            }
            ViewBag.Floor_01_ID = new SelectList(db.Floor_Material_01, "ID", "MaterialName", materialList.Floor_01_ID);
            ViewBag.Floor_02_ID = new SelectList(db.Floor_Material_02, "ID", "MaterialName", materialList.Floor_02_ID);
            ViewBag.General_01_ID = new SelectList(db.General_Material_01, "ID", "MaterialName", materialList.General_01_ID);
            ViewBag.General_02_ID = new SelectList(db.General_Material_02, "ID", "MaterialName", materialList.General_02_ID);
            ViewBag.ProjectID = new SelectList(db.Projects, "ID", "ProjectName", materialList.ProjectID);
            ViewBag.Roof_01_ID = new SelectList(db.Roof_Material_01, "ID", "MaterialName", materialList.Roof_01_ID);
            ViewBag.Roof_02_ID = new SelectList(db.Roof_Material_02, "ID", "MaterialName", materialList.Roof_02_ID);
            return View(materialList);
        }

        // POST: MaterialLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProjectID,Floor_01_ID,Material_01_quantity,Floor_02_ID,Material_02_quantity,General_01_ID,Material_03_quantity,General_02_ID,Material_04_quantity,Roof_01_ID,Material_05_quantity,Roof_02_ID,Material_06_quantity,MaterialCost")] MaterialList materialList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(materialList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Floor_01_ID = new SelectList(db.Floor_Material_01, "ID", "MaterialName", materialList.Floor_01_ID);
            ViewBag.Floor_02_ID = new SelectList(db.Floor_Material_02, "ID", "MaterialName", materialList.Floor_02_ID);
            ViewBag.General_01_ID = new SelectList(db.General_Material_01, "ID", "MaterialName", materialList.General_01_ID);
            ViewBag.General_02_ID = new SelectList(db.General_Material_02, "ID", "MaterialName", materialList.General_02_ID);
            ViewBag.ProjectID = new SelectList(db.Projects, "ID", "ProjectName", materialList.ProjectID);
            ViewBag.Roof_01_ID = new SelectList(db.Roof_Material_01, "ID", "MaterialName", materialList.Roof_01_ID);
            ViewBag.Roof_02_ID = new SelectList(db.Roof_Material_02, "ID", "MaterialName", materialList.Roof_02_ID);
            return View(materialList);
        }

        // GET: MaterialLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaterialList materialList = db.MaterailLists.Find(id);
            if (materialList == null)
            {
                return HttpNotFound();
            }
            return View(materialList);
        }

        // POST: MaterialLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MaterialList materialList = db.MaterailLists.Find(id);
            db.MaterailLists.Remove(materialList);
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
