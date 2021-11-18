using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoRestaurante.Models
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Rol> Roles { get; set; }
        public DbSet<Platillo> Platillos { get; set; }
        public DbSet<Factura_Platillo> Facturas_Platillo { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<Reservacion> Reservaciones { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
    }
}
