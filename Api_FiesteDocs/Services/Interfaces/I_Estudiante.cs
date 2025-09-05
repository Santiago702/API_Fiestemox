using Api_FiesteDocs.Entities;
using Api_FiesteDocs.Models;

namespace Api_FiesteDocs.Services.Interfaces
{
    public interface I_Estudiante
    {
        /// <summary>
        /// Obtiene la lista de estudiantes y sus usuarios asociados a un grupo específico.
        /// </summary>
        /// <param name="Id_Grupo">Identificador único del grupo.</param>
        /// <returns>Lista de objetos <see cref="InfoEstudiante"/> que representan
        /// la relación entre un estudiante y su usuario dentro del grupo.</returns>
        Task<List<InfoEstudiante>> Listar(int Id_Grupo);

        /// <summary>
        /// Obtiene la información de un estudiante específico junto con su usuario asociado.
        /// </summary>
        /// <param name="Id_Estudiante">Identificador único del estudiante.</param>
        /// <returns>Un objeto <see cref="InfoEstudiante"/> con los datos combinados
        /// del estudiante y su usuario.</returns>
        Task<InfoEstudiante> ObtenerId(int Id_Estudiante);

        /// <summary>
        /// Obtiene la información de un estudiante específico junto con su usuario asociado.
        /// </summary>
        /// <param name="Id_Estudiante">Identificador único del usuario.</param>
        /// <returns>Un objeto <see cref="InfoEstudiante"/> con los datos combinados
        /// del estudiante y su usuario.</returns>
        Task<InfoEstudiante> ObtenerIdUsuario(int Id_Usuario);

        /// <summary>
        /// Edita los datos de un estudiante en la base de datos.
        /// </summary>
        /// <param name="estudiante">Objeto <see cref="Estudiante"/> con la información actualizada.</param>
        /// <returns>Un objeto <see cref="Request"/> que indica el resultado de la operación.</returns>
        Task<Request> Editar(Estudiante estudiante);

        /// <summary>
        /// Elimina un estudiante y su usuario asociado de la base de datos.
        /// </summary>
        /// <param name="Id_Estudiante">Identificador único del estudiante.</param>
        /// <returns>Un objeto <see cref="Request"/> que indica el resultado de la operación.</returns>
        Task<Request> Eliminar(int Id_Estudiante);

        /// <summary>
        /// Crea un nuevo estudiante en la base de datos.
        /// </summary>
        /// <param name="estudiante">Objeto <see cref="Estudiante"/> con los datos del nuevo registro.</param>
        /// <returns>Un objeto <see cref="Request"/> que indica el resultado de la operación.</returns>
        Task<Request> Crear(InfoEstudiante estudiante);
    }
}
