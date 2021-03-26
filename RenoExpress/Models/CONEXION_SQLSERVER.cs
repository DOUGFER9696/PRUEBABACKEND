using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
namespace RenoExpress.Models
{
    public class CONEXION_SQLSERVER
    {
        private SqlConnection conexion;
        private SqlDataAdapter adaptador;
        private DataTable informacion;


        public void EjecutarComandos(SqlCommand comando)
        {
            try
            {
                conexion = new SqlConnection();
                conexion.ConnectionString = "data source=DOUGLASROQUE;initial catalog=RenoExpress;user id=sa;password=DOUG9696*";
                conexion.Open();
                comando.Connection = conexion;
                comando.ExecuteNonQuery();

            }
            catch (SqlException error)
            {
                throw new System.ArgumentException(error.ToString());
            }
            finally
            {
                conexion.Close();
            }

        }




        public DataTable EjecutarComandosInformacion(SqlCommand comando)
        {
            try
            {
                adaptador = new SqlDataAdapter();
                informacion = new DataTable();
                conexion = new SqlConnection();
                conexion.ConnectionString = "data source=DOUGLASROQUE;initial catalog=RenoExpress;user id=sa;password=DOUG9696*";
                conexion.Open();
                comando.Connection = conexion;
                comando.ExecuteNonQuery();
                adaptador.SelectCommand = comando;
                adaptador.Fill(informacion);
                return informacion;

            }
            catch (SqlException error)
            {
                throw new System.ArgumentException(error.ToString());
            }
            finally
            {
                conexion.Close();
            }

        }
    }
}