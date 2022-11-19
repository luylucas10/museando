using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Museando.Web.Data;
using Museando.Web.Models;

namespace Museando.Web.Controllers
{
    public class PecaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PecaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Peca
        public async Task<IActionResult> Index()
        {
              return _context.Peca != null ? 
                          View(await _context.Peca.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Peca'  is null.");
        }

        // GET: Peca/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Peca == null)
            {
                return NotFound();
            }

            var peca = await _context.Peca
                .FirstOrDefaultAsync(m => m.Id == id);
            if (peca == null)
            {
                return NotFound();
            }

            return View(peca);
        }

        // GET: Peca/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Peca/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Descricao,Cadastro")] Peca peca)
        {
            if (ModelState.IsValid)
            {
                _context.Add(peca);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(peca);
        }

        // GET: Peca/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Peca == null)
            {
                return NotFound();
            }

            var peca = await _context.Peca.FindAsync(id);
            if (peca == null)
            {
                return NotFound();
            }
            return View(peca);
        }

        // POST: Peca/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Descricao,Cadastro")] Peca peca)
        {
            if (id != peca.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(peca);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PecaExists(peca.Id))
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
            return View(peca);
        }

        // GET: Peca/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Peca == null)
            {
                return NotFound();
            }

            var peca = await _context.Peca
                .FirstOrDefaultAsync(m => m.Id == id);
            if (peca == null)
            {
                return NotFound();
            }

            return View(peca);
        }

        // POST: Peca/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Peca == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Peca'  is null.");
            }
            var peca = await _context.Peca.FindAsync(id);
            if (peca != null)
            {
                _context.Peca.Remove(peca);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PecaExists(int id)
        {
          return (_context.Peca?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
