using Api_FiesteDocs.Entities;
using Api_FiesteDocs.Functions;
using Api_FiesteDocs.Models;
using Api_FiesteDocs.Services.Interfaces;
using Dropbox.Api;
using Dropbox.Api.Files;
using Microsoft.AspNetCore.Http;

namespace Api_FiesteDocs.Services
{
    public class S_Archivo : I_Archivo
    {
        private readonly I_Dropbox _dropbox;
        private DropboxClient _dbx;

        public S_Archivo(I_Dropbox dropbox)
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

        public async Task<string> Crear(Partitura partitura)
        {
            var dbx = await GetClientAsync();

            IFormFile archivo = Archivos.ConvertirIFormFile(partitura);
            if (archivo == null || archivo.Length == 0) return null;

            string ruta = $"/{partitura.Carpeta.ToUpper()}/{archivo.FileName}";

            try
            {
                using var stream = archivo.OpenReadStream();
                var metadata = await dbx.Files.UploadAsync(
                    ruta,
                    WriteMode.Add.Instance,
                    autorename: true,
                    body: stream
                );

                return metadata.PathDisplay + "." + partitura.Tipo.Trim().ToLower();
            }
            catch (ApiException<UploadError> ex)
            {
                Console.WriteLine($"Upload error: {ex.ErrorResponse}");
                throw;
            }
        }

        public async Task<List<MetaDatos>> Listar(string nombreCarpeta)
        {
            var dbx = await GetClientAsync();
            List<FileMetadata> archivosResultantes = new List<FileMetadata>();
            string carpetaNormalizada = Archivos.NormalizarCarpeta(nombreCarpeta);
            string rutaListar = string.IsNullOrEmpty(carpetaNormalizada) ? string.Empty : carpetaNormalizada;

            var lista = await dbx.Files.ListFolderAsync(rutaListar);
            archivosResultantes.AddRange(lista.Entries.Where(e => e.IsFile).Select(e => e.AsFile));

            while (lista.HasMore)
            {
                lista = await dbx.Files.ListFolderContinueAsync(lista.Cursor);
                archivosResultantes.AddRange(lista.Entries.Where(e => e.IsFile).Select(e => e.AsFile));
            }

            return archivosResultantes.Select(a => new MetaDatos
            {
                Nombre = a.Name,
                Ruta = a.PathDisplay,
                Tamano = Math.Round(a.Size / 1024.0, 2) + " KB"
            }).ToList();
        }

        public async Task<string> EliminarNombre(Partitura partitura)
        {
            if (string.IsNullOrWhiteSpace(partitura.Nombre))
                throw new ArgumentException("Nombre de Archivo vacío", nameof(partitura.Nombre));

            string carpetaNormalizada = Archivos.NormalizarCarpeta(partitura.Carpeta);
            string ruta = string.IsNullOrEmpty(carpetaNormalizada)
                ? $"/{partitura.Nombre.Trim()}"
                : $"{carpetaNormalizada}/{partitura.Nombre.Trim()}";

            return await EliminarRuta(ruta);
        }

        public async Task<string> EliminarRuta(string ruta)
        {
            if (string.IsNullOrWhiteSpace(ruta))
                throw new ArgumentException("Ruta vacía", nameof(ruta));

            if (!ruta.StartsWith("/")) ruta = "/" + ruta.Trim();
            var dbx = await GetClientAsync();

            try
            {
                var metadata = await dbx.Files.GetMetadataAsync(ruta);
                if (metadata == null)
                    return $"El archivo o carpeta '{ruta}' no existe en Dropbox.";

                var result = await dbx.Files.DeleteV2Async(ruta);
                return $"Eliminado correctamente: {result.Metadata.Name}";
            }
            catch (ApiException<GetMetadataError> ex) when (ex.ErrorResponse.IsPath && ex.ErrorResponse.AsPath.Value.IsNotFound)
            {
                return $"No se encontró el archivo o carpeta en la ruta: {ruta}";
            }
            catch (ApiException<DeleteError> ex)
            {
                throw new InvalidOperationException($"Error eliminando {ruta} en Dropbox: {ex.ErrorResponse}", ex);
            }
        }
    }
}
