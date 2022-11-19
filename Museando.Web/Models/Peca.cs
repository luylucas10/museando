namespace Museando.Web.Models;

public class Peca
{
	public Peca()
	{
		Imagens = new List<Imagem>();
		Videos = new List<Video>();
	}

	public int Id { get; set; }
	public string Titulo { get; set; }
	public string Descricao { get; set; }
	public DateTime Cadastro { get; set; }
	public virtual ICollection<Imagem> Imagens { get; set; }
	public virtual ICollection<Video> Videos { get; set; }
}

public class Imagem
{
	public int Id { get; set; }
	public int PecaId { get; set; }
	public byte[] Conteudo { get; set; }
	public string TipoConteudo { get; set; }
	public DateTime Cadastro { get; set; }
	public virtual Peca Peca { get; set; }
}

public class Video
{
	public int Id { get; set; }
	public int PecaId { get; set; }
	public string Link { get; set; }
	public DateTime Cadastro { get; set; }
	public virtual Peca Peca { get; set; }
}