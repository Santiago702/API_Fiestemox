using System;
using System.Collections.Generic;

namespace Api_FiesteDocs.Models;

public partial class Grupo
{
    public int IdGrupo { get; set; }

    public int IdUsuarioDirector { get; set; }

    public string Nombre { get; set; } = null!;

    public string Ciudad { get; set; } = null!;

    public string? Codigo { get; set; }

    public virtual ICollection<Ensayo> Ensayos { get; set; } = new List<Ensayo>();

    public virtual ICollection<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();
}
