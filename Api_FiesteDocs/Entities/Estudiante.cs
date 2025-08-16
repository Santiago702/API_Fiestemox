using System;
using System.Collections.Generic;

namespace Api_FiesteDocs.Entities;

public partial class Estudiante
{
    public int IdEstudiante { get; set; }

    public string Documento { get; set; } = null!;

    public string TipoDocumento { get; set; } = null!;

    public int? IdInstrumento { get; set; }

    public int? IdGrupo { get; set; }

    public int? IdUsuario { get; set; }

    public virtual Grupo? IdGrupoNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
