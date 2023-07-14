using System;
using System.Collections.Generic;
using System.IO;
namespace HL7
{
    public class Leer
    {
        private string renglonResultado;
        List<Entidades.Analito> l_analitos ;
        private string path = "C:\\fpm\\git\\ServerSockethl7\\ServerSocketHL7\\";
        private string archivoNombre = "resultados.txt";
        public Leer()
        {
            renglonResultado= "R|";
            l_analitos = new List<Entidades.Analito>();
            l_analitos.Add(new Entidades.Analito { Nombre = "SG" });
            l_analitos.Add(new Entidades.Analito { Nombre = "LEU" });
            l_analitos.Add(new Entidades.Analito { Nombre = "NIT" });
            l_analitos.Add(new Entidades.Analito { Nombre = "pH" });
            l_analitos.Add(new Entidades.Analito { Nombre = "PRO" });
            l_analitos.Add(new Entidades.Analito { Nombre = "GLU" });
            l_analitos.Add(new Entidades.Analito { Nombre = "KET" });
            l_analitos.Add(new Entidades.Analito { Nombre = "UBG" });
            l_analitos.Add(new Entidades.Analito { Nombre = "BIL" });
            l_analitos.Add(new Entidades.Analito { Nombre = "BLD" });
            l_analitos.Add(new Entidades.Analito { Nombre = "COLOR" });
            l_analitos.Add(new Entidades.Analito { Nombre = "CLARITY" });
            l_analitos.Add(new Entidades.Analito { Nombre = "RBC" });
            l_analitos.Add(new Entidades.Analito { Nombre = "nRBC" });
            l_analitos.Add(new Entidades.Analito { Nombre = "WBC" });
            l_analitos.Add(new Entidades.Analito { Nombre = "EPC" });
            l_analitos.Add(new Entidades.Analito { Nombre = "Casts" });
            l_analitos.Add(new Entidades.Analito { Nombre = "HYA" });
            l_analitos.Add(new Entidades.Analito { Nombre = "GRAN" });
            l_analitos.Add(new Entidades.Analito { Nombre = "CRYSTALS" });
            l_analitos.Add(new Entidades.Analito { Nombre = "CaOX" });
            l_analitos.Add(new Entidades.Analito { Nombre = "CUSTOM1" });
            l_analitos.Add(new Entidades.Analito { Nombre = "TRIP" });
            l_analitos.Add(new Entidades.Analito { Nombre = "UA" });
            l_analitos.Add(new Entidades.Analito { Nombre = "AMO" });
            l_analitos.Add(new Entidades.Analito { Nombre = "Bacteria" });
            l_analitos.Add(new Entidades.Analito { Nombre = "YST" });
        }
        public void archivoLeer()
        {
            if (File.Exists(path+archivoNombre))
            {
                try
                {
                    StreamReader sr = new StreamReader(path + archivoNombre);
                    string nombre = "";
                    string valor = "";
                    string renglon = sr.ReadLine();
                    while (renglon != null) {
                        if (renglon.IndexOf(renglonResultado)==0) {
                            string[] renglonValores=renglon.Split('|');
                            Entidades.Analito analito = new Entidades.Analito();
                            nombre= renglonValores[2].ToString().Substring(3);
                            valor= renglonValores[3].ToString();
                            l_analitos.Find(x => x.Nombre == nombre).Valor = valor;
                        }
                        renglon = sr.ReadLine();
                    }
                    sr.Close();
                    foreach (Entidades.Analito analito in l_analitos)
                    {
                        if (analito.Nombre== "SG")
                        {
                            if (analito.Valor.ToString().IndexOf(".") != -1) analito.Valor.ToString().Replace('.', ',');
                        }
                        if (analito.Nombre == "LEU")
                        { 
                            // los leuco no se pasan a kern
                        }
                        if (analito.Nombre == "NIT")
                        {
                            if (analito.Valor.ToString().IndexOf("Pos.") != -1) analito.Valor = "Positivo";
                            else analito.Valor = "Negativo";
                        }
                        if (analito.Nombre == "pH")
                        {
                            if (analito.Valor.ToString().IndexOf(".") != -1) analito.Valor.ToString().Replace('.', ',');
                        }
                        if (analito.Nombre == "PRO")
                        {
                            analito.Valor= resultadoProcesarContieneTrazas(analito.Valor);
                        }
                        if (analito.Nombre == "GLU")
                        {
                            analito.Valor = resultadoProcesarContieneTrazas(analito.Valor);
                        }
                        if (analito.Nombre == "KET")
                        {
                            analito.Valor = resultadoProcesarContieneTrazas(analito.Valor);
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
                            analito.Valor = resultadoProcesarContieneTrazas(analito.Valor);
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
                            analito.Valor = resultadosMenosValorMasde30(analito.Valor);
                        }
                            //( analito.Valor = "Contiene +";
                        if (analito.Nombre == "nRBC")
                        {
                            //no va
                        }
                        if (analito.Nombre == "WBC")
                        {
                            analito.Valor = resultadosMenosValorMasde30(analito.Valor);
                        }
                        if (analito.Nombre == "EPC")
                        {
                            analito.Valor = resultadosMenosValorMasde30(analito.Valor);
                        }
                        if (analito.Nombre == "Casts")
                        {
                        }
                        if (analito.Nombre == "HYA")
                        {
                        }
                        if (analito.Nombre == "GRAN")
                        {
                        }
                        if (analito.Nombre == "CRYSTALS")
                        {
                        }
                        if (analito.Nombre == "CaOX")
                        {
                        }
                        if (analito.Nombre == "CUSTOM1")
                        {
                        }
                        if (analito.Nombre == "TRIP")
                        {
                        }
                        if (analito.Nombre == "UA")
                        {
                        }
                        if (analito.Nombre == "AMO")
                        { }
                        if (analito.Nombre == "Bacteria")
                        {
                        }
                        if (analito.Nombre == "YST")
                        {
                        }
                    }
                }
                catch(Exception e) {
                    string error = e.Message;
                }
            }
        }
        private string resultadoProcesarContieneTrazas( string Valor)
        {
            if (Valor.IndexOf("Neg.") != -1) Valor = "No Contiene";
            if (Valor.IndexOf("Traces") != -1) Valor = "Contiene Trazas";
            if (Valor.IndexOf("+") != -1)
            {
                Valor.Replace("+", "");
                if (Valor == "1") Valor = "Contiene +";
                if (Valor == "2") Valor = "Contiene +";
                if (Valor == "3") Valor = "Contiene +";
                if (Valor == "4") Valor = "Contiene +";
                if (Valor == "5") Valor = "Contiene +";
                if (Valor == "6") Valor = "Contiene +";
            }
            return Valor;
        }
        private string resultadosMenosValorMasde30(string Valor)
        {
            char[] numeros = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            if (Valor.IndexOfAny(numeros) >= 0)
            {
                int numero = Convert.ToInt32(Valor);
                if (Convert.ToInt32(Valor) == 0)
                {
                    Valor = "Menos de 1";
                }
                else if (Convert.ToInt32(Valor) <= 30)
                {

                }
                else { Valor = "Mas de 30"; }
            }
            return Valor;
        }

    }
}
