using Api_FiesteDocs.Entities;
using Api_FiesteDocs.Models;

namespace Api_FiesteDocs.Services.Interfaces
{
    public interface I_Usuario
    {
        /// <summary>
        /// Obtiene todos los registros de usuarios de la BD.
        /// </summary>
        /// <returns>Lista de todos los Usuarios registrados en la BD.</returns>
        Task<List<Usuario>> Listar();

        

        /// <summary>
        /// Busca y obtiene un usuario a partir de su correo electrónico.
        /// </summary>
        /// <param name="correo">Correo electrónico del usuario a buscar.</param>
        /// <returns>Objeto Usuario correspondiente al correo proporcionado, si existe. Si no existe, devuelve modelo vacío</returns>
        Task<Usuario> ObtenerCorreo(string correo);

        /// <summary>
        /// Crea un nuevo registro de usuario en la BD.
        /// </summary>
        /// <param name="usuario">Objeto Usuario con la información del nuevo usuario.</param>
        /// <returns>True si la creación fue exitosa, False en caso contrario.</returns>
        Task<Request> Crear(Usuario usuario);

        /// <summary>
        /// Edita la información de un usuario existente en la BD.
        /// </summary>
        /// <param name="usuario">Objeto Usuario con la información actualizada.</param>
        /// <returns>True si la edición fue exitosa, False en caso contrario.</returns>
        Task<Request> Editar(Usuario usuario);

        /// <summary>
        /// Elimina un registro de usuario en la BD.
        /// </summary>
        /// <param name="id_Usuario">Identificador único del usuario a eliminar.</param>
        /// <returns>True si la eliminación fue exitosa, False en caso contrario.</returns>
        Task<Request> Eliminar(int id_Usuario);

        /// <summary>
        /// Obtiene un usuario específico a partir de su ID.
        /// </summary>
        /// <param name="id_Usuario">Identificador único del usuario.</param>
        /// <returns>Objeto Usuario correspondiente al ID proporcionado, si existe.</returns>
        Task<Usuario> ObtenerId(int id_Usuario);
    }
}
