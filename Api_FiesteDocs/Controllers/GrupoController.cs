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
    public class GrupoController : ControllerBase
    {
        private I_Grupo _grupo;
        public GrupoController(I_Grupo grupo)
        {
            _grupo = grupo;
        }

        [HttpGet]
        [Route("Listar")]
        public IActionResult Listar()
        {
            try
            {
                List<Grupo> grupos = _grupo.Listar();
                return StatusCode(StatusCodes.Status200OK, new {mensaje = "Listados", response = grupos});
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new {mensaje = error.Message});
            }
        }

        [HttpPost]
        [Route("ObtenerIdDirector/{Id_Director:int}")]
        public IActionResult ObtenerIdDirector(int Id_Director)
        {

            try
            {
                List<Grupo> grupos = _grupo.ObtenerIdDirector(Id_Director);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Listados", response = grupos});
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        [Route("ObtenerIdEstudiante/{Id_Estudiante:int}")]
        public IActionResult ObtenerIdEstudiante(int Id_Estudiante)
        {

            try
            {
                List<Grupo> grupos = _grupo.ObtenerIdEstudiante(Id_Estudiante);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Listados", response = grupos });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        [Route("Obtener/{Id_Grupo:int}")]
        public IActionResult Obtener(int Id_Grupo)
        {
            try { 
                Grupo grupo = _grupo.ObtenerIdGrupo(Id_Grupo);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = grupo});
            
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] Grupo grupo)
        {
            Request resultado = new Request();
            try
            {
                resultado = _grupo.Editar(grupo);
                if (!resultado.Success)
                    return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = resultado.Message, response = resultado.Success });
                
                return StatusCode(StatusCodes.Status200OK, new {mensaje = resultado.Message, response = resultado.Success});
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("Crear")]
        public IActionResult Crear([FromBody] Grupo grupo)
        {
            Request resultado = new Request();
            try
            {
                resultado = _grupo.Crear(grupo);
                if (!resultado.Success)
                    return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = resultado.Message, response = resultado.Success });

                return StatusCode(StatusCodes.Status200OK, new { mensaje = resultado.Message, response = resultado.Success });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("Eliminar/{Id_Grupo:int}")]
        public IActionResult Eliminar(int Id_Grupo)
        {
            Request resultado = new Request();
            try
            {
                resultado = _grupo.Eliminar(Id_Grupo);
                if (!resultado.Success)
                    return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = resultado.Message, response = resultado.Success });

                return StatusCode(StatusCodes.Status200OK, new { mensaje = resultado.Message, response = resultado.Success });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }
    }
}
