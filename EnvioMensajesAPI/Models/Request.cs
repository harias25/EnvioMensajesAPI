using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnvioMensajesAPI.Models
{
    public class Request
    {
        public String Mensaje { get; set; }
        public String Numero { get; set; }
        public int tipoMensaje { get; set; }
        public String Empresa { get; set; }


        public string enviaMensajeTexto()
        {
            Bitacora bita = new Bitacora();
            try
            {
                bita.iniciarBitacoraMensaje(Mensaje, Numero, tipoMensaje, Empresa);
                EnvioMensaje objMensaje = new EnvioMensaje(Mensaje, Numero, tipoMensaje, Empresa);
                RespuestaEnvio respuesta = objMensaje.generarEnvio();
                
                if (respuesta == null)
                {
                    respuesta = new RespuestaEnvio();
                    respuesta.CodigoResultado = 200;
                    respuesta.MensajeResultado = "No se obtuvo respuesta de envio.";
                }
                bita.escribirBitacora("          Resultado de Envio de Mensaje  --> Codigo:" + respuesta.CodigoResultado + "  Mensaje: " + respuesta.MensajeResultado);
                string json = JsonConvert.SerializeObject(respuesta);
                bita.finalizarBitacoraMensaje();
                return json;
            }
            catch (Exception ex)
            {
                RespuestaEnvio respuesta = new RespuestaEnvio();
                bita.escribirBitacoraError(ex, "wsEnvioMensaje", "enviaMensajeTexto");
                respuesta.CodigoResultado = 100;
                respuesta.MensajeResultado = "Error interno en WS";
                string json = JsonConvert.SerializeObject(respuesta);
                bita.finalizarBitacoraMensaje();
                return json;
            }

        }

    }
}
