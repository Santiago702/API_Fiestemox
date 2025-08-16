using Api_FiesteDocs.Data;
using Api_FiesteDocs.Models;
using Api_FiesteDocs.Entities;
using Api_FiesteDocs.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Api_FiesteDocs.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    
    public class UsuarioController : ControllerBase
    {
        private I_Usuario _usuario;

        public UsuarioController(I_Usuario usuario)
        {
            _usuario = usuario;
        }

        /// <summary>
        /// Obtiene los datos de todos los usuarios y los envía a través de la API 
        /// </summary>
        /// <returns> Lista de Usuarios</returns>
        [HttpGet]
        [Route("Listar")]
        public IActionResult Listar()
        {
            List<Usuario> lista = new List<Usuario>();
            try
            {
                lista = _usuario.Listar();
                return StatusCode(StatusCodes.Status200OK, new {mensaje = "Ok", response = lista });

            }catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = lista });
            }

        }

        /// <summary>
        /// Obtiene los datos de los estudiantes de un grupo o clase específico
        /// </summary>
        /// <param name="id_Grupo">Id del grupo  o clase del que se desea obtener los estudiantes</param>
        /// <returns>Lista de Usuarios de rol Estudiante en el grupo específicado</returns>
        [HttpGet]
        [Route("Estudiantes/{id_Grupo:int}")]
        public IActionResult Estudiantes(int id_Grupo)
        {
            List<Usuario> lista = new List<Usuario>();
            try
            {
                lista = _usuario.ListarEstudiantes(id_Grupo);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Ok", response = lista });

            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = lista });
            }
        }

        [HttpPost]
        [Route("ObtenerCorreo")]
        public IActionResult ObtenerCorreo([FromBody] string correo)
        {
            Usuario usuario = new Usuario();
            try
            {
                usuario = _usuario.ObtenerCorreo(correo);
                if (usuario != null)
                {
                    return StatusCode(StatusCodes.Status200OK, new { mensaje = "Ok", response = usuario });
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "No se encontró usuario asociado", response = usuario });

            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = usuario });
            }
        }

        [HttpPut]
        [Route("Crear")]
        public IActionResult Crear([FromBody] Usuario usuario)
        {
            try
            {
                Request peticion = _usuario.Crear(usuario);
                if (!peticion.Success)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = peticion.Message});
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = peticion.Message});

            } catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

    }
}
