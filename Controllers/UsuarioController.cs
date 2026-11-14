
using Microsoft.AspNetCore.Mvc;
using TPFINAL.Models;

namespace TPFINAL.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Registro()
        {
            return View();
        }
    }
}
