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
	public class ImagemController : Controller
	{
		private readonly ApplicationDbContext _context;

		public ImagemController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: Imagem
		public async Task<IActionResult> Index()
		{
			var applicationDbContext = _context.Imagem.Include(x => x.Peca);
			return View(await applicationDbContext.ToListAsync());
		}

		// GET: Imagem/Create
		public IActionResult Create()
		{
			ViewData["PecaId"] = new SelectList(_context.Peca, "Id", "Descricao");
			return View();
		}

		// POST: Imagem/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(int pecaId, List<IFormFile> files)
		{
			if (files.Any())
			{
				foreach (var img in files)
				{
					using var ms = new MemoryStream();
					await img.CopyToAsync(ms);
					_context.Add(new Imagem()
					{
						PecaId = pecaId,
						Conteudo = ms.ToArray(),
						TipoConteudo = img.ContentType,
						Cadastro = DateTime.Now
					});
				}

				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}

			ViewData["PecaId"] = new SelectList(_context.Peca, "Id", "Descricao", pecaId);
			return RedirectToAction(nameof(Create));
		}

		// GET: Imagem/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Imagem == null)
			{
				return NotFound();
			}

			var imagem = await _context.Imagem
				.Include(i => i.Peca)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (imagem == null)
			{
				return NotFound();
			}

			return View(imagem);
		}

		// POST: Imagem/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Imagem == null)
			{
				return Problem("Entity set 'ApplicationDbContext.Imagem'  is null.");
			}

			var imagem = await _context.Imagem.FindAsync(id);
			if (imagem != null)
			{
				_context.Imagem.Remove(imagem);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool ImagemExists(int id)
		{
			return (_context.Imagem?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}