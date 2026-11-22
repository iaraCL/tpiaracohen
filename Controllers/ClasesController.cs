using Microsoft.AspNetCore.Mvc;
using TPFINAL.Models;

namespace TPFINAL.Controllers
{
    public class ClasesController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("IdUsuario") == null)
        {
             return RedirectToAction("Login", "Usuario");
        }
            BD bd = new BD();

            List<Clase> clases = bd.ObtenerClases();

            return View(clases);
        }

            [HttpGet]
            public IActionResult VerMas(int cantidad)
            {
                if (HttpContext.Session.GetInt32("IdUsuario") == null)
                {
                    return Unauthorized();
                }

                if (cantidad < 0)
                {
                    return BadRequest();
                }

                BD bd = new BD();

                List<Clase> clases = bd.ObtenerMasClases(cantidad);

                return Json(clases);
            }

            public IActionResult Detalle(int id)
            {
                if (HttpContext.Session.GetInt32("IdUsuario") == null)
                {
                    return RedirectToAction("Login", "Usuario");
                }

                BD bd = new BD();

                Clase clase = bd.ObtenerDetalleClase(id);

                if (clase == null)
                {
                    return NotFound();
                }

                return View(clase);
            }


    }
}
