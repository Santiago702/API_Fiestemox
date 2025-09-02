using Api_FiesteDocs.Entities;
using Api_FiesteDocs.Models;
using Dropbox.Api.Files;

namespace Api_FiesteDocs.Services.Interfaces
{
    /// <summary>
    /// Define las operaciones disponibles para gestionar archivos en Dropbox
    /// desde el sistema. Permite crear, listar y eliminar archivos.
    /// </summary>
    public interface I_Archivo
    {
        /// <summary>
        /// Sube un archivo a Dropbox dentro de la carpeta especificada.
        /// </summary>
        /// <param name="Partitura">Objeto con la información del archivo</param>
        /// <returns>Ruta completa del archivo en Dropbox.</returns>
        Task<Request> Crear(Partitura Partitura);

        /// <summary>
        /// Lista los archivos de una carpeta en Dropbox (excluye las subcarpetas).
        /// </summary>
        /// <param name="Nombre">Nombre de la carpeta en Dropbox.</param>
        /// <returns>Lista de objetos FileMetadata que representan los archivos encontrados.</returns>
        Task<List<MetaDatos>> Listar(string NombreCarpeta);

        /// <summary>
        /// Elimina un archivo de Dropbox a partir del nombre del archivo 
        /// dentro de una carpeta específica.
        /// </summary>
        /// <param name="Partitura">Objeto con la información del archivo</param>
        /// <returns>Objeto Metadata con información sobre el archivo eliminado.</returns>
        Task<Request> EliminarNombre(Partitura Partitura);

        /// <summary>
        /// Elimina un archivo de Dropbox utilizando su ruta completa.
        /// </summary>
        /// <param name="Ruta">Ruta completa al archivo en Dropbox (incluyendo carpeta y nombre).</param>
        /// <returns>String con información sobre el archivo eliminado.</returns>
        Task<Request> EliminarRuta(string Ruta);
    }
}
