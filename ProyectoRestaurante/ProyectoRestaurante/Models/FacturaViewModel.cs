using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoRestaurante.Models
{
    public class FacturaViewModel
    {
        public FacturaViewModel()
        {
            Reservaciones = new List<SelectListItem>();
            Empleados = new List<SelectListItem>();
        }

        public Factura Factura { get; set; }

        public IEnumerable<SelectListItem> Reservaciones { get; set; }

        public IEnumerable<SelectListItem> Empleados { get; set; }
    }
}
