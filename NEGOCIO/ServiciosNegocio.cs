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
    public class ServiciosNegocio
    {
        ServiciosDatos serviciosDatos = new ServiciosDatos();

        /*---Consultar---*/
        public List<ServiciosEntidad> MtdConsultar()
        {
            try
            {
                DataTable dt = serviciosDatos.MtdConsultar();

                List<ServiciosEntidad> lista = new List<ServiciosEntidad>();

                foreach (DataRow row in dt.Rows)
                {
                    ServiciosEntidad servicio = new ServiciosEntidad
                    {
                        CodigoServicio = Convert.ToInt32(row["CodigoServicio"]),
                        Nombre = row["Nombre"].ToString(),
                        Tipo = row["Tipo"].ToString(),
                        Categoria = row["Categoria"].ToString(),
                        UnidadMedida = row["UnidadMedida"].ToString(),
                        PrecioUnitario = Convert.ToDecimal(row["PrecioUnitario"]),
                        TiempoPreparacion = Convert.ToInt32(row["TiempoPreparacion"]),
                        Estado = Convert.ToBoolean(row["Estado"])
                    };

                    lista.Add(servicio);
                }

                return lista;
            }
            catch
            {
                throw;
            }
        }
        /*public object MtdConsultarServicios()
        {
            return serviciosDatos.MtdConsultar();
        }*/

        /*-- Agregar --*/
        public string MtdAgregarServicio(ServiciosEntidad servicio)
        {
            if (servicio == null)
                throw new ArgumentNullException(nameof(servicio), "El servicio no puede ser nulo.");
            if (servicio.Nombre == null)
                throw new ArgumentException("El nombre no puede ser nulo.", nameof(servicio.Nombre));
            if (servicio.Tipo == null)
                throw new ArgumentException("El tipo no puede ser nulo.", nameof(servicio.Tipo));
            if (servicio.Categoria == null)
                throw new ArgumentException("La categoria debe ser mayor a cero.", nameof(servicio.Categoria));
            try
            {
                return serviciosDatos.MtdAgregarServicio(servicio);
            }
            catch
            {
                throw;
            }
        }
        /*-- Editar --*/
        public string MtdEditarServicio(ServiciosEntidad servicio)
        {
            if (servicio == null)
                throw new ArgumentNullException(nameof(servicio), "El servicio no puede ser nulo.");
            if (servicio.Nombre == null)
                throw new ArgumentException("El nombre no puede ser nulo.", nameof(servicio.Nombre));
            if (servicio.Tipo == null)
                throw new ArgumentException("El tipo no puede ser nulo.", nameof(servicio.Tipo));
            if (servicio.Categoria == null)
                throw new ArgumentException("La categoria debe ser mayor a cero.", nameof(servicio.Categoria));
            try
            {
                return serviciosDatos.MtdEditarServicio(servicio);
            }
            catch
            {
                throw;
            }
        }
        /*-- Eliminar --*/
        public string MtdEliminarServicio(int codigoServicio)
        {
            if (codigoServicio <= 0)
                throw new Exception("Debe seleccionar un servicio válido");

            try
            {
                return serviciosDatos.MtdEliminarServicio(codigoServicio);
            }
            catch
            {
                throw;
            }
        }
        /* ---- BUSCAR ---- */
        public List<ServiciosEntidad> MtdBuscarServicio(string servicio)
        {
            try
            {
                DataTable resultado = serviciosDatos.MtdBuscarServicio(servicio.Trim());

                List<ServiciosEntidad> servicios = new List<ServiciosEntidad>();

                foreach (DataRow row in resultado.Rows)
                {
                    ServiciosEntidad service = new ServiciosEntidad
                    {
                        CodigoServicio = Convert.ToInt32(row["CodigoServicio"]),
                        Nombre = row["Nombre"].ToString(),
                        Tipo = row["Tipo"].ToString(),
                        Categoria = row["Categoria"].ToString(),
                        UnidadMedida = row["UnidadMedida"].ToString(),
                        PrecioUnitario = Convert.ToDecimal(row["PrecioUnitario"]),
                        TiempoPreparacion = Convert.ToDecimal(row["TiempoPreparacion"]),
                        Estado = Convert.ToBoolean(row["Estado"])
                    };

                    servicios.Add(service);
                }

                return servicios;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
