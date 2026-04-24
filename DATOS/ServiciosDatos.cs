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
    public class ServiciosDatos
    {
        ConexionDatos conexionDatos = new ConexionDatos();

        /* ---- CONSULTAR SERVICIOS ---- */
        public DataTable MtdConsultar()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = conexionDatos.MtdConexionBDD())
            {
                using (SqlCommand cmd = new SqlCommand("usp_ConsultarServicio", conn))
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
        /* ---- AGREGAR ----  */
        public string MtdAgregarServicio(ServiciosEntidad servicio)
        {
            using (SqlConnection conn = conexionDatos.MtdConexionBDD())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("usp_AgregarServicio", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Nombre", servicio.Nombre);
                    cmd.Parameters.AddWithValue("@Tipo", servicio.Tipo);
                    cmd.Parameters.AddWithValue("@Categoria", servicio.Categoria);
                    cmd.Parameters.AddWithValue("@UnidadMedida", servicio.UnidadMedida);
                    cmd.Parameters.AddWithValue("@PrecioUnitario", servicio.PrecioUnitario);
                    cmd.Parameters.AddWithValue("@TiempoPreparacion", servicio.TiempoPreparacion);
                    cmd.Parameters.AddWithValue("@Estado", servicio.Estado);

                    var pResultado = new SqlParameter("@Resultado", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                    var pMensaje = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 500) { Direction = ParameterDirection.Output };

                    cmd.Parameters.Add(pResultado);
                    cmd.Parameters.Add(pMensaje);

                    cmd.ExecuteNonQuery();

                    bool resultado = Convert.ToBoolean(pResultado.Value);
                    string mensaje = pMensaje.Value?.ToString() ?? "Sin mensaje del servidor";

                    if (!resultado) throw new Exception(mensaje);

                    return mensaje;
                }
            }
        }

        /* ---- EDITAR ----  */
        public string MtdEditarServicio(ServiciosEntidad servicio)
        {
            using (SqlConnection conn = conexionDatos.MtdConexionBDD())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("usp_EditarServicio", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CodigoServicio", servicio.CodigoServicio);
                    cmd.Parameters.AddWithValue("@Nombre", servicio.Nombre);
                    cmd.Parameters.AddWithValue("@Tipo", servicio.Tipo);
                    cmd.Parameters.AddWithValue("@Categoria", servicio.Categoria);
                    cmd.Parameters.AddWithValue("@UnidadMedida", servicio.UnidadMedida);
                    cmd.Parameters.AddWithValue("@PrecioUnitario", servicio.PrecioUnitario);
                    cmd.Parameters.AddWithValue("@TiempoPreparacion", servicio.TiempoPreparacion);
                    cmd.Parameters.AddWithValue("@Estado", servicio.Estado);

                    var pResultado = new SqlParameter("@Resultado", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                    var pMensaje = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 500) { Direction = ParameterDirection.Output };

                    cmd.Parameters.Add(pResultado);
                    cmd.Parameters.Add(pMensaje);

                    cmd.ExecuteNonQuery();

                    bool resultado = Convert.ToBoolean(pResultado.Value);
                    string mensaje = pMensaje.Value?.ToString() ?? "Sin mensaje del servidor";

                    if (!resultado) throw new ApplicationException(mensaje);

                    return mensaje;
                }
            }
        }

        /* ---- ELIMINAR ---- */
        public string MtdEliminarServicio(int codigoServicio)
        {
            using (SqlConnection conn = conexionDatos.MtdConexionBDD())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("usp_EliminarServicio", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CodigoServicio", codigoServicio);

                    var pResultado = new SqlParameter("@Resultado", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                    var pMensaje = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 500) { Direction = ParameterDirection.Output };

                    cmd.Parameters.Add(pResultado);
                    cmd.Parameters.Add(pMensaje);

                    cmd.ExecuteNonQuery();

                    bool resultado = Convert.ToBoolean(pResultado.Value);
                    string mensaje = pMensaje.Value?.ToString() ?? "Sin mensaje del servidor";

                    if (!resultado) throw new Exception(mensaje);

                    return mensaje;
                }
            }
        }
        /* ---- BUSCAR  ----  */
        public DataTable MtdBuscarServicio(string servicio)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = conexionDatos.MtdConexionBDD())
            {
                using (SqlCommand cmd = new SqlCommand("usp_BuscarServicio", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Nombre", servicio);

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
