using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using TPFINAL.Models;

namespace TPFINAL.Controllers
{
    public class UsuarioController : Controller
    {
        
        public IActionResult Registro()
        {
            if (HttpContext.Session.GetInt32("IdUsuario") != null)
            {
                return RedirectToAction("Index", "Clases");
            }

            return View();
        }


        [HttpPost]
        public IActionResult Registro(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Nombre) ||
                string.IsNullOrWhiteSpace(usuario.Apellido) ||
                string.IsNullOrWhiteSpace(usuario.NombreUsuario) ||
                string.IsNullOrWhiteSpace(usuario.Contrasena))
            {
                ViewBag.Error = "Completa todos los campos";
                return View();
            }

            if (usuario.Nombre.Length > 50 ||
                usuario.Apellido.Length > 50 ||
                usuario.NombreUsuario.Length > 50)
            {
                ViewBag.Error = "Los datos son demasiado largos";
                return View();
            }

            if (usuario.Contrasena.Length < 8)
            {
                ViewBag.Error = "La contraseña debe tener al menos 8 caracteres";
                return View();
            }

            BD bd = new BD();

            if (bd.ExisteUsuario(usuario.NombreUsuario))
            {
                ViewBag.Error = "Ese nombre de usuario ya existe";
                return View();
            }

            PasswordHasher<Usuario> hasher =
                new PasswordHasher<Usuario>();

            usuario.Contrasena = hasher.HashPassword(
                usuario, usuario.Contrasena);

            try
            {
                bool registrado = bd.RegistrarUsuario(usuario);

                if (registrado)
                {
                    return RedirectToAction("Login", "Usuario");
                }
            }
            catch (SqlException ex) when (
                ex.Number == 2601 || ex.Number == 2627)
            {
                ViewBag.Error = "Ese nombre de usuario ya existe";
                return View();
            }

            ViewBag.Error = "No se pudo registrar el usuario";
            return View();
        }
              public IActionResult Login()
        {
             return View();
        }

      [HttpPost]
      public IActionResult Login(string NombreUsuario, string Contrasena)
        {
             if (string.IsNullOrWhiteSpace(NombreUsuario) ||
             string.IsNullOrWhiteSpace(Contrasena))
               {
                     ViewBag.Error = "Completa todos los campos";
                     return View();
               }

     BD bd = new BD(); 

    Usuario usuario = bd.BuscarUsuario(NombreUsuario);

    if (usuario == null)
    {
        ViewBag.Error = "Usuario o contraseña incorrectos";
        return View();
    }

    PasswordHasher<Usuario> hasher = new PasswordHasher<Usuario>();

    PasswordVerificationResult resultado = hasher.VerifyHashedPassword(
        usuario, usuario.Contrasena, Contrasena);

    if (resultado == PasswordVerificationResult.Failed)
    {
        ViewBag.Error = "Usuario o contraseña incorrectos";
        return View();
    }

    HttpContext.Session.SetInt32("IdUsuario", usuario.Id);


            return RedirectToAction("Index", "Clases");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Usuario");
        }
    }
}
