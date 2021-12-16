using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoRestaurante.Models
{
    public class FacturaPlatilloViewModel
    {
        public FacturaPlatilloViewModel()
        {
            Facturas = new List<SelectListItem>();
            Platillos = new List<SelectListItem>();
            Nombres = new List<SelectListItem>();
            Detalles = new List<SelectListItem>();
            Costos = new List<SelectListItem>();
        }
        public Factura_Platillo FacturaPlatillo { get; set; }

        public IEnumerable<SelectListItem> Facturas { get; set; }
        public IEnumerable<SelectListItem> Platillos { get; set; }
        public IEnumerable<SelectListItem> Nombres { get; set; }
        public IEnumerable<SelectListItem> Detalles { get; set; }
        public IEnumerable<SelectListItem> Costos { get; set; }
    }
}
