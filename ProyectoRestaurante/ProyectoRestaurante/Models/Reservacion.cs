using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoRestaurante.Models
{
    public class Reservacion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Cantidad de asientos")]
        public int CantidadAsientos { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime Fecha { get; set; }

        [Required]
        [Display(Name = "Cliente")]
        public string CedulaCliente { get; set; }

        [ForeignKey(nameof(CedulaCliente))]
        public Cliente Cliente { get; set; }
    }
}
