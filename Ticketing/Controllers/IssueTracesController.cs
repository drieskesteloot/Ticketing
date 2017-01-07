using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ticketing.Models;
using ClosedXML.Excel;
using System.Configuration;
using System.Data.SqlClient;
using WebMatrix.Data;


namespace Ticketing.Controllers
{
    public class IssueTracesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: IssueTraces
        public ActionResult Index(string sortOrder, string searchString, string searchStatus)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            ViewBag.StatusInfo = (from m in db.StatusInfo select m.Omschrijving);

            var issueTraces = from m in db.IssueTraces select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                issueTraces = issueTraces.Where(m => m.Status.Contains(searchString)
                                       || m.Onderwerp.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(searchStatus))
            {
                issueTraces = issueTraces.Where(m => m.Status.Contains(searchStatus));
            }

            switch (sortOrder)
            {
                case "name_desc": issueTraces.OrderByDescending(m => m.Status);
                    break;
                case "Date": issueTraces.OrderBy(m => m.LogDateTime);
                    break;
                case "date_desc": issueTraces.OrderByDescending(m => m.LogDateTime);
                    break;
                default:
                    issueTraces = issueTraces.OrderByDescending(m => m.LogDateTime);
                    break; 
            }

            return View(issueTraces.ToList());
        }

        // GET: IssueTraces/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IssueTrace issueTrace = db.IssueTraces.Find(id);
            if (issueTrace == null)
            {
                return HttpNotFound();
            }
            return View(issueTrace);
        }

        // GET: IssueTraces/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IssueTraces/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LogDateTime,Onderwerp,Status,Omschrijving,Prioriteit,DatumAangemaakt,DatumAanpassing,Teruggestuurd,RedenTeruggestuurd,ApplicationUser,CreationGebruiker,SolverGebruiker")] IssueTrace issueTrace)
        {
            if (ModelState.IsValid)
            {
                db.IssueTraces.Add(issueTrace);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(issueTrace);
        }

        // GET: IssueTraces/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IssueTrace issueTrace = db.IssueTraces.Find(id);
            if (issueTrace == null)
            {
                return HttpNotFound();
            }
            return View(issueTrace);
        }

        // POST: IssueTraces/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LogDateTime,Onderwerp,Status,Omschrijving,Prioriteit,DatumAangemaakt,DatumAanpassing,Teruggestuurd,RedenTeruggestuurd,ApplicationUser,CreationGebruiker,SolverGebruiker")] IssueTrace issueTrace)
        {
            if (ModelState.IsValid)
            {
                db.Entry(issueTrace).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(issueTrace);
        }

        // GET: IssueTraces/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IssueTrace issueTrace = db.IssueTraces.Find(id);
            if (issueTrace == null)
            {
                return HttpNotFound();
            }
            return View(issueTrace);
        }

        // POST: IssueTraces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IssueTrace issueTrace = db.IssueTraces.Find(id);
            db.IssueTraces.Remove(issueTrace);
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

        public ActionResult ExportData()
        {
            
            string constr = ConfigurationManager.ConnectionStrings["Defaultconnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM IssueTraces"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                wb.Worksheets.Add(dt, "IssueTraces");
                                Response.Clear();
                                Response.Buffer = true;
                                Response.Charset = "";
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AddHeader("content-disposition", "attachment;filename=SqlExport.xlsx");
                                using (MemoryStream MyMemoryStream = new MemoryStream())
                                {
                                    wb.SaveAs(MyMemoryStream);
                                    MyMemoryStream.WriteTo(Response.OutputStream);
                                    Response.Flush();
                                    Response.End();
                                }
                            }
                        }
                    }
                }
            }
            

            /*
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            GridView gv = new GridView();
            gv.DataSource = db.IssueTraces.ToList();
            gv.DataBind();
            gv.RenderControl(hw);
            Response.ContentType = "application/vnd.ms-excel"; // 2003 office excel
            Response.AppendHeader("Content-Disposition", "filename=IssueTraces.xlsx");
            string style = @"<style> TABLE { border: 1px solid black; border-collapse: collapse; } TD {mso-number-format:\@; }</style> ";
            Response.Write(style);
            Response.Write(tw.ToString());
            Response.End();
            */
            return RedirectToAction("Index");
        }

        public ActionResult MakeChart()
        {
            return PartialView("_MakeChart");
        }

    }
}
