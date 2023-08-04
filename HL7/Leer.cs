using System;
using System.Collections.Generic;
using System.IO;
namespace HL7
{
    /// <summary>
    /// Carga los analitos recibos y los procesa
    /// </summary>
    public class HL7
    {
        private string renglonResultado;
        private string muestraID;
        private string muestra;
        private string path = "C:\\fpm\\git\\ServerSockethl7\\ServerSocketHL7\\";
        private string archivoNombre = "resultados.txt";
        private const string c_tipoNumerico = "N";
        public HL7()
        {
            renglonResultado= "R|";
            muestra= "O|";
            muestraID = "";
        }
        //public void archivoLeer(ref List<Entidades.Analito> l_analitos)
        //{
        //    // NO DEBER FUNCIONAR MODIFIQUE ESTO Y LO CONVERTI EN UNA FUNCION QUE LEE UN STRING.
        //    // agregar analitos de orinas
        //    agregarAnalitos(ref l_analitos);
        //    if (File.Exists(path+archivoNombre))
        //    {
        //        try
        //        {
        //            StreamReader sr = new StreamReader(path + archivoNombre);
        //            string nombre = "";
        //            string valor = "";
        //            string renglon = sr.ReadLine();
        //            while (renglon != null) {
        //                if (renglon.IndexOf(renglonResultado) == 0)
        //                {
        //                    string[] renglonValores = renglon.Split('|');
        //                    Entidades.Analito analito = new Entidades.Analito();
        //                    nombre = renglonValores[2].ToString().Substring(3);
        //                    valor = renglonValores[3].ToString();
        //                    l_analitos.Find(x => x.Nombre == nombre).Valor = valor;
        //                }
        //                renglon = sr.ReadLine();
        //            }
        //            sr.Close();
        //            foreach (Entidades.Analito analito in l_analitos)
        //            {
        //                if (analito.Nombre== "SG")
        //                {
        //                    if (analito.Valor.ToString().IndexOf(".") != -1) analito.Valor=analito.Valor.ToString().Replace('.', ',');
        //                    analito.TipoValor = c_tipoNumerico;
        //                }
        //                if (analito.Nombre == "LEU")
        //                { 
        //                    // los leuco no se pasan a kern
        //                }
        //                if (analito.Nombre == "NIT")
        //                {
        //                    if (analito.Valor.ToString().IndexOf("Pos.") != -1) analito.Valor = "Positivo";
        //                    else analito.Valor = "Negativo";
        //                    analito.TipoValor = "";
        //                }
        //                if (analito.Nombre == "pH")
        //                {
        //                    if (analito.Valor.ToString().IndexOf(".") != -1) analito.Valor=analito.Valor.ToString().Replace('.', ',');
        //                    analito.TipoValor = c_tipoNumerico;
        //                }
        //                if (analito.Nombre == "PRO")
        //                {
        //                    analito.Valor= resultadoProcesarContieneTrazas(analito.Valor);
        //                }
        //                if (analito.Nombre == "GLU")
        //                {
        //                    analito.Valor = resultadoProcesarContieneTrazas(analito.Valor);
        //                }
        //                if (analito.Nombre == "KET")
        //                {
        //                    analito.Valor = resultadoProcesarContieneTrazas(analito.Valor);
        //                }
        //                if (analito.Nombre == "UBG")
        //                {
        //                    analito.Valor = resultadoProcesarContieneTrazas(analito.Valor);
        //                }
        //                if (analito.Nombre == "BIL")
        //                {
        //                    analito.Valor = resultadoProcesarContieneTrazas(analito.Valor);
        //                }
        //                if (analito.Nombre == "BLD")
        //                {
        //                    analito.Valor = resultadoProcesarContieneTrazas(analito.Valor);
        //                }
        //                if (analito.Nombre == "COLOR")
        //                { 
        //                    //no va a kern
        //                }
        //                if (analito.Nombre == "CLARITY")
        //                {
        //                    //no va a kern
        //                }
        //                if (analito.Nombre == "RBC")
        //                {
        //                    string v = analito.Valor;
        //                    string t = analito.TipoValor;
        //                    analito.Valor = resultadosMenosValorMasde30(ref v, ref analito.TipoValor);
        //                }
        //                    //( analito.Valor = "Contiene +";
        //                if (analito.Nombre == "nRBC")
        //                {
        //                    //no va
        //                }
        //                if (analito.Nombre == "WBC")
        //                {
        //                    analito.Valor = resultadosMenosValorMasde30(analito.Valor);
        //                }
        //                if (analito.Nombre == "EPC")
        //                {
        //                    analito.Valor = resultadosMenosValorMasde30(analito.Valor);
        //                }
        //                if (analito.Nombre == "Casts")
        //                {
        //                }
        //                if (analito.Nombre == "HYA")
        //                {
        //                    int numero = Convert.ToInt32(analito.Valor);
        //                    if (numero == 0) analito.Valor = "";
        //                }
        //                if (analito.Nombre == "GRAN")
        //                {
        //                }
        //                if (analito.Nombre == "CRYSTALS")
        //                {
        //                }
        //                if (analito.Nombre == "CaOX")
        //                {
        //                }
        //                if (analito.Nombre == "CUSTOM1")
        //                {
        //                }
        //                if (analito.Nombre == "TRIP")
        //                {
        //                }
        //                if (analito.Nombre == "UA")
        //                {
        //                }
        //                if (analito.Nombre == "AMO")
        //                {
                            
        //                }
        //                if (analito.Nombre == "Bacteria")
        //                {
        //                    //no se graba en kern
        //                }
        //                if (analito.Nombre == "YST")
        //                {
        //                    //no se graba en kern
        //                }
        //            }
        //        }
        //        catch(Exception e) {
        //            string error = e.Message;
        //        }
        //    }
        //}
        /// <summary>
        /// Procesar un mensaje(string) y buscar valores
        /// </summary>
        /// <param name="mensaje"></param>
        /// <param name="l_analitos"></param>
        public void mensajeLeer(string mensaje,ref List<Entidades.Analito> l_analitos)
        {
            // procesa el resultado
            try
            {
                muestraID = "";
                agregarAnalitos(ref l_analitos);
                //StreamReader sr = new StreamReader(path + archivoNombre);
                string nombre = "";
                string valor = "";
                string [] mensajeDivido = mensaje.Split((char)13);
                string renglon = "";
                for(int i =0;mensajeDivido.Length>i;i++)
                {
                    renglon = mensajeDivido[i];
                    if (renglon.IndexOf(renglonResultado) == 0)
                    {
                        string[] renglonValores = renglon.Split('|');
                        Entidades.Analito analito = new Entidades.Analito();
                        nombre = renglonValores[2].ToString().Substring(3);
                        if (renglonValores[3].ToString().IndexOf('^') == -1)
                        {
                            valor = renglonValores[3].ToString();
                        }
                        else
                        {
                            //resultado del sedimento
                            valor = renglonValores[3].ToString().Split('^')[0];
                        }

                        l_analitos.Find(x => x.Nombre == nombre).Valor = valor;
                    }
                    else if (renglon.IndexOf(muestra) == 0 && muestraID == "")
                    {
                        string[] renglonValores = renglon.Split('|');
                        muestraID = renglonValores[2].ToString().Split('^')[0];
                    }

                    //renglon = sr.ReadLine();
                }
                foreach (Entidades.Analito analito in l_analitos)
                {
                    analito.MuestraID = muestraID;
                    if (analito.Nombre == "SG")
                    {
                        if (analito.Valor.ToString().IndexOf(".") != -1) analito.Valor = analito.Valor.ToString().Replace('.', ',');
                        analito.TipoValor = c_tipoNumerico;
                    }
                    if (analito.Nombre == "LEU")
                    {
                        // los leuco no se pasan a kern
                    }
                    if (analito.Nombre == "NIT")
                    {
                        if (analito.Valor.ToString().IndexOf("Pos.") != -1) analito.Valor = "Positivo";
                        else analito.Valor = "Negativo";
                        analito.TipoValor= "";
                    }
                    if (analito.Nombre == "pH")
                    {
                        if (analito.Valor.ToString().IndexOf(".") != -1) analito.Valor = analito.Valor.ToString().Replace('.', ',');
                        analito.TipoValor = c_tipoNumerico;
                    }
                    if (analito.Nombre == "PRO")
                    {
                        analito.Valor = resultadoProcesarContieneTrazas(analito.Valor);
                    }
                    if (analito.Nombre == "GLU")
                    {
                        analito.Valor = resultadoProcesarContieneTrazas(analito.Valor);
                    }
                    if (analito.Nombre == "KET")
                    {
                        analito.Valor = resultadoProcesarContieneTrazas_v2(analito.Valor);
                    }
                    if (analito.Nombre == "UBG")
                    {
                        analito.Valor = resultadoProcesarContieneTrazas(analito.Valor);
                    }
                    if (analito.Nombre == "BIL")
                    {
                        analito.Valor = resultadoProcesarContieneTrazas(analito.Valor);
                    }
                    if (analito.Nombre == "BLD")
                    {
                        analito.Valor = resultadoProcesarContieneTrazas_v2(analito.Valor);
                    }
                    if (analito.Nombre == "COLOR")
                    {
                        //no va a kern
                    }
                    if (analito.Nombre == "CLARITY")
                    {
                        //no va a kern
                    }
                    if (analito.Nombre == "RBC")
                    {
                        string v = analito.Valor;
                        string t = analito.TipoValor;
                         resultadosMenosValorMasde30_v2(ref v, ref t);
                        analito.Valor = v;
                        analito.TipoValor = t;
                    }
                    //( analito.Valor = "Contiene +";
                    if (analito.Nombre == "nRBC")
                    {
                        //no va
                    }
                    if (analito.Nombre == "WBC")
                    {
                        string v = analito.Valor;
                        string t = analito.TipoValor;
                        resultadosMenosValorMasde30_v2(ref v, ref t);
                        analito.Valor = v;
                        analito.TipoValor = t;
                    }
                    if (analito.Nombre == "EPC")
                    {
                        string v = analito.Valor;
                        string t = analito.TipoValor;
                        resultadosMenosValorMasde30(ref v, ref t);
                        analito.Valor = v;
                        analito.TipoValor = t;
                    }
                    if (analito.Nombre == "Casts")
                    {
                        if (Convert.ToInt32(analito.Valor) == 0) analito.Valor = "";
                    }
                    if (analito.Nombre == "HYA")
                    {
                        if (Convert.ToInt32(analito.Valor) == 0) analito.Valor = "";
                    }
                    if (analito.Nombre == "GRAN")
                    {
                        if (Convert.ToInt32(analito.Valor) == 0) analito.Valor = "";
                    }
                    if (analito.Nombre == "CRYSTALS")
                    {
                        int numero = Convert.ToInt32(analito.Valor);
                        if (numero >= 0 && numero<=1) analito.Valor = "-";
                        if (numero > 1 && numero <=14) analito.Valor = "+";
                        if (numero > 14 && numero <= 32) analito.Valor = "++";
                        if (numero > 14 && numero <= 32) analito.Valor = "++";
                        if (numero > 32 && numero <= 66) analito.Valor = "+++";
                        if (numero > 66) analito.Valor = "++++";
                    }
                    if (analito.Nombre == "CaOX")
                    {
                        int numero = Convert.ToInt32(analito.Valor);
                        if (numero >= 0 && numero <= 1) analito.Valor = "NOBS";
                        if (numero > 1 && numero <= 5) analito.Valor = "ESC";
                        if (numero > 5 && numero <= 15) analito.Valor = "FRE";
                        if (numero > 15 && numero <= 25) analito.Valor = "ABUN";
                        if (numero > 25 ) analito.Valor = "ABUN";

                    }
                    if (analito.Nombre == "CUSTOM1\\Fosf am")
                    {
                        int numero = Convert.ToInt32(analito.Valor);
                        if (numero >= 0 && numero < 1) analito.Valor = "NOBS";
                        if (numero >= 1 && numero <= 5) analito.Valor = "ESC";
                        if (numero > 5 && numero <= 15) analito.Valor = "FRE";
                        if (numero > 15 && numero <= 25) analito.Valor = "ABUND";
                        if (numero > 25) analito.Valor = "ABUND";
                    }
                    if (analito.Nombre == "TRIP")
                    {
                        int numero = Convert.ToInt32(analito.Valor);
                        if (numero >= 0 && numero <= 1) analito.Valor = "NOBS";
                        if (numero > 1 && numero <= 5) analito.Valor = "ESC";
                        if (numero > 5 && numero <= 15) analito.Valor = "FRE";
                        if (numero > 15 && numero <= 25) analito.Valor = "ABUN";
                        if (numero > 25) analito.Valor = "ABUN";
                    }
                    if (analito.Nombre == "UA")
                    {
                        int numero = Convert.ToInt32(analito.Valor);
                        if (numero >= 0 && numero <= 1) analito.Valor = "NOBS";
                        if (numero > 1 && numero <= 5) analito.Valor = "ESC";
                        if (numero > 5 && numero <= 15) analito.Valor = "FRE";
                        if (numero > 15 && numero <= 25) analito.Valor = "ABUN";
                        if (numero > 25) analito.Valor = "ABUN";
                    }
                    if (analito.Nombre == "AMO")
                    {
                        int numero = Convert.ToInt32(analito.Valor);
                        if (numero >= 0 && numero <= 1) analito.Valor = "NOBS";
                        if (numero > 1 && numero <= 5) analito.Valor = "ESC";
                        if (numero > 5 && numero <= 15) analito.Valor = "FRE";
                        if (numero > 15 && numero <= 25) analito.Valor = "ABUND";
                        if (numero > 25) analito.Valor = "ABUND";
                    }
                    if (analito.Nombre == "Bacteria")
                    {
                        //no se graba en kern
                    }
                    if (analito.Nombre == "YST")
                    {
                        //no se graba en kern
                    }
                }
            }
            catch (Exception e)
            {
                string error = e.Message;
            }
        }

        /// <summary>
        /// Cambia el valor traces neg. 6
        /// </summary>
        /// <param name="Valor"></param>
        /// <returns></returns>
        private string resultadoProcesarContieneTrazas( string Valor)
        {
            
            if (Valor.IndexOf("Neg.") != -1)  Valor = "NC";
            if (Valor.IndexOf("Norm.") != -1) Valor = "NC";
            if (Valor.IndexOf("Traces") != -1) Valor = "T";
            if (Valor.IndexOf("+") != -1)
            {
                if (Valor.IndexOf("1")>=0) Valor = "+";
                if (Valor.IndexOf("2") >= 0) Valor = "++";
                if (Valor.IndexOf("3") >= 0) Valor = "+++";
                if (Valor.IndexOf("4") >= 0) Valor = "++++";
                if (Valor.IndexOf("5") >= 0) Valor = "+++++";
                if (Valor.IndexOf("6") >= 0) Valor = "++++++";
            }
            return Valor;
        }
        private string resultadoProcesarContieneTrazas_v2(string Valor)
        {

            if (Valor.IndexOf("Neg.") != -1) Valor = "NC";
            if (Valor.IndexOf("Norm.") != -1) Valor = "NC";
            if (Valor.IndexOf("Traces") != -1) Valor = "Trazas";
            if (Valor.IndexOf("+") != -1)
            {
                if (Valor.IndexOf("1") >= 0) Valor = "+";
                if (Valor.IndexOf("2") >= 0) Valor = "++";
                if (Valor.IndexOf("3") >= 0) Valor = "+++";
                if (Valor.IndexOf("4") >= 0) Valor = "++++";
                if (Valor.IndexOf("5") >= 0) Valor = "+++++";
                if (Valor.IndexOf("6") >= 0) Valor = "++++++";
            }
            return Valor;
        }
        /// <summary>
        /// retorna menor de .....1,2, mayor de 30
        /// </summary>
        /// <param name="Valor"></param>
        /// <returns></returns>
        private void resultadosMenosValorMasde30(ref string Valor, ref string TipoValor)
        {
            char[] numeros = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            if (Valor.IndexOfAny(numeros) >= 0)
            {
                int numero = Convert.ToInt32(Valor);
                if (Convert.ToInt32(Valor) == 0)
                {
                    Valor = "NC";
                    TipoValor = "";
                }
                else if (Convert.ToInt32(Valor) <= 30)
                {
                    TipoValor = c_tipoNumerico;
                    //se deja el mismo valor
                }
                else { 
                    Valor = ">30";
                    TipoValor = "";
                }
            }
        }
        private void resultadosMenosValorMasde30_v2(ref string Valor, ref string TipoValor)
        {
            char[] numeros = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            if (Valor.IndexOfAny(numeros) >= 0)
            {
                int numero = Convert.ToInt32(Valor);
                if (Convert.ToInt32(Valor) == 0)
                {
                    Valor = "NC";
                    TipoValor = "";
                }
                else if (Convert.ToInt32(Valor) <= 30)
                {
                    TipoValor = c_tipoNumerico;
                    //se deja el mismo valor
                }
                else
                {
                    Valor = "&gt;30";
                    TipoValor = "";
                }
            }
        }

        /// <summary>
        /// Cargar analitos de la prestacion
        /// </summary>
        /// <param name="l_analitos"></param>
        private void agregarAnalitos(ref List<Entidades.Analito> l_analitos)
        {
            l_analitos.Add(new Entidades.Analito { Nombre = "SG",CodigoLis="SG" ,TipoValor="N"});
            l_analitos.Add(new Entidades.Analito { Nombre = "LEU"});// NO VA KERN
            l_analitos.Add(new Entidades.Analito { Nombre = "NIT",CodigoLis="NIT"});
            l_analitos.Add(new Entidades.Analito { Nombre = "pH",CodigoLis="PH" ,TipoValor="N"});
            l_analitos.Add(new Entidades.Analito { Nombre = "PRO",CodigoLis="PRO" });
            l_analitos.Add(new Entidades.Analito { Nombre = "GLU",CodigoLis="GLU" });
            l_analitos.Add(new Entidades.Analito { Nombre = "KET",CodigoLis="KET" });
            l_analitos.Add(new Entidades.Analito { Nombre = "UBG",CodigoLis="URO" });//?? confirmar con Mariano
            l_analitos.Add(new Entidades.Analito { Nombre = "BIL" ,CodigoLis="BIL"});
            l_analitos.Add(new Entidades.Analito { Nombre = "BLD" ,CodigoLis="BLD"});
            l_analitos.Add(new Entidades.Analito { Nombre = "COLOR" });//NO VA KERN
            l_analitos.Add(new Entidades.Analito { Nombre = "CLARITY" });//NO VA  KERN
            l_analitos.Add(new Entidades.Analito { Nombre = "RBC" ,CodigoLis="RBC"});
            l_analitos.Add(new Entidades.Analito { Nombre = "nRBC" });//NO VA A KERN
            l_analitos.Add(new Entidades.Analito { Nombre = "WBC",CodigoLis="WBC" });
            l_analitos.Add(new Entidades.Analito { Nombre = "EPC" ,CodigoLis="SQEP"});//?? confirmar con mariano
            l_analitos.Add(new Entidades.Analito { Nombre = "Casts" });//NO VA A KERN
            l_analitos.Add(new Entidades.Analito { Nombre = "HYA" });//?? confirmar con mariano
            l_analitos.Add(new Entidades.Analito { Nombre = "GRAN" });// ?? ver con mariano
            l_analitos.Add(new Entidades.Analito { Nombre = "CRYSTALS" });//NO VA A KERN
            l_analitos.Add(new Entidades.Analito { Nombre = "CaOX",CodigoLis="CAOX" });
            l_analitos.Add(new Entidades.Analito { Nombre = "CUSTOM1\\Fosf am" ,CodigoLis="AMOR"});//?? VER CON MARIANO
            l_analitos.Add(new Entidades.Analito { Nombre = "TRIP",CodigoLis="FOSFTRIPLE" });//???VER !! GUSTAVO
            l_analitos.Add(new Entidades.Analito { Nombre = "UA" ,CodigoLis="URIC"});//??VER CON MARIANO
            l_analitos.Add(new Entidades.Analito { Nombre = "AMO" });// mariano por ahora no lo pasamos
            l_analitos.Add(new Entidades.Analito { Nombre = "Bacteria" });//NO VA A KERN
            l_analitos.Add(new Entidades.Analito { Nombre = "YST" });//NO VA A KERN
        }
    }
}
