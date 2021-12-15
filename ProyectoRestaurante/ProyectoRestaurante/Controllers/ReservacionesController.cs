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
    public class ReservacionesController : Controller
    {
        public ReservacionesController(ApplicationDbContext database)
        {
            Database = database;
        }

        ApplicationDbContext Database;

        [Authorize]
        public IActionResult Index()
        {
            List<Reservacion> r = Database.Reservaciones.ToList();
            List<Cliente> cl = Database.Clientes.ToList();
            r.ForEach(c => c.CedulaCliente = cl.FirstOrDefault(s => s.CedulaCliente == c.CedulaCliente).ToString());
            return View(r);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            Reservacion r = new Reservacion();

            ReservacionViewModel model =
                new ReservacionViewModel
                {
                    Reservacion = r,
                    Cliente = Database.Clientes.ToList().ConvertAll(s => new SelectListItem(
                        s.CedulaCliente + " " +s.Nombre + " " + s.Apellido1 + " " + s.Apellido2, 
                        s.CedulaCliente.ToString(),
                        s.CedulaCliente == r.CedulaCliente
                    ))
                };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(ReservacionViewModel c)
        {
            Reservacion r = c.Reservacion;

            if (!ModelState.IsValid)
            {
                return View(c);
            }

            Reservacion reservacion = new Reservacion();

            if (r == null)
            {
                return NotFound();
            }

            reservacion.CantidadAsientos = r.CantidadAsientos;
            reservacion.Fecha = r.Fecha;
            reservacion.CedulaCliente = r.CedulaCliente.ToString();


            Database.Reservaciones.Add(reservacion);
            Database.SaveChanges();
            /*try
            {
                Database.SaveChanges();
            } catch(Exception)
            {
                return RedirectToAction(nameof(Index));
            }*/
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpGet]
        public IActionResult Upsert(int? _id)
        {
            Reservacion r = new Reservacion();

            r = Database.Reservaciones.FirstOrDefault(s => s.Id == _id);

            if (r == null)
            {
                return NotFound();
            }

            ReservacionViewModel model =
                new ReservacionViewModel
                {
                    Reservacion = r,
                    Cliente = Database.Clientes.ToList().ConvertAll(s => new SelectListItem(
                        s.CedulaCliente + " " + s.Nombre + " " + s.Apellido1 + " " + s.Apellido2,
                        s.CedulaCliente.ToString(),
                        s.CedulaCliente == r.CedulaCliente
                    ))
                };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Upsert(ReservacionViewModel c)
        {
            Reservacion r = c.Reservacion;

            if (!ModelState.IsValid)
            {
                return View(c);
            }

            Reservacion re = Database.Reservaciones.FirstOrDefault(s => s.Id == r.Id);

            if (r == null)
            {
                return NotFound();
            }

            re.Id = r.Id;
            re.CantidadAsientos = r.CantidadAsientos;
            re.Fecha = r.Fecha;
            re.CedulaCliente = r.CedulaCliente.ToString();

            Database.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpDelete]
        public IActionResult Borrar(int _id)
        {
            Reservacion r = Database.Reservaciones.FirstOrDefault(s => s.Id == _id);
            if (r == null)
            {
                return Json(new { success = false, message = "Reservación no encontrada." });
            }

            Database.Reservaciones.Remove(r);
            Database.SaveChanges();

            return Json(new { success = true, message = "Reservación borrada permanente." });
        }
    }
}
