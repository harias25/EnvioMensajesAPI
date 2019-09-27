using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de ResultadoTigo
/// </summary>
public class ResultadoTigo
{

    private int sms_sent;
    private String sms_message;
    private int code;
    private String error;


    public ResultadoTigo()
    {
        sms_sent = -1;
        sms_message = "";
        code = -1;
        error = "";
    }

    public int Sms_sent
    {
        get
        {
            return sms_sent;
        }

        set
        {
            sms_sent = value;
        }
    }

    public string Sms_message
    {
        get
        {
            return sms_message;
        }

        set
        {
            sms_message = value;
        }
    }

    public int Code
    {
        get
        {
            return code;
        }

        set
        {
            code = value;
        }
    }

    public string Error
    {
        get
        {
            return error;
        }

        set
        {
            error = value;
        }
    }
}