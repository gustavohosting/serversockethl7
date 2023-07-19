using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recursos
{
    public class SimboloXml
    {
        private string simboloInicio = "<";
        private string simboloBarra = "/";
        private string simboloFin = ">";
        public string campoInicio(string nombreCampo, string idCampo)
        {
            return simboloInicio + nombreCampo + idCampo + simboloFin;
        }
        public string campoFin(string nombreCampo, string idCampo)
        {
            return simboloInicio+simboloBarra + nombreCampo + idCampo + simboloFin;
        }
    }

    public class varios
    {
        //string texto;

        public string redondeo(string valor)
        {
            int indice = valor.IndexOf(".");
            if (indice != 0)
            { 
                int valorDecimal = Convert.ToInt32( valor.Substring (indice+1).PadRight (2,'0'));
                if (valorDecimal > 50)
                {
                    // saco la parte decimal e incremento en uno la parte entera
                    valor = Convert.ToString(Convert.ToInt32(valor.Substring(0, indice )) + 1);
                }
                else {
                    // saco la parte decimal
                    valor = valor.Substring(0, indice);
                }
            }
            return valor;
        }
        /// <summary>
        /// si el valor contiene ->  "+-" devuelve "traza"
        /// si el valor contiene ->  "Neg" devuelve "NC"
        /// en caso contrario debe tener un valor numerico posicion[1] ->  2 devuelve "++" tantas cruces como valor numerico
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public string traza(string valor)
        {
            
            if (valor.Contains("Neg")) valor = "NC";
            else if (valor.Contains("+-")) valor = "Trazas";
            else if (valor.Contains("Trace")) valor = "Trazas";
            else
            {

                string[] calcular_cruces = valor.Split(' ');
                int numero = Convert.ToInt32(calcular_cruces[1].Replace('+',' ').Trim());
                if (numero >= 1)
                {
                    if (numero > 3) numero = 3;// para el equipo de orinas es hasta 3 cruces.
                    valor = "";
                    for (int i = 1; i <= numero; i++)
                    {
                        valor += "+";
                    }
                }
                else valor = "+";
            }
            return valor;
        }
        /// <summary>
        /// Esta funcion es para proteina
        /// </summary>
        /// <param name="valor"></param>
        /// <param name="esProteina"></param>
        /// <returns></returns>
        public string traza(string valor, bool esProteina)
        {

            if (valor.Contains("Neg")) valor = "NC";
            else if (valor.Contains("+-")) valor = "T";
            else if (valor.Contains("Trace")) valor = "T";
            else
            {

                string[] calcular_cruces = valor.Split(' ');
                int numero = Convert.ToInt32(calcular_cruces[1].Replace('+', ' ').Trim());
                if (numero >= 1)
                {
                    if (numero > 3) numero = 3;// para el equipo de orinas es hasta 3 cruces.
                    valor = "";
                    for (int i = 1; i <= numero; i++)
                    {
                        valor += "+";
                    }
                }
                else valor = "+";
            }
            return valor;
        }
        /// <summary>
        /// Devuelve positivo o negativo. Valor esperado "Neg" si no es asi, se devuelve positivo.
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public string positivo_Negativo(string valor)
        {
            if (valor.Trim() == "Neg") valor = "Negativo";
            else valor = "Positivo";
            return valor;
        }

    }
}
