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
    public class ConfiguracaoController : Controller
    {
        private readonly GERVEICULOSContext _context;

        public ConfiguracaoController(GERVEICULOSContext context)
        {
            _context = context;
        }

        // GET: Configuracao
        public async Task<IActionResult> Index()
        {
              return _context.Configuracaos != null ? 
                          View(await _context.Configuracaos.ToListAsync()) :
                          Problem("Entity set 'GERVEICULOSContext.Configuracaos'  is null.");
        }

        // GET: Configuracao/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Configuracaos == null)
            {
                return NotFound();
            }

            var configuracao = await _context.Configuracaos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (configuracao == null)
            {
                return NotFound();
            }

            return View(configuracao);
        }

        // GET: Configuracao/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Configuracao/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DiasDeLocacao")] Configuracao configuracao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(configuracao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(configuracao);
        }

        // GET: Configuracao/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Configuracaos == null)
            {
                return NotFound();
            }

            var configuracao = await _context.Configuracaos.FindAsync(id);
            if (configuracao == null)
            {
                return NotFound();
            }
            return View(configuracao);
        }

        // POST: Configuracao/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DiasDeLocacao")] Configuracao configuracao)
        {
            if (id != configuracao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(configuracao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConfiguracaoExists(configuracao.Id))
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
            return View(configuracao);
        }

        // GET: Configuracao/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Configuracaos == null)
            {
                return NotFound();
            }

            var configuracao = await _context.Configuracaos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (configuracao == null)
            {
                return NotFound();
            }

            return View(configuracao);
        }

        // POST: Configuracao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Configuracaos == null)
            {
                return Problem("Entity set 'GERVEICULOSContext.Configuracaos'  is null.");
            }
            var configuracao = await _context.Configuracaos.FindAsync(id);
            if (configuracao != null)
            {
                _context.Configuracaos.Remove(configuracao);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConfiguracaoExists(int id)
        {
          return (_context.Configuracaos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
