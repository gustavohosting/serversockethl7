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
                }
                catch(Exception e) {
                    string error = e.Message;
                    
                }
            }
        }
    }
}
