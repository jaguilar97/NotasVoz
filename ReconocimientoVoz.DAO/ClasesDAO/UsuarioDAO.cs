using ReconocimientoVoz.EN.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReconocimientoVoz.DAO.ClasesDAO
{
    public class UsuarioDAO
    {

        public static UsuarioDAO instance()
        {
            return new UsuarioDAO();
        }

        string sentencia = "";
        int respuesta = 0;

        public int RegistrarUsuario(UsuarioEN usu)
        {
            try
            {
                int validacion = validarUsuarioExiste(usu.Usu_Login);

                if (validacion > 0)
                {
                    return -2;
                }

                sentencia = "INSERT INTO Usuario VALUES (@Usu_Login, @Usu_Nombre, @Usu_Apellido, @Usu_Password)";

                List<IDataParameter> parametros = new List<IDataParameter>();
                parametros.Add(new SqlParameter("@Usu_Login", SqlDbType.NVarChar) { Value = usu.Usu_Login });
                parametros.Add(new SqlParameter("@Usu_Nombre", SqlDbType.NVarChar) { Value = usu.Usu_Nombre });
                parametros.Add(new SqlParameter("@Usu_Apellido", SqlDbType.NVarChar) { Value = usu.Usu_Apellido });
                parametros.Add(new SqlParameter("@Usu_Password", SqlDbType.NVarChar) { Value = usu.Usu_Password });

                respuesta = ConexionDAO.instance().ejecutarSentencia(sentencia, parametros);

                return respuesta;

            } catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return -1;
            }
        }

        public int ModificarUsuario(UsuarioEN usu)
        {
            try
            {

                if (usu.Usu_Password != "" && usu.Usu_PasswordActual != "")
                {
                    int validacion = validarContraseña(usu.Usu_Login, usu.Usu_PasswordActual);

                    if (validacion == 0)
                    {
                        return -2;
                    }

                    sentencia = "UPDATE Usuario SET Usu_Nombre = @Usu_Nombre, Usu_Apellido = @Usu_Apellido, Usu_Password = @Usu_Password WHERE Usu_Login = @Usu_Login;";
                } else
                {
                    sentencia = "UPDATE Usuario SET Usu_Nombre = @Usu_Nombre, Usu_Apellido = @Usu_Apellido WHERE Usu_Login = @Usu_Login";
                }

                List<IDataParameter> parametros = new List<IDataParameter>();
                parametros.Add(new SqlParameter("@Usu_Login", SqlDbType.NVarChar) { Value = usu.Usu_Login });
                parametros.Add(new SqlParameter("@Usu_Nombre", SqlDbType.NVarChar) { Value = usu.Usu_Nombre });
                parametros.Add(new SqlParameter("@Usu_Apellido", SqlDbType.NVarChar) { Value = usu.Usu_Apellido });
                if (usu.Usu_Password != "" && usu.Usu_PasswordActual != "")
                {
                    parametros.Add(new SqlParameter("@Usu_Password", SqlDbType.NVarChar) { Value = usu.Usu_Password });
                }

                respuesta = ConexionDAO.instance().ejecutarSentencia(sentencia, parametros);

                return respuesta;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return -1;
            }
        }

        public int validarContraseña(string usu, string pass)
        {
            try
            {
                sentencia = "SELECT COUNT(*) FROM Usuario WHERE Usu_Login = @Usu_Login AND Usu_Password = @Usu_Password";

                List<IDataParameter> parametros = new List<IDataParameter>();
                parametros.Add(new SqlParameter("@Usu_Login", SqlDbType.NVarChar) { Value = usu });
                parametros.Add(new SqlParameter("@Usu_Password", SqlDbType.NVarChar) { Value = pass });

                respuesta = ConexionDAO.instance().ejecutarScalar(sentencia, parametros);

                return respuesta;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return -1;
            }
        }

        public int IniciarSesion(UsuarioEN usu)
        {
            try
            {
                sentencia = "SELECT COUNT(*) FROM Usuario WHERE Usu_Login = @Usu_Login AND Usu_Password = @Usu_Password";

                List<IDataParameter> parametros = new List<IDataParameter>();
                parametros.Add(new SqlParameter("@Usu_Login", SqlDbType.NVarChar) { Value = usu.Usu_Login });
                parametros.Add(new SqlParameter("@Usu_Password", SqlDbType.NVarChar) { Value = usu.Usu_Password });

                respuesta = ConexionDAO.instance().ejecutarScalar(sentencia, parametros);

                return respuesta;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return -1;
            }
        }

        public UsuarioEN RecuperarDatosUsuario(string usuario)
        {

            UsuarioEN usu = new UsuarioEN();

            try
            {
                sentencia = "SELECT Usu_Nombre, Usu_Apellido FROM Usuario WHERE Usu_Login = @Usu_Login";

                List<IDataParameter> parametros = new List<IDataParameter>();
                parametros.Add(new SqlParameter("@Usu_Login", SqlDbType.NVarChar) { Value = usuario });

                SqlDataReader respuestaSDR = ConexionDAO.instance().ejecutarSqlReader(sentencia, parametros);

                if (respuestaSDR.Read())
                {
                    usu.Usu_Nombre = respuestaSDR.GetString(0);
                    usu.Usu_Apellido = respuestaSDR.GetString(1);
                }

                respuestaSDR.Close();

                return usu;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                usu.Usu_Nombre = "";
                usu.Usu_Apellido = "";
                return usu;
            }
        }

        public int validarUsuarioExiste(string usu)
        {
            try
            {
                sentencia = "SELECT COUNT(*) FROM Usuario WHERE Usu_Login = @Usu_Login";

                List<IDataParameter> parametros = new List<IDataParameter>();
                parametros.Add(new SqlParameter("@Usu_Login", SqlDbType.NVarChar) { Value = usu });

                respuesta = ConexionDAO.instance().ejecutarScalar(sentencia, parametros);

                return respuesta;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return -1;
            }
        }
    }
}
