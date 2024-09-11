using EvaluacionTecnica.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvaluacionTecnica.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly EvaluacionTecnicaDbContext _dbcontext;

        // Constructor de inyeccion de dependencias del contexto de la BD
        public UsuarioController(EvaluacionTecnicaDbContext context)
        {
            _dbcontext = context;
        }

        // Muestra la vista de usuarios
        [HttpGet]
        [Route("UsuarioView")]
        public async Task<IActionResult> UsuarioView()
        {
            var usuarios = await _dbcontext.Usuarios.ToListAsync();
            return View("UsuarioView", usuarios);
        }

        // Agrega un nuevo usuario
        [HttpPost]
        public async Task<IActionResult> AgregarUsu([FromForm] Usuario Request)
        {
            await _dbcontext.Usuarios.AddAsync(Request);
            await _dbcontext.SaveChangesAsync();
            return RedirectToAction("UsuarioView", "Usuario");
        }

        // Elimina un usuario por ID
        [HttpPost]
        public async Task<IActionResult> EliminarUsu(int Id)
        {
            var usu = await _dbcontext.Usuarios.FindAsync(Id);
            if (usu == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Usuario no encontrado");
            }

            _dbcontext.Usuarios.Remove(usu);
            await _dbcontext.SaveChangesAsync();
            return RedirectToAction("UsuarioView", "Usuario");
        }

        // Actualiza un usuario existente
        [HttpPost]
        public async Task<IActionResult> ActualizarUsu([FromForm] Usuario Request)
        {
            var usuExistente = await _dbcontext.Usuarios.FindAsync(Request.Id);

            if (usuExistente == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Usuario no encontrado");
            }

            // Actualiza los campos del usuario
            usuExistente.RolesId = Request.RolesId;
            usuExistente.Nombre = Request.Nombre;
            usuExistente.Apellido = Request.Apellido;
            usuExistente.Cedula = Request.Cedula;
            usuExistente.UsuarioNombre = Request.UsuarioNombre;
            usuExistente.Contrasena = Request.Contrasena;
            usuExistente.FechaNacimiento = Request.FechaNacimiento;
            usuExistente.FechaTransaccion = DateTime.Now;

            _dbcontext.Usuarios.Update(usuExistente);
            await _dbcontext.SaveChangesAsync();

            return RedirectToAction("UsuarioView", "Usuario");
        }
    }
}
