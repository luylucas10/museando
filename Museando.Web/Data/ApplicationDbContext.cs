using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Museando.Web.Models;

namespace Museando.Web.Data;

public class ApplicationDbContext : IdentityDbContext
{
	public DbSet<Peca> Peca { get; set; }
	public DbSet<Imagem> Imagem { get; set; }
	public DbSet<Video> Video { get; set; }

	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);
		builder.ApplyConfiguration(new PecaMapeamento());
		builder.ApplyConfiguration(new ImagemMapeamento());
		builder.ApplyConfiguration(new VideoMapeamento());
	}
}