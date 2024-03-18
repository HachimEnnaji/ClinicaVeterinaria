using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClinicaVeterinaria.Models;

namespace ClinicaVeterinaria.Controllers
{
    public class VisitaController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Visita
        public ActionResult Index()
        {
            
                var visita = db.Visita.Include(v => v.Animale);
                return View(visita.ToList());
            
            
            
        }

        // GET: Visita/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visita visita = db.Visita.Find(id);
            if (visita == null)
            {
                return HttpNotFound();
            }
            return View(visita);
        }

        // GET: Visita/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                ViewBag.IdAnimale_Fk = new SelectList(db.Animale, "IdAnimale", "Nome");
                return View();
            }
            else if(db.Animale.Any(a => a.IdAnimale == id))
            {
                ViewBag.IdAnimale_Fk = new SelectList(db.Animale.Where(a => a.IdAnimale == id), "IdAnimale", "Nome");
                return View();
            } else {
                TempData["error"] = "L'animale selezionato non esiste";
                return RedirectToAction("Index", "Animale");
            }
        }
        

        // POST: Visita/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdVisita,DataVisita,Esame,DescrizioneCura,IdAnimale_Fk")] Visita visita)
        {
            if (ModelState.IsValid)
            {
                db.Visita.Add(visita);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdAnimale_Fk = new SelectList(db.Animale, "IdAnimale", "Nome", visita.IdAnimale_Fk);
            return View(visita);
        }

        public ActionResult VisitePerAnimale(int id)
        {
            TempData["idAnimale"] = id;
            if (db.Animale.Any(a => a.IdAnimale == id))
            {
                var visita = db.Visita.Include(v => v.Animale).Where(v => v.IdAnimale_Fk == id).OrderByDescending(v => v.DataVisita);
                return View(visita.ToList());
            }
            else
            {
                TempData["error"] = "L'animale selezionato non esiste";
                return RedirectToAction("Index", "Animale");
            }
        }

            // GET: Visita/Edit/5
            public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visita visita = db.Visita.Find(id);
            if (visita == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdAnimale_Fk = new SelectList(db.Animale, "IdAnimale", "Nome", visita.IdAnimale_Fk);
            return View(visita);
        }

        // POST: Visita/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdVisita,DataVisita,Esame,DescrizioneCura,IdAnimale_Fk")] Visita visita)
        {
            if (ModelState.IsValid)
            {
                db.Entry(visita).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdAnimale_Fk = new SelectList(db.Animale, "IdAnimale", "Nome", visita.IdAnimale_Fk);
            return View(visita);
        }

        // GET: Visita/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visita visita = db.Visita.Find(id);
            if (visita == null)
            {
                return HttpNotFound();
            }
            return View(visita);
        }

        // POST: Visita/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Visita visita = db.Visita.Find(id);
            db.Visita.Remove(visita);
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
