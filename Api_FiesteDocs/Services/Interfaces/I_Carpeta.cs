using Dropbox.Api;

namespace Api_FiesteDocs.Services.Interfaces
{
    /// <summary>
    /// Define las operaciones de gestión de carpetas en Dropbox.
    /// Permite crear, editar (renombrar), listar y eliminar carpetas.
    /// </summary>
    public interface I_Carpeta
    {
        /// <summary>
        /// Renombra una carpeta en Dropbox.
        /// </summary>
        /// <param name="nombreActual">Nombre actual de la carpeta.</param>
        /// <param name="nuevoNombre">Nuevo nombre que tendrá la carpeta.</param>
        Task Editar(string nombreActual, string nuevoNombre);

        /// <summary>
        /// Crea una nueva carpeta en Dropbox en la ruta especificada.
        /// </summary>
        /// <param name="folderPath">Ruta/nombre de la carpeta a crear.</param>
        Task Crear(string folderPath);

        /// <summary>
        /// Lista las carpetas y archivos dentro del directorio raíz de Dropbox.
        /// Solo devuelve información de primer nivel (no recursivo).
        /// </summary>
        
        Task Listar();

        /// <summary>
        /// Elimina una carpeta existente en Dropbox según el nombre indicado.
        /// </summary>
        /// <param name="folderName">Nombre de la carpeta a eliminar.</param>
        Task Eliminar(string folderName);
    }
}
