using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using locacao_veiculo;

namespace locacao_veiculo.Controllers
{
    public class CarroController : Controller
    {
        private readonly GERVEICULOSContext _context;

        public CarroController(GERVEICULOSContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var gERVEICULOSContext = _context.Carros.Include(c => c.Cliente);
            return View(await gERVEICULOSContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Carros == null)
            {
                return NotFound();
            }

            var carro = await _context.Carros
                .Include(c => c.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carro == null)
            {
                return NotFound();
            }

            return View(carro);
        }

        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Marca,Modelo,ClienteId")] Carro carro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id", carro.ClienteId);
            return View(carro);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Carros == null)
            {
                return NotFound();
            }

            var carro = await _context.Carros.FindAsync(id);
            if (carro == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id", carro.ClienteId);
            return View(carro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Marca,Modelo,ClienteId")] Carro carro)
        {
            if (id != carro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarroExists(carro.Id))
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
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id", carro.ClienteId);
            return View(carro);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Carros == null)
            {
                return NotFound();
            }

            var carro = await _context.Carros
                .Include(c => c.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carro == null)
            {
                return NotFound();
            }

            return View(carro);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Carros == null)
            {
                return Problem("Entity set 'GERVEICULOSContext.Carros'  is null.");
            }
            var carro = await _context.Carros.FindAsync(id);
            if (carro != null)
            {
                _context.Carros.Remove(carro);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarroExists(int id)
        {
          return (_context.Carros?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
