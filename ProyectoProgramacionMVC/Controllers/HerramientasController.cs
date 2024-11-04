using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoProgramacionMVC.Models;

namespace ProyectoProgramacionMVC.Controllers
{
    public class HerramientasController : Controller
    {
        private readonly bodegaHerramientasContext _context;

        public HerramientasController(bodegaHerramientasContext context)
        {
            _context = context;
        }

        // GET: Herramientas
        public async Task<IActionResult> Index()
        {
            var bodegaHerramientasContext = _context.Herramienta.Include(h => h.Modelo);
            return View(await bodegaHerramientasContext.ToListAsync());
        }

        // GET: Herramientas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Herramienta == null)
            {
                return NotFound();
            }

            var herramientum = await _context.Herramienta
                .Include(h => h.Modelo)
                .FirstOrDefaultAsync(m => m.HerramientaId == id);
            if (herramientum == null)
            {
                return NotFound();
            }

            return View(herramientum);
        }

        // GET: Herramientas/Create
        public IActionResult Create()
        {
            ViewData["ModeloId"] = new SelectList(_context.Modelos, "ModeloId", "ModeloId");
            return View();
        }

        // POST: Herramientas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ModeloId,NumeroSerie,Estado,FechaIngreso")] Herramientum herramientum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(herramientum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ModeloId"] = new SelectList(_context.Modelos, "ModeloId", "ModeloId", herramientum.ModeloId);
            return View(herramientum);
        }

        // GET: Herramientas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Herramienta == null)
            {
                return NotFound();
            }

            var herramientum = await _context.Herramienta.FindAsync(id);
            if (herramientum == null)
            {
                return NotFound();
            }
            ViewData["ModeloId"] = new SelectList(_context.Modelos, "ModeloId", "ModeloId", herramientum.ModeloId);
            return View(herramientum);
        }

        // POST: Herramientas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HerramientaId,ModeloId,NumeroSerie,Estado,FechaIngreso")] Herramientum herramientum)
        {
            if (id != herramientum.HerramientaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(herramientum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HerramientumExists(herramientum.HerramientaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ModeloId"] = new SelectList(_context.Modelos, "ModeloId", "ModeloId", herramientum.ModeloId);
            return View(herramientum);
        }

        // GET: Herramientas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Herramienta == null)
            {
                return NotFound();
            }

            var herramientum = await _context.Herramienta
                .Include(h => h.Modelo)
                .FirstOrDefaultAsync(m => m.HerramientaId == id);
            if (herramientum == null)
            {
                return NotFound();
            }

            return View(herramientum);
        }

        // POST: Herramientas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Herramienta == null)
            {
                return Problem("Entity set 'bodegaHerramientasContext.Herramienta'  is null.");
            }
            var herramientum = await _context.Herramienta.FindAsync(id);
            if (herramientum != null)
            {
                _context.Herramienta.Remove(herramientum);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HerramientumExists(int id)
        {
          return (_context.Herramienta?.Any(e => e.HerramientaId == id)).GetValueOrDefault();
        }
    }
}
