using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Infrastructure.DataAccess.Configurations;

public class BoletimConfiguration : IEntityTypeConfiguration<Boletim>
{
	public void Configure(EntityTypeBuilder<Boletim> builder)
	{
		builder.ToTable("Boletim");

		builder.HasKey(b => b.Id);

		builder.HasIndex(b => b.AlunoId)
			.IsUnique();

		builder.HasMany(b => b.DisciplinasNotas)
			.WithOne(n => n.Boletim)
			.HasForeignKey(n => n.BoletimId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}