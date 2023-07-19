using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Entidades;

namespace BD
{
    public abstract class grabar
    {
        public abstract bool grabarResultados(string equipo, string analizador, List<Entidades.Analito> l_determinacionAnalizadorEquipo);
    }
}
