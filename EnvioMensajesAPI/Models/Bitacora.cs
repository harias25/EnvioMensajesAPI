using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

 class Bitacora
    {
        private String ruta = "C:\\LogSistemas\\EnvioMensajeAPI";

        public void iniciarBitacoraMensaje(String telefono, String mensaje, int tipo, String Empresa)
        {
            try
            {
                var vrlPathFolderLog = ruta;
                if (!Directory.Exists(vrlPathFolderLog))
                {
                    DirectoryInfo di = Directory.CreateDirectory(vrlPathFolderLog);
                }

                var vrlFechaActual = DateTime.Now.ToString("yyyyMMdd");
                var vrlNombreArchivo = string.Format("{0}/{1}.txt", vrlPathFolderLog, vrlFechaActual);

                StreamWriter sw = new StreamWriter(vrlNombreArchivo, true);
                sw.Write("******************** " + DateTime.Now);
                sw.WriteLine(" ********************");
                sw.WriteLine();
                sw.WriteLine("                    SE INICIA ENVIO DEl MENSAJE ");
                sw.WriteLine("TELEFONO: "+ telefono);
                sw.WriteLine("MENSAJE: "+ mensaje);
                sw.WriteLine("TIPO MENSAJE: "+tipo.ToString());
                sw.WriteLine("EMPRESA: " + Empresa);
                sw.WriteLine();
                sw.Close();
            }
            catch { }
            
        }

        public void finalizarBitacoraMensaje()
        {
            try
            {
                var vrlPathFolderLog = ruta;
                if (!Directory.Exists(vrlPathFolderLog))
                {
                    DirectoryInfo di = Directory.CreateDirectory(vrlPathFolderLog);
                }

                var vrlFechaActual = DateTime.Now.ToString("yyyyMMdd");
                var vrlNombreArchivo = string.Format("{0}/{1}.txt", vrlPathFolderLog, vrlFechaActual);

                StreamWriter sw = new StreamWriter(vrlNombreArchivo, true);
                sw.WriteLine();
                sw.WriteLine("                    SE FINALIZA ENVIO DE MENSAJE            ");
                sw.WriteLine(" ********************************************************* ");
                sw.WriteLine();
                sw.Close();
            }
            catch{ }
            
        }


        public void escribirBitacora(String texto)
        {
            try
            {
                var vrlPathFolderLog = ruta;
                if (!Directory.Exists(vrlPathFolderLog))
                {
                    DirectoryInfo di = Directory.CreateDirectory(vrlPathFolderLog);
                }

                var vrlFechaActual = DateTime.Now.ToString("yyyyMMdd");
                var vrlNombreArchivo = string.Format("{0}/{1}.txt", vrlPathFolderLog, vrlFechaActual);

                StreamWriter sw = new StreamWriter(vrlNombreArchivo, true);
                sw.WriteLine();
                sw.WriteLine(texto);
                sw.WriteLine();
                sw.Close();
            }
            catch{ }
            
        }

        public void escribirBitacoraError(Exception exc, string Clase, string Metodo)
        {
            try
            {
                var vrlPathFolderLog = ruta;
                if (!Directory.Exists(vrlPathFolderLog))
                {
                    DirectoryInfo di = Directory.CreateDirectory(vrlPathFolderLog);
                }

                var vrlFechaActual = DateTime.Now.ToString("yyyyMMdd");
                var vrlNombreArchivo = string.Format("{0}/{1}.txt", vrlPathFolderLog, vrlFechaActual);

                StreamWriter sw = new StreamWriter(vrlNombreArchivo, true);
                sw.Write("******************** " + DateTime.Now);
                sw.WriteLine(" ********************");
                if (exc.InnerException != null)
                {
                    sw.Write("Inner Exception Type: ");
                    sw.WriteLine(exc.InnerException.GetType().ToString());
                    sw.Write("Inner Exception: ");
                    sw.WriteLine(exc.InnerException.Message);
                    sw.Write("Inner Source: ");
                    sw.WriteLine(exc.InnerException.Source);
                    if (exc.InnerException.StackTrace != null)
                        sw.WriteLine("Inner Stack Trace: ");
                    sw.WriteLine(exc.InnerException.StackTrace);
                }
                sw.Write("Exception Type: ");
                sw.WriteLine(exc.GetType().ToString());
                sw.WriteLine("Exception: " + exc.Message);
                sw.WriteLine("Clase: " + Clase);
                sw.WriteLine("Metodo: " + Metodo);
                sw.WriteLine("Stack Trace: ");
                if (exc.StackTrace != null)
                    sw.WriteLine(exc.StackTrace);
                sw.WriteLine();
                sw.WriteLine(" *********************************************************");
                sw.Close();
            }
            catch
            {

            }
            
        }

    }
