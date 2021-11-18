using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoRestaurante.Models
{
    public class ReservacionViewModel
    {
        public ReservacionViewModel()
        {
            Cliente = new List<SelectListItem>();
        }

        public Reservacion Reservacion { get; set; }

        public IEnumerable<SelectListItem> Cliente { get; set; }
    }
}
