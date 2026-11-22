using Microsoft.AspNetCore.Mvc;
using TPFINAL.Models;

namespace TPFINAL.Controllers
{
    public class ReservasController : Controller
    {
        public IActionResult Index()
        {
            int? idUsuario = HttpContext.Session.GetInt32("IdUsuario");

            if (idUsuario == null)
            {
                return RedirectToAction("Login", "Usuario");
            }

            BD bd = new BD();

            List<Reserva> reservas = bd.ObtenerReservasUsuario(idUsuario.Value);

            return View(reservas);
        }

            [HttpPost]
            public IActionResult Cancelar(int idReserva)
            {
                int? idUsuario = HttpContext.Session.GetInt32("IdUsuario");

                if (idUsuario == null)
                {
                    return Unauthorized();
                }

                if (idReserva <= 0)
                {
                    return BadRequest();
                }

                BD bd = new BD();

                int resultado = bd.CancelarReserva(idReserva, idUsuario.Value);

                return Json(new { resultado = resultado });
            }

    }
}
