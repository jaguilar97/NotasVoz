using ReconocimientoVoz.EN;
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
    public class NotaDAO
    {
        public static NotaDAO instance() {
            return new NotaDAO();
        }

        string sentencia = "";
        int respuesta = 0;
        DateTime fechaActual = DateTime.Now;
        int iteraciones = 1;
        string tituloOriginal = "";

        public string GuardarNota(NotaEN nota)
        {
            try
            {
                if (iteraciones == 1)
                {
                    tituloOriginal = nota.Titulo;
                }

                int validacion = validarNotaExiste(nota.Titulo);

                if (validacion > 0)
                {
                    iteraciones = iteraciones + 1;
                    nota.Titulo = tituloOriginal + " - (" + iteraciones + ")";
                    return GuardarNota(nota);
                }

                if (iteraciones > 1)
                {
                    nota.Titulo = tituloOriginal + " - (" + iteraciones + ")";
                }

                sentencia = "INSERT INTO Nota VALUES (@Usu_Login, @Titulo, @Contenido, @Fecha, @Prioridad);";

                List<IDataParameter> parametros = new List<IDataParameter>();
                parametros.Add(new SqlParameter("@Usu_Login", SqlDbType.NVarChar) { Value = nota.Usu_Login });
                parametros.Add(new SqlParameter("@Titulo", SqlDbType.NVarChar) { Value = nota.Titulo });
                parametros.Add(new SqlParameter("@Contenido", SqlDbType.NVarChar) { Value = nota.Contenido });
                parametros.Add(new SqlParameter("@Fecha", SqlDbType.DateTime) { Value = fechaActual });
                parametros.Add(new SqlParameter("@Prioridad", SqlDbType.Int) { Value = nota.Prioridad });

                respuesta = ConexionDAO.instance().ejecutarSentencia(sentencia, parametros);

                if (respuesta > 0)
                {
                    tituloOriginal = "";
                    iteraciones = 1;
                    return "Nota creada con éxito";
                } else
                {
                    tituloOriginal = "";
                    iteraciones = 1;
                    return "No fue posible crear la nota, verifique con su proveedor";
                }

            } catch (Exception ex)
            {
                tituloOriginal = "";
                iteraciones = 1;
                Console.WriteLine("Error: " + ex);
                return "Ocurrió un error: " + ex;
            }
        }

        public string ActualizarNota(NotaEN nota)
        {
            try
            {
                sentencia = "UPDATE Nota SET Titulo = @Titulo, Contenido = @Contenido, Prioridad = @Prioridad WHERE NotaId = @NotaId;";

                List<IDataParameter> parametros = new List<IDataParameter>();
                parametros.Add(new SqlParameter("@NotaId", SqlDbType.NVarChar) { Value = nota.NotaId });
                parametros.Add(new SqlParameter("@Titulo", SqlDbType.NVarChar) { Value = nota.Titulo });
                parametros.Add(new SqlParameter("@Contenido", SqlDbType.NVarChar) { Value = nota.Contenido });
                parametros.Add(new SqlParameter("@Prioridad", SqlDbType.Int) { Value = nota.Prioridad });

                respuesta = ConexionDAO.instance().ejecutarSentencia(sentencia, parametros);

                if (respuesta > 0)
                {
                    return "Nota actualizada con éxito";
                }
                else
                {
                    return "No fue posible actualizar la nota, verifique con su proveedor";
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
                return "Ocurrió un error: " + ex;
            }
        }

        public string EliminarNota(int NotaId)
        {
            try
            {
                sentencia = "DELETE FROM Nota WHERE NotaId = @NotaId";

                List<IDataParameter> parametros = new List<IDataParameter>();
                parametros.Add(new SqlParameter("@NotaId", SqlDbType.Int) { Value = NotaId });

                respuesta = ConexionDAO.instance().ejecutarSentencia(sentencia, parametros);

                if (respuesta > 0)
                {
                    return "Nota eliminada con éxito";
                }
                else
                {
                    return "No fue posible eliminar la nota, verifique con su proveedor";
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
                return "Ocurrió un error: " + ex;
            }
        }

        public Reply<List<NotaEN>> ListadoNotas(string usuario)
        {
            var listado = new List<NotaEN>();
            var reply = new Reply<List<NotaEN>>();
            try
            {
                sentencia = "SELECT * FROM Nota WHERE Usu_Login = @Usu_Login ORDER BY Prioridad asc, NotaId desc;";

                List<IDataParameter> parametros = new List<IDataParameter>();
                parametros.Add(new SqlParameter("@Usu_Login", SqlDbType.NVarChar) { Value = usuario });

                SqlDataReader respuestaSDR = ConexionDAO.instance().ejecutarSqlReader(sentencia, parametros);

                while (respuestaSDR.Read())
                {
                    NotaEN nota = new NotaEN();
                    nota.NotaId = respuestaSDR.GetInt32(0);
                    nota.Usu_Login = respuestaSDR.GetString(1);
                    nota.Titulo = respuestaSDR.GetString(2);
                    nota.Contenido = respuestaSDR.GetString(3);
                    nota.Fecha = respuestaSDR.GetDateTime(4);
                    nota.Prioridad = respuestaSDR.GetInt32(5);
                    listado.Add(nota);
                }

                reply.obj = listado;
                if (reply.obj != null)
                {
                    reply.ok = true;
                    reply.msg = "Listado de notas recuperado con éxito";
                }
                else if (reply.obj == null)
                {
                    reply.ok = false;
                    reply.msg = "No se pudo obtener el listado de notas";
                }

                return reply;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
                reply.obj = null;
                reply.ok = false;
                reply.msg = "Ocurrió un error:" + ex;
                return reply;
            }
        }

        public Reply<List<NotaEN>> RecuperarNota(int NotaId)
        {
            var listado = new List<NotaEN>();
            var reply = new Reply<List<NotaEN>>();
            try
            {
                sentencia = "SELECT * FROM Nota WHERE @NotaId = NotaId;";

                List<IDataParameter> parametros = new List<IDataParameter>();
                parametros.Add(new SqlParameter("@NotaId", SqlDbType.Int) { Value = NotaId });

                SqlDataReader respuestaSDR = ConexionDAO.instance().ejecutarSqlReader(sentencia, parametros);

                while (respuestaSDR.Read())
                {
                    NotaEN nota = new NotaEN();
                    nota.NotaId = respuestaSDR.GetInt32(0);
                    nota.Usu_Login = respuestaSDR.GetString(1);
                    nota.Titulo = respuestaSDR.GetString(2);
                    nota.Contenido = respuestaSDR.GetString(3);
                    nota.Fecha = respuestaSDR.GetDateTime(4);
                    nota.Prioridad = respuestaSDR.GetInt32(5);
                    listado.Add(nota);
                }

                reply.obj = listado;
                if (reply.obj != null)
                {
                    reply.ok = true;
                    reply.msg = "Nota recuperada con éxito";
                }
                else if (reply.obj == null)
                {
                    reply.ok = false;
                    reply.msg = "No se pudo recuperar la nota";
                }

                return reply;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
                reply.obj = null;
                reply.ok = false;
                reply.msg = "Ocurrió un error:" + ex;
                return reply;
            }
        }

        public int validarNotaExiste(string Titulo)
        {
            try
            {
                sentencia = "SELECT COUNT(*) FROM Nota WHERE Titulo = @Titulo;";

                List<IDataParameter> parametros = new List<IDataParameter>();
                parametros.Add(new SqlParameter("@Titulo", SqlDbType.NVarChar) { Value = Titulo });

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
