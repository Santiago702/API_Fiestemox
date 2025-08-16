using System;
using System.Collections.Generic;

namespace Api_FiesteDocs.Entities;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nombre { get; set; }

    public string Correo { get; set; }

    public string Contrasena { get; set; }

    public string Foto { get; set; }

    public string Ciudad { get; set; }

    public bool Estado { get; set; }

    public int? IdRol { get; set; }
}
