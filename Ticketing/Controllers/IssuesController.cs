using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ticketing.Models;
using Microsoft.AspNet.Identity;
using System.Web.Security;
using System.Security.Claims;
using System.Data.Entity.Infrastructure;

namespace Ticketing.Controllers
{
    public class IssuesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Issues
        
        public ActionResult Index()
        {
            var issues = db.Issues.Include(i => i.CreationGebruiker).Include(i => i.Gebruiker).Include(i => i.PrioriteitInfo).Include(i => i.SolverGebruiker).Include(i => i.StatusInfo);
            return View(issues.ToList());
        }

        // GET: Issues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Issue issue = db.Issues.Find(id);
            if (issue == null)
            {
                return HttpNotFound();
            }
            return View(issue);
        }

        // GET: Issues/Create
        
        public ActionResult Create()
        {
            ViewBag.CreationGebruikerId = new SelectList(db.Users, "Id", "Voornaam");
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Voornaam");
            ViewBag.PrioriteitInfoId = new SelectList(db.PriorirteitInfo, "Id", "Omschrijving");
            ViewBag.SolverGebruikerId = new SelectList(db.Users, "Id", "Voornaam");
            ViewBag.StatusInfoId = new SelectList(db.StatusInfo, "Id", "Omschrijving");
            return View();
        }

        // POST: Issues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Onderwerp,StatusInfoId,Omschrijving,PrioriteitInfoId,DatumAanpassing,DatumAangemaakt,Teruggestuurd,RedenTeruggestuurd,ApplicationUserId,CreationGebruikerId,SolverGebruikerId")] Issue issue)
        public ActionResult Create([Bind(Include = "Id,Onderwerp,StatusInfoId,Omschrijving,PrioriteitInfoId,Teruggestuurd,RedenTeruggestuurd,ApplicationUserId,CreationGebruikerId,SolverGebruikerId")] Issue issue)
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            int statusId = issue.StatusInfoId;
            StatusInfo currentStatus = db.StatusInfo.FirstOrDefault(x => x.Id == statusId);
            int prioriteitInfoId = issue.PrioriteitInfoId;
            PrioriteitInfo currentPrioriteit = db.PriorirteitInfo.Find(prioriteitInfoId);
            if (ModelState.IsValid)
            {
                issue.DatumAangemaakt = DateTime.Now;
                issue.DatumAanpassing = DateTime.Now;
                issue.CreationGebruiker = currentUser;

                db.StatusTraces.Add(new StatusTrace {
                    LogDateTime = DateTime.Now,
                    Issue = issue,
                    IssueId = issue.Id.ToString(),
                    Status = currentStatus.Omschrijving
                });

                db.IssueTraces.Add(new IssueTrace
                {
                    LogDateTime = DateTime.Now,
                    ApplicationUser = issue.ApplicationUserId,
                    CreationGebruiker = issue.CreationGebruikerId,
                    DatumAangemaakt = issue.DatumAangemaakt,
                    DatumAanpassing = issue.DatumAanpassing,
                    Omschrijving = issue.Omschrijving,
                    Onderwerp = issue.Onderwerp,
                    Prioriteit = currentPrioriteit.Omschrijving,
                    Status = currentStatus.Omschrijving,
                    Teruggestuurd = issue.Teruggestuurd,
                    RedenTeruggestuurd = issue.RedenTeruggestuurd,
                    SolverGebruiker = issue.SolverGebruikerId,
                    Issue = issue
                });

                
                db.Issues.Add(issue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CreationGebruikerId = new SelectList(db.Users, "Id", "Voornaam", issue.CreationGebruikerId);
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Voornaam", issue.ApplicationUserId);
            ViewBag.PrioriteitInfoId = new SelectList(db.PriorirteitInfo, "Id", "Omschrijving", issue.PrioriteitInfoId);
            ViewBag.SolverGebruikerId = new SelectList(db.Users, "Id", "Voornaam", issue.SolverGebruikerId);
            ViewBag.StatusInfoId = new SelectList(db.StatusInfo, "Id", "Omschrijving", issue.StatusInfoId);
            return View(issue);
        }

        // GET: Issues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Issue issue = db.Issues.Find(id);
            if (issue == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreationGebruikerId = new SelectList(db.Users, "Id", "Voornaam", issue.CreationGebruikerId);
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Voornaam", issue.ApplicationUserId);
            ViewBag.PrioriteitInfoId = new SelectList(db.PriorirteitInfo, "Id", "Omschrijving", issue.PrioriteitInfoId);
            ViewBag.SolverGebruikerId = new SelectList(db.Users, "Id", "Voornaam", issue.SolverGebruikerId);
            ViewBag.StatusInfoId = new SelectList(db.StatusInfo, "Id", "Omschrijving", issue.StatusInfoId);
            return View(issue);
        }

        // POST: Issues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Onderwerp,StatusInfoId,Omschrijving,PrioriteitInfoId,DatumAanpassing,DatumAangemaakt,Teruggestuurd,RedenTeruggestuurd,ApplicationUserId,CreationGebruikerId,SolverGebruikerId")] Issue issue)
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            //
            var startingIssue = db.Issues.AsNoTracking().Where(m => m.Id == issue.Id).FirstOrDefault();

            if (ModelState.IsValid)
            {
                db.Entry(issue).State = EntityState.Modified;
                issue.DatumAanpassing = DateTime.Now;
                //issue.SolverGebruiker = currentUser;
                
                StatusInfo currentStatus = db.StatusInfo.FirstOrDefault(x => x.Id == issue.StatusInfoId);
                PrioriteitInfo currentPrioriteit = db.PriorirteitInfo.FirstOrDefault(x => x.Id == issue.PrioriteitInfoId);

                if(startingIssue.PrioriteitInfoId != issue.PrioriteitInfoId || startingIssue.StatusInfoId != issue.StatusInfoId ||
                    startingIssue.Omschrijving != issue.Omschrijving || startingIssue.RedenTeruggestuurd != issue.RedenTeruggestuurd)
                {
                    db.IssueTraces.Add(new IssueTrace
                    {
                        LogDateTime = DateTime.Now,
                        ApplicationUser = issue.ApplicationUserId,
                        CreationGebruiker = issue.CreationGebruikerId,
                        DatumAangemaakt = issue.DatumAangemaakt,
                        DatumAanpassing = issue.DatumAanpassing,
                        Omschrijving = issue.Omschrijving,
                        Onderwerp = issue.Onderwerp,
                        Prioriteit = currentPrioriteit.Omschrijving,
                        Status = currentStatus.Omschrijving,
                        Teruggestuurd = issue.Teruggestuurd,
                        RedenTeruggestuurd = issue.RedenTeruggestuurd,
                        SolverGebruiker = issue.SolverGebruikerId,
                        Issue = issue
                    });
                }

                if (startingIssue.StatusInfoId != issue.StatusInfoId)
                {
                    db.StatusTraces.Add(new StatusTrace
                    {
                        LogDateTime = DateTime.Now,
                        Issue = issue,
                        IssueId = issue.Id.ToString(),
                        Status = currentStatus.Omschrijving
                    });
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreationGebruikerId = new SelectList(db.Users, "Id", "Voornaam", issue.CreationGebruikerId);
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Voornaam", issue.ApplicationUserId);
            ViewBag.PrioriteitInfoId = new SelectList(db.PriorirteitInfo, "Id", "Omschrijving", issue.PrioriteitInfoId);
            ViewBag.SolverGebruikerId = new SelectList(db.Users, "Id", "Voornaam", issue.SolverGebruikerId);
            ViewBag.StatusInfoId = new SelectList(db.StatusInfo, "Id", "Omschrijving", issue.StatusInfoId);
            return View(issue);
        }

        // GET: Issues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Issue issue = db.Issues.Find(id);
            if (issue == null)
            {
                return HttpNotFound();
            }
            return View(issue);
        }

        // POST: Issues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Issue issue = db.Issues.Find(id);
            db.Issues.Remove(issue);
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
