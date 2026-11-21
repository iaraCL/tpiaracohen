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
    
 
       
        public bool ExisteUsuario(string nombreUsuario)
        {
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                int cantidad = conexion.QueryFirst<int>(
                    "sp_ExisteUsuario",
                    new { NombreUsuario = nombreUsuario },
                    commandType: System.Data.CommandType.StoredProcedure
                );

                return cantidad > 0;
            }
        }

        
        
       
        public bool RegistrarUsuario(Usuario usuario)
        {
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                int filas = conexion.Execute(
                    "sp_RegistrarUsuario",
                    new
                    {
                        NombreUsuario = usuario.NombreUsuario,
                        Contrasena = usuario.Contrasena,
                        Nombre = usuario.Nombre,
                        Apellido = usuario.Apellido
                    },
                    commandType: System.Data.CommandType.StoredProcedure
                );

                return filas > 0;
            }
        }


        
        
        public Usuario BuscarUsuario(string nombreUsuario)
        {
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                return conexion.QueryFirstOrDefault<Usuario>(
                    "sp_BuscarUsuario",
                    new { NombreUsuario = nombreUsuario },
                    commandType: System.Data.CommandType.StoredProcedure
                );
            }
        }


       
        public List<Clase> ObtenerClases()
        {
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                
                return new List<Clase>(conexion.Query<Clase>(
                    "sp_ObtenerClases",
                    commandType: System.Data.CommandType.StoredProcedure
                ));

            }
        }


        
        public List<Clase> ObtenerMasClases(int cantidad)
        {
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                return new List<Clase>(conexion.Query<Clase>(
                    "sp_ObtenerMasClases",
                    new { Cantidad = cantidad },
                    commandType: System.Data.CommandType.StoredProcedure
                ));
            }
        }


    }
}
