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

        public Object consultarUrlTransmision()
        {
            return _sorteo.consultarUrlTransmision();
        }

        public bool actualizarTransmision(int id, string url)
        {            
            return _sorteo.actualizarTransmision(id, url);
        }

        public async Task<CartonesCliente> getCartonesClientes(string idCliente)
        {
            return await _sorteo.getCartonesClientes(idCliente);
        }

        public async Task balotaEliminada(ElimarBalotaDTO request)
        {
            await _sorteo.balotaEliminada(request.numeroMovimiento);
        }

        public List<int> cartonesZona(ListarCartonesZonaDTO request)
        {
            return _sorteo.cartonesZona(request.zona);
        }

        public List<string> getZonas()
        {
            return _sorteo.getZonas();
        }

        public Carton getCartonZona(string cartonId, string zona)
        {
            return _sorteo.getCartonZona(cartonId, zona);

        }

    }
}
