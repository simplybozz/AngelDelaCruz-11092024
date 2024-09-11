using System;
using System.Collections.Generic;

namespace EvaluacionTecnica.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public int RolesId { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public long Cedula { get; set; }

    public string? UsuarioNombre { get; set; }

    public string? Contrasena { get; set; }

    public DateTime? FechaNacimiento { get; set; }

    public string? UsuarioTransaccion { get; set; }

    public DateTime? FechaTransaccion { get; set; }

    public virtual Role Roles { get; set; } = null!;
}
