using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using EnvioMensajesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EnvioMensajesAPI.Controllers
{
    [Route("api/[controller]")]
    public class MensajeController : Controller
    {
        [HttpPost]
        public string SendMessage([FromBody]Request request)
        {
            if (request == null || (request.Numero == null && request.Mensaje == null) || request.Numero == "" || request.Mensaje == "")
                return JsonConvert.SerializeObject(new { CodigoResultado = -1, MensajeResultado = "Solicitud Incorrecta" });

            return request.enviaMensajeTexto();
        }
    }
}
