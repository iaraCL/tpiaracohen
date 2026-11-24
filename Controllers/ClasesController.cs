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

                ViewBag.Horarios = bd.ObtenerHorariosClase(id);
                return View(clase);
            }

            [HttpPost]
            public IActionResult Reservar(int idHorarioClase)
            {
                int? idUsuario = HttpContext.Session.GetInt32("IdUsuario");

                if (idUsuario == null)
                {
                    return Unauthorized();
                }

                if (idHorarioClase <= 0)
                {
                    return BadRequest();
                }

                BD bd = new BD();

                int resultado = bd.ReservarClase(idUsuario.Value, idHorarioClase);

                return Json(new { resultado = resultado });
            }


            [HttpGet]
            public IActionResult VerLugares(int idHorarioClase)
            {
                int? idUsuario = HttpContext.Session.GetInt32("IdUsuario");

                if (idUsuario == null)
                {
                    return Unauthorized();
                }

                if (idHorarioClase <= 0)
                {
                    return BadRequest();
                }

                BD bd = new BD();

                int lugares = bd.ObtenerLugaresDisponibles(idHorarioClase);

                return Json(lugares);
            }




    }
}
