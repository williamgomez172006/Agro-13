using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Agro_Mercado.AppMVC.Models;

namespace Agromercado.AppMVC.Controllers
{
    public class MovimientosInventarioController : Controller
    {
        private readonly AgroMercadoSprintContext _context;

        public MovimientosInventarioController(AgroMercadoSprintContext context)
        {
            _context = context;
        }

        // ============================
        // LISTA DE MOVIMIENTOS
        // ============================
        public IActionResult Index()
        {
            var movimientos = _context.MovimientosInventarios
                .Include(m => m.Producto)
                .OrderByDescending(m => m.Fecha)
                .ToList();

            return View(movimientos);
        }

        // ============================
        // FORMULARIO DE ENTRADA
        // ============================
        public IActionResult CrearEntrada()
        {
            ViewBag.Productos = new SelectList(
                _context.Productos.Where(p => p.Activo == true),
                "Id",
                "Nombre"
            );

            return View();
        }

        // ============================
        // GUARDAR ENTRADA
        // ============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CrearEntrada(int productoId, int cantidad)
        {
            if (cantidad <= 0)
            {
                ModelState.AddModelError("", "La cantidad debe ser mayor a 0");
            }

            // VALIDACIÓN DE STOCK INICIAL
            var existeEntradaInicial = _context.MovimientosInventarios
                .Any(m => m.ProductoId == productoId && m.TipoMovimiento == "Entrada Inicial");

            if (existeEntradaInicial)
            {
                ModelState.AddModelError("", "Este producto ya tiene stock inicial registrado.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Productos = new SelectList(
                    _context.Productos.Where(p => p.Activo == true),
                    "Id",
                    "Nombre"
                );

                return View();
            }

            // Buscar producto
            var producto = _context.Productos.Find(productoId);

            if (producto == null)
            {
                return NotFound();
            }

            // Actualizar stock
            producto.Stock = (producto.Stock ?? 0) + cantidad;

            // Crear movimiento
            var movimiento = new MovimientosInventario
            {
                ProductoId = productoId,
                TipoMovimiento = "Entrada Inicial",
                Cantidad = cantidad,
                Fecha = DateTime.Now
            };

            _context.MovimientosInventarios.Add(movimiento);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // ============================
        // DETALLE
        // ============================
        public IActionResult Details(int id)
        {
            var movimiento = _context.MovimientosInventarios
                .Include(m => m.Producto)
                .FirstOrDefault(m => m.Id == id);

            if (movimiento == null)
            {
                return NotFound();
            }

            return View(movimiento);
        }

        // ============================
        // CONFIRMAR ELIMINAR
        // ============================
        public IActionResult Delete(int id)
        {
            var movimiento = _context.MovimientosInventarios
                .Include(m => m.Producto)
                .FirstOrDefault(m => m.Id == id);

            if (movimiento == null)
            {
                return NotFound();
            }

            return View(movimiento);
        }

        // ============================
        // ELIMINAR
        // ============================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var movimiento = _context.MovimientosInventarios.Find(id);

            if (movimiento != null)
            {
                _context.MovimientosInventarios.Remove(movimiento);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}