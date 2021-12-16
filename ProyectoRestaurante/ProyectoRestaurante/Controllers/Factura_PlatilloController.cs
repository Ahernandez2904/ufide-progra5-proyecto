using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoRestaurante.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoRestaurante.Controllers
{
    public class Factura_PlatilloController : Controller
    {

        public Factura_PlatilloController(ApplicationDbContext db)
        {
            Database = db;
        }

        ApplicationDbContext Database;

        public IActionResult Index()
        {

            List<Factura_Platillo> factura_Platillos = Database.Facturas_Platillo.ToList();
            List<Factura> facturas = Database.Facturas.ToList();
            List<Platillo> platillos = Database.Platillos.ToList();
  

            factura_Platillos.ForEach(x => x.Factura = facturas.FirstOrDefault(z => z.IdFactura == x.IdFactura));
            factura_Platillos.ForEach(x => x.Platillo = platillos.FirstOrDefault(z => z.Id == x.IdPlatillo));
           /* factura_Platillos.ForEach(x => x.Platillo = platillos.FirstOrDefault(z => z.Costo == x.Cantidad)); */
           /* factura_Platillos.ForEach(x => x.Platillo = platillos.FirstOrDefault(z => z.Descripcion == x.Detalle_Platillo)); */


            return View(factura_Platillos);
        }

        [HttpGet]
        public IActionResult Upsert(int id)
        {
            Factura_Platillo factura_Platillo = new Factura_Platillo();

            if(id > 0)
            {
                factura_Platillo = Database.Facturas_Platillo.FirstOrDefault(z => z.IdFactura == id);
                if(factura_Platillo == null)
                {
                    return NotFound();
                }

            }

            FacturaPlatilloViewModel model = new FacturaPlatilloViewModel
            {
                FacturaPlatillo = factura_Platillo,
                Facturas = Database.Facturas.ToList().ConvertAll(s => new SelectListItem(s.IdFactura.ToString(), s.IdFactura.ToString(), s.IdFactura == factura_Platillo.IdFactura)),
                Platillos = Database.Platillos.ToList().ConvertAll(s => new SelectListItem(s.Id.ToString(), s.Id.ToString(), s.Id == factura_Platillo.IdPlatillo)),
               /* Costos = Database.Platillos.ToList().ConvertAll(s => new SelectListItem(s.Costo.ToString(), s.Costo.ToString(), s.Costo == factura_Platillo.Cantidad)),  
                Detalles = Database.Platillos.ToList().ConvertAll(s => new SelectListItem(s.Descripcion, s.Descripcion, s.Descripcion == factura_Platillo.Detalle_Platillo)) */

            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Upsert(FacturaPlatilloViewModel fp)
        {
            Factura_Platillo x = fp.FacturaPlatillo;

            if (!ModelState.IsValid)
            {
                FacturaPlatilloViewModel model = new FacturaPlatilloViewModel
                {
                    FacturaPlatillo = x,
                    Facturas = Database.Facturas.ToList().ConvertAll(z => new SelectListItem(z.IdFactura.ToString(), z.IdFactura.ToString(), z.IdFactura == x.IdFactura))
                };

                return View(model);
            }

            Factura_Platillo factura_Platillo =
                x.Id > 0
                ? Database.Facturas_Platillo.FirstOrDefault(z => z.Id == x.Id)
                : new Factura_Platillo();

            if(factura_Platillo == null)
            {
                return NotFound();
            }

            factura_Platillo.Id = x.Id;
            factura_Platillo.IdFactura = x.IdFactura;
            factura_Platillo.IdPlatillo = x.IdPlatillo;
           factura_Platillo.Cantidad = x.Cantidad;
            /*factura_Platillo.Platillo.Nombre = x.Platillo.Nombre;*/
           factura_Platillo.Detalle_Platillo = x.Detalle_Platillo;
            /*factura_Platillo.Platillo.Costo = x.Platillo.Costo;*/

            if(x.Id < 1)
            {
                Database.Facturas_Platillo.Add(factura_Platillo);
            }
            Database.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
