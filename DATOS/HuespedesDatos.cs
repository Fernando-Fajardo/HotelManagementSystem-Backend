using MODELO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class HuespedesDatos
    {
        ConexionDatos conexionDatos = new ConexionDatos();

        /*  ---- CONSULTAR  ----  */
        public DataTable MtdConsultar()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = conexionDatos.MtdConexionBDD())
            {
                using (SqlCommand cmd = new SqlCommand("usp_ConsultarHuespedes", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        conn.Open();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }

            return dt;
        }
        /*  ---- AGREGAR  ----  */
        public string MtdAgregarHuespedes(HuespedesEntidad huesped)
        {
                using( SqlConnection conn = conexionDatos.MtdConexionBDD())
                {
                conn.Open();

                using ( SqlCommand cmd = new SqlCommand ("usp_AgregarHuespedes", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Nombre",huesped.Nombre);
                    cmd.Parameters.AddWithValue("@TipoIdentificacion",huesped.TipoIdentificacion);
                    cmd.Parameters.AddWithValue("@NumeroIdentificacion",huesped.NumeroIdentificacion);
                    cmd.Parameters.AddWithValue("@FechaNacimiento",huesped.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@Genero",huesped.Genero);
                    cmd.Parameters.AddWithValue("@Telefono",huesped.Telefono);
                    cmd.Parameters.AddWithValue("@Direccion",huesped.Direccion);
                    cmd.Parameters.AddWithValue("@Puntuacion",huesped.Puntuacion);
                    cmd.Parameters.AddWithValue("@Estado",huesped.Estado);

                    var pResultado = new SqlParameter("@Resultado", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    var pMensaje = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 500)
                    {
                        Direction = ParameterDirection.Output
                    };

                    cmd.Parameters.Add(pResultado);
                    cmd.Parameters.Add(pMensaje);

                    cmd.ExecuteNonQuery();

                    bool resultado = Convert.ToBoolean(pResultado.Value);
                    string mensaje = pMensaje.Value?.ToString() ?? "Sin mensaje del servidor";

                    if (!resultado)
                        throw new Exception(mensaje);

                    return mensaje;
                }
            }
        }

        /*  ---- EDITAR  ----  */
        public string MtdEditarHuesped(HuespedesEntidad huesped)
        {
            using (SqlConnection conn = conexionDatos.MtdConexionBDD())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("usp_EditarHuespedes", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CodigoHuesped", huesped.CodigoHuesped);
                    cmd.Parameters.AddWithValue("@Nombre", huesped.Nombre);
                    cmd.Parameters.AddWithValue("@TipoIdentificacion", huesped.TipoIdentificacion);
                    cmd.Parameters.AddWithValue("@NumeroIdentificacion", huesped.NumeroIdentificacion);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", huesped.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@Genero", huesped.Genero);
                    cmd.Parameters.AddWithValue("@Telefono", huesped.Telefono);
                    cmd.Parameters.AddWithValue("@Direccion", huesped.Direccion);
                    cmd.Parameters.AddWithValue("@Puntuacion", huesped.Puntuacion);
                    cmd.Parameters.AddWithValue("@Estado", huesped.Estado);

                    var pResultado = new SqlParameter("@Resultado", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    var pMensaje = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 500)
                    {
                        Direction = ParameterDirection.Output
                    };

                    cmd.Parameters.Add(pResultado);
                    cmd.Parameters.Add(pMensaje);

                    cmd.ExecuteNonQuery();

                    bool resultado = Convert.ToBoolean(pResultado.Value);
                    string mensaje = pMensaje.Value?.ToString() ?? "Sin mensaje del servidor";

                    if (!resultado)
                        throw new ApplicationException(mensaje);

                    return mensaje;
                }
            }
        }
        /* ---- ELIMINAR ---- */
        public string MtdEliminarHuesped(int codigoHuesped)
        {
            using (SqlConnection conn = conexionDatos.MtdConexionBDD())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("usp_EliminarHuespedes", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CodigoHuesped", codigoHuesped);

                    var pResultado = new SqlParameter("@Resultado", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    var pMensaje = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 500)
                    {
                        Direction = ParameterDirection.Output
                    };

                    cmd.Parameters.Add(pResultado);
                    cmd.Parameters.Add(pMensaje);

                    cmd.ExecuteNonQuery();

                    bool resultado = Convert.ToBoolean(pResultado.Value);
                    string mensaje = pMensaje.Value?.ToString() ?? "Sin mensaje del servidor";

                    if (!resultado)
                        throw new Exception(mensaje);

                    return mensaje;
                }
            }
        }
        /*-- Buscar --*/
        public DataTable MtdBuscarHuesped(string huesped)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = conexionDatos.MtdConexionBDD())
            {
                using (SqlCommand cmd = new SqlCommand("usp_BuscarHuespedes", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Nombre", huesped);

                    try
                    {
                        conn.Open();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }

            return dt;
        }
    }
}