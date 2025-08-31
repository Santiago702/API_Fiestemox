using Api_FiesteDocs.Data;
using Api_FiesteDocs.Entities;
using Api_FiesteDocs.Functions;
using Api_FiesteDocs.Models;
using Api_FiesteDocs.Services.Interfaces;
using Dropbox.Api;
using Dropbox.Api.Files;
using System.Collections.Generic;

namespace Api_FiesteDocs.Services
{
    public class S_Archivo:I_Archivo
    {
        private readonly IConfiguration _configuration;
        private static string _token;
        private readonly DropboxClient _dbx;
        public S_Archivo(IConfiguration configuration)
        {
            _configuration = configuration;

            var token = _configuration.GetSection("Token:Key").Value
                        ?? throw new ArgumentNullException("El token de Dropbox no está configurado.");

            _dbx = new DropboxClient(token); 
        }

        public async Task<string> Crear(Partitura Partitura)
        {
            IFormFile Archivo = Archivos.ConvertirIFormFile(Partitura);
            if (Archivo == null || Archivo.Length == 0)
                return null;
            string Ruta = $"/{Partitura.Carpeta.ToUpper()}/{Archivo.FileName}";
            try
            {
                using var stream = Archivo.OpenReadStream();
                var MetaData = await _dbx.Files.UploadAsync(
                    Ruta,
                    WriteMode.Add.Instance,
                    autorename: true,
                    body: stream
                    );

                return MetaData.PathDisplay + Partitura.Tipo.Trim().ToLower();
            }
            catch(ApiException<UploadError> ex)
            {
                Console.WriteLine($"Upload error: {ex.ErrorResponse}");
                throw;
            }
            


        }

        public async Task<List<MetaDatos>> Listar(string NombreCarpeta)
        {
            List<FileMetadata> archivosResultantes = new List<FileMetadata>();
            string carpetaNormalizada = Archivos.NormalizarCarpeta(NombreCarpeta);

            string rutaListar = string.IsNullOrEmpty(carpetaNormalizada) 
                ? string.Empty 
                : carpetaNormalizada;

            var lista = await _dbx.Files.ListFolderAsync(rutaListar);

            archivosResultantes.AddRange(lista.Entries.Where(e => e.IsFile).Select(e => e.AsFile));
            while (lista.HasMore)
            {
                lista = await _dbx.Files.ListFolderContinueAsync(lista.Cursor);
                archivosResultantes.AddRange(lista.Entries.Where(e => e.IsFile).Select(e => e.AsFile));
            }
            List<MetaDatos> datos = new List<MetaDatos>();
            foreach (var archivo in archivosResultantes)
            {
                datos.Add(new MetaDatos
                {
                    Nombre = archivo.Name,
                    Ruta = archivo.PathDisplay,
                    Tamano = Math.Round(archivo.Size / 1024.0, 2) + " KB"
                });
            }
            return datos;
        }

        public async Task<string> EliminarNombre(Partitura Partitura)
        {
            if (string.IsNullOrWhiteSpace(Partitura.Nombre)) 
                throw new ArgumentException("Nombre de Archivo vacío", nameof(Partitura.Nombre));
            
            string carpetaNormalizada = Archivos.NormalizarCarpeta(Partitura.Carpeta);
            string ruta = string.IsNullOrEmpty(carpetaNormalizada) 
                ? $"/{Partitura.Nombre.Trim()}" 
                : $"{carpetaNormalizada}/{Partitura.Nombre.Trim()}";

            return await EliminarRuta(ruta);
        }

        public async Task<string> EliminarRuta(string ruta)
        {
            if (string.IsNullOrWhiteSpace(ruta))
                throw new ArgumentException("Ruta vacía", nameof(ruta));

            if (!ruta.StartsWith("/"))
                ruta = "/" + ruta.Trim();

            try
            {
                var metadata = await _dbx.Files.GetMetadataAsync(ruta);

                if (metadata == null)
                    return $"El archivo o carpeta '{ruta}' no existe en Dropbox.";

                var result = await _dbx.Files.DeleteV2Async(ruta);

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
