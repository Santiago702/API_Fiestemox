using Api_FiesteDocs.Entities;
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
    public class TrabajoController : ControllerBase
    {
        private readonly I_Trabajo _trabajo;
        public TrabajoController(I_Trabajo trabajo)
        {
            _trabajo = trabajo;
        }

        [HttpGet]
        [Route("Listar")]
        public async Task<IActionResult> Listar()
        {
            try
            {
                List<Trabajo> trabajos = new List<Trabajo>();
                trabajos = await _trabajo.Listar();
                return StatusCode(StatusCodes.Status200OK, new { Message = "Lista de ensayos obtenida exitosamente", Response = trabajos });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Error interno del servidor", Response = ex.Message });
            }
        }


        [HttpGet]
        [Route("ListarInfo")]
        public async Task<IActionResult> ListarInfo()
        {
            try
            {
                List<InfoTrabajo> trabajos = new List<InfoTrabajo>();
                trabajos = await _trabajo.ListarInfo();
                return StatusCode(StatusCodes.Status200OK, new { Message = "Lista de ensayos obtenida exitosamente", Response = trabajos });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Error interno del servidor", Response = ex.Message });
            }
        }

        [HttpPost]
        [Route("ListarIdEnsayo/{Id_Ensayo:int}")]
        public async Task<IActionResult> ListarIdEnsayo(int Id_Ensayo)
        {
            try
            {
                List<InfoTrabajo> trabajo = new List<InfoTrabajo>();
                trabajo = await _trabajo.ListarIdEnsayo(Id_Ensayo);
                return StatusCode(StatusCodes.Status200OK, new { Message = "Trabajos obtenido exitosamente", Response = trabajo });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Error interno del servidor", Response = ex.Message });
            }
        }


        [HttpPost]
        [Route("Obtener/{Id_Trabajo:int}")]
        public async Task<IActionResult> Obtener(int Id_Trabajo)
        {
            try
            {
                Trabajo trabajo = new Trabajo();
                trabajo = await _trabajo.ObtenerId(Id_Trabajo);
                if (trabajo == null)
                    return StatusCode(StatusCodes.Status404NotFound, new { Message = "El trabajo no existe" });
                return StatusCode(StatusCodes.Status200OK, new { Message = "Trabajo obtenido exitosamente", Response = trabajo });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Error interno del servidor", Response = ex.Message });
            }
        }

        [HttpPut]
        [Route("Crear")]
        public async Task<IActionResult> Crear([FromBody] Trabajo trabajo)
        {
            try
            {
                Request resultado = new Request();
                resultado = await _trabajo.Crear(trabajo);
                if (!resultado.Success)
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Message = resultado.Message });
                return StatusCode(StatusCodes.Status200OK, new { Message = resultado.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Error interno del servidor", Response = ex.Message });
            }
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] Trabajo trabajo)
        {
            try
            {
                Request resultado = new Request();
                resultado = await _trabajo.Editar(trabajo);
                if (!resultado.Success)
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Message = resultado.Message });
                return StatusCode(StatusCodes.Status200OK, new { Message = resultado.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Error interno del servidor", Response = ex.Message });
            }
        }

        [HttpDelete]
        [Route("Eliminar/{Id_Trabajo:int}")]
        public async Task<IActionResult> Eliminar(int Id_Trabajo)
        {
            try
            {
                Request resultado = new Request();
                resultado = await _trabajo.Eliminar(Id_Trabajo);
                if (!resultado.Success)
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Message = resultado.Message });
                return StatusCode(StatusCodes.Status200OK, new { Message = resultado.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Error interno del servidor", Response = ex.Message });
            }
        }
    }
}
