namespace ApiNovaAnalyzer.Models
{
    public class CartonesCliente
    {
        public string Nombre { get; set; } = string.Empty;
        public string Cedula { get; set; } = string.Empty;
        public string Sala { get; set; } = string.Empty;
        public string Zona { get; set; } = string.Empty;
        public List<int> Cartones { get; set; } = new();
    }
}
