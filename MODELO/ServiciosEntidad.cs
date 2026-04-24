using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELO
{
    public class ServiciosEntidad
    {
        [Key]
        [Required]
        public int CodigoServicio { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Tipo { get; set; }
        [Required]
        public string Categoria { get; set; }
        public string UnidadMedida { get; set; }
        [Required]
        public decimal PrecioUnitario { get; set; }
        public decimal TiempoPreparacion { get; set; }
        public bool Estado { get; set; }
    }
}
