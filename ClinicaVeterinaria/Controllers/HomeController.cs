using ClinicaVeterinaria.Models;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ClinicaVeterinaria.Controllers
{
    public class HomeController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CercaIlTuoAnimale()
        {

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> CercaAnimale(string Microchip)
        {
            //prendimi l'animale con il microchip uguale a quello che mi hai passato e includi le proprieta di navigazione di tipo Ricovero e Visita
            var search = await db.Animale.Where(a => a.Microchip == Microchip && a.Propietario == "rifugio").FirstOrDefaultAsync();
            if (search == null)
            {
                return new HttpStatusCodeResult(404);
            }
            return Json(search, JsonRequestBehavior.AllowGet);

        }


    }
}