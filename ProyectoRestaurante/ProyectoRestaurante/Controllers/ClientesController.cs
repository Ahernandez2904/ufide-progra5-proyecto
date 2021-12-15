using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoRestaurante.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoRestaurante.Controllers
{
    public class ClientesController : Controller
    {
        public ClientesController(ApplicationDbContext database)
        {
            Database = database;
        }

        ApplicationDbContext Database;

        [Authorize]
        public IActionResult Index()
        {
            List<Cliente> clientes = Database.Clientes.ToList();
            return View(clientes);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            Cliente cliente = new Cliente();

            return View(cliente);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(Cliente c)
        {
            if (!ModelState.IsValid)
            {
                return View(c);
            }

            Cliente cliente = new Cliente();

            if (cliente == null)
            {
                return NotFound();
            }

            cliente.CedulaCliente = c.CedulaCliente;
            cliente.Nombre = c.Nombre;
            cliente.Apellido1 = c.Apellido1;
            cliente.Apellido2 = c.Apellido2;
            cliente.Telefono = c.Telefono;
            cliente.Correo = c.Correo;

            Database.Clientes.Add(cliente);
            try
            {
                Database.SaveChanges();
            } catch(Exception e)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpGet]
        public IActionResult Upsert(string? CedulaCliente)
        {
            Cliente cliente = new Cliente();

            cliente = Database.Clientes.FirstOrDefault(s => s.CedulaCliente == CedulaCliente);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Upsert(Cliente c)
        {
            if (!ModelState.IsValid)
            {
                return View(c);
            }

            Cliente cliente = Database.Clientes.FirstOrDefault(s => s.CedulaCliente == c.CedulaCliente);

            if (cliente == null)
            {
                return NotFound();
            }

            cliente.CedulaCliente = c.CedulaCliente;
            cliente.Nombre = c.Nombre;
            cliente.Apellido1 = c.Apellido1;
            cliente.Apellido2 = c.Apellido2;
            cliente.Telefono = c.Telefono;
            cliente.Correo = c.Correo;

            Database.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpDelete]
        public IActionResult Borrar(string CedulaCliente)
        {
            Cliente cliente = Database.Clientes.FirstOrDefault(s => s.CedulaCliente == CedulaCliente);
            if (cliente == null)
            {
                return Json(new { success = false, message = "Cliente no encontrado." });
            }

            Database.Clientes.Remove(cliente);
            Database.SaveChanges();

            return Json(new { success = true, message = "Cliente borrado permanente." });
        }
    }
}
