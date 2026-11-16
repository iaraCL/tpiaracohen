
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
                    ViewBag.Mensaje = "Usuario registrado correctamente";
                    return View();
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
    }
}
