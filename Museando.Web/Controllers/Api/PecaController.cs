using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Museando.Web.Data;
using Museando.Web.Models;

namespace Museando.Web.Controllers.Api
{
	[Route("api/[controller]")]
	[ApiController]
	public class PecaController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

		public PecaController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: api/PecaApi
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Peca>>> GetPeca()
		{
			if (_context.Peca == null)
			{
				return NotFound();
			}

			return await _context.Peca.Select(x => new Peca()
			{
				Id = x.Id,
				Titulo = x.Titulo,
				Descricao = x.Descricao
			}).ToListAsync();
		}

		// GET: api/PecaApi/5
		[HttpGet("{id}")]
		public async Task<ActionResult> GetPeca(int id)
		{
			if (_context.Peca == null)
			{
				return NotFound();
			}

			var peca = await _context.Peca.Select(x => new
			{
				x.Id,
				x.Titulo,
				x.Descricao,
				x.Cadastro,
				Imagems = x.Imagens.Select(
					a => new { imagem = Url.Action("Imagem", new { id = a.Id }) }),
				Videos = x.Videos.Select(a => a.Link)
			}).Where(x => x.Id == id).FirstOrDefaultAsync();

			if (peca == null)
			{
				return NotFound();
			}

			return Ok(peca);
		}

		public async Task<IActionResult> Imagem(int id)
		{
			var imagem = await _context.Imagem.FindAsync(id);
			return File(imagem.Conteudo, imagem.TipoConteudo);
		}
	}
}