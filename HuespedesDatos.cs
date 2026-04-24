
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

        /*---Consultar---*/
        public List<HuespedesEntidad> MtdConsultar()
        {
            List<HuespedesEntidad> ListaHuespedes = new List<HuespedesEntidad>();
            try
            {
                using (SqlConnection conn = conexionDatos.MtdConexionBDD())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("usp_ConsultarHuespedes", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                ListaHuespedes.Add(new HuespedesEntidad()
                                {
                                    CodigoHuesped = Convert.ToInt32(dr["CodigoHuesped"]),
                                    Nombre = dr["Nombre"].ToString().Trim(),
                                    TipoIdentificacion = dr["TipoIdentificacion"].ToString().Trim(),
                                    NumeroIdentificacion = Convert.ToInt64(dr["NumeroIdentificacion"]),
                                    FechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"]),
                                    Genero = dr["Genero"].ToString().Trim(),
                                    Telefono = Convert.ToInt32(dr["Telefono"]), 
                                    Direccion = dr["Direccion"].ToString().Trim(),
                                    Puntuacion = Convert.ToDecimal(dr["Puntuacion"]),
                                    Estado = Convert.ToBoolean(dr["Estado"])
                                });
                            }
                        }
                    }
                }
                return ListaHuespedes;
            }
            catch (Exception ex)
            {
                // log ex (incluyendo InnerException)
                throw new Exception("Error en la capa datos al consultar huéspedes", ex);
            }
        }
        /*-- Agregar --*/
        public bool MtdAgregarHuesped(HuespedesEntidad huesped)
        {
            try
            {
                using (SqlConnection conn = conexionDatos.MtdConexionBDD())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("usp_AgregarHuespedes", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombre", huesped.Nombre);
                        cmd.Parameters.AddWithValue("@TipoIdentificacion", huesped.TipoIdentificacion);
                        cmd.Parameters.AddWithValue("@NumeroIdentificacion", huesped.NumeroIdentificacion);
                        cmd.Parameters.AddWithValue("@FechaNacimiento", huesped.FechaNacimiento);
                        cmd.Parameters.AddWithValue("@Genero", huesped.Genero);
                        cmd.Parameters.AddWithValue("@Telefono", huesped.Telefono);
                        cmd.Parameters.AddWithValue("@Direccion", huesped.Direccion);
                        cmd.Parameters.AddWithValue("@Puntuacion", huesped.Puntuacion);
                        cmd.Parameters.AddWithValue("@Estado", huesped.Estado);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error en la capa datos al agregar un nuevo huesped", ex);
            }
        }
        /*-- Editar --*/
        public bool MtdEditarHuesped(HuespedesEntidad huesped)
        {
            try
            {
                using (SqlConnection conn = conexionDatos.MtdConexionBDD())
                {
                    conn.Open(); // <-- CORRECCIÓN aplicada: abrir la conexión antes de usar el comando
                    using (SqlCommand cmd = new SqlCommand("usp_EditarHuespedes", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
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
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la capa datos al editar un huesped", ex);
            }
        }
        /*-- Eliminar --*/
        public bool MtdEliminarHuesped(int codigoHuesped)
        {
            try
            {
                using (SqlConnection conn = conexionDatos.MtdConexionBDD())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("usp_EliminarHUespedes", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CodigoHuesped", codigoHuesped);
                        int filasAfectadas = cmd.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error en la capa datos al eliminar un huesped", ex);
            }
        }
        /*-- Buscar --*/
        public DataTable MtdBuscarHuesped(string huesped)
        {
            DataTable dtHuesped = new DataTable();
            try
            {
                using (SqlConnection conn = conexionDatos.MtdConexionBDD())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("usp_BuscarHuespedes", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombre", huesped);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dtHuesped);
                    }
                }
                return dtHuesped;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la base de datos: " + ex.Message);
            }
        }
    }
}