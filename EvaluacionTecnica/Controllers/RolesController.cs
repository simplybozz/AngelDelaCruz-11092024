using EvaluacionTecnica.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvaluacionTecnica.Controllers
{
    public class RolesController : Controller
    {
        private readonly EvaluacionTecnicaDbContext _dbcontext;

        // Constructor de inyeccion de dependencias del contexto de la BD
        public RolesController(EvaluacionTecnicaDbContext context)
        {
            _dbcontext = context;
        }

        // Muestra la vista con todos los roles
        [HttpGet]
        [Route("RolesView")]
        public async Task<IActionResult> RolesView()
        {
            var roles = await _dbcontext.Roles.ToListAsync();
            return View("RolesView", roles);
        }

        // Agrega un nuevo rol
        [HttpPost]
        public async Task<IActionResult> AgregarRol([FromForm] Role Request)
        {
            await _dbcontext.Roles.AddAsync(Request);
            await _dbcontext.SaveChangesAsync();
            return RedirectToAction("RolesView", "Roles");
        }

        // Elimina un rol por ID
        [HttpPost]
        public async Task<IActionResult> EliminarRol(int Id)
        {
            var rol = await _dbcontext.Roles.FindAsync(Id);
            if (rol == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Rol no encontrado");
            }

            _dbcontext.Roles.Remove(rol);
            await _dbcontext.SaveChangesAsync();
            return RedirectToAction("RolesView", "Roles");
        }

        // Actualiza un rol existente
        [HttpPost]
        public async Task<IActionResult> ActualizarRol([FromForm] Role Request)
        {
            var rolExistente = await _dbcontext.Roles.FindAsync(Request.Id);

            if (rolExistente == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Rol no encontrado");
            }

            rolExistente.Nombre = Request.Nombre;
            rolExistente.FechaTransaccion = DateTime.Now;

            _dbcontext.Roles.Update(rolExistente);
            await _dbcontext.SaveChangesAsync();

            return RedirectToAction("RolesView", "Roles");
        }
    }
}
