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
    public class EmpleadosDatos
    {
        ConexionDatos conexionDatos = new ConexionDatos();

        /*---Consultar---*/
        public DataTable MtdConsultar()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = conexionDatos.MtdConexionBDD())
            {
                using (SqlCommand cmd = new SqlCommand("usp_ConsultarEmpleado", conn))
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
        public string MtdAgregarEmpleado(EmpleadosEntidad empleado)
        {
            using (SqlConnection conn = conexionDatos.MtdConexionBDD())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("usp_AgregarEmpleado", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Nombre", empleado.Nombre);
                    cmd.Parameters.AddWithValue("@NumeroDPI", empleado.NumeroDPI);
                    cmd.Parameters.AddWithValue("@Genero", empleado.Genero);
                    cmd.Parameters.AddWithValue("@Cargo", empleado.Cargo);
                    cmd.Parameters.AddWithValue("@Salario", empleado.Salario);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", empleado.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@FechaContratacion", empleado.FechaContratacion);
                    cmd.Parameters.AddWithValue("@Estado", empleado.Estado);

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
        /*-- Editar --*/
        public string MtdEditarEmpleado(EmpleadosEntidad empleado)
        {
            using (SqlConnection conn = conexionDatos.MtdConexionBDD())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("usp_EditarEmpleado", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CodigoEmpleado", empleado.CodigoEmpleado);
                    cmd.Parameters.AddWithValue("@Nombre", empleado.Nombre);
                    cmd.Parameters.AddWithValue("@NumeroDPI", empleado.NumeroDPI);
                    cmd.Parameters.AddWithValue("@Genero", empleado.Genero);
                    cmd.Parameters.AddWithValue("@Cargo", empleado.Cargo);
                    cmd.Parameters.AddWithValue("@Salario", empleado.Salario);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", empleado.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@FechaContratacion", empleado.FechaContratacion);
                    cmd.Parameters.AddWithValue("@Estado", empleado.Estado);

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
        /*-- Eliminar --*/
        public string MtdEliminarEmpleado(int codigoEmpleado)
        {
            using (SqlConnection conn = conexionDatos.MtdConexionBDD())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("usp_EliminarEmpleado", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetro de entrada
                    cmd.Parameters.AddWithValue("@CodigoEmpleado", codigoEmpleado);

                    // Parámetros de SALIDA para capturar la respuesta de SQL
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

                    // Ejecución
                    cmd.ExecuteNonQuery();

                    // Recuperamos los valores
                    bool resultado = Convert.ToBoolean(pResultado.Value);
                    string mensaje = pMensaje.Value?.ToString() ?? "Sin mensaje del servidor";

                    // Si SQL dice que no se pudo (ej. el empleado tiene registros vinculados), lanzamos el error
                    if (!resultado)
                        throw new Exception(mensaje);

                    // Si todo bien, devolvemos el mensaje de éxito
                    return mensaje;
                }
            }
        }
        /*-- Buscar --*/
        public DataTable MtdBuscarEmpleado(string empleado)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = conexionDatos.MtdConexionBDD())
            {
                using (SqlCommand cmd = new SqlCommand("usp_BuscarEmpleado", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Nombre", empleado);
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
