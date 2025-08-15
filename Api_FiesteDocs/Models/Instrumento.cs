using System;
using System.Collections.Generic;

namespace Api_FiesteDocs.Models;

public partial class Instrumento
{
    public int IdInstrumento { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdSeccion { get; set; }

    public virtual Seccion IdSeccionNavigation { get; set; } = null!;
}
