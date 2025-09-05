using Api_FiesteDocs.Entities;
using Api_FiesteDocs.Models;

namespace Api_FiesteDocs.Services.Interfaces
{
    /// <summary>
    /// Define los métodos de servicio para la gestión de partituras dentro del sistema.
    /// </summary>
    public interface I_Partitura
    {
        /// <summary>
        /// Obtiene todas las partituras registradas en la base de datos.
        /// </summary>
        /// <returns>Una lista de objetos <see cref="Partitura"/>.</returns>
        Task<List<Partitura>> Listar();

        /// <summary>
        /// Obtiene los datos necesarios de las partituras registradas en la base de datos sin traer los documentos.
        /// </summary>
        /// /// <param name="Id_Grupo">Identificador único del grupo.</param>
        /// <returns>Una lista de objetos <see cref="InfoPartitura"/>.</returns>
        Task<List<InfoPartitura>> ListarInfo(int Id_Grupo);

        /// <summary>
        /// Obtiene todas las partituras asociadas a una sección específica.
        /// </summary>
        /// <param name="Id_Seccion">Identificador único de la sección.</param>
        /// <returns>Una lista de objetos <see cref="Partitura"/> pertenecientes a la sección indicada.</returns>
        Task<List<Partitura>> ListarIdSeccion(int Id_Seccion);

        /// <summary>
        /// Obtiene todas las partituras asociadas a un grupo musical.
        /// </summary>
        /// <param name="Id_Grupo">Identificador único del grupo musical.</param>
        /// <returns>Una lista de objetos <see cref="Partitura"/> relacionadas con el grupo indicado.</returns>
        Task<List<Partitura>> ListarIdGrupo(int Id_Grupo);

        /// <summary>
        /// Obtiene una partitura específica por su identificador.
        /// </summary>
        /// <param name="Id_Partitura">Identificador único de la partitura.</param>
        /// <returns>Un objeto <see cref="Partitura"/> si existe, en caso contrario <c>null</c>.</returns>
        Task<Partitura> Obtener(int Id_Partitura);

        /// <summary>
        /// Busca partituras cuyo nombre coincida parcial o totalmente con el texto proporcionado.
        /// </summary>
        /// <param name="Nombre">Texto a buscar en los nombres de las partituras.</param>
        /// <returns>Una lista de objetos <see cref="Partitura"/> que coincidan con la búsqueda.</returns>
        Task<List<Partitura>> Buscar(string Nombre);

        /// <summary>
        /// Crea una nueva partitura en la base de datos.
        /// </summary>
        /// <param name="partitura">Objeto <see cref="Partitura"/> con la información de la nueva partitura.</param>
        /// <returns>Un objeto <see cref="Request"/> indicando si la operación fue exitosa.</returns>
        Task<Request> Crear(Partitura partitura);

        /// <summary>
        /// Edita una partitura existente en la base de datos.
        /// Solo se actualizarán los campos proporcionados en el objeto <paramref name="partitura"/>.
        /// </summary>
        /// <param name="partitura">Objeto <see cref="Partitura"/> con los nuevos datos.</param>
        /// <returns>Un objeto <see cref="Request"/> con el resultado de la operación.</returns>
        Task<Request> Editar(Partitura partitura);

        /// <summary>
        /// Elimina una partitura de la base de datos según su identificador.
        /// </summary>
        /// <param name="Id_Partitura">Identificador único de la partitura a eliminar.</param>
        /// <returns>Un objeto <see cref="Request"/> con el resultado de la operación.</returns>
        Task<Request> Eliminar(int Id_Partitura);
    }
}
