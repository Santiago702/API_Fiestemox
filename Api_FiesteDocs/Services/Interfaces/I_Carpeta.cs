using Api_FiesteDocs.Models;
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
        Task<Request> Editar(string nombreActual, string nuevoNombre);

        /// <summary>
        /// Crea una nueva carpeta en Dropbox en la ruta especificada.
        /// </summary>
        /// <param name="rutaCarpeta">Ruta/nombre de la carpeta a crear.</param>
        Task<Request> Crear(string rutaCarpeta);

        /// <summary>
        /// Lista las carpetas y archivos dentro de un directorio específico de Dropbox.
        /// /// <param name="Ruta">Nombre de la carpeta donde se va a buscar subcarpetas</param>
        /// </summary>

        Task<List<MetaDatos>>Listar(string Ruta);

        /// <summary>
        /// Elimina una carpeta existente en Dropbox según el nombre indicado.
        /// </summary>
        /// <param name="nombreCarpeta">Nombre de la carpeta a eliminar.</param>
        Task<Request>Eliminar(string nombreCarpeta);

        /// <summary>
        /// Busca si existe una carpeta especifica en Dropbox.
        /// </summary>
        /// <param name="rutaCarpeta">Ruta/nombre de la carpeta a buscar.</param>
        Task<Request>Existe(string rutaCarpeta);
    }
}
