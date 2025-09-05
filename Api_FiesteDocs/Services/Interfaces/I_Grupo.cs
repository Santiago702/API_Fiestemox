using Api_FiesteDocs.Entities;
using Api_FiesteDocs.Models;

namespace Api_FiesteDocs.Services.Interfaces
{
    public interface I_Grupo
    {
        /// <summary>
        /// Obtiene todos los grupos musicales que pertenecen a un director en específico.
        /// </summary>
        /// <param name="Id_Director">Identificador único del usuario director.</param>
        /// <returns>Lista de objetos <see cref="Grupo"/> asociados al director.</returns>
        Task<List<Grupo>> ObtenerIdDirector(int Id_Director);


        /// <summary>
        /// Obtiene la lista de todos los grupos musicales registrados en el sistema,
        /// sin importar el director al que pertenezcan.
        /// </summary>
        /// <returns>Lista completa de <see cref="Grupo"/>.</returns>
        Task<List<Grupo>> Listar();


        /// <summary>
        /// Obtiene la información detallada de un grupo musical en específico.
        /// </summary>
        /// <param name="id_grupo">Identificador único del grupo musical.</param>
        /// <returns>Un objeto <see cref="Grupo"/> con la información del grupo, o null si no existe.</returns>
        Task<Grupo> ObtenerIdGrupo(int Id_Grupo);

        /// <summary>
        /// Obtiene todos los grupos musicales que pertenecen a un estudiante en específico.
        /// </summary>
        /// <param name="Id_Estudiante">Identificador único del usuario estudiante.</param>
        /// <returns>Lista de objetos <see cref="Grupo"/> asociados al estudiante.</returns>
        Task<List<Grupo>> ObtenerIdEstudiante(int Id_Estudiante);


        /// <summary>
        /// Edita o actualiza los datos de un grupo musical existente.
        /// </summary>
        /// <param name="grupo">Objeto <see cref="Grupo"/> con los valores actualizados.</param>
        /// <returns>
        /// Objeto <see cref="Request"/> que indica si la operación fue exitosa,
        /// y en caso contrario, devuelve el error correspondiente.
        /// </returns>
        Task<Request> Editar(Grupo grupo);


        /// <summary>
        /// Crea un nuevo grupo musical y lo asocia a un director.
        /// </summary>
        /// <param name="grupo">Objeto <see cref="Grupo"/> con la información del nuevo grupo.</param>
        /// <returns>
        /// Objeto <see cref="Request"/> que indica el resultado de la operación,
        /// incluyendo si se creó correctamente o si ocurrió un error.
        /// </returns>
        Task<Request> Crear(Grupo grupo);


        /// <summary>
        /// Elimina un grupo musical existente de la base de datos.
        /// </summary>
        /// <param name="Id_Grupo">Identificador único del grupo musical a eliminar.</param>
        /// <returns>
        /// Objeto <see cref="Request"/> que indica el resultado de la eliminación.
        /// </returns>
        Task<Request> Eliminar(int Id_Grupo);



    }
}
