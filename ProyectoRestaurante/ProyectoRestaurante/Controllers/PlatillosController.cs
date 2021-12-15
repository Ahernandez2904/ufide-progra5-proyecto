using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoRestaurante.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoRestaurante.Controllers
{
    public class PlatillosController : Controller
    {
        public PlatillosController(ApplicationDbContext database)
        {
            Database = database;
        }

        ApplicationDbContext Database;

        [Authorize]
        public IActionResult Index()
        {
            List<Platillo> platillos = Database.Platillos.ToList();
            return View(platillos);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            Platillo p = new Platillo();
            return View(p);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(Platillo c)
        {
            if (!ModelState.IsValid)
            {
                return View(c);
            }

            Platillo p = new Platillo();

            if (p == null)
            {
                return NotFound();
            }

            p.Id = c.Id;
            p.Nombre = c.Nombre;
            p.Descripcion = c.Descripcion;
            p.Costo = c.Costo;
            p.activo = c.activo;

            Database.Platillos.Add(p);

            Database.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpGet]
        public IActionResult Upsert(int? _id)
        {
            Platillo p = new Platillo();

            p = Database.Platillos.FirstOrDefault(s => s.Id == _id);
            if (p == null)
            {
                return NotFound();
            }

            return View(p);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Upsert(Platillo c)
        {
            if (!ModelState.IsValid)
            {
                return View(c);
            }

            Platillo p  = Database.Platillos.FirstOrDefault(s => s.Id == c.Id);

            if ( p == null)
            {
                return NotFound();
            }

            p.Id = c.Id;
            p.Nombre = c.Nombre;
            p.Descripcion = c.Descripcion;
            p.Costo = c.Costo;
            p.activo = c.activo;

            Database.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpDelete]
        public IActionResult Borrar(int _id)
        {
            Platillo p = Database.Platillos.FirstOrDefault(s => s.Id == _id);
            if (p == null)
            {
                return Json(new { success = false, message = "Platillo no encontrado." });
            }

            Database.Platillos.Remove(p);
            Database.SaveChanges();

            return Json(new { success = true, message = "Platillo borrado permanente." });
        }
    }
}
