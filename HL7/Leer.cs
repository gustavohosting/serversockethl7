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
        }
        public void archivoLeer()
        {
            if (File.Exists(path+archivoNombre))
            {
                try
                {
                    StreamReader sr = new StreamReader(path + archivoNombre);
                    string renglon = sr.ReadLine();
                    while (renglon != null) {
                        if (renglon.IndexOf(renglonResultado)==0) {
                            string[] renglonValores=renglon.Split('|');
                            Entidades.Analito analito = new Entidades.Analito();
                            analito.Nombre= renglonValores[2].ToString().Substring(3);
                            analito.Valor= renglonValores[3].ToString();
                            l_analitos.Add(analito);
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
