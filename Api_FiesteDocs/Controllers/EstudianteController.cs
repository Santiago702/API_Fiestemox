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
    public class EstudianteController : ControllerBase
    {
        private readonly I_Estudiante _estudiante;
        public EstudianteController(I_Estudiante estudianteService)
        {
            _estudiante = estudianteService;
        }
        /// <summary>
        /// Obtiene la lista de estudiantes y sus usuarios asociados a un grupo específico.
        /// </summary>
        /// <param name="Id_Grupo">Identificador único del grupo.</param>
        /// <returns>Lista de objetos <see cref="InfoEstudiante"/> que representan
        /// la relación entre un estudiante y su usuario dentro del grupo.</returns>
        [HttpGet]
        [Route("Listar")]
        public IActionResult Listar(int Id_Grupo)
        {
            try
            {
                var estudiantes = _estudiante.Listar(Id_Grupo);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Ok", response = estudiantes });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene la información de un estudiante específico junto con su usuario asociado.
        /// </summary>
        /// <param name="Id_Estudiante"> Id del Estudiante</param>
        /// <returns>Objeto con la informacion de usuario y estudiante</returns>
        [HttpPost]
        [Route("ObtenerId/{Id_Estudiante:int}")]
        public IActionResult ObtenerId(int Id_Estudiante)
        {
            try
            {
                var estudiante = _estudiante.ObtenerId(Id_Estudiante);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Ok", response = estudiante });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene la información de un estudiante específico junto con su usuario asociado.
        /// </summary>
        /// <param name="Id_Estudiante"> Id del Usuario</param>
        /// <returns>Objeto con la informacion de usuario y estudiante</returns>
        [HttpPost]
        [Route("ObtenerIdUsuario/{Id_Usuario:int}")]
        public IActionResult ObtenerIdUsuario(int Id_Usuario)
        {
            try
            {
                var estudiante = _estudiante.ObtenerIdUsuario(Id_Usuario);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Ok", response = estudiante });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        /// <summary>
        /// Edita los datos de un estudiante existente. Si algún campo viene vacío, se conserva el valor actual en la BD.
        /// </summary>
        /// <param name="estudiante">Objeto Estudiante con los datos actualizados (recibido en el body). Debe contener el Id para identificar el registro a editar.</param>
        /// <returns>Respuesta HTTP con el resultado de la operación en la propiedad "mensaje".</returns>
        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] Estudiante estudiante)
        {
            try
            {
                var result = _estudiante.Editar(estudiante);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = result.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        /// <summary>
        /// Crea un nuevo estudiante en la base de datos.
        /// </summary>
        /// <param name="usuario">Objeto InfoEstudiante con los datos que se desean crear (recibido en el body).</param>
        /// <returns>Respuesta HTTP con el resultado de la operación en la propiedad "mensaje".</returns>
        [HttpPut]
        [Route("Crear")]
        public IActionResult Crear([FromBody] InfoEstudiante estudiante)
        {
            try
            {
                var result = _estudiante.Crear(estudiante);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = result.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }
        /// <summary>
        /// Elimina un estudiante por su identificador.
        /// </summary>
        /// <param name="id_Estudiante">Identificador del estudiante a eliminar (en la ruta).</param>
        /// <returns>Respuesta HTTP con el resultado de la operación en la propiedad "mensaje".</returns>
        [HttpDelete]
        [Route("Eliminar/{Id_Estudiante:int}")]
        public IActionResult Eliminar(int Id_Estudiante)
        {
            try
            {
                var result = _estudiante.Eliminar(Id_Estudiante);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = result.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }
    }
}
