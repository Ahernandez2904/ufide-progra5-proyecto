using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoRestaurante.Models
{
    public class Factura_Platillo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Factura")]
        public int IdFactura { get; set; }

        [Required]
        [Display(Name = "Platillo")]
        public int IdPlatillo { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        [StringLength(60, ErrorMessage = "El campo Detalle de platillo no debe ser mayor a 60 caracteres")]
        public string Detalle_Platillo { get; set; }

        [ForeignKey(nameof(IdFactura))]
        public Factura Factura { get; set; }

        [ForeignKey(nameof(IdPlatillo))]
        public Platillo Platillo { get; set; }
    }
}
