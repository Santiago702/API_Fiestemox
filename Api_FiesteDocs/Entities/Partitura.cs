using System;
using System.Collections.Generic;

namespace Api_FiesteDocs.Entities;

public partial class Partitura
{
    public int IdPartitura { get; set; }

    public int IdSeccion { get; set; }

    public string Archivo { get; set; }

    public string Comentarios { get; set; }

    public string Nombre { get; set; }
    public string Carpeta { get; set; }
    public string Tipo { get; set; }
    }
