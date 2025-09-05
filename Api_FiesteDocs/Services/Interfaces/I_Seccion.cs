using Api_FiesteDocs.Entities;
using Api_FiesteDocs.Models;

namespace Api_FiesteDocs.Services.Interfaces
{
    /// <summary>
    /// Define las operaciones que se pueden realizar sobre una sección dentro de un grupo musical.
    /// Una sección hace referencia a subconjuntos de instrumentos o músicos que conforman un grupo
    /// (ejemplo: percusión, vientos metales, cuerdas, etc.).
    /// </summary>
    public interface I_Seccion
    {
        /// <summary>
        /// Lista todas las secciones registradas en el sistema sin filtrar por grupo o director.
        /// </summary>
        /// <returns>Una lista con todas las secciones disponibles.</returns>
        Task<List<Seccion>> Listar();

        /// <summary>
        /// Lista las secciones creadas bajo la dirección de un grupo específico.
        /// </summary>
        /// <param name="Id_Grupo">Identificador único del director.</param>
        /// <returns>Una lista de secciones pertenecientes a los grupos musicales de ese grupo musical.</returns>
        Task<List<Seccion>> ListarIdGrupo(int Id_Grupo);

        /// <summary>
        /// Obtiene la información detallada de una sección específica a partir de su identificador.
        /// </summary>
        /// <param name="Id_Seccion">Identificador único de la sección.</param>
        /// <returns>Un objeto <see cref="Seccion"/> con los datos de la sección.</returns>
        Task<Seccion> Obtener(int Id_Seccion);

        /// <summary>
        /// Crea una nueva sección dentro de un grupo musical.
        /// </summary>
        /// <param name="seccion">Objeto que contiene la información de la sección a crear.</param>
        /// <returns>Un objeto <see cref="Request"/> con el resultado de la operación.</returns>
        Task<Request> Crear(Seccion seccion);

        /// <summary>
        /// Edita los datos de una sección ya existente.
        /// </summary>
        /// <param name="seccion">Objeto con los datos actualizados de la sección.</param>
        /// <returns>Un objeto <see cref="Request"/> con el resultado de la operación.</returns>
        Task<Request> Editar(Seccion seccion);

        /// <summary>
        /// Elimina una sección a partir de su identificador único.
        /// </summary>
        /// <param name="Id_Seccion">Identificador único de la sección a eliminar.</param>
        /// <returns>Un objeto <see cref="Request"/> con el resultado de la operación.</returns>
        Task<Request> Eliminar(int Id_Seccion);
    }
}

