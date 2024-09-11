using Microsoft.AspNetCore.Mvc;
using EvaluacionTecnica.Models;
using Microsoft.EntityFrameworkCore;

namespace EvaluacionTecnica.Controllers
{
    public class LoginController : Controller
    {
        private readonly EvaluacionTecnicaDbContext _dbcontext;

        // Constructor: Configura el DbContext
        public LoginController(EvaluacionTecnicaDbContext dbcontext)
        {
            _dbcontext = dbcontext ?? throw new ArgumentNullException(nameof(dbcontext));
        }

        // Valida al usuario y maneja la sesión
        [HttpPost]
        [Route("ValidarUsuario")]
        public async Task<IActionResult> ValidarUsuario([FromForm] LoginViewModel oUsuario)
        {
            // Busca el usuario por nombre y contraseña
            var usuarioEncontrado = await _dbcontext.Usuarios.FirstOrDefaultAsync(u => u.UsuarioNombre == oUsuario.UsuarioNombre && u.Contrasena == oUsuario.Contrasena);

            if (usuarioEncontrado == null)
            {
                // Usuario no encontrado
                return Json(new { success = false, mensaje = "Usuario no encontrado" });
            }

            // Guarda el nombre del usuario en la sesión
            HttpContext.Session.SetString("Usuario", usuarioEncontrado.UsuarioNombre);

            // Redirige según el rol del usuario
            if (usuarioEncontrado.UsuarioNombre == "ADMIN")
            {
                return Json(new { success = true, redirectUrl = Url.Action("RolesView", "Roles") });
            }
            else if (usuarioEncontrado.UsuarioNombre == "DESARROLLADOR")
            {
                return Json(new { success = true, redirectUrl = Url.Action("UsuarioView", "Usuario") });
            }
            else
            {
                return Json(new { success = false, mensaje = "Usuario sin permisos para acceder a esta vista." });
            }
        }
    }
}

