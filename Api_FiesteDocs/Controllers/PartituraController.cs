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
    public class PartituraController : ControllerBase
    {
        private readonly I_Partitura _partitura;
        public PartituraController(I_Partitura partituraService)
        {
            _partitura = partituraService;
        }

        [HttpGet]
        [Route("Listar")]
        public IActionResult Listar()
        {
            try
            {
                var partituras = _partitura.Listar();
                return StatusCode(StatusCodes.Status200OK, new { Message = "Lista de partituras obtenida exitosamente", Response = partituras });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error interno del servidor", Response = ex.Message });
            }
        }

        [HttpPost]
        [Route("ListarIdSeccion/{Id_Seccion:int}")]
        public IActionResult ListarIdSeccion(int Id_Seccion)
        {
            try
            {
                var partituras = _partitura.ListarIdSeccion(Id_Seccion);
                return StatusCode(StatusCodes.Status200OK, new { Message = "Lista de partituras por sección obtenida exitosamente", Response = partituras });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error interno del servidor", Response = ex.Message });
            }
        }

        [HttpPost]
        [Route("ListarIdGrupo/{Id_Grupo:int}")]
        public IActionResult ListarIdGrupo(int Id_Grupo)
        {
            try
            {
                var partituras = _partitura.ListarIdGrupo(Id_Grupo);
                return StatusCode(StatusCodes.Status200OK, new { Message = "Lista de partituras por grupo obtenida exitosamente", Response = partituras });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error interno del servidor", Response = ex.Message });
            }
        }

        [HttpPost]
        [Route("ListarInfo/{Id_Grupo:int}")]
        public IActionResult ListarInfo(int Id_Grupo)
        {
            try
            {
                var partituras = _partitura.ListarInfo(Id_Grupo);
                return StatusCode(StatusCodes.Status200OK, new { Message = "Lista de info de partituras obtenida exitosamente", Response = partituras });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error interno del servidor", Response = ex.Message });
            }
        }

        [HttpPost]
        [Route("Obtener/{Id_Partitura:int}")]
        public IActionResult Obtener(int Id_Partitura)
        {
            try
            {
                var partitura = _partitura.Obtener(Id_Partitura);
                if (partitura == null)
                    return StatusCode(StatusCodes.Status404NotFound, new { Message = "La partitura no existe" });

                return StatusCode(StatusCodes.Status200OK, new { Message = "Partitura obtenida exitosamente", Response = partitura });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error interno del servidor", Response = ex.Message });
            }
        }

        [HttpPost]
        [Route("Buscar")]
        public IActionResult Buscar([FromBody]string nombre)
        {
            try
            {
                var partituras = _partitura.Buscar(nombre);
                return StatusCode(StatusCodes.Status200OK, new { Message = "Búsqueda realizada exitosamente", Response = partituras });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error interno del servidor", Response = ex.Message });
            }
        }

        [HttpPut]
        [Route("Crear")]
        public IActionResult Crear([FromBody] Partitura partitura)
        {
            try
            {
                var resultado = _partitura.Crear(partitura);
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
        public IActionResult Editar([FromBody] Partitura partitura)
        {
            try
            {
                var resultado = _partitura.Editar(partitura);
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
        public IActionResult Eliminar(int id)
        {
            try
            {
                var resultado = _partitura.Eliminar(id);
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
