using Api_FiesteDocs.Entities;
using Api_FiesteDocs.Models;

namespace Api_FiesteDocs.Services.Interfaces
{
    /// <summary>
    /// Define las operaciones disponibles para gestionar ensayos dentro de un grupo musical.
    /// Un ensayo corresponde a una práctica programada con fecha, hora y grupo asociado.
    /// </summary>
    public interface I_Ensayo
    {
        /// <summary>
        /// Lista todos los ensayos programados para un grupo específico.
        /// </summary>
        /// <param name="Id_Grupo">Identificador del grupo musical.</param>
        /// <returns>Una lista de ensayos asociados al grupo.</returns>
        Task<List<Ensayo>> Listar(int Id_Grupo);

        /// <summary>
        /// Obtiene la información de un ensayo en particular a partir de su identificador.
        /// </summary>
        /// <param name="Id_Ensayo">Identificador único del ensayo.</param>
        /// <returns>El objeto <see cref="Ensayo"/> correspondiente, o null si no existe.</returns>
        Task<Ensayo> ObtenerId(int Id_Ensayo);

        /// <summary>
        /// Edita los datos de un ensayo existente.
        /// </summary>
        /// <param name="ensayo">Objeto con la información del ensayo a actualizar.</param>
        /// <returns>Un objeto <see cref="Request"/> indicando el resultado de la operación.</returns>
        Task <Request> Editar(Ensayo ensayo);

        /// <summary>
        /// Elimina un ensayo existente a partir de su identificador.
        /// </summary>
        /// <param name="Id_Ensayo">Identificador único del ensayo a eliminar.</param>
        /// <returns>Un objeto <see cref="Request"/> indicando el resultado de la operación.</returns>
        Task<Request> Eliminar(int Id_Ensayo);

        /// <summary>
        /// Crea un nuevo ensayo en el sistema.
        /// </summary>
        /// <param name="ensayo">Objeto con la información del ensayo a registrar.</param>
        /// <returns>Un objeto <see cref="Request"/> indicando si la creación fue exitosa.</returns>
        Task<Request> Crear(Ensayo ensayo);
    }
}
