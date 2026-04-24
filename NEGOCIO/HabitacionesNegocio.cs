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
    public class HabitacionesNegocio
    {
        HabitacionesDatos habitacionesDatos = new HabitacionesDatos();

        /*---Consultar---*/
        public List<HabitacionesEntidad> MtdConsultar()
        {
            try
            {
                DataTable dt = habitacionesDatos.MtdConsultar();
                List<HabitacionesEntidad> lista = new List<HabitacionesEntidad>();

                foreach (DataRow row in dt.Rows)
                {
                    HabitacionesEntidad habitacion = new HabitacionesEntidad
                    {
                        CodigoHabitacion = Convert.ToInt32(row["CodigoHabitacion"]),
                        Numero = row["Numero"].ToString(),
                        Ubicacion = row["Ubicacion"].ToString(),
                        Tipo = row["Tipo"].ToString(),
                        PetFriendly = Convert.ToBoolean(row["PetFriendly"]),
                        CantidadHuesped = Convert.ToInt32(row["CantidadHuesped"]),
                        Precio = Convert.ToDecimal(row["Precio"]),
                        PorcentajeAnticipo = Convert.ToDecimal(row["PorcentajeAnticipo"]),
                        Estado = Convert.ToBoolean(row["Estado"])
                    };

                    lista.Add(habitacion);
                }

                return lista;
            }
            catch
            {
                throw;
            }
        }


        /*public object MtdConsultarHabitacion()
        {
            return habitacionesDatos.MtdConsultar();
        }*/

        /*-- Agregar --*/
        public string MtdAgregarHabitacion(HabitacionesEntidad habitacion)
        {
            if (habitacion == null)
                throw new ArgumentNullException(nameof(habitacion), "La habitación no puede ser nula.");
            if (habitacion.Numero == null)
                throw new ArgumentException("El número de habitación no es válido o no puede ser cero.", nameof(habitacion.Numero));
            if (habitacion.Tipo == null)
                throw new ArgumentException("El tipo de habitación no puede ser nulo.", nameof(habitacion.Tipo));
            if (habitacion.Precio <= 0)
                throw new ArgumentException("El precio no puede ser cero.", nameof(habitacion.Precio));
            try
            {
                return habitacionesDatos.MtdAgregarHabitacion(habitacion);
            }
            catch
            {
                throw;
            }
        }
        /*-- Editar --*/
        public string MtdEditarHabitacion(HabitacionesEntidad habitacion)
        {
            if (habitacion == null)
                throw new ArgumentNullException(nameof(habitacion), "La habitación no puede ser nula.");
            if (habitacion.Numero == null)
                throw new ArgumentException("El número de habitación no es válido o no puede ser cero.", nameof(habitacion.Numero));
            if (habitacion.Tipo == null)
                throw new ArgumentException("El tipo de habitación no puede ser nulo.", nameof(habitacion.Tipo));
            if (habitacion.Precio <= 0)
                throw new ArgumentException("El precio no puede ser cero.", nameof(habitacion.Precio));
            try
            {
                return habitacionesDatos.MtdEditarHabitacion(habitacion);
            }
            catch
            {
                throw;
            }
        }
        /*-- Eliminar --*/
        public string MtdEliminarHabitacion(int codigoHabitacion)
        {
            if (codigoHabitacion <= 0)
                throw new Exception("Debe seleccionar una habitación válida");

            try
            {
                return habitacionesDatos.MtdEliminarHabitacion(codigoHabitacion);
            }
            catch
            {
                throw;
            }
        }
        /*-- Buscar --*/
        public List<HabitacionesEntidad> MtdBuscarHabitacion(string habitacion)
        {
            try
            {
                DataTable dt = habitacionesDatos.MtdBuscarHabitacion(habitacion.Trim());

                List<HabitacionesEntidad> lista = new List<HabitacionesEntidad>();

                foreach (DataRow row in dt.Rows)
                {
                    HabitacionesEntidad habitacionEntidad = new HabitacionesEntidad
                    {
                        CodigoHabitacion = Convert.ToInt32(row["CodigoHabitacion"]),
                        Numero = row["Numero"].ToString(),
                        Ubicacion = row["Ubicacion"].ToString(),
                        Tipo = row["Tipo"].ToString(),
                        PetFriendly = Convert.ToBoolean(row["PetFriendly"]),
                        CantidadHuesped = Convert.ToInt32(row["CantidadHuesped"]),
                        Precio = Convert.ToDecimal(row["Precio"]),
                        PorcentajeAnticipo = Convert.ToDecimal(row["PorcentajeAnticipo"]),
                        Estado = Convert.ToBoolean(row["Estado"])
                    };

                    lista.Add(habitacionEntidad);
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
