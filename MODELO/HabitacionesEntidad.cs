using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELO
{
    public class HabitacionesEntidad
    {
        [Key]
        [Required]
        public int CodigoHabitacion { get; set; }
        [Required]
        public string Numero { get; set; }
        [Required]
        public string Ubicacion { get; set; }
        [Required]
        public string Tipo { get; set; }
        public bool PetFriendly { get; set; }
        [Required]
        public int CantidadHuesped { get; set; }
        [Required]
        public decimal Precio { get; set; }
        public decimal PorcentajeAnticipo { get; set; }
        public bool Estado { get; set; }
    }
}
