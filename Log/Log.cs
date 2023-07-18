using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Log
{
    public class Log
    {
        private string pathLog;
        private string separador = "-----------------------------------------------------------------------------------------------------------------------------------------------";
        public string PathLog
        {
            get { return pathLog; }
            set { pathLog = value; }
        }
        private string archivo;

        private string extension;

        string archivoLog = "";
        /// <summary>
        /// nombre del archivo por defecto "log"
        /// extension del archivo por defecto "txt"
        /// </summary>
        /// <param name="path"></param>
        public Log(string path)
        {
            this.pathLog = path;
            archivo = "log";
            extension = "txt";
            PathLog = path;
            archivoLog = path + "\\" + archivo + "." + extension;
        }
        /// <summary>
        /// Ingrese donde se guardar el archivo log. El nombre por defecto y extension log.txt
        /// </summary>
        /// <param name="pathLog"></param>
        public void iniciar()
        {
            if (!Directory.Exists(pathLog))
            {
                Directory.CreateDirectory(pathLog);
            }
            archivoLog = @pathLog+"\\"+archivo+"."+extension;
            if (!File.Exists(archivoLog)) File.CreateText(archivoLog);
        }
        /// <summary>
        /// 
        /// </summary>
        public void limpiar()
        {
            if (Directory.Exists(pathLog))
            {
                string log = @pathLog + "\\" + archivo+"."+extension;
                if (File.Exists(log))
                {
                    FileInfo fileInfo = new FileInfo(log);
                    if (fileInfo.Length > 1000000)
                    {
                        try
                        {
                            string renombrarArchivo = archivo + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString()+"."+extension;
                            string log2 = @pathLog + "\\" + renombrarArchivo;
                            File.Move(log, log2);
                            iniciar();
                        }
                        catch
                        {

                        }
                    }
                }
                else
                {
                    iniciar();
                }
            }
            else
            {
                iniciar();
            }
        }
        /// <summary>
        /// Escribir el log..
        /// </summary>
        /// <param name="renglon"></param>
        public void escribirLog(string renglon)
        {

            try
            {
                limpiar();
                StreamWriter archivoEscribir = new StreamWriter(archivoLog, true);
                archivoEscribir.WriteLine(separador);
                archivoEscribir.WriteLine(DateTime.Now.ToString());
                archivoEscribir.WriteLine(renglon);
                archivoEscribir.WriteLine(separador);
                archivoEscribir.Close();
            }
            catch { 
            }
        }

    }
}
