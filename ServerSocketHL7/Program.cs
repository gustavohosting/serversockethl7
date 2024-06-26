﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using Entidades;
using System.Net.NetworkInformation;

namespace MultiThreadedTcpEchoServer
{
    public class Program
    {
            static void Main(string[] args)
            {
                var ourHl7Server = new OurSimpleMultiThreadedTcpServer();
                ourHl7Server.StartOurTcpServer(3000);

                Console.WriteLine("Press any key to exit program...");
                Console.ReadLine();
            }
    }

    class OurSimpleMultiThreadedTcpServer
    {
        HL7.HL7 hl7 = new HL7.HL7();
        BD.grabarKern grabar = new BD.grabarKern();
        private TcpListener _tcpListener;
        private bool conexion = false;
        private bool conexionPermiso = false;
        private bool recibiendoResultados= false;
        private string mensajeResultados = string.Empty;
        private string mensajeResultados2 = string.Empty;
        private string mensajeFin = CR+"L|1";
        private static char END_OF_BLOCK = '\u001c';
        private static char START_OF_BLOCK = '\u000b';
        private static char CARRIAGE_RETURN = (char)13;
        private static char EOT = (char)4;
        private static char ENQ = (char)5;
        private static char ACK = (char)6;
        private static char LF = (char)10;
        private static char CR = (char)13;
        private string mensajeEspacio = "     ";
        private Log.Log log = new Log.Log("c:\\interfazLog"); 
        /// <summary>
        /// Espéra las conexiones entrantes
        /// </summary>
        /// <param name="portNumberToListenOn"></param>
        public void StartOurTcpServer(int portNumberToListenOn)
        {
            try
            {
                //NetworkInterface[] adaptadores = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
                //foreach (NetworkInterface adaptador in adaptadores)
                //{
                //    if (adaptador.NetworkInterfaceType.ToString().IndexOf("Ethernet")>-1)
                //    {
                //        Console.WriteLine("Adaptador Nombre {0}", adaptador.Name);
                //        Console.WriteLine("          Descripcion {0}", adaptador.Description);
                //        Console.WriteLine("          Tipo {0}", adaptador.NetworkInterfaceType);
                //        IPInterfaceProperties adaptadorPropiedades = adaptador.GetIPProperties();
                //        MulticastIPAddressInformationCollection multicasts = adaptadorPropiedades.MulticastAddresses;
                //        if (multicasts.Count > 0)
                //        {
                //            foreach (IPAddressInformation multi in multicasts)
                //            {
                //                Console.WriteLine("          address {0}", multi.Address);
                //                //multi.
                //            }
                //        }
                //    }
                //}
                string ip = "192.168.1.106";
                if (true)
                {
                    //IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
                    //ip = host.AddressList[0].ToString() ;
                    _tcpListener = new TcpListener(IPAddress.Parse(ip), portNumberToListenOn);
                    //start the TCP listener that we have instantiated
                    _tcpListener.Start();
                    Console.WriteLine("Grabando log en " + log.PathLog);
                    Console.WriteLine("Server escuchando en IP -->" + ip + " port:" + portNumberToListenOn);
                    //leerHl7.archivoLeer();
                    while (true)
                    {
                        //wait for client connections to come in
                        var incomingTcpClientConnection = _tcpListener.AcceptTcpClient();

                        Console.WriteLine(mensajeEspacio + "-->Accepted incoming client connection...");

                        //create a new thread to process this client connection
                        var clientProcessingThread = new Thread(ProcessClientConnection);

                        //start processing client connections to this server
                        clientProcessingThread.Start(incomingTcpClientConnection);
                    }
                }
                else
                { Console.WriteLine("No se encontro interface local de red ethernet"); }
            }
            catch (Exception ex)
            {
                //print any exceptions during the communications to the console
                Console.WriteLine(ex.Message);
            }
            finally
            {
                //stop the TCP listener before you dispose of it
                _tcpListener?.Stop();
            }
        }
        /// <summary>
        /// Procesa los mensajes del cliente
        /// </summary>
        /// <param name="argumentPassedForThreadProcessing"></param>
        private void ProcessClientConnection(object argumentPassedForThreadProcessing)
        {
            //the argument passed to the thread delegate is the incoming tcp client connection
            var tcpClientConnection = (TcpClient)argumentPassedForThreadProcessing;
            Console.WriteLine(mensajeEspacio+ "-->A client connection was initiated from " + tcpClientConnection.Client.RemoteEndPoint);
            var receivedByteBuffer = new byte[5000];
            var netStream = tcpClientConnection.GetStream();
            try
            {
                // Keep receiving data from the client closes connection
                int bytesReceived; // Received byte count
                var hl7Data = string.Empty;
                //keeping reading until there is data available from the client and echo it back
                while ((bytesReceived = netStream.Read(receivedByteBuffer, 0, receivedByteBuffer.Length)) > 0)
                {
                    hl7Data += Encoding.UTF8.GetString(receivedByteBuffer, 0, bytesReceived);
                    if (hl7Data.Length == 1 && hl7Data.IndexOf(ENQ) == 0 && !conexion)
                    {
                        //-----------------------------------------------------------------
                        //equipo quiere establecer conexion!!
                        //-----------------------------------------------------------------
                        Console.WriteLine(mensajeEspacio + "-->ENQ del equipo para establecer conexion");
                        var ackMessage = GetAckMessage();
                        var buffer = Encoding.UTF8.GetBytes(ackMessage);
                        if (netStream.CanWrite)
                        {
                            netStream.Write(buffer, 0, buffer.Length);
                            Console.WriteLine(mensajeEspacio + "Se envio ACK para establecer conexion-->");
                        }
                        conexionPermiso = true;//habilitar conexion
                        hl7Data = String.Empty;//limpio mensaje

                    }
                    else if (hl7Data.Length == 1 && hl7Data.IndexOf(EOT) == 0 && !conexion)
                    {
                        //-----------------------------------------------------------------
                        //conexion aceptada y establecida por el equipo!!
                        //-----------------------------------------------------------------
                        conexion = true;// conexion acepta
                        hl7Data = String.Empty;
                        Console.WriteLine(mensajeEspacio + "-->EOT del equipo conexion establecida!!");
                    }
                    else if (hl7Data.Length == 1 && hl7Data.IndexOf(ENQ) == 0 && conexion && !recibiendoResultados)
                    {
                        //-----------------------------------------------------------------
                        //equipo quiere enviar resultados
                        //-----------------------------------------------------------------
                        Console.WriteLine(mensajeEspacio + "-->ENQ equipo para enviar resultados");
                        var ackMessage = GetAckMessage();
                        var buffer = Encoding.UTF8.GetBytes(ackMessage);
                        if (netStream.CanWrite)
                        {
                            netStream.Write(buffer, 0, buffer.Length);
                            Console.WriteLine(mensajeEspacio + "Se envio ACK para recibir resultados-->");
                        }
                        mensajeResultados = string.Empty;
                        hl7Data = String.Empty;//limpio mensaje
                        recibiendoResultados = true;
                    }
                    else if (hl7Data.Length > 1 && hl7Data.IndexOf(mensajeFin) >= 0 && conexion && recibiendoResultados)
                    {
                        //-----------------------------------------------------------------
                        //equipo enviado resultados
                        //-----------------------------------------------------------------
                        mensajeResultados += mensajeResultados + hl7Data;
                        hl7Data = String.Empty;//limpio mensaje
                        log.escribirLog(mensajeResultados);
                        recibiendoResultados = false;
                        Console.WriteLine(mensajeEspacio + "-->Fin de mensaje");
                        var ackMessage = GetEOTMessage();
                        var buffer = Encoding.UTF8.GetBytes(ackMessage);
                        if (netStream.CanWrite)
                        {
                            netStream.Write(buffer, 0, buffer.Length);
                            Console.WriteLine(mensajeEspacio + "EOT recepcion completa-->");
                        }
                        //procesar mensaje
                        List<Entidades.Analito> l_analitos = new List<Analito>();
                        hl7.mensajeLeer(mensajeResultados, ref l_analitos);
                        mensajeResultados = "";
                        hl7Data = string.Empty;
                      
                        grabar.grabarResultados(l_analitos);
                        string texto = "";
                        foreach (Entidades.Analito analito in l_analitos)
                        {
                            
                            if (analito.CodigoLis != null)
                            {
                                texto = "Equipo->" + analito.Nombre + " Analito LIS->" + analito.CodigoLis + " Muestra->" + analito.MuestraID + " Resultado->" + analito.Valor;
                                Console.WriteLine(texto);
                            }
                            else {
                                texto = "Equipo->" + analito.Nombre + " Analito LIS-> NO TIENE EQUIVALENCIA O NO SE GRABA  Muestra->" + analito.MuestraID + " Resultado->" + analito.Valor;
                                Console.WriteLine(texto);
                            }
                        }
                        Console.WriteLine("se grabo resultado muestra--->" + l_analitos[0].MuestraID);
                        log.escribirLog("se grabo resultado muestra-->" + l_analitos[0].MuestraID);
                    }
                    else if (hl7Data.Length > 1 && hl7Data.IndexOf(EOT) >= 0 && conexion && recibiendoResultados)
                    {
                        mensajeResultados = String.Empty;
                        hl7Data = String.Empty;//limpio mensaje
                        log.escribirLog("Mensaje finalizado!!");
                        //recibiendoResultados = false;
                        Console.WriteLine(mensajeEspacio + "-->Fin de transmicion");
                        var ackMessage = GetAckMessage();
                        var buffer = Encoding.UTF8.GetBytes(ackMessage);
                        if (netStream.CanWrite)
                        {
                            //netStream.Write(buffer, 0, buffer.Length);
                            Console.WriteLine(mensajeEspacio + "...........>");
                        }
                    }
                    else {
                        // guardar los mensajes hasta el fin
                        mensajeResultados += mensajeResultados + hl7Data;
                        hl7Data = String.Empty;//limpio mensaje
                    }

                }
            }
            catch (Exception e)
            {
                //print any exceptions during the communications to the console
                Console.WriteLine(e.Message);
            }
            finally
            {
                // Close the stream and the connection with the client
                netStream.Close();
                netStream.Dispose();
                tcpClientConnection.Close();
            }
        }
        private string GetAckMessage()
        {
            var ackMessage = new StringBuilder();
            ackMessage = ackMessage.Append(ACK);
            return ackMessage.ToString();
        }
        private string GetEOTMessage()
        {
            var ackMessage = new StringBuilder();
            ackMessage = ackMessage.Append(EOT);
            return ackMessage.ToString();
        }
    }
}