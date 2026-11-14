
using Microsoft.Data.SqlClient;
using Dapper;

namespace TPFINAL.Models
{
    public class BD
    {
       
       private string connectionString =
    @"Server=(localdb)\MSSQLLocalDB;Database=GimnasioFuerzaDB;Trusted_Connection=True;";


        public bool ProbarConexion()
        {
            using (SqlConnection conexion =
                new SqlConnection(connectionString))
            {
                conexion.Open();
                return true;
            }
        }
    }
}
