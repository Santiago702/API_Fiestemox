using System;
using System.Collections.Generic;

namespace Api_FiesteDocs.Entities;

public partial class Seccion
{
    public int IdSeccion { get; set; }

    public string Descripcion { get; set; }

    public int? IdGrupo { get; set; }
}
