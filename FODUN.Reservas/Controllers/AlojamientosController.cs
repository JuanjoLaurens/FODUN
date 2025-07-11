using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FODUN.Reservas.Data;
using FODUN.Reservas.Models;
using Microsoft.AspNetCore.Authorization;
using FODUN.Reservas.Services; 

namespace FODUN.Reservas.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AlojamientosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IReservaService _reservaService;

        public AlojamientosController(ApplicationDbContext context, IReservaService reservaService)
        {
            _context = context;
            _reservaService = reservaService; 
        }

        // GET: Alojamientos
        public async Task<IActionResult> Index()
        {
            try
            {
                var applicationDbContext = _context.Alojamientos.Include(a => a.Ubicacion);
                return View(await applicationDbContext.ToListAsync());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error al cargar alojamientos.";
                Console.WriteLine($"[Index] Error: {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: Alojamientos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    TempData["ErrorMessage"] = "ID no proporcionado.";
                    return NotFound();
                }

                var alojamiento = await _context.Alojamientos
                    .Include(a => a.Ubicacion)
                    .FirstOrDefaultAsync(m => m.AlojamientoId == id);
                if (alojamiento == null)
                {
                    TempData["ErrorMessage"] = "Alojamiento no encontrado.";
                    return NotFound();
                }

                return View(alojamiento);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error al mostrar detalles del alojamiento.";
                Console.WriteLine($"[Details] Error: {ex.Message}");
                return RedirectToAction("Index");
            }
        }

        // GET: Alojamientos/Create
        public async Task<IActionResult> Create()
        {
            try
            {
                var ubicaciones = await _reservaService.GetAllUbicaciones();
                ViewBag.UbicacionId = new SelectList(ubicaciones, "UbicacionId", "Nombre");

                var nuevoAlojamiento = new Alojamiento
                {
                    TarifaDiaOrdinario = 0,
                    TarifaDiaEspecial = 0,
                    Activo = true,
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now,
                    CamasDobles = 0,
                    CamasSencillas = 0,
                    Camarotes = 0,
                    TieneBanoPrivado = false,
                    TieneNevera = false,
                    TieneTelevisor = false,
                    TieneCocinetaEquipada = false,
                    TieneSalaEstar = false,
                    TieneTerraza = false
                };

                return View(nuevoAlojamiento);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error al cargar el formulario de creación de alojamiento.";
                Console.WriteLine($"[Create GET] Error: {ex.Message}");
                return RedirectToAction("Index");
            }
        }

        // POST: Alojamientos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AlojamientoId,UbicacionId,Nombre,Descripcion,TipoAlojamiento,CapacidadMaximaPersonas,CamasDobles,CamasSencillas,Camarotes,TieneBanoPrivado,TieneNevera,TieneTelevisor,TieneCocinetaEquipada,TieneSalaEstar,TieneTerraza,TarifaDiaOrdinario,TarifaDiaEspecial,Activo")] Alojamiento alojamiento)
        {
            try
            {
                alojamiento.FechaCreacion = DateTime.Now;
                alojamiento.FechaActualizacion = DateTime.Now;

                if (ModelState.IsValid)
                {
                    _context.Add(alojamiento);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Alojamiento creado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }

                var ubicaciones = await _reservaService.GetAllUbicaciones();
                ViewBag.UbicacionId = new SelectList(ubicaciones, "UbicacionId", "Nombre", alojamiento.UbicacionId);

                TempData["ErrorMessage"] = "Error de validación al crear el alojamiento. Por favor, revise los campos.";
                return View(alojamiento);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error al crear alojamiento. Detalles: " + ex.Message;
                Console.WriteLine($"[Create POST] Error: {ex.Message}");

                var ubicaciones = await _reservaService.GetAllUbicaciones();
                ViewBag.UbicacionId = new SelectList(ubicaciones, "UbicacionId", "Nombre", alojamiento.UbicacionId);

                return View(alojamiento);
            }
        }

        // GET: Alojamientos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    TempData["ErrorMessage"] = "ID no proporcionado para edición.";
                    return NotFound();
                }

                var alojamiento = await _context.Alojamientos.FindAsync(id);
                if (alojamiento == null)
                {
                    TempData["ErrorMessage"] = "Alojamiento no encontrado para edición.";
                    return NotFound();
                }

                var ubicaciones = await _reservaService.GetAllUbicaciones();
                ViewBag.UbicacionId = new SelectList(ubicaciones, "UbicacionId", "Nombre", alojamiento.UbicacionId);

                return View(alojamiento);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error al cargar el formulario de edición.";
                Console.WriteLine($"[Edit GET] Error: {ex.Message}");
                return RedirectToAction("Index");
            }
        }

        // POST: Alojamientos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, [Bind("AlojamientoId,UbicacionId,Nombre,Descripcion,TipoAlojamiento,CapacidadMaximaPersonas,CamasDobles,CamasSencillas,Camarotes,TieneBanoPrivado,TieneNevera,TieneTelevisor,TieneCocinetaEquipada,TieneSalaEstar,TieneTerraza,TarifaDiaOrdinario,TarifaDiaEspecial,Activo")] Alojamiento alojamiento)
        {
            if (id != alojamiento.AlojamientoId)
            {
                TempData["ErrorMessage"] = "ID de alojamiento no coincide.";
                return NotFound();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var alojamientoToUpdate = await _context.Alojamientos.AsNoTracking().FirstOrDefaultAsync(a => a.AlojamientoId == id);
                    if (alojamientoToUpdate == null)
                    {
                        TempData["ErrorMessage"] = "Alojamiento no encontrado para actualizar.";
                        return NotFound();
                    }

                    alojamiento.FechaCreacion = alojamientoToUpdate.FechaCreacion; // Preservar la fecha de creación
                    alojamiento.FechaActualizacion = DateTime.Now;

                    _context.Update(alojamiento);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Alojamiento actualizado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }

                var ubicaciones = await _reservaService.GetAllUbicaciones();
                ViewBag.UbicacionId = new SelectList(ubicaciones, "UbicacionId", "Nombre", alojamiento.UbicacionId);

                TempData["ErrorMessage"] = "Error de validación al actualizar el alojamiento. Por favor, revise los campos.";
                return View(alojamiento);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlojamientoExists(alojamiento.AlojamientoId))
                {
                    TempData["ErrorMessage"] = "El alojamiento no existe o ha sido eliminado por otro usuario.";
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error al actualizar alojamiento. Detalles: " + ex.Message;
                Console.WriteLine($"[Edit POST] Error: {ex.Message}");

                var ubicaciones = await _reservaService.GetAllUbicaciones();
                ViewBag.UbicacionId = new SelectList(ubicaciones, "UbicacionId", "Nombre", alojamiento.UbicacionId);

                return View(alojamiento);
            }
        }

        // GET: Alojamientos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    TempData["ErrorMessage"] = "ID no proporcionado para eliminación.";
                    return NotFound();
                }

                var alojamiento = await _context.Alojamientos
                    .Include(m => m.Ubicacion)
                    .FirstOrDefaultAsync(m => m.AlojamientoId == id);

                if (alojamiento == null) return NotFound();

                return View(alojamiento);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error al cargar eliminación.";
                Console.WriteLine($"[Delete GET] Error: {ex.Message}");
                return RedirectToAction("Index");
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var alojamiento = await _context.Alojamientos.FindAsync(id);
                if (alojamiento != null)
                {
                    _context.Alojamientos.Remove(alojamiento);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Alojamiento eliminado.";
                }
                else TempData["ErrorMessage"] = "Alojamiento no encontrado.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error al eliminar alojamiento.";
                Console.WriteLine($"[Delete POST] Error: {ex.Message}");
            }

            return RedirectToAction(nameof(Index));
        }

        private bool AlojamientoExists(int id)
        {
            return _context.Alojamientos.Any(e => e.AlojamientoId == id);
        }
    }
}