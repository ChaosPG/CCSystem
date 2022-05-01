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
    public class MachineListsController : Controller
    {
        private DB_Entities db = new DB_Entities();

        // GET: MachineLists
        public ActionResult Index()
        {
            if (Session["idUser"] != null)
            {
                var machineLists = db.MachineLists.Include(m => m.Machine_Asphalt).Include(m => m.Machine_Bulldozer).Include(m => m.Machine_Crane).Include(m => m.Machine_Excavator).Include(m => m.Machine_Folklift).Include(m => m.Machine_Roller).Include(m => m.Machine_WheelLoader).Include(m => m.Project);
                return View(machineLists.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }

        // GET: MachineLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MachineList machineList = db.MachineLists.Find(id);
            if (machineList == null)
            {
                return HttpNotFound();
            }
            return View(machineList);
        }

        // GET: MachineLists/Create
        public ActionResult Create()
        {
            ViewBag.MA_ID = new SelectList(db.Machine_Asphalts, "ID", "MachineName");
            ViewBag.MB_ID = new SelectList(db.Machine_Bulldozers, "ID", "MachineName");
            ViewBag.MC_ID = new SelectList(db.Machine_Cranes, "ID", "MachineName");
            ViewBag.ME_ID = new SelectList(db.Machine_Excavators, "ID", "MachineName");
            ViewBag.MF_ID = new SelectList(db.Machine_Folklifts, "ID", "MachineName");
            ViewBag.MR_ID = new SelectList(db.Machine_Rollers, "ID", "MachineName");
            ViewBag.MW_ID = new SelectList(db.Machine_WheelLoaders, "ID", "MachineName");
            ViewBag.ProjectID = new SelectList(db.Projects, "ID", "ProjectName");
            return View();
        }

        // POST: MachineLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ProjectID,MA_ID,Machine_01_Unit,MB_ID,Machine_02_Unit,MC_ID,Machine_03_Unit,ME_ID,Machine_04_Unit,MF_ID,Machine_05_Unit,MR_ID,Machine_06_Unit,MW_ID,Machine_07_Unit,MachineRental")] MachineList machineList)
        {
            if (ModelState.IsValid)
            {
                // Calculate MachineRental For Project

                decimal mlRentalPrice1 = (from ml in db.MachineLists
                                          join ma in db.Machine_Asphalts
                                          on ml.MA_ID equals ma.ID
                                          select ma.MachineRentalPrice).FirstOrDefault();

                decimal mlRentalPrice2 = (from ml in db.MachineLists
                                          join mb in db.Machine_Bulldozers
                                          on ml.MB_ID equals mb.ID
                                          select mb.MachineRentalPrice).FirstOrDefault();

                decimal mlRentalPrice3 = (from ml in db.MachineLists
                                          join mc in db.Machine_Cranes
                                          on ml.MC_ID equals mc.ID
                                          select mc.MachineRentalPrice).FirstOrDefault();

                decimal mlRentalPrice4 = (from ml in db.MachineLists
                                          join me in db.Machine_Excavators
                                          on ml.ME_ID equals me.ID
                                          select me.MachineRentalPrice).FirstOrDefault();

                decimal mlRentalPrice5 = (from ml in db.MachineLists
                                          join mf in db.Machine_Folklifts
                                          on ml.MF_ID equals mf.ID
                                          select mf.MachineRentalPrice).FirstOrDefault();

                decimal mlRentalPrice6 = (from ml in db.MachineLists
                                          join mr in db.Machine_Rollers
                                          on ml.MR_ID equals mr.ID
                                          select mr.MachineRentalPrice).FirstOrDefault();

                decimal mlRentalPrice7 = (from ml in db.MachineLists
                                          join mw in db.Machine_WheelLoaders
                                          on ml.MW_ID equals mw.ID
                                          select mw.MachineRentalPrice).FirstOrDefault();

                if (machineList.Machine_02_Unit != null)
                {
                    machineList.MachineRental = Convert.ToDecimal(machineList.Machine_01_Unit * mlRentalPrice1) +
                            Convert.ToDecimal(machineList.Machine_02_Unit * mlRentalPrice2);

                    if (machineList.Machine_03_Unit != null)
                    {
                        machineList.MachineRental = Convert.ToDecimal(machineList.Machine_01_Unit * mlRentalPrice1) +
                            Convert.ToDecimal(machineList.Machine_02_Unit * mlRentalPrice2) +
                            Convert.ToDecimal(machineList.Machine_03_Unit * mlRentalPrice3);

                        if (machineList.Machine_04_Unit != null)
                        {
                            machineList.MachineRental = Convert.ToDecimal(machineList.Machine_01_Unit * mlRentalPrice1) +
                            Convert.ToDecimal(machineList.Machine_02_Unit * mlRentalPrice2) +
                            Convert.ToDecimal(machineList.Machine_03_Unit * mlRentalPrice3) +
                            Convert.ToDecimal(machineList.Machine_04_Unit * mlRentalPrice4);

                            if (machineList.Machine_05_Unit != null)
                            {
                                machineList.MachineRental = Convert.ToDecimal(machineList.Machine_01_Unit * mlRentalPrice1) +
                                Convert.ToDecimal(machineList.Machine_02_Unit * mlRentalPrice2) +
                                Convert.ToDecimal(machineList.Machine_03_Unit * mlRentalPrice3) +
                                Convert.ToDecimal(machineList.Machine_04_Unit * mlRentalPrice4) +
                                Convert.ToDecimal(machineList.Machine_05_Unit * mlRentalPrice5);

                                if (machineList.Machine_06_Unit != null)
                                {
                                    machineList.MachineRental = Convert.ToDecimal(machineList.Machine_01_Unit * mlRentalPrice1) +
                                    Convert.ToDecimal(machineList.Machine_02_Unit * mlRentalPrice2) +
                                    Convert.ToDecimal(machineList.Machine_03_Unit * mlRentalPrice3) +
                                    Convert.ToDecimal(machineList.Machine_04_Unit * mlRentalPrice4) +
                                    Convert.ToDecimal(machineList.Machine_05_Unit * mlRentalPrice5) +
                                    Convert.ToDecimal(machineList.Machine_06_Unit * mlRentalPrice6);

                                    if (machineList.Machine_07_Unit != null)
                                    {
                                        machineList.MachineRental = Convert.ToDecimal(machineList.Machine_01_Unit * mlRentalPrice1) +
                                        Convert.ToDecimal(machineList.Machine_02_Unit * mlRentalPrice2) +
                                        Convert.ToDecimal(machineList.Machine_03_Unit * mlRentalPrice3) +
                                        Convert.ToDecimal(machineList.Machine_04_Unit * mlRentalPrice4) +
                                        Convert.ToDecimal(machineList.Machine_05_Unit * mlRentalPrice5) +
                                        Convert.ToDecimal(machineList.Machine_06_Unit * mlRentalPrice6) +
                                        Convert.ToDecimal(machineList.Machine_07_Unit * mlRentalPrice7);
                                    }
                                }
                            }
                        }
                    }
                }

                ViewBag.mlRentalSum = machineList.MachineRental;

                db.MachineLists.Add(machineList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MA_ID = new SelectList(db.Machine_Asphalts, "ID", "MachineName", machineList.MA_ID);
            ViewBag.MB_ID = new SelectList(db.Machine_Bulldozers, "ID", "MachineName", machineList.MB_ID);
            ViewBag.MC_ID = new SelectList(db.Machine_Cranes, "ID", "MachineName", machineList.MC_ID);
            ViewBag.ME_ID = new SelectList(db.Machine_Excavators, "ID", "MachineName", machineList.ME_ID);
            ViewBag.MF_ID = new SelectList(db.Machine_Folklifts, "ID", "MachineName", machineList.MF_ID);
            ViewBag.MR_ID = new SelectList(db.Machine_Rollers, "ID", "MachineName", machineList.MR_ID);
            ViewBag.MW_ID = new SelectList(db.Machine_WheelLoaders, "ID", "MachineName", machineList.MW_ID);
            ViewBag.ProjectID = new SelectList(db.Projects, "ID", "ProjectName", machineList.ProjectID);
            return View(machineList);
        }

        // GET: MachineLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MachineList machineList = db.MachineLists.Find(id);
            if (machineList == null)
            {
                return HttpNotFound();
            }
            ViewBag.MA_ID = new SelectList(db.Machine_Asphalts, "ID", "MachineName", machineList.MA_ID);
            ViewBag.MB_ID = new SelectList(db.Machine_Bulldozers, "ID", "MachineName", machineList.MB_ID);
            ViewBag.MC_ID = new SelectList(db.Machine_Cranes, "ID", "MachineName", machineList.MC_ID);
            ViewBag.ME_ID = new SelectList(db.Machine_Excavators, "ID", "MachineName", machineList.ME_ID);
            ViewBag.MF_ID = new SelectList(db.Machine_Folklifts, "ID", "MachineName", machineList.MF_ID);
            ViewBag.MR_ID = new SelectList(db.Machine_Rollers, "ID", "MachineName", machineList.MR_ID);
            ViewBag.MW_ID = new SelectList(db.Machine_WheelLoaders, "ID", "MachineName", machineList.MW_ID);
            ViewBag.ProjectID = new SelectList(db.Projects, "ID", "ProjectName", machineList.ProjectID);
            return View(machineList);
        }

        // POST: MachineLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProjectID,MA_ID,Machine_01_Unit,MB_ID,Machine_02_Unit,MC_ID,Machine_03_Unit,ME_ID,Machine_04_Unit,MF_ID,Machine_05_Unit,MR_ID,Machine_06_Unit,MW_ID,Machine_07_Unit,MachineRental")] MachineList machineList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(machineList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MA_ID = new SelectList(db.Machine_Asphalts, "ID", "MachineName", machineList.MA_ID);
            ViewBag.MB_ID = new SelectList(db.Machine_Bulldozers, "ID", "MachineName", machineList.MB_ID);
            ViewBag.MC_ID = new SelectList(db.Machine_Cranes, "ID", "MachineName", machineList.MC_ID);
            ViewBag.ME_ID = new SelectList(db.Machine_Excavators, "ID", "MachineName", machineList.ME_ID);
            ViewBag.MF_ID = new SelectList(db.Machine_Folklifts, "ID", "MachineName", machineList.MF_ID);
            ViewBag.MR_ID = new SelectList(db.Machine_Rollers, "ID", "MachineName", machineList.MR_ID);
            ViewBag.MW_ID = new SelectList(db.Machine_WheelLoaders, "ID", "MachineName", machineList.MW_ID);
            ViewBag.ProjectID = new SelectList(db.Projects, "ID", "ProjectName", machineList.ProjectID);
            return View(machineList);
        }

        // GET: MachineLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MachineList machineList = db.MachineLists.Find(id);
            if (machineList == null)
            {
                return HttpNotFound();
            }
            return View(machineList);
        }

        // POST: MachineLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MachineList machineList = db.MachineLists.Find(id);
            db.MachineLists.Remove(machineList);
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
