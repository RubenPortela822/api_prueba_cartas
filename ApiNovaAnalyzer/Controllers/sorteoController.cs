using ApiNovaAnalyzer.DTOs;
using ApiNovaAnalyzer.Models;
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
        public async void enviarBalotaJugada() {           
            await _sorteoService.enviarBalotaJugada();           
        }

        [HttpGet, Route("balotas-jugadas")]
        public async void balotasLanzadas()
        {
            await _sorteoService.balotasLanzadas();
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

        [HttpGet("cartones-clientes/{idCliente}")]
        public async Task<CartonesCliente> getCartonesClientes(string idCliente)
        {
            return await _sorteoService.getCartonesClientes(idCliente);            
        }

        [HttpPost, Route("lista-ganadores")]
        public async void listaGanadores([FromBody] ListaGanadoresDTO listaGanadores)
        {
            await _sorteoService.listaGanadores(listaGanadores);            
        }

    }
}
