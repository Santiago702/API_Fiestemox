using Api_FiesteDocs.Entities;
using Api_FiesteDocs.Models;
using Api_FiesteDocs.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_FiesteDocs.Controllers
{
    [Route("api/[controller]")]
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
        public IActionResult Listar()
        {
            try
            {
                List<Trabajo> trabajos = new List<Trabajo>();
                trabajos = _trabajo.Listar();
                return StatusCode(StatusCodes.Status200OK, new { Message = "Lista de ensayos obtenida exitosamente", Response = trabajos });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Error interno del servidor", Response = ex.Message });
            }
        }


        [HttpGet]
        [Route("ListarInfo")]
        public IActionResult ListarInfo()
        {
            try
            {
                List<InfoTrabajo> trabajos = new List<InfoTrabajo>();
                trabajos = _trabajo.ListarInfo();
                return StatusCode(StatusCodes.Status200OK, new { Message = "Lista de ensayos obtenida exitosamente", Response = trabajos });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Error interno del servidor", Response = ex.Message });
            }
        }

        [HttpPost]
        [Route("ListarIdEnsayo/{Id_Ensayo:int}")]
        public IActionResult ListarIdEnsayo(int Id_Ensayo)
        {
            try
            {
                List<InfoTrabajo> trabajo = new List<InfoTrabajo>();
                trabajo = _trabajo.ListarIdEnsayo(Id_Ensayo);
                return StatusCode(StatusCodes.Status200OK, new { Message = "Trabajos obtenido exitosamente", Response = trabajo });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Error interno del servidor", Response = ex.Message });
            }
        }


        [HttpPost]
        [Route("Obtener/{Id_Trabajo:int}")]
        public IActionResult Obtener(int Id_Trabajo)
        {
            try
            {
                Trabajo trabajo = new Trabajo();
                trabajo = _trabajo.ObtenerId(Id_Trabajo);
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
        public IActionResult Crear([FromBody] Trabajo trabajo)
        {
            try
            {
                Request resultado = new Request();
                resultado = _trabajo.Crear(trabajo);
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
        public IActionResult Editar([FromBody] Trabajo trabajo)
        {
            try
            {
                Request resultado = new Request();
                resultado = _trabajo.Editar(trabajo);
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
        public IActionResult Eliminar(int Id_Trabajo)
        {
            try
            {
                Request resultado = new Request();
                resultado = _trabajo.Eliminar(Id_Trabajo);
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
