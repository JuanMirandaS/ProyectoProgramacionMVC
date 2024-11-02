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
    public class MovimientosController : Controller
    {
        private readonly bodegaHerramientasContext _context;

        public MovimientosController(bodegaHerramientasContext context)
        {
            _context = context;
        }

        // GET: Movimientos
        public async Task<IActionResult> Index()
        {
            var bodegaHerramientasContext = _context.Movimientos.Include(m => m.Herramienta).Include(m => m.Usuario);
            return View(await bodegaHerramientasContext.ToListAsync());
        }

        // GET: Movimientos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Movimientos == null)
            {
                return NotFound();
            }

            var movimiento = await _context.Movimientos
                .Include(m => m.Herramienta)
                .Include(m => m.Usuario)
                .FirstOrDefaultAsync(m => m.MovimientoId == id);
            if (movimiento == null)
            {
                return NotFound();
            }

            return View(movimiento);
        }

        // GET: Movimientos/Create
        public IActionResult Create()
        {
            ViewData["HerramientaId"] = new SelectList(_context.Herramienta, "HerramientaId", "HerramientaId");
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId");
            return View();
        }

        // POST: Movimientos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovimientoId,HerramientaId,UsuarioId,TipoMovimiento,Fecha,Observacion")] Movimiento movimiento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movimiento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HerramientaId"] = new SelectList(_context.Herramienta, "HerramientaId", "HerramientaId", movimiento.HerramientaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId", movimiento.UsuarioId);
            return View(movimiento);
        }

        // GET: Movimientos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Movimientos == null)
            {
                return NotFound();
            }

            var movimiento = await _context.Movimientos.FindAsync(id);
            if (movimiento == null)
            {
                return NotFound();
            }
            ViewData["HerramientaId"] = new SelectList(_context.Herramienta, "HerramientaId", "HerramientaId", movimiento.HerramientaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId", movimiento.UsuarioId);
            return View(movimiento);
        }

        // POST: Movimientos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovimientoId,HerramientaId,UsuarioId,TipoMovimiento,Fecha,Observacion")] Movimiento movimiento)
        {
            if (id != movimiento.MovimientoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movimiento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovimientoExists(movimiento.MovimientoId))
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
            ViewData["HerramientaId"] = new SelectList(_context.Herramienta, "HerramientaId", "HerramientaId", movimiento.HerramientaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId", movimiento.UsuarioId);
            return View(movimiento);
        }

        // GET: Movimientos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Movimientos == null)
            {
                return NotFound();
            }

            var movimiento = await _context.Movimientos
                .Include(m => m.Herramienta)
                .Include(m => m.Usuario)
                .FirstOrDefaultAsync(m => m.MovimientoId == id);
            if (movimiento == null)
            {
                return NotFound();
            }

            return View(movimiento);
        }

        // POST: Movimientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Movimientos == null)
            {
                return Problem("Entity set 'bodegaHerramientasContext.Movimientos'  is null.");
            }
            var movimiento = await _context.Movimientos.FindAsync(id);
            if (movimiento != null)
            {
                _context.Movimientos.Remove(movimiento);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovimientoExists(int id)
        {
          return (_context.Movimientos?.Any(e => e.MovimientoId == id)).GetValueOrDefault();
        }
    }
}
