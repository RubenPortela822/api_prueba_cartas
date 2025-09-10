using ApiNovaAnalyzer.Conexion;
using ApiNovaAnalyzer.DTOs;

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

        public async Task cerrarSorteo()
        {
            await _sorteo.cerrarSorteo();
        }

        public async Task balotaEliminada(ElimarBalotaDTO request)
        {            
            //Console.WriteLine($"Balota: {request.numeroMoviento}");
            await _sorteo.balotaEliminada(request.numeroMovimiento);
        }
    }
}
