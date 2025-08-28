using Api_FiesteDocs.Entities;
using Api_FiesteDocs.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_FiesteDocs.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class InstrumentoController : ControllerBase
    {
        private readonly I_Instrumento _Instrumento;

        public InstrumentoController(I_Instrumento Instrumento)
        {
            _Instrumento = Instrumento;
        }

        [HttpGet]
        [Route("Listar")]
        public IActionResult Listar()
        {
            try
            {
                List<Instrumento> instrumentos = _Instrumento.Listar();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Ok", response = instrumentos });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { mensaje = "Error al listar instrumentos: " + ex.Message });
            }
        }

        [HttpPost]
        [Route("ListarIdGrupo/{Id_Grupo:int}")]
        public IActionResult ListarIdGrupo(int Id_Grupo)
        {
            try
            {
                List<Instrumento> instrumentos = _Instrumento.ListarIdGrupo(Id_Grupo);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Ok", response = instrumentos });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { mensaje = "Error al listar instrumentos por grupo: " + ex.Message });
            }
        }

        [HttpPost]
        [Route("ListarIdSeccion/{Id_Seccion:int}")]
        public IActionResult ListarIdSeccion(int Id_Seccion)
        {
            try
            {
                List<Instrumento> instrumentos = _Instrumento.ListarIdSeccion(Id_Seccion);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Ok", response = instrumentos });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { mensaje = "Error al listar instrumentos por sección: " + ex.Message });
            }
        }

        [HttpPost]
        [Route("Obtener/{Id_Instrumento:int}")]
        public IActionResult Obtener(int Id_Instrumento)
        {
            try
            {
                Instrumento instrumento = _Instrumento.Obtener(Id_Instrumento);
                if (instrumento == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Instrumento no encontrado" });
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Ok", response = instrumento });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { mensaje = "Error al obtener instrumento: " + ex.Message });
            }
        }

        [HttpPut]
        [Route("Crear")]
        public IActionResult Crear([FromBody] Instrumento instrumento)
        {
            try
            {
                var result = _Instrumento.Crear(instrumento);
                if (!result.Success)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = result.Message, response = result.Success });
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = result.Message, response = result.Success });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { mensaje = "Error al crear instrumento: " + ex.Message });
            }
        }

        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] Instrumento instrumento)
        {
            try
            {
                var result = _Instrumento.Editar(instrumento);
                if (!result.Success)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = result.Message, response = result.Success });
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = result.Message, response = result.Success });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { mensaje = "Error al editar instrumento: " + ex.Message });
            }
        }

        [HttpDelete]
        [Route("Eliminar/{Id_Instrumento:int}")]
        public IActionResult Eliminar(int Id_Instrumento)
        {
            try
            {
                var result = _Instrumento.Eliminar(Id_Instrumento);
                if (!result.Success)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = result.Message, response = result.Success });
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = result.Message, response = result.Success });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { mensaje = "Error al eliminar instrumento: " + ex.Message });
            }
        }
    }
}
