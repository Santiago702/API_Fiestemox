using Api_FiesteDocs.Models;
using Api_FiesteDocs.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_FiesteDocs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private readonly I_Autenticar _auth;
        public AutenticacionController(I_Autenticar auth)
        {
            _auth = auth;
        }

        [HttpPost]
        [Route("Validar")]
        public IActionResult Validar([FromBody] Autenticacion autenticacion)
        {
            try
            {
                string Token = _auth.Autenticar(autenticacion);
                if (Token == null)
                    return StatusCode(StatusCodes.Status401Unauthorized, new { Token = "" });
                return StatusCode(StatusCodes.Status200OK, new { Token = Token });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Token = ex.Message });
            }
            
        }
    }
}
