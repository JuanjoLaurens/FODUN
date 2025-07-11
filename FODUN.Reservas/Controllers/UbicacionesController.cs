using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FODUN.Reservas.Data;
using FODUN.Reservas.Models;

namespace FODUN.Reservas.Controllers
{
    public class UbicacionesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UbicacionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _context.Ubicaciones.ToListAsync());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error al cargar las ubicaciones.";
                Console.WriteLine($"[Index] {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            try
            {
                var ubicacion = await _context.Ubicaciones.FirstOrDefaultAsync(m => m.UbicacionId == id);
                if (ubicacion == null)
                    return NotFound();

                return View(ubicacion);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error al mostrar detalles.";
                Console.WriteLine($"[Details] {ex.Message}");
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UbicacionId,Nombre,TipoUbicacion")] Ubicacion ubicacion)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(ubicacion);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Ubicación creada exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error al crear la ubicación.";
                Console.WriteLine($"[Create] {ex.Message}");
            }

            return View(ubicacion);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            try
            {
                var ubicacion = await _context.Ubicaciones.FindAsync(id);
                if (ubicacion == null)
                    return NotFound();

                return View(ubicacion);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error al cargar la ubicación para editar.";
                Console.WriteLine($"[Edit-GET] {ex.Message}");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UbicacionId,Nombre,TipoUbicacion")] Ubicacion ubicacion)
        {
            if (id != ubicacion.UbicacionId)
                return NotFound();

            try
            {
                if (ModelState.IsValid)
                {
                    _context.Update(ubicacion);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Ubicación actualizada exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Ubicaciones.Any(e => e.UbicacionId == id))
                    return NotFound();
                else
                    throw;
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error al editar la ubicación.";
                Console.WriteLine($"[Edit-POST] {ex.Message}");
            }

            return View(ubicacion);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            try
            {
                var ubicacion = await _context.Ubicaciones.FirstOrDefaultAsync(m => m.UbicacionId == id);
                if (ubicacion == null)
                    return NotFound();

                return View(ubicacion);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error al cargar la ubicación para eliminar.";
                Console.WriteLine($"[Delete-GET] {ex.Message}");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var ubicacion = await _context.Ubicaciones.FindAsync(id);
                if (ubicacion != null)
                {
                    _context.Ubicaciones.Remove(ubicacion);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Ubicación eliminada exitosamente.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Ubicación no encontrada para eliminar.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error al eliminar la ubicación.";
                Console.WriteLine($"[Delete-POST] {ex.Message}");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
