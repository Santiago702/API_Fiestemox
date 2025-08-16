using System;
using System.Collections.Generic;

namespace Api_FiesteDocs.Entities;

public partial class Trabajo
{
    public int IdTrabajo { get; set; }

    public string Descripcion { get; set; }

    public string Detalles { get; set; }

    public string Evidencia { get; set; }

    public string Foto { get; set; }

    public string Comentarios { get; set; }

    public int IdEnsayo { get; set; }

    public int IdSeccion { get; set; }
}
