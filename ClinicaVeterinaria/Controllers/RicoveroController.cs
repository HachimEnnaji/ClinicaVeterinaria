using ClinicaVeterinaria.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ClinicaVeterinaria.Controllers
{
    public class RicoveroController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Ricovero
        public ActionResult Index()
        {
            var ricovero = db.Ricovero.Include(r => r.Animale);
            return View(ricovero.ToList());
        }

        // GET: Ricovero/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ricovero ricovero = db.Ricovero.Find(id);
            if (ricovero == null)
            {
                return HttpNotFound();
            }
            return View(ricovero);
        }

        // GET: Ricovero/Create
        public ActionResult Create(int? id)
        {
            if (id != null)
            {
                //TO DO: migliorare la select list per renderla readonly
                ViewBag.id_Animale_FK = new SelectList(db.Animale.Where(a => a.IdAnimale == id), "IdAnimale", "Nome");
            }
            return View();
        }

        // POST: Ricovero/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdRicovero,DataInizio,Costo,IsAttivo,id_Animale_FK")] Ricovero ricovero)
        {
            if (ModelState.IsValid)
            {
                db.Ricovero.Add(ricovero);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_Animale_FK = new SelectList(db.Animale, "IdAnimale", "Nome", ricovero.id_Animale_FK);
            return View(ricovero);
        }

        // GET: Ricovero/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ricovero ricovero = db.Ricovero.Find(id);
            if (ricovero == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_Animale_FK = new SelectList(db.Animale, "IdAnimale", "Nome", ricovero.id_Animale_FK);
            return View(ricovero);
        }

        // POST: Ricovero/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdRicovero,DataInizio,Costo,IsAttivo,id_Animale_FK")] Ricovero ricovero)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ricovero).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_Animale_FK = new SelectList(db.Animale, "IdAnimale", "Nome", ricovero.id_Animale_FK);
            return View(ricovero);
        }

        // GET: Ricovero/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ricovero ricovero = db.Ricovero.Find(id);
            if (ricovero == null)
            {
                return HttpNotFound();
            }
            return View(ricovero);
        }

        // POST: Ricovero/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ricovero ricovero = db.Ricovero.Find(id);
            db.Ricovero.Remove(ricovero);
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
        public async Task<ActionResult> RicoveroAttivo(int month)
        {
            var ricovero = db.Ricovero.Include(r => r.Animale).Where(r => r.DataInizio.Month == month && r.IsAttivo == true);
            return View(await ricovero.ToListAsync());
        }
    }
}
