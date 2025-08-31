using Api_FiesteDocs.Entities;

namespace Api_FiesteDocs.Functions
{
    /// <summary>
    /// Contiene funciones auxiliares para formatear y manipular entidades
    /// del sistema antes de ser procesadas en los servicios.
    /// </summary>
    public class Clases
    {

        /// <summary>
        /// Formatea un objeto <see cref="Usuario"/> aplicando reglas
        /// de normalización como mayúsculas y eliminación de espacios en blanco.
        /// </summary>
        /// <param name="user">Objeto usuario a formatear.</param>
        /// <returns>Un nuevo objeto <see cref="Usuario"/> con los datos formateados.</returns>
        public static Usuario Formatear(Usuario user)
        {
            return new Usuario
            {
                Nombre = user.Nombre.ToUpper(),
                Ciudad = user.Ciudad.ToUpper(),
                Correo = user.Correo.Trim(),
                Contrasena = user.Contrasena,
                Estado = user.Estado,
                Foto = user.Foto,
                IdRol = user.IdRol,
                IdUsuario = user.IdUsuario
            };
        }

        /// <summary>
        /// Formatea un objeto <see cref="Estudiante"/> aplicando reglas
        /// de normalización como mayúsculas en tipo de documento.
        /// </summary>
        /// <param name="estudiante">Objeto estudiante a formatear.</param>
        /// <returns>Un nuevo objeto <see cref="Estudiante"/> con los datos formateados.</returns>
        public static Estudiante Formatear(Estudiante estudiante)
        {
            return new Estudiante
            {
                IdEstudiante = estudiante.IdEstudiante,
                Documento = estudiante.Documento,
                TipoDocumento = estudiante.TipoDocumento.ToUpper(),
                IdInstrumento = estudiante.IdInstrumento,
                IdUsuario = estudiante.IdUsuario
            };
        }



        /// <summary>
        /// Formatea un objeto <see cref="Grupo"/> aplicando reglas
        /// de normalización como convertir a mayúsculas los campos de texto.
        /// </summary>
        /// <param name="grupo">Objeto grupo a formatear.</param>
        /// <returns>Un nuevo objeto <see cref="Grupo"/> con los datos formateados.</returns>
        public static Grupo Formatear(Grupo grupo)
        {
            return new Grupo
            {
                IdGrupo = grupo.IdGrupo,
                Ciudad = grupo.Ciudad.ToUpper(),
                Codigo = grupo.Codigo.ToUpper(),
                IdUsuarioDirector = grupo.IdUsuarioDirector,
                Nombre = grupo.Nombre.ToUpper()
            };


        }

        /// <summary>
        /// Formatea un objeto <see cref="Seccion"/> aplicando reglas
        /// de normalización como convertir a mayúsculas los campos de texto.
        /// </summary>
        /// <param name="seccion">Objeto seccion a formatear.</param>
        /// <returns>Un nuevo objeto <see cref="Seccion"/> con los datos formateados.</returns>
        public static Seccion Formatear(Seccion seccion)
        {
            return new Seccion
            {
                IdSeccion = seccion.IdSeccion,
                Descripcion = seccion.Descripcion.ToUpper(),
                IdGrupo = seccion.IdGrupo
            };


        }
    }
}
