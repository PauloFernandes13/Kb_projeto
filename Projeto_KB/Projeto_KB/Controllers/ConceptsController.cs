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
using Newtonsoft.Json;

namespace Projeto_KB.Controllers
{   
    [Authorize]
    public class ConceptsController : Controller
    {
        private KbaseContext db = new KbaseContext();


        //Search Concepts
        public ActionResult SearchConcept(string SearchConcepts)
        {
           
            if (User.IsInRole("Client_1"))
            {
                 ViewBag.dataSearch = SearchConcepts;
                var search = db.Concepts.Include(f => f.Journey).Where(g => g.Journey.ID == 1).Include(f => f.Phase).Include(f => f.TopicConcept)
                        .Where(r => r.Order.Contains(SearchConcepts) || r.Text.Contains(SearchConcepts) || r.Phase.Name.Contains(SearchConcepts)
                        || r.TopicConcept.Name.Contains(SearchConcepts));

                return View(search);
            }
            else if (User.IsInRole("Client_2"))
            {
                ViewBag.dataSearch = SearchConcepts;
                var search = db.Concepts.Include(f => f.Journey).Where(g => g.Journey.ID == 1 || g.JourneyID == 2).Include(f => f.Phase).Include(f => f.TopicConcept)
                        .Where(r => r.Order.Contains(SearchConcepts) || r.Text.Contains(SearchConcepts) || r.Phase.Name.Contains(SearchConcepts)
                        || r.TopicConcept.Name.Contains(SearchConcepts));

                return View(search);
            }
            else
            {
                ViewBag.dataSearch = SearchConcepts;
                var search = db.Concepts.Include(f => f.Journey).Include(f => f.Phase).Include(f => f.TopicConcept)
                        .Where(r => r.Order.Contains(SearchConcepts) || r.Text.Contains(SearchConcepts) || r.Phase.Name.Contains(SearchConcepts)
                        || r.TopicConcept.Name.Contains(SearchConcepts));

                return View(search);
            }
            ////ViewBag.dataSearch = SearchConcepts;
            //var search = db.Concepts.Include(f => f.Subject).Include(f => f.Topic).Include(f=>f.Journey)
            //        .Where(r => r.KeyWords.Contains(SearchConcepts) || r.Text.Contains(SearchConcepts) || r.Subject.Name.Contains(SearchConcepts)
            //        || r.Topic.Name.Contains(SearchConcepts));

            //return View(search);

        }

        //Get Concepts to partial View
        [ChildActionOnly]
        public ActionResult ConceptsPartialView()
        {

           

            if (User.IsInRole("Client_1"))
            {
                var conceptsPartial = db.Concepts.Include("Journey").Where(g => g.Journey.ID == 1).Include(c => c.Phase).Include(c => c.TopicConcept).OrderBy(c => c.TopicConcept.Order);
                return PartialView(conceptsPartial);
            }
            else if (User.IsInRole("Client_2"))
            {
                var conceptsPartial = db.Concepts.Include("Journey").Where(g => g.Journey.ID == 1 || g.Journey.ID == 2).Include(c => c.Phase).Include(c => c.TopicConcept).OrderBy(c=>c.TopicConcept.Order);
                return PartialView(conceptsPartial);
            }
            else
            {
                var conceptsPartial = db.Concepts.Include(c => c.Journey).Include(c => c.Phase).Include(c => c.TopicConcept).OrderBy(c => c.TopicConcept.Order);
                return PartialView(conceptsPartial);
            }

        }

        // GET: Concepts
        [Authorize(Roles = "Admin, Manager")]
        public async Task<ActionResult> Index()
        {
            var concepts = db.Concepts.Include(c => c.Journey).Include(c => c.Phase).Include(c => c.TopicConcept);
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
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Create()
        {
            
            var uniquePhase = (from dbPhases in db.Concepts //etapas para validar inserção de conteudos
            select dbPhases.Phase.ID).Distinct().OrderBy(name => name); //nome da variavel é irrelevante em Order, já foi feito o Select, só irá ordenar os nomes
            ViewBag.comparePhase = JsonConvert.SerializeObject(uniquePhase); 

            ViewBag.JourneyID = new SelectList(db.Journeys, "ID", "Name");
            ViewBag.PhaseID = new SelectList(db.Phases, "Name", "Name");
            ViewBag.TopicConceptID = new SelectList(db.TopicConcepts, "ID", "Name");
            return View();
        }
        
        // POST: Concepts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Create([Bind(Include = "ID,Title,Text,ContentDate,KeyWords,JourneyID,TopicConceptID,PhaseID")] Concept concept)
        {
            if (ModelState.IsValid)
            {
                db.Concepts.Add(concept);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.JourneyID = new SelectList(db.Journeys, "ID", "Name", concept.JourneyID);
            ViewBag.PhaseID = new SelectList(db.Phases, "ID", "Name", concept.PhaseID);
            ViewBag.TopicConceptID = new SelectList(db.TopicConcepts, "ID", "Name", concept.TopicConceptID);
            return View(concept);
        }

        // GET: Concepts/Edit/5
        [Authorize(Roles = "Admin, Manager")]
        [ValidateInput(false)]
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
            ViewBag.PhaseID = new SelectList(db.Phases, "ID", "Name", concept.PhaseID);
            ViewBag.TopicConceptID = new SelectList(db.TopicConcepts, "ID", "Name", concept.TopicConceptID);
            return View(concept);
        }

        // POST: Concepts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Title,Text,ContentDate,KeyWords,JourneyID,TopicConceptID,PhaseID")] Concept concept)
        {
            if (ModelState.IsValid)
            {
                db.Entry(concept).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.JourneyID = new SelectList(db.Journeys, "ID", "Name", concept.JourneyID);
            ViewBag.PhaseID = new SelectList(db.Phases, "ID", "Name", concept.PhaseID);
            ViewBag.TopicConceptID = new SelectList(db.TopicConcepts, "ID", "Name", concept.TopicConceptID);
            return View(concept);
        }

        // GET: Concepts/Delete/5
        [Authorize(Roles = "Admin, Manager")]
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
        [Authorize(Roles = "Admin, Manager")]
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
