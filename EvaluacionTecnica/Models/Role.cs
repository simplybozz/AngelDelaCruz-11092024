using System;
using System.Collections.Generic;

namespace EvaluacionTecnica.Models;

public partial class Role
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? UsuarioTransaccion { get; set; }

    public DateTime? FechaTransaccion { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
