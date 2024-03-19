﻿using ClinicaVeterinaria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClinicaVeterinaria.Controllers
{
    public class CarrelloController : Controller
    {
        private ModelDbContext db = new ModelDbContext();
        // GET: Carrello
        public ActionResult Index()
        {
            var ListaCarrello = Session["CarrelloSession"] as List<Prodotti>;
            if (ListaCarrello == null || !ListaCarrello.Any())
            {
                return RedirectToAction("Index", "Prodotti");
            }
            return View(ListaCarrello);
        }
    }
}