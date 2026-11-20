
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
            
             using (SqlConnection conexion =
             new SqlConnection(connectionString))
            {
             string sql = @"SELECT COUNT(*)
                       FROM Usuarios
                       WHERE NombreUsuario = @NombreUsuario";

            int cantidad = conexion.QueryFirst<int>(
            sql, new { NombreUsuario = nombreUsuario });

        return cantidad > 0;
             }
        }
        
        public bool RegistrarUsuario(Usuario usuario)
        {
          using (SqlConnection conexion =
          new SqlConnection(connectionString)){
        
        string sql = @"INSERT INTO Usuarios
                       (NombreUsuario, Contrasena, Nombre, Apellido)
                       VALUES
                       (@NombreUsuario, @Contrasena, @Nombre, @Apellido)";

        int filas = conexion.Execute(sql, usuario);

        return filas > 0;  }

        
        } 
        
        public Usuario BuscarUsuario(string nombreUsuario)
        {
             using (SqlConnection conexion =
             new SqlConnection(connectionString))
            {
                 string sql = @"SELECT *
                 FROM Usuarios
                 WHERE NombreUsuario = @NombreUsuario";

                 Usuario usuario = conexion.QueryFirstOrDefault<Usuario>(
                  sql, new { NombreUsuario = nombreUsuario });

                  return usuario;
            }
}
 
        }  
         


}
