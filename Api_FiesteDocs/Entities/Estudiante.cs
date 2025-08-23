using System;
using System.Collections.Generic;

namespace Api_FiesteDocs.Entities;

public partial class Estudiante
{
    public int IdEstudiante { get; set; }

    public string Documento { get; set; }

    public string TipoDocumento { get; set; }

    public int? IdInstrumento { get; set; }

    public int? IdUsuario { get; set; }
}
