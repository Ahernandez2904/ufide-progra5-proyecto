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
    public class EmpleadosController : Controller
    {
        public EmpleadosController(ApplicationDbContext database)
        {
            Database = database;
        }

        ApplicationDbContext Database;

        [Authorize]
        public IActionResult Index()
        {
            List<Empleado> empleados = Database.Empleados.ToList();
            List<Rol> roles = Database.Roles.ToList();
            empleados.ForEach(c => c.Rol = roles.FirstOrDefault(s => s.Id == c.IdRol));
            return View(empleados);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            Empleado empleado = new Empleado();
            
            EmpleadoViewModel model =
                new EmpleadoViewModel
                {
                    Empleado = empleado,
                    Rol = Database.Roles.ToList().ConvertAll
                        (s => new SelectListItem(s.Nombre.ToString(), s.Id.ToString(), s.Id == empleado.IdRol))
                };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(EmpleadoViewModel c)
        {
            Empleado e = c.Empleado;

            if (!ModelState.IsValid)
            {
                return View(c);
            }

            Empleado empleado = new Empleado();

            if (empleado == null)
            {
                return NotFound();
            }

            empleado.CedulaEmpleado = e.CedulaEmpleado;
            empleado.Contraseña = e.Contraseña;
            empleado.Nombre = e.Nombre;
            empleado.Apellido1 = e.Apellido1;
            empleado.Apellido2 = e.Apellido2;
            empleado.Telefono = e.Telefono;
            empleado.Correo = e.Correo;
            empleado.Nacimiento = e.Nacimiento;
            empleado.activo = e.activo;
            empleado.Rol = Database.Roles.FirstOrDefault(s => s.Id == e.IdRol);

            Database.Empleados.Add(empleado);

            Database.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpGet]
        public IActionResult Upsert(string? CedulaEmpleado)
        {
            Empleado empleado = new Empleado();

            empleado = Database.Empleados.FirstOrDefault(s => s.CedulaEmpleado == CedulaEmpleado);

            EmpleadoViewModel model =
                new EmpleadoViewModel
                {
                    Empleado = empleado,
                    Rol = Database.Roles.ToList().ConvertAll
                        (s => new SelectListItem(s.Nombre, s.Id.ToString(), s.Id == empleado.IdRol))
                };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Upsert(EmpleadoViewModel c)
        {
            Empleado e = c.Empleado;

            if (!ModelState.IsValid)
            {
                return View(c);
            }

            Empleado empleado = Database.Empleados.FirstOrDefault(s => s.CedulaEmpleado == e.CedulaEmpleado);

            if (empleado == null)
            {
                return NotFound();
            }

            empleado.CedulaEmpleado = e.CedulaEmpleado;
            empleado.Contraseña = e.Contraseña;
            empleado.Nombre = e.Nombre;
            empleado.Apellido1 = e.Apellido1;
            empleado.Apellido2 = e.Apellido2;
            empleado.Telefono = e.Telefono;
            empleado.Correo = e.Correo;
            empleado.Nacimiento = e.Nacimiento;
            empleado.activo = e.activo;
            empleado.Rol = Database.Roles.FirstOrDefault(s => s.Id == e.IdRol);

            Database.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpDelete]
        public IActionResult Borrar(string CedulaEmpleado)
        {
            Empleado empleados = Database.Empleados.FirstOrDefault(s => s.CedulaEmpleado == CedulaEmpleado);
            if (empleados == null)
            {
                return Json(new { success = false, message = "Empleado no encontrado." });
            }

            Database.Empleados.Remove(empleados);
            Database.SaveChanges();

            return Json(new { success = true, message = "Empleado borrado permanente." });
        }
    }
}
