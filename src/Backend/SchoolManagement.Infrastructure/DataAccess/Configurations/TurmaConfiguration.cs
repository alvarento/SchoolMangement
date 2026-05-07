using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Infrastructure.DataAccess.Configurations;

public class TurmaConfiguration : IEntityTypeConfiguration<Turma>
{
	public void Configure(EntityTypeBuilder<Turma> builder)
	{
		builder.ToTable("Turmas");

		builder.HasKey(t => t.Id);

		builder.Property(t => t.Codigo)
			.HasMaxLength(50)
			.IsRequired();

		builder.HasIndex(t => t.Codigo)
			.IsUnique();

		builder.Property(t => t.Turno)
			.HasConversion<string>()
			.IsRequired();

		builder.HasMany(t => t.Alunos)
			.WithOne()
			.HasForeignKey("TurmaId");
	}
}