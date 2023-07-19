using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BD
{
    public class ConectarKern:Conectar
    {
        public ConectarKern()
        {
            ConexionCadena = "Data Source=192.168.0.15;Initial Catalog=KERN_LAB_DB_FPM;User ID=sa;Password=Fpm.2018";
            Conexion.ConnectionString = ConexionCadena;
        }
    }
}
