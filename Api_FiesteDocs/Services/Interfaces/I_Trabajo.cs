using Api_FiesteDocs.Models;
using Api_FiesteDocs.Entities;

namespace Api_FiesteDocs.Services.Interfaces
{
    /// <summary>
    /// Define los métodos de servicio para la gestión de trabajos en los ensayos.
    /// </summary>
    public interface I_Trabajo
    {
        /// <summary>
        /// Obtiene la lista completa de trabajos registrados en la base de datos.
        /// </summary>
        /// <returns>Una lista de objetos <see cref="Trabajo"/>.</returns>
        public List<Trabajo> Listar();

        /// <summary>
        /// Obtiene la lista completa de información de trabajos registrados en la base de datos, sin sus archivos.
        /// </summary>
        /// <returns>Una lista de objetos <see cref="InfoTrabajo"/>.</returns>
        public List<InfoTrabajo> ListarInfo();

        /// <summary>
        /// Obtiene un trabajo específico a partir de su identificador.
        /// </summary>
        /// <param name="Id_Trabajo">Identificador único del trabajo.</param>
        /// <returns>Un objeto <see cref="Trabajo"/> correspondiente al identificador, o <c>null</c> si no existe.</returns>
        public Trabajo ObtenerId(int Id_Trabajo);

        /// <summary>
        /// Obtiene los trabajos asociados a un ensayo
        /// </summary>
        /// <param name="Id_Ensayo">Identificador único del ensayo.</param>
        /// <returns>Un objeto <see cref="Trabajo"/> correspondiente al identificador, o <c>null</c> si no existe.</returns>
        public List<InfoTrabajo> ListarIdEnsayo(int Id_Ensayo);

        /// <summary>
        /// Crea un nuevo trabajo en la base de datos.
        /// </summary>
        /// <param name="trabajo">Objeto <see cref="Trabajo"/> con la información del trabajo a registrar.</param>
        /// <returns>Un objeto <see cref="Request"/> indicando si la operación fue exitosa o no.</returns>
        public Request Crear(Trabajo trabajo);

        /// <summary>
        /// Edita la información de un trabajo existente en la base de datos.
        /// Solo se actualizarán los campos enviados en el objeto <paramref name="trabajo"/>.
        /// </summary>
        /// <param name="trabajo">Objeto <see cref="Trabajo"/> con los nuevos valores.</param>
        /// <returns>Un objeto <see cref="Request"/> indicando si la operación fue exitosa o no.</returns>
        public Request Editar(Trabajo trabajo);

        /// <summary>
        /// Elimina un trabajo de la base de datos según su identificador.
        /// </summary>
        /// <param name="Id_Trabajo">Identificador único del trabajo a eliminar.</param>
        /// <returns>Un objeto <see cref="Request"/> con el resultado de la operación.</returns>
        public Request Eliminar(int Id_Trabajo);
    }
}
