using Microsoft.AspNetCore.Mvc;
using Agro_Mercado.AppMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace Agro_Mercado.AppMVC.Controllers
{
    public class ProductoController : BaseController
    {
        private readonly AgroMercadoSprintContext _context;

        public ProductoController(AgroMercadoSprintContext context)
        {
            _context = context;
        }

        // LISTAR
        public IActionResult Index()
        {
            if (!TieneAcceso(1))
                return RedirectToAction("Index", "Home");

            var productos = _context.Productos
                .Include(p => p.Categoria)
                .Include(p => p.UnidadMedida)
                .ToList();

            return View(productos);
        }

        // CREAR GET
        public IActionResult Create()
        {
            if (!TieneAcceso(1))
                return RedirectToAction("Index", "Home");

            ViewBag.Categorias = _context.Categorias.ToList();
            ViewBag.Unidades = _context.UnidadMedida.ToList();

            return View();
        }

        // CREAR POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Producto producto)
        {
            if (!TieneAcceso(1))
                return RedirectToAction("Index", "Home");

            ModelState.Remove("Categoria");
            ModelState.Remove("UnidadMedida");

            if (ModelState.IsValid)
            {
                var nuevoProducto = new Producto
                {
                    Nombre = producto.Nombre,
                    CategoriaId = producto.CategoriaId,
                    UnidadMedidaId = producto.UnidadMedidaId,
                    PrecioVenta = producto.PrecioVenta,
                    PrecioCompraPromedio = producto.PrecioCompraPromedio,
                    Stock = producto.Stock,
                    StockMinimo = producto.StockMinimo,
                    FechaRegistro = DateTime.Now,
                    Activo = producto.Activo
                };

                _context.Productos.Add(nuevoProducto);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.Categorias = _context.Categorias.ToList();
            ViewBag.Unidades = _context.UnidadMedida.ToList();

            return View(producto);
        }

        // EDITAR GET
        public IActionResult Edit(int id)
        {
            if (!TieneAcceso(1))
                return RedirectToAction("Index", "Home");

            var producto = _context.Productos.Find(id);

            if (producto == null)
                return NotFound();

            ViewBag.Categorias = _context.Categorias.ToList();
            ViewBag.Unidades = _context.UnidadMedida.ToList();

            return View(producto);
        }

        // EDITAR POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Producto producto)
        {
            if (!TieneAcceso(1))
                return RedirectToAction("Index", "Home");

            ModelState.Remove("Categoria");
            ModelState.Remove("UnidadMedida");

            if (!ModelState.IsValid)
            {
                ViewBag.Categorias = _context.Categorias.ToList();
                ViewBag.Unidades = _context.UnidadMedida.ToList();
                return View(producto);
            }

            var productoDb = _context.Productos.Find(id);

            if (productoDb == null)
                return NotFound();

            productoDb.Nombre = producto.Nombre;
            productoDb.CategoriaId = producto.CategoriaId;
            productoDb.UnidadMedidaId = producto.UnidadMedidaId;
            productoDb.PrecioVenta = producto.PrecioVenta;
            productoDb.PrecioCompraPromedio = producto.PrecioCompraPromedio;
            productoDb.Stock = producto.Stock;
            productoDb.StockMinimo = producto.StockMinimo;
            productoDb.Activo = producto.Activo;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // DELETE GET
        public IActionResult Delete(int id)
        {
            if (!TieneAcceso(1))
                return RedirectToAction("Index", "Home");

            var producto = _context.Productos
                .Include(p => p.Categoria)
                .Include(p => p.UnidadMedida)
                .FirstOrDefault(p => p.Id == id);

            if (producto == null)
                return NotFound();

            return View(producto);
        }

        // DELETE POST
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!TieneAcceso(1))
                return RedirectToAction("Index", "Home");

            var producto = _context.Productos.Find(id);

            if (producto != null)
            {
                _context.Productos.Remove(producto);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        // DETAILS
        public IActionResult Details(int id)
        {
            if (!TieneAcceso(1))
                return RedirectToAction("Index", "Home");

            var producto = _context.Productos
                .Include(p => p.Categoria)
                .Include(p => p.UnidadMedida)
                .FirstOrDefault(p => p.Id == id);

            if (producto == null)
                return NotFound();

            return View(producto);
        }
    }
}