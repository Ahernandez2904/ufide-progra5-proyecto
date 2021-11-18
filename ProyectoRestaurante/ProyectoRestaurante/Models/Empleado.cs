using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoRestaurante.Models
{
    public class Empleado
    {
        [Key]
        [Display(Name = "Cédula")]
        [StringLength(20, ErrorMessage = "El campo cédula no debe ser mayor a 60 caracteres")]
        public string CedulaEmpleado { get; set; }

        [StringLength(60, ErrorMessage = "El campo contraseña no debe ser mayor a 60 caracteres")]
        public string Contraseña { get; set; }

        [StringLength(60, ErrorMessage = "El campo nombre no debe ser mayor a 60 caracteres")]
        public string Nombre { get; set; }

        [StringLength(60, ErrorMessage = "El campo primer apellido no debe ser mayor a 60 caracteres")]
        [Display(Name = "Primer Apellido")]
        public string Apellido1 { get; set; }

        [StringLength(60, ErrorMessage = "El campo segundo apellido no debe ser mayor a 60 caracteres")]
        [Display(Name = "Segundo Apellido")]
        public string Apellido2 { get; set; }

        [StringLength(8, ErrorMessage = "El campo teléfono no debe ser mayor a 8 caracteres")]
        public string Telefono { get; set; }

        [StringLength(60, ErrorMessage = "El campo correo no debe ser mayor a 60 caracteres")]
        [EmailAddress]
        public string Correo { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime Nacimiento { get; set; }

        [Required]
        public int activo { get; set; }

        [Required]
        [Display(Name = "Rol de Usuario")]
        public int IdRol { get; set; }

        [ForeignKey(nameof(IdRol))]
        public Rol Rol { get; set; }
    }
}
