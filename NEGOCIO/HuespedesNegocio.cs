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
    public class HuespedesNegocio
    {
        HuespedesDatos huespedesDatos = new HuespedesDatos();

        /* ----- CONSULTAR ----- */
        public List<HuespedesEntidad> MtdConsultar()
        {
            try
            {
                DataTable dt = huespedesDatos.MtdConsultar();

                List<HuespedesEntidad> lista = new List<HuespedesEntidad>();

                foreach (DataRow row in dt.Rows)
                {
                    HuespedesEntidad huesped = new HuespedesEntidad
                    {
                        CodigoHuesped = Convert.ToInt32(row["CodigoHuesped"]),
                        Nombre = row["Nombre"].ToString(),
                        TipoIdentificacion = row["TipoIdentificacion"].ToString(),
                        NumeroIdentificacion = Convert.ToInt64(row["NumeroIdentificacion"]),
                        FechaNacimiento = Convert.ToDateTime(row["FechaNacimiento"]),
                        Genero = Convert.ToChar(row["Genero"]),
                        Telefono = Convert.ToInt32(row["Telefono"]),
                        Direccion = row["Direccion"].ToString(),
                        Puntuacion = Convert.ToDecimal(row["Puntuacion"]),
                        Estado = Convert.ToBoolean(row["Estado"])
                    };

                    lista.Add(huesped);
                }

                return lista;
            }
            catch
            {
                throw;
            }
        }

        /*public object MtdConsultarHuespedes()
        {
            return huespedesDatos.MtdConsultar();
        }*/

        /* ----- AGREGAR ----- */
        public string MtdAgregarHuespedes(HuespedesEntidad huesped)
        {
            if (huesped == null)
                throw new Exception("No se recibieron datos");

            if (huesped.CodigoHuesped != 0)
                throw new Exception("Codigo de huesped no necesita valor");

            if (string.IsNullOrWhiteSpace(huesped.Nombre))
                throw new Exception("No envió valor al campo nombre");

            if (string.IsNullOrWhiteSpace(huesped.TipoIdentificacion))
                throw new Exception("No envió valor al campo tipo de identificación");

            if (huesped.NumeroIdentificacion <= 0)
                throw new Exception("No envió valor al campo numero de identificación");

            if (huesped.FechaNacimiento >= DateTime.Today)
                throw new Exception("La fecha de nacimiento tiene que ser menor a la fecha del dia");

            if (huesped.Telefono <= 0)
                throw new Exception("Telefono no válido");

            if (huesped.Puntuacion <= 0)
                throw new Exception("Numero de puntuación no es válida");

            try
            {
                return huespedesDatos.MtdAgregarHuespedes(huesped);
            }
            catch
            {
                throw;
            }
        }
        /*-- Editar --*/
        public string MtdEditarHuesped(HuespedesEntidad huesped)
        {
            if (huesped == null)
                throw new ArgumentNullException(nameof(huesped), "El huesped no puede ser nulo.");
            if (huesped.Nombre == null)
                throw new ArgumentException("El nombre no puede ser nulo.", nameof(huesped.Nombre));
            if (huesped.TipoIdentificacion == null)
                throw new ArgumentException("El tipo identificacion no puede ser nulo.", nameof(huesped.TipoIdentificacion));
            if (huesped.NumeroIdentificacion <0)
                throw new ArgumentException("El numero identificacion no puede ser nulo.", nameof(huesped.NumeroIdentificacion));
            if (huesped.FechaNacimiento == null)
                throw new ArgumentException("La fecha nacimiento no puede ser nulo.", nameof(huesped.FechaNacimiento));
            try
            {
                return huespedesDatos.MtdEditarHuesped(huesped);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la lógica de negocio al editar el huesped", ex);
            }
        }
        /*-- Eliminar --*/
        public string MtdEliminarHuesped(int codigoHuesped)
        {
            if (codigoHuesped <= 0)
                throw new Exception("Debe seleccionar un huésped válido");

            try
            {
                return huespedesDatos.MtdEliminarHuesped(codigoHuesped);
            }
            catch
            {
                throw;
            }
        }
        /*-- Buscar --*/
        public List<HuespedesEntidad> MtdBuscarHuesped(string huesped)
        {
            try
            {
                DataTable dt = huespedesDatos.MtdBuscarHuesped(huesped.Trim());

                List<HuespedesEntidad> lista = new List<HuespedesEntidad>();

                foreach (DataRow row in dt.Rows)
                {
                    HuespedesEntidad huespedes = new HuespedesEntidad
                    {
                        CodigoHuesped = Convert.ToInt32(row["CodigoHuesped"]),
                        Nombre = row["Nombre"].ToString(),
                        TipoIdentificacion = row["TipoIdentificacion"].ToString(),
                        NumeroIdentificacion = Convert.ToInt64(row["NumeroIdentificacion"]),
                        FechaNacimiento = Convert.ToDateTime(row["FechaNacimiento"]),
                        Genero = Convert.ToChar(row["Genero"]),
                        Telefono = Convert.ToInt32(row["Telefono"]),
                        Direccion = row["Direccion"].ToString(),
                        Puntuacion = Convert.ToDecimal(row["Puntuacion"]),
                        Estado = Convert.ToBoolean(row["Estado"])
                    };

                    lista.Add(huespedes);
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
