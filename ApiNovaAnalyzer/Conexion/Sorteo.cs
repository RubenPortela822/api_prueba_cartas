using ApiNovaAnalyzer.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.SqlClient;

namespace ApiNovaAnalyzer.Conexion
{
    public class Sorteo
    {
        private readonly IHubContext<SorteoHub> _hubContext;

        public Sorteo(IHubContext<SorteoHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task enviarBalotaJugada()
        {
            var balotas = this.consultarBalotasSorteo();
            await _hubContext.Clients.All.SendAsync("balotas-jugadas", balotas);
        }

        public async Task cerrarSorteo()
        {            
            await _hubContext.Clients.All.SendAsync("cerrar-sorteo","CERRAR");
        }

        public async Task balotaEliminada(int numeroMovimiento)
        {
            await _hubContext.Clients.All.SendAsync("balota-eliminada", numeroMovimiento);
        }


        private int consultarBalotasSorteo() 
        {

            var balotaJugada = 0;

            try
            {
                ConexionBD con_valores = new ConexionBD();

                using (SqlConnection v_con_valores = con_valores.Conexion())
                {

                    string balotas = "SELECT TOP 1 * FROM movimientos_centralizados where estado='V' order by id_movimiento desc ";
                    
                    using (SqlCommand command = new SqlCommand(balotas, v_con_valores))
                    {
                        v_con_valores.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                balotaJugada = reader.GetInt32(3);
                            }
                        }
                    }

                    v_con_valores.Dispose();
                }


            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }


            return balotaJugada;
        }
    }
}
