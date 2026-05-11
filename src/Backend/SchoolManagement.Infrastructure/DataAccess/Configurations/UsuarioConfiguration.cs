using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Infrastructure.DataAccess.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
	public virtual void Configure(EntityTypeBuilder<Usuario> builder)
	{
		builder.ToTable("Usuarios");

		builder.HasKey(p => p.Id);

		builder.OwnsOne(p => p.Nome, nome =>
		{
			nome.Property(n => n.Valor)
				.HasColumnName("Nome")
				.IsRequired()
				.HasMaxLength(255);
		});


		builder.Property(p => p.Senha)
			.HasColumnName("Senha")
			.IsRequired();


		builder.OwnsOne(p => p.Email, email =>
		{
			email.Property(e => e.Valor)
				.HasColumnName("Email")
				.IsRequired()
				.HasMaxLength(100);

			email.HasIndex(e => e.Valor).IsUnique();
		});
	}
}