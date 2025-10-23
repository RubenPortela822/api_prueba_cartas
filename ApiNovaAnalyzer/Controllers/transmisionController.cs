using ApiNovaAnalyzer.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiNovaAnalyzer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class transmisionController : ControllerBase
    {
        private readonly SorteoService _sorteoService;
        public transmisionController(SorteoService sorteoService)
        {
            _sorteoService = sorteoService;
        }

        [HttpGet("ultima")]
        public IActionResult GetUltimaTransmision()
        {
            var transmision = _sorteoService.consultarUrlTransmision();

            if (transmision == null)
                return NotFound("No hay transmisiones aún");

            return Ok(transmision);
        }

        [HttpPut("actualizar/{id}")]
        public IActionResult ActualizarTransmision(int id, [FromBody] string url)
        {
            bool actualizado = _sorteoService.actualizarTransmision(id, url);

            if (!actualizado)
                return NotFound("Error al actualizar");

            return Ok("Transmisión actualizada");
        }

    }
}
