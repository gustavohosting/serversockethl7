using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Analito
    {
        private string nombre;
        private string valor;
        private string codigoLis;
        private string muestraID;
        private string tipoValor;
        public string Nombre { get => nombre; set => nombre = value; }
        public string Valor { get => valor; set => valor = value; }
        public string CodigoLis { get => codigoLis; set => codigoLis = value; }
        public string MuestraID { get => muestraID; set => muestraID = value; }
        public string TipoValor { get => tipoValor; set => tipoValor = value; }
    }
}
