using Api_FiesteDocs.Data;
using Api_FiesteDocs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_FiesteDocs.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    
    public class HomeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Listar")]
        public IActionResult Listar()
        {
            List<Usuario>Usuarios = new List<Usuario>();
            try
            {
                Usuarios = _context.Usuarios.ToList();
                return StatusCode(StatusCodes.Status200OK, new {mensaje = "Ok", response = Usuarios });

            }catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = Usuarios });
            }

        }

    }
}
