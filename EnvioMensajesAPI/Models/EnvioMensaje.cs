using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;

class EnvioMensaje
    {
        private int tipoMensaje;
        private String mensaje;
        private Bitacora bita;
        private int tipo;
        private String numero;
        private String empresa;
        public EnvioMensaje(String strMensaje, String strNumero, int tipoMensaje, String Empresa)
        {
            bita = new Bitacora();
            mensaje = strMensaje;
            numero = strNumero;
            tipo = tipoMensaje;
            empresa = Empresa;
        }
        
        public RespuestaEnvio generarEnvio()
        {

        String link = "";
        if(empresa.ToUpper() == "MED")
        {
            link = "https://comunicador.tigo.com.gt/api/http/send_to_contact?msisdn=502<NUMERO>&message=<MENSAJE>&api_key=tvijdj36ovmf4tkdlveaai93fplfdgrs&id=<ID>";
        }
        else if (empresa.ToUpper() == "AME")
        {
            link = "https://comunicador.tigo.com.gt/api/http/send_to_contact?msisdn=502<NUMERO>&message=<MENSAJE>&api_key=12FepToLKbPgoZXpw94h7SA9Ji6ugEnI&id=<ID>";
        }


        try
        {     
            String id = numero+DateTime.Now.ToString("ddMMyyHHmmss");
            link = link.Replace("<NUMERO>", numero);
            link = link.Replace("<MENSAJE>", mensaje);
            link = link.Replace("<ID>", id);

            WebRequest request = WebRequest.Create(link);
            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            String str = reader.ReadLine();
            RespuestaEnvio respuesta = new RespuestaEnvio();

            if (str == null || str == "")
            {
                respuesta.CodigoResultado = 125;
                respuesta.MensajeResultado = "No se tuvo respuesta del envio del mensaje";
            }
            else
            {
                ResultadoTigo resultadoTigo = JsonConvert.DeserializeObject<ResultadoTigo>(str);

                if (resultadoTigo.Sms_sent == 1 && resultadoTigo.Sms_message != "")
                {
                    respuesta.CodigoResultado = 0;
                    respuesta.MensajeResultado = "Mensaje Enviado Exitosamente";
                }
                else
                {
                    respuesta.CodigoResultado = resultadoTigo.Code;
                    respuesta.MensajeResultado = resultadoTigo.Error;
                }
            }

                OperacionesBD sqlDb = new OperacionesBD();
                List<SqlParameter> parametrosDeEntrada = new List<SqlParameter>();

                parametrosDeEntrada.Add(sqlDb.crearParametro("@i_operacion", SqlDbType.Char, 'A'));
                parametrosDeEntrada.Add(sqlDb.crearParametro("@i_empresa", SqlDbType.NVarChar, empresa));
                parametrosDeEntrada.Add(sqlDb.crearParametro("@i_numero", SqlDbType.NVarChar, numero));
                parametrosDeEntrada.Add(sqlDb.crearParametro("@i_mensajeEnviado", SqlDbType.NVarChar, mensaje));
                parametrosDeEntrada.Add(sqlDb.crearParametro("@i_codigoResultado", SqlDbType.Int, respuesta.CodigoResultado));
                parametrosDeEntrada.Add(sqlDb.crearParametro("@i_mensajeResultado", SqlDbType.NVarChar, respuesta.MensajeResultado));
                parametrosDeEntrada.Add(sqlDb.crearParametro("@i_tipoMensaje", SqlDbType.Int, tipoMensaje));
                int resultado = sqlDb.runBySP("HOSPITAL",
                                                        "sp_admMensajeria",
                                                         parametrosDeEntrada);


            return respuesta;
            }
            catch(Exception ex)
            {
                bita.escribirBitacoraError(ex, "Mensaje", "generarEnvio");
                RespuestaEnvio respuesta = new RespuestaEnvio();
                respuesta.CodigoResultado = 126;
                respuesta.MensajeResultado = "Error en conexión con el proveedor de SMS, contacte a Sistemas";
                return respuesta;
            }
        }

    }
