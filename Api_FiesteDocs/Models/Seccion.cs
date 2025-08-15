using System;
using System.Collections.Generic;

namespace Api_FiesteDocs.Models;

public partial class Seccion
{
    public int IdSeccion { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Instrumento> Instrumentos { get; set; } = new List<Instrumento>();

    public virtual ICollection<Partitura> Partituras { get; set; } = new List<Partitura>();

    public virtual ICollection<Trabajo> Trabajos { get; set; } = new List<Trabajo>();
}
