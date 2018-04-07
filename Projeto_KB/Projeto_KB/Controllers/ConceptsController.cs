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

        //                       ---------------------------GetData-----------------------------------------

        //Get: Phases unicas para validação de inserção de dados.
        public  IOrderedQueryable GetUniquePhase()
        {
            var uniquePhase = (from dbPhases in db.Concepts //etapas para validar inserção de conteudos
                                       select dbPhases.Phase.ID).Distinct().OrderBy(name => name); //nome da variavel é irrelevante em Order, já foi feito o Select, só irá ordenar os nomes
            return uniquePhase;
        }
        
        //Get: Topic unicas para validação de inserção de dados.
        public IOrderedQueryable GetUniqueTopic()
        {
            var uniqueTopic = (from dbTopics in db.Concepts
                               select dbTopics.TopicConceptID).Distinct().OrderBy(name => name); //topicos para validar inserção de conteúdos
            return uniqueTopic;
        }

        //Get: Lista de Journeys para ligar(Bind) em DropDown, Jorney, Phases e TopicConcept ....
        public List<Journey> GetJourneyList()
        {
            List<Journey> journeys = db.Journeys.ToList();
            return journeys;
        }

        //Get: Lista de Phases para ligar(Bind) em DropDown, Jorney, Phases e TopicConcept ....
        public ActionResult GetPhaseList(int journeyId)
        {
            List<Phase> phaseList = db.Phases.Where(x => x.JourneyID == journeyId).ToList();
            ViewBag.PhaseOption = new SelectList(phaseList, "ID", "Name");
            return PartialView("PhaseOptionsPartial");
        }
        public ActionResult GetTopicList(int phaseId)
        {
            List<TopicConcept> topicList = db.TopicConcepts.Where(x => x.PhaseID == phaseId ).ToList();
            ViewBag.TopicOption = new SelectList(topicList, "ID", "Name");
            return PartialView("TopicConceptsOptionPartial");
        }
//                                            ------------------GetDataEnd------------------------

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
                var conceptsPartial = db.Concepts.Include("Journey").Where(g => g.Journey.ID == 1 || g.Journey.ID == 2).Include(c => c.Phase).Include(c => c.TopicConcept).OrderBy(c => c.TopicConcept.Order);
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

            var uniqueTopic = GetUniqueTopic();
            ViewBag.compareTopic = JsonConvert.SerializeObject(uniqueTopic);//valores podem vir nulos.

            var uniquePhase = GetUniquePhase();
            ViewBag.comparePhase = JsonConvert.SerializeObject(uniquePhase);//valores podem vir nulos.

            ViewBag.JourneyList = new SelectList(GetJourneyList(), "ID", "Name"); //Metodo GetJourneyList() para obter a Jorney...          
            ViewBag.PhaseID = new SelectList(db.Phases, "ID", "Name");
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

            var uniqueTopic = GetUniqueTopic();
            ViewBag.compareTopic = JsonConvert.SerializeObject(uniqueTopic);//valores podem vir nulos.

            var uniquePhase = GetUniquePhase();
            ViewBag.comparePhase = JsonConvert.SerializeObject(uniquePhase);//valores podem vir nulos.
            
            ViewBag.JourneyList = new SelectList(GetJourneyList(), "ID", "Name");
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
            var uniqueTopic = new SelectList(GetUniqueTopic());
            ViewBag.compareTopic = JsonConvert.SerializeObject(uniqueTopic);//valores podem vir nulos.

            var uniquePhase = new SelectList(GetUniquePhase());
            ViewBag.comparePhase = JsonConvert.SerializeObject(uniquePhase);//valores podem vir nulos.

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

            var uniqueTopic = new SelectList(GetUniqueTopic());
            ViewBag.compareTopic = JsonConvert.SerializeObject(uniqueTopic);//valores podem vir nulos.

            var uniquePhase = new SelectList(GetUniquePhase());
            ViewBag.comparePhase = JsonConvert.SerializeObject(uniquePhase);//valores podem vir nulos.

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
