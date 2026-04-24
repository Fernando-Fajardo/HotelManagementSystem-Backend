using DATOS;
using MODELO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEGOCIO
{
    public class EmpleadosNegocio
    {
        EmpleadosDatos empleadosDatos = new EmpleadosDatos();

        /* ----- CONSULTAR ----- */
        public List<EmpleadosEntidad> MtdConsultar()
        {
            try
            {
                DataTable dt = empleadosDatos.MtdConsultar();

                List<EmpleadosEntidad> lista = new List<EmpleadosEntidad>();

                foreach (DataRow row in dt.Rows)
                {
                    EmpleadosEntidad empleado = new EmpleadosEntidad
                    {
                        CodigoEmpleado = Convert.ToInt32(row["CodigoEmpleado"]),
                        Nombre = row["Nombre"].ToString(),
                        NumeroDPI = Convert.ToInt64(row["NumeroDPI"]),
                        Genero = row["Genero"].ToString(),
                        Cargo = row["Cargo"].ToString(),
                        Salario = Convert.ToDecimal(row["Salario"]),
                        FechaNacimiento = Convert.ToDateTime(row["FechaNacimiento"]),
                        FechaContratacion = Convert.ToDateTime(row["FechaContratacion"]),
                        Estado = Convert.ToBoolean(row["Estado"])

                    };

                    lista.Add(empleado);
                }

                return lista;
            }
            catch
            {
                throw;
            }
        }

        /*public object MtdConsultarEmpleado()
        {
            return empleadosDatos.MtdConsultar();
        }*/

        /*-- Agregar --*/
        public string MtdAgregarEmpleado(EmpleadosEntidad empleado)
        {
            if (empleado == null)
                throw new ArgumentNullException(nameof(empleado), "El huesped no puede ser nulo.");
            if (empleado.Nombre == null)
                throw new ArgumentException("El nombre no puede ser nulo.", nameof(empleado.Nombre));
            if (empleado.NumeroDPI <= 0)
                throw new ArgumentException("El número de DPI no es válido o no puede ser cero.", nameof(empleado.NumeroDPI));
            if (empleado.Genero == null)
                throw new ArgumentException("El genero no debe ser nulo.", nameof(empleado.Genero));
            if (empleado.Cargo == null)
                throw new ArgumentException("El cargo no puede ser nulo.", nameof(empleado.Cargo));
            if (empleado.Salario <= 0)
                throw new ArgumentException("El salario no puede ser cero.", nameof(empleado.Salario));
            try
            {
                return empleadosDatos.MtdAgregarEmpleado(empleado);
            }
            catch
            {
                throw;
            }
        }
        /*-- Editar --*/
        public string MtdEditarEmpleado(EmpleadosEntidad empleado)
        {
            if (empleado == null)
                throw new ArgumentNullException(nameof(empleado), "El huesped no puede ser nulo.");
            if (empleado.Nombre == null)
                throw new ArgumentException("El nombre no puede ser nulo.", nameof(empleado.Nombre));
            if (empleado.NumeroDPI <= 0)
                throw new ArgumentException("El número de DPI no es válido o no puede ser cero.", nameof(empleado.NumeroDPI));
            if (empleado.Genero == null)
                throw new ArgumentException("El genero no debe ser nulo.", nameof(empleado.Genero));
            if (empleado.Cargo == null)
                throw new ArgumentException("El cargo no puede ser nulo.", nameof(empleado.Cargo));
            if (empleado.Salario <= 0)
                throw new ArgumentException("El salario no puede ser cero.", nameof(empleado.Salario));
            try
            {
                return empleadosDatos.MtdEditarEmpleado(empleado);
            }
            catch
            {
                throw;
            }
        }
        /*-- Eliminar --*/
        public string MtdEliminarEmpleado(int codigoEmpleado)
        {
            if (codigoEmpleado <= 0)
                throw new Exception("Debe seleccionar un empleado válido");

            try
            {
                return empleadosDatos.MtdEliminarEmpleado(codigoEmpleado);
            }
            catch
            {
                throw;
            }
            
        }
        /*-- Buscar --*/
        public List<EmpleadosEntidad> MtdBuscarEmpleado(string empleado)
        {
            try
            {
                DataTable dt = empleadosDatos.MtdBuscarEmpleado(empleado.Trim());

                List<EmpleadosEntidad> lista = new List<EmpleadosEntidad>();

                foreach (DataRow row in dt.Rows)
                {
                    EmpleadosEntidad empleados = new EmpleadosEntidad
                    {
                        CodigoEmpleado = Convert.ToInt32(row["CodigoEmpleado"]),
                        Nombre = row["Nombre"].ToString(),
                        NumeroDPI = Convert.ToInt64(row["NumeroDPI"]),
                        Genero = row["Genero"].ToString(),
                        Cargo = row["Cargo"].ToString(),
                        Salario = Convert.ToDecimal(row["Salario"]),
                        FechaNacimiento = Convert.ToDateTime(row["FechaNacimiento"]),
                        FechaContratacion = Convert.ToDateTime(row["FechaContratacion"]),
                        Estado = Convert.ToBoolean(row["Estado"])
                    };

                    lista.Add(empleados);
                }

                return lista;
            }
            catch
            {
                throw;
            }
        }
    }
}
