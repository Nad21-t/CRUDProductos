using CRUDProductos.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using CRUDProductos.Models;

namespace CRUDProductos.Controllers
{
    public class ProductoController(AppDBContext context) : Controller
    {
        private readonly AppDBContext _context = context;
        public async Task<IActionResult> Index(string busqueda)
        {
            var query = _context.Producto.AsQueryable();

            if (!string.IsNullOrEmpty(busqueda))
            {
                query = query.Where(t => t.Nombre!.Contains(busqueda) || t.Descripcion!.Contains(busqueda));
            }

            var prod = await query.OrderBy(t => t.Nombre).ToListAsync();

            ViewBag.CurrentFilter = busqueda;

            return View(prod);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Producto prod)
        {
            if (ModelState.IsValid)
            {
                prod.FechaCreacion = DateTime.Now;
                _context.Producto.Add(prod);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "El producto fue creado.";
                return RedirectToAction(nameof(Index));
            }
            return View(prod);
        }

        public async Task<IActionResult> Editar(int id)
        {
            var prod = await _context.Producto.FindAsync(id);
            if (prod == null) return NotFound();
            return View(prod);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Editar(int id, Producto prod)
        {
            if (id != prod.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Producto.Update(prod);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "El producto se ha modificado.";
                return RedirectToAction(nameof(Index));
            }

            return View(prod);
        }

        public async Task<IActionResult> Eliminar(int id)
        {
            var prod = await _context.Producto.FindAsync(id);
            if (prod == null) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Producto.Remove(prod);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "El producto se ha eliminado.";
                return RedirectToAction(nameof(Index));
            }

            return View(prod);
        }

    }
}
