using System.Data.SqlClient;

namespace ApiNovaAnalyzer.Conexion
{
    public class ConexionBD
    {

        public SqlConnection Conexion()
        {


            /*SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = "sql5052.site4now.net";
            builder.UserID = "db_a4f52c_datamartbingonova_admin";
            builder.Password = "Gr3c01_2024*";
            builder.InitialCatalog = "db_a4f52c_datamartbingonova"; */


            /*
            
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString =
              "Data Source=sql5052.site4now.net;" +
              "Initial Catalog=db_a4f52c_datamartbingonova;" +
              "User id=db_a4f52c_datamartbingonova_admin;" +
              "Password=Gr3c01_2024*;";
            
            */

            //conexion a nueva bd centralizada
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=SQL1003.site4now.net;Initial Catalog=db_ab8ee3_novapruebas;User Id=db_ab8ee3_novapruebas_admin;Password=T8%52FGH96jjDeZ";


            return conn;

        }

    }
}
