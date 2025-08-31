namespace Api_FiesteDocs.Services.Interfaces
{
    public interface I_Dropbox
    {
        /// <summary>
        /// Crea un nuevo token de acceso utilizando el token de refresco
        /// </summary>
        /// <returns></returns>
        Task<string> Token();
    }
}
