using Api_FiesteDocs.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_FiesteDocs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarpetaController : ControllerBase
    {
        private readonly I_Carpeta _carpeta;
        public CarpetaController(I_Carpeta carpeta)
        {
            _carpeta = carpeta;
        }

    }
}
