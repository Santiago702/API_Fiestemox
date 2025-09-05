using Api_FiesteDocs.Models;
using Api_FiesteDocs.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_FiesteDocs.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CarpetaController : ControllerBase
    {
        private readonly I_Carpeta _carpeta;
        public CarpetaController(I_Carpeta carpeta)
        {
            _carpeta = carpeta;
        }

        [HttpPost]
        [Route("Listar")]
        public async Task<IActionResult> Listar([FromBody] string ruta)
        {
            try
            {
                var datos = await _carpeta.Listar(ruta);
                return StatusCode(StatusCodes.Status200OK, new { Message = "Lista de carpetas obtenida exitosamente", Response = datos });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error interno del servidor", Response = ex.Message });
            }
        }

        [HttpPut]
        [Route("Crear")]
        public async Task<IActionResult> Crear([FromBody] string rutaCarpeta)
        {
            try
            {
                var respuesta = await _carpeta.Crear(rutaCarpeta);
                if (!respuesta.Success)
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Message = respuesta.Message });
                return StatusCode(StatusCodes.Status200OK, new { Message = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("Existe")]
        public async Task<IActionResult> Existe([FromBody] string rutaCarpeta)
        {
            try
            {
                var respuesta = await _carpeta.Existe(rutaCarpeta);
                if (!respuesta.Success)
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Message = respuesta.Message });
                return StatusCode(StatusCodes.Status200OK, new { Message = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] DatoCarpeta carpeta)
        {
            try
            {
                string nombreActual = carpeta.nombreActual;
                string nuevoNombre = carpeta.nuevoNombre;
                var respuesta = await _carpeta.Editar(nombreActual, nuevoNombre);
                if (!respuesta.Success)
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Message = respuesta.Message });
                return StatusCode(StatusCodes.Status200OK, new { Message = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }
        [HttpDelete]
        [Route("Eliminar")]
        public async Task<IActionResult> Eliminar([FromBody] string nombreCarpeta)
        {
            try
            {
                var respuesta = await _carpeta.Eliminar(nombreCarpeta);
                if (!respuesta.Success)
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Message = respuesta.Message });
                return StatusCode(StatusCodes.Status200OK, new { Message = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }
    }
}
