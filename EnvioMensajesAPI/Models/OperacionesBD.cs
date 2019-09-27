using EnvioMensajesAPI;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

class OperacionesBD
    {
        SqlConnection con;
        String conexionString;
        private readonly IOptions<ConexionString> strString;

        public OperacionesBD()
        {
            this.strString = BasicAuth.connexion;
            conexionString = this.strString.Value.ConnectionString;
            con = new SqlConnection(conexionString);
        }

        private void openConexion()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
        }

        private void closeConexion()
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }

        
        public bool execQuery(string query, string db_base)
        {
            try
            {
                openConexion();
                con.ChangeDatabase(db_base);
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.Text;
                com.CommandText = query;
                int resultado = com.ExecuteNonQuery();
                closeConexion();
                if (resultado > 0) return true;
                return false;
            }
            catch (Exception ex)
            {
                Bitacora bita = new Bitacora();
                bita.escribirBitacoraError(ex, "OperacionesDB", "execQuery");
                closeConexion();
                return false;
            }
        }

        public DataTable getTableByQuery(String query, String db_base)
        {
            try
            {
                DataTable dataTable = new DataTable();
                openConexion();
                con.ChangeDatabase(db_base);
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.Text;
                com.CommandText = query;
                SqlDataReader reader = com.ExecuteReader();
                dataTable.Load(reader);
                closeConexion();
                return dataTable;
            }
            catch (Exception ex)
            {
                Bitacora bita = new Bitacora();
                bita.escribirBitacoraError(ex, "OperacionesDB", "getTableByQuery");
                closeConexion();
                return null;
            }
        }


        public int runBySP(string db_base, string nombre_sp, List<SqlParameter> list_parametros)
        {
            try
            {
                openConexion();
                con.ChangeDatabase(db_base);

                SqlCommand cmd = new SqlCommand(nombre_sp, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                foreach (SqlParameter pm in list_parametros)
                {
                    cmd.Parameters.Add(pm);
                }

                int resultado = cmd.ExecuteNonQuery();


                closeConexion();
                return resultado;
            }
            catch (Exception ex)
            {
                closeConexion();
                Bitacora bita = new Bitacora();
                bita.escribirBitacoraError(ex, "OperacionesDB", "getTableBySP");
                return -1;
            }

        }

        public DataTable getTableBySP(string db_base, string nombre_sp, List<SqlParameter> list_parametros)
        {
            try
            {
                DataTable tblRes = new DataTable();
                openConexion();
                con.ChangeDatabase(db_base);

                SqlCommand cmd = new SqlCommand(nombre_sp, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                foreach (SqlParameter pm in list_parametros)
                {
                    cmd.Parameters.Add(pm);
                }

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(tblRes);
                closeConexion();
                return tblRes;
            }
            catch (Exception ex)
            {
                closeConexion();
                Bitacora bita = new Bitacora();
                bita.escribirBitacoraError(ex, "OperacionesDB", "getTableBySP");
                return null;
            }

        }

        public SqlParameter crearParametro(string nombre, SqlDbType tipo, object value)
        {
            var sqlParameter = new SqlParameter(nombre, tipo);
            sqlParameter.Value = value;
            return sqlParameter;

        }

    }

