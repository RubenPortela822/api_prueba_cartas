using ApiNovaAnalyzer.DTOs;
using ApiNovaAnalyzer.Hubs;
using ApiNovaAnalyzer.Models;
using Microsoft.AspNetCore.Server.Kestrel.Core;
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

        public void balotasLanzadas()
        {
            this.enviarBalotaJugada();
        }

        public async Task cerrarSorteo()
        {
            await _hubContext.Clients.All.SendAsync("cerrar-sorteo", "CERRAR");
        }

        public async Task listaGanadores(ListaGanadoresDTO listaGanadores)
        {
            await _hubContext.Clients.All.SendAsync("ganadores", listaGanadores.ganadores);
        }

        public async Task balotaEliminada(int numeroMovimiento)
        {
            await _hubContext.Clients.All.SendAsync("balota-eliminada", numeroMovimiento);
        }

        public async Task<CartonesCliente> getCartonesClientes(string idCliente)
        {
            return await this.consultarCartonesClientes(idCliente);
        }

        public List<int> cartonesZona(string zona)
        {
            return this.consultarCartonesZona(zona);
        }

        public List<string> getZonas()
        {
            return this.consultarZonas();
        }

        public Carton getCartonZona(string cartonId, string zona)
        {
            return  this.consultarCartonZona(cartonId,zona);
        }

        private async Task<CartonesCliente> consultarCartonesClientes(string idCliente)
        {
            List<int> cartones = new List<int>();
            string nombre = "";
            string cedula = idCliente;
            string sala = "";
            string zona = "";

            try
            {
                ConexionBD con_valores = new ConexionBD();

                using (SqlConnection v_con_valores = con_valores.Conexion())
                {

                    string sql1 = "SELECT TOP 1 * FROM bonos_venta where cedula_cliente='" + idCliente + "' order by id_bono_ruletazo desc ";
                    using (SqlCommand command = new SqlCommand(sql1, v_con_valores))
                    {
                        v_con_valores.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                nombre = reader.GetString(1);
                                sala = reader.GetString(4);
                                zona = reader.GetString(2);
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

            try
            {
                ConexionBD con_valores = new ConexionBD();

                using (SqlConnection v_con_valores = con_valores.Conexion())
                {

                    string sql2 = "SELECT numero_bono FROM bonos_venta where cedula_cliente='" + idCliente + "' order by id_bono_ruletazo desc ";
                    Console.WriteLine(sql2);
                    using (SqlCommand command = new SqlCommand(sql2, v_con_valores))
                    {
                        v_con_valores.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cartones.Add(int.Parse(reader.GetString(0)));
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

            return new CartonesCliente
            {
                Nombre = nombre,
                Cedula = cedula,
                Sala = sala,
                Zona = zona,
                Cartones = cartones
            };


        }

        private List<int> consultarBalotasSorteo()
        {
            List<int> balotasJugadas = new List<int>();
            var balotaJugada = 0;

            try
            {
                ConexionBD con_valores = new ConexionBD();

                using (SqlConnection v_con_valores = con_valores.Conexion())
                {

                    string balotas = "SELECT * FROM movimientos_centralizados where estado='V' order by id_movimiento desc ";

                    using (SqlCommand command = new SqlCommand(balotas, v_con_valores))
                    {
                        v_con_valores.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                balotasJugadas.Add(reader.GetInt32(3));
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


            return balotasJugadas;
        }

        private List<int> consultarCartonesZona(string zona)
        {
            List<int> cartones = new List<int>();

            try
            {
                ConexionBD con_valores = new ConexionBD();

                using (SqlConnection v_con_valores = con_valores.Conexion())
                {

                    string sql1 = "SELECT DISTINCT[numero_bono] FROM bonos_venta where nombre_campana='" + zona + "'";
                    using (SqlCommand command = new SqlCommand(sql1, v_con_valores))
                    {
                        v_con_valores.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cartones.Add(int.Parse(reader.GetString(0)));
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

            return cartones;
        }

        private List<string> consultarZonas()
        {
            List<string> zonas = new List<string>();


            try
            {
                ConexionBD con_valores = new ConexionBD();

                using (SqlConnection v_con_valores = con_valores.Conexion())
                {

                    string sql1 = "select nombre_campana from bonos_venta group by nombre_campana";
                    using (SqlCommand command = new SqlCommand(sql1, v_con_valores))
                    {
                        v_con_valores.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                zonas.Add((reader.GetString(0)));
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


            return zonas;
        }
        private Carton consultarCartonZona(string cartonId, string zona)
        {
            Carton carton = new Carton();

            try
            {
                ConexionBD con_valores = new ConexionBD();

                using (SqlConnection v_con_valores = con_valores.Conexion())
                {

                    string sql1 = "SELECT TOP 1 * FROM bonos_venta where numero_bono='" + cartonId + "' AND nombre_campana='" + zona+ "' order by id_bono_ruletazo desc ";
                    using (SqlCommand command = new SqlCommand(sql1, v_con_valores))
                    {
                        v_con_valores.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                carton.id_bono_ruletazo = reader.GetInt32(0);
                                carton.nombre_cliente = reader.GetString(1);
                                carton.nombre_campana = reader.GetString(2);
                                carton.numero_bono = reader.GetString(3);
                                carton.nombre_sala = reader.GetString(4);
                                carton.cedula_cliente = reader.GetString(5);
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

            return carton;
        }

    }
}

