﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Projeto_KB.DAL;
using Projeto_KB.Models;

namespace Projeto_KB.Controllers
{
    public class ConceptsController : Controller
    {
        private KbaseContext db = new KbaseContext();

        // GET: Concepts
        public async Task<ActionResult> Index()
        {
            var concepts = db.Concepts.Include(c => c.Journey).Include(c => c.Subject).Include(c => c.Topic);
            return View(await concepts.ToListAsync());
        }

        // GET: Concepts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Concept concept = await db.Concepts.FindAsync(id);
            if (concept == null)
            {
                return HttpNotFound();
            }
            return View(concept);
        }

        // GET: Concepts/Create
        public ActionResult Create()
        {
            ViewBag.JourneyID = new SelectList(db.Journeys, "ID", "Name");
            ViewBag.SubjectID = new SelectList(db.Subjects, "ID", "Name");
            ViewBag.TopicID = new SelectList(db.Topics, "ID", "Name");
            return View();
        }

        // POST: Concepts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Title,Text,ContentDate,KeyWords,JourneyID,TopicID,SubjectID")] Concept concept)
        {
            if (ModelState.IsValid)
            {
                db.Concepts.Add(concept);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.JourneyID = new SelectList(db.Journeys, "ID", "Name", concept.JourneyID);
            ViewBag.SubjectID = new SelectList(db.Subjects, "ID", "Name", concept.SubjectID);
            ViewBag.TopicID = new SelectList(db.Topics, "ID", "Name", concept.TopicID);
            return View(concept);
        }

        // GET: Concepts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Concept concept = await db.Concepts.FindAsync(id);
            if (concept == null)
            {
                return HttpNotFound();
            }
            ViewBag.JourneyID = new SelectList(db.Journeys, "ID", "Name", concept.JourneyID);
            ViewBag.SubjectID = new SelectList(db.Subjects, "ID", "Name", concept.SubjectID);
            ViewBag.TopicID = new SelectList(db.Topics, "ID", "Name", concept.TopicID);
            return View(concept);
        }

        // POST: Concepts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Title,Text,ContentDate,KeyWords,JourneyID,TopicID,SubjectID")] Concept concept)
        {
            if (ModelState.IsValid)
            {
                db.Entry(concept).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.JourneyID = new SelectList(db.Journeys, "ID", "Name", concept.JourneyID);
            ViewBag.SubjectID = new SelectList(db.Subjects, "ID", "Name", concept.SubjectID);
            ViewBag.TopicID = new SelectList(db.Topics, "ID", "Name", concept.TopicID);
            return View(concept);
        }

        // GET: Concepts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Concept concept = await db.Concepts.FindAsync(id);
            if (concept == null)
            {
                return HttpNotFound();
            }
            return View(concept);
        }

        // POST: Concepts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Concept concept = await db.Concepts.FindAsync(id);
            db.Concepts.Remove(concept);
            await db.SaveChangesAsync();
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
