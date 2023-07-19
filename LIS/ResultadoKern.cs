using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIS
{/// <summary>
/// Como guardo los resultados
/// </summary>

    public class ResultadoKern:Lis
    {
        private string inicioXml ;
        private string finXml;

        string simboloInicioCampo ;
        string simboloNombreCampo ;
        string simboloBarraCampo ;
        string simboloInicioCampoMensaje ;
        string simboloFinCampo;

        private string valorAnalito;
        private string valorNumerico;
        private string valorTexto;
        private string valorCampo4;
        private string valorCampo5;


        public ResultadoKern()
        {
            simboloInicioCampo = "<";
            simboloNombreCampo = "campo";
            simboloBarraCampo = "/";
            simboloInicioCampoMensaje = simboloInicioCampo + simboloBarraCampo + simboloNombreCampo;
            simboloInicioCampoMensaje = simboloInicioCampo + simboloNombreCampo;
            simboloFinCampo = ">";
            inicioXml = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?><items><item>";
            finXml = "</item></items>";
            //   "<?xml version="1.0" encoding="ISO - 8859 - 1"?><items><item><campo1>"
            //1069
            //</campo1><campo2>
            //139
            //</campo2><campo3></campo3><campo4></campo4><campo5></campo5></item></items>'"
        }

        public string ValorAnalito { get => valorAnalito; set => valorAnalito = value; }
        public string ValorNumerico { get => valorNumerico; set => valorNumerico = value; }
        public string ValorTexto { get => valorTexto; set => valorTexto = value; }
        public string ValorCampo4 { get => valorCampo4; set => valorCampo4 = value; }
        public string ValorCampo5 { get => valorCampo5; set => valorCampo5 = value; }

        public string xml(string analito, string valor,string tipoResultado)
        {
            if (tipoResultado == "N")
            {
                ValorNumerico= valor;
                ValorTexto= "";
            }else
            {
                ValorNumerico = "";
                ValorTexto = valor;
            }
            valorAnalito = analito;
            ValorCampo4 = "";
            ValorCampo5= "";
            return inicioXml + camposToXml() + finXml ;
        }
        private string camposToXml()
        {
            Recursos.SimboloXml simboloXml = new Recursos.SimboloXml();
            string nombreCampo = "campo";
            string campoXml = "";
            int idCampo = 1;
            campoXml =  simboloXml.campoInicio(nombreCampo,idCampo.ToString().Trim()) + valorAnalito.ToString().Trim() + simboloXml.campoFin(nombreCampo, idCampo.ToString().Trim()); 
            campoXml = campoXml + simboloXml.campoInicio(nombreCampo, idCampo.ToString().Trim()) + valorNumerico.ToString().Trim() + simboloXml.campoFin(nombreCampo, idCampo++.ToString().Trim());
            campoXml = campoXml + simboloXml.campoInicio(nombreCampo, idCampo.ToString().Trim()) + ValorTexto.ToString().Trim() + simboloXml.campoFin(nombreCampo, idCampo++.ToString().Trim());
            campoXml = campoXml + simboloXml.campoInicio(nombreCampo, idCampo.ToString().Trim()) + valorCampo4 + simboloXml.campoFin(nombreCampo, idCampo++.ToString().Trim());
            campoXml = campoXml + simboloXml.campoInicio(nombreCampo, idCampo.ToString().Trim()) + valorCampo5 + simboloXml.campoFin(nombreCampo, idCampo++.ToString().Trim());
            return campoXml;
        }
    }
}
