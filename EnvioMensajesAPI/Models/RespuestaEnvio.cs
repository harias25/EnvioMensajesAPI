using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public class RespuestaEnvio
{
    private int codigoResultado;
    private String mensajeResultado;

    public int CodigoResultado
    {
        get
        {
            return codigoResultado;
        }

        set
        {
            codigoResultado = value;
        }
    }

    public string MensajeResultado
    {
        get
        {
            return mensajeResultado;
        }

        set
        {
            mensajeResultado = value;
        }
    }

}