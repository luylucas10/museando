using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Museando.Web.Models;

public class PecaMapeamento : IEntityTypeConfiguration<Peca>
{
	public void Configure(EntityTypeBuilder<Peca> builder)
	{
		builder.ToTable("Peca");
		builder.HasKey(x => x.Id);
		builder.Property(x => x.Titulo).IsRequired().HasMaxLength(50).HasColumnType("varchar");
		builder.Property(x => x.Descricao).IsRequired().HasMaxLength(500).HasColumnType("text");
		
		builder.HasMany(x => x.Imagens)
			.WithOne(x => x.Peca)
			.HasPrincipalKey(x => x.Id)
			.HasForeignKey(x => x.PecaId)
			.OnDelete(DeleteBehavior.Cascade);
		
		builder.HasMany(x => x.Videos)
			.WithOne(x => x.Peca)
			.HasPrincipalKey(x => x.Id)
			.HasForeignKey(x => x.PecaId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}

public class ImagemMapeamento : IEntityTypeConfiguration<Imagem>
{
	public void Configure(EntityTypeBuilder<Imagem> builder)
	{
		builder.ToTable("Imagem");
		builder.HasKey(x => x.Id);
		builder.Property(x => x.TipoConteudo).IsRequired().HasMaxLength(100).HasColumnType("varchar");
	}
}

public class VideoMapeamento : IEntityTypeConfiguration<Video>
{
	public void Configure(EntityTypeBuilder<Video> builder)
	{
		builder.ToTable("Video");
		builder.HasKey(x => x.Id);
		builder.Property(x => x.Link).IsRequired().HasMaxLength(300).HasColumnType("varchar");
	}
}