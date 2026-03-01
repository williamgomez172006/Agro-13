using Microsoft.AspNetCore.Mvc;
using Agro_Mercado.AppMVC.Models;
using System.Linq;

namespace Agro_Mercado.AppMVC.Controllers
{
    public class CategoriasController : BaseController
    {
        private readonly AgroMercadoSprintContext _context;

        public CategoriasController(AgroMercadoSprintContext context)
        {
            _context = context;
        }

        // LISTAR
        public IActionResult Index()
        {
            if (!TieneAcceso(1, 6, 8))
                return RedirectToAction("Index", "Home");

            var lista = _context.Categorias.ToList();
            return View(lista);
        }

        // DETALLE
        public IActionResult Details(int id)
        {
            if (!TieneAcceso(1, 6, 8))
                return RedirectToAction("Index", "Home");

            var categoria = _context.Categorias.Find(id);
            if (categoria == null)
                return NotFound();

            return View(categoria);
        }

        // CREAR GET
        public IActionResult Create()
        {
            if (!TieneAcceso(1, 6, 8))
                return RedirectToAction("Index", "Home");

            return View();
        }

        // CREAR POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Categoria categoria)
        {
            if (!TieneAcceso(1, 6, 8))
                return RedirectToAction("Index", "Home");

            if (!ModelState.IsValid)
                return View(categoria);

            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // EDITAR GET
        public IActionResult Edit(int id)
        {
            if (!TieneAcceso(1, 6, 8))
                return RedirectToAction("Index", "Home");

            var categoria = _context.Categorias.Find(id);
            if (categoria == null)
                return NotFound();

            return View(categoria);
        }

        // EDITAR POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Categoria categoria)
        {
            if (!TieneAcceso(1, 6, 8))
                return RedirectToAction("Index", "Home");

            if (!ModelState.IsValid)
                return View(categoria);

            var categoriaDb = _context.Categorias.Find(id);
            if (categoriaDb == null)
                return NotFound();

            categoriaDb.Nombre = categoria.Nombre;
            categoriaDb.Descripcion = categoria.Descripcion;
            categoriaDb.Estado = categoria.Estado;

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: Delete
        public IActionResult Delete(int id)
        {
            if (!TieneAcceso(1, 6, 8))
                return RedirectToAction("Index", "Home");

            var categoria = _context.Categorias.Find(id);
            if (categoria == null)
                return NotFound();

            return View(categoria);
        }

        // POST: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!TieneAcceso(1, 6, 8))
                return RedirectToAction("Index", "Home");

            var categoria = _context.Categorias.Find(id);

            if (categoria == null)
                return NotFound();

            // Validar si tiene productos relacionados
            if (categoria.Productos != null && categoria.Productos.Any())
            {
                TempData["Error"] = "No se puede eliminar porque tiene productos asociados.";
                return RedirectToAction(nameof(Index));
            }

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            TempData["Success"] = "Categoría eliminada correctamente.";
            return RedirectToAction(nameof(Index));
        }
    }
}