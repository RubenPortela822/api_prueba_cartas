using ApiNovaAnalyzer.Conexion;
using ApiNovaAnalyzer.DTOs;
using ApiNovaAnalyzer.Models;

namespace ApiNovaAnalyzer.Services
{
    public class SorteoService
    {
        private readonly Sorteo _sorteo;

        public SorteoService(Sorteo sorteo)
        {
            _sorteo = sorteo;
        }

        public async Task enviarBalotaJugada()
        {                        
            await _sorteo.enviarBalotaJugada();
        }

        public async Task balotasLanzadas()
        {
            _sorteo.balotasLanzadas();
        }

        public async Task cerrarSorteo()
        {
            await _sorteo.cerrarSorteo();
        }

        public async Task listaGanadores(ListaGanadoresDTO listaGanadores)
        {
            await _sorteo.listaGanadores(listaGanadores);
        }

        public async Task<CartonesCliente> getCartonesClientes(string idCliente) 
        {
            return await _sorteo.getCartonesClientes(idCliente);
        }

        public async Task balotaEliminada(ElimarBalotaDTO request)
        {            
            //Console.WriteLine($"Balota: {request.numeroMoviento}");
            await _sorteo.balotaEliminada(request.numeroMovimiento);
        }
    }
}
