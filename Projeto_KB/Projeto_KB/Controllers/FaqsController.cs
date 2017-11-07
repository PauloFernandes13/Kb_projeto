using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Projeto_KB.DAL;
using Projeto_KB.Models;

namespace Projeto_KB.Controllers
{
    public class FaqsController : Controller
    {
        private KbaseContext db = new KbaseContext();

        // GET: Faqs
        public ActionResult Index(string searchTerm = null)
        {
            var faqs = db.Faqs.Include(f => f.Subject).Include(f => f.Topic)
             .Where(r => searchTerm == null || r.Subject.Name.StartsWith(searchTerm));
            return View(faqs.ToList());
        }

        //Get: Faqs
        public ActionResult SearchFaq(string SearchFaqs )
        {
            ViewBag.dataSearch = SearchFaqs;
            var searchGeral = db.Faqs.Include(f => f.Subject).Include(f => f.Topic)
                    .Where(r => r.Answer.Contains(SearchFaqs) || r.Question.Contains(SearchFaqs) || r.Description.Contains(SearchFaqs) || r.Subject.Name.Contains(SearchFaqs));

            return View(searchGeral);

        }
      

        //GET: Categories(Subject)
        
        public ActionResult Categories(string searchFaq = null)
        {
            var subjects = db.Subjects.ToList().OrderBy(i => i.Name);
            return View(subjects.ToList());
        }


        // Get: Retrieve categorie and associated description
        public ActionResult Description(int? id)
        {

            var descriptions = db.Faqs.Include("Topic").Include(g => g.Subject).Where(g => g.SubjectID == id).Take(3);
            if (id != null)
            {
                return View(descriptions);
            }
            return HttpNotFound();
        }
        // Get: Retrieve all questions associated to a Topic
        public ActionResult DescriptionAll(int? idTopic, int? idSubject)
        {
            var descriptionsFull = db.Faqs.Include("Subject").Where(g => g.SubjectID == idSubject).Include(g => g.Topic).Where(g => g.TopicID == idTopic).OrderBy(g => g.Question);

            if (idTopic != null)
            {

                return View(descriptionsFull);
            }
            return HttpNotFound();
        }



        // GET: Faqs/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faq faq = db.Faqs.Find(id);
            if (faq == null)
            {
                return HttpNotFound();
            }
            return View(faq);
        }


        // GET: Faqs/Create

        public ActionResult Create()
        {
            ViewBag.SubjectID = new SelectList(db.Subjects, "ID", "Name");
            ViewBag.TopicID = new SelectList(db.Topics, "ID", "Name");
            return View();
        }

        // POST: Faqs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "ID,Description,Question,Answer,UrlFaq,TopicID,SubjectID")] Faq faq)
        {
            if (ModelState.IsValid)
            {
                db.Faqs.Add(faq);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SubjectID = new SelectList(db.Subjects, "ID", "Name", faq.SubjectID);
            ViewBag.TopicID = new SelectList(db.Topics, "ID", "Name", faq.TopicID);
            return View(faq);
        }

        // GET: Faqs/Edit/5
        [ValidateInput(false)] //não permite as validações para que o texto com Markup de CKEditor retorne da B.D.
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faq faq = db.Faqs.Find(id);
            if (faq == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubjectID = new SelectList(db.Subjects, "ID", "Name", faq.SubjectID);
            ViewBag.TopicID = new SelectList(db.Topics, "ID", "Name", faq.TopicID);
            return View(faq);
        }

        // POST: Faqs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "ID,Description,Question,Answer,UrlFaq,TopicID,SubjectID")] Faq faq)
        {
            if (ModelState.IsValid)
            {
                db.Entry(faq).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SubjectID = new SelectList(db.Subjects, "ID", "Name", faq.SubjectID);
            ViewBag.TopicID = new SelectList(db.Topics, "ID", "Name", faq.TopicID);
            return View(faq);
        }

        // GET: Faqs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faq faq = db.Faqs.Find(id);
            if (faq == null)
            {
                return HttpNotFound();
            }
            return View(faq);
        }

        // POST: Faqs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Faq faq = db.Faqs.Find(id);
            db.Faqs.Remove(faq);
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
