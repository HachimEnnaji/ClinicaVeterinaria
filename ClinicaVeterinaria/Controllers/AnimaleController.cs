﻿using ClinicaVeterinaria.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ClinicaVeterinaria.Controllers
{
    [Authorize(Roles = "Veterinario")]
    public class AnimaleController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Animale
        public ActionResult Index()
        {
            return View(db.Animale.ToList());
        }

        // GET: Animale/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animale animale = db.Animale.Find(id);
            if (animale == null)
            {
                return HttpNotFound();
            }
            return View(animale);
        }

        // GET: Animale/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Animale/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAnimale,DataRegistrazione,Nome,Tipo,Colore,DataNascita,Microchip,Propietario,Foto")] Animale animale)
        {
            if (ModelState.IsValid)
            {
                if (!animale.Microchip.IsNullOrWhiteSpace())
                {
                    if (db.Animale.Any(a => a.Microchip == animale.Microchip))
                    {
                        TempData["error"] = $"Il microchip {animale.Microchip} è già stato registrato";
                        return View();
                    }
                }
                if (animale.Foto.IsNullOrWhiteSpace())
                {
                    animale.Foto = "AnimaleDefault.jpg";
                }
                if (animale.Propietario.IsNullOrWhiteSpace())
                {
                    animale.Propietario = "rifugio";
                }
                if (animale.DataNascita < DateTime.Now)
                {
                    TempData["success"] = $"{animale.Nome} aggiunto con successo";
                    db.Animale.Add(animale);
                    db.SaveChanges();
                    if (animale.Propietario == "rifugio")
                    {

                        return RedirectToAction("Create", "Ricovero", new { id = animale.IdAnimale });
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["error"] = $"{animale.DataNascita} La data di nascita non può essere posteriore a {DateTime.Now}";
                    return View();
                }
            }
            TempData["error"] = "Errore nell'inserimento";
            return View();

        }

        // GET: Animale/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animale animale = db.Animale.Find(id);
            if (animale == null)
            {
                return HttpNotFound();
            }
            return View(animale);
        }

        // POST: Animale/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAnimale,DataRegistrazione,Nome,Tipo,Colore,DataNascita,Microchip,Propietario,Foto")] Animale animale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(animale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(animale);
        }

        // GET: Animale/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animale animale = db.Animale.Find(id);
            if (animale == null)
            {
                return HttpNotFound();
            }
            return View(animale);
        }

        // POST: Animale/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Animale animale = db.Animale.Find(id);
            db.Animale.Remove(animale);
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
