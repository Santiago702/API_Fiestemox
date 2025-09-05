using Api_FiesteDocs.Entities;

namespace Api_FiesteDocs.Functions
{
    public class Archivos
    {
        /// <summary>
        /// Normaliza el nombre de una carpeta para que cumpla con el formato esperado en Dropbox.
        /// </summary>
        /// <param name="NombreCarpeta">
        /// Nombre de la carpeta a normalizar. Puede venir con espacios, barras iniciales o finales.
        /// </param>
        /// <returns>
        /// Retorna la ruta de la carpeta en formato válido:
        /// - Si el valor es nulo o vacío, devuelve <c>string.Empty</c>.  
        /// - Si tiene texto, devuelve la cadena con una sola barra inicial (ejemplo: "/MiCarpeta").
        /// </returns>
        public static string NormalizarCarpeta(string NombreCarpeta)
        {
            
            if (string.IsNullOrWhiteSpace(NombreCarpeta))
                return string.Empty;

            var normalizada = NombreCarpeta.ToUpper().Trim();

            return "/" + normalizada;
        }


        /// <summary>
        /// Convierte un archivo recibido como <see cref="IFormFile"/> en una cadena Base64.
        /// </summary>
        /// <param name="Archivo">Archivo a convertir.</param>
        /// <returns>
        /// Cadena en formato Base64 que representa el contenido del archivo.  
        /// Si el archivo está vacío o es nulo, retorna <c>null</c>.
        /// </returns>
        public static async Task<string> ConvertirBase64(IFormFile Archivo)
        {
            if (Archivo == null || Archivo.Length == 0) 
                return null;
            using var ms = new MemoryStream();

            await Archivo.CopyToAsync(ms);
            return Convert.ToBase64String(ms.ToArray());
        }


        /// <summary>
        /// Convierte una cadena Base64 en un archivo <see cref="IFormFile"/>.
        /// </summary>
        /// <param name="Base64">Cadena Base64 a convertir.</param>
        /// <param name="NombreArchivo">Nombre que tendrá el archivo resultante.</param>
        /// <param name="Tipo">Tipo MIME del archivo (ejemplo: "application/pdf").</param>
        /// <returns>
        /// Objeto <see cref="IFormFile"/> construido a partir de los datos en Base64.  
        /// Si la cadena es nula o inválida, retorna <c>null</c>.
        /// </returns>
        public static IFormFile ConvertirIFormFile(Partitura partitura)
    {
        if (string.IsNullOrWhiteSpace(partitura.Archivo))
            return null;

        try
        {
            var fileBytes = Convert.FromBase64String(partitura.Archivo);
            var stream = new MemoryStream(fileBytes);


                return new FormFile(stream, 0, fileBytes.Length, "file", partitura.Nombre)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = (string.IsNullOrEmpty(partitura.Tipo)) 
                    ? "application/" + partitura.Tipo.Trim().ToLower()
                    : "application/octet-stream"
                };
        }
        catch
        {
            return null; 
        }
    }


}
}
