using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoRestaurante.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoRestaurante.Controllers
{
    public class FacturaController : Controller
    {

        public FacturaController(ApplicationDbContext db)
        {
            Database = db;
        }

        ApplicationDbContext Database;

        [Authorize]
        public IActionResult Index()
        {
            List<Factura> facturas = Database.Facturas.ToList();
            List<Reservacion> reservacions = Database.Reservaciones.ToList();
            List<Empleado> empleados = Database.Empleados.ToList();

            facturas.ForEach(x => x.Reservacion = reservacions.FirstOrDefault(z => z.Id == x.IdReservacion));
            facturas.ForEach(x => x.Empleado = empleados.FirstOrDefault(z => z.CedulaEmpleado == x.CedulaEmpleado));
            

            return View(facturas);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Upsert(int id)
        {
            Factura factura = new Factura();

            if(id > 0)
            {
                factura = Database.Facturas.FirstOrDefault(z => z.IdReservacion == id);
                if(factura == null)
                {
                    return NotFound();
                }
            }

            FacturaViewModel model = new FacturaViewModel
            {
                Factura = factura,
                Reservaciones = Database.Reservaciones.ToList().ConvertAll(s => new SelectListItem(s.Id.ToString(), s.Id.ToString(), s.Id == factura.IdReservacion)),
                Empleados = Database.Empleados.ToList().ConvertAll(s => new SelectListItem(s.CedulaEmpleado, s.CedulaEmpleado, s.CedulaEmpleado == factura.CedulaEmpleado))
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Upsert(FacturaViewModel f)
        {
            Factura x = f.Factura;

            if (!ModelState.IsValid)
            {
                FacturaViewModel model = new FacturaViewModel
                {
                    Factura = x,
                    Reservaciones = Database.Reservaciones.ToList().ConvertAll(z => new SelectListItem(z.CantidadAsientos.ToString(), z.Id.ToString(), z.Id == x.IdFactura))
                };

                return View(model);
            }

            Factura factura =
                x.IdFactura > 0
                ? Database.Facturas.FirstOrDefault(z => z.IdFactura == x.IdFactura)
                : new Factura();

            if(factura == null)
            {
                return NotFound();
            }

            factura.IdFactura = x.IdFactura;
            factura.IdReservacion = x.IdReservacion;
            factura.CedulaEmpleado = x.CedulaEmpleado;
            factura.Reservacion = Database.Reservaciones.FirstOrDefault(z => z.Id == x.IdReservacion);
            
            if(x.IdFactura < 1)
            {
                Database.Facturas.Add(factura);
            }
            Database.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpDelete]
        public IActionResult Borrar(int idFactura)
        {
            Factura f = Database.Facturas.FirstOrDefault(s => s.IdReservacion == idFactura);
            if (f == null)
            {
                return Json(new { success = false, message = "Facturo no encontrada." });
            }

            Database.Facturas.Remove(f);
            Database.SaveChanges();

            return Json(new { success = false, message = "La eliminación de la factura ha sido exitosa." });
        }

    }

}

