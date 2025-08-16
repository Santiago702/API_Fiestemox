using System;
using System.Collections.Generic;

namespace Api_FiesteDocs.Entities;

public partial class Grupo
{
    public int IdGrupo { get; set; }

    public int IdUsuarioDirector { get; set; }

    public string Nombre { get; set; }

    public string Ciudad { get; set; }

    public string Codigo { get; set; }
}
