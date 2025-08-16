using System;
using System.Collections.Generic;

namespace Api_FiesteDocs.Entities;

public partial class Instrumento
{
    public int IdInstrumento { get; set; }

    public string Nombre { get; set; }

    public int IdSeccion { get; set; }
}
