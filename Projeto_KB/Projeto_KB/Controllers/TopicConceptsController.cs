using System;
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
    public class TopicConceptsController : Controller
    {
        private KbaseContext db = new KbaseContext();

        // GET: TopicConcepts
        public async Task<ActionResult> Index()
        {
            return View(await db.TopicConcepts.ToListAsync());
        }

        // GET: TopicConcepts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TopicConcept topicConcept = await db.TopicConcepts.FindAsync(id);
            if (topicConcept == null)
            {
                return HttpNotFound();
            }
            return View(topicConcept);
        }

        // GET: TopicConcepts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TopicConcepts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name")] TopicConcept topicConcept)
        {
            if (ModelState.IsValid)
            {
                db.TopicConcepts.Add(topicConcept);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(topicConcept);
        }

        // GET: TopicConcepts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TopicConcept topicConcept = await db.TopicConcepts.FindAsync(id);
            if (topicConcept == null)
            {
                return HttpNotFound();
            }
            return View(topicConcept);
        }

        // POST: TopicConcepts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name")] TopicConcept topicConcept)
        {
            if (ModelState.IsValid)
            {
                db.Entry(topicConcept).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(topicConcept);
        }

        // GET: TopicConcepts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TopicConcept topicConcept = await db.TopicConcepts.FindAsync(id);
            if (topicConcept == null)
            {
                return HttpNotFound();
            }
            return View(topicConcept);
        }

        // POST: TopicConcepts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TopicConcept topicConcept = await db.TopicConcepts.FindAsync(id);
            db.TopicConcepts.Remove(topicConcept);
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
