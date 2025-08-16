namespace Api_FiesteDocs.Models
{
    public class UsuarioModel
    {
        public int IdUsuario { get; set; }

        public string Nombre { get; set; } = null!;

        public string Correo { get; set; } = null!;

        public string Contrasena { get; set; } = null!;

        public string Foto { get; set; } = null!;

        public string Ciudad { get; set; } = null!;

        public bool Estado { get; set; }

        public int IdRol { get; set; }
    }
}
