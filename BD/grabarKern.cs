using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BD
{
    /// <summary>
    /// Si cambiara el LIS tengo que generar otra grabarPLINPLIN para que se adapte a ese LIS
    /// </summary>
    public class grabarKern
    {
        public grabarKern()
        {
        }
        public bool grabarResultados( List<Entidades.Analito> l_analitos)
        {
            LIS.ResultadoKern resultadoKern = new LIS.ResultadoKern();
            bool ok = false;
            try
            {
                string analizadorID="IQ";
                string muestraID;
                string usuarioID = "526";
                string ingreso = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().Trim().PadLeft(2, '0') + DateTime.Now.Day.ToString().Trim().PadLeft(2, '0') + " " + DateTime.Now.TimeOfDay.ToString().Substring(0, 8);
                string resultadoLIS = "";
                int cantidadDeterminaciones = l_analitos.Count();
                foreach (Entidades.Analito analitio in l_analitos)
                {
                    if (analitio.CodigoLis !=null )
                    {
                        resultadoLIS = "";
                        muestraID = analitio.MuestraID;
                        //resultadoLIS = resultadoKern.xml(l_determinacionAnalizadorEquipo[i].Determinacion_analizador.CodigoLIS, l_determinacionAnalizadorEquipo[i].Determinacion_analizador.Resultado);
                        resultadoLIS = resultadoKern.xml(analitio.CodigoLis, analitio.Valor, analitio.TipoValor);
                        ejecutarSP(muestraID, analizadorID, ingreso, usuarioID, resultadoLIS);
                    }
                }
                //for (int i = 0; i < cantidadDeterminaciones; i++)
                //{
                //    if (l_determinacionAnalizadorEquipo[i]. != null)
                //    {
                //        resultadoLIS = "";
                //        muestraID = l_determinacionAnalizadorEquipo[i].Determinacion_analizador.MuestraCodigo;
                //        analizadorID = analizador.Codigo;
                //        resultadoLIS = resultadoKern.xml(l_determinacionAnalizadorEquipo[i].Determinacion_analizador.CodigoLIS, l_determinacionAnalizadorEquipo[i].Determinacion_analizador.Resultado);
                //        resultadoLIS = resultadoKern.xml(l_determinacionAnalizadorEquipo[i].Determinacion_analizador.CodigoLIS, l_determinacionAnalizadorEquipo[i].Determinacion_analizador.Resultado, l_determinacionAnalizadorEquipo[i].Determinacion_analizador.TipoResultado);
                //        ejecutarSP(muestraID, analizadorID, ingreso, usuarioID, resultadoLIS);
                //    }
                //}
                return ok;
            }
            catch (Exception)
            {
                return ok;
                throw new NotImplementedException();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="muestraID"></param>
        /// <param name="analizadorID"></param>
        /// <param name="horaIngreso"></param>
        /// <param name="usuarioID"></param>
        /// <param name="resultado"></param>
        private void ejecutarSP(string muestraID,string analizadorID, string horaIngreso, string usuarioID,string resultadoxmlKern)
        {
            try
            {
                BD.Conectar conexion = new BD.ConectarKern();
                conexion.abrir();
                string mue = muestraID;
                string ana = analizadorID;
                string ingreso = horaIngreso;
                string usu = usuarioID;
                string res = resultadoxmlKern;
                SqlCommand comando = new SqlCommand("interfaz_resultados", conexion.abrir());
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@BARCODE", muestraID);
                comando.Parameters.AddWithValue("@ANALIZADOR_COD", analizadorID);
                comando.Parameters.AddWithValue("@FECHAHORA_RESULTADO", horaIngreso);
                comando.Parameters.AddWithValue("@USUARIO_ID", usuarioID);
                comando.Parameters.AddWithValue("@xmlPres", res);
                comando.ExecuteNonQuery();
                conexion.cerrar();
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
    }
}
