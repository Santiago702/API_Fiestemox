using Api_FiesteDocs.Models;
using Api_FiesteDocs.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Api_FiesteDocs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private readonly I_Autenticar _auth;
        private readonly I_Dropbox _dropbox;
        public AutenticacionController(I_Autenticar auth, I_Dropbox dropbox)
        {
            _auth = auth;
            _dropbox = dropbox;
        }

        [HttpPost]
        [Route("Fiestedocs/Validar")]
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

        [HttpGet]
        [Authorize]
        [Route("Dropbox/Validar")]
        public async Task<IActionResult> RefrescarToken()
        {
            try
            {
                var nuevoToken = await _dropbox.Token();

                return StatusCode(StatusCodes.Status200OK, new
                {
                    Token = nuevoToken
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Token = ex.Message
                });
            }
        }
    }
}
