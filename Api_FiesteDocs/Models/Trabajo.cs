using System;
using System.Collections.Generic;

namespace Api_FiesteDocs.Models;

public partial class Trabajo
{
    public int IdTrabajo { get; set; }

    public string Descripcion { get; set; } = null!;

    public string Detalles { get; set; } = null!;

    public string? Evidencia { get; set; }

    public string? Foto { get; set; }

    public string? Comentarios { get; set; }

    public int IdEnsayo { get; set; }

    public int IdSeccion { get; set; }

    public virtual Ensayo IdEnsayoNavigation { get; set; } = null!;

    public virtual Seccion IdSeccionNavigation { get; set; } = null!;
}
