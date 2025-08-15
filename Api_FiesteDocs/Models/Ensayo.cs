using System;
using System.Collections.Generic;

namespace Api_FiesteDocs.Models;

public partial class Ensayo
{
    public int IdEnsayo { get; set; }

    public DateOnly Fecha { get; set; }

    public TimeOnly HoraInicio { get; set; }

    public TimeOnly HoraFin { get; set; }

    public int IdGrupo { get; set; }

    public virtual Grupo IdGrupoNavigation { get; set; } = null!;

    public virtual ICollection<Trabajo> Trabajos { get; set; } = new List<Trabajo>();
}
