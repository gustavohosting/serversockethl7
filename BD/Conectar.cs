using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BD
{
    public abstract class Conectar
    {
        private SqlConnection conexion = new SqlConnection() ;
        private string conexionCadena;

        public SqlConnection Conexion { get => conexion; set => conexion = value; }
        public string ConexionCadena { get => conexionCadena; set => conexionCadena = value; }

        public virtual SqlConnection abrir()
        {
            try
            {
                if (Conexion.State == System.Data.ConnectionState.Closed) Conexion.Open();
            }
            catch
            {
                if (Conexion.State == System.Data.ConnectionState.Open) Conexion.Close();
            }
            return Conexion;
        }
        public virtual SqlConnection cerrar()
        {
            try
            {
                if (Conexion.State == System.Data.ConnectionState.Open) Conexion.Close();
            }
            catch
            {
                if (Conexion.State == System.Data.ConnectionState.Open) Conexion.Close();
            }
            return Conexion;
        }
    }
}
