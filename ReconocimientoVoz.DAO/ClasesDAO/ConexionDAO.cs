using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace ReconocimientoVoz.DAO.ClasesDAO
{
    public class ConexionDAO
    {

        public static ConexionDAO instance()
        {
            return new ConexionDAO();
        }

        string conection = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;

        public int ejecutarSentencia(string sentencia, List<IDataParameter> parametros)
        {
            using (SqlConnection connection = new SqlConnection(conection))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sentencia, connection))
                    {

                        if (parametros != null)
                        {
                            foreach (var param in parametros)
                            {
                                command.Parameters.Add(param);
                            }
                        }

                        int filasAfectadas = command.ExecuteNonQuery();
                        return filasAfectadas;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return -1;
                }
            }
        }

        public int ejecutarScalar(string sentencia, List<IDataParameter> parametros)
        {
            using (SqlConnection connection = new SqlConnection(conection))
            {
                int resultado = -1;
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sentencia, connection))
                    {

                        if (parametros != null)
                        {
                            foreach (var param in parametros)
                            {
                                command.Parameters.Add(param);
                            }
                        }

                        object resultadoScalar = command.ExecuteScalar();
                        if (resultadoScalar != null && resultadoScalar != DBNull.Value)
                        {
                            resultado = Convert.ToInt32(resultadoScalar);
                            return resultado;
                        } else
                        {
                            return 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return resultado;
                }
            }
        }

        public SqlDataReader ejecutarSqlReader(string sentencia, List<IDataParameter> parametros)
        {
            SqlConnection connection = new SqlConnection(conection);
            try
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sentencia, connection))
                {

                    if (parametros != null)
                    {
                        foreach (var param in parametros)
                        {
                            command.Parameters.Add(param);
                        }
                    }

                    SqlDataReader resultado = command.ExecuteReader();
                    return resultado;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

    }
}
