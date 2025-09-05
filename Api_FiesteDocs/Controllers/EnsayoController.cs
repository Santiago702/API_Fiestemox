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
    public class EnsayoController : ControllerBase
    {
        private readonly I_Ensayo _ensayo;
        public EnsayoController(I_Ensayo ensayoService)
        {
            _ensayo = ensayoService;
        }

        [HttpPost]
        [Route("Listar/{Id_Grupo:int}")]
        public async Task<IActionResult> Listar(int Id_Grupo)
        {
            List<Ensayo> ensayos = new List<Ensayo>();
            try
            {
                ensayos = await _ensayo.Listar(Id_Grupo);
                return StatusCode(StatusCodes.Status200OK, new { Message = "Lista de ensayos obtenida exitosamente", Response = ensayos });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error interno del servidor", Response = ex.Message });
            }
        }

        [HttpPost]
        [Route("Obtener/{Id_Ensayo:int}")]
        public async Task<IActionResult> Obtener(int Id_Ensayo)
        {
            Ensayo ensayo = new Ensayo();
            try
            {
                ensayo = await _ensayo.ObtenerId(Id_Ensayo);
                if (ensayo == null)
                    return StatusCode(StatusCodes.Status404NotFound, new { Message = "El ensayo no existe" });
                return StatusCode(StatusCodes.Status200OK, new { Message = "Ensayo obtenido exitosamente", Response = ensayo });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error interno del servidor", Response = ex.Message });
            }
        }

        [HttpPut]
        [Route("Crear")]
        public async Task<IActionResult> Crear([FromBody] Ensayo ensayo)
        {
            Request resultado = new Request();
            try
            {
                resultado = await _ensayo.Crear(ensayo);
                if (!resultado.Success)
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Message = resultado.Message });
                return StatusCode(StatusCodes.Status200OK, new { Message = resultado.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error interno del servidor", Response = ex.Message });
            }
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] Ensayo ensayo)
        {
            Request resultado = new Request();
            try
            {
                resultado = await _ensayo.Editar(ensayo);
                if (!resultado.Success)
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Message = resultado.Message });
                return StatusCode(StatusCodes.Status200OK, new { Message = resultado.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error interno del servidor", Response = ex.Message });
            }
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            Request resultado = new Request();
            try
            {
                resultado = await _ensayo.Eliminar(id);
                if (!resultado.Success)
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Message = resultado.Message });
                return StatusCode(StatusCodes.Status200OK, new { Message = resultado.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error interno del servidor", Response = ex.Message });
            }
        }
    }
}
