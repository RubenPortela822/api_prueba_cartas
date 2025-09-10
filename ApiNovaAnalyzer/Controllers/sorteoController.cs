using ApiNovaAnalyzer.DTOs;
using ApiNovaAnalyzer.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiNovaAnalyzer.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class sorteoController : ControllerBase
    {
        private readonly SorteoService _sorteoService;
        public sorteoController(SorteoService sorteoService)
        {
            _sorteoService = sorteoService;
        }

        [HttpGet, Route("enviar-balota-jugada")]
        public async Task<IActionResult> enviarBalotaJugada() {           
            await _sorteoService.enviarBalotaJugada();
            return Ok(new { status = "balota enviada" });
        }


        [HttpGet, Route("cerrar-sorteo")]
        public async Task<IActionResult> cerrarSorteo()
        {
            await _sorteoService.cerrarSorteo();
            return Ok(new { status = "sorteo cerrado" });
        }

        [HttpPost, Route("balota-eliminada")]
        public async Task<IActionResult> balotaEliminada([FromBody] ElimarBalotaDTO eliminarBalotaDto)
        {
            await _sorteoService.balotaEliminada(eliminarBalotaDto);            
            return Ok(new { status = "balota eliminada" });
        }

    }
}
