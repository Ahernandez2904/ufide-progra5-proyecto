using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoRestaurante.Models
{
    public class EmpleadoViewModel
    {
        public EmpleadoViewModel()
        {
            Rol = new List<SelectListItem>();
        }

        public Empleado Empleado { get; set; }

        public IEnumerable<SelectListItem> Rol { get; set; }

    }
}
