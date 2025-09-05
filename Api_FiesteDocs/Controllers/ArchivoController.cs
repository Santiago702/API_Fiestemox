using Api_FiesteDocs.Entities;
using Api_FiesteDocs.Models;
using Api_FiesteDocs.Services.Interfaces;
using Dropbox.Api.Files;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_FiesteDocs.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ArchivoController : ControllerBase
    {
        private I_Archivo _archivo;

        public ArchivoController(I_Archivo archivo)
        {
            _archivo = archivo;
        }

        [HttpPost]
        [Route("Listar")]
        public async Task<IActionResult> Listar([FromBody] string ruta)
        {
            try
            {
                List<MetaDatos> datos;
                datos = await _archivo.Listar(ruta);
                return StatusCode(StatusCodes.Status200OK, new { Message = "Lista de archivos obtenida exitosamente", Response = datos });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error interno del servidor", Response = ex.Message });
            }
        }

        [HttpPut]
        [Route("Crear")]
        public async Task<IActionResult> Crear([FromBody] Partitura Partitura)
        {
            try
            {
                Request respuesta = await _archivo.Crear(Partitura);
                if(!respuesta.Success)
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Message = respuesta.Message });
                return StatusCode(StatusCodes.Status200OK, new { Message = respuesta });
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("EliminarNombre")]
        public async Task<IActionResult> EliminarNombre([FromBody] Partitura Partitura)
        {
            try
            {
                Request respuesta = await _archivo.EliminarNombre(Partitura);
                if(!respuesta.Success)
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Message = respuesta.Message });

                return StatusCode(StatusCodes.Status200OK, new { Message = respuesta.Message});
            }
            catch (ArgumentException argEx)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { Message = argEx.Message });
            }
            catch (InvalidOperationException invOpEx)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = invOpEx.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = $"Error interno del servidor: {ex.Message}"});
            }
        }

        [HttpDelete]
        [Route("EliminarRuta")]
        public async Task<IActionResult> EliminarRuta([FromBody] string Ruta)
        {
            try
            {
                Request respuesta = await _archivo.EliminarRuta(Ruta);
                if(!respuesta.Success)
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Message = respuesta.Message });
                return StatusCode(StatusCodes.Status200OK, new { Message = respuesta.Message});
            }
            catch (ArgumentException argEx)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { Message = argEx.Message });
            }
            catch (InvalidOperationException invOpEx)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = invOpEx.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = $"Error interno del servidor {ex.Message}"});
            }
        }


    }
}
