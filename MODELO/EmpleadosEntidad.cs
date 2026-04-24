using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELO
{
    public class EmpleadosEntidad
    {
        [Key]
        [Required]
        public int CodigoEmpleado { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public long NumeroDPI { get; set; }
        [Required]
        public string Genero { get; set; }
        [Required]
        public string Cargo { get; set; }
        [Required]
        public decimal Salario { get; set; }
        [Required]
        public DateTime FechaNacimiento { get; set; }
        [Required]
        public DateTime FechaContratacion { get; set; }
        [Required]
        public bool Estado { get; set; }
    }
}
