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
    public class HabitacionesDatos
    {
        ConexionDatos conexionDatos = new ConexionDatos();

        /*--- Consultar ---*/
        public DataTable MtdConsultar()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = conexionDatos.MtdConexionBDD())
            {
                using (SqlCommand cmd = new SqlCommand("usp_ConsultarHabitacion", conn))
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
        /*-- Agregar --*/
        public string MtdAgregarHabitacion(HabitacionesEntidad habitacion)
        {
            using (SqlConnection conn = conexionDatos.MtdConexionBDD())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("usp_AgregarHabitacion", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Numero", habitacion.Numero);
                    cmd.Parameters.AddWithValue("@Ubicacion", habitacion.Ubicacion);
                    cmd.Parameters.AddWithValue("@Tipo", habitacion.Tipo);
                    cmd.Parameters.AddWithValue("@PetFriendly", habitacion.PetFriendly);
                    cmd.Parameters.AddWithValue("@CantidadHuesped", habitacion.CantidadHuesped);
                    cmd.Parameters.AddWithValue("@Precio", habitacion.Precio);
                    cmd.Parameters.AddWithValue("@PorcentajeAnticipo", habitacion.PorcentajeAnticipo);
                    cmd.Parameters.AddWithValue("@Estado", habitacion.Estado);

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
        /* ---- EDITAR ---- */
        public string MtdEditarHabitacion(HabitacionesEntidad habitacion)
        {
            using (SqlConnection conn = conexionDatos.MtdConexionBDD())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("usp_EditarHabitacion", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CodigoHabitacion", habitacion.CodigoHabitacion);
                    cmd.Parameters.AddWithValue("@Numero", habitacion.Numero);
                    cmd.Parameters.AddWithValue("@Ubicacion", habitacion.Ubicacion);
                    cmd.Parameters.AddWithValue("@Tipo", habitacion.Tipo);
                    cmd.Parameters.AddWithValue("@PetFriendly", habitacion.PetFriendly);
                    cmd.Parameters.AddWithValue("@CantidadHuesped", habitacion.CantidadHuesped);
                    cmd.Parameters.AddWithValue("@Precio", habitacion.Precio);
                    cmd.Parameters.AddWithValue("@PorcentajeAnticipo", habitacion.PorcentajeAnticipo);
                    cmd.Parameters.AddWithValue("@Estado", habitacion.Estado);

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
        /* ---- ELIMINAR HABITACIÓN ---- */
        public string MtdEliminarHabitacion(int codigoHabitacion)
        {
            using (SqlConnection conn = conexionDatos.MtdConexionBDD())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("usp_EliminarHabitacion", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CodigoHabitacion", codigoHabitacion);

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
        public DataTable MtdBuscarHabitacion(string numero)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = conexionDatos.MtdConexionBDD())
            {
                using (SqlCommand cmd = new SqlCommand("usp_BuscarHabitacion", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Numero", numero);

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
