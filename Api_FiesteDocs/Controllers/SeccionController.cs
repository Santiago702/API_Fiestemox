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
    public class SeccionController : ControllerBase
    {
        private I_Seccion _seccion;

        /// <summary>
        /// Constructor del controlador SeccionController.
        /// </summary>
        /// <param name="seccion">Servicio que gestiona las operaciones de Sección.</param>
        public SeccionController(I_Seccion seccion)
        {
            _seccion = seccion;
        }

        /// <summary>
        /// Obtiene todas las secciones registradas en el sistema.
        /// </summary>
        /// <returns>Lista de secciones.</returns>
        [HttpGet]
        [Route("Listar")]
        public async Task<IActionResult> Listar()
        {
            try
            {
                List<Seccion> secciones = await _seccion.Listar();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Ok", response = secciones });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error", response = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene todas las secciones asociadas a un grupo específico.
        /// </summary>
        /// <param name="Id_Grupo">ID del grupo.</param>
        /// <returns>Lista de secciones correspondientes al grupo.</returns>
        [HttpPost]
        [Route("ListarIdGrupo/{Id_Grupo:int}")]
        public async Task<IActionResult> ListarIdGrupo(int Id_Grupo)
        {
            try
            {
                List<Seccion> secciones = await _seccion.ListarIdGrupo(Id_Grupo);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Ok", response = secciones });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error", response = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene una sección específica a partir de su ID.
        /// </summary>
        /// <param name="Id_Seccion">ID de la sección.</param>
        /// <returns>Objeto Sección.</returns>
        [HttpPost]
        [Route("Obtener/{Id_Seccion:int}")]
        public async Task<IActionResult> Obtener(int Id_Seccion)
        {
            try
            {
                Seccion seccion = await _seccion.Obtener(Id_Seccion);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Ok", response = seccion });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error", response = ex.Message });
            }
        }

        /// <summary>
        /// Crea una nueva sección en el sistema.
        /// </summary>
        /// <param name="seccion">Objeto Sección a crear.</param>
        /// <returns>Resultado de la operación.</returns>
        [HttpPut]
        [Route("Crear")]
        public async Task<IActionResult> Crear([FromBody] Seccion seccion)
        {
            try
            {
                var result = await _seccion.Crear(seccion);
                if (!result.Success)
                    return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = result.Message });
                return StatusCode(StatusCodes.Status200OK, new { mensaje = result.Message, response = result.Success });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error", response = ex.Message });
            }
        }

        /// <summary>
        /// Edita la información de una sección existente.
        /// </summary>
        /// <param name="seccion">Objeto Sección con los datos a actualizar.</param>
        /// <returns>Resultado de la operación.</returns>
        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] Seccion seccion)
        {
            try
            {
                var result = await _seccion.Editar(seccion);
                if (!result.Success)
                    return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = result.Message });
                return StatusCode(StatusCodes.Status200OK, new { mensaje = result.Message, response = result.Success });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error", response = ex.Message });
            }
        }

        /// <summary>
        /// Elimina una sección específica del sistema.
        /// </summary>
        /// <param name="Id_Seccion">ID de la sección a eliminar.</param>
        /// <returns>Resultado de la operación.</returns>
        [HttpDelete]
        [Route("Eliminar/{Id_Seccion:int}")]
        public async Task<IActionResult> Eliminar(int Id_Seccion)
        {
            try
            {
                var result = await _seccion.Eliminar(Id_Seccion);
                if (!result.Success)
                    return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = result.Message });
                return StatusCode(StatusCodes.Status200OK, new { mensaje = result.Message, response = result.Success });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error", response = ex.Message });
            }
        }
    }
}
