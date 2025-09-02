using Api_FiesteDocs.Functions;
using Api_FiesteDocs.Models;
using Api_FiesteDocs.Services.Interfaces;
using Dropbox.Api;
using Dropbox.Api.Files;

namespace Api_FiesteDocs.Services
{
    public class S_Carpeta : I_Carpeta
    {
        private readonly I_Dropbox _dropbox;
        private DropboxClient _dbx;

        public S_Carpeta(I_Dropbox dropbox)
        {
            _dropbox = dropbox ?? throw new ArgumentNullException(nameof(dropbox));
        }

        /// <summary>
        /// Asegura que _dbx esté inicializado con un token válido.
        /// </summary>
        private async Task<DropboxClient> GetClientAsync()
        {
            if (_dbx == null)
            {
                string token = await _dropbox.Token();
                _dbx = new DropboxClient(token);
            }
            return _dbx;
        }

        public async Task<Request> Crear(string rutaCarpeta)
        {
            var dbx = await GetClientAsync();
            rutaCarpeta = Archivos.NormalizarCarpeta(rutaCarpeta);
            try
            {
                var result = await dbx.Files.CreateFolderV2Async(new CreateFolderArg(rutaCarpeta));
                return new Request
                {
                    Message = $"Carpeta creada: {result.Metadata.Name}",
                    Success = true
                };
            }
            catch (ApiException<CreateFolderError> ex)
            {
                return new Request
                {
                    Message = $"Error al crear carpeta: {ex.ErrorResponse}",
                    Success = false
                };
            }

        }

        public async Task<Request> Editar(string nombreActual, string nuevoNombre)
        {
            var dbx = await GetClientAsync();
            nombreActual = Archivos.NormalizarCarpeta(nombreActual);
            nuevoNombre = Archivos.NormalizarCarpeta(nuevoNombre);
            try
            {
                var respuesta = await dbx.Files.MoveV2Async(nombreActual, nuevoNombre);
                return new Request
                {
                    Message = $"Carpeta renombrada: {respuesta.Metadata.Name}",
                    Success = true
                };
            }
            catch (ApiException<RelocationError> ex)
            {
                return new Request
                {
                    Message = $"Error al renombrar carpeta: {ex.ErrorResponse}",
                    Success = false
                };
            }
        }

        public async Task<Request> Eliminar(string nombreCarpeta)
        {
            var dbx = await GetClientAsync();
            nombreCarpeta = Archivos.NormalizarCarpeta(nombreCarpeta);
            try
            {
                var result = await dbx.Files.DeleteV2Async(nombreCarpeta);
                return new Request
                {
                    Message = $"Carpeta eliminada: {result.Metadata.Name}",
                    Success = true
                };
            }
            catch (ApiException<DeleteError> ex)
            {
                return new Request
                {
                    Message = $"Error al eliminar carpeta: {ex.ErrorResponse}",
                    Success = false
                };
            }
        }

        public async Task<List<MetaDatos>> Listar(string rutaCarpeta)
        {
            var dbx = await GetClientAsync();
            var resultados = new List<MetaDatos>();

            string rutaNormalizada = Archivos.NormalizarCarpeta(rutaCarpeta);

            var lista = await dbx.Files.ListFolderAsync(rutaNormalizada);

            resultados.AddRange(lista.Entries
                .Where(e => e.IsFolder)
                .Select(e => new MetaDatos
                {
                    Nombre = e.Name,
                    Ruta = e.PathDisplay
                }));

            while (lista.HasMore)
            {
                lista = await dbx.Files.ListFolderContinueAsync(lista.Cursor);
                resultados.AddRange(lista.Entries
                    .Where(e => e.IsFolder)
                    .Select(e => new MetaDatos
                    {
                        Nombre = e.Name,
                        Ruta = e.PathDisplay
                    }));
            }

            return resultados;
        }

    }
}
