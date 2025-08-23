using Api_FiesteDocs.Entities;
using Api_FiesteDocs.Models;

namespace Api_FiesteDocs.Services.Interfaces
{
    /// <summary>
    /// Interfaz que define los métodos de gestión para la entidad "Instrumento".
    /// Los instrumentos pertenecen a una sección (ejemplo: Metales → Trompeta, Tuba, etc.).
    /// </summary>
    public interface I_Instrumento
    {
        /// <summary>
        /// Obtiene la lista completa de instrumentos sin ningún filtro.
        /// </summary>
        /// <returns>Lista de objetos <see cref="Instrumento"/>.</returns>
        public List<Instrumento> Listar();

        /// <summary>
        /// Obtiene los instrumentos que pertenecen a un grupo específico.
        /// </summary>
        /// <param name="Id_Grupo">Id del grupo.</param>
        /// <returns>Lista de instrumentos pertenecientes al grupo indicado.</returns>
        public List<Instrumento> ListarIdGrupo(int Id_Grupo);

        /// <summary>
        /// Obtiene los instrumentos que pertenecen a una sección específica.
        /// </summary>
        /// <param name="Id_Seccion">Id de la sección.</param>
        /// <returns>Lista de instrumentos pertenecientes a esa sección.</returns>
        public List<Instrumento> ListarIdSeccion(int Id_Seccion);

        /// <summary>
        /// Obtiene un instrumento específico según su Id.
        /// </summary>
        /// <param name="Id_Instrumento">Id del instrumento.</param>
        /// <returns>Objeto <see cref="Instrumento"/> si existe, null en caso contrario.</returns>
        public Instrumento Obtener(int Id_Instrumento);

        /// <summary>
        /// Crea un nuevo instrumento en la base de datos.
        /// </summary>
        /// <param name="instrumento">Objeto <see cref="Instrumento"/> con los datos del nuevo instrumento.</param>
        /// <returns>Objeto <see cref="Request"/> indicando el resultado de la operación.</returns>
        public Request Crear(Instrumento instrumento);

        /// <summary>
        /// Edita un instrumento existente en la base de datos.
        /// </summary>
        /// <param name="instrumento">Objeto <see cref="Instrumento"/> con los datos actualizados.</param>
        /// <returns>Objeto <see cref="Request"/> indicando el resultado de la operación.</returns>
        public Request Editar(Instrumento instrumento);

        /// <summary>
        /// Elimina un instrumento de la base de datos.
        /// </summary>
        /// <param name="Id_Instrumento">Id del instrumento a eliminar.</param>
        /// <returns>Objeto <see cref="Request"/> indicando el resultado de la operación.</returns>
        public Request Eliminar(int Id_Instrumento);
    }
}
