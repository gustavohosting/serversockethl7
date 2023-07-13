using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HL7
{
    public class Leer
    {
        private string resultadoSimbolo ;
        List<Entidades.Analito> analitos ;
        public Leer()
        {
            resultadoSimbolo = "R|";
            analitos = new List<Entidades.Analito>();
        }
    }
}
