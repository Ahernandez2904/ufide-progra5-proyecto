using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoRestaurante.Models
{
    public class Factura
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Reservacion")]
        public int IdReservacion { get; set; }

        [Required]
        [Display(Name = "Empleado")]
        public string CedulaEmpleado { get; set; }

        [ForeignKey(nameof(IdReservacion))]
        public Reservacion Reservacion { get; set; }

        [ForeignKey(nameof(CedulaEmpleado))]
        public Empleado Empleado { get; set; }
    }
}
