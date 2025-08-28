using Api_FiesteDocs.Data;
using Api_FiesteDocs.Entities;
using Api_FiesteDocs.Models;
using Api_FiesteDocs.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        /// Obtiene los datos de todos los usuarios y los envía a través de la API.
        /// </summary>
        /// <returns>Respuesta HTTP con la lista de usuarios en la propiedad "response".</returns>
        [HttpGet]
        [Authorize]
        [Route("Listar")]
        public IActionResult Listar()
        {
            List<Usuario> lista = new List<Usuario>();
            try
            {
                lista = _usuario.Listar();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Ok", response = lista });

            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = lista });
            }

        }

        

        /// <summary>
        /// Busca y devuelve un usuario a partir de su correo.
        /// </summary>
        /// <param name="correo">Correo electrónico del usuario a buscar (se recibe en el body).</param>
        /// <returns>Respuesta HTTP con el usuario encontrado en "response", o un mensaje indicando que no se encontró.</returns>
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

        /// <summary>
        /// Obtiene un usuario por su identificador único (Id).
        /// </summary>
        /// <param name="id_Usuario">Identificador del usuario a buscar (en la ruta).</param>
        /// <returns>Respuesta HTTP con el usuario encontrado en "response", o un mensaje indicando que no se encontró.</returns>
        [HttpPost]
        [Authorize]
        [Route("ObtenerId/{id_Usuario:int}")]
        public IActionResult ObtenerId(int id_Usuario)
        {
            Usuario usuario = new Usuario();
            try
            {
                usuario = _usuario.ObtenerId(id_Usuario);
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

        /// <summary>
        /// Crea un nuevo usuario en la base de datos.
        /// </summary>
        /// <param name="usuario">Objeto Usuario con los datos que se desean crear (recibido en el body).</param>
        /// <returns>Respuesta HTTP con el resultado de la operación en la propiedad "mensaje".</returns>
        [HttpPut]
        [Authorize]
        [Route("Crear")]
        public IActionResult Crear([FromBody] Usuario usuario)
        {
            try
            {
                Request peticion = _usuario.Crear(usuario);
                if (!peticion.Success)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = peticion.Message });
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = peticion.Message });

            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        /// <summary>
        /// Edita los datos de un usuario existente. Si algún campo viene vacío, se conserva el valor actual en la BD.
        /// </summary>
        /// <param name="usuario">Objeto Usuario con los datos actualizados (recibido en el body). Debe contener el Id para identificar el registro a editar.</param>
        /// <returns>Respuesta HTTP con el resultado de la operación en la propiedad "mensaje".</returns>
        [HttpPut]
        [Authorize]
        [Route("Editar")]
        public IActionResult Editar([FromBody] Usuario usuario = null)
        {
            try
            {
                Request peticion = _usuario.Editar(usuario);
                if (!peticion.Success)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = peticion.Message });
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = peticion.Message });

            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        /// <summary>
        /// Elimina un usuario por su identificador.
        /// </summary>
        /// <param name="id_Usuario">Identificador del usuario a eliminar (en la ruta).</param>
        /// <returns>Respuesta HTTP con el resultado de la operación en la propiedad "mensaje".</returns>
        [HttpDelete]
        [Authorize]
        [Route("Eliminar/{id_Usuario:int}")]
        public IActionResult Eliminar(int id_Usuario)
        {

            try
            {
                Request peticion = _usuario.Eliminar(id_Usuario);
                if (!peticion.Success)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = peticion.Message });
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = peticion.Message });

            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

    }
}
