using ApiNovaAnalyzer.Conexion;
using ApiNovaAnalyzer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Net;

namespace ApiNovaAnalyzer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class salasController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<salasController> _logger;

        public salasController(ILogger<salasController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {

            try
            {


                ConexionBD con_valores = new ConexionBD();

                using (SqlConnection v_con_valores = con_valores.Conexion())
                {
                    v_con_valores.Open();

                    string queryUpdate = "update MaestraSalas set ensala = 83, enenvio = 83 where IdUnidadOrganizacional = 26";

                    using (SqlCommand command = new SqlCommand(queryUpdate, v_con_valores)) command.ExecuteNonQuery();

                    v_con_valores.Dispose();

                }


            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }


            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }





        [HttpGet, Route("ObtenerSalas")]
        public List<salas> ObtenerSalas()
        {

            salas salasDatos = new salas();

            List<salas> listaSalas = new List<salas>();


            try
            {


                ConexionBD con_valores = new ConexionBD();

                using (SqlConnection v_con_valores = con_valores.Conexion())
                {
                 
                    string salas = "select IdUnidadOrganizacional,NombreUnidadOrg,IdMunicipio,IdGrupo,id_adicional,ensala,enenvio FROM MaestraSalas where id_adicional >= 0 order by IdUnidadOrganizacional ASC";


                    using (SqlCommand command = new SqlCommand(salas, v_con_valores))
                    {
                        v_con_valores.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                salasDatos = new salas();

                                salasDatos.IdUnidadOrganizacional = reader.GetInt32(0);
                                salasDatos.NombreUnidadOrg = reader.IsDBNull(1) ? "NA" : reader.GetString(1);
                                salasDatos.IdMunicipio = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                                salasDatos.IdGrupo = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);
                                salasDatos.id_adicional = reader.IsDBNull(4) ? 0 : reader.GetInt32(4);
                                salasDatos.ensala = reader.IsDBNull(5) ? 0 : reader.GetInt32(5);
                                salasDatos.enenvio = reader.IsDBNull(6) ? 0 : reader.GetInt32(6);

                                listaSalas.Add(salasDatos);

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


                return listaSalas;
        }






        [HttpGet, Route("ObtenerResultadosSalas")]
        public List<salas> ObtenerResultadosSalas()
        {

            salas salasDatos = new salas();

            List<salas> listaSalas = new List<salas>();


            try
            {
                ConexionBD con_valores = new ConexionBD();

                using (SqlConnection v_con_valores = con_valores.Conexion())
                {

                    string salas = "select IdUnidadOrganizacional,NombreUnidadOrg,ensala,enenvio FROM MaestraSalas where id_adicional >= 0 order by IdUnidadOrganizacional ASC";


                    using (SqlCommand command = new SqlCommand(salas, v_con_valores))
                    {
                        v_con_valores.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                salasDatos = new salas();

                                salasDatos.IdUnidadOrganizacional = reader.GetInt32(0);
                                salasDatos.NombreUnidadOrg = reader.IsDBNull(1) ? "NA" : reader.GetString(1);
                                salasDatos.ensala = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                                salasDatos.enenvio = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);

                                listaSalas.Add(salasDatos);

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


            return listaSalas;
        }




        /*[HttpGet, Route("UpdateSalaData")]
        public int UpdateSalaData(int unidad_sala,int data)
        {

            var valor = 1;

            salas obrAdicionales = new salas();

            try
            {

                ConexionBD con_valores = new ConexionBD();

                using (SqlConnection v_con_valores = con_valores.Conexion())
                {
                    v_con_valores.Open();

                    string queryUpdate = "update MaestraSalas set id_adicional = "+data+" where IdUnidadOrganizacional = "+unidad_sala+"";

                    using (SqlCommand command = new SqlCommand(queryUpdate, v_con_valores)) command.ExecuteNonQuery();

                    v_con_valores.Dispose();

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }


            return valor;
         }*/




        [HttpGet, Route("ObtenerDiferenciasSalas")]
        public List<salas> ObtenerDiferenciasSalas()
        {

            salas salasDatos = new salas();

            List<salas> listaSalas = new List<salas>();


            try
            {
                ConexionBD con_valores = new ConexionBD();

                using (SqlConnection v_con_valores = con_valores.Conexion())
                {

                    string salas = "select IdUnidadOrganizacional,NombreUnidadOrg,ensala,enenvio FROM MaestraSalas where id_adicional >= 0 order by IdUnidadOrganizacional ASC";


                    using (SqlCommand command = new SqlCommand(salas, v_con_valores))
                    {
                        v_con_valores.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                salasDatos = new salas();

                                salasDatos.IdUnidadOrganizacional = reader.GetInt32(0);
                                salasDatos.NombreUnidadOrg = reader.IsDBNull(1) ? "NA" : reader.GetString(1);
                                int alfa = salasDatos.ensala = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                                int beta = salasDatos.enenvio = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);
                                salasDatos.diferencias = alfa - beta;

                                if (salasDatos.diferencias > 0 ) 
                                {
                                    listaSalas.Add(salasDatos);
                                }
                                

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


            return listaSalas;
        }




        [HttpGet, Route("datosSala")]
        public List<salas> datosSala(int IdUnidadOrganizacional)
        {

            salas salasDatos = new salas();

            List<salas> listaSalas = new List<salas>();


            try
            {
                ConexionBD con_valores = new ConexionBD();

                using (SqlConnection v_con_valores = con_valores.Conexion())
                {

                 
                    string salaDatos = "select IdUnidadOrganizacional, NombreUnidadOrg, ensala, enenvio, id_adicional FROM MaestraSalas where IdUnidadOrganizacional = " + IdUnidadOrganizacional + "";

                    using (SqlCommand command = new SqlCommand(salaDatos, v_con_valores))
                    {
                        v_con_valores.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                salasDatos = new salas();

                                salasDatos.IdUnidadOrganizacional = reader.GetInt32(0);
                                salasDatos.NombreUnidadOrg = reader.IsDBNull(1) ? "NA" : reader.GetString(1);
                                int alfa = salasDatos.ensala = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                                int beta = salasDatos.enenvio = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);
                                salasDatos.id_adicional = reader.IsDBNull(4) ? 0 : reader.GetInt32(4);

                                salasDatos.diferencias = alfa - beta;

                                if (salasDatos.diferencias > 0)
                                {
                                    listaSalas.Add(salasDatos);
                                }

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


            return listaSalas;
        }





        [HttpGet, Route("UpdateSalaData")]
        public List<salas> UpdateSalaData(String unidad_sala, int data)
        {

            var valor = 1;

            List<salas> listaSalas = new List<salas>();

            int x = Int32.Parse(unidad_sala);


            try
            {

                ConexionBD con_valores = new ConexionBD();

                using (SqlConnection v_con_valores = con_valores.Conexion())
                {
                    v_con_valores.Open();

                    string queryUpdate = "update MaestraSalas set id_adicional = " + data + " where IdUnidadOrganizacional = " + x + "";

                    using (SqlCommand command = new SqlCommand(queryUpdate, v_con_valores)) command.ExecuteNonQuery();

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
                    v_con_valores.Open();

                    //string queryUpdate = "update MaestraSalas set id_adicional = " + data + " where IdUnidadOrganizacional = " + x + "";

                    string QueryInsert = "INSERT INTO HistoricoRev (IdUnidadOrganizacional, id_adicional, Fecha) VALUES ("+x+" ,"+data+ ", GETDATE())";

                    using (SqlCommand command = new SqlCommand(QueryInsert, v_con_valores)) command.ExecuteNonQuery();

                    v_con_valores.Dispose();

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }


            salas salasDatos = new salas();


            try
            {
                ConexionBD con_valores = new ConexionBD();

                using (SqlConnection v_con_valores = con_valores.Conexion())
                {


                    string salaDatos = "select IdUnidadOrganizacional, NombreUnidadOrg, ensala, enenvio, id_adicional FROM MaestraSalas where IdUnidadOrganizacional = " + x + "";

                    using (SqlCommand command = new SqlCommand(salaDatos, v_con_valores))
                    {
                        v_con_valores.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                salasDatos = new salas();

                                salasDatos.IdUnidadOrganizacional = reader.GetInt32(0);
                                salasDatos.NombreUnidadOrg = reader.IsDBNull(1) ? "NA" : reader.GetString(1);
                                int alfa = salasDatos.ensala = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                                int beta = salasDatos.enenvio = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);
                                salasDatos.id_adicional = reader.IsDBNull(4) ? 0 : reader.GetInt32(4);

                                salasDatos.diferencias = alfa - beta;

                                listaSalas.Add(salasDatos);
                                

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


            return listaSalas;

        }



        [HttpGet, Route("ObtenerHistoricoSalas")]
        public List<salas> ObtenerHistoricoSalas(String unidad_sala)
        {

            int x = Int32.Parse(unidad_sala);

            salas salasDatos = new salas();

            List<salas> listaSalas = new List<salas>();

            try
            {
                ConexionBD con_valores = new ConexionBD();

                using (SqlConnection v_con_valores = con_valores.Conexion())
                {

                    //string salas = "select IdUnidadOrganizacional,NombreUnidadOrg,ensala,enenvio FROM MaestraSalas where id_adicional >= 0 order by IdUnidadOrganizacional ASC";

                    string historicoSalas = "SELECT IdUnidadOrganizacional,id_adicional,Fecha FROM HistoricoRev where IdUnidadOrganizacional = "+x+" order by Fecha desc";

                    using (SqlCommand command = new SqlCommand(historicoSalas, v_con_valores))
                    {
                        v_con_valores.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                salasDatos = new salas();

                                salasDatos.IdUnidadOrganizacional = reader.GetInt32(0);
                                salasDatos.id_adicional =reader.GetInt32(1);
                                salasDatos.Fecha = reader.GetDateTime(2).ToString();

                                //int alfa = salasDatos.ensala = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                                //int beta = salasDatos.enenvio = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);
                                //salasDatos.diferencias = alfa - beta;                                
                                listaSalas.Add(salasDatos);

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

            return listaSalas;
        }



    }
}
