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
    public class MantencionsController : Controller
    {
        private readonly bodegaHerramientasContext _context;

        public MantencionsController(bodegaHerramientasContext context)
        {
            _context = context;
        }

        // GET: Mantencions
        public async Task<IActionResult> Index()
        {
            var bodegaHerramientasContext = _context.Mantencions.Include(m => m.Herramienta).Include(m => m.Usuario);
            return View(await bodegaHerramientasContext.ToListAsync());
        }

        // GET: Mantencions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Mantencions == null)
            {
                return NotFound();
            }

            var mantencion = await _context.Mantencions
                .Include(m => m.Herramienta)
                .Include(m => m.Usuario)
                .FirstOrDefaultAsync(m => m.MantencionId == id);
            if (mantencion == null)
            {
                return NotFound();
            }

            return View(mantencion);
        }

        // GET: Mantencions/Create
        public IActionResult Create()
        {
            ViewData["HerramientaId"] = new SelectList(_context.Herramienta, "HerramientaId", "HerramientaId");
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId");
            return View();
        }

        // POST: Mantencions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HerramientaId,UsuarioId,FechaInicio,FechaFin,Descripcion")] Mantencion mantencion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mantencion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HerramientaId"] = new SelectList(_context.Herramienta, "HerramientaId", "HerramientaId", mantencion.HerramientaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId", mantencion.UsuarioId);
            return View(mantencion);
        }

        // GET: Mantencions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Mantencions == null)
            {
                return NotFound();
            }

            var mantencion = await _context.Mantencions.FindAsync(id);
            if (mantencion == null)
            {
                return NotFound();
            }
            ViewData["HerramientaId"] = new SelectList(_context.Herramienta, "HerramientaId", "HerramientaId", mantencion.HerramientaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId", mantencion.UsuarioId);
            return View(mantencion);
        }

        // POST: Mantencions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MantencionId,HerramientaId,UsuarioId,FechaInicio,FechaFin,Descripcion")] Mantencion mantencion)
        {
            if (id != mantencion.MantencionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mantencion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MantencionExists(mantencion.MantencionId))
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
            ViewData["HerramientaId"] = new SelectList(_context.Herramienta, "HerramientaId", "HerramientaId", mantencion.HerramientaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId", mantencion.UsuarioId);
            return View(mantencion);
        }

        // GET: Mantencions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Mantencions == null)
            {
                return NotFound();
            }

            var mantencion = await _context.Mantencions
                .Include(m => m.Herramienta)
                .Include(m => m.Usuario)
                .FirstOrDefaultAsync(m => m.MantencionId == id);
            if (mantencion == null)
            {
                return NotFound();
            }

            return View(mantencion);
        }

        // POST: Mantencions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Mantencions == null)
            {
                return Problem("Entity set 'bodegaHerramientasContext.Mantencions'  is null.");
            }
            var mantencion = await _context.Mantencions.FindAsync(id);
            if (mantencion != null)
            {
                _context.Mantencions.Remove(mantencion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MantencionExists(int id)
        {
          return (_context.Mantencions?.Any(e => e.MantencionId == id)).GetValueOrDefault();
        }
    }
}
