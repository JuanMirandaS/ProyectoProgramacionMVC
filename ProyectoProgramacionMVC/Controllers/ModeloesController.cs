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
    public class ModeloesController : Controller
    {
        private readonly bodegaHerramientasContext _context;

        public ModeloesController(bodegaHerramientasContext context)
        {
            _context = context;
        }

        // GET: Modeloes
        public async Task<IActionResult> Index()
        {
            var bodegaHerramientasContext = _context.Modelos.Include(m => m.Marca);
            return View(await bodegaHerramientasContext.ToListAsync());
        }

        // GET: Modeloes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Modelos == null)
            {
                return NotFound();
            }

            var modelo = await _context.Modelos
                .Include(m => m.Marca)
                .FirstOrDefaultAsync(m => m.ModeloId == id);
            if (modelo == null)
            {
                return NotFound();
            }

            return View(modelo);
        }

        // GET: Modeloes/Create
        public IActionResult Create()
        {
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "MarcaId", "MarcaId");
            return View();
        }

        // POST: Modeloes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ModeloId,MarcaId,Nombre,Descripcion")] Modelo modelo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(modelo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "MarcaId", "MarcaId", modelo.MarcaId);
            return View(modelo);
        }

        // GET: Modeloes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Modelos == null)
            {
                return NotFound();
            }

            var modelo = await _context.Modelos.FindAsync(id);
            if (modelo == null)
            {
                return NotFound();
            }
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "MarcaId", "MarcaId", modelo.MarcaId);
            return View(modelo);
        }

        // POST: Modeloes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ModeloId,MarcaId,Nombre,Descripcion")] Modelo modelo)
        {
            if (id != modelo.ModeloId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(modelo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModeloExists(modelo.ModeloId))
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
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "MarcaId", "MarcaId", modelo.MarcaId);
            return View(modelo);
        }

        // GET: Modeloes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Modelos == null)
            {
                return NotFound();
            }

            var modelo = await _context.Modelos
                .Include(m => m.Marca)
                .FirstOrDefaultAsync(m => m.ModeloId == id);
            if (modelo == null)
            {
                return NotFound();
            }

            return View(modelo);
        }

        // POST: Modeloes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Modelos == null)
            {
                return Problem("Entity set 'bodegaHerramientasContext.Modelos'  is null.");
            }
            var modelo = await _context.Modelos.FindAsync(id);
            if (modelo != null)
            {
                _context.Modelos.Remove(modelo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModeloExists(int id)
        {
          return (_context.Modelos?.Any(e => e.ModeloId == id)).GetValueOrDefault();
        }
    }
}
