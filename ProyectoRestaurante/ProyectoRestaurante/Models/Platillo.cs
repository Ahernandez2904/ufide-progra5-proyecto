using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoRestaurante.Models
{
    public class Platillo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(60, ErrorMessage = "El campo nombre no debe ser mayor a 60 caracteres")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "El campo nombre no debe ser mayor a 300 caracteres")]
        public string Descripcion { get; set; }

        [Required]
        public float Costo { get; set; }

        [Required]
        public int activo { get; set; }
    }
}
