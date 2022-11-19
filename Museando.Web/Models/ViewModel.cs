using System.ComponentModel.DataAnnotations;

namespace Museando.Web.Models;

public class ErrorViewModel
{
	public string? RequestId { get; set; }
	public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}

public class PecaViewModel
{
	public int Id { get; set; }
	public string Titulo { get; set; }
	public string Descricao { get; set; }
}

public class NovoVideoViewModel
{
	[Required] public int PecaId { get; set; }
	[Required] public string Link { get; set; }
}