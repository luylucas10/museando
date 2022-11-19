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
	public class VideoController : Controller
	{
		private readonly ApplicationDbContext _context;

		public VideoController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: Video
		public async Task<IActionResult> Index()
		{
			var applicationDbContext = _context.Video.Include(v => v.Peca);
			return View(await applicationDbContext.ToListAsync());
		}

		// GET: Video/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Video == null)
			{
				return NotFound();
			}

			var video = await _context.Video
				.Include(v => v.Peca)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (video == null)
			{
				return NotFound();
			}

			return View(video);
		}

		// GET: Video/Create
		public IActionResult Create()
		{
			ViewData["PecaId"] = new SelectList(_context.Peca, "Id", "Descricao");
			return View();
		}

		// POST: Video/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(NovoVideoViewModel video)
		{
			if (ModelState.IsValid)
			{
				_context.Add(new Video() { PecaId = video.PecaId, Link = video.Link, Cadastro = DateTime.Now });
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}

			ViewData["PecaId"] = new SelectList(_context.Peca, "Id", "Descricao", video.PecaId);
			return View(video);
		}

		// GET: Video/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Video == null)
			{
				return NotFound();
			}

			var video = await _context.Video
				.Include(v => v.Peca)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (video == null)
			{
				return NotFound();
			}

			return View(video);
		}

		// POST: Video/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Video == null)
			{
				return Problem("Entity set 'ApplicationDbContext.Video'  is null.");
			}

			var video = await _context.Video.FindAsync(id);
			if (video != null)
			{
				_context.Video.Remove(video);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool VideoExists(int id)
		{
			return (_context.Video?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}