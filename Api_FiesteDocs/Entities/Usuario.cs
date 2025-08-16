using System;
using System.Collections.Generic;

namespace Api_FiesteDocs.Entities;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public string Foto { get; set; } = null!;

    public string Ciudad { get; set; } = null!;

    public bool Estado { get; set; }

    public int? IdRol { get; set; }

    public virtual ICollection<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();
}
