using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Configuration;

namespace CCSystem_New.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            if (Session["idUser"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        ReportsDataset ds = new ReportsDataset();
        public ActionResult ReportsProject()
        {
            if (Session["idUser"] != null)
            {
                ReportViewer reportViewer = new ReportViewer();
                reportViewer.ProcessingMode = ProcessingMode.Local;
                reportViewer.AsyncRendering = false;
                reportViewer.SizeToReportContent = true;
                var connectionString = ConfigurationManager.ConnectionStrings["CCS_DB"].ConnectionString;
                SqlConnection conx = new SqlConnection(connectionString);
                SqlDataAdapter adp = new SqlDataAdapter("SELECT * FROM Project", conx);
                adp.Fill(ds, ds.Project.TableName);
                reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"RPTDataset\Report1.rdlc";
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("MyDataSet", ds.Tables[0]));
                ViewBag.ReportViewer = reportViewer;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}